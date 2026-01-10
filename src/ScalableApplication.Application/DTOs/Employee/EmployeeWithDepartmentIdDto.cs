namespace ScalableApplication.Application.DTOs.Employee
{
    public class EmployeeWithDepartmentIdDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
