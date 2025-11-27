using ScalableApplication.Application.DTOs;

namespace ScalableApplication.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<CustomHttpResponse<List<AllEmployeesDto>>> GetAllEmployees();
        Task<CustomHttpResponse<GetEmployeeDto?>> GetEmployee(Guid? id, string? userName);
    }
}
