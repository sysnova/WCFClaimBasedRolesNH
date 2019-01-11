using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Security.Principal;
using System.Threading;
using sysnova.Infrastructure.Interfaces;
using sysnova.Domain.Entities;
using Ninject;
using System.ServiceModel.Security;

namespace sysnova.Infrastructure.Security
{
    public class MyServiceAuthorizationManager : ServiceAuthorizationManager
    {
        //IProductService _serviceCategory;
        //private static readonly object _padlock = new object();
        IKernel _kernel;

        public MyServiceAuthorizationManager(IKernel kernel)//(IProductService serviceCategory)
        {
            //_serviceCategory = serviceCategory;
            _kernel = kernel;
        }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            IProductService _serviceCategory = (IProductService)_kernel.Get(typeof(IProductService));

            //lock (_padlock)
            //{
                //Accedo al Repo dentro del ServiceAuthManager
                IEnumerable<Category> result = _serviceCategory.GetCategories(3);

                //CHECK IDENTITY PRIMARY
                IIdentity identity = operationContext.ServiceSecurityContext.PrimaryIdentity;

                if (identity == null)
                    throw new FaultException();//Es capturada por el IErrorHandler genérico

                //CHECK IDENTITY PRINCIPAL
                var principal = Thread.CurrentPrincipal;
                if (!(principal.IsInRole("Condiments")))
                    throw new MessageSecurityException("CheckAccessCore is invalid", new FaultException("Core is invalid"));

                // Extract the action URI from the OperationContext. Match this against the claims
                // in the AuthorizationContext.
                string action = operationContext.RequestContext.RequestMessage.Headers.Action;
                //Thread.CurrentPrincipal.IsInRole("Admin");
                //return true;

                //CHECK HEADER CUSTOM
                //var requestMessage = operationContext.RequestContext.RequestMessage;
                //var requestProperty = (HttpRequestMessageProperty)requestMessage
                //    .Properties[HttpRequestMessageProperty.Name];
                //var token = requestProperty.Headers["X-MyCustomHeader"];
                return true;
            //}
            
            // Iterate through the various claim sets in the AuthorizationContext.
            /*
            foreach (ClaimSet cs in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            {
                // Examine only those claim sets issued by System.
                if (cs.Issuer == ClaimSet.System)
                {
                    // Iterate through claims of type "http://www.contoso.com/claims/allowedoperation".
                    foreach (Claim c in cs.FindClaims("http://schemas.xmlsoap.org/ws/2005/05/identity/right/possessproperty", Rights.PossessProperty))
                    {
                        // If the Claim resource matches the action URI then return true to allow access.
                        if (action == c.Resource.ToString())
                            return true;
                    }
                }
            }
            */
        }
   
   }
}
