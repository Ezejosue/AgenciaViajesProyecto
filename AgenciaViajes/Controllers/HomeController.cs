using AgenciaViajes.Models;
using AgenciaViajes.Recursos;
using AgenciaViajes.Servicios.Contrato;
using AgenciaViajes.Servicios.Implementacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace AgenciaViajes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AgenciaViajesContext _context;
        private readonly IUsuarioService _contextService;


        public HomeController(ILogger<HomeController> logger, AgenciaViajesContext context, IUsuarioService contextService)
        {
            _logger = logger;
            _context = context;
            _contextService = contextService;
        }


        [HttpPost]
        public async Task<IActionResult> Buscar(string criterioDeBusqueda)
        {
            var destinosEncontrados = _context.Destinos.Where(d => d.Nombre.Contains(criterioDeBusqueda)).ToList();
            

            var correo = "";
            var idUsuario = 0;

            if (User.Identity.IsAuthenticated)
            {
                correo = User.FindFirstValue(ClaimTypes.Email);
                Usuario usuario_encontrado = await _contextService.GetIDUsuario(correo);
                idUsuario = usuario_encontrado.UsuarioId;

            }



            if (idUsuario==0)
            {
                // Guardar la búsqueda
                var busqueda = new Busqueda
                {
                    FechaBusqueda = DateTime.Now,
                    ParametrosBusqueda = criterioDeBusqueda,
                    Destinos = destinosEncontrados,

                };
                _context.Busquedas.Add(busqueda);
                await _context.SaveChangesAsync();



                // Redireccionar al primer destino encontrado
                if (destinosEncontrados.Any())
                {
                    var idDestinoEncontrado = destinosEncontrados.First().DestinoId;
                    return RedirectToAction("Privacy", "Home", new { id = idDestinoEncontrado });
                }
            }
            else
            {
                // Guardar la búsqueda
                var busqueda = new Busqueda
                {
                    FechaBusqueda = DateTime.Now,
                    ParametrosBusqueda = criterioDeBusqueda,
                    Destinos = destinosEncontrados,
                    UsuarioId = idUsuario,

                };
                _context.Busquedas.Add(busqueda);
                await _context.SaveChangesAsync();



                // Redireccionar al primer destino encontrado
                if (destinosEncontrados.Any())
                {
                    var idDestinoEncontrado = destinosEncontrados.First().DestinoId;
                    return RedirectToAction("Privacy", "Home", new { id = idDestinoEncontrado });
                }
            }

          

            // Si no se encuentra ningún destino, puedes redirigir a otra vista o mostrar un mensaje
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            return _context.Destinos != null ?
                        View(await _context.Destinos.ToListAsync()) :
                        Problem("Entity set 'AgenciaViajesContext.Destinos'  is null.");
        }

        public async Task<IActionResult> Privacy(int? id)
        {
            if (id == null || _context.Destinos == null)
            {
                return NotFound();
            }

            var destino = await _context.Destinos
                .FirstOrDefaultAsync(m => m.DestinoId == id);

            var actividades = await _context.Actividades.Where(a => a.DestinoId == id).ToListAsync();

            var destinosRandom = await _context.Destinos
                    .OrderBy(x => Guid.NewGuid())  // Ordena aleatoriamente
                    .Take(5)                       // Toma los primeros 5
                    .ToListAsync();

            if (destino == null)
            {
                return NotFound();
            }

            var viewModel = new DestinoActividadViewModel
            {
                Destino = destino,
                Actividades = actividades,
                destinosRandom = destinosRandom
            };

            return View(viewModel);
        }

        public IActionResult IndexPrivado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}