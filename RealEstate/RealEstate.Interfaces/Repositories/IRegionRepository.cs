using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Repositories
{
   public  interface IRegionRepository : IBaseRepository<Region>
    {
        List<City> GetCities(int regionId);
        List<Region> GetRegions();



    }
}
