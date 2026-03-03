using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScalableApplication.Domain.Entities
{
    [Table(nameof(Store), Schema = "dbo")]
    public class Store
    {
        [Key, Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int RetailerCode { get; set; }
        public Retailer Retailer { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public int StoreCategoryId { get; set; }
        public StoreCategory Category { get; set; }
        [Required]
        public DateTime LiveDate { get; set; }
        public DateTime? DisabledDate { get; set; }
    }
}
