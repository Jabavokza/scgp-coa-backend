using Microsoft.EntityFrameworkCore;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.DATAACCESS.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SCGP.COA.DATAACCESS.Infrastructures
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        public DbDataContext _db;
        public DbReadDataContext _dbRead;

        public RepositoryBase(DbDataContext db, DbReadDataContext dbRead)
        {
            _db = db;
            _dbRead = dbRead;
        }
        public void Dispose()
        {
            //   Db.Dispose();
        }


        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate).AsQueryable();
        }
        public void Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);

        }
        public void Delete(List<TEntity> entity)
        {
            _db.Set<TEntity>().RemoveRange(entity);

        }
        public TEntity Add(TEntity item)
        {
            try
            {
                _db.Add(item);

                return item;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;

            }

        }
        public List<TEntity> Add(List<TEntity> item)
        {
            try
            {
                _db.AddRange(item);

                return item;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;

            }

        }

        public TEntity GetById(int id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public List<TEntity> Get()
        {

            return _db.Set<TEntity>().ToList();
        }
        public void DetachAllEntities()
        {
            var changedEntriesCopy = _db.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public bool Update(TEntity item)
        {

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {

                    _db.Entry(item).State = EntityState.Modified;
                    //_db.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;


                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                }
            } while (saveFailed);
            return true;

        }
        public IQueryable<TEntity> Query()
        {
            var dd = _db.Database.GetDbConnection().ConnectionString;
            return _db.Set<TEntity>();
        }

        public IQueryable<TEntity> Read()
        {
            var dd = _db.Database.GetDbConnection().ConnectionString;
            return _db.Set<TEntity>();
        }


        public int Count()
        {
            return _db.Set<TEntity>().Count();
        }

        public void Commit()
        {
            try
            {
                Validate();
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
        public void Validate()
        {
            var entities = _db.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
            }
        }

        public IQueryable<TEntity> GetRead(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbRead.Set<TEntity>().Where(predicate).AsQueryable();
        }


    }
}
