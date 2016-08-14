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

            //NinjectServiceHost host = Kernel.Get<NinjectServiceHost>();
            //host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = Kernel.Get<CustomUserNameValidator>();
            
        }
    }
}
