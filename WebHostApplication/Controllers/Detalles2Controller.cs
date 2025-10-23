using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using WebHostApplication.Data;
using WebHostApplication.Models;
using WebHostApplication.Models.Detalles2;

namespace WebHostApplication.Controllers
{
    public class Detalles2Controller : Controller
    {
        private readonly WebHostDbcontext _context;

        public Detalles2Controller(WebHostDbcontext context)
        {
            _context = context;
        }

        // GET: Detalle2
        public async Task<IActionResult> Index()
        {
            var detalles = _context.Detalle2
                .Include(d => d.Vendedor)
                .Include(d => d.Cliente)
                .Include(d => d.Cliente2)
                .Include(d => d.Cajero)
                .Include(d => d.Detalles);
            return View(await detalles.ToListAsync());
        }

        // GET: Detalle2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var detalle2 = await _context.Detalle2
                .Include(d => d.Vendedor)
                .Include(d => d.Cliente)
                .Include(d => d.Cliente2)
                .Include(d => d.Cajero)
                .Include(d => d.Detalles)
                .FirstOrDefaultAsync(m => m.NumeroFactura == id);

            if (detalle2 == null) return NotFound();

            return View(detalle2);
        }

        // GET: Detalle2/Create
        public IActionResult Create()
        {
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "IdVendedor", "Nombre");
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Nombre");
            ViewData["IdCliente2"] = new SelectList(_context.Clientes2, "IdCliente2", "Nombre");
            ViewData["IdCajero"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre");
            return View();
        }

        // POST: Detalle2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Detalles.Detalles2 detalle2, List<Detalle1> detalle1s)
        {
            if (ModelState.IsValid)
            {
                // Agregamos Detalle2
                _context.Add(detalle2);
                await _context.SaveChangesAsync();

                // Agregamos los Detalle1 relacionados
                if (detalle1s != null && detalle1s.Count > 0)
                {
                    foreach (var item in detalle1s)
                    {
                        item.NumeroFactura = detalle2.NumeroFactura; // FK
                        _context.Detalle1.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "IdVendedor", "Nombre", detalle2.IdVendedor);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Nombre", detalle2.IdCliente);
            ViewData["IdCliente2"] = new SelectList(_context.Clientes2, "IdCliente2", "Nombre", detalle2.IdCliente2);
            ViewData["IdCajero"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre", detalle2.IdCajero);

            return View(detalle2);
        }

        // GET: Detalle2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var detalle2 = await _context.Detalle2
                .Include(d => d.Detalles)
                .FirstOrDefaultAsync(d => d.NumeroFactura == id);

            if (detalle2 == null) return NotFound();

            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "IdVendedor", "Nombre", detalle2.IdVendedor);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Nombre", detalle2.IdCliente);
            ViewData["IdCliente2"] = new SelectList(_context.Clientes2, "IdCliente2", "Nombre", detalle2.IdCliente2);
            ViewData["IdCajero"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre", detalle2.IdCajero);

            return View(detalle2);
        }

        // POST: Detalle2/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Detalles.Detalles2 detalle2, List<Detalle1> detalle1s)
        {
            if (id != detalle2.NumeroFactura) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle2);

                    // Actualizar Detalle1s: eliminamos y agregamos de nuevo para simplificar
                    var existentes = _context.Detalle1.Where(d => d.NumeroFactura == id);
                    _context.Detalle1.RemoveRange(existentes);

                    if (detalle1s != null)
                    {
                        foreach (var item in detalle1s)
                        {
                            item.NumeroFactura = detalle2.NumeroFactura;
                            _context.Detalle1.Add(item);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Detalle2.Any(e => e.NumeroFactura == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "IdVendedor", "Nombre", detalle2.IdVendedor);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Nombre", detalle2.IdCliente);
            ViewData["IdCliente2"] = new SelectList(_context.Clientes2, "IdCliente2", "Nombre", detalle2.IdCliente2);
            ViewData["IdCajero"] = new SelectList(_context.Cajeros, "IdCajero", "Nombre", detalle2.IdCajero);

            return View(detalle2);
        }

        // GET: Detalle2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var detalle2 = await _context.Detalle2
                .Include(d => d.Vendedor)
                .Include(d => d.Cliente)
                .Include(d => d.Cliente2)
                .Include(d => d.Cajero)
                .Include(d => d.Detalles)
                .FirstOrDefaultAsync(m => m.NumeroFactura == id);

            if (detalle2 == null) return NotFound();

            return View(detalle2);
        }

        // POST: Detalle2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle2 = await _context.Detalle2
                .Include(d => d.Detalles)
                .FirstOrDefaultAsync(d => d.NumeroFactura == id);

            if (detalle2 != null)
            {
                _context.Detalle1.RemoveRange(detalle2.Detalles);
                _context.Detalle2.Remove(detalle2);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

