using ScalableApplication.Application.DTOs;
using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployee(Guid? id);
        Task<Employee?> GetEmployee(string? userName);
    }
}
