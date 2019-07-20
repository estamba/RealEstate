using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();

        }
        public virtual void AddRange(List<TEntity> entities)
        {
            dbSet.AddRange(entities);
            dbContext.SaveChanges();

        }
        public void Delete(object Id)
        {
            TEntity entity = dbSet.Find(Id);
            dbContext.Remove(entity);
            dbContext.SaveChanges();

        }
        public void Update(TEntity entity)
        {
            dbContext.Attach(entity).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
    }
}
