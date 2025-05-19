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

public partial class Operation_Module_PropertyGatePassRequest : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        GetApprover();
        GetRequested();
    }

    #region Metadata
    public void GetRequested()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetUserListBySite(Common.ConnString, Convert.ToInt32(Common.SiteID));

        ddlRequestedBy.DataSource = dt;
        ddlRequestedBy.DataValueField = "UserID";
        ddlRequestedBy.DataTextField = "FullName";
        ddlRequestedBy.DataBind();
        ddlRequestedBy.Items.Insert(0, "--- Select Requested By ---");
    }

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    [WebMethod]
    public static string CreateGatePassHTML(string issuanceno,
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
            PropertyGatePassBll y = new PropertyGatePassBll();
            x.GatePassNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string gatepassdetails = y.PropertyGatePassDetails(x);
            string accountablename = gatepassdetails.Split('|')[1];
            string approvername = gatepassdetails.Split('|')[2];
            string remarks = gatepassdetails.Split('|')[3];
            string createddate = gatepassdetails.Split('|')[4];
            DataTable dt = y.PropertyGatePassRequestList(Common.ConnString, issuanceno);
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();
            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 180px; }"
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
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td</tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>GATE PASS for Personal Property</h3></br></td</tr></table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 170px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td align='left'; style='height:20px;width:50%; border-bottom:1px solid;border-top:1px solid;border-left:1px solid;border-right:1px solid;'><b>REQUESTED BY :</b></td><td align='left'; style='height:20px;width:50%; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>APPROVED BY :</b></td></tr>";
            html += "<tr><td align='center'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/><br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><b><u>" + accountablename + "</u></b><br/>Designation</center></br/></td><td style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/><br/><br/><center><div style='width: 50%; height: 75px;' ></div><br /><b><u>" + approvername + "</u></b><br/>Chief, General Services Division</center></br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>DISTRIBUTION OF COPY:</b><br/>ORIGINAL- BEARER<br/>DUPLICATE- SECURITY<br/>DUPLICATE- SECURITY<br/>TRI- ASSET MANAGEMENT SECTION</td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Inspected by:<br/><div style='width: 50%; height: 75px;' ></div><br /><br/><br/><br/><br/><b>SECURITY PERSONNEL</b></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';></td><td style='width:50%;text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><b><i>DATE</i></b> " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                    + "<tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; colspan='5'>&nbsp;</td></tr>"
                    + "<tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; colspan='5'><b>This is to authorize the bearer of this gatepass to bring in/out to the office the following:</b></td></tr>"
                    + "</tr><tr>"
                      + "<td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ITEM NO.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>QTY.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>UNIT</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;' colspan='2';><b>DESCRIPTION</b><br/><i>(Kindly  include the brand, model, serial number if necessary and accessories included)</i></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>" + dt.Rows[i]["Description"].ToString() + "</td>"
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
    public static string CreateGatePass(string issuanceno,
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
            PropertyGatePassBll y = new PropertyGatePassBll();

            x.GatePassNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string gatepassdetails = y.PropertyGatePassDetails(x);
            string accountablename = gatepassdetails.Split('|')[1];
            string approvername = gatepassdetails.Split('|')[2];
            string remarks = gatepassdetails.Split('|')[3];
            string createddate = gatepassdetails.Split('|')[4];

            DataTable dt = y.PropertyGatePassRequestList(Common.ConnString, issuanceno);

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 180px; }"
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
            + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px'/></td</tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>GATE PASS for Personal Property</h3></br></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 130px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td align='left'; style='height:20px;width:50%; border-bottom:1px solid;border-top:1px solid;border-left:1px solid;border-right:1px solid;'><b>REQUESTED BY :</b></td><td align='left'; style='height:20px;width:50%; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>APPROVED BY :</b></td></tr>";
            html += "<tr><td align='center'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/><br/><br/><center><img src='" + requestedimagebase64 + "' width='50%' height='75' /><br />" + (requesteddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<b><u>" + accountablename + "</u></b><br/>Designation</center></br/></td><td style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/><br/><br/><center><img src='" + approvedimagebase64 + "' width='50%' height='75' /><br />" + (approveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<b><u>" + approvername + "</u></b><br/>Chief, General Services Division</center></br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>DISTRIBUTION OF COPY:</b><br/>ORIGINAL- BEARER<br/>DUPLICATE- SECURITY<br/>DUPLICATE- SECURITY<br/>TRI- ASSET MANAGEMENT SECTION</td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Inspected by:<br/><img src='" + issuedimagebase64 + "' width='50%' height='75' /><br />" + (issueddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<br/><br/><br/><br/><b>SECURITY PERSONNEL</b></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';></td><td style='width:50%;text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><b><i>DATE</i></b> " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                    + "<tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; colspan='5'>&nbsp;</td></tr>"
                    + "<tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; colspan='5'><b>This is to authorize the bearer of this gatepass to bring in/out to the office the following:</b></td></tr>"

                    + "</tr><tr>"
                      + "<td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ITEM NO.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>QTY.</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>UNIT</b></td>"
                      + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;' colspan='2';><b>DESCRIPTION</b><br/><i>(Kindly  include the brand, model, serial number if necessary and accessories included)</i></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>" + dt.Rows[i]["Description"].ToString() + "</td>"
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

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Operation_Module/PropertyGatePassRequest.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
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

    #endregion Metadata

    #region Datatables

    [WebMethod]
    public static string GetPropertyAvailableForGatePass(string search)
    {
        PropertyGatePassBll y = new PropertyGatePassBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyAvailableForGatePass(search, Common.ConnString, Convert.ToInt32(Common.UserID));
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
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success btn_Add' pmlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To gate Pass List'><span class='fa fa-plus'></span></a> </center>";
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
    public static string GetGatePassPropertyList()
    {
        PropertyGatePassBll y = new PropertyGatePassBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetGatePassPropertyList(Common.ConnString, Convert.ToInt32(Common.UserID));
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
                    if (DeleteAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' pmlid='" + dt.Rows[i]["PMLID"].ToString() + "' data-toggle='tooltip'  title='Remove To Gate Pass List'><span class='fa fa-close'></span></a> </center>";
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
    public static string GetPropertyGatePassPendingUpload(string search)
    {
        PropertyGatePassBll y = new PropertyGatePassBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyGatePassPendingUpload(search, Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.GatePassNumber = dt.Rows[i]["GatePassNo"].ToString();
                    x.NoOfItemReturn = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += "<center><button class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewList('" + dt.Rows[i]["GatePassNo"] + "')\"><span class='fa fa-eye'></span></button>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        if (dt.Rows[i]["Status"].ToString() == "Approved")
                        {
                            action += " <button class='btn btn-xs btn-danger btn_Cancel' disabled data-toggle='tooltip'  title='Cancel Transaction' onclick=\"Cancel('" + dt.Rows[i]["GatePassNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
                        }
                        else
                        {
                            action += " <button class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"Cancel('" + dt.Rows[i]["GatePassNo"] + "')\"><span class='fa fa-times-circle'></span></button></center>";
                        }
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

    #endregion Datatables

    #region Methods

    [WebMethod]
    public static string IfGatePassNeedsApproval()
    {
        return Common.GatePassApproval;
    }

    [WebMethod]
    public static string GetCountPropertyGatePassRequest()
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.GetCountPropertyGatePassRequest(x);
    }

    [WebMethod]
    public static string TransferToPropertyGatePassRequest(int pmlid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.RecID = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.TransferToPropertyGatePassRequest(x);

        return "";
    }

    [WebMethod]
    public static string RemoveToPropertyGatePassRequest(int pmlid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.RecID = pmlid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveToPropertyGatePassRequest(x);

        return "";
    }

    [WebMethod]
    public static string RemoveAllToPropertyGatePassRequest()
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveAllToPropertyGatePassRequest(x);

        return "";
    }

    [WebMethod]
    public static string ConfirmPropertyGatePassRequest(string remarks, int approverid, int requestedBy)
    {
        try
        {
            PropertyEntity x = new PropertyEntity();
            PropertyGatePassBll y = new PropertyGatePassBll();

            x.CreatedBy = Common.UserName;
            x.ApproverID = approverid;
            x.Remarks = remarks;
            x.ConnectionString = Common.ConnString;
            x.RequestedById = requestedBy;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string gatepassno = y.ConfirmPropertyGatePassRequest(x);
            x.GatePassNumber = gatepassno;

            DataTable dt = y.PropertyGatePassRequestList(Common.ConnString, gatepassno);

            string gatepassdetails = y.PropertyGatePassDetails(x);
            string accountablename = gatepassdetails.Split('|')[1];
            string approvername = gatepassdetails.Split('|')[2];
            string createddate = gatepassdetails.Split('|')[4];
            string accountableemail = gatepassdetails.Split('|')[5];
            string approveremail = gatepassdetails.Split('|')[6];

            if (approveremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(approveremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Gate Pass Request)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
                Body += "A PROPERTY GATE PASS REQUEST                                                                                                   transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                    + "Gate Pass No : " + gatepassno + "<br>"
                    + "Purpose/Remarks : " + remarks + " <br>"
                    + "Processed By : " + accountablename + " <br>"
                    + "Processed Date : " + createddate + "<br><br>";
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
    public static string CancelGatePassRequest(string gatepassno, string remarks)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.GatePassNumber = gatepassno;
        x.CreatedBy = Common.UserName;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;
        y.CancelGatePassRequest(x);

        return "";
    }

    [WebMethod]
    public static string GenerateGatePassReport(string gatepassno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.GatePassNumber = gatepassno;
        x.ConnectionString = Common.ConnString;

        string gatepassdetails = y.PropertyGatePassDetails(x);
        string accountablename = gatepassdetails.Split('|')[1];
        string approvername = gatepassdetails.Split('|')[2];
        string remarks = gatepassdetails.Split('|')[3];
        string createddate = gatepassdetails.Split('|')[4];

        DataTable dt = y.PropertyGatePassRequestList(Common.ConnString, gatepassno);

        return FormsAndReports.GenerateNewGatePassForm(dt, gatepassno, accountablename, approvername, remarks, createddate);
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