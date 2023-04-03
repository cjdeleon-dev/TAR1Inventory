using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using TARELCO1WAREHOUSE_v2._0._1.Utilities;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserLoginModel ulm)
        {
            UserLoginDAO uld = new UserLoginDAO();
            UserLoginModel ulogged = new UserLoginModel();
            ulogged = uld.GetUserLoginByCredentials(ulm.UserName, ulm.Password);
            if ( ulogged!= null)
            {
                System.Web.HttpContext.Current.Session["Id"] = ulogged.Id;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("CredentialError", "User is not exist or username and password is incorrect.");
                return View("SignIn");
            }
            
        }

        public ActionResult SignOut()
        {
            System.Web.HttpContext.Current.Session.RemoveAll();
            return RedirectToAction("SignIn", "Authentication");
        }
    }
}