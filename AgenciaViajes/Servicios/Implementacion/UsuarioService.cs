using AgenciaViajes.Models;
using AgenciaViajes.Servicios.Contrato;
using Microsoft.EntityFrameworkCore;

namespace AgenciaViajes.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AgenciaViajesContext _context;

        public UsuarioService(AgenciaViajesContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuario(string correo, string password)
        {
            Usuario usuario_encontrado = await _context.Usuarios.Where(u => u.Email == correo && u.Contrasena == password)
                .FirstOrDefaultAsync();
            return usuario_encontrado;
        }

        public async Task<Usuario> GetIDUsuario(string correo)
        {
            Usuario usuario_encontrado = await _context.Usuarios.Where(u => u.Email == correo)
                .FirstOrDefaultAsync();
            return usuario_encontrado;
        }


        public async Task<Usuario> SaveUsuario(Usuario usuario) {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
