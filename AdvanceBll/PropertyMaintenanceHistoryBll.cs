using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class PropertyMaintenanceHistoryBll
    {
        private PropertyMaintenanceHistoryDal z = new PropertyMaintenanceHistoryDal();

        public DataTable GetAvailablePropertyOnMaintenance(string ConnString, string search, int siteid, string datefrom, string dateto, string Filter)
        {
            try
            {
                return z.GetAvailablePropertyOnMaintenance(ConnString, search, siteid, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }
    }
}