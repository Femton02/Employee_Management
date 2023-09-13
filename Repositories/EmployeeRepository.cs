using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Employee_Management.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Tasks)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdWithDepartmentAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                    .ThenInclude(d => d.Employees)
                        .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<IEnumerable<Models.Task>?> GatAllTasksAsync(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Tasks)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);
            return employee?.Tasks;
        }
    }
}