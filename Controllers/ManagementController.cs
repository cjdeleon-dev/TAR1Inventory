using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TARELCO1WAREHOUSE_v2._0._1.Filters;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using TARELCO1WAREHOUSE_v2._0._1.Utilities;

namespace TARELCO1WAREHOUSE_v2._0._1.Controllers
{
    public class ManagementController : Controller
    {
        [SessionTimeout]
        //USERS Management
        public ActionResult Users()
        {            
            return View();
        }

        public JsonResult UsersData()
        {
            UserDAO udao = new UserDAO();
            List<UserModel> lstum = udao.GetAllUsers();

            return Json(new { data = lstum }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UsersDataToExcel()
        {
            UserDAO udao = new UserDAO();
            GridView gv = new GridView();
       
            gv.DataSource = udao.GetAllUsers();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=Users_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Users");
        }

        //STOCKS Management
        public ActionResult Stocks()
        {
            return View();
        }

        public JsonResult StocksData()
        {
            StockDAO sdao = new StockDAO();
            List<StockModel> lstsm = sdao.GetAllStocks();

            return Json(new { data = lstsm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StocksDataToExcel()
        {
            StockDAO sdao = new StockDAO();
            GridView gv = new GridView();

            gv.DataSource = sdao.GetAllStocks();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=Stocks_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Stocks");
        }

        //SUPPLIERS Management
        public ActionResult Suppliers()
        {
            return View();
        }

        public JsonResult SuppliersData()
        {
            SupplierDAO sdao = new SupplierDAO();
            List<SupplierModel> lstsm = sdao.GetAllSuppliers();

            return Json(new { data = lstsm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SuppliersDataToExcel()
        {
            SupplierDAO sdao = new SupplierDAO();
            GridView gv = new GridView();

            gv.DataSource = sdao.GetAllSuppliers();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=Suppliers_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Suppliers");
        }

        //UNITS Management
        public ActionResult Units()
        {
            return View();
        }

        public JsonResult UnitsData()
        {
            UnitDAO udao = new UnitDAO();
            List<UnitModel> lstum = udao.GetAllUnits();

            return Json(new { data = lstum }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UnitsDataToExcel()
        {
            UnitDAO udao = new UnitDAO();
            GridView gv = new GridView();

            gv.DataSource = udao.GetAllUnits();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=Units_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Units");
        }


        //DEPARTMENTS Management
        public ActionResult Departments()
        {
            return View();
        }

        public JsonResult DepartmentsData()
        {
            DepartmentDAO ddao = new DepartmentDAO();
            List<DepartmentModel> lstdm = ddao.GetAllDepartments();

            return Json(new { data = lstdm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DepartmentsDataToExcel()
        {
            DepartmentDAO ddao = new DepartmentDAO();
            GridView gv = new GridView();

            List<DepartmentModel> lst = ddao.GetAllDepartments();

            gv.DataSource = ddao.GetAllDepartments();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=Departments_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Departments");
        }

        //POSITIONS Management
        public ActionResult Positions()
        {
            return View();
        }

        public JsonResult PositionsData()
        {
            PositionDAO pdao = new PositionDAO();
            List<PositionModel> lstpm = pdao.GetAllPositions();

            return Json(new { data = lstpm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PositionsDataToExcel()
        {
            PositionDAO pdao = new PositionDAO();
            GridView gv = new GridView();

            gv.DataSource = pdao.GetAllPositions();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=Positions_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Positions");
        }

        //JOWOMOS Management
        public ActionResult JOWOMO()
        {
            return View();
        }

        public JsonResult JowomosData()
        {
            JowomoDAO jdao = new JowomoDAO();
            List<JOWOMOModel> lstjm = jdao.GetAllJOWOMOs();

            return Json(new { data = lstjm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult JowomosDataToExcel()
        {
            JowomoDAO jdao = new JowomoDAO();
            GridView gv = new GridView();

            gv.DataSource = jdao.GetAllJOWOMOs();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=JOWOMOs_TARELCO1_Warehouse_" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("JOWOMO");
        }
    }
}