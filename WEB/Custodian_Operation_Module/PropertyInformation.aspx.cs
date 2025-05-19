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

public partial class Operation_Module_PropertyInformation : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Datatables
    [WebMethod]
    public static string GetList(string search)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyInformation(Common.ConnString, search);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.SupplierName = dt.Rows[i]["SupplierName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();

                    x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();

                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Area = dt.Rows[i]["Area"].ToString();

                    x.DateOfCreation = dt.Rows[i]["CreatedDate"].ToString();
                    x.DateOfAcquisition = dt.Rows[i]["DateOfAcquisition"].ToString().Split(' ')[0];
                    x.DateOfMaintenance = dt.Rows[i]["MaintenanceDate"].ToString().Split(' ')[0];
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    if (dt.Rows[i]["TransactionType"].ToString() != "")
                    {
                        x.Status = dt.Rows[i]["Status"].ToString() + " (" + dt.Rows[i]["TransactionType"].ToString() + ")";
                    }
                    else
                    {
                        x.Status = dt.Rows[i]["Status"].ToString();
                    }

                    x.Action = " <center><button type='button' class='btn btn-xs btn-warning fa fa-list btn_show' data-toggle='tooltip' title='Update Information' ></button></center>";

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
    //Serial Number Check for Duplicate
    [WebMethod]
    public static string CheckDuplicateSerial(string serialno)
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.CheckDuplicateSerial(Common.ConnString, serialno);
    }
    #endregion

    #region Method
    [WebMethod]
    public static string GetPropertyInformation(string propertynumber)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll y = new PropertyBll();

        x.PropertyNumber = propertynumber;
        x.ConnectionString = Common.ConnString;

        return y.GetPropertyInformation(x);
    }

    [WebMethod]
    public static string UpdatePropertyInformation(string propertynumber, string serialnumber, string labeltoprint, string longdescription, string warranty, string insurance, string expiry, string newprnr, decimal acquiredcost, int custodian)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll y = new PropertyBll();

        x.NewPropertyNumber = newprnr;
        x.PropertyNumber = propertynumber;
        x.SerialNo = serialnumber;
        x.LabelToPrint = labeltoprint;
        x.LongDescription = longdescription;
        x.ExpiryDate = expiry;
        if (warranty == "" || warranty == "-")
        {
            x.WarrantyStartDate = null;
            x.WarrantyEndDate = null;
        }
        else
        {
            x.WarrantyStartDate = warranty.Split('-')[0].Trim();
            x.WarrantyEndDate = warranty.Split('-')[1].Trim();
        }
        if (insurance == "" || insurance == "-")
        {
            x.InsuranceStartDate = null;
            x.InsuranceEndDate = null;
        }
        else
        {
            x.InsuranceStartDate = insurance.Split('-')[0].Trim();
            x.InsuranceEndDate = insurance.Split('-')[1].Trim();
        }
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.AcquiredCost = acquiredcost;
        x.CustodianID = custodian;

        return y.UpdatePropertyInformation(x).ToString();

    }
    #endregion


    //forEmail
    private void EMailThis()
    {
        //ddd
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