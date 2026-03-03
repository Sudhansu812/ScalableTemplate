using ScalableApplication.Application.DTOs.Common;
using ScalableApplication.Application.DTOs.Store;
using ScalableApplication.Application.Exceptions;
using ScalableApplication.Application.Interfaces.v1.Repositories;
using ScalableApplication.Application.Interfaces.v1.Services;
using System.Net;

namespace ScalableApplication.Application.Features.v1
{
    public class StoreService(IStoreRepository storeRepository) : IStoreService
    {
        private readonly IStoreRepository _storeRepository = storeRepository;

        public async Task<CustomHttpResponse<List<int>>> GetRetailerNumbers()
        {
            List<int> storeNumbers = await _storeRepository.GetRetailerNumbers();
            return new CustomHttpResponse<List<int>>(HttpStatusCode.OK, storeNumbers, null);
        }

        public async Task<CustomHttpResponse<List<int>>> GetStoreNumbers(int retailer)
        {
            List<int> storeNumbers = await _storeRepository.GetStoreNumbers(retailer);
            return new CustomHttpResponse<List<int>>(HttpStatusCode.OK, storeNumbers, null);
        }

        public async Task<CustomHttpResponse<List<ReadStoreCategoryDropdown>>> GetStoreCategoryIds()
        {
            List<ReadStoreCategoryDropdown> storeCategories = await _storeRepository.GetStoreCategoryIds();
            return new CustomHttpResponse<List<ReadStoreCategoryDropdown>>(HttpStatusCode.OK, storeCategories, null);
        }

        public async Task<CustomHttpResponse<PagedResponse<ReadStoreDto>>> GetStoresByFilter(StoreFilterDto filter)
        {
            if (filter.Page is null || filter.PageSize is null || filter.Page < 0 || filter.PageSize <= 0)
            {
                throw new InvalidPaginationException();
            }

            (int count, List<ReadStoreDto> stores) = await _storeRepository.GetStoresByFilter(filter);
            PagedResponse<ReadStoreDto> response = new PagedResponse<ReadStoreDto>(stores, (int)filter.Page, (int)filter.PageSize, count);

            return new CustomHttpResponse<PagedResponse<ReadStoreDto>>(HttpStatusCode.OK, response, null);
        }
    }
}
