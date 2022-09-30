using System.Linq.Expressions;

namespace APICatalogo_.Repository
{
    //IRepository tem que ser generica "<T>"
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        
        
    }
}
