using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces.Services.Properties
{
    public interface IPropertyService
    {
        Property GetProperty(Guid Id);
        Task<List<Property>> GetPropertiesByTypeAsync(short typeId, int count =0);
        List<Property> GetProperties(int count =0);
        Task<List<Property>> GetPropertiesByAgentIDAsync(Guid agentID, PropertySortOptions propertySortOptions = PropertySortOptions.Default);


    }
}
