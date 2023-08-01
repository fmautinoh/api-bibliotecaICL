using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class LibroxAutorRepositorio : Repositorio<LibrosAutore>, ILibroxAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public LibroxAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<LibrosAutore> Actualizar(LibrosAutore entidad)
        {
            _context.LibrosAutores.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
