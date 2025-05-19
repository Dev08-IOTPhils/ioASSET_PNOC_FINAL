using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyRequestForMaintenanceBll
    {
        private PropertyRequestForMaintenanceDal z = new PropertyRequestForMaintenanceDal();

        public int AddToMaintenanceRequest(PropertyEntity x)
        {
            int id = z.AddToMaintenanceRequest(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CancelMaintenanceRequest(PropertyEntity x)
        {
            try
            {
                int id = z.CancelMaintenanceRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetAvailablePropertyOnMaintenance(string ConnString, string search, int accountable)
        {
            try
            {
                return z.GetAvailablePropertyOnMaintenance(ConnString, search, accountable);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyAvailableForRequestMaintenance(string ConnString, string search, int accountable)
        {
            try
            {
                return z.GetPropertyAvailableForRequestMaintenance(ConnString, search, accountable);
            }
            catch
            {
                throw;
            }
        }
    }
}