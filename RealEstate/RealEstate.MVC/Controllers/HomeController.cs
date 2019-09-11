using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Interfaces.Services.Metadata;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.MVC.Models;
using RealEstate.MVC.Services;

namespace RealEstate.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IMetadataService metadataService;
        private readonly PropertyVmService propertyVmService;

        public HomeController(IPropertyService  propertyService, IMetadataService metadataService, PropertyVmService propertyVmService)
        {
            this.propertyService = propertyService;
            this.metadataService = metadataService;
            this.propertyVmService = propertyVmService;
        }
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel()
            {
                Properties = propertyService.GetProperties(10),
                PropertyTypes = propertyVmService.GetPropertyTypes()
            };

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
