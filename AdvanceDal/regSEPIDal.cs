using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class regSEPIDal
    {
        public DataTable GetList(string ConnString, int type, string Filter, string datefrom, string dateto)
        {
            //edited by: domdom
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string where = "";
            if (type != 0)
            {
                where = "CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "') " + Filter;
            }
            else
            {
                where = "CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "') " + Filter;
            }
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_REGSEPI]")
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

        public DataTable getlists(LedgerCardEntity x,string datefrom, string dateto)
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
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_REGSEPI]")
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
    }
}