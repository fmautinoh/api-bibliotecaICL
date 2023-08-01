using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_bibliotecaICL.Models;

public partial class LibrosAutore
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LibroAutorID { get; set; }
    [JsonRequired]
    [Required]
    public int LibroId { get; set; }
    [JsonRequired]
    [Required]
    public int AutorId { get; set; }

    [JsonIgnore]
    [Ignore]
    public virtual Autore Autor { get; set; } = null!;
    [JsonIgnore]
    [Ignore]
    public virtual Libro Libro { get; set; } = null!;
}
