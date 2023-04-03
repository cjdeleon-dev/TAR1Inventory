using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARELCO1WAREHOUSE_v2._0._1.Utilities;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class MenuController : Controller
    {
        //post
        [HttpGet]
        public PartialViewResult LoggedById()
        {
            MenuDAO mdao = new MenuDAO();
            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            try
            {
                var logged = mdao.GetUserLoggedById(loggedid);
                return PartialView(logged);
            }
            catch (Exception)
            {
                return PartialView("Error");   
            }
            
        }
    }
}