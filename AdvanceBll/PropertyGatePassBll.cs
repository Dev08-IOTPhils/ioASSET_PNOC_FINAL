using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class PropertyGatePassBll
    {
        private PropertyGatePassDal z = new PropertyGatePassDal();

        #region Datatables

        public DataTable GetGatePassPropertyList(string ConnString, int accountable)
        {
            try
            {
                return z.GetGatePassPropertyList(ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyAvailableForGatePass(string search, string ConnString, int accountable)
        {
            try
            {
                return z.GetPropertyAvailableForGatePass(search, ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyGatePassPendingUpload(string search, string ConnString, string accountable)
        {
            try
            {
                return z.GetPropertyGatePassPendingUpload(search, ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyGatePassPendingUpload(string ConnString, int accountable)
        {
            try
            {
                return z.GetPropertyGatePassPendingUpload(ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyGatePassRequestList(string ConnString, string gatepassno)
        {
            try
            {
                return z.PropertyGatePassRequestList(ConnString, gatepassno);
            }
            catch
            {
                throw;
            }
        }

        #endregion Datatables

        #region Methods

        public int CancelGatePassRequest(PropertyEntity x)
        {
            try
            {
                int id = z.CancelGatePassRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string ConfirmPropertyGatePassRequest(PropertyEntity x)
        {
            try
            {
                return z.ConfirmGatePassRequest(x);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetCountPropertyGatePassRequest(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.GetCountPropertyGatePassRequest(x);

                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string PropertyGatePassDetails(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.PropertyGatePassDetails(x);
                result = dt.Rows[0]["GatePassNo"].ToString() + "|" +
                    dt.Rows[0]["AccountableName"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["AccountableEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString();
                return result;
            }
            catch(Exception ex)
            {

                string mes = ex.Message.ToString();
                throw;
            }
        }

        public int RemoveAllToPropertyGatePassRequest(PropertyEntity x)
        {
            try
            {
                int id = z.RemoveAllToPropertyGatePassRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToPropertyGatePassRequest(PropertyEntity x)
        {
            try
            {
                int id = z.RemoveToPropertyGatePassRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int TransferToPropertyGatePassRequest(PropertyEntity x)
        {
            try
            {
                int id = z.TransferToPropertyGatePassRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Methods

        #region Request Management

        public int ApprovePropertyGatePass(PropertyEntity x)
        {
            int id = z.ApprovePropertyGatePass(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyGatePass(PropertyEntity x)
        {
            int id = z.DeclinePropertyGatePass(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPropertyGatePassPendingApproval(string ConnString, string administrative, string approver)
        {
            try
            {
                if (administrative == "True")
                {
                    return z.GetPropertyGatePassPendingApproval(ConnString);
                }
                else
                {
                    return z.GetPropertyGatePassPendingApproval(ConnString, approver);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion Request Management

        #region Gate Pass View

        public int CheckIn(PropertyEntity x)
        {
            try
            {
                int id = z.CheckIn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int CheckOut(PropertyEntity x)
        {
            try
            {
                int id = z.CheckOut(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetPropertyGatePassApproved(string ConnString, int site, string search, string Filter)
        {
            try
            {
                return z.GetPropertyGatePassApproved(ConnString, site, search, Filter);
            }
            catch
            {
                throw;
            }
        }
        #endregion Gate Pass View
    }
}