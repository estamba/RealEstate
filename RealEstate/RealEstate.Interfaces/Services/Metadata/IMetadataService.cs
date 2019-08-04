using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Metadata
{
   public interface IMetadataService
    {
        List<PropertyState> GetPropertyStates();
        List<PropertyStatus> GetPropertyStatuses();
        List<PropertyType> GetPropertyTypes();


    }
}
