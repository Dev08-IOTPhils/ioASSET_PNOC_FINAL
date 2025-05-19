using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class SupplyAllocationBll
    {
        private SupplyAllocationDal supplyallocationDal = new SupplyAllocationDal();

        #region Property Transfer

        public DataTable GetAvailableSupplyForTransfer(StockEntity x, int site, string search, int custodianid)
        {
            try
            {
                return supplyallocationDal.GetAvailableSupplyForTransfer(x, site, search, custodianid);
            }
            catch
            {
                throw;
            }
        }

        public int StockTransferToAllocation(StockEntity x)
        {
            int id = supplyallocationDal.StockTransferToAllocation(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string SupplyTransferGetCount(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = supplyallocationDal.SupplyTransferGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                //throw;
                return "";
            }
        }

        public DataTable GetToTransferAssetList(StockEntity x)
        {
            try
            {
                return supplyallocationDal.GetToTransferAssetList(x);
            }
            catch
            {
                throw;
            }
        }

        public string ConfirmPropertyTransfer(StockEntity x)
        {
            string result = supplyallocationDal.ConfirmPropertyTransfer(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }


        }
            public int RemoveAllToTransfer(StockEntity x)
        {
            try
            {
                int id = supplyallocationDal.RemoveAllToTransfer(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }


        public int RemoveToTransfer(StockEntity x)
        {
            try
            {
                int id = supplyallocationDal.RemoveToTransfer(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        //public int UploadSupplyAllocation(StockEntity x)
        //{
        //    int id = supplyallocationDal.UploadSupplyAllocation(x);
        //    try
        //    {
        //        return id;
        //    }
        //    catch
        //    {
        //        return id;
        //    }
        //}

        public DataTable UploadSupplyAllocation(string ConnString, string transactionno, string createdby)
        {
            try
            {
                return supplyallocationDal.UploadSupplyAllocation(ConnString, transactionno, createdby);
            }
            catch
            {
                throw;
            }
        }


        public DataTable PropertyTransferPendingApproval(string search, string ConnString, string custodianid, int siteid, string administrative)
        {
            try
            {
                return supplyallocationDal.PropertyTransferPendingApproval(search, ConnString, custodianid, siteid, administrative);
            }
            catch
            {
                throw;
            }
        }
        public int CancelPropertyTransfer(StockEntity x)
        {
            int id = supplyallocationDal.CancelPropertyTransfer(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }


        public DataTable PLPendingApproval(string ConnString, string transferno)
        {
            try
            {
                return supplyallocationDal.PLPendingApproval(ConnString, transferno);
            }
            catch
            {
                throw;
            }
        }




        //Request Management -------------------------------------------------------------------------------------
        public DataTable SALPendingUpload(StockEntity x)
        {
            try
            {
                return supplyallocationDal.SALPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSAPendingApproval(string approver, string administrative, string ConnString)
        {
            try
            {
                if (administrative == "True")
                {
                    return supplyallocationDal.GetSAPendingApproval(ConnString);
                }
                else
                {
                    return supplyallocationDal.GetSAPendingApproval(approver, ConnString);
                }
            }
            catch
            {
                throw;
            }
        }


        public int ApproveStockAllocation(StockEntity x)
        {
            int id = supplyallocationDal.ApproveStockAllocation(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineStockAllocation(StockEntity x)
        {
            int id = supplyallocationDal.DeclineStockAllocation(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        //STOCK ALLOCATION HISTORY --------------------------------------------------------------------------------------------------------

        public DataTable StockAllocationHistory(string ConnString, int siteid, string custodianid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            try
            {
                return supplyallocationDal.StockAllocationHistory(ConnString, siteid, custodianid, search, datefrom, dateto, Filter, administrative);
            }
            catch
            {
                throw;
            }
        }

        //public DataTable GetAvailablePropertyForTransferNotIssued(PropertyEntity x, string search, int custodianid)
        //{
        //    try
        //    {
        //        return propertytransferDal.GetAvailablePropertyForTransferNotIssued(x, search, custodianid);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}





        //public DataTable PLPendingUpload(PropertyEntity x)
        //{
        //    try
        //    {
        //        return propertytransferDal.PLPendingUpload(x);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public string PropertyTransferDetails(string ConnString, string x)
        //{
        //    try
        //    {
        //        string result = "";

        //        DataTable dt = supplyallocationDal.PropertyTransferDetails(ConnString, x);
        //        result = dt.Rows[0]["TransferNumber"].ToString() + "|" +
        //            dt.Rows[0]["ApproverName"].ToString() + "|" +
        //            dt.Rows[0]["ApproverEmail"].ToString() + "|" +
        //            dt.Rows[0]["TransactorName"].ToString() + "|" +
        //            dt.Rows[0]["TransactorEmail"].ToString() + "|" +
        //            dt.Rows[0]["Custodian"].ToString() + "|" +
        //            dt.Rows[0]["Remarks"].ToString() + "|" +
        //            dt.Rows[0]["CreatedDate"].ToString();
        //        return result;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public string PropertyTransferDetailsView(string ConnString, string x)
        //{
        //    try
        //    {
        //        string result = "";

        //        DataTable dt = propertytransferDal.PropertyTransferDetailsView(ConnString, x);
        //        result = dt.Rows[0]["TransferNumber"].ToString() + "|" +
        //            dt.Rows[0]["ApproverName"].ToString() + "|" +
        //            dt.Rows[0]["ApproverEmail"].ToString() + "|" +
        //            dt.Rows[0]["TransactorName"].ToString() + "|" +
        //            dt.Rows[0]["TransactorEmail"].ToString() + "|" +
        //            dt.Rows[0]["Custodian"].ToString() + "|" + //5
        //            dt.Rows[0]["Remarks"].ToString() + "|" +
        //            dt.Rows[0]["CreatedDate"].ToString() + "|" +
        //            dt.Rows[0]["FromSiteName"].ToString() + "|" +
        //            dt.Rows[0]["FromLocationName"].ToString() + "|" +
        //            dt.Rows[0]["FromAreaName"].ToString() + "|" +
        //            dt.Rows[0]["ToSiteName"].ToString() + "|" +
        //            dt.Rows[0]["ToLocationName"].ToString() + "|" +
        //            dt.Rows[0]["ToAreaName"].ToString() + "|" +
        //            dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
        //            dt.Rows[0]["TransactorPosition"].ToString() + "|" + //15
        //            dt.Rows[0]["ApproverDepartment"].ToString() + "|" +
        //            dt.Rows[0]["ApproverPosition"].ToString() + "|" +
        //            dt.Rows[0]["CreatedDate"].ToString() + "|" +
        //            dt.Rows[0]["ToEmployeeName"].ToString() + "|" + //19
        //            dt.Rows[0]["ToPositionName"].ToString() + "|" +//20
        //             dt.Rows[0]["FromEmployeeName"].ToString() + "|" +
        //             dt.Rows[0]["TransactionType"].ToString() + "|" +
        //             dt.Rows[0]["ToPositionName"].ToString()
        //            ;
        //        return result;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public string PropertyTransferGetCount(PropertyEntity x)
        //{
        //    try
        //    {
        //        string result = "";

        //        DataTable dt = propertytransferDal.PropertyTransferGetCount(x);
        //        result = dt.Rows[0]["Count"].ToString();
        //        return result;
        //    }
        //    catch
        //    {
        //        //throw;
        //        return "";
        //    }
        //}






        //public int TransferToLocationTempTable(PropertyEntity x, int ExpenseType)
        //{
        //    try
        //    {
        //        int id = propertytransferDal.TransferToLocationTempTable(x, ExpenseType);
        //        return 1;
        //    }
        //    catch
        //    {
        //        return 1;
        //    }
        //}
        //public int UploadPropertyTransfer(PropertyEntity x)
        //{
        //    try
        //    {
        //        int id = propertytransferDal.UploadPropertyTransfer(x);
        //        return 1;
        //    }
        //    catch
        //    {
        //        return 1;
        //    }
        //}
        //#endregion Property Transfer

        //#region Request Management

        //public int ApprovePropertyTransfer(PropertyEntity x)
        //{
        //    int id = propertytransferDal.ApprovePropertyTransfer(x);
        //    try
        //    {
        //        return id;
        //    }
        //    catch
        //    {
        //        return id;
        //    }
        //}

        //public int DeclinePropertyTransfer(PropertyEntity x)
        //{
        //    int id = propertytransferDal.DeclinePropertyTransfer(x);
        //    try
        //    {
        //        return id;
        //    }
        //    catch
        //    {
        //        return id;
        //    }
        //}

        //public DataTable PropertyTransferPendingApproval(string ConnString, string administrative, string approver)
        //{
        //    try
        //    {
        //        if (administrative == "True")
        //        {
        //            return propertytransferDal.PropertyTransferPendingApproval(ConnString);
        //        }
        //        else
        //        {
        //            return propertytransferDal.PropertyTransferPendingApproval(ConnString, approver);
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        #endregion Request Management
    }
}