using Employee_Management.Repositories;
using Employee_Management.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Employee_Management.Models;
using Employee_Management.Services;

namespace Employee_Management.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            return await _employeeService.GetAllEmployees();
        }

        [HttpGet("paged")]
        public async Task<IEnumerable<EmployeeDto>> GetPaged([FromQuery] PaginationParameters paginationParameters)
        {
            return await _employeeService.GetPagedEmployees(paginationParameters);
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDto> Get(int id)
        {
            return await _employeeService.GetEmployeeById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            return Ok(await _employeeService.CreateEmployee(employee));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeDto employee)
        {
            return Ok(await _employeeService.UpdateEmployee(employee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _employeeService.DeleteEmployee(id));
        }
    }
}