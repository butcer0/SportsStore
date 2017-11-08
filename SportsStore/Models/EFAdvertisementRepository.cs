using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFAdvertisementRepository : IAdvertisementRepository
    {
        private ApplicationDbContext context;

        public EFAdvertisementRepository(ApplicationDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Advertisement> Advertisements => this.context.Advertisements;

        public void SaveAdvertisement(Advertisement ad)
        {
            if(ad.AdvertisementID == 0)
            {
                context.Advertisements.Add(ad);
            }
            else
            {
                Advertisement dbEntry = context.Advertisements.FirstOrDefault(a => a.AdvertisementID == ad.AdvertisementID);
                if(dbEntry != null)
                {
                    dbEntry.CompanyName = ad.CompanyName;
                    dbEntry.Description = ad.Description;
                    dbEntry.ImageURL = ad.ImageURL;
                    dbEntry.PricePerClick = ad.PricePerClick;
                    dbEntry.Clicks = ad.Clicks;
                }
            }

            context.SaveChanges();
        }

        public Advertisement DeleteAdvertisement(int adID)
        {
            Advertisement dbEntry = context.Advertisements.FirstOrDefault(a => a.AdvertisementID == adID);
            if (dbEntry != null)
            {
                context.Advertisements.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool ResetClicks(int adID)
        {
            bool resetSuccess = false;
            Advertisement dbEntry = context.Advertisements.FirstOrDefault(a => a.AdvertisementID == adID);
            if(dbEntry != null)
            {
                dbEntry.Clicks = 0;
                context.SaveChanges();
                resetSuccess = true;
            }
            return resetSuccess;
        }

        public bool AddClick(int adID)
        {
            bool addSuccess = false;
            Advertisement dbEntry = context.Advertisements.FirstOrDefault(a => a.AdvertisementID == adID);
            if(dbEntry != null)
            {
                dbEntry.Clicks += 1;
                context.SaveChanges();
                addSuccess = true;
            }
            return addSuccess;
        }

        public Advertisement GetRandomAd()
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, (context.Advertisements.Count()));

            return context.Advertisements.Skip(toSkip).Take(1).FirstOrDefault();
        }

        
    }
}
