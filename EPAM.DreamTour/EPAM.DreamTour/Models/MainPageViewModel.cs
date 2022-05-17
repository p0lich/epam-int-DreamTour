using EPAM.DreamTour.DataAccess.Models;
using System.Collections.Generic;

namespace EPAM.DreamTour.Models
{
    public class MainPageViewModel
    {
        public IEnumerable<TourModel> BestTours { get; set; }

        public IEnumerable<GroupedTours> CountryGroups { get; set; }

        public SearchRequest SearchRequest { get; set; }
    }
}
