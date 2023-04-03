using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class MCRTDAO
    {
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<MCRTHeaderModel> GetAllMCRTHeaders()
        {
            List<MCRTHeaderModel> lstmcrthm = new List<MCRTHeaderModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select hdr.Id,hdr.ReturnedDate,ISNULL(hdr.ReturnedById,0)[ReturnedById],IsConsumer," +
                                                         "case when hdr.IsConsumer = 1 then hdr.ConsumerName else ret.[Name] end AS[ReturnedBy]," +
                                                         "PostedById, UPPER(usr.[FirstName] + ' ' + REPLACE(usr.[MiddleInitial], '.', '') + '. ' + usr.[LastName])[PostedBy], Remarks " +
                                                         "from MCRTHeaders hdr inner join Users usr " +
                                                         "on hdr.PostedById = usr.Id " +
                                                         "inner join Employees ret " +
                                                         "on hdr.ReturnedById = ret.Id " +
                                                         "order by hdr.Id", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstmcrthm.Add(new MCRTHeaderModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                ReturnDate = ordr["ReturnedDate"].ToString(),
                                IsConsumer = Convert.ToBoolean(ordr["IsConsumer"]),
                                ReturnedById = Convert.ToInt32(ordr["ReturnedById"]),
                                ReturnedBy = ordr["ReturnedBy"].ToString(),
                                PostedById = Convert.ToInt32(ordr["PostedById"]),
                                PostedBy = ordr["PostedBy"].ToString(),
                                Remarks = ordr["Remarks"].ToString(),
                            });
                        }
                    }
                    else
                        lstmcrthm = null;
                }
                catch (Exception ex)
                {
                    lstmcrthm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstmcrthm;
        }
    }
}