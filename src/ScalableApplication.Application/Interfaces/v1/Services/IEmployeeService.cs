using Microsoft.AspNetCore.JsonPatch;
using ScalableApplication.Application.DTOs.Employee;

namespace ScalableApplication.Application.Interfaces.v1.Services
{
    public interface IEmployeeService
    {
        Task<CustomHttpResponse<List<AllEmployeesDto>>> GetAllEmployees();
        Task<CustomHttpResponse<GetEmployeeDto?>> GetEmployee(Guid? id, string? userName);
        Task<CustomHttpResponse<GetEmployeeDto>> CreateEmployee(CreateEmployeeDto emp);
        Task<CustomHttpResponse<string>> UpdateEmployee(Guid empId, UpdateEmployeeDto? emp);
        Task<CustomHttpResponse<string>> RemoveEmployee(Guid empId);
        Task<CustomHttpResponse<string>> PatchEmployeeDetails(Guid empId, JsonPatchDocument<PatchEmployeeDto> patchDocument);
        Task<CustomHttpResponse<string>> AssignDepartment(Guid empId, Guid? depId);
        Task<CustomHttpResponse<List<EmployeeWithDepartmentIdDto>>> GetAllEmployeesWithDepartmentId();
    }
}
