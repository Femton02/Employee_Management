using Employee_Management.Models;
using Employee_Management.UnitofWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Employee_Management.Specifications;
using AutoMapper;

namespace Employee_Management.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskService(IMapper mapper ,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public TaskDto AssignTask(TaskDto taskAssignmentDto)
        {
            var taskAssignment = new Models.Task
            {
                Description = taskAssignmentDto.Description,
                EmployeeId = taskAssignmentDto.EmployeeId,
            };
            _unitOfWork.Tasks.AddAsync(taskAssignment);
            _unitOfWork.CompleteAsync();
            return taskAssignmentDto;
        }

        public async Task<IEnumerable<EmployeeDto>?> GetEmployeesInDepartment()
        {
            string? userId = ClaimsPrincipal.Current?.FindFirstValue(ClaimTypes.NameIdentifier);
            var useremployee = await _unitOfWork.Employees.FirstOrDefaultAsync(e => e.UserId == userId) ?? throw new NotFoundException("You don't exist :)");
            //var employee = await _unitOfWork.Employees.GetByIdWithDepartmentAsync(useremployee.Id);
            var specification = new EmployeeByIdWithDepartmentSpecification(useremployee.Id);
            var employee = _unitOfWork.Employees.FindWithSpecificationPattern(specification).FirstOrDefault()?.Department?.Employees;
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
            return await System.Threading.Tasks.Task.FromResult(employeesDto);
        }

        public async Task<IEnumerable<TaskDto>> ViewMyTasks()
        {
            string? userId = ClaimsPrincipal.Current?.FindFirstValue(ClaimTypes.NameIdentifier);
            var useremployee = await _unitOfWork.Employees.FirstOrDefaultAsync(e => e.UserId == userId) ?? throw new NotFoundException("You don't exist :)");
            var tasks = (await _unitOfWork.Employees.GatAllTasksAsync(useremployee.Id)) ?? throw new NotFoundException("No tasks found!");
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return await System.Threading.Tasks.Task.FromResult(tasksDto);
        }
    }
}
