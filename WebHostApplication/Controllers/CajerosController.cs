using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models;

namespace WebHostApplication.Controllers
{
    public class CajerosController : Controller
    {
        private readonly WebHostDbcontext _context;

        public CajerosController(WebHostDbcontext context)
        {
            _context = context;
        }

        // GET: Cajeros
        public async Task<IActionResult> Index()
        {
            var cajeros = await _context.Cajeros.ToListAsync();
            return View(cajeros);
        }

        // GET: Cajeros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cajero = await _context.Cajeros
                .Include(c => c.Facturas) // 👈 Incluye facturas relacionadas
                .FirstOrDefaultAsync(m => m.IdCajero == id);

            if (cajero == null) return NotFound();

            return View(cajero);
        }

        // GET: Cajeros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cajeros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PrimerApellido,SegundoApellido")] Cajero cajero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cajero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cajero);
        }

        // GET: Cajeros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cajero = await _context.Cajeros.FindAsync(id);
            if (cajero == null) return NotFound();

            return View(cajero);
        }

        // POST: Cajeros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCajero,Nombre,PrimerApellido,SegundoApellido")] Cajero cajero)
        {
            if (id != cajero.IdCajero) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cajero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajeroExists(cajero.IdCajero))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cajero);
        }

        // GET: Cajeros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cajero = await _context.Cajeros
                .FirstOrDefaultAsync(m => m.IdCajero == id);

            if (cajero == null) return NotFound();

            return View(cajero);
        }

        // POST: Cajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cajero = await _context.Cajeros.FindAsync(id);
            if (cajero != null)
            {
                _context.Cajeros.Remove(cajero);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CajeroExists(int id)
        {
            return _context.Cajeros.Any(e => e.IdCajero == id);
        }
    }
}
