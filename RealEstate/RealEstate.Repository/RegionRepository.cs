using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repositories
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        private readonly RealEstateDbContext dbContext;

        public RegionRepository(RealEstateDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public override async Task<List<Region>> GetAllAsync()
        {

            return await dbContext.Region.Include(r => r.Cities).ToListAsync();
        }

        public List<City> GetCities(int regionId)
        {
            return dbContext.City.Where(c => c.RegionId == regionId).ToList();
        }

        public List<City> GetCities()
        {
            return dbContext.City.ToList();
        }

        public List<Region> GetRegions()
        {
            return dbContext.Region.Include(r => r.Cities).ToList();
        }
    }
}
