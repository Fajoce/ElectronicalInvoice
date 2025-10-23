using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models.Detalles;
using WebHostApplication.Models.Detalles2;

namespace WebHostApplication.Controllers
{
    public class FacturasController : Controller
    {
        private readonly WebHostDbcontext _context;

        public FacturasController(WebHostDbcontext context)
        {
            _context = context;
        }

        // =========================
        // GET: Facturas/Create
        // =========================
        public async Task<IActionResult> Create()
        {
            // Cargar combos para relaciones
            ViewData["IdCliente"] = new SelectList(await _context.Cliente.ToListAsync(), "IdCliente", "Nombre");
            ViewData["IdCliente2"] = new SelectList(await _context.Clientes2.ToListAsync(), "IdCliente2", "Nombre");
            ViewData["IdVendedor"] = new SelectList(await _context.Vendedores.ToListAsync(), "IdVendedor", "Nombre");
            ViewData["IdCajero"] = new SelectList(await _context.Cajeros.ToListAsync(), "IdCajero", "Nombre");

            return View();
        }

        // =========================
        // POST: Facturas/Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Detalles.Detalles2 factura, List<Detalles1> detalles)
        {
            if (ModelState.IsValid)
            {
                // Guardar factura
                _context.Add(factura);
                await _context.SaveChangesAsync();

                // Guardar los detalles con referencia al número de factura
                foreach (var item in detalles)
                {
                    item.NumeroFactura = factura.NumeroFactura;
                   // _context.Detalle1.Add(item);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Si hay error recargamos combos
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Nombre", factura.IdCliente);
            ViewData["IdCliente2"] = new SelectList(_context.Clientes2, "IdCliente2", "Nombre", factura.IdCliente2);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "IdVendedor", "Nombre", factura.IdVendedor);
            ViewData["IdCajero"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre", factura.IdCajero);

            return View(factura);
        }

        // =========================
        // GET: Facturas
        // =========================
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Detalle2
                .Include(f => f.Cliente)
                //.Include(f => f.IdCliente2)
                .Include(f => f.Vendedor)
                .Include(f => f.Cajero)
                .Include(f => f.Detalles) // incluir los ítems de la factura
                .ToListAsync();

            return View(facturas);
        }
    }
}

