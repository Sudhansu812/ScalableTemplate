using ScalableApplication.Application.DTOs;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Application.Interfaces.Services;
using ScalableApplication.Domain.Entities;
using System.Net;

namespace ScalableApplication.Application.Features
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        public async Task<CustomHttpResponse<List<AllEmployeesDto>>> GetAllEmployees()
        {
            List<AllEmployeesDto> employees = [.. (await _employeeRepository.GetAllEmployees()).Select(e => new AllEmployeesDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName!,
                DepartmentName = e.Department!.Name
            })];

            return new CustomHttpResponse<List<AllEmployeesDto>>(HttpStatusCode.OK, employees, null);
        }

        public async Task<CustomHttpResponse<GetEmployeeDto?>> GetEmployee(Guid? id, string? userName)
        {
            Employee? employee;
            switch(id, userName)
            {
                case (not null, not null):
                case (not null, null):
                    employee = await _employeeRepository.GetEmployee(id);
                    break;
                case (null, not null):
                    employee = await _employeeRepository.GetEmployee(userName);
                    break;
                default:
                    throw new ArgumentException(nameof(id), nameof(userName));
            }

            if (employee is null)
            {
                return new CustomHttpResponse<GetEmployeeDto?>(HttpStatusCode.NotFound, null, null);
            }

            GetEmployeeDto emp = new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Department = employee.Department is not null ? new GetDepartmentDto
                {
                    Id = employee.Department.Id,
                    Name = employee.Department.Name,
                    Description = employee.Department.Description,
                    IsActive = employee.Department.IsActive
                } : null,
                Email = employee.Email
            };

            return new CustomHttpResponse<GetEmployeeDto?>(HttpStatusCode.OK, emp, null);
        }
    }
}
