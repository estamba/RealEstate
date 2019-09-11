using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
   public class AgentPropertyStats
    {
        public Guid AgentId { get; set; }
        public int PropertyCount { get; set; }
        public int ViewCount { get; set; }
        public int ActivePropertyCount { get; set; }
        public int SoldPropertyCount { get; set; }


    }
}
