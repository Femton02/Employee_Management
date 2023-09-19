using Employee_Management.Models;
using Employee_Management.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Employee_Management.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        virtual public async System.Threading.Tasks.Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(PaginationParameters paginationParameters)
        {
            var query = _dbSet.Skip(paginationParameters.Skip)
                              .Take(paginationParameters.Take);

            return await query.ToListAsync();
        }
        public IEnumerable<T> FindWithSpecificationPattern(ISpecification<T>? specification = null)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        virtual public async void Remove(T entity)
        {
            await System.Threading.Tasks.Task.Run(() => _dbSet.Remove(entity));
        }

        virtual public void Update(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
