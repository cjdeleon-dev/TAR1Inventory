using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class MCTHeaderModel
    {
        public int Id { get; set; }
        public string PostedDate { get; set; }
        public int PostedById { get; set; }
        public string PostedBy { get; set; }
        public string PosPostedBy { get; set; }
        public int IssuedById { get; set; }
        public string IssuedBy { get; set; }
        public string PosIssuedBy { get; set; }
        public bool IsConsumerReceived { get; set; }
        public int ReceivedById { get; set; }
        public string ReceivedBy { get; set; }
        public string ConsumerReceivedBy { get; set; }
        public string PosReceivedBy { get; set; }
        public int CheckedById { get; set; }
        public string CheckedBy { get; set; }
        public string PosCheckedBy { get; set; }
        public int AuditedById { get; set; }
        public string AuditedBy { get; set; }
        public string PosAuditedBy { get; set; }
        public int NotedById { get; set; }
        public string NotedBy { get; set; }
        public string PosNotedBy { get; set; }
        public string Project { get; set; }
        public string ProjectAddress { get; set; }
        public int JOWOMOId { get; set; }
        public string JOWOMOCode { get; set; }
        public string JOWOMONumber { get; set; }
    }
}