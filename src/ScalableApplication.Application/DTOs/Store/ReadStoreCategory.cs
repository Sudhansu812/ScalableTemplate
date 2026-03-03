using System.ComponentModel.DataAnnotations;

namespace ScalableApplication.Application.DTOs.Store
{
    public class ReadStoreCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LiveDate { get; set; }
        public DateTime? DisabledDate { get; set; }
    }
}
