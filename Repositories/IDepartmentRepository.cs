using Employee_Management.Models;

namespace Employee_Management.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<int> ExecuteUpdate(DepartmentDto departmentDto);
    }
}
