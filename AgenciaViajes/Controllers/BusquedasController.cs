using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciaViajes.Models;

namespace AgenciaViajes.Controllers
{
    public class BusquedasController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public BusquedasController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: Busquedas
        public async Task<IActionResult> Index()
        {
            var agenciaViajesContext = _context.Busquedas.Include(b => b.Usuario);
            return View(await agenciaViajesContext.ToListAsync());
        }

        // GET: Busquedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Busquedas == null)
            {
                return NotFound();
            }

            var busqueda = await _context.Busquedas
                .Include(b => b.Usuario)
                .FirstOrDefaultAsync(m => m.BusquedaId == id);
            if (busqueda == null)
            {
                return NotFound();
            }

            return View(busqueda);
        }

        // GET: Busquedas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email");
            return View();
        }

        // POST: Busquedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusquedaId,UsuarioId,FechaBusqueda,ParametrosBusqueda")] Busqueda busqueda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busqueda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email", busqueda.UsuarioId);
            return View(busqueda);
        }

        // GET: Busquedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Busquedas == null)
            {
                return NotFound();
            }

            var busqueda = await _context.Busquedas.FindAsync(id);
            if (busqueda == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email", busqueda.UsuarioId);
            return View(busqueda);
        }

        // POST: Busquedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusquedaId,UsuarioId,FechaBusqueda,ParametrosBusqueda")] Busqueda busqueda)
        {
            if (id != busqueda.BusquedaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busqueda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusquedaExists(busqueda.BusquedaId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Email", busqueda.UsuarioId);
            return View(busqueda);
        }

        // GET: Busquedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Busquedas == null)
            {
                return NotFound();
            }

            var busqueda = await _context.Busquedas
                .Include(b => b.Usuario)
                .FirstOrDefaultAsync(m => m.BusquedaId == id);
            if (busqueda == null)
            {
                return NotFound();
            }

            return View(busqueda);
        }

        // POST: Busquedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Busquedas == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.Busquedas'  is null.");
            }
            var busqueda = await _context.Busquedas.FindAsync(id);
            if (busqueda != null)
            {
                _context.Busquedas.Remove(busqueda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusquedaExists(int id)
        {
          return (_context.Busquedas?.Any(e => e.BusquedaId == id)).GetValueOrDefault();
        }
    }
}
