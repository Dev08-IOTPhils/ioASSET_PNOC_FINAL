using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class LocationDal
    {
        //public int SaveLocation(LocationEntity locationEntity)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_SaveLocation"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("LocationID", locationEntity.LocationID)
        //            , new SqlParameter("Location", locationEntity.Location)
        //            , new SqlParameter("WarehouseID", locationEntity.WarehouseID)
        //            , new SqlParameter("Description", locationEntity.Description)
        //            , new SqlParameter("Active", locationEntity.Active)
        //            , new SqlParameter("User", locationEntity.User)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int CheckLC(LocationEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CheckLC"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("LocationCode", x.LocationCode)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CountSubLocation(string ConnString, int parentid)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.ExecuteScalar<int>(
             "SP_CountSubLocation"
             , CommandType.StoredProcedure
             , conString1
             , new SqlParameter("ParentID", parentid)
             );
        }

        public int DeleteLocation(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteLocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", locationEntity.LocationID)
                    , new SqlParameter("User", locationEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDynamicLocationList(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDynamicLocation"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDynamicLocationTree(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDynamicLocationTree"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocation(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[VW_Location]")
                    , new SqlParameter("orderBy", "Active DESC, LocationID DESC")
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
        public DataTable GetLocationByName(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveLocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", locationEntity.LocationID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocationList(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveLocation"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocationListBySite(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveLocationByWarehouse"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("WarehouseID", locationEntity.WarehouseID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetOfficeByLocation(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveAreaByLocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", locationEntity.LocationID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSubLocation(string ConnString, int parentid)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.GetDataTable(
               "SP_RetrieveSubLocation",
               CommandType.StoredProcedure,
               conString1,
               new SqlParameter("ParentID", parentid)
            );
        }

        //Dynamic Location
        public int SaveDynamicLocation(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveDynamicLocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", locationEntity.LocationID)
                    , new SqlParameter("Location", locationEntity.Location)
                    , new SqlParameter("ParentLocationID", locationEntity.ParentLocationID)
                    , new SqlParameter("Description", locationEntity.Description)
                    , new SqlParameter("Active", locationEntity.Active)
                    , new SqlParameter("User", locationEntity.User)
                    , new SqlParameter("Value", locationEntity.Value)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        public int SaveLocation(LocationEntity locationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[locationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveLocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", locationEntity.LocationID)
                    , new SqlParameter("Location", locationEntity.Location)
                    , new SqlParameter("LocationCode", locationEntity.LocationCode)
                    , new SqlParameter("WarehouseID", locationEntity.WarehouseID)
                    , new SqlParameter("Description", locationEntity.Description)
                    , new SqlParameter("Active", locationEntity.Active)
                    , new SqlParameter("User", locationEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}