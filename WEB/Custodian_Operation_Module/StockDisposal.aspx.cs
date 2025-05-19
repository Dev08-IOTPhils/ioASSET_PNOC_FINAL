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

public partial class Operation_Module_StockDisposal : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        ret = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = ret.ToUpper();
        CheckAccess();
        ModeOfDisposal();
        GetApprover();
    }

    #region metadata

    [WebMethod]
    public static string CreatePDFWasteMaterial(string issuanceno,
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
            StockEntity x = new StockEntity();
            StockDisposalBll y = new StockDisposalBll();

            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.StockDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string transactordep = disposaldetails.Split('|')[3];
            string transactorpos = disposaldetails.Split('|')[4];
            string custodian = disposaldetails.Split('|')[5];
            string remarks = disposaldetails.Split('|')[6];
            string disposaldate = disposaldetails.Split('|')[7];

            DataTable dt = y.SDLPendingUpload(x);

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
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><img id='imgicsfrom' src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + custodian + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><img id='imgicsfrom' src='" + byimagebase64 + "' width='50%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
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
    public static string CreatePDFDisposal(string issuanceno,
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
            StockEntity x = new StockEntity();
            StockDisposalBll y = new StockDisposalBll();

            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;

            string disposaldetails = y.StockDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string transactordep = disposaldetails.Split('|')[3];
            string transactorpos = disposaldetails.Split('|')[4];
            string custodian = disposaldetails.Split('|')[5];
            string remarks = disposaldetails.Split('|')[6];
            string disposaldate = disposaldetails.Split('|')[7];

            DataTable dt = y.SDLPendingUpload(x);

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
            + ".page-header, .page-header-space {height: 295px; }"
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
                + "<tr><td style='width:70%;'></td><td style='font-size:14;width:30%;'>No. <u>" + issuanceno + "</u></td</tr>"
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
                          + "<td style='width:10%;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/StockDisposal.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string IfNeedsApproval()
    {
        return Common.StockDisposalApproval;
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

    #endregion metadata

    #region Datatables

    [WebMethod]
    public static string GetStockList(string search, int custodian)
    {
        StockDisposalBll y = new StockDisposalBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetAvailableStockForDisposal(Common.ConnString, search, custodian);
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
                        x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' smlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Disposal List'></a> </center>";
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
    public static string GetStockDisposalList(int custodian)
    {
        StockDisposalBll y = new StockDisposalBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetStockDisposalList(Common.ConnString, custodian, Common.UserName, Convert.ToInt32(Common.SiteID));
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
                    if (DeleteAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-danger btn_Remove' data-toggle='tooltip' smlid='" + dt.Rows[i]["SMLID"].ToString() + "' title='Remove To Disposal List'><span class='fa fa-close'></span></a> </center>";
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
    public static string GetPendingUploadList(string search)
    {
        StockDisposalBll y = new StockDisposalBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockDisposalPendingUpload(search, Common.ConnString, Common.CustodianID, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.ModeOfDisposal = dt.Rows[i]["ModeOfDisposal"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

                    string action = "";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Supplies' onclick=\"ViewDisposalDetails('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["Status"].ToString() + "')\"><span class='fa fa-eye'></span></a>";
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

    #endregion Datatables

    #region Methods

    [WebMethod]
    public static string StockDisposalGetCount(int custodian)
    {
        StockDisposalBll y = new StockDisposalBll();
        StockEntity x = new StockEntity();

        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;

        x.ConnectionString = Common.ConnString;

        return y.StockDisposalGetCount(x);
    }

    [WebMethod]
    public static string StockTransferToDisposal(int smlid, int quantity, int custodian)
    {
        StockDisposalBll y = new StockDisposalBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        x.CustodianID = custodian;
        x.Quantity = quantity;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockTransferToDisposal(x).ToString();
    }

    [WebMethod]
    public static string StockRemoveToDisposal(int smlid, int custodian)
    {
        StockDisposalBll y = new StockDisposalBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRemoveToDisposal(x).ToString();
    }

    [WebMethod]
    public static string StockRemoveAllToDisposal(int custodian)
    {
        StockDisposalBll y = new StockDisposalBll();
        StockEntity x = new StockEntity();

        x.CustodianID = custodian;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockRemoveAllToDisposal(x).ToString();
    }

    [WebMethod]
    public static string ConfirmStockDisposal1(int approver, string modeofdisposal, int custodian, string remarks)
    {
        try
        {
            StockDisposalBll y = new StockDisposalBll();
            StockEntity x = new StockEntity();

            x.ModeOfDisposal = modeofdisposal;
            x.CustodianID = custodian;
            x.ApproverID = approver;
            x.Remarks = remarks;
            x.CreatedBy = Common.UserName;
            x.ConnectionString = Common.ConnString;

            string returnresult = y.ConfirmStockDisposal(x);
            if (Common.StockDisposalApproval == "True")
            {
                try
                {
                    UserBll userBll = new UserBll();
                    string ApprovalDetails = userBll.GetEmail(Common.ConnString, approver);

                    string smtphost = Common.SMTPhost;
                    string smtpport = Common.SMTPport;
                    string smtpmail = Common.SMTPmail;
                    string smtppass = Common.SMTPpass;

                    DataTable dt = y.GetStockDisposalList(Common.ConnString, custodian, Common.UserName, Convert.ToInt32(Common.SiteID));


                    if (ApprovalDetails.Split('|')[0] != "")
                    {
                        string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                        MailMessage mail = new MailMessage();
                        mail.To.Add(ApprovalDetails.Split('|')[0]);

                        mail.From = new MailAddress(smtpmail);
                        mail.Subject = Common.EmailSubject + " (Stock Disposal)";
                        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + ApprovalDetails.Split('|')[1] + ",<br><br><br>";
                        Body += "A STOCK DISPOSAL transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>";
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
    public static string CancelStockDisposal(string disposalno, string remarks)
    {
        StockDisposalBll y = new StockDisposalBll();
        StockEntity x = new StockEntity();

        x.CreatedBy = Common.UserName;
        x.DisposalNumber = disposalno;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;

        y.CancelStockDisposal(x);

        return "";
    }

    public bool success = false;

    [WebMethod]
    public static string UploadStockDisposal(string disposalno)
    {
        StockDisposalBll y = new StockDisposalBll();

        DataTable dt = y.UploadStockDisposalDT(Common.ConnString, disposalno, Common.UserName);
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

    [WebMethod]
    public static string GenerateStockDisposalReport(string disposalno)
    {
        StockEntity x = new StockEntity();
        StockDisposalBll y = new StockDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.StockDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string transactordep = disposaldetails.Split('|')[3];
        string transactorpos = disposaldetails.Split('|')[4];
        string custodian = disposaldetails.Split('|')[5];
        string remarks = disposaldetails.Split('|')[6];
        string disposaldate = disposaldetails.Split('|')[7];

        DataTable dt = y.SDLPendingUpload(x);

        return GenerateStockDisposalForm(dt, disposalno, transactorname, approvername, transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    public static string GenerateStockDisposalForm(DataTable dt, string disposalno, string transactorname, string approvername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
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
            + ".page-header, .page-header-space {height: 295px; }"
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
                          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
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
            html += "<tr><td style='width:50%'>Transacted by</td><td  style='width:50%'>Approved by</td></tr>";//<td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
            html += "<tr rowspan='2'><td style='width:50%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='width:50%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "</td></tr>";//<td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
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
                          + "<td style='width:10%;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    [WebMethod]
    public static string GenerateStockDisposalReportWMR(string disposalno)
    {
        StockEntity x = new StockEntity();
        StockDisposalBll y = new StockDisposalBll();

        x.DisposalNumber = disposalno;
        x.ConnectionString = Common.ConnString;

        string disposaldetails = y.StockDisposalDetails(x);
        string transactorname = disposaldetails.Split('|')[1];
        string approvername = disposaldetails.Split('|')[2];
        string transactordep = disposaldetails.Split('|')[3];
        string transactorpos = disposaldetails.Split('|')[4];
        string custodian = disposaldetails.Split('|')[5];
        string remarks = disposaldetails.Split('|')[6];
        string disposaldate = disposaldetails.Split('|')[7];

        DataTable dt = y.SDLPendingUpload(x);

        return FormsAndReports.GenerateStockDisposalFormWMR(dt, disposalno, transactorname, approvername, "", "", transactordep, transactorpos, disposaldate, remarks, custodian);
    }

    #endregion Methods

    [WebMethod]
    public static string CreateSDWasteMaterial(string issuanceno,
    string imagebase64,
    string passphrase,
    string certbase64,
    bool datestamp,
    string transtype,
    string operationtype)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockDisposalBll y = new StockDisposalBll();
            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string disposaldetails = y.StockDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string transactordep = disposaldetails.Split('|')[3];
            string transactorpos = disposaldetails.Split('|')[4];
            string custodian = disposaldetails.Split('|')[5];
            string remarks = disposaldetails.Split('|')[6];
            string disposaldate = disposaldetails.Split('|')[7];
            DataTable dt = y.SDLPendingUpload(x);
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
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 190px;'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + custodian + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><div style='width: 50%; height: 75px;' ></div><br /><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-bottom:1px solid;borde r-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><div style='width: 50%; height: 75px;' ></div><br/><center><u></u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><div style='width: 50%; height: 75px;' ></div><br /><center><u></u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
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
    public static string CreateSDDisposalForm(string issuanceno,
    string imagebase64,
    string passphrase,
    string certbase64,
    bool datestamp,
    string transtype,
    string operationtype)
    {
        try
        {
            StockEntity x = new StockEntity();
            StockDisposalBll y = new StockDisposalBll();
            x.DisposalNumber = issuanceno;
            x.ConnectionString = Common.ConnString;
            string disposaldetails = y.StockDisposalDetails(x);
            string transactorname = disposaldetails.Split('|')[1];
            string approvername = disposaldetails.Split('|')[2];
            string transactordep = disposaldetails.Split('|')[3];
            string transactorpos = disposaldetails.Split('|')[4];
            string custodian = disposaldetails.Split('|')[5];
            string remarks = disposaldetails.Split('|')[6];
            string disposaldate = disposaldetails.Split('|')[7];
            DataTable dt = y.SDLPendingUpload(x);
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
            + ".page-header, .page-header-space {height: 295px; }"
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
                          + "<td style='width:10%;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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