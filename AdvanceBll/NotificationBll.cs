using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class NotificationBll
    {
        private NotificationDal x = new NotificationDal();

        public int GetNotifCount(string ConnString, string UserID)
        {
            try
            {
                return x.GetNotifCount(ConnString, UserID);
            }
            catch
            {
                throw;
            }
        }

        //GetNotification
        public DataTable GetNotification(string ConnString, string UserID, string trx)
        {
            try
            {
                return x.GetNotification(ConnString, UserID, trx);
            }
            catch
            {
                throw;
            }
        }

        public string ReadALLNotification(string ConnString, string UserID)
        {
            try
            {
                return x.ReadALLNotification(ConnString, UserID);
            }
            catch
            {
                throw;
            }
        }

        public string ReadNotification(string ConnString, int NotifID, string UserID)
        {
            try
            {
                return x.ReadNotification(ConnString, NotifID, UserID);
            }
            catch
            {
                throw;
            }
        }
    }
}