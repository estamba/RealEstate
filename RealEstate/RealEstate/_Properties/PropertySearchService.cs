using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Properties
{
    public class PropertySearchService : IPropertySearchService
    {
        private readonly IPropertyRepository propertyRepository;

        public PropertySearchService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }
        public PaginatedSearchResult<Property> Search(PropertySearchFilter searchFilter, int pageSize = 3, int pageNumber = 0)
        {
            return propertyRepository.Search(searchFilter,pageSize,pageNumber);
        }
    }
}
