using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class RRHeaderModel
    {
        public int Id { get; set; }
        public string ReceivedDate { get; set; }
        public double ReceivedTotalCost { get; set; }
        public int? PreparedById { get; set; } //users table
        public string PreparedBy { get; set; }
        public int? ReceivedById { get; set; }//employees table
        public string ReceivedBy { get; set; }
        public int? CheckedById { get; set; } //employees table
        public string CheckedBy { get; set; }
        public int? NotedById { get; set; } //employees table
        public string NotedBy { get; set; }
        public int? AuditedById { get; set; } //employees table
        public string AuditedBy { get; set; }
        public int? SupplierId { get; set; }
        public string Supplier { get; set; }
        public bool IsOld { get; set; }
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
        public string Remark { get; set; }
        public string DeliveryDate { get; set; }
    }
}
