using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciaViajes.Models;
using AgenciaViajes.Recursos;
using System.Text.Json;

namespace AgenciaViajes.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public UsuariosController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return _context.Usuarios != null ? 
                          View(await _context.Usuarios.ToListAsync()) :
                          Problem("Entity set 'AgenciaViajesContext.Usuarios'  is null.");
        }

        public async Task<IActionResult> IndexPrivado()
        {
            var tiposDeUsuarios = await _context.Usuarios
                .GroupBy(u => u.TipoUsuario)
                .Select(group => new
                {
                    Tipo = group.Key,
                    Cantidad = group.Count()
                })
                .ToListAsync();

            var tiposDeUsuariosJson = JsonSerializer.Serialize(tiposDeUsuarios.Select(x => x.Tipo));
            var cantidadesJson = JsonSerializer.Serialize(tiposDeUsuarios.Select(x => x.Cantidad));

            ViewBag.TiposDeUsuariosJson = tiposDeUsuariosJson;
            ViewBag.CantidadesJson = cantidadesJson;


            var destinosPorPais = await _context.Destinos
               .GroupBy(d => d.Pais)
               .Select(group => new
               {
                   Pais = group.Key,
                   Cantidad = group.Count()
               })
               .ToListAsync();

            ViewBag.DestinosPorPaisJson = JsonSerializer.Serialize(destinosPorPais.Select(x => x.Pais));
            ViewBag.CantidadesPorPaisJson = JsonSerializer.Serialize(destinosPorPais.Select(x => x.Cantidad));

            var actividadesPorPrecio = _context.Actividades
                .AsEnumerable() // Usar AsEnumerable si la lógica de agrupación no se puede traducir a SQL
                .GroupBy(a =>
                {
                    // Define los rangos de precios, por ejemplo:
                    if (a.Precio < 50) return "Menos de $50";
                    if (a.Precio >= 50 && a.Precio < 100) return "$50 - $99";
                    // Agrega más rangos según sea necesario
                    return "Más de $100";
                })
                .Select(group => new
                {
                    RangoPrecio = group.Key,
                    Cantidad = group.Count()
                })
                .ToList();

            ViewBag.ActividadesPorPrecioJson = JsonSerializer.Serialize(actividadesPorPrecio);




            var registrosPorFecha = await _context.Usuarios
              .Where(u => u.FechaRegistro != null)
              .GroupBy(u => new { Año = u.FechaRegistro.Value.Year, Mes = u.FechaRegistro.Value.Month })
              .Select(group => new
              {
                  group.Key.Año,
                  group.Key.Mes,
                  Cantidad = group.Count()
              })
              .OrderBy(x => x.Año).ThenBy(x => x.Mes)
              .ToListAsync();

            var registrosFormateados = registrosPorFecha
                .Select(x => new
                {
                    Fecha = $"{x.Mes}/{x.Año}",
                    x.Cantidad
                })
                .ToList();

            ViewBag.RegistrosPorFechaJson = JsonSerializer.Serialize(registrosFormateados);
            return View("/Views/Home/IndexPrivado.cshtml");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Email,Contrasena,FechaRegistro,TipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Contrasena = Util.encriptarClave(usuario.Contrasena);
                usuario.FechaRegistro = DateTime.Now;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,Email,Contrasena,FechaRegistro,TipoUsuario")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Contrasena = Util.encriptarClave(usuario.Contrasena);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioId))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
    }
}
