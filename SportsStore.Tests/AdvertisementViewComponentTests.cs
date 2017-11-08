using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class AdvertisementViewComponentTests : TestsBase
    {
        [Fact]
        public void Can_Display_Advertisement()
        {
            // Arrange - moq the GetRandomAd method
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.GetRandomAd()).Returns(new Advertisement { AdvertisementID = 1, CompanyName = "Test", Clicks = 0, });

            // Arrange - create component
            AdvertisementViewComponent target = new AdvertisementViewComponent(mock.Object);

            // Act - invoke component
            Advertisement resultViewComp = GetComponentViewModel<Advertisement>(target.Invoke());

            #region Depricated - Introduced Helper Method
            //IViewComponentResult result = target.Invoke();

            //Advertisement resultViewComp = (result as ViewViewComponentResult)?.ViewData.Model as Advertisement;
            #endregion

            mock.Verify(m => m.GetRandomAd(), Times.Once);
            Assert.IsType<ViewViewComponentResult>(target.Invoke());
            Assert.True(resultViewComp.CompanyName.Equals("Test", StringComparison.InvariantCultureIgnoreCase));
            Assert.True(resultViewComp.Clicks == 0);
        }
    }
}
