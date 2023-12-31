﻿using System;
using System.Collections.Generic;

namespace api_bibliotecaICL.Models;

public partial class InventarioLibro
{
    public int InventarioId { get; set; }

    public int LibroId { get; set; }

    public string Codigo { get; set; } = null!;

    public int EstadoId { get; set; }

    public virtual EstadoConservacion Estado { get; set; } = null!;

    public virtual Libro Libro { get; set; } = null!;
}
