using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScalableApplication.Application.DTOs.Common;
using ScalableApplication.Application.DTOs.Department;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.Exceptions;
using ScalableApplication.Application.Interfaces.v1.Repositories;
using ScalableApplication.Application.Interfaces.v1.Services;
using ScalableApplication.Domain.Entities;
using System.Net;

namespace ScalableApplication.Application.Features.v1
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
                DepartmentName = e.Department is null ? null : e.Department.Name,
                CreatedTime = e.CreatedTime,
                DisabledTime = e.DisabledTime
            })];

            return new CustomHttpResponse<List<AllEmployeesDto>>(HttpStatusCode.OK, employees, null);
        }

        public async Task<CustomHttpResponse<List<EmployeeWithDepartmentIdDto>>> GetAllEmployeesWithDepartmentId()
        {
            List <EmployeeWithDepartmentIdDto> employees = [.. (await _employeeRepository.GetAllEmployees()).Select(e => new EmployeeWithDepartmentIdDto {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName!,
                DepartmentId = e.Department is null ? null : e.Department.Id
            })];

            return new CustomHttpResponse<List<EmployeeWithDepartmentIdDto>>(HttpStatusCode.OK, employees, null);
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
                    IsActive = employee.Department.IsActive,
                    CreatedTime = employee.Department.CreatedTime,
                } : null,
                Email = employee.Email,
                CreatedTime = employee.CreatedTime,
                DisabledTime = employee.DisabledTime
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
                UserName = emp.UserName,
                CreatedTime = DateTime.UtcNow,
                DisabledTime = null
            };

            if (await _employeeRepository.UserNameExists(emp.UserName) || await _employeeRepository.EmailExists(emp.Email))
            {
                throw new DuplicateResourceException($"{nameof(employee.UserName)} / {nameof(employee.Email)}");
            }

            employee = await _employeeRepository.AddAsync(employee);

            if (await _employeeRepository.SaveChangesAsync())
                return new CustomHttpResponse<GetEmployeeDto>(HttpStatusCode.OK, new GetEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = null,
                    CreatedTime = employee.CreatedTime,
                    DisabledTime = employee.DisabledTime
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
            employee.CreatedTime = emp.CreatedTime;
            employee.DisabledTime = emp.DisabledTime;

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
                DepartmentId = employee.DepartmentId,
                CreatedTime = employee.CreatedTime,
                DisabledTime = employee.DisabledTime
            };

            patchDocument.ApplyTo(empDto);
            
            employee.FirstName = empDto.FirstName;
            employee.LastName = empDto.LastName;
            employee.Email = empDto.Email;
            employee.UserName = empDto.UserName;
            employee.DepartmentId = empDto.DepartmentId;
            employee.CreatedTime = empDto.CreatedTime;
            employee.DisabledTime = empDto.DisabledTime;

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

        public async Task<CustomHttpResponse<string>> AssignDepartment(AssignDepartmentDto employeeDepartment)
        {
            if (employeeDepartment is null)
            {
                throw new ArgumentNullException(nameof(employeeDepartment));
            }
            Employee? employee = await _employeeRepository.GetByIdAsync(employeeDepartment.EmployeeId);
            if (employee is null)
            {
                throw new ResourceNotFoundException(nameof(employee));
            }

            employee.DepartmentId = employeeDepartment.DepartmentId;
            await _employeeRepository.SaveChangesAsync();

            return new CustomHttpResponse<string>(HttpStatusCode.NoContent, "Updated.", null);
        }

        public async Task<CustomHttpResponse<PagedResponse<ReadEmployeeDto>>> GetEmployees(int? page = 0, int? pageSize = 100)
        {
            if (page is null || pageSize is null || page < 0 || pageSize <= 0)
            {
                throw new InvalidPaginationException();
            }
            List<ReadEmployeeDto> employees = await _employeeRepository.GetEmployees((int)page, (int)pageSize);

            PagedResponse<ReadEmployeeDto> pagedEmployees = new PagedResponse<ReadEmployeeDto>(employees, (int)page, (int)pageSize, await _employeeRepository.GetEmployeesCount());
            return new CustomHttpResponse<PagedResponse<ReadEmployeeDto>>(HttpStatusCode.OK, pagedEmployees, null);
        }

        public async Task<CustomHttpResponse<PagedResponse<ReadEmployeeDto>>> GetEmployees(GetFilteredEmployeeDto filter)
        {
            if (filter.Page is null || filter.PageSize is null || filter.Page < 0 || filter.PageSize <= 0)
            {
                throw new InvalidPaginationException();
            }

            (int count, List<ReadEmployeeDto> employees) = await _employeeRepository.GetEmployees(filter);
            PagedResponse<ReadEmployeeDto> response = new PagedResponse<ReadEmployeeDto>(employees, (int)filter.Page, (int)filter.PageSize, count);

            return new CustomHttpResponse<PagedResponse<ReadEmployeeDto>>(HttpStatusCode.OK, response, null);
        }
    }
}
