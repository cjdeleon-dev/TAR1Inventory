using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class RRDetailModel
    {
        public int Id { get; set; }
        public int RRHeaderId { get; set; }
        public int MaterialId { get; set; }
        public string Material { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public double InventorialCost { get; set; }
        public double VAT { get; set; }
        public int OnHand { get; set; }
        public int BalanceQty { get; set; }
        public string Remark { get; set; }
    }
}