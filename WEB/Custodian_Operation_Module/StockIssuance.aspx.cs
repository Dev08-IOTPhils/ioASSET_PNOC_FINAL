using AdvanceBll;
using AdvanceEntity;
using OpenHtmlToPdf;
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
using System.Web.UI.WebControls;

public partial class Operation_Module_StockIssuance : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        ret = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = ret.ToUpper() + "/DEPARTMENT";
        CheckAccess();
    }

    #region metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/StockIssuance.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string GetDepartmentSI()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();
        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList(departmentEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static List<ListItem> GetUserNoDepartment()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Convert.ToInt32(Common.SiteID));
        userEntity.UserID = Convert.ToInt32(Convert.ToInt32(Common.UserID));
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetUserNoDepartment(userEntity);
    }

    [WebMethod]
    public static List<ListItem> GetUser(int department)
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.DepartmentID = department;
        userEntity.SiteID = Convert.ToInt32(Convert.ToInt32(Common.SiteID));
        userEntity.UserID = Convert.ToInt32(Convert.ToInt32(Common.UserID));
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetUser(userEntity);
    }

    #endregion metadata

    #region datatables

    [WebMethod]
    public static string GetList(string search, int custodian)
    {
        StockIssuanceBll y = new StockIssuanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailableStockForIssuance(Common.ConnString, search, custodian);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' smlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Issuance List'></a> </center>";
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
    public static string GetStockIssuanceList(int employeeid, int custodian)
    {
        StockIssuanceBll y = new StockIssuanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetStockIssuanceList(Common.ConnString, employeeid, custodian, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();

                    x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' smlid='" + dt.Rows[i]["SMLID"].ToString() + "' data-toggle='tooltip'  title='Remove To Issuance List'><span class='fa fa-close'></span></a> </center>";
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
    public static string StockIssuancePendingUpload(string search)
    {
        StockIssuanceBll y = new StockIssuanceBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockIssuancePendingUpload(search, Common.ConnString, Common.UserName, Common.CustodianID, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.IssuanceNo = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.Location = dt.Rows[i]["SiteName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.NoOfItem = dt.Rows[i]["NoOfItem"].ToString();
                    x.TransactionDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <center><button class='btn btn-xs btn-primary btn_View'  type='button' onclick=\"ViewStocks('" + dt.Rows[i]["IssuanceNo"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></button> ";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs btn-danger btn_Cancel' type='button' onclick=\"CancelStockIssuance('" + dt.Rows[i]["IssuanceNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
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
    public static string StockDetailsPendingUpload(string stockissuanceno)
    {
        StockIssuanceBll y = new StockIssuanceBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, stockissuanceno);
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
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();

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
    public static string StockRequestPendingUpload(string search)
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockRequestPendingUpload(search, Common.ConnString, Convert.ToInt32(Common.SiteID), Common.CustodianID, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.RequestNo = dt.Rows[i]["RequestNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.Location = dt.Rows[i]["EmployeeSite"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.NoOfItem = dt.Rows[i]["NoOfItem"].ToString();
                    x.TransactionDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    if (ViewAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-primary btn_View'  type='button' onclick=\"ViewStocksRequest('" + dt.Rows[i]["RequestNo"] + "','" + dt.Rows[i]["IsApproved"] + "','" + dt.Rows[i]["Status"] + "','" + dt.Rows[i]["ApproverName"] + "','" + dt.Rows[i]["ApproverEmail"] + "')\"><span class='fa fa-eye'></span></button> ";
                    }
                    if (DeleteAccess == "0")
                    {
                        x.Action += "";
                    }
                    else
                    {
                        x.Action += " <button class='btn btn-xs btn-danger btn_Cancel' type='button' onclick=\"CancelStockRequest('" + dt.Rows[i]["RequestNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
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
    public static string CompleteRequestStock(string requestno, string ApproverName, string ApproverEmail)
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.CompleteRequestStock(Common.ConnString, requestno, Common.UserName);
            if (Common.StockIssuanceApproval == "True")
            {
                try
                {
                    UserBll userBll = new UserBll();

                    string smtphost = Common.SMTPhost;
                    string smtpport = Common.SMTPport;
                    string smtpmail = Common.SMTPmail;
                    string smtppass = Common.SMTPpass;

                    if (ApproverEmail != "")
                    {
                        string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                        MailMessage mail = new MailMessage();
                        mail.To.Add(ApproverEmail);

                        mail.From = new MailAddress(smtpmail);
                        mail.Subject = Common.EmailSubject + " (Stock Request)";
                        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + ApproverName + ",<br><br><br>";
                        Body += "A STOCK REQUEST transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>";
                        Body += "Request No :" + requestno;
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
                        return "Success";
                    }
                    return "Success";
                }
                catch (Exception)
                {
                    return "sorry";
                }
            }
            else
            {
                return "Success";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string StockRequestDetailsPendingUpload(string requestno, string approved, string Stat)
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, requestno);
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
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    if (approved == "True")
                    {
                        x.QuantityStringIssue = dt.Rows[i]["QuantityIssued"].ToString();
                    }
                    else
                    {
                        if (Stat == "Pending Approval")
                        {
                            x.QuantityStringIssue = "<input class='form-control issueqty' type='number' min='1' id='qsi" + i + "' onchange='Math.max(this.value,1); updateissuequantity(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")' value='" + dt.Rows[i]["QuantityIssued"].ToString() + "' onkeypress='return onlyNos(event,this)' ondrop='return false;' onpaste='return false;' disabled/>";

                        }
                        else
                        {
                            x.QuantityStringIssue = "<input class='form-control issueqty' type='number' min='1' id='qsi" + i + "' onchange='Math.max(this.value,1); updateissuequantity(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")' value='" + dt.Rows[i]["QuantityIssued"].ToString() + "' onkeypress='return onlyNos(event,this)' ondrop='return false;' onpaste='return false;'/>";

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

    #endregion datatables

    #region Methods

    [WebMethod]
    public static string UpdateQuantityIssue(int recid, int issuequantity)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.RecID = recid;
        x.Quantity = issuequantity;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.UpdateQuantityIssue(x).ToString();
    }

    [WebMethod]
    public static string StockIssuanceGetCount(int employeeid, int custodian)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        StockEntity x = new StockEntity();

        x.EmployeeID = employeeid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockGetCount(x);
    }

    [WebMethod]
    public static string IfIssuanceNeedsApproval()
    {
        return Common.StockIssuanceApproval;
    }

    [WebMethod]
    public static List<ListItem> GetApprover(int issuedto)
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Common.SiteID);
        userEntity.UserID = Convert.ToInt32(Common.UserID);
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetApproverInIssuance(userEntity, issuedto);
    }

    [WebMethod]
    public static string GetCountChecker(string issuanceno)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        StockEntity x = new StockEntity();

        x.IssuanceNo = issuanceno;
        x.ConnectionString = Common.ConnString;

        return y.GetCountChecker(x);
    }

    [WebMethod]
    public static string StockTransferToIssuance(int smlid, int employeeid, int quantity, int custodian)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        x.EmployeeID = employeeid;
        x.CustodianID = custodian;
        x.Quantity = quantity;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockTransferToIssuance(x).ToString();
    }

    [WebMethod]
    public static string StockRemoveToIssuance(int smlid, int employeeid)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        x.EmployeeID = employeeid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRemoveToIssuance(x).ToString();
    }

    [WebMethod]
    public static string StockRemoveAllToIssuance(int employeeid, int custodian)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        StockEntity x = new StockEntity();

        x.EmployeeID = employeeid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRemoveAllToIssuance(x).ToString();
    }

    [WebMethod]
    public static string CancelStockRequest(string requestno, string remarks)
    {
        try
        {
            StockRequestBll y = new StockRequestBll();
            StockEntity x = new StockEntity();

            x.RequestNo = requestno;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string returnresult = y.CancelStockRequest(x);
            return returnresult;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [WebMethod]
    public static string ConfirmStockIssuance(int employeeid, int custodian, int approverid, string remarks)
    {
        try
        {
            StockIssuanceBll y = new StockIssuanceBll();
            StockEntity x = new StockEntity();

            x.EmployeeID = employeeid;
            x.CustodianID = custodian;
            x.ApproverID = approverid;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string returnresult = y.ConfirmStockIssuance(x);
            if (Common.StockIssuanceApproval == "True")
            {
                try
                {
                    UserBll userBll = new UserBll();
                    string ApprovalDetails = userBll.GetEmail(Common.ConnString, approverid);

                    string smtphost = Common.SMTPhost;
                    string smtpport = Common.SMTPport;
                    string smtpmail = Common.SMTPmail;
                    string smtppass = Common.SMTPpass;

                    DataTable dt = y.GetStockIssuanceList1(Common.ConnString, employeeid, custodian, Common.UserName, returnresult);


                    if (ApprovalDetails.Split('|')[0] != "")
                    {
                        string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                        MailMessage mail = new MailMessage();
                        mail.To.Add(ApprovalDetails.Split('|')[0]);

                        mail.From = new MailAddress(smtpmail);
                        mail.Subject = Common.EmailSubject + " (Stock Issuance)";
                        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + ApprovalDetails.Split('|')[1] + ",<br><br><br>";
                        Body += "A STOCK ISSUANCE transaction is AWAITING FOR YOUR APPROVAL <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>Stock Issuance No: " + returnresult;

                        if (dt.Rows.Count > 0)
                        {
                            Body += "<table border='1'><tr>"
                               + "<th>ITEM CODE</th>"
                               + "<th>Quantity</th></tr>";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Body += "<tr>";
                                Body += "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                                     + "<td>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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

                    }
                    return returnresult;
                }
                catch (Exception)
                {
                    return "sorry";
                }
            }
            else
            {
                return returnresult;
            }

        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [WebMethod]
    public static string CancelStockIssuance(string issuanceno, string remarks)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        StockEntity x = new StockEntity();

        x.CreatedBy = Common.UserName;
        x.IssuanceNo = issuanceno;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;

        y.CancelStockIssuance(x);

        return "";
    }

    public bool success = false;

    [WebMethod]
    public static string UploadStockIssuance(string issuanceno)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        string valuestring = "";
        DataTable dt = y.UploadStockIssuanceDT(Common.ConnString, issuanceno, Common.UserName);
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
            string issuanceno = txtStockIssuanceNo.Text;

            if (fuDocuments.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/StockIssuance/" + issuanceno + "/");
                string pathFinal = rootPath + '/' + issuanceno + '/';
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
                    UploadStockIssuance(issuanceno);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadsuccess();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:filenotseen();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:filenotseen();", true);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    [WebMethod]
    public static string GenerateStockIssuanceReport(string issuanceno)
    {
        StockEntity x = new StockEntity();
        StockIssuanceBll y = new StockIssuanceBll();

        x.IssuanceNo = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.StockIssuanceDetails(x);
        string employeename = issuancedetails.Split('|')[1];
        string employeedep = issuancedetails.Split('|')[2];
        string employeepos = issuancedetails.Split('|')[3];
        string approvername = issuancedetails.Split('|')[4];
        string transactorname = issuancedetails.Split('|')[5];
        string transactordep = issuancedetails.Split('|')[6];
        string transactorpos = issuancedetails.Split('|')[7];
        string custodian = issuancedetails.Split('|')[8];
        string remarks = issuancedetails.Split('|')[9];
        string issueddate = issuancedetails.Split('|')[10];

        DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

        return FormsAndReports.GenerateRequestFormCTRIssuance(dt, issuanceno, employeename, employeedep, employeepos, issueddate, remarks, custodian, approvername, issueddate);
    }

    [WebMethod]
    public static string GenerateStockIssuanceReportRIS(string issuanceno)
    {
        StockEntity x = new StockEntity();
        StockIssuanceBll y = new StockIssuanceBll();

        x.IssuanceNo = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.StockIssuanceDetails(x);
        string employeename = issuancedetails.Split('|')[1];
        string employeedep = issuancedetails.Split('|')[2];
        string employeepos = issuancedetails.Split('|')[3];
        string approvername = issuancedetails.Split('|')[4];
        string transactorname = issuancedetails.Split('|')[5];
        string transactordep = issuancedetails.Split('|')[6];
        string transactorpos = issuancedetails.Split('|')[7];
        string custodian = issuancedetails.Split('|')[8];
        string remarks = issuancedetails.Split('|')[9];
        string issueddate = issuancedetails.Split('|')[10];
        string site = issuancedetails.Split('|')[13];
        string approverpos = issuancedetails.Split('|')[14];


        DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

        return FormsAndReports.GenerateStockIssuanceFormRIS(dt, issuanceno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos);
    }

    

    [WebMethod]
    public static string GenerateStockIssuanceReportITR(string issuanceno)
    {
        StockEntity x = new StockEntity();
        StockIssuanceBll y = new StockIssuanceBll();

        x.IssuanceNo = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.StockIssuanceDetails(x);
        string employeename = issuancedetails.Split('|')[1];
        string employeedep = issuancedetails.Split('|')[2];
        string employeepos = issuancedetails.Split('|')[3];
        string approvername = issuancedetails.Split('|')[4];
        string transactorname = issuancedetails.Split('|')[5];
        string transactordep = issuancedetails.Split('|')[6];
        string transactorpos = issuancedetails.Split('|')[7];
        string custodian = issuancedetails.Split('|')[8];
        string remarks = issuancedetails.Split('|')[9];
        string issueddate = issuancedetails.Split('|')[10];
        string site = issuancedetails.Split('|')[13];

        DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

        return FormsAndReports.GenerateStockIssuanceFormITR(dt, issuanceno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }


    [WebMethod]
    public static string GenerateStockRequestReport(string requestno)
    {
        StockEntity x = new StockEntity();
        StockRequestBll y = new StockRequestBll();

        x.RequestNo = requestno;
        x.ConnectionString = Common.ConnString;

        string requestdetails = y.StockRequestDetails(x);
        string employeename = requestdetails.Split('|')[1];
        string employeedep = requestdetails.Split('|')[2];
        string employeepos = requestdetails.Split('|')[3];
        string approvername = requestdetails.Split('|')[4];
        string transactorname = requestdetails.Split('|')[5];
        string transactordep = requestdetails.Split('|')[6];
        string transactorpos = requestdetails.Split('|')[7];
        string custodian = requestdetails.Split('|')[8];
        string remarks = requestdetails.Split('|')[9];
        string issueddate = requestdetails.Split('|')[10];
        string approverpos = requestdetails.Split('|')[12];
        string site = requestdetails.Split('|')[13];


        DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, requestno);

        return FormsAndReports.GenerateStockIssuanceRequestFormRIS(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos);
    }

    

    #endregion Methods

    #region Request Upload

    [WebMethod]
    public static string UploadStockRequest(string requestno)
    {
        StockIssuanceBll y = new StockIssuanceBll();

        DataTable dt = y.UploadStockRequestDT(Common.ConnString, requestno, Common.UserName);

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

    protected void btnUpload_ClickRequest(object sender, EventArgs e)
    {
        try
        {
            string requestno = txtStockRequestNo.Text;

            if (fuDocuments.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/StockRequest/" + requestno + "/");
                string pathFinal = rootPath + '/' + requestno + '/';
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
                    UploadStockRequest(requestno);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadsuccess();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:filenotseen();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:filenotseen();", true);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    #endregion Request Upload

    [WebMethod]
    public static string CreateITR(string issuanceno,
       string itrapprovedpkipassword,
       string itrreleasedpkipassword,
       string itrreceivedpkipassword,
       string itrapprovedimagebase64,
       string itrreleaseimagebase64,
       string itrreceivedimagebase64,
       string itrapprovedcertbase64,
       string itrreleasecertbase64,
       string itrreceivedcertbase64,
       bool itrapproveddatestamp,
       bool itrreleaseddatestamp,
       bool itrreceiveddatestamp
    )
    {
        try
        {
            StockEntity x = new StockEntity();
            StockIssuanceBll y = new StockIssuanceBll();

            x.IssuanceNo = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.StockIssuanceDetails(x);
            string employeename = issuancedetails.Split('|')[1];
            string employeedep = issuancedetails.Split('|')[2];
            string employeepos = issuancedetails.Split('|')[3];
            string approvername = issuancedetails.Split('|')[4];
            string transactorname = issuancedetails.Split('|')[5];
            string transactordep = issuancedetails.Split('|')[6];
            string transactorpos = issuancedetails.Split('|')[7];
            string custodian = issuancedetails.Split('|')[8];
            string remarks = issuancedetails.Split('|')[9];
            string issueddate = issuancedetails.Split('|')[10];
            string site = issuancedetails.Split('|')[13];

            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 253px; }"
            + ".page-footer, .page-footer-space {height: 220px;}"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY TRANSFER REPORT<h3></td</tr>";
            html += "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
            html += "<tr><td><table style='font-size:75%;';>";
            html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%'>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
            html += "<tr><td>Signature :</td><td ><img src='" + itrapprovedimagebase64 + "' width='50%' height='75' /><br />" + (itrapproveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreleaseimagebase64 + "' width='50%' height='75' /><br />" + (itrreleaseddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreceivedimagebase64 + "' width='50%' height='75' /><br />" + (itrreceiveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td></tr>";
            html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
            html += "<tr><td>Designation :</td><td></td><td>" + transactorpos + "</td><td>" + employeepos + "</td></tr>";
            html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
            html += "</table></table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
                      + "<tr>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
                      + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

            #endregion footer placeholder

            byte[] pdf = PDFConverter.ConvertHTMLtoPDF(html, PaperSize.A4, 1.25.Centimeters(), PDFConverter.PageOrientation.Portrait);

            pdf = PDFConverter.AddSignature(pdf, itrapprovedcertbase64, itrapprovedpkipassword);
            pdf = PDFConverter.AddSignature(pdf, itrreleasecertbase64, itrreleasedpkipassword);
            pdf = PDFConverter.AddSignature(pdf, itrreceivedcertbase64, itrreceivedpkipassword);

            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
    }

    [WebMethod]
    public static string CreateRISRequest(string issuanceno,
      string requestedpkipassword,
      string approvedpkipassword,
      string issuedpkipassword,
      string requestedimagebase64,
      string approvedimagebase64,
      string issuedimagebase64,
      string requestedcertbase64,
      string approvedcertbase64,
      string issuedcertbase64,
      bool requesteddatestamp,
      bool approveddatestamp,
      bool issueddatestamp
   )
    {
        try
        {
            StockEntity x = new StockEntity();
            StockRequestBll y = new StockRequestBll();

            x.RequestNo = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.StockRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = requestdetails.Split('|')[1];
            string transactordep = requestdetails.Split('|')[2];
            string transactorpos = requestdetails.Split('|')[3];
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string site = "";

            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 250px; }"
            + ".page-footer, .page-footer-space {height: 250px;}"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by<br /><center><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td><td valign='top'; align='left'>Approved by<br /><center><img src='" + approvedimagebase64 + "' width='50%' height='75' /><br />" + (approveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td><td valign='top'; align='left'>Issued by<br /><center><img src='" + issuedimagebase64 + "' width='50%' height='75' /><br />" + (issueddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td><td valign='top'; align='left'>Recieved by<br /><center><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeepos + "</td><td></td><td>" + transactordep + "</td><td style='text-align: center;'>" + employeepos + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedep + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
                     + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string statusYes = "✓";
                    string statusNo = "";
                    if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
                    {
                    }
                    else
                    {
                        statusYes = "";
                        statusNo = "✓";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["QuantityIssued"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

            #endregion footer placeholder

            byte[] pdf = PDFConverter.ConvertHTMLtoPDF(html, PaperSize.A4, 1.25.Centimeters(), PDFConverter.PageOrientation.Portrait);

            pdf = PDFConverter.AddSignature(pdf, requestedcertbase64, requestedpkipassword);
            pdf = PDFConverter.AddSignature(pdf, approvedcertbase64, approvedpkipassword);
            pdf = PDFConverter.AddSignature(pdf, issuedcertbase64, issuedpkipassword);


            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
    }

    [WebMethod]
    public static string CreateRequestRISHTML(string issuanceno,
    string imagebase64,
    string passphrase,
    string certbase64,
    bool datestamp,
    string transtype,
    string operationtype
  )
    {
        try
        {
            StockEntity x = new StockEntity();
            StockRequestBll y = new StockRequestBll();

            x.RequestNo = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.StockRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = requestdetails.Split('|')[1];
            string transactordep = requestdetails.Split('|')[2];
            string transactorpos = requestdetails.Split('|')[3];
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string site = "";

            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 255px; }"
            + ".page-footer, .page-footer-space {height: 250px;}"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions
            html += "<p style='font-size:11.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px; font-size:11.5px; font-family: Arial;'; valign='bottom'; align='left'; >Signature :</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left';>Requested by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Approved by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Issued by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Printed Name :</td><td style='text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + approvername + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + transactorname + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Designation :</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + transactordep + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + transactordep + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Date :</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issueddate + "</td><td style='font-size:11.5px; font-family: Arial;text-align: center;'>" + issueddate + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issueddate + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedep + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
                     + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string statusYes = "✓";
                    string statusNo = "";
                    if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
                    {
                    }
                    else
                    {
                        statusYes = "";
                        statusNo = "✓";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["QuantityIssued"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

            #endregion footer placeholder


            return PDFConverter.CreatePDF(html, imagebase64, certbase64, passphrase, datestamp, issuanceno, transtype, operationtype);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
    }

    [WebMethod]
    public static string CreateITRHTML(string issuanceno,
    string imagebase64,
    string passphrase,
    string certbase64,
    bool datestamp,
    string transtype,
    string operationtype
   )
    {
        try
        {
            StockEntity x = new StockEntity();
            StockIssuanceBll y = new StockIssuanceBll();

            x.IssuanceNo = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.StockIssuanceDetails(x);
            string employeename = issuancedetails.Split('|')[1];
            string employeedep = issuancedetails.Split('|')[2];
            string employeepos = issuancedetails.Split('|')[3];
            string approvername = issuancedetails.Split('|')[4];
            string transactorname = issuancedetails.Split('|')[5];
            string transactordep = issuancedetails.Split('|')[6];
            string transactorpos = issuancedetails.Split('|')[7];
            string custodian = issuancedetails.Split('|')[8];
            string remarks = issuancedetails.Split('|')[9];
            string issueddate = issuancedetails.Split('|')[10];
            string site = issuancedetails.Split('|')[13];

            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 253px; }"
            + ".page-footer, .page-footer-space {height: 220px;}"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY TRANSFER REPORT<h3></td</tr>";
            html += "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
            html += "<tr><td><table style='font-size:75%;';>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;' width='23%';></td><td style='font-size:9.5px; font-family: Arial;' width='35%';>Approved by</td><td style='font-size:9.5px; font-family: Arial;' width='28%';>Released/Issued by:</td><td style='font-size:9.5px; font-family: Arial;' width='26%';>Received by:</td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Signature :</td><td style='font-size:9.5px; font-family: Arial;'><div style='width: 50%; height: 75px;' ></div><br /></td><td style='font-size:9.5px; font-family: Arial;'><div style='width: 50%; height: 75px;' ></div><br /></td><td style='font-size:9.5px; font-family: Arial;'><div style='width: 50%; height: 75px;' ></div><br /></td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Printed Name :</td><td style='font-size:9.5px; font-family: Arial;'>" + approvername + "</td><td style='font-size:9.5px; font-family: Arial;'>" + transactorname + "</td><td style='font-size:9.5px; font-family: Arial;'>" + employeename + "</td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Designation :</td><td ></td><td style='font-size:9.5px; font-family: Arial;'>" + transactorpos + "</td><td style='font-size:9.5px; font-family: Arial;'>" + employeepos + "</td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Date :</td><td style='font-size:9.5px; font-family: Arial;'>" + issueddate + "</td><td style='font-size:9.5px; font-family: Arial;'>" + issueddate + "</td><td style='font-size:9.5px; font-family: Arial;'>" + issueddate + "</td></tr>";
            html += "</table></table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
                      + "<tr>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
                      + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

            #endregion footer placeholder


            return PDFConverter.CreatePDF(html, imagebase64, certbase64, passphrase, datestamp, issuanceno, transtype, operationtype);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
    }

    [WebMethod]
    public static string CreateRISHTML(string issuanceno,
    string imagebase64,
    string passphrase,
    string certbase64,
    bool datestamp,
    string transtype,
    string operationtype
    )
    {
        try
        {
            StockEntity x = new StockEntity();
            StockIssuanceBll y = new StockIssuanceBll();

            x.IssuanceNo = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.StockIssuanceDetails(x);
            string employeename = issuancedetails.Split('|')[1];
            string employeedep = issuancedetails.Split('|')[2];
            string employeepos = issuancedetails.Split('|')[3];
            string approvername = issuancedetails.Split('|')[4];
            string transactorname = issuancedetails.Split('|')[5];
            string transactordep = issuancedetails.Split('|')[6];
            string transactorpos = issuancedetails.Split('|')[7];
            string custodian = issuancedetails.Split('|')[8];
            string remarks = issuancedetails.Split('|')[9];
            string issueddate = issuancedetails.Split('|')[10];
            string site = issuancedetails.Split('|')[13];

            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 255px; }"
            + ".page-footer, .page-footer-space {height: 250px;}"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer


            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions
            html += "<p style='font-size:11.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px; font-size:11.5px; font-family: Arial;'; valign='bottom'; align='left'; >Signature :</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left';>Requested by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Approved by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Issued by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Printed Name :</td><td style='text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + approvername + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + transactorname + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Designation :</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + transactordep + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + transactordep + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Date :</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issueddate + "</td><td style='font-size:11.5px; font-family: Arial;text-align: center;'>" + issueddate + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issueddate + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";

            ////footer div

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedep + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
                     + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string statusYes = "✓";
                    string statusNo = "";
                    if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
                    {
                    }
                    else
                    {
                        statusYes = "";
                        statusNo = "✓";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + remarks + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

            #endregion footer placeholder
            return PDFConverter.CreatePDF(html, imagebase64, certbase64, passphrase, datestamp, issuanceno, transtype, operationtype);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
    }

    [WebMethod]
    public static string CreateRIS(string issuanceno,
       string requestedpkipassword,
       string approvedpkipassword,
       string issuedpkipassword,
       string requestedimagebase64,
       string approvedimagebase64,
       string issuedimagebase64,
       string requestedcertbase64,
       string approvedcertbase64,
       string issuedcertbase64,
       bool requesteddatestamp,
       bool approveddatestamp,
       bool issueddatestamp
    )
    {
        try
        {
            StockEntity x = new StockEntity();
            StockIssuanceBll y = new StockIssuanceBll();

            x.IssuanceNo = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.StockIssuanceDetails(x);
            string employeename = issuancedetails.Split('|')[1];
            string employeedep = issuancedetails.Split('|')[2];
            string employeepos = issuancedetails.Split('|')[3];
            string approvername = issuancedetails.Split('|')[4];
            string transactorname = issuancedetails.Split('|')[5];
            string transactordep = issuancedetails.Split('|')[6];
            string transactorpos = issuancedetails.Split('|')[7];
            string custodian = issuancedetails.Split('|')[8];
            string remarks = issuancedetails.Split('|')[9];
            string issueddate = issuancedetails.Split('|')[10];
            string site = issuancedetails.Split('|')[13];

            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 250px; }"
            + ".page-footer, .page-footer-space {height: 250px;}"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by<br /><center><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td><td valign='top'; align='left'>Approved by<br /><center><img src='" + approvedimagebase64 + "' width='50%' height='75' /><br />" + (approveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td><td valign='top'; align='left'>Issued by<br /><center><img src='" + issuedimagebase64 + "' width='50%' height='75' /><br />" + (issueddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td><td valign='top'; align='left'>Recieved by<br /><center><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</center></td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeepos + "</td><td></td><td>" + transactordep + "</td><td style='text-align: center;'>" + employeepos + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedep + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
                     + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string statusYes = "✓";
                    string statusNo = "";
                    if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
                    {
                    }
                    else
                    {
                        statusYes = "";
                        statusNo = "✓";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + remarks + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

            #endregion footer placeholder

            byte[] pdf = PDFConverter.ConvertHTMLtoPDF(html, PaperSize.A4, 1.25.Centimeters(), PDFConverter.PageOrientation.Portrait);

            pdf = PDFConverter.AddSignature(pdf, requestedcertbase64, requestedpkipassword);
            pdf = PDFConverter.AddSignature(pdf, approvedcertbase64, approvedpkipassword);
            pdf = PDFConverter.AddSignature(pdf, issuedcertbase64, issuedpkipassword);

            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
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