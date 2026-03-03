using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScalableApplication.Domain.Entities
{
    [Table(nameof(StoreCategory), Schema = "dbo")]
    public class StoreCategory
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime LiveDate { get; set; }
        public DateTime? DisabledDate { get; set; }
        public ICollection<Store> Stores { get; set; }
    }
}
