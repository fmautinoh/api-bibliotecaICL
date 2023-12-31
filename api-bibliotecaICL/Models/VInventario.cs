﻿using System;
using System.Collections.Generic;

namespace api_bibliotecaICL.Models;

public partial class VInventario
{
    public int LibroId { get; set; }

    public int InventarioId { get; set; }

    public int EstadoId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Valor { get; set; }

    public string Color { get; set; } = null!;
}
