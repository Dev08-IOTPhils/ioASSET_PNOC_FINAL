using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyMaintenanceHistoryDal
    {
        public DataTable GetAvailablePropertyOnMaintenance(string ConnString, string search, int siteid, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string filter = " (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  " + Filter;

                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyOnMaintenanceHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}