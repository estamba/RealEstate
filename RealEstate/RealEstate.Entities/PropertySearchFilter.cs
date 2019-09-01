using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class PropertySearchFilter
    {
        public int? TypeId { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? Maxprice { get; set; }
        public int? StatusId  { get; set; }
        public PropertySortOptions SortOption { get; set; }





    }
}
