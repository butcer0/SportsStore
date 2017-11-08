using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class AdvertisementViewComponent : ViewComponent
    {
        private IAdvertisementRepository repository;

        public AdvertisementViewComponent(IAdvertisementRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke() => View(repository.GetRandomAd());

    }
}
