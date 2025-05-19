using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PPEDal
    {
        public DataTable GetList(string ConnString, int type, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = "";
            if (type != 0)
            {
                filter = " RecID>0  " + Filter;
            }
            else
            {
                filter = " PropertyID>0  " + Filter;
            }
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyCard"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Type", type)
                    , new SqlParameter("Filter", filter)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable getlists(LedgerCardEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "PropertyNumber = '" + x.PropertyNo + "' ";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_PPE]")
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