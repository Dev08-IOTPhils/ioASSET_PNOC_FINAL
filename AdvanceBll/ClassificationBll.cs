using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class ClassificationBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private ClassificationDal classificationDAL = new ClassificationDal();
        public int checkCC(ClassificationEntity classificationEntity)
        {
            try
            {
                return classificationDAL.checkCC(classificationEntity);
            }
            catch
            {
                throw;
            }
        }

        public string ClassificationbyID(ClassificationEntity classificationEntity)
        {
            try
            {
                string result = "";

                DataTable dt = classificationDAL.ClassificationbyID(classificationEntity);

                result = dt.Rows[0]["ClassificationID"].ToString() + "|" +
                         dt.Rows[0]["ClassificationName"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString() + "|" +
                         dt.Rows[0]["ClassificationCode"].ToString() + "|" +
                          dt.Rows[0]["StartingCount"].ToString() + "|" +
                         dt.Rows[0]["IsFloater"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteClassification(ClassificationEntity classificationEntity)
        {
            int returnVal = classificationDAL.DeleteClassification(classificationEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetClassification(string ConnString)
        {
            try
            {
                return classificationDAL.GetClassification(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetClassificationList(string ConnString)
        {
            try
            {
                return classificationDAL.GetClassificationList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveClassification(ClassificationEntity classificationEntity)
        {
            int id = classificationDAL.SaveClassification(classificationEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
    }
}