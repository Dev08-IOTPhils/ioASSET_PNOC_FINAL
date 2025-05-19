using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class UOMDal
    {
        public int checkUOMCode(UOMEntity uomEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[uomEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CheckUOMCode"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UnitOfMeasurementID", uomEntity.UnitOfMeasurementID)
                    , new SqlParameter("uomCode", uomEntity.UOMCode)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public int DeleteUnitOfMeasurement(UOMEntity uomEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[uomEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteUnitOfMeasurement"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UnitOfMeasurementID", uomEntity.UnitOfMeasurementID)
                    , new SqlParameter("User", uomEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUnitOfMeasurement(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_UnitOfMeasurement]")
                    , new SqlParameter("orderBy", "Active Desc, UnitOfMeasurementID DESC")
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

        public DataTable GetUOMlist(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUnitOfMeasurement"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveUnitOfMeasurement(UOMEntity uomEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[uomEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveUnitOfMeasurement"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UnitOfMeasurementID", uomEntity.UnitOfMeasurementID)
                    , new SqlParameter("UnitOfMeasurement", uomEntity.UnitOfMeasurement)
                    , new SqlParameter("uomCode", uomEntity.UOMCode)
                    , new SqlParameter("Description", uomEntity.Description)
                    , new SqlParameter("Active", uomEntity.Active)
                    , new SqlParameter("User", uomEntity.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable UnitOfMeasurementByID(UOMEntity uomEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[uomEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUnitOfMeasurement"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UnitOfMeasurementID", uomEntity.UnitOfMeasurementID)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}