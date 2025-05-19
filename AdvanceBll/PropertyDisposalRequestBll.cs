using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class PropertyDisposalRequestBll
    {
        private PropertyDisposalRequestDal z = new PropertyDisposalRequestDal();

        public int CancelPropertyDisposal(PropertyEntity x)
        {
            int id = z.CancelPropertyDisposal(x);
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
                int id = z.changeCondition(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string ConfirmPropertyDisposal(PropertyEntity x)
        {
            string result = z.ConfirmPropertyDisposal(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable DisposalPendingUpload(string search, string ConnString, int siteid, string custodian, string CreatedBy)
        {
            try
            {
                return z.DisposalPendingUpload(search, ConnString, siteid, custodian, CreatedBy);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyAvailableForDisposalRequest(string search, string ConnString, int accountable, int custodianid, int ExpenseType)
        {
            try
            {
                return z.GetPropertyAvailableForDisposalRequest(search, ConnString, accountable, custodianid, ExpenseType);
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
                return z.GetPropertyDisposalList(x);
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
                return z.PDLPendingUpload(x);
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

                DataTable dt = z.PropertyDisposalDetails(x);
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

                DataTable dt = z.PropertyGetCount(x);
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
                int id = z.PropertyRemoveAllToDisposal(x);
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
                int id = z.PropertyRemoveToDisposal(x);
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
                int id = z.PropertyTransferToDisposal(x);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
                return 1;
            }
        }
    }
}