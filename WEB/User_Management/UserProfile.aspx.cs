using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data;
using System.IO;

public partial class User_Management_User_Profile : System.Web.UI.Page
{
    //protected System.Web.UI.WebControls.Button btnUpload;



    protected void Page_Load(object sender, EventArgs e)
    {
        fullName.InnerText = Common.FullName;
        //PositionDisplay.InnerText = (GetUserDet()).Split('|')[6];
        DisplayPhoto.InnerHtml = DispPhoto();
       // GetQuestions();
    }
    
    [WebMethod]
    public static string GetQuestions1(string Q1, string Q2, string Q3, string Q4, string Q5)
    {
        string Question = " and QuestionID not in ('" + Q1 + "','" + Q2 + "','" + Q3 + "','" + Q4 + "','" + Q5 + "') ";
        UserProfileBll userBll = new UserProfileBll();

        DataTable dt = userBll.GetQuestion(Common.ConnString, Question);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetQuestions2(string Q1, string Q2, string Q3, string Q4, string Q5)
    {
        string Question = " and QuestionID not in ('" + Q1 + "','" + Q2 + "','" + Q3 + "','" + Q4 + "','" + Q5 + "') ";
        UserProfileBll userBll = new UserProfileBll();

        DataTable dt = userBll.GetQuestion(Common.ConnString, Question);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetQuestions3(string Q1, string Q2, string Q3, string Q4, string Q5)
    {
        string Question = " and QuestionID not in ('" + Q1 + "','" + Q2 + "','" + Q3 + "','" + Q4 + "','" + Q5 + "') ";
        UserProfileBll userBll = new UserProfileBll();

        DataTable dt = userBll.GetQuestion(Common.ConnString, Question);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetQuestions4(string Q1, string Q2, string Q3, string Q4, string Q5)
    {
        string Question = " and QuestionID not in ('" + Q1 + "','" + Q2 + "','" + Q3 + "','" + Q4 + "','" + Q5 + "') ";
        UserProfileBll userBll = new UserProfileBll();

        DataTable dt = userBll.GetQuestion(Common.ConnString, Question);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    [WebMethod]
    public static string GetQuestions5(string Q1, string Q2, string Q3, string Q4, string Q5)
    {
        string Question = " and QuestionID not in ('" + Q1 + "','" + Q2 + "','" + Q3 + "','" + Q4 + "','" + Q5 + "') ";
        UserProfileBll userBll = new UserProfileBll();

        DataTable dt = userBll.GetQuestion(Common.ConnString, Question);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }


    public string DispPhoto()
    {
        string html = "";

        string ext = Common.Image;

        String dirPath = Server.MapPath("~/Pictures/EmployeeImages/");
        if (Directory.GetFiles(dirPath, Common.UserName + ext).Length == 0)
        {
            html += "<center><img src ='../Pictures/EmployeeImages/user_profile.png' alt = '...' class='user-image' style='width: 200px; height: 200px;'></center> ";
               
        }
        else
        {
            html += " <center><img src='../Pictures/EmployeeImages/" + Common.UserName + ext + "' alt='...' class='user-image' style='width: 200px; height: 200px;'> </center>";
        }

        return html;
    }

    public string GetDepartMent()
    {

        return "";
    }
    public string GetPosition()
    {
        
        return "";
    }


    [WebMethod]
    public static string GetUserInfo()
    {
        UserProfileBll y = new UserProfileBll();
        return y.GetUserInfo(Common.ConnString, Common.UserID);
    }

    [WebMethod]
    public static string passwordchecking(string inputPass, string hiddenPass)
    {
       var curentpass= AES.Encrypt(inputPass, inputPass);
        var match = "";
        if (curentpass == hiddenPass)
        {
            match = curentpass;
        }


        return match;


    }

    [WebMethod]
    public static string saveProfile(string email, string Profile,  string password)
    {
        var response = "";
        UserEntity UE = new UserEntity();
        UserProfileBll y = new UserProfileBll();
       
        UE.ImageExtension = Profile;
        UE.EmailAddress = email;
        UE.ConnectionString = Common.ConnString;
        UE.UserID = Int32.Parse(Common.UserID);
        if (Profile == "")
        {
            UE.ImageExtension = Common.Image;
        }
        if (password != Common.Password && password != "")
        {
            string validba = PasswordPolicy.IsValidBa(password);
            if (validba == "")
            {
                UE.Password = AES.Encrypt(password, password);

                response = y.UpdateUserInfo(UE);
                if (response == "1")
                {
                    response = "succesful";
                }
            }
            else
            {
                response = validba;
            }
        }
        else
        {
            UE.Password = Common.Password;

            response = y.UpdateUserInfo(UE);
            if (response == "1")
            {
                response = "succesful";
            }
        }
        
        return response;
    }

    [WebMethod]
    public static string saveSecurityQandA(string securityquestions, string securityanswers)
    {
        SecretQuestion sq = new SecretQuestion();
        SecretQuestionBll sqb = new SecretQuestionBll();

        string[] questions = securityquestions.Split(';');
        string[] answers = securityanswers.Split(';');


        List<UserSecurityQuestion> UserSecurityQuestion = new List<UserSecurityQuestion>();
        List<UserSecurityAnswer> UserSecurityAnswer = new List<UserSecurityAnswer>();
        UserSecurityQuestion usq = new UserSecurityQuestion();
        UserSecurityAnswer usa = new UserSecurityAnswer();

        for (int ctr = 0; ctr < questions.Length; ctr++)
        {
            usq = new UserSecurityQuestion();
            usq.QuestionID = Convert.ToInt32(questions[ctr]);
            UserSecurityQuestion.Add(usq);
        }
        usq.getSetSecurityQuestionID = UserSecurityQuestion;

        for (int ctr = 0; ctr < answers.Length; ctr++)
        {
            usa = new UserSecurityAnswer();
            usa.Answer = answers[ctr];
            UserSecurityAnswer.Add(usa);
        }
        usa.getSetSecurityAnswer = UserSecurityAnswer;

        sq.UserID = Convert.ToInt32(Common.UserID);
        sq.GetSetSecurityQuestion = usq.getSetSecurityQuestionID;
        sq.GetSetSecurityAnswer = usa.getSetSecurityAnswer;
        sq.ConnectionString = Common.ConnString;

        sqb.SaveSecurityQandA(sq);

        return sqb.SaveSecurityQandA(sq).ToString();
    }

    //protected void Upload(object sender, EventArgs e)
    ////protected void Upload()
    //{
    //    //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Pictures/EmployeeImages/");
    //    if (UploadprofilePic.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(UploadprofilePic.FileName).ToLower();
    //        //extension = fileExtension;
    //        String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
    //        for (int i = 0; i < allowedExtensions.Length; i++)
    //        {
    //            if (fileExtension == allowedExtensions[i])
    //            {
    //                fileOK = true;
    //            }
    //        }
    //        if (fileOK)
    //        {
    //            try
    //            {
    //                UploadprofilePic.SaveAs(path + PositionDisplay.InnerText + System.IO.Path.GetExtension(UploadprofilePic.FileName).ToLower());
    //            }
    //            catch (Exception ex)
    //            {
    //                //Label1.Text = "File could not be uploaded.";
    //            }
    //        }
    //        else
    //        {
    //            Response.Write("<script>alert('Please Select .PNG, .JPEG or .JPG to upload');</script>");
    //        }
    //    }
    //    else
    //    {
    //    }
    //}
    [WebMethod]
    public static string GetSecurityQuestion()
    {
        UserProfileBll y = new UserProfileBll();

        DataTable dt = y.GetSecurityQuestion(Common.ConnString, Common.UserID);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    protected void Upload(object sender, System.EventArgs e)
    {
        Boolean fileOK = false;
        String path = Server.MapPath("~/Pictures/EmployeeImages/");
        String sigpath = Server.MapPath("~/Pictures/Signature/");

        if (UploadprofilePic.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(UploadprofilePic.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {
                try
                {
                    UploadprofilePic.SaveAs(path +Common.UserName + System.IO.Path.GetExtension(UploadprofilePic.FileName).ToLower());
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Response.Write("<script>alert('Please Select .PNG, .JPEG or .JPG to upload');</script>");
            }
        }
        else
        {

        }

        //fileOK = false;
        //if (txtsignature.HasFile)
        //{
        //    String fileExtension =
        //        System.IO.Path.GetExtension(txtsignature.FileName).ToLower();
        //    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
        //    for (int i = 0; i < allowedExtensions.Length; i++)
        //    {
        //        if (fileExtension == allowedExtensions[i])
        //        {
        //            fileOK = true;
        //        }
        //    }
        //    if (fileOK)
        //    {
        //        try
        //        {
        //            txtsignature.SaveAs(sigpath + Common.UserName + System.IO.Path.GetExtension(txtsignature.FileName).ToLower());
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('Please Select .PNG, .JPEG or .JPG for signature to upload');</script>");
        //    }
        //}
        //else
        //{

        //}

    }

    //protected void Uploadsignature(object sender, System.EventArgs e)
    //{
    //    Boolean fileOK = false;
    //    String sigpath = Server.MapPath("~/Pictures/Signature/");
    //    //for signarute upload
    //    if (txtsignature.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(txtsignature.FileName).ToLower();
    //        String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
    //        for (int i = 0; i < allowedExtensions.Length; i++)
    //        {
    //            if (fileExtension == allowedExtensions[i])
    //            {
    //                fileOK = true;
    //            }
    //        }
    //        if (fileOK)
    //        {
    //            try
    //            {
    //                txtsignature.SaveAs(sigpath + Common.UserName + System.IO.Path.GetExtension(txtsignature.FileName).ToLower());
    //            }
    //            catch (Exception ex)
    //            {
    //            }
    //        }
    //        else
    //        {
    //            Response.Write("<script>alert('Please Select .PNG, .JPEG or .JPG for signature to upload');</script>");
    //        }
    //    }
    //    else
    //    {

    //    }
    //}


}