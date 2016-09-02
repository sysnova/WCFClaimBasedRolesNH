using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;
using NHibernate;
//
using sysnova.Domain.Interfaces;
using sysnova.Infraestructure.Data;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IdentityModel.Policy;
using sysnova.Infrastructure.Security;
//
using sysnova.Infrastructure.Services;
using sysnova.Infrastructure.Interfaces;
using sysnova.Infrastructure.CommandBus.Dispatcher;
using sysnova.Infrastructure.CommandBus.Command;
using sysnova.Infrastructure.CommandBus.ValidationHandler;
using sysnova.Infrastructure.CommandBus.Handler;


namespace sysnova.Infrastructure.DependencyResolution
{
    public class RepositoryModule : WcfModule
    {
        public override void Load()
        {

            Bind<ISessionFactory>().ToProvider<NhibernateSessionFactoryProvider>().InSingletonScope();

            Bind<ISession>().ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();
            
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            Bind<IProductService>().To<ProductService>();

            Bind<ICommandBus>().To<DefaultCommandBus>();

            Bind<ICommandHandler<CreateOrUpdateCategoryCommand>>().To<CreateOrUpdateCategoryHandler>();

            Bind<IValidationHandler<CreateOrUpdateCategoryCommand>>().To<CreateOrUpdateCategoryValidationHandler>();
        }
    }
}
