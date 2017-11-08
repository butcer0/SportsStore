using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Advertisement
    {
        public int AdvertisementID { get; set; }
        [Display(Name ="Company Name")]
        [Required(ErrorMessage = "Please enter a company name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Please enter an image url")]
        public string ImageURL { get; set; }
        [Display(Name = "Price Per Click")]
        [Required(ErrorMessage = "Please enter a price")]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "Please enter a positive price")]
        public decimal PricePerClick { get; set; }
        public int Clicks { get; set; } = 0;

        public decimal ComputeTotalValue() => PricePerClick * Clicks;
    }
}
