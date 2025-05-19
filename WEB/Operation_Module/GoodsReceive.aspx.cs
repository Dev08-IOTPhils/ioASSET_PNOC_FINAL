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
using ClosedXML.Excel;

public partial class Operation_Module_GoodsReceive : BasePage
{
    string retSite = "";
    string retLoc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        retSite = shSystemConfigurationsSite.Value;
        lbldisplaySite1.Text = retSite;


        SystemConfigurations shSystemConfigurationsLocation = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        retLoc = shSystemConfigurationsLocation.Value;
        lbldisplayLoc.Text = retLoc + ":";


        CheckAccess();
        GetCustodianList();
        GetEmployee();
    }

    #region METADATA

    [WebMethod]
    public static string CreateIARHTML(int issuanceno,
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
            PropertyBll propertyBll = new PropertyBll();
            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, issuanceno);
            string grdetails = propertyBll.GoodsReceiveDetails(Common.ConnString, issuanceno);
            string grnumber = grdetails.Split('|')[0];
            string transactorname = grdetails.Split('|')[1];
            string transactoremail = grdetails.Split('|')[2];
            string approvername = grdetails.Split('|')[3];
            string approveremail = grdetails.Split('|')[4];
            string custodianname = grdetails.Split('|')[5];
            string createddate = grdetails.Split('|')[6];
            string PONumber = grdetails.Split('|')[7];
            string suppliername = grdetails.Split('|')[8];
            string DRNumber = grdetails.Split('|')[9];

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();
            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];
            string invNo = "";
            if (dt.Rows.Count > 0)
            {
                invNo = dt.Rows[0]["InvoiceNumber"].ToString();
            }



            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 265px; }"
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
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>INSPECTION AND ACCEPTANCE REPORT<h3></td</tr>";
            //html += "<tr><td colspan='2' align='justify' style='font-size:12;'><br>Entity Name : " + CompanyName + "<br></td</tr></table>";
            html += "</div>";
            #endregion header
            #region footer
            //footer div
            html += "<div class='page-footer'  style='margin-left: -8px;margin-bottom: 90px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%';><center><i><b>INSPECTION</b></i></center></td><td width='50%';><center><i><b>ACCEPTANCE</b></i></center></td></tr>";
            html += "<tr><td width='50%';><b>Date Inspected : ________________________________ </b> <br/> <input type='checkbox'> Inspected, verified and found in order as to quantity and specifications </br> </br> </br> </br>  </br> <center><div style='width: 50%; height: 75px;' ></div><br /><br />__________________________________<br/>Inspection Officer/Inspection Committee</center></td>"
                + "<td width='50%';><b>Date Received : ________________________________ </b> <br/> <input type='checkbox'> Complete </br> <input type='checkbox'> Partial (pls. specify quantity) </br> </br> </br>  </br> <center><div style='width: 50%; height: 75px;' ></div><br /><br />__________________________________<br/>Supply and/or Property Custodian</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>Supplier : : " + suppliername + "<br/>PO No./Date : " + PONumber + "<br/>Requisitioning Office/Dept. : " + "" + "<br/></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>IAR No. : " + issuanceno + "<br/>Delivery No: " + createddate + "<br/>Invoice No. : " + invNo + "<br/>Delivery No : " + DRNumber + "<br/></td></tr>"
                      + "<tr>"
                      + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Item No.</i></b></td>"
                      + "<td width='35%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Description</i></b></td>"
                      + "<td width='15%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Unit</i></b></td>"
                      + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b><i>Quantity</i></b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + i+1 + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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
            return PDFConverter.CreatePDF(html, imagebase64, certbase64, passphrase, datestamp, grnumber, transtype, operationtype);
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
    public static string CreatePDFIAR(int issuanceno,
   string fromimagebase64,
   string byimagebase64,
   string frompassphrase,
   string bypassphrase,
   string fromcertbase64,
   string bycertbase64,
   bool fromdatestamp,
   bool bydatestamp
   )
    {
        try
        {
            PropertyBll propertyBll = new PropertyBll();
            List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, issuanceno);
            string grdetails = propertyBll.GoodsReceiveDetails(Common.ConnString, issuanceno);
            string grnumber = grdetails.Split('|')[0];
            string transactorname = grdetails.Split('|')[1];
            string transactoremail = grdetails.Split('|')[2];
            string approvername = grdetails.Split('|')[3];
            string approveremail = grdetails.Split('|')[4];
            string custodianname = grdetails.Split('|')[5];
            string createddate = grdetails.Split('|')[6];
            string PONumber = grdetails.Split('|')[7];
            string suppliername = grdetails.Split('|')[8];
            string DRNumber = grdetails.Split('|')[9];

            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string companyinfo = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = companyinfo.Split('|')[0];
            string CompanyContactNumber = companyinfo.Split('|')[3];
            string CompanyAddress = companyinfo.Split('|')[5];
            string invNo = "";
            if (dt.Rows.Count > 0)
            {
                invNo = dt.Rows[0]["InvoiceNumber"].ToString();
            }
            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 265px; }"
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


            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' width='50%' /></td></tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INSPECTION AND ACCEPTANCE REPORT<h3></td</tr>";
            //html += "<tr><td colspan='2' align='justify' style='font-size:12;'><br>Entity Name : " + CompanyName + "<br></td</tr></table>";

            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'  style='margin-left: -8px;margin-bottom: 90px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%';><center><i><b>INSPECTION</b></i></center></td><td width='50%';><center><i><b>ACCEPTANCE</b></i></center></td></tr>";
            html += "<tr><td width='50%';><b>Date Inspected : ________________________________ </b> <br/> <input type='checkbox'> Inspected, verified and found in order as to quantity and specifications </br> </br> </br> </br>  </br> <center><img src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<br />__________________________________<br/>Inspection Officer/Inspection Committee</center></td>"
                + "<td width='50%';><b>Date Received : ________________________________ </b> <br/> <input type='checkbox'> Complete </br> <input type='checkbox'> Partial (pls. specify quantity) </br> </br> </br>  </br> <center><img src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<br />__________________________________<br/>Supply and/or Property Custodian</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>Supplier : : " + suppliername + "<br/>PO No./Date : " + PONumber + "<br/>Requisitioning Group/User Dept. : " + "" + "<br/></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>IAR No. : " + issuanceno + "<br/>Date : " + createddate + "<br/>Invoice No. : " + invNo + "<br/>Delivery No : " + createddate + "<br/></td></tr>"
                      + "<tr>"
                      + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Item No.</i></b></td>"
                      + "<td width='35%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Description</i></b></td>"
                      + "<td width='15%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Unit</i></b></td>"
                      + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b><i>Quantity</i></b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + i+1 + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {

        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Operation_Module/GoodsReceive.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
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
        if (Common.Administrative == "True")
        {
            warehouseEntity.WarehouseID = 0;
        }
        else
        {
            warehouseEntity.WarehouseID = Convert.ToInt32(Common.SiteID);
        }

        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.GetWarehouseByID(warehouseEntity);
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
    public static string IfNeedsApproval()
    {
        return Common.CreationApproval;
    }

    [WebMethod]
    public static string IfPropNumberManualAuto()
    {
        return Common.PropertyNumberGeneration;
    }


    public void GetCustodianList()
    {
        CustodianBll custodianBll = new CustodianBll();

        DataTable dt = custodianBll.GetCustodianList(Common.ConnString);

        ddlCustodian.DataSource = dt;
        ddlCustodian.DataValueField = "CustodianID";
        ddlCustodian.DataTextField = "Custodian";
        ddlCustodian.DataBind();
        ddlCustodian.Items.Insert(0, "--- Select Custodian Type---");
    }


    [WebMethod]
    public static List<ListItem> GetApproverByCustodian(int receivedby)
    {
        UserEntity x = new UserEntity();
        UserBll userBll = new UserBll();

        x.IntegratorID = receivedby;
        x.SiteID = Convert.ToInt32(Common.SiteID);
        x.UserID = Convert.ToInt32(Common.UserID);
        x.ConnectionString = Common.ConnString;

        return userBll.GetApproverByCustodian(x);
    }

    [WebMethod]
    public static List<ListItem> GetUserByCustodian(int custodianid)
    {
        UserEntity x = new UserEntity();
        UserBll userBll = new UserBll();

        x.IntegratorID = custodianid;
        x.SiteID = Convert.ToInt32(Common.SiteID);
        x.UserID = Convert.ToInt32(Common.UserID);
        x.ConnectionString = Common.ConnString;

        return userBll.GetUserByCustodian(x);
    }

    //Barcode and RFID Generation Checker
    [WebMethod]
    public static string GetRFIDandBarcodeGeneration()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetRFIDandBarcodeGeneration(Common.ConnString);
    }

    [WebMethod]
    public static string getRFIDfromFile()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.getRFIDfromFile(Common.ConnString);
    }

    [WebMethod]
    public static string RevertRFIDNumbers(string epc)
    {
        MiscBll mb = new MiscBll();
        RFIDnumberEntity rfid = new RFIDnumberEntity();
        string[] epcnumbers = epc.Split(',');

        List<RFIDNumbers> ListRFIDnumber = new List<RFIDNumbers>();
        RFIDNumbers rfidnumbers = new RFIDNumbers();

        for (int ctr = 0; ctr < epcnumbers.Length; ctr++)
        {
            rfidnumbers = new RFIDNumbers();
            rfidnumbers.RFIDnumber = Converter.AsciiToHex(epcnumbers[ctr]);
            ListRFIDnumber.Add(rfidnumbers);
        }
        rfidnumbers.getSetRFIDnumber = ListRFIDnumber;
        rfid.GetSetRFIDnumber = rfidnumbers.getSetRFIDnumber;
        rfid.ConnectionString = Common.ConnString;
        mb.RevertRFIDNumbers(rfid);

        return "";
    }

    //Serial Number Check for Duplicate
    [WebMethod]
    public static string CheckDuplicateSerial(string serialno)
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.CheckDuplicateSerial(Common.ConnString, serialno);
    }

    //Serial Number Check for Duplicate
    [WebMethod]
    public static string CheckDuplicatePropertyNo(string propertyno)
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.CheckDuplicatePropertyNo(Common.ConnString, propertyno);
    }

    [WebMethod]
    public static void SetRFIDandBarcodeCount(bool rfidinc, bool barcodeinc, int increment)
    {
        MiscBll miscBll = new MiscBll();

        miscBll.SetRFIDandBarcodeCount(Common.ConnString, rfidinc, barcodeinc, increment);
    }

    #endregion METADATA

    #region Property Goods Received

    [WebMethod]
    public static string GetProperty(string search)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetPropertyViewGoodsReceive(search, Common.ConnString, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();
                    x.Brand = dt.Rows[i]["BrandName"].ToString();
                    x.Model = dt.Rows[i]["ModelName"].ToString();

                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs  btn-success fa fa-plus btn_AddGoodsReceive' type='button' data-toggle='tooltip'  title='Add to Goods Receive' onclick=\"AddToGoodsReceiveProperty('" + dt.Rows[i]["PropertyID"] + "','" + dt.Rows[i]["ItemCode"].ToString() + "','" + dt.Rows[i]["TagTypeID"].ToString() + "','" + dt.Rows[i]["Description"].ToString() + "')\"></button>  </center>";
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
    public static void AddToPropertyList(int grpropertyid, int propertyid, string propertyno, string serialno, string longdescription, string warrantydate, string insurancedate, string expirydate, string labeltoprint, string rfidnumber, string barcode, decimal acquiredcost)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();
        if (Common.PropertyNumberGeneration == "Auto generated")
        {
            x.PropertyNumber = null;
        }
        else
        {
            x.PropertyNumber = propertyno;
        }
        x.GRPropertyID = grpropertyid;
        x.PropertyID = propertyid;
        x.SerialNo = serialno;
        x.LongDescription = longdescription;
        if (warrantydate == "" || warrantydate == "-")
        {
            x.WarrantyStartDate = null;
            x.WarrantyEndDate = null;
        }
        else
        {
            x.WarrantyStartDate = warrantydate.Split('-')[0].Trim();
            x.WarrantyEndDate = warrantydate.Split('-')[1].Trim();
        }
        if (insurancedate == "" || insurancedate == "-")
        {
            x.InsuranceStartDate = null;
            x.InsuranceEndDate = null;
        }
        else
        {
            x.InsuranceStartDate = insurancedate.Split('-')[0].Trim();
            x.InsuranceEndDate = insurancedate.Split('-')[1].Trim();
        }
        if (expirydate == "")
        {
            x.ExpiryDate = null;
        }
        else
        {
            x.ExpiryDate = expirydate;
        }
        x.LabelToPrint = labeltoprint;
        x.RFIDNumber = rfidnumber;
        x.BarcodeNumber = barcode;
        x.AcquiredCost = acquiredcost;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        propertyBll.AddToPropertyList(x);
    }

    [WebMethod]
    public static int AddToGroupPropertyList(int propertyid, int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
        int custodian, string invoicenumber, int approver, string drnumber, string remarks, int quantity, decimal acquiredcost, decimal saleprice, int site, int assignedperson, int location)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();
        x.PropertyID = propertyid;
        x.SupplierID = supplier;
        x.DateOfAcquisition = dateofacquisition;
        x.PRNumber = prnumber;
        x.ReceivedBy = receivedby;
        x.PONumber = ponumber;
        x.CustodianID = custodian;
        x.InvoiceNumber = invoicenumber;
        x.ApproverID = approver;
        x.DRNumber = drnumber;
        x.Remarks = remarks;
        x.Quantity = quantity;
        x.SiteID = site;
        x.LocationID = location;
        x.AssignedPerson = assignedperson;

        x.AcquiredCost = acquiredcost;
        x.SalePrice = saleprice;

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return propertyBll.AddToGroupPropertyList(x);
    }

    [WebMethod]
    public static string GetPropertyAddedList(int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
        int custodian, string invoicenumber, int approver, string drnumber, string remarks)
    {
        PropertyEntity xy = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();
        xy.SupplierID = supplier;
        xy.DateOfAcquisition = dateofacquisition;
        xy.PRNumber = prnumber;
        xy.ReceivedBy = receivedby;
        xy.PONumber = ponumber;
        xy.CustodianID = custodian;
        xy.InvoiceNumber = invoicenumber;
        xy.ApproverID = approver;
        xy.DRNumber = drnumber;
        xy.Remarks = remarks;
        xy.CreatedBy = Common.UserName;
        xy.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetPropertyAddedList(xy);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    if (dt.Rows[i]["PropertyID"].ToString() != "0")
                    {
                        x.AssetType = "Property";
                    }
                    else
                    {
                        x.AssetType = "Stock";
                    }
                    x.GRPropertyID = Convert.ToInt32(dt.Rows[i]["GRPropertyID"]);
                    x.PropertyID = Convert.ToInt32(dt.Rows[i]["PropertyID"]);
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();

                    x.Action = " <center><button class='btn btn-xs  btn-danger fa fa-close btn_Remove' type='button' data-toggle='tooltip'  title='Remove to Goods Receive' onclick=\"RemoveToGoodsReceiveProperty('" + dt.Rows[i]["GRPropertyID"] + "')\"></button>  </center>";

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
    public static string getPropertyDetails(int grpropertyid, int propertyid)
    {
        PropertyBll propertyBll = new PropertyBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.getPropertyDetails(Common.ConnString, grpropertyid, propertyid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyID = Convert.ToInt32(dt.Rows[i]["PropertyID"]);
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.LongDescription = dt.Rows[i]["LongDescription"].ToString();
                    x.ExpiryDate = dt.Rows[i]["ExpiryDate"].ToString();
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
    public static void RemoveToPropertyList(int grpropertyid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.GRPropertyID = grpropertyid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        propertyBll.RemoveToPropertyList(x);
    }

    [WebMethod]
    public static void RemoveToGoodsReceiveList(int detailsid, int grpropertyid, string goodsreceivenumber)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.GRPropertyID = grpropertyid;
        x.DetailsID = detailsid;
        x.GoodsReceiveNumber = goodsreceivenumber;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        propertyBll.RemoveToGoodsReceiveList(x);
    }

    [WebMethod]
    public static void RemoveAllToPropertyList()
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        propertyBll.RemoveAllToPropertyList(x);
    }

    [WebMethod]
    public static string ConfirmGoodsReceive(int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
       int custodian, string invoicenumber, int approver, string drnumber, string remarks, int site, int location, int assignedperson)
    {
        try
        {
            PropertyEntity xy = new PropertyEntity();
            PropertyBll propertyBll = new PropertyBll();
            xy.SupplierID = supplier;
            xy.DateOfAcquisition = dateofacquisition;
            xy.PRNumber = prnumber;
            xy.ReceivedBy = receivedby;
            xy.PONumber = ponumber;
            xy.CustodianID = custodian;
            xy.InvoiceNumber = invoicenumber;
            xy.ApproverID = approver;
            xy.DRNumber = drnumber;
            xy.Remarks = remarks;
            xy.SiteID = site;
            xy.LocationID = location;
            xy.AssignedPerson = assignedperson;
            xy.CreatedBy = Common.UserName;
            xy.ConnectionString = Common.ConnString;

            MiscEntity me = new MiscEntity();
            if (Common.CreationApproval == "True" || Common.CreationApproval == "1")
                me.CreationApproval = true;
            else
                me.CreationApproval = false;

            string smtphost = Common.SMTPhost;
            string smtpport = Common.SMTPport;
            string smtpmail = Common.SMTPmail;
            string smtppass = Common.SMTPpass;

            int grid = propertyBll.ConfirmGoodsReceive(xy, me);

            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, grid);

            string grdetails = propertyBll.GoodsReceiveDetails(Common.ConnString, grid);
            string grnumber = grdetails.Split('|')[0];
            string transactorname = grdetails.Split('|')[1];
            string transactoremail = grdetails.Split('|')[2];
            string approvername = grdetails.Split('|')[3];
            string approveremail = grdetails.Split('|')[4];
            string custodianname = grdetails.Split('|')[5];
            string createddate = grdetails.Split('|')[6];

            if (approveremail != "")
            {
                string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                MailMessage mail = new MailMessage();
                mail.To.Add(approveremail);

                mail.From = new MailAddress(smtpmail);
                mail.Subject = Common.EmailSubject + " (Goods Receive)";
                string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
                Body += "A GOODS RECEIVE transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
                    + "Goods Receive No : " + grnumber + "<br>"
                    + "Remarks : " + remarks + " <br>"
                    + "Processed By : " + transactorname + " <br>"
                    + "Processed Date : " + createddate + "<br><br>";
                Body += "<table border='1'><tr>"
                    + "<th>Asset Type</th>"
                    + "<th>Item Code</th>"
                    + "<th>Quantity</th></tr>";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Body += "<tr>";
                        if (dt.Rows[i]["PropertyID"].ToString() == "" || dt.Rows[i]["PropertyID"].ToString() == "NULL")
                        {
                            Body += "<td>Stock</td>";
                        }
                        else
                        {
                            Body += "<td>Property</td>";
                        }
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

                return "okay";
            }
            else
            {
                return "approver has no email";
            }
        }
        catch (Exception ex)
        { return ex.ToString(); }
    }

    [WebMethod]
    public static string GetCountGoodsReceive(int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
       int custodian, string invoicenumber, int approver, string drnumber, string remarks, int site, int location, int assignedperson)
    {
        PropertyEntity xy = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();
        xy.SupplierID = supplier;
        xy.DateOfAcquisition = dateofacquisition;
        xy.PRNumber = prnumber;
        xy.ReceivedBy = receivedby;
        xy.PONumber = ponumber;
        xy.CustodianID = custodian;
        xy.InvoiceNumber = invoicenumber;
        xy.ApproverID = approver;
        xy.DRNumber = drnumber;
        xy.Remarks = remarks;
        xy.SiteID = site;
        xy.LocationID = location;
        xy.AssignedPerson = assignedperson;
        xy.CreatedBy = Common.UserName;
        xy.ConnectionString = Common.ConnString;

        return propertyBll.GetCountGoodsReceive(xy);
    }

    [WebMethod]
    public static string GetGoodsReceiveHeader(string search)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetGoodsReceiveHeader(search, Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.GoodsReceiveNumber = dt.Rows[i]["GoodsReceiveNumber"].ToString();
                    x.Receiver = dt.Rows[i]["Receiver"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.CreateDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    string action = "";
                    action = "<center><button type='button' class='btn btn-xs btn-primary fa fa-paperclip btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["GoodsReceiveNumber"] + "','GoodsReceive')\"></button>";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs  btn-primary fa fa-eye btn_View' type='button' data-toggle='tooltip'  title='View Details' onclick=\"viewGRDetails('" + dt.Rows[i]["GoodsReceiveID"] + "','" + dt.Rows[i]["GoodsReceiveNumber"] + "','" + dt.Rows[i]["Status"] + "')\"></button> ";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button type='button' class='btn btn-xs btn-danger fa fa-times btn_Cancel' data-toggle='tooltip' title='Cancel Goods Receive' onclick=\"cancelGoodsReceive('" + dt.Rows[i]["GoodsReceiveID"] + "')\"></button> </center>";
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
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["TotalCost"].ToString());

                    x.Action = " <center><button class='btn btn-xs  btn-danger fa fa-close btn_Remove' type='button' data-toggle='tooltip'  title='Remove to Goods Receive' onclick=\"RemoveToGoodsReceiveList('" + dt.Rows[i]["DetailsID"] + "','" + dt.Rows[i]["GRPropertyID"] + "','" + dt.Rows[i]["GoodsReceiveNumber"] + "')\"></button> </center>";


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
    public static string GetGoodsReceiveDetailsForm(int goodsreceiveid)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            string grdetails = propertyBll.GoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            string grnumber = grdetails.Split('|')[0];
            string transactorname = grdetails.Split('|')[1];
            string transactoremail = grdetails.Split('|')[2];
            string approvername = grdetails.Split('|')[3];
            string approveremail = grdetails.Split('|')[4];
            string custodianname = grdetails.Split('|')[5];
            string createddate = grdetails.Split('|')[6];
            string PONumber = grdetails.Split('|')[7];
            string suppliername = grdetails.Split('|')[8];
            string DRNumber = grdetails.Split('|')[9];
            string Receiver = grdetails.Split('|')[10];
            string WarehouseName = grdetails.Split('|')[11];
            string ApproverPosition = grdetails.Split('|')[12];
            string ReceiverPosition = grdetails.Split('|')[13];





            string formScript = GenerateGoodsReceiveIAR(dt, grnumber, suppliername, PONumber, createddate, DRNumber, approvername, Receiver, WarehouseName, ApproverPosition, ReceiverPosition);

            return formScript;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public static string GenerateGoodsReceiveIAR(DataTable dt, string goodsreceiveid, string suppliername, string PONumber, string createddate, string DRNumber, string Approver, string Receiver, string WarehouseName, string ApproverPosition, string ReceiverPosition)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];


            string invNo = "";
            if (dt.Rows.Count > 0)
            {
                invNo = dt.Rows[0]["InvoiceNumber"].ToString();
            }

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 247px; }"
            + ".page-footer {page-break-before: auto;} .page-footer-space {height: 250px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto } "
            + "@page {size: letter; margin: 10mm;}"

            + "@media print {  "
                + "table {padding: 0px 10px 0px 0px; page-break-after: auto }"
                + "tr {page-break-inside:avoid; page-break-after:auto;}"
                + "thead {display: table-header-group; }"
                + "tfoot {display: table-footer-group;}"
        + "tbody tr:nth-child(11n) { page-break-after: always; }"
                 + "body { margin: 0; zoom: 90%; }"
            + "}"
            + "</style >";
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='left' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

            html += "<tr><td colspan='4' align='center' ><br><h3>INSPECTION AND ACCEPTANCE REPORT<h3></td</tr>";
            html += "<tr><td colspan='2' align='justify' style='font-size:12;'><br></td</tr></table>";


            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%';><center><i><b>INSPECTION</b></i></center></td><td width='50%';><center><i><b>ACCEPTANCE</b></i></center></td></tr>";
            html += "<tr><td width='50%';><input type='checkbox'> Inspected, verified and found in order as to quantity and specifications </br> </br> </br><center>"+ Receiver + "<br/> "+ ReceiverPosition +"</center> </br> <center>__________________________________<br/>Proponent</center></td>"
                + "<td width='50%';><input type='checkbox'> Complete </br> <input type='checkbox'> Partial (pls. specify quantity) </br> </br> </br>  </br> <center>" + Approver + "<br/> "+ ApproverPosition + "</center></td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table border: thick solid  style='width:100%;' cellspacing='0'><thead border: 14pt double(1,12,1)><tr><td><div class='page-header-space'>";
            html += "<tr>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>Supplier : : " + suppliername + "<br/>PO No./Date : " + PONumber + "<br/>Requisitioning Office/Dept. : " + WarehouseName + "<br/></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>IAR No. : " + goodsreceiveid + "<br/>Date : " + createddate + "<br/>Invoice No. : " + invNo + "<br/>Delivery No. : " + DRNumber + "<br/></td></tr>"
                    + "<tr>"
                    + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Item No.</i></b></td>"
                    + "<td width='35%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Description</i></b></td>"
                    + "<td width='15%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Unit</i></b></td>"
                    + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b><i>Quantity</i></b></td>"
                    + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='border-bottom:1px solid;border-left:1px solid; height : 32.5px'>" + (i+1) + "</td>"
                          + "<td  style='border-bottom:1px solid;border-left:1px solid; height : 32.5px'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='border-bottom:1px solid;border-left:1px solid; height : 32.5px'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td style='width:15%;border-bottom:1px solid;border-left:1px solid;border-right:1px solid; height : 32.5px'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></tbody>";
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
    public static void CancelGoodsReceive(int goodsreceiveid, string remarks)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.GoodsReceiveID = goodsreceiveid;
        x.CreatedBy = Common.UserName;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;

        propertyBll.CancelGoodsReceive(x);
    }

    [WebMethod]
    public static void UploadDocumentsGR(int goodsreceiveid)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.GoodsReceiveID = goodsreceiveid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        propertyBll.UploadDocumentsGR(x);
    }

    protected void btnCompleteGR(object sender, EventArgs e)
    {
        int goodsreceiveid = Convert.ToInt32(txtGoodsReceiveID.Text);
        UploadDocumentsGR(goodsreceiveid);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:swal('Success','Transaction Complete without supporting Document','success');", true);
    }

    public bool success = false;

    protected void btnUploadGR(object sender, EventArgs e)
    {
        try
        {
            int goodsreceiveid = Convert.ToInt32(txtGoodsReceiveID.Text);
            string goodsreceivenumber = txtGoodsReceiveNumber.Text;
            if (GRupload.HasFiles)
            {
                string rootPath = Server.MapPath("~/SupportingDocuments/GoodsReceive/" + goodsreceivenumber + "/");
                foreach (HttpPostedFile file in GRupload.PostedFiles)
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
                    UploadDocumentsGR(goodsreceiveid);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:swal('Success','Transaction Complete with supporting Document','success');", true);
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:filenotseen();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:swal('Warning','Please Select a file to upload','warning');", true);
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:filenotseen();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:swal('Warning','Please Select a file to upload','warning');", true);
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.ToString());
            throw ex;
        }
    }

    #endregion Property Goods Received

    #region Stock Goods Received

    [WebMethod]
    public static string GetStock(string search)
    {
        StockBll stockBll = new StockBll();
        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {

            DataTable dt = stockBll.GetStockViewGoodsReceive(search, Common.ConnString);
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
                    x.Action = " <center><button class='btn btn-xs btn-success fa fa-plus ' type='button' data-toggle='tooltip'  title='Add to Goods Receive' onclick=\"AddToGoodsReceiveStock('" + dt.Rows[i]["StockID"] + "','" + dt.Rows[i]["ItemCode"] + "')\"></button></center> ";

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
    public static int AddToStockList(int stockid, int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
        int custodian, string invoicenumber, int approver, string drnumber, string remarks, int quantity, decimal acquiredcost, int site, int assignedperson, int location)
    {
        StockEntity x = new StockEntity();
        StockBll y = new StockBll();
        x.StockID = stockid;
        x.SupplierID = supplier;
        x.DateOfAcquisition = dateofacquisition;
        x.PRNumber = prnumber;
        x.ReceivedBy = receivedby;
        x.PONumber = ponumber;
        x.CustodianID = custodian;
        x.InvoiceNumber = invoicenumber;
        x.ApproverID = approver;
        x.DRNumber = drnumber;
        x.Remarks = remarks;
        x.Quantity = quantity;
        x.AcquiredCost = acquiredcost;
        x.ConnectionString = Common.ConnString;
        x.SiteID = site;
        x.LocationID = location;
        x.AssignedPerson = assignedperson;

        x.CreatedBy = Common.UserName;

        return y.AddToStockList(x);
    }

    [WebMethod]
    public static string GetStockAddedList(int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
        int custodian, string invoicenumber, int approver, string drnumber, string remarks)
    {
        StockEntity xy = new StockEntity();
        StockBll s = new StockBll();
        xy.SupplierID = supplier;
        xy.DateOfAcquisition = dateofacquisition;
        xy.PRNumber = prnumber;
        xy.ReceivedBy = receivedby;
        xy.PONumber = ponumber;
        xy.CustodianID = custodian;
        xy.InvoiceNumber = invoicenumber;
        xy.ApproverID = approver;
        xy.DRNumber = drnumber;
        xy.Remarks = remarks;
        xy.CreatedBy = Common.UserName;
        xy.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = s.GetStockAddedList(xy);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.GRStockID = Convert.ToInt32(dt.Rows[i]["GRStockID"]);
                    x.StockID = Convert.ToInt32(dt.Rows[i]["StockID"]);
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();

                    x.Action = " <center><button class='btn btn-med  btn-danger fa fa-minus btn_Remove' type='button' data-toggle='tooltip'  title='Remove to Goods Receive' onclick=\"RemoveToGoodsReceiveStock('" + dt.Rows[i]["GRStockID"] + "')\"></button>  </center>";

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
    public static void RemoveToStockList(int grstockid)
    {
        StockEntity x = new StockEntity();
        StockBll y = new StockBll();

        x.GRStockID = grstockid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveToStockList(x);
    }

    [WebMethod]
    public static void RemoveAllToStockList()
    {
        StockEntity x = new StockEntity();
        StockBll y = new StockBll();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.RemoveAllToStockList(x);
    }

    [WebMethod]
    public static string ConfirmGoodsReceiveStock(int supplier, string dateofacquisition, string prnumber, int receivedby, string ponumber,
       int custodian, string invoicenumber, int approver, string drnumber, string remarks)
    {
        StockEntity x = new StockEntity();
        StockBll y = new StockBll();

        x.SupplierID = supplier;
        x.DateOfAcquisition = dateofacquisition;
        x.PRNumber = prnumber;
        x.ReceivedBy = receivedby;
        x.PONumber = ponumber;
        x.CustodianID = custodian;
        x.InvoiceNumber = invoicenumber;
        x.ApproverID = approver;
        x.DRNumber = drnumber;
        x.Remarks = remarks;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        MiscEntity me = new MiscEntity();
        if (Common.CreationApproval == "True" || Common.CreationApproval == "1")
            me.CreationApproval = true;
        else
            me.CreationApproval = false;

        return y.ConfirmGoodsReceiveStock(x, me).ToString();
    }

    #endregion Stock Goods Received
    [WebMethod]
    public static bool checkPDF(string transnum, string transtype, string documenttype)
    {
        try
        {
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

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