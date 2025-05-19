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

public partial class Operation_Module_DayPass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Operation_Module/DayPass.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string IfGatePassNeedsApproval()
    {
        return Common.GatePassApproval;
    }

    [WebMethod]
    public static string GetApproverDP()
    {
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetApproverListBySite(Common.ConnString, Convert.ToInt32(Common.SiteID), Convert.ToInt32(Common.UserID));

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetPropertyDayPass()
    {
        DayPassBll y = new DayPassBll();

        string html = string.Empty;
        List<DayPassEntity> JsonItemList = new List<DayPassEntity>();
        try
        {
            DataTable dt = y.GetPropertyDayPass(Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DayPassEntity x = new DayPassEntity();
                    x.RecIDstring = dt.Rows[i]["RecID"].ToString();
                    x.SerialNumber = dt.Rows[i]["SerialNumber"].ToString();
                    x.Category = dt.Rows[i]["Category"].ToString();
                    x.Brand = dt.Rows[i]["Brand"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Image = "<a target='_blank' href='../SupportingDocuments/Daypass/" + dt.Rows[i]["Image"].ToString() + "'><label>" + dt.Rows[i]["Image"].ToString() + "</label></a><br>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-warning btn_Edit' data-toggle='tooltip'  title='Update Information'><span class='fa fa-edit'></span></a> ";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-warning btn_Edit' data-toggle='tooltip'  title='Update Information'><span class='fa fa-edit'></span></a> "
                        + "<a href='' class='btn btn-xs btn-danger btn_Remove' data-toggle='tooltip'  title='Remove from list'><span class='fa fa-close'></span></a> </center>";
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
    public static string GetPropertyDayPassPendingApproval()
    {
        DayPassBll y = new DayPassBll();

        string html = string.Empty;
        List<DayPassEntity> JsonItemList = new List<DayPassEntity>();
        try
        {
            DataTable dt = y.GetPropertyDayPassPendingApproval(Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DayPassEntity x = new DayPassEntity();
                    x.DayPassNumber = dt.Rows[i]["DayPassNumber"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Purpose = dt.Rows[i]["Remarks"].ToString();

                    x.Status = dt.Rows[i]["Status"].ToString();
                    if (ViewAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {

                        if (dt.Rows[i]["Status"].ToString() == "Approved")
                        {
                            x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewList('" + dt.Rows[i]["DayPassNumber"] + "')\"><span class='fa fa-eye'></span></a></center>";
                        }
                        else
                        {
                            x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewList('" + dt.Rows[i]["DayPassNumber"] + "')\"><span class='fa fa-eye'></span></a>" +
                                " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"Cancel('" + dt.Rows[i]["DayPassNumber"] + "')\"><span class='fa fa-times-circle'></span></a></center>";
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
    public static string PropertyDayPassRequestList(string daypassno)
    {
        DayPassBll y = new DayPassBll();
        string html = string.Empty;
        List<DayPassEntity> JsonItemList = new List<DayPassEntity>();
        try
        {
            DataTable dt = y.PropertyDayPassRequestList(Common.ConnString, daypassno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DayPassEntity x = new DayPassEntity();
                    x.SerialNumber = dt.Rows[i]["SerialNumber"].ToString();
                    x.Category = dt.Rows[i]["Category"].ToString();
                    x.Brand = dt.Rows[i]["Brand"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

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
    public static string GenerateDayPassReport(string daypassno)
    {
        DayPassEntity x = new DayPassEntity();
        DayPassBll y = new DayPassBll();

        x.DayPassNumber = daypassno;
        x.ConnectionString = Common.ConnString;

        string daypassdetails = y.PropertyDayPassDetails(x);
        string accountablename = daypassdetails.Split('|')[1];
        string approvername = daypassdetails.Split('|')[2];
        string purpose = daypassdetails.Split('|')[3];
        string createddate = daypassdetails.Split('|')[4];

        DataTable dt = y.PropertyDayPassRequestList(Common.ConnString, daypassno);

        return GenerateDayPassForm(dt, daypassno, accountablename, approvername, purpose, createddate);

    }
    public static string GenerateDayPassForm(DataTable dt, string daypassno, string accountablename, string approvername, string remarks, string createddate)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];
            string CompanyLogo = x.Split('|')[6];

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 315px; }"
            + ".page-footer, .page-footer-space {height: 150px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; margin: 10mm;}"

            + "@media print {"
                + "table {padding: 0px 10px 0px 0px;}"
                + "thead {display: table-header-group; }"
                + "tfoot {display: table-footer-group;}"
                + "body { margin: 0;}"

            + "}"
            + "</style >";
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
             + "<tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
             + "<tr><td align='center' style='font-size:15;'><br><b>Personal Property Gate Pass Request <b></td</tr>"
             + "<tr><td align='right' style='font-size:14;'>No. <u>" + daypassno + "</u></td</tr><tr><td align='right' style='font-size:14;'>Date. <u>" + createddate.Split(' ')[0] + "</u></td</tr></table>";
            html += "<table  style='font-size:90%; font-family:Arial;' width='100%'><tr><td>TO: GUARD ON DUTY</td><td align='right'></td></tr>";
            html += "<tr><td>&emsp;Please allow <u> " + accountablename + "</u> to bring outside the Company premises the following items<br></td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //purpose

            html += "<p>Purpose : " + remarks + "</p>";

            //signatories
            html += "<table style='border-collapse:collapse; font-size:90%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='border-right: 1px solid black; border-bottom: 1px solid black;' align='center'>REQUESTED BY</td><td style='border-right: 1px solid black; border-bottom: 1px solid black;' align='center'>APPROVED BY</td><td style='border-bottom: 1px solid black;' align='center'>CHECKED BY</td></tr>";
            html += "<tr><td style='border-right: 1px solid black;' align='center'><br><br><br> " + accountablename + "</td><td style='border-right: 1px solid black;' align='center'><br><br><br>" + approvername + "</td><td align='center'><br><br><br>GUARD ON DUTY</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td><center><b>QTY</b><center></td>"
                      + "<td><center><b>Category</b><center></td>"
                      + "<td><center><b>Brand</b><center></td>"
                      + "<td><center><b>Serial #</b><center></td>"
                      + "<td><center><b>Description</b><center></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td ><center>1</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Category"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Brand"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["SerialNumber"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Description"].ToString() + "</center></td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='5' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";
            #endregion


            #region footer placeholder
            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";
            #endregion

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string PropertyAddToDayPass(int recid, string serialno, string category, string brand, string description, string image)
    {
        DayPassEntity x = new DayPassEntity();
        DayPassBll y = new DayPassBll();

        x.RecID = recid;
        x.SerialNumber = serialno;
        x.Category = category;
        x.Brand = brand;
        x.Description = description;
        x.Image = image;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;


        return y.PropertyAddToDayPass(x).ToString();

    }

    [WebMethod]
    public static string PropertyRemoveToDayPass(int recid)
    {
        DayPassEntity x = new DayPassEntity();
        DayPassBll y = new DayPassBll();

        x.RecID = recid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.PropertyRemoveToDayPass(x).ToString();

    }

    [WebMethod]
    public static string ConfirmPropertyDayPassRequest(string purpose, int approverid)
    {
        try
        {
            DayPassEntity x = new DayPassEntity();
            DayPassBll y = new DayPassBll();

            x.CreatedBy = Common.UserName;
            x.ApproverID = approverid;
            x.Purpose = purpose;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string daypassno = y.ConfirmPropertyDayPassRequest(x);
            x.DayPassNumber = daypassno;

            string daypassdetails = y.PropertyDayPassDetails(x);
            string accountablename = daypassdetails.Split('|')[1];
            string approvername = daypassdetails.Split('|')[2];
            string createddate = daypassdetails.Split('|')[4];
            string accountableemail = daypassdetails.Split('|')[5];
            string approveremail = daypassdetails.Split('|')[6];

            DataTable dt = y.PropertyDayPassRequestList(Common.ConnString, daypassno);
            if (approveremail != "")
            {

                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(approveremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Day Pass Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
                Body += "A PROPERTY DAY PASS REQUEST  transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                    + "Day Pass No : " + daypassno + "<br>"
                    + "Purpose/Remarks : " + purpose + " <br>"
                    + "Processed By : " + accountablename + " <br>"
                    + "Processed Date : " + createddate + "<br><br>";
                Body += "<table border='1'><tr>"
                    + "<th>Serial No</th>"
                    + "<th>Category</th>"
                    + "<th>Brand</th>"
                    + "<th>Description</th></tr>";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Body += "<tr>"
                            + "<td>" + dt.Rows[i]["SerialNumber"].ToString() + "</td>"
                            + "<td>" + dt.Rows[i]["Category"].ToString() + "</td>"
                            + "<td>" + dt.Rows[i]["Brand"].ToString() + "</td>"
                            + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
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

                return "okay";
            }
            else
            {
                return "approver has no email";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }

    [WebMethod]
    public static string GetCountPropertyDayPassRequest()
    {
        DayPassEntity x = new DayPassEntity();
        DayPassBll y = new DayPassBll();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.GetCountPropertyDayPassRequest(x);
    }

    [WebMethod]
    public static string CancelDayPassRequest(string daypassno)
    {
        DayPassEntity x = new DayPassEntity();
        DayPassBll y = new DayPassBll();

        x.DayPassNumber = daypassno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.CancelDayPassRequest(x);

        return "";
    }


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