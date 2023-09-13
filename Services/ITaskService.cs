using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<TaskDto>> ViewMyTasks();
        public Task<IEnumerable<EmployeeDto>?> GetEmployeesInDepartment();
        public TaskDto AssignTask(TaskDto taskAssignmentDto);
    }
}
