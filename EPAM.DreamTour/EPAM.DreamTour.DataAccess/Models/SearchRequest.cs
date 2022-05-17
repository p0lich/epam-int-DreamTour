using System;

namespace EPAM.DreamTour.DataAccess.Models
{
    public class SearchRequest
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int? MinimalPrice { get; set; }
        public int? MaximumPrice { get; set; }
        public short? DaysCount { get; set; }
        public DateTime? BeginDate { get; set; }
    }
}
