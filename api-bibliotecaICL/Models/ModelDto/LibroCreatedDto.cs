using System.ComponentModel.DataAnnotations;

namespace Api_Inventariobiblioteca.Models.ModelDto
{
    public class LibroCreatedDto
    {
        [Required]
        [StringLength(80)]
        public string NombreLib { get; set; } = null!;
        [Required]
        public int TipoId { get; set; }
        [Required]
        public int? Edicion { get; set; }
        [Required]
        public string? Año { get; set; }
        [Required]
        [StringLength(80)]
        public string? Editorial { get; set; }
        [Required]
        public List<int> Autor { get; set; }
    }
}
