using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

public partial class Operation_Module_PropertyVerification : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        TransactionType();
        GetDeviceListUSBWriter();
    }

    #region metadata
    private void TransactionType()
    {
        ddlTransactionType.Items.Clear();
        ddlTransactionType.Items.Add("--- SELECT TRANSACTION ---");
        ddlTransactionType.Items.Add("GOODS RECEIVE");
        ddlTransactionType.Items.Add("ISSUANCE");
        ddlTransactionType.Items.Add("TRANSFER");
        ddlTransactionType.Items.Add("RETURN");
        ddlTransactionType.Items.Add("DISPOSAL");

    }

    [WebMethod]
    public static string GetSystemSetting()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetSystemSetting(Common.ConnString);
    }

    private void GetDeviceListUSBWriter()
    {
        TagTypeBll tagTypeBll = new TagTypeBll();

        DataTable dt = tagTypeBll.GetDeviceListUSBWriter(Common.ConnString);

        ddlCom.DataSource = dt;
        ddlCom.DataValueField = "DeviceID";
        ddlCom.DataTextField = "DeviceName";
        ddlCom.DataBind();
        ddlCom.Items.Insert(0, "--- Select USB Writer ---");
    }

    [WebMethod]
    public static string GetDeviceWriterStatus(int deviceid)
    {
        DeviceEntity deviceEntity = new DeviceEntity();
        DeviceBll deviceBll = new DeviceBll();

        deviceEntity.DeviceID = deviceid;
        deviceEntity.ConnectionString = Common.ConnString;

        return deviceBll.DevicebyID(deviceEntity).Split('|')[7];
    }
    #endregion

    [WebMethod]
    public static string GetList(int transactiontype, string search)
    {
        PropertyVerificationBll y = new PropertyVerificationBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyVerification(Common.ConnString, search, transactiontype,Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.pmlid = Convert.ToInt32(dt.Rows[i]["RecID"]);
                    if (dt.Rows[i]["IsVerified"].ToString() == "1")
                    {
                        x.TransactionType = "Goods Receive";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "2")
                    {
                        x.TransactionType = "Property Issuance";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "3")
                    {
                        x.TransactionType = "Property Transfer";
                    }
		    else if (dt.Rows[i]["IsVerified"].ToString() == "5")
                    {
                        x.TransactionType = "Property Return";
                    }
                    else 
                    {
                        x.TransactionType = "Property Disposal";
                    }
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();

                    x.Action = " <center><button type='button' class='btn btn-xs btn-primary btn_Verify' pmlid='"+ dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Verify Property'>VERIFY</button> </center>";
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
    public static string GetListThingMagicMode(int transactiontype, string search)
    {
        PropertyVerificationBll y = new PropertyVerificationBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyVerification(Common.ConnString, search, transactiontype, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    if (dt.Rows[i]["IsVerified"].ToString() == "1")
                    {
                        x.TransactionType = "Goods Receive";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "2")
                    {
                        x.TransactionType = "Property Issuance";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "3")
                    {
                        x.TransactionType = "Property Transfer";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "4")
                    {
                        x.TransactionType = "Property Return";
                    }
                    else
                    {
                        x.TransactionType = "Property Disposal";
                    }
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();

                    x.Action = "";
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
    public static string GetListBarcodeBuddyProMode(int transactiontype, string search)
    {
        PropertyVerificationBll y = new PropertyVerificationBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyVerification(Common.ConnString, search, transactiontype, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    if (dt.Rows[i]["IsVerified"].ToString() == "1")
                    {
                        x.TransactionType = "Goods Receive";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "2")
                    {
                        x.TransactionType = "Property Issuance";
                    }
                    else if (dt.Rows[i]["IsVerified"].ToString() == "3")
                    {
                        x.TransactionType = "Property Transfer";
                    }
		    else if (dt.Rows[i]["IsVerified"].ToString() == "5")
                    {
                        x.TransactionType = "Property Return";
                    }
                    else 
                    {
                        x.TransactionType = "Property Disposal";
                    }
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Action = "";
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
    public static string VerifyProperty(int pmlid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyVerificationBll y = new PropertyVerificationBll();

        x.RecID = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.VerifyProperty(x).ToString();

    }

    [WebMethod]
    public static string VerifyPropertyByTM(string propertynumber, int deviceid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyVerificationBll y = new PropertyVerificationBll();

        x.PropertyNumber = propertynumber;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.DeviceID = deviceid;

        return y.VerifyPropertyByTM(x).ToString();

    }

    [WebMethod]
    public static string VerifyPropertyByBarcode(string barcode)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyVerificationBll y = new PropertyVerificationBll();

        x.BarcodeNumber = barcode;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.VerifyPropertyByBarcode(x).ToString();

    }


    //forEmail
    private void EMailThis()
    {
        DataTable dt = new DataTable();

        string smtphost = Common.SMTPhost;
        string smtpport = Common.SMTPport;
        string smtpmail = Common.SMTPmail;
        string smtppass = Common.SMTPpass;
        MailMessage mail = new MailMessage();
        mail.To.Add("/dddapproveremail");

        mail.From = new MailAddress(smtpmail);
        mail.Subject = Common.EmailSubject + " (Property Disposal - Approval)";
        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + "/dddapprovername" + ",<br><br><br>";
        Body += "A PROPERTY DISPOSAL transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + "/dddhostaddress" + " / Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
            + "Disposal No : " + "/ddddisposalno" + " < br>"
            + "Purpose/Remarks : " + "/dddremarks" + " < br>"
            + "Processed By : " + "/dddtransactorname" + " < br>"
            + "Processed Date : " + "/ddddisposaldate" + " < br><br>";
        Body += "<table border='1'><tr>"
            + "<th>Property No</th>"
            + "<th>Description</th>"
            + "<th>Serial No</th></tr>";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Body += "<tr>"
                    + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                    + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
                    + "<td>" + dt.Rows[i]["SerialNumber"].ToString() + "</td>"
                    + "</tr>";
            }
            Body += "</table>";
        }
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

        string dddreturn = "okay";
    }
}