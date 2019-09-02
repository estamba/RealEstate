using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.MVC.Models;

namespace RealEstate.MVC.Controllers
{
    [Route("agents")]
    public class AgentController : Controller
    {
        private readonly IPropertyService propertyService;

        public AgentController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }
        [HttpGet("{Id}/{name}", Name ="agentProperties")]
        public async Task<IActionResult> Agent(Guid Id)
        {
            var properties = await propertyService.GetPropertiesByAgentIDAsync(Id);

            var agent = properties[0].Ágent;
            AgentPropertiesVM agentPropertiesVM = new AgentPropertiesVM()
            {
                Properties = properties,
                SortOptions = GetSortingOptions(),
                Agent = agent
            };

            return View(agentPropertiesVM);
        }
        public async Task<IActionResult> GetPropertiesListPartial(Guid Id, PropertySortOptions sortOption)
        {
            var properties = await propertyService.GetPropertiesByAgentIDAsync(Id, sortOption);

            return PartialView("_SearchResultsListPartial", properties);
        }
        private List<SelectListItem> GetSortingOptions()
        {
            var enumValues = Enum.GetValues(typeof(PropertySortOptions));
            var selectList = new List<SelectListItem>();
            foreach (var enumValue in enumValues)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = enumValue.ToString(),
                    Text = GetSortingDescription((PropertySortOptions)enumValue)
                });
            }
            return selectList;
        }
        private static string GetSortingDescription(PropertySortOptions sortOption)
        {
            string description = "Default";
            switch (sortOption)
            {
                case PropertySortOptions.PriceAsc:
                    description = "Price Low to High";
                    break;
                case PropertySortOptions.PriceDesc:
                    description = "Price High to Low";
                    break;
                case PropertySortOptions.AddedDateAsc:
                    description = "Oldest Properties";
                    break;
                case PropertySortOptions.AddedDateDesc:
                    description = "Newest Properties";
                    break;
                default:
                    description = "Default Order";
                    break;
            }
            return description;
        }
    }
}