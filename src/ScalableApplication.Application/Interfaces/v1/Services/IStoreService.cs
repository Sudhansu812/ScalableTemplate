using ScalableApplication.Application.DTOs.Common;
using ScalableApplication.Application.DTOs.Store;

namespace ScalableApplication.Application.Interfaces.v1.Services
{
    public interface IStoreService
    {
        Task<CustomHttpResponse<List<int>>> GetRetailerNumbers();
        Task<CustomHttpResponse<List<ReadStoreCategoryDropdown>>> GetStoreCategoryIds();
        Task<CustomHttpResponse<List<int>>> GetStoreNumbers(int retailer);
        Task<CustomHttpResponse<PagedResponse<ReadStoreDto>>> GetStoresByFilter(StoreFilterDto filter);
    }
}
