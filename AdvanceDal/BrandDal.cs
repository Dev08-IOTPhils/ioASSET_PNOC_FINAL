using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class BrandDal
    {
        public DataTable BrandbyID(BrandEntity brandEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[brandEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveBrand"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("BrandID", brandEntity.BrandID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeleteBrand(BrandEntity brandEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[brandEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteBrand"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("BrandID", brandEntity.BrandID)
                    , new SqlParameter("User", brandEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetBrand(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_Brand]")
                    , new SqlParameter("orderBy", "Active Desc, BrandID DESC")
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

        public DataTable GetBrandList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveBrand"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveBrand(BrandEntity brandEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[brandEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveBrand"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("BrandID", brandEntity.BrandID)
                    , new SqlParameter("BrandName", brandEntity.BrandName)
                    , new SqlParameter("Description", brandEntity.Description)
                    , new SqlParameter("Active", brandEntity.Active)
                    , new SqlParameter("User", brandEntity.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}