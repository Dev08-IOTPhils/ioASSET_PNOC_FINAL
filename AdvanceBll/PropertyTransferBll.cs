using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyTransferBll
    {
        private PropertyTransferDal propertytransferDal = new PropertyTransferDal();

        #region Property Transfer

        public int CancelPropertyTransfer(PropertyEntity x)
        {
            int id = propertytransferDal.CancelPropertyTransfer(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string ConfirmPropertyTransfer(PropertyEntity x)
        {
            string result = propertytransferDal.ConfirmPropertyTransfer(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable GetAvailablePropertyForTransfer(PropertyEntity x, string search, int custodianid, int ExpenseType)
        {
            try
            {
                return propertytransferDal.GetAvailablePropertyForTransfer(x, search, custodianid, ExpenseType);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyForTransferNotIssued(PropertyEntity x, string search, int custodianid)
        {
            try
            {
                return propertytransferDal.GetAvailablePropertyForTransferNotIssued(x, search, custodianid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetToTransferAssetList(PropertyEntity x)
        {
            try
            {
                return propertytransferDal.GetToTransferAssetList(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PLPendingApproval(string ConnString, string transferno)
        {
            try
            {
                return propertytransferDal.PLPendingApproval(ConnString, transferno);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PLPendingApprovalWithComponent(string ConnString, string transferno)
        {
            try
            {
                return propertytransferDal.PLPendingApprovalWithComponent(ConnString, transferno);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PLPendingUpload(PropertyEntity x)
        {
            try
            {
                return propertytransferDal.PLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public string PropertyTransferDetails(string ConnString, string x)
        {
            try
            {
                string result = "";

                DataTable dt = propertytransferDal.PropertyTransferDetails(ConnString, x);
                result = dt.Rows[0]["TransferNumber"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
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

        public string PropertyTransferDetailsView(string ConnString, string x)
        {
            try
            {
                string result = "";

                DataTable dt = propertytransferDal.PropertyTransferDetailsView(ConnString, x);
                result = dt.Rows[0]["TransferNumber"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" + //5
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["FromSiteName"].ToString() + "|" +
                    dt.Rows[0]["FromLocationName"].ToString() + "|" +
                    dt.Rows[0]["FromAreaName"].ToString() + "|" +
                    dt.Rows[0]["ToSiteName"].ToString() + "|" +
                    dt.Rows[0]["ToLocationName"].ToString() + "|" +
                    dt.Rows[0]["ToAreaName"].ToString() + "|" +
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" + //15
                    dt.Rows[0]["ApproverDepartment"].ToString() + "|" +
                    dt.Rows[0]["ApproverPosition"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["ToEmployeeName"].ToString() + "|" + //19
                    dt.Rows[0]["ToPositionName"].ToString() + "|" +//20
                     dt.Rows[0]["FromEmployeeName"].ToString() + "|" + //21
                     dt.Rows[0]["TransactionType"].ToString() + "|" + //22
                     dt.Rows[0]["ToPositionName"].ToString() + "|" + //23
                     dt.Rows[0]["FromPositionName"].ToString() + "|" + //24
                      dt.Rows[0]["FromDepartmentName"].ToString(); //25
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string PropertyTransferGetCount(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertytransferDal.PropertyTransferGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                //throw;
                return "";
            }
        }

        public DataTable PropertyTransferPendingApproval(string search, string ConnString, string custodianid, int siteid, string administrative)
        {
            try
            {
                return propertytransferDal.PropertyTransferPendingApproval(search, ConnString, custodianid, siteid, administrative);
            }
            catch
            {
                throw;
            }
        }
        public int RemoveAllToTransfer(PropertyEntity x)
        {
            try
            {
                int id = propertytransferDal.RemoveAllToTransfer(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToTransfer(PropertyEntity x)
        {
            try
            {
                int id = propertytransferDal.RemoveToTransfer(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int TransferToLocationTempTable(PropertyEntity x, int ExpenseType)
        {
            try
            {
                int id = propertytransferDal.TransferToLocationTempTable(x, ExpenseType);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        public int UploadPropertyTransfer(PropertyEntity x)
        {
            try
            {
                int id = propertytransferDal.UploadPropertyTransfer(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Property Transfer

        #region Request Management

        public int ApprovePropertyTransfer(PropertyEntity x)
        {
            int id = propertytransferDal.ApprovePropertyTransfer(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyTransfer(PropertyEntity x)
        {
            int id = propertytransferDal.DeclinePropertyTransfer(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable PropertyTransferPendingApproval(string ConnString, string administrative, string approver)
        {
            try
            {
                if (administrative == "True")
                {
                    return propertytransferDal.PropertyTransferPendingApproval(ConnString);
                }
                else
                {
                    return propertytransferDal.PropertyTransferPendingApproval(ConnString, approver);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion Request Management
    }
}