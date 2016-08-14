using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;

namespace sysnova.Infrastructure.Security
{
    public class ClaimsTransformationModule : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated == true)
            {
                ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim(ClaimTypes.Role, "Coco"));
            }

            return incomingPrincipal;
        }
    }
}
// ESTA CLASE NO SE IMPLEMENTA. ES INVOCADA POR EL HTTP-MODULE QUE DESACTIVE EN EL WEB.CONFIG
// SE DESACTIVO PORQUE SE REEMPLAZA POR EL IAUTHPOLICY DONDE SE MANIPULA LOS CLAIMS PARA LAS VALIDACIONES
