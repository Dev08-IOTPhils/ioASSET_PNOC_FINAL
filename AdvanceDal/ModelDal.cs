using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class ModelDal
    {
        public int DeleteModel(ModelEntity modelEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[modelEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteModel"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModelID", modelEntity.ModelID)
                    , new SqlParameter("User", modelEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetModel(string ConnString, string orderby)
        {
            if (orderby == "NewModel")
            {
                orderby = "ModelID DESC";
            }
            else
            {
                orderby = "ModelID DESC";
            }
            orderby = "Active DESC, " + orderby;
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";

                where = "IsDeleted = 'False'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_Model]")
                    , new SqlParameter("orderBy", orderby)
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

        public DataTable GetModelByID(ModelEntity modelEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[modelEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveModel"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModelID", modelEntity.ModelID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetModelList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveModel"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveModel(ModelEntity modelEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[modelEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveModel"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModelID", modelEntity.ModelID)
                    , new SqlParameter("ModelName", modelEntity.ModelName)
                    , new SqlParameter("Description", modelEntity.Description)
                    , new SqlParameter("Active", modelEntity.Active)
                    , new SqlParameter("Image", modelEntity.Image)
                    //, new SqlParameter("TagType", modelEntity.TagType)
                    , new SqlParameter("User", modelEntity.CreatedBy)
                    //, new SqlParameter("IsComponent", modelEntity.IsComponent)
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