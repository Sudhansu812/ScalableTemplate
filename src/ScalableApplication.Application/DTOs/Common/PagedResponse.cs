namespace ScalableApplication.Application.DTOs.Common
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
        public int? MaxId { get; set; }

        public PagedResponse(List<T> data, int pageNumber, int pageSize, int totalCount, int? maxId = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((decimal)totalCount / (decimal)pageSize);
            Data = data;
            MaxId = maxId;
        }
    }
}
