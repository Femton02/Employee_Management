using Employee_Management.Repositories;
using Employee_Management.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Employee_Management.Models;
using Employee_Management.Services;

namespace Employee_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = UserRoles.Employee)]
        [HttpGet("ViewMyTasks")]
        public Task<IEnumerable<TaskDto>> ViewMyTasks()
        {
            return _taskService.ViewMyTasks();
        }

        [Authorize(Roles = UserRoles.Manager)]
        [HttpGet("GetEmployeesInMyDepartment")]
        public Task<IEnumerable<EmployeeDto>> GetEmployeesInDepartment()
        {
            return _taskService.GetEmployeesInDepartment();
        }

        [Authorize(Roles = UserRoles.Manager)]
        [HttpPost("Assign-tasks")]
        public IActionResult AssignTask(TaskDto taskAssignmentDto)
        {
            return Ok(_taskService.AssignTask(taskAssignmentDto));
        }

    }
}