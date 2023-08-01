using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using api_bibliotecaICL.Models;

namespace Api_Inventariobiblioteca.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DatabaseContext _context;
        internal DbSet<T> dbSet;

        public Repositorio(DatabaseContext db)
        {
            _context = db;
            this.dbSet = _context.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Listar(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListObjetos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Grabar();
        }
    }
}
