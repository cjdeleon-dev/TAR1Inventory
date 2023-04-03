using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using System.Configuration;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class DepartmentDAO
    {


        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<DepartmentModel> GetAllDepartments()
        {
            List<DepartmentModel> lstdm = new List<DepartmentModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SELECT [Id], UPPER([Code])[Code], UPPER([Description])[Description] " +
                                                         "FROM [wh_dpwh].[dbo].[Departments] WHERE[IsActive]=1", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstdm.Add(new DepartmentModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Code = ordr["Code"].ToString(),
                                Description = ordr["Description"].ToString()
                            });
                        }
                    }
                    else
                        lstdm = null;
                }
                catch(Exception ex)
                {
                    lstdm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstdm;
        }
    }
}
