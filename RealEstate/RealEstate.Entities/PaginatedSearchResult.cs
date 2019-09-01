using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class PaginatedSearchResult<T>
    {
        public List<T> Results { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }

        public int Total { get; set; }

    }
}
