using Microsoft.AspNetCore.JsonPatch;
using ScalableApplication.Application.DTOs.Common;
using ScalableApplication.Application.DTOs.Department;
using ScalableApplication.Application.DTOs.Employee;

namespace ScalableApplication.Application.Interfaces.v1.Services
{
    public interface IDepartmentService
    {
        Task<CustomHttpResponse<DepartmentDto>> CreateDepartment(DepartmentDto? departmentDto);
        Task<CustomHttpResponse<List<ActiveDepartmentDto>>> GetActiveDepartments();
        Task<CustomHttpResponse<GetDepartmentDto>> GetDepartment(Guid id);
        Task<CustomHttpResponse<List<AllEmployeesDto>?>> GetDepartmentEmployees(Guid id);
        Task<CustomHttpResponse<List<GetDepartmentDto>>> GetDepartments(Guid id);
        Task<CustomHttpResponse<bool>> PatchDepartment(Guid id, JsonPatchDocument departmentPatch);
        Task<CustomHttpResponse<bool>> UpdateDepartment(Guid id, DepartmentDto? departmentDto);
    }
}
