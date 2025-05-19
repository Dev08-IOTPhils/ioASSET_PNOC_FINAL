using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class CategoryBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private CategoryDal categoryDAL = new CategoryDal();
        public string CategoryByID(CategoryEntity categoryEntity)
        {
            try
            {
                string result = "";

                DataTable dt = categoryDAL.CategoryByID(categoryEntity);

                result = dt.Rows[0]["CategoryID"].ToString() + "|" +
                         dt.Rows[0]["CategoryName"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString() + "|" +
                         dt.Rows[0]["CategoryCode"].ToString() + "|" +
                         dt.Rows[0]["StartingCount"].ToString() + "|" +
                         dt.Rows[0]["AssetInventoryAccount"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int checkCC(CategoryEntity categoryEntity)
        {
            try
            {
                return categoryDAL.checkCC(categoryEntity);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCategory(CategoryEntity categoryEntity)
        {
            int returnVal = categoryDAL.DeleteCategory(categoryEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetCategoryList(string ConnString)
        {
            try
            {
                return categoryDAL.GetCategoryList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCategoryV2(string ConnString)
        {
            try
            {
                return categoryDAL.GetCategoryV2(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveCategory(CategoryEntity categoryEntity)
        {
            int id = categoryDAL.SaveCategory(categoryEntity);
            try
            {
                return id;
            }
            catch (Exception e)
            {
                return id;
            }
        }

        //public string CategoryByID(CategoryEntity categoryEntity)
        //{
        //    try
        //    {
        //        string result = "";

        //        DataTable dt = categoryDAL.CategoryByID(categoryEntity);

        //        result = dt.Rows[0]["CategoryID"].ToString() + "|" +
        //                 dt.Rows[0]["CategoryName"].ToString() + "|" +
        //                 dt.Rows[0]["Description"].ToString() + "|" +
        //                 dt.Rows[0]["Active"].ToString() + "|" +
        //                 dt.Rows[0]["CategoryCode"].ToString() + "|" +
        //                 dt.Rows[0]["StartingCount"].ToString();
        //        return result;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}