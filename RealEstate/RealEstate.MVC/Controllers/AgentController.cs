using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.MVC.Models;

namespace RealEstate.MVC.Controllers
{
    [Route("agents")]
    public class AgentController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAgentService agentService;

        public AgentController(IPropertyService propertyService, UserManager<ApplicationUser> userManager,IAgentService agentService)
        {
            this.propertyService = propertyService;
            this.userManager = userManager;
            this.agentService = agentService;
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
        [Authorize]
        [HttpGet("my-properties", Name = "my-properties")]
        public async Task<IActionResult> MyProperties()
        {
            var userId = userManager.GetUserId(User);
            var agent = await agentService.GetAgentByApplicationUserIdAsync(userId);
            var properties = await propertyService.GetPropertiesByAgentIDAsync(agent.Id);
            MyPropertiesVM vm = new MyPropertiesVM() { properties = properties, SortOptions = GetSortingOptions() };
            return View(vm);
        }
        [Authorize]
        [HttpGet("dashboard", Name = "dashboard")]
        public IActionResult DashBoard()
        {

            return View();
        }
        [HttpGet("my-propertiesPartial")]
        public async Task<IActionResult> MyPropertiesPartial(Guid Id, PropertySortOptions sortOption)
        {
            var properties = await propertyService.GetPropertiesByAgentIDAsync(Id, sortOption);

            return PartialView("_MyPropertiesPartial", properties);

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