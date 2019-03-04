using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmailEngine.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using VATEEP.Repository.Base;

namespace EmailEngine.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TVatEmailLog, TVatEmailTemplate, TDbContext> : RepositoryBase<TVatEmailLog, TVatEmailTemplate> where TVatEmailLog:VatEmailLog where TVatEmailTemplate:VatEmailTemplate where TDbContext: DbContext
    {
        private readonly TDbContext _dbContextProvider;

        public Repository(TDbContext dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }


        /// <summary>
        /// get the current Entity
        /// <returns><see cref="DbSet{TEntity}"/></returns>
        /// </summary>
        public virtual DbSet<TVatEmailLog> Table => _dbContextProvider.Set<TVatEmailLog>();
        public virtual DbSet<TVatEmailTemplate> TableTemplate => _dbContextProvider.Set<TVatEmailTemplate>();

        /// <summary>
        /// get the entity <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns></returns>


        public override TVatEmailLog GetById(int id)
        {
            return Table.Find(id);
        }



        public override void ExecuteStoreprocedure(string storeprocedureName, params object[] parameters)
        {
            //_dbContextProvider.Database.ExecuteSqlCommand("EXEC " + storeprocedureName, parameters);
        }       
        
        public override async Task<EntityEntry<TVatEmailLog>> InsertOrUpdateAsync(Expression<Func<TVatEmailLog, bool>> predicate, TVatEmailLog entity)
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

        public override EntityEntry<TVatEmailLog> Insert(TVatEmailLog entity)
        {
            return Table.Add(entity);
        }

        public override EntityEntry<TVatEmailTemplate> Insert(TVatEmailTemplate entity)
        {
            return TableTemplate.Add(entity);
        }       

        public override Task<EntityEntry<TVatEmailTemplate>> InsertAsync(TVatEmailTemplate entity)
        {
            return Task.FromResult(TableTemplate.Add(entity));
        }

        public override int InsertAndGetId(TVatEmailLog entity)
        {
            entity = Insert(entity).Entity;
            //entity.IsTransient()
            //if (entity.IsTransient())
            //{
                _dbContextProvider.SaveChanges();
            //}

            return entity.Id;
        }

        public override async Task<int> InsertAndGetIdAsync(TVatEmailLog entity)
        {
            var ent = await InsertAsync(entity);
            entity = ent.Entity;
            //if (entity.IsTransient())
            //{
                await _dbContextProvider.SaveChangesAsync();
            //}

            return entity.Id;
        }

        public override int InsertOrUpdateAndGetId(TVatEmailTemplate entity)
        {
            entity = InsertOrUpdate(entity).Entity;
            //entity.IsTransient()
            //if (entity.IsTransient())
            //{
                _dbContextProvider.SaveChanges();
            //}

            return entity.Id;
        }

        public override async Task<int> InsertOrUpdateAndGetIdAsync(TVatEmailTemplate entity)
        {
            var ent = await InsertOrUpdateAsync(entity);
            entity = ent.Entity;
            //if (entity.IsTransient())
            //{
                await _dbContextProvider.SaveChangesAsync();
            //}

            return entity.Id;
        }

        public override EntityEntry<TVatEmailTemplate> Update(TVatEmailTemplate entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return ent;
        }

        public override Task<EntityEntry<TVatEmailLog>> UpdateAsync(TVatEmailLog entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return Task.FromResult(ent);
        }

        public override void Delete(TVatEmailLog entity)
        {
            Table.Remove(entity);
            _dbContextProvider.SaveChanges();
        }


        public override TVatEmailLog Update(int id, Action<TVatEmailLog> updateAction)
        {
            var entity = Table.Find(id);
            updateAction(entity);
            return entity;
        }

        public override void Delete(int id)
        {
            var email =  TableTemplate.FirstOrDefault(x => x.Id == id);
            TableTemplate.Remove(email);
            _dbContextProvider.SaveChanges();
        }

        public override Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));

        }


        public override void ExecuteRawSqlQuery(string storeprocedureName, object[] param)
        {
            //_dbContextProvider.Database.ExecuteSqlCommand("EXEC " + storeprocedureName, param);
        }


        public override async Task<TVatEmailLog> UpdateAsync(int id, Func<TVatEmailLog, Task> updateAction)
        {
            var entity = await Table.FindAsync(id);
            await updateAction(entity);
            return entity;
        }

        public override async Task<TVatEmailLog> UpdateAsync(Expression<Func<TVatEmailLog, bool>> predicate, Func<TVatEmailLog, Task> updateAction)
        {
            var entity = await Table.FirstOrDefaultAsync(predicate);
            await updateAction(entity);
            return entity;
        }


        protected virtual void AttachIfNot(TVatEmailTemplate entity)
        {
            if (!TableTemplate.Local.Contains(entity))
            {
                TableTemplate.Attach(entity);
            }
        }

        protected virtual void AttachIfNot(TVatEmailLog entity)
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

        public override void Remove(TVatEmailLog entity)
        {
            _dbContextProvider.Remove(entity);
        }

        public override IQueryable<TVatEmailLog> GetAllWithoutActive()
        {
            return Table;
        }

        public override async Task<TVatEmailTemplate> UpdateAsync(Expression<Func<TVatEmailTemplate, bool>> predicate, Func<TVatEmailTemplate, Task> updateAction)
        {
            var emailTemlate = await TableTemplate.FirstOrDefaultAsync(predicate);
            await updateAction(emailTemlate);
            return emailTemlate;

        }

        public override EntityEntry<TVatEmailLog> Update(TVatEmailLog entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return ent;
        }

        public override IQueryable<TVatEmailLog> GetAll()
        {
            return Table;
        }

        public override IQueryable<TVatEmailTemplate> GetEMailTemplate(Expression<Func<TVatEmailTemplate, bool>> predicate)
        {
            return TableTemplate.Where(predicate);
        }
    }
}