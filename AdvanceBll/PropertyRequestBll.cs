using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyRequestBll
    {
        private PropertyRequestDal z = new PropertyRequestDal();

        #region Property Request

        public int CancelPropertyRequest(PropertyEntity x)
        {
            int id = z.CancelPropertyRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string ConfirmPropertyRequest(PropertyEntity x)
        {
            string result = z.ConfirmPropertyRequest(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable GetAvailablePropertyForRequest(string ConnString, string search, int custodianid, int siteid, int expenseType)
        {
            try
            {
                return z.GetAvailablePropertyForRequest(ConnString, search, custodianid, siteid, expenseType);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyRequestList(PropertyEntity x)
        {
            try
            {
                return z.GetPropertyRequestList(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PRLPendingUpload(PropertyEntity x)
        {
            try
            {
                return z.PRLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveAllToRequest(PropertyEntity x)
        {
            try
            {
                int id = z.PropertyRemoveAllToRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int PropertyRemoveToRequest(PropertyEntity x)
        {
            try
            {
                int id = z.PropertyRemoveToRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string PropertyRequestDetails(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.PropertyRequestDetails(x);
                result = dt.Rows[0]["RequestNo"].ToString() + "|" +
                    dt.Rows[0]["RequestorName"].ToString() + "|" +
                    dt.Rows[0]["RequestorDepartment"].ToString() + "|" +
                    dt.Rows[0]["RequestorPosition"].ToString() + "|" +
                    dt.Rows[0]["RequestorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string PropertyRequestGetCount(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.PropertyRequestGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int PropertyTransferToRequest(PropertyEntity x)
        {
            try
            {
                int id = z.PropertyTransferToRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable RequestPendingUpload(string search, string ConnString, string user, string administrative)
        {
            try
            {
                return z.RequestPendingUpload(search, ConnString, user, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable RequestPendingUpload(string search, string custodianid, int siteid, string ConnString, string administrative)
        {
            try
            {
                return z.RequestPendingUpload(search, custodianid, siteid, ConnString, administrative);
            }
            catch
            {
                throw;
            }
        }
        public int UploadPropertyRequest(PropertyEntity x)
        {
            try
            {
                int id = z.UploadPropertyRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Property Request

        #region request management

        public int ApprovePropertyRequest(PropertyEntity x)
        {
            int id = z.ApprovePropertyRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyRequest(PropertyEntity x)
        {
            int id = z.DeclinePropertyRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable PropertyRequestPendingUpload(string ConnString, string isadministrative, string approver)
        {
            try
            {
                if (isadministrative == "True")
                {
                    return z.PropertyRequestPendingUpload(ConnString);
                }
                else
                {
                    return z.PropertyRequestPendingUpload(ConnString, approver);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion request management
    }
}