using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARELCO1WAREHOUSE_v2._0._1.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
    }
}
