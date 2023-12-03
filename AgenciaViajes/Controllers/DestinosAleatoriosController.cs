using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgenciaViajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgenciaViajes.Controllers
{
    public class DestinosAleatoriosController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public DestinosAleatoriosController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: DestinosAleatorios
        public async Task<IActionResult> Index()
        {
            var agenciaViajesContext = _context.DestinosAleatorios.Include(d => d.Destino);
            return View(await agenciaViajesContext.ToListAsync());
        }

        // GET: DestinosAleatorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DestinosAleatorios == null)
            {
                return NotFound();
            }

            var destinosAleatorio = await _context.DestinosAleatorios
                .Include(d => d.Destino)
                .FirstOrDefaultAsync(m => m.DestinoAleatorioId == id);
            if (destinosAleatorio == null)
            {
                return NotFound();
            }

            return View(destinosAleatorio);
        }

        // GET: DestinosAleatorios/Create
        public IActionResult Create()
        {
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre");
            return View();
        }

        // POST: DestinosAleatorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestinoAleatorioId,DestinoId,FechaGeneracion")] DestinosAleatorio destinosAleatorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destinosAleatorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre", destinosAleatorio.DestinoId);
            return View(destinosAleatorio);
        }

        // GET: DestinosAleatorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DestinosAleatorios == null)
            {
                return NotFound();
            }

            var destinosAleatorio = await _context.DestinosAleatorios.FindAsync(id);
            if (destinosAleatorio == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre", destinosAleatorio.DestinoId);
            return View(destinosAleatorio);
        }

        // POST: DestinosAleatorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DestinoAleatorioId,DestinoId,FechaGeneracion")] DestinosAleatorio destinosAleatorio)
        {
            if (id != destinosAleatorio.DestinoAleatorioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destinosAleatorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinosAleatorioExists(destinosAleatorio.DestinoAleatorioId))
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
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre", destinosAleatorio.DestinoId);
            return View(destinosAleatorio);
        }

        // GET: DestinosAleatorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DestinosAleatorios == null)
            {
                return NotFound();
            }

            var destinosAleatorio = await _context.DestinosAleatorios
                .Include(d => d.Destino)
                .FirstOrDefaultAsync(m => m.DestinoAleatorioId == id);
            if (destinosAleatorio == null)
            {
                return NotFound();
            }

            return View(destinosAleatorio);
        }

        // POST: DestinosAleatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DestinosAleatorios == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.DestinosAleatorios'  is null.");
            }
            var destinosAleatorio = await _context.DestinosAleatorios.FindAsync(id);
            if (destinosAleatorio != null)
            {
                _context.DestinosAleatorios.Remove(destinosAleatorio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinosAleatorioExists(int id)
        {
            return (_context.DestinosAleatorios?.Any(e => e.DestinoAleatorioId == id)).GetValueOrDefault();
        }
    }
}
