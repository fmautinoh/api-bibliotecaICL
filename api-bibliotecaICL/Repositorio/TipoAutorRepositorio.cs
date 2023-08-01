using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class TipoAutorRepositorio : ReaderRepositorio<TipoAutor>, ITipoAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
