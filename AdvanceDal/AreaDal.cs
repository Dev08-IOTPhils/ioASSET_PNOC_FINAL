using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class AreaDal
    {
        public int DeleteArea(AreaEntity areaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[areaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteArea"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AreaID", areaEntity.AreaID)
                    , new SqlParameter("User", areaEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetArea(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[VW_Area]")
                    , new SqlParameter("orderBy", "Active Desc, Location ASC, Area ASC")
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

        public DataTable GetAreaByName(AreaEntity areaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[areaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveArea"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AreaID", areaEntity.AreaID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAreaList(AreaEntity areaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[areaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveArea"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAreaListByLocation(AreaEntity areaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[areaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveAreaByLocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("LocationID", areaEntity.LocationID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveArea(AreaEntity areaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[areaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveArea"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AreaID", areaEntity.AreaID)
                    , new SqlParameter("LocationID", areaEntity.LocationID)
                    , new SqlParameter("Area", areaEntity.Area)
                    , new SqlParameter("Description", areaEntity.Description)
                    , new SqlParameter("Active", areaEntity.Active)
                    , new SqlParameter("User", areaEntity.User)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
    }
}