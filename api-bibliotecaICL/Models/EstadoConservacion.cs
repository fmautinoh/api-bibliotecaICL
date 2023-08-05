using System;
using System.Collections.Generic;

namespace api_bibliotecaICL.Models;

public partial class EstadoConservacion
{
    public int EstadoId { get; set; }

    //Descripcion => "Bueno", "Execelente", etc
    public string Descripcion { get; set; } = null!;
    // valor es 1, 2, 3, 4, 5
    public int Valor { get; set; }
    //Color es para que se pase como parametro de color de la fila
    public string Color { get; set; } = null!;

    public virtual ICollection<InventarioLibro> InventarioLibros { get; set; } = new List<InventarioLibro>();
}
