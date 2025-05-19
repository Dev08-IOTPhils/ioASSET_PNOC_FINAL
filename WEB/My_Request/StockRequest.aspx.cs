using AdvanceBll;
using AdvanceEntity;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Operation_Module_StockRequest : BasePage
{
    public bool ShowStockRemaining = false;

    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        ret = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = ret.ToUpper() + "/DEPARTMENT";
        CheckAccess();
        GetCustodian();
        GetUserDetails();
    }

    #region metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("My_Request/StockRequest.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
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
            + ".page-header, .page-header-space {height: 240px; }"
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
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td><td style='text-align: center;'><img src='" + approvedimagebase64 + "' width='50%' height='75' /><br />" + (approveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + approvername + "</td><td style='text-align: center;'><img src='" + issuedimagebase64 + "' width='50%' height='75' /><br />" + (issueddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td><td style='text-align: center;'><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeepos + "</td><td></td><td>" + employeepos + "</td><td style='text-align: center;'>" + employeepos + "</td></tr>";
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
                     + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Supply Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Supply No.</b></td>"
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
                          //dothis
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

    public void GetUserDetails()
    {
        try
        {
            if (Common.StockRemainingQuantity == "True") ShowStockRemaining = true;
        }
        catch { }
    }

    [WebMethod]
    public static List<ListItem> GetApprover()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Common.SiteID);
        userEntity.UserID = Convert.ToInt32(Common.UserID);
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetApproverInIssuance(userEntity, 0);
    }

    [WebMethod]
    public static string IfIssuanceNeedsApproval()
    {
        return Common.StockIssuanceApproval;
    }

    [WebMethod]
    public static string CheckStockRemainingQuantity()
    {
        return Common.StockRemainingQuantity;
    }

    public void GetCustodian()
    {
        CustodianEntity custodianEntity = new CustodianEntity();
        CustodianBll custodianBll = new CustodianBll();

        DataTable dt = custodianBll.GetCustodianList(Common.ConnString);

        ddlCustodian.DataSource = dt;
        ddlCustodian.DataValueField = "CustodianID";
        ddlCustodian.DataTextField = "Custodian";
        ddlCustodian.DataBind();
        ddlCustodian.Items.Insert(0, "--- Select Custodian Type ---");
    }

    #endregion metadata

    #region datatables

    [WebMethod]
    public static string GetList(string search, int custodian)
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailableStockForRequest(Common.ConnString, search, custodian, Convert.ToInt32(Common.SiteID), Common.Administrative);
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
                        x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' smlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip' nero='" + dt.Rows[i]["Quantity"].ToString() + "'  title='Add To Request List'></a> </center>";
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
    public static string GetStockRequestList(int custodian)
    {
        StockRequestBll y = new StockRequestBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetStockRequestList(Common.ConnString, custodian, Common.UserName);
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

                    x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' smlid='" + dt.Rows[i]["SMLID"].ToString() + "' data-toggle='tooltip'  title='Remove To Request List'><span class='fa fa-close'></span></a> </center>";
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
            DataTable dt = y.StockRequestPendingUpload(search, Common.ConnString, Common.UserName, Common.Administrative, Common.CustodianID);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.RequestNo = dt.Rows[i]["RequestNo"].ToString();
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
                        action += " <center><button class='btn btn-xs btn-primary btn_View'  type='button' onclick=\"ViewStocks('" + dt.Rows[i]["RequestNo"] + "')\"><span class='fa fa-eye'></span></button> ";
                    }
                    //if (DeleteAccess == "0")
                    //{
                    //    action += "";
                    //}
                    //else
                    //{
                    //    action += " <button class='btn btn-xs btn-danger btn_Cancel' type='button' onclick=\"CancelStockRequest('" + dt.Rows[i]["RequestNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
                    //}
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
    public static string StockDetailsPendingUpload(string requestno)
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

    #region Method

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

        //return FormsAndReports.GenerateStockIssuanceFormRISStockRequest(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site);
        return GenerateStockIssuanceRequestFormRIS(dt, requestno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos);
    }

    public static string GenerateStockIssuanceRequestFormRIS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site, string approverpos)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string y = miscBll.GetFormHeader(Common.ConnString);
            string header1 = y.Split('|')[0];
            string header2 = y.Split('|')[1];

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 180px; }"
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
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr ><td  width='20%' rowspan = '2' align='left' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  style='width: 100px; height: 100px' /></td><td style='font-size:15;font-weight:bold;'><br>" + header1 + "<br>" + header2 + "</td></tr>";
            //+ "<tr ><td>" + header2 + "</td></tr>";
            // + "<tr><td colspan='1' align='center'; ><center><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></center></td></tr>";
            html += "<tr><td  align='center' ><br><h3>Withdrawal Slip for Common-Used Office Supplies and Forms<h3></td</tr></table>";

            // html += "<table style='width:100%;font-family: Arial;'>"
            //html += "<table><tr><td width='75%' style='font-size:12;'>Entity Name : <u>" + header1 + "</u></td><td width='25%' style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header


            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + issueddate + "</u><br/>Control No: <u>" + issuanceno + "</u></td></tr>"
                     + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Supply Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
                      + "<tr>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + (i+1) + "</td>"
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
            #endregion


            #region footer placeholder
            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";
            #endregion
            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            //html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Approved for Issuance by:</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + "" + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td>" + approverpos + "</td><td>" + transactordepartment + "</td><td>" + "" + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + "" + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
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


    //
    [WebMethod]
    public static string checkQuantity(string itemCode)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.ItemCode = itemCode;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.checkQuantity(x);
    }

    [WebMethod]
    public static string StockRequestGetCount(int custodian)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRequestGetCount(x);
    }

    [WebMethod]
    public static string StockTransferToRequest(int smlid, int custodian, int quantity)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        x.CustodianID = custodian;
        x.Quantity = quantity;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockTransferToRequest(x).ToString();
    }

    [WebMethod]
    public static string StockRemoveToRequest(int smlid, int custodian)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRemoveToRequest(x).ToString();
    }

    [WebMethod]
    public static string StockRemoveAllToRequest(int custodian)
    {
        StockRequestBll y = new StockRequestBll();
        StockEntity x = new StockEntity();

        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRemoveAllToRequest(x).ToString();
    }

    [WebMethod]
    public static string ConfirmStockRequest(int custodian, int approver, string remarks)
    {
        try
        {
            StockRequestBll y = new StockRequestBll();
            StockEntity x = new StockEntity();

            x.CustodianID = custodian;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ApproverID = approver;
            x.ConnectionString = Common.ConnString;

            string returnresult = y.ConfirmStockRequest(x);
            return returnresult;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    #endregion Method

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