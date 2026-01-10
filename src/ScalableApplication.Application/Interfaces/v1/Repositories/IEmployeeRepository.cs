using ScalableApplication.Application.DTOs;
using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Application.Interfaces.v1.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> EmailExists(string email);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployee(Guid? id);
        Task<Employee?> GetEmployee(string? userName);
        Task<bool> UserNameExists(string userName);
    }
}
