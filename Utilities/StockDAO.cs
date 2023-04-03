using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class StockDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<StockModel> GetAllStocks()
        {
            List<StockModel> lstsm = new List<StockModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT [Id], UPPER([Code])[Code], UPPER([Description])[Description],[OnHand] " +
                                                         "FROM[dbo].[Stocks] WHERE[IsActive]=1 order by [Code];", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstsm.Add(new StockModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Code = ordr["Code"].ToString(),
                                Description = ordr["Description"].ToString(),
                                Onhand = Convert.ToInt32(ordr["Onhand"])
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

        public List<StockModel> GetAllWithOnHandStocks()
        {
            List<StockModel> lstsm = new List<StockModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT [Id], UPPER([Code])[Code], UPPER([Description])[Description],[OnHand] " +
                                                         "FROM[dbo].[Stocks] WHERE [IsActive]=1 and Onhand>0 order by [Code];", con);
                   
                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstsm.Add(new StockModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Code = ordr["Code"].ToString(),
                                Description = ordr["Description"].ToString(),
                                Onhand = Convert.ToInt32(ordr["Onhand"])
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
