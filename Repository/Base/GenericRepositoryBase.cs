using EmailEngine.Base.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.Base
{
    public abstract class GenericRepositoryBase<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IGenericEntity
    {
        public abstract int SaveChanges();


        public abstract Task<int> SaveChangesAsync();


        public abstract EntityEntry<TEntity> Insert(TEntity entity);

        public virtual Task<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual int InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Entity.Id;
        }

        public virtual Task<int> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }
        public virtual async Task InsertOrUpdateAsync(List<TEntity> entity)
        {

            foreach (var entity1 in entity)
            {
                var unused = entity1.IsTransient()
                     ? await InsertAsync(entity1)
                     : await UpdateAsync(entity1);
            }
        }

        public virtual int InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Entity.Id;
        }

        public virtual Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        public abstract Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, Task> updateAction);

        public abstract EntityEntry<TEntity> Update(TEntity entity);

        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public abstract TEntity Update(int id, Action<TEntity> updateAction);
        public abstract Task<TEntity> UpdateAsync(int id, Func<TEntity, Task> updateAction);


        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }



        public abstract void Delete(int id);
        public abstract Task DeleteAsync(int id);
        public abstract void ExecuteRawSqlQuery(string storeprocedureName, object[] param);
        public abstract Task<EntityEntry<TEntity>> InsertOrUpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);


        public abstract IQueryable<TEntity> GetAll();

        public abstract TEntity GetById(int id);

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }


        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual TEntity Get(int id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new Exception("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new Exception("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity FirstOrDefault(int id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().LastOrDefault(predicate);
        }

        public virtual Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LastOrDefault(predicate));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public virtual TEntity Load(int id)
        {
            return Get(id);
        }


        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        //public abstract DbRawSqlQuery<TEntity> StoreprocedureQuery(string storeprocedureName, object[] parameters);

        //public abstract DbRawSqlQuery<TEntity> StoreprocedureQuery(string storeprocedureName, params object[] parameters);
        //public abstract DbRawSqlQuery<TEntity> StoreprocedureQuery(string storeprocedureName);

        public abstract void ExecuteStoreprocedure(string storeprocedureName, params object[] parameters);
        //public abstract DbRawSqlQuery<T> StoreprocedureQuery<T>(string storeprocedureName, params object[] parameters) where T : class;
        //public abstract DbRawSqlQuery<T> StoreprocedureQueryFor<T>(string storeprocedureName, params object[] parameters);
        //public abstract DbRawSqlQuery<T> StoreprocedureQuery<T>(string storeprocedureName) where T : class;
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(int id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(int))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public abstract void Remove(int id);
        public abstract void Remove(TEntity entity);
        public abstract IQueryable<TEntity> GetAllWithoutActive();
    }
}
