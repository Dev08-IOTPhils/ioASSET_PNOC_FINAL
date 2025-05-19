using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyRequestDal
    {
        #region Property Request

        public int CancelPropertyRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNumber)
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

        public string ConfirmPropertyRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmPropertyRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("DateOfReturn", x.DateOfReturn)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetAvailablePropertyForRequest(string ConnString, string search, int custodianid, int siteid, int expenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("Search", search)
                    , new SqlParameter("ExpenseType", expenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyRequestList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                //where = "CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1 and CustodianID = '" + x.CustodianID + "'";
                where = "CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1 and CustodianID = '" + x.CustodianID + "' and PARICS = '" + x.TransactionType + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForRequest]")
                    , new SqlParameter("orderBy", "PropertyNumber asc")
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

        public DataTable PRLPendingUpload(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "RequestNo = '" + x.RequestNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_PRLPendingUpload]")
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

        public int PropertyRemoveAllToRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveAllToRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)

                    );
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveToRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveToRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyRequestDetails(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyRequestDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNumber)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyRequestGetCount(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyRequestGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int PropertyTransferToRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable RequestPendingUpload(string search, string ConnString, string user, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";

                if (administrative == "True")
                {
                    if (search == "")
                    {
                        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                    }
                    else
                    {
                        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and RequestNo like '%" + search + "%'";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "CreatedBy = '" + user + "' and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                    }
                    else
                    {
                        where = "CreatedBy = '" + user + "' and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and RequestNo like '%" + search + "%'";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRequestPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyRequestID Desc")
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

        public DataTable RequestPendingUpload(string search, string custodianid, int siteid, string ConnString, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');
            try
            {
                string where = "";  //"CustodianID = '" + custodianid + "' and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                                    //if (administrative == "True")
                                    //{
                                    //    if (custodianid == "0" && search == "")
                                    //        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                                    //    if (custodianid != "0" && search == "")
                                    //        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                                    //    if (custodianid != "0" && search != "")
                                    //        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (RequestNo like '%" + search + "%' or RequestorName like '%" + search + "%')";
                                    //    if (custodianid == "0" && search != "")
                                    //        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (RequestNo like '%" + search + "%' or RequestorName like '%" + search + "%')";
                                    //}
                                    //else
                                    //{
                                    //    if (custodianid == "0" && search == "")
                                    //        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                                    //    if (custodianid != "0" && search == "")
                                    //        where = "CustodianID in (" + custodianid + ") and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                                    //    if (custodianid != "0" && search != "")
                                    //        where = "CustodianID in (" + custodianid + ") and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (RequestNo like '%" + search + "%' or RequestorName like '%" + search + "%')";
                                    //    if (custodianid == "0" && search != "")
                                    //        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (RequestNo like '%" + search + "%' or RequestorName like '%" + search + "%')";
                                    //}'
                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload' or [Status] = 'For Acknowledgement') and (RequestNo like '%" + search + "%' or RequestorName  like '%" + search + "%')";
                    }
                    else
                    {
                        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload' or [Status] = 'For Acknowledgement')";
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "CustodianID in (" + custodianid + ")   and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload' or [Status] = 'For Acknowledgement') and (RequestNo like '%" + search + "%' or RequestorName  like '%" + search + "%')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ")  and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload' or [Status] = 'For Acknowledgement')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRequestPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyRequestID ASC")
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
            //string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //try
            //{
            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "SP_ViewPropertyRequestPendingUpload"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("CustodianID", custodianid)
            //        , new SqlParameter("SiteID", siteid)
            //        , new SqlParameter("Search", search)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }
        public int UploadPropertyRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Property Request

        #region Request Management

        public int ApprovePropertyRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclinePropertyRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RequestNo", x.RequestNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyRequestPendingUpload(string ConnString, string approver)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approver + "' and HasDocument = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRequestPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyRequestID ASC")
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

        public DataTable PropertyRequestPendingUpload(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "HasDocument = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRequestPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyRequestID ASC")
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