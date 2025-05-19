using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyGatePassDal
    {

        public DataTable GetPropertyAvailableForGatePass(string search, string ConnString, int accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "EmployeeID = '" + accountable + "'";
                }
                else
                {
                    where = "EmployeeID = '" + accountable + "' and (PropertyNumber like '%" + search + "%' or Description  like '%" + search + "%' or SerialNumber  like '%" + search + "%' or ClassificationName like '%" + search + "%'  or CategoryName like '%" + search + "%' or BrandName like '%" + search + "%' or ModelName like '%" + search + "%')";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_AvailablePropertyForGatePass]")
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

        public DataTable GetGatePassPropertyList(string ConnString, int accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "EmployeeID = '" + accountable + "' and IsTransferred = 1";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForGatePassRequest]")
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

        public DataTable GetPropertyGatePassPendingUpload(string search, string ConnString, string accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "CreatedBy = '" + accountable + "' and ([Status] = 'Pending Approval' or [Status] = 'Approved') and IsIn = 'False'";
                }
                else
                {
                    where = "CreatedBy = '" + accountable + "' and ([Status] = 'Pending Approval' or [Status] = 'Approved') and IsIn = 'False' and (GatePassNo like '%" + search + "%' or Remarks like  '%" + search + "%')";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyGatePassRequestPendingUpload]")
                    , new SqlParameter("orderBy", "GatePassNo Desc")
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

        public DataTable GetPropertyGatePassPendingUpload(string ConnString, int accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "EmployeeID = '" + accountable + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyGatePassRequestPendingUpload]")
                    , new SqlParameter("orderBy", "GatePassNo ASC")
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

        public DataTable PropertyGatePassRequestList(string ConnString, string gatepassno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "GatePassNo = '" + gatepassno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForGatePassRequest]")
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

        #region methods

        public DataTable GetCountPropertyGatePassRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyGatePassRequestGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        //public int TransferToPropertyGatePassRequest(PropertyEntity x)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_PropertyTransferToGatePassRequest"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("PropertyNumber", x.PropertyNumber)
        //            , new SqlParameter("User", x.CreatedBy)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public int RemoveToPropertyGatePassRequest(PropertyEntity x)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_PropertyRemoveToGatePassRequest"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("PropertyNumber", x.PropertyNumber)
        //            , new SqlParameter("User", x.CreatedBy)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int RemoveAllToPropertyGatePassRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveAllToGatePassRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string ConfirmGatePassRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmPropertyGatePassRequest_1"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("RequestedBy", x.RequestedById)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CancelGatePassRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyGatePassRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GatePassNo", x.GatePassNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyGatePassDetails(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyGatePassDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GatePassNo", x.GatePassNumber)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion methods

        #region Request Management

        public DataTable GetPropertyGatePassPendingApproval(string ConnString, string approver)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approver + "' and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyGatePassRequestPendingUpload]")
                    , new SqlParameter("orderBy", "GatePassNo ASC")
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

        public DataTable GetPropertyGatePassPendingApproval(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "[Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyGatePassRequestPendingUpload]")
                    , new SqlParameter("orderBy", "GatePassNo ASC")
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

        public int ApprovePropertyGatePass(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyGatePass"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GatePassNo", x.GatePassNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclinePropertyGatePass(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyGatePass"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GatePassNo", x.GatePassNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Request Management

        #region Gate Pass view

        public DataTable GetPropertyGatePassApproved(string ConnString, int site, string search, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = " [Status] = 'Approved' and (IsIn = 'False' or IsOut = 'False')  and (AccountableName like '%" + search + "%' or GatePassNo  like '%" + search + "%') " + Filter;

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyGatePassRequestPendingUpload]")
                    , new SqlParameter("orderBy", "GatePassNo ASC")
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

        public int CheckIn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyGatePassCheckIn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GatePassNo", x.GatePassNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CheckOut(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyGatePassCheckOut"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GatePassNo", x.GatePassNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public int TransferToPropertyGatePassRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToGatePassRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int RemoveToPropertyGatePassRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveToGatePassRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Gate Pass view
    }
}