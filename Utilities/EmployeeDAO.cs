using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class EmployeeDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> lstem = new List<EmployeeModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT emp.[id],[name],isnull([positionid],0) positionid,isnull(pos.Description,'') [position] " +
                                                         "FROM [Employees] emp left join Positions pos " +
                                                         "on emp.PositionId = pos.Id order by [name];", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstem.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(ordr["id"]),
                                Name = ordr["name"].ToString(),
                                PositionId = Convert.ToInt16(ordr["positionid"]),
                                Position = ordr["position"].ToString()
                            });
                        }
                    }
                    else
                        lstem = null;
                }
                catch(Exception ex)
                {
                    lstem = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstem;
        }
    }
}