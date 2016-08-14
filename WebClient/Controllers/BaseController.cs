using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //bool isAuthorised = context.HttpContext.User.IsInRole(context.RequestContext.HttpContext.User.Identity.IsAuthenticated); 
                if (context.HttpContext.User.IsInRole("Out"))
                {
                    var url = new UrlHelper(context.RequestContext);
                    var logonUrl = url.Action("Index", "Home", new { reason = "youAreAuthorisedButNotAllowedToViewThisPage" });
                    context.Result = new RedirectResult(logonUrl);
                    return;
                }
            }
        }
    }
}