using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
    }
}