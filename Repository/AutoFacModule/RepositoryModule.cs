using Autofac;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.Base;
using EmailEngine.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmailEngine.Repository.AutoFacModule
{
    public class RepositoryModule<TVatEmailLog, TVatEmailTemplate, TDbContext>:Module where TVatEmailLog : VatEmailLog where TVatEmailTemplate : VatEmailTemplate where TDbContext :DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {



            builder.RegisterType<Repository<TVatEmailLog, TVatEmailTemplate, TDbContext>>()
                .As<IRepository<TVatEmailLog, TVatEmailTemplate>>()
                .InstancePerLifetimeScope();

            

            //builder.RegisterGeneric()typeof(Repository<TVatEmailLog,TVatEmailTemplate,TDbContext>))
            //    .As(typeof(IRepository<,>))
            //    .InstancePerLifetimeScope();
            //builder.RegisterType<NACCPaymentJobRepository>().As<INACCPaymentJobRepository>().InstancePerMatchingLifetimeScope();
            //builder.RegisterType<EmailJobRepositorycs>().As<IEmailJobRepository>().InstancePerMatchingLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRegister).Assembly)
                .AssignableTo<IAutoDependencyRegister>()
                .As<IAutoDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}