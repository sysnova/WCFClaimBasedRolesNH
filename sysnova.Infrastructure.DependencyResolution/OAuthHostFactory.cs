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

namespace sysnova.Infrastructure.DependencyResolution
{
    public class OAuthHostFactory : NinjectServiceHostFactory
    {
        private IKernel _kernel;
        public OAuthHostFactory()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            RegisterServices(_kernel);
            SetKernel(_kernel);
        }
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);          
            IProductService _className = (IProductService) _kernel.Get(typeof(IProductService));
            //AUTHENTICATION
            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomUserNameValidator(_className);
            //FIN AUTHENTICATION

            //AUTHORIZATION MANAGER & POLICY
            var policies = new List<IAuthorizationPolicy>();
            if (host.Authorization.ExternalAuthorizationPolicies != null)
            {
                policies.AddRange(host.Authorization.ExternalAuthorizationPolicies);
            }
            // Add new policy
            policies.Add(new CustomAuthorizationPolicy(_className)); //REGLA CUSTOM
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            // Set correct mode
            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager(_className);
            //FIN AUTHORIZATION

            return host;
        }
        private static void RegisterServices(IKernel kernel)
        {
            var modules = new List<WcfModule>
                {
                    new RepositoryModule()
                };
            kernel.Load(modules);
        } 
    }
}
