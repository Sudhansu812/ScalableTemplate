using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScalableApplication.Application.DTOs.Department;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.Interfaces.Services;

namespace ScalableApplication.API.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService departmentService) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
        {
            CustomHttpResponse<GetDepartmentDto> response = await _departmentService.GetDepartment(id);
            return StatusCode((int) response.StatusCode, response.Data);
        }

        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetDepartmentEmployees([FromRoute] Guid id)
        {
            CustomHttpResponse<List<AllEmployeesDto>?> response = await _departmentService.GetDepartmentEmployees(id);
            return StatusCode((int) response.StatusCode, response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto department)
        {
            var response = await _departmentService.CreateDepartment(department);
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] DepartmentDto department)
        {
            var response = await _departmentService.UpdateDepartment(id, department);
            return StatusCode((int)response.StatusCode);
        }
    }
}
