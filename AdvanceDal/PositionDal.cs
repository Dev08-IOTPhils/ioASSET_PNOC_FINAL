using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PositionDal
    {
        public int DeletePosition(PositionEntity positionEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[positionEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeletePosition"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PositionID", positionEntity.PositionID)
                    , new SqlParameter("User", positionEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPosition(PositionEntity positionEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[positionEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePositionList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DepartmentID", positionEntity.DepartmentID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPosition(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsDeleted ='False'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_Position]")
                    , new SqlParameter("orderBy", "Active DESC, PositionID DESC")
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

        public DataTable GetPositionByID(PositionEntity positionEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[positionEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePosition"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PositionID", positionEntity.PositionID)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPositionList(PositionEntity positionEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[positionEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePosition"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SavePosition(PositionEntity positionEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[positionEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SavePosition"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PositionID", positionEntity.PositionID)
                    , new SqlParameter("PositionName", positionEntity.Position)
                    , new SqlParameter("DepartmentID", positionEntity.DepartmentID)
                    , new SqlParameter("Description", positionEntity.Description)
                    , new SqlParameter("Active", positionEntity.Active)
                    , new SqlParameter("User", positionEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}