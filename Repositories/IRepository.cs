using System.Linq.Expressions;

namespace Employee_Management.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
