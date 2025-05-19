using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyRequestForMaintenanceDal
    {
        public int AddToMaintenanceRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyAddToMaintenanceRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecordID", x.RecordID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("Remarks", x.MaintenanceRemarks)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CancelMaintenanceRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyMaintenanceRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyOnMaintenance(string ConnString, string search, int accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyRequestMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    , new SqlParameter("EmployeeID", accountable)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetPropertyAvailableForRequestMaintenance(string ConnString, string search, int accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForRequestMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    , new SqlParameter("EmployeeID", accountable)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}