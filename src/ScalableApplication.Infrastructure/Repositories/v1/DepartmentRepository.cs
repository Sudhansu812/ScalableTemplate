using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.Interfaces.v1.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories.v1
{
    public class DepartmentRepository(AppDbContext db) : BaseRepository<Department>(db), IDepartmentRepository
    {
        public async Task<List<Department>> GetActiveDepartments()
        {
            return await _db.Departments.Where(d => d.IsActive == true).ToListAsync();
        }

        public async Task<Department?> GetDepartmentEmployees(Guid id)
        {
            return await _db.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
