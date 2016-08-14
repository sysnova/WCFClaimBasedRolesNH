using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using System.Threading;
//
using sysnova.Domain.Forms;
using sysnova.Domain.Interfaces;
using sysnova.Domain.Entities;

namespace WebClient.Controllers
{
    public class SecurityController : BaseController
    {
        // GET: Security
        public ActionResult Index()
        {
            var ident = new ClaimsIdentity(
                  new[] { 
                      // adding following 2 claim just for supporting default antiforgery provider
                      new Claim(ClaimTypes.NameIdentifier, "technisys"),
                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                      new Claim(ClaimTypes.Name,"technisys"),
                      // optionally you could add roles if any
                      new Claim(ClaimTypes.Role, "RoleName"),
                      new Claim(ClaimTypes.Role, "AnotherRole"),
                  },
                    DefaultAuthenticationTypes.ApplicationCookie);

            var claimsPrincipal = new ClaimsPrincipal(ident);
            Thread.CurrentPrincipal = claimsPrincipal;

            HttpContext.GetOwinContext().Authentication.SignIn(
               new AuthenticationProperties { IsPersistent = false }, ident);
            /*
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "surname", "Smith"
                    },
                    { 
                        "age", "20"
                    },
                    { 
                        "gender", "Male"
                    }
                });
            */
            //HttpContext.GetOwinContext().Authentication.SignIn(props, ident);

            return RedirectToAction("Details/5");
        }

        // GET: Security/Details/5
        [Authorize(Roles = "RoleName")]
        public ActionResult Details(int id)
        {
            //El FACTORY lo cree al pedo, porque el CREATE del objeto del tipo Issues se instancia en cada View Model
            //Cloud formulario = (Cloud)FormsFactory.Create<FormCloud>().GetInstance();

            //Implementar la forma que se determina el tipo de formulario a crear y redirigir al controller correspondiente
            //Dentro de cada controller se instancia el Model. El ISSUE es la clase madre y heredan los Forms del tipo CLOUD y HELPDESK
            return RedirectToAction("../Cloud/Index/");

        }

        // GET: Security/Create
        [Authorize(Roles = "AnotherRole1")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return View();
        }

        // POST: Security/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
