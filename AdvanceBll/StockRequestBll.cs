using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class StockRequestBll
    {
        private StockRequestDal y = new StockRequestDal();

        public string CancelStockRequest(StockEntity x)
        {
            string result = y.CancelStockRequest(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public string checkQuantity(StockEntity x)
        {
            try
            {
                return y.checkQuantity(x).ToString();
            }
            catch
            {
                throw;
            }
        }

        public DataTable CompleteRequestStock(string ConnString, string requestno, string user)
        {
            try
            {
                return y.CompleteRequestStock(ConnString, requestno, user);
            }
            catch
            {
                throw;
            }
        }

        public string ConfirmStockRequest(StockEntity x)
        {
            string result = y.ConfirmStockRequest(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable GetAvailableStockForRequest(string ConnString, string search, int custodianid, int siteid, string administrative)
        {
            try
            {
                return y.GetAvailableStockForRequest(ConnString, search, custodianid, siteid, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockRequestList(string ConnString, int custodian, string createdby)
        {
            try
            {
                return y.GetStockRequestList(ConnString, custodian, createdby);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockDetailsPendingUpload(string ConnString, string requestno)
        {
            try
            {
                return y.StockDetailsPendingUpload(ConnString, requestno);
            }
            catch
            {
                throw;
            }
        }

        public int StockRemoveAllToRequest(StockEntity x)
        {
            try
            {
                int id = y.StockRemoveAllToRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int StockRemoveToRequest(StockEntity x)
        {
            try
            {
                int id = y.StockRemoveToRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string StockRequestDetails(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockRequestDetails(x);
                result = dt.Rows[0]["RequestNo"].ToString() + "|" + //0
                    dt.Rows[0]["EmployeeName"].ToString() + "|" + //1
                    dt.Rows[0]["EmployeeDepartment"].ToString() + "|" + //2
                    dt.Rows[0]["EmployeePosition"].ToString() + "|" + //3
                    dt.Rows[0]["ApproverName"].ToString() + "|" + //4
                    dt.Rows[0]["TransactorName"].ToString() + "|" + //5
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" + //6
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" + //7
                    dt.Rows[0]["Custodian"].ToString() + "|" + //8
                    dt.Rows[0]["Remarks"].ToString() + "|" + //9
                    dt.Rows[0]["CreatedDate"].ToString() + "|" + //10
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" + //11
                    dt.Rows[0]["ApproverPosition"].ToString() + "|" + //12
                    dt.Rows[0]["EmployeeSite"].ToString(); //13
                //dt.Rows[0]["TransactorEmail"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        //
        public string StockRequestGetCount(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockRequestGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockRequestPendingUpload(string search, string ConnString, string createdby, string administrative, string custodianid)
        {
            try
            {
                return y.StockRequestPendingUpload(search, ConnString, createdby, administrative, custodianid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockRequestPendingUpload(string search, string ConnString, int siteid, string custodianid, string administrative)
        {
            try
            {
                return y.StockRequestPendingUpload(search, ConnString, siteid, custodianid, administrative);
            }
            catch
            {
                throw;
            }
        }
        public string StockReturnGetCount(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockReturnGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int StockTransferToRequest(StockEntity x)
        {
            try
            {
                int id = y.StockTransferToRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        public int UpdateQuantityIssue(StockEntity x)
        {
            try
            {
                int id = y.UpdateQuantityIssue(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        #region Stock Request History

        public DataTable StockRequestHistory(string ConnString, int siteid, string custodianid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            try
            {
                return y.StockRequestHistory(ConnString, siteid, custodianid, search, datefrom, dateto, Filter, administrative);
            }
            catch
            {
                throw;
            }
        }

        #endregion Stock Request History

        #region Request Management

        public int ApproveStockRequest(StockEntity x)
        {
            int id = y.ApproveStockRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineStockRequest(StockEntity x)
        {
            int id = y.DeclineStockRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPendingApprovalStockRequest(string approver, string isadministrative, string ConnString)
        {
            try
            {
                if (isadministrative == "True")
                {
                    return y.GetPendingApprovalStockRequest(ConnString);
                }
                else
                {
                    return y.GetPendingApprovalStockRequest(approver, ConnString);
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