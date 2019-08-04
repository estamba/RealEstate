using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class PostPropertyModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Area { get; set; }
        public double Price { get; set; }
        public int? SelectedType { get; set; }
        public int? SelectedStatus { get; set; }
        public int? SelectedCity { get; set; }
        public int? SelectedRegion { get; set; }
        public Guid AgentId { get; set; }
        public List<Document> Documents { get; set; }



    }
}
