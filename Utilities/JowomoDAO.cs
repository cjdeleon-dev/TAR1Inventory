using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class JowomoDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<JOWOMOModel> GetAllJOWOMOs()
        {
            List<JOWOMOModel> lstjm = new List<JOWOMOModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT[Id],[Code],[Description],[DR_Account] " +
                                                         "FROM[wh_dpwh].[dbo].[JOWOMO] WHERE IsActive = 1;", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstjm.Add(new JOWOMOModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Code = ordr["Code"].ToString(),
                                Description = ordr["Description"].ToString(),
                                Draccount = ordr["DR_Account"].ToString()
                            });
                        }
                    }
                    else
                        lstjm = null;
                }
                catch
                {
                    lstjm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstjm;
        }
    }
}
