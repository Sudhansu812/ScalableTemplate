namespace ScalableApplication.Application.DTOs.Store
{
    public class StoreFilterDto
    {
        public int? RetailerCode { get; set; }
        public int? StoreCode { get; set; }
        public int? StoreCategoryid { get; set; }
        public bool? IsActive { get; set; }
        public int? Page { get; set; } = 0;
        public int? PageSize { get; set; } = 100;
    }
}
