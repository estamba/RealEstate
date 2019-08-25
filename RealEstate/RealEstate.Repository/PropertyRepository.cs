using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstate.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        private readonly RealEstateDbContext dbContext;

        public PropertyRepository(RealEstateDbContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<PropertyState> GetPropertyStates()
        {
            return dbContext.PropertyState.ToList();
        }

        public List<PropertyStatus> GetPropertyStatuses()
        {
            return dbContext.PropertyStatus.ToList();
        }

        public List<PropertyType> GetPropertyTypes()
        {
            return dbContext.PropertyType.ToList();
        }
        public Property GetPropertyById(Guid Id)
        {
            return dbContext.Property
                .Include(d => d.Ágent)
               .Include(d => d.Status)
               .Include(d => d.Type)
                .Include(d => d.PropertyImages)
                .Include(d => d.City)
                .Include(d => d.Status)


                .Where(d => d.Id == Id).FirstOrDefault();
        }
    }
}
