using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TARELCO1WAREHOUSE_v2._0._1.Filters;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using TARELCO1WAREHOUSE_v2._0._1.Utilities;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class RRStockController : Controller
    {
        [SessionTimeout]
        public ActionResult RRStock()
        {

            return View();
        }

        public JsonResult RRHeadersData()
        {
            RRDAO rdao = new RRDAO();
            List<RRHeaderModel> lstrm = new List<RRHeaderModel>();
            lstrm = rdao.GetAllRRHeaders();
            var jsonResult = Json(new { data = lstrm }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult RRReportView(int id)
        {
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("/Reports"), "rptReceivedMaterial.rdlc");
            lr.ReportPath = p;

            DataSet ds = new DataSet();

            ////header
            RRDAO rdao = new RRDAO();
            RRHeaderModel rrhm = rdao.GetAllRRHeaders().Find(x => x.Id.Equals(id));

            DataTable dtHeader = new DataTable();

            dtHeader = RRHeaderModelToDataTable(rrhm);
            ds.Tables.Add(dtHeader);

            ////details
            List<RRDetailModel> lstrrdtl = new List<RRDetailModel>();
            lstrrdtl = rdao.GetAllDetailsByRRHeaderId(id);

            DataTable dtDetails = new DataTable();

            dtDetails = ListRRDetailModelToDataTable(lstrrdtl);
            ds.Tables.Add(dtDetails);

            ////balance
            List<RRBalanceDetailModel> lstrrbdtl = new List<RRBalanceDetailModel>();
            lstrrbdtl = rdao.GetAllBalanceDetailByRRHeaderId(id);

            DataTable dtNotes = new DataTable();

            dtNotes = ListRRBalanceDetailModelToDataTable(lstrrbdtl);
            ds.Tables.Add(dtNotes);

            //ReportDataSource for Header
            ReportDataSource rdhdr = new ReportDataSource("dsRRStockHeader", ds.Tables["Table1"]);
            //ReportDataSource for Details
            ReportDataSource rddtl = new ReportDataSource("dsRRStockDetails", ds.Tables["Table2"]);
            //ReportDataSource for Notes
            ReportDataSource rdnotes = new ReportDataSource("dsRRStockNote", ds.Tables["Table3"]);

            lr.DataSources.Add(rdhdr);//Header
            lr.DataSources.Add(rddtl);//Details
            lr.DataSources.Add(rdnotes);//Material Balances

            string mt, enc, f;
            string[] s;
            Warning[] w;

            //Rendering
            byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);

            return File(b, mt);
        }

        [HttpGet]
        public JsonResult GetMaterials()
        {
            StockDAO sdao = new StockDAO();
            List<StockModel> lsts = new List<StockModel>();
            lsts = sdao.GetAllStocks();
            return Json(lsts,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSuppliers()
        {
            SupplierDAO sdao = new SupplierDAO();
            List<SupplierModel> lsts = new List<SupplierModel>();
            lsts = sdao.GetAllSuppliers();
            return Json(lsts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUnits()
        {
            UnitDAO udao = new UnitDAO();
            List<UnitModel> lstu = new List<UnitModel>();
            lstu = udao.GetAllUnits();
            return Json(lstu, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmployees()
        {
            EmployeeDAO edao = new EmployeeDAO();
            List<EmployeeModel> lstemp = new List<EmployeeModel>();
            lstemp = edao.GetAllEmployees();
            return Json(lstemp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReceivedHeader(RRHeaderModel rrhdr)
        {
            RRDAO rrd = new RRDAO();
            return Json(rrd.AddNewRRHeaderStock(rrhdr), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLoggedUserMaxRMId(int id)
        {
            RRDAO rrd = new RRDAO();
            return Json(rrd.GetCurrentRRHeaderIdByUserId(id),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReceiveMaterialDetail(List<RRDetailModel> lstrrdm)
        {
            RRDAO rrd = new RRDAO();
            return Json(rrd.AddNewRRDetailStock(lstrrdm), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RRDataToExcel(FormCollection formCollection)
        {
            ViewBag.ErrMessage = "";

            RRDAO rrdao = new RRDAO();
            GridView gv = new GridView();

            string resultmsg = string.Empty;
            RRExportModel rrm = new RRExportModel();

            try
            {
                string datefr = formCollection["dtpFrom"].ToString();
                string dateto = formCollection["dtpTo"].ToString();

                gv.DataSource = rrdao.GetRRDataByDateRange(datefr, dateto);
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                string datenow = DateTime.Now.ToShortDateString();
                Response.AddHeader("content-disposition", "attachment; filename=RRStocks_TARELCO1_Warehouse_"
                                   + datefr.Replace("-", string.Empty) + "TO" + dateto.Replace("-", string.Empty) + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();

            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("RRStock","RRStock");
        }

        //FUNCTIONS------------------------------------------------------------------------------------

        private DataTable RRHeaderModelToDataTable(RRHeaderModel rrhm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(RRHeaderModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }
            //for rows
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {

                values[i] = Props[i].GetValue(rrhm, null);
            }
            dt.Rows.Add(values);
            return dt;
        }

        private DataTable ListRRDetailModelToDataTable(List<RRDetailModel> lstrrdm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(RRDetailModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }

            //for rows
            foreach (RRDetailModel item in lstrrdm)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }

            return dt;
        }

        private DataTable ListRRBalanceDetailModelToDataTable(List<RRBalanceDetailModel> lstrrbdm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(RRBalanceDetailModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }

            //for rows
            if (lstrrbdm != null)
            {
                foreach (RRBalanceDetailModel item in lstrrbdm)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dt.Rows.Add(values);
                }
            }
            return dt;
        }

        //END FUNCTIONS--------------------------------------------------------------------------------
    }
}