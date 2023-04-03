using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class MenuDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();
        
        public UserLoggedModel GetUserLoggedById(int id)
        {
            UserLoggedModel ulm = new UserLoggedModel();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select u.Id, UPPER(FirstName + ' ' + Left(MiddleInitial,1)+'. ' + LastName) [Name], p.Description [Position], r.Code [Role], u.UserPic " +
                                                         "from Users u inner join Positions p on u.PositionId = p.Id inner join Roles r on u.RoleId = r.Id where u.Id = ?", con);

                    ocmd.Parameters.AddWithValue("@id", id);

                    OleDbDataReader ordr = ocmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            ulm.Id = Convert.ToInt32(ordr["Id"]);
                            ulm.Name = ordr["Name"].ToString();
                            ulm.Position = ordr["Position"].ToString();
                            ulm.Role = ordr["Role"].ToString();
                            ulm.UserPic = ordr["UserPic"] != DBNull.Value ? (byte[])ordr["UserPic"] : null;
                            if (ulm.UserPic == null)
                            {
                                MemoryStream ms = new MemoryStream();
                                Image img = Image.FromFile(HostingEnvironment.MapPath("/Content/images/UserDefault.jpg"));
                                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                ulm.UserPic = ms.ToArray();
                            }
                        }
                    }
                    else
                        ulm = null;
                }
                catch (Exception ex)
                {
                    ulm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return ulm;
        }
    }
}