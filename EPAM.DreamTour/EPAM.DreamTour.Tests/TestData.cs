using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace EPAM.DreamTour.Tests
{
    public static class TestData
    {
        public static async Task<IEnumerable<string>> GetCanadaTestRegions()
        {
            List<string> regions = new List<string>();
            
            regions.Add("Ontario");
            regions.Add("British Columbia");
            regions.Add("Manitoba");
            regions.Add("Quebec");

            return regions;
        }

        public static async Task<IEnumerable<string>> GetBritishColumbiaCities()
        {
            List<string> cities = new List<string>();

            cities.Add("Burnaby");
            cities.Add("Surrey");

            return cities;
        }

        public static async Task<IEnumerable<string>> GetEmptyCollection()
        {
            return new List<string>();
        }
    }
}
