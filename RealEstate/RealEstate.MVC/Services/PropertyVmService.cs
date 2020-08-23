using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Metadata;
using RealEstate.MVC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public List<SelectListItem> GetPriceRanges()
        {
            List<string> priceRanges = new List<string>();
            priceRanges.AddRange(new List<string>()
            {
                "0-1,000",
                "5,000-10,000",
                "10,000-15,000",
                "15,000-20,000",
                "20,000-25,000",
                "25,000-30,000",
                "30,000-35,000",
                "35,000+",



            });
            return priceRanges.Select(p => new SelectListItem()
            {
                Value = p,
                Text = p

            }).ToList();
        }
        public PriceRange GetPriceRange(string rawPriceRange)
        {
            if (string.IsNullOrEmpty(rawPriceRange) || rawPriceRange.Contains("+"))
                return PriceRange.DefaultRange;

            var prices = rawPriceRange.Split('-');

            if (prices.Length != 2) return PriceRange.DefaultRange;

            if (!int.TryParse(prices[0].Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int minPrice)) return PriceRange.DefaultRange;
            if (!int.TryParse(prices[1].Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int maxPrice)) return PriceRange.DefaultRange;

            return new PriceRange()
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

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
