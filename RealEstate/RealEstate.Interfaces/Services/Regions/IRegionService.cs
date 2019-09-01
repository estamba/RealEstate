using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces.Services.Regions
{
    public interface ILocationService
    {
        Task<List<Region>>GetRegionsAsync();
        List<City> GetCities(int regionId);
        List<Region> GetRegions();

    }
}
