using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.Interfaces.v1.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories.v1
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

        public async Task<bool> EmailExists(string email) => await _db.Employees.FirstOrDefaultAsync(e => e.Email.Equals(email)) is not null;

        public async Task<bool> UserNameExists(string userName) => await _db.Employees.FirstOrDefaultAsync(e => e.UserName.Equals(userName)) is not null;

        public async Task<Employee?> GetEmployee(string? userName)
        {
            return await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.UserName.Equals(userName));
        }
    }
}
