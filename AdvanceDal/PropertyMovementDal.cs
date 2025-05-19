using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyMovementDal
    {
        public DataTable GetPropertyMovement(string ConnString, string search, string datefrom, string dateto, int siteid, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            //string filter = " LogDate between cast('" + datefrom + "' as datetime) and DateAdd(day, 1, cast('" + dateto + "' AS datetime))  " + Filter;
            string filter = "LogDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "') " + Filter;
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyMovement"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    //, new SqlParameter("SiteID", siteid)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}