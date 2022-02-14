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
            //_tourContext.Tours.Add(tour);
            await _tourContext.SaveChangesAsync();
        }

        public async Task<TourModel> Get(Guid id)
        {
            return await _tourContext.Tours.FindAsync(id);
        }

        public async Task<IEnumerable<TourModel>> GetBest()
        {
            return await _tourContext.Tours.OrderBy(t => Guid.NewGuid()).Take(5).ToListAsync();
        }

        public async Task<IEnumerable<GroupedTours>> GetCountryGroups()
        {
            return await _tourContext.Tours.Select(t => new GroupedTours()
            {
                Country = t.Country,
                Count = _tourContext.Tours.Count(st => st.Country == t.Country)
            }).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCountryRegions(string country)
        {
            return await _tourContext.Tours.Where(t => t.Country == country)
                .Select(t => t.Region)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<TourModel>> GetFilteredTours(SearchRequest searchRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetRegionCities(string region)
        {
            return await _tourContext.Tours.Where(t => t.Region == region)
                .Select(t => t.Region)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<TourModel>> GetTours()
        {
            return await _tourContext.Tours.ToListAsync();
        }
    }
}
