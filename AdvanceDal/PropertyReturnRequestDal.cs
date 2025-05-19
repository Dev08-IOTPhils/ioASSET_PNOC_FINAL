using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyReturnRequestDal
    {
        #region datatables

        public int ApproveExtension(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyExtension"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineExtension(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyExtension"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyAvailableForReturn(string search, string ConnString, int accountable, int custodianid, int ExpenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search != "")
                {
                    where = "EmployeeID = '" + accountable + "' and CustodianID = '" + custodianid + "' and (PropertyNumber like '%" + search + "%' or Description  like '%" + search + "%' or SerialNumber  like '%" + search + "%' or ClassificationName  like '%" + search + "%' or CategoryName  like '%" + search + "%' or BrandName  like '%" + search + "%' or ModelName  like '%" + search + "%') and PARICS='" + ExpenseType + "'";
                }
                else
                {
                    where = "EmployeeID = '" + accountable + "' and CustodianID = '" + custodianid + "' and PARICS='" + ExpenseType + "'";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_AvailablePropertyForReturn]")
                    , new SqlParameter("orderBy", "PropertyNumber ASC")
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

        public DataTable GetPropertyReturnPendingUpload(string search, string ConnString, string accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "CreatedBy = '" + accountable + "' and (Status = 'Pending Upload' or Status = 'On Maintenance' or Status = 'Pending Verification')";
                }
                else
                {
                    where = "CreatedBy = '" + accountable + "' and (Status = 'Pending Upload' or Status = 'On Maintenance' or Status = 'Pending Verification') and ReturnRequestNo like '%" + search + "%'";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnRequestPendingUpload]")
                    , new SqlParameter("orderBy", "ReturnRequestNo Desc")
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

        public DataTable GetPropertyReturnPendingUpload(string search, string ConnString, string custodianid, int siteid, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');
            try
            {
                string where = "";

                if (administrative == "True")
                {

                    if (search == "")
                    {
                        where = "(Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance')";
                    }
                    else
                    {
                        where = "(Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance') and (ReturnRequestNo like '%" + search + "%' or EmployeeName like '%" + search + "%')";
                    }
                }
                else
                {

                    if (search == "")
                    {
                        where = "CustodianID in (" + custodianid + ") and  (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ") and  (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance') and (ReturnRequestNo like '%" + search + "%' or EmployeeName like '%" + search + "%')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnRequestPendingUpload]")
                    , new SqlParameter("orderBy", "ReturnRequestNo Desc")
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

        public DataTable GetReturnPropertyList(string ConnString, int accountable, int custodianid, string ExpenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "EmployeeID = '" + accountable + "' and CustodianID = '" + custodianid + "' and IsTransferred = 1 and PARICS = '" + ExpenseType + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForReturnRequest]")
                    , new SqlParameter("orderBy", "PropertyNumber ASC")
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
        public DataTable PropertyExtensionDateRequestList(string search, string ConnString, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "CreatedBy = '" + createdby + "' and [Status] = 'Pending Approval'";
                }
                else
                {
                    where = "CreatedBy = '" + createdby + "' and [Status] = 'Pending Approval' and (PropertyNumber like '%" + search + "%' or Description like '%" + search + "%' or SerialNumber like '%" + search + "%' or ClassificationName like '%" + search + "%' or Remarks like '%" + search + "%')";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnDateExtension]")
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

        //request management
        public DataTable PropertyExtensionDateRequestPendingApproval(string ConnString, string approveruser)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approveruser + "' and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnDateExtension]")
                    , new SqlParameter("orderBy", "PropertyNumber asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable PropertyExtensionDateRequestPendingApproval(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "[Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnDateExtension]")
                    , new SqlParameter("orderBy", "PropertyNumber asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable PropertyReturnRequestList(string ConnString, string returnrequestnumber)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ReturnRequestNo = '" + returnrequestnumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForReturnRequest]")
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
        #endregion datatables

        #region methods

        public int AddToRequestForExtensionReturnDate(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRequestReturnDate"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("PMLID", x.pmlid)
                    , new SqlParameter("DateOfReturn", x.DateOfReturn)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CancelExtension(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyExtension"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CancelReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyReturnRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ReturnRequestNo", x.ReturnRequestNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ConfirmPropertyReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmPropertyReturnRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("PARICS", x.ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetCountPropertyReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyReturnRequestGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("PARICS", x.ExpenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveAllToPropertyReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                //ExpenseType
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveAllToRequestReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", Convert.ToInt32(x.TransactionType)) // added by nero 022724
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToPropertyReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveToRequestReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int TransferToPropertyReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToRequestReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("PARICS", x.ExpenseType)
                    , new SqlParameter("PMLID", x.RecID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion methods

        #region Property Return History View

        public DataTable GetPropertyReturnPendingUploadView(string search, string datefrom, string dateto, string ConnString, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyReturnHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SiteID", siteid)
                    , new SqlParameter("Search", search)
                    , new SqlParameter("DateFrom", datefrom)
                    , new SqlParameter("DateTo", dateto)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
            //string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //try
            //{
            //    string where = "";
            //    if (search == "")
            //    {
            //        where = "SiteID = '" + siteid + "'";
            //    }
            //    else
            //    {
            //        where = "SiteID = '" + siteid + "' and (ReturnRequestNo like '%" + search + "%' or EmployeeName like '%" + search + "%')";
            //    }
            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnRequestPendingUpload]")
            //        , new SqlParameter("orderBy", "ReturnRequestNo Desc")
            //        , new SqlParameter("fieldlist", "*")
            //        , new SqlParameter("filter", where)
            //        , new SqlParameter("pageNum", 0)
            //        , new SqlParameter("pageSize", 0)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }

        #endregion Property Return History View

        #region Property Return

        public int CancelReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ReturnNo", x.ReturnNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ConfirmPropertyReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmPropertyReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
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

        public DataTable GetCountPropertyReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyReturnGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetForReturnPropertyList(string ConnString, int employeeid, int custodian, string ExpenseType) // add ExpenseType by nero 022624
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "CustodianID = '" + custodian + "' and EmployeeID = '" + employeeid + "' and IsTransferred = 1 and parics = '" + ExpenseType + "'"; // add ExpenseType by nero 022624

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForReturn]")
                    , new SqlParameter("orderBy", "PropertyNumber ASC")
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

        public DataTable GetPropertyReturnPending(string search, string ConnString, int siteid, string custodian, string administrative)
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
                        where = "(Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance')";
                    }
                    else
                    {
                        where = "(Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance') and (ReturnNo like '%" + search + "%' or EmployeeName like '%" + search + "%')";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "CustodianID in (" + custodian + ") and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodian + ") and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload' or Status = 'On Maintenance') and (ReturnNo like '%" + search + "%' or EmployeeName like '%" + search + "%')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyReturnPendingUpload]")
                    , new SqlParameter("orderBy", "ReturnNo Desc")
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

        public DataTable PropertyReturnList(string ConnString, string returnnumber)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ReturnNo = '" + returnnumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForReturn]")
                    , new SqlParameter("orderBy", "ReturnNo asc")
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


        public int RemoveAllToPropertyReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveAllToReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToPropertyReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveToReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int TransferToPropertyReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion Property Return
    }
}