using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Threading;

namespace sysnova.Services.CRUDService
{
    public class MyClaimsAuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += Context_PostAuthenticateRequest;
        }

        public void Dispose()
        {
            // Nothing to dispose, method required by IHttpModule
        }

        void Context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var transformer = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager;
            if (transformer != null)
            {
                var context = ((HttpApplication)sender).Context;
                var principal = context.User as ClaimsPrincipal;
                var transformedPrincipal = transformer.Authenticate(context.Request.RawUrl, principal);

                context.User = transformedPrincipal;
                Thread.CurrentPrincipal = transformedPrincipal;
            }
        }
    }
}
