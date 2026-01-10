using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Application.Interfaces.v1.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<List<Department>> GetActiveDepartments();
        Task<Department?> GetDepartmentEmployees(Guid id);
    }
}
