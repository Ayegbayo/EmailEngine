using EmailEngine.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.Base
{
    public class GenericRepository<TEntity, TDbContext> : GenericRepositoryBase<TEntity> where TEntity : class, IGenericEntity where TDbContext : DbContext
    {
        private readonly TDbContext _dbContextProvider;

        public GenericRepository(TDbContext dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }


        /// <summary>
        /// get the current Entity
        /// <returns><see cref="DbSet{TEntity}"/></returns>
        /// </summary>
        public virtual DbSet<TEntity> Table => _dbContextProvider.Set<TEntity>();

        /// <summary>
        /// get the entity <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Table.Where(x => !x.IsDeleted && x.IsActive);
        }

        public override TEntity GetById(int id)
        {
            return Table.Find(id);
        }



        public override void ExecuteStoreprocedure(string storeprocedureName, params object[] parameters)
        {
            //_dbContextProvider.Database.ExecuteSqlCommand("EXEC " + storeprocedureName, parameters);
        }









        public override async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            return !Table.Any(predicate) && entity.Id == 0
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }
        public override int SaveChanges()
        {
            return _dbContextProvider.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await _dbContextProvider.SaveChangesAsync();
        }

        public override EntityEntry<TEntity> Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        public override Task<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Table.Add(entity));
        }

        public override int InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity).Entity;
            //entity.IsTransient()
            if (entity.IsTransient())
            {
                _dbContextProvider.SaveChanges();
            }

            return entity.Id;
        }

        public override async Task<int> InsertAndGetIdAsync(TEntity entity)
        {
            var ent = await InsertAsync(entity);
            entity = ent.Entity;
            if (entity.IsTransient())
            {
                await _dbContextProvider.SaveChangesAsync();
            }

            return entity.Id;
        }

        public override int InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity).Entity;
            //entity.IsTransient()
            if (entity.IsTransient())
            {
                _dbContextProvider.SaveChanges();
            }

            return entity.Id;
        }

        public override async Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            var ent = await InsertOrUpdateAsync(entity);
            entity = ent.Entity;
            if (entity.IsTransient())
            {
                await _dbContextProvider.SaveChangesAsync();
            }

            return entity.Id;
        }

        public override EntityEntry<TEntity> Update(TEntity entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return ent;
        }

        public override Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return Task.FromResult(ent);
        }

        public override void Delete(TEntity entity)
        {
            Update(entity.Id, x => x.IsDeleted = true);
        }


        public override TEntity Update(int id, Action<TEntity> updateAction)
        {
            var entity = Table.Find(id);
            updateAction(entity);
            return entity;
        }

        public override void Delete(int id)
        {
            Update(id, x => x.IsDeleted = true);
        }

        public override Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));

        }


        public override void ExecuteRawSqlQuery(string storeprocedureName, object[] param)
        {
            ///_dbContextProvider.Database.ExecuteSqlCommand("EXEC " + storeprocedureName, param);
        }


        public override async Task<TEntity> UpdateAsync(int id, Func<TEntity, Task> updateAction)
        {
            var entity = await Table.FindAsync(id);
            await updateAction(entity);
            return entity;
        }

        public override async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, Task> updateAction)
        {
            var entity = await Table.FirstOrDefaultAsync(predicate);
            await updateAction(entity);
            return entity;
        }


        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        public override void Remove(int id)
        {
            var entity = Table.FirstOrDefault(x => x.Id == id);
            _dbContextProvider.Remove(entity);
        }

        public override void Remove(TEntity entity)
        {
            _dbContextProvider.Remove(entity);
        }

        public override IQueryable<TEntity> GetAllWithoutActive()
        {
            return Table.Where(x => !x.IsDeleted);
        }
    }
}
