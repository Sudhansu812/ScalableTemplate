using ScalableApplication.Domain.Entities;

namespace ScalableApplication.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> GetDepartmentEmployees(Guid id);
    }
}
