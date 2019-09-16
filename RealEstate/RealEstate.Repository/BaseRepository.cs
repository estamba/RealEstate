using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        DbContext dbContext;
        DbSet<TEntity> dbSet;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }
        public TEntity  Add(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
            return entity;

        }
        public virtual void AddRange(List<TEntity> entities)
        {
            dbSet.AddRange(entities);
            dbContext.SaveChanges();

        }
        public virtual void Delete(object Id)
        {
            TEntity entity = dbSet.Find(Id);
            dbContext.Remove(entity);
            dbContext.SaveChanges();

        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        public async virtual Task<List<TEntity>> FindAsync(
           Expression<Func<TEntity, bool>> filter = null,
           
           string includeProperties = "", int count =0)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (count > 0) query = query.Take(count);



            return await query.ToListAsync();
        }
        public virtual TEntity Update(TEntity entity)
        {
            dbContext.Attach(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
            return entity;
        }
    }
}
