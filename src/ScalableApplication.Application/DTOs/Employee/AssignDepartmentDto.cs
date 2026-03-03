namespace ScalableApplication.Application.DTOs.Employee
{
    public class AssignDepartmentDto
    {
        public Guid EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
