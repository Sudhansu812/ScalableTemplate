using ScalableApplication.Application.DTOs.Department;

namespace ScalableApplication.Application.DTOs.Employee
{
    public class GetEmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime? DisabledTime { get; set; }
        public GetDepartmentDto? Department { get; set; }

        public GetEmployeeDto()
        {

        }

        public GetEmployeeDto(Guid id, string firstName, string? lastName, string email, GetDepartmentDto? department)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Department = department;
        }
    }
}
