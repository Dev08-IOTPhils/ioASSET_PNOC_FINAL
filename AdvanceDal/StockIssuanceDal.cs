using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class StockDisposalDal
    {
        public int CancelStockDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelStockDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", x.DisposalNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public string ConfirmStockDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmStockDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("ModeOfDisposal", x.ModeOfDisposal)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetAvailableStockForDisposal(string ConnString, string search, int custodianid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailableStockForDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("Search", search)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockDisposalList(string ConnString, int custodian, string createdby, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "CustodianID = '" + custodian + "' and SiteID = '" + siteid + "' and CreatedBy = '" + createdby + "' and IsTransferred = 1";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockForDisposal]")
                    , new SqlParameter("orderBy", "SMLID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable SDLPendingUpload(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "DisposalNo = '" + x.DisposalNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_SDLPendingUpload]")
                , new SqlParameter("orderBy", "RecordID asc")
                , new SqlParameter("fieldlist", "*")
                , new SqlParameter("filter", where)
                , new SqlParameter("pageNum", 0)
                , new SqlParameter("pageSize", 0)
                );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockDisposalDetails(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStockDisposalDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", x.DisposalNumber)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockDisposalGetCount(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockDisposalGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable StockDisposalPendingUpload(string search, string ConnString, string custodian, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodian = custodian.Replace(';', ',');
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    if (search == "")
                    {
                        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Upload' or [Status] = 'Pending Verification')";
                    }
                    else
                    {
                        where = "HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%') and ([Status] = 'Pending Approval' or [Status] = 'Pending Upload' or [Status] = 'Pending Verification')";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "CustodianID in (" + custodian + ")  and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Upload' or [Status] = 'Pending Verification')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodian + ")  and HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%') and ([Status] = 'Pending Approval' or [Status] = 'Pending Upload' or [Status] = 'Pending Verification')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "StockDisposalID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int StockRemoveAllToDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveAllToDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int StockRemoveToDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveToDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int StockTransferToDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockTransferToDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable UploadStockDisposalDT(string ConnString, string disposalno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_UploadStockDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", disposalno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch
            {
                throw;
            }
        }
        #region Request Management

        public int ApproveStockDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveStockDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", x.DisposalNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineStockDisposal(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineStockDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", x.DisposalNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSDPendingApproval(string approver, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                string where = "";
                where = "ApproverUser ='" + approver + "' and HasDocument = 0 and Status = 'Pending Approval'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "StockDisposalID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSDPendingApproval(string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                string where = "";
                where = "HasDocument = 0 and Status = 'Pending Approval'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "StockDisposalID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Request Management

        #region Disposal History

        public DataTable GetDisposalHistory(string ConnString, int siteid, string search, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search != "")
                {
                    where = "(CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and  (DisposalNo like '" + search + "' or TransactorName like '" + search + "') and [Status] != 'Pending Approval' and Status != 'Pending Upload'";
                }
                else
                {
                    where = "(CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and  [Status] != 'Pending Approval' and Status != 'Pending Upload'";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "StockDisposalID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Disposal History
    }

    public class StockIssuanceDal
    {
        public int CancelStockIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNo)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public string ConfirmStockIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetAvailableStockForIssuance(string ConnString, string search, int custodianid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailableStockForIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("Search", search)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCountChecker(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockIssuanceGetChecker"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNo)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockIssuanceList(string ConnString, int employeeid, int custodian, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "CustodianID = '" + custodian + "' and EmployeeID = '" + employeeid + "' and CreatedBy = '" + createdby + "' and IsTransferred = 1 ";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockForIssuance]")
                    , new SqlParameter("orderBy", "SMLID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetStockIssuanceList1(string ConnString, int employeeid, int custodian, string createdby, string issuanceno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IssuanceNo = '" + issuanceno + "' and CustodianID = '" + custodian + "' and EmployeeID = '" + employeeid + "' and CreatedBy = '" + createdby + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockForIssuance]")
                    , new SqlParameter("orderBy", "SMLID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable StockDetailsPendingUpload(string ConnString, string stockissuanceno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IssuanceNo = '" + stockissuanceno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockDetailsPendingUpload]")
                    , new SqlParameter("orderBy", "RecordID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockGetCount(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockIssuanceGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockIssuanceDetails(string ConnString, int IssuanceNo)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStockIssuanceDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", IssuanceNo)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockIssuanceDetails(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStockIssuanceDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNo)
                    );
            }
            catch
            {
                throw;
            }
        }

        //public DataTable StockRequestPendingUpload(string search, string ConnString, string createdby, string administrative, string custodianid)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
        //    try
        //    {
        //        string where = "";
        //        if (administrative == "True")
        //        {
        //            if (search != "")
        //            {
        //                where = "IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval') and RequestNo like '%" + search + "%'";
        //            }
        //            else
        //            {
        //                where = "IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval')";
        //            }
        //        }
        //        else
        //        {
        //            if (search != "")
        //            {
        //                where = "CustodianID in (" + custodianid + ") and CreatedBy = '" + createdby + "' and IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval') and RequestNo like '%" + search + "%'";
        //            }
        //            else
        //            {
        //                where = "CustodianID in (" + custodianid + ") and CreatedBy = '" + createdby + "' and IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval')";
        //            }
        //        }
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //            "UtilPaging"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("datasrc", "[dbo].[VW_StockRequestPendingUpload]")
        //            , new SqlParameter("orderBy", "StockRequestID Desc")
        //            , new SqlParameter("fieldlist", "*")
        //            , new SqlParameter("filter", where)
        //            , new SqlParameter("pageNum", 0)
        //            , new SqlParameter("pageSize", 0)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public DataTable StockIssuancePendingUpload(string search, string ConnString, string createdby, string custodian, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodian = custodian.Replace(';', ',');
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "IsUploaded = 0 and ([Status] = 'Pending Upload' or [Status]='Pending Approval') and (IssuanceNo like '%" + search + "%' or EmployeeName  like '%" + search + "%')";
                    }
                    else
                    {
                        where = "IsUploaded = 0 and ([Status] = 'Pending Upload' or [Status]='Pending Approval')";
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "CustodianID in (" + custodian + ") and CreatedBy = '" + createdby + "' and IsUploaded = 0 and ([Status] = 'Pending Upload' or [Status]='Pending Approval') and (IssuanceNo like '%" + search + "%' or EmployeeName  like '%" + search + "%')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodian + ") and CreatedBy = '" + createdby + "' and IsUploaded = 0 and ([Status] = 'Pending Upload' or [Status]='Pending Approval')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "StockIssuanceID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        public int StockRemoveAllToIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveAllToIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int StockRemoveToIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveToIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int StockTransferToIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockTransferToIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        public string UploadStockIssuance(string ConnString, string issuanceno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_UploadStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", issuanceno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable UploadStockIssuanceDT(string ConnString, string issuanceno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_UploadStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", issuanceno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string UploadStockRequest(string ConnString, string requestno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_UploadStockRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", requestno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable UploadStockRequestDT(string ConnString, string requestno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_UploadStockRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", requestno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch
            {
                throw;
            }
        }
        #region Stock Issuance History

        public DataTable StockIssuanceHistory(string ConnString, int siteid, string custodianid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //try
            //{
            //    return DataAccess.MicrosoftDB.GetDataTable(
            //       "SP_SearchStockIssuanceHistory"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("SiteID", siteid)
            //        , new SqlParameter("CustodianID", custodianid)
            //        , new SqlParameter("Search", search)
            //        , new SqlParameter("DateFrom", datefrom)
            //        , new SqlParameter("DateTo", dateto)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
            try
            {
                string where = "";
                custodianid = custodianid.Replace(';', ',');

                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "(TransactorName like '%" + search + "%' or EmployeeName like '%" + search + "%' or IssuanceNo like '%" + search + "%') and CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                    else
                    {
                        where = "(CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "(TransactorName like '%" + search + "%' or EmployeeName like '%" + search + "%' or IssuanceNo like '%" + search + "%') and CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                }



                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "StockIssuanceID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion Stock Issuance History

        #region Stock Master List

        public DataTable GetStockMasterList(string ConnString, int siteid, string search, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "(ItemCode like '%" + search + "%' or Description like '%" + search + "%' or Custodian like '%" + search + "%') " + Filter;

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockMasterList]")
                    , new SqlParameter("orderBy", "RecID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetStockMasterListSMPI(string ConnString, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockMasterListPerItemCode]")
                    , new SqlParameter("orderBy", "StockID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockMinimumLevel(string ConnString, int siteid, string search)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "WarehouseID = '" + siteid + "' and (ItemCode like '%" + search + "%' or Description like '%" + search + "%' or Custodian like '%" + search + "%')";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockMasterList1]")
                    , new SqlParameter("orderBy", "RecID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Stock Master List

        #region Request Management

        public int ApproveStockIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNo)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineStockIssuance(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNo)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalStockIssuance(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "IsUploaded = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "StockIssuanceID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalStockIssuance(string approver, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approver + "' and IsUploaded = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "StockIssuanceID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Request Management
    }
}