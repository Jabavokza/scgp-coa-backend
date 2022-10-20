using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SCGP.COA.DATAACCESS.Infrastructures
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity item);
        List<TEntity> Add(List<TEntity> item);
        TEntity GetById(int id);
        List<TEntity> Get();
        bool Update(TEntity item);
        int Count();
        void Dispose();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        void Delete(List<TEntity> entity);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Read();
        void Commit();
        void DetachAllEntities();
    }


}
