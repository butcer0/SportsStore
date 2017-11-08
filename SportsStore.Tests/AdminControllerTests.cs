using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
    public class AdminControllerTests : TestsBase
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
            }).AsQueryable<Product>());

            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object, null);

            // Action
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();
                
            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
            }.AsQueryable<Product>());

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object, null);

            // Act
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            //Assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }


        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
            }.AsQueryable<Product>());

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object, null);

            // Act
            Product result = GetViewModel<Product>(target.Edit(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create mock temp data
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object, null)
            {
                TempData = tempData.Object
            };

            // Arrange - create a product
            Product product = new Product { Name = "Test" };

            // Act - try to save the product
            IActionResult result = target.Edit(product);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveProduct(product));
            // Assert - check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object, null);
            // Arrange - create the product
            Product product = new Product { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            IActionResult result = target.Edit(product);

            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // Assert - check the method result type
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            // Arrange - create a Product
            Product prod = new Product { ProductID = 2, Name = "Test" };

            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "P1" },
                new Product{ProductID = 3, Name = "P3" }
            }.AsQueryable<Product>());

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object, null);

            // Act - delete the product
            target.Delete(prod.ProductID);

            // Assert - ensure that the repository delete method was 
            // called with the correct Product
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }


        #region Advertisement
        [Fact]
        public void Ad_Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns((new Advertisement[]
            {
                new Advertisement {AdvertisementID = 1, CompanyName = "A1" },
                new Advertisement {AdvertisementID = 2, CompanyName = "A2" },
                new Advertisement {AdvertisementID = 3, CompanyName = "A3" }
            }).AsQueryable<Advertisement>());

            // Arrange - create a controller
            AdminController target = new AdminController(null, mock.Object);

            // Action
            Advertisement[] result = GetViewModel<IEnumerable<Advertisement>>(target.AdIndex())?.ToArray();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("A1", result[0].CompanyName);
            Assert.Equal("A2", result[1].CompanyName);
            Assert.Equal("A3", result[2].CompanyName);
        }

        [Fact]
        public void Ad_Can_Edit_Product()
        {
            // Arrange - create the mock repository
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new Advertisement[]
            {
                new Advertisement {AdvertisementID = 1, CompanyName = "A1" },
                new Advertisement {AdvertisementID = 2, CompanyName = "A2" },
                new Advertisement {AdvertisementID = 3, CompanyName = "A3" }
            }.AsQueryable<Advertisement>());

            // Arrange - create the controller
            AdminController target = new AdminController(null, mock.Object);

            // Act
            Advertisement p1 = GetViewModel<Advertisement>(target.AdEdit(1));
            Advertisement p2 = GetViewModel<Advertisement>(target.AdEdit(2));
            Advertisement p3 = GetViewModel<Advertisement>(target.AdEdit(3));

            //Assert
            Assert.Equal(1, p1.AdvertisementID);
            Assert.Equal(2, p2.AdvertisementID);
            Assert.Equal(3, p3.AdvertisementID);
        }


        [Fact]
        public void Ad_Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new Advertisement[]
            {
                new Advertisement {AdvertisementID = 1, CompanyName = "A1" },
                 new Advertisement {AdvertisementID = 2, CompanyName = "A2" },
                 new Advertisement {AdvertisementID = 3, CompanyName = "A3" }
            }.AsQueryable<Advertisement>());

            // Arrange - create the controller
            AdminController target = new AdminController(null, mock.Object);

            // Act
            Advertisement result = GetViewModel<Advertisement>(target.AdEdit(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Ad_Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            // Arrange - create mock temp data
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            // Arrange - create the controller
            AdminController target = new AdminController(null, mock.Object)
            {
                TempData = tempData.Object
            };

            // Arrange - create a advertisement
            Advertisement advertisement = new Advertisement { CompanyName = "Test" };

            // Act - try to save the product
            IActionResult result = target.AdEdit(advertisement);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveAdvertisement(advertisement));
            // Assert - check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AdIndex", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Ad_Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(null, mock.Object);
            // Arrange - create a advertisement
            Advertisement advertisement = new Advertisement { CompanyName = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the advertisement
            IActionResult result = target.AdEdit(advertisement);

            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveAdvertisement(It.IsAny<Advertisement>()), Times.Never());
            // Assert - check the method result type
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Ad_Can_Delete_Valid_Products()
        {
            // Arrange - create a advertisement
            Advertisement advertisement = new Advertisement { CompanyName = "Test" };

            // Arrange - create the mock repository
            Mock<IAdvertisementRepository> mock = new Mock<IAdvertisementRepository>();
            mock.Setup(m => m.Advertisements).Returns(new Advertisement[]
            {
                new Advertisement {AdvertisementID = 1, CompanyName = "A1" },
                new Advertisement {AdvertisementID = 2, CompanyName = "A2" }
            }.AsQueryable<Advertisement>());

            // Arrange - create the controller
            AdminController target = new AdminController(null, mock.Object);

            // Act - delete the product
            target.AdDelete(advertisement.AdvertisementID);

            // Assert - ensure that the repository delete method was 
            // called with the correct Product
            mock.Verify(m => m.DeleteAdvertisement(advertisement.AdvertisementID));
        }

        #endregion

    }
}
