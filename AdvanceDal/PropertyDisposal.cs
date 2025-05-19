using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyDisposal
    {
        public int CancelPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyDisposal"
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

        public int CancelPropertyDisposal1(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyDisposalRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", x.DisposalNumber)
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
                    "SP_PropertyChangeCondition"
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
                    "SP_ConfirmPropertyDisposal"
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

        public DataTable DisposalPendingUpload(string search, string ConnString, int siteid, string custodian, string administrative, string datefrom, string dateto)
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
                        where = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) and HasDocument = 0 and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                        ;
                    }
                    else
                    {
                        where = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))and HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%') and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) and CustodianID in (" + custodian + ") and SiteID = '" + siteid + "' and HasDocument = 0 and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
                    else
                    {
                        where = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) and CustodianID in (" + custodian + ") and SiteID = '" + siteid + "' and HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%') and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
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

        public DataTable GetAvailablePropertyForDisposal(string ConnString, string search, int siteid, int custodianid, int ExpenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    , new SqlParameter("SiteID", siteid)
                    , new SqlParameter("CustodianID", custodianid)
                    , new SqlParameter("ExpenseType", ExpenseType)
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
                where = "CustodianID = '" + x.CustodianID + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 1 and PARICS = '" + x.TransactionType + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyForDisposal]")
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

        public DataTable PropertyDisposalExcels(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveIIRUPandIIRUSPandWMR"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyDisposalID", x.PropertyDisposalID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PDLPendingUploadRequest(PropertyEntity x)
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
                , new SqlParameter("datasrc", "[dbo].[VW_PDRLPendingUpload]")
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

        public DataTable PDRLPendingUpload(PropertyEntity x)
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
                , new SqlParameter("datasrc", "[dbo].[VW_PDRLPendingUpload]")
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
                    "SP_RetrievePropertyDisposalDetails"
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

        public DataTable PropertyDisposalRequestDetails(PropertyEntity x)
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
                    "SP_PDPropertyGetCount"
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
                    "SP_PropertyRemoveAllToDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    //, new SqlParameter("ExpenseType", Convert.ToInt32(x.TransactionType))
                    );
            }
            catch (Exception ex)
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
                    "SP_PropertyRemoveToDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.pmlid)
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
                    "SP_PropertyTransferToDisposal"
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
        public int UploadDocumentsDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyDisposal"
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

        public int UploadDocumentsDisposalRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyDisposalRequest"
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

        public int UploadPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyDisposal"
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
        #region Request management View

        public int ApproveCertifyPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveCertifyPropertyDisposal"
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

        public int ApproveInspectPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveInspectPropertyDisposal"
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

        public int ApprovePropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyDisposal"
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
        public int ApproveJDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveJunkDisposal"
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
        public int ApprovePropertyDisposalRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyDisposalRequest"
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

        public int DeclineCertifyPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineCertifyPropertyDisposal"
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

        public int DeclineInspectPropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineInspectPropertyDisposal"
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

        public int DeclinePropertyDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyDisposal"
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
        public int DeclineJDisposal(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineJunkDisposal"
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
        public int DeclinePropertyDisposalRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyDisposalRequest"
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

        public DataTable DisposalPendingUploadView(string ConnString, int siteid, string approver)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "ApproverUser = '" + approver + "' and SiteID = '" + siteid + "' and HasDocument = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DisposalHistory]")
                    , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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

        public DataTable DisposalPendingUploadView(string ConnString, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "HasDocument = 0 and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DisposalHistory]")
                    , new SqlParameter("orderBy", "DisposalNo ASC")
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

        public DataTable DisposalPendingUploadViewCertify(string ConnString, int siteid, string certifier)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "CertifierUser = '" + certifier + "' and SiteID = '" + siteid + "' and HasDocument = 0 and [Status] = 'Pending Certification'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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

        public DataTable DisposalPendingUploadViewCertify(string ConnString, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "SiteID = '" + siteid + "' and HasDocument = 0 and [Status] = 'Pending Certification'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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

        public DataTable DisposalPendingUploadViewInspection(string ConnString, int siteid, string inpector)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "InspectorUser = '" + inpector + "' and SiteID = '" + siteid + "' and HasDocument = 0 and [Status] = 'Pending Inspection'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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

        public DataTable DisposalPendingUploadViewInspection(string ConnString, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "SiteID = '" + siteid + "' and HasDocument = 0 and [Status] = 'Pending Inspection'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
                    , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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

        public DataTable DisposalRequestPendingUploadView(string ConnString, int siteid, string approver, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string where = "";
            if (administrative == "True")
            {
                where = "HasDocument = '0' and [Status] = 'Pending Approval'";
            }
            else
            {
                where = "ApproverUser = '" + approver + "' and SiteID = '" + siteid + "' and HasDocument = '0' and [Status] = 'Pending Approval'";
            }
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalRequestPU]")
                    , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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
        #endregion Request management View

        #region Disposal Request History

        public DataTable GetDisposalRequestHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = " [Status] != 'Pending Upload' and Status != 'Pending Approval' and Status !='Pending Verification'  and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  " + Filter;

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyDisposalRequestHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion Disposal Request History

        #region Disposal History

        public DataTable DisposalPendingUpload1(string search, string ConnString, int siteid, string custodian, string administrative)
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
                        where = "HasDocument = 0 and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
                    else
                    {
                        where = "HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%') and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "CustodianID in (" + custodian + ") and SiteID = '" + siteid + "' and HasDocument = 0 and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodian + ") and SiteID = '" + siteid + "' and HasDocument = 0 and (DisposalNo like '%" + search + "%' or TransactorName like '%" + search + "%') and (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";
                    }
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

        public DataTable GetDisposalHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = " [Status] != 'Pending Upload' and Status != 'Pending Approval' and Status != 'Pending Verification'  and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  " + Filter;

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyDisposalHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
            //try
            //{
            //    string where = "SiteID = '" + siteid + "' and HasDocument = 1";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
            //        , new SqlParameter("orderBy", "PropertyDisposalID ASC")
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
        public DataTable PropertyDisposalDetailsRequest(PropertyEntity x)
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

        #endregion Disposal History
    }
}