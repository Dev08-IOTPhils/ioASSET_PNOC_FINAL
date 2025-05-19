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

public partial class Operation_Module_PropertyMaintenance : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        if (!IsPostBack)
        {
            ServiceProvider();
            GetTask();
        }

    }

    #region metadata
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Operation_Module/PropertyMaintenance.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    private void GetTask()
    {
        TaskBll t = new TaskBll();
        DataTable dt = t.GetTaskList(Common.ConnString);

        ddlTask.DataSource = dt;
        ddlTask.DataValueField = "TaskID";
        ddlTask.DataTextField = "TaskName";
        ddlTask.DataBind();
        ddlTask.Items.Insert(0, "--- Select Task ---");

    }

    private void ServiceProvider()
    {
        SupplierBll s = new SupplierBll();
        DataTable dt = s.GetServiceProviderList(Common.ConnString);

        ddlAssignTo.DataSource = dt;
        ddlAssignTo.DataValueField = "SupplierID";
        ddlAssignTo.DataTextField = "SupplierName";
        ddlAssignTo.DataBind();
        ddlAssignTo.Items.Insert(0, "--- Select Service Provider ---");

    }

    [WebMethod]
    public static string TaskByID(int id)
    {
        TaskEntity TaskEntity = new TaskEntity();
        TaskBll TaskBll = new TaskBll();

        TaskEntity.TaskID = id;
        TaskEntity.ConnectionString = Common.ConnString;

        return TaskBll.TaskbyID(TaskEntity);
    }

    #endregion

    #region Datatables

    //Properties Available For Maintenance
    [WebMethod]
    public static string GetList(string search, string FilterDateFrom, string FilterDateTo)
    {
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailablePropertyForMaintenance(Common.ConnString, search, Convert.ToInt32(Common.SiteID), FilterDateFrom, FilterDateTo);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.DateOfMaintenance = dt.Rows[i]["MaintenanceDate"].ToString();
                    x.MaintenanceLeadTime = dt.Rows[i]["MaintenanceLeadTime"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success btn_Add' pmlid='"+ dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Maintenance List'><span class='fa fa-plus'></span></a> </center>";
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

    //Properties Available For Maintenance
    [WebMethod]
    public static string GetPropertyOnMaintenance(string search)
    {
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailablePropertyOnMaintenance(Common.ConnString, search, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Dept = dt.Rows[i]["Description"].ToString().Replace("\n"," ");
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = Dept + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.DateOfCreation = dt.Rows[i]["CreatedDate"].ToString();
                    x.NoOfDaysFromCreation = dt.Rows[i]["NoOfDaysCreatedDate"].ToString();
                    x.NextStep = dt.Rows[i]["NextStep"].ToString();
                    x.PercentageOfCompletion = "<a href='#' class='percent_design' data-toggle='tooltip' title='" + dt.Rows[i]["PercentageOfCompletion"].ToString() + "% Complete'><h3><small class='pull-right'>" + dt.Rows[i]["PercentageOfCompletion"].ToString() + "%</small></h3>"
                      + "<div class='progress xs'>"
                      + "<div class='progress-bar progress-bar-red' style='width: " + dt.Rows[i]["PercentageOfCompletion"].ToString() + "%' role='progressbar' aria-valuenow='20' aria-valuemin='0' aria-valuemax='100'>"
                      + "</div>"
                      + "</div>"
                       + "</a>";
                    x.MaintenanceRemarks = dt.Rows[i]["MaintenanceRemarks"].ToString();
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-warning btn_Edit' data-toggle='tooltip'  title='Update Property Maintenance' onclick=\"UpdateMaintenance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + Dept + "','" + dt.Rows[i]["TaskID"] + "','" + dt.Rows[i]["Step"] + "','" + dt.Rows[i]["MaintenanceRemarks"] + "', '" + dt.Rows[i]["SendEmail"].ToString() + "', '" + dt.Rows[i]["PMLID"].ToString() + "')\"><span class='fa fa-edit'></span></a> ";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-warning btn_Edit' data-toggle='tooltip'  title='Update Property Maintenance' onclick=\"UpdateMaintenance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + Dept + "','" + dt.Rows[i]["TaskID"] + "','" + dt.Rows[i]["Step"] + "','" + dt.Rows[i]["MaintenanceRemarks"] + "', '" + dt.Rows[i]["SendEmail"].ToString() + "', '" + dt.Rows[i]["PMLID"].ToString() + "')\"><span class='fa fa-edit'></span></a> "
                        + "<a href = '' class='btn btn-xs btn-danger btn_Stop' data-toggle='tooltip'  title='Terminate Property Maintenance' onclick=\"TerminateMaintenance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + Dept + "','" + dt.Rows[i]["TaskID"] + "','" + dt.Rows[i]["Step"] + "','" + dt.Rows[i]["NumberOfSteps"] + "','" + dt.Rows[i]["EmployeeID"] + "','" + dt.Rows[i]["ReturnRequestNo"] + "', 'For Disposal','" + dt.Rows[i]["PMLID"] + "')\"><span class='fa fa-stop'></span></a> "
                        + "<a class='btn btn-xs btn-info btn_times' data-toggle='tooltip'  title='Cancel Property Maintenance' onclick=\"cancelMaintenance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + dt.Rows[i]["PMLID"] + "')\" ><span class='fa fa-times'></span></a></center>";
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

    //Properties Available For Maintenance
    [WebMethod]
    public static string PropertyMaintenanceSteps(int recid)
    {
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyMaintenanceSteps(Common.ConnString, recid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.MaintenanceStep = dt.Rows[i]["NextStep"].ToString();
                    x.ServiceProvider = dt.Rows[i]["SupplierName"].ToString();
                    x.DateOfCreation = dt.Rows[i]["CreatedDate"].ToString();
                    x.MaintenanceRemarks = dt.Rows[i]["Remarks"].ToString();
                    x.MaintenanceStatus = dt.Rows[i]["Status"].ToString();

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
    public static int CancelMaintenance(int id, string propertyno,int pmlid, string remarks)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        x.RecordID = id;
        x.PropertyNumber = propertyno;
        x.CreatedBy = Common.UserName;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;
        x.RecID = pmlid;

        return y.CancelMaintenance(x);
    }


    [WebMethod]
    public static int AddToMaintenance(int id, string propertyno, int taskid, int serviceproviderid, string remarks, string sendEmail, string currentStep,int pmlid)
    {
        int ret;
        PropertyEntity x = new PropertyEntity();
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        x.RecordID = id;
        x.RecID = pmlid;
        x.TaskID = taskid;
        x.ServiceProviderID = serviceproviderid; //to whom
        x.MaintenanceRemarks = remarks;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.emailSend = sendEmail;

        ret = y.AddToMaintenance(x);
        //0 for add to maintenance
        //else for update
        try
        {
            if (sendEmail == "true")
            {
                sendingEmail(propertyno, serviceproviderid, currentStep, pmlid);
            }
        }
        catch (Exception)
        {
        }
        

        return ret;
    }

    [WebMethod]
    public static int TerminatePropertyMaintenance(int id, string propertyno, string remarks, string status, string returnrequestno,int pmlid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        x.RecID = pmlid;
        x.RecordID = id;
        x.PropertyNumber = propertyno;
        x.MaintenanceRemarks = remarks;
        x.Status = status;
        x.ReturnRequestNumber = returnrequestno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.TerminatePropertyMaintenance(x);
    }

    public bool success = false;
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(txtRecordIDhidden.Text);
            string propertyno = txtPropertyNumberhidden.Text;
            int taskid = Convert.ToInt32(txtTaskIDhidden.Text);
            string serviceproviderid = ddlAssignTo.SelectedItem.Value;
            string remarks = txtRemarks.Text;

            if (AttachmentUpload.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/PropertyMaintenance/" + id.ToString() + "-" + propertyno + "/");
                string directpath = Server.MapPath("~/SupportingDocuments/PropertyMaintenance/" + id.ToString() + "-" + propertyno + "/"+ txtStepCurrent.Text + "/");
                foreach (HttpPostedFile file in AttachmentUpload.PostedFiles)
                {
                    if (file.ContentLength > 0)
                    {
                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);                            
                        }
                        Directory.CreateDirectory(directpath);
                        file.SaveAs(Path.Combine(directpath, file.FileName));
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
                    AddToMaintenance(id, propertyno, taskid, Convert.ToInt32(serviceproviderid), remarks, "", "",1);
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

    #endregion

    public static string sendingEmail(string propertyNo, int IDsendTO, string Cstep, int pmlid)
    {
        string smtphost = Common.SMTPhost;
        string smtpport = Common.SMTPport;
        string smtpmail = Common.SMTPmail;
        string smtppass = Common.SMTPpass;
        string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
        string MaintenanceNumber = "";
        string desc = "";



        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        string mainTo = y.GetEmail(IDsendTO, Common.ConnString);
        DataTable dt = y.maintenanceInfo(propertyNo, Common.ConnString, pmlid);
        if (dt.Rows.Count > 0)
        {
            MaintenanceNumber = dt.Rows[0]["MaintenanceNo"].ToString();
            desc = dt.Rows[0]["desc"].ToString();
        }

        MailMessage mail = new MailMessage();


        mail.To.Add(mainTo);

        mail.From = new MailAddress(smtpmail);
        mail.Subject = Common.EmailSubject + " (Property Maintenance)";
        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Good Day, <br><br><br>";
        Body += "A maintenance task has been ASSIGNED to you.<br><br>Task overview<br>Maintenance Number: <strong>" + MaintenanceNumber + "</strong><br>";
        Body += "Item Number: <strong>" + propertyNo + "</strong><br>Item Description : <strong>" + desc + "</strong><br>Your Assigned Task : <strong>" + Cstep + "</strong><br><br>"
                + " <a href='" + hostaddress + "/Operation_Module/PropertyMaintenance.aspx'>View Property Maintenance page.</a><br><br>";
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