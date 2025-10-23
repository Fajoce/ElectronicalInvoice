using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models;

namespace WebHostApplication.Controllers
{
    public class ResolucionesElectronicasController : Controller
    {
        private readonly WebHostDbcontext _context;

        public ResolucionesElectronicasController(WebHostDbcontext context)
        {
            _context = context;
        }

        // GET: ResolucionesElectronicas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResolucionElectronicas.ToListAsync());
        }

        // GET: ResolucionesElectronicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resolucionElectronica = await _context.ResolucionElectronicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resolucionElectronica == null)
            {
                return NotFound();
            }

            return View(resolucionElectronica);
        }

        // GET: ResolucionesElectronicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResolucionesElectronicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,IdDian,Inicio,Final,Consecutivo,Estado,FechaFinal,Prefijo,Resolucion,Fecha,IdResolDian")] ResolucionElectronica resolucionElectronica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resolucionElectronica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resolucionElectronica);
        }

        // GET: ResolucionesElectronicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resolucionElectronica = await _context.ResolucionElectronicas.FindAsync(id);
            if (resolucionElectronica == null)
            {
                return NotFound();
            }
            return View(resolucionElectronica);
        }

        // POST: ResolucionesElectronicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,IdDian,Inicio,Final,Consecutivo,Estado,FechaFinal,Prefijo,Resolucion,Fecha,IdResolDian")] ResolucionElectronica resolucionElectronica)
        {
            if (id != resolucionElectronica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resolucionElectronica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResolucionElectronicaExists(resolucionElectronica.Id))
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
            return View(resolucionElectronica);
        }

        // GET: ResolucionesElectronicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resolucionElectronica = await _context.ResolucionElectronicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resolucionElectronica == null)
            {
                return NotFound();
            }

            return View(resolucionElectronica);
        }

        // POST: ResolucionesElectronicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resolucionElectronica = await _context.ResolucionElectronicas.FindAsync(id);
            if (resolucionElectronica != null)
            {
                _context.ResolucionElectronicas.Remove(resolucionElectronica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResolucionElectronicaExists(int id)
        {
            return _context.ResolucionElectronicas.Any(e => e.Id == id);
        }
    }
}
