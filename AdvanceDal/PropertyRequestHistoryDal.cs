using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyRequestHistoryDal
    {
        public DataTable RequestPendingUpload(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            string filter = " (CreatedDate between cast('" + datefrom + "' as datetime) and DateAdd(day, 1, cast('" + dateto + "' AS datetime))) and [Status] != 'Pending Acknowledgement' and [Status] != 'Pending Upload' and Status != 'Pending Approval' and Status!='Pending Verification'  " + Filter;

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyRequestHistory"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    , new SqlParameter("Filter", filter)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}