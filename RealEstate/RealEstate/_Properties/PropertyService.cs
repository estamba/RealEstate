using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services.Properties
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public List<Property> GetProperties(int count = 0)
        {
            return propertyRepository.GetProperties(count);
        }

        public async Task<List<Property>> GetPropertiesByAgentIDAsync(Guid agentID, PropertySortOptions propertySortOptions = PropertySortOptions.Default)
        {
            return await propertyRepository.GetPropertiesByAgentIDAsync(agentID, propertySortOptions);

        }

        public async Task<List<Property>> GetPropertiesByTypeAsync(short typeId,int count =0)
        {
            if(count ==0)
            return (await propertyRepository.FindAsync(t => t.TypeId == typeId, "Type,Status,PropertyImages,City")).ToList();
            return (await propertyRepository.FindAsync(t => t.TypeId == typeId, "Type,Status,PropertyImages,City",count)).ToList();

        }

        public Property GetProperty(Guid Id)
        {
            return propertyRepository.GetPropertyById(Id);
        }
    }
}
