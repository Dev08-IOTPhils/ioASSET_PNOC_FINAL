using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class UserProfileDal
    {
        public string CheckAnswer(string ConnectionString, string Email, string Questions, string Answers)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            string[] Ans = Answers.Split('|');
            string[] Que = Questions.Split('|');
            string isEqual = "";
            try
            {
                for (int i = 0; i < Ans.Length; i++)
                {
                    DataTable dt = DataAccess.MicrosoftDB.GetDataTable(
                       "SP_CheckSecurityAnswer"
                       , CommandType.StoredProcedure
                       , conString
                      , new SqlParameter("EmailAddress", Email)
                      , new SqlParameter("QuestionID", Que[i])
                      , new SqlParameter("Answer", Ans[i])
                       );
                    isEqual += dt.Rows[0]["Answer"].ToString() + "|";
                }

                return isEqual.Remove(isEqual.Length - 1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GenerateNewPass(UserEntity ue)
        {
            string conString = ConfigurationManager.ConnectionStrings[ue.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateUserPassword"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmailAddress", ue.EmailAddress)
                    , new SqlParameter("Password", ue.Password)
                    );
            }
            catch
            {
                throw;
            }
        }

        //public DataTable GetQuestion4(string ConnectionString, string Question)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
        //    try
        //    {
        //        var query = "SELECT * FROM tbl_SecretQuestions WHERE Active = 1 " + Question;
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //             query
        //             , CommandType.Text
        //             , conString
        //             );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //public DataTable GetQuestion5(string ConnectionString, string Question)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
        //    try
        //    {
        //        var query = "SELECT * FROM tbl_SecretQuestions WHERE Active = 1 " + Question;
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //             query
        //             , CommandType.Text
        //             , conString
        //             );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public string GetDepartment(string ConnectionString, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_RetrieveUser"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", UserID)

                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetEmail(string ConnectionString, string Email)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveRandomQuestion"
                    , CommandType.StoredProcedure
                    , conString
                   , new SqlParameter("EmailAddress", Email)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetQuestion(string ConnectionString, string Question)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_GetQuestions"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Question", Question)
                    );
                //var query = "SELECT * FROM tbl_SecretQuestions WHERE Active = 1 " + Question;
                //return DataAccess.MicrosoftDB.GetDataTable(
                //     query
                //     , CommandType.Text
                //     , conString
                //     );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetSecurityQuestion(string ConnectionString, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_GetUserSecurityQuestion"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", UserID)
                    );

                //    var query = "SELECT * FROM tbl_UserSecurityQuestion WHERE UserID = '" + UserID + "'";
                //    return DataAccess.MicrosoftDB.GetDataTable(
                //         query
                //         , CommandType.Text
                //         , conString
                //         );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserInfo(string ConnectionString, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            try
            {
                //////-----------------------------BEFORE
                //var query = "SELECT * FROM VW_UserProfile WHERE UserID = '" + UserID + "'";
                //return DataAccess.MicrosoftDB.GetDataTable(
                //     query
                //     , CommandType.Text
                //     , conString
                //     );

                //////-----------------------------AFTER
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_GetUserInfo"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", UserID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public DataTable GetEmail(string ConnectionString, string Email)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<DataTable>(
        //           "SP_RetrieveRandomQuestion"
        //           , CommandType.StoredProcedure
        //           , conString
        //           , new SqlParameter("EmailAddress", Email)

        //           );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //public DataTable GetQuestion1(string ConnectionString, string Question)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
        //    try
        //    {
        //        var query = "SELECT * FROM tbl_SecretQuestions WHERE Active = 1 " + Question;
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //             query
        //             , CommandType.Text
        //             , conString
        //             );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //public DataTable GetQuestion2(string ConnectionString, string Question)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
        //    try
        //    {
        //        var query = "SELECT * FROM tbl_SecretQuestions WHERE Active = 1 " + Question;
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //             query
        //             , CommandType.Text
        //             , conString
        //             );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public DataTable GetQuestion3(string ConnectionString, string Question)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
        //    try
        //    {
        //        var query = "SELECT * FROM tbl_SecretQuestions WHERE Active = 1 " + Question;
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //             query
        //             , CommandType.Text
        //             , conString
        //             );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //UpdateUserInfo

        public int UpdateUserInfo(UserEntity user)
        {
            string conString = ConfigurationManager.ConnectionStrings[user.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateUserProfile"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", user.UserID)
                    , new SqlParameter("EmailAdd", user.EmailAddress)
                    , new SqlParameter("Image", user.ImageExtension)
                    //, new SqlParameter("Signature", user.SignatureExtension)
                    , new SqlParameter("Password", user.Password)

                    );
            }
            catch
            {
                throw;
            }
        }
    }
}