using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto> DeleteEmployee(int id);
        Task<IEnumerable<EmployeeDto>> GetPagedEmployees(PaginationParameters paginationParameters);
    }
}
