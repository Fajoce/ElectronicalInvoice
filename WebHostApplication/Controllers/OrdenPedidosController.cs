using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using WebHostApplication.Data;
using WebHostApplication.Models;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.OrdenPedidos;
using WebHostApplication.Models.Pedidos;
using WebHostApplication.Models.Vendedores;
using WebHostApplication.ViewModels.OrdenPedidos;

namespace WebHostApplication.Controllers
{
    public class OrdenPedidosController : Controller
    {
        private readonly WebHostDbcontext _context;

        public OrdenPedidosController(WebHostDbcontext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos2
                .Include(p => p.Clientes)
                .Where(p=> p.Estado == false)
                .Include(p=>p.Vendedores)
                .Include(f => f.Cajero)
                .Include(p => p.Detalles)
                .OrderBy(p => p.IdPedido)
                
                .ToListAsync();

            return View(pedidos);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["Vendedores"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre");
            ViewData["Clientes"] = new SelectList(_context.Cliente, "IdCliente", "Nombre");
            ViewData["Clientes2"] = new SelectList(_context.Clientes2, "IdCliente2", "Nombre");
            ViewData["Vendedores"] = new SelectList(_context.Vendedores, "IdVendedor", "Nombre");
            ViewData["Cajeros"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre");
            ViewData["Productos"] = new SelectList(_context.Productos, "Id", "Descripcion");
            ViewData["FormasPago"] = new SelectList(new[] { "Efectivo", "Tcredito", "Tdebito", "Transferencia", "Credito" });
            var pedido = new CreatePedidosDTO
            {
                Fecha = DateTime.Now,
                Detalles = new List<PedidosDetalleDTO>
        {
            new PedidosDetalleDTO()
        }       
            };

            return View(pedido);
        }

        // POST: Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePedidosDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState
        .Where(kv => kv.Value.Errors.Count > 0)
        .Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value.Errors.Select(e => e.ErrorMessage))}");

                TempData["ModelErrors"] = string.Join(" | ", errores);
                await CargarSelects();
                return View(model);
            }

            var pedido = new Pedidos
            {
                Fecha = model.Fecha,
               IdCliente = model.IdCliente,
                IdCliente2 = model.IdCliente2,
                IdVendedor = 1,
                IdCajero = model.IdCajero,
                Subtotal = model.Detalles.Sum(d => (d.Precio ?? 0) * (d.Cantidad ?? 0)),
                Efectivo = model.Efectivo,
                Dinero = model.Dinero,
                //Total = model.Detalles.Sum(d => (d.Precio1 ?? 0) * (d.Cantidad ?? 0)),
                Detalles = model.Detalles.Select(d => new PedidosDetalles
                {
                    IdProducto = d.IdProducto,
                    CodigoFijo = d.CodigoFijo,
                    Referencia = d.Referencia,
                    Descripcion = d.Descripcion,
                    Precio = d.Precio ?? 0,
                    Cantidad = d.Cantidad ?? 0,
                    Subtotal = (d.Precio ?? 0) * (d.Cantidad ?? 0),
                    Costo = d.Costo,
                    Existencia = d.Existencia ?? 0                                       
                  
                }).ToList()
            };

            _context.Add(pedido);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Pedido creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarSelects()
        {
            var clientes = (await _context.Cliente.ToListAsync()) ?? new List<Cliente>();
            var clientes2 = (await _context.Clientes2.ToListAsync()) ?? new List<Cliente2>();
            var vendedores = (await _context.Vendedor2.ToListAsync()) ?? new List<Vendedor2>();
            var cajeros = (await _context.Cajeros.ToListAsync()) ?? new List<Cajero>();
            var productos = (await _context.Productos.ToListAsync()) ?? new List<Productos>();

            ViewData["Clientes"] = new SelectList(clientes, "IdCliente", "Nombre");
            ViewData["Clientes2"] = new SelectList(clientes2, "IdCliente2", "Nombre");
            ViewData["Vendedores"] = new SelectList(vendedores, "IdVendedor", "Nombre");
          
            ViewData["Cajeros"] = new SelectList(cajeros, "IdCajero", "Nombre");
            ViewData["Productos"] = new SelectList(productos, "Id", "Descripcion");
        }
    }
}
