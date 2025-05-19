using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class UserProfileBll
    {
        private UserProfileDal x = new UserProfileDal();

        public string CheckAnswer(string ConnString, string Email, string Questions, string Answers)
        {
            try
            {
                return x.CheckAnswer(ConnString, Email, Questions, Answers);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //pass
        public int GenerateNewPass(UserEntity ue)
        {
            try
            {
                return x.GenerateNewPass(ue);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetEmail(string ConnString, string Email)
        {
            try
            {
                return x.GetEmail(ConnString, Email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetQuestion(string ConnString, string Question)
        {
            try
            {
                return x.GetQuestion(ConnString, Question);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSecurityQuestion(string connstring, string userID)
        {
            try
            {
                DataTable dt = x.GetSecurityQuestion(connstring, userID);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetUserInfo(string connstring, string userID)
        {
            var ret = "";

            try
            {
                DataTable dt = x.GetUserInfo(connstring, userID);

                if (dt.Rows.Count > 0)
                {
                    ret =
                    dt.Rows[0]["EmailAddress"].ToString() + "|" +
                    dt.Rows[0]["UserName"].ToString() + "|" +
                    dt.Rows[0]["Password"].ToString() + "|" +
                    dt.Rows[0]["EmployeeNo"].ToString() + "|" +
                    dt.Rows[0]["Image"].ToString() + "|" +
                    dt.Rows[0]["DepartmentName"].ToString() + "|";
                    if ((dt.Rows[0]["PositionName"].ToString()) != "")
                    {
                        ret += dt.Rows[0]["PositionName"].ToString();
                    }
                    else
                    {
                        ret += "[No Job Title]";
                    }

                    return ret;
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string UpdateUserInfo(UserEntity user)
        {
            try
            {
                return x.UpdateUserInfo(user).ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public DataTable GetQuestion1(string ConnString, string Question)
        //{
        //    try
        //    {
        //        return x.GetQuestion1(ConnString, Question);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //public DataTable GetQuestion2(string ConnString, string Question)
        //{
        //    try
        //    {
        //        return x.GetQuestion2(ConnString, Question);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //public DataTable GetQuestion3(string ConnString, string Question)
        //{
        //    try
        //    {
        //        return x.GetQuestion3(ConnString, Question);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public DataTable GetQuestion4(string ConnString, string Question)
        //{
        //    try
        //    {
        //        return x.GetQuestion4(ConnString, Question);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public DataTable GetQuestion5(string ConnString, string Question)
        //{
        //    try
        //    {
        //        return x.GetQuestion5(ConnString, Question);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}