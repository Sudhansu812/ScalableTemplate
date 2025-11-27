using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.DTOs;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext db) : IEmployeeRepository
    {
        private readonly AppDbContext _db = db;
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _db.Employees.Include(e => e.Department).ToListAsync();
        }

        public async Task<Employee?> GetEmployee(Guid? id)
        {
            return await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public Task<Employee?> GetEmployee(string? userName)
        {
            return _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.UserName.Equals(userName));
        }
    }
}
