using EPAM.DreamTour.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPAM.DreamTour.DataAccess.Data
{
    public interface ITourData
    {
        Task<TourModel> Get(Guid id);
        Task<IEnumerable<TourModel>> GetTours();
        Task<IEnumerable<TourModel>> GetBest();
        Task<IEnumerable<GroupedTours>> GetCountryGroups();
        Task<IEnumerable<TourModel>> GetFilteredTours(SearchRequest searchRequest);
    }
}