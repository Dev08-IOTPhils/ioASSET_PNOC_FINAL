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

public partial class Operation_Module_PropertyDisposal : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        GetApprover();
        ModeOfDisposal();
        GetItem();

    }

    #region Metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/PropertyDisposal.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    public void GetItem()
    {
        ddlItem.Items.Clear();
        ddlItem.Items.Add("--- Select Property Type Cost ---");
        ddlItem.Items.Add("ICS LOW - " + " Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and below.");
        ddlItem.Items.Add("ICS HIGH - " + " Between above Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
        ddlItem.Items.Add("PAR - " + " Above Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
    }

    [WebMethod]
    public static string IfNeedsApproval()
    {
        return Common.DisposalApproval;
    }

    private void ModeOfDisposal()
    {
        ddlModeOfDisposal.Items.Clear();
        ddlModeOfDisposal.Items.Insert(0, "--- Select Mode of Disposal ---");
        ddlModeOfDisposal.Items.Insert(1, "Condemnation/Destruction of Properties");
        ddlModeOfDisposal.Items.Insert(2, "Transfer of Properties to Another Government Agency");
        ddlModeOfDisposal.Items.Insert(3, "Barter");
        ddlModeOfDisposal.Items.Insert(4, "Donation");
        ddlModeOfDisposal.Items.Insert(5, "Return to the Supplier/Vendor");
        ddlModeOfDisposal.Items.Insert(6, "Sale to Government Officials/Employees of the Agency");
        ddlModeOfDisposal.Items.Insert(7, "Public Auction");
        ddlModeOfDisposal.Items.Insert(8, "Negotiated Sale");
        ddlModeOfDisposal.Items.Insert(9, "Sale of Junk or Scrap and/or Recycling");
        ddlModeOfDisposal.Items.Insert(10, "Direct Negotiation");
        ddlModeOfDisposal.Items.Insert(11, "Lost");
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
    public static List<ListItem> GetInspector(int userid)
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Common.SiteID);
        userEntity.ApproverID = userid;
        userEntity.UserID = Convert.ToInt32(Common.UserID);
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetInspector(userEntity);
    }

    [WebMethod]
    public static List<ListItem> GetCertifier(int userid)
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Common.SiteID);
        userEntity.ApproverID = userid;
        userEntity.UserID = Convert.ToInt32(Common.UserID);
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetCertifier(userEntity);
    }

    #endregion Metadata

    #region Datatables

    [WebMethod]
    public static string GetList(string search, int custodian, int ExpenseType)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailablePropertyForDisposal(Common.ConnString, search, Convert.ToInt32(Common.SiteID), custodian, ExpenseType);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.pmlid = Convert.ToInt32(dt.Rows[i]["RecID"]);
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success btn_Add' pmlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Disposal List'><span class='fa fa-plus'></span></a> </center>";
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
    public static string GetPropertyDisposalList(int custodian, string ExpenseType)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.CustodianID = custodian;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.TransactionType = ExpenseType;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyDisposalList(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string condition = dt.Rows[i]["Condition"].ToString();
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    switch (condition)
                    {
                        case "No Longer Needed":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")'><option value='Select Condition'>Select Condition</option><option value='No Longer Needed' selected>No Longer Needed</option><option value='Excess'>Excess</option><option value='Obsolete'>Obsolete</option><option value='Defective Mechanism'>Defective Mechanism</option><option value='Defective / Unrepairable'>Defective / Unrepairable</option></select>";
                            break;

                        case "Excess":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")'><option value='Select Condition'>Select Condition</option><option value='No Longer Needed'>No Longer Needed</option><option value='Excess' selected>Excess</option><option value='Obsolete'>Obsolete</option><option value='Defective Mechanism'>Defective Mechanism</option><option value='Defective / Unrepairable'>Defective / Unrepairable</option></select>";
                            break;

                        case "Obsolete":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")'><option value='Select Condition'>Select Condition</option><option value='No Longer Needed'>No Longer Needed</option><option value='Excess'>Excess</option><option value='Obsolete' selected>Obsolete</option><option value='Defective Mechanism'>Defective Mechanism</option><option value='Defective / Unrepairable'>Defective / Unrepairable</option></select>";
                            break;

                        case "Defective Mechanism":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")'><option value='Select Condition'>Select Condition</option><option value='No Longer Needed'>No Longer Needed</option><option value='Excess'>Excess</option><option value='Obsolete'>Obsolete</option><option value='Defective Mechanism' selected>Defective Mechanism</option><option value='Defective / Unrepairable'>Defective / Unrepairable</option></select>";
                            break;

                        case "Defective / Unrepairable ":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")'><option value='Select Condition'>Select Condition</option><option value='No Longer Needed'>No Longer Needed</option><option value='Excess'>Excess</option><option value='Obsolete'>Obsolete</option><option value='Defective Mechanism'>Defective Mechanism</option><option value='Defective / Unrepairable' selected>Defective / Unrepairable</option></select>";
                            break;

                        case "":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecordID"].ToString() + "," + i + ")'><option value='Select Condition' selected>Select Condition</option><option value='No Longer Needed'>No Longer Needed</option><option value='Excess'>Excess</option><option value='Obsolete'>Obsolete</option><option value='Defective Mechanism'>Defective Mechanism</option><option value='Defective / Unrepairable'>Defective / Unrepairable</option></select>";
                            break;
                    }
                    if (DeleteAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' pmlid='" + dt.Rows[i]["PMLID"].ToString() + "' data-toggle='tooltip'  title='Remove To Disposal List'><span class='fa fa-close'></span></a> </center>";
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
    public static string GetPendingUploadList(string search, string datefrom, string dateto)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.DisposalPendingUpload(search, Common.ConnString, Convert.ToInt32(Common.SiteID), Common.CustodianID, Common.Administrative, datefrom, dateto);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    PropertyEntity x = new PropertyEntity();
                    x.PropertyDisposalID = dt.Rows[i]["PropertyDisposalID"].ToString();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.Paricsword = dt.Rows[i]["PARICSword"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.ModeOfDisposal = dt.Rows[i]["ModeOfDisposal"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetails('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" 
                            + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsCertified"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["Status"].ToString() 
                            + "','" + dt.Rows[i]["PARICS"].ToString() + "')\"><span class='fa fa-eye'></span></a>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
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
    public static string GetPLPendingUploadList(string disposalno)
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
                    x.Condition = "<select><option>End of Life</option><option>Transfer</option><option>Destruction</option><option>Donation</option></select>";

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
    public static string GetPLPendingUploadListRequest(string disposalno)
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
                    x.Condition = "<select><option>End of Life</option><option>Transfer</option><option>Destruction</option><option>Donation</option></select>";

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
    #endregion Datatables

    #region Methods

    [WebMethod]
    public static string changeCondition(int recid, string condition)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = recid;
        x.Condition = condition;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.changeCondition(x).ToString();
    }

    [WebMethod]
    public static string IfDisposalNeedsApproval()
    {
        return Common.DisposalApproval;
    }

    [WebMethod]
    public static string PropertyTransferToDisposal(int pmlid, int custodian, int ExpenseType)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = pmlid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.ExpenseType = ExpenseType;

        return y.PropertyTransferToDisposal(x).ToString();
    }

    [WebMethod]
    public static string PropertyRemoveToDisposal(int pmlid)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.pmlid = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.PropertyRemoveToDisposal(x).ToString();
    }

    [WebMethod]
    public static string PropertyRemoveAllToDisposal(int custodian, string ExpenseType)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.CreatedBy = Common.UserName;
        x.CustodianID = custodian;
        x.ConnectionString = Common.ConnString;
        x.TransactionType = ExpenseType;

        return y.PropertyRemoveAllToDisposal(x).ToString();
    }

    [WebMethod]
    public static string ConfirmPropertyDisposal(int approverid, int inspectorid, int certifierid, string modeofdisposal, string remarks, int custodianid, int ExpenseType)
    {
        try
        {
            PropertyDisposalBll y = new PropertyDisposalBll();
            PropertyEntity x = new PropertyEntity();

            x.ModeOfDisposal = modeofdisposal;
            x.Remarks = remarks;
            x.ApproverID = approverid;
            x.InspectorID = inspectorid;
            x.CertifierID = certifierid;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;
            x.CustodianID = custodianid;
            x.ExpenseType = ExpenseType;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string disposalno = y.ConfirmPropertyDisposal(x);

            x.DisposalNumber = disposalno;

            DataTable dt = y.PDLPendingUpload(x);

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string disposaldate = disposaldetails.Split('|')[9];
            string transactoremail = disposaldetails.Split('|')[10];
            string approveremail = disposaldetails.Split('|')[11];
            string inspectoremail = disposaldetails.Split('|')[12];
            string certifieremail = disposaldetails.Split('|')[13];

            if (approveremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(approveremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Disposal - Approval)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
                Body += "A PROPERTY DISPOSAL transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
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
    public static string CancelPropertyDisposal(string disposalno, string remarks)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.DisposalNumber = disposalno;
        x.CreatedBy = Common.UserName;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;

        y.CancelPropertyDisposal(x);

        return "";
    }

    [WebMethod]
    public static string CancelPropertyDisposal1(string disposalno)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.DisposalNumber = disposalno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.CancelPropertyDisposal1(x);

        return "";
    }
    [WebMethod]
    public static string PropertyGetCount(int custodian, int ExpenseType)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity x = new PropertyEntity();

        x.CreatedBy = Common.UserName;
        x.CustodianID = custodian;
        x.ConnectionString = Common.ConnString;
        x.ExpenseType = ExpenseType;

        return y.PropertyGetCount(x);
    }

    [WebMethod]
    public static string GenerateDisposalReport(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUpload(x);

        return GenerateDisposalForm(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateDisposalForm(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 325px; }"
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
                          + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
           + "<tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>DISPOSAL FORM<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + disposalno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'></td><td style='font-size:12;'></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + disposaldate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br</td><td style='font-size:12;'><br><u></u></td></tr>"
                + "<tr><td style='font-size:12;'>Remarks/Purpose:</td><td style='font-size:12;' colspan='3'><u>" + remarks + "</u></td></tr></table>";

            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'><b><u>TERMS AND CONDITIONS:</u></b>";
            html += "<table style='width:100%;'><tr><td><p align='justify' style='font-size:9.5px; font-family: Arial;'>In trust for and as property of <b> " + CompanyName + ",</b> the above described equipment having been lend and entrusted to me for trials and with the"
                + " obligation on my part to return the same complete in good order and condition before the agreed date. I further agree to the said property in my residence or place of business at the address indicated herein above and not to remove the same from said premises without previous knowledge and consent by <b>" + CompanyName + "</b>"
                + " I agree to use the Company Property provided to me solely for the intended uses: official business use and/or work related purposes. I am responsible for the care and"
                + " maintenance of the equipment. In case that the equipment is, in any case, damaged, lost, stolen, while outside the office premises, I shall be responsible for its cost of replacement or repair and other incidental charges it may occur."
                + " In case the equipment gets broken due to wear and tear, I shall notify the Company Custodian immediately and have it inspected by the Company's Technician as soon as possible. All civic actions on this contract shall be instituted in the court"//s of San Juan, Metro Manila"
                + " <p align='justify' style='font-size:9.5px; font-family: Arial;'>I agree to maintain the property in good condition and to return it when I cease working for the company, or earlier on request.";
            html += " By accepting this Agreement, I acknowledge the receipt in working order and good condition of the company-owned property listed above and I conform to the terms and condition stated.</p></td></tr></table>";
            //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:25%'>Transacted by</td><td  style='width:25%'>Approved by</td><td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
            html += "<tr rowspan='2'><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
            html += "<tr><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Model</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>PROPERTY #</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }


    #region Disposal Request


    [WebMethod]
    public static string GenerateDisposalReportIIRUPRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetailsRequest(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];
        string approverpos = disposaldetails.Split('|')[14];

        DataTable dt = y.PDLPendingUploadRequest(x);

        return GenerateDisposalFormIIRUPRequest(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian, approverpos);
    }

    public static string GenerateDisposalFormIIRUPRequest(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian, string approverpos)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 200px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

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
            + "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
            + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE PROPERTY<b></td</tr>"

                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string donation = "";

                    string auction = "";
                    string transfer = "";
                    string destruction = "";
                    string others = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            destruction = "1";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            destruction = "0";
                            auction = "";
                            transfer = "1";
                            donation = "";
                            others = "";
                            break;

                        case "Barter":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Barter)";
                            break;

                        case "Donation":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Donation)";
                            break;

                        case "Return to the Supplier/Vendor":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Return to the Supplier/Vendor)";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale to Government Officials/Employees of the Agency)";
                            break;

                        case "Public Auction":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Public Auction)";
                            break;

                        case "Negotiated Sale":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Negotiated Sale)";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale of Junk or Scrap and/or Recycling)";
                            break;

                        case "Direct Negotiation":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Direct Negotiation)";
                            break;

                        case "Lost":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Lost)";
                            break;

                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + transfer + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + destruction + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + others + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportIIRSEPRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetailsRequest(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];
        string approverpos = disposaldetails.Split('|')[14];

        DataTable dt = y.PDLPendingUploadRequest(x);

        return GenerateDisposalFormIIRUSPRequest(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian, approverpos);
    }

    public static string GenerateDisposalFormIIRUSPRequest(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian, string approverpos)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 200px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

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
            + "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE SEMI-EXPENDABLE PROPERTY<b></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            //html += "<tr><td style='width:60%'><br/><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</br></br></br></br>&emsp;&emsp;Requested by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:<br/><br/>&emsp;&emsp;&emsp;<u>" + transactorname + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of <br/>&emsp;&emsp;&emsp;&emsp;&emsp; Accountable Officer) &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Authorized Officer) <br/><br/>&emsp;&emsp;&emsp;<u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;___________________________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Accountable Official)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Authorized Official)<br/><br/></td>"
            //    + "<td align='left';valign='top' style='width:40%'><br/> I CERTIFY that I have inspected each &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;I CERTIFY that I have <br/> and every article enumerated in this report,&emsp;&emsp;&emsp;&emsp;witnessed the disposition of the<br/>and that the disposition made therof was, &emsp;&emsp;&emsp;&emsp; articles enumerated on this <br/> in my judgement, the best for the public &emsp;&emsp;&emsp;&emsp;&emsp; report this __day of <br/>interest. &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_____________,______<br/><br/><br/>________________________________&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;________________________<br/>&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of<br>&emsp;&emsp;&emsp;&emsp;&emsp;Inspection Officer)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Witness)</td></tr></table>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dothis
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string donation = "";

                    string auction = "";
                    string transfer = "";
                    string destruction = "";
                    string others = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            destruction = "1";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            destruction = "0";
                            auction = "";
                            transfer = "1";
                            donation = "";
                            others = "";
                            break;

                        case "Barter":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Barter)";
                            break;

                        case "Donation":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Donation)";
                            break;

                        case "Return to the Supplier/Vendor":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Return to the Supplier/Vendor)";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale to Government Officials/Employees of the Agency)";
                            break;

                        case "Public Auction":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Public Auction)";
                            break;

                        case "Negotiated Sale":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Negotiated Sale)";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale of Junk or Scrap and/or Recycling)";
                            break;

                        case "Direct Negotiation":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Direct Negotiation)";
                            break;

                        case "Lost":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Lost)";
                            break;

                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + transfer + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + destruction + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + others + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }


    [WebMethod]
    public static string GenerateDisposalReportROSEPRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetailsRequest(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUploadRequest(x);

        return GenerateFormROSEPRequest(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateFormROSEPRequest(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 390px;}"
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
                  + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"

            + "<tr><td align='center' style='font-size:15;'><br><h4>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED SEMI-EXPENDABLE PROPERTY</h4></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><u>" + transactorname + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + Convert.ToDateTime(disposaldate).ToString() + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><u>" + approvername + "</u><br/>Signature over Printed Name of the Immediate Supervisor<br/><u>" + Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy") + "</u><br/>Date</td></tr>";

            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordepartment + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorposition + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';>"
                   + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>ICS No. : <u>" + "" + "</u></b><br/>"
                   + "<b>ICS Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"

                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Semi-expendable Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportROPRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetailsRequest(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUploadRequest(x);

        return GenerateFormROPRequest(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateFormROPRequest(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 390px;}"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED PROPERTY<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><u>" + "" + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + "" + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><u>" + "" + "</u><br/>Signature over Printed Name of the Immediate Supervisor<br/><u>" + "" + "</u><br/>Date</td></tr>";

            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordepartment + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorposition + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><b>RLSDDSP No. : <u>" + "" + "</u></b><br/>"
                   + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>PAR No. : <u>" + "" + "</u></b><br/>"
                   + "<b>PAR Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"

                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                         + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetailsRequest(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUploadRequest(x);

        return GenerateDisposalFormRequest(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateDisposalFormRequest(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 325px; }"
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
                          + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png'; width='50%'; style='width: 150px; height: 150px' /></td</tr>"
           + "<tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>DISPOSAL FORM<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + disposalno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'></td><td style='font-size:12;'></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + disposaldate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br</td><td style='font-size:12;'><br><u></u></td></tr>"
                + "<tr><td style='font-size:12;'>Remarks/Purpose:</td><td style='font-size:12;' colspan='3'><u>" + remarks + "</u></td></tr></table>";

            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'><b><u>TERMS AND CONDITIONS:</u></b>";
            html += "<table style='width:100%;'><tr><td><p align='justify' style='font-size:9.5px; font-family: Arial;'>In trust for and as property of <b> " + CompanyName + ",</b> the above described equipment having been lend and entrusted to me for trials and with the"
                + " obligation on my part to return the same complete in good order and condition before the agreed date. I further agree to the said property in my residence or place of business at the address indicated herein above and not to remove the same from said premises without previous knowledge and consent by <b>" + CompanyName + "</b>"
                + " I agree to use the Company Property provided to me solely for the intended uses: official business use and/or work related purposes. I am responsible for the care and"
                + " maintenance of the equipment. In case that the equipment is, in any case, damaged, lost, stolen, while outside the office premises, I shall be responsible for its cost of replacement or repair and other incidental charges it may occur."
                + " In case the equipment gets broken due to wear and tear, I shall notify the Company Custodian immediately and have it inspected by the Company's Technician as soon as possible. All civic actions on this contract shall be instituted in the court"//s of San Juan, Metro Manila"
                + " <p align='justify' style='font-size:9.5px; font-family: Arial;'>I agree to maintain the property in good condition and to return it when I cease working for the company, or earlier on request.";
            html += " By accepting this Agreement, I acknowledge the receipt in working order and good condition of the company-owned property listed above and I conform to the terms and condition stated.</p></td></tr></table>";
            //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:25%'>Transacted by</td><td  style='width:25%'>Approved by</td><td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
            html += "<tr rowspan='2'><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
            html += "<tr><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Model</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>PROPERTY #</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportWMRRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetailsRequest(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUploadRequest(x);

        return GenerateFormWMRRequest(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateFormWMRRequest(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 255px; }"
            + ".page-footer, .page-footer-space {height: 280px;}"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
                 + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>WASTE MATERIALS REPORT<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><u>" + transactorname + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><center><u>" + inspectorname + "</u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><center><u>" + certifiername + "</u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='7';><b>ITEMS FOR DISPOSAL</b></td></tr>"
                      + "<tr>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td colspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Record of Sales</b></td>"
                      + "</tr><tr>"
                      + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Official Receipt</b></td>"
                      + "</tr><tr>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Date</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Amount</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + i + 1 + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    #endregion

    [WebMethod]
    public static string GenerateDisposalReportIIRUP(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];
        string approverpos = disposaldetails.Split('|')[14];

        DataTable dt = y.PDLPendingUpload(x);

        return GenerateDisposalFormIIRUP(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian, approverpos);
    }

    public static string GenerateDisposalFormIIRUP(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian, string approverpos)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 200px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

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
            + "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
            + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE PROPERTY<b></td</tr>"

                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As of ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + "" + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dothis
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string donation = "";

                    string auction = "";
                    string transfer = "";
                    string destruction = "";
                    string others = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            destruction = "1";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            destruction = "0";
                            auction = "";
                            transfer = "1";
                            donation = "";
                            others = "";
                            break;

                        case "Barter":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Barter)";
                            break;

                        case "Donation":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Donation)";
                            break;

                        case "Return to the Supplier/Vendor":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Return to the Supplier/Vendor)";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale to Government Officials/Employees of the Agency)";
                            break;

                        case "Public Auction":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Public Auction)";
                            break;

                        case "Negotiated Sale":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Negotiated Sale)";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale of Junk or Scrap and/or Recycling)";
                            break;

                        case "Direct Negotiation":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Direct Negotiation)";
                            break;

                        case "Lost":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Lost)";
                            break;

                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + transfer + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + destruction + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + others + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportIIRSEP(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];
        string approverpos = disposaldetails.Split('|')[14];

        DataTable dt = y.PDLPendingUpload(x);

        return GenerateDisposalFormIIRUSP(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian, approverpos);
    }

    public static string GenerateDisposalFormIIRUSP(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian, string approverpos)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 200px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

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
            + "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE SEMI-EXPENDABLE PROPERTY<b></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dothis
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string donation = "";

                    string auction = "";
                    string transfer = "";
                    string destruction = "";
                    string others = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            destruction = "1";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            destruction = "0";
                            auction = "";
                            transfer = "1";
                            donation = "";
                            others = "";
                            break;

                        case "Barter":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Barter)";
                            break;

                        case "Donation":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Donation)";
                            break;

                        case "Return to the Supplier/Vendor":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Return to the Supplier/Vendor)";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale to Government Officials/Employees of the Agency)";
                            break;

                        case "Public Auction":
                            destruction = "0";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Public Auction)";
                            break;

                        case "Negotiated Sale":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Negotiated Sale)";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Sale of Junk or Scrap and/or Recycling)";
                            break;

                        case "Direct Negotiation":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Direct Negotiation)";
                            break;

                        case "Lost":
                            destruction = "";
                            auction = "";
                            transfer = "";
                            donation = "";
                            others = "1(Lost)";
                            break;

                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + transfer + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + destruction + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + others + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportWMR(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUpload(x);

        return GenerateFormWMR(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateFormWMR(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 255px; }"
            + ".page-footer, .page-footer-space {height: 280px;}"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
                 + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>WASTE MATERIALS REPORT<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Office/Agency : <u>" + "" + "</u></td><td style='text-align: left;font-size:12;'>WMR : <u>" + "" + "</u> </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><u>" + transactorname + "</u><br>Signature over Printed Name of<br/> Administrative Officer and/or</br>Property Custodian<br/></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><u>" + approvername + "</u><br>Signature over Printed Name of<br/>Signature over Printed Name of Chief</br>Administrative Officer, PRM and/or Authorized</br>Representative</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + "" + "</u><br/><br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><center><u>" + inspectorname + "</u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><center><u>" + certifiername + "</u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='7';><b>ITEMS FOR DISPOSAL</b></td></tr>"
                      + "<tr>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Details</b></td>"
                      + "<td colspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Record of Sales</b></td>"
                      + "</tr><tr>"
                      //+ "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Official Receipt</b></td>"
                      + "</tr><tr>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No.</b></td>"
                      //+ "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Date</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Amount</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + i + 1 + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          //+ "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    private static string CompanyName = "";
    private static string CompanyContactNumber = "";
    private static string CompanyAddress = "";
    private static string CompanyLogo = "";

    [WebMethod]
    public static string GenerateDisposalReportROP(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUpload(x);

        return GenerateFormROP(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateFormROP(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 390px;}"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED PROPERTY<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><u>" + "" + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + "" + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><u>" + "" + "</u><br/>Signature over Printed Name of the Immediate Supervisor<br/><u>" + "" + "</u><br/>Date</td></tr>";

            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordepartment + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorposition + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';>"
                   + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>PAR No. : <u>" + "" + "</u></b><br/>"
                   + "<b>PAR Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"

                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                         + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateDisposalReportROSEP(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.PropertyDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string inspectorname = disposaldetails.Split('|')[3];
        string certifiername = disposaldetails.Split('|')[4];
        string transactordep = disposaldetails.Split('|')[5];
        string transactorpos = disposaldetails.Split('|')[6];
        string custodian = disposaldetails.Split('|')[7];
        string remarks = disposaldetails.Split('|')[8];
        string disposaldate = disposaldetails.Split('|')[9];

        DataTable dt = y.PDLPendingUpload(x);

        return GenerateFormROSEP(dt, disposalno, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateFormROSEP(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 390px;}"
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
                  + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"

            + "<tr><td align='center' style='font-size:15;'><br><h4>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED SEMI-EXPENDABLE PROPERTY</h4></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><u>" + transactorname + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + Convert.ToDateTime(disposaldate).ToString() + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><u>" + approvername + "</u><br/>Signature over Printed Name of the Immediate Supervisor<br/><u>" + Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy") + "</u><br/>Date</td></tr>";

            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordepartment + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorposition + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';>"
                   + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>ICS No. : <u>" + "" + "</u></b><br/>"
                   + "<b>ICS Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"

                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Semi-expendable Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static void UploadDocumentsDisposal(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.UploadDocumentsDisposal(x);
    }
    [WebMethod]
    public static void UploadDocumentsDisposalRequest(string disposalno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyDisposalBll y = new PropertyDisposalBll();

        x.DisposalNumber = disposalno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.UploadDocumentsDisposalRequest(x);
    }
    protected void btnCompleteDisposal(object sender, EventArgs e)
    {
        string disposalnumber = txtDisposalNumber.Text;
        UploadDocumentsDisposal(disposalnumber);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccessW();", true);
    }

    public bool success = false;

    protected void btnUploadDisposal(object sender, EventArgs e)
    {
        try
        {
            string disposalnumber = txtDisposalNumber.Text;
            if (AFupload.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/PropertyDisposal/" + disposalnumber + "/");
                foreach (HttpPostedFile file in AFupload.PostedFiles)
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
                    UploadDocumentsDisposal(disposalnumber);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccess();", true);
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

    [WebMethod]
    public static string CreatePDWasteMaterialHTML(string issuanceno,
string imagebase64,
string passphrase,
string certbase64,
bool datestamp,
string transtype,
string operationtype)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();
            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            DataTable dt = y.PDLPendingUpload(x);
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
            + ".page-footer, .page-footer-space {height: 280px;}"
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
            html += "<tr><td colspan='4' align='center' ><br><h3>WASTE MATERIALS REPORT<h3></td</tr>";
            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 230px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + transactorname + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><div style='width: 50%; height: 75px;' ></div><br /><center><u></u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><div style='width: 50%; height: 75px;' ></div><br /><center><u></u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='7';><b>ITEMS FOR DISPOSAL</b></td></tr>"
                      + "<tr>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td colspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Record of Sales</b></td>"
                      + "</tr><tr>"
                      + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Official Receipt</b></td>"
                      + "</tr><tr>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Date</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Amount</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + i + 1 + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string CreatePDDisposalFormHTML(string issuanceno,
string imagebase64,
string passphrase,
string certbase64,
bool datestamp,
string transtype,
string operationtype)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();
            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            DataTable dt = y.PDLPendingUpload(x);
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
            + ".page-header, .page-header-space {height: 325px; }"
            + ".page-footer, .page-footer-space {height: 310px;}"
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
                          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><br><b>DISPOSAL FORM<b></td</tr>"
                + "</table><br>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='width:70%;'></td><td style='font-size:14;width:30%;'>No. <u>" + issuanceno + "</u></td</tr>"
                + "<tr><td style='font-size:12;width:70%;'><td style='font-size:12;width:30%;'>Date:<u>" + disposaldate + "</u></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
               + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br</td><td style='font-size:12;'><br><u></u></td></tr>"
                + "<tr><td style='font-size:12;'>Remarks/Purpose:</td><td style='font-size:12;' colspan='3'><u>" + remarks + "</u></td></tr></table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions
            html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'><b><u>TERMS AND CONDITIONS:</u></b>";
            html += "<table style='width:100%;'><tr><td><p align='justify' style='font-size:9.5px; font-family: Arial;'>In trust for and as property of <b> " + CompanyName + ",</b> the above described equipment having been lend and entrusted to me for trials and with the"
                + " obligation on my part to return the same complete in good order and condition before the agreed date. I further agree to the said property in my residence or place of business at the address indicated herein above and not to remove the same from said premises without previous knowledge and consent by <b>" + CompanyName + "</b>"
                + " I agree to use the Company Property provided to me solely for the intended uses: official business use and/or work related purposes. I am responsible for the care and"
                + " maintenance of the equipment. In case that the equipment is, in any case, damaged, lost, stolen, while outside the office premises, I shall be responsible for its cost of replacement or repair and other incidental charges it may occur."
                + " In case the equipment gets broken due to wear and tear, I shall notify the Company Custodian immediately and have it inspected by the Company's Technician as soon as possible. All civic actions on this contract shall be instituted in the court"//s of San Juan, Metro Manila"
                + " <p align='justify' style='font-size:9.5px; font-family: Arial;'>I agree to maintain the property in good condition and to return it when I cease working for the company, or earlier on request.";
            html += " By accepting this Agreement, I acknowledge the receipt in working order and good condition of the company-owned property listed above and I conform to the terms and condition stated.</p></td></tr></table>";
            //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='font-size:10.5px; font-family: Arial; width:50%'>Transacted by</td><td  style='font-size:10.5px; font-family: Arial; width:50%'>Approved by</td></tr>";//<td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
            html += "<tr rowspan='2'><td style='text-align: center; font-size:9.5px; font-family: Arial; width:50%'><div style='width: 50%; height: 75px;' ></div><br />&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='text-align: center; font-size:9.5px; font-family: Arial; width:50%'><div style='width: 50%; height: 75px;' ></div><br />&emsp;&emsp;&emsp;<br><br>" + approvername + "</td></tr>";//<td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial; width:50%'>Date</td><td style='font-size:9.5px; font-family: Arial; width:50%'>Date</td></tr>";//<td  style='width:25%'>Date</td><td  style='width:25%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion footer
            #endregion div header and footer
            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion header placeholder
            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>ITEM CODE</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>" + "1" + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string CreatePDRSEPHTML(string issuanceno,
string imagebase64,
string passphrase,
string certbase64,
bool datestamp,
string transtype,
string operationtype)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();
            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            string approverpos = disposaldetails.Split('|')[14];
            DataTable dt = y.PDLPendingUpload(x);
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();
            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];
            string g = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = g.Split('|')[0];
            string CompanyContactNumber = g.Split('|')[3];
            string CompanyAddress = g.Split('|')[5];
            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 265px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"
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
            html += "<table style='width:100%;font-family: Arial;'>" +
                "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE SEMI-EXPENDABLE PROPERTY<b></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorpos + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><div style='width: 50%; height: 75px;' ></div><br />&emsp;&emsp;&emsp;<br><br><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorpos + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><div style='width: 50%; height: 75px;' ></div><br />&emsp;&emsp;&emsp;<u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string cdp = "";
                    string tpaga = "";
                    string bart = "";
                    string don = "";
                    string rpv = "";
                    string sgoea = "";
                    string pa = "";
                    string ns = "";
                    string sjsr = "";
                    string dn = "";
                    string lost = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            cdp = "1";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            tpaga = "1";
                            break;

                        case "Barter":
                            bart = "1";
                            break;

                        case "Donation":
                            don = "1";
                            break;

                        case "Return to the Supplier/Vendor":
                            rpv = "1";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            sgoea = "1";
                            break;

                        case "Public Auction":
                            pa = "1";
                            break;

                        case "Negotiated Sale":
                            ns = "1";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            sjsr = "1";
                            break;

                        case "Direct Negotiation":
                            dn = "1";
                            break;

                        case "Lost":
                            lost = "1";
                            break;
                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + cdp + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + tpaga + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + bart + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + don + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + rpv + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + sgoea + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + pa + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + ns + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + sjsr + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dn + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string CreatePDROSEPHTML(string issuanceno,
string imagebase64,
string passphrase,
string certbase64,
bool datestamp,
string transtype,
string operationtype)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();
            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            DataTable dt = y.PDLPendingUpload(x);
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();
            string g = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = g.Split('|')[0];
            CompanyContactNumber = g.Split('|')[3];
            CompanyAddress = g.Split('|')[5];
            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 430px;}"
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
                  + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td</tr>"
            + "<tr><td align='center' style='font-size:15;'><br><h4>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED SEMI-EXPENDABLE PROPERTY</h4></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer'  style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><div style='width: 50%; height: 75px;' ></div><br />&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + Convert.ToDateTime(disposaldate).ToString("MM/dd/yyyy") + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><div style='width: 50%; height: 75px;' ></div><br />&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/> Signature over Printed Name of the Immediate Supervisor<br/><u>" + Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy") + "</u><br/>Date</td></tr>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordep + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorpos + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';>"
                   + "<b>RLSDDSP No. : <u>" + issuanceno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>ICS No. : <u>" + "" + "</u></b><br/>"
                   + "<b>ICS Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Semi-expendable Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string CreatePDFPrintGridDataROSEP(string disposalno,
    string fromimagebase64,
    string byimagebase64,
    string frompassphrase,
    string bypassphrase,
    string fromcertbase64,
    string bycertbase64,
    bool fromdatestamp,
    bool bydatestamp)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];

            DataTable dt = y.PDLPendingUpload(x);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string g = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = g.Split('|')[0];
            CompanyContactNumber = g.Split('|')[3];
            CompanyAddress = g.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 430px;}"
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
                  + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td</tr>"

            + "<tr><td align='center' style='font-size:15;'><br><h4>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED SEMI-EXPENDABLE PROPERTY</h4></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'  style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + Convert.ToDateTime(disposaldate).ToString("MM/dd/yyyy") + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/> Signature over Printed Name of the Immediate Supervisor<br/><u>" + Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy") + "</u><br/>Date</td></tr>";

            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordep + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorpos + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';>"
                   + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>ICS No. : <u>" + "" + "</u></b><br/>"
                   + "<b>ICS Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"

                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Semi-expendable Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            pdf = PDFConverter.AddSignature(pdf, fromcertbase64, frompassphrase);
            pdf = PDFConverter.AddSignature(pdf, bycertbase64, bypassphrase);

            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string CreatePDFPrintGridDataROP(string disposalno,
  string fromimagebase64,
  string byimagebase64,
  string frompassphrase,
  string bypassphrase,
  string fromcertbase64,
  string bycertbase64,
    bool fromdatestamp,
    bool bydatestamp)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];

            DataTable dt = y.PDLPendingUpload(x);

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
            + ".page-header, .page-header-space {height: 235px; }"
            + ".page-footer, .page-footer-space {height: 430px;}"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
     + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td</tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED PROPERTY<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 90px;'>;";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
            html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
                    + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + Convert.ToDateTime(disposaldate).ToString("MM/dd/yyyy") + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
                + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/> Signature over Printed Name of the Immediate Supervisor<br/><u>" + Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy") + "</u><br/>Date</td></tr>";

            html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
                    + "<b>Department/Office : <u>" + transactordep + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorpos + "</u></b><br/>"
                   + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
                   + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';>"
                   + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
                   + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
                   + "<b>PAR No. : <u>" + "" + "</u></b><br/>"
                   + "<b>PAR Date : <u>" + "" + "</u></b></td><br/>"
                   + "</tr>"

                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                         + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            pdf = PDFConverter.AddSignature(pdf, fromcertbase64, frompassphrase);
            pdf = PDFConverter.AddSignature(pdf, bycertbase64, bypassphrase);

            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string CreatePDFWasteMaterial(string disposalno,
     string fromimagebase64,
     string byimagebase64,
     string frompassphrase,
     string bypassphrase,
     string fromcertbase64,
     string bycertbase64,
    bool fromdatestamp,
    bool bydatestamp)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];

            DataTable dt = y.PDLPendingUpload(x);

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
            + ".page-footer, .page-footer-space {height: 280px;}"
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
            html += "<tr><td colspan='4' align='center' ><br><h3>WASTE MATERIALS REPORT<h3></td</tr>";
            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 90px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br /><u>" + transactorname + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br /><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><center><u></u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><center><u></u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='7';><b>ITEMS FOR DISPOSAL</b></td></tr>"
                      + "<tr>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td colspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Record of Sales</b></td>"
                      + "</tr><tr>"
                      + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Official Receipt</b></td>"
                      + "</tr><tr>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Date</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Amount</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + i + 1 + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            pdf = PDFConverter.AddSignature(pdf, fromcertbase64, frompassphrase);
            pdf = PDFConverter.AddSignature(pdf, bycertbase64, bypassphrase);

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
    public static string CreatePDFDisposal(string disposalno,
     string fromimagebase64,
     string byimagebase64,
     string frompassphrase,
     string bypassphrase,
     string fromcertbase64,
     string bycertbase64,
    bool fromdatestamp,
    bool bydatestamp)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];

            DataTable dt = y.PDLPendingUpload(x);

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
            + ".page-header, .page-header-space {height: 265px; }"
            + ".page-footer, .page-footer-space {height: 270px;}"
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
                          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><br><b>DISPOSAL FORM<b></td</tr>"
                + "</table><br>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='width:70%;'></td><td style='font-size:14;width:30%;'>No. <u>" + disposalno + "</u></td</tr>"
                + "<tr><td style='font-size:12;width:70%;'><td style='font-size:12;width:30%;'>Date:<u>" + disposaldate + "</u></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
               + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br</td><td style='font-size:12;'><br><u></u></td></tr>"
                + "<tr><td style='font-size:12;'>Remarks/Purpose:</td><td style='font-size:12;' colspan='3'><u>" + remarks + "</u></td></tr></table>";

            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions
            html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'><b><u>TERMS AND CONDITIONS:</u></b>";
            html += "<table style='width:100%;'><tr><td><p align='justify' style='font-size:9.5px; font-family: Arial;'>In trust for and as property of <b> " + CompanyName + ",</b> the above described equipment having been lend and entrusted to me for trials and with the"
                + " obligation on my part to return the same complete in good order and condition before the agreed date. I further agree to the said property in my residence or place of business at the address indicated herein above and not to remove the same from said premises without previous knowledge and consent by <b>" + CompanyName + "</b>"
                + " I agree to use the Company Property provided to me solely for the intended uses: official business use and/or work related purposes. I am responsible for the care and"
                + " maintenance of the equipment. In case that the equipment is, in any case, damaged, lost, stolen, while outside the office premises, I shall be responsible for its cost of replacement or repair and other incidental charges it may occur."
                + " In case the equipment gets broken due to wear and tear, I shall notify the Company Custodian immediately and have it inspected by the Company's Technician as soon as possible. All civic actions on this contract shall be instituted in the court"//s of San Juan, Metro Manila"
                + " <p align='justify' style='font-size:9.5px; font-family: Arial;'>I agree to maintain the property in good condition and to return it when I cease working for the company, or earlier on request.";
            html += " By accepting this Agreement, I acknowledge the receipt in working order and good condition of the company-owned property listed above and I conform to the terms and condition stated.</p></td></tr></table>";
            //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:50%'>Transacted by</td><td  style='width:50%'>Approved by</td></tr>";//<td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
            html += "<tr rowspan='2'><td style='width:50%'><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='width:50%'><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<br><br>" + approvername + "</td></tr>";//<td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
            html += "<tr><td  style='width:50%'>Date</td><td  style='width:50%'>Date</td></tr>";//<td  style='width:25%'>Date</td><td  style='width:25%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>ITEM CODE</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>" + "1" + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            pdf = PDFConverter.AddSignature(pdf, fromcertbase64, frompassphrase);
            pdf = PDFConverter.AddSignature(pdf, bycertbase64, bypassphrase);

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
    public static string CreatePDFIIRUP(string disposalno,
     string fromimagebase64,
     string byimagebase64,
     string frompassphrase,
     string bypassphrase,
     string fromcertbase64,
     string bycertbase64,
    bool fromdatestamp,
    bool bydatestamp)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            string approverpos = disposaldetails.Split('|')[14];

            DataTable dt = y.PDLPendingUpload(x);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string g = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = g.Split('|')[0];
            string CompanyContactNumber = g.Split('|')[3];
            string CompanyAddress = g.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 265px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

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
                                   + "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px'/></td></tr>"

            + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE PROPERTY<b></td</tr>"

                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorpos + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<br><br><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorpos + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string cdp = "";
                    string tpaga = "";
                    string bart = "";
                    string don = "";
                    string rpv = "";
                    string sgoea = "";
                    string pa = "";
                    string ns = "";
                    string sjsr = "";
                    string dn = "";
                    string lost = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            cdp = "1";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            tpaga = "1";
                            break;

                        case "Barter":
                            bart = "1";
                            break;

                        case "Donation":
                            don = "1";
                            break;

                        case "Return to the Supplier/Vendor":
                            rpv = "1";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            sgoea = "1";
                            break;

                        case "Public Auction":
                            pa = "1";
                            break;

                        case "Negotiated Sale":
                            ns = "1";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            sjsr = "1";
                            break;

                        case "Direct Negotiation":
                            dn = "1";
                            break;

                        case "Lost":
                            lost = "1";
                            break;

                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + cdp + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + tpaga + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + bart + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + don + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + rpv + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + sgoea + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + pa + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + ns + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + sjsr + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dn + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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



            byte[] pdf = PDFConverter.ConvertHTMLtoPDF(html, PaperSize.A4, 1.25.Centimeters(), PDFConverter.PageOrientation.Landscape);

            pdf = PDFConverter.AddSignature(pdf, fromcertbase64, frompassphrase);
            pdf = PDFConverter.AddSignature(pdf, bycertbase64, bypassphrase);

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
    public static string CreatePDFIISEP(string disposalno,
     string fromimagebase64,
     string byimagebase64,
     string frompassphrase,
     string bypassphrase,
     string fromcertbase64,
     string bycertbase64,
    bool fromdatestamp,
    bool bydatestamp)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyDisposalBll y = new PropertyDisposalBll();

            x.DisposalNumber = disposalno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.PropertyDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string inspectorname = disposaldetails.Split('|')[3];
            string certifiername = disposaldetails.Split('|')[4];
            string transactordep = disposaldetails.Split('|')[5];
            string transactorpos = disposaldetails.Split('|')[6];
            string custodian = disposaldetails.Split('|')[7];
            string remarks = disposaldetails.Split('|')[8];
            string disposaldate = disposaldetails.Split('|')[9];
            string approverpos = disposaldetails.Split('|')[14];

            DataTable dt = y.PDLPendingUpload(x);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string g = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = g.Split('|')[0];
            string CompanyContactNumber = g.Split('|')[3];
            string CompanyAddress = g.Split('|')[5];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 295px; }"
            + ".page-footer, .page-footer-space {height: 265px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

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
            html += "<table style='width:100%;font-family: Arial;'>" +
                "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"

                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE SEMI-EXPENDABLE PROPERTY<b></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorpos + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
                + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<br><br><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorpos + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + approverpos + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + "_______________________________" + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";

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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
                     + "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
                      + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
                      + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dothis
                    string DepreciatedCostString = "";
                    if (depreciationSetting == "Straight Line Depreciation Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                        }
                    }
                    else if (depreciationSetting == "Diminishing Balance Method")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                        {
                            DepreciatedCostString = "0.00";
                        }
                        else
                        {
                            DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                        }
                    }
                    string cdp = "";
                    string tpaga = "";
                    string bart = "";
                    string don = "";
                    string rpv = "";
                    string sgoea = "";
                    string pa = "";
                    string ns = "";
                    string sjsr = "";
                    string dn = "";
                    string lost = "";
                    switch (dt.Rows[i]["ModeOfDisposal"].ToString())
                    {
                        case "Condemnation/Destruction of Properties":
                            cdp = "1";
                            break;

                        case "Transfer of Properties to Another Government Agency":
                            tpaga = "1";
                            break;

                        case "Barter":
                            bart = "1";
                            break;

                        case "Donation":
                            don = "1";
                            break;

                        case "Return to the Supplier/Vendor":
                            rpv = "1";
                            break;

                        case "Sale to Government Officials/Employees of the Agency":
                            sgoea = "1";
                            break;

                        case "Public Auction":
                            pa = "1";
                            break;

                        case "Negotiated Sale":
                            ns = "1";
                            break;
                        case "Sale of Junk or Scrap and/or Recycling":
                            sjsr = "1";
                            break;

                        case "Direct Negotiation":
                            dn = "1";
                            break;

                        case "Lost":
                            lost = "1";
                            break;

                        default:
                            break;
                    }
                    html += "<tr>"
                            + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + cdp + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + tpaga + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + bart + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + don + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + rpv + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + sgoea + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + pa + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + ns + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + sjsr + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dn + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

                          + "</tr>";
                }
                html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            byte[] pdf = PDFConverter.ConvertHTMLtoPDF(html, PaperSize.A4, 1.25.Centimeters(), PDFConverter.PageOrientation.Landscape);

            pdf = PDFConverter.AddSignature(pdf, fromcertbase64, frompassphrase);
            pdf = PDFConverter.AddSignature(pdf, bycertbase64, bypassphrase);

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
    public static string GetPendingUploadList1(string search)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.DisposalPendingUpload1(search, Common.ConnString, Convert.ToInt32(Common.SiteID), Common.CustodianID, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.ModeOfDisposal = dt.Rows[i]["ModeOfDisposal"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "<center>";
                    }
                    else
                    {
                        action += "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetailsRequest('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsCertified"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["Status"].ToString() + "','" + dt.Rows[i]["PARICS"].ToString() + "')\"><span class='fa fa-eye'></span></a>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "</center>";
                    }
                    else
                    {
                        action += " <a href='' class='btn btn-xs btn-danger btn_Cancel1' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
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
}