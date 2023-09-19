using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly EmployeeContext _context;
        public DepartmentRepository(EmployeeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> ExecuteUpdate(DepartmentDto departmentDto)
        {
            return await _context.Departments
                .Where(e => e.Id == departmentDto.Id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(b => b.Name, departmentDto.Name)
                    .SetProperty(b => b.Description, departmentDto.Description)
                    .SetProperty(b => b.ManagerID, departmentDto.ManagerID)
                );
        }
    }
}
