using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class ClassificationDal
    {
        //public int SaveClassification(ClassificationEntity classificationEntity)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[classificationEntity.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_SaveClassification"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("ClassificationID", classificationEntity.ClassificationID)
        //            , new SqlParameter("ClassificationName", classificationEntity.ClassificationName)
        //            , new SqlParameter("ClassificationCode", classificationEntity.ClassificationCode)
        //            , new SqlParameter("StartingCount", classificationEntity.StartingCount)
        //            , new SqlParameter("Description", classificationEntity.Description)
        //            , new SqlParameter("Active", classificationEntity.Active)
        //            , new SqlParameter("User", classificationEntity.CreatedBy)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int checkCC(ClassificationEntity classificationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[classificationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CheckCC"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ClassificationID", classificationEntity.ClassificationID)
                    , new SqlParameter("ClassificationCode", classificationEntity.ClassificationCode)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable ClassificationbyID(ClassificationEntity classificationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[classificationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveClassification"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ClassificationID", classificationEntity.ClassificationID)
                    );
            }
            catch
            {
                throw;
            }
        }
        public int DeleteClassification(ClassificationEntity classificationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[classificationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteClassification"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ClassificationID", classificationEntity.ClassificationID)
                    , new SqlParameter("User", classificationEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetClassification(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_Classification]")
                    , new SqlParameter("orderBy", "Active Desc, ClassificationID DESC")
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
        public DataTable GetClassificationList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveClassification"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveClassification(ClassificationEntity classificationEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[classificationEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveClassification"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ClassificationID", classificationEntity.ClassificationID)
                    , new SqlParameter("ClassificationName", classificationEntity.ClassificationName)
                    , new SqlParameter("ClassificationCode", classificationEntity.ClassificationCode)
                    , new SqlParameter("StartingCount", 0)
                    , new SqlParameter("Description", classificationEntity.Description)
                    , new SqlParameter("IsFloater", classificationEntity.IsFloater)
                    , new SqlParameter("Active", classificationEntity.Active)
                    , new SqlParameter("User", classificationEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}