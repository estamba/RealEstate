using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
   public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; }

    }
}
