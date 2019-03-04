using Autofac;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.Base;
using EmailEngine.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Repository.AutoFacModule
{
    public class GenericRepositoryModule<TEntity, TDbContext> : Module where TEntity : class,IGenericEntity where TDbContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {



            builder.RegisterType<GenericRepository<TEntity, TDbContext>>()
                .As<IGenericRepository<TEntity>>()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRegister).Assembly)
                .AssignableTo<IAutoDependencyRegister>()
                .As<IAutoDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
