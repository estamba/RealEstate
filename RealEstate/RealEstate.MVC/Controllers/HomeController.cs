using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.MVC.Models;

namespace RealEstate.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService propertyService;

        public HomeController(IPropertyService  propertyService)
        {
            this.propertyService = propertyService;
        }
        public IActionResult Index()
        {
            var viewModel =new  HomeViewModel();
            viewModel.Properties = propertyService.GetProperties(10);

            return View(viewModel);
        }
        public IActionResult Test()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
