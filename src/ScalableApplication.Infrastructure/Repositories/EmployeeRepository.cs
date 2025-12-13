using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.DTOs;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext db) : BaseRepository<Employee>(db), IEmployeeRepository
    {
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _db.Employees.Include(e => e.Department).ToListAsync();
        }

        public async Task<Employee?> GetEmployee(Guid? id)
        {
            return await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<Employee?> GetEmployee(string? userName)
        {
            return await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.UserName.Equals(userName));
        }
    }
}
