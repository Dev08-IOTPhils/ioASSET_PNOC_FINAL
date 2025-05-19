using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class PropertyMaintenanceBll
    {
        private PropertyMaintenanceDal z = new PropertyMaintenanceDal();

        public int AddToMaintenance(PropertyEntity x)
        {
            int id = z.AddToMaintenance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CancelMaintenance(PropertyEntity x)
        {
            int id = z.CancelMaintenance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetAvailablePropertyForMaintenance(string ConnString, string search, int siteid, string FilterDateFrom, string FilterDateTo)
        {
            try
            {
                return z.GetAvailablePropertyForMaintenance(ConnString, search, siteid, FilterDateFrom, FilterDateTo);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyOnMaintenance(string ConnString, string search, int siteid)
        {
            try
            {
                return z.GetAvailablePropertyOnMaintenance(ConnString, search, siteid);
            }
            catch
            {
                throw;
            }
        }

        public string GetEmail(int IDEmailto, string connString)
        {
            string email = z.getEmail(IDEmailto, connString);
            try
            {
                return email;
            }
            catch (Exception)
            {
                return email;
            }
        }

        public DataTable maintenanceInfo(string propertyNo, string ConnString, int pmlid)
        {
            try
            {
                return z.maintenanceInfo(propertyNo, ConnString, pmlid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyMaintenanceSteps(string ConnString, int recordid)
        {
            try
            {
                return z.PropertyMaintenanceSteps(ConnString, recordid);
            }
            catch
            {
                throw;
            }
        }
        public int TerminatePropertyMaintenance(PropertyEntity x)
        {
            int id = z.TerminatePropertyMaintenance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        } // CancelMaintenance
        //GetEmail
        //maintenanceInfo
    }
}