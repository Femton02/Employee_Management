using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployee(int id, EmployeeDto employeeDto);
        Task<EmployeeDto> DeleteEmployee(int id);
    }
}
