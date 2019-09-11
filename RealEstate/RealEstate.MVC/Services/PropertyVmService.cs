using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstate.MVC.Services
{
    public class PropertyVmService
    {
        private readonly IMetadataService metadataService;

        public PropertyVmService(IMetadataService metadataService)
        {
            this.metadataService = metadataService;
        }

        public List<SelectListItem> GetPropertyTypes()
        {
            var propertyTypes = metadataService.GetPropertyTypes();

            return propertyTypes.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
        public List<SelectListItem> GetPropertyStatuses()
        {

            var propertyStatuses = metadataService.GetPropertyStatuses();

            return propertyStatuses.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
        public List<SelectListItem> GetSortingOptions()
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
