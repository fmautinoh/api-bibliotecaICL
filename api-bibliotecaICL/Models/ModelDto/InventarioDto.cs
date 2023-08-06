namespace api_bibliotecaICL.Models.ModelDto
{
    public class InventarioDto
    {
        public int LibroId { get; set; }

        public string Codigo { get; set; } = null!;

        public int EstadoId { get; set; }
    }
}
