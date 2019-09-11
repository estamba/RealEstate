using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
   public class EditPropertyModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public double Area { get; set; }
        public double Price { get; set; }
        public int? SelectedType { get; set; }
        public int? SelectedStatus { get; set; }
        public int? SelectedCity { get; set; }
    }
}
