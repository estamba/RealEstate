using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Metadata;
using RealEstate.Core.Interfaces.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Metadata
{
    
    public class MetadataService : IMetadataService
    {

        static string statesCacheKey;
        static string typesCacheKey;
        static string statusCacheKey;
        private readonly IPropertyRepository propertyRepository;
        private readonly ICacheManager cacheManager;

        static MetadataService()
        {
            statesCacheKey = Guid.NewGuid().ToString();
            typesCacheKey = Guid.NewGuid().ToString();
            statusCacheKey = Guid.NewGuid().ToString();


        }
        public MetadataService(IPropertyRepository propertyRepository, ICacheManager cacheManager)
        {
            this.propertyRepository = propertyRepository;
            this.cacheManager = cacheManager;
        }
        public List<PropertyState> GetPropertyStates()
        {
            var propertyStates = cacheManager.Get<List<PropertyState>>(statesCacheKey);
            if (propertyStates != null) return propertyStates;

            propertyStates = propertyRepository.GetPropertyStates();
            cacheManager.Store(statesCacheKey, propertyStates, DateTime.Now.AddHours(3));
            return propertyStates;
        }

        public List<PropertyStatus> GetPropertyStatuses()
        {
            var propertyStatuses= cacheManager.Get<List<PropertyStatus>>(statusCacheKey);
            if (propertyStatuses != null) return propertyStatuses;

            propertyStatuses = propertyRepository.GetPropertyStatuses();
            cacheManager.Store(statusCacheKey, propertyStatuses, DateTime.Now.AddHours(3));
            return propertyStatuses;
        }

        public List<PropertyType> GetPropertyTypes()
        {
            var propertyTypes = cacheManager.Get<List<PropertyType>>(typesCacheKey);
            if (propertyTypes != null) return propertyTypes;

            propertyTypes = propertyRepository.GetPropertyTypes();
            cacheManager.Store(typesCacheKey, propertyTypes, DateTime.Now.AddHours(3));
            return propertyTypes;
        }
    }
}
