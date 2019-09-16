using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(object Id);
        TEntity Update(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> FindAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, string includeProperties = "", int count =0);
    }
}
