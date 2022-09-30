using APICatalogo_.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo_.Repository
{
    //classe genericar que herda de IRepository
    //com uma restrição "where T" que só pode ser uma classe
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        //o método set<T> do contexto retorna uma instância
        //DbSet<T> para o acesso a entidades de determinado
        //tipo no contexto.
        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }
        //delegate, metodo anomino
        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }           

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
