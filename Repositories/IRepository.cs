using Employee_Management.Models;
using System.Linq.Expressions;

namespace Employee_Management.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPagedAsync(PaginationParameters paginationParameters);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        System.Threading.Tasks.Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
