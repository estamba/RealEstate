using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services.Regions
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        public List<City> GetCities(int regionId)
        {

            return regionRepository.GetCities(regionId);
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
           return await  regionRepository.GetAllAsync();
        }
    }
}
