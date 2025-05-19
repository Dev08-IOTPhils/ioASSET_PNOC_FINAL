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

public partial class Operation_Module_PropertyReturn : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        GetItem();
    }

    #region Metada

    public static string AddAccess = "";
    public static string DeleteAccess = "";
    public static string EditAccess = "";
    public static string ViewAccess = "";

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

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/PropertyReturn.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    public void GetApprover()
    {
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetApproverListBySiteCustodian(Common.ConnString, Convert.ToInt32(Common.SiteID), Convert.ToInt32(Common.UserID), Convert.ToInt32(Common.CustodianID));
    }


    [WebMethod]
    public static string GetDepartmentPR()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();
        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList(departmentEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    public void GetItem()
    {
        ddlItem.Items.Clear();
        ddlItem.Items.Add("--- Select Property Type Cost ---");
        ddlItem.Items.Add("ICS LOW - " + " Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and below.");
        ddlItem.Items.Add("ICS HIGH - " + " Between above Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
        ddlItem.Items.Add("PAR - " + " Above Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
    }

    #endregion Metada

    #region Datatables

    [WebMethod]
    public static string GenerateViewListReturn(string returnno, string Remarks, string EmployeePosition, string EmployeeDepartment, string CustodianPosition, string CustodianDepartment, string CustodianName, string employeename, string daterequested)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturn(dt, returnno, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName, employeename, daterequested);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GenerateViewListReturnCTR(string returnno)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturnCTR(dt, "", "", "", "", "", "", "", "", "");
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GenerateViewListReturnRAR(string returnno, string Remarks, string employeename, string daterequested, string CustodianName)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturnRAR(dt, Remarks, returnno, employeename, daterequested, CustodianName);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GenerateViewListReturnRARRequest(string returnno, string Remarks, string employeename, string daterequested, string CustodianName)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturnRAR(dt, Remarks, returnno, employeename, daterequested, CustodianName);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GenerateViewListReturnPPE(string returnno, string Remarks, string employeename, string daterequested, string CustodianName, string EmployeePosition, string CustodianPosition)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnList(Common.ConnString, returnno);
            return GenerateListReturnPPE(dt, Remarks, returnno, employeename, daterequested, CustodianName, EmployeePosition, CustodianPosition);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string GenerateListReturnPPE(DataTable dt, string Remarks, string returnno, string employeename, string daterequested, string CustodianName, string EmployeePosition, string CustodianPosition)
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
            + ".page-header, .page-header-space {height: 305px; }"
            + ".page-footer, .page-footer-space {height: 140px;}"
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

            + "<tr><td align='center' style='font-size:13;'></br><h3>RETURN SLIP FOR PROPERTY, PLANT AND EQUIPMENT</h3></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:11;'><b>MEMORANDUM</b></td></tr>"
                + "<tr><td style='text-align: right; font-size:12;'>" + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                + "<tr><td style='text-align: right; font-size:11;'><b>Date</b></td></tr>"
                + "<tr><td style='font-size:11;'><b>FOR: Asset Management Section</b></td></tr>"
                + "<tr><td style='font-size:11;'><b>FROM : " + employeename + "</b></td></tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='34%';  style='text-align: center;'> <b>RETURNED BY: </b></td><td width='34%';  style='text-align: center;'> <b>RECEIVED BY:</b> </td><td width='34%';  style='text-align: center;'> <b>NOTED BY:</b> </td></tr>";
            html += "<tr><td width='34%';  style='text-align: center;'> <br/>" + employeename + "<br/>" + EmployeePosition + " </td><td width='34%';  style='text-align: center;'> <br/>" + CustodianName + "<br/>" + CustodianPosition + " </td><td width='34%';  style='text-align: center;'> <br/>" + "" + "  </td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                      + "<td width='5%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td width='5%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description/Particulars</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Memo Receipt-<br/>Property No.</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date<br/>Acquired</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remark/Reason for<br/> Return</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + daterequested + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["AcquiredCost"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["AcquiredCost"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateViewListReturnRARSEP(string returnno, string Remarks, string employeename, string daterequested, string CustodianName)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturnRARSEP(dt, Remarks, returnno, employeename, daterequested, CustodianName);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GenerateViewListReturnRARSEPRequest(string returnno, string Remarks, string employeename, string daterequested, string CustodianName)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturnRARSEP(dt, Remarks, returnno, employeename, daterequested, CustodianName);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GenerateViewListReturnRequestPPE(string returnno, string Remarks, string employeename, string daterequested, string CustodianName, string EmployeePosition)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, returnno);
            return GenerateListReturnRequestPPE(dt, Remarks, returnno, employeename, daterequested, CustodianName, EmployeePosition);
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public static string GenerateListReturnRequestPPE(DataTable dt, string Remarks, string returnno, string employeename, string daterequested, string CustodianName, string EmployeePosition)
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
            + ".page-header, .page-header-space {height: 305px; }"
            + ".page-footer, .page-footer-space {height: 140px;}"
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

            + "<tr><td align='center' style='font-size:13;'></br><h3>RETURN SLIP FOR PROPERTY, PLANT AND EQUIPMENT</h3></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:11;'><b>MEMORANDUM</b></td></tr>"
                + "<tr><td style='text-align: right; font-size:12;'>" + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                + "<tr><td style='text-align: right; font-size:11;'><b>Date</b></td></tr>"
                + "<tr><td style='font-size:11;'><b>FOR: Asset Management Section</b></td></tr>"
                + "<tr><td style='font-size:11;'><b>FROM : " + employeename + "</b></td></tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='34%';  style='text-align: center;'> <b>RETURNED BY: </b></td><td width='34%';  style='text-align: center;'> <b>RECEIVED BY:</b> </td><td width='34%';  style='text-align: center;'> <b>NOTED BY:</b> </td></tr>";
            html += "<tr><td width='34%';  style='text-align: center;'> <br/>" + employeename + "<br/>" + EmployeePosition + " </td><td width='34%';  style='text-align: center;'> <br/>" + "" + " </td><td width='34%';  style='text-align: center;'> <br/>" + "" + "  </td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                      + "<td width='5%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                      + "<td width='5%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description/Particulars</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Memo Receipt-<br/>Property No.</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date<br/>Acquired</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remark/Reason for<br/> Return</b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "1" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + daterequested + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["AcquiredCost"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["AcquiredCost"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateViewListReturnRequest(string returnno, string Remarks, string EmployeePosition, string EmployeeDepartment, string CustodianPosition, string CustodianDepartment, string CustodianName, string employeename, string daterequested)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, returnno);
            return FormsAndReports.GenerateListReturnRequest(dt, returnno, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName, employeename, daterequested);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GetPropertyAvailableForReturn(string search, int employeeid, int custodian, int ExpenseType)
    {
        PropertyReturnBll y = new PropertyReturnBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyAvailableForReturn(search, employeeid, Common.ConnString, Convert.ToInt32(Common.SiteID), custodian, ExpenseType);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.pmlid = Convert.ToInt32(dt.Rows[i]["RecID"]);
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.TransactorName = dt.Rows[i]["EmployeeName"].ToString();
                    x.DateOfReturn = dt.Rows[i]["DateOfReturn"].ToString().Split(' ')[0];
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success btn_Add' pmlid= '" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Return List'><span class='fa fa-plus'></span></a> </center>";
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
    public static string GetPropertyReturnPending(string search)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyReturnPending(search, Common.ConnString, Convert.ToInt32(Common.SiteID), Common.CustodianID, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ReturnNumber = dt.Rows[i]["ReturnNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.NoOfItemReturn = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewListReturn('" + dt.Rows[i]["ReturnNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["Status"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["Remarks"] + "','" + dt.Rows[i]["EmployeePosition"] + "','" + dt.Rows[i]["EmployeeDepartment"] + "','" + dt.Rows[i]["TransactorPosition"] + "','" + dt.Rows[i]["TransactorDepartment"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["PARICS"] + "')\"><span class='fa fa-eye'></span></a>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += "&nbsp;<a href = '' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelReturn('" + dt.Rows[i]["ReturnNo"] + "')\"><span class='fa fa-times-circle'></span></a></center>";
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
    public static string GetPropertyReturnPendingUpload(string search)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyReturnPendingUpload(search, Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ReturnRequestNumber = dt.Rows[i]["ReturnRequestNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.NoOfItemReturn = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    if (ViewAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewList('" + dt.Rows[i]["ReturnRequestNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["Status"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["Remarks"] + "','" + dt.Rows[i]["EmployeePosition"] + "','" + dt.Rows[i]["EmployeeDepartment"] + "','','','" + dt.Rows[i]["Custodian"] + "','" + dt.Rows[i]["PARICS"] + "')\"><span class='fa fa-eye'></span></a>";
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
    public static string GetReturnPropertyList(int employeeid, int custodian, string ExpenseType)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetForReturnPropertyList(Common.ConnString, employeeid, custodian, ExpenseType);
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

                    x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' pmlid= '" + dt.Rows[i]["PMLID"].ToString() + "' data-toggle='tooltip'  title='Remove To Return List'><span class='fa fa-close'></span></a> </center>";

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
    public static string PropertyReturnList(string returnno)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnList(Common.ConnString, returnno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string condition = dt.Rows[i]["Reason"].ToString();
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    switch (condition)
                    {
                        case "Termination of employment":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Reason</option><option value='Termination of employment' selected>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Loss of value due to depreciation":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Reason</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation' selected>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Maintenance":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Reason</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance' selected>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal/option></select>";
                            break;

                        case "Repair":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Reason</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair' selected>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Disposal":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Reason</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal' selected>Disposal</option></select>";
                            break;

                        case "":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlCondition" + i + "' onchange='changeCondition(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason' selected>Select Reason</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;
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
    public static string PropertyReturnRequestList(string returnrequestno)
    {
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, returnrequestno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string condition = dt.Rows[i]["Reason"].ToString();
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    switch (condition)
                    {
                        case "Termination of employment":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlConditionr" + i + "' onchange='changeCondition1(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Reason</option><option value='Termination of employment' selected>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Loss of value due to depreciation":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlConditionr" + i + "' onchange='changeCondition1(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Condition</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation' selected>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Maintenance":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlConditionr" + i + "' onchange='changeCondition1(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Condition</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance' selected>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Repair":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlConditionr" + i + "' onchange='changeCondition1(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Condition</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair' selected>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;

                        case "Disposal":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlConditionr" + i + "' onchange='changeCondition1(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason'>Select Condition</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal' selected>Disposal</option></select>";
                            break;

                        case "":
                            x.Condition = "<select class='form-control select2 ddlcondition' id='ddlConditionr" + i + "' onchange='changeCondition1(" + dt.Rows[i]["RecID"].ToString() + "," + i + ")'><option value='Select Reason' selected>Select Condition</option><option value='Termination of employment'>Termination of employment</option><option value='Loss of value due to depreciation'>Loss of value due to depreciation</option><option value='Maintenance'>Maintenance</option><option value='Repair'>Repair</option><option value='Disposal'>Disposal</option></select>";
                            break;
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

    #endregion Datatables

    #region methods

    [WebMethod]
    public static string changeCondition(int recid, string condition)
    {
        PropertyReturnBll y = new PropertyReturnBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = recid;
        x.Condition = condition;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.changeConditionReturn(x).ToString();
    }

    [WebMethod]
    public static string changeConditionRequest(int recid, string condition)
    {
        PropertyReturnBll y = new PropertyReturnBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = recid;
        x.Condition = condition;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.changeConditionReturnRequest(x).ToString();
    }

    public bool success = false;

    [WebMethod]
    public static string CancelReturn(string returnno, string remarks)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.ReturnNumber = returnno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.Remarks = remarks;
        y.CancelReturn(x);

        return "";
    }

    [WebMethod]
    public static string ConfirmPropertyReturn(int employeeid, int custodian, int ExpenseType)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.EmployeeID = employeeid;
        x.CreatedBy = Common.UserName;
        x.CustodianID = custodian;
        x.ConnectionString = Common.ConnString;
        x.ExpenseType = ExpenseType;

        return y.ConfirmPropertyReturn(x).ToString();
    }

    [WebMethod]
    public static string GetCountPropertyReturn(int employeeid, int custodian, int ExpenseType)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.EmployeeID = employeeid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.CustodianID = custodian;
        x.ExpenseType = ExpenseType;

        return y.GetCountPropertyReturn(x);
    }

    [WebMethod]
    public static string RemoveAllToPropertyReturn(int employeeid, int custodian)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.EmployeeID = employeeid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.CustodianID = custodian;

        y.RemoveAllToPropertyReturn(x);

        return "";
    }

    [WebMethod]
    public static string RemoveToPropertyReturn(int pmlid, int employeeid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.EmployeeID = employeeid;
        x.RecID = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveToPropertyReturn(x);

        return "";
    }

    [WebMethod]
    public static string TransferToPropertyReturn(int pmlid, int employeeid, int custodian, int ExpenseType)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnRequestBll y = new PropertyReturnRequestBll();

        x.EmployeeID = employeeid;
        x.RecID = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.CustodianID = custodian;
        x.ExpenseType = ExpenseType;

        y.TransferToPropertyReturn(x);

        return "";
    }

    [WebMethod]
    public static void UploadReturnRequest(string returnrequestno, string remarks)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyReturnBll y = new PropertyReturnBll();

        x.ReturnRequestNumber = returnrequestno;
        x.Remarks = remarks;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.UploadReturnRequest(x);
    }

    [WebMethod]
    public static string CreatePDFReturnedReturn(string issuanceno,
       string fromimagebase64,
       string byimagebase64,
       string frompassphrase,
       string bypassphrase,
       string fromcertbase64,
       string bycertbase64,
       string Remarks,
       string EmployeePosition,
       string EmployeeDepartment,
       string CustodianPosition,
       string CustodianDepartment,
       string CustodianName,
       string employeename,
       string daterequested,
       bool fromdatestamp,
       bool bydatestamp)
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();

            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnList(Common.ConnString, issuanceno);
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
            + ".page-header, .page-header-space {height: 210px; }"
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
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;';><br/>Returned by :<br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + employeename + "</u></br>End User</br></br>" + daterequested + "</br>Date</center></td>" +
                "<td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><br/>Received by :<br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + DateTime.Now.ToString("MM/dd/yyyy") + "</br>Date</center></td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                html += "<tr><td colspan='5'; align='center'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><br><h4>RETURN AND RECEIPT OF PROPERTY/EQUIPMENT</h4></td></tr>"
                  + "<tr><td rowspan='2'; colspan='4'; align='left'; witdth='80%'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Entity Name: " + CompanyName + "<br/></td><td width='20%'; style='border-bottom:1px solid;border-right:1px solid;';>Date: " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                  + "<tr><td witdth='20%'; style='border-bottom:1px solid;border-right:1px solid;'></br>RRSP No. :</br>" + issuanceno + "</td></tr>"
                  + "<tr><td colspan='5' align='center'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' >This is to acknowledge receipt of the returned Property/Equipment</td></tr>"

                  + "<tr>"
                  + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                  + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                  + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ICS No.</b></td>"
                  + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>End-user</b></td>"
                  + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + employeename + "</td>"
                        + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
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
    public static string CreateReturnedHTML(string issuanceno,
   string imagebase64,
   string passphrase,
   string certbase64,
   bool datestamp,
   string transtype,
   string operationtype,
   string Remarks,
   string EmployeePosition,
   string EmployeeDepartment,
   string CustodianPosition,
   string CustodianDepartment,
   string CustodianName,
   string employeename,
   string daterequested)
    {
        try
        {

            PropertyReturnRequestBll y = new PropertyReturnRequestBll();
            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, issuanceno);

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
            + ".page-header, .page-header-space {height: 210px; }"
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
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;';><br/>Returned by :<br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + employeename + "</u></br>End User</br></br>" + daterequested + "</br>Date</center></td>" +
            "<td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><br/>Received by :<br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + DateTime.Now.ToString("MM/dd/yyyy") + "</br>Date</center></td></tr>";
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
                html += "<tr><td colspan='5'; align='center'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><br><h4>RETURN AND RECEIPT OF PROPERTY/EQUIPMENT</h4></td></tr>"
                + "<tr><td rowspan='2'; colspan='4'; align='left'; witdth='80%'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Entity Name: " + CompanyName + "<br/></td><td width='20%'; style='border-bottom:1px solid;border-right:1px solid;';>Date: " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                + "<tr><td witdth='20%'; style='border-bottom:1px solid;border-right:1px solid;'></br>RRSP No. :</br>" + issuanceno + "</td></tr>"
                + "<tr><td colspan='5' align='center'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' >This is to acknowledge receipt of the returned Property/Equipment</td></tr>"

                + "<tr>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ICS No.</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>End-user</b></td>"
                + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + employeename + "</td>"
                        + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
                        + "</tr>";
                }
                html += "<tr><td colspan='5' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string CreatePDFReturned(string issuanceno,
       string fromimagebase64,
       string byimagebase64,
       string frompassphrase,
       string bypassphrase,
       string fromcertbase64,
       string bycertbase64,
       string Remarks,
       string EmployeePosition,
       string EmployeeDepartment,
       string CustodianPosition,
       string CustodianDepartment,
       string CustodianName,
       string employeename,
       string daterequested,
       bool fromdatestamp,
       bool bydatestamp)
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();
            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, issuanceno);

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
            + ".page-header, .page-header-space {height: 210px; }"
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
            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;';><br/>Returned by :<br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + employeename + "</u></br>End User</br></br>" + daterequested + "</br>Date</center></td>" +
            "<td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><br/>Received by :<br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + DateTime.Now.ToString("MM/dd/yyyy") + "</br>Date</center></td></tr>";
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
                html += "<tr><td colspan='5'; align='center'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><br><h4>RETURN AND RECEIPT OF PROPERTY/EQUIPMENT</h4></td></tr>"
                + "<tr><td rowspan='2'; colspan='4'; align='left'; witdth='80%'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Entity Name: " + CompanyName + "<br/></td><td width='20%'; style='border-bottom:1px solid;border-right:1px solid;';>Date: " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                + "<tr><td witdth='20%'; style='border-bottom:1px solid;border-right:1px solid;'></br>RRSP No. :</br>" + issuanceno + "</td></tr>"
                + "<tr><td colspan='5' align='center'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' >This is to acknowledge receipt of the returned Property/Equipment</td></tr>"

                + "<tr>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ICS No.</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>End-user</b></td>"
                + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + employeename + "</td>"
                        + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
                        + "</tr>";
                }
                html += "<tr><td colspan='5' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string CreateRSEPHTML(string issuanceno,
    string imagebase64,
    string passphrase,
    string certbase64,
    bool datestamp,
    string transtype,
    string operationtype,
    string Remarks,
    string EmployeePosition,
    string EmployeeDepartment,
    string CustodianPosition,
    string CustodianDepartment,
    string CustodianName,
    string employeename,
    string daterequested)
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();

            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnList(Common.ConnString, issuanceno);
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
            + ".page-header, .page-header-space {height: 210px; }"
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
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;';><br/>Returned by :<br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + employeename + "</u></br>End User</br></br>" + daterequested + "</br>Date</center></td>" +
                "<td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><br/>Received by :<br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + DateTime.Now.ToString("MM/dd/yyyy") + "</br>Date</center></td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                html += "<tr><td colspan='5'; align='center'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><br><h4>RETURN AND RECEIPT OF PROPERTY/EQUIPMENT</h4></td></tr>"
                  + "<tr><td rowspan='2'; colspan='4'; align='left'; witdth='80%'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Entity Name: " + CompanyName + "<br/></td><td width='20%'; style='border-bottom:1px solid;border-right:1px solid;';>Date: " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                  + "<tr><td witdth='20%'; style='border-bottom:1px solid;border-right:1px solid;'></br>RRSP No. :</br>" + issuanceno + "</td></tr>"
                  + "<tr><td colspan='5' align='center'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' >This is to acknowledge receipt of the returned Property/Equipment</td></tr>"

                  + "<tr>"
                  + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                  + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                  + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ICS No.</b></td>"
                  + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>End-user</b></td>"
                  + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td></tr>";
                //+ "<td witdth='20%'; colspan='2'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>"+ TransactionNo + "</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + employeename + "</td>"
                        + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
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
    public static string CreatePDFTurnInReturnHTML(string issuanceno,
string imagebase64,
string passphrase,
string certbase64,
bool datestamp,
string transtype,
string operationtype,
 string Remarks,
     string EmployeePosition,
     string EmployeeDepartment,
     string CustodianPosition,
     string CustodianDepartment,
     string CustodianName,
     string employeename,
     string daterequested)
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();
            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnList(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];
            string TransactionNo = issuanceno;

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 210px; }"
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
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY TURN -IN SLIP</h3></br></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%' valign='top'><br/>Received by :<br/><br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + daterequested + "</br>Date</center></td>" +
            "<td>I CERTIFY that the articles listed on are turned-in under the circumtances indicated in 'REMARKS'<br/><br/><br/><center><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</center><center>Name of Personnel returning equipment</center><br/><br/><br/>Noted by:<br/><br/>___________________________________&emsp;_____________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Date<br/>Supply Accountable Officer Supply Accountable Group</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='3';></br>TO:&emsp;" + CustodianName + "</br>" + CustodianPosition + "," + CustodianDepartment + "</br></br></td><td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' width='10%';> Page of Pages </td><td width='10%'; style='text-align: right; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;' rowspan='2'; colspan='3';>FROM:&emsp;" + employeename + "</br>" + EmployeePosition + "," + EmployeeDepartment + "</td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Voucher No. </td><td style='text-align: right; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Turn-in Slip No. </td><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>" + TransactionNo + "</b></td></tr>"
                + "<tr><td colspan='5'  style='text-align: center; border-left:1px solid;border-right:1px solid;'>This is to acknowledge receipt of the returned Property/Equipment</td></tr>"
                + "<tr>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>";
                //+ "<td witdth='20%'; colspan='2'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>"+ TransactionNo + "</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> 1" + "" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ReturnNo"].ToString() + " . " + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
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
    public static string CreatePDFTurnInHTML(string issuanceno,
string imagebase64,
string passphrase,
string certbase64,
bool datestamp,
string transtype,
string operationtype,
 string Remarks,
     string EmployeePosition,
     string EmployeeDepartment,
     string CustodianPosition,
     string CustodianDepartment,
     string CustodianName,
     string employeename,
     string daterequested)
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();

            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, issuanceno);

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
            + ".page-header, .page-header-space {height: 210px; }"
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
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY TURN -IN SLIP</h3></br></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%' valign='top'><br/>Received by :<br/><br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + daterequested + "</br>Date</center></td>" +
            "<td>I CERTIFY that the articles listed on are turned-in under the circumtances indicated in 'REMARKS'<br/><br/><br/><center><div style='width: 50%; height: 75px;' ></div><br />" + employeename + "</center><center>Name of Personnel returning equipment</center><br/><br/><br/>Noted by:<br/><br/>___________________________________&emsp;_____________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Date<br/>Supply Accountable Officer Supply Accountable Group</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='3';></br>TO:&emsp;" + CustodianName + "</br>" + CustodianPosition + "" + CustodianDepartment + "</br></br></td><td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' width='10%';> Page of Pages </td><td width='10%'; style='text-align: right; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;' rowspan='2'; colspan='3';>FROM:&emsp;" + employeename + "</br>" + EmployeePosition + "," + EmployeeDepartment + "</td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Voucher No. </td><td style='text-align: right; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Turn-in Slip No. </td><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>" + issuanceno + "</b></td></tr>"
                + "<tr><td colspan='5'  style='text-align: center; border-left:1px solid;border-right:1px solid;'>This is to acknowledge receipt of the returned Property/Equipment</td></tr>"
                + "<tr>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> 1" + "" + "</td>"
                    + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ReturnRequestNo"].ToString() + " . " + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
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
    public static string CreatePDFTurnInReturn(string issuanceno,
     string fromimagebase64,
     string byimagebase64,
     string frompassphrase,
     string bypassphrase,
     string fromcertbase64,
     string bycertbase64,
     string Remarks,
     string EmployeePosition,
     string EmployeeDepartment,
     string CustodianPosition,
     string CustodianDepartment,
     string CustodianName,
     string employeename,
     string daterequested,
     bool fromdatestamp,
     bool bydatestamp)
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();
            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnList(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];
            string TransactionNo = issuanceno;

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 210px; }"
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
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY TURN -IN SLIP</h3></br></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%' valign='top'><br/>Received by :<br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + daterequested + "</br>Date</center></td>" +
            "<td>I CERTIFY that the articles listed on are turned-in under the circumtances indicated in 'REMARKS'<br/><br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</center><center>Name of Personnel returning equipment</center><br/><br/><br/>Noted by:<br/><br/>___________________________________&emsp;_____________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Date<br/>Supply Accountable Officer Supply Accountable Group</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='3';></br>TO:&emsp;" + CustodianName + "</br>" + CustodianPosition + "," + CustodianDepartment + "</br></br></td><td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' width='10%';> Page of Pages </td><td width='10%'; style='text-align: right; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;' rowspan='2'; colspan='3';>FROM:&emsp;" + employeename + "</br>" + EmployeePosition + "," + EmployeeDepartment + "</td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Voucher No. </td><td style='text-align: right; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Turn-in Slip No. </td><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>" + TransactionNo + "</b></td></tr>"
                + "<tr><td colspan='5'  style='text-align: center; border-left:1px solid;border-right:1px solid;'>This is to acknowledge receipt of the returned Property/Equipment</td></tr>"
                + "<tr>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>";
                //+ "<td witdth='20%'; colspan='2'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>"+ TransactionNo + "</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> 1" + "" + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ReturnNo"].ToString() + " . " + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
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
    public static string CreatePDFTurnIn(string issuanceno,
       string fromimagebase64,
       string byimagebase64,
       string frompassphrase,
       string bypassphrase,
       string fromcertbase64,
       string bycertbase64,
       string Remarks,
       string EmployeePosition,
       string EmployeeDepartment,
       string CustodianPosition,
       string CustodianDepartment,
       string CustodianName,
       string employeename,
       string daterequested,
       bool fromdatestamp,
       bool bydatestamp
       )
    {
        try
        {
            PropertyReturnRequestBll y = new PropertyReturnRequestBll();

            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();

            DataTable dt = y.PropertyReturnRequestList(Common.ConnString, issuanceno);

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
            + ".page-header, .page-header-space {height: 210px; }"
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
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY TURN -IN SLIP</h3></br></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 70px;'>";
            //terms and conditions //signatories
            html += "<table border='1'; class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%' valign='top'><br/>Received by :<br/><br/><br/><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + daterequested + "</br>Date</center></td>" +
            "<td>I CERTIFY that the articles listed on are turned-in under the circumtances indicated in 'REMARKS'<br/><br/><br/><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</center><center>Name of Personnel returning equipment</center><br/><br/><br/>Noted by:<br/><br/>___________________________________&emsp;_____________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Date<br/>Supply Accountable Officer Supply Accountable Group</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='3';></br>TO:&emsp;" + CustodianName + "</br>" + CustodianPosition + "" + CustodianDepartment + "</br></br></td><td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' width='10%';> Page of Pages </td><td width='10%'; style='text-align: right; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;' rowspan='2'; colspan='3';>FROM:&emsp;" + employeename + "</br>" + EmployeePosition + "," + EmployeeDepartment + "</td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Voucher No. </td><td style='text-align: right; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
                + "<tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> Turn-in Slip No. </td><td style='text-align: left; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>" + issuanceno + "</b></td></tr>"
                + "<tr><td colspan='5'  style='text-align: center; border-left:1px solid;border-right:1px solid;'>This is to acknowledge receipt of the returned Property/Equipment</td></tr>"
                + "<tr>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
                + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                    + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> 1" + "" + "</td>"
                    + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                    + "<td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ReturnRequestNo"].ToString() + " . " + dt.Rows[i]["Description"].ToString() + " </td>"
                    + "<td colspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + Remarks + "</td>"
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
        string returnrequestnumber = txtReturnRequestNumber.Text;
        if (txtRemarks.Value != "")
        {
            UploadReturnRequest(returnrequestnumber, txtRemarks.Value);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccessW();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:inputremarks();", true);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRemarks.Value != "")
            {
                string returnrequestnumber = txtReturnRequestNumber.Text;
                if (AFupload.HasFiles)
                {
                    string rootPath = Server.MapPath("~/SupportingDocuments/PropertyReturnRequest/" + returnrequestnumber);
                    string pathFinal = rootPath + '/' + returnrequestnumber + '/';
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
                        UploadReturnRequest(returnrequestnumber, txtRemarks.Value);
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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:inputremarks();", true);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    #endregion methods

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