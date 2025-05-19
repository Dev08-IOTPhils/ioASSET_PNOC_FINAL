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

public partial class Operation_Module_StockMerge : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        GetApprover();
    }
    #region Metadata
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Operation_Module/StockMerge.aspx", Convert.ToInt32(Common.RoleId), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    public void GetApprover()
    {
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetApproverListBySite(Common.ConnString, Convert.ToInt32(Common.SiteID), Convert.ToInt32(Common.UserID));

        ddlApprover.DataSource = dt;
        ddlApprover.DataValueField = "UserID";
        ddlApprover.DataTextField = "FullName";
        ddlApprover.DataBind();
        ddlApprover.Items.Insert(0, "--- Select Approver ---");
    }

    [WebMethod]
    public static List<ListItem> GetStockItemCode(string itemcode)
    {
        StockMergeBll y = new StockMergeBll();

        return y.GetStockItemCode(Common.ConnString, itemcode, Convert.ToInt32(Common.CustodianID), Convert.ToInt32(Common.SiteID));
    }

    [WebMethod]
    public static string IfMergeNeedsApproval()
    {
        return Common.MergeStockApproval;
    }
    #endregion


    #region Datatables

    [WebMethod]
    public static string GetList(string search)
    {
        StockMergeBll y = new StockMergeBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetAvailableStockForMerge(Common.ConnString, search, Common.CustodianID, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();
                    x.Brand = dt.Rows[i]["BrandName"].ToString();
                    x.Model = dt.Rows[i]["ModelName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success glyphicon glyphicon-transfer btn_Merge' data-toggle='tooltip'  title='Merge to Another Supply'></a> </center>";
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
    public static string GetPendingApprovalStockMerge(string search)
    {
        StockMergeBll y = new StockMergeBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetPendingApprovalStockMerge(search, Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.StockMergeNo = dt.Rows[i]["StockMergeNo"].ToString();
                    x.FromStockIC = dt.Rows[i]["FromIC"].ToString();
                    x.ToStockIC = dt.Rows[i]["ToIC"].ToString();
                    x.QuantityString = dt.Rows[i]["MergeFromStockQuantity"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.TransactionDate = dt.Rows[i]["CreatedDate"].ToString();
                    if (DeleteAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-danger fa fa-remove btn_Cancel' data-toggle='tooltip'  title='Cancel Stock Merge' onclick=\"CancelStockMerge('" + dt.Rows[i]["StockMergeNo"] + "')\"></a> </center>";
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

    #endregion

    #region methods

    [WebMethod]
    public static string Merge(string mergefrom, int mergeto, int approverid, string remarks)
    {
        StockEntity x = new StockEntity();
        StockMergeBll sm = new StockMergeBll();

        x.ItemCode = mergefrom;
        x.StockID = mergeto;
        x.ApproverID = approverid;
        x.Remarks = remarks;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return sm.MergeStock(x);
    }


    [WebMethod]
    public static string CancelStockMerge(string stockmergeno)
    {
        StockMergeBll y = new StockMergeBll();
        StockEntity x = new StockEntity();

        x.CreatedBy = Common.UserName;
        x.StockMergeNo = stockmergeno;
        x.ConnectionString = Common.ConnString;

        y.CancelStockMerge(x);

        return "";
    }



    #endregion



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