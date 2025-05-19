using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

public partial class Operation_Module_StockReturn : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        ret = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = ret.ToUpper();
        CheckAccess();
        GetEmployee();
    }

    #region Metada

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/StockReturn.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string GetEmployeeSR()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetUserListBySite(Common.ConnString, Convert.ToInt32(Common.SiteID));

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    #endregion Metada

    #region datatables

    [WebMethod]
    public static string GetStockAvailableForReturn(string search, int accountable)
    {
        StockReturnBll y = new StockReturnBll();
        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetStockAvailableForReturn(search, Common.ConnString, accountable);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.RecIDstring = dt.Rows[i]["RecID"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-sm btn-success btn_Add' data-toggle='tooltip'  title='Add To Return List'><span class='fa fa-plus' onclick=\"addToReturnList('" + dt.Rows[i]["RecID"] + "', '" + dt.Rows[i]["ItemCode"] + "','" + dt.Rows[i]["Quantity"] + "')\"></span></a> </center>";
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
    public static string GetStockReturnList(int accountable)
    {
        StockReturnBll y = new StockReturnBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetReturnStockList(Common.ConnString, accountable, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();

                    x.RecIDstring = dt.Rows[i]["RecordID"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["ReturnQuantity"].ToString();
                    x.Action = " <center><a href='' class='btn btn-sm btn-danger btn_Remove' data-toggle='tooltip'  title='Remove To Return List'><span class='fa fa-close'></span></a> </center>";

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
    public static string StockReturnPendingUpload(string search)
    {
        StockReturnBll y = new StockReturnBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockReturnPendingUpload(search, Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ReturnNo = dt.Rows[i]["ReturnNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.NoOfItem = dt.Rows[i]["NoOfItem"].ToString();
                    x.TransactionDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <center><button class='btn btn-xs btn-primary btn_View'  type='button' onclick=\"ViewStocks('" + dt.Rows[i]["ReturnNo"] + "')\"><span class='fa fa-eye'></span></button> ";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs btn-danger btn_Cancel' type='button' onclick=\"CancelStockReturn('" + dt.Rows[i]["ReturnNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
                    }
                    x.Action = action;

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
    public static string StockDetailsPendingUpload(string returnno)
    {
        StockReturnBll y = new StockReturnBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, returnno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();
                    x.Brand = dt.Rows[i]["BrandName"].ToString();
                    x.Model = dt.Rows[i]["ModelName"].ToString();
                    x.QuantityString = dt.Rows[i]["ReturnQuantity"].ToString();

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

    #endregion datatables

    #region Methods

    [WebMethod]
    public static string StockReturnGetCount(int employeeid)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.EmployeeID = employeeid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockReturnGetCount(x);
    }

    [WebMethod]
    public static string TransferToReturn(int accountable, int recid, int returnquantity)
    {
        StockEntity x = new StockEntity();
        StockReturnBll y = new StockReturnBll();

        x.EmployeeID = accountable;
        x.RecID = recid;
        x.Quantity = returnquantity;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.TransferToReturn(x);

        return "";
    }

    [WebMethod]
    public static string RemoveToStockReturn(int recid)
    {
        StockEntity x = new StockEntity();
        StockReturnBll y = new StockReturnBll();

        x.RecID = recid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveToStockReturn(x);

        return "";
    }

    [WebMethod]
    public static string RemoveAllToStockReturn(int accountable)
    {
        StockEntity x = new StockEntity();
        StockReturnBll y = new StockReturnBll();

        x.EmployeeID = accountable;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveAllToStockReturn(x);

        return "";
    }

    [WebMethod]
    public static string ConfirmStockReturn(int accountable, string remarks)
    {
        StockEntity x = new StockEntity();
        StockReturnBll y = new StockReturnBll();

        x.EmployeeID = accountable;
        x.Remarks = remarks;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.ConfirmStockReturn(x);

        return "";
    }

    [WebMethod]
    public static string CancelStockReturn(string returnno)
    {
        StockEntity x = new StockEntity();
        StockReturnBll y = new StockReturnBll();

        x.CreatedBy = Common.UserName;
        x.ReturnNo = returnno;
        x.ConnectionString = Common.ConnString;

        y.CancelStockReturn(x);

        return "";
    }

    public bool success = false;

    [WebMethod]
    public static string UploadStockReturn(string returnno)
    {
        StockReturnBll y = new StockReturnBll();

        DataTable dt = y.UploadStockReturnDT(Common.ConnString, returnno, Common.UserName);

        string valuestring = "";
        List<string> returnValue = new List<string>();
        if (dt.Rows.Count >= 1)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
            }
            string[] list = returnValue.ToArray();
            valuestring = string.Join(", ", list);
            if (list.Length > 1)
            {
                return valuestring;
            }
            else
            {
                if (valuestring == "Success")
                {
                    return valuestring;
                }
                else
                {
                    return valuestring;
                }
            }
        }
        return valuestring;
    }

    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        try
        {
            string returnno = txtStockReturnNo.Text;

            if (fuDocuments.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/StockReturn/" + returnno + "/");
                string pathFinal = rootPath + '/' + returnno + '/';
                foreach (HttpPostedFile file in fuDocuments.PostedFiles)
                {
                    if (file.ContentLength > 0)
                    {
                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);
                        }
                        file.SaveAs(Path.Combine(rootPath, file.FileName));
                        success = true;
                        continue;
                    }
                    else
                    {
                        success = false;
                        break;
                    }
                }
                if (success == true)
                {
                    UploadStockReturn(returnno);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadsuccess();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSelect();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSelect();", true);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    #endregion Methods

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