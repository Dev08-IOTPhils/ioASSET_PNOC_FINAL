using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class CategoryDal
    {
        public DataTable CategoryByID(CategoryEntity categoryEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[categoryEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCategory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CategoryID", categoryEntity.CategoryID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int checkCC(CategoryEntity categoryEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[categoryEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CheckCategoryCode"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CategoryID", categoryEntity.CategoryID)
                    , new SqlParameter("CategoryCode", categoryEntity.Code)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCategory(CategoryEntity categoryEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[categoryEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteCategory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CategoryID", categoryEntity.CategoryID)
                    , new SqlParameter("User", categoryEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCategoryList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCategory"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        //public int SaveCategory(CategoryEntity categoryEntity)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[categoryEntity.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_SaveCategory"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("CategoryID", categoryEntity.CategoryID)
        //            , new SqlParameter("CategoryName", categoryEntity.CategoryName)
        //            , new SqlParameter("Description", categoryEntity.Description)
        //            , new SqlParameter("Active", categoryEntity.Active)
        //            , new SqlParameter("User", categoryEntity.CreatedBy)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public DataTable GetCategoryV2(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_Category]")
                    , new SqlParameter("orderBy", "Active Desc, CategoryID DESC")
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
        public int SaveCategory(CategoryEntity categoryEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[categoryEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveCategory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CategoryID", categoryEntity.CategoryID)
                    , new SqlParameter("CategoryName", categoryEntity.CategoryName)
                    , new SqlParameter("Description", categoryEntity.Description)
                    , new SqlParameter("AssetInventoryAccount", categoryEntity.AssetInventoryAccount)
                    , new SqlParameter("Active", categoryEntity.Active)
                    , new SqlParameter("User", categoryEntity.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}