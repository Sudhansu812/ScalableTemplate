using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScalableApplication.Domain.Entities
{
    [Table(nameof(Employee), Schema = "dbo")]
    public class Employee
    {
        [Key, Required]
        public Guid Id { get; set; }
        [Required, MinLength(1), MaxLength(256)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(256)]
        public string? LastName { get; set; } = string.Empty;
        [Required, MinLength(1), MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(1), MaxLength(64)]
        public string UserName { get; set; } = string.Empty;
        [ForeignKey("fk_emoployee_department")]
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        public DateTime? DisabledTime { get; set; }
    }
}
