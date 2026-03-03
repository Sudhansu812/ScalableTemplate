namespace ScalableApplication.Application.DTOs.Employee
{
    public class GetFilteredEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? Department { get; set; }
        public bool? IsActive { get; set; }
        public int? Page { get; set; } = 0;
        public int? PageSize { get; set; } = 0;
    }
}
