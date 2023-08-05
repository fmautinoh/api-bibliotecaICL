using api_bibliotecaICL.Models;
using api_bibliotecaICL.Repositorio.IRepositorio;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;

namespace api_bibliotecaICL.Repositorio
{
    public class vInventarioRepositorio : ReaderRepositorio<VInventario>, IvInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public vInventarioRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
