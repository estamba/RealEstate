using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Models
{
    public class AddPropertyVm
    {
        [Required]
        public string Title { get; set; }
        [Required] public string  Description { get; set; }
        [Required] public double? Area { get; set; }
        [Required] public double? Price { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
        public IEnumerable<SelectListItem> PropertyStatuses { get; set; }

        [Required(ErrorMessage ="Property type field is required")] public int? SelectedType { get; set; }
        [Required] public int? SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }

        [Required (ErrorMessage ="City  field is required")] public int SelectedCity { get; set; }
        [Required(ErrorMessage = "Region  field is required")] public int SelectedRegion{ get; set; }

    }
    public class PropertyDetailsVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int ViewCount { get; set; }

        public List<Guid> ImageIds { get; set; }
        public  Agent Agent { get; set; }
        public List<Property>SimilarProperties { get; set; }

        public PropertySearchFormVm PropertySearchFormVm { get; set; }
    }
    public class PropertySearchFormVm
    {
        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
        public IEnumerable<SelectListItem> PropertyStatuses { get; set; }
    }
    public class PropertySearchVM
    {
        public PropertySearchVM()
        {
            SearchFilter = new PropertySearchFilter();
        }
        public IEnumerable<SelectListItem> PropertyStatuses { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
        public IEnumerable<SelectListItem> SortOptions { get; set; }
        public IEnumerable<SelectListItem> PriceRanges { get; set; }


        public PropertySearchFilter SearchFilter { get; set; }
        public PaginatedSearchResult<Property> searchResult { get; set; }
    }
    public class PriceRange
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public static PriceRange DefaultRange
        {
            get
            {
                return new PriceRange()
                {
                    MinPrice = 0,
                    MaxPrice = int.MaxValue
                };
            }
        }

    }
    public class AgentPropertiesVM
    {
        public AgentPropertiesVM()
        {
            Agent = new Agent();
        }
        public IEnumerable<SelectListItem> SortOptions { get; set; }
        public List<Property> Properties { get; set; }
        public Agent Agent { get; set; }



    }
    public class MyPropertiesVM
    {
        public List<Property> properties { get; set; }
        public IEnumerable<SelectListItem> SortOptions { get; set; }

    }

    public class EditPropertyVM
    {
            public Guid Id { get; set; }
            [Required]
            public string Title { get; set; }
            [Required] public string Description { get; set; }
            [Required] public double? Area { get; set; }
            [Required] public double? Price { get; set; }

            public IEnumerable<SelectListItem> PropertyTypes { get; set; }
            public IEnumerable<SelectListItem> PropertyStatuses { get; set; }

            [Required(ErrorMessage = "Property type field is required")] public int? SelectedType { get; set; }
            [Required] public int? SelectedStatus { get; set; }
            public IEnumerable<SelectListItem> Cities { get; set; }
            public IEnumerable<SelectListItem> Regions { get; set; }

            [Required(ErrorMessage = "City  field is required")] public int? SelectedCity { get; set; }
            [Required(ErrorMessage = "Region  field is required")] public int SelectedRegion { get; set; }

        
    }

}
