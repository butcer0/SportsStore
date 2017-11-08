using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IAdvertisementRepository
    {
        IQueryable<Advertisement> Advertisements { get; }
        void SaveAdvertisement(Advertisement ad);
        Advertisement DeleteAdvertisement(int adID);
        bool ResetClicks(int adID);
        bool AddClick(int adID);
        Advertisement GetRandomAd();

    }
}
