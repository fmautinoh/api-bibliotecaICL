using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;

namespace api_bibliotecaICL.Repositorio.IRepositorio
{
    public interface IInventarioRepositorio : IRepositorio<InventarioLibro>
    {
        Task<InventarioLibro> Actualizar(InventarioLibro entidad);
    }
}
