using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext db) : BaseRepository<Department>(db), IDepartmentRepository
    {
        public async Task<Department?> GetDepartmentEmployees(Guid id)
        {
            return await _db.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
