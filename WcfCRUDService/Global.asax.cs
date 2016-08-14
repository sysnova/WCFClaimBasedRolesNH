using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
//
using System.ServiceModel.Activation;
using System.Web.Routing;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;
using sysnova.Infrastructure.DependencyResolution;
using sysnova.Infrastructure.Security;
//

namespace sysnova.Services.CRUDService
{
    public class Global : HttpApplication
    {

        // NinjectHttpApplication needs to do some initialization and it does this by hooking the Application_Started
        // method for us.  When usiong NinjectHttpApplication, move any code you would put in Application_Started
        // to the OnApplicationStarted method below.
        
        /*
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }
        */

        // OnApplicationStarted is a virtual method provided by NinjectHttpApplication so you can do startup logic that you
        // would normally do in Application_Started.  One nice side effect of this is that you can use the Ninject Kernel
        // to resolve services that might be needed during application startup.
        
        /*
        protected override void OnApplicationStarted()
        {
            //base.OnApplicationStarted();
            RegisterRoutes();
        }
        */

        /*
        private void RegisterRoutes()
        {
            // We replace WebServiceHostFactory with NinjectWebServiceHostFactory so Ninject can handle creation of
            // the services using the Ninjection kernel for each inbound request.
            //RouteTable.Routes.Add(new ServiceRoute("Service1", new WebServiceHostFactory(), typeof(Service1)));
            RouteTable.Routes.Add(new ServiceRoute("Service1", new OAuthHostFactory(), typeof(Service1)));
        }
        */

        // Since Ninject depends on a Kernel with the appropriate bindings, NinjectHttpApplication gets this from us
        // via the abstract method CreateKernel.  
        
        /*
        protected override IKernel CreateKernel()
        {
            // The actual bindings are provided via ServiceModule.  Alternatively, we could call the StandardKernel overload that finds
            // NinjectModules based on paths to assemblies, etc.
            //return new StandardKernel(new RepositoryModule());
        }
        */
        /*
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {

                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                //kernel.Bind<ISession>().ToMethod(x => NHibernateSession.Current);
                //
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
        private static void RegisterServices(IKernel kernel)
        {
            // Bind local services
            //kernel.Bind<IProductService>().To<ProductService>();

            // Add data and infrastructure modules
            var modules = new List<WcfModule>
                {
                    new RepositoryModule()

                };
            kernel.Load(modules);
        } 
        */
    }
}