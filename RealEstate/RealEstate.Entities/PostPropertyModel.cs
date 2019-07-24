using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
   public class PostPropertyModel
    {
        public string CityId { get; set; }
        public string RegionId { get; set; }

        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string AgentId { get; set; }
        public List<Document> Document { get; set; }



    }
}
