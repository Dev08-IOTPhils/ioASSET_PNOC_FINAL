using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class StockCardDal
    {
        public DataTable GetStockCardV2(string ConnString, int StockID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStockCardByStockID"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockID", StockID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetStockList(string ConnString, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "StockID>0  " + Filter;

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockList]")
                    , new SqlParameter("orderBy", "StockID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}