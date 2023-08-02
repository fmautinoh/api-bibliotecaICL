using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bibliotecaICL.Models;
using api_bibliotecaICL.Models.ModelDto;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class vlibrorepositorio : ReaderRepositorio<VLibro>, Ivlibrorepositorio
    {
        private readonly DatabaseContext _context;

        public vlibrorepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        // Implement the ListLibrosConAutores method
        public async Task<List<VLibro>> ListLibrosConAutores()
        {
            // Fetch the grouped books without including the authors
            var librosGrouped = await _context.VLibros
                .GroupBy(libro => libro.LibroId)
                .Select(group => new VLibro
                {
                    LibroId = group.Key,
                    NombreLib = group.First().NombreLib,
                    TipoLibroId = group.First().TipoLibroId,
                    TipoLibro = group.First().TipoLibro,
                    Edicion = group.First().Edicion,
                    Año = group.First().Año,
                    Editorial = group.First().Editorial,
                })
                .ToListAsync();

            // Fetch the authors separately
            var autores = await _context.VLibros
                .Select(libro => new { libro.LibroId, libro.AutorId, libro.NombreAutor })
                .Distinct()
                .ToListAsync();

            // Map the authors to the corresponding books
            foreach (var libro in librosGrouped)
            {
                libro.AutoresIds = autores
                    .Where(a => a.LibroId == libro.LibroId && a.AutorId != 0) // Exclude entries with autorId = 0
                    .Select(a => new AutorDtosList { AutorId = a.AutorId, NombreAutor = a.NombreAutor })
                    .ToList();
            }

            return librosGrouped;
        }
    }
}
