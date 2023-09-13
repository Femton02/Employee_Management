using Employee_Management.Models;
using Employee_Management.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Employee_Management.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeContext _context;

        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }
        public ITaskRepository Tasks { get; }

        public UnitOfWork(EmployeeContext context)
        {
            _context = context;
            Departments = new DepartmentRepository(context);
            Employees = new EmployeeRepository(context);
            Tasks = new TaskRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
