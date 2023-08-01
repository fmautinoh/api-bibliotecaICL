using System;
using System.Collections.Generic;

namespace api_bibliotecaICL.Models;

public partial class Libro
{
    public int LibroId { get; set; }

    public string NombreLib { get; set; } = null!;

    public int TipoId { get; set; }

    public int? Edicion { get; set; }

    public string? Año { get; set; }

    public string? Editorial { get; set; }

    public virtual ICollection<InventarioLibro> InventarioLibros { get; set; } = new List<InventarioLibro>();

    public virtual TipoLibro Tipo { get; set; } = null!;
}
