using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ScalableApplication.Application.DTOs;
using ScalableApplication.Application.Interfaces.Services;

namespace ScalableApplication.API.Controllers
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
            return StatusCode((int) response.StatusCode, response.Data);
        }

        [HttpGet("GetEmployeeByName")]
        public async Task<IActionResult> GetEmployee([FromQuery] Guid? id, string? userName)
        {
            CustomHttpResponse<GetEmployeeDto?> response = await _employee.GetEmployee(id, userName);
            return StatusCode((int) response.StatusCode, response.Data);
        }
    }
}
