using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Busqueda
{
    public int BusquedaId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaBusqueda { get; set; }

    public string? ParametrosBusqueda { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Destino> Destinos { get; set; } = new List<Destino>();
}
