using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Interfaces.Services.Regions;
using System.Collections.Generic;
using System.Linq;

namespace RealEstate.MVC.Controllers
{
    public class RegionController : Controller
    {
        private readonly ILocationService regionService;

        public RegionController(ILocationService regionService)
        {
            this.regionService = regionService;
        }
        public IActionResult GetCities(int? regionId)
        {
            if (regionId is null) return BadRequest();
            var cities = regionService.GetCities(regionId.Value);
            return Json(cities);
        }
     
        [HttpGet]
        public IActionResult GetLocations(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm)) searchTerm = searchTerm.ToLower();

            var regions = regionService.GetRegions();
            var locations = new List<string>();
            foreach (var region in regions)
            {
                locations.Add(region.Name);
                locations.AddRange(region.Cities.Select(c => c.Name));
            }
            var results = locations.Where(l => l.ToLower().Contains(searchTerm)).Distinct();
            return Ok(results);
        }
    }
}