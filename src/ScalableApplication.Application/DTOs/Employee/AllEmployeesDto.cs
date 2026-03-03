namespace ScalableApplication.Application.DTOs.Employee
{
    public class AllEmployeesDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? DepartmentName { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime? DisabledTime { get; set; }
    }
}
