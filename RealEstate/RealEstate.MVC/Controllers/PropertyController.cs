using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.MVC.Models;

namespace RealEstate.MVC.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Add()
        {
            AddPropertyVm vm = new AddPropertyVm()
            {
                Cities = GetCities(),
                Regions = GetRegions(),
                PropertyTypes = GetPropertyTypes(),
                PropertyStates = GetPropertyTypes()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(PostPropertyVm vm)
        {
            return View(vm);
        }
        private List<SelectListItem> GetCities()
        {

            var cities = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Banjul", Value = "1"},
                new SelectListItem(){ Text = "Basse", Value = "2"},
                new SelectListItem(){ Text = "Brikama", Value = "3"}


            };
            return cities;
        }
        private List<SelectListItem> GetRegions()
        {

            var regions = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "West Coast Region", Value = "1"},
                new SelectListItem(){ Text = "KMC", Value = "2"},
                new SelectListItem(){ Text = "URR", Value = "3"}


            };
            return regions;
        }
        private List<SelectListItem> GetPropertyStatuses()
        {

            var propertyStatuses = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "For Sale", Value = "1"},
                new SelectListItem(){ Text = "For Rent", Value = "2"},


            };
            return propertyStatuses;
        }
        private List<SelectListItem> GetPropertyTypes()
        {

            var propertyTypes = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Land", Value = "1"},
                new SelectListItem(){ Text = "Apartment", Value = "2"},
                new SelectListItem(){ Text = "House", Value = "3"},



            };
            return propertyTypes;
        }
    }
}