using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class StockBll
    {
        private StockDal stockDal = new StockDal();

        #region FILES DATA STOCK

        public int DeleteStock(StockEntity x)
        {
            int returnVal = stockDal.DeleteStock(x);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetStock(string ConnString)
        {
            try
            {
                return stockDal.GetStock(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveStock(StockEntity x)
        {
            int id = stockDal.SaveStock(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public string StockByID(StockEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = stockDal.StockByID(x);

                result = dt.Rows[0]["StockID"].ToString() + "|" +
                         dt.Rows[0]["ItemCode"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["ClassificationID"].ToString() + "|" +
                         dt.Rows[0]["CategoryID"].ToString() + "|" +
                         dt.Rows[0]["BrandID"].ToString() + "|" +
                         dt.Rows[0]["ModelID"].ToString() + "|" +
                         dt.Rows[0]["UnitOfMeasurementID"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString() + "|" +
                         dt.Rows[0]["IsReturnable"].ToString() + "|" +
                         dt.Rows[0]["AssetInventoryAccountID"].ToString() + "|" +
                         dt.Rows[0]["IncomeAccountID"].ToString() + "|" +
                         dt.Rows[0]["ExpenseAccountID"].ToString() + "|" +
                         dt.Rows[0]["StockImage"].ToString() + "|" +
                         dt.Rows[0]["CriticalLevel"].ToString() + "|" +
                         dt.Rows[0]["TagTypeID"].ToString() + "|" +
                         dt.Rows[0]["RFIDNumber"].ToString() + "|" +
                         dt.Rows[0]["Barcode"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }
        #endregion FILES DATA STOCK

        #region STOCK GOODS RECEIVE

        public int AddToStockList(StockEntity x)
        {
            int id = stockDal.AddToStockList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ConfirmGoodsReceiveStock(StockEntity x, MiscEntity me)
        {
            int id = stockDal.ConfirmGoodsReceiveStock(x, me);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetStockAddedList(StockEntity xy)
        {
            try
            {
                return stockDal.GetStockAddedList(xy);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockViewGoodsReceive(string search, string ConnString)
        {
            try
            {
                return stockDal.GetStockViewGoodsReceive(search, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public int RemoveAllToStockList(StockEntity x)
        {
            int id = stockDal.RemoveAllToStockList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int RemoveToStockList(StockEntity x)
        {
            int id = stockDal.RemoveToStockList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion STOCK GOODS RECEIVE
    }
}