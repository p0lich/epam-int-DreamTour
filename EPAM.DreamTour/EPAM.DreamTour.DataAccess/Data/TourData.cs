using EPAM.DreamTour.DataAccess.DbAccess;
using EPAM.DreamTour.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EPAM.DreamTour.DataAccess.Data
{
    public class TourData
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
    }
}
