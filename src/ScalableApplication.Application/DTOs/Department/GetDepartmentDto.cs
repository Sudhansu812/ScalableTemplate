namespace ScalableApplication.Application.DTOs.Department
{
    public class GetDepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedTime { get; set; }
    }
}
