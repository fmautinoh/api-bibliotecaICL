using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Inventariobiblioteca.Models.ModelDto
{
    public class AutorDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutorId { get; set; }
        [Required]
        [StringLength(80)]
        public string NombreAutor { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(TipoAutorId))]
        public int TipoAutorId { get; set; }
    }
}
