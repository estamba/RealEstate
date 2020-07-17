using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces.Repositories
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Property GetPropertyById(Guid Id);
        List<PropertyState> GetPropertyStates();
        List<PropertyStatus> GetPropertyStatuses();
        List<PropertyType> GetPropertyTypes();
        List<Property> GetProperties(int count);
        PaginatedSearchResult<Property> Search(PropertySearchFilter searchFilter,int pageSize, int pageNumber);
        Task<List<Property>> GetPropertiesByAgentIDAsync(Guid Id, PropertySortOptions sortOptions);
        void AddTempPropertyInfo(TempPropertyInfo tempPropertyInfo);
        TempPropertyInfo GetTempPropertyInfo(Guid Id);
    }
}
