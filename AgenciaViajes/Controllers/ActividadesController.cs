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
    public class ActividadesController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public ActividadesController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: Actividades
        public async Task<IActionResult> Index()
        {
            var agenciaViajesContext = _context.Actividades.Include(a => a.Destino);
            return View(await agenciaViajesContext.ToListAsync());
        }

        // GET: Actividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actividades == null)
            {
                return NotFound();
            }

            var actividade = await _context.Actividades
                .Include(a => a.Destino)
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividade == null)
            {
                return NotFound();
            }

            return View(actividade);
        }

        // GET: Actividades/Create
        public IActionResult Create()
        {
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre");
            return View();
        }

        // POST: Actividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActividadId,DestinoId,Nombre,Descripcion,Precio,Duracion")] Actividade actividade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actividade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre", actividade.DestinoId);
            return View(actividade);
        }

        // GET: Actividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actividades == null)
            {
                return NotFound();
            }

            var actividade = await _context.Actividades.FindAsync(id);
            if (actividade == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre", actividade.DestinoId);
            return View(actividade);
        }

        // POST: Actividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActividadId,DestinoId,Nombre,Descripcion,Precio,Duracion")] Actividade actividade)
        {
            if (id != actividade.ActividadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActividadeExists(actividade.ActividadId))
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
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "Nombre", actividade.DestinoId);
            return View(actividade);
        }

        // GET: Actividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actividades == null)
            {
                return NotFound();
            }

            var actividade = await _context.Actividades
                .Include(a => a.Destino)
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividade == null)
            {
                return NotFound();
            }

            return View(actividade);
        }

        // POST: Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actividades == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.Actividades'  is null.");
            }
            var actividade = await _context.Actividades.FindAsync(id);
            if (actividade != null)
            {
                _context.Actividades.Remove(actividade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActividadeExists(int id)
        {
          return (_context.Actividades?.Any(e => e.ActividadId == id)).GetValueOrDefault();
        }
    }
}
