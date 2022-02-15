using EPAM.DreamTour.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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

        public EFTourData(TourContext tourContext)
        {
            _tourContext = tourContext;
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
            await _tourContext.SaveChangesAsync();
        }

        public async Task<TourModel> Get(Guid id)
        {
            return await _tourContext.Tour.FindAsync(id);
        }

        public async Task<IEnumerable<TourModel>> GetBest()
        {
            return await _tourContext.Tour.OrderBy(t => Guid.NewGuid()).Take(3).ToListAsync();
        }

        public async Task<IEnumerable<GroupedTours>> GetCountryGroups()
        {
            return await _tourContext.Tour.Select(t => new GroupedTours()
            {
                Country = t.Country,
                Count = _tourContext.Tour.Count(st => st.Country == t.Country)
            }).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCountryRegions(string country)
        {
            return await _tourContext.Tour.Where(t => t.Country == country)
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
            return await _tourContext.Tour.Where(t => t.Region == region)
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
