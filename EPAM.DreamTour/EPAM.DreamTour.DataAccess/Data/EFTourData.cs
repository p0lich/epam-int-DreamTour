using EPAM.DreamTour.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.DreamTour.DataAccess.Data
{
    public class EFTourData : ITourData
    {
        private TourContext _tourContext;
        private IMemoryCache _cache;

        public EFTourData(TourContext tourContext, IMemoryCache cache)
        {
            _tourContext = tourContext;
            _cache = cache;
        }

        public async Task Add(CreateTourModel tour)
        {
            _tourContext.Tour.Add(new TourModel()
            {
                Country = tour.Country,
                Region = tour.Region,
                District = tour.District,
                City = tour.City,
                MinimalPrice = tour.MinimalPrice,
                MaximumPrice = tour.MaximumPrice,
                DaysCount = tour.DaysCount,
                BeginDate = tour.BeginDate
            });

            int addCount = await _tourContext.SaveChangesAsync();

            if (addCount > 0)
            {
                _cache.Set($"MemoryCache_DreamTour_Add_{DateTime.Now.ToString("dd - MM - yyyy - hh:mm")}",
                    tour, new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }

        public async Task<TourModel> Get(Guid id)
        {
            return await _tourContext.Tour.FindAsync(id);
        }

        public async Task<IEnumerable<TourModel>> GetBest()
        {
            if (!_cache.TryGetValue($"MemoryCache_DreamTour_GetBest_{DateTime.Now.ToString("dd - MM - yyyy - hh:mm")}",
                out IEnumerable<TourModel> bestTours))
            {
                bestTours = await _tourContext.Tour
                    .OrderBy(t => Guid.NewGuid())
                    .Take(3)
                    .ToListAsync();

                if (bestTours != null)
                {
                    _cache.Set($"MemoryCache_DreamTour_GetBest_{DateTime.Now.ToString("dd - MM - yyyy - hh:mm")}",
                        bestTours, new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
            }

            return bestTours;
        }

        public async Task<IEnumerable<GroupedTours>> GetCountryGroups()
        {
            return await _tourContext.Tour
                .GroupBy(t => t.Country)
                .Select(g => new GroupedTours()
                {
                    Country = g.Key,
                    Count = g.Count()
                }).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCountryRegions(string country)
        {
            return await _tourContext.Tour
                .Where(t => t.Country == country)
                .Select(t => t.Region)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<TourModel>> GetFilteredTours(SearchRequest searchRequest)
        {
            return await _tourContext.Tour
                .Where(t =>
                    (t.MinimalPrice >= searchRequest.MinimalPrice || !searchRequest.MinimalPrice.HasValue) &&
                    (t.MaximumPrice <= searchRequest.MaximumPrice || !searchRequest.MaximumPrice.HasValue) &&
                    (t.DaysCount == searchRequest.DaysCount || !searchRequest.DaysCount.HasValue) &&
                    (t.BeginDate == searchRequest.BeginDate || !searchRequest.BeginDate.HasValue) &&
                    (EF.Functions.Like(t.Country, $"%{searchRequest.Country}%") || string.IsNullOrEmpty(searchRequest.Country)) &&
                    (EF.Functions.Like(t.Region, $"%{searchRequest.Region}%") || string.IsNullOrEmpty(searchRequest.Region)) &&
                    (EF.Functions.Like(t.District, $"%{searchRequest.District}%") || string.IsNullOrEmpty(searchRequest.District)) &&
                    (EF.Functions.Like(t.City, $"%{searchRequest.City}%") || string.IsNullOrEmpty(searchRequest.City)))
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRegionCities(string region)
        {
            return await _tourContext.Tour
                .Where(t => t.Region == region)
                .Select(t => t.Region)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<TourModel>> GetTours()
        {
            return await _tourContext.Tour.ToListAsync();
        }
    }
}
