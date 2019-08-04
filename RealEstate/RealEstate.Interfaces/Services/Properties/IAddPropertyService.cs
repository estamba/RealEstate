using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Properties
{
    public interface IAddPropertyService
    {
        void Add(PostPropertyModel postPropertyModel);
    }
}
