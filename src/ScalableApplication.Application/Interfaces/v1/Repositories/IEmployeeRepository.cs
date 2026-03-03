using ScalableApplication.Application.DTOs;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Application.Interfaces.v1.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> EmailExists(string email);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployee(Guid? id);
        Task<Employee?> GetEmployee(string? userName);
        Task<List<ReadEmployeeDto>> GetEmployees(int page = 0, int pageSize = 100);
        Task<(int, List<ReadEmployeeDto>)> GetEmployees(GetFilteredEmployeeDto filter);
        Task<int> GetEmployeesCount();
        Task<bool> UserNameExists(string userName);
    }
}
