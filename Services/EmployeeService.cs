using Employee_Management.Models;
using Employee_Management.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Employee_Management.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public EmployeeService(IMapper mapper,IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {

            var userExists = await _userManager.FindByNameAsync(employeeDto.Name);
            if (userExists == null)
            {
                IdentityUser user = new()
                {
                    UserName = employeeDto.Name,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = employeeDto.Email,
                };
                var password = "Dummy@123";
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new Exception("User creation failed! Please check user details and try again.");
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
                    await _userManager.AddToRoleAsync(user, UserRoles.Employee);
                var employeeToAdd = _mapper.Map<Employee>(employeeDto);
                employeeToAdd.UserId = user.Id;
                await _unitOfWork.Employees.AddAsync(employeeToAdd);
                await _unitOfWork.CompleteAsync();
                return employeeDto;
            }

            throw new Exception("An error occured");
        }

        public async Task<EmployeeDto> DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id) ?? throw new NotFoundException("Employee not found!");
            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.CompleteAsync();
            var employeedto = _mapper.Map<EmployeeDto>(employee);
            return employeedto;
        }

        public Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = _unitOfWork.Employees.GetAllAsync().Result ?? throw new NotFoundException("No employees found!");
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return System.Threading.Tasks.Task.FromResult(employeeDtos);
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id) ?? throw new NotFoundException("Employee not found!");
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            var employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.Employees.Update(employeeToUpdate);
            await _unitOfWork.CompleteAsync();
            if (employeeToUpdate.DepartmentId != null)
            {
                int depid = employeeToUpdate.DepartmentId ?? default;
                var department = await _unitOfWork.Departments.GetByIdAsync(depid);
                if (department?.ManagerID == employeeToUpdate.Id)
                {
                    var user = await _userManager.FindByNameAsync(employeeToUpdate.Name);
                    if (user != null)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Manager);
                    }
                }
            }
            return employeeDto;
        }
    }
}
