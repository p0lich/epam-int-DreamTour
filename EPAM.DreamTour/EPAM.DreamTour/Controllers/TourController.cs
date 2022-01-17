using EPAM.DreamTour.DataAccess.Data;
using EPAM.DreamTour.DataAccess.Models;
using EPAM.DreamTour.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EPAM.DreamTour.Controllers
{
    public class TourController : Controller
    {
        private readonly ILogger<TourController> _logger;
        private readonly ITourData _tourData;

        public TourController(ILogger<TourController> logger, ITourData tourData)
        {
            _logger = logger;
            _tourData = tourData;
        }

        // GET: TourController
        public async Task<ActionResult> Index()
        {
            var bestTours = await _tourData.GetBest();
            var groupedCountry = await _tourData.GetCountryGroups();

            MainPageViewModel mainPageView = new MainPageViewModel()
            {
                BestTours = bestTours,
                CountryGroups = groupedCountry
            };

            return View(mainPageView);
        }

        public async Task<ActionResult> Search(SearchRequest searchRequest)
        {
            var foundedTours = await _tourData.GetFilteredTours(searchRequest);

            return View(foundedTours);
        }
    }
}
