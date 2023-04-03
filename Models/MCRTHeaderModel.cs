using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class MCRTHeaderModel
    {
        public int Id { get; set; }
        public string ReturnDate { get; set; }
        public bool IsConsumer { get; set; }
        public int ReturnedById { get; set; }
        public string ReturnedBy { get; set; }
        public int PostedById { get; set; }
        public string PostedBy { get; set; }
        public string Remarks { get; set; }
    }
}