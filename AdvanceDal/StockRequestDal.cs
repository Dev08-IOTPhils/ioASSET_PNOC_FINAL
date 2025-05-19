using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class StockRequestDal
    {
        public string CancelStockRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_CancelStockRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("RequestNo", x.RequestNo)
                    , new SqlParameter("Remarks", x.Remarks)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public int checkQuantity(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try

            {
                string query = "SELECT quantity FROM tbl_stockMasterList WHERE StockID IN (SELECT StockID FROM tbl_stock WHERE itemcode = '" + x.ItemCode + "')";

                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable CompleteRequestStock(string ConnString, string requestno, string user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_ConfirmStockRequestQuantity"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", requestno)
                    , new SqlParameter("User", user)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string ConfirmStockRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmStockRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetAvailableStockForRequest(string ConnString, string search, int custodianid, int siteid,string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                if (administrative == "True")
                {
                    return DataAccess.MicrosoftDB.GetDataTable(
                        "SP_SearchAvailableStockForRequest"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("CustodianID", custodianid)
                        , new SqlParameter("SiteID", 0)
                        , new SqlParameter("Search", search)
                        );
                }
                else
                {
                    return DataAccess.MicrosoftDB.GetDataTable(
                        "SP_SearchAvailableStockForRequest"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("CustodianID", custodianid)
                        , new SqlParameter("SiteID", siteid)
                        , new SqlParameter("Search", search)
                        );
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public DataTable GetStockRequestList(string ConnString, int custodian, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "CustodianID = '" + custodian + "' and CreatedBy = '" + createdby + "' and IsTransferred = 1";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockForRequest]")
                    , new SqlParameter("orderBy", "SMLID asc")
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

        public DataTable StockDetailsPendingUpload(string ConnString, string requestno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "RequestNo = '" + requestno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockRequestDetailsPendingUpload]")
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

        public int StockRemoveAllToRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveAllToRequest"
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

        public int StockRemoveToRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveToRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockRequestDetails(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStockRequestDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNo)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockRequestGetCount(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockRequestGetCount"
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

        public DataTable StockRequestPendingUpload(string search, string ConnString, string createdby, string administrative, string custodianid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval') and RequestNo like '%" + search + "%'";
                    }
                    else
                    {
                        where = "IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval')";
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "CreatedBy = '" + createdby + "' and IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval') and RequestNo like '%" + search + "%'";
                    }
                    else
                    {
                        where = "CreatedBy = '" + createdby + "' and IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockRequestPendingUpload]")
                    , new SqlParameter("orderBy", "StockRequestID Desc")
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

        public DataTable StockRequestPendingUpload(string search, string ConnString, int siteid, string custodianid, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval') and (RequestNo like '%" + search + "%' or EmployeeName  like '%" + search + "%')";
                    }
                    else
                    {
                        where = "IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload'  or [Status] = 'Pending Approval')";
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "CustodianID in (" + custodianid + ") and IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload' or [Status] = 'Pending Approval') and (RequestNo like '%" + search + "%' or EmployeeName  like '%" + search + "%')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ") and IsUploaded = 0 and ([Status] = 'Pending Quantity Issuance' or [Status] = 'Pending Upload'  or [Status] = 'Pending Approval')";
                    }
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockRequestPendingUpload]")
                    , new SqlParameter("orderBy", "StockRequestID ASC")
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
        public DataTable StockReturnGetCount(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockReturnGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int StockTransferToRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockTransferToRequest"
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
        public int UpdateQuantityIssue(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateQuantityIssue"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        #region Stock Request History

        public DataTable StockRequestHistory(string ConnString, int siteid, string custodianid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');
            string filter = "";
            //if (custodianid == "0")
            if (administrative == "True")
            {
                //{
                filter = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  and [Status] != 'Pending Upload' and [Status] != 'Pending Approval' and [Status]!= 'Pending Quantity Issuance' " + Filter;
            }
            else
            {
                filter = "CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  and [Status] != 'Pending Upload' and [Status] != 'Pending Approval' and [Status]!= 'Pending Quantity Issuance' " + Filter;

            }
            //}
            //else
            //{
            //    filter = "CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  and [Status] = 'Issued' " + Filter;
            //}

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                   "SP_SearchStockRequestHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("CustodianID", custodianid)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Stock Request History

        #region Request Management

        public int ApproveStockRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveStockRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNo)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineStockRequest(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineStockRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNo)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalStockRequest(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "IsUploaded = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockRequestPendingUpload]")
                    , new SqlParameter("orderBy", "StockRequestID ASC")
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

        public DataTable GetPendingApprovalStockRequest(string approver, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approver + "' and IsUploaded = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockRequestPendingUpload]")
                    , new SqlParameter("orderBy", "StockRequestID ASC")
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