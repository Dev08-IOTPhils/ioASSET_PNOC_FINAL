using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class AuditTrailDal
    {
        public DataTable GetAuditTrail(string ConnString, string search, string datefrom, string dateto, int siteid, string IpAddress, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = " (ActivityDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  " + Filter;

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAuditTrail"
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