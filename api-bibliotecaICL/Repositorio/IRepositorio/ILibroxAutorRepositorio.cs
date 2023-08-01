using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;

namespace Api_Inventariobiblioteca.Repositorio.IRepositorio
{
    public interface ILibroxAutorRepositorio : IRepositorio<LibrosAutore>
    {
        Task<LibrosAutore> Actualizar(LibrosAutore entidad);
    }
}
