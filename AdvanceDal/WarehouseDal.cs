using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class WarehouseDal
    {
        //string conString = ConfigurationManager.ConnectionStrings["AIMMIS_TEST_DB"].ToString();

        public int DeleteWarehouse(WarehouseEntity warehouseEntity)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[warehouseEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteWarehouse"
                    , CommandType.StoredProcedure
                    , conString1
                    , new SqlParameter("WarehouseID", warehouseEntity.WarehouseID)
                    , new SqlParameter("User", warehouseEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetWarehouse(string connectionstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connectionstring].ToString();
            try
            {
                string where = "";
                where = "IsDeleted ='False' and WarehouseID != 2";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_Warehouse]")
                    , new SqlParameter("orderBy", "Active DESC, WarehouseID DESC")
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

        public DataTable GetWarehouseByID(WarehouseEntity warehouseEntity)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[warehouseEntity.ConnectionString].ToString();
            try
            {
                if (warehouseEntity.WarehouseID == 0)
                {
                    return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveWarehouse"
                    , CommandType.StoredProcedure
                    , conString1
                    , new SqlParameter("WarehouseID", null)
                    );
                }
                else
                {
                    return DataAccess.MicrosoftDB.GetDataTable(
                        "SP_RetrieveWarehouse"
                        , CommandType.StoredProcedure
                        , conString1
                        , new SqlParameter("WarehouseID", null)
                        );
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetWarehouseByName(WarehouseEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveWarehouse1"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("WarehouseID", x.WarehouseID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetWarehouseList(string ConnString)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveWarehouse"
                    , CommandType.StoredProcedure
                    , conString1
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveWarehouse(WarehouseEntity warehouseEntity)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[warehouseEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveWarehouse"
                    , CommandType.StoredProcedure
                    , conString1
                    , new SqlParameter("WarehouseID", warehouseEntity.WarehouseID)
                    , new SqlParameter("WarehouseCode", warehouseEntity.WarehouseCode)
                    , new SqlParameter("WarehouseName", warehouseEntity.WarehouseName)
                    , new SqlParameter("Active", warehouseEntity.Active)
                    , new SqlParameter("User", warehouseEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}