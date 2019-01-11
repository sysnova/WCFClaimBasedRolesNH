using sysnova.Infrastructure.Interfaces;
using sysnova.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Wcf;
using sysnova.Infrastructure.Security;
using System.IdentityModel.Policy;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Ninject.Web.Common;
using sysnova.Infrastructure.EventBus;
using sysnova.Infrastructure.EventBus.Dispatcher;
using sysnova.Infrastructure.EventBroker;

namespace sysnova.Infrastructure.DependencyResolution
{
    public class OAuthHostFactory : NinjectServiceHostFactory
    {
        private IKernel _kernel;       
        public OAuthHostFactory()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<ServiceHost>().To<NinjectServiceHost>().InTransientScope();
            //_kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            RegisterServices(_kernel);
            SetKernel(_kernel);
        }
        //public IKernel GetKernel { get { return _kernel; } }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);          
            
            //Voy a tratar de injectar el Kernel y no el Servicio en los Custom Authorization y Authentication
            //IProductService _className = (IProductService) _kernel.Get(typeof(IProductService));
            
            //AUTHENTICATION
            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomUserNameValidator(_kernel);
            //FIN AUTHENTICATION

            //AUTHORIZATION MANAGER & POLICY
            var policies = new List<IAuthorizationPolicy>();
            if (host.Authorization.ExternalAuthorizationPolicies != null)
            {
                policies.AddRange(host.Authorization.ExternalAuthorizationPolicies);
            }
            // Add new policy
            policies.Add(new CustomAuthorizationPolicy(_kernel)); //REGLA CUSTOM
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            // Set correct mode
            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager(_kernel);
            //FIN AUTHORIZATION

            return host;
        }
        private static void RegisterServices(IKernel kernel)
        {
            //DomainEvent.Dispatcher = new DefaultEventBus(kernel);

            var modules = new List<WcfModule>
                {
                    new RepositoryModule(),
                    new EventBrokerModule()
                };
            kernel.Load(modules);
        } 
    }
}
