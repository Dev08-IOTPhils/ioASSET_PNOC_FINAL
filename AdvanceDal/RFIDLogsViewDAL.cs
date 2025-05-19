using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class RFIDLogsViewDAL
    {
        public DataTable GetRFIDLOGS(string connstring, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "sp_GetRFIDLogs"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DateFrom", convertStringToDate(datefrom).ToString("dd-MM-yyyy"))
                    , new SqlParameter("DateTo", convertStringToDate(dateto).ToString("dd-MM-yyyy"))
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private DateTime convertStringToDate(string sval)
        {
            DateTime dt;
            DateTime.TryParse(sval, out dt);
            return dt;
        }
    }
}