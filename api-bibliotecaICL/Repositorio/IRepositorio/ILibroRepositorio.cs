using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;

namespace Api_Inventariobiblioteca.Repositorio.IRepositorio
{
    public interface ILibroRepositorio :IRepositorio<Libro>
    {
        Task<Libro> Actualizar(Libro entidad);

    }
}
