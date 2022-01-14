using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.DreamTour.DataAccess.Models
{
    public class TourModel
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public int MinimalPrice { get; set; }

        public int MaximumPrice { get; set; }

        public int DaysCount { get; set; }

        public DateTime BeginDate { get; set; }
    }
}
