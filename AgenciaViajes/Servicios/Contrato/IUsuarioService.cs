using AgenciaViajes.Models;

namespace AgenciaViajes.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string email, string password);
        Task<Usuario> SaveUsuario(Usuario usuario);
    }
}
