using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Repositories
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Property GetPropertyById(Guid Id);
        List<PropertyState> GetPropertyStates();
        List<PropertyStatus> GetPropertyStatuses();
        List<PropertyType> GetPropertyTypes();
   
    }
}
