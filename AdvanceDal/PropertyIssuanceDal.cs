using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyIssuanceDal
    {
        public int CancelPropertyIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNumber)
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

        public string ConfirmPropertyIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmPropertyIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("CustodianID", x.CustodianID)
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

        public DataTable GetAvailablePropertyForIssuance(string search, int custodianid, int siteid, string ConnString, int ExpenseType, int EmployeeID, string notlike)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("Search", search)
                    , new SqlParameter("ExpenseType", ExpenseType)
                    , new SqlParameter("EmployeeID", EmployeeID)
                    , new SqlParameter("NotLike", notlike)
                    );
            }
            catch
            {
                throw;
            }
            //try
            //{
            //    string where = "";
            //    if(custodianid != 0)
            //        where = "CustodianID = '" + custodianid + "'";
            //    else
            //        where = "";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_AvailablePropertyForIssuance]")
            //        , new SqlParameter("orderBy", "RecID ASC")
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

        public DataTable GetPropertyIssuanceList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                ////Commented by nero 022224
                //where = "CustodianID = '" + x.CustodianID + "' and EmployeeID = '" + x.EmployeeID + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1";
                where = "CustodianID = '" + x.CustodianID + "' and EmployeeID = '" + x.EmployeeID + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1 and PARICS = '" + x.TransactionType + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForIssuance]")
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

        public DataTable IssuancePendingUpload(string search, string custodianid, string ConnString, int siteid, string administrative)
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
                        where = "HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload' or [Status] = 'For Acknowledgement') and (IssuanceNo like '%" + search + "%' or EmployeeName  like '%" + search + "%')";
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
                        where = "CustodianID in (" + custodianid + ")   and HasDocument = 0 and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload' or [Status] = 'For Acknowledgement') and (IssuanceNo like '%" + search + "%' or EmployeeName  like '%" + search + "%')";
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
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "PropertyIssuanceID Desc")
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

        public DataTable PLPendingUpload(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "IssuanceNo = '" + x.IssuanceNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_PILPendingUpload]")
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

        public DataTable PLPendingUploadWithComponent(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "IssuanceNo = '" + x.IssuanceNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_PILwithComponentPendingUpload]")
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

        public DataTable PropertyGetCount(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PIPropertyGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyIssuanceDetails(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyIssuanceDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyIssuanceNo", x.IssuanceNumber)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveAllToIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveAllToIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveToIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveToIssuance"
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

        public int PropertyTransferToIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
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
        public int UploadPropertyIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #region request management

        public int ApprovePropertyIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclinePropertyIssuance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("IssuanceNo", x.IssuanceNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalPropertyIssuance(string approver, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approver + "' and HasDocument = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "PropertyIssuanceID ASC")
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

        public DataTable GetPendingApprovalPropertyIssuance(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "HasDocument = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyIssuancePendingUpload]")
                    , new SqlParameter("orderBy", "PropertyIssuanceID ASC")
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
        #endregion request management
    }
}