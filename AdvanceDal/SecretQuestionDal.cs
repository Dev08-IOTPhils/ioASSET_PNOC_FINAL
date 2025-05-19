using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class SecretQuestionDal
    {
        public int DeleteSecretQuestion(SecretQuestion secretQuestion)
        {
            string conString = ConfigurationManager.ConnectionStrings[secretQuestion.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteSecretQuestion"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("QuestionID", secretQuestion.QuestionID)
                    , new SqlParameter("User", secretQuestion.CreatedBy)
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

        public DataTable GetSecretQuestion(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsDeleted = 'False' ";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_SecretQuestions]")
                    , new SqlParameter("orderBy", "Active Desc, QuestionID DESC")
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

        public int SaveSecreQuestion(SecretQuestion secretQuestion)
        {
            string conString = ConfigurationManager.ConnectionStrings[secretQuestion.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveSecretQuestion"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("QuestionID", secretQuestion.QuestionID)
                    , new SqlParameter("Question", secretQuestion.Question)
                    , new SqlParameter("Active", secretQuestion.Active)
                    , new SqlParameter("User", secretQuestion.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveSecurityQandA(SecretQuestion sq)
        {
            string conString = ConfigurationManager.ConnectionStrings[sq.ConnectionString].ToString();
            try
            {
                for (int qctr = 0; qctr < sq.GetSetSecurityQuestion.Count; qctr++)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                            "SP_SaveSecurityQuestion"
                            , CommandType.StoredProcedure
                            , conString
                            , new SqlParameter("UserID", sq.UserID)
                            , new SqlParameter("QuestionID", Convert.ToInt32(sq.GetSetSecurityQuestion[qctr].QuestionID))
                            , new SqlParameter("Answer", sq.GetSetSecurityAnswer[qctr].Answer.ToString())
                        );
                }
                //foreach (UserSecurityQuestion usq in sq.GetSetSecurityQuestion)
                //{
                //    foreach (UserSecurityAnswer usa in sq.GetSetSecurityAnswer)
                //    {
                //    }
                //}
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable SecretQuestionByID(SecretQuestion secretQuestion)
        {
            string conString = ConfigurationManager.ConnectionStrings[secretQuestion.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveSecretQuestion"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("QuestionID", secretQuestion.QuestionID)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}