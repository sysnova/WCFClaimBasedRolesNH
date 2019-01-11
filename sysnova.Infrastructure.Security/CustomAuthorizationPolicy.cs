using Ninject;
using Ninject.Web.Common;
using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using sysnova.Infraestructure.Data;
using sysnova.Infrastructure.Interfaces;
using sysnova.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace sysnova.Infrastructure.Security
{
    public class CustomAuthorizationPolicy : IAuthorizationPolicy
    {
        //IProductService _serviceCategory;
        //private static readonly object _padlock = new object();
        IKernel _kernel;

        public CustomAuthorizationPolicy(IKernel kernel)//(IProductService serviceCategory)
        {
            //_serviceCategory = serviceCategory;
            _kernel = kernel;
        }
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            IProductService _serviceCategory = (IProductService)_kernel.Get(typeof(IProductService));

            //lock (_padlock)
            //{
                var identity = (evaluationContext.Properties["Identities"] as List<IIdentity>).Single(i => i.AuthenticationType == "CustomUserNameValidator");
                var claimsIdentity = new ClaimsIdentity(identity);

                IEnumerable<Category> result = _serviceCategory.GetCategories(2);

                //ITERAR POR ROLES
                claimsIdentity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, result.FirstOrDefault().CategoryName));
                claimsIdentity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Networking"));
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //
                evaluationContext.Properties["Principal"] = claimsPrincipal;
                Thread.CurrentPrincipal = claimsPrincipal;
                return true;
            //}
        }

        protected virtual ClaimSet GetClaimSetByIdentity(IIdentity identity)
        {
            List<System.IdentityModel.Claims.Claim> claims = new List<System.IdentityModel.Claims.Claim>();

            //Ejemplo de call Constructor de clase por Reflection para cuando no tenemos IoC
            //IRepository<Category> _repo = (IRepository<Category>)Activator.CreateInstance(typeof(Category));

            claims.Add(new System.IdentityModel.Claims.Claim("CREATED", "Patients", "ACCEPTED"));
            return new DefaultClaimSet(this.Issuer, claims);
        }
        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id
        {
            get { return Guid.NewGuid().ToString(); }
        }

    }
}
