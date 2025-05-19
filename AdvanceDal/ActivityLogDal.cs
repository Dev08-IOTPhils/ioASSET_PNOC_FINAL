using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class ActivityLogDal
    {
        public int SaveActivityLog(string user, string activity, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                   "SaveActivityLog"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("UserID", user)
                   , new SqlParameter("Activity", activity)
                   );
            }
            catch
            {
                throw;
            }
        }
    }
}