using Microsoft.AspNetCore.JsonPatch;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.Exceptions;
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
                DepartmentName = e.Department is null ? null : e.Department.Name
            })];

            return new CustomHttpResponse<List<AllEmployeesDto>>(HttpStatusCode.OK, employees, null);
        }

        public async Task<CustomHttpResponse<GetEmployeeDto?>> GetEmployee(Guid? id, string? userName)
        {
            Employee? employee;
            switch (id, userName)
            {
                case (not null, not null):
                case (not null, null):
                    employee = await _employeeRepository.GetEmployee(id);
                    break;
                case (null, not null):
                    employee = await _employeeRepository.GetEmployee(userName);
                    break;
                default:
                    throw new ArgumentNullException(nameof(id) + "' and '" + nameof(userName));
            }

            if (employee is null)
            {
                throw new ResourceNotFoundException(nameof(employee));
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

        public async Task<CustomHttpResponse<GetEmployeeDto>> CreateEmployee(CreateEmployeeDto emp)
        {
            Employee employee = new Employee
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                UserName = emp.UserName
            };

            employee = await _employeeRepository.AddAsync(employee);

            if (await _employeeRepository.SaveChangesAsync())
                return new CustomHttpResponse<GetEmployeeDto>(HttpStatusCode.OK, new GetEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = null
                }, null);

            throw new SaveChangeFailedException(nameof(employee));
        }

        public async Task<CustomHttpResponse<string>> UpdateEmployee(Guid empId, UpdateEmployeeDto? emp)
        {
            if (emp is null)
            {
                throw new ArgumentNullException(nameof(emp));
            }

            Employee? employee = (await _employeeRepository.GetEmployee(empId)) ?? (await _employeeRepository.GetEmployee(emp.UserName));
            if (employee is null)
            {
                throw new ResourceNotFoundException(emp.UserName);
            }

            employee.FirstName = emp.FirstName;
            employee.LastName = emp.LastName;
            employee.Email = emp.Email;
            employee.DepartmentId = emp.DepartmentId;

            await _employeeRepository.SaveChangesAsync();

            return new CustomHttpResponse<string>()
            {
                StatusCode = HttpStatusCode.NoContent,
                Data = "Updated.",
                Error = null
            };
        }

        public async Task<CustomHttpResponse<string>> PatchEmployeeDetails(Guid empId, JsonPatchDocument<PatchEmployeeDto> patchDocument)
        {
            Employee? employee = await _employeeRepository.GetEmployee(empId);
            if (employee is null)
            {
                throw new ResourceNotFoundException(nameof(employee));
            }

            PatchEmployeeDto empDto = new PatchEmployeeDto
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                UserName = employee.UserName,
                DepartmentId = employee.DepartmentId
            };

            patchDocument.ApplyTo(empDto);
            
            employee.FirstName = empDto.FirstName;
            employee.LastName = empDto.LastName;
            employee.Email = empDto.Email;
            employee.UserName = empDto.UserName;
            employee.DepartmentId = empDto.DepartmentId;

            await _employeeRepository.SaveChangesAsync();

            return new CustomHttpResponse<string>(HttpStatusCode.NoContent, "Patched.", null);
        }

        public async Task<CustomHttpResponse<string>> RemoveEmployee(Guid empId)
        {
            Employee? employee = await _employeeRepository.GetEmployee(empId);
            if (employee is null)
            {
                throw new ResourceNotFoundException(nameof(employee));
            }
            await _employeeRepository.DeleteAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            return new CustomHttpResponse<string>(statusCode: HttpStatusCode.NoContent, data: "Deleted.", error: null);
        }

        public async Task<CustomHttpResponse<string>> AssignDepartment(Guid empId, Guid? depId)
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(empId);
            if (employee is null)
            {
                throw new ResourceNotFoundException(nameof(employee));
            }

            employee.DepartmentId = depId;
            await _employeeRepository.SaveChangesAsync();

            return new CustomHttpResponse<string>(HttpStatusCode.NoContent, "Updated.", null);
        }
    }
}
