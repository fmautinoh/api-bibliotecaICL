using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class AutorRepositorio : Repositorio<Autore>, IAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public AutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<Autore> Actualizar(Autore entidad)
        {
            _context.Autores.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
