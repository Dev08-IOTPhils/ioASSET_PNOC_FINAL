using AdvanceBll;
using AdvanceEntity;
using IOTPIEncrypto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Logging;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using ClosedXML.Excel;

public partial class Login : System.Web.UI.Page
{
    private UserEntity userEntity = new UserEntity();
    private UserBll userBll = new UserBll();
    private MiscBll miscBll = new MiscBll();
    private DateTime ssDate;
    private LicenseEncrpyto licenseEncrypto = new LicenseEncrpyto();
    private string dbselected = "";
    public static string mylicense = "";
    private string lc = LicenseChecker.CheckLicenseUAC();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetConnectionStrings();
            if (Session["UserLogin"] != null)
            {
                Response.Redirect(ResolveUrl("~/Home/Dashboard.aspx"));
            }
        }

        ExternalLoginInfo LoginInfo = Request.GetOwinContext().Authentication.GetExternalLoginInfo();
        if (LoginInfo != null)
        {

            string EmailAddress = LoginInfo.Email;
            var claims = LoginInfo.ExternalIdentity.Claims.ToList();
            string FirstName = claims[1].Value;
            string LastName = claims[2].Value;
            //Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            UserEntity ue = new UserEntity();
            UserBll ub = new UserBll();
            ue.EmailAddress = EmailAddress;
            ue.UserName = EmailAddress;
            ue.ConnectionString = "ioASSET";
            int isExist = ub.CheckEmail(ue); // check if existing email
            if (isExist != 1)
            {
                ue.UserID = 0;
                ue.FirstName = FirstName;
                ue.LastName = LastName;
                ue.EmployeeNo = "";
                ue.ImageExtension = ".";
                ue.DepartmentID = 2;
                ue.PositionID = 2;
                ue.IsCustodian = false;
                ue.Custodians = null;
                ue.IsAdministrative = false;
                ue.IsApprover = false;
                ue.IsInspector = false;
                ue.IsCertifier = false;
                ue.Active = true;
                ue.SiteID = 3;
                //ub.SaveUser(ue);
                ub.SaveUser(ue, Convert.ToInt32(Common.noofminda), Convert.ToInt32(Common.nooftsuc));
            }
            else
            {
                string result = ub.GetUserSystemInformation(ddlDatabase.SelectedItem.Text, ue);
                Session["UserLogin"] = result.Split('|')[1];
                Session["ConnString"] = ddlDatabase.SelectedItem.Text;
                Session["IPAddress"] = HttpContext.Current.Request.UserHostAddress;

                Common.UserID = result.Split('|')[0];
                Common.UserName = result.Split('|')[1];
                Common.FullName = result.Split('|')[2] + " " + result.Split('|')[3];
                //Common.RoleId = result.Split('|')[4];
                Common.Role = result.Split('|')[4];
                Common.CreatedDate = result.Split('|')[5];
                Common.Email = result.Split('|')[6];
                Common.IsApprover = result.Split('|')[7];
                Common.IsInspector = result.Split('|')[8];
                Common.IsCertifier = result.Split('|')[9];
                Common.Image = result.Split('|')[10];
                Common.CustodianID = result.Split('|')[11];
                Common.SiteID = result.Split('|')[12];
                Common.Password = result.Split('|')[13];
                Common.Administrative = result.Split('|')[14];
                Common.IsCustodian = result.Split('|')[15];
                Common.FirstLogin = result.Split('|')[16];
                Common.RoleId = result.Split('|')[18];
                Common.ConnString = ddlDatabase.SelectedItem.Text;

                //Common.Signature = result.Split('|')[15]; //added by nero 20220711

                string smtp_ = miscBll.GetSMTP(ddlDatabase.SelectedItem.Text);
                Common.SMTPhost = smtp_.Split('|')[0];
                Common.SMTPport = smtp_.Split('|')[1];
                Common.SMTPmail = smtp_.Split('|')[2];
                Common.SMTPpass = smtp_.Split('|')[3];
                Common.EmailSubject = smtp_.Split('|')[4];

                string x = miscBll.GetSystemSetting(ddlDatabase.SelectedItem.Text);
                Common.CreationApproval = x.Split('|')[0];
                Common.MergeStockApproval = x.Split('|')[1];
                Common.IssuanceApproval = x.Split('|')[2];
                Common.TransferApproval = x.Split('|')[3];
                Common.TurnOverApproval = x.Split('|')[4];
                Common.DisposalApproval = x.Split('|')[5];
                Common.GatePassApproval = x.Split('|')[6];
                Common.DataUploadApproval = x.Split('|')[7];
                Common.DatabaseRestoreApproval = x.Split('|')[8];

                Common.VerifiyCreation = x.Split('|')[9];
                Common.VerifyIssuance = x.Split('|')[10];
                Common.VerifyTransfer = x.Split('|')[11];
                Common.StockTaking = x.Split('|')[12];

                Common.RFIDPrintingCreation = x.Split('|')[13];

                Common.RequestApproval = x.Split('|')[14];
                Common.VerifyRequest = x.Split('|')[15];
                Common.VerifyReturnRequest = x.Split('|')[16];
                Common.VerifyDisposal = x.Split('|')[17];

                Common.VerificationMode = x.Split('|')[18];//added by nero 20220517
                Common.IssuanceAcknowledgement = x.Split('|')[19]; //added by nero 20220517

                Common.StockDisposalApproval = x.Split('|')[21];
                Common.StockIssuanceApproval = x.Split('|')[22];

                Common.PropertyNumberGeneration = x.Split('|')[24];
                Common.ICSValue = x.Split('|')[27];
                Common.PARValue = x.Split('|')[28];
                Common.RetaggingApproval = x.Split('|')[30];
                Common.StockRemainingQuantity = x.Split('|')[31];

                Common.CompanyInfo = miscBll.GetCompanyInfo(ddlDatabase.SelectedItem.Text);

                GetLocationTerminology();  //added by ner 20240408
                GetSiteTerminology(); //added by ner 20240408
                GetAreaTerminology(); //added by ner 20240408
                GetIOVersTerminology(); //addded by domdom 20240606
                GetContactNameTerminology(); //addded by domdom 20240607
                GetContactEmailTerminology(); //addded by domdom 20240607
                GetContactNumberTerminology(); //addded by domdom 20240607
                GetEmailBodyTerminology(); //added by domdom 20240807



                Common.IsUserAuthenticated = true;
                Common.AddToActiveUsers();
                Response.Redirect(ResolveUrl("~/Home/Dashboard.aspx"));

            }
        }

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        LoginImage.InnerHtml = GetLoginLoogo();
        //slideshowid.InnerHtml = GetImages();
        var checkpostbackEvent = Request["__EVENTTARGET"];
        //string mylicense = "alright";//GetLicense(); //"alright";//
        //try
        //{
        //    ssDate = LicenseChecker.sil.SSADate;
        //}
        //catch { }


        //if (CheckTimeBomb())
        //{
        //    mylicense = "timebombactivated";
        //}
        //if (CheckSSA() && mylicense != "timebombactivated")
        //{
        //    CheckSSADate();
        //}

        //if (checkpostbackEvent != null)
        //{
        //    if (checkpostbackEvent.ToString() == "Button1") return;
        //}

        //if (mylicense == "404")
        //{
        //    //divLicense.InnerHtml = "<center><h3 style='color:red;'>Your Trial Period is already expired. Please contact Internet of Things Philippines or <u><button style='color:green;' onclick='ActivateDialog()'> CLICK HERE</button></u> to activate.</h3></center>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:script404();", true);
        //}
        //else if (mylicense == "alright")
        //{
        //    divLicense.InnerHtml = "";
        //}
        //else if (mylicense == "Your License Key is invalid. Please contact system administrator.")
        //{
        //    //divLicense.InnerHtml = "<center><h3>" + mylicense + "</h3></center>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:invalidlicensecall();", true);
        //}
        //else if (mylicense == "No License Found.")
        //{
        //    //divLicense.InnerHtml = "<center><h3><label  style='color:red;'>NO LICENSE FOUND. <u><button style='color:green;' onclick='ActivateDialog()'> CLICK HERE</button></u> to activate.</label></h3></center>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadLicense();", true);
        //}
        //else if (mylicense == "timebombactivated")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:timebombactivated();", true);
        //}
        //else
        //{
        //    //divLicense.InnerHtml = "<center><h3>" + mylicense + "</h3></center>";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:licensedayCount('" + mylicense + "');", true);
        //}
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string lc = LicenseChecker.CheckLicense();
        lc = "";
        if (lc == "404")
        {
            lblError.Text = "<h5 style='color:red'><b>Your license has expired.</b><h5>";
        }
        else if (lc == "No License Found.")
        {
            lblError.Text = "<h5 style='color:red'><b>Please contact your solution provider to activate your license.</b><h5>";
        }

        else
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            IdentityModelEventSource.ShowPII = true;
            if (!Request.IsAuthenticated)
            {

                ExternalLoginInfo LoginInfo = Request.GetOwinContext().Authentication.GetExternalLoginInfo();
                if (LoginInfo == null)
                {
                    Request.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Login.aspx" }, new string[] { "Google" });
                }
                else
                {
                    Response.Redirect("Home/Dashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("Home/Dashboard.aspx");
            }
        }

    }

    private void CheckSSADate()
    {
        try
        {
            if (ssDate != null)
            {
                //Response.Write("<script>swal('Software Support Agreement','" + countSSADays() + "', 'warning');</script> ");
                string myssa = countSSADays();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:showSSAnotif('" + myssa.Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "');", true);

                // hfLicense.Text = countSSADays();
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
            else myout = "Software Support Agreement has lapsed.\nConsult your Software Vendor to get a new SSA activation file. Click Yes to re activate";
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

    [WebMethod]
    public static string CheckLicense()
    {
        string numberofdays = LicenseChecker.CheckLicense();
        //numberofdays = "alright";
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
            return numberofdays;
        }

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
            return "You have " + numberofdays + " day(s) left to activate this system.";
        }
    }

    public string GetImages()
    {
        string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Image/IOT"));
        string html = "";
        html += "";
        int i = 1;
        foreach (string name in files)
        {
            int j = name.Split('\\').Length;
            string[] file = name.Split('\\');
            html += "<div data-b='" + i + "'><img class='mySlides' src='../Image/IOT/" + file[j - 1] + "' style='height:500px;width:700px;'></div>";
            i++;
        }
        html += "";

        return html;
    }

    protected void GetConnectionStrings()
    {
        ddlDatabase.Items.Clear();
        foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
        {
            //ddlDatabase.Items.Clear();
            if (!(c.Name == "LocalMySqlServer" || c.Name == "LocalSqlServer" || c.Name == "aimmis1"))
            {
                ddlDatabase.Items.Add(c.Name);
            }
        }
    }

    public string GetLoginLoogo()
    {
        try
        {
            string html = "";
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(ddlDatabase.SelectedItem.Text);
            string path = AppDomain.CurrentDomain.BaseDirectory;

            if (File.Exists(path + "/Image/" + x.Split('|')[0] + "/" + x.Split('|')[6]))
            {
                html += "<span class='logo-mini'>";
                html += "<img src='Image/" + x.Split('|')[0] + "/" + x.Split('|')[6] + "' /></span>";
            }
            else
            {
                html += "<span class='logo-mini'>"
                //+ "<img src='Image/ioASSET/ioASSET.png' style='width: auto; height: 80px;' /></span>";
                + "<img src='Image/ioASSET/IOT BRAND LOGO_480px.png' /></span>";
            }

            return html;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            userEntity.UserName = txtUserId.Text;

            string en = userBll.GetPassword(ddlDatabase.SelectedItem.Text, userEntity);

            if (en == "Max Login Attempt")
            {
                lblError.Text = "<h5 style='color:red'><b>Account is Locked. You have reached the maximum no. of Login Attempt. Please contact your administrator.</b><h5>";
            }
            else if (en == "No License Found.")
            {
                lblError.Text = "<h5 style='color:red'><b>Please contact your solution provider to activate your license.</b><h5>";
            }
            else if (en == "404")
            {
                lblError.Text = "<h5 style='color:red'><b>Your trial period has expired.</b><h5>";
            }
            else if (en == "Account is Inactive")
            {
                lblError.Text = "<h5 style='color:red'><b>Your user account is currently inactive. Please contact your administrator for more information</b><h5>";
            }
            else
            {
                string dec = AES.Decrypt(en.Split('|')[0], txtPassword.Text);
                if (txtPassword.Text == "")
                {
                    lblError.Text = "<h5 style='color:red'><b>Password is Required!</b><h5>";
                }
                else
                {
                    if (dec == txtPassword.Text)
                    {
                        userEntity.Password = en.Split('|')[0];

                        if (userBll.SQLValidateUser(ddlDatabase.SelectedItem.Text, userEntity) == true)
                        {
                            string result = userBll.GetUserSystemInformation(ddlDatabase.SelectedItem.Text, userEntity);
                            Session["UserLogin"] = result.Split('|')[1];
                            Session["ConnString"] = ddlDatabase.SelectedItem.Text;
                            Session["IPAddress"] = HttpContext.Current.Request.UserHostAddress;

                            Common.UserID = result.Split('|')[0];
                            Common.UserName = result.Split('|')[1];
                            Common.FullName = result.Split('|')[2] + " " + result.Split('|')[3];
                            Common.Role = result.Split('|')[4];
                            Common.CreatedDate = result.Split('|')[5];
                            Common.Email = result.Split('|')[6];
                            Common.IsApprover = result.Split('|')[7];
                            Common.IsInspector = result.Split('|')[8];
                            Common.IsCertifier = result.Split('|')[9];
                            Common.Image = result.Split('|')[10];
                            Common.CustodianID = result.Split('|')[11];
                            Common.SiteID = result.Split('|')[12];
                            Common.Password = result.Split('|')[13];
                            Common.Administrative = result.Split('|')[14];
                            Common.IsCustodian = result.Split('|')[15];
                            Common.FirstLogin = result.Split('|')[16];
                            Common.RoleId = result.Split('|')[18];
                            Common.ConnString = ddlDatabase.SelectedItem.Text;

                            //Common.Signature = result.Split('|')[15]; //added by nero 20220711

                            string smtp_ = miscBll.GetSMTP(ddlDatabase.SelectedItem.Text);
                            Common.SMTPhost = smtp_.Split('|')[0];
                            Common.SMTPport = smtp_.Split('|')[1];
                            Common.SMTPmail = smtp_.Split('|')[2];
                            Common.SMTPpass = smtp_.Split('|')[3];
                            Common.EmailSubject = smtp_.Split('|')[4];

                            string[] uac = lc.Split('|');
                            Common.noofminda = uac[0];
                            Common.nooftsuc = uac[0];

                            string x = miscBll.GetSystemSetting(ddlDatabase.SelectedItem.Text);
                            Common.CreationApproval = x.Split('|')[0];
                            Common.MergeStockApproval = x.Split('|')[1];
                            Common.IssuanceApproval = x.Split('|')[2];
                            Common.TransferApproval = x.Split('|')[3];
                            Common.TurnOverApproval = x.Split('|')[4];
                            Common.DisposalApproval = x.Split('|')[5];
                            Common.GatePassApproval = x.Split('|')[6];
                            Common.DataUploadApproval = x.Split('|')[7];
                            Common.DatabaseRestoreApproval = x.Split('|')[8];

                            Common.VerifiyCreation = x.Split('|')[9];
                            Common.VerifyIssuance = x.Split('|')[10];
                            Common.VerifyTransfer = x.Split('|')[11];
                            Common.StockTaking = x.Split('|')[12];

                            Common.RFIDPrintingCreation = x.Split('|')[13];

                            Common.RequestApproval = x.Split('|')[14];
                            Common.VerifyRequest = x.Split('|')[15];
                            Common.VerifyReturnRequest = x.Split('|')[16];
                            Common.VerifyDisposal = x.Split('|')[17];

                            Common.VerificationMode = x.Split('|')[18];//added by nero 20220517
                            Common.IssuanceAcknowledgement = x.Split('|')[19]; //added by nero 20220517

                            Common.StockDisposalApproval = x.Split('|')[21];
                            Common.StockIssuanceApproval = x.Split('|')[22];

                            Common.PropertyNumberGeneration = x.Split('|')[24];
                            Common.ICSValue = x.Split('|')[27];
                            Common.PARValue = x.Split('|')[28];
                            Common.RetaggingApproval = x.Split('|')[30];
                            Common.StockRemainingQuantity = x.Split('|')[31];


                            Common.CompanyInfo = miscBll.GetCompanyInfo(ddlDatabase.SelectedItem.Text);


                            GetLocationTerminology();  //added by ner 20240408
                            GetSiteTerminology(); //added by ner 20240408
                            GetAreaTerminology(); //added by ner 20240408
                            GetIOVersTerminology(); //addded by domdom 20240606
                            GetContactNameTerminology(); //addded by domdom 20240607
                            GetContactEmailTerminology(); //addded by domdom 20240607
                            GetContactNumberTerminology(); //addded by domdom 20240607
                            GetEmailBodyTerminology(); //added by domdom 20240807



                            //string userpasspolicy = miscBll.GetUserPasswordPolicy(ddlDatabase.SelectedItem.Text);
                            //Common.PasswordSize = userpasspolicy.Split('|')[0];
                            //Common.WithUpperCase = userpasspolicy.Split('|')[1];
                            //Common.UpperCaseSize = userpasspolicy.Split('|')[2];
                            //Common.WithLowerCase = userpasspolicy.Split('|')[3];
                            //Common.LowerCaseSize = userpasspolicy.Split('|')[4];
                            //Common.WithNumber = userpasspolicy.Split('|')[5];
                            //Common.NumberSize = userpasspolicy.Split('|')[6];
                            //Common.WithSymbol = userpasspolicy.Split('|')[7];
                            //Common.SymbolSize = userpasspolicy.Split('|')[8];
                            //Common.WithLoginAttempt = userpasspolicy.Split('|')[9];
                            //Common.LoginAttempt = userpasspolicy.Split('|')[10];
                            //Common.UsernameOrEmail = userpasspolicy.Split('|')[11];
                            //Common.DefaultOrGenerated = userpasspolicy.Split('|')[12];
                            //Common.DefaultPassword = userpasspolicy.Split('|')[13];

                            //Common.IsUserAuthenticated = true;
                            //Common.AddToActiveUsers();

                            //Response.Redirect("Home/Dashboard.aspx");
                            if (result.Split('|')[17].ToString() == "True")
                            {
                                lblError.Text = "<h5 style='color:red'><b>User Currently Login</b><h5>";
                            }
                            else
                            {
                                Common.IsUserAuthenticated = true;
                                Common.AddToActiveUsers();
                                Response.Redirect("Home/Dashboard.aspx");
                            }
                        }
                    }
                    else if (en == "")
                    {
                        lblError.Text = "<h5 style='color:red'><b>License key is invalid. Please contact your administrator.</b><h5>";
                        //lblError.Text = "<h5 style='color:red'><b>Incorrect Password!</b><h5>";
                    }
                    else
                    {
                        userBll.Security(ddlDatabase.SelectedItem.Text, userEntity);
                        lblError.Text = "<h5 style='color:red'><b>Incorrect Password!<br> Login Attempt: " + (Convert.ToInt32(en.Split('|')[1]) + 1) + "</b><h5>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "<h5 style='color:red'><b>User Does not Exist!</b><h5>";

            if (ex.ToString().Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server."))// == "A network-related or instance-specific error occurred while establishing a connection to SQL Server.The server was not found or was not accessible.Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (pr...")
            {
                lblError.Text = "<h5 style='color:red'><b>Could Not Connect to SQL SERVER! Please Check your connection.</b><h5>";
            }
        }
    }

    [WebMethod]
    public static string btnUploadActivation(string filename)
    {
        String path = HttpContext.Current.Server.MapPath("~/Bin/");
        string returnValue = LicenseChecker.uploadactivation(path, filename);
        return returnValue;
    }
    //protected void btnUploadActivation(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Boolean fileOK = false;
    //        String path = Server.MapPath("~/Bin/");
    //        if (FileUploadActivation.HasFile)
    //        {
    //            String fileExtension = System.IO.Path.GetExtension(FileUploadActivation.FileName).ToLower();
    //            String[] allowedExtensions = { ".dat" };
    //            for (int i = 0; i < allowedExtensions.Length; i++)
    //            {
    //                if (fileExtension == allowedExtensions[i])
    //                {
    //                    fileOK = true;
    //                }
    //            }
    //            if (fileOK)
    //            {
    //                try
    //                {
    //                    if (!Directory.Exists(path))
    //                    {
    //                        Directory.CreateDirectory(path);
    //                    }
    //                    FileUploadActivation.SaveAs(path + FileUploadActivation.FileName);
    //                    string returnValue = LicenseChecker.uploadactivation(path, FileUploadActivation.FileName);
    //                    if (returnValue == "activated")
    //                    {
    //                        //Response.Write("<script>swal('Success','System Activated. Thank you!','success');</script>");
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Activated();", true);
    //                    }
    //                    else if (returnValue == "extended")
    //                    {
    //                        //Response.Write("<script>swal('Success','Temporary Lincense Extended. Thank you!','success');</script>");
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Extended();", true);
    //                    }
    //                    else if (returnValue == "temp")
    //                    {
    //                        //Response.Write("<script>swal('Success','Temporary Lincense Activated. Thank you!','success');</script>");
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:TempActivated();", true);
    //                    }
    //                    else
    //                    {
    //                        //Response.Write("<script>swal('Error','System Cannot activate. Please Contact Solution Provider','error');</script>");
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Failed();", true);
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Console.Write(ex);
    //                }
    //            }
    //            else
    //            {
    //                //Response.Write("<script>swal('Error','Please Select .dat file to upload','error');</script>");
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:InvalidFile();", true);
    //            }
    //        }
    //        else
    //        {
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}

    [WebMethod]
    public static string CheckEmail(string Email, string ConnString)
    {
        try
        {
            UserProfileBll userBll = new UserProfileBll();

            DataTable dt = userBll.GetEmail(ConnString, Email);

            return Control.MyConverter.ConvertDataTabletoJSON(dt);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string CheckAnswer(string Email, string ConnString, string Questions, string Answers)
    {
        try
        {
            UserProfileBll userBll = new UserProfileBll();

            string Ans = userBll.CheckAnswer(ConnString, Email, Questions, Answers);
            if (Ans == "1|1|1")
            {
                GenerateNewPass(Email, ConnString);
            }

            return Ans;
        }
        catch (Exception ex)
        {
            return "";
        }
    }


    [WebMethod]
    public static string GetSMTP(string connString)
    {
        MiscBll mb = new MiscBll();
        string smtp = mb.GetSMTP(connString);
        return smtp;
    }

    [WebMethod]
    public static int GenerateNewPass(string Email, string ConnString)
    {
        try
        {
            UserEntity ue = new UserEntity();
            UserProfileBll userBll = new UserProfileBll();

            int Pass_Minimum_Length = 0;
            int Upper_Case_length = 0;
            int Lower_Case_length = 0;
            int NonAlpha_length = 0;
            int Numeric_length = 0;

            string[] upasspolicy = GetUserAcccountSettings(ConnString).Split('|');
            string smtp = GetSMTP(ConnString);



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

            string Body = "";
            Body += "Dear Sir/Madam,<br><br>"
               + "You have successfully reset your password. You may now log-in using your new password: <b>" + generatedpass + "</b><br><br>"
               + "<b>PLEASE PROTECT YOUR ACCOUNT INFORMATION</b>. You assume full responsibility for maintaining the confidentiality of your Username and Password and for all transactions made using your account. <br><br>"
               + "The Management/Administrator shall not be held liable for any activities arising from access or use of your account, whether such access or use was with or without authority granted by you as account holder.<br><br> "
               + "Please be reminded also to <b>log out</b> from your account at the end of each session.";

            EmailSending.SendEmail(smtp, Email, Common.EmailSubject + " (Password Reset)", Body);
            ue.Password = AES.Encrypt(generatedpass, generatedpass);
            ue.ConnectionString = ConnString;
            ue.EmailAddress = Email;

            int returnVal = userBll.GenerateNewPass(ue);

            return returnVal;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    //[WebMethod]
    //public static string GetNewPassword(string Email, string ConnString)
    //{
    //    try
    //    {
    //        Email = "dev03@iotphils.com";
    //        MiscBll miscBllLogin = new MiscBll();
    //        string smtp_ = miscBllLogin.GetSMTP(ConnString);
    //        string smtphost = smtp_.Split('|')[0];
    //        string smtpport = smtp_.Split('|')[1];
    //        string smtpmail = smtp_.Split('|')[2];
    //        string smtppass = smtp_.Split('|')[3];
    //        string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
    //        string GeneratedPass = GeneratePassword(ConnString);
    //        if (GeneratedPass == "")
    //        {
    //            return "";
    //        }
    //        MailMessage mail = new MailMessage();
    //        mail.To.Add(Email);
    //        mail.From = new MailAddress(smtpmail);
    //        mail.Subject = "ASSET IS (Property Disposal - Approval)";
    //        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hiiiii<br><br><br>";
    //        Body += "<br><br><br><i>***This is a system generated mail. Please do not reply.***</i></font>";
    //        mail.Body = Body;
    //        SmtpClient smtp = new SmtpClient();
    //        smtp.Host = smtphost; //Or Your SMTP Server Address
    //        smtp.Port = Convert.ToInt32(smtpport);
    //        smtp.UseDefaultCredentials = false;
    //        smtp.Credentials = new System.Net.NetworkCredential
    //        (smtpmail, smtppass);
    //        //Or your Smtp Email ID and Password
    //        mail.IsBodyHtml = true;
    //        smtp.EnableSsl = true;
    //        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //        smtp.Send(mail);
    //        return "Ok";
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "sorry";
    //    }
    //}

    public static string GeneratePassword(string ConnString)
    {
        try
        {
            int Pass_Minimum_Length = 0;
            int Upper_Case_length = 0;
            int Lower_Case_length = 0;
            int NonAlpha_length = 0;
            int Numeric_length = 0;
            UserEntity userEntity = new UserEntity();
            UserBll userBLL = new UserBll();
            string[] upasspolicy = GetUserAcccountSettings(ConnString).Split('|');
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
            if (upasspolicy[1] == "Generated")
            {
                Pass_Minimum_Length = Convert.ToInt32(upasspolicy[3]);
                var generator = new PasswordGenerator(Pass_Minimum_Length,
                                              Pass_Minimum_Length,
                                              Lower_Case_length,
                                              Upper_Case_length,
                                              Numeric_length,
                                              NonAlpha_length);
                userEntity.Password = generator.Generate();
            }
            else
            {
                userEntity.Password = null;
            }
            return userEntity.Password;
        }
        catch (Exception)
        {
            return "";
        }

    }

    [WebMethod]
    public static string GetUserAcccountSettings(string ConnString)
    {
        MiscBll mb = new MiscBll();
        string upasspolicy = mb.GetUserAcccountSettings(ConnString);

        return upasspolicy;
    }


    public string Encrypt(string defaultText)
    {
        try
        { return licenseEncrypto.Encrypt("Succe55", defaultText); }
        catch (Exception)
        { return "Unable to encrypt the data"; }
    }

    public string Decrypt(string cipherText)
    {
        try
        { return licenseEncrypto.Decrypt("Succe55", cipherText); }
        catch (Exception)
        { return "Unable to decrypt the data"; }
    }

    [WebMethod]
    public static string LogOut()
    {
        Common.LogOut();
        HttpContext.Current.Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        return "../Login.aspx";
    }

    [WebMethod]
    public static string LogOut2()
    {
        Common.LogOut();
        HttpContext.Current.Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        return "Login.aspx";
    }

    protected void GetSiteTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsSite = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "Site";
        shSystemConfigurationsSite = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsSite"] = shSystemConfigurationsSite[0];


    }

    protected void GetLocationTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsLocation = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "Location";
        shSystemConfigurationsLocation = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsLocation"] = shSystemConfigurationsLocation[0];

    }

    protected void GetAreaTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsArea = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "Area";
        shSystemConfigurationsArea = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsArea"] = shSystemConfigurationsArea[0];

    }
    
    protected void GetIOVersTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsIOVers = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "ioVersion";
        shSystemConfigurationsIOVers = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsIOVers"] = shSystemConfigurationsIOVers[0];

    }

    protected void GetContactNameTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsContactName = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "ContactName";
        shSystemConfigurationsContactName = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsContactName"] = shSystemConfigurationsContactName[0];

    }

    protected void GetContactEmailTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsContactEmail = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "ContactEmail";
        shSystemConfigurationsContactEmail = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsContactEmail"] = shSystemConfigurationsContactEmail[0];

    }

    protected void GetContactNumberTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> GetContactNumberTerminology = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "ContactNumber";
        GetContactNumberTerminology = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsContactNumber"] = GetContactNumberTerminology[0];

    }

    protected void GetEmailBodyTerminology()
    {
        SystemConfigurationsManager smSystemConfigurations = new SystemConfigurationsManager();
        SystemConfigurationsCollection<SystemConfigurations> GetEmailBodyTerminology = new SystemConfigurationsCollection<SystemConfigurations>();

        smSystemConfigurations.GetTheConnectionString = ConfigurationManager.ConnectionStrings["ioASSET"].ToString();
        smSystemConfigurations.FilterByIsRecordActive = true;
        smSystemConfigurations.FilterByCode = "EmailBody";
        GetEmailBodyTerminology = smSystemConfigurations.GetData();

        smSystemConfigurations.Dispose();

        Session["shSystemConfigurationsEmail"] = GetEmailBodyTerminology[0];

    }
    //[WebMethod]

    //protected void btnTest_Click(object sender, EventArgs e)
    //{
    //    using (var workbook = new XLWorkbook())
    //    {
    //        var worksheet = workbook.Worksheets.Add("IAR");

    //        //TITLE START
    //        var IARText = worksheet.Range("A3:F3").Merge();
    //        worksheet.Cell(3, 1).Value = "INSPECTION AND ACCEPTANCE REPORT";
    //        IARText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        IARText.Style.Font.Bold = true;
    //        IARText.Style.Font.FontSize = 16;
    //        //TITLE END --------------------------------------------------------------------------

    //        //HEADER START
    //        var EntiText = worksheet.Cell("A6");
    //        var ClusText = worksheet.Cell("D6");
    //        worksheet.Cell(6, 1).Value = "Entity Name: ";
    //        worksheet.Cell(6, 4).Value = "Fund Cluster: ";
    //        EntiText.Style.Font.Bold = true;
    //        ClusText.Style.Font.Bold = true;

    //        worksheet.Cell(8, 1).Value = "Supplier: ";
    //        worksheet.Cell(8, 4).Value = "IAR No.: ";

    //        worksheet.Cell(9, 1).Value = "PO No./Date: ";
    //        worksheet.Cell(9, 4).Value = "Date: ";

    //        worksheet.Cell(10, 1).Value = "Requisitioning Office/Dept.: ";
    //        worksheet.Cell(10, 4).Value = "Invoice No.: ";

    //        worksheet.Cell(11, 1).Value = "Responsibility Office/Dept.: ";
    //        worksheet.Cell(11, 4).Value = "Date:";

    //        var AllFormRange = worksheet.Range("A8:F37");
    //        var AllFormtBorder = AllFormRange.Style.Border;
    //        AllFormtBorder.OutsideBorder = XLBorderStyleValues.Thin;

    //        var DetailsLeftRange = worksheet.Range("A8:C11");
    //        var DetailsLeftBorder = DetailsLeftRange.Style.Border;
    //        DetailsLeftBorder.OutsideBorder = XLBorderStyleValues.Thin;

    //        var DetailsRightRange = worksheet.Range("D8:F11");
    //        var DetailsRightBorder = DetailsRightRange.Style.Border;
    //        DetailsRightBorder.OutsideBorder = XLBorderStyleValues.Thin;
    //        //HEADER END --------------------------------------------------------------------------


    //        //DETAILS START ------------------------------------------------------------------------
    //        var StockPropText = worksheet.Cell("A13");
    //        StockPropText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        StockPropText.Style.Font.Bold = true;
    //        worksheet.Cell(13, 1).Value = "Stock/Property No.";

    //        var DescText = worksheet.Range("B13:C13").Merge();
    //        DescText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        DescText.Style.Font.Bold = true;
    //        worksheet.Cell(13, 2).Value = "Description";

    //        var UnitText = worksheet.Cell("D13");
    //        UnitText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        UnitText.Style.Font.Bold = true;
    //        worksheet.Cell(13, 4).Value = "Unit";

    //        var QuantText = worksheet.Range("E13:F13").Merge();
    //        QuantText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        QuantText.Style.Font.Bold = true;
    //        worksheet.Cell(13, 5).Value = "Quantity";

    //        var DetailsBodyStockRange = worksheet.Range("A14:A26");
    //        var DetailsBodyStockBorder = DetailsBodyStockRange.Style.Border;
    //        DetailsBodyStockBorder.OutsideBorder = XLBorderStyleValues.Thin;
    //        DetailsBodyStockBorder.InsideBorder = XLBorderStyleValues.Thin;

    //        var DetailsBodyDescRange = worksheet.Range("B14:C26");
    //        var DetailsBodyDescBorder = DetailsBodyDescRange.Style.Border;
    //        DetailsBodyDescBorder.TopBorder = XLBorderStyleValues.Thin;
    //        DetailsBodyDescBorder.BottomBorder = XLBorderStyleValues.Thin;

    //        var DetailsBodUnitRange = worksheet.Range("D14:D26");
    //        var DetailsBodyUnitBorder = DetailsBodUnitRange.Style.Border;
    //        DetailsBodyUnitBorder.OutsideBorder = XLBorderStyleValues.Thin;
    //        DetailsBodyUnitBorder.InsideBorder = XLBorderStyleValues.Thin;

    //        var DetailsBodyQuanRange = worksheet.Range("E14:F26");
    //        var DetailsBodyQuanBorder = DetailsBodyQuanRange.Style.Border;
    //        DetailsBodyQuanBorder.OutsideBorder = XLBorderStyleValues.Thin;
    //        DetailsBodyQuanBorder.BottomBorder = XLBorderStyleValues.Thin;

    //        //DETAILS END -------------------------------------------------------------------------------

    //        //FOOTER START

    //        var FooterLeftRange = worksheet.Range("A27:C37");
    //        var FooterLeftBorder = FooterLeftRange.Style.Border;
    //        FooterLeftBorder.OutsideBorder = XLBorderStyleValues.Thin;

    //        var FooterBottRange = worksheet.Range("A27:F27");
    //        var FooterBottBorder = FooterBottRange.Style.Border;
    //        FooterBottBorder.OutsideBorder = XLBorderStyleValues.Thin;


    //        var InspecText = worksheet.Range("A27:C27").Merge();
    //        worksheet.Cell(27, 1).Value = "INSPECTION";
    //        InspecText.Style.Font.Bold = true;
    //        InspecText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        InspecText.Style.Font.FontSize = 14;

    //        var AccepText = worksheet.Range("D27:F27").Merge();
    //        worksheet.Cell(27, 4).Value = "ACCEPTANCE";
    //        AccepText.Style.Font.Bold = true;
    //        AccepText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //        AccepText.Style.Font.FontSize = 14;

    //        var DateInspecText = worksheet.Range("A28:C29").Merge();
    //        worksheet.Cell(28, 1).Value = "Date Inspected: ";
    //        DateInspecText.Style.Font.Bold = true;


    //        var DateAcceptText = worksheet.Range("D28:F29").Merge();
    //        worksheet.Cell(28, 4).Value = "Date Received: ";
    //        DateAcceptText.Style.Font.Bold = true;

    //        var InspectCheckText = worksheet.Range("A30:C33").Merge();
    //        worksheet.Cell(30, 1).Value = "▢ Inspected, verified and found in order as to quantity and specifications";
    //        InspectCheckText.Style.Alignment.WrapText = true;

    //        var CompCheckText = worksheet.Range("D30:F31").Merge();
    //        var PartCheckText = worksheet.Range("D32:F33").Merge();

    //        worksheet.Cell(30, 4).Value = "▢ Complete";
    //        worksheet.Cell(32, 4).Value = "▢ Partial (pls. specify quantity)";

    //        var InspectOscarText = worksheet.Range("A36:C36").Merge();
    //        worksheet.Cell(36, 1).Value = "Inspection Officer/Inspection Committee";
    //        InspectOscarText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


    //        var SupCustText = worksheet.Range("D36:F36").Merge();
    //        worksheet.Cell(36, 4).Value = "Supply and/or Property Custodian";
    //        SupCustText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


    //        //FOOTER END --------------------------------------------------------------------------

    //        //ADJUST ALL CELLS ACCORDING TO TEXT INSIDE
    //        foreach (var column in worksheet.ColumnsUsed())
    //        {
    //            column.AdjustToContents();
    //        }

    //        //SAVE PATH
    //        workbook.SaveAs("C:\\SampleIAR.xlsx");
    //    }
    //}
}