using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Models
{
    public class DashboardVM
    {
        public List<Property> Properties { get; set; }
        public Agent Agent { get; set; }
        public AgentPropertyStats propertyStats { get; set; }
    }
}
