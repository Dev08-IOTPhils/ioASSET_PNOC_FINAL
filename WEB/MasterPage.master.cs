using AdvanceBll;
using AdvanceEntity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private MiscBll m = new MiscBll();
    private RoleBll roleBll = new RoleBll();
    private UserBll userBll = new UserBll();
    private UserEntity ue = new UserEntity();
    private DateTime ssDate;
    private string mylicense;
    string retSite = "";
    string retLoc = "";
    string retArea = "";
    string ret = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsIOVers = (SystemConfigurations)Session["shSystemConfigurationsIOVers"];
        ret = shSystemConfigurationsIOVers.Value;
        lblMastVer.Text = ret;


        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        retSite = shSystemConfigurationsSite.Value;
        lblSiteAdduser.Text = retSite;
        lblWarehouseSite.Text = retSite;
        lblSiteCodeDialogDisp.Text = retSite + " Code: ";
        lblWarehouseAddEdit.Text = "Add/Edit " + retSite;
        lblLocDialog.Text = retSite;


        SystemConfigurations shSystemConfigurationsLocation = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        retLoc = shSystemConfigurationsLocation.Value;
        lblLocDialogDisp.Text = retLoc + ": ";
        lblLocCodeDialogDisp.Text = retLoc + " Code: ";
        lblLocDialogAddEdit.Text = "Add/Edit " + retLoc;
        lblLocAreaDialog.Text = retLoc + ": ";


        SystemConfigurations shSystemConfigurationsArea = (SystemConfigurations)Session["shSystemConfigurationsArea"];
        retArea = shSystemConfigurationsArea.Value;
        lblAreaAreaDialog.Text = retArea + ": ";
        lblAddEditAreaDialog.Text = "Add/Edit " + retArea;

        navbar_side.InnerHtml = LoadModule(Convert.ToInt32(Common.Role));
        userinfo.InnerHtml = DisplayProfile();
        dpname.InnerHtml = DisplayPictureName();

        mylicense = "alright";// GetLicense();


        loadNotification();

    }

    public static bool IsSessionExpired()
    {
        if (HttpContext.Current.Session != null)
        {
            if (HttpContext.Current.Session.IsNewSession)
            {
                string CookieHeaders = HttpContext.Current.Request.Headers["Cookie"];

                if ((null != CookieHeaders) && (CookieHeaders.IndexOf("ASP.NET_SessionId") >= 0))
                {
                    // IsNewSession is true, but session cookie exists,
                    // so, ASP.NET session is expired
                    return true;
                }
            }
        }
        // Session is not expired and function will return false,
        // could be new session, or existing active session
        return false;
    }
    public void loadNotification()
    {

        //int Countall = Int32.Parse(NotifCnT()) + Int32.Parse(GetGRPendingApproval()) + Int32.Parse(GetPropertyIssuancePendingApproval()) +
        //        Int32.Parse(GetPropertyRequestPendingApproval()) + Int32.Parse(GetPropertyTransferPendingApproval()) + Int32.Parse(GetPropertyDisposalPendingApproval()) +
        //        Int32.Parse(GetPropertyGatePassPendingApproval()) +
        //        Int32.Parse(GetPropertyDayPassPendingApproval()) +
        //        Int32.Parse(GetFileUploadPendingApproval()) +
        //        Int32.Parse(getCountGR()) + Int32.Parse(getCountIssuance()) + Int32.Parse(getCountRequest()) + Int32.Parse(getCountTransfer()) +
        //        Int32.Parse(getCountDisposal()) + Int32.Parse(getCountReturn()) + Int32.Parse(GetStockIssuancePendingApproval()) + Int32.Parse(GetStockRequestPendingApproval()) + Int32.Parse(GetStockDisposalPendingApproval());

        int Countall = Int32.Parse(NotifCnT()) + 
        Int32.Parse(GetGRPendingApproval()) +
        Int32.Parse(GetPropertyIssuancePendingApproval()) +
        //Int32.Parse(GetPropertyRequestPendingApproval()) + 
        Int32.Parse(GetPropertyTransferPendingApproval()) + 
        Int32.Parse(GetPropertyDisposalPendingApproval()) +
        //Int32.Parse(GetPropertyGatePassPendingApproval()) +
        //Int32.Parse(GetPropertyDayPassPendingApproval()) +
        Int32.Parse(GetFileUploadPendingApproval()) +
        Int32.Parse(getCountGR()) + 
        Int32.Parse(getCountIssuance()) + 
        //Int32.Parse(getCountRequest()) + 
        Int32.Parse(getCountTransfer()) +
        Int32.Parse(getCountDisposal()) + 
        Int32.Parse(getCountReturn()) + 
        Int32.Parse(GetStockIssuancePendingApproval()) + 
        Int32.Parse(GetStockRequestPendingApproval()) + 
        Int32.Parse(GetStockDisposalPendingApproval());

        Messages.InnerText = NotifCnT();
        NotifCount.InnerHtml = Countall.ToString();
        NotificationVNT.InnerHtml = "You have <strong>" + Countall + "</strong> unread messages";

        GRApproval.InnerText = GetGRPendingApproval();
        IssuanceApproval.InnerText = GetPropertyIssuancePendingApproval();
        PropertyRequestApproval.InnerText = GetPropertyRequestPendingApproval();
        PropertyTransferApproval.InnerText = GetPropertyTransferPendingApproval();
        PropertyDisposalApproval.InnerText = GetPropertyDisposalPendingApproval();
        PropertyDisposalRequestApproval.InnerText = GetPropertyDisposalRequestPendingApproval();
        PropertyGatePassApproval.InnerText = GetPropertyGatePassPendingApproval();
        PropertyDayPassApproval.InnerText = GetPropertyDayPassPendingApproval();
        FileUploadApproval.InnerText = GetFileUploadPendingApproval();
        //DatabaseRestoreApproval.InnerText = GetDatebaseRestorePendingApproval();
        //StockMergeApproval.InnerText = GetStockMergePendingApproval();
        //ReturnDateExApproval.InnerText = GetPropertyReturnDateExtensionPendingApproval();
        StockIssuanceApproval.InnerText = GetStockIssuancePendingApproval();
        StockRequestApproval.InnerText = GetStockRequestPendingApproval();
        StockDisposalApproval.InnerText = GetStockDisposalPendingApproval();

        GRVerification.InnerText = getCountGR();
        IssuanceVerification.InnerText = getCountIssuance();
        PropertyRequestVerification.InnerText = getCountRequest();
        PropertyTransferVerification.InnerText = getCountTransfer();
        PropertyDisposalVerification.InnerText = getCountDisposal();
        PropertyReturnVerification.InnerText = getCountReturn();
    }

    private bool CheckTimeBomb()
    {
        bool myout = false;
        try
        {
            if (LicenseChecker.sil.TimeBombToggle == 1 && (DateTime.Now) >= LicenseChecker.sil.TimeBombDate) myout = true;
        }
        catch { }
        return myout;
    }

    private bool CheckSSA()
    {
        bool myout = false;
        try
        {
            if (LicenseChecker.sil.SSAToggle == 1 && (DateTime.Now) >= LicenseChecker.sil.SSADate.AddMonths(-1)) myout = true;
        }
        catch { }
        return myout;
    }

    private void CheckSSADate()
    {
        try
        {
            if (ssDate != null)
            {
                //Response.Write("<script>swal('Software Support Agreement','" + countSSADays() + "', 'warning');</script> ");
                hfLicense.Text = countSSADays();
            }
        }
        catch { }
    }

    private string countSSADays()
    {
        string myout = "";
        try
        {
            int remainingdays = LicenseChecker.Converter.convertDoubleToInt((LicenseChecker.sil.SSADate.AddDays(1) - DateTime.Now).TotalDays);
            if (remainingdays > 0) myout = "Software Support Agreement has " + remainingdays + " days before lapsing.\nConsult your Software Vendor to get continuous Support and Upgrades.";
            else myout = "Software Support Agreement has lapsed.\nConsult your Software Vendor to get a new SSA-2 activation file. Click Yes to re activate";
        }
        catch { }
        return myout;
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

    #region User Info

    public string DisplayPicture()
    {
        string html = "";
        string name;

        string ext = Common.Image;

        name = Common.UserName;
        String dirPath = Server.MapPath("~/Pictures/EmployeeImages/");
        if (Directory.GetFiles(dirPath, name + ext).Length == 0)
        {
            html += "<img src ='../Pictures/EmployeeImages/user_profile.png' alt = '...' class='img-circle'>";
        }
        else
        {
            html += "<img src='../Pictures/EmployeeImages/" + name + ext + "' alt='...' class='img-circle'>";
        }

        return html;
    }

    public string DisplayProfile()
    {
        string html = "";
        string name;
        UserEntity user = new UserEntity();

        user.UserName = Common.UserID;

        string ext = Common.Image;

        String dirPath = Server.MapPath("~/Pictures/EmployeeImages/");
        if (Directory.GetFiles(dirPath, Common.UserName + ext).Length == 0)
        {
            html += "<li class='user-header'>"
                + "<img src = '../Pictures/EmployeeImages/user_profile.png' class='img-circle' alt='User Image'>"
                + "<p>" + Common.FullName
                + "<small>" + Common.CreatedDate + "</small>"
                + "</p>"
                + "</li>";
            html += "<li class='user-body'>"
               + "<p align='center'>"
               + "<small>Email: " + Common.Email + "</small>"
               + "</p>"
                + "<p align='center'>"
               + "<small>Role: " + Common.RoleId + "</small>"
               + "</p>"
               + "</li>";
            html += "<li class='user-footer'>"
                + "<div class='pull-left'>"
                //+ "<a onclick='changePassDialog()' class='btn btn-primary btn-flat'>Change Password</a>"
                //+ "<a href='../User_Management/UserProfile.aspx' class='btn btn-primary btn-flat'>Change Password</a>"
                + "<a href='../User_Management/UserProfile.aspx' class='btn btn-primary btn-flat'>Update Profile</a>"
                + "</div>"
                + "<div class='pull-right'>"
                + "<a onclick = 'LogOut()' class='btn btn-danger btn-flat'>Sign out</a>"
                + "</div>"
                + "</li>";
        }
        else
        {
            html += "<li class='user-header'>"
                + "<img src='../Pictures/EmployeeImages/" + Common.UserName + ext + "' alt='...' class='img-circle'>"
                + "<p>" + Common.FullName
                + "<small>Member since " + Common.CreatedDate + "</small>"
                + "</p>"
                + "</li>";
            html += "<li class='user-body'>"
               + "<p align='center'>"
               + "<small>Email: " + Common.Email + "</small>"
               + "</p>"
               + "</li>";
            html += "<li class='user-footer'>"
                + "<div class='pull-left'>"
                //+ "<a onclick='changePassDialog()' class='btn btn-primary btn-flat'>Change Password</a>"
                //+ "<a href='../User_Management/UserProfile.aspx' class='btn btn-primary btn-flat'>Change Password</a>"
                + "<a href='../User_Management/UserProfile.aspx' class='btn btn-primary btn-flat'>Update Profile</a>"
                + "</div>"
                + "<div class='pull-right'>"
                + "<a onclick = 'LogOut()' class='btn btn-danger btn-flat'>Sign out</a>"
                + "</div>"
                + "</li>";
        }

        return html;
    }

    public string DisplayPictureName()
    {
        string html = "";

        string ext = Common.Image;

        String dirPath = Server.MapPath("~/Pictures/EmployeeImages/");
        if (Directory.GetFiles(dirPath, Common.UserName + ext).Length == 0)
        {
            html += "<img src ='../Pictures/EmployeeImages/user_profile.png' alt = '...' class='user-image'>"
                + "<span class='hidden-xs'>" + Common.FullName + "</span>";
        }
        else
        {
            html += "<img src='../Pictures/EmployeeImages/" + Common.UserName + ext + "' alt='...' class='user-image'>"
                + "<span class='hidden-xs'>" + Common.FullName + "</span>";
        }

        return html;
    }

    #endregion User Info

    #region Module

    protected string LoadModule(int roleID)
    {
        string content = "<li class='header'>MAIN NAVIGATION</li>";
        string i = "";

        string abspath = HttpContext.Current.Request.Url.AbsolutePath;

        DataTable dtMenu = roleBll.GetRoleModuleAccess(Common.ConnString, roleID);

        for (int ctr = 0; ctr < dtMenu.Rows.Count; ctr++)
        {
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Dashboard")
            {
                i = "<i class='fa fa-home'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "System")
            {
                i = "<i class='fa fa-lock'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "User Management")
            {
                i = "<i class='fa fa-user'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Files Data")
            {
                i = "<i class='fa fa-folder'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Operation Module")
            {
                i = "<i class='fa fa-gear'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Views")
            {
                i = "<i class='fa fa-file-text'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "History Views")
            {
                i = "<i class='fa fa-file-text'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Forms Views")
            {
                i = "<i class='fa fa-file-text'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Preventive Maintenance")
            {
                i = "<i class='fa fa-wrench'></i>";
            }

            if (dtMenu.Rows[ctr]["Name"].ToString() == "About")
            {
                i = "<i class='fa fa-info'></i>";
            }

            if (dtMenu.Rows[ctr]["Name"].ToString() == "Approvals")
            {
                i = "<i class='fa fa-thumbs-up'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "My Request")
            {
                i = "<i class='fa fa-list'></i>";
            }
            if (dtMenu.Rows[ctr]["Name"].ToString() == "Custodian Operation Module")
            {
                i = "<i class='fa fa-gear'></i>";
            }
            ////////////////
            string sample = dtMenu.Rows[ctr]["URL"].ToString();
            if (abspath.Contains(dtMenu.Rows[ctr]["URL"].ToString().Split('/')[0]))
            {
                if (dtMenu.Rows[ctr]["Name"].ToString() == "About")
                {
                    content += "<li ><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtMenu.Rows[ctr]["URL"].ToString())) + "\">" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span></a>";
                    content += "</li>";
                }
                else if (dtMenu.Rows[ctr]["Name"].ToString() == "Approvals")
                {
                    content += "<li ><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtMenu.Rows[ctr]["URL"].ToString())) + "\">" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span></a>";
                    content += "</li>";
                }
                else if (dtMenu.Rows[ctr]["Name"].ToString() == "Dashboard")
                {
                    content += "<li ><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtMenu.Rows[ctr]["URL"].ToString())) + "\">" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span></a>";
                    content += "</li>";
                }
                else
                {
                    content += "<li class='treeview menu-open'><a>" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                    content += "<ul class='treeview-menu' style='display:block;'>";
                    if (roleBll.CountSubRoleModuleAccess(Common.ConnString, roleID, int.Parse(dtMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
                    {
                        content += GetSubRoleModuleAccess(Common.ConnString, roleID, int.Parse(dtMenu.Rows[ctr]["ModuleId"].ToString()));
                    }
                    content += "</ul>";
                    content += "</li>";
                }
            }
            else
            {
                if (dtMenu.Rows[ctr]["Name"].ToString() == "About")
                {
                    content += "<li ><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtMenu.Rows[ctr]["URL"].ToString())) + "\">" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span></a>";
                    content += "</li>";
                }
                else if (dtMenu.Rows[ctr]["Name"].ToString() == "Approvals")
                {
                    content += "<li ><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtMenu.Rows[ctr]["URL"].ToString())) + "\">" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span></a>";
                    content += "</li>";
                }
                else if (dtMenu.Rows[ctr]["Name"].ToString() == "Dashboard")
                {
                    content += "<li ><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtMenu.Rows[ctr]["URL"].ToString())) + "\">" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span></a>";
                    content += "</li>";
                }
                else
                {
                    content += "<li class='treeview'><a>" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                    content += "<ul class='treeview-menu'>";
                    if (roleBll.CountSubRoleModuleAccess(Common.ConnString, roleID, int.Parse(dtMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
                    {
                        content += GetSubRoleModuleAccess(Common.ConnString, roleID, int.Parse(dtMenu.Rows[ctr]["ModuleId"].ToString()));
                    }
                    content += "</ul>";
                    content += "</li>";
                }
            }
            ////////////////

            //content += "<li class='treeview'><a>" + i + "<span>" + dtMenu.Rows[ctr]["Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";

            //if (roleBll.CountSubRoleModuleAccess(roleID, int.Parse(dtMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
            //{
            //    content += GetSubRoleModuleAccess(roleID, int.Parse(dtMenu.Rows[ctr]["ModuleId"].ToString()));
            //}
            //content += "</li>";
            i = "";
        }
        //content += "</ul>";
        Common.ModuleData = content;
        return content;
    }

    protected string GetSubRoleModuleAccess(string ConnString, int roleID, int parentModuleID)
    {
        string contentsub = "";
        string abspath = HttpContext.Current.Request.Url.AbsolutePath;

        DataTable dtSubMenu = roleBll.GetSubRoleModuleAccess(ConnString, roleID, parentModuleID);

        for (int ctr = 0; ctr < dtSubMenu.Rows.Count; ctr++)
        {
            if (abspath == "/" + dtSubMenu.Rows[ctr]["URL"].ToString())
            {
                if (Common.UserName == "superadmin" || Common.UserName == "iotdevonly" || Common.UserName == "dev08")
                {
                    contentsub += "<li class='active'><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtSubMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtSubMenu.Rows[ctr]["URL"].ToString())) + "\"><i class='fa fa-circle-o'></i>" + dtSubMenu.Rows[ctr]["Name"].ToString() + "</a>";

                    if (roleBll.CountSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
                    {
                        contentsub += GetSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString()));
                    }
                    contentsub += "</li>";
                }
                else
                {
                    if (dtSubMenu.Rows[ctr]["Name"].ToString() == "Module")
                    {
                    }
                    else
                    {
                        contentsub += "<li class='active'><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtSubMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtSubMenu.Rows[ctr]["URL"].ToString())) + "\"><i class='fa fa-circle-o'></i>" + dtSubMenu.Rows[ctr]["Name"].ToString() + "</a>";

                        if (roleBll.CountSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
                        {
                            contentsub += GetSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString()));
                        }
                        contentsub += "</li>";
                    }
                }
            }
            else
            {
                if (Common.UserName == "superadmin" || Common.UserName == "iotdevonly" || Common.UserName == "dev08")
                {
                    contentsub += "<li><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtSubMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtSubMenu.Rows[ctr]["URL"].ToString())) + "\"><i class='fa fa-circle-o'></i>" + dtSubMenu.Rows[ctr]["Name"].ToString() + "</a>";

                    if (roleBll.CountSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
                    {
                        contentsub += GetSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString()));
                    }
                    contentsub += "</li>";
                }
                else
                {
                    if (dtSubMenu.Rows[ctr]["Name"].ToString() == "Module")
                    {
                    }
                    else
                    {
                        contentsub += "<li><a href=\"" + Page.ResolveClientUrl((String.IsNullOrEmpty(dtSubMenu.Rows[ctr]["URL"].ToString()) ? "#" : dtSubMenu.Rows[ctr]["URL"].ToString())) + "\"><i class='fa fa-circle-o'></i>" + dtSubMenu.Rows[ctr]["Name"].ToString() + "</a>";

                        if (roleBll.CountSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString())) > 0)
                        {
                            contentsub += GetSubRoleModuleAccess(ConnString, roleID, int.Parse(dtSubMenu.Rows[ctr]["ModuleId"].ToString()));
                        }
                        contentsub += "</li>";
                    }
                }
            }
        }
        return contentsub;
    }

    #endregion Module

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
            userEntity.ConnectionString = Common.ConnString;

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



    #region Notification

    public static string NotifCnT()
    {
        var res = "";

        NotificationBll x = new NotificationBll();
        res = x.GetNotifCount(Common.ConnString, Common.UserID).ToString();

        return res;
    }

    //[WebMethod]
    //public string getTOp5List()
    //{
    //    string res = "";
    //    //GetNotification
    //    NotificationBll x = new NotificationBll();

    //    try
    //    {
    //        DataTable dt = x.GetNotification(Common.ConnString, Common.UserID, "top5");
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                //< li >
    //                //    < a href = "../Operation_Module/PropertyAcknowledgement.aspx" >

    //                //            < i class="fa fa-users text-aqua"></i>5 new members joined today
    //                //        </a>
    //                //</li>
    //                string link = dt.Rows[i]["Link"].ToString();
    //                string notifID = dt.Rows[i]["NotificationID"].ToString();

    //                res += "\n<div role='row' class='NewNotif' > \n";
    //                if (dt.Rows[i]["IsRead"].ToString() == "False")
    //                {
    //                    res += "<div role='col' class='pull-left' style='width:60px;height:60px;text-align:center'> <span class='NotifImage'></span><img src='../pictures/employeeimages/user_profile.png' class='user-image img-circle' style='width:40px; height: 40px; '></div>"
    //                        + "<a  Onclick='readNotification(" + notifID +")' style='color: black; ' href = '" + link + "' value = '" + notifID + "' > <div role='col'>\n"
    //                        + "<div ><strong>" + dt.Rows[i]["Notif"].ToString() + "</strong></div>";
    //                }
    //                else
    //                {
    //                    res += "<div role='col' class='pull-left' style='width:60px;height:60px;text-align:center'> <span class='NotifImage'></span><img src='../pictures/employeeimages/user_profile.png' class='user-image img-circle' style='width:40px; height: 40px; '></div>"
    //                        + "<a Onclick='readNotification("  + notifID + ")' style='color: gray; ' href = '" + link + "' value = '" + notifID + "' > <div role='col'>\n"
    //                        +"<div >" + dt.Rows[i]["Notif"].ToString() + "</div>";
    //                }


    //                res += "<div><i style='font-size:13px'>" + dt.Rows[i]["Date"].ToString() + "</i></div>"
    //                     + "</div></a><br></div>";

    //                //res += dt.Rows[i]["NotificationID"].ToString() + "|";
    //                //res += dt.Rows[i]["ToUser"].ToString() + "|";
    //                //res += dt.Rows[i]["Name"].ToString() + "|";
    //                //res += dt.Rows[i]["Notif"].ToString() + "|";
    //                //res += dt.Rows[i]["Date"].ToString() + "|";
    //                //res += dt.Rows[i]["IsDone"].ToString() + "|";
    //                //res += dt.Rows[i]["Image"].ToString() + "|";
    //                //res += dt.Rows[i]["Trx"].ToString() + "|";
    //                //res += dt.Rows[i]["Link"].ToString() + "|";
    //                //res += dt.Rows[i]["IsRead"].ToString();
    //                //res += "\n";
    //            }

    //            return res;
    //        }
    //        else { return ""; }

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }       
    //}


    //[WebMethod]
    //public static string GetNotification()
    //{
    //    var res = "";

    //    NotificationBll x = new NotificationBll();
    //    DataTable dt = x.GetNotification(Common.ConnString, Common.UserID);

    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            res += dt.Rows[i]["NotificationID"].ToString() + "|";
    //            res += dt.Rows[i]["ToUser"].ToString() + "|";
    //            res += dt.Rows[i]["Name"].ToString() + "|";
    //            res += dt.Rows[i]["Notif"].ToString() + "|";
    //            res += dt.Rows[i]["Date"].ToString() + "|";
    //            res += dt.Rows[i]["IsDone"].ToString() + "|";
    //            res += dt.Rows[i]["Image"].ToString() + "|";
    //            res += dt.Rows[i]["Trx"].ToString() + "|";
    //            res += dt.Rows[i]["Link"].ToString() + "|";
    //            res += dt.Rows[i]["IsRead"].ToString();
    //            res += "\n";
    //        }

    //        return res;
    //    }
    //    else { return ""; }
    //}

    //[WebMethod]
    //public static string ReadNotification(int NotifID)
    //{

    //    NotificationBll x = new NotificationBll();
    //    var res = "";

    //    try
    //    {
    //        res = x.ReadNotification(Common.ConnString, NotifID, Common.UserID);

    //        return res;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //         throw;
    //    }
    //}

    public static string getCountGR()
    {
        PropertyBll x = new PropertyBll();
        return x.GetGRPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountIssuance()
    {
        PropertyBll x = new PropertyBll();
        return x.GetIssuancePropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountTransfer()
    {
        PropertyBll x = new PropertyBll();
        return x.GetTransferPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountRequest()
    {
        PropertyBll x = new PropertyBll();
        return x.GetRequestPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountReturn()
    {
        PropertyBll x = new PropertyBll();
        return x.GetReturnPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountDisposal()
    {
        PropertyBll x = new PropertyBll();
        return x.GetDisposalPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }



    public static string GetGRPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetGRPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyIssuancePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyIssuancePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyRequestPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyRequestPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyTransferPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyTransferPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }


    public static string GetPropertyDisposalRequestPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyDisposalRequestPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyDisposalPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyDisposalPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyGatePassPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyGatePassPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyDayPassPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyDayPassPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetFileUploadPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetFileUploadPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetDatebaseRestorePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetDatebaseRestorePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockMergePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockMergePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }
    public static string GetPropertyReturnDateExtensionPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyReturnDateExtensionPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockIssuancePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockIssuancePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockRequestPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockRequestPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockDisposalPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockDisposalPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }
    #endregion

}