using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Tests
{
    public class TestsBase
    {
        internal T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        internal T GetComponentViewModel<T>(IViewComponentResult result) where T : class
        {
            return (result as ViewViewComponentResult)?.ViewData.Model as T;
        }

    }
}
