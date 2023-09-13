using Employee_Management.Repositories;
using Employee_Management.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Employee_Management.Models;
using Employee_Management.Services;

namespace Employee_Management.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentDto>> GetAll()
        {
            return await _departmentService.GetAllDepartments();
        }

        [HttpGet("{id}")]
        public async Task<DepartmentDto> Get(int id)
        {
            return await _departmentService.GetDepartmentById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartmentDto departmentDto)
        {
            return Ok(await _departmentService.CreateDepartment(departmentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartmentDto departmentDto)
        {
            return Ok(await _departmentService.UpdateDepartment(id, departmentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _departmentService.DeleteDepartment(id));
        }
    }
}