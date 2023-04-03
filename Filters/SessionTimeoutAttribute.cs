using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TARELCO1WAREHOUSE_v2._0._1.Filters
{
    public class SessionTimeoutAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["Id"] == null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/SignIn");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}