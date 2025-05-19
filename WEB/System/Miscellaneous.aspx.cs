using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Security;
using System.IO;

public partial class System_Miscellaneous : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginImages.InnerHtml = GetLoginImages();
        GetThreshold();
        GetCompanyInformation();
        //GetDBDirectory();
        GetUploadFIlesDirectoryPath();
        GetDefaultPassword();
        GetMail();
        //ConversionMonth();
        //EndMonth();
        //TransactionMonth();
    }

    //private void ConversionMonth()
    //{
    //    ddlConversionMonth.Items.Clear();
    //    ddlConversionMonth.Items.Insert(0, "--- Select Conversion Month ---");
    //    ddlConversionMonth.Items.Insert(1, "January");
    //    ddlConversionMonth.Items.Insert(2, "February");
    //    ddlConversionMonth.Items.Insert(3, "March");
    //    ddlConversionMonth.Items.Insert(4, "April");
    //    ddlConversionMonth.Items.Insert(5, "May");
    //    ddlConversionMonth.Items.Insert(6, "June");
    //    ddlConversionMonth.Items.Insert(7, "July");
    //    ddlConversionMonth.Items.Insert(8, "August");
    //    ddlConversionMonth.Items.Insert(9, "September");
    //    ddlConversionMonth.Items.Insert(10, "October");
    //    ddlConversionMonth.Items.Insert(11, "November");
    //    ddlConversionMonth.Items.Insert(12, "December");

    //}

    //private void EndMonth()
    //{
    //    ddlEndMonth.Items.Clear();
    //    ddlEndMonth.Items.Insert(0, "--- Select End Month ---");
    //    ddlEndMonth.Items.Insert(1, "January");
    //    ddlEndMonth.Items.Insert(2, "February");
    //    ddlEndMonth.Items.Insert(3, "March");
    //    ddlEndMonth.Items.Insert(4, "April");
    //    ddlEndMonth.Items.Insert(5, "May");
    //    ddlEndMonth.Items.Insert(6, "June");
    //    ddlEndMonth.Items.Insert(7, "July");
    //    ddlEndMonth.Items.Insert(8, "August");
    //    ddlEndMonth.Items.Insert(9, "September");
    //    ddlEndMonth.Items.Insert(10, "October");
    //    ddlEndMonth.Items.Insert(11, "November");
    //    ddlEndMonth.Items.Insert(12, "December");

    //}

    //private void TransactionMonth()
    //{
    //    ddlTransactionMonth.Items.Clear();
    //    ddlTransactionMonth.Items.Insert(0, "--- Select Transaction Month ---");
    //    ddlTransactionMonth.Items.Insert(1, "January");
    //    ddlTransactionMonth.Items.Insert(2, "February");
    //    ddlTransactionMonth.Items.Insert(3, "March");
    //    ddlTransactionMonth.Items.Insert(4, "April");
    //    ddlTransactionMonth.Items.Insert(5, "May");
    //    ddlTransactionMonth.Items.Insert(6, "June");
    //    ddlTransactionMonth.Items.Insert(7, "July");
    //    ddlTransactionMonth.Items.Insert(8, "August");
    //    ddlTransactionMonth.Items.Insert(9, "September");
    //    ddlTransactionMonth.Items.Insert(10, "October");
    //    ddlTransactionMonth.Items.Insert(11, "November");
    //    ddlTransactionMonth.Items.Insert(12, "December");

    //}


    #region Threshold Setting
    [WebMethod]
    public static void SetThreshold(int maintenancethreshold)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.AgingThreshold = 0;// agingthreshold;
        miscEntity.HandHeldThreshold = 0;// handheldthreshold;
        miscEntity.MaintenanceThreshold = maintenancethreshold;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetThreshold(miscEntity);
    }

    public void GetThreshold()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select * from tbl_Threshold";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            //txtAgingThreshold.Text = dr1[1].ToString();
            //txtHandheldThreshold.Text = dr1[2].ToString();
            txtMaintenanceThreshold.Text = dr1[3].ToString();
        }
        else
        {

        }
        connection.Close();
    }

    [WebMethod]
    public static string RetrieveThreshold()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.RetrieveThreshold(Common.ConnString);
    }
    #endregion


    #region Home Page Image Setting
    //protected void Upload(object sender, EventArgs e)
    //{
    //    //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Image/AssetIS/");
    //    if (fuHomePage.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(fuHomePage.FileName).ToLower();
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
    //                fuHomePage.SaveAs(path + fuHomePage.FileName);
    //            }
    //            catch (Exception ex)
    //            {
    //                //Label1.Text = "File could not be uploaded.";
    //            }
    //        }
    //        else
    //        {
    //            //Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //            //swal('Error', 'Please select an image to upload.', 'warning');
    //            //Response.Write("<script>swal('Please select an image to upload.');</script>");
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSelect();", true);
    //        }
    //    }
    //    else
    //    {
    //        //Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //        //Response.Write("<script>swal('Error', 'Please select an image to upload.', 'warning');</script>");
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSelect();", true);
    //    }
    //}

    //[WebMethod]
    //public static string SaveHomePageImage(string homepageimage)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();

    //    miscEntity.HomePageImage = homepageimage;
    //    miscEntity.ConnectionString = Common.ConnString;
    //    miscEntity.CreatedBy = Common.UserName;

    //    return miscBll.SaveHomePageImage(miscEntity).ToString();
    //}


    #endregion

    #region Carousel
    [WebMethod]
    public static string GetLoginImages()
    {
        string[] filesGR = new string[0];
        try
        {
            filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../Image/ioAsset/Carousel"));
        }
        catch
        {

        }
        string html = "";

        html = " <div class='table-responsive' style='height: 250px;'><table border='0'><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr>";
        foreach (string name in filesGR)
        {
            int j = name.Split('\\').Length;
            string[] file = name.Split('\\');
            html += "<td><img class='zoom' src='../Image/ioASSET/Carousel/" + file[j - 1] + "' style='height: 100px; width:130px'><br><center><button type='button' id='" + file[j - 1] + "' onclick=\"DeleteImageClick('" + file[j - 1] + "')\" class='btn btn-danger btn-xs' Text='Remove'>Remove</button></center></td>";//<label>" + file[j - 1] + "</label></a><br>";
        }
        html += "</tr></table></div>";
        return html;
    }


    [WebMethod]
    public static string DeleteFile(string filename)
    {
        try
        {

            string strPhysicalFolder = System.Web.HttpContext.Current.Server.MapPath("../Image/ioASSET/Carousel/");

            string strFileFullPath = strPhysicalFolder + filename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
                return "file deleted.";
            }
            else
            {
                return "file not found.";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    #endregion


    #region Company Information Setting
    [WebMethod]
    public static void SetCompanyInformation(string companyname, string tradingname, string contactperson, string contactnumber, string emailaddress, string address, string companylogo, string formlogo, string webicon)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();
        string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
        miscEntity.CompanyName = companyname;
        miscEntity.TradingName = tradingname;
        miscEntity.ContactPerson = contactperson;
        miscEntity.ContactNumber = contactnumber;
        miscEntity.EmailAddress = emailaddress;
        miscEntity.Address = address;
        miscEntity.CompanyLogo = companylogo;
        miscEntity.FormLogo = formlogo;
        if (webicon == "")
        {
            miscEntity.WebIcon = "";
        }
        else
        {
            miscEntity.WebIcon = hostaddress + "/Image/IOT/" + webicon;
        }
        miscEntity.LogoPath = hostaddress +"/Image/";
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetCompanyInformation(miscEntity);
    }

    protected void CompanyLogoUpload(object sender, EventArgs e)
    {
        string companyname = txtCompanyName.Text;
        Boolean fileOK = false;
        String path = Server.MapPath("~/Image/" + companyname + "/");
        if (FileUploadCompanyLogo.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(FileUploadCompanyLogo.FileName).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg" };
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
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileUploadCompanyLogo.SaveAs(path + companyname + System.IO.Path.GetExtension(FileUploadCompanyLogo.FileName).ToLower());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:doneupload();", true);
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                //Response.Write("<script>swal('Warning','Please Select .PNG, .JPEG or .JPG to upload','warning');</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:selectfie();", true);
            }
        }
        else
        {

        }
    }

    public void GetCompanyInformation()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select * from tbl_CompanyInformation";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            txtCompanyName.Text = dr1[0].ToString();
            txtTradingName.Text = dr1[1].ToString();
            txtContactPerson.Text = dr1[2].ToString();
            txtContactNumber.Text = dr1[3].ToString();
            txtEmailAddress.Text = dr1[4].ToString();
            txtAddress.Text = dr1[5].ToString();
            CompanyLogo.InnerHtml = "<img src='../Image/" + dr1[0].ToString() + "/" + dr1[6].ToString() + "' style='max-width: 50%; height:50%;'>";
            FormLogo.InnerHtml = "<img src='../Image/" + dr1[0].ToString() + "/" + dr1[7].ToString() + "' style='max-width: 50%; height:50%;'>";
            WebIcon.InnerHtml = "<img src='../Image/IOT/favico.ico' style='max-width: 100%; height:auto;'>";
        }
        else
        {

        }
        connection.Close();
    }
    #endregion


    #region Depreciation Setting

    [WebMethod]
    public static void SetDepreciation(string depreciationformula, int usefullife, int daysperyear, decimal residualvalue, decimal salvagevalue, decimal rateofdepreciation)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.DepreciationFormula = depreciationformula;
        miscEntity.UsefulLife = usefullife;
        miscEntity.DaysPerYear = daysperyear;
        miscEntity.ResidualValue = residualvalue;
        miscEntity.SalvageValue = salvagevalue;
        miscEntity.RateOfDepreciation = rateofdepreciation;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetDepreciation(miscEntity);
    }

    [WebMethod]
    public static string GetDepreciation()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetDepreciation(Common.ConnString);
    }

    #endregion


    #region RFID and Barcode Generation
    [WebMethod]
    public static void SetRFIDandBarcodeGeneration(string generateormanualrfid, string generateormanualbarcode)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.RFIDNumberGeneration = generateormanualrfid;
        miscEntity.BarcodeNumberGeneration = generateormanualbarcode;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetRFIDandBarcodeGeneration(miscEntity);
    }

    [WebMethod]
    public static string GetRFIDandBarcodeGeneration()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetRFIDandBarcodeGeneration(Common.ConnString);
    }

    #endregion


    #region System Settings

    [WebMethod]
    public static void SetSystemSetting(string verificationmode, bool creationapproval, bool stockmergeapproval, bool propertyissuanceapproval, bool propertytransferapproval,
        bool turnoverstockapproval, bool propertydisposalapproval, bool gatepassapproval, bool csvuploadapproval, bool dbrestoreapproval, bool supplyallocationapproval,
        bool creationscanner, bool propertyissuancescanner, bool propertytransferscanner, bool stocktaking, bool rfidprinting, bool propertyrequestapproval, bool propertyrequestscanner,
        bool propertyreturnrequestscanner, bool propertydisposalscanner, bool IssuanceAcknowledgement, string AcknowledgementMode, bool stockdisposalapproval, bool stockissuanceapproval,
        bool requiremaintenance, string propertynogeneration, string ExpenseType, decimal ICS, decimal PAR, decimal OPEXCAPEX, bool RetaggingApproval, bool StockRequestRemainingQuantity)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.VersionMode = "Government Version";
        miscEntity.ICS = ICS;
        miscEntity.PAR = PAR;
        miscEntity.CapexOpex = OPEXCAPEX;
        miscEntity.ExpenseType = ExpenseType;
        miscEntity.VerificationMode = verificationmode;
        miscEntity.CreationApproval = creationapproval;
        miscEntity.StockMergeApproval = stockmergeapproval;
        miscEntity.PropertyIssuanceApproval = propertyissuanceapproval;
        miscEntity.PropertyTransferApproval = propertytransferapproval;
        miscEntity.TurnOverStockApproval = turnoverstockapproval;
        miscEntity.PropertyDisposalApproval = propertydisposalapproval;
        miscEntity.GatePassApproval = gatepassapproval;
        miscEntity.CSVUploadApproval = csvuploadapproval;
        miscEntity.DBRestoreApproval = dbrestoreapproval;
        miscEntity.StockDisposalApproval = stockdisposalapproval;
        miscEntity.StockIssuanceApproval = stockissuanceapproval;
        miscEntity.RetaggingApproval = RetaggingApproval;
        miscEntity.SupplyAllocationApproval = supplyallocationapproval;


        miscEntity.CreationScanner = creationscanner;
        miscEntity.PropertyIssuanceScanner = propertyissuancescanner;
        miscEntity.PropertyTransferScanner = propertytransferscanner;
        miscEntity.StockTaking = stocktaking;

        miscEntity.RFIDprinting = rfidprinting;

        miscEntity.PropertyRequestApproval = propertyrequestapproval;
        miscEntity.PropertyRequestScanner = propertyrequestscanner;
        miscEntity.PropertyReturnRequestScanner = propertyreturnrequestscanner;
        miscEntity.PropertyDisposalScanner = propertydisposalscanner;
        miscEntity.IssuanceAcknowledgement = IssuanceAcknowledgement;
        miscEntity.AcknowledgementMode = AcknowledgementMode;

        miscEntity.RequireMaintenance = requiremaintenance;

        miscEntity.PropertyNoGeneration = propertynogeneration;

        miscEntity.StockRequestRemainingQuantity = StockRequestRemainingQuantity;

        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetSystemSetting(miscEntity);
    }

    [WebMethod]
    public static string GetSystemSetting()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetSystemSetting(Common.ConnString);
    }

    #endregion


    #region Database Back-up Directory
    [WebMethod]
    public static int SetDBDirectory(string directory)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.DBDirectory = directory;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        return miscBll.SetDBDirectory(miscEntity);
    }

    public void GetDBDirectory()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select DBDirectoryPath from tbl_Miscellaneous";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            txtDBDirectory.Text = dr1[0].ToString().Replace(',', '\\');
        }
        else
        {

        }
        connection.Close();
    }
    #endregion


    #region Uploaded Files Directory
    [WebMethod]
    public static int SetUploadedFilesDirectoryPath(string directory)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.UploadedFilesDirectory = directory;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        return miscBll.SetUploadedFilesDirectoryPath(miscEntity);
    }

    public void GetUploadFIlesDirectoryPath()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select UploadFilesDirectoryPath from tbl_Miscellaneous";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            txtUploadFilesDirectoryPath.Text = dr1[0].ToString().Replace(',', '\\');
        }
        else
        {

        }
        connection.Close();
    }
    #endregion


    #region User Default Password
    [WebMethod]
    public static int SetDefaultPassword(string defaultpassword)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        string pass = AES.Encrypt(defaultpassword, defaultpassword);

        miscEntity.UserDefaultPassword = pass;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        return miscBll.SetDefaultPassword(miscEntity);
    }

    [WebMethod]
    public static int SetUserAcccountSettings(string UsernameOrEmail, string DefaultOrGenerated, string defaultpassword,
        int PasswordSize, int UpperCaseSize, int LowerCaseSize, int NumberSize, int SymbolSize, int LoginAttempt, bool WithLoginAttempt,
        bool WithUpperCase, bool WithLowerCase, bool WithNumber, bool WithSymbol)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        string pass = AES.Encrypt(defaultpassword, defaultpassword);

        miscEntity.UserDefaultPassword = pass;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;
        miscEntity.UsernameOrEmail = UsernameOrEmail;
        miscEntity.DefaultOrGenerated = DefaultOrGenerated;
        miscEntity.PasswordSize = PasswordSize;
        miscEntity.UpperCaseSize = UpperCaseSize;
        miscEntity.LowerCaseSize = LowerCaseSize;
        miscEntity.NumberSize = NumberSize;
        miscEntity.SymbolSize = SymbolSize;
        miscEntity.LoginAttempt = LoginAttempt;
        miscEntity.WithLoginAttempt = WithLoginAttempt;
        miscEntity.WithUpperCase = WithUpperCase;
        miscEntity.WithLowerCase = WithLowerCase;
        miscEntity.WithNumber = WithNumber;
        miscEntity.WithSymbol = WithSymbol;

        return miscBll.SetUserAcccountSettings(miscEntity);
    }

    [WebMethod]
    public static string GetUserAcccountSettings()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetUserAcccountSettings(Common.ConnString);
    }
    public void GetDefaultPassword()
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();
        txtDefaultPassword.Text = miscBll.GetUserDefaultPassword(Common.ConnString);
        //string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        //SqlConnection connection = new SqlConnection(conString);
        //connection.Open();
        //string q1 = "Select UserDefaultPassword from tbl_Miscellaneous";
        //SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        //SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        //if (dr1.Read())
        //{
        //    string en = dr1[0].ToString();
        //    txtDefaultPassword.Text = en;
        //}
        //connection.Close();

    }

    #endregion


    #region Mail Settings

    [WebMethod]
    public static void SetMail(string host, int port, string email, string password, string emailsubject)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.SMTPhost = host;
        miscEntity.SMTPport = port;
        miscEntity.Email = email;
        miscEntity.Password = password;
        miscEntity.EmailSubject = emailsubject;
        miscEntity.CreatedBy = Common.UserID;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetMail(miscEntity);
    }

    public void GetMail()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select SMTPhost,SMTPport,Email,Password,EmailSubject from tbl_Miscellaneous";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            txtSMTPhost.Text = dr1[0].ToString();
            txtSMTPport.Text = dr1[1].ToString();
            txtEmail.Text = dr1[2].ToString();
            txtPassword.Text = dr1[3].ToString();
            txtEmailSubject.Text = dr1[4].ToString();
        }
        else
        {

        }
        connection.Close();
    }

    [WebMethod]
    public static string SendTestEmail(string email)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            string smtp_ = miscBll.GetSMTP(Common.ConnString);
            string smtphost = smtp_.Split('|')[0];
            string smtpport = smtp_.Split('|')[1];
            string smtpmail = smtp_.Split('|')[2]; //"rfid.aimmis@gmail.com";
            string smtppass = smtp_.Split('|')[3];
            if (email != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject;
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br>";
                Body += "This is a TEST EMAIL";
                Body += "<br><br><br><i>***This is a system generated mail. Please do not reply.***</i></font>";
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtphost; //Or Your SMTP Server Address
                smtp.Port = Convert.ToInt32(smtpport);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (smtpmail, smtppass);
                //Or your Smtp Email ID and Password
                mail.IsBodyHtml = true;
                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
                return "okay";
            }
            else
            {
                return "sorry";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }

    #endregion


    #region Accounting Setting
    [WebMethod]
    public static void SetAccountingSetting(int accountingperiod, int conversionmonth, int endmonth, int transactionyear, int transactionmonth)
    {
        MiscEntity miscEntity = new MiscEntity();
        MiscBll miscBll = new MiscBll();

        miscEntity.AccountingPeriod = accountingperiod;
        miscEntity.ConversionMonth = conversionmonth;
        miscEntity.EndMonth = endmonth;
        miscEntity.TransactionYear = transactionyear;
        miscEntity.TransactionMonth = transactionmonth;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.SetAccountingSetting(miscEntity);
    }

    [WebMethod]
    public static string GetAccountingSetting()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetAccountingSetting(Common.ConnString);
    }


    #endregion


    ////public void GetAssetSettings()
    ////{
    ////    string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
    ////    SqlConnection connection = new SqlConnection(conString);
    ////    connection.Open();
    ////    string q1 = "Select StartCountTaggable,TaggablePrefix,StartCountNonTaggable,NonTaggablePrefix from tbl_Miscellaneous";
    ////    SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

    ////    SqlDataReader dr1 = sqlCmd1.ExecuteReader();
    ////    if (dr1.Read())
    ////    {
    ////        txtPrefixTaggable.Text = dr1[1].ToString();
    ////        txtStartingCountTaggable.Text = dr1[0].ToString();
    ////        txtPrefixNonTaggable.Text = dr1[3].ToString();
    ////        txtStartingCountNonTaggable.Text = dr1[2].ToString();
    ////    }
    ////    else
    ////    {

    ////    }
    ////    connection.Close();
    ////}

    ////public void GetPARICS()
    ////{
    ////    string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
    ////    SqlConnection connection = new SqlConnection(conString);
    ////    connection.Open();
    ////    string q1 = "Select Amount from tbl_Miscellaneous";
    ////    SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

    ////    SqlDataReader dr1 = sqlCmd1.ExecuteReader();
    ////    if (dr1.Read())
    ////    {
    ////        txtCost.Text = dr1[0].ToString();
    ////    }
    ////    else
    ////    {

    ////    }
    ////    connection.Close();
    ////}

    //public void GetDefaultPassword()
    //{
    //    string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
    //    SqlConnection connection = new SqlConnection(conString);
    //    connection.Open();
    //    string q1 = "Select UserDefaultPassword from tbl_Miscellaneous";
    //    SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

    //    SqlDataReader dr1 = sqlCmd1.ExecuteReader();
    //    if (dr1.Read())
    //    {
    //        string en = dr1[0].ToString();
    //        txtDefaultPassword.Text = en;
    //    }
    //    connection.Close();
    //}





    //[WebMethod]
    //public static int SetDefaultPassword(string defaultpassword)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();

    //    string pass = AES.Encrypt(defaultpassword, defaultpassword);

    //    miscEntity.DefaultPassword = pass;
    //    miscEntity.User = Common.UserID;

    //    return miscBll.SetDefaultPassword(miscEntity);
    //}

    //[WebMethod]
    //public static int SetAssetSettings(int startingcounttaggable, string prefixtaggable, string prefixnontaggable, int startingcountnontaggable)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();


    //    miscEntity.StartCountTaggable = startingcounttaggable;
    //    miscEntity.PrefixTaggable = prefixtaggable;
    //    miscEntity.PrefixNonTaggable = prefixnontaggable;
    //    miscEntity.StartCountNonTaggable = startingcountnontaggable;
    //    miscEntity.User = Common.UserID;

    //    return miscBll.SetAssetSettings(miscEntity);
    //}

    //protected void Upload(object sender, EventArgs e)
    //{
    //    //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Image/LRTA/");
    //    if (FileUpload2.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
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
    //                FileUpload2.SaveAs(path + FileUpload2.FileName);
    //            }
    //            catch (Exception ex)
    //            {
    //                //Label1.Text = "File could not be uploaded.";
    //            }
    //        }
    //        else
    //        {
    //            Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //    }
    //}

    //[WebMethod]
    //public static string SaveAnnouncement(string announcement)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();

    //    miscEntity.Announcement = announcement;

    //    return miscBll.SaveAnnouncement(miscEntity);
    //}




    //protected void UploadSmallLogo(object sender, EventArgs e)
    //{
    //    //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Image/LRTA/");
    //    if (FileUploadSmallLogo.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(FileUploadSmallLogo.FileName).ToLower();
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
    //                FileUploadSmallLogo.SaveAs(path + FileUploadSmallLogo.FileName);
    //            }
    //            catch (Exception ex)
    //            {
    //                //Label1.Text = "File could not be uploaded.";
    //            }
    //        }
    //        else
    //        {
    //            Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //    }
    //}

    //[WebMethod]
    //public static string SaveSmallLogo(string smalllogo)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();

    //    miscEntity.Announcement = smalllogo;

    //    return miscBll.SaveSmallLogo(miscEntity);
    //}

    //protected void UploadBigLogo(object sender, EventArgs e)
    //{
    //    //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Image/LRTA/");
    //    if (FileUploadBigLogo.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(FileUploadBigLogo.FileName).ToLower();
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
    //                FileUploadBigLogo.SaveAs(path + FileUploadBigLogo.FileName);
    //            }
    //            catch (Exception ex)
    //            {
    //                //Label1.Text = "File could not be uploaded.";
    //            }
    //        }
    //        else
    //        {
    //            Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //    }
    //}

    //[WebMethod]
    //public static string SaveBigLogo(string biglogo)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();

    //    miscEntity.Announcement = biglogo;

    //    return miscBll.SaveBigLogo(miscEntity);
    //}

    //protected void UploadLoginLogo(object sender, EventArgs e)
    //{
    //    //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Image/LRTA/");
    //    if (FileUploadLoginLogo.HasFile)
    //    {
    //        String fileExtension =
    //            System.IO.Path.GetExtension(FileUploadLoginLogo.FileName).ToLower();
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
    //                FileUploadLoginLogo.SaveAs(path + FileUploadLoginLogo.FileName);
    //            }
    //            catch (Exception ex)
    //            {
    //                //Label1.Text = "File could not be uploaded.";
    //            }
    //        }
    //        else
    //        {
    //            Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("<script>alert('Please Select .PNG, .GIF, .JPEG or .JPG to upload');</script>");
    //    }
    //}

    //[WebMethod]
    //public static string SaveLoginLogo(string loginlogo)
    //{
    //    MiscEntity miscEntity = new MiscEntity();
    //    MiscBll miscBll = new MiscBll();

    //    miscEntity.Announcement = loginlogo;

    //    return miscBll.SaveLoginLogo(miscEntity);
    //}
}