using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARELCO1WAREHOUSE_v2._0._1.Filters;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using TARELCO1WAREHOUSE_v2._0._1.Utilities;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class MCRTStockController : Controller
    {
        [SessionTimeout]
        // GET: MCRTStock
        public ActionResult MCRTStock()
        {
            return View();
        }

        public JsonResult MCRTHeadersData()
        {
            MCRTDAO mdao = new MCRTDAO();
            List<MCRTHeaderModel> lstmm = new List<MCRTHeaderModel>();
            lstmm = mdao.GetAllMCRTHeaders();
            var mydata = JsonConvert.SerializeObject(lstmm);
            var jsonResult = Json(new { data = mydata }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}