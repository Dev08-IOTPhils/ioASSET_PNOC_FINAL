using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class DeviceDal
    {
        #region Device management

        public int DeleteDevice(DeviceEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteDevice"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable DevicebyID(DeviceEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDevice"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DeviceID", x.DeviceID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDevice(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsDeleted = 'False'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DeviceManagement]")
                    , new SqlParameter("orderBy", "Active Desc,CreatedDate DESC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDeviceList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDevice"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveDevice(DeviceEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveDevice"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("DeviceName", x.DeviceName)
                    , new SqlParameter("IPAddress", x.IPAddress)
                    , new SqlParameter("Port", x.Port)
                    , new SqlParameter("Description", x.Description)
                    , new SqlParameter("DeviceTypeID", x.DeviceTypeID)
                    , new SqlParameter("Active", x.Active)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Device management

        #region Device type

        public DataTable GetDeviceTypeList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDeviceType"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Device type
    }
}