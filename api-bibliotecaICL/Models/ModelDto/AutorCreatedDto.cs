using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Inventariobiblioteca.Models.ModelDto
{
    public class AutorCreatedDto
    {
        [Required]
        [StringLength(80)]
        public string NombreAutor { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(TipoAutorId))]
        public int TipoAutorId { get; set; }
    }
}
