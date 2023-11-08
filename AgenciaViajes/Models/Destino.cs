using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Destino
{
    public int DestinoId { get; set; }

    public string? Nombre { get; set; }

    public string? Pais { get; set; }

    public string? ZonaGeografica { get; set; }

    public string? Descripcion { get; set; }

    public string? ImagenUrl { get; set; }

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();

    public virtual ICollection<DestinosAleatorio> DestinosAleatorios { get; set; } = new List<DestinosAleatorio>();

    public virtual ICollection<Busqueda> Busqueda { get; set; } = new List<Busqueda>();
}
