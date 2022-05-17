using EPAM.DreamTour.DataAccess.DbAccess;
using EPAM.DreamTour.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EPAM.DreamTour.DataAccess.Data
{
    public class TourData : ITourData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public TourData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public Task<IEnumerable<TourModel>> GetTours()
        {
            return _sqlDataAccess.LoadData<TourModel, dynamic>("dbo.spTour_GetAll", new { });
        }

        public async Task<TourModel> Get(Guid id)
        {
            var tour = await _sqlDataAccess.LoadData<TourModel, dynamic>("dbo.spTour_Get", new { id });

            return tour.FirstOrDefault();
        }

        public Task<IEnumerable<TourModel>> GetBest()
        {
            return _sqlDataAccess.LoadData<TourModel, dynamic>("dbo.spTour_GetBest", new { });
        }

        public Task<IEnumerable<GroupedTours>> GetCountryGroups()
        {
            return _sqlDataAccess.LoadData<GroupedTours, dynamic>("dbo.spTour_GroupCountry", new { });
        }

        public Task<IEnumerable<TourModel>> GetFilteredTours(SearchRequest searchRequest)
        {
            var parameters = new
            {
                country = searchRequest.Country,
                region = searchRequest.Region,
                district = searchRequest.District,
                city = searchRequest.City,
                minimalPrice = searchRequest.MinimalPrice,
                maximumPrice = searchRequest.MaximumPrice,
                daysCount = searchRequest.DaysCount,
                beginDate = searchRequest.BeginDate
            };

            return _sqlDataAccess.LoadData<TourModel, dynamic>("dbo.spTour_GetFiltered", parameters);
        }

        public async Task Add(CreateTourModel tour)
        {
            await _sqlDataAccess.SaveData<CreateTourModel>("dbo.spTour_Add", tour);
        }

        public async Task<IEnumerable<string>> GetCountryRegions(string country)
        {
            return await _sqlDataAccess.LoadData<string, dynamic>("dbo.spTour_GetCountryRegions", new { country });
        }

        public async Task<IEnumerable<string>> GetRegionCities(string region)
        {
            return await _sqlDataAccess.LoadData<string, dynamic>("dbo.spTour_GetRegionCities", new { region });
        }
    }
}
