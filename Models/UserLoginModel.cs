using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Rights { get; set; }
    }
}
