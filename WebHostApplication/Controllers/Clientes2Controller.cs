using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models.Clientes;

namespace WebHostApplication.Controllers
{
    public class Clientes2Controller : Controller
    {
        private readonly WebHostDbcontext _context;

        public Clientes2Controller(WebHostDbcontext context)
        {
            _context = context;
        }

        // GET: Clientes2
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes2.ToListAsync());
        }

        // GET: Clientes2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente2 = await _context.Clientes2
                .FirstOrDefaultAsync(m => m.IdCliente2 == id);
            if (cliente2 == null)
            {
                return NotFound();
            }

            return View(cliente2);
        }

        // GET: Clientes2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente2,Id,Nombre,Apellido1,Apellido2,Dir,Tel,Correo,Tipo,Porcentaje,Condicion")] Cliente2 cliente2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente2);
        }

        // GET: Clientes2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente2 = await _context.Clientes2.FindAsync(id);
            if (cliente2 == null)
            {
                return NotFound();
            }
            return View(cliente2);
        }

        // POST: Clientes2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente2,Id,Nombre,Apellido1,Apellido2,Dir,Tel,Correo,Tipo,Porcentaje,Condicion")] Cliente2 cliente2)
        {
            if (id != cliente2.IdCliente2)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cliente2Exists(cliente2.IdCliente2))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente2);
        }

        // GET: Clientes2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente2 = await _context.Clientes2
                .FirstOrDefaultAsync(m => m.IdCliente2 == id);
            if (cliente2 == null)
            {
                return NotFound();
            }

            return View(cliente2);
        }

        // POST: Clientes2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente2 = await _context.Clientes2.FindAsync(id);
            if (cliente2 != null)
            {
                _context.Clientes2.Remove(cliente2);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cliente2Exists(int id)
        {
            return _context.Clientes2.Any(e => e.IdCliente2 == id);
        }
    }
}
