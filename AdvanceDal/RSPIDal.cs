using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class RSPIDal
    {
        public DataTable GetList(string ConnString, int type, string Filter, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            try
            {
                string where = "";
                where = "CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "') " + Filter;

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_RSPI]")
                    , new SqlParameter("orderBy", "CreatedDate ASC")
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

        public DataTable getlists(RSPIEntity x, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "') ";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_RSPI]")
                    , new SqlParameter("orderBy", "ItemCode ASC")
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