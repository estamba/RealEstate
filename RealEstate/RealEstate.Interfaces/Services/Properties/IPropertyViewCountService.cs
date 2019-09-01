using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Properties
{
   public interface IPropertyViewCountService
    {
        void Update(Guid propertyId, string visitorId);
    }
}
