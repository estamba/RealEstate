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
        public string Title { get; set; }
        public string  Description { get; set; }
        public double Area { get; set; }
        public double Price { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
        public IEnumerable<SelectListItem> PropertyStates { get; set; }

        public int SelectedType { get; set; }
        public int SelectedState { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }

        public int SelectedCity { get; set; }
        public int SelectedRegion{ get; set; }





    }
    public class PostPropertyVm
    {
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public double Area { get; set; }
        [Required] public double Price { get; set; }
        [Required] public int? SelectedType { get; set; }
        [Required] public int? SelectedState { get; set; }
        [Required] public int? SelectedCity { get; set; }
        [Required] public int? SelectedRegion { get; set; }
    }

}
