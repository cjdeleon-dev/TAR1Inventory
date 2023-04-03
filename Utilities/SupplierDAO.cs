using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class SupplierDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<SupplierModel> GetAllSuppliers()
        {
            List<SupplierModel> lstsm = new List<SupplierModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT [Id], upper([Name])[Name], upper([Address]) [Address] " +
                                                         "FROM [wh_dpwh].[dbo].[Suppliers] WHERE IsActive=1", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstsm.Add(new SupplierModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Name = ordr["Name"].ToString(),
                                Address = ordr["Address"].ToString(),
                            });
                        }
                    }
                    else
                        lstsm = null;
                }
                catch
                {
                    lstsm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstsm;
        }
    }
}
