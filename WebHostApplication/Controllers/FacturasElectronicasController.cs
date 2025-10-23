using Humanizer;
using iText.Kernel.Pdf.Canvas.Wmf;
using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using WebHostApplication.Data;
using WebHostApplication.Models;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.Vendedores;
using WebHostApplication.ViewModels.Facturas;
using WebHostApplication.ViewModels.OrdenPedidos;

namespace WebHostApplication.Controllers
{
    public class FacturasElectronicasController : Controller
    {
        private readonly WebHostDbcontext _context;

        public FacturasElectronicasController(WebHostDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Clientes"] = new SelectList(await _context.Cliente.ToListAsync() ?? new List<Cliente>(), "IdCliente", "Nombre");
            ViewData["Clientes2"] = new SelectList(await _context.Clientes2.ToListAsync() ?? new List<Cliente2>(), "IdCliente2", "Nombre");
            ViewData["Vendedores"] = new SelectList(await _context.Vendedor2.ToListAsync() ?? new List<Vendedor2>(), "IdVendedor", "Nombre");
            ViewData["Cajeros"] = new SelectList(await _context.Cajeros.ToListAsync() ?? new List<Cajero>(), "IdCajero", "Nombre");
            ViewData["Productos"] = new SelectList(await _context.Productos.ToListAsync() ?? new List<Productos>(), "Id", "Descripcion");
            ViewData["FormasPago"] = new SelectList(new[] { "Efectivo", "Tcredito", "Tdebito", "Transferencia", "Credito" });
            return View(new FacturaCreateDTO
            {
                Fecha = DateTime.Now,
                Detalles = new List<DetalleCreateDTO> { new DetalleCreateDTO() }
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacturaCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                await CargarSelects();
                return View(model);
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1️⃣ Calcular totales
                    model.Subtotal = model.Detalles.Sum(d => (d.Precio ?? 0) * (d.Cantidad ?? 0));
                    model.Dinero = (model.Efectivo ?? 0);

                    // 2️⃣ Insertar cabecera de factura
                    FormattableString sqlFactura = $@"
                INSERT INTO detalle2
                (fecha, idcliente, idcliente2, idvendedor, idcajero,
                 servicio1, servicio2, servicio3, subtotal, subtotal2, subtotal3,
                 dinero, efectivo, tcredito, tdebito, credito, transferencia, electronica)
                VALUES
                ({model.Fecha}, {model.IdCliente}, 1,
                 {model.IdVendedor}, {model.IdCajero}, {model.Servicio1 ?? 0},
                 {model.Servicio2 ?? 0}, {model.Servicio3 ?? 0}, {model.Subtotal ?? 0},
                 {model.Subtotal2 ?? 0}, {model.Subtotal3 ?? 0}, {model.Dinero ?? 0},
                 {model.Efectivo ?? 0}, {model.Tcredito ?? 0}, {model.Tdebito ?? 0},
                 {model.Credito ?? 0}, {model.Transferencia ?? 0}, 1);";

                    await _context.Database.ExecuteSqlInterpolatedAsync(sqlFactura);

                    // 3️⃣ Obtener el ID de la factura recién creada
                    var cmd = _context.Database.GetDbConnection().CreateCommand();
                    cmd.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction();
                    cmd.CommandText = "SELECT LAST_INSERT_ID();";

                    if (cmd.Connection.State != System.Data.ConnectionState.Open)
                        await cmd.Connection.OpenAsync();

                    var numeroFactura = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    // 4️⃣ Recorrer los detalles
                    foreach (var d in model.Detalles)
                    {
                        d.Subtotal = (d.Precio ?? 0) * (d.Cantidad ?? 0);
                        decimal utilidad = ((d.Precio ?? 0) - (d.Costo ?? 0)) * (d.Cantidad ?? 0);

                        // 🔹 Insertar detalle de factura
                        FormattableString sqlDetalle = $@"
                    INSERT INTO detalle1
                    (numerofactura, codigofijo, descripcion, precio, can, subtotal, costo, idvendedor2)
                    VALUES
                    ({numeroFactura}, {d.CodigoFijo ?? ""}, {d.Descripcion ?? ""}, {d.Precio ?? 0},
                     {d.Cantidad ?? 0}, {d.Subtotal ?? 0}, {d.Costo ?? 0}, 1);";

                        await _context.Database.ExecuteSqlInterpolatedAsync(sqlDetalle);

                        // 🔹 Insertar movimiento en Kardex
                        FormattableString sqlKardex = $@"
                    INSERT INTO kardex
                    (Fecha, Tipo_Movimiento, Producto_Id, Descripcion, Cantidad,
                     Precio_Venta, Precio_Costo, Utilidad, Existencia_Antes, Existencia_Despues,
                    Referencia_Documento, Usuario,Observacion )
                    VALUES
                    ({DateTime.Now}, {"VENTA"}, {d.CodigoFijo ?? ""}, {d.Descripcion ?? ""},
                     {d.Cantidad ?? 0}, {d.Precio ?? 0}, {d.Costo ?? 0}, {utilidad}, {d.Exis}, {d.Exis - d.Cantidad}, {numeroFactura},'1','Venta de productos');";

                        await _context.Database.ExecuteSqlInterpolatedAsync(sqlKardex);
                    }
                    if (model.Pedido != null && model.Pedido > 0)
                    {
                        FormattableString sqlPedidos = $@"
                        UPDATE Pedidos
                        SET Estado = 1
                        WHERE IdPedido = {model.Pedido};";

                        await _context.Database.ExecuteSqlInterpolatedAsync(sqlPedidos);
                    }

                    // 5️⃣ Confirmar transacción
                    await transaction.CommitAsync();

                    TempData["Success"] = "✅ Factura creada correctamente y registrada en Kardex.";

                    // 6️⃣ Recargar selects y limpiar formulario
                    await CargarSelects();
                    var nuevoModelo = new FacturaCreateDTO
                    {
                        Fecha = DateTime.Now,
                        Detalles = new List<DetalleCreateDTO> { new DetalleCreateDTO() }
                    };

                    return View(nuevoModelo);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Error al crear factura: " + ex.Message);
                    Console.WriteLine(ex.Message);
                    await CargarSelects();
                    return View(model);
                }
            });
        }
        // Método auxiliar para recargar selects cuando falle la validación
        private async Task CargarSelects()
        {
            ViewData["Clientes"] = new SelectList(await _context.Cliente.ToListAsync(), "IdCliente", "Nombre");
            ViewData["Clientes2"] = new SelectList(await _context.Clientes2.ToListAsync(), "Id", "Nombre");
            ViewData["Vendedores"] = new SelectList(await _context.Vendedor2.ToListAsync(), "IdVendedor", "Nombre");
            ViewData["Vendedores2"] = new SelectList(await _context.Vendedores.ToListAsync(), "IdVendedor", "Nombre");
            ViewData["Cajeros"] = new SelectList(await _context.Cajeros.ToListAsync(), "IdCajero", "Nombre");
            ViewData["Productos"] = new SelectList(await _context.Productos.ToListAsync(), "Id", "Descripcion");
            ViewData["FormasPago"] = new SelectList(new[] { "Efectivo", "Tcredito", "Tdebito", "Transferencia", "Credito" });
        }
        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Detalle2
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .Include(f => f.Cajero)
                .Include(f => f.Detalles)
                .ToListAsync();

