using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class StockDisposalBll
    {
        private StockDisposalDal y = new StockDisposalDal();

        public int CancelStockDisposal(StockEntity x)
        {
            int id = y.CancelStockDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string ConfirmStockDisposal(StockEntity x)
        {
            string result = y.ConfirmStockDisposal(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable GetAvailableStockForDisposal(string ConnString, string search, int custodianid)
        {
            try
            {
                return y.GetAvailableStockForDisposal(ConnString, search, custodianid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockDisposalList(string ConnString, int custodian, string createdby, int siteid)
        {
            try
            {
                return y.GetStockDisposalList(ConnString, custodian, createdby, siteid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable SDLPendingUpload(StockEntity x)
        {
            try
            {
                return y.SDLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public string StockDisposalDetails(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockDisposalDetails(x);
                result = dt.Rows[0]["DisposalNo"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string StockDisposalGetCount(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockDisposalGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }
        public DataTable StockDisposalPendingUpload(string search, string ConnString, string custodian, string administrative)
        {
            try
            {
                return y.StockDisposalPendingUpload(search, ConnString, custodian, administrative);
            }
            catch
            {
                throw;
            }
        }

        public int StockRemoveAllToDisposal(StockEntity x)
        {
            try
            {
                int id = y.StockRemoveAllToDisposal(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int StockRemoveToDisposal(StockEntity x)
        {
            try
            {
                int id = y.StockRemoveToDisposal(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int StockTransferToDisposal(StockEntity x)
        {
            int id = y.StockTransferToDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public DataTable UploadStockDisposalDT(string ConnString, string disposalno, string createdby)
        {
            try
            {
                return y.UploadStockDisposalDT(ConnString, disposalno, createdby);
            }
            catch
            {
                throw;
            }
        }
        #region reguest Management

        public int ApproveStockDisposal(StockEntity x)
        {
            int id = y.ApproveStockDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineStockDisposal(StockEntity x)
        {
            int id = y.DeclineStockDisposal(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetSDPendingApproval(string approver, string administrative, string ConnString)
        {
            try
            {
                if (administrative == "True")
                {
                    return y.GetSDPendingApproval(ConnString);
                }
                else
                {
                    return y.GetSDPendingApproval(approver, ConnString);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion reguest Management

        #region Disposal History

        public DataTable GetDisposalHistory(string ConnString, int siteid, string search, string datefrom, string dateto)
        {
            try
            {
                return y.GetDisposalHistory(ConnString, siteid, search, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }

        #endregion Disposal History
    }

    public class StockIssuanceBll
    {
        private StockIssuanceDal y = new StockIssuanceDal();

        public int CancelStockIssuance(StockEntity x)
        {
            int id = y.CancelStockIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string ConfirmStockIssuance(StockEntity x)
        {
            string result = y.ConfirmStockIssuance(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable GetAvailableStockForIssuance(string ConnString, string search, int custodianid)
        {
            try
            {
                return y.GetAvailableStockForIssuance(ConnString, search, custodianid);
            }
            catch
            {
                throw;
            }
        }

        public string GetCountChecker(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.GetCountChecker(x);
                result = dt.Rows[0]["Returnvalue"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockIssuanceList(string ConnString, int employeeid, int custodian, string createdby)
        {
            try
            {
                return y.GetStockIssuanceList(ConnString, employeeid, custodian, createdby);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockIssuanceList1(string ConnString, int employeeid, int custodian, string createdby, string issuanceno)
        {
            try
            {
                return y.GetStockIssuanceList1(ConnString, employeeid, custodian, createdby, issuanceno);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockDetailsPendingUpload(string ConnString, string stockissuanceno)
        {
            try
            {
                return y.StockDetailsPendingUpload(ConnString, stockissuanceno);
            }
            catch
            {
                throw;
            }
        }

        public string StockGetCount(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string StockIssuanceDetails(string ConnString, int IssuanceNo)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockIssuanceDetails(ConnString, IssuanceNo);
                result = dt.Rows[0]["IssuanceNo"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string StockIssuanceDetails(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = y.StockIssuanceDetails(x);
                result = dt.Rows[0]["IssuanceNo"].ToString() + "|" + //0
                    dt.Rows[0]["EmployeeName"].ToString() + "|" + //1
                    dt.Rows[0]["EmployeeDepartment"].ToString() + "|" + //2
                    dt.Rows[0]["EmployeePosition"].ToString() + "|" + //3
                    dt.Rows[0]["ApproverName"].ToString() + "|" + //4
                    dt.Rows[0]["TransactorName"].ToString() + "|" +// 5
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +// 6
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" +//7
                    dt.Rows[0]["Custodian"].ToString() + "|" + //8
                    dt.Rows[0]["Remarks"].ToString() + "|" + //9
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +//10
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" + //11
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" + //12
                    dt.Rows[0]["SiteName"].ToString() + "|" +//13
                    dt.Rows[0]["ApproverPosition"].ToString(); //14
                return result;
            }
            catch
            {
                throw;
            }
        }

        //public DataTable StockRequestPendingUpload(string search, string ConnString, string createdby, string administrative, string custodianid)
        //{
        //    try
        //    {
        //        return y.StockRequestPendingUpload(search, ConnString, createdby, administrative, custodianid);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public DataTable StockIssuancePendingUpload(string search, string ConnString, string createdby, string custodian, string administrative)
        {
            try
            {
                return y.StockIssuancePendingUpload(search, ConnString, createdby, custodian, administrative);
            }
            catch
            {
                throw;
            }
        }
        public int StockRemoveAllToIssuance(StockEntity x)
        {
            try
            {
                int id = y.StockRemoveAllToIssuance(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int StockRemoveToIssuance(StockEntity x)
        {
            try
            {
                int id = y.StockRemoveToIssuance(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int StockTransferToIssuance(StockEntity x)
        {
            int id = y.StockTransferToIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public string UploadStockIssuance(string ConnString, string issuanceno, string createdby)
        {
            string value = "";
            value = y.UploadStockIssuance(ConnString, issuanceno, createdby);
            try
            {
                return value;
            }
            catch
            {
                return value;
            }
        }

        public DataTable UploadStockIssuanceDT(string ConnString, string issuanceno, string createdby)
        {
            try
            {
                return y.UploadStockIssuanceDT(ConnString, issuanceno, createdby);
            }
            catch
            {
                throw;
            }
        }

        public string UploadStockRequest(string ConnString, string requestno, string createdby)
        {
            string value = "";
            value = y.UploadStockRequest(ConnString, requestno, createdby);
            try
            {
                return value;
            }
            catch
            {
                return value;
            }
        }

        public DataTable UploadStockRequestDT(string ConnString, string requestno, string createdby)
        {
            try
            {
                return y.UploadStockRequestDT(ConnString, requestno, createdby);
            }
            catch
            {
                throw;
            }
        }
        #region Stock Issuance History

        public DataTable StockIssuanceHistory(string ConnString, int siteid, string custodianid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            try
            {
                return y.StockIssuanceHistory(ConnString, siteid, custodianid, search, datefrom, dateto, Filter, administrative);
            }
            catch
            {
                throw;
            }
        }

        #endregion Stock Issuance History

        #region Stock Master List

        public DataTable GetStockMasterList(string ConnString, int siteid, string search, string Filter)
        {
            try
            {
                return y.GetStockMasterList(ConnString, siteid, search, Filter);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetStockMasterListSMPI(string ConnString, int siteid)
        {
            try
            {
                return y.GetStockMasterListSMPI(ConnString, siteid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockMinimumLevel(string ConnString, int siteid, string search)
        {
            try
            {
                return y.GetStockMinimumLevel(ConnString, siteid, search);
            }
            catch
            {
                throw;
            }
        }

        #endregion Stock Master List

        #region Request Management

        public int ApproveStockIssuance(StockEntity x)
        {
            int id = y.ApproveStockIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineStockIssuance(StockEntity x)
        {
            int id = y.DeclineStockIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPendingApprovalStockIssuance(string approver, string isadministrative, string ConnString)
        {
            try
            {
                if (isadministrative == "True")
                {
                    return y.GetPendingApprovalStockIssuance(ConnString);
                }
                else
                {
                    return y.GetPendingApprovalStockIssuance(approver, ConnString);
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