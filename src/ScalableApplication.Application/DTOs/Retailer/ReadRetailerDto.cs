using System.ComponentModel.DataAnnotations;

namespace ScalableApplication.Application.DTOs.Retailer
{
    public class ReadRetailerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public DateTime LiveDate { get; set; }
        public DateTime? DisabledDate { get; set; }
    }
}
