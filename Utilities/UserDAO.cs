using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class UserDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> lstum = new List<UserModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT u.[Id],upper([FirstName])[FirstName],upper([MiddleInitial])[MiddleInitial],upper([LastName])[LastName]," +
                                                         "[PositionId], upper(p.[Description])[Position], upper([Address])[Address],[UserName],[Password],[RoleId], upper(r.[Role])[Role] " +
                                                         "FROM[dbo].[Users] u " +
                                                         "inner join Positions p " +
                                                         "on u.PositionId=p.Id " +
                                                         "inner join Roles r " +
                                                         "on u.RoleId=r.Id " +
                                                         "WHERE u.IsActive=1;", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstum.Add(new UserModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                FirstName = ordr["FirstName"].ToString(),
                                MiddleInitial = ordr["MiddleInitial"].ToString(),
                                LastName = ordr["LastName"].ToString(),
                                PositionId = Convert.ToInt32(ordr["PositionId"]),
                                Position = ordr["Position"].ToString(),
                                Address = ordr["Address"].ToString(),
                                RoleId = Convert.ToInt32(ordr["RoleId"]),
                                Role = ordr["Role"].ToString()
                            });
                        }
                    }
                    else
                        lstum = null;
                }
                catch(Exception ex)
                {
                    lstum = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstum;
        }

    }
}
