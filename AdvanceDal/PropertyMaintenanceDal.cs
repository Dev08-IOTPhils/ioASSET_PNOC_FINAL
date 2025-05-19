using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyMaintenanceDal
    {
        public int AddToMaintenance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyAddToMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecordID", x.RecordID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("TaskID", x.TaskID)
                    , new SqlParameter("ServiceProviderID", x.ServiceProviderID)
                    , new SqlParameter("Remarks", x.MaintenanceRemarks)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("SendEmail", x.emailSend)
                    );
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }
        }

        //CancelMaintenance
        public int CancelMaintenance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_cancelPropertyMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecordID", x.RecordID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)


                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyForMaintenance(string ConnString, string search, int siteid, string FilterDateFrom, string FilterDateTo)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("FilterDateFrom", FilterDateFrom)
                    , new SqlParameter("FilterDateTo", FilterDateTo)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyOnMaintenance(string ConnString, string search, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyOnMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    //, new SqlParameter("SiteID", siteid)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string getEmail(int IDEmailto, string connectionStr)
        {
            string conString = ConfigurationManager.ConnectionStrings[connectionStr].ToString();
            try
            {
                string query = "SELECT Email FROM tbl_Supplier WHERE SupplierID =" + IDEmailto;
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable maintenanceInfo(string propertyNo, string ConnString, int pmlid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_GetmaintenanceInfo"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyNo", pmlid)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable PropertyMaintenanceSteps(string ConnString, int recid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchMaintenanceSteps"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecordID", recid)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public int TerminatePropertyMaintenance(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TerminatePropertyMaintenance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecordID", x.RecordID)
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("Status", x.Status)
                    , new SqlParameter("Remarks", x.MaintenanceRemarks)
                    , new SqlParameter("ReturnRequestNo", x.ReturnRequestNumber)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

//getEmail

        //maintenanceInfo
    }
}