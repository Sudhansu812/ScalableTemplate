using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScalableApplication.Domain.Entities
{
    [Table(nameof(Retailer), Schema = "dbo")]
    public class Retailer
    {
        [Key, Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public DateTime LiveDate { get; set; }
        public DateTime? DisabledDate { get; set; }
        public ICollection<Store>? Stores { get; set; }
    }
}
