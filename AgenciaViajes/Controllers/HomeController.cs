using AgenciaViajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AgenciaViajes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AgenciaViajesContext _context;

        public HomeController(ILogger<HomeController> logger, AgenciaViajesContext context)
        {
            _logger = logger;
            _context = context;
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