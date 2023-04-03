using System;
using System.Collections.Generic;
using System.Data.OleDb;
using TARELCO1WAREHOUSE_v2._0._1.Models;
using System.Configuration;
using SecureLib11;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class UserLoginDAO
    {
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public UserLoginModel GetUserLoginByCredentials(string username, string password)
        {
            UserLoginModel ulm = new UserLoginModel();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select users.id,username,password,positions.description [position],roles.code [rights] " +
                                                         "from users left join positions on users.positionid=positions.id " +
                                                         "inner join roles on users.roleid=roles.id " +
                                                         "where username=? and password=?;", con);

                    Encryptor decryptor = new Encryptor();
                    username = decryptor.ByECode10(username);
                    password = decryptor.ByECode10(password);

                    ocmd.Parameters.AddWithValue("@username", username);
                    ocmd.Parameters.AddWithValue("@password", password);

                    OleDbDataReader ordr = ocmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            ulm.Id = Convert.ToInt32(ordr["id"]);
                            ulm.UserName = username;
                            ulm.Password = password;
                            ulm.Rights = ordr["rights"].ToString();
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