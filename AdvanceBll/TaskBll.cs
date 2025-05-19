using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class TaskBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private TaskDal taskDAL = new TaskDal();
        public int DeleteTask(TaskEntity TaskEntity)
        {
            int returnVal = taskDAL.DeleteTask(TaskEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetTask(string ConnString)
        {
            try
            {
                return taskDAL.GetTask(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTaskList(string ConnString)
        {
            try
            {
                return taskDAL.GetTaskList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveTask(TaskEntity TaskEntity)
        {
            int id = taskDAL.SaveTask(TaskEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string TaskbyID(TaskEntity TaskEntity)
        {
            try
            {
                string result = "";

                DataTable dt = taskDAL.TaskbyID(TaskEntity);

                result = dt.Rows[0]["TaskID"].ToString() + "|" +
                         dt.Rows[0]["TaskName"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["NumberOfSteps"].ToString() + "|" +
                         dt.Rows[0]["Steps"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}