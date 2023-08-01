using System.Linq.Expressions;

namespace Api_Inventariobiblioteca.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);
        Task<List<T>> ListObjetos(Expression<Func<T, bool>>? filtro = null);
        Task<T> Listar(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task Remover(T entidad);
        Task Grabar();
    }
}
