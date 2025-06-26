using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

public partial class Home_RequestManagement : BasePage
{
    private static SqlConnection con = new SqlConnection();
    private static SqlConnection conCurrent = new SqlConnection();
    private static SqlCommand sqlcmd = new SqlCommand();
    private static SqlDataAdapter da = new SqlDataAdapter();
    private static DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        GetDirectory();
    }

    public void GetDirectory()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select DBDirectoryPath from tbl_Miscellaneous";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            txtDirectory.Text = dr1[0].ToString().Replace('.', '\\');
            connection.Close();
        }
        else
        {
            connection.Close();
        }
    }

    #region Goods Receive

    [WebMethod]
    public static string GetGoodsReceiveHeader()
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetGoodsReceiveForApproval(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["GoodsReceiveNumber"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/GoodsReceive/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.GoodsReceiveNumber = dt.Rows[i]["GoodsReceiveNumber"].ToString();
                    x.Receiver = dt.Rows[i]["Receiver"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    //x.NoOfItem = dt.Rows[i]["NoOfItem"].ToString();
                    x.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["Approver"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/GoodsReceive/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    //x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = " <center><button class='btn btn-xs  btn-primary fa fa-eye btn_View' type='button' data-toggle='tooltip'  title='View Details' onclick=\"viewGRDetails('" + dt.Rows[i]["GoodsReceiveID"] + "')\"></button></center> ";
                    //+ " <button type='button' class='btn btn-med btn-danger fa fa-times btn_Cancel' data-toggle='tooltip' title='Cancel Goods Receive' onclick=\"cancelGoodsReceive('" + dt.Rows[i]["GoodsReceiveID"] + "')\"></button> </center>";
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
    public static string GetGoodsReceiveDetails(int goodsreceiveid)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    if (dt.Rows[i]["PropertyID"].ToString() == "" || dt.Rows[i]["PropertyID"].ToString() == "NULL")
                    {
                        x.AssetType = "Stock";
                    }
                    else
                    {
                        x.AssetType = "Property";
                    }
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["TotalCost"].ToString());

                    x.Action = " <center><button class='btn btn-xs  btn-danger fa fa-close btn_Remove' type='button' data-toggle='tooltip'  title='Remove to Goods Receive' onclick=\"RemoveRMGoodsReceive('" + dt.Rows[i]["DetailsID"] + "','" + dt.Rows[i]["GRPropertyID"] + "','" + dt.Rows[i]["GoodsReceiveNumber"] + "')\"></button> </center>";

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
    public static string GetSupplyAllocationHeader(int goodsreceiveid)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    if (dt.Rows[i]["PropertyID"].ToString() == "" || dt.Rows[i]["PropertyID"].ToString() == "NULL")
                    {
                        x.AssetType = "Stock";
                    }
                    else
                    {
                        x.AssetType = "Property";
                    }
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["TotalCost"].ToString());

                    x.Action = " <center><button class='btn btn-xs  btn-danger fa fa-close btn_Remove' type='button' data-toggle='tooltip'  title='Remove to Goods Receive' onclick=\"RemoveRMGoodsReceive('" + dt.Rows[i]["DetailsID"] + "','" + dt.Rows[i]["GRPropertyID"] + "','" + dt.Rows[i]["GoodsReceiveNumber"] + "')\"></button> </center>";

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
    public static string ApproveGoodsReceive(int goodsreceiveid)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyBll propertyBll = new PropertyBll();

            x.GoodsReceiveID = goodsreceiveid;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.ApproveGoodsReceive(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, goodsreceiveid);

            string grdetails = propertyBll.GoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            string grnumber = grdetails.Split('|')[0];
            string transactorname = grdetails.Split('|')[1];
            string transactoremail = grdetails.Split('|')[2];
            string approvername = grdetails.Split('|')[3];
            string approveremail = grdetails.Split('|')[4];
            string custodianname = grdetails.Split('|')[5];
            string createddate = grdetails.Split('|')[6];
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Goods Receive)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "Your Request for GOODS RECEIVE has been APPROVED.<br><br>"
                    + "Goods Receive No : " + grnumber + "<br>";

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

    [WebMethod]
    public static void RemoveRMGoodsReceive(int detailsid, int grpropertyid, string goodsreceivenumber)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.GRPropertyID = grpropertyid;
        x.DetailsID = detailsid;
        x.GoodsReceiveNumber = goodsreceivenumber;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        propertyBll.RemoveRMGoodsReceive(x);
    }

    [WebMethod]
    public static string DeclineGoodsReceive(int goodsreceiveid, string remarks)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyBll propertyBll = new PropertyBll();

            x.GoodsReceiveID = goodsreceiveid;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.DeclineGoodsReceive(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string grdetails = propertyBll.GoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            string grnumber = grdetails.Split('|')[0];
            string transactorname = grdetails.Split('|')[1];
            string transactoremail = grdetails.Split('|')[2];
            string approvername = grdetails.Split('|')[3];
            string approveremail = grdetails.Split('|')[4];
            string custodianname = grdetails.Split('|')[5];
            string createddate = grdetails.Split('|')[6];
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Goods Receive)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarks + ".<br><br>"
                    + "Goods Receive No : " + grnumber + "<br><br>";
                //    + "REMARKS : " + remarks + "<br/>";

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

    #endregion Goods Receive

    #region Property Issuance

    [WebMethod]
    public static string GetPendingApprovalPropertyIssuance()
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPendingApprovalPropertyIssuance(Common.UserName, Common.Administrative, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["IssuanceNo"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyIssuance/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyIssuance/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssetsIssuanceDetails('" + dt.Rows[i]["IssuanceNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelTransaction('" + dt.Rows[i]["IssuanceNo"] + "')\" ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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

    //Pending Upload Property Issuance  List
    [WebMethod]
    public static string GetPropertyIssuanceList(string issuanceno)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.IssuanceNumber = issuanceno;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PLPendingUpload(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

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
    public static string ApprovePropertyIssuance(string issuanceno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll propertyBll = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.ApprovePropertyIssuance(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string issuancedetails = propertyBll.PropertyIssuanceDetails(x);
            string employeename = issuancedetails.Split('|')[1];
            string employeedep = issuancedetails.Split('|')[2];
            string employeepos = issuancedetails.Split('|')[3];
            string approvername = issuancedetails.Split('|')[4];
            string transactorname = issuancedetails.Split('|')[5];
            string transactordep = issuancedetails.Split('|')[6];
            string transactorpos = issuancedetails.Split('|')[7];
            string custodian = issuancedetails.Split('|')[8];
            string issueddate = issuancedetails.Split('|')[10];
            string approveremail = issuancedetails.Split('|')[11];
            string transactoremail = issuancedetails.Split('|')[12];
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Issuance)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "Your Request for PROPERTY ISSUANCE has been APPROVED.<br><br>"
                    + "Issuance No : " + issuanceno + "<br>";

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

    [WebMethod]
    public static string DeclinePropertyIssuance(string issuanceno, string remarks)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll propertyBll = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.DeclinePropertyIssuance(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string issuancedetails = propertyBll.PropertyIssuanceDetails(x);
            string employeename = issuancedetails.Split('|')[1];
            string employeedep = issuancedetails.Split('|')[2];
            string employeepos = issuancedetails.Split('|')[3];
            string approvername = issuancedetails.Split('|')[4];
            string transactorname = issuancedetails.Split('|')[5];
            string transactordep = issuancedetails.Split('|')[6];
            string transactorpos = issuancedetails.Split('|')[7];
            string custodian = issuancedetails.Split('|')[8];
            string issueddate = issuancedetails.Split('|')[10];
            string approveremail = issuancedetails.Split('|')[11];
            string transactoremail = issuancedetails.Split('|')[12];
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Issuance)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarks + ".<br><br>"
                    + "Issuance No : " + issuanceno + "<br><br>";

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

    #endregion Property Issuance

    #region Property Request

    //Pending Upload Request Transaction
    [WebMethod]
    public static string PropertyRequestPendingApproval()
    {
        PropertyRequestBll y = new PropertyRequestBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyRequestPendingUpload(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["RequestNo"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyRequest/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.RequestNumber = dt.Rows[i]["RequestNo"].ToString();
                    x.RequestedBy = dt.Rows[i]["RequestorName"].ToString();
                    //x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfReturn = Convert.ToDateTime(dt.Rows[i]["DateOfReturn"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyRequest/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssetsRequest('" + dt.Rows[i]["RequestNo"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelTransactionRequest('" + dt.Rows[i]["RequestNo"] + "')\" ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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

    //Pending Upload Property Request List
    [WebMethod]
    public static string GetPRequestPendingUploadList(string requestnumber)
    {
        PropertyRequestBll y = new PropertyRequestBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.RequestNumber = requestnumber;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PRLPendingUpload(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

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
    public static string ApprovePropertyRequest(string requestno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyRequestBll propertyBll = new PropertyRequestBll();

            x.RequestNumber = requestno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.ApprovePropertyRequest(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string requestdetails = propertyBll.PropertyRequestDetails(x);
            string requestorname = requestdetails.Split('|')[1];
            string requestordept = requestdetails.Split('|')[2];
            string requestorpos = requestdetails.Split('|')[3];
            string requestoremail = requestdetails.Split('|')[4];
            string approvername = requestdetails.Split('|')[5];
            string approveremail = requestdetails.Split('|')[6];
            string createddate = requestdetails.Split('|')[9];
            if (requestoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(requestoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + requestorname + ",<br><br><br>";
                Body += "Your Request for PROPERTY REQUEST has been APPROVED.<br><br>"
                    + "Request No : " + requestno + "<br>";

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

    [WebMethod]
    public static string DeclinePropertyRequest(string requestno, string remarks)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyRequestBll propertyBll = new PropertyRequestBll();

            x.RequestNumber = requestno;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.DeclinePropertyRequest(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string requestdetails = propertyBll.PropertyRequestDetails(x);
            string requestorname = requestdetails.Split('|')[1];
            string requestordept = requestdetails.Split('|')[2];
            string requestorpos = requestdetails.Split('|')[3];
            string requestoremail = requestdetails.Split('|')[4];
            string approvername = requestdetails.Split('|')[5];
            string approveremail = requestdetails.Split('|')[6];
            string createddate = requestdetails.Split('|')[9];
            if (requestoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(requestoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + requestorname + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarks + ".<br><br>"
                    + "Request No : " + requestno + "<br><br>";

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

    #endregion Property Request

    #region Property Extension of ReturnDate

    [WebMethod]
    public static string PropertyExtensionDateRequestList()
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyExtensionDateRequestPendingApproval(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();

                    x.Approver = dt.Rows[i]["Approver"].ToString();
                    x.DateOfReturn = Convert.ToDateTime(dt.Rows[i]["DateOfReturn"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.DateOfExtension = Convert.ToDateTime(dt.Rows[i]["ExtensionDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                    x.Action = "<center><a href='' class='btn btn-xs btn-success btn_ApproveExtension' data-toggle='tooltip'  title='Approve Extension' onclick=\"ApproveExtension('" + dt.Rows[i]["RecID"] + "')\"><span class='fa fa-check'></span></a> "
                        + " <a href='' class='btn btn-xs btn-danger btn_DeclineExtension' data-toggle='tooltip'  title='Decline Extension' onclick=\"DeclineExtension('" + dt.Rows[i]["RecID"] + "')\"><span class='fa fa-remove'></span></a></center>";
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
    public static string ApproveExtension(int recid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.RecID = recid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.ApproveExtension(x);

        return "";
    }

    [WebMethod]
    public static string DeclineExtension(int recid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.RecID = recid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.DeclineExtension(x);

        return "";
    }

    #endregion

    #region Property Transfer

    [WebMethod]
    public static string PropertyTransferPendingApproval()
    {
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertytransferBll.PropertyTransferPendingApproval(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["TransferNumber"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyTransfer/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.TransferNumber = dt.Rows[i]["TransferNumber"].ToString();
                    x.RemarksTransfer = dt.Rows[i]["Remarks"].ToString();
                    //x.StringNoOfItem = dt.Rows[i]["Quantity"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.DateOfTransfer = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyTransfer/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    x.Action = "<center><button class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["TransferNumber"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></button><center>";

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
    public static string PLPendingApproval(string transactionno)
    {
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertytransferBll.PLPendingApproval(Common.ConnString, transactionno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    if (dt.Rows[i]["TransactionType"].ToString() == "1")
                    {

                        x.Origin = dt.Rows[i]["FromEmployeeName"].ToString();
                        x.Destination = dt.Rows[i]["ToEmployeeName"].ToString();

                    }
                    else
                    {
                        if (dt.Rows[i]["FromAreaName"].ToString() != "")
                        {
                            x.Origin = dt.Rows[i]["FromSiteName"].ToString() + "-" + dt.Rows[i]["FromLocationName"].ToString() + "-" + dt.Rows[i]["FromAreaName"].ToString();
                        }
                        else
                        {
                            x.Origin = dt.Rows[i]["FromSiteName"].ToString() + "-" + dt.Rows[i]["FromLocationName"].ToString();
                        }

                        if (dt.Rows[i]["ToAreaName"].ToString() != "")
                        {
                            x.Destination = dt.Rows[i]["ToSiteName"].ToString() + "-" + dt.Rows[i]["ToLocationName"].ToString() + "-" + dt.Rows[i]["ToAreaName"].ToString();
                        }
                        else
                        {
                            x.Destination = dt.Rows[i]["ToSiteName"].ToString() + "-" + dt.Rows[i]["ToLocationName"].ToString();
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
    public static string ApprovePropertyTransfer(string transferno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyTransferBll propertyBll = new PropertyTransferBll();

            x.TransferNumber = transferno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.ApprovePropertyTransfer(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string transferdetails = propertyBll.PropertyTransferDetails(Common.ConnString, transferno);
            string approvername = transferdetails.Split('|')[1];
            string approveremail = transferdetails.Split('|')[2];
            string transactorname = transferdetails.Split('|')[3];
            string transactoremail = transferdetails.Split('|')[4];
            string custodian = transferdetails.Split('|')[5];
            string remarks = transferdetails.Split('|')[6];
            string createddate = transferdetails.Split('|')[7];
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Transfer)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "Your Request for PROPERTY TRANSFER has been APPROVED.<br><br>"
                    + "Transfer No : " + transferno + "<br>";

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

    [WebMethod]
    public static string DeclinePropertyTransfer(string transferno, string remarks)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyTransferBll propertyBll = new PropertyTransferBll();

            x.TransferNumber = transferno;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.DeclinePropertyTransfer(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string transferdetails = propertyBll.PropertyTransferDetails(Common.ConnString, transferno);
            string approvername = transferdetails.Split('|')[1];
            string approveremail = transferdetails.Split('|')[2];
            string transactorname = transferdetails.Split('|')[3];
            string transactoremail = transferdetails.Split('|')[4];
            string custodian = transferdetails.Split('|')[5];
            string remarkS = transferdetails.Split('|')[6];
            string createddate = transferdetails.Split('|')[7];
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Transfer)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarks + ".<br><br>"
                    + "Transfer No : " + transferno + "<br><br>";

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

    #endregion Property Transfer

    #region Property Disposal

    [WebMethod]
    public static string GetPendingApprovalPropertyDisposal()
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.DisposalPendingUploadView(Common.ConnString, Common.Administrative, Convert.ToInt32(Common.SiteID), Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.TransactionType = dt.Rows[i]["Type"].ToString();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString() == "0" ? "" : dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetails('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsApproved"] + "','" + dt.Rows[i]["Type"] + "','" + dt.Rows[i]["Remarks"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string GetPendingApprovalPropertyDisposalRequest()
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.DisposalRequestPendingUploadView(Common.ConnString, Common.Administrative, Convert.ToInt32(Common.SiteID), Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetailsRequest('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string DisposalPendingUploadViewInspection()
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.DisposalPendingUploadViewInspection(Common.ConnString, Common.Administrative, Convert.ToInt32(Common.SiteID), Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["InspectorName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetailsInspection('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string DisposalPendingUploadViewCertify()
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.DisposalPendingUploadViewCertify(Common.ConnString, Common.Administrative, Convert.ToInt32(Common.SiteID), Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["CertifierName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetailsCertification('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsApproved"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string GetPropertyDisposalListRequest(string disposalno)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.DisposalNumber = disposalno;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PDLPendingUploadRequest(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();

                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.Condition = dt.Rows[i]["Condition"].ToString();


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
    public static string GetPropertyDisposalList(string disposalno)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.DisposalNumber = disposalno;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PDLPendingUpload(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.Condition = dt.Rows[i]["Condition"].ToString();

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
    public static string GetPropertyJDisposalList(string disposalno, string remarks)
    {
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        string html = string.Empty;
        List<JunkDisposalEntity> JsonItemList = new List<JunkDisposalEntity>();
        try
        {
            string foldernameGR = disposalno;
            string[] filesGR = new string[0];
            try
            {
                filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/JunkDisposal/" + foldernameGR));
            }
            catch
            {

            }
            JunkDisposalEntity x = new JunkDisposalEntity();
            x.TransactionNo = disposalno;
            x.Remarks = remarks;
            foreach (string name in filesGR)
            {
                int j = name.Split('\\').Length;
                string[] file = name.Split('\\');
                x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/JunkDisposal/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
            }

            JsonItemList.Add(x);

            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = Int32.MaxValue;
            html = json.Serialize(JsonItemList);

            return html;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    ////TO BE DELETED
    [WebMethod]
    public static string ApprovePropertyDisposalRequest(string disposalno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll propertyBll = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            propertyBll.ApprovePropertyDisposalRequest(x);

            DataTable dt = propertyBll.PDLPendingUpload(x);

            string disposaldetails = propertyBll.PropertyDisposalRequestDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            string transactoremail = disposaldetails.Split('|')[10];
            string approveremail = disposaldetails.Split('|')[11];
            string inspectoremail = disposaldetails.Split('|')[12];
            string certifieremail = disposaldetails.Split('|')[13];

            if (inspectoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(inspectoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Disposal Request - Inspection)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + inspectorname + ",<br><br><br>";
                Body += "A PROPERTY DISPOSAL transaction is AWAITING FOR YOUR INSPECTION APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                    + "Disposal No : " + disposalno + "<br>"
                    + "Purpose/Remarks : " + remarks + " <br>"
                    + "Processed By : " + transactorname + " <br>"
                    + "Processed Date : " + disposaldate + "<br><br>";
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

                return "okay";
            }
            else
            {
                return "inspector has no email";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }

    //[WebMethod]
    //public static string DeclinePropertyDisposalRequest(string disposalno, string remarks)
    //{
    //    try
    //    {
    //        PropertyEntity x = new PropertyEntity();
    //        PropertyDisposalBll propertyBll = new PropertyDisposalBll();

    //        x.DisposalNumber = disposalno;
    //        x.Remarks = remarks;
    //        x.CreatedBy = Common.UserName;
    //        x.ConnectionString = Common.ConnString;

    //        string smtphost = Common.SMTPhost;
    //        string smtpport = Common.SMTPport;
    //        string smtpmail = Common.SMTPmail;
    //        string smtppass = Common.SMTPpass;

    //        propertyBll.DeclinePropertyDisposalRequest(x);

    //        string disposaldetails = propertyBll.PropertyDisposalRequestDetails(x);
    //        string transactorname = disposaldetails.Split('|')[1];
    //        string approvername = disposaldetails.Split('|')[2];
    //        string inspectorname = disposaldetails.Split('|')[3];
    //        string certifiername = disposaldetails.Split('|')[4];
    //        string transactordep = disposaldetails.Split('|')[5];
    //        string transactorpos = disposaldetails.Split('|')[6];
    //        string custodian = disposaldetails.Split('|')[7];
    //        string remarkS = disposaldetails.Split('|')[8];
    //        string disposaldate = disposaldetails.Split('|')[9];
    //        string transactoremail = disposaldetails.Split('|')[10];
    //        string approveremail = disposaldetails.Split('|')[11];
    //        string inspectoremail = disposaldetails.Split('|')[12];
    //        string certifieremail = disposaldetails.Split('|')[13];

    //        if (transactoremail != "")
    //        {
    //            string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

    //            MailMessage mail = new MailMessage();
    //            mail.To.Add(transactoremail);

    //            mail.From = new MailAddress(smtpmail);
    //            mail.Subject = Common.EmailSubject + " (Property Disposal Request - Approval)";
    //            string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
    //            Body += "I am declining this request with reason, " + remarks + ".<br><br>"
    //                + "Disposal No : " + disposalno + "<br><br>";

    //            Body += "<br><br><br><i>***This is a system generated mail. Please do not reply.***</i></font>";

    //            mail.Body = Body;
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = smtphost; //Or Your SMTP Server Address
    //            smtp.Port = Convert.ToInt32(smtpport);
    //            smtp.UseDefaultCredentials = false;
    //            smtp.Credentials = new System.Net.NetworkCredential
    //            (smtpmail, smtppass);

    //            //Or your Smtp Email ID and Password
    //            mail.IsBodyHtml = true;
    //            smtp.EnableSsl = true;

    //            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(mail);

    //            return "okay";
    //        }
    //        else
    //        {
    //            return "transactor has no email";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "sorry";
    //    }
    //}

    [WebMethod]
    public static string ApprovePropertyDisposal(string disposalno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll propertyBll = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            propertyBll.ApprovePropertyDisposal(x);

            DataTable dt = propertyBll.PDLPendingUpload(x);

            string disposaldetails = propertyBll.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            string transactoremail = disposaldetails.Split('|')[10];
            string approveremail = disposaldetails.Split('|')[11];
            string inspectoremail = disposaldetails.Split('|')[12];
            string certifieremail = disposaldetails.Split('|')[13];

            if (inspectoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(inspectoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Disposal - Inspection)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + inspectorname + ",<br><br><br>";
                Body += "A PROPERTY DISPOSAL transaction is AWAITING FOR YOUR INSPECTION APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                    + "Disposal No : " + disposalno + "<br>"
                    + "Purpose/Remarks : " + remarks + " <br>"
                    + "Processed By : " + transactorname + " <br>"
                    + "Processed Date : " + disposaldate + "<br><br>";
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

                return "okay";
            }
            else
            {
                return "inspector has no email";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }


    [WebMethod]
    public static string DeclinePropertyDisposal(string disposalno, string remarks)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll propertyBll = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            propertyBll.DeclinePropertyDisposal(x);

            string disposaldetails = propertyBll.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarkS = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            string transactoremail = disposaldetails.Split('|')[10];
            string approveremail = disposaldetails.Split('|')[11];
            string inspectoremail = disposaldetails.Split('|')[12];
            string certifieremail = disposaldetails.Split('|')[13];

            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Disposal - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarks + ".<br><br>"
                    + "Disposal No : " + disposalno + "<br><br>";

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
                return "transactor has no email";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }


    [WebMethod]
    public static string ApproveJDisposal(string disposalno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll propertyBll = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            propertyBll.ApproveJDisposal(x);


            return "okay";
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }


    [WebMethod]
    public static string DeclineJDisposal(string disposalno, string remarks)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll propertyBll = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.DeclineJDisposal(x);

            return "okay";
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }

    #endregion Property Disposal

    #region Property Gate Pass Request

    [WebMethod]
    public static string GetPropertyGatePassPendingUpload()
    {
        PropertyGatePassBll y = new PropertyGatePassBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyGatePassPendingApproval(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.GatePassNumber = dt.Rows[i]["GatePassNo"].ToString();
                    x.RequestedBy = dt.Rows[i]["AccountableName"].ToString();
                    x.NoOfItemReturn = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewList('" + dt.Rows[i]["GatePassNo"] + "')\"><span class='fa fa-eye'></span></a></center>";

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
    public static string PropertyGatePassRequestList(string gatepassno)
    {
        PropertyGatePassBll y = new PropertyGatePassBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyGatePassRequestList(Common.ConnString, gatepassno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

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
    public static string ApprovePropertyGatePass(string gatepassno)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyGatePassBll propertyBll = new PropertyGatePassBll();

            x.GatePassNumber = gatepassno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            propertyBll.ApprovePropertyGatePass(x);
            string gatepassdetails = propertyBll.PropertyGatePassDetails(x);
            string accountablename = gatepassdetails.Split('|')[1];
            string approvername = gatepassdetails.Split('|')[2];
            string createddate = gatepassdetails.Split('|')[4];
            string accountableemail = gatepassdetails.Split('|')[5];
            string approveremail = gatepassdetails.Split('|')[6];

            if (accountableemail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(accountableemail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Gate Pass Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + accountablename + ",<br><br><br>";
                Body += "Your PROPERTY GATE PASS REQUEST has been APPROVED.<br>"
                    + "Gate Pass No : " + gatepassno + "<br>";

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
    public static string DeclinePropertyGatePass(string gatepassno, string remarkS)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyGatePassBll propertyBll = new PropertyGatePassBll();

            x.GatePassNumber = gatepassno;
            x.Remarks = remarkS;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            propertyBll.DeclinePropertyGatePass(x);

            string gatepassdetails = propertyBll.PropertyGatePassDetails(x);
            string accountablename = gatepassdetails.Split('|')[1];
            string approvername = gatepassdetails.Split('|')[2];
            string createddate = gatepassdetails.Split('|')[4];
            string accountableemail = gatepassdetails.Split('|')[5];
            string approveremail = gatepassdetails.Split('|')[6];

            if (accountableemail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(accountableemail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Gate Pass Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + accountablename + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Gate Pass No : " + gatepassno + "<br><br>";

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

    #endregion Property Gate Pass Request

    #region Property Day Pass

    [WebMethod]
    public static string GetPropertyDayPassPendingApproval()
    {
        DayPassBll y = new DayPassBll();

        string html = string.Empty;
        List<DayPassEntity> JsonItemList = new List<DayPassEntity>();
        try
        {
            DataTable dt = y.GetPropertyDayPassPendingApprovalRM(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DayPassEntity x = new DayPassEntity();
                    x.DayPassNumber = dt.Rows[i]["DayPassNumber"].ToString();
                    x.RequestedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Purpose = dt.Rows[i]["Remarks"].ToString();
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewListDayPass('" + dt.Rows[i]["DayPassNumber"] + "')\"><span class='fa fa-eye'></span></a></center";// +
                                                                                                                                                                                                                                                                  //" <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"Cancel('" + dt.Rows[i]["DayPassNumber"] + "')\"><span class='fa fa-times-circle'></span></a></center>";

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
    public static string ApprovePropertyDayPass(string daypassno)
    {
        try
        {
            DayPassEntity x = new DayPassEntity();
            DayPassBll y = new DayPassBll();

            x.DayPassNumber = daypassno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            y.ApprovePropertyDayPass(x);

            string daypassdetails = y.PropertyDayPassDetails(x);
            string accountablename = daypassdetails.Split('|')[1];
            string approvername = daypassdetails.Split('|')[2];
            string createddate = daypassdetails.Split('|')[4];
            string accountableemail = daypassdetails.Split('|')[5];
            string approveremail = daypassdetails.Split('|')[6];

            if (accountableemail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(accountableemail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Day Pass Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + accountablename + ",<br><br><br>";
                Body += "Your PROPERTY DAY PASS REQUEST has been APPROVED.<br>"
                    + "Day Pass No : " + daypassno + "<br>";

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
                return "requestor has no email";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }

    [WebMethod]
    public static string DeclinePropertyDayPass(string daypassno, string remarkS)
    {
        try
        {
            DayPassEntity x = new DayPassEntity();
            DayPassBll y = new DayPassBll();

            x.DayPassNumber = daypassno;
            x.Remarks = remarkS;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            y.DeclinePropertyDayPass(x);

            string daypassdetails = y.PropertyDayPassDetails(x);
            string accountablename = daypassdetails.Split('|')[1];
            string approvername = daypassdetails.Split('|')[2];
            string createddate = daypassdetails.Split('|')[4];
            string accountableemail = daypassdetails.Split('|')[5];
            string approveremail = daypassdetails.Split('|')[6];

            if (accountableemail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(accountableemail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Day Pass Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + accountablename + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Day Pass No : " + daypassno + "<br><br>";

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
                return "requestor has no email";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "sorry";
        }
    }

    #endregion Property Day Pass

    #region Database Restore

    [WebMethod]
    public static string GetDatabaseRestore()
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<MiscEntity> JsonItemList = new List<MiscEntity>();
        try
        {
            DataTable dt = miscBll.GetDatabaseRestoreApproval(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MiscEntity x = new MiscEntity();
                    x.RestoreName = dt.Rows[i]["RestoreName"].ToString();
                    x.RestoreRequestor = dt.Rows[i]["Requestor"].ToString();
                    x.RequestedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                    //x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = " <center><button class='btn btn-med  btn-success fa fa-check btn_Approve' type='button' data-toggle='tooltip'  title='Approve Database Restore' onclick=\"ApproveDatabaseRestore('" + dt.Rows[i]["RestoreName"] + "')\"></button> "
                    + " <button type='button' class='btn btn-med btn-danger fa fa-times btn_Cancel' data-toggle='tooltip' title='Decline Database Restore' onclick=\"DeclineDatabaseRestore('" + dt.Rows[i]["RecordID"] + "')\"></button> </center>";
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

    protected void Restore(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        try
        {
            con.Close();
            conCurrent.Close();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["aimmis1"].ConnectionString;
            conCurrent.ConnectionString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(conCurrent.ConnectionString);
            string server = builder.DataSource;
            string database = builder.InitialCatalog;


            con.Open();


            Page page = (Page)HttpContext.Current.Handler;
            string sourceDir = txtDirectory.Text;
            string bakfilename = txtBakFileName.Value;

            sqlcmd = new SqlCommand("Alter Database " + database + " SET SINGLE_USER With ROLLBACK IMMEDIATE Restore database " + database + " from disk='" + sourceDir + bakfilename + "' Alter Database " + database + " SET MULTI_USER With ROLLBACK IMMEDIATE", con);
            sqlcmd.ExecuteNonQuery();

            //con.Open();
            //sqlcmd = new SqlCommand("Insert into tbl_ActivityLog (Activity, ActivityDate, UserID) values ('Successfully Restored Database to ['" + bakfilename + "']',GETDATE(),'" + Common.UserName + "')");
            //sqlcmd.ExecuteNonQuery();
            Response.Write("<script>alert('Restore database successfully');</script>");

            Common.LogOut();
            Response.Redirect("../Login.aspx");
        }
        catch (Exception ex)
        {
            //Response.Write("<script>alert('Error during database restoration!');</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:ErrorDatabaseRestore();", true);
        }
    }

    [WebMethod]
    public static string DeclineDatabaseRestore(int recordid)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.RestoreID = recordid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.DeclineDatabaseRestore(x).ToString();
    }

    #endregion Database Restore

    #region Stock Merge

    [WebMethod]
    public static string GetPendingApprovalStockMerge()
    {
        StockMergeBll y = new StockMergeBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetPendingApprovalStockMergeRM(Common.ConnString, Common.Administrative, Convert.ToInt32(Common.UserID));
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
                    x.TransactionDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-check btn_ApproveStockMerge' data-toggle='tooltip'  title='Approve Stock Merge' onclick=\"ApproveStockMerge('" + dt.Rows[i]["StockMergeNo"] + "')\"></a> "
                        + "<a href='' class='btn btn-xs btn-danger fa fa-remove btn_DeclineStockMerge' data-toggle='tooltip'  title='Decline Stock Merge' onclick=\"DeclineStockMerge('" + dt.Rows[i]["StockMergeNo"] + "')\"></a> </center>";

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
    public static string ApproveStockMerge(string stockmergeno)
    {
        StockMergeBll y = new StockMergeBll();
        StockEntity x = new StockEntity();

        x.CreatedBy = Common.UserName;
        x.StockMergeNo = stockmergeno;
        x.ConnectionString = Common.ConnString;

        y.ApproveStockMerge(x);

        return "";
    }

    [WebMethod]
    public static string DeclineStockMerge(string stockmergeno)
    {
        StockMergeBll y = new StockMergeBll();
        StockEntity x = new StockEntity();

        x.CreatedBy = Common.UserName;
        x.StockMergeNo = stockmergeno;
        x.ConnectionString = Common.ConnString;

        y.DeclineStockMerge(x);

        return "";
    }

    #endregion Stock Merge

    #region DATA UPLOAD

    #region Property

    [WebMethod]
    public static string GetPropertyUploadApproval()
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<MiscEntity> JsonItemList = new List<MiscEntity>();
        try
        {
            DataTable dt = miscBll.GetPropertyUploadApproval(Common.ConnString, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MiscEntity x = new MiscEntity();
                    x.DataUploadFileNameProperty = dt.Rows[i]["FileName"].ToString();
                    x.UploadBy = dt.Rows[i]["TransactorName"].ToString();
                    x.UploadDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                    x.Action = " <center><button class='btn btn-xs  btn-primary fa fa-eye btn_Approve' type='button' data-toggle='tooltip'  title='View Upload Property Details' onclick=\"viewPropertyUploadDetails('" + Regex.Replace(dt.Rows[i]["FileName"].ToString(), @"\r", "") + "')\"></button> </center>";

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
    public static string GetPropertyUploadApprovalDetails(string batchname)
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = miscBll.GetPropertyUploadApprovalDetails(Common.ConnString, batchname);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    //x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.Description = dt.Rows[i]["LongDescription"].ToString();

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
    public static string ApprovePropertyUpload(string batchname)
    {
        try
        {
            MiscBll mb = new MiscBll();
            return mb.ApprovePropertyUpload(batchname, Common.UserName, Common.ConnString).ToString();
        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }
    [WebMethod]
    public static string DeclinePropertyUpload(string batchname)
    {
        try
        {
            MiscBll mb = new MiscBll();
            return mb.DeclinePropertyUpload(batchname, Common.UserName, Common.ConnString).ToString();
        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    protected void ApprovePU_Click(object sender, EventArgs e)
    {
        string batchname = txtBatchName.Value;
        MiscBll mb = new MiscBll();

        mb.ApprovePropertyUpload(batchname, Common.UserName, Common.ConnString);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:ApprovePropertyUpload();", true);
    }

    protected void DeclinePU_Click(object sender, EventArgs e)
    {
        string batchname = txtBatchName.Value;
        MiscBll mb = new MiscBll();
        try
        {


            mb.DeclinePropertyUpload(batchname, Common.UserName, Common.ConnString);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:DeclinePropertyUpload();", true);
            //Response.Write("<script>alert('Successfully Declined the Property Upload Transaction!');</script>");
        }
        catch (SqlException sqlex)
        {
            throw;
        }
        catch (Exception ex)
        { throw; }
    }

    #endregion Property

    #region Stock

    [WebMethod]
    public static string GetStockUploadApproval()
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<MiscEntity> JsonItemList = new List<MiscEntity>();
        try
        {
            DataTable dt = miscBll.GetStockUploadApproval(Common.ConnString, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MiscEntity x = new MiscEntity();
                    x.DataUploadFileNameStock = dt.Rows[i]["FileName"].ToString();
                    x.UploadBy = dt.Rows[i]["TransactorName"].ToString();
                    x.UploadDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                    x.Action = " <center><button class='btn btn-xs  btn-primary fa fa-eye btn_Approve' type='button' data-toggle='tooltip'  title='View Upload Property Details' onclick=\"viewStockUploadDetails('" + Regex.Replace(dt.Rows[i]["FileName"].ToString(), @"\r", "") + "')\"></button> </center>";

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
    public static string GetStockUploadApprovalDetails(string batchname)
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = miscBll.GetStockUploadApprovalDetails(Common.ConnString, batchname);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
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
    public static string ApproveStockUpload(string batchname)
    {
        try
        {
            MiscBll mb = new MiscBll();
            return mb.ApproveStockUpload(batchname, Common.UserName, Common.ConnString).ToString();
        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }
    [WebMethod]
    public static string DeclineStockUpload(string batchname)
    {
        try
        {
            MiscBll mb = new MiscBll();
            return mb.DeclineStockUpload(batchname, Common.UserName, Common.ConnString).ToString();
        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    #endregion Stock

    #region User
    [WebMethod]
    public static string GetUserUploadApproval()
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<MiscEntity> JsonItemList = new List<MiscEntity>();
        try
        {
            DataTable dt = miscBll.GetUserUploadApproval(Common.ConnString, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MiscEntity x = new MiscEntity();
                    x.DataUploadFileNameUser = dt.Rows[i]["FileName"].ToString();
                    x.UploadBy = dt.Rows[i]["TransactorName"].ToString();
                    x.UploadDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                    x.Action = " <center><button class='btn btn-xs  btn-primary fa fa-eye btn_Approve' type='button' data-toggle='tooltip'  title='View Upload Property Details' onclick=\"viewUserUploadDetails('" + Regex.Replace(dt.Rows[i]["FileName"].ToString(), @"\r", "") + "')\"></button> </center>";

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
    public static string GetUserUploadApprovalDetails(string batchname)
    {
        MiscBll miscBll = new MiscBll();
        string html = string.Empty;
        List<UserEntity> JsonItemList = new List<UserEntity>();
        try
        {
            DataTable dt = miscBll.GetUserUploadApprovalDetails(Common.ConnString, batchname);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserEntity x = new UserEntity();
                    x.FirstName = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                    x.EmailAddress = dt.Rows[i]["EmailAddress"].ToString();
                    x.UserName = dt.Rows[i]["UserName"].ToString();
                    x.EmployeeNo = dt.Rows[i]["EmployeeNo"].ToString();

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
    public static int ApproveUserUpload(string batchname)
    {
        MiscBll mb = new MiscBll();
        return mb.ApproveUserUpload(batchname, Common.UserName, Common.ConnString, Convert.ToInt32(Common.nooftsuc), Convert.ToInt32(Common.noofminda));

    }
    [WebMethod]
    public static string DeclineUserUpload(string batchname)
    {
        try
        {
            MiscBll mb = new MiscBll();
            return mb.DeclineUserUpload(batchname, Common.UserName, Common.ConnString).ToString();
        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }



    protected void DeclineUU_Click(object sender, EventArgs e)
    {
        string batchname = txtUserBatchName.Value;
        MiscBll mb = new MiscBll();
        try
        {

            mb.DeclineUserUpload(batchname, Common.UserName, Common.ConnString);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:DeclineUserUpload();", true);
        }
        catch (SqlException sqlex)
        {
            throw;
        }
        catch (Exception ex)
        { throw; }
    }
    #endregion

    #endregion DATA UPLOAD

    #region Stock Issuance
    [WebMethod]
    public static string StockIssuancePendingApproval()
    {
        StockIssuanceBll y = new StockIssuanceBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetPendingApprovalStockIssuance(Common.UserName, Common.Administrative, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.IssuanceNo = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.TransactionDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = " <center><button class='btn btn-xs btn-primary btn_View'  type='button' onclick=\"ViewStockIssuanceDetails('" + dt.Rows[i]["IssuanceNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["TransactorEmail"] + "')\"><span class='fa fa-eye'></span></center>";
                    //+ " <button class='btn btn-xs btn-danger btn_Cancel' type='button' onclick=\"CancelStockIssuance('" + dt.Rows[i]["IssuanceNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
                    //    " <a href='' class='btn btn-xs btn-info btn_attach'><span class='fa fa-upload'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string StockDetailsPendingApproval(string stockissuanceno)
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
    public static string ApproveStockIssuance(string issuanceno)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockIssuanceBll y = new StockIssuanceBll();

            x.IssuanceNo = issuanceno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            return y.ApproveStockIssuance(x).ToString();

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    //[WebMethod]
    //public static string DeclineStockIssuance(string issuanceno)
    //{
    //    try
    //    {
    //        StockEntity x = new StockEntity();
    //        StockIssuanceBll y = new StockIssuanceBll();

    //        x.IssuanceNo = issuanceno;
    //        x.CreatedBy = Common.UserName;
    //        x.ConnectionString = Common.ConnString;

    //        return y.DeclineStockIssuance(x).ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        return "sorry";
    //    }
    //}
    [WebMethod]
    public static string DeclineStockIssuance(string issuanceno, string remarkS, string transactoremail, string name)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockIssuanceBll y = new StockIssuanceBll();

            x.IssuanceNo = issuanceno;
            x.Remarks = remarkS;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            y.DeclineStockIssuance(x).ToString();

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Stock Issuance - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + name + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Issuance No : " + issuanceno + "<br><br>";

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
                return "transactor has no email";
            }

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    #endregion

    #region Stock Request
    [WebMethod]
    public static string StockRequestPendingApproval()
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetPendingApprovalStockRequest(Common.UserName, Common.Administrative, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.RequestNo = dt.Rows[i]["RequestNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.TransactionDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = " <center><button class='btn btn-xs btn-primary btn_View'  type='button' onclick=\"ViewStockRequestDetails('" + dt.Rows[i]["RequestNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["EmployeeEmail"] + "','" + dt.Rows[i]["EmployeeName"] + "')\"><span class='fa fa-eye'></span></button></center> ";
                    //+ " <button class='btn btn-xs btn-danger btn_Cancel' type='button' onclick=\"CancelStockRequest('" + dt.Rows[i]["RequestNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
                    //    " <a href='' class='btn btn-xs btn-info btn_attach'><span class='fa fa-upload'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string StockRequestDetailsPendingApproval(string stockrequestno)
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, stockrequestno);
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
                    x.QuantityStringIssue = dt.Rows[i]["QuantityIssued"].ToString();

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
    public static string ApproveStockRequest(string requestno, string transactoremail, string name)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockRequestBll y = new StockRequestBll();

            x.RequestNo = requestno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            y.ApproveStockRequest(x).ToString();
            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Stock Request - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + name + ",<br><br><br>";
                Body += "Your Request for PROPERTY REQUEST has been APPROVED.<br><br>"
                    + "Request No : " + requestno + "<br><br>";

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
                return "transactor has no email";
            }

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    [WebMethod]
    public static string DeclineStockRequest(string requestno, string remarkS, string transactoremail, string name)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockRequestBll y = new StockRequestBll();

            x.RequestNo = requestno;
            x.Remarks = remarkS;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            y.DeclineStockRequest(x).ToString();

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Stock Request - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + name + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Request No : " + requestno + "<br><br>";

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
                return "transactor has no email";
            }

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }
    #endregion

    #region Stock Disposal
    [WebMethod]
    public static string GetSDPendingApproval()
    {
        StockDisposalBll y = new StockDisposalBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetSDPendingApproval(Common.UserName, Common.Administrative, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.Approver = dt.Rows[i]["ApproverName"].ToString();
                    x.ModeOfDisposal = dt.Rows[i]["ModeOfDisposal"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    //x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");
                    x.Status = dt.Rows[i]["Status"].ToString();

                    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Stocks' onclick=\"ViewStockDisposalDetails('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["TransactorEmail"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //" <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string GetSDLPendingUpload(string disposalno)
    {
        StockDisposalBll y = new StockDisposalBll();
        StockEntity se = new StockEntity();

        se.DisposalNumber = disposalno;
        se.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.SDLPendingUpload(se);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
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
    public static string ApproveStockDisposal1(string disposalno)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockDisposalBll y = new StockDisposalBll();

            x.DisposalNumber = disposalno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            return y.ApproveStockDisposal(x).ToString();

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    [WebMethod]
    public static string DeclineStockDisposal(string disposalno, string remarkS, string transactoremail, string name)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockDisposalBll y = new StockDisposalBll();

            x.DisposalNumber = disposalno;
            x.Remarks = remarkS;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            y.DeclineStockDisposal(x).ToString();

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Stock Disposal - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + name + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Disposal No : " + disposalno + "<br><br>";

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
                return "transactor has no email";
            }

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }


    #endregion

    #region Property Retagging

    [WebMethod]
    public static string GetPropertyRetaggingHeader()
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetPropertyRetaggingHeader(Common.ConnString, Common.Administrative, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.TransactionNumber = dt.Rows[i]["TransactionNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();

                    x.Action = " <center><button class='btn btn-xs  btn-primary fa fa-eye btn_View' type='button' data-toggle='tooltip'  title='View Details' onclick=\"viewRetaggingDetails('" + dt.Rows[i]["TransactionNumber"] + "','" + dt.Rows[i]["TransactorEmail"] + "','" + dt.Rows[i]["TransactorName"] + "')\"></button></center>";

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
    public static string PropertyRetaggingApprovalDetails(string TransactionNumber)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetPropertyRetaggingApprovalDetails(Common.ConnString, TransactionNumber);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.TransactionNumber = dt.Rows[i]["TransactionNumber"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Status = dt.Rows[i]["PrintStatus"].ToString();
                    //   x.Action = " <center><button class='btn btn-xs  btn-primary fa fa-eye btn_View' type='button' data-toggle='tooltip'  title='View Details' onclick=\"viewRetaggingDetails('" + dt.Rows[i]["TransactionNumber"] + "')\"></button></center>";

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
    public static string ApprovePropertyRetagging(string transactionNumber, string transactorname, string transactoremail)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyBll propertyBll = new PropertyBll();

            x.TransactionNumber = transactionNumber;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.ApprovePropertyRetagging(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Retagging)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "Your Request for PROPERTY RETAGGING has been APPROVED.<br><br>"
                    + "Transaction No : " + transactionNumber + "<br>";

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


    [WebMethod]
    public static string DeclinePropertyRetagging(string transactionNumber, string transactorname, string transactoremail, string remarkS)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyBll propertyBll = new PropertyBll();

            x.TransactionNumber = transactionNumber;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            propertyBll.DeclinePropertyRetagging(x);

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;


            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Retagging)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + transactorname + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Transaction No : " + transactionNumber + "<br><br>";

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

    [WebMethod]
    public static string checkRetaggingApproval()
    {
        return Common.RetaggingApproval;
    }

    #endregion Property Retagging

    #region Stock Allocation

    [WebMethod]
    public static string GetSAPendingApproval()
    {
        SupplyAllocationBll y = new SupplyAllocationBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetSAPendingApproval(Common.UserName, Common.Administrative, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.StockAllocationNo = dt.Rows[i]["StockAllocationNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.FromSiteStr = dt.Rows[i]["FromSiteName"].ToString();
                    x.ToSiteStr = dt.Rows[i]["ToSiteName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    //x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    //x.DateOfTransaction = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                    x.Action = "<center><a class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Stocks' onclick=\"ViewStockAllocationDetails('" + dt.Rows[i]["StockAllocationNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["TransactorEmail"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    //" <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string GetSALPendingUpload(string disposalno)
    {
        SupplyAllocationBll y = new SupplyAllocationBll();
        StockEntity se = new StockEntity();

        se.DisposalNumber = disposalno;
        se.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.SALPendingUpload(se);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.StockAllocationNo = dt.Rows[i]["StockAllocationNo"].ToString();
                    x.Remarks = dt.Rows[i]["Description"].ToString();
                    x.FromSiteStr = dt.Rows[i]["FromSiteName"].ToString();
                    x.ToSiteStr = dt.Rows[i]["ToSiteName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
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
    public static string ApproveStockAllocation2(string disposalno)
    {
        try
        {
            StockEntity x = new StockEntity();
            SupplyAllocationBll y = new SupplyAllocationBll();

            x.DisposalNumber = disposalno;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            return y.ApproveStockAllocation(x).ToString();

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }


    [WebMethod]
    public static string DeclineStockAllocation(string disposalno, string remarkS, string transactoremail, string name)
    {
        try
        {
            StockEntity x = new StockEntity();
            SupplyAllocationBll y = new SupplyAllocationBll();

            x.DisposalNumber = disposalno;
            x.Remarks = remarkS;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            y.DeclineStockAllocation(x).ToString();

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;
            if (transactoremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(transactoremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Stock Disposal - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + name + ",<br><br><br>";
                Body += "I am declining this request with reason, " + remarkS + ".<br><br>"
                    + "Disposal No : " + disposalno + "<br><br>";

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
                return "transactor has no email";
            }

        }
        catch (Exception ex)
        {
            return "sorry";
        }
    }

    #endregion Stock Allocation
}