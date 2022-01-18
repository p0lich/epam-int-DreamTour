using EPAM.DreamTour.DataAccess.Data;
using EPAM.DreamTour.DataAccess.Models;
using EPAM.DreamTour.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EPAM.DreamTour.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.DreamTour.Controllers
{
    public class TourController : Controller
    {
        private readonly ILogger<TourController> _logger;
        private readonly ITourData _tourData;
        private readonly IDistributedCache _cache;

        public TourController(ILogger<TourController> logger, ITourData tourData, IDistributedCache cache)
        {
            _logger = logger;
            _tourData = tourData;
            _cache = cache;
        }

        // GET: TourController
        public async Task<ActionResult> Index()
        {
            string bestIdsRecordKey = $"DreamTour_BestIds_{DateTime.Now.ToString("dd-MM-yyyy-hh:mm")}";
            string groupsRecordKey = $"DreamTour_Groups_{DateTime.Now.ToString("dd-MM-yyyy-hh:mm")}";

            var toursIds = await _cache.GetRecordAsync<IEnumerable<Guid>>(bestIdsRecordKey);
            var groups = await _cache.GetRecordAsync<IEnumerable<GroupedTours>>(groupsRecordKey);

            IEnumerable<GroupedTours> groupedCountry;
            IEnumerable<TourModel> bestTours;

            if (toursIds is null)
            {
                bestTours = await _tourData.GetBest();
                await _cache.SetRecordsAsync(bestIdsRecordKey, bestTours.Select(t => t.Id));
            }

            else
            {
                bestTours = toursIds.Select(id => _tourData.Get(id).Result);
            }

            if (groups is null)
            {
                groupedCountry = await _tourData.GetCountryGroups();
                await _cache.SetRecordsAsync(groupsRecordKey, groupedCountry);
            }

            else
            {
                groupedCountry = groups;
            }

            MainPageViewModel mainPageView = new MainPageViewModel()
            {
                BestTours = bestTours,
                CountryGroups = groupedCountry
            };

            return View(mainPageView);
        }

        public async Task<ActionResult> Search(SearchRequest searchRequest)
        {
            string keyPart = $"" +
                $"{searchRequest.Country}-" +
                $"-{searchRequest.Region}-" +
                $"-{searchRequest.District}-" +
                $"-{searchRequest.Region}-" +
                $"-{searchRequest.City}-" +
                $"-{searchRequest.MinimalPrice}-" +
                $"-{searchRequest.MaximumPrice}-" +
                $"-{searchRequest.DaysCount}-" +
                $"-{searchRequest.BeginDate}";

            string searchIdsRecordKey = $"DreamTour_SearchIds_{keyPart}_{DateTime.Now.ToString("dd-MM-yyyy-hh:mm")}";

            var searchIds = await _cache.GetRecordAsync<IEnumerable<Guid>>(searchIdsRecordKey);

            IEnumerable<TourModel> foundedTours;

            if (searchIds is null)
            {
                foundedTours = await _tourData.GetFilteredTours(searchRequest);
                await _cache.SetRecordsAsync(searchIdsRecordKey, foundedTours.Select(t => t.Id));
            }

            else
            {
                foundedTours = searchIds.Select(id => _tourData.Get(id).Result);
            }

            return View(foundedTours);
        }
    }
}
