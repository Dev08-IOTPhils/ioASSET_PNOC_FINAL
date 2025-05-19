using AdvanceBll;
using AdvanceEntity;
using System;
using System.IO;
using System.Web.Services;

public partial class Home_Home : System.Web.UI.Page
{
    private MiscBll m = new MiscBll();
    private UserBll u = new UserBll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Common.IsUserAuthenticated)
            Response.Redirect(@"..\Login.aspx");
        announcement.InnerHtml = GetHomePageImage();

        //string mylicense = GetLicense();

        //if (mylicense == "404")
        //{
        //    Response.Write("<script>swal('Warning', 'Your Trial Period is already expired. Please contact Internet of Things Philippines.', 'warning');</script> ");
        //    Common.LogOut();
        //    Response.Redirect(@"..\Login.aspx");
        //}
        //else if (mylicense == "alright")
        //{
        //    if (Common.Password == m.GetUserDefaultPassword(Common.ConnString))
        //    {
        //        Response.Write("<script>swal('Warning', 'You are using the default password. Please change your password for security purposes.', 'warning');</script> ");
        //    }
        //}
        //else if (mylicense == "invalid")
        //{
        //    Response.Write("<script>swal('Warning'," + mylicense + ", 'warning');</script> ");
        //    Common.LogOut();
        //    Response.Redirect(@"..\Login.aspx");
        //}
        //else if (mylicense == "No License Found.")
        //{
        //    Response.Write("<script>swal('Warning'," + mylicense + ", 'warning');</script> ");
        //    Common.LogOut();
        //    Response.Redirect(@"..\Login.aspx");
        //}
        //else
        //{
        //    Response.Write("<script>swal('Warning'," + mylicense + ", 'warning');</script> ");
        //}
    }

    public string GetLicense()
    {
        string numberofdays = LicenseChecker.CheckLicense();
        if (numberofdays == "404")
        {
            return "404";
        }
        else if (numberofdays == "alright")
        {
            return "alright";
        }
        else if (numberofdays == "invalid")
        {
            return "Your License Key is invalid. Please contact system administrator.";
        }
        else if (numberofdays == "No License Found.")
        {
            return "No License Found.";
        }
        else
        {
            return "You have <label style='color:red;'>" + numberofdays + " day(s)</label> left to activate this system. Please contact your Solution Provider. <u><button style='color:green;' onclick='ActivateDialog()'> CLICK HERE</button></u> to activate.";
        }
    }

    [WebMethod]
    public static string GetHomePageImage()
    {
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string announcement = miscBll.GetHomePageImage(Common.ConnString);
        string html = "";
        string path = AppDomain.CurrentDomain.BaseDirectory;

        if (File.Exists(path + "/Image/AssetIS/" + announcement))
        {
            html += "<br/><br/><center><img src='../Image/AssetIS/" + announcement + "' width='80%'></center>";
        }
        else
        {
            html += "<br/><br/><center><img src='../Image/Internet of Things Philippines/Internet of Things Philippines.png' width='100%'></center>";
        }
        //html += "<br/><br/><center><img src='../Image/LRTA/" + announcement + "' width='50%'></center>";

        return html;
    }

    [WebMethod]
    public static string ChangePassword(string password, string newpassword, string confirmpassword)
    {
        string response = "";
        UserEntity userEntity = new UserEntity();
        UserBll userBLL = new UserBll();
        string pass = AES.Encrypt(newpassword, newpassword);

        if (PasswordPolicy.IsValid(newpassword) == true)
        {
            userEntity.Password = password;
            userEntity.NewPassword = pass;
            userEntity.ConfirmPassword = confirmpassword;
            userEntity.UserName = Common.UserName;

            try
            {
                string en = userBLL.GetPassword(Common.ConnString, userEntity);

                string dec = AES.Decrypt(en, password);
                if (dec == password)
                {
                    userBLL.ChangePassword(Common.ConnString, userEntity);

                    response = "success";
                }
                else
                {
                    response = "Incorrect Password";
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "<h5 style='color:red'><b>User Does not Exist!</b><h5>";
            }
        }
        else
        {
            response = "Password must contain:\n\n*Upper Case\n*Lower Case\n*Numeric characters\n*Non-alphanumeric character";
        }

        return response;
    }
}