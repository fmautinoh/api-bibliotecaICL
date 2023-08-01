using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class TipoLibroRepositorio : ReaderRepositorio<TipoLibro>, ITipoLibroRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoLibroRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
