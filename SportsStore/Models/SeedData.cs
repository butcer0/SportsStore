using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

            #region Depricated - Don't use in Production : can cause data loss
            //context.Database.Migrate();
            #endregion

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Category = "Soccer",
                        Price = 34.95M
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Category = "Soccer",
                        Price = 79500
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Category = "Chess",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price = 29.95M
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamon-studded King",
                        Category = "Chess",
                        Price = 1200
                    }
                    );
                context.SaveChanges();
            }
        }

        public static void EnsureAdsPopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

            #region Depricated - Don't use in Production : can cause data loss
            //context.Database.Migrate();
            #endregion

            if (!context.Advertisements.Any())
            {
                context.Advertisements.AddRange(
                    new Advertisement
                    {
                        CompanyName = "Slack",
                        Description = "What it feels like to sit in 25% fewer meetings",
                        ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-slack.png",
                        PricePerClick = 0.05m
                    },
                     new Advertisement
                     {
                         CompanyName = "Google",
                         Description = "What it feels like to sit in 25% fewer meetings",
                         ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-google.png",
                         PricePerClick = 0.05m
                     },
                      new Advertisement
                      {
                          CompanyName = "Bates Motel",
                          Description = "What it feels like to sit in 25% fewer meetings",
                          ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-bates-motel.png",
                          PricePerClick = 0.04m
                      },
                       new Advertisement
                       {
                           CompanyName = "Dollar Shave Club",
                           Description = "What it feels like to sit in 25% fewer meetings",
                           ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-dollar-shave-club.png",
                           PricePerClick = 0.15m
                       },
                        new Advertisement
                        {
                            CompanyName = "Shopify",
                            Description = "What it feels like to sit in 25% fewer meetings",
                            ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-shopify.png",
                            PricePerClick = 0.12m
                        },
                         new Advertisement
                         {
                             CompanyName = "Shopify",
                             Description = "What it feels like to sit in 25% fewer meetings",
                             ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-shopify-2.png",
                             PricePerClick = 0.02m
                         },
                          new Advertisement
                          {
                              CompanyName = "Heal",
                              Description = "What it feels like to sit in 25% fewer meetings",
                              ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-heal.png",
                              PricePerClick = 0.03m
                          },
                           new Advertisement
                           {
                               CompanyName = "Athos",
                               Description = "What it feels like to sit in 25% fewer meetings",
                               ImageURL = @"http://www.wordstream.com/images/facebook-ad-examples-athos.png",
                               PricePerClick = 0.10m
                           },
                            new Advertisement
                            {
                                CompanyName = "DigitalMarketer",
                                Description = "The Ultimate Social Media Swipe File",
                                ImageURL = @"https://s3.amazonaws.com/digitalmarketer-downloads/website/content/uploads/2014/12/DM-Facebook-Ads-Img9.jpg",
                                PricePerClick = 0.03m
                            },
                             new Advertisement
                             {
                                 CompanyName = "DigitalMarketer",
                                 Description = "Traffic & Convertsion Summit 2017",
                                 ImageURL = @"https://s3.amazonaws.com/digitalmarketer-downloads/wp-content/uploads/2017/01/best-facebook-ads-img8.jpg",
                                 PricePerClick = 0.08m
                             }
                    );
                context.SaveChanges();
            }
        }


        #region Depricated - Updated for Production
        //public static void EnsurePopulated(IApplicationBuilder app)
        //{
        //    ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
        //    context.Database.Migrate();
        //    if (!context.Products.Any())
        //    {
        //        context.Products.AddRange(
        //            new Product
        //            {
        //                Name = "Kayak",
        //                Description = "A boat for one person",
        //                Category = "Watersports",
        //                Price = 275
        //            },
        //            new Product
        //            {
        //                Name = "Lifejacket",
        //                Description = "Protective and fashionable",
        //                Category = "Watersports",
        //                Price = 48.95M
        //            },
        //            new Product
        //            {
        //                Name = "Soccer Ball",
        //                Description = "FIFA-approved size and weight",
        //                Category = "Soccer",
        //                Price = 19.50M
        //            },
        //            new Product
        //            {
        //                Name = "Corner Flags",
        //                Description = "Give your playing field a professional touch",
        //                Category = "Soccer",
        //                Price = 34.95M
        //            },
        //            new Product
        //            {
        //                Name = "Stadium",
        //                Description = "Flat-packed 35,000-seat stadium",
        //                Category = "Soccer",
        //                Price = 79500
        //            },
        //            new Product
        //            {
        //                Name = "Thinking Cap",
        //                Description = "Improve brain efficiency by 75%",
        //                Category = "Chess",
        //                Price = 16
        //            },
        //            new Product
        //            {
        //                Name = "Unsteady Chair",
        //                Description = "Secretly give your opponent a disadvantage",
        //                Category = "Chess",
        //                Price = 29.95M
        //            },
        //            new Product
        //            {
        //                Name = "Human Chess Board",
        //                Description = "A fun game for the family",
        //                Category = "Chess",
        //                Price = 75
        //            },
        //            new Product
        //            {
        //                Name = "Bling-Bling King",
        //                Description = "Gold-plated, diamon-studded King",
        //                Category = "Chess",
        //                Price = 1200
        //            }
        //            );
        //        context.SaveChanges();
        //    }
        //}
        #endregion


    }
}
