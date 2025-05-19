using System.Collections.Generic;

namespace AdvanceEntity
{
    public class SecretQuestion : BaseEntity
    {
        private List<UserSecurityAnswer> UserSecurityAnswer = new List<UserSecurityAnswer>();
        private List<UserSecurityQuestion> UserSecurityQuestion = new List<UserSecurityQuestion>();
        public bool Active { get; set; }
        public string Answer { get; set; }
        public List<UserSecurityAnswer> GetSetSecurityAnswer
        {
            set { UserSecurityAnswer = value; }
            get { return UserSecurityAnswer; }
        }

        public List<UserSecurityQuestion> GetSetSecurityQuestion
        {
            set { UserSecurityQuestion = value; }
            get { return UserSecurityQuestion; }
        }

        public string Question { get; set; }
        public int QuestionID { get; set; }
        public int UserID { get; set; }
    }

    public class UserSecurityAnswer
    {
        public string Answer { get; set; }
        public List<UserSecurityAnswer> getSetSecurityAnswer { get; set; }
    }

    public class UserSecurityQuestion
    {
        public List<UserSecurityQuestion> getSetSecurityQuestionID { get; set; }
        public int QuestionID { get; set; }
    }
}