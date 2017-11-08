using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        private IAdvertisementRepository ad_repository;

        public AdminController(IProductRepository repo, IAdvertisementRepository ad_repo)
        {
            repository = repo;
            ad_repository = ad_repo;
        }

        #region =========Products=========
        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            } else
            {
                // there is something wrong with the data values
                return View(product);
            }

        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if(TempData != null)
            {
                TempData["message"] = (deletedProduct != null) ? $"{deletedProduct.Name} was deleted" : "Failed to delete item";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Advertisements
        public ViewResult AdIndex() => View(ad_repository.Advertisements);

        public ViewResult AdEdit(int adId) => View(ad_repository.Advertisements.FirstOrDefault(a => a.AdvertisementID == adId));

        [HttpPost]
        public IActionResult AdEdit(Advertisement ad)
        {
            if(ModelState.IsValid)
            {
                ad_repository.SaveAdvertisement(ad);
                TempData["message"] = $"{ad.CompanyName}'s ad has been saved";
                return RedirectToAction("AdIndex");
            }
            else
            {
                return View(ad);
            }
        }

        public ViewResult AdCreate() => View("AdEdit", new Advertisement());

        [HttpPost]
        public IActionResult AdDelete(int adID)
        {
            Advertisement dbEntry = ad_repository.DeleteAdvertisement(adID);
            if(TempData != null)
            {
                TempData["message"] = (dbEntry != null) ? $"{dbEntry.Description} was deleted" : "Failed to delete advertisement";
            }
            return RedirectToAction("AdIndex");
        }

        public IActionResult AdClicksReset(int adID)
        {
            bool resetSuccess = ad_repository.ResetClicks(adID);
            if (TempData != null)
            {
                TempData["message"] = (resetSuccess) ? "Advertisement click count reset" : "Failed to reset advertisement click count";
            }
            return RedirectToAction("AdIndex");
        }

        #endregion

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            SeedData.EnsureAdsPopulated(HttpContext.RequestServices);
            return Redirect(nameof(Index));
        }
    }
}
