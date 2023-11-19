using Microsoft.AspNetCore.Mvc;

using AgenciaViajes.Models;
using AgenciaViajes.Recursos;
using AgenciaViajes.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace AgenciaViajes.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public InicioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario usuario) {
            usuario.Contrasena = Util.encriptarClave(usuario.Contrasena);
            Usuario usuario_creado = await _usuarioService.SaveUsuario(usuario);
            if (usuario_creado.UsuarioId > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            
            return View(); 
        }

        public IActionResult IniciarSesion() { return View(); }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string email, string password)
        {

            Usuario usuario_encontrado = await _usuarioService.GetUsuario(email,Util.encriptarClave(password));
            if (usuario_encontrado==null)
            {
                ViewData["Mensaje"] = "No se encontraron coicidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombre),
                new Claim(ClaimTypes.Role, usuario_encontrado.TipoUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties);

            if (usuario_encontrado.TipoUsuario == "Administrador")
            {
                return RedirectToAction("IndexPrivado", "Home");
            }
            else
            {
                // Redirigir al usuario no administrador a la página principal
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }



    }
}
