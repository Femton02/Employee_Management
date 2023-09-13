using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartments();
        Task<DepartmentDto> GetDepartmentById(int id);
        Task<DepartmentDto> CreateDepartment(DepartmentDto departmentDto);
        Task<DepartmentDto> UpdateDepartment(int id, DepartmentDto departmentDto);
        Task<DepartmentDto> DeleteDepartment(int id);
    }
}
