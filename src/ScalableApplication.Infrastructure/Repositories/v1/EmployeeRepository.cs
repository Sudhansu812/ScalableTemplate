using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.DTOs.Employee;
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

        public async Task<Employee?> GetEmployee(Guid? id) => await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id.Equals(id));

        public async Task<bool> EmailExists(string email) => await _db.Employees.FirstOrDefaultAsync(e => e.Email.Equals(email)) is not null;

        public async Task<bool> UserNameExists(string userName) => await _db.Employees.FirstOrDefaultAsync(e => e.UserName.Equals(userName)) is not null;

        public async Task<Employee?> GetEmployee(string? userName)
        {
            return await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.UserName.Equals(userName));
        }

        public async Task<List<ReadEmployeeDto>> GetEmployees(int page = 0, int pageSize = 100) => await
            (
                from e in _db.Employees.AsNoTracking()
                join d in _db.Departments.AsNoTracking() on e.DepartmentId equals d.Id into deptGroup
                from d in deptGroup.DefaultIfEmpty()
                select new ReadEmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    UserName = e.UserName,
                    DepartmentName = d == null ? null : d.Name,
                    CreatedTime = e.CreatedTime,
                    DisabledTime = e.DisabledTime
                }
            )
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ThenBy(e => e.Id)
            .Skip(page * pageSize).Take(pageSize)
            .ToListAsync();

        public async Task<int> GetEmployeesCount() => await
            _db.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .CountAsync();

        public async Task<(int, List<ReadEmployeeDto>)> GetEmployees(GetFilteredEmployeeDto filter)
        {
            var employees =
                from e in _db.Employees.AsNoTracking()
                join d in _db.Departments.AsNoTracking() on e.DepartmentId equals d.Id into deptGroup
                from d in deptGroup.DefaultIfEmpty()
                select new
                {
                    emp = e,
                    dep = d
                }
            ;

            if (filter.FirstName is not null)
            {
                employees = employees.Where(e => e.emp.FirstName.ToLower().Equals(filter.FirstName.ToLower()));
            }

            if (filter.LastName is not null)
            {
                employees = employees.Where(e => e.emp.LastName!.ToLower().Equals(filter.LastName.ToLower()));
            }

            if (filter.Department is not null)
            {
                employees = employees.Where(e => e.emp.DepartmentId.Equals(filter.Department));
            }

            if (filter.IsActive is not null && filter.IsActive != false)
            {
                employees = employees.Where(e => e.emp.DisabledTime == null || e.emp.DisabledTime >= DateTime.Today);
            }

            int count = await employees.CountAsync();

            if (filter.Page is not null && filter.PageSize is not null)
            {
                employees = 
                    employees
                    .OrderBy(e => e.emp.FirstName)
                    .ThenBy(e => e.emp.LastName)
                    .ThenBy(e => e.emp.Id)
                    .Skip((int)filter.Page * (int)filter.PageSize).Take((int)filter.PageSize)
                ;
            }


            List<ReadEmployeeDto> employeeList = await employees.Select(e => new ReadEmployeeDto
            {
                Id = e.emp.Id,
                FirstName = e.emp.FirstName,
                LastName = e.emp.LastName,
                Email = e.emp.Email,
                UserName = e.emp.UserName,
                DepartmentName = e.dep == null ? null : e.dep.Name,
                CreatedTime = e.emp.CreatedTime,
                DisabledTime = e.emp.DisabledTime
            }).ToListAsync();

            return (count, employeeList);
        }
    }
}
