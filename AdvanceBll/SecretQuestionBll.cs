using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class SecretQuestionBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private SecretQuestionDal SecretDal = new SecretQuestionDal();
        public int DeleteSecretQuestion(SecretQuestion secretQuestion)
        {
            int returnval = SecretDal.DeleteSecretQuestion(secretQuestion);
            try
            {
                return returnval;
            }
            catch
            {
                return returnval;
            }
        }

        public DataTable GetBrandList(string ConnString)
        {
            try
            {
                return SecretDal.GetBrandList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSecretQuestion(string ConnString)
        {
            try
            {
                return SecretDal.GetSecretQuestion(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveSecretQuestion(SecretQuestion secretQuestion)
        {
            int id = SecretDal.SaveSecreQuestion(secretQuestion);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int SaveSecurityQandA(SecretQuestion sq)
        {
            try
            {
                SecretDal.SaveSecurityQandA(sq);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public string SecretQuestionByID(SecretQuestion secretQuestion)
        {
            try
            {
                string result = "";

                DataTable dt = SecretDal.SecretQuestionByID(secretQuestion);

                result = dt.Rows[0]["QuestionID"].ToString() + "|" +
                         dt.Rows[0]["Question"].ToString() + "|" +
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