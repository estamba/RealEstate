using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services.Regions
{
    public class LocationService : ILocationService
    {
        private readonly IRegionRepository regionRepository;

        public LocationService(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        public List<City> GetCities(int regionId)
        {

            return regionRepository.GetCities(regionId);
        }
        public List<City> GetCities()
        {

            return regionRepository.GetCities();
        }
        public List<Region> GetRegions()
        {
            return regionRepository.GetRegions();
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
           return await  regionRepository.GetAllAsync();
        }
    }
}
