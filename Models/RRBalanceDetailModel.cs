using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class RRBalanceDetailModel
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
    }
}