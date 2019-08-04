using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Metadata
{
    
    public class MetadataService : IMetadataService
    {
        IPropertyRepository propertyRepository;
        public MetadataService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }
        public List<PropertyState> GetPropertyStates()
        {
            return propertyRepository.GetPropertyStates();
        }

        public List<PropertyStatus> GetPropertyStatuses()
        {
            return propertyRepository.GetPropertyStatuses();
        }

        public List<PropertyType> GetPropertyTypes()
        {
            return propertyRepository.GetPropertyTypes();
        }
    }
}
