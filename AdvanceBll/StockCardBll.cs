using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class StockCardBll
    {
        public DataTable GetStockCardV2(string ConnString, int StockID)
        {
            StockCardDal scd = new StockCardDal();
            try
            {
                return scd.GetStockCardV2(ConnString, StockID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockList(string ConnString, string Filter)
        {
            StockCardDal scd = new StockCardDal();
            try
            {
                return scd.GetStockList(ConnString, Filter);
            }
            catch
            {
                throw;
            }
        }
    }
}