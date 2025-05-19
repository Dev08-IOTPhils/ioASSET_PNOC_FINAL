using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyDisposalRequestDal
    {
        public int CancelPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyDisposalRequest"
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

        public int changeCondition(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyChangeConditionRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("Condition", x.Condition)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string ConfirmPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmPropertyDisposalRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModeOfDisposal", x.ModeOfDisposal)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("InspectorID", x.InspectorID)
                    , new SqlParameter("CertifierID", x.CertifierID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable DisposalPendingUpload(string search, string ConnString, int siteid, string custodian, string CreatedBy)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodian = custodian.Replace(';', ',');
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "CreatedBy = '" + CreatedBy + "' and SiteID = '" + siteid + "' and HasDocument = 0";
                }
                else
                {
                    where = "CreatedBy = '" + CreatedBy + "'  and SiteID = '" + siteid + "' and HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%')";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalRequestPU]")
                    , new SqlParameter("orderBy", "PropertyDisposalID Desc")
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

        public DataTable GetPropertyAvailableForDisposalRequest(string search, string ConnString, int accountable, int custodianid, int ExpenseType)
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

        public DataTable GetPropertyDisposalList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                //where = "CustodianID = '" + x.CustodianID + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1";
                where = "CustodianID = '" + x.CustodianID + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1 and PARICS = '" + x.TransactionType + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForDisposalRequest]")
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

        public DataTable PDLPendingUpload(PropertyEntity x)
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
                , new SqlParameter("datasrc", "[dbo].[VW_PDLPendingUpload]")
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

        public DataTable PropertyDisposalDetails(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyDisposalRequestDetails"
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

        public DataTable PropertyGetCount(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PDRPropertyGetCount"
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

        public int PropertyRemoveAllToDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveAllToDisposalRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveToDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyRemoveToDisposalRequest"
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

        public int PropertyTransferToDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToDisposalRequest"
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
    }
}