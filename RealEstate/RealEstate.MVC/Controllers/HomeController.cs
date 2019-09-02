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

namespace RealEstate.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IMetadataService metadataService;

        public HomeController(IPropertyService  propertyService, IMetadataService metadataService)
        {
            this.propertyService = propertyService;
            this.metadataService = metadataService;
        }
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel()
            {
                Properties = propertyService.GetProperties(10),
                PropertyTypes = GetPropertyTypes()
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
        private List<SelectListItem> GetPropertyTypes()
        {
            var propertyTypes = metadataService.GetPropertyTypes();

            return propertyTypes.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
