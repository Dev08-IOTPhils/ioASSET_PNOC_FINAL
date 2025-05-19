using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class StockReturnBll
    {
        private StockReturnDal y = new StockReturnDal();

        public int CancelStockReturn(StockEntity x)
        {
            int id = y.CancelStockReturn(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ConfirmStockReturn(StockEntity x)
        {
            try
            {
                int id = y.ConfirmStockReturn(x);
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public DataTable GetReturnStockList(string ConnString, int accountable, string createdby)
        {
            try
            {
                return y.GetReturnStockList(ConnString, accountable, createdby);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockAvailableForReturn(string search, string ConnString, int accountable)
        {
            try
            {
                return y.GetStockAvailableForReturn(search, ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }
        public int RemoveAllToStockReturn(StockEntity x)
        {
            try
            {
                int id = y.RemoveAllToStockReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToStockReturn(StockEntity x)
        {
            try
            {
                int id = y.RemoveToStockReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable StockDetailsPendingUpload(string ConnString, string returnno)
        {
            try
            {
                return y.StockDetailsPendingUpload(ConnString, returnno);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockReturnPendingUpload(string search, string ConnString, string createdby)
        {
            try
            {
                return y.StockReturnPendingUpload(search, ConnString, createdby);
            }
            catch
            {
                throw;
            }
        }
        public int TransferToReturn(StockEntity x)
        {
            try
            {
                int id = y.TransferToReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        public string UploadStockReturn(string ConnString, string returnno, string createdby)
        {
            string value = "";
            value = y.UploadStockReturn(ConnString, returnno, createdby);
            try
            {
                return value;
            }
            catch
            {
                return value;
            }
        }

        public DataTable UploadStockReturnDT(string ConnString, string returnno, string createdby)
        {
            try
            {
                return y.UploadStockReturnDT(ConnString, returnno, createdby);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}