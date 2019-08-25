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
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }


        public List<Guid> ImageIds { get; set; }
        public  Agent Agent { get; set; }
        public List<Property>SimilarProperties { get; set; }
    }
  
}
