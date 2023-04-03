using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TARELCO1WAREHOUSE_v2._0._1.Models;

namespace TARELCO1WAREHOUSE_v2._0._1.Utilities
{
    public class MCTDAO
    {
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public List<MCTHeaderModel> GetAllMCTHeaders()
        {
            List<MCTHeaderModel> lstmcthm = new List<MCTHeaderModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select hdr.Id, convert(varchar(10),hdr.PostedDate,101)PostedDate, hdr.PostedById, UPPER(usr.FirstName + ' ' + usr.MiddleInitial + ' ' + usr.LastName) [PostedBy],     " +
                                                         "posPosted.[Description] [PosPostedBy], hdr.IssuedById, empIssued.[Name] [IssuedBy], posIssued.[Description] [PosIssuedBy]," +
                                                         "hdr.ReceivedById, empReceived.[Name] [ReceivedBy], posReceived.[Description] [PosReceivedBy]," +
                                                         "hdr.CheckedById, empChecked.[Name] [CheckedBy], posChecked.[Description] [PosCheckedBy]," +
                                                         "hdr.AuditedById, empAudited.[Name] [AuditedBy], posAudited.[Description] [PosAuditedBy]," +
                                                         "hdr.NotedById, empNoted.[Name] [NotedBy], posNoted.[Description] [PosNotedBy]," +
                                                         "hdr.JOWOMOId,jwm.Code,hdr.JOWOMONumber,hdr.Project,hdr.ProjectAddress,isconsumerreceived,receivedconsumer " +
                                                         "from ChargedMaterialHeaders hdr " +
                                                         "left join Users usr " +
                                                         "on hdr.PostedById=usr.Id " +
                                                         "left join Positions posPosted " +
                                                         "on usr.PositionId=posPosted.Id " +
                                                         "left join Employees empIssued " +
                                                         "on hdr.IssuedById=empIssued.Id " +
                                                         "left join Positions posIssued " +
                                                         "on empIssued.PositionId=posIssued.Id " +
                                                         "left join Employees empReceived " +
                                                         "on hdr.ReceivedById=empReceived.Id " +
                                                         "left join Positions posReceived " +
                                                         "on empReceived.PositionId=posReceived.Id " +
                                                         "left join Employees empChecked " +
                                                         "on hdr.CheckedById=empChecked.Id " +
                                                         "left join Positions posChecked " +
                                                         "on empChecked.PositionId=posChecked.Id " +
                                                         "left join Employees empAudited " +
                                                         "on hdr.AuditedById=empAudited.Id " +
                                                         "left join Positions posAudited " +
                                                         "on empAudited.PositionId=posAudited.Id " +
                                                         "left join Employees empNoted " +
                                                         "on hdr.NotedById=empNoted.Id " +
                                                         "left join Positions posNoted " +
                                                         "on empNoted.PositionId=posNoted.Id " +
                                                         "left  join JOWOMO jwm " +
                                                         "on hdr.JOWOMOId=jwm.Id Order by Id DESC;", con);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstmcthm.Add(new MCTHeaderModel
                            {
                                Id = Convert.ToInt32(ordr["id"]),
                                PostedDate = ordr["posteddate"].ToString(),
                                PostedById = Convert.ToInt32(ordr["postedbyid"]),
                                PostedBy = ordr["postedby"].ToString(),
                                PosPostedBy = ordr["pospostedby"].ToString(),
                                IssuedById = Convert.ToInt32(ordr["issuedbyid"]),
                                IssuedBy = ordr["issuedby"].ToString(),
                                PosIssuedBy = ordr["posissuedby"].ToString(),
                                IsConsumerReceived = Convert.ToBoolean(ordr["isconsumerreceived"]),
                                ReceivedById = ordr["receivedbyid"] == DBNull.Value ? 0 : Convert.ToInt32(ordr["receivedbyid"]),
                                ReceivedBy = ordr["receivedby"] == DBNull.Value ? ordr["receivedconsumer"].ToString() : ordr["receivedby"].ToString(),
                                ConsumerReceivedBy = ordr["receivedconsumer"] == DBNull.Value ? null : ordr["receivedconsumer"].ToString(),
                                PosReceivedBy = ordr["posreceivedby"].ToString() == "" ? "Consumer" : ordr["posreceivedby"].ToString(),
                                CheckedById = Convert.ToInt32(ordr["checkedbyid"]),
                                CheckedBy = ordr["checkedby"].ToString(),
                                PosCheckedBy = ordr["poscheckedby"].ToString(),
                                AuditedById = Convert.ToInt32(ordr["auditedbyid"]),
                                AuditedBy = ordr["auditedby"].ToString(),
                                PosAuditedBy = ordr["posauditedby"].ToString(),
                                NotedById = Convert.ToInt32(ordr["notedbyid"]),
                                NotedBy = ordr["notedby"].ToString(),
                                PosNotedBy = ordr["posnotedby"].ToString(),
                                Project = ordr["project"].ToString(),
                                ProjectAddress = ordr["projectaddress"].ToString(),
                                JOWOMOId = Convert.ToInt32(ordr["jowomoid"]),
                                JOWOMOCode = ordr["code"].ToString(),
                                JOWOMONumber = ordr["jowomonumber"].ToString()
                            });
                        }
                    }
                    else
                        lstmcthm = null;
                }
                catch (Exception ex)
                {
                    lstmcthm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstmcthm;
        }

        public List<MCTDetailModel> GetAllDetailsByMCTHeaderId(int hdrId)
        {
            List<MCTDetailModel> lstdtl = new List<MCTDetailModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select dtl.id, chargedmaterialheaderid,materialid,mat.Description [material],serialno, " +
                                                         "dtl.quantity, dtl.unitid, unt.code[unit], dtl.JOWOMOId, jwm.Code, dtl.JOWOMONumber " +
                                                         "from chargedmaterialdetails dtl inner join Stocks mat " +
                                                         "on dtl.materialid = mat.id " +
                                                         "inner join units unt on dtl.unitid = unt.id " +
                                                         "inner join JOWOMO jwm on dtl.JOWOMOId = jwm.Id " +
                                                         "where dtl.chargedmaterialheaderid = ?;", con);

                    ocmd.Parameters.AddWithValue("@hdrid", hdrId);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstdtl.Add(new MCTDetailModel
                            {
                                Id = Convert.ToInt32(ordr["id"]),
                                MCTHeaderId = hdrId,
                                MaterialId = Convert.ToInt32(ordr["materialid"]),
                                Material = ordr["material"].ToString(),
                                UnitId = Convert.ToInt32(ordr["unitid"]),
                                Unit = ordr["unit"].ToString(),
                                Quantity = Convert.ToInt32(ordr["quantity"]),
                                SerialNo = ordr["serialno"].ToString(),
                                JOWOMOId = Convert.ToInt32(ordr["jowomoid"]),
                                JOWOMOCode = ordr["code"].ToString(),
                                JOWOMONumber = ordr["jowomonumber"].ToString()
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

        public string GetUnitAndOnHandByMaterialId(int matid)
        {
            string result = string.Empty;

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select dtl.UnitId,unit.Code [unit],sum(dtl.OnHand) onhand " +
                                                         "from ReceivedMaterialDetails dtl " +
                                                         "inner join Units unit " +
                                                         "on dtl.UnitId=unit.Id " +
                                                         "where dtl.MaterialId=? " +
                                                         "group by dtl.UnitId, unit.Code " +
                                                         "having SUM(dtl.OnHand)>0; ", con);

                    ocmd.Parameters.AddWithValue("@matid", matid);

                    OleDbDataReader ordr = ocmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            result = ordr["UnitId"].ToString() + "#" + ordr["unit"].ToString() + "#" + ordr["onhand"].ToString();
                        }
                    }
                    else
                        result = string.Empty;
                }
                catch (Exception ex)
                {
                    result = string.Empty;
                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }

        public ProcessModel AddNewMCTHeaderStock(MCTHeaderModel mcthm)
        {
            ProcessModel pm = new ProcessModel();

            if (mcthm.PostedById != 0)
            {
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
                        cmd.CommandText = "insert into chargedmaterialheaders(posteddate,postedbyid,issuedbyid,receivedbyid," +
                                          "checkedbyid,auditedbyid,notedbyid,project,projectaddress,jowomoid,jowomonumber,isconsumerreceived,receivedconsumer) " +
                                          "values(?,?,?,?,?,?,?,?,?,?,?,?,?);";

                        //@posteddate,@postedbyid,@issuedbyid,@receivedbyid,@checkedbyid,@auditedbyid,@notedbyid,@project,@projectaddress,@jowomoid,@jowomonumber,@isconsumerreceived,@receivedconsumer

                        cmd.Parameters.AddWithValue("@posteddate", mcthm.PostedDate);
                        cmd.Parameters.AddWithValue("@postedbyid", mcthm.PostedById);
                        cmd.Parameters.AddWithValue("@issuedbyid", mcthm.IssuedById);

                        if (mcthm.IsConsumerReceived)
                            cmd.Parameters.AddWithValue("@receivedbyid", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@receivedbyid", mcthm.ReceivedById);

                        cmd.Parameters.AddWithValue("@checkedbyid", mcthm.CheckedById);
                        cmd.Parameters.AddWithValue("@auditedbyid", mcthm.AuditedById);
                        cmd.Parameters.AddWithValue("@notedbyid", mcthm.NotedById);
                        cmd.Parameters.AddWithValue("@project", mcthm.Project);
                        cmd.Parameters.AddWithValue("@projectaddress", mcthm.ProjectAddress);
                        cmd.Parameters.AddWithValue("@jowomoid", mcthm.JOWOMOId);
                        cmd.Parameters.AddWithValue("@jowomonumber", mcthm.JOWOMONumber);
                        cmd.Parameters.AddWithValue("@isconsumerreceived", mcthm.IsConsumerReceived);

                        if (mcthm.IsConsumerReceived)
                            cmd.Parameters.AddWithValue("@receivedconsumer", mcthm.ConsumerReceivedBy);
                        else
                            cmd.Parameters.AddWithValue("@receivedconsumer", DBNull.Value);

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
            }
            else
            {
                pm.IsSuccess = false;
                pm.ProcessMessage = "No Posted Id";
            }
               
            return pm;

        }

        public MCTHeaderModel GetCurrentMCTHeaderIdByUserId(int id)
        {
            MCTHeaderModel mcthm = new MCTHeaderModel();

            using (OleDbDataAdapter da = new OleDbDataAdapter())
            {
                da.SelectCommand = new OleDbCommand();
                da.SelectCommand.Connection = new OleDbConnection(constr);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select max(Id) Id from chargedmaterialheaders where postedbyid=?;";

                da.SelectCommand.Parameters.AddWithValue("@postedbyid", id);

                try
                {
                    OleDbDataReader ordr = da.SelectCommand.ExecuteReader(CommandBehavior.SingleResult);

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            mcthm.Id = Convert.ToInt32(ordr["Id"]);
                        }
                    }
                    else
                    {
                        mcthm.Id = 0;
                    }
                    
                }
                catch (Exception ex)
                {
                    mcthm = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return mcthm;
        }

        public ProcessModel AddNewMCTDetailStock(List<MCTDetailModel> lstmctdm)
        {
            ProcessModel pm = new ProcessModel();

            if (lstmctdm != null)
            {
                OleDbConnection con = new OleDbConnection(constr);
                OleDbTransaction trans = null;

                con.Open();
                trans = con.BeginTransaction();

                foreach (MCTDetailModel mctdm in lstmctdm)
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        try
                        {
                            cmd.Connection = con;
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "insert into ChargedMaterialDetails(chargedmaterialheaderid,materialid,serialno,quantity,unitid,jowomoid,jowomonumber) " +
                                              "values(?,?,?,?,?,?,?); " +
                                              "update stocks set OnHand=OnHand-? where id=?;";

                            //@chargedmaterialheaderid,@materialid,@serialno,@quantity,@unitid,@jowomoid,@jowomonumber
                            //OnHand = OnHand - @quantity where id = @materialid

                            cmd.Parameters.Clear();
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@ChargedMaterialHeaderId", mctdm.MCTHeaderId);
                            cmd.Parameters.AddWithValue("@MaterialId", mctdm.MaterialId);
                            cmd.Parameters.AddWithValue("@SerialNo", mctdm.SerialNo ?? string.Empty);
                            cmd.Parameters.AddWithValue("@Quantity", mctdm.Quantity);
                            cmd.Parameters.AddWithValue("@UnitId", mctdm.UnitId);
                            cmd.Parameters.AddWithValue("@jowomoid", mctdm.JOWOMOId);
                            cmd.Parameters.AddWithValue("@jowomonumber", mctdm.JOWOMONumber);

                            cmd.Parameters.AddWithValue("@Quantity2", mctdm.Quantity);
                            cmd.Parameters.AddWithValue("@MaterialId2", mctdm.MaterialId);

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
                    con.Close();
                    con.Dispose();
                }
                else
                { 
                    trans.Commit();
                    con.Close();
                    con.Dispose();
                    pm = PerformMCTApply(lstmctdm[0].MCTHeaderId);
                }
            }
            return pm;
        }

        private ProcessModel PerformMCTApply(int mcthdrid)
        {
            ProcessModel pm = new ProcessModel();

            if (mcthdrid > 0)
            {
                OleDbConnection con = new OleDbConnection(constr);

                using (OleDbCommand cmd = new OleDbCommand("sp_executeMCTApplybyRR"))
                {
                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mcthdrid", mcthdrid);

                        cmd.ExecuteNonQuery();
                        pm.IsSuccess = true;
                        pm.ProcessMessage = "Successfully Performed.";

                    }
                    catch (Exception ex)
                    {
                        pm.IsSuccess = false;
                        pm.ProcessMessage = "An error occured: \n" + ex.Message;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }

            return pm;
        }

        public List<MCTExportModel> GetMCTDataByDateRange(string dateFrom, string dateTo)
        {
            List<MCTExportModel> lstmctem = new List<MCTExportModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select cmh.Id,cmh.PostedDate,UPPER(usrPosted.FirstName + ' ' + usrPosted.MiddleInitial + ' ' + usrPosted.LastName) [PostedBy], " +
                                                         "empissued.Name[IssuedBy], UPPER(case when isConsumerReceived = 1 then ReceivedConsumer else empreceived.Name end)[ReceivedBy]," +
                                                         "empchecked.Name[CheckedBy], empaudited.Name[AuditedBy], empnoted.Name[NotedBy], JOWOMO.Code, JOWOMO.Description[JOWOMO], cmh.JOWOMONumber," +
                                                         "Project, ProjectAddress, mat.Code[Material], mat.Description[MaterialDescription], cmd.SerialNo, cmd.Quantity, unt.Code[Unit], cma.MCTPrice " +
                                                         "from ChargedMaterialHeaders cmh " +
                                                         "inner join ChargedMaterialDetails cmd " +
                                                         "on cmh.Id = cmd.ChargedMaterialHeaderId " +
                                                         "left join Users usrPosted " +
                                                         "on cmh.PostedById = usrPosted.id " +
                                                         "inner join Employees empissued " +
                                                         "on cmh.IssuedById = empissued.Id " +
                                                         "left join Employees empreceived " +
                                                         "on cmh.ReceivedById = empreceived.Id " +
                                                         "inner join Employees empchecked " +
                                                         "on cmh.CheckedById = empchecked.Id " +
                                                         "inner join Employees empaudited " +
                                                         "on cmh.AuditedById = empaudited.Id " +
                                                         "inner join Employees empnoted " +
                                                         "on cmh.NotedById = empnoted.Id " +
                                                         "inner join JOWOMO on cmh.JOWOMOId = JOWOMO.Id " +
                                                         "inner join Stocks mat on cmd.MaterialId = mat.Id " +
                                                         "inner join Units unt on cmd.UnitId = unt.Id " +
                                                         "inner join ( " +
                                                         "              select MCTDetailId, SUM(RRUnitCost * MCTOutQty)[MCTPrice] " +
                                                         "              from ChargedMaterialApply " +
                                                         "              group by MCTDetailId " +
                                                         ") cma " +
                                                         "on cmd.Id = cma.MCTDetailId " +
                                                         "where PostedDate between ? and ?;", con);

                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@datefrom", dateFrom);
                    ocmd.Parameters.AddWithValue("@dateto", dateTo);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstmctem.Add(new MCTExportModel
                            {
                                MCTId = Convert.ToInt32(ordr["Id"]),
                                PostedDate = ordr["PostedDate"].ToString(),
                                PostedBy = ordr["PostedBy"].ToString(),
                                IssuedBy = ordr["IssuedBy"].ToString(),
                                ReceivedBy = ordr["ReceivedBy"].ToString(),
                                CheckedBy = ordr["CheckedBy"].ToString(),
                                AuditedBy = ordr["AuditedBy"].ToString(),
                                NotedBy = ordr["NotedBy"].ToString(),
                                Code = ordr["Code"].ToString(),
                                JOWOMO = ordr["JOWOMO"].ToString(),
                                JOWOMONumber = ordr["JOWOMONumber"].ToString(),
                                Project = ordr["Project"].ToString(),
                                ProjectAddress = ordr["ProjectAddress"].ToString(),
                                Material = ordr["Material"].ToString(),
                                MaterialDescription = ordr["MaterialDescription"].ToString(),
                                SerialNumber = ordr["SerialNo"].ToString(),
                                Quantity = Convert.ToInt32(ordr["Quantity"]),
                                Unit = ordr["Unit"].ToString(),
                                MCTPrice = Convert.ToDouble(ordr["MCTPrice"])
                            });
                        }
                    }
                    else
                        lstmctem = null;
                }
                catch(Exception ex)
                {
                    lstmctem = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstmctem;
        }
    }
}