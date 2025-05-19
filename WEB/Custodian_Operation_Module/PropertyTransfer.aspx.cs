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

public partial class Operation_Module_PropertyTransfer : BasePage
{
    string retSite = "";
    string retLoc = "";
    string retArea = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        retSite = shSystemConfigurationsSite.Value;
        lblDisplayFromSite.Text = "From " + retSite + ":";
        lblDisplayToSite.Text = "To " + retSite + ":";

        SystemConfigurations shSystemConfigurationsLocation = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        retLoc = shSystemConfigurationsLocation.Value;
        lblDisplayFromLoc.Text = "From " + retLoc + ":";
        lblDisplayToLoc.Text = "To " + retLoc + ":";

        SystemConfigurations shSystemConfigurationsArea = (SystemConfigurations)Session["shSystemConfigurationsArea"];
        retArea = shSystemConfigurationsArea.Value;
        lblDisplayFromArea.Text = "From " + retArea + ":";
        lblDisplayToArea.Text = "To " + retArea + ":";

        CheckAccess();
        GetApprover();
        GetItem();
    }

    #region metadata
    public void GetItem()
    {
        ddlItem.Items.Clear();
        ddlItem.Items.Add("--- Select Property Type Cost ---");
        ddlItem.Items.Add("ICS LOW - " + " Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and below.");
        ddlItem.Items.Add("ICS HIGH - " + " Between above Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
        ddlItem.Items.Add("PAR - " + " Above Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
    }


    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/PropertyTransfer.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static List<ListItem> GetWarehouseByID()
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        WarehouseEntity warehouseEntity = new WarehouseEntity();

        warehouseEntity.WarehouseID = Convert.ToInt32(Common.SiteID);
        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.GetWarehouseByID(warehouseEntity);
    }

    [WebMethod]
    public static string GetWarehousePT()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetLocationListPT(int warehouse)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.WarehouseID = warehouse;
        locationEntity.ConnectionString = Common.ConnString;

        DataTable dt = locationBll.GetLocationListBySitePT(locationEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetOfficeListsPT(int location)
    {
        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.LocationID = location;
        areaEntity.ConnectionString = Common.ConnString;

        DataTable dt = areaBll.GetAreaListByLocationPT(areaEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    //To

    [WebMethod]
    public static string GetWarehouseToPT()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetLocationListToPT(int warehouse)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.WarehouseID = warehouse;
        locationEntity.ConnectionString = Common.ConnString;

        DataTable dt = locationBll.GetLocationListBySiteTo(locationEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetOfficeListsToPT(int location)
    {
        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.LocationID = location;
        areaEntity.ConnectionString = Common.ConnString;

        DataTable dt = areaBll.GetAreaListByLocationTo(areaEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    //end
    [WebMethod]
    public static string GetCustodianID()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.UserName = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetCustodianID(userEntity);
    }

    [WebMethod]
    public static List<ListItem> GetLocationListLT(int warehouse)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.WarehouseID = warehouse;
        locationEntity.ConnectionString = Common.ConnString;

        return locationBll.GetLocationListBySite(locationEntity);
    }

    [WebMethod]
    public static List<ListItem> GetToLocationList(int warehouse)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.WarehouseID = warehouse;
        locationEntity.ConnectionString = Common.ConnString;

        return locationBll.GetLocationListBySite(locationEntity);
    }

    [WebMethod]
    public static List<ListItem> GetToOfficeLists(int location)
    {
        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.LocationID = location;
        areaEntity.ConnectionString = Common.ConnString;

        return areaBll.GetAreaListByLocation(areaEntity);
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
            PropertyEntity x = new PropertyEntity();
            PropertyTransferBll y = new PropertyTransferBll();
            x.TransferNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, issuanceno);
            string approvername = transferdetails.Split('|')[1];
            string approveremail = transferdetails.Split('|')[2];
            string transactorname = transferdetails.Split('|')[3];
            string transactoremail = transferdetails.Split('|')[4];
            string custodian = transferdetails.Split('|')[5];
            string remarks = transferdetails.Split('|')[6];
            string createddate = transferdetails.Split('|')[7];
            string fromsitename = transferdetails.Split('|')[8];
            string fromlocationname = transferdetails.Split('|')[9];
            string fromareaname = transferdetails.Split('|')[10];
            string tositename = transferdetails.Split('|')[11];
            string tolocationname = transferdetails.Split('|')[12];
            string toareaname = transferdetails.Split('|')[13];
            string transactordep = transferdetails.Split('|')[14];
            string transactorpos = transferdetails.Split('|')[15];
            string approverdep = transferdetails.Split('|')[16];
            string approverpos = transferdetails.Split('|')[17];
            string issueddate = transferdetails.Split('|')[18];
            string empname = transferdetails.Split('|')[19];
            string emppos = transferdetails.Split('|')[20];
            DataTable dt = y.PLPendingApproval(Common.ConnString, issuanceno);
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
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><br/></td></tr>";
            html += "<tr><td><table style='font-size:75%;';>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;' width='23%';></td><td style='font-size:9.5px; font-family: Arial;' width='35%';>Approved by</td><td style='font-size:9.5px; font-family: Arial;' width='28%';>Released/Issued by:</td><td style='font-size:9.5px; font-family: Arial;' width='26%';>Received by:</td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Signature :</td><td style='font-size:9.5px; font-family: Arial;'><div style='width: 50%; height: 75px;' ></div><br /></td><td style='font-size:9.5px; font-family: Arial;'><div style='width: 50%; height: 75px;' ></div><br /></td><td style='font-size:9.5px; font-family: Arial;'><div style='width: 50%; height: 75px;' ></div><br /></td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Printed Name :</td><td style='font-size:9.5px; font-family: Arial;'>" + approvername + "</td><td style='font-size:9.5px; font-family: Arial;'>" + transactorname + "</td><td style='font-size:9.5px; font-family: Arial;'>" + empname + "</td></tr>";
            html += "<tr><td style='font-size:9.5px; font-family: Arial;'>Designation :</td><td ></td><td style='font-size:9.5px; font-family: Arial;'>" + transactorpos + "</td><td style='font-size:9.5px; font-family: Arial;'>" + emppos + "</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + approvername + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
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
            PropertyTransferBll y = new PropertyTransferBll();

            x.TransferNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, issuanceno);

            string approvername = transferdetails.Split('|')[1];
            string approveremail = transferdetails.Split('|')[2];
            string transactorname = transferdetails.Split('|')[3];
            string transactoremail = transferdetails.Split('|')[4];
            string custodian = transferdetails.Split('|')[5];
            string remarks = transferdetails.Split('|')[6];
            string createddate = transferdetails.Split('|')[7];
            string fromsitename = transferdetails.Split('|')[8];
            string fromlocationname = transferdetails.Split('|')[9];
            string fromareaname = transferdetails.Split('|')[10];
            string tositename = transferdetails.Split('|')[11];
            string tolocationname = transferdetails.Split('|')[12];
            string toareaname = transferdetails.Split('|')[13];
            string transactordep = transferdetails.Split('|')[14];
            string transactorpos = transferdetails.Split('|')[15];
            string approverdep = transferdetails.Split('|')[16];
            string approverpos = transferdetails.Split('|')[17];
            string issueddate = transferdetails.Split('|')[18];

            DataTable dt = y.PLPendingApproval(Common.ConnString, issuanceno);

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
            html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
            html += "<tr><td>Signature :</td><td><img src='" + itrapprovedimagebase64 + "' width='50%' height='75' /><br />" + (itrapproveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreleaseimagebase64 + "' width='50%' height='75' /><br />" + (itrreleaseddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td><td><img src='" + itrreceivedimagebase64 + "' width='50%' height='75' /><br />" + (itrreceiveddatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "</td></tr>";
            html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + approvername + "</td></tr>";
            html += "<tr><td>Designation :</td><td></td><td>" + transactorpos + "</td><td>" + approverpos + "</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + approvername + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
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
    public static List<ListItem> GetUserNoDepartment()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.SiteID = Convert.ToInt32(Convert.ToInt32(Common.SiteID));
        userEntity.UserID = Convert.ToInt32(Convert.ToInt32(Common.UserID));
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetUserNoDepartment(userEntity);
    }

    #endregion metadata

    #region Datatables

    [WebMethod]
    public static string GetList(int site, int location, int area, int tosite, string search, int custodianid, int employeeID, int TrasferType, int ExpenseType)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        if (TrasferType == 1)
        {
            propertyEntity.EmployeeID = employeeID;
        }
        else if (TrasferType == 2)
        {
            propertyEntity.SiteID = site;
            propertyEntity.LocationID = location;
            propertyEntity.AreaID = area;
        }

        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        if (site == tosite || TrasferType == 1)
        {
            try
            {
                DataTable dt = propertytransferBll.GetAvailablePropertyForTransfer(propertyEntity, search, custodianid, ExpenseType);
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
                            x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' pmlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Transfer List' type='button'></a> </center>";
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
        else
        {
            try
            {
                DataTable dt = propertytransferBll.GetAvailablePropertyForTransferNotIssued(propertyEntity, search, custodianid);
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

                        x.Action = " <center><a class='btn btn-xs btn-success fa fa-plus btn_Add' data-toggle='tooltip' pmlid='" + dt.Rows[i]["RecID"].ToString() + "'  title='Add To Transfer List' type='button' ></a> </center>";

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

    [WebMethod]
    public static string GetToTransferAssetList(int fromsite, int fromlocation, int fromarea, int tosite, int tolocation, int toarea, int custodian, int FromEmployeeID, int ToEmployeeID, int TransferType, int ExpenseType)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.SiteID = fromsite;
        propertyEntity.LocationID = fromlocation;
        propertyEntity.AreaID = fromarea;
        propertyEntity.ToSiteID = tosite;
        propertyEntity.ToLocationID = tolocation;
        propertyEntity.ToAreaID = toarea;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.CustodianID = custodian;

        propertyEntity.FromEmployeeID = FromEmployeeID;
        propertyEntity.ToEmployeeID = ToEmployeeID;
        propertyEntity.TransferType = TransferType;
        propertyEntity.ExpenseType = ExpenseType;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertytransferBll.GetToTransferAssetList(propertyEntity);
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

                    x.Action = " <center><button class='btn btn-xs btn-danger btn_Remove fa fa-close' pmlid='" + dt.Rows[i]["PMLID"].ToString() + "' data-toggle='tooltip'  title='Remove from List' type='button' ></button></center>";

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

    //Pending Upload Transactions
    [WebMethod]
    public static string PropertyTransferPendingApproval(string search, int custodian)
    {
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertytransferBll.PropertyTransferPendingApproval(search, Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.TransferNumber = dt.Rows[i]["TransferNumber"].ToString();
                    x.RemarksTransfer = dt.Rows[i]["Remarks"].ToString();
                    x.StringNoOfItem = dt.Rows[i]["Quantity"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.DateOfTransfer = dt.Rows[i]["CreatedDate"].ToString();

                    string action = "<center><button type = 'button' class='btn btn-xs btn-primary  btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["TransferNumber"] + "','PropertyTransfer')\"><span class='fa fa-paperclip'></span></button>";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["TransferNumber"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "','" + dt.Rows[i]["TransactionType"] + "','" + dt.Rows[i]["PARICS"] + "')\"><span class='fa fa-eye'></span></button>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelTransaction('" + dt.Rows[i]["TransferNumber"] + "')\" ><span class='fa fa-times-circle'></span></button></center>";
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

                    x.TransferType = int.Parse(dt.Rows[i]["TransactionType"].ToString());

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

    private static string CompanyName = "";
    private static string CompanyContactNumber = "";
    private static string CompanyAddress = "";
    private static string CompanyLogo = "";

    [WebMethod]
    public static string GenerateGatePass(string transferno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyTransferBll y = new PropertyTransferBll();

        x.TransferNumber = transferno;
        x.ConnectionString = Common.ConnString;

        string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, transferno);

        string approvername = transferdetails.Split('|')[1];
        string approveremail = transferdetails.Split('|')[2];
        string transactorname = transferdetails.Split('|')[3];
        string transactoremail = transferdetails.Split('|')[4];
        string custodian = transferdetails.Split('|')[5];
        string remarks = transferdetails.Split('|')[6];
        string createddate = transferdetails.Split('|')[7];
        string fromsitename = transferdetails.Split('|')[8];
        string fromlocationname = transferdetails.Split('|')[9];
        string fromareaname = transferdetails.Split('|')[10];
        string tositename = transferdetails.Split('|')[11];
        string tolocationname = transferdetails.Split('|')[12];
        string toareaname = transferdetails.Split('|')[13];

        DataTable dt = y.PLPendingApproval(Common.ConnString, transferno);

        return GenerateGatePassForm(dt, transferno, transactorname, approvername, remarks, createddate);
    }

    public static string GenerateGatePassForm(DataTable dt, string transferno, string accountablename, string approvername, string remarks, string createddate)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];
            CompanyLogo = x.Split('|')[6];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 200px; }"
            + ".page-footer, .page-footer-space {height: 150px;}"
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

            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
             + "<tr><td align='center' style='font-size:15;'><br><b>G A T E   P A S S <b></td</tr>"
             + "<tr><td align='right' style='font-size:14;'>No. <u>" + transferno + "</u></td</tr><tr><td align='right' style='font-size:14;'>Date. <u>" + createddate.Split(' ')[0] + "</u></td</tr></table>";
            html += "<table  style='font-size:90%; font-family:Arial;' width='100%'><tr><td>TO: GUARD ON DUTY</td><td align='right'></td></tr>";
            html += "<tr><td>&emsp;Please allow  to bring inside/outside the Company premises the following items<br></td></tr>";
            html += "</table>";

            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //purpose

            html += "<p>Purpose : " + remarks + "</p>";

            //signatories
            html += "<table style='border-collapse:collapse; font-size:90%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='border-right: 1px solid black; border-bottom: 1px solid black;' align='center'>REQUESTED BY</td><td style='border-right: 1px solid black; border-bottom: 1px solid black;' align='center'>APPROVED BY</td><td style='border-bottom: 1px solid black;' align='center'>CHECKED BY</td></tr>";
            html += "<tr><td style='border-right: 1px solid black;' align='center'><br><br><br> " + accountablename + "</td><td style='border-right: 1px solid black;' align='center'><br><br><br>" + approvername + "</td><td align='center'><br><br><br>GUARD ON DUTY</td></tr>";
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
            html += "<tbody><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td><center><b>QTY</b><center></td>"
                      + "<td><center><b>Unit</b><center></td>"
                      + "<td><center><b>Description</b><center></td>"
                      + "<td><center><b>Property #</b></center></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td ><center>1</center></td>"
                          + "<td ><center>" + dt.Rows[i]["UOMcode"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["PropertyNumber"].ToString() + "</center></td>"
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
    public static string GenerateTransferReportITR(string transferno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyTransferBll y = new PropertyTransferBll();

        x.TransferNumber = transferno;
        x.ConnectionString = Common.ConnString;

        string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, transferno);

        string approvername = transferdetails.Split('|')[1];
        string approveremail = transferdetails.Split('|')[2];
        string transactorname = transferdetails.Split('|')[3];
        string transactoremail = transferdetails.Split('|')[4];
        string custodian = transferdetails.Split('|')[5];
        string remarks = transferdetails.Split('|')[6];
        string createddate = transferdetails.Split('|')[7];
        string fromsitename = transferdetails.Split('|')[8];
        string fromlocationname = transferdetails.Split('|')[9];
        string fromareaname = transferdetails.Split('|')[10];
        string tositename = transferdetails.Split('|')[11];
        string tolocationname = transferdetails.Split('|')[12];
        string toareaname = transferdetails.Split('|')[13];
        string transactordep = transferdetails.Split('|')[14];
        string transactorpos = transferdetails.Split('|')[15];
        string approverdep = transferdetails.Split('|')[16];
        string approverpos = transferdetails.Split('|')[17];
        string issueddate = transferdetails.Split('|')[18];
        string employeename = transferdetails.Split('|')[19];
        string employeepos = transferdetails.Split('|')[20];

        string fromEmployeeName = transferdetails.Split('|')[21];
        string trxType = transferdetails.Split('|')[22];
    

        string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
        string toloc = tositename + " - " + tolocationname + " - " + toareaname;

        if (trxType == "1")
        {
            fromloc = fromEmployeeName;
            toloc = employeename;
        }
        else
        {
            fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
            toloc = tositename + " - " + tolocationname + " - " + toareaname;
        }




        DataTable dt = y.PLPendingApproval(Common.ConnString, transferno);

        return FormsAndReports.GenerateTransferFormITR(dt, transferno, employeename, approverdep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, fromloc, toloc);
    }

    [WebMethod]
    public static string GenerateTransferReportPTR(string transferno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyTransferBll y = new PropertyTransferBll();

        string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, transferno);

        string approvername = transferdetails.Split('|')[1];
        string approveremail = transferdetails.Split('|')[2];
        string transactorname = transferdetails.Split('|')[3];
        string transactoremail = transferdetails.Split('|')[4];
        string custodian = transferdetails.Split('|')[5];
        string remarks = transferdetails.Split('|')[6];
        string createddate = transferdetails.Split('|')[7];
        string fromsitename = transferdetails.Split('|')[8];
        string fromlocationname = transferdetails.Split('|')[9];
        string fromareaname = transferdetails.Split('|')[10];
        string tositename = transferdetails.Split('|')[11];
        string tolocationname = transferdetails.Split('|')[12];
        string toareaname = transferdetails.Split('|')[13];
        string transactordep = transferdetails.Split('|')[14];
        string transactorpos = transferdetails.Split('|')[15];
        string approverdep = transferdetails.Split('|')[16];
        string approverpos = transferdetails.Split('|')[17];
        string issueddate = transferdetails.Split('|')[18];
        string employeename = transferdetails.Split('|')[19];
        string employeepos = transferdetails.Split('|')[20];
        string employeedep = transferdetails.Split('|')[23];
        string employeefromname = transferdetails.Split('|')[21];

        string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
        string toloc = tositename + " - " + tolocationname + " - " + toareaname;
        DataTable dt = y.PLPendingApproval(Common.ConnString, transferno);

        return GenerateTransferFormPTR(dt, transferno, employeename, approverdep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, fromloc, toloc, employeefromname, employeedep);
    }

    public static string GenerateTransferFormPTR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string fromloc, string toloc, string employeefromname, string employeedep)
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
            #endregion
            #region div header and footer
            #region header
            //header div
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
                 + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
            //<img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>PROPERTY TRANSFER REPORT<h3></td</tr>";
            // html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'></td></tr></table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>" +remarks+ "</center><br/></td></tr>";
            //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
            //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
            //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
            //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
            //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
            //    + "</td></tr>";
            html += "<tr><td><table style='font-size:75%;';>";
            html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
            html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
            html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
            html += "<tr><td>Designation :</td><td>" + employeedep + "</td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
            html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
            html += "</table></table>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + employeefromname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>PTR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
                     + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
                      + "<tr>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
                      + "<td width='20%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
                      //+ "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
                      + "<td width='35%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
                      + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
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

    #endregion Datatables

    #region Methods

    [WebMethod]
    public static string IfTransferNeedsApproval()
    {
        return Common.TransferApproval;
    }

    [WebMethod]
    public static string CheckTransactionType(string propertyNumber)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.PropertyNumber = propertyNumber;
        propertyEntity.ConnectionString = Common.ConnString;

        return "";
    }

    [WebMethod]
    public static string TransferToTempTable(int pmlid, int fromsite, int fromlocation, int fromarea, int tosite, int tolocation, int toarea, int custodian, int transferType, int employeeIDFrom, int employeeIDTo, int ExpenseType)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.RecID = pmlid;
        propertyEntity.SiteID = fromsite;
        propertyEntity.LocationID = fromlocation;
        propertyEntity.AreaID = fromarea;
        propertyEntity.ToSiteID = tosite;
        propertyEntity.ToLocationID = tolocation;
        propertyEntity.ToAreaID = toarea;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.CustodianID = custodian;
        propertyEntity.TransferType = transferType;
        propertyEntity.FromEmployeeID = employeeIDFrom;
        propertyEntity.ToEmployeeID = employeeIDTo;

        propertytransferBll.TransferToLocationTempTable(propertyEntity, ExpenseType);

        return "";
    }

    [WebMethod]
    public static string RemoveToTransfer(int pmlid, int trasferType)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.RecID = pmlid;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.TransferType = trasferType;

        propertytransferBll.RemoveToTransfer(propertyEntity);

        return "";
    }

    [WebMethod]
    public static string RemoveAllToTransfer(int fromsite, int fromlocation, int fromarea, int tosite, int tolocation, int toarea, int custodian, int trasferType, int employeeIDFrom, int employeeIDTo, int Expensetype)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.SiteID = fromsite;
        propertyEntity.LocationID = fromlocation;
        propertyEntity.AreaID = fromarea;
        propertyEntity.ToSiteID = tosite;
        propertyEntity.ToLocationID = tolocation;
        propertyEntity.ToAreaID = toarea;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.CustodianID = custodian;
        propertyEntity.TransferType = trasferType;
        propertyEntity.ToEmployeeID = employeeIDTo;
        propertyEntity.FromEmployeeID = employeeIDFrom;
        propertyEntity.ExpenseType = Expensetype;

        propertytransferBll.RemoveAllToTransfer(propertyEntity);

        return "";
    }

    [WebMethod]
    public static string ConfirmPropertyTransfer(int approverid, string purpose, int fromsite, int fromlocation, int fromarea, int tosite, int tolocation, int toarea, int custodianid, int FromEmployeeID, int ToEmployeeID, int TransferType, int ExpenseType)
    {
        try
        {
            PropertyEntity propertyEntity = new PropertyEntity();
            PropertyTransferBll propertytransferBll = new PropertyTransferBll();

            propertyEntity.SiteID = fromsite;
            propertyEntity.LocationID = fromlocation;
            propertyEntity.AreaID = fromarea;
            propertyEntity.ToSiteID = tosite;
            propertyEntity.ToLocationID = tolocation;
            propertyEntity.ToAreaID = toarea;
            propertyEntity.CreatedBy = Common.UserName;
            propertyEntity.Remarks = purpose;
            propertyEntity.ApproverID = approverid;
            propertyEntity.CustodianID = custodianid;
            propertyEntity.ConnectionString = Common.ConnString;

            propertyEntity.FromEmployeeID = FromEmployeeID;
            propertyEntity.ToEmployeeID = ToEmployeeID;
            propertyEntity.TransferType = TransferType;
            propertyEntity.ExpenseType = ExpenseType;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            string transferno = propertytransferBll.ConfirmPropertyTransfer(propertyEntity);

            DataTable dt = propertytransferBll.PLPendingApproval(Common.ConnString, transferno);

            string transferdetails = propertytransferBll.PropertyTransferDetails(Common.ConnString, transferno);
            string approvername = transferdetails.Split('|')[1];
            string approveremail = transferdetails.Split('|')[2];
            string transactorname = transferdetails.Split('|')[3];
            string transactoremail = transferdetails.Split('|')[4];
            string custodian = transferdetails.Split('|')[5];
            string remarks = transferdetails.Split('|')[6];
            string createddate = transferdetails.Split('|')[7];

            if (approveremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(approveremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Property Transfer)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
                if (TransferType == 1) //if person transfer type
                {
                    Body += "A PROPERTY TRANSFER transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                   + "Transfer No : " + transferno + "<br>"
                   + "Remarks : " + remarks + " <br>"
                   + "Processed By : " + transactorname + " <br>"
                   + "Processed Date : " + createddate + "<br>"
                   + "Source : " + dt.Rows[0]["FromEmployeeName"].ToString() + "<br>"
                   + "Destination : " + dt.Rows[0]["ToEmployeeName"].ToString() + "<br><br>";
                }
                else
                {
                    Body += "A PROPERTY TRANSFER transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                        + "Transfer No : " + transferno + "<br>"
                        + "Remarks : " + remarks + " <br>"
                        + "Processed By : " + transactorname + " <br>"
                        + "Processed Date : " + createddate + "<br>"
                        + "Source : " + dt.Rows[0]["FromSiteName"].ToString() + " - " + dt.Rows[0]["FromLocationName"].ToString() + " - " + dt.Rows[0]["FromAreaName"].ToString() + "<br>"
                        + "Destination : " + dt.Rows[0]["ToSiteName"].ToString() + " - " + dt.Rows[0]["ToLocationName"].ToString() + " - " + dt.Rows[0]["ToAreaName"].ToString() + "<br><br>";
                }
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
    public static string PropertyTransferGetCount(int fromsite, int fromlocation, int fromarea, int tosite, int tolocation, int toarea, int custodian, int FromEmployeeID, int ToEmployeeID, int TransferType, int ExpenseType)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.SiteID = fromsite;
        propertyEntity.LocationID = fromlocation;
        propertyEntity.AreaID = fromarea;
        propertyEntity.ToSiteID = tosite;
        propertyEntity.ToLocationID = tolocation;
        propertyEntity.ToAreaID = toarea;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.CustodianID = custodian;

        propertyEntity.FromEmployeeID = FromEmployeeID;
        propertyEntity.ToEmployeeID = ToEmployeeID;
        propertyEntity.TransferType = TransferType;
        propertyEntity.ExpenseType = ExpenseType;

        return propertytransferBll.PropertyTransferGetCount(propertyEntity);
    }

    [WebMethod]
    public static string CancelPropertyTransfer(string transactionno, string remarks)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.TransferNumber = transactionno;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertyEntity.Remarks = remarks;

        propertytransferBll.CancelPropertyTransfer(propertyEntity);

        return "";
    }

    [WebMethod]
    public static void UploadPropertyTransfer(string transactionno)
    {
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        propertyEntity.TransferNumber = transactionno;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;
        propertytransferBll.UploadPropertyTransfer(propertyEntity);
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        string transno = txtTransactionNo.Text;
        UploadPropertyTransfer(transno);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccessW();", true);
    }

    public bool success = false;

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string transactionno = txtTransactionNo.Text;

            if (AFupload.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/PropertyTransfer/" + transactionno + "/");
                string pathFinal = rootPath + '/' + transactionno + '/';
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
                    UploadPropertyTransfer(transactionno);
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

    [WebMethod]
    public static string GenerateIssuanceReportPAR(string transferno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyTransferBll y = new PropertyTransferBll();

        string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, transferno);

        string approvername = transferdetails.Split('|')[1];
        string approveremail = transferdetails.Split('|')[2];
        string transactorname = transferdetails.Split('|')[3];
        string transactoremail = transferdetails.Split('|')[4];
        string custodian = transferdetails.Split('|')[5];
        string remarks = transferdetails.Split('|')[6];
        string createddate = transferdetails.Split('|')[7];
        string fromsitename = transferdetails.Split('|')[8];
        string fromlocationname = transferdetails.Split('|')[9];
        string fromareaname = transferdetails.Split('|')[10];
        string tositename = transferdetails.Split('|')[11];
        string tolocationname = transferdetails.Split('|')[12];
        string toareaname = transferdetails.Split('|')[13];
        string transactordep = transferdetails.Split('|')[14];
        string transactorpos = transferdetails.Split('|')[15];
        string approverdep = transferdetails.Split('|')[16];
        string approverpos = transferdetails.Split('|')[17];
        string issueddate = transferdetails.Split('|')[18];
        string employeename = transferdetails.Split('|')[19];
        string employeepos = transferdetails.Split('|')[20];
        string transactiontype = transferdetails.Split('|')[22];
        string employeedep = "";

        string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
        string toloc = tositename + " - " + tolocationname + " - " + toareaname;
        DataTable dt;
        if (transactiontype == "1")
        {
            dt = y.PLPendingApprovalWithComponent(Common.ConnString, transferno);
        }
        else
        {
            dt = y.PLPendingApproval(Common.ConnString, transferno);
        }
        return FormsAndReports.GeneratePropertyTransferFormPAR(dt, transferno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
    }

    [WebMethod]
    public static string GenerateIssuanceReportICS(string transferno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyTransferBll y = new PropertyTransferBll();

        string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, transferno);

        string approvername = transferdetails.Split('|')[1];
        string approveremail = transferdetails.Split('|')[2];
        string transactorname = transferdetails.Split('|')[3];
        string transactoremail = transferdetails.Split('|')[4];
        string custodian = transferdetails.Split('|')[5];
        string remarks = transferdetails.Split('|')[6];
        string createddate = transferdetails.Split('|')[7];
        string fromsitename = transferdetails.Split('|')[8];
        string fromlocationname = transferdetails.Split('|')[9];
        string fromareaname = transferdetails.Split('|')[10];
        string tositename = transferdetails.Split('|')[11];
        string tolocationname = transferdetails.Split('|')[12];
        string toareaname = transferdetails.Split('|')[13];
        string transactordep = transferdetails.Split('|')[14];
        string transactorpos = transferdetails.Split('|')[15];
        string approverdep = transferdetails.Split('|')[16];
        string approverpos = transferdetails.Split('|')[17];
        string issueddate = transferdetails.Split('|')[18];
        string employeename = transferdetails.Split('|')[19];
        string employeepos = transferdetails.Split('|')[20];
        string employeedep = transferdetails.Split('|')[23];
        string transactiontype = transferdetails.Split('|')[22];

        string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
        string toloc = tositename + " - " + tolocationname + " - " + toareaname;

        DataTable dt;
        if (transactiontype == "1")
        {
            dt = y.PLPendingApprovalWithComponent(Common.ConnString, transferno);
        }
        else
        {
            dt = y.PLPendingApproval(Common.ConnString, transferno);
        }
        
        return FormsAndReports.GeneratePropertyTransferFormICS(dt, transferno, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
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

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        smtp.Send(mail);

        string dddreturn = "okay";
    }
}