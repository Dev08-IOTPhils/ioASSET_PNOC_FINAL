using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class PropertyDisposalBll
    {
        private PropertyDisposal propertyDisposalDal = new PropertyDisposal();

        #region Property Disposal

        public int CancelPropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.CancelPropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CancelPropertyDisposal1(PropertyEntity x)
        {
            int id = propertyDisposalDal.CancelPropertyDisposal1(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int changeCondition(PropertyEntity x)
        {
            try
            {
                int id = propertyDisposalDal.changeCondition(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string ConfirmPropertyDisposal(PropertyEntity x)
        {
            string result = propertyDisposalDal.ConfirmPropertyDisposal(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable DisposalPendingUpload(string search, string ConnString, int siteid, string custodian, string administrative, string datefrom, string dateto)
        {
            try
            {
                return propertyDisposalDal.DisposalPendingUpload(search, ConnString, siteid, custodian, administrative, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyForDisposal(string ConnString, string search, int siteid, int custodianid, int ExpenseType)
        {
            try
            {
                return propertyDisposalDal.GetAvailablePropertyForDisposal(ConnString, search, siteid, custodianid, ExpenseType);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyDisposalList(PropertyEntity x)
        {
            try
            {
                return propertyDisposalDal.GetPropertyDisposalList(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PDLPendingUpload(PropertyEntity x)
        {
            try
            {
                return propertyDisposalDal.PDLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyDisposalExcels(PropertyEntity x)
        {
            try
            {
                return propertyDisposalDal.PropertyDisposalExcels(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PDLPendingUploadRequest(PropertyEntity x)
        {
            try
            {
                return propertyDisposalDal.PDLPendingUploadRequest(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PDRLPendingUpload(PropertyEntity x)
        {
            try
            {
                return propertyDisposalDal.PDRLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public string PropertyDisposalDetails(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDisposalDal.PropertyDisposalDetails(x);
                result = dt.Rows[0]["DisposalNo"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["InspectorName"].ToString() + "|" +
                    dt.Rows[0]["CertifierName"].ToString() + "|" +
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["InspectorEmail"].ToString() + "|" +
                    dt.Rows[0]["CertifierEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverPosition"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string PropertyDisposalRequestDetails(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDisposalDal.PropertyDisposalRequestDetails(x);
                result = dt.Rows[0]["DisposalNo"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["InspectorName"].ToString() + "|" +
                    dt.Rows[0]["CertifierName"].ToString() + "|" +
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["InspectorEmail"].ToString() + "|" +
                    dt.Rows[0]["CertifierEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverPosition"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string PropertyGetCount(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDisposalDal.PropertyGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveAllToDisposal(PropertyEntity x)
        {
            try
            {
                int id = propertyDisposalDal.PropertyRemoveAllToDisposal(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int PropertyRemoveToDisposal(PropertyEntity x)
        {
            try
            {
                int id = propertyDisposalDal.PropertyRemoveToDisposal(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int PropertyTransferToDisposal(PropertyEntity x)
        {
            try
            {
                int id = propertyDisposalDal.PropertyTransferToDisposal(x);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
                return 1;
            }
        }
        public int UploadDocumentsDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.UploadDocumentsDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int UploadDocumentsDisposalRequest(PropertyEntity x)
        {
            int id = propertyDisposalDal.UploadDocumentsDisposalRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int UploadPropertyDisposal(PropertyEntity x)
        {
            try
            {
                int id = propertyDisposalDal.UploadPropertyDisposal(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Property Disposal

        #region Request management View

        public int ApproveCertifyPropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.ApproveCertifyPropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ApproveInspectPropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.ApproveInspectPropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ApprovePropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.ApprovePropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public int ApproveJDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.ApproveJDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public int ApprovePropertyDisposalRequest(PropertyEntity x)
        {
            int id = propertyDisposalDal.ApprovePropertyDisposalRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineCertifyPropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.DeclineCertifyPropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineInspectPropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.DeclineInspectPropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public int DeclinePropertyDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.DeclinePropertyDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public int DeclineJDisposal(PropertyEntity x)
        {
            int id = propertyDisposalDal.DeclineJDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyDisposalRequest(PropertyEntity x)
        {
            int id = propertyDisposalDal.DeclinePropertyDisposalRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable DisposalPendingUploadView(string ConnString, string administrative, int siteid, string approver)
        {
            try
            {
                if (administrative == "True")
                {
                    return propertyDisposalDal.DisposalPendingUploadView(ConnString, siteid);
                }
                else
                {
                    return propertyDisposalDal.DisposalPendingUploadView(ConnString, siteid, approver);
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable DisposalPendingUploadViewCertify(string ConnString, string administrative, int siteid, string certifier)
        {
            try
            {
                if (administrative == "True")
                {
                    return propertyDisposalDal.DisposalPendingUploadViewCertify(ConnString, siteid);
                }
                else
                {
                    return propertyDisposalDal.DisposalPendingUploadViewCertify(ConnString, siteid, certifier);
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable DisposalPendingUploadViewInspection(string ConnString, string administrative, int siteid, string inspector)
        {
            try
            {
                if (administrative == "True")
                {
                    return propertyDisposalDal.DisposalPendingUploadViewInspection(ConnString, siteid);
                }
                else
                {
                    return propertyDisposalDal.DisposalPendingUploadViewInspection(ConnString, siteid, inspector);
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable DisposalRequestPendingUploadView(string ConnString, string administrative, int siteid, string approver)
        {
            try
            {
                return propertyDisposalDal.DisposalRequestPendingUploadView(ConnString, siteid, approver, administrative);
            }
            catch
            {
                throw;
            }
        }
        #endregion Request management View

        #region Disposal History

        public DataTable DisposalPendingUpload1(string search, string ConnString, int siteid, string custodian, string administrative)
        {
            try
            {
                return propertyDisposalDal.DisposalPendingUpload1(search, ConnString, siteid, custodian, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDisposalHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return propertyDisposalDal.GetDisposalHistory(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDisposalRequestHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return propertyDisposalDal.GetDisposalRequestHistory(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }
        public string PropertyDisposalDetailsRequest(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDisposalDal.PropertyDisposalDetailsRequest(x);
                result = dt.Rows[0]["DisposalNo"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["InspectorName"].ToString() + "|" +
                    dt.Rows[0]["CertifierName"].ToString() + "|" +
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["InspectorEmail"].ToString() + "|" +
                    dt.Rows[0]["CertifierEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverPosition"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        #endregion Disposal History
    }
}