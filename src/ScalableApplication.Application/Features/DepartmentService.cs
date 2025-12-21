using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using ScalableApplication.Application.DTOs.Department;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.Exceptions;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Application.Interfaces.Services;
using ScalableApplication.Domain.Entities;
using System.Net;

namespace ScalableApplication.Application.Features
{
    public class DepartmentService(IDepartmentRepository repository) : IDepartmentService
    {
        private readonly IDepartmentRepository _repository = repository;

        public async Task<CustomHttpResponse<DepartmentDto>> CreateDepartment(DepartmentDto? departmentDto)
        {
            if (departmentDto == null)
            {
                throw new ArgumentNullException(nameof(departmentDto));
            }

            Department department = new Department
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                IsActive = departmentDto.IsActive
            };

            department = await _repository.AddAsync(department);

            if (await _repository.SaveChangesAsync())
            {
                return new CustomHttpResponse<DepartmentDto>(HttpStatusCode.Created, departmentDto);
            }
            throw new SaveChangeFailedException(nameof(department));
        }

        public async Task<CustomHttpResponse<GetDepartmentDto>> GetDepartment(Guid id)
        {
            Department department = await _repository.GetByIdAsync(id) ?? throw new ResourceNotFoundException(nameof(department));
            return new CustomHttpResponse<GetDepartmentDto>(statusCode: HttpStatusCode.Found, data: new GetDepartmentDto
            {
                Id = id,
                Name = department.Name,
                Description = department.Description,
                IsActive = department.IsActive
            }
            , error: null);
        }

        public async Task<CustomHttpResponse<List<GetDepartmentDto>>> GetDepartments(Guid id)
        {
            List<GetDepartmentDto> departments = (await _repository.GetAllAsync()).Select(d => new GetDepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                IsActive = d.IsActive
            }).ToList();

            return new CustomHttpResponse<List<GetDepartmentDto>>(HttpStatusCode.OK, departments, null);
        }

        public async Task<CustomHttpResponse<List<AllEmployeesDto>?>> GetDepartmentEmployees(Guid id)
        {
            Department department = await _repository.GetDepartmentEmployees(id) ?? throw new ResourceNotFoundException(nameof(department));

            if (department.Employees is null) {
                return new CustomHttpResponse<List<AllEmployeesDto>?>(HttpStatusCode.NotFound, null);
            }
            
            string departmentName = department.Name;
            List<AllEmployeesDto> employees = department.Employees.Select(e => new AllEmployeesDto
            {
                Id=e.Id,
                DepartmentName = departmentName,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).ToList();

            return new CustomHttpResponse<List<AllEmployeesDto>?>(HttpStatusCode.Found, employees);
        }

        public async Task<CustomHttpResponse<bool>> UpdateDepartment(Guid id, DepartmentDto? departmentDto)
        {
            if (departmentDto is null) throw new ArgumentNullException(nameof(departmentDto));
            
            Department department = await _repository.GetByIdAsync(id) ?? throw new ResourceNotFoundException(nameof(department));
            department.Name = departmentDto.Name;
            department.Description = departmentDto.Description;
            department.IsActive = departmentDto.IsActive;

            await _repository.SaveChangesAsync();

            return new CustomHttpResponse<bool>(HttpStatusCode.NoContent, true, null);
        }

        public async Task<CustomHttpResponse<bool>> PatchDepartment(Guid id, JsonPatchDocument departmentPatch)
        {
            Department? department = await _repository.GetByIdAsync(id) ?? throw new ResourceNotFoundException(nameof(department));
            DepartmentDto deptDto = new DepartmentDto
            {
                Name = department.Name,
                Description = department.Description,
                IsActive = department.IsActive
            };
            
            departmentPatch.ApplyTo(deptDto);

            department.Name = deptDto.Name;
            department.Description = deptDto.Description;
            department.IsActive = department.IsActive;

            await _repository.SaveChangesAsync();

            return new CustomHttpResponse<bool>(HttpStatusCode.NoContent, true, null);
        }

        public async Task<CustomHttpResponse<bool>> InactivateDepartment(Guid id)
        {
            Department department = await _repository.GetByIdAsync(id) ?? throw new ResourceNotFoundException(nameof(department));
            department.IsActive = false;
            await _repository.SaveChangesAsync();

            return new CustomHttpResponse<bool>(HttpStatusCode.NoContent, true, null);
        }
    }
}
