using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Actividade
{
    public int ActividadId { get; set; }

    public int? DestinoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public string? Duracion { get; set; }

    public virtual Destino? Destino { get; set; }
}
