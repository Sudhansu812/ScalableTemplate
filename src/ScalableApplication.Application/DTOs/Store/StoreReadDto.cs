using ScalableApplication.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ScalableApplication.Application.DTOs.Store
{
    public class ReadStoreDto
    {
        public Guid Id { get; set; }
        public int RetailerCode { get; set; }
        public string RetailerName { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string StoreCategoryName { get; set; }
        public DateTime LiveDate { get; set; }
        public DateTime? DisabledDate { get; set; }
    }
}
