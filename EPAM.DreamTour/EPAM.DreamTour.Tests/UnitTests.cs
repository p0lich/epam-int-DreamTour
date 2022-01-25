using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using EPAM.DreamTour.DataAccess.Data;
using EPAM.DreamTour.DataAccess.DbAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;

namespace EPAM.DreamTour.Tests
{
    [TestClass]
    public class UnitTests
    {
        #region GetCountryRegion_TESTS

        [TestMethod]
        public async Task GetCountryRegions_TrueReturn_ReturnsSameRegionsCountForCanada()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string country = "Canada";

                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<string, dynamic>("dbo.spTour_GetCountryRegions",
                                                            // new { country },
                                                            It.IsAny<object>(),
                                                            "Default"))
                    .Returns(TestData.GetCanadaTestRegions());

                var tourData = mock.Create<TourData>();

                var expected = await TestData.GetCanadaTestRegions();
                var actual = await tourData.GetCountryRegions("Canada");

                Assert.AreEqual(expected.Count(), actual.Count());
            }

            //var mockSqlAccess = new Mock<ISqlDataAccess>();

            //mockSqlAccess
            //    .Setup(x => x.LoadData<string, dynamic>("dbo.spTour_GetCountryRegions",
            //                                            new { Country = "Canada" },
            //                                            "Default"))
            //    .Returns(TestData.GetCanadaTestRegions());

            //var dataAccess = new TourData(mockSqlAccess.Object);

            //var expected = TestData.GetCanadaTestRegions();
            //var actual = dataAccess.GetCountryRegions("Canada");

            //Assert.AreEqual(expected.Result.Count(), actual.Result.Count());
        }

        [TestMethod]
        public async Task GetCountryRegions_TrueReturn_ReturnsZeroRegionCountForEmptyCollection()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<string, dynamic>("dbo.spTour_GetCountryRegions",
                                           It.IsAny<object>(),
                                           "Default"))
                    .Returns(TestData.GetEmptyCollection());

                var tourData = mock.Create<TourData>();

                var expected = 0;
                var actual = await tourData.GetCountryRegions("Canadaaa");

                Assert.AreEqual(expected, actual.Count());
            }
        }

        #endregion

        #region GetRegionCities_TESTS

        [TestMethod]
        public async Task GetRegionCities_TrueReturn_ReturnsSameCityCountFor()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string region = "British Columbia";

                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<string, dynamic>("dbo.spTour_GetRegionCities",
                                                            //new { region },
                                                            It.IsAny<object>(),
                                                            "Default"))
                    .Returns(TestData.GetBritishColumbiaCities());

                var tourData = mock.Create<TourData>();

                var expected = await TestData.GetBritishColumbiaCities();
                var actual = await tourData.GetRegionCities("British Columbia");

                Assert.AreEqual(expected.Count(), actual.Count());
            }
        }

        [TestMethod]
        public async Task GetRegionCities_TrueReturn_ReturnsZeroCityCountForEmptyCollection()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<string, dynamic>("dbo.spTour_GetRegionCities",
                                                            //new { region },
                                                            It.IsAny<object>(),
                                                            "Default"))
                    .Returns(TestData.GetEmptyCollection());

                var tourData = mock.Create<TourData>();

                var expected = 0;
                var actual = await tourData.GetRegionCities("British Columbiaaaa");

                Assert.AreEqual(expected, actual.Count());
            }
        }

        #endregion
    }
}
