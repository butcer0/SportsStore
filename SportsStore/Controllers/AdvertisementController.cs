using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class AdvertisementController : Controller
    {
        private IAdvertisementRepository repository;
        
        public AdvertisementController(IAdvertisementRepository repo)
        {
            repository = repo;
        }

        public IActionResult AddClick(int adID, string returnUrl)
        {
            repository.AddClick(adID);
            return Redirect( returnUrl );
        }

    }
}
