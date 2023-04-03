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
    public class RRDAO
    {
        //private string constr = "Provider=sqloledb;Data Source=TAR1CJ\\MSSQLSERVER01;Initial Catalog=wh_dpwh;User Id=sa;Password=sa;";
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<RRHeaderModel> GetAllRRHeaders()
        {
            List<RRHeaderModel> lstpm = new List<RRHeaderModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select rmh.Id,Convert(varchar(10),rmh.ReceivedDate,101)ReceivedDate,rmh.PreparedById," +
                                                         "UPPER(prep.FirstName + ' ' + prep.MiddleInitial + ' ' + prep.LastName)[PreparedBy], " +
                                                         "rmh.ReceivedTotalCost, ISNULL(rmh.ReceivedById, 0)[ReceivedById], UPPER(rec.[Name])ReceivedBy, " +
                                                         "ISNULL(rmh.CheckedById, 0)CheckedById, UPPER(chk.[Name])[CheckedBy],ISNULL(rmh.NotedById, 0)NotedById," +
                                                         "UPPER(note.[Name])[NotedBy], ISNULL(rmh.AuditedById, 0)AuditedById, UPPER(aud.[Name])[AuditedBy], " +
                                                         "rmh.IsOld, ISNULL(rmh.SupplierId, 0)SupplierId, sup.[Name][Supplier], PO1, PO2, PO3, PO4, PO5," +
                                                         "SI1, SI2, SI3, SI4, SI5, DR1, DR2, DR3, DR4, DR5, Convert(varchar(10), rmh.DeliveryDate, 101)DeliveryDate, Remark  " +
                                                         "from ReceivedMaterialHeaders rmh " +
                                                         "inner join Users prep on rmh.PreparedById = prep.Id " +
                                                         "left join Employees rec on rmh.ReceivedById=rec.Id " +
                                                         "left join Employees chk on rmh.CheckedById = chk.Id " +
                                                         "left join Employees note on rmh.NotedById = note.Id " +
                                                         "left join Employees aud on rmh.AuditedById = aud.Id " +
                                                         "left join Suppliers sup on rmh.SupplierId = sup.Id " +
                                                         "order by ReceivedDate desc", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstpm.Add(new RRHeaderModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                ReceivedDate = ordr["ReceivedDate"].ToString(),
                                PreparedById = Convert.ToInt32(ordr["PreparedById"]),
                                PreparedBy = ordr["PreparedBy"].ToString(),
                                ReceivedTotalCost = Convert.ToDouble(ordr["ReceivedTotalCost"]),
                                ReceivedById = Convert.ToInt32(ordr["ReceivedById"]),
                                ReceivedBy = ordr["ReceivedBy"].ToString(),
                                CheckedById = Convert.ToInt32(ordr["CheckedById"]),
                                CheckedBy = ordr["CheckedBy"].ToString(),
                                NotedById = Convert.ToInt32(ordr["NotedById"]),
                                NotedBy = ordr["NotedBy"].ToString(),
                                AuditedById = Convert.ToInt32(ordr["AuditedById"]),
                                AuditedBy = ordr["AuditedBy"].ToString(),
                                IsOld = Convert.ToBoolean(ordr["IsOld"]),
                                SupplierId = Convert.ToInt32(ordr["SupplierId"]),
                                Supplier = ordr["Supplier"].ToString(),
                                DeliveryDate = ordr["DeliveryDate"].ToString(),
                                Remark = ordr["Remark"].ToString(),
                                PO1 = ordr["PO1"].ToString(),
                                PO2 = ordr["PO2"].ToString(),
                                PO3 = ordr["PO3"].ToString(),
                                PO4 = ordr["PO4"].ToString(),
                                PO5 = ordr["PO5"].ToString(),
                                SI1 = ordr["SI1"].ToString(),
                                SI2 = ordr["SI2"].ToString(),
                                SI3 = ordr["SI3"].ToString(),
                                SI4 = ordr["SI4"].ToString(),
                                SI5 = ordr["SI5"].ToString(),
                                DR1 = ordr["DR1"].ToString(),
                                DR2 = ordr["DR2"].ToString(),
                                DR3 = ordr["DR3"].ToString(),
                                DR4 = ordr["DR4"].ToString(),
                                DR5 = ordr["DR5"].ToString()
                            });
                        }
                    }
                    else
                        lstpm = null;
                }
                catch(Exception ex)
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

        public List<RRDetailModel> GetAllDetailsByRRHeaderId(int hdrId)
        {
            List<RRDetailModel> lstdtl = new List<RRDetailModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select dtl.id, recievedmaterialheaderid, materialid,mat.Description [material],dtl.quantity ,dtl.unitid,unt.code [unit],unitcost,totalcost,inventorialcost,vat,dtl.onhand " +
                                                         "from receivedmaterialdetails dtl inner join stocks mat on dtl.materialid=mat.id " +
                                                         "inner join units unt on dtl.unitid=unt.id " +
                                                         "where dtl.recievedmaterialheaderid=?;", con);

                    ocmd.Parameters.AddWithValue("@hdrid", hdrId);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstdtl.Add(new RRDetailModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                RRHeaderId = Convert.ToInt32(ordr["recievedmaterialheaderid"]),
                                MaterialId = Convert.ToInt32(ordr["materialid"]),
                                Material = ordr["material"].ToString(),
                                Quantity = Convert.ToInt32(ordr["quantity"]),
                                UnitId = Convert.ToInt32(ordr["unitid"]),
                                Unit = ordr["unit"].ToString(),
                                UnitCost = Convert.ToDouble(ordr["unitcost"]),
                                TotalCost = Convert.ToDouble(ordr["totalcost"]),
                                InventorialCost = Convert.ToDouble(ordr["inventorialcost"]),
                                VAT = Convert.ToDouble(ordr["vat"]),
                                OnHand = Convert.ToInt32(ordr["onhand"])
                            });

                        }
                    }
                    else
                        lstdtl = null;
                }
                catch (Exception ex)
                {
                    lstdtl = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstdtl;
        }

        public List<RRBalanceDetailModel> GetAllBalanceDetailByRRHeaderId(int hdrId)
        {
            List<RRBalanceDetailModel> lstrrbdm = new List<RRBalanceDetailModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select rmbd.Id,RecievedMaterialHeaderId,rmbd.MaterialId,mat.Description [Material],BalanceQty [Quantity],UnitId,units.Code [Unit],Remark " +
                                                         "from ReceivedMaterialDetails rmbd inner join stocks mat " +
                                                         "on rmbd.MaterialId=mat.Id " +
                                                         "inner join units on rmbd.UnitId=units.Id " +
                                                         "where RecievedMaterialHeaderId=? and (BalanceQty>0 or rtrim(remark)<>'');", con);

                    ocmd.Parameters.AddWithValue("@hdrid", hdrId);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstrrbdm.Add(new RRBalanceDetailModel
                            {
                                Id = Convert.ToInt32(ordr["Id"]),
                                Material = ordr["material"].ToString(),
                                Quantity= Convert.ToInt32(ordr["Quantity"]),
                                Unit=ordr["unit"].ToString(),
                                Remark=ordr["remark"].ToString()
                            });

                        }
                    }
                    else
                        lstrrbdm = null;
                }
                catch (Exception ex)
                {
                    lstrrbdm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstrrbdm;
        }

        public ProcessModel AddNewRRHeaderStock(RRHeaderModel rrhm)
        {
            ProcessModel pm = new ProcessModel();

            using (OleDbCommand cmd = new OleDbCommand())
            {
                OleDbConnection con = new OleDbConnection(constr);
                OleDbTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into receivedmaterialheaders(receiveddate,preparedbyid,receivedtotalcost," +
                                      "receivedbyid,checkedbyid,notedbyid,auditedbyid,isold," +
                                      "supplierid,po1,po2,po3,po4,po5,si1,si2,si3,si4,si5," +
                                      "dr1,dr2,dr3,dr4,dr5,remark,deliverydate,entrydatetime) " +
                                      "values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,getdate());";
                    //@receiveddate,@receivedbyid,@receivedtotalcost

                    cmd.Parameters.AddWithValue("@receiveddate", rrhm.ReceivedDate);
                    cmd.Parameters.AddWithValue("@preparedbyid", rrhm.PreparedById);
                    cmd.Parameters.AddWithValue("@receivedtotalcost", rrhm.ReceivedTotalCost);
                    cmd.Parameters.AddWithValue("@receivedbyid", rrhm.ReceivedById);
                    cmd.Parameters.AddWithValue("@checkedbyid", rrhm.CheckedById);
                    cmd.Parameters.AddWithValue("@notedbyid", rrhm.NotedById);
                    cmd.Parameters.AddWithValue("@auditedbyid", rrhm.AuditedById);
                    cmd.Parameters.AddWithValue("@isold", rrhm.IsOld);
                    cmd.Parameters.AddWithValue("@supplierid", rrhm.SupplierId == null ? 0 : rrhm.SupplierId);
                    cmd.Parameters.AddWithValue("@po1", rrhm.PO1 == null ? "" : rrhm.PO1);
                    cmd.Parameters.AddWithValue("@po2", rrhm.PO2 == null ? "" : rrhm.PO2);
                    cmd.Parameters.AddWithValue("@po3", rrhm.PO3 == null ? "" : rrhm.PO3);
                    cmd.Parameters.AddWithValue("@po4", rrhm.PO4 == null ? "" : rrhm.PO4);
                    cmd.Parameters.AddWithValue("@po5", rrhm.PO5 == null ? "" : rrhm.PO5);
                    cmd.Parameters.AddWithValue("@si1", rrhm.SI1 == null ? "" : rrhm.SI1);
                    cmd.Parameters.AddWithValue("@si2", rrhm.SI2 == null ? "" : rrhm.SI2);
                    cmd.Parameters.AddWithValue("@si3", rrhm.SI3 == null ? "" : rrhm.SI3);
                    cmd.Parameters.AddWithValue("@si4", rrhm.SI4 == null ? "" : rrhm.SI4);
                    cmd.Parameters.AddWithValue("@si5", rrhm.SI5 == null ? "" : rrhm.SI5);
                    cmd.Parameters.AddWithValue("@dr1", rrhm.DR1 == null ? "" : rrhm.DR1);
                    cmd.Parameters.AddWithValue("@dr2", rrhm.DR2 == null ? "" : rrhm.DR2);
                    cmd.Parameters.AddWithValue("@dr3", rrhm.DR3 == null ? "" : rrhm.DR3);
                    cmd.Parameters.AddWithValue("@dr4", rrhm.DR4 == null ? "" : rrhm.DR4);
                    cmd.Parameters.AddWithValue("@dr5", rrhm.DR5 == null ? "" : rrhm.DR5);
                    cmd.Parameters.AddWithValue("@remark", rrhm.Remark == null ? "" : rrhm.Remark);
                    cmd.Parameters.AddWithValue("@deliverydate", rrhm.DeliveryDate);

                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pm.IsSuccess = true;
                    pm.ProcessMessage = "Received Material Header Saved.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    pm.IsSuccess = false;
                    pm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }


            }

            return pm;
        
}

        public ProcessModel AddNewRRDetailStock(List<RRDetailModel> lstrrdm)
        {
            ProcessModel pm = new ProcessModel();

            if (lstrrdm != null)
            {
                OleDbConnection con = new OleDbConnection(constr);
                OleDbTransaction trans = null;

                con.Open();
                trans = con.BeginTransaction();

                foreach (RRDetailModel rrdm in lstrrdm)
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        try
                        {
                            cmd.Connection = con;
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "insert into ReceivedMaterialDetails(RecievedMaterialHeaderId,MaterialId,Quantity,UnitId,UnitCost,TotalCost,InventorialCost,VAT,OnHand,BalanceQty,Remark) " +
                                              "values(?,?,?,?,?,?,?,?,?,?,?); " +
                                              "update stocks set OnHand=OnHand+? where id=?;" +
                                              "update ReceivedMaterialHeaders " +
                                              "set ReceivedTotalCost = (select sum(totalcost) from ReceivedMaterialDetails where RecievedMaterialHeaderId =?) " +
                                              "where id =?; ";
                            //@RecievedMaterialHeaderId,@MaterialId,@Quantity,@UnitId,@UnitCost,@OnHand


                            string rem = string.Empty;

                            if (rrdm.Remark == null)
                            {
                                rem = "";
                            }
                            else
                            {
                                rem = rrdm.Remark;
                            }

                            //getting Inventorial Cost
                            rrdm.InventorialCost = (rrdm.TotalCost / 1.12);
                            //getting VAT
                            rrdm.VAT = (rrdm.InventorialCost * 0.12);
                            //getting Unit Cost
                            rrdm.UnitCost = (rrdm.InventorialCost / rrdm.Quantity);

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@RecievedMaterialHeaderId", rrdm.RRHeaderId);
                            cmd.Parameters.AddWithValue("@MaterialId", rrdm.MaterialId);
                            cmd.Parameters.AddWithValue("@Quantity", rrdm.Quantity);
                            cmd.Parameters.AddWithValue("@UnitId", rrdm.UnitId);
                            cmd.Parameters.AddWithValue("@UnitCost", rrdm.UnitCost);
                            cmd.Parameters.AddWithValue("@TotalCost", rrdm.TotalCost);
                            cmd.Parameters.AddWithValue("@InventorialCost", rrdm.InventorialCost);
                            cmd.Parameters.AddWithValue("@VAT", rrdm.VAT);
                            cmd.Parameters.AddWithValue("@OnHand", rrdm.OnHand);
                            cmd.Parameters.AddWithValue("@BalanceQty", rrdm.BalanceQty);
                            cmd.Parameters.AddWithValue("@Remark", rem);
                            cmd.Parameters.AddWithValue("@Quantity", rrdm.Quantity);
                            cmd.Parameters.AddWithValue("@MaterialId", rrdm.MaterialId);
                            cmd.Parameters.AddWithValue("@RecievedMaterialHeaderId", rrdm.RRHeaderId);
                            cmd.Parameters.AddWithValue("@RecievedMaterialHeaderId2", rrdm.RRHeaderId);

                            cmd.ExecuteNonQuery();

                            pm.IsSuccess = true;
                        }
                        catch (Exception ex)
                        {
                            pm.IsSuccess = false;
                            pm.ProcessMessage = "An error occured: \n" + ex.Message;
                        }
                    }
                }

                if (!pm.IsSuccess)
                {
                    trans.Rollback();
                }
                else
                {
                    pm.ProcessMessage = "RR Details Saved.";
                    trans.Commit();
                    con.Close();
                    con.Dispose();
                }
            }
            return pm;
        }

        public RRHeaderModel GetCurrentRRHeaderIdByUserId(int id)
        {
            RRHeaderModel rrhm = new RRHeaderModel();

            using (OleDbDataAdapter da = new OleDbDataAdapter())
            {
                da.SelectCommand = new OleDbCommand();
                da.SelectCommand.Connection = new OleDbConnection(constr);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select max(Id) Id from receivedmaterialheaders where preparedbyid=?;"; ;

                da.SelectCommand.Parameters.AddWithValue("@preparedbyid", id);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rrhm.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        rrhm.PreparedById = 0;
                        rrhm.ReceivedDate = string.Empty;
                        rrhm.ReceivedTotalCost = 0;
                    }
                    else
                    {
                        rrhm.Id = 0;
                        rrhm.PreparedById = 0;
                        rrhm.ReceivedDate = string.Empty;
                        rrhm.ReceivedTotalCost = 0;
                    }
                }
                catch (Exception ex)
                {
                    rrhm = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return rrhm;
        }

        public List<RRExportModel> GetRRDataByDateRange(string dateFrom, string dateTo)
        {
            List<RRExportModel> lstrrem = new List<RRExportModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select rmh.Id,Convert(varchar(10),rmh.ReceivedDate,101)ReceivedDate, UPPER(usrprepared.FirstName + ' ' + " +
                                                         "usrprepared.MiddleInitial + ' ' + usrprepared.LastName) [PreparedBy]," +
                                                         "rmh.ReceivedTotalCost, ISNULL(IsOld,'False') [IsOld], ISNULL(sup.Name,'')[Supplier], " +
                                                         "ISNULL(PO1,'')[PO1], ISNULL(PO2,'')[PO2], ISNULL(PO3,'')[PO3], ISNULL(PO4,'')[PO4], ISNULL(PO5,'')[PO5]," +
                                                         "ISNULL(SI1,'')[SI1], ISNULL(SI2,'')[SI2], ISNULL(SI3,'')[SI3], ISNULL(SI4,'')[SI4], ISNULL(SI5,'')[SI5]," +
                                                         "ISNULL(DR1,'')[DR1], ISNULL(DR2,'')[DR2], ISNULL(DR3,'')[DR3], ISNULL(DR4,'')[DR4], ISNULL(DR5,'')[DR5]," +
                                                         "ISNULL(Convert(varchar(10),DeliveryDate,101),'')[DeliveryDate],ISNULL(rmh.Remark,'')[Remark], ISNULL(mat.Code,'')[StockCode]," +
                                                         "ISNULL(mat.Description,'')[Description], ISNULL(Quantity,0)[Quantity], ISNULL(unt.Code,'') [UnitCode]," +
                                                         "ISNULL(UnitCost,0) [UnitCost], ISNULL(TotalCost,0)[TotalCost], ISNULL(InventorialCost,0)[InventorialCost]," +
                                                         "ISNULL(VAT,0)[VAT], ISNULL(rmd.OnHand,0)[OnHand], ISNULL(BalanceQty,0) [BalanceQty], " +
                                                         "ISNULL(rmd.Remark,'') [BalanceRemark] " +
                                                         "from ReceivedMaterialHeaders rmh " +
                                                         "inner join ReceivedMaterialDetails rmd " +
                                                         "on rmh.id = rmd.RecievedMaterialHeaderId " +
                                                         "inner join Users usrprepared " +
                                                         "on rmh.PreparedById = usrprepared.Id " +
                                                         "left join Suppliers sup " +
                                                         "on rmh.SupplierId = sup.Id " +
                                                         "inner join Stocks mat " +
                                                         "on rmd.MaterialId = mat.Id " +
                                                         "inner join Units unt " +
                                                         "on rmd.UnitId = unt.Id " +
                                                         "where ReceivedDate between ? and ?", con);

                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@datefrom", dateFrom);
                    ocmd.Parameters.AddWithValue("@dateto", dateTo);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstrrem.Add(new RRExportModel
                            {
                                RRId = Convert.ToInt32(ordr["Id"]),
                                ReceivedDate = ordr["ReceivedDate"].ToString(),
                                PreparedBy = ordr["PreparedBy"].ToString(),
                                ReceivedTotalCost = Convert.ToDouble(ordr["ReceivedTotalCost"]),
                                IsOld = ordr["IsOld"].ToString(),
                                Supplier = ordr["Supplier"].ToString(),
                                PO1 = ordr["PO1"].ToString(),
                                PO2 = ordr["PO2"].ToString(),
                                PO3 = ordr["PO3"].ToString(),
                                PO4 = ordr["PO4"].ToString(),
                                PO5 = ordr["PO5"].ToString(),
                                SI1 = ordr["SI1"].ToString(),
                                SI2 = ordr["SI2"].ToString(),
                                SI3 = ordr["SI3"].ToString(),
                                SI4 = ordr["SI4"].ToString(),
                                SI5 = ordr["SI5"].ToString(),
                                DR1 = ordr["DR1"].ToString(),
                                DR2 = ordr["DR2"].ToString(),
                                DR3 = ordr["DR3"].ToString(),
                                DR4 = ordr["DR4"].ToString(),
                                DR5 = ordr["DR5"].ToString(),
                                DeliveryDate = ordr["DeliveryDate"].ToString(),
                                Remark = ordr["Remark"].ToString(),
                                StockCode = ordr["StockCode"].ToString(),
                                Description = ordr["Description"].ToString(),
                                Quantity = Convert.ToInt32(ordr["Quantity"]),
                                UnitCode = ordr["UnitCode"].ToString(),
                                UnitCost = Convert.ToDouble(ordr["UnitCost"]),
                                TotalCost = Convert.ToDouble(ordr["TotalCost"]),
                                InventorialCost = Convert.ToDouble(ordr["InventorialCost"]),
                                VAT = Convert.ToDouble(ordr["VAT"]),
                                OnHand = Convert.ToInt32(ordr["OnHand"]),
                                BalanceQty = Convert.ToInt32(ordr["BalanceQty"]),
                                BalanceRemark = ordr["BalanceRemark"].ToString()
                            });
                        }
                    }
                    else
                        lstrrem = null;
                }
                catch
                {
                    lstrrem = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstrrem;
        }
    }
}
