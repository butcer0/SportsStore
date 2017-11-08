using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;


        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = (category == null)? repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize)
                : repository.Products
                    .Where(p => p.Category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = (category == null)? repository.Products.Count() : repository.Products.Where(c => c.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase)).Count()
                },
                CurrentCategory = category
                #region Depricated - Introduced Categories
                //PagingInfo = new PagingInfo
                //{
                //    CurrentPage = productPage,
                //    ItemsPerPage = PageSize,
                //    TotalItems = repository.Products.Count()
                //},
                #endregion
            });


        #region Depricated - Introduced Categories
        //public ViewResult List(int productPage = 1)
        //    => View(new ProductsListViewModel
        //    {
        //        Products = repository.Products
        //            .OrderBy(p => p.ProductID)
        //            .Skip((productPage - 1) * PageSize)
        //            .Take(PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = productPage,
        //            ItemsPerPage = PageSize,
        //            TotalItems = repository.Products.Count()
        //        }
        //    });
        #endregion
        #region Depricated - Introduced ViewModel
        //public ViewResult List(int productPage = 1) 
        //    => View(repository.Products
        //        .OrderBy(p => p.ProductID)
        //        .Skip((productPage - 1) * PageSize)
        //        .Take(PageSize));
        #endregion
        #region Depricated - Added Pagination
        //public ViewResult List() => View(repository.Products);
        #endregion
    }
}
