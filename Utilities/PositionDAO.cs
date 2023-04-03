using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class PositionDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<PositionModel> GetAllPositions()
        {
            List<PositionModel> lstpm = new List<PositionModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT[Id],[Code],[Description] " +
                                                         "FROM[wh_dpwh].[dbo].[Positions] WHERE IsActive = 1;", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstpm.Add(new PositionModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Code = ordr["Code"].ToString(),
                                Description = ordr["Description"].ToString(),
                            });
                        }
                    }
                    else
                        lstpm = null;
                }
                catch
                {
                    lstpm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstpm;
        }
    }
}