            return View(facturas);
        }
        //Pedidos
        [HttpGet]
        public async Task<IActionResult> FromPedido(int numeroPedido)
        {
            var pedido = await _context.Pedidos2
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdPedido == numeroPedido);

            if (pedido == null) return NotFound();

            var facturaModel = new FacturaCreateDTO
            {
                Pedido = numeroPedido,
                Fecha = DateTime.Now,
                IdCliente = pedido.IdCliente,
                IdCliente2 = pedido.IdCliente2,
                IdCajero = pedido.IdCajero,
                IdVendedor = pedido.IdVendedor ?? 1,
                Efectivo = pedido.Efectivo,
                Dinero = pedido.Dinero,
                Tcredito = pedido.Tcredito,
                Tdebito = pedido.Tdebito,
                Credito = pedido.Credito,
                Transferencia = pedido.Transferencia,
                Subtotal = pedido.Detalles.Sum(d => d.Precio * d.Cantidad),
                Detalles = pedido.Detalles.Select(d => new DetalleCreateDTO
                {
                    IdProducto = d.CodigoFijo,
                    CodigoFijo = d.CodigoFijo,
                    Referencia = d.Referencia,
                    Descripcion = d.Descripcion,
                    Precio = d.Precio,
                    Cantidad = d.Cantidad,                                      
                    Subtotal = d.Precio * d.Cantidad,
                    Costo = d.Costo,
                    Exis = d.Existencia,
                    IdVendedor2 = d.IdVendedor2
                }).ToList()
            };

            
            if (facturaModel.Detalles == null || !facturaModel.Detalles.Any())
            {
                facturaModel.Detalles = new List<DetalleCreateDTO> { new DetalleCreateDTO() };
            }

            await CargarSelects();
            return View("Create", facturaModel);
        }
    }
}



