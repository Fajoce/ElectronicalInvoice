using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models.DetallesPedido;
using WebHostApplication.Models.Pedidos;
using WebHostApplication.ViewModels.Pedidos;

namespace WebHostApplication.Controllers
{
    public class PedidosController : Controller
    {
        private readonly WebHostDbcontext _context;


        public PedidosController(WebHostDbcontext context)
        {
            _context = context;
        }
        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Vendedor)
                .Include(p => p.Detalles)
                .ToListAsync();

            return View(pedidos);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            var clientes = _context.Cliente
        .Select(c => new
        {
            IdCliente = c.IdCliente,
            NombreCompleto = c.Nombre + " " + c.Apellido1 + " " + c.Apellido2
        })
        .ToList();

            ViewBag.Clientes = new SelectList(clientes, "IdCliente", "NombreCompleto");

            var vendedores = _context.Vendedores
                .Select(v => new
                {
                    IdVendedor = v.IdVendedor,
                    NombreCompleto = v.Nombre
                })
                .ToList();

            ViewBag.Vendedores = new SelectList(vendedores, "IdVendedor", "NombreCompleto");

            var productos = _context.Productos
                .Select(p => new
                {
                    Id = p.Id,
                    Nombre = p.Descripcion,
                    Precio = p.Precio1
                })
                .ToList();

            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            return View(new PedidosDTO());
        }

        // POST: Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidosDTO pedidoDto)
        {
            pedidoDto.Detalles ??= new List<DetallesDTO>();

            if (!pedidoDto.Detalles.Any(d => !string.IsNullOrEmpty(d.ProductoId) && d.Cantidad > 0))
            {
                ModelState.AddModelError("", "Debe agregar al menos un producto.");
            }

            if (ModelState.IsValid)
            {
                var pedido = new Pedido
                {
                    IdCliente = pedidoDto.IdCliente,
                    IdVendedor = pedidoDto.IdVendedor,
                    Fecha = pedidoDto.Fecha,
                    FormaPago = pedidoDto.FormaPago,
                    Detalles = new List<PedidoDetalles>()
                };

                foreach (var d in pedidoDto.Detalles.Where(x => !string.IsNullOrEmpty(x.ProductoId) && x.Cantidad > 0))
                {
                    var producto = await _context.Productos.FindAsync(d.ProductoId);
                    if (producto == null) continue;

                    pedido.Detalles.Add(new PedidoDetalles
                    {
                        ProductoId = d.ProductoId,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio,
                        SubTotal = d.Cantidad * producto.Precio1
                    });
                }

                pedido.Total = pedido.Detalles.Sum(x => x.SubTotal);

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(_context.Cliente, "Id", "Nombre", pedidoDto.IdCliente);
            ViewBag.Vendedores = new SelectList(_context.Vendedores, "Id", "Nombre", pedidoDto.IdVendedor);
            ViewBag.Productos = new SelectList(_context.Productos, "Id", "Nombre");

            return View(pedidoDto);
        }     

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)  // EF Core reconoce correctamente la navegación
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound();

            return View(pedido);
        }
        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound();

            var pedidoDto = new PedidosDTO
            {
                IdCliente = pedido.IdCliente,
                IdVendedor = pedido.IdVendedor,
                Fecha = pedido.Fecha,
                FormaPago = pedido.FormaPago,
                Detalles = pedido.Detalles.Select(d => new DetallesDTO
                {
                    IdDetalle = d.Id,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                }).ToList()
            };

            ViewBag.Clientes = new SelectList(_context.Cliente, "IdCliente", "Nombre", pedido.IdCliente);
            ViewBag.Vendedores = new SelectList(_context.Vendedores, "IdVendedor", "Nombre", pedido.IdVendedor);
            ViewBag.Productos = new SelectList(_context.Productos, "Id", "Descripcion");

            return View(pedidoDto);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PedidosDTO pedidoDto)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound();

            if (!pedidoDto.Detalles.Any(d => !string.IsNullOrEmpty(d.ProductoId) && d.Cantidad > 0))
                ModelState.AddModelError("", "Debe agregar al menos un producto.");

            if (ModelState.IsValid)
            {
                // Actualizar campos generales
                pedido.IdCliente = pedidoDto.IdCliente;
                pedido.IdVendedor = pedidoDto.IdVendedor;
                pedido.Fecha = pedidoDto.Fecha;
                pedido.FormaPago = pedidoDto.FormaPago;

                // Eliminar detalles existentes
                _context.PedidoDetalles.RemoveRange(pedido.Detalles);

                // Agregar detalles nuevos
                pedido.Detalles = pedidoDto.Detalles
                    .Where(d => !string.IsNullOrEmpty(d.ProductoId) && d.Cantidad > 0)
                    .Select(d => new PedidoDetalles
                    {
                        ProductoId = d.ProductoId,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio,
                        SubTotal = d.Cantidad * d.Precio
                    }).ToList();

                pedido.Total = pedido.Detalles.Sum(x => x.SubTotal);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(_context.Cliente, "IdCliente", "Nombre", pedidoDto.IdCliente);
            ViewBag.Vendedores = new SelectList(_context.Vendedores, "IdVendedor", "Nombre", pedidoDto.IdVendedor);
            ViewBag.Productos = new SelectList(_context.Productos, "Id", "Descripcion");

            return View(pedidoDto);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound();

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido != null)
            {
                _context.PedidoDetalles.RemoveRange(pedido.Detalles);
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }    
}
