using Employee_Management.Models;
using Employee_Management.Specifications;
using System.Linq.Expressions;

namespace Employee_Management.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> FindWithSpecificationPattern(ISpecification<T>? specification = null);
        Task<IEnumerable<T>> GetPagedAsync(PaginationParameters paginationParameters);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        System.Threading.Tasks.Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
