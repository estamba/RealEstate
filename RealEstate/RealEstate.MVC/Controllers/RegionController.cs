using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Interfaces.Services.Regions;

namespace RealEstate.MVC.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService regionService;

        public RegionController(IRegionService regionService)
        {
            this.regionService = regionService;
        }
        public IActionResult GetCities(int? regionId)
        {
            if (regionId is null) return BadRequest();
            var cities = regionService.GetCities(regionId.Value);
            return Json(cities);
        }
    }
}