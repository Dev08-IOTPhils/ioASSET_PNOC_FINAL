using AdvanceDal;

namespace AdvanceBll
{
    public class ActivityLogBll
    {
        private ActivityLogDal activitylogDal = new ActivityLogDal();

        public int SaveActivityLog(string user, string activity, string ConnString)
        {
            try
            {
                return activitylogDal.SaveActivityLog(user, activity, ConnString);
            }
            catch
            {
                throw;
            }
        }
    }
}