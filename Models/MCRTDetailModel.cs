using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class MCRTDetailModel
    {
        public int Id { get; set; }
        public int MCRTId { get; set; }
        public bool IsOld { get; set; }
        public int MCTHeaderId { get; set; }
        public string JOWOMONumber { get; set; }
        public int StockId { get; set; }
        public int MaterialTypeSizeId { get; set; }
        public bool IsSalvage { get; set; }
        public int NoOfYears { get; set; }
        public int ReturnedRateId { get; set; }
        public double UnitCost { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
    }
}