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
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        private readonly RealEstateDbContext dbContext;

        public PropertyRepository(RealEstateDbContext dbContext) : base(dbContext)
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
                .ThenInclude(d=>d.ApplicationUser)
                .Include(d=>d.Ágent.City)
               .Include(d => d.Status)
               .Include(d => d.State)
               .Include(d => d.Type)
                .Include(d => d.PropertyImages)
                .Include(d => d.City)


                .Where(d => d.Id == Id).AsNoTracking().FirstOrDefault();
        }
        public async Task<List<Property>> GetPropertiesByAgentIDAsync(Guid Id, PropertySortOptions sortOptions)
        {
            var query = dbContext.Property.Include(d => d.Ágent)
                .ThenInclude(d => d.ApplicationUser)
                .Include(d => d.Type)
                .Include(d => d.Status)
               .Include(d => d.State)
                .Include(d => d.PropertyImages)
                .Include(d => d.City)
                .Where(d => d.AgentId == Id && !d.IsDelete).AsQueryable();
            query = Order(query, sortOptions);
            return await query.ToListAsync();
        }
        public List<Property> GetProperties(int count)
        {
            if (count > 0)
                return dbContext.Property.Include(d => d.Ágent)
                .Include(d => d.Status)
                .Include(d => d.Type)
                .Include(d => d.PropertyImages)
                .Include(d => d.City)
                .Include(d => d.Status)
                .Where(d => !d.IsDelete)
                .Take(count).AsNoTracking().ToList();

            return dbContext.Property.Include(d => d.Ágent)
                .Include(d => d.Status)
                .Include(d => d.Type)
                .Include(d => d.PropertyImages)
                .Include(d => d.City)
                .Include(d => d.Status)
                .Where(d=>!d.IsDelete)
                .AsNoTracking().ToList();
        }

        public PaginatedSearchResult<Property> Search(PropertySearchFilter searchFilter, int pageSize, int pageNumber)
        {
         
            var query = dbContext.Property.AsQueryable();
            if (!string.IsNullOrEmpty(searchFilter.Title)) query = query.Where(x => x.Title.Contains(searchFilter.Title));
            if (!string.IsNullOrEmpty(searchFilter.Location)) query = query.Where(x => x.City.Name == searchFilter.Location || x.City.Region.Name == searchFilter.Location);
            if (searchFilter.MinPrice != null) query = query.Where(x => x.Price >= searchFilter.MinPrice);
            if (searchFilter.Maxprice != null) query = query.Where(x => x.Price <= searchFilter.Maxprice);
            if (searchFilter.TypeId != null) query = query.Where(x => x.TypeId == searchFilter.TypeId);
            if (searchFilter.StatusId != null) query = query.Where(x => x.StatusId == searchFilter.StatusId);

            int skipCount = 0;
            if (pageNumber > 0) skipCount  = (pageNumber - 1) * pageSize;
            query = Order(query, searchFilter.SortOption);

            var totalCount = query.ToList().Count;
            query = query.Skip(skipCount);
            query = query.Take(pageSize);

            query = query.Include(d => d.Status)
               .Include(d => d.Type)
                .Include(d => d.PropertyImages)
                .Include(d => d.City)
                .Include(d => d.Status)
                .Where(d => !d.IsDelete);

            var results = query.ToList();
         
            return new PaginatedSearchResult<Property>()
            {
                Page = pageNumber,
                PageSize = pageSize,
                Results = results,
                PageCount = GetPageCount(totalCount,pageSize),
                Total = totalCount
            };
        }
        private IQueryable<Property> Order(IQueryable<Property> query, PropertySortOptions SortOption)
        {
            switch (SortOption)
            {
                case PropertySortOptions.AddedDateAsc:
                    query = query.OrderBy(p => p.DateCreated);
                    break;
                case PropertySortOptions.AddedDateDesc:
                    query = query.OrderByDescending(p => p.DateCreated);
                    break;
                case PropertySortOptions.PriceAsc:
                    query = query.OrderBy(p => p.Price);
                    break;
                case PropertySortOptions.PriceDesc:
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = query.OrderByDescending(p => p.DateCreated);
                    break;

            }
            return query;
        }
        private int GetPageCount(int total, int pageSize)
        {
            int pageCount = total / pageSize;
            return total > (pageCount * pageSize) ? pageCount + 1 : pageCount;
        }
    }
}
