using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Properties
{
   public interface IPropertySearchService
    {
        PaginatedSearchResult<Property> Search(PropertySearchFilter searchFilter, int pageSize = 10, int pageNumber = 0);

    }
}
