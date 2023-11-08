using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class DestinosAleatorio
{
    public int DestinoAleatorioId { get; set; }

    public int? DestinoId { get; set; }

    public DateTime? FechaGeneracion { get; set; }

    public virtual Destino? Destino { get; set; }
}
