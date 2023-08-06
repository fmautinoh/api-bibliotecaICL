using System;
using System.Collections.Generic;

namespace api_bibliotecaICL.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Usu { get; set; } = null!;

    public string Pwsd { get; set; } = null!;
}
