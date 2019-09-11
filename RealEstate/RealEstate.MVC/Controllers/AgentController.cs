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
using RealEstate.MVC.Services;

namespace RealEstate.MVC.Controllers
{
    [Route("agents")]
    public class AgentController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAgentService agentService;
        private readonly PropertyVmService propertyVmService;
        private readonly IAgentPropertyStatsService agentPropertyStatsService;

        public AgentController(IPropertyService propertyService, UserManager<ApplicationUser> userManager,IAgentService agentService, PropertyVmService propertyVmService, IAgentPropertyStatsService agentPropertyStatsService)
        {
            this.propertyService = propertyService;
            this.userManager = userManager;
            this.agentService = agentService;
            this.propertyVmService = propertyVmService;
            this.agentPropertyStatsService = agentPropertyStatsService;
        }
        [HttpGet("{Id}/{name}", Name ="agentProperties")]
        public async Task<IActionResult> Agent(Guid Id)
        {
            var properties = await propertyService.GetPropertiesByAgentIDAsync(Id);

            var agent = properties[0].Ágent;
            AgentPropertiesVM agentPropertiesVM = new AgentPropertiesVM()
            {
                Properties = properties,
                SortOptions = propertyVmService. GetSortingOptions(),
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
            MyPropertiesVM vm = new MyPropertiesVM() { properties = properties, SortOptions = propertyVmService.GetSortingOptions() };
            return View(vm);
        }
        [Authorize]
        [HttpGet("dashboard", Name = "dashboard")]
        public async Task<IActionResult> DashBoard()
        {
            var userId = userManager.GetUserId(User);
            var agent = await agentService.GetAgentByApplicationUserIdAsync(userId);
            var properties = await propertyService.GetPropertiesByAgentIDAsync(agent.Id);
            DashboardVM vm = new DashboardVM()
            {
                Properties = properties?.OrderByDescending(p => p.DateCreated)?.Take(5)?.ToList(),
                Agent = agent,
                propertyStats = await agentPropertyStatsService.Get(agent.Id)
            };
            return View(vm);
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
  
    }
}