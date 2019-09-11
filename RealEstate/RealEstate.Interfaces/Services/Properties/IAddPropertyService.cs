using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Properties
{
    public interface IAddPropertyService
    {
        Property Add(PostPropertyModel postPropertyModel);
        void UpdatePropertyState(Guid Id, string state);
        void Delete(Guid Id);
        void Update(EditPropertyModel editPropertyModel);

    }
}
