using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.Core.Interfaces.Services.Metadata;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.Core.Interfaces.Services.Regions;
using RealEstate.MVC.Extensions;
using RealEstate.MVC.Models;

namespace RealEstate.MVC.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IRegionService regionService;
        private readonly IMetadataService metadataService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAgentService agentService;
        private readonly IAddPropertyService addPropertyService;
        private readonly IPropertyService propertyService;

        public PropertyController(IRegionService regionService, IMetadataService metadataService,IMapper mapper, UserManager<ApplicationUser> userManager, IAgentService agentService, IAddPropertyService addPropertyService, IPropertyService propertyService)
        {
            this.regionService = regionService;
            this.metadataService = metadataService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.agentService = agentService;
            this.addPropertyService = addPropertyService;
            this.propertyService = propertyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var regions = await regionService.GetRegionsAsync();
            AddPropertyVm vm = new AddPropertyVm();
            
            return View(await AddMetaDataAsync(vm));
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyVm vm, IFormFileCollection files)
        {
            var form = await
                Request.ReadFormAsync();
            if (!ModelState.IsValid) return View(await AddMetaDataAsync(vm));

            var postPropertyModel = mapper.Map<PostPropertyModel>(vm);
            var userId = userManager.GetUserId(User);

            var agent = await agentService.GetAgentByApplicationUserIdAsync(userId);
            postPropertyModel.Documents = await GetDocuments(files);
            postPropertyModel.AgentId = agent.Id;
            var property = addPropertyService.Add(postPropertyModel);
            return Ok(property.Id);
        }

        [Authorize]
        public async Task<IActionResult> Details(Guid Id)
        {
            var property = propertyService.GetProperty(Id);
            var similarProperties = await propertyService.GetPropertiesByTypeAsync(property.TypeId,8);
            PropertyDetailsVm vm = new PropertyDetailsVm()
            {
                Agent = property.Ágent,
                ImageIds = property.PropertyImages.Select(i => i.ImageId).ToList(),
                Description = property.Description,
                Title = property.Title,
                City = property.City.Name,
                Price = property.Price,
                Type = property.Type.Name,
                Status = property.Status.Name,

                SimilarProperties = similarProperties
            };
            return View(vm);
        }

        private static async Task<List<Document>> GetDocuments(IFormFileCollection files)
        {
            var documents = new List<Document>();
            foreach (var file in files)
            {
                var bytes = await file.ToByteArrayAsync();
                Document document = new Document()
                {
                    Id = Guid.NewGuid(),
                    ContentType = file.ContentType,
                    Extension = Path.GetExtension(file.FileName),
                    Name = file.FileName,
                    Content = bytes
                };

                documents.Add(document);
            }
            return documents;
        }

        [HttpPost("file-upload")]
        public IActionResult Test(string test)
        {
            var form = Request.ReadFormAsync();
            AddPropertyVm vm = new AddPropertyVm();
            if (!ModelState.IsValid) return View(vm);

            return View(vm);
        }
        private async Task<AddPropertyVm> AddMetaDataAsync(AddPropertyVm vm)
        {
            var regions = await regionService.GetRegionsAsync();

           //vm.Cities = GetCities(regions);
            vm.Regions = GetRegions(regions);
            vm.PropertyTypes = GetPropertyTypes();
            vm.PropertyStatuses = GetPropertyStatuses();
            return vm;
        }
        private List<SelectListItem> GetCities(List<Region> regions)
        {
            List<City> cities = new List<City>();
            foreach (var region in regions)
            {
                cities.AddRange(region.Cities);
            }
            return cities.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }

        private List<SelectListItem> GetRegions(List<Region> regions)
        {

            return regions.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
        private List<SelectListItem> GetPropertyStatuses()
        {

            var propertyStatuses = metadataService.GetPropertyStatuses();

            return propertyStatuses.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
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
    }
}

