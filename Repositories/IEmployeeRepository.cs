using Employee_Management.Models;

namespace Employee_Management.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByIdWithDepartmentAsync(int employeeId);
        Task<IEnumerable<Models.Task>?> GatAllTasksAsync(int employeeId);
        Task<int> ExecuteUpdateAync(EmployeeDto employeeDto);
    }
}
