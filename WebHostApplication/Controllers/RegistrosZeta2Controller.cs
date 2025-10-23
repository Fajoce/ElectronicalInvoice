using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models.RegistroZeta;

namespace WebHostApplication.Controllers
{
    public class RegistrosZeta2Controller : Controller
    {
        private readonly WebHostDbcontext _context;

        public RegistrosZeta2Controller(WebHostDbcontext context)
        {
            _context = context;
        }

        // GET: RegistrosZeta2
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegistroZeta2s.ToListAsync());
        }

        // GET: RegistrosZeta2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroZeta2 = await _context.RegistroZeta2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroZeta2 == null)
            {
                return NotFound();
            }

            return View(registroZeta2);
        }

        // GET: RegistrosZeta2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistrosZeta2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Z2,FechaHora,Maq")] RegistroZeta2 registroZeta2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroZeta2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registroZeta2);
        }

        // GET: RegistrosZeta2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroZeta2 = await _context.RegistroZeta2s.FindAsync(id);
            if (registroZeta2 == null)
            {
                return NotFound();
            }
            return View(registroZeta2);
        }

        // POST: RegistrosZeta2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Z2,FechaHora,Maq")] RegistroZeta2 registroZeta2)
        {
            if (id != registroZeta2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroZeta2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroZeta2Exists(registroZeta2.Id))
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
            return View(registroZeta2);
        }

        // GET: RegistrosZeta2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroZeta2 = await _context.RegistroZeta2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroZeta2 == null)
            {
                return NotFound();
            }

            return View(registroZeta2);
        }

        // POST: RegistrosZeta2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroZeta2 = await _context.RegistroZeta2s.FindAsync(id);
            if (registroZeta2 != null)
            {
                _context.RegistroZeta2s.Remove(registroZeta2);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroZeta2Exists(int id)
        {
            return _context.RegistroZeta2s.Any(e => e.Id == id);
        }
    }
}
