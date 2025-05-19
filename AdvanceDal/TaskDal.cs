using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class TaskDal
    {
        public int DeleteTask(TaskEntity t)
        {
            string conString = ConfigurationManager.ConnectionStrings[t.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteTask"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TaskID", t.TaskID)
                    , new SqlParameter("User", t.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTask(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_Task]")
                    , new SqlParameter("orderBy", "Active Desc, TaskID DESC")
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

        public DataTable GetTaskList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveTask"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveTask(TaskEntity t)
        {
            string conString = ConfigurationManager.ConnectionStrings[t.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveTask"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TaskID", t.TaskID)
                    , new SqlParameter("TaskName", t.TaskName)
                    , new SqlParameter("Description", t.Description)
                    , new SqlParameter("NumberOfSteps", t.NumberOfSteps)
                    , new SqlParameter("Steps", t.Steps)
                    , new SqlParameter("Active", t.Active)
                    , new SqlParameter("User", t.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable TaskbyID(TaskEntity t)
        {
            string conString = ConfigurationManager.ConnectionStrings[t.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveTask"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TaskID", t.TaskID)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}