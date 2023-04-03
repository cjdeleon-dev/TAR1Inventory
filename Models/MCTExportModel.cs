using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class MCTExportModel
    {
        public int MCTId { get; set; }
        public string PostedDate { get; set; }
        public string PostedBy { get; set; }
        public string IssuedBy { get; set; }
        public string ReceivedBy { get; set; }
        public string CheckedBy { get; set; }
        public string AuditedBy { get; set; }
        public string NotedBy { get; set; }
        public string Code { get; set; }
        public string JOWOMO { get; set; }
        public string JOWOMONumber { get; set; }
        public string Project { get; set; }
        public string ProjectAddress { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double MCTPrice { get; set; }
    }
}