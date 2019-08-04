using Microsoft.AspNetCore.Mvc.Rendering;
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
  
}
