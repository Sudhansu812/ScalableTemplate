using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.DTOs.Store;
using ScalableApplication.Application.Interfaces.v1.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories.v1
{
    public class StoreRepository(AppDbContext db) : BaseRepository<Employee>(db), IStoreRepository
    {
        public async Task<List<int>> GetRetailerNumbers()
        {
            return await _db.Retailers.Select(r => r.Code).ToListAsync();
        }

        public async Task<List<int>> GetStoreNumbers(int retailer)
        {
            return await _db.Stores.Where(r => r.RetailerCode == retailer).Select(s => s.Code).ToListAsync();
        }

        public async Task<List<ReadStoreCategoryDropdown>> GetStoreCategoryIds()
        {
            return await _db.StoreCategories.Select(sc => new ReadStoreCategoryDropdown { Id = sc.Id, Name = sc.Name }).ToListAsync();
        }

        public async Task<(int, List<ReadStoreDto>)> GetStoresByFilter(StoreFilterDto filter)
        {
            var stores =
               from s in _db.Stores.AsNoTracking()
               join r in _db.Retailers.AsNoTracking() on s.RetailerCode equals r.Code
               join sc in _db.StoreCategories.AsNoTracking() on s.StoreCategoryId equals sc.Id
               select new
               {
                   str = s,
                   rtlr = r,
                   sct = sc
               }
           ;

            if (filter.RetailerCode is not null)
            {
                stores = stores.Where(s => s.str.RetailerCode == filter.RetailerCode);
            }

            if (filter.StoreCode is not null)
            {
                stores = stores.Where(s => s.str.Code == filter.StoreCode);
            }

            if (filter.StoreCategoryid is not null)
            {
                stores = stores.Where(s => s.str.StoreCategoryId == filter.StoreCategoryid);
            }

            if (filter.IsActive is not null && filter.IsActive != false)
            {
                stores = stores.Where(s => s.str.DisabledDate == null || s.str.DisabledDate > DateTime.Today);
            }

            int count = await stores.CountAsync();

            if (filter.Page is not null && filter.PageSize is not null)
            {
                stores =
                    stores
                    .OrderBy(s => s.str.RetailerCode)
                    .ThenBy(s => s.str.Code)
                    .ThenBy(s => s.str.DisabledDate)
                    .Skip((int)filter.Page * (int)filter.PageSize).Take((int)filter.PageSize)
                ;
            }

            List<ReadStoreDto> storelist = await stores.Select(s => new ReadStoreDto
            {
                Id = s.str.Id,
                RetailerCode = s.str.RetailerCode,
                Code = s.str.Code,
                Name = s.str.Name,
                RetailerName = s.rtlr.Name,
                LiveDate = s.str.LiveDate,
                DisabledDate = s.str.DisabledDate,
                StoreCategoryName = s.sct.Name
            }).ToListAsync();

            return (count, storelist);
        }
    }
}
