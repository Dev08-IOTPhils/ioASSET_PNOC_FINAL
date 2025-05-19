using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class GoodsReceiveHistoryDal
    {
        public DataTable GetGoodsReceiveDetails(string ConnString, int goodsreceiveid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "GoodsReceiveID = '" + goodsreceiveid + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveDetails]")
                    , new SqlParameter("orderBy", "GoodsReceiveID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
          
        public DataTable GetGoodsReceiveHeader(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string filter = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) and Status != 'Pending Approval' and Status != 'Pending Upload' " + Filter;
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchGoodsReceiveHistory"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    , new SqlParameter("Filter", filter)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
            //try
            //{
            //    string where = "";
            //    where = "IsApproved = 1 and HasDocument = 1";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveHeader]")
            //        , new SqlParameter("orderBy", "GoodsReceiveID ASC")
            //        , new SqlParameter("fieldlist", "*")
            //        , new SqlParameter("filter", where)
            //        , new SqlParameter("pageNum", 0)
            //        , new SqlParameter("pageSize", 0)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }
        public DataTable GetGRHeader(string ConnString, int goodsreceiveid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "GoodsReceiveID = '" + goodsreceiveid + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveHeader]")
                    , new SqlParameter("orderBy", "GoodsReceiveID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}