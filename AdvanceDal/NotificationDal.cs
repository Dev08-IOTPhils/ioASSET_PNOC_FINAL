using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class NotificationDal
    {
        public int GetNotifCount(string ConnString, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_Notification"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Transaction", "CNT")
                    , new SqlParameter("userID", UserID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetNotification(string ConnString, string UserID, string trx)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
               "SP_Notification"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("Transaction", trx)
                , new SqlParameter("userID", UserID)
                );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ReadALLNotification(string ConnString, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                  "SP_Notification"
                  , CommandType.StoredProcedure
                  , conString
                  , new SqlParameter("Transaction", "READALL")
                  , new SqlParameter("userID", UserID)
                  );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ReadNotification(string ConnString, int NotifID, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                  "SP_Notification"
                  , CommandType.StoredProcedure
                  , conString
                  , new SqlParameter("Transaction", "READ")
                  , new SqlParameter("NotifID", NotifID)
                  , new SqlParameter("userID", UserID)
                  );
            }
            catch (Exception)
            {
                throw;
            }
        }//ReadALLNotification
    }
}