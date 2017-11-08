using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class AdvertisementControllerTests : TestsBase
    {
        [Fact]
        public void Can_Add_Click()
        {
            // Arrange - create an Advertisement
            Advertisement ad = new Advertisement { AdvertisementID = 1, CompanyName = "Test", Clicks = 0, };
            // Arrange - moq the GetRandomAd method
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns((new Advertisement[]
            {
                ad
            }).AsQueryable<Advertisement>());

            // Arrange - create the advertisement controller
            AdvertisementController target = new AdvertisementController(mock.Object);

            // Act - 
            var result = target.AddClick(ad.AdvertisementID, "/Admin/AdIndex");

            // Assert - verify AddClick called
            mock.Verify(m => m.AddClick(It.IsAny<int>()), Times.Once);
            Assert.IsType<RedirectResult>(result);
        }
    }
}
