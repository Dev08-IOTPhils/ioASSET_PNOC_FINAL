using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyIssuanceHistoryDal
    {
        public DataTable IssuancePendingUpload(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string filter = " (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  and [Status] != 'Pending Acknowledgement' and [Status] != 'Pending Upload' and Status != 'Pending Approval' and Status!='Pending Verification' " + Filter;

                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyIssuanceHistory"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("Search", search)
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
            //    string where = "Status = 'Issued'";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_PropertyIssuancePendingUpload]")
            //        , new SqlParameter("orderBy", "PropertyIssuanceID ASC")
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

        public DataTable PLPendingUpload(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "IssuanceNo = '" + x.IssuanceNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_PILPendingUpload]")
                , new SqlParameter("orderBy", "RecordID asc")
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