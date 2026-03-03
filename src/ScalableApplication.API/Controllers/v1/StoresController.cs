using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScalableApplication.Application.DTOs.Common;
using ScalableApplication.Application.DTOs.Store;
using ScalableApplication.Application.Interfaces.v1.Services;

namespace ScalableApplication.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController(IStoreService storeService) : ControllerBase
    {
        private readonly IStoreService _storeService = storeService;

        [HttpGet("numbers-by-retailer")]
        public async Task<IActionResult> GetStoreNumbers(int retailer)
        {
            CustomHttpResponse<List<int>> response = await _storeService.GetStoreNumbers(retailer);
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpGet("retailer-numbers")]
        public async Task<IActionResult> GetRetailerNumbers()
        {
            CustomHttpResponse<List<int>> response = await _storeService.GetRetailerNumbers();
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpGet("category-ids")]
        public async Task<IActionResult> GetStoreCategoryIds()
        {
            CustomHttpResponse<List<ReadStoreCategoryDropdown>> response = await _storeService.GetStoreCategoryIds();
            return StatusCode((int)response.StatusCode, response.Data);
        }

        [HttpGet("by-filters")]
        public async Task<IActionResult> GetStoresByFilter([FromQuery] StoreFilterDto filter)
        {
            CustomHttpResponse<PagedResponse<ReadStoreDto>> response = await _storeService.GetStoresByFilter(filter);
            return StatusCode((int)response.StatusCode, response.Data);
        }
    }
}
