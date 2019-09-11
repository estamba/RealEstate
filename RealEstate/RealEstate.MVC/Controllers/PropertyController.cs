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
using RealEstate.MVC.Services;

namespace RealEstate.MVC.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ILocationService regionService;
        private readonly IMetadataService metadataService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAgentService agentService;
        private readonly IAddPropertyService addPropertyService;
        private readonly IPropertyService propertyService;
        private readonly CurrentUser currentUser;
        private readonly IPropertyViewCountService viewCountService;
        private readonly IPropertySearchService propertySearchService;
        private readonly PropertyVmService propertyVmService;

        public PropertyController(ILocationService regionService, IMetadataService metadataService,IMapper mapper, UserManager<ApplicationUser> userManager, IAgentService agentService, IAddPropertyService addPropertyService, 
            IPropertyService propertyService, CurrentUser currentUser, IPropertyViewCountService viewCountService, IPropertySearchService propertySearchService, PropertyVmService propertyVmService)
        {
            this.regionService = regionService;
            this.metadataService = metadataService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.agentService = agentService;
            this.addPropertyService = addPropertyService;
            this.propertyService = propertyService;
            this.currentUser = currentUser;
            this.viewCountService = viewCountService;
            this.propertySearchService = propertySearchService;
            this.propertyVmService = propertyVmService;
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
            PropertySearchFormVm propertySearchFormVm = new PropertySearchFormVm()
            {
                PropertyStatuses = propertyVmService.GetPropertyStatuses(),
                PropertyTypes = propertyVmService.GetPropertyTypes()
            };
            PropertyDetailsVm vm = new PropertyDetailsVm()
            {
                Id = property.Id,
                Agent = property.Ágent,
                ImageIds = property.PropertyImages.Select(i => i.ImageId).ToList(),
                Description = property.Description,
                Title = property.Title,
                City = property.City.Name,
                Price = property.Price,
                Type = property.Type.Name,
                Status = property.Status.Name,
                ViewCount = property.ViewCount,

                SimilarProperties = similarProperties,
                PropertySearchFormVm = propertySearchFormVm
            };
            return View(vm);
        }


        [HttpPost]
        public IActionResult UpdateViewCount(Guid proprtyId)
        {
            if (string.IsNullOrEmpty(currentUser.VisitorId)) currentUser.VisitorId = Guid.NewGuid().ToString ();

            viewCountService.Update(proprtyId, currentUser.VisitorId);
            return Ok();
        }

        [HttpGet]
        public IActionResult Search(PropertySearchFilter searchFilter, int page = 1)
        {

            var data = propertySearchService.Search(searchFilter ?? new PropertySearchFilter(), pageNumber: page);
            var vm = new PropertySearchVM()
            {
                searchResult = data,
                SearchFilter = searchFilter,
                PropertyStatuses = propertyVmService.GetPropertyStatuses(),
                PropertyTypes = propertyVmService.GetPropertyTypes(),
                SortOptions = propertyVmService.GetSortingOptions()
            };
            return View(vm);
        }

        public IActionResult UpdateState(Guid Id, string state)
        {
            addPropertyService.UpdatePropertyState(Id, state);
            return Ok();
        }
        public IActionResult Delete(Guid Id)
        {
            addPropertyService.Delete(Id);
            return Ok();
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var property = propertyService.GetProperty(Id);
            var regions = await regionService.GetRegionsAsync();
            EditPropertyVM vm = new EditPropertyVM()
            {
                Area = property.Area,
                Description = property.Description,
                PropertyStatuses = propertyVmService.GetPropertyStatuses(),
                PropertyTypes = propertyVmService.GetPropertyTypes(),
                Price = (double)property.Price,
                Title = property.Title,
                SelectedCity = property.CityId,
                SelectedStatus = property.StatusId,
                SelectedType = property.TypeId,
                SelectedRegion = property.City.RegionId,
                Regions = GetRegions(regions),
                Cities = GetCitySelectList(property.City)
            };
           return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult>Edit(EditPropertyVM model)
        {

            if (!ModelState.IsValid)
            {
                var property = propertyService.GetProperty(model.Id);

                var regions = await regionService.GetRegionsAsync();
                model.PropertyStatuses = propertyVmService.GetPropertyStatuses();
                model.PropertyTypes = propertyVmService.GetPropertyTypes();
                model.Regions = GetRegions(regions);
                model.Cities = GetCitySelectList(property.City);
                return View(model);
            }
            var editModel = mapper.Map<EditPropertyModel>(model);

            addPropertyService.Update(editModel);
            TempData["edit-success"] = true;
            return RedirectToRoute("my-properties", new { process = "editSuccess" });
        
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
            vm.PropertyTypes = propertyVmService. GetPropertyTypes();
            vm.PropertyStatuses = propertyVmService.GetPropertyStatuses();
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
        private List<SelectListItem> GetCitySelectList(City city)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = city.Name, Value = city.Id.ToString() });

            return selectListItems;
        }

        private List<SelectListItem> GetRegions(List<Region> regions)
        {

            return regions.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
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

    }
}

