using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.Core.Interfaces.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Properties
{
   public  class PropertyViewCountService : IPropertyViewCountService
    {
        private readonly ICacheManager cacheManager;
        private readonly IPropertyRepository propertyRepository;

        public PropertyViewCountService(ICacheManager cacheManager, IPropertyRepository propertyRepository)
        {
            this.cacheManager = cacheManager;
            this.propertyRepository = propertyRepository;
        }
        public void Update(Guid propertyId, string visitorId)
        {
            string cacheKey = propertyId.ToString() + visitorId;
            var value = cacheManager.Get<string>(cacheKey);
            if (!string.IsNullOrEmpty(value)) return;

            var property =propertyRepository.GetPropertyById(propertyId);
            if (property is null) return;

            property.ViewCount++;
            propertyRepository.Update(property);
            cacheManager.Store(cacheKey, cacheKey, DateTime.Now.AddMonths(1));
        }
    }
}
