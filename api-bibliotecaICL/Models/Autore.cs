using System;
using System.Collections.Generic;

namespace api_bibliotecaICL.Models;

public partial class Autore
{
    public int AutorId { get; set; }

    public string NombreAutor { get; set; } = null!;

    public int TipoAutorId { get; set; }

    public virtual TipoAutor TipoAutor { get; set; } = null!;
}
