using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using AdvanceBll;
using AdvanceEntity;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Runtime.InteropServices;


using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using QRCoder;

public partial class User_Management_User : BasePage
{
    public static string retEmail = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsEmail = (SystemConfigurations)Session["shSystemConfigurationsEmail"];
        retEmail = shSystemConfigurationsEmail.Value;
        //lbldisplayEmail.Text = retEmail + ":";

        CheckAccess();
        //UserList.InnerHtml = GetUser("", 1);
        // GetDepartmentList();
        //GetIntegratorList();
        //GetFromWarehouse();
    }

    #region DropDownList

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("User_Management/User.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string GetIntegratorList()
    {
        CustodianBll custodianBll = new CustodianBll();
        DataTable dt = custodianBll.GetCustodianList(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetDepartmentID(int id)
    {
        DepartmentBll departmentBll = new DepartmentBll();
        DepartmentEntity departmentEntity = new DepartmentEntity();

        departmentEntity.PositionID = id;
        departmentEntity.ConnectionString = Common.ConnString;

        return departmentBll.GetDepartmentID(departmentEntity);
    }


    #endregion

    #region Methods
    [WebMethod]
    public static int CheckEmpNo(int userid, string empno)
    {
        UserEntity x = new UserEntity();
        UserBll y = new UserBll();

        x.UserID = userid;
        x.EmployeeNo = empno;
        x.ConnectionString = Common.ConnString;

        return y.CheckEmpNo(x);
    }
    [WebMethod]
    public static string GetUser()
    {
        UserBll userBLL = new UserBll();
        string html = string.Empty;
        List<UserEntity> JsonItemList = new List<UserEntity>();
        try
        {
            DataTable dt = userBLL.GetUser(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserEntity x = new UserEntity();
                    x.Name = dt.Rows[i]["LastName"].ToString() + ", " + dt.Rows[i]["FirstName"].ToString();
                    x.EmployeeNo = dt.Rows[i]["EmployeeNo"].ToString();
                    x.EmailAddress = dt.Rows[i]["EmailAddress"].ToString();
                    x.RoleName = dt.Rows[i]["RoleName"].ToString();
                    x.Site = dt.Rows[i]["SiteName"].ToString();
                    x.Custodians = "<img " + ((bool.Parse(dt.Rows[i]["IsCustodian"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";
                    x.Administrative = "<img " + ((bool.Parse(dt.Rows[i]["IsAdministrative"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";
                    x.Approver = "<img " + ((bool.Parse(dt.Rows[i]["IsApprover"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";
                    //x.Inspector = "<img " + ((bool.Parse(dt.Rows[i]["IsInspector"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";
                    //x.Certifier = "<img " + ((bool.Parse(dt.Rows[i]["IsCertifier"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";

                    if (Common.RoleId == "1")
                    {
                        if (DeleteAccess == "0")
                        {
                            x.Action = "<center><button class='btn btn-xs btn-warning' type='button' title='Force Sign out User' onclick=\"ForceLogOut('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-sign-out'></span></button> "
                                + "<button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["EmailAddress"] + "','" + dt.Rows[i]["UserName"] + "')\"><span class='fa fa-exchange'></span></button> "
                              + "<button class='btn btn-xs btn-warning'  type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> </center>";
                        }
                        else
                        {
                            x.Action = "<center><button class='btn btn-xs btn-warning' type='button' title='Force Sign out User' onclick=\"ForceLogOut('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-sign-out'></span></button> "
                                + "<button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["EmailAddress"] + "','" + dt.Rows[i]["UserName"] + "')\"><span class='fa fa-exchange'></span></button> "
                              + "<button class='btn btn-xs btn-warning'  type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> "
                              + "<button class='btn btn-xs btn-danger btn_DeleteUser btn_DeleteUsers'  type='button' title='Delete User' onclick=\"deleteUser('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></center>";
                        }

                    }
                    else
                    {
                        if (dt.Rows[i]["UserID"].ToString() == "1")
                        {
                            if (DeleteAccess == "0")
                            {
                                x.Action = "<center><button class='btn btn-xs btn-warning' type='button' title='Force Sign out User' onclick=\"ForceLogOut('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-sign-out'></span></button> "
                                    + "<button class='btn btn-xs btn-danger' disabled type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["EmailAddress"] + "','" + dt.Rows[i]["UserName"] + "')\"><span class='fa fa-exchange'></span></button> "
                                    + "<button class='btn btn-xs btn-warning' disabled type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                            }
                            else
                            {
                                x.Action = "<center><button class='btn btn-xs btn-warning' type='button' title='Force Sign out User' onclick=\"ForceLogOut('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-sign-out'></span></button> "
                                    + "<button class='btn btn-xs btn-danger' disabled type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["EmailAddress"] + "','" + dt.Rows[i]["UserName"] + "')\"><span class='fa fa-exchange'></span></button> "
                                    + "<button class='btn btn-xs btn-warning' disabled type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> "
                                    + "<button class='btn btn-xs btn-danger btn_DeleteUser' disabled type='button' title='Delete User' onclick=\"deleteUser('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></center>";
                            }
                        }
                        else
                        {
                            if (DeleteAccess == "0")
                            {
                                x.Action = "<center><button class='btn btn-xs btn-warning' type='button' title='Force Sign out User' onclick=\"ForceLogOut('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-sign-out'></span></button> "
                                    + "<button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["EmailAddress"] + "','" + dt.Rows[i]["UserName"] + "')\"><span class='fa fa-exchange'></span></button> "
                                    + "<button class='btn btn-xs btn-warning' type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> </center>";
                            }
                            else
                            {
                                x.Action = "<center><button class='btn btn-xs btn-warning' type='button' title='Force Sign out User' onclick=\"ForceLogOut('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-sign-out'></span></button> "
                                    + "<button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["EmailAddress"] + "','" + dt.Rows[i]["UserName"] + "')\"><span class='fa fa-exchange'></span></button> "
                                    + "<button class='btn btn-xs btn-warning' type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> "
                                    + "<button class='btn btn-xs btn-danger btn_DeleteUsers' type='button' title='Delete User' onclick=\"deleteUser('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></center>";
                            }
                        }
                    }
                    JsonItemList.Add(x);

                }

                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList);


                return html;
            }

            else { return ""; }

        }
        catch (Exception ex)
        {
            return "";
        }

    }
    [WebMethod]
    public static string ForceLogOut(int Userid)
    {
        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = Userid;
        userEntity.UserName = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;

        return userBll.ForceLogOut(userEntity).ToString();
    }
  

    [WebMethod]
    public static string GetUserByID(int id)
    {
        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = id;
        userEntity.ConnectionString = Common.ConnString;
        return userBll.GetUserByID(userEntity);
    }

    [WebMethod]
    public static string DeleteUser(int id)
    {
        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = id;
        userEntity.CreatedBy = Common.UserID;
        userEntity.ConnectionString = Common.ConnString;

        return userBll.DeleteUser(userEntity).ToString();

    }

    //DICTchange
    [WebMethod]
    public static string ResetPassword(int id, string LastName, string FirstName, string Email, string UserName)
    {
        int Pass_Minimum_Length = 0;
        int Upper_Case_length = 0;
        int Lower_Case_length = 0;
        int NonAlpha_length = 0;
        int Numeric_length = 0;
        string[] upasspolicy = GetUserAcccountSettings().Split('|');

        if (upasspolicy[4] == "True")
        {
            Upper_Case_length = Convert.ToInt32(upasspolicy[5]);
        }
        if (upasspolicy[6] == "True")
        {
            Lower_Case_length = Convert.ToInt32(upasspolicy[7]);
        }
        if (upasspolicy[8] == "True")
        {
            Numeric_length = Convert.ToInt32(upasspolicy[9]);
        }
        if (upasspolicy[10] == "True")
        {
            NonAlpha_length = Convert.ToInt32(upasspolicy[11]);
        }

        Pass_Minimum_Length = Convert.ToInt32(upasspolicy[3]);

        string generatedpass = "";
        var generator = new PasswordGenerator(Pass_Minimum_Length,
                                      Pass_Minimum_Length,
                                      Lower_Case_length,
                                      Upper_Case_length,
                                      Numeric_length,
                                      NonAlpha_length);
        generatedpass = generator.Generate();


        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = id;
        userEntity.CreatedBy = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;
        userEntity.Password = AES.Encrypt(generatedpass, generatedpass);

        string PassReset = userBll.ResetPassword(userEntity).ToString();

        try
        {
            if (PassReset == "1")
            {
                string smtp = GetSMTP();
                string Body = "";
                Body += "Hello Sir/Maam " + LastName + ", " + FirstName + ",<br><br>"
                   + "You may now log-in to "+ retEmail + " using these credentials : <br/><br/> Username: <b>" + UserName + "</b><br>Password: <b>" + generatedpass + "</b><br><br>"
                   + "<b>PLEASE PROTECT YOUR ACCOUNT INFORMATION</b>. You assume full responsibility for maintaining the confidentiality of your Username and Password and for all transactions made using your account. <br><br>"
                   + "The Management/Administrator shall not be held liable for any activities arising from access or use of your account, whether such access or use was with or without authority granted by you as account holder.<br><br> "
                   + "Please be reminded also to <b>log out</b> from your account at the end of each session.";

                EmailSending.SendEmail(smtp, Email, Common.EmailSubject + " (Password)", Body);
            }
        }
        catch (Exception)
        {
            PassReset = "Email";
        }


        return PassReset;
        //return GetUser(pageNo);
    }
    #endregion

}