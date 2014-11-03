using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PreOrderTreeTraversal.GenericRepository
{
         public class GenericRepository<TEntity> where TEntity : class
         {
             internal DbContext context;
             internal DbSet<TEntity> dbSet;

             public GenericRepository(DbContext context)
             {
                 this.context = context;
                 this.dbSet = context.Set<TEntity>();
             }

             protected virtual IEnumerable<TEntity> Get(Func<TEntity, bool> filter)
             {
                 return dbSet.Where(filter).ToList();
             }

             protected virtual IEnumerable<TEntity> Get()
             {
                 return dbSet;
             }

             public virtual TEntity[] GetAll()
             {
                 return dbSet.ToArray();
             }

             public virtual TEntity GetByID(object id)
             {
                 return dbSet.Find(id);
             }

             public virtual void Add(TEntity entity)
             {
                 dbSet.Add(entity);
             }

             public virtual void Delete(TEntity entity)
             {
                 dbSet.Remove(entity);
             }

             public virtual void DeleteRange(IEnumerable<TEntity> entities)
             {
                 dbSet.RemoveRange(entities);
             }
         }
}
