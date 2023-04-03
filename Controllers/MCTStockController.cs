using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TARELCO1WAREHOUSE_v2._0._1.Filters;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using TARELCO1WAREHOUSE_v2._0._1.Utilities;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class MCTStockController : Controller
    {
        [SessionTimeout]
        // GET: MCTStock
        public ActionResult MCTStock()
        {
            return View();
        }

        public JsonResult MCTHeadersData()
        {
            MCTDAO mdao = new MCTDAO();
            List<MCTHeaderModel> lstmm = new List<MCTHeaderModel>();
            lstmm = mdao.GetAllMCTHeaders();

            JsonResult jsonResult;
            if (lstmm!=null)
            {
                jsonResult = Json(new { data = lstmm }, JsonRequestBehavior.AllowGet);
            }else
                jsonResult = Json(new { data = "[]"}, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult MCTReportView(int id)
        {
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("/Reports"), "rptChargedMaterial.rdlc");
            lr.ReportPath = p;

            DataSet ds = new DataSet();

            MCTDAO mdao = new MCTDAO();
            MCTHeaderModel mhm = mdao.GetAllMCTHeaders().Find(x => x.Id == id);
            DataTable dtHeader = new DataTable();

            dtHeader = MCTHeaderModelToDataTable(mhm);
            ds.Tables.Add(dtHeader);


            //details
            List<MCTDetailModel> lstmdm = new List<MCTDetailModel>();
            lstmdm = mdao.GetAllDetailsByMCTHeaderId(id);
            DataTable dtDetails = new DataTable();

            dtDetails = ListRMDMToDataTable(lstmdm);
            ds.Tables.Add(dtDetails);

            //ReportDataSource for Header
            ReportDataSource cdhdr = new ReportDataSource("dsMCTHeader", ds.Tables["Table1"]);
            //ReportDataSource for Details
            ReportDataSource cddtl = new ReportDataSource("dsMCTDetails", ds.Tables["Table2"]);

            lr.DataSources.Add(cdhdr);//Header
            lr.DataSources.Add(cddtl);//Details

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
            lsts = sdao.GetAllWithOnHandStocks();
            return Json(lsts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmployees()
        {
            EmployeeDAO edao = new EmployeeDAO();
            List<EmployeeModel> lstemp = new List<EmployeeModel>();
            lstemp = edao.GetAllEmployees();
            return Json(lstemp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJOWOMOs()
        {
            JowomoDAO jdao = new JowomoDAO();
            List<JOWOMOModel> lstjm = new List<JOWOMOModel>();
            lstjm = jdao.GetAllJOWOMOs();
            return Json(lstjm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnitAndOnHandByMaterialId(int matid)
        {
            MCTDAO mdao = new MCTDAO();
            return Json(mdao.GetUnitAndOnHandByMaterialId(matid), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddMCTHeader(MCTHeaderModel mcthdr)
        {
            MCTDAO mdao = new MCTDAO();
            return Json(mdao.AddNewMCTHeaderStock(mcthdr), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLoggedUserMaxMMId(int id)
        {
            MCTDAO mdao = new MCTDAO();
            return Json(mdao.GetCurrentMCTHeaderIdByUserId(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddMCTDetail(List<MCTDetailModel> lstmctdm)
        {
            MCTDAO mdao = new MCTDAO();
            return Json(mdao.AddNewMCTDetailStock(lstmctdm), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MCTDataToExcel(FormCollection formCollection)
        {
            ViewBag.ErrMessage = "";

            MCTDAO mdao = new MCTDAO();
            GridView gv = new GridView();

            string resultmsg = string.Empty;
            MCTExportModel mctem = new MCTExportModel();

            try
            {
                string datefr = formCollection["dtpFrom"].ToString();
                string dateto = formCollection["dtpTo"].ToString();

                gv.DataSource = mdao.GetMCTDataByDateRange(datefr, dateto);
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                string datenow = DateTime.Now.ToShortDateString();
                Response.AddHeader("content-disposition", "attachment; filename=MCTStocks_TARELCO1_Warehouse_"
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

            return RedirectToAction("MCTStock", "MCTStock");
        }

        //FUNCTIONS------------------------------------------------------------------------------------

        private DataTable MCTHeaderModelToDataTable(MCTHeaderModel mcthm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(MCTHeaderModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }
            //for rows
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {

                values[i] = Props[i].GetValue(mcthm, null);
            }
            dt.Rows.Add(values);
            return dt;
        }

        private DataTable ListRMDMToDataTable(List<MCTDetailModel> lstcmdm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(MCTDetailModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }

            //for rows
            foreach (MCTDetailModel item in lstcmdm)
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
        //END FUNCTIONS--------------------------------------------------------------------------------
    }
}