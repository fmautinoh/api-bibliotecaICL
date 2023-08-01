using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;

namespace Api_Inventariobiblioteca.Repositorio.IRepositorio
{
    public interface IAutorRepositorio: IRepositorio<Autore>
    {
        Task<Autore> Actualizar(Autore entidad);
    }
}
