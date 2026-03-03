using ScalableApplication.Application.DTOs.Store;

namespace ScalableApplication.Application.Interfaces.v1.Repositories
{
    public interface IStoreRepository
    {
        Task<List<int>> GetRetailerNumbers();
        Task<List<ReadStoreCategoryDropdown>> GetStoreCategoryIds();
        Task<List<int>> GetStoreNumbers(int retailer);
        Task<(int, List<ReadStoreDto>)> GetStoresByFilter(StoreFilterDto filter);
    }
}
