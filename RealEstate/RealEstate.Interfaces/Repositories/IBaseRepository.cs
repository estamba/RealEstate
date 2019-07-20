using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(object Id);
        void Update(TEntity entity);
    }
}
