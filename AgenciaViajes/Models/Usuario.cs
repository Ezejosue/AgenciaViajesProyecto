using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Contrasena { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? TipoUsuario { get; set; }

    public virtual ICollection<Busqueda> Busqueda { get; set; } = new List<Busqueda>();
}
