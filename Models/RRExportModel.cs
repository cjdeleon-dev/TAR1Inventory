using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class RRExportModel
    {
        public int RRId { get; set; }
        public string ReceivedDate { get; set; }
        public string PreparedBy { get; set; }
        public double ReceivedTotalCost { get; set; }
        public string IsOld { get; set; }
        public string Supplier { get; set; }
        public string PO1 { get; set; }
        public string PO2 { get; set; }
        public string PO3 { get; set; }
        public string PO4 { get; set; }
        public string PO5 { get; set; }
        public string SI1 { get; set; }
        public string SI2 { get; set; }
        public string SI3 { get; set; }
        public string SI4 { get; set; }
        public string SI5 { get; set; }
        public string DR1 { get; set; }
        public string DR2 { get; set; }
        public string DR3 { get; set; }
        public string DR4 { get; set; }
        public string DR5 { get; set; }
        public string DeliveryDate { get; set; }
        public string Remark { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public double InventorialCost { get; set; }
        public double VAT { get; set; }
        public int OnHand { get; set; }
        public int BalanceQty { get; set; }
        public string BalanceRemark { get; set; }

    }
}