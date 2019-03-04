using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.Base
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        #region SaveChanges
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        EntityEntry<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity);

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        int InsertAndGetId(TEntity entity);

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        Task<int> InsertAndGetIdAsync(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        EntityEntry<TEntity> InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertOrUpdateAsync(List<TEntity> entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        int InsertOrUpdateAndGetId(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);

        #endregion

        #region Update

        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, Task> updateAction);
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        EntityEntry<TEntity> Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        TEntity Update(int id, Action<TEntity> updateAction);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        Task<TEntity> UpdateAsync(int id, Func<TEntity, Task> updateAction);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        Task DeleteAsync(TEntity entity);

        ///// <summary>
        ///// Deletes an entity by primary key.
        ///// </summary>
        ///// <param name="id">Primary key of the entity</param>
        //void Delete(TPrimaryKey id);

        ///// <summary>
        ///// Deletes an entity by primary key.
        ///// </summary>
        ///// <param name="id">Primary key of the entity</param>
        //Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(int id);
        void Remove(int id);
        void Remove(TEntity entity);
        #endregion



        #region Select/Get/Query

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// <see>
        ///     <cref>UnitOfWorkAttribute</cref>
        /// </see>
        ///     attribute must be used to be able to call this method since this method
        /// returns IQueryable and it requires open database connection to use it.
        /// </summary>
        /// <returns>
        /// <see cref="IQueryable{TEntity}"/> to be used to select entities from database 
        /// 
        /// </returns>
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllWithoutActive();

        TEntity GetById(int id);



        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>
        /// <see cref="List{T}"/>
        /// List of all entities</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// Used to get all entities Async.
        /// </summary>
        /// <returns>List of all entities Async</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to run a query over entire entities.
        /// <see>
        ///     <cref>UnitOfWorkAttribute</cref>
        /// </see>
        ///     attribute is not always necessary (as opposite to <see cref="GetAll"/>)
        /// if <paramref name="queryMethod"/> finishes IQueryable with ToList, FirstOrDefault etc..
        /// </summary>
        /// <typeparam name="T">Type of return value of this method</typeparam>
        /// <param name="queryMethod">This method is used to query over entities</param>
        /// <returns>Query result</returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity <see cref="TEntity"/></returns>
        TEntity Get(int id);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity <see cref="Task{TEntity}"/></returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity <see cref="TEntity"/></param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        TEntity FirstOrDefault(int id);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        Task<TEntity> FirstOrDefaultAsync(int id);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Creates an entity with given primary key without database access.
        /// </summary>
        /// <param name="id">Primary key of the entity to load</param>
        /// <returns>Entity</returns>
        TEntity Load(int id);

        #endregion
        #region Aggregates

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        int Count();

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount();

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Store Procedure



        void ExecuteStoreprocedure(string storeprocedureName, params object[] parameters);
        void ExecuteRawSqlQuery(string storeprocedureName, object[] param);

        #endregion
    }
}
