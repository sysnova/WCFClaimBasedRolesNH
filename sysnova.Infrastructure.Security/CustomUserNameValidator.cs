using Ninject;
using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using sysnova.Infrastructure.Errors;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.Security
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        private static readonly object _padlock = new object();
        IProductService _serviceCategory;

        public CustomUserNameValidator (IKernel kernel) //(IProductService serviceCategory)
        {
            //_serviceCategory = serviceCategory;
            _serviceCategory = (IProductService)kernel.Get(typeof(IProductService));
        }

        public override void Validate(string userName, string password)
        {
            lock (_padlock)
            {
            
                IEnumerable<Category> result = _serviceCategory.GetCategories(1);

                if (!(result.FirstOrDefault().CategoryName == "Beverages"))
                {
                    throw new MessageSecurityException("Userid or Password is invalid", new FaultException("Userid or Password is invalid"));
                }
            }

        }

        private static GenericPrincipal GetGenericPrincipal()
        {
            // Use values from the current WindowsIdentity to construct
            // a set of GenericPrincipal roles.
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            string[] roles = new string[10];
            if (windowsIdentity.IsAuthenticated)
            {
                // Add custom NetworkUser role.
                roles[0] = "NetworkUser";
            }

            if (windowsIdentity.IsGuest)
            {
                // Add custom GuestUser role.
                roles[1] = "GuestUser";
            }

            if (windowsIdentity.IsSystem)
            {
                // Add custom SystemUser role.
                roles[2] = "SystemUser";
            }

            // Construct a GenericIdentity object based on the current Windows
            // identity name and authentication type.
            string authenticationType = windowsIdentity.AuthenticationType;
            string userName = windowsIdentity.Name;
            GenericIdentity genericIdentity =
                new GenericIdentity(userName, authenticationType);

            // Construct a GenericPrincipal object based on the generic identity
            // and custom roles for the user.
            GenericPrincipal genericPrincipal =
                new GenericPrincipal(genericIdentity, roles);

            return genericPrincipal;
        }


    }
}
