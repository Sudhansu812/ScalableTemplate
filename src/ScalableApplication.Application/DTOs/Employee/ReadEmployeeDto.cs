namespace ScalableApplication.Application.DTOs.Employee
{
    public class ReadEmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
        public string? DepartmentName { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime? DisabledTime { get; set; }
    }
}
