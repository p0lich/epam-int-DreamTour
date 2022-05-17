using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EPAM.DreamTour.DataAccess
{
    public partial class Tour
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int MinimalPrice { get; set; }
        public int MaximumPrice { get; set; }
        public short DaysCount { get; set; }
        public DateTime BeginDate { get; set; }
    }
}
