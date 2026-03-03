using Asp.Versioning;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ScalableApplication.Application.DTOs.Common;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.Interfaces.v1.Services;

namespace ScalableApplication.API.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService employee) : ControllerBase
    {
        private readonly IEmployeeService _employee = employee;

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            CustomHttpResponse<List<AllEmployeesDto>> response = await _employee.GetAllEmployees();
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllEmployeesWithDepartmentId()
        {
            CustomHttpResponse<List<EmployeeWithDepartmentIdDto>> response = await _employee.GetAllEmployeesWithDepartmentId();
            return StatusCode((int)response.StatusCode,response.Data);
        }

        [HttpGet("by-user-name")]
        public async Task<IActionResult> GetEmployee([FromQuery] Guid? id, string? userName)
        {
            CustomHttpResponse<GetEmployeeDto?> response = await _employee.GetEmployee(id, userName);
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto emp)
        {
            CustomHttpResponse<GetEmployeeDto> response = await _employee.CreateEmployee(emp);
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpPut("{empid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid empid, [FromBody] UpdateEmployeeDto emp)
        {
            CustomHttpResponse<string> response = await _employee.UpdateEmployee(empid, emp);
            return StatusCode((int)response.StatusCode);
        }

        [HttpPatch("{empid}")]
        public async Task<IActionResult> PatchEmployeeDetails([FromRoute] Guid empid, [FromBody] JsonPatchDocument<PatchEmployeeDto> patchDocument)
        {
            CustomHttpResponse<string> response = await _employee.PatchEmployeeDetails(empid, patchDocument);
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut("assign-department")]
        public async Task<IActionResult> AssignDepartment([FromBody] AssignDepartmentDto employeeDepartment)
        {
            CustomHttpResponse<string> response = await _employee.AssignDepartment(employeeDepartment);
            return StatusCode((int)response.StatusCode);
        }

        [HttpDelete("{empid}")]
        public async Task<IActionResult> RemoveEmployee([FromRoute] Guid empid)
        {
            CustomHttpResponse<string> response = await _employee.RemoveEmployee(empid);
            return StatusCode((int)response.StatusCode);
        }

        [HttpGet("paginated-list")]
        public async Task<IActionResult> GetEmployees(int? page = 0, int? pageSize = 100)
        {
            CustomHttpResponse<PagedResponse<ReadEmployeeDto>> response = await _employee.GetEmployees(page, pageSize);
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpGet("filtered-list")]
        public async Task<IActionResult> GetEmployees([FromQuery] GetFilteredEmployeeDto filter)
        {
            CustomHttpResponse<PagedResponse<ReadEmployeeDto>> response = await _employee.GetEmployees(filter);
            return StatusCode((int)response.StatusCode, response.Data);
        }
    }
}
