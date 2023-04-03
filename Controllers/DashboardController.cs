using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TARELCO1WAREHOUSE_v2._0._1.Filters;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class DashboardController : Controller
    {
        [SessionTimeout]
        public ActionResult Index()
        {
            return View();
        }
    }
}