using AdvanceBll;
using AdvanceEntity;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

public partial class Operation_Module_PropertyIssuance : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetItem();
        CheckAccess();
    }

    #region Metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/PropertyIssuance.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
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
    public static string GetDepartmentPI()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();
        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList(departmentEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static List<System.Web.UI.WebControls.ListItem> GetUser(int department)
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.DepartmentID = department;
        userEntity.SiteID = Convert.ToInt32(Convert.ToInt32(Common.SiteID));
        userEntity.UserID = Convert.ToInt32(Convert.ToInt32(Common.UserID));
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetUser(userEntity);
    }

    [WebMethod]
    public static List<System.Web.UI.WebControls.ListItem> GetUserNoDepartment()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Convert.ToInt32(Common.SiteID));
        userEntity.UserID = Convert.ToInt32(Convert.ToInt32(Common.UserID));
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetUserNoDepartment(userEntity);
    }

    public void GetApprover()
    {
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetApproverListBySiteCustodian(Common.ConnString, Convert.ToInt32(Common.SiteID), Convert.ToInt32(Common.UserID), Convert.ToInt32(Common.CustodianID));

        ddlApprover.DataSource = dt;
        ddlApprover.DataValueField = "UserID";
        ddlApprover.DataTextField = "FullName";
        ddlApprover.DataBind();
        ddlApprover.Items.Insert(0, "--- Select Approver ---");
    }

    [WebMethod]
    public static List<System.Web.UI.WebControls.ListItem> GetApprover(int issuedto)
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Convert.ToInt32(Common.SiteID));
        userEntity.UserID = Convert.ToInt32(Convert.ToInt32(Common.UserID));
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetApproverInIssuance(userEntity, issuedto);
    }

    #endregion Metadata

    #region Datatables

    //Properties Available For Issuance
    [WebMethod]
    public static string GetList(int custodian, string search, int ExpenseType, int EmployeeID, string notlike)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailablePropertyForIssuance(search, custodian, Convert.ToInt32(Common.SiteID), Common.ConnString, ExpenseType, EmployeeID, notlike);
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
                        x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' pmlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Issuance List'></a></center>";
                    }

                    JsonItemList.Add(x);
                }
                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList);

                return html;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GetList2(int custodian, string search, int ExpenseType, int EmployeeID, string notlike)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailablePropertyForIssuance(search, custodian, Convert.ToInt32(Common.SiteID), Common.ConnString, ExpenseType, EmployeeID, notlike);
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
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();

                    x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' pmlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Issuance List'></a></center>";

                    JsonItemList.Add(x);
                }
                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList);

                return html;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }


    [WebMethod]
    public static string GetPropertyIssuanceList(int custodian, int employeeid, string typeCost)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.CustodianID = custodian;
        propertyEntity.EmployeeID = employeeid;
        propertyEntity.TransactionType = typeCost;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyIssuanceList(propertyEntity);
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

                    x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' pmlid='" + dt.Rows[i]["PMLID"].ToString() + "' data-toggle='tooltip'  title='Remove To Issuance List'><span class='fa fa-close'></span></a> </center>";

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

    //Pending Upload Issuance Transactions
    [WebMethod]
    public static string GetPendingUploadList(string search, int custodian)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.IssuancePendingUpload(search, Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    string action = "";
                    action = "<center><button type='button' class='btn btn-xs btn-primary  btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["IssuanceNo"] + "','PropertyIssuance')\"><span class='fa fa-paperclip'></span></button>";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button type='button' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["IssuanceNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "','" + dt.Rows[i]["Status"].ToString() + "','" + dt.Rows[i]["PARICS"].ToString() + "')\"><span class='fa fa-eye'></span></button>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button type='button' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelTransaction('" + dt.Rows[i]["IssuanceNo"] + "')\" ><span class='fa fa-times-circle'></span></button></center>";
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

    //Pending Upload Property Issuance  List
    [WebMethod]
    public static string GetPLPendingUploadList(string issuanceno)
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
    public static string GenerateIssuanceReport(string issuanceno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        x.IssuanceNumber = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.PropertyIssuanceDetails(x);
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

        DataTable dt = y.PLPendingUpload(x);

        return FormsAndReports.GenerateRequestFormCTR(dt, issuanceno, employeename, employeedep, employeepos, issueddate, remarks, custodian, approvername, issueddate);
    }

    [WebMethod]
    public static string GenerateIssuanceReportPAR(string issuanceno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        x.IssuanceNumber = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.PropertyIssuanceDetails(x);
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

        DataTable dt = y.PLPendingUploadWithComponent(x);

        return GenerateIssuanceFormPAR(dt, issuanceno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    public static string GenerateIssuanceFormPAR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            + ".page-header, .page-header-space {height: 270px; }"
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>"

            + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPTssss</h3></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Date : </td><td style='font-size:12;'><u>" + issueddate + "</u></td><td align='right' style='font-size:12;'>PAR No. : <u>" + issuanceno + "</u></td><td style='font-size:12;'><u>" + "" + "</u></td></tr></table>";
            html += "</div>";

            #endregion header


            #endregion

            #region header placeholder
            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                       + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
                          + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td colspan='2'><b>REMARKS:</b><br/><br/><center><u>" + remarks + "</u></center><br/></td></tr>";
            html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + "" + "</u><br/>Date<center><br/></td>" +
                "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + "" + "</u><br/>Date</center></td></tr>";
            html += "</table>";
            html += "</div>";
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
    public static string GenerateIssuanceReportICS(string issuanceno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        x.IssuanceNumber = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.PropertyIssuanceDetails(x);
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

        DataTable dt = y.PLPendingUploadWithComponent(x);

        return GenerateIssuanceFormICS(dt, issuanceno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    public static string GenerateIssuanceFormICS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            + ".page-header, .page-header-space {height: 270px; }"
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
            #endregion

            #region div header and footer
            #region header
            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";



            html += "<tr><td style='font-size:12;'>Date : </td><td style='font-size:12;'><u>" + issueddate + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            #endregion header


            #endregion

            #region header placeholder
            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Inventory Item No.</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Acquisition Cost</b></td>"
                      + "<td  style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["AcquiredCost"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";

            html += "<tr><td colspan='2'><b>REMARKS:</b><br/><br/><center><u>" + remarks + "</u></center><br/></td></tr>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorposition + "/" + transactordepartment + "</u></center><center>Position/Office</center><br/><center><u>" + "" + "</u><center><center>Date</center><br/></td>" +
                "<td width='50%'><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeeposition + "|" + employeedepartment + "</u></center><center>Position/Office</center><br/><center><u>" + "" + "</u><center><center>Date</center><br/></td></tr>";
            html += "</table>";
            html += "</div>";
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
    public static string GenerateIssuanceReportITR(string issuanceno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        x.IssuanceNumber = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.PropertyIssuanceDetails(x);
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

        string approverpos = issuancedetails.Split('|')[14];
        string issuedby = issuancedetails.Split('|')[15];
        string issuedpos = issuancedetails.Split('|')[16];

        DataTable dt = y.PLPendingUpload(x);

        return FormsAndReports.GenerateTransferFormITRIssuance(dt, issuanceno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    [WebMethod]
    public static string GenerateIssuanceReportRIS(string issuanceno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();

        x.IssuanceNumber = issuanceno;
        x.ConnectionString = Common.ConnString;

        string issuancedetails = y.PropertyIssuanceDetails(x);
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
        string issuedby = issuancedetails.Split('|')[15];
        string issuedpos = issuancedetails.Split('|')[16];


        DataTable dt = y.PLPendingUpload(x);

        return GenerateRISFormIssuance(dt, issuanceno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos, issuedby, issuedpos);
    }
    public static string GenerateRISFormIssuance(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site, string approverpos, string issuedby, string issuedpos)
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
                   + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header



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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
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
                    string statusNo = "";
                    string statusYes = "✓";
                    if (dt.Rows[i]["Status"].ToString() == "Issued")
                    {
                        statusNo = "✓";
                        statusYes = "";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
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

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + issuedby + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td style='text-align: center;'>" + approverpos + "</td><td style='text-align: center;'>" + issuedpos + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer
            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    static string CompanyName = "";
    static string CompanyContactNumber = "";
    static string CompanyAddress = "";
    static string CompanyLogo = "";


    [WebMethod]
    public static string GenerateIssuanceReportRISRequest(string requestno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyRequestBll z = new PropertyRequestBll();

        x.RequestNumber = requestno;
        x.ConnectionString = Common.ConnString;

        string requestdetails = y.PropertyRequestDetails(x);
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
        string returndate = requestdetails.Split('|')[12];
        string site = requestdetails.Split('|')[13];
        string IssuedBy = requestdetails.Split('|')[15];
        string IssuedByPos = requestdetails.Split('|')[16];
        string approverpos = requestdetails.Split('|')[14];

        DataTable dt = z.PRLPendingUpload(x);

        return FormsAndReports.GenerateRISFormIssuanceRequest(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, IssuedBy, IssuedByPos, approverpos);
    }

    [WebMethod]
    public static string GenerateIssuanceReportPARRequest(string requestno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyRequestBll z = new PropertyRequestBll();

        x.RequestNumber = requestno;
        x.ConnectionString = Common.ConnString;

        string requestdetails = y.PropertyRequestDetails(x);
        string employeename = requestdetails.Split('|')[1];
        string employeedep = requestdetails.Split('|')[2];
        string employeepos = requestdetails.Split('|')[3];
        string approvername = requestdetails.Split('|')[4];
        string transactorname = "";
        string transactordep = "";
        string transactorpos = "";
        string custodian = requestdetails.Split('|')[5];
        string remarks = requestdetails.Split('|')[6];
        string issueddate = requestdetails.Split('|')[7];
        string returndate = requestdetails.Split('|')[9];

        DataTable dt = z.PRLPendingUpload(x);

        return FormsAndReports.GenerateIssuanceFormPARRequest(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    [WebMethod]
    public static string GenerateIssuanceReportICSRequest(string requestno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyRequestBll z = new PropertyRequestBll();

        x.RequestNumber = requestno;
        x.ConnectionString = Common.ConnString;

        string requestdetails = y.PropertyRequestDetails(x);
        string employeename = requestdetails.Split('|')[1];
        string employeedep = requestdetails.Split('|')[2];
        string employeepos = requestdetails.Split('|')[3];
        string approvername = requestdetails.Split('|')[4];
        string transactorname = "";
        string transactordep = "";
        string transactorpos = "";
        string custodian = requestdetails.Split('|')[5];
        string remarks = requestdetails.Split('|')[6];
        string issueddate = requestdetails.Split('|')[7];
        string returndate = requestdetails.Split('|')[9];

        DataTable dt = z.PRLPendingUpload(x);

        return GenerateIssuanceFormICSRequest(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    public static string GenerateIssuanceFormICSRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            + ".page-header, .page-header-space {height: 270px; }"
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
            #endregion

            #region div header and footer
            #region header
            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";


            html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            #endregion header


            #endregion

            #region header placeholder
            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                      + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td colspan='2'><b>REMARKS:</b><br/><br/><center><u>" + remarks + "</u></center><br/></td></tr>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorposition + "/" + transactordepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
                "<td width='50%'><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeeposition + "|" + employeedepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
            html += "</table>";
            html += "</div>";
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
    public static string GenerateIssuanceReportITRRequest(string requestno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyRequestBll z = new PropertyRequestBll();

        x.RequestNumber = requestno;
        x.ConnectionString = Common.ConnString;

        string requestdetails = y.PropertyRequestDetails(x);
        string employeename = requestdetails.Split('|')[1];
        string employeedep = requestdetails.Split('|')[2];
        string employeepos = requestdetails.Split('|')[3];
        string approvername = requestdetails.Split('|')[4];
        string transactorname = "";
        string transactordep = "";
        string transactorpos = "";
        string custodian = requestdetails.Split('|')[5];
        string remarks = requestdetails.Split('|')[6];
        string issueddate = requestdetails.Split('|')[7];
        string returndate = requestdetails.Split('|')[9];

        DataTable dt = z.PRLPendingUpload(x);
        return FormsAndReports.GenerateTransferFormITRIssuanceRequest(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    //Pending Upload Request Transaction
    [WebMethod]
    public static string PropertyRequestPendingUpload(string search, int custodian)
    {
        PropertyRequestBll y = new PropertyRequestBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.RequestPendingUpload(search, Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.RequestNumber = dt.Rows[i]["RequestNo"].ToString();
                    x.RequestedBy = dt.Rows[i]["RequestorName"].ToString();
                    x.DateOfReturn = dt.Rows[i]["DateOfReturn"].ToString().Split(' ')[0];
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    string action = "";
                    action += "<center><button type='button' class='btn btn-xs btn-primary  btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["RequestNo"] + "','PropertyRequest')\"><span class='fa fa-paperclip'></span></button>";
                    action += " <button type='button' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssetsRequest('" + dt.Rows[i]["RequestNo"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "','" + dt.Rows[i]["PARICS"] + "')\"><span class='fa fa-eye'></span></button></center>";// +
                    x.Action = action;                                                                                                                                                                                                                                                                                                                                                                                  //" <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelTransactionRequest('" + dt.Rows[i]["RequestNo"] + "')\" ><span class='fa fa-times-circle'></span></a></center>";
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
    public static string GenerateRequestReport(string requestno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyRequestBll z = new PropertyRequestBll();

        x.RequestNumber = requestno;
        x.ConnectionString = Common.ConnString;

        string requestdetails = y.PropertyRequestDetails(x);
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
        string returndate = requestdetails.Split('|')[12];

        DataTable dt = z.PRLPendingUpload(x);

        return FormsAndReports.GenerateRequestForm(dt, requestno, employeename, employeedep, employeepos, issueddate, remarks, custodian, approvername, returndate);
    }

    #endregion Datatables

    #region Methods

    [WebMethod]
    public static string IfIssuanceNeedsApproval()
    {
        return Common.IssuanceApproval;
    }

    [WebMethod]
    public static string PropertyTransferToIssuance(int pmlid, int employeeid, int custodian, int ExpenseType)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = pmlid;
        x.EmployeeID = employeeid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.ExpenseType = ExpenseType;

        return y.PropertyTransferToIssuance(x).ToString();
    }

    [WebMethod]
    public static string PropertyRemoveToIssuance(int pmlid)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.PropertyRemoveToIssuance(x).ToString();
    }

    [WebMethod]
    public static string PropertyRemoveAllToIssuance(int employeeid, int custodian)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity x = new PropertyEntity();

        x.EmployeeID = employeeid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.PropertyRemoveAllToIssuance(x).ToString();
    }

    [WebMethod]
    public static string ConfirmPropertyIssuance(int employeeid, int custodians, int approverid, string remarks, int ExpenseType)
    {
        try
        {
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyEntity x = new PropertyEntity();

            x.EmployeeID = employeeid;
            x.Remarks = remarks;
            x.ApproverID = approverid;
            x.CustodianID = custodians;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;
            x.ExpenseType = ExpenseType;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string issuanceno = y.ConfirmPropertyIssuance(x);

            x.IssuanceNumber = issuanceno;
            DataTable dt = y.PLPendingUpload(x);

            string issuancedetails = y.PropertyIssuanceDetails(x);
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

            if (approveremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(approveremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Issuance)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
                Body += "A PROPERTY ISSUANCE transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                    + "Issuance No : " + issuanceno + "<br>"
                    + "Issued to: " + employeename + "<br>"
                    + "Remarks : " + remarks + " <br>"
                    + "Processed By : " + transactorname + " <br>"
                    + "Processed Date : " + issueddate + "<br><br>";
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

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
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
    public static string CancelPropertyIssuance(string issuanceno, string remarks)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity x = new PropertyEntity();

        x.IssuanceNumber = issuanceno;
        x.CreatedBy = Common.UserName;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;

        y.CancelPropertyIssuance(x);

        return "";
    }

    [WebMethod]
    public static string PropertyGetCount(int custodian, int employeeid, int ExpenseType)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity x = new PropertyEntity();

        x.CustodianID = custodian;
        x.EmployeeID = employeeid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.ExpenseType = ExpenseType;

        return y.PropertyGetCount(x);
    }

    [WebMethod]
    public static void UploadPropertyIssuance(string issuanceno)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity x = new PropertyEntity();

        x.IssuanceNumber = issuanceno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.UploadPropertyIssuance(x);
    }

    [WebMethod]
    public static string CreateRISRequestHTML(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];
            string site = requestdetails.Split('|')[10];
            string approverpos = requestdetails.Split('|')[11];
            string issuedby = requestdetails.Split('|')[12];
            string issuedpos = requestdetails.Split('|')[13];

            DataTable dt = z.PRLPendingUpload(x);

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
                 + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px'/></td></tr>";


            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";


            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions
            html += "<p style='font-size:11.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px; font-size:11.5px; font-family: Arial;'; valign='bottom'; align='left'; >Signature :</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left';>Requested by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Approved by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Issued by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Printed Name :</td><td style='text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + approvername + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + issuedby + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Designation :</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + approverpos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issuedpos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td></tr>";
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
                    string statusNo = "";
                    string statusYes = "✓";
                    if (dt.Rows[i]["Status"].ToString() == "Issued")
                    {
                        statusNo = "✓";
                        statusYes = "";
                    }
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
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
    public static string CreateITRRequestHTML(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];

            DataTable dt = z.PRLPendingUpload(x);


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
            + ".page-header, .page-header-space {height: 230px; }"
            + ".page-footer, .page-footer-space {height: 230px;}"
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

          + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"

            + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
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
    public static string CreateICSRequestHTML(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];

            DataTable dt = z.PRLPendingUpload(x);
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
            + ".page-header, .page-header-space {height: 270px; }"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 80px;'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><div style='width: 50%; height: 105px;' ></div><br /><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorpos + "/" + transactordep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
            "<td width='50%'><b>Received by:</b><br/><br/><br/><center><div style='width: 50%; height: 105px;' ></div><br /><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeepos + "|" + employeedep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
            html += "</table>";
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
                html += "<tr>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
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
    public static string CreateRISHTML(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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
            string issuedby = issuancedetails.Split('|')[15];
            string issuedpos = issuancedetails.Split('|')[16];

            DataTable dt = y.PLPendingUpload(x);

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
            + ".page-footer, .page-footer-space {height: 240px;}"
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
            html += "<p style='font-size:11.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px; font-size:11.5px; font-family: Arial;'; valign='bottom'; align='left'; >Signature :</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left';>Requested by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Approved by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Issued by</td><td style='font-size:11.5px; font-family: Arial;'; valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Printed Name :</td><td style='text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + approvername + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + issuedby + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</td></tr>";
            html += "<tr><td style='font-size:11.5px; font-family: Arial;'>Designation :</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + approverpos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + issuedpos + "</td><td style='font-size:11.5px; font-family: Arial; text-align: center;'>" + employeepos + "</td></tr>";
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
                    string statusNo = "";
                    string statusYes = "✓";
                    if (dt.Rows[i]["Status"].ToString() == "Issued")
                    {
                        statusNo = "✓";
                        statusYes = "";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
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
    public static string CreateICSHTML(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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

            DataTable dt = y.PLPendingUpload(x);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();
            string comapanyinfo = miscBll.GetCompanyInformation(Common.ConnString);

            string CompanyName = comapanyinfo.Split('|')[0];
            string CompanyContactNumber = comapanyinfo.Split('|')[3];
            string CompanyAddress = comapanyinfo.Split('|')[5];

            string html = "";

            //style
            html += "<html><head><style>"
            + ".page-header, .page-header-space {height: 270px; }"
            + ".page-footer, .page-footer-space {height: 330px;}"
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
            + "</style></head><body>";

            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";


            html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            html += "</div>";

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><div style='width: 50%; height: 100px;' ></div><br /><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorpos + "/" + transactordep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u></center><center>Date</center><br/></td>" +
                "<td width='50%'><b>Received by:</b><br/><br/><br/><center><div style='width: 50%; height: 100px;' ></div><br /><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeepos + "|" + employeedep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u></center><center>Date</center><br/></td></tr>";
            html += "</table>";
            html += "</div>";

            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
                    + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table></body></html>";


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
    public static string GeneratePDF(string issuanceno, string fromimagebase64, string byimagebase64)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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

            DataTable dt = y.PLPendingUpload(x);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string xx = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = xx.Split('|')[0];
            string CompanyContactNumber = xx.Split('|')[3];
            string CompanyAddress = xx.Split('|')[5];
            string html = "";

            #region styles

            html += "<html><head></head><body>";

            ////style
            string style = @".page-header, .page-header-space {height: 130px; } .page-footer, .page-footer-space {height: 330px;}.page-footer {position: fixed;bottom: 0;width: 100%; }.page-header {position: fixed;top: 0mm;width: 100%;  }.page-body {page-break-after: auto;} @page {size: A4; margin: 10mm;} @media print { table {padding: 0px 10px 0px 0px;} thead {display: table-header-group; } tfoot {display: table-footer-group;} body { margin: 0;}} .table-style { width:100%;font-family: Arial;}.td-style { font-size:15;}";

            #endregion styles

            #region div header and footer

            #region header

            //header div
            html += "<div class='page-header' style='height: 130px;position: fixed;top: 0mm;width: 100%;'>";
            html += "<table class='table-style'>"
                + "<tr><td align='center' class='td-style' style='font-size:15;'><h3>INVENTORY CUSTODIAN SLIP</h3><br /></td></tr></table>";
            html += "<table class='table-style'>"
                + "<tr><td colspan='4' style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='height: 330px;position: fixed;bottom: 0;width: 100%;'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='100%' height='150' /><br /><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorpos + "/" + transactordep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u></center><center>Date</center><br/></td>" +
                "<td width='50%'><b>Received by:</b><br/><br/><br/><center><img id='imgicsby' src='" + byimagebase64 + "' width='100%' height='150' /><br /><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeepos + "|" + employeedep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u></center><center>Date</center><br/></td></tr>";
            html += "</table>";
            html += "</div>";

            #endregion footer

            #endregion div header and footer

            #region header placeholder

            //table header
            html += "<div class='page-body' style='page-break-after: auto;line-height: 1;'><table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space' style='height: 130px;'>";
            html += "</div></td></tr></thead>";

            #endregion header placeholder

            #region body placeholder

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr>";
            if (dt.Rows.Count > 0)
            {
                html += ""
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                      + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                      + "<tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</tr></tbody>";

            #endregion body placeholder

            #region footer placeholder

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space' style='height: 330px;'>";
            html += "</div></td></tr></tfoot></table></div></body></html>";

            #endregion footer placeholder

            byte[] pdf;
            using (var memoryStream = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(style)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }

                document.Close();

                pdf = memoryStream.ToArray();
            }
            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    public static System.Drawing.Image LoadImage(string base64)
    {
        base64 = base64.Split(',')[1];

        byte[] bytes = Convert.FromBase64String(base64);

        System.Drawing.Image image;
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            image = System.Drawing.Image.FromStream(ms);
        }

        return image;
    }

    private static byte[] AddSignature(byte[] pdf,
        string certbase64,
        string passphrase)
    {
        certbase64 = certbase64.Split(',')[1];
        iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(pdf);
        iTextSharp.text.pdf.PdfStamper fromstamper;

        Org.BouncyCastle.Pkcs.Pkcs12Store store;
        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(certbase64)))
        {
            store = new Org.BouncyCastle.Pkcs.Pkcs12Store(ms, passphrase.ToCharArray());
            ms.Close();
        }
        var alias = "";
        foreach (string al in store.Aliases)
        {
            if (store.IsKeyEntry(al) && store.GetKey(al).Key.IsPrivate)
            {
                alias = al;
                break;
            }
        }

        MemoryStream ms1 = new MemoryStream();
        fromstamper = iTextSharp.text.pdf.PdfStamper.CreateSignature(reader, ms1, '\0', null, true);

        var pk = store.GetKey(alias);
        List<Org.BouncyCastle.X509.X509Certificate> chain = store.GetCertificateChain(alias).Select(c => c.Certificate).ToList();

        var parameters = pk.Key as Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters;

        var appearance = fromstamper.SignatureAppearance;

        iTextSharp.text.pdf.security.IExternalSignature pks = new iTextSharp.text.pdf.security.PrivateKeySignature(parameters, iTextSharp.text.pdf.security.DigestAlgorithms.SHA256);
        iTextSharp.text.pdf.security.MakeSignature.SignDetached(appearance, pks, chain, null, null, null, 0, iTextSharp.text.pdf.security.CryptoStandard.CMS);
        ms1.Close();
        return ms1.ToArray();
    }

    private static string CreatePDF(string issuanceno, string issuancedetails,
        DataTable dt,
        string comapanyinfo, string fromimagebase64, string byimagebase64, string frompassphrase,
        string bypassphrase,
        string fromcertbase64,
        string bycertbase64)
    {
        try
        {
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

            string CompanyName = comapanyinfo.Split('|')[0];
            string CompanyContactNumber = comapanyinfo.Split('|')[3];
            string CompanyAddress = comapanyinfo.Split('|')[5];

            byte[] pdf;
            var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
            using (var memoryStream = new MemoryStream())
            {
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
                document.AddAuthor("Internet Of Things");
                document.AddCreator("Internet Of Things");
                document.AddKeywords("Inventory Custodian Slip");
                document.AddSubject("Inventory Custodian Slip");
                document.AddTitle("Inventory Custodian Slip");
                document.Open();

                iTextSharp.text.Font ft;

                #region "Header"

                iTextSharp.text.pdf.PdfPTable table1 = new iTextSharp.text.pdf.PdfPTable(2);
                table1.WidthPercentage = 100;
                iTextSharp.text.pdf.PdfPCell cell;

                //Title
                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 15, iTextSharp.text.Font.BOLD);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("INVENTORY CUSTODIAN SLIP", ft));
                cell.Colspan = 2;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cell.BorderWidth = 0;
                table1.AddCell(cell);

                //Blank
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                cell.BorderWidth = 0;
                cell.HorizontalAlignment = 0;
                table1.AddCell(cell);
                //Blank
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                cell.BorderWidth = 0;
                cell.HorizontalAlignment = 0;
                table1.AddCell(cell);

                //Entity Name
                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Entity Name : " + CompanyName, ft));
                cell.BorderWidth = 0;
                cell.HorizontalAlignment = 0;
                table1.AddCell(cell);

                //Blank
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                cell.BorderWidth = 0;
                cell.HorizontalAlignment = 0;
                table1.AddCell(cell);

                //Func Cluster
                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Fund Cluster : ", ft));
                cell.BorderWidth = 0;
                cell.HorizontalAlignment = 0;
                table1.AddCell(cell);

                //ICS Mo
                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("ICS Mo : " + issuanceno, ft));
                cell.BorderWidth = 0;
                cell.HorizontalAlignment = 2;
                table1.AddCell(cell);

                document.Add(table1);

                #endregion "Header"

                document.Add(new iTextSharp.text.Paragraph(" "));

                if (dt.Rows.Count > 0)
                {
                    #region "Table Header"

                    iTextSharp.text.pdf.PdfPTable table2 = new iTextSharp.text.pdf.PdfPTable(7);
                    table2.WidthPercentage = 100;
                    iTextSharp.text.pdf.PdfPCell cell2;

                    //Quantity
                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Quantity", ft));
                    cell2.Rowspan = 2;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.VerticalAlignment = 5;
                    cell2.BorderWidth = 1;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Unit", ft));
                    cell2.Rowspan = 2;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.BorderWidth = 1;
                    cell2.VerticalAlignment = 5;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Amount", ft));
                    cell2.Colspan = 2;
                    cell2.VerticalAlignment = 5;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.BorderWidth = 1;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Description", ft));
                    cell2.Rowspan = 2;
                    cell2.VerticalAlignment = 5;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.BorderWidth = 1;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Item No.", ft));
                    cell2.Rowspan = 2;
                    cell2.VerticalAlignment = 5;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.BorderWidth = 1;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Estimated Useful Life", ft));
                    cell2.Rowspan = 2;
                    cell2.VerticalAlignment = 5;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.BorderWidth = 1;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Unit Cost", ft));
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.BorderWidth = 1;
                    cell2.VerticalAlignment = 5;
                    table2.AddCell(cell2);

                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                    cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Total Cost", ft));
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.VerticalAlignment = 5;
                    cell2.BorderWidth = 1;
                    table2.AddCell(cell2);

                    #endregion "Table Header"

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region "Table Body"

                        //Quantity
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("1", ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //UOMcode
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["UOMcode"].ToString(), ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //AcquiredCost
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()), ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //AcquiredCost
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()), ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //LongDescription
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["LongDescription"].ToString(), ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //Itemcode
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["ItemCode"].ToString(), ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //Usefule Life
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(dt.Rows[i]["UsefulLife"].ToString(), ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.BorderWidth = 1;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        //Nothing Follows
                        ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLDITALIC);
                        cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("***Nothing Follows ***", ft));
                        cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cell2.Colspan = 7;
                        cell2.BorderWidth = 0;
                        cell2.VerticalAlignment = 5;
                        table2.AddCell(cell2);

                        #endregion "Table Body"
                    }
                    document.Add(table2);
                }
                else
                {
                    ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.RED);
                    iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph("No Records Found", ft);
                    p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    document.Add(p);
                }

                #region "Footer"

                iTextSharp.text.pdf.PdfPTable tablefooter = new iTextSharp.text.pdf.PdfPTable(2);
                iTextSharp.text.Rectangle pagesize = writer.PageSize;
                tablefooter.TotalWidth = (pagesize.Width - 20);
                iTextSharp.text.pdf.PdfPCell cellfooter;

                #region "Table From"

                //Table Received from
                iTextSharp.text.pdf.PdfPTable tablefrom = new iTextSharp.text.pdf.PdfPTable(1);
                tablefrom.WidthPercentage = 100;
                iTextSharp.text.pdf.PdfPCell tablefromcell;

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Received from:", ft));
                tablefromcell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                System.Drawing.Image imgfrom = LoadImage(fromimagebase64);
                iTextSharp.text.Image picfrom = iTextSharp.text.Image.GetInstance(imgfrom, System.Drawing.Imaging.ImageFormat.Png);
                picfrom.ScaleAbsolute(230f, 150f);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(picfrom);
                tablefromcell.BorderWidth = 0;
                tablefromcell.HorizontalAlignment = 1;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(transactorname, ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Signature over Printed Name", ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(transactorpos + "/" + transactordep, ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Position/Office", ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(issueddate, ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablefromcell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Date", ft));
                tablefromcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablefromcell.VerticalAlignment = 5;
                tablefromcell.BorderWidth = 0;
                tablefrom.AddCell(tablefromcell);

                cellfooter = new iTextSharp.text.pdf.PdfPCell(tablefrom);
                tablefooter.AddCell(cellfooter);

                #endregion "Table From"

                #region "Table By"

                //Table Received from
                iTextSharp.text.pdf.PdfPTable tableby = new iTextSharp.text.pdf.PdfPTable(1);
                tableby.WidthPercentage = 100;
                iTextSharp.text.pdf.PdfPCell tablebycell;

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.BOLD);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Received by:", ft));
                tablebycell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                System.Drawing.Image imgby = LoadImage(byimagebase64);
                iTextSharp.text.Image picby = iTextSharp.text.Image.GetInstance(imgby, System.Drawing.Imaging.ImageFormat.Png);
                picby.ScaleAbsolute(230f, 150f);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(picby);
                tablebycell.BorderWidth = 0;
                tablebycell.HorizontalAlignment = 1;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(employeename, ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Signature over Printed Name", ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(employeepos + "/" + employeedep, ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Position/Office", ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(" ", ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.UNDERLINE);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(issueddate, ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                ft = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 10, iTextSharp.text.Font.NORMAL);
                tablebycell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Date", ft));
                tablebycell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablebycell.VerticalAlignment = 5;
                tablebycell.BorderWidth = 0;
                tableby.AddCell(tablebycell);

                cellfooter = new iTextSharp.text.pdf.PdfPCell(tableby);
                tablefooter.AddCell(cellfooter);

                #endregion "Table By"
                tablefooter.WriteSelectedRows(0, -1, 10, (tablefooter.TotalHeight + 20), writer.DirectContent);
                #endregion "Footer"

                document.Close();
                writer.Close();

                pdf = memoryStream.ToArray();
                pdf = AddSignature(pdf, fromcertbase64, frompassphrase);
                pdf = AddSignature(pdf, bycertbase64, bypassphrase);
            }
            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    public class dfsfsfsfsfs : iTextSharp.text.pdf.PdfPageEventHelper
    {
        public string HeaderHtml { get; set; }
        public string FooterHtml { get; set; }
        public iTextSharp.tool.xml.ElementList Header { get; set; }
        public iTextSharp.tool.xml.ElementList Footer { get; set; }

        public override void OnOpenDocument(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            Header = iTextSharp.tool.xml.XMLWorkerHelper.ParseToElementList(HeaderHtml, null);
            Footer = iTextSharp.tool.xml.XMLWorkerHelper.ParseToElementList(FooterHtml, null);
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            // Add the header to the top of the page
            if (Header != null)
            {
                foreach (var element in Header)
                {
                    document.Add(element);
                }
            }

            // Add the footer to the bottom of the page
            if (Footer != null)
            {
                foreach (var element in Footer)
                {
                    document.Add(element);
                }
            }
        }
    }

    [WebMethod]
    public static string CreatePARRequest(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];

            DataTable dt = z.PRLPendingUpload(x);

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
            + ".page-header, .page-header-space {height: 270px; }"
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
            html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px'/></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>PAR Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";

            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeepos + "/" + employeedep + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
            "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorpos + "|" + transactordep + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
            html += "</table>";
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
                html += "<tr>"
                       + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
                          + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
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
    public static string CreatePAR(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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

            DataTable dt = y.PLPendingUpload(x);

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
            + ".page-header, .page-header-space {height: 270px; }"
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
            html += "<table style='width:100%;font-family: Arial;'>"
          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"

            + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT</h3></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td align='right' style='font-size:12;'>PAR No. : <u>" + issuanceno + "</u></td><td style='font-size:12;'><u>" + "" + "</u></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeepos + "/" + employeedep + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
                "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center><img id='imgicsby' src='" + byimagebase64 + "'  width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorpos + "|" + transactordep + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
            html += "</table>";
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
                html += "<tr>"
                       + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
                          + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
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
    public static string CreateITRRequest(string issuanceno,
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
            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];

            DataTable dt = z.PRLPendingUpload(x);


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
            + ".page-header, .page-header-space {height: 230px; }"
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
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"

            + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
            html += "<tr><td><table style='font-size:75%;';>";
            html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
            html += "<tr><td>Signature :</td><td><img src='" + itrapprovedimagebase64 + "' width='50%' height='75' /><br />" + (itrapproveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreleaseimagebase64 + "' width='50%' height='75' /><br />" + (itrreleaseddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreceivedimagebase64 + "' width='50%' height='75' /><br />" + (itrreceiveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td></tr>";
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
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
            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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
            string issuedby = issuancedetails.Split('|')[15];
            string issuedpos = issuancedetails.Split('|')[16];

            DataTable dt = y.PLPendingUpload(x);

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
            + ".page-header, .page-header-space {height: 230px; }"
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
            html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
            html += "<tr><td><table style='font-size:75%;';>";
            html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
            html += "<tr><td>Signature :</td><td><img src='" + itrapprovedimagebase64 + "' width='50%' height='75' /><br />" + (itrapproveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreleaseimagebase64 + "' width='50%' height='75' /><br />" + (itrreleaseddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreceivedimagebase64 + "' width='50%' height='75' /><br />" + (itrreceiveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td></tr>";
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
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
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

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";

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

            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];
            string site = requestdetails.Split('|')[10];
            string approverpos = requestdetails.Split('|')[11];
            string issuedby = requestdetails.Split('|')[12];
            string issuedpos = requestdetails.Split('|')[13];

            DataTable dt = z.PRLPendingUpload(x);

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
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td><td style='text-align: center;'><img src='" + approvedimagebase64 + "' width='50%' height='75' /><br />" + (approveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + approvername + "</td><td style='text-align: center;'><img src='" + issuedimagebase64 + "' width='50%' height='75' /><br />" + (issueddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + issuedby + "</td><td style='text-align: center;'><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeepos + "</td><td></td><td></td><td style='text-align: center;'>" + employeepos + "</td></tr>";
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
                    string statusNo = "";
                    string statusYes = "✓";
                    if (dt.Rows[i]["Status"].ToString() == "Issued")
                    {
                        statusNo = "✓";
                        statusYes = "";
                    }
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
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
            PropertyEntity x = new PropertyEntity();
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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
            string issuedby = issuancedetails.Split('|')[15];
            string issuedpos = issuancedetails.Split('|')[16];

            DataTable dt = y.PLPendingUpload(x);

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
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td><td style='text-align: center;'><img src='" + approvedimagebase64 + "' width='50%' height='75' /><br />" + (approveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + approvername + "</td><td style='text-align: center;'><img src='" + issuedimagebase64 + "' width='50%' height='75' /><br />" + (issueddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + issuedby + "</td><td style='text-align: center;'><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeepos + "</td><td style='text-align: center;'>" + approverpos + "</td><td style='text-align: center;'>" + issuedpos + "</td><td style='text-align: center;'>" + employeepos + "</td></tr>";
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
                    string statusNo = "";
                    string statusYes = "✓";
                    if (dt.Rows[i]["Status"].ToString() == "Issued")
                    {
                        statusNo = "✓";
                        statusYes = "";
                    }
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
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
    public static string CreateICSRequest(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();
            PropertyRequestBll z = new PropertyRequestBll();

            x.RequestNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string requestdetails = y.PropertyRequestDetails(x);
            string employeename = requestdetails.Split('|')[1];
            string employeedep = requestdetails.Split('|')[2];
            string employeepos = requestdetails.Split('|')[3];
            string approvername = requestdetails.Split('|')[4];
            string transactorname = "";
            string transactordep = "";
            string transactorpos = "";
            string custodian = requestdetails.Split('|')[5];
            string remarks = requestdetails.Split('|')[6];
            string issueddate = requestdetails.Split('|')[7];
            string returndate = requestdetails.Split('|')[9];

            DataTable dt = z.PRLPendingUpload(x);
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
            + ".page-header, .page-header-space {height: 270px; }"
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

            ////header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";

            html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorpos + "/" + transactordep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
            "<td width='50%'><b>Received by:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeepos + "|" + employeedep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
            html += "</table>";
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
                html += "<tr>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
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
    public static string CreateICS(string issuanceno,
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
            PropertyIssuanceBll y = new PropertyIssuanceBll();

            x.IssuanceNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string issuancedetails = y.PropertyIssuanceDetails(x);
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

            DataTable dt = y.PLPendingUpload(x);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();
            string comapanyinfo = miscBll.GetCompanyInformation(Common.ConnString);

            string CompanyName = comapanyinfo.Split('|')[0];
            string CompanyContactNumber = comapanyinfo.Split('|')[3];
            string CompanyAddress = comapanyinfo.Split('|')[5];

            string html = "";

            //style
            html += "<html><head><style>"
            + ".page-header, .page-header-space {height: 270px; }"
            + ".page-footer, .page-footer-space {height: 330px;}"
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
            + "</style></head><body>";

            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                 + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
                + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY CUSTODIAN SLIP</h3><br /></td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4' style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
            html += "</div>";

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 10px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorpos + "/" + transactordep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u></center><center>Date</center><br/></td>" +
                "<td width='50%'><b>Received by:</b><br/><br/><br/><center><img id='imgicsby' src='" + byimagebase64 + "'  width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeepos + "|" + employeedep + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u></center><center>Date</center><br/></td></tr>";
            html += "</table>";
            html += "</div>";

            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";

            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
                + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
                    + "</tr>";
                }
                html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";

            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table></body></html>";

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

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        string issuanceno = txtIssuanceNo.Text;
        UploadPropertyIssuance(issuanceno);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccessW();", true);
    }

    public bool success = false;

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string issuanceno = txtIssuanceNo.Text;
            if (AFupload.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/PropertyIssuance/" + issuanceno + "/");
                string pathFinal = rootPath + '/' + issuanceno + '/';
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
                    UploadPropertyIssuance(issuanceno);
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

    //Property Request
    [WebMethod]
    public static void UploadPropertyRequest(string requestno)
    {
        PropertyRequestBll y = new PropertyRequestBll();
        PropertyEntity x = new PropertyEntity();

        x.RequestNumber = requestno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.UploadPropertyRequest(x);
    }

    protected void btnCompleteRequest_Click(object sender, EventArgs e)
    {
        string requestno = txtRequestNo.Text;
        UploadPropertyRequest(requestno);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccessRequestW();", true);
    }

    public bool successRequest = false;

    protected void btnUploadRequest_Click(object sender, EventArgs e)
    {
        try
        {
            string requestno = txtRequestNo.Text;
            if (fuUploadRequest.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/PropertyRequest/" + requestno + "/");
                string pathFinal = rootPath + '/' + requestno + '/';
                foreach (HttpPostedFile file in fuUploadRequest.PostedFiles)
                {
                    if (file.ContentLength > 0)
                    {
                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);
                        }
                        file.SaveAs(Path.Combine(rootPath, file.FileName));
                        successRequest = true;
                        continue;
                    }
                    else
                    {
                        successRequest = false;
                        break;
                    }
                }
                if (successRequest == true)
                {
                    UploadPropertyRequest(requestno);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccessRequest();", true);
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

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        smtp.Send(mail);

        string dddreturn = "okay";
    }
}
