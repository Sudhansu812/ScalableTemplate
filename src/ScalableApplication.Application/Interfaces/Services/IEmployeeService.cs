using Microsoft.AspNetCore.JsonPatch;
using ScalableApplication.Application.DTOs;

namespace ScalableApplication.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<CustomHttpResponse<List<AllEmployeesDto>>> GetAllEmployees();
        Task<CustomHttpResponse<GetEmployeeDto?>> GetEmployee(Guid? id, string? userName);
        Task<CustomHttpResponse<GetEmployeeDto>> CreateEmployee(CreateEmployeeDto emp);
        Task<CustomHttpResponse<string>> UpdateEmployee(Guid empId, UpdateEmployeeDto? emp);
        Task<CustomHttpResponse<string>> RemoveEmployee(Guid empId);
        Task<CustomHttpResponse<string>> PatchEmployeeDetails(Guid empId, JsonPatchDocument<PatchEmployeeDto> patchDocument);
    }
}
