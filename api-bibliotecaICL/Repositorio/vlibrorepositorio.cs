using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class vlibrorepositorio : ReaderRepositorio<VLibro>, Ivlibrorepositorio
    {
        private readonly DatabaseContext _context;

        public vlibrorepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
