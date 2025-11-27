using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScalableApplication.Domain.Entities
{
    [Table("Department", Schema = "dbo")]
    public class Department
    {
        [Key, Required]
        public Guid Id { get; set; }
        [Required, MinLength(1), MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public IEnumerable<Employee>? Employees { get; set; }
    }
}
