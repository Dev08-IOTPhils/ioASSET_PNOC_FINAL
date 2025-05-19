using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AdvanceEntity;
using AdvanceBll;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;

/// <summary>
/// Summary description for FormsAndReports
/// </summary>
public class FormsAndReports
{
    static string CompanyName = "";
    static string CompanyContactNumber = "";
    static string CompanyAddress = "";
    static string CompanyLogo = "";
    public FormsAndReports()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GenerateRequestFormCTR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate)
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>COMPANY TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian //+ "-" + transactorname 
                + "</u></td><td style='font-size:12;'><br>Return Date: </td><td style='font-size:12;'><br><u>" + returndate.Split(' ')[0] + "</u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + custodian + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

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
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateRequestFormCTRIssuance(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate)
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
            + ".page-header, .page-header-space {height: 210px; }"
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>COMPANY TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "</u></td>"//+ "-" + transactorname 
                + "<td style='font-size:12;'><br>Return Date: </td><td style='font-size:12;'><br><u>" + returndate.Split(' ')[0] + "</u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + custodian + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Model</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>Item Code</b></td>"
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateListReturnRequest(DataTable dt, string returnno, string Remarks, string EmployeePosition, string EmployeeDepartment, string CustodianPosition, string CustodianDepartment, string CustodianName, string employeename, string daterequested)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];
            string TransactionNo = returnno;

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
                + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "'; width='50%'; style='width: 150px; height: 150px'/></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY TURN-IN SLIP</h3></br></td</tr></table>";
            html += "</div>";
            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table border='1'; class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%' valign='top'><br/>Received by :<br/><br/><br/><center><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + daterequested + "</br>Date</center></td>" +
                "<td>I CERTIFY that the articles listed on are turned-in under the circumtances indicated in 'REMARKS'<br/><br/><br/><center>" + employeename + "</center><center>Name of Personnel returning equipment</center><br/><br/><br/>Noted by:<br/><br/>___________________________________&emsp;_____________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Date<br/>Supply Accountable Officer Supply Accountable Office</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='3';></br>TO:&emsp;" + CustodianName + "</br>" + CustodianPosition + "" + CustodianDepartment + "</br></br></td><td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' width='10%';> Page of Pages </td><td width='10%'; style='text-align: right; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ></td></tr>"
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ReturnRequestNo"].ToString() + " . " + dt.Rows[i]["Description"].ToString() + " </td>"
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

    public static string GenerateListReturn(DataTable dt, string returnno, string Remarks, string EmployeePosition, string EmployeeDepartment, string CustodianPosition, string CustodianDepartment, string CustodianName, string employeename, string daterequested)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];
            string TransactionNo = returnno;

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 200px; }"
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
                + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px'/></td</tr>"
            + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY TURN-IN SLIP</h3></br></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table border='1'; class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%' valign='top'><br/>Received by :<br/><br/><br/><center><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + daterequested + "</br>Date</center></td>" +
                "<td>I CERTIFY that the articles listed on are turned-in under the circumtances indicated in 'REMARKS'<br/><br/><br/><center>" + employeename + "</center><center>Name of Personnel returning equipment</center><br/><br/><br/>Noted by:<br/><br/>___________________________________&emsp;_____________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Date<br/>Supply Accountable Officer Supply Accountable Office</td></tr>";
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

    public static string GenerateListReturnCTR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate)
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
            + ".page-header, .page-header-space {height: 260px; }"
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>COMPANY TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian //+ "-" + transactorname 
                + "</u></td><td style='font-size:12;'><br>Return Date: </td><td style='font-size:12;'><br><u>" + returndate.Split(' ')[0] + "</u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + custodian + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

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
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + "" + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateListReturnRAR(DataTable dt, string Remarks, string returnno, string employeename, string daterequested, string CustodianName)
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
            + ".page-header, .page-header-space {height: 170px; }"
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
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px'/></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table border='1'; cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;';><br/>Returned by :<br/><br/><center><u>" + employeename + "</u></br>End User</br></br>" + daterequested + "</br>Date</center></td>" +
                "<td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><br/>Received by :<br/><br/><center><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + DateTime.Now.ToString("MM/dd/yyyy") + "</br>Date</center></td></tr>";
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
                + "<tr><td witdth='20%'; style='border-bottom:1px solid;border-right:1px solid;'></br>RRSP2 No. :</br>" + returnno + "</td></tr>"
                + "<tr><td colspan='5' align='center'; style='border-left:1px solid;border-right:1px solid;' >This is to acknowledge receipt of the returned Property/Equipment</td></tr>"

                                 + "<tr>"
                                 + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                                 + "<td witdth='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
                                 + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>PAR No.</b></td>"
                                 + "<td witdth='45%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>End-user</b></td>"
                                 + "<td witdth='15%'; colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td></tr>";
                //+ "<td witdth='20%'; colspan='2'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>"+ TransactionNo + "</b></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Description"].ToString() + "</td>"
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }
    public static string GenerateListReturnRARSEP(DataTable dt, string Remarks, string returnno, string employeename, string daterequested, string CustodianName)
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
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table border='1'; cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' width='100%'>";
            html += "<tr><td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;';><br/>Returned by :<br/><br/><center><u>" + employeename + "</u></br>End User</br></br>" + daterequested + "</br>Date</center></td>" +
                "<td width='50%'; valign='top';style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><br/>Received by :<br/><br/><center><u>" + CustodianName + "</u></br>Head, Property and/or Supply Division/Unit</br></br>" + DateTime.Now.ToString("MM/dd/yyyy") + "</br>Date</center></td></tr>";
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
                html += "<tr><td colspan='5'; align='center'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' ><br><h4>RECEIPT OF RETURNED SEMI-EXPENDABLE PROPERTY</h4></td></tr>"
                  + "<tr><td rowspan='2'; colspan='4'; align='left'; witdth='80%'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Entity Name: " + CompanyName + "<br/></td><td width='20%'; style='border-bottom:1px solid;border-right:1px solid;';>Date: " + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr>"
                  + "<tr><td witdth='20%'; style='border-bottom:1px solid;border-right:1px solid;'></br>RRSP No. :</br>" + returnno + "</td></tr>"
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
                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Description"].ToString() + "</td>"
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }
    // public static string GenerateIssuanceFormICS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    // {
    //     try
    //     {
    //         MiscBll miscBll = new MiscBll();
    //         MiscEntity miscEntity = new MiscEntity();

    //         string x = miscBll.GetCompanyInformation(Common.ConnString);
    //         string CompanyName = x.Split('|')[0];
    //         string CompanyContactNumber = x.Split('|')[3];
    //         string CompanyAddress = x.Split('|')[5];

    //         string html = "";
    //         #region styles
    //         //style
    //         html += "<style>"
    //         + ".page-header, .page-header-space {height: 210px; }"
    //         + ".page-footer, .page-footer-space {height: 250px;}"
    //         + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //         + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //         + ".page-body {page-break-after: auto;}"
    //         + "@page {size: A4; margin: 10mm;}"

    //         + "@media print {"
    //             + "table {padding: 0px 10px 0px 0px;}"
    //             + "thead {display: table-header-group; }"
    //             + "tfoot {display: table-footer-group;}"
    //             + "body { margin: 0;}"
    //         + "}"
    //         + "</style >";
    //         #endregion

    //         #region div header and footer
    //         #region header
    //         ////header div
    //         //html += "<div class='page-header'>";
    //         //html += "<table style='width:100%;font-family: Arial;'>"
    //         //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
    //         html += "<div class='page-header'>";
    //         html += "<table style='width:100%;font-family: Arial;'>"

    //       + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

    //         //"<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
    //         html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";


    //         //   html += "<table style='width:100%;font-family: Arial;'>";
    //         html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
    //             + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
    //         html += "</div>";

    //         #endregion header

    //         #region footer
    //         //footer div
    //         html += "<div class='page-footer'>";
    //         //terms and conditions //signatories
    //         //html += "<hr style='border-top: 1px dashed'>";
    //         html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //         html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorposition + "/" + transactordepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
    //             "<td width='50%'><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeeposition + "|" + employeedepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
    //         html += "</table>";
    //         html += "</div>";
    //         #endregion
    //         #endregion

    //         #region header placeholder
    //         //table header
    //         html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //         html += "</div></td></tr></thead>";
    //         #endregion

    //         #region body placeholder
    //         //table body
    //         html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //         if (dt.Rows.Count > 0)
    //         {
    //             html += "<tr>"
    //                   + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                   + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                   + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                   + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                   + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                   + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
    //                   + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
    //             for (int i = 0; i < dt.Rows.Count; i++)
    //             {
    //                 html += "<tr>"
    //                       //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                       //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                       //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                       //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                       //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
    //                       + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
    //                       + "</tr>";
    //             }
    //             html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //         }
    //         else
    //         {
    //             html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //         }
    //         html += "</div></td></tr></tbody>";
    //         #endregion


    //         #region footer placeholder
    //         //table footer
    //         html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //         html += "</div></td></tr></tfoot></table>";
    //         #endregion

    //         return html;
    //     }
    //     catch (Exception ex)
    //     {
    //         ex.ToString();
    //         return "";
    //     }
    // }

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
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            //"<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";


            //   html += "<table style='width:100%;font-family: Arial;'>";
            //html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
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
                          //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
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
            //html += "<hr style='border-top: 1px dashed'>";
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

    ////public static string GenerateIssuanceFormICS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 130px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorposition + "/" + transactordepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
    //            "<td width='50%'><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeeposition + "|" + employeedepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
    //                  + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GeneratePropertyTransferFormICS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            //"<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";


            //   html += "<table style='width:100%;font-family: Arial;'>";
            //html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
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
                      //+ "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
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
                          //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
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
            //html += "<hr style='border-top: 1px dashed'>";
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
    //public static string GeneratePropertyTransferFormICS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "<html><head>";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 130px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style ></head><body>";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorposition + "/" + transactordepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
    //            "<td width='50%'><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeeposition + "|" + employeedepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
    //                  + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table></body></html>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    //public static string GenerateIssuanceFormPAR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        string CompanyName = x.Split('|')[0];
    //        string CompanyContactNumber = x.Split('|')[3];
    //        string CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 210px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //      + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"

    //        + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT</h3></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td align='right' style='font-size:12;'>PAR No. : <u>" + issuanceno + "</u></td><td style='font-size:12;'><u>" + "" + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
    //            "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                   + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
    //                      + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    //public static string GenerateIssuanceFormPAR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        string CompanyName = x.Split('|')[0];
    //        string CompanyContactNumber = x.Split('|')[3];
    //        string CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 270px; }"
    //        + ".page-footer, .page-footer-space {height: 280px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //      //+ "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
    //      + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>"

    //        + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT</h3></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td align='right' style='font-size:12;'>PAR No. : <u>" + issuanceno + "</u></td><td style='font-size:12;'><u>" + "" + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header


    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                   + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
    //                      + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td colspan='2'><b>REMARKS:</b><br/><br/><center><u>" + remarks + "</u></center><br/></td></tr>";
    //        html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
    //            "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion


    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GeneratePropertyTransferFormPAR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "<html><head>";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 265px; }"
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
            + "</style ></head><body>";
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
           //+ "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "'; width='50%'; /></td</tr>"
           + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>"

             + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT</h3></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td align='right' style='font-size:12;'>PAR No. : <u>" + issuanceno + "</u></td><td style='font-size:12;'><u>" + "" + "</u></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";

            html += "<tr><td colspan='2'><b>REMARKS:</b><br/><br/><center><u>" + remarks + "</u></center><br/></td></tr>";
            html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
                "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
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
                          //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
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
            #endregion


            #region footer placeholder
            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table></body></html>";
            #endregion

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }
    public static string GenerateIssuanceForm(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>ITEM TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br>Return Date:</td><td style='font-size:12;'><br><u></u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

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
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateRequestForm(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate)
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>COMPANY TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian //+ "-" + transactorname 
                + "</u></td><td style='font-size:12;'><br>Return Date: </td><td style='font-size:12;'><br><u>" + returndate.Split(' ')[0] + "</u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + custodian + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

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
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateRequestFormS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate)
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>COMPANY TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian //+ "-" + transactorname 
                + "</u></td><td style='font-size:12;'><br>Return Date: </td><td style='font-size:12;'><br><u>" + returndate.Split(' ')[0] + "</u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + custodian + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

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
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          //dothis
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

    public static string GenerateRequestFormRIS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate, string site, string approverpos, string transactorname, string transactordep)
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
            #endregion

            #region div header and footer
            #region header
            ////header div
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
            //<img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";


            // html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            //html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td>" + approverpos + "</td><td>" + transactordep + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
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
                    string statusNo = "";
                    string statusYes = "✓";
                    if (dt.Rows[i]["Status"].ToString() == "Issued")
                    {
                        statusNo = "✓";
                        statusYes = "";
                    }
                    html += "<tr>"
                          //dothis
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

    //public static string GenerateRequestFormRIS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string issueddate, string remarks, string custodian, string approvername, string returndate, string site)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + custodian + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td></td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusNo = "";
    //                string statusYes = "1";
    //                if (dt.Rows[i]["Status"].ToString() == "Issued")
    //                {
    //                    statusNo = "1";
    //                    statusYes = "";
    //                }
    //                html += "<tr>"
    //                      //dothis
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}

    public static string GenerateNewGatePassForm(DataTable dt, string gatepassno, string accountablename, string approvername, string remarks, string createddate)
    {
        try
        {
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

                + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td align='center' style='font-size:15;'><h3>GATE PASS for Personal Property</h3></br></td</tr></table>";

            html += "</div>";


            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td align='left'; style='height:20px;width:50%; border-bottom:1px solid;border-top:1px solid;border-left:1px solid;border-right:1px solid;'><b>REQUESTED BY :</b></td><td align='left'; style='height:20px;width:50%; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>APPROVED BY :</b></td></tr>";
            html += "<tr><td align='center'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/><br/><br/><center><b><u>" + accountablename + "</u></b><br/>Designation</center></br/></td><td style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/><br/><br/><center><b><u>" + approvername + "</u></b><br/>Chief, General Services Division</center></br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>DISTRIBUTION OF COPY:</b><br/>ORIGINAL- BEARER<br/>DUPLICATE- SECURITY<br/>DUPLICATE- SECURITY<br/>TRI- ASSET MANAGEMENT SECTION</td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Inspected by:<br/><br/><br/><br/><b>SECURITY PERSONNEL</b></td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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


    public static string GenerateGatePassForm(DataTable dt, string gatepassno, string accountablename, string approvername, string remarks, string createddate)
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";

            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
             + "<tr><td align='center' style='font-size:15;'><br><b>G A T E   P A S S <b></td</tr>"
             + "<tr><td align='right' style='font-size:14;'>No. <u>" + gatepassno + "</u></td</tr><tr><td align='right' style='font-size:14;'>Date. <u>" + createddate.Split(' ')[0] + "</u></td</tr></table>";
            html += "<table  style='font-size:90%; font-family:Arial;' width='100%'><tr><td>TO: GUARD ON DUTY</td><td align='right'></td></tr>";
            html += "<tr><td>&emsp;Please allow <u> " + accountablename + "</u> to bring inside/outside the Company premises the following items<br></td></tr>";
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
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

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
                          + "<td ><center>" + dt.Rows[i]["Description"].ToString() + "</center></td>"
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
    public static string GenerateDayPassForm(DataTable dt, string daypassno, string accountablename, string approvername, string remarks, string createddate)
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
             + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
             + "<tr><td align='center' style='font-size:15;'><br><b>Personal Property Gate Pass Request <b></td</tr>"
             + "<tr><td align='right' style='font-size:14;'>No. <u>" + daypassno + "</u></td</tr><tr><td align='right' style='font-size:14;'>Date. <u>" + createddate.Split(' ')[0] + "</u></td</tr></table>";
            html += "<table  style='font-size:90%; font-family:Arial;' width='100%'><tr><td>TO: GUARD ON DUTY</td><td align='right'></td></tr>";
            html += "<tr><td>&emsp;Please allow <u> " + accountablename + "</u> to bring outside the Company premises the following items<br></td></tr>";
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
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td><center><b>QTY</b><center></td>"
                      + "<td><center><b>Category</b><center></td>"
                      + "<td><center><b>Brand</b><center></td>"
                      + "<td><center><b>Serial #</b><center></td>"
                      + "<td><center><b>Description</b><center></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td ><center>1</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Category"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Brand"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["SerialNumber"].ToString() + "</center></td>"
                          + "<td ><center>" + dt.Rows[i]["Description"].ToString() + "</center></td>"
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    //public static string GenerateDisposalForm(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername,  string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 210px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: always;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
    //            + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
    //            + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
    //            + "<tr><td align='center' style='font-size:15;'><br><b>DISPOSAL FORM<b></td</tr>"
    //            + "<tr><td align='right' style='font-size:14;'>No. <u>" + disposalno + "</u></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'></td><td style='font-size:12;'></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + disposaldate + "</u></td></tr>"
    //            //+ "<tr><td style='font-size:12;'></td><td style='font-size:12;'></td><td style='font-size:12;'></td><td style='font-size:12;'></td></tr>"
    //            + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br</td><td style='font-size:12;'><br><u></u></td></tr>"
    //            + "<tr><td style='font-size:12;'>Remarks/Purpose:</td><td style='font-size:12;' colspan='3'><u>" + remarks + "</u></td></tr></table>";

    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'><b><u>TERMS AND CONDITIONS:</u></b>";
    //        html += "<table style='width:100%;'><tr><td><p align='justify' style='font-size:9.5px; font-family: Arial;'>In trust for and as property of <b> " + CompanyName + ",</b> the above described equipment having been lend and entrusted to me for trials and with the"
    //            + " obligation on my part to return the same complete in good order and condition before the agreed date. I further agree to the said property in my residence or place of business at the address indicated herein above and not to remove the same from said premises without previous knowledge and consent by <b>" + CompanyName + "</b>"
    //            + " I agree to use the Company Property provided to me solely for the intended uses: official business use and/or work related purposes. I am responsible for the care and"
    //            + " maintenance of the equipment. In case that the equipment is, in any case, damaged, lost, stolen, while outside the office premises, I shall be responsible for its cost of replacement or repair and other incidental charges it may occur."
    //            + " In case the equipment gets broken due to wear and tear, I shall notify the Company Custodian immediately and have it inspected by the Company's Technician as soon as possible. All civic actions on this contract shall be instituted in the court"//s of San Juan, Metro Manila"
    //            + " <p align='justify' style='font-size:9.5px; font-family: Arial;'>I agree to maintain the property in good condition and to return it when I cease working for the company, or earlier on request.";
    //        html += " By accepting this Agreement, I acknowledge the receipt in working order and good condition of the company-owned property listed above and I conform to the terms and condition stated.</p></td></tr></table>";
    //        //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='width:25%'>Transacted by</td><td  style='width:25%'>Approved by</td><td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
    //        html += "<tr rowspan='2'><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
    //        html += "<tr><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td><td  style='width:25%'>Date</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                  + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
    //                  + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Model</b></td>"
    //                  + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
    //                  + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>PROPERTY #</b></td>"
    //                  //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      + "<td >" + dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString()+ "</td>"
    //                      + "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='5' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    //public static string GenerateDisposalFormIIRUP(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 165px; }"
    //        + ".page-footer, .page-footer-space {height: 200px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; size: landscape; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>INVENTORY AND INSPECTION REPORT OF UNSERVICEABLE PROPERTY<b></td</tr>"
    //            + "<tr><td colspan='2'; align='center' style='font-size:14;'> As at ___________________________________<br/><br/></td</tr>"
    //            + "<tr><td align='left' style='font-size:14;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:14;'>Fund Cluster: ______________________</td></tr>"
    //            + "<tr><td colspan='2'; align='left' style='font-size:12;'></br>&emsp; <u>" + transactorname + "</u> &emsp;&emsp;&emsp;&emsp; <u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;_______________________<br/>&emsp;&emsp;<i>(Name of Accountable Officer)</i>&emsp;&emsp;&emsp;&emsp;<i>(Designation)</i>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<i>(Station)</i></td</tr>"
    //            + "</table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
    //        //html += "<tr><td style='width:60%'><br/><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</br></br></br></br>&emsp;&emsp;Requested by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:<br/><br/>&emsp;&emsp;&emsp;<u>" + transactorname + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of <br/>&emsp;&emsp;&emsp;&emsp;&emsp; Accountable Officer) &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Authorized Officer) <br/><br/>&emsp;&emsp;&emsp;<u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;___________________________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Accountable Official)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Authorized Official)<br/><br/></td>"
    //        //    + "<td align='left';valign='top' style='width:40%'><br/> I CERTIFY that I have inspected each &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;I CERTIFY that I have <br/> and every article enumerated in this report,&emsp;&emsp;&emsp;&emsp;witnessed the disposition of the<br/>and that the disposition made therof was, &emsp;&emsp;&emsp;&emsp; articles enumerated on this <br/> in my judgement, the best for the public &emsp;&emsp;&emsp;&emsp;&emsp; report this __day of <br/>interest. &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_____________,______<br/><br/><br/>________________________________&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;________________________<br/>&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of<br>&emsp;&emsp;&emsp;&emsp;&emsp;Inspection Officer)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Witness)</td></tr></table>";
    //        html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
    //            + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + "" + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + inspectorname + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + certifiername + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: center; border-top:1px solid;border-left:1px solid;' colspan='10';><b>INVENTORY:</b></td><td style='text-align: center; border-top:1px solid;border-left:1px solid;border-right:1px solid;' colspan='8'><b>INSPECTION and DISPOSAL</b></td></tr>"
    //                 + "<tr>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Particulars/Articles</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Qty</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depreciation</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Carrying Amount</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Remarks</b></td>"
    //                  + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;' colspan='5';>DISPOSAL</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-top:1px solid;'  rowspan='2'><i><b>Appraised Value</td><td style='text-align: center; border-left:1px solid;border-right:1px solid;border-bottom:1px solid;border-top:1px solid;'  colspan='2'>RECORD OS SALES</td>"
    //                  + "</tr><tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Sale</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Transfer</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Destruction</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Others(Specify)</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>OR No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                //dothis
    //                string DepreciatedCostString = "";
    //                if (depreciationSetting == "Straight Line Depreciation Method")
    //                {
    //                    if (Convert.ToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
    //                    {
    //                        DepreciatedCostString = "0.00";
    //                    }
    //                    else
    //                    {
    //                        DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
    //                    }
    //                }
    //                else if (depreciationSetting == "Diminishing Balance Method")
    //                {
    //                    if (Convert.ToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
    //                    {
    //                        DepreciatedCostString = "0.00";
    //                    }
    //                    else
    //                    {
    //                        DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
    //                    }
    //                }
    //                string auction = "";
    //                string transfer = "";
    //                string destruction = "";
    //                string donation = "";
    //                switch (dt.Rows[i]["ModeOfDisposal"].ToString())
    //                {
    //                    case "AUCTION":
    //                        auction = "1";
    //                        break;
    //                    case "TRANSFER":
    //                        transfer = "1";
    //                        break;
    //                    case "DONATION":
    //                        donation = "1";
    //                        break;
    //                    case "DESTRUCTION":
    //                        destruction = "1";
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                html += "<tr>"
    //                        + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + DepreciatedCostString + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + auction + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + transfer + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + destruction + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + donation + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"

    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='18' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}

    //public static string GenerateTransferFormPTR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string fromloc, string toloc, string employeefromname, string employeedep)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();
    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];
    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 255px; }"
    //        + ".page-footer, .page-footer-space {height: 220px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"
    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion
    //        #region div header and footer
    //        #region header
    //        //header div
    //        //html += "<div class='page-header'>";
    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
    //             + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
    //        //<img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
    //        html += "<tr><td colspan='4' align='center' ><br><h3>PROPERTY TRANSFER REPORT<h3></td</tr>";
    //        // html += "<table style='width:100%;font-family: Arial;'>"
    //        html += "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";
    //        #endregion header
    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
    //        //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
    //        //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
    //        //    + "</td></tr>";
    //        html += "<tr><td><table style='font-size:75%;';>";
    //        html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
    //        html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
    //        html += "<tr><td>Designation :</td><td>" + employeedep + "</td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
    //        html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
    //        html += "</table></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion
    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion
    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + employeefromname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>PTR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
    //                  + "<tr>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td width='20%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
    //                  //+ "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
    //                  + "<td width='35%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"
    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion
    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion
    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}

    public static string GenerateTransferFormITRIssuance(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            + ".page-header, .page-header-space {height: 170px; }"
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
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>"

            + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
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
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
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
            html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
            html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
            html += "</table></table>";
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


    //public static string GenerateTransferFormITRIssuance(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        string CompanyName = x.Split('|')[0];
    //        string CompanyContactNumber = x.Split('|')[3];
    //        string CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 170px; }"
    //        + ".page-footer, .page-footer-space {height: 220px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"

    //      + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"

    //        + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
    //        //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
    //        //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
    //        //    + "</td></tr>";
    //        html += "<tr><td><table style='font-size:75%;';>";
    //        html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
    //        html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
    //        html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
    //        html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
    //        html += "</table></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion


    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
    //                  + "<tr>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
    //                  + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateStockIssuanceFormITR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            + ".page-header, .page-header-space {height: 253px; }"
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
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";
            //<img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY TRANSFER REPORT<h3></td</tr>";

            //  html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + " </td>"
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
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
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
            html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
            html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
            html += "</table></table>";
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

    //public static string GenerateStockIssuanceFormITR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 200px; }"
    //        + ".page-footer, .page-footer-space {height: 220px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        //html += "<div class='page-header'>";
    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";

    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
    //            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
    //        //<img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
    //        html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY TRANSFER REPORT<h3></td</tr>";

    //        //  html += "<table style='width:100%;font-family: Arial;'>"
    //        html += "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
    //        //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
    //        //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
    //        //    + "</td></tr>";
    //        html += "<tr><td><table style='font-size:75%;';>";
    //        html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
    //        html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
    //        html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
    //        html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
    //        html += "</table></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion


    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
    //                  + "<tr>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
    //                  + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    //public static string GenerateStockIssuanceFormITR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 110px; }"
    //        + ".page-footer, .page-footer-space {height: 220px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
    //        //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
    //        //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
    //        //    + "</td></tr>";
    //        html += "<tr><td><table style='font-size:75%;';>";
    //        html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
    //        html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
    //        html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
    //        html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
    //        html += "</table></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion


    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
    //                  + "<tr>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
    //                  + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}

    public static string GenerateStockDisposalForm(DataTable dt, string disposalno, string transactorname, string approvername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
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
                          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
            + "<tr><td align='center' style='font-size:15;'><br><b>DISPOSAL FORM<b></td</tr>"
                + "</table><br>";
            //<tr><td align='right' style='font-size:14;'>No. <u>" + disposalno + "</u></td</tr>
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:50%'>Transacted by</td><td  style='width:50%'>Approved by</td></tr>";//<td  style='width:25%'>Inspected by</td><td  style='width:25%'>Certified by</td></tr>";
            html += "<tr rowspan='2'><td style='width:50%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "</td><td style='width:50%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "</td></tr>";//<td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + inspectorname + "</td><td style='width:25%'>&emsp;&emsp;&emsp;<br><br>" + certifiername + "</td></tr>";
            html += "<tr><td  style='width:50%'>Date</td><td  style='width:50%'>Date</td></tr>";//<td  style='width:25%'>Date</td><td  style='width:25%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>ITEM CODE</b></td>"
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateStockDisposalFormWMR(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
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
            #endregion

            #region div header and footer
            #region header
            //header div
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>WASTE MATERIALS REPORT</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'><tr><td colspan='1' align='left'; '>"
                 + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
            //<img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>WASTE MATERIALS REPORT<h3></td</tr>";

            //html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
            html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><u>" + custodian + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
            html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
            html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><center><u>" + inspectorname + "</u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><center><u>" + certifiername + "</u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + "" + "</u></td></tr>"
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
    //public static string GenerateStockDisposalFormWMR(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 280px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>WASTE MATERIALS REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
    //        html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><u>" + custodian + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
    //        html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
    //        html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
    //        html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><center><u>" + inspectorname + "</u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><center><u>" + certifiername + "</u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + "" + "</u></td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='7';><b>ITEMS FOR DISPOSAL</b></td></tr>"
    //                  + "<tr>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item</b></td>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td colspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Record of Sales</b></td>"
    //                  + "</tr><tr>"
    //                  + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Official Receipt</b></td>"
    //                  + "</tr><tr>"
    //                  + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No.</b></td>"
    //                  + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Date</b></td>"
    //                  + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Amount</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + i + 1 + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}

    public static string GenerateStockIssuanceForm(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>ITEM TRUST RECEIPT<b></td</tr>"
                + "<tr><td align='right' style='font-size:14;'>No. <u>" + issuanceno + "</u></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Issued to:</td><td style='font-size:12;'><u>" + employeename + "</u></td><td style='font-size:12;'>Date:</td><td style='font-size:12;'><u>" + issueddate + "</u></td></tr>"
                + "<tr><td style='font-size:12;'>Position:</td><td style='font-size:12;'><u>" + employeeposition + "</u></td><td style='font-size:12;'>User Department:</td><td style='font-size:12;'><u>" + employeedepartment + "</u></td></tr>"
                + "<tr><td style='font-size:12;'><br>Custodian:</td><td style='font-size:12;'><br><u>" + custodian + "-" + transactorname + "</u></td><td style='font-size:12;'><br>Return Date:</td><td style='font-size:12;'><br><u></u></td></tr>"
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
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='width:20%'>Requested by</td><td  style='width:20%'>Approved by</td><td  style='width:20%'>Released by</td><td  style='width:20%'>Returned to</td><td  style='width:20%'>Returned by</td></tr>";
            html += "<tr rowspan='2'><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + employeename + "</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + approvername + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>" + transactorname + "&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td><td  style='width:20%'>&emsp;&emsp;&emsp;<br><br>&emsp;&emsp;&emsp;</td></tr>";
            html += "<tr><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td><td  style='width:20%'>Date</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                      + "<td style='width:10%; border-top:1px dashed;border-bottom:1px dashed;'><b>QTY</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Model</b></td>"
                      + "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Description</b></td>"
                      + "<td style='width:15%;border-top:1px dashed;border-bottom:1px dashed;'><b>Item Code</b></td>"
                      //+ "<td style='border-top:1px dashed;border-bottom:1px dashed;'><b>Unit Price (Php)</b></td>"
                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td style='width:10%;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          + "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td style='width:15%;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    public static string GenerateStockIssuanceFormRIS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site, string approverpos)
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
            html += "<table><tr><td width='75%' style='font-size:12;'>Entity Name : <u>" + header1 + "</u></td><td width='25%' style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
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
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td style='text-align: center;'>" + approverpos + "</td><td style='text-align: center;'>" + transactorposition + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
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
                          + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + (i + 1) + "</td>"
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

    //public static string GenerateStockIssuanceFormRIS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        string CompanyName = x.Split('|')[0];
    //        string CompanyContactNumber = x.Split('|')[3];
    //        string CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 200px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        //html += "<div class='page-header'>";
    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px'/></td</tr>";
    //        // + "<tr><td colspan='1' align='center'; ><center><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></center></td></tr>";
    //        html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

    //        // html += "<table style='width:100%;font-family: Arial;'>"
    //        html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td>" + transactordepartment + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + employeedepartment + "</u><br/>Office: <u>" + site + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusYes = "✓";
    //                string statusNo = "";
    //                if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
    //                {
    //                }
    //                else
    //                {
    //                    statusYes = "";
    //                    statusNo = "✓";
    //                }
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    ////public static string GenerateStockIssuanceFormRIS(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td>" + transactordepartment + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + employeedepartment + "</u><br/>Office: <u>" + site + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusYes = "1";
    //                string statusNo = "";
    //                if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
    //                {
    //                }
    //                else
    //                {
    //                    statusYes = "";
    //                    statusNo = "1";
    //                }
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + remarks + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateStockIssuanceFormRISStockRequest(DataTable dt, string requestno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site)
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
            #endregion

            #region div header and footer
            #region header
            ////header div
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                  + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";
            //<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            //html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions
            //html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td>" + transactordepartment + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
            html += "</table>";
            html += "</div>";
            #endregion
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + requestno + "</u></td></tr>"
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

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }
    //public static string GenerateStockIssuanceFormRISStockRequest(DataTable dt, string requestno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td>" + transactordepartment + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + employeedepartment + "</u><br/>Office: <u>" + site + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + requestno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusYes = "1";
    //                string statusNo = "";
    //                if (Convert.ToInt32(dt.Rows[i]["Quantity"].ToString()) > 0)
    //                {
    //                }
    //                else
    //                {
    //                    statusYes = "";
    //                    statusNo = "1";
    //                }
    //                html += "<tr>"
    //                      //dothis
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + remarks + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateTransferFormITR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string fromloc, string toloc)
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
            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY TRANSFER REPORT<h3></td</tr>";

            // html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
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
            html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + fromloc + "<br/>To Accountable Officer/Agency/Fund Cluster : " + toloc + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
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
    //public static string GenerateTransferFormITR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 110px; }"
    //        + ".page-footer, .page-footer-space {height: 220px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
    //        //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
    //        //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
    //        //    + "</td></tr>";
    //        html += "<tr><td><table style='font-size:75%;';>";
    //        html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
    //        html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
    //        html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
    //        html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
    //        html += "</table></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion


    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
    //                  + "<tr>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
    //                  + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}

    //public static string GenerateFormWMR(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 280px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>WASTE MATERIALS REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
    //        html += "<tr><td style='height:35px;width:50%;border-top:1px solid;border-left:1px solid;' valign='top'; align='left'; >Certified Correct: </br><center><u>" + custodian + "</u><br>Signature over Printed Name of Supply<br/>and/or Property Custodian</br></center></td><td style='height:35px;width:50%;border-top:1px solid;border-right:1px solid;';  valign='top'; align='left';>Disposal Approved : </br><center><u>" + approvername + "</u><br>Signature over Printed Name of Head of<br/>Agency/Entity or his/her Authorized</br>Representative</center></td></tr>";
    //        html += "<tr><td colspan='2'; style='border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><center>CERTIFICATE OF INSPECTION</center></td></tr>";
    //        html += "<tr><td colspan='2'; style='border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><br/>I hereby that the property enumerated above was disposed of as follows:<br/><br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Destroyed <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at private sale <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Sold at public auction <br/>&emsp;&emsp;Item &nbsp;&nbsp; _________ &nbsp;&nbsp; Transferred without cost to <u>" + CompanyName + "</u><br/><br/></td></tr>";
    //        html += "<tr><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Certified Correct: </br><center><u>" + inspectorname + "</u><br>Signature over Printed Name of<br/>Inspection Officer</br></center></td><td valign='top'; align='left'; style='height:35px;width:50%; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>Witness Disposal : </br><center><u>" + certifiername + "</u><br>Signature over Printed Name of<br/>Witness</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>Place of Storage: <u>" + "" + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Date: <u>" + "" + "</u></td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='7';><b>ITEMS FOR DISPOSAL</b></td></tr>"
    //                  + "<tr>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item</b></td>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td rowspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td colspan='3'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Record of Sales</b></td>"
    //                  + "</tr><tr>"
    //                  + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Official Receipt</b></td>"
    //                  + "</tr><tr>"
    //                  + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No.</b></td>"
    //                  + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Date</b></td>"
    //                  + "<td style='text-align: center; border-right:1px solid;border-bottom:1px solid;'><b>Amount</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + i+1 + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
    //                      + "<td  style='width:15%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    //public static string GenerateFormROP(DataTable dt, string disposalno, string transactorname, string approvername, string inspectorname, string certifiername, string transactordepartment, string transactorposition, string disposaldate, string remarks, string custodian)
    //{ 
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 80px; }"
    //        + ".page-footer, .page-footer-space {height: 390px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h4>REPORT OF LOST, STOLEN, DAMAGED OR DESTROYED PROPERTY</h4></td></tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;';cellspacing='0';  width='100%'>";
    //        html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > <b>Circumstances:</b><center><br/><u>" + remarks + "</u><br/><br/></center></td></tr>";
    //        html += "<tr><td style='height:35px;width:65%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left';>"
    //            + "I hereby certify that the item/s and circumstances stated above are true and correct. <br/><br/><center><u>" + "" + "</u><br/>Signature over Printed Name of the Accountable Officer<br/><br/><u>" + "" + "</u></br>Date</center><br/><br/>Government Issued ID : _______________<br/> ID No. : ___________________________ </br> Date Issued : ______________________ </td>"
    //            + "<td style='height:35px;width:35%;border-left:1px solid;border-right:1px solid;'; valign='top'; align='left'; ><br/>Noted by:<br/><br/><center><u>" + "" + "</u><br/>Signature over Printed Name of the Immediate Supervisor<br/><u>" + "" + "</u><br/>Date</td></tr>";

    //        html += "<tr><td colspan='2'; style='height:35px;border-top:1px solid;border-left:1px solid;border-bottom:1px solid;border-right:1px solid;'; valign='top'; align='left'; > </br><b>SUBSCRIBED AND SWORN </b> to before me this _______ day of __________, affiant exhibiting the above goverment issued identification card.<br/> Doc. No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ________________ <br/> Page No. &nbsp;&nbsp;__________ &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Notary Public <br/> Book No. &nbsp;&nbsp;__________ <br/> Series of &nbsp;&nbsp;__________ <br/><br/></td></tr>";

    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>"
    //                + "<b>Department/Office : <u>" + transactordepartment + "</u></b><br/> <b>Accountable Officer : <u>" + transactorname + "</u></b><br/> <b>Designation : <u>" + transactorposition + "</u></b><br/>"
    //               + "Police Notified :  <input type='checkbox'> <b>Yes</b>&emsp;&emsp;&emsp;&emsp; Police Station: ______________ </br> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> <b>No</b> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Date : ______________ </td>"
    //               + "<td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;';><b>RLSDDSP No. : <u>" + "" + "</u></b><br/>"
    //               + "<b>RLSDDSP No. : <u>" + disposalno + "</u></b><br/>"
    //               + "<b>RLSDDSP Date. : <u>" + disposaldate + "</u></b><br/>"
    //               + "<b>PAR No. : <u>" + "" + "</u></b><br/>"
    //               + "<b>PAR Date : <u>" + "" + "</u></b></td><br/>"
    //               + "</tr>"

    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='3';>Status of Property : (check application box)<br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Lost &nbsp;&nbsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Damaged <br/> &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Stolen &nbsp; &emsp;&emsp;&emsp; <input type='checkbox'> &nbsp;&nbsp;&nbsp; Destroyed</td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Acquisition Cost</b></td>"
    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                     + "<td  style='width:20%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      + "<td  style='width:50%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='width:30%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='3' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateGoodsReceiveIAR(DataTable dt, int goodsreceiveid, string suppliername, string PONumber, string createddate)
    {

        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];
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

            //+ ".page-header, .page-header-space {height: 265px; }"
            //+ ".page-footer, .page-footer-space {height: 250px;}"
            //+ ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            //+ ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            //+ ".page-body {page-break-after: auto;}"
            //+ "@page {size: A4; margin: 10mm;}"

            //+ "@media print {"
            //    + "table {padding: 0px 10px 0px 0px;}"
            //    + "thead {display: table-header-group; }"
            //    + "tfoot {display: table-footer-group;}"
            //    + "body { margin: 0;}"
            //+ "}"
            //+ "</style >";
            #endregion

            #region div header and footer
            #region header
            //header div
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>";
            //html += "<tr><td><img src=\"../Image/DICT_LOGO.png\" style=\"width: 100px; height: 50px; \"/> </td><td align='center' style='font-size:15;'>INSPECTION AND ACCEPTANCE REPORT123123</td></tr>";
            //html += "<tr><td align='center' style='font-size:15;'><br><h3>INSPECTION AND ACCEPTANCE REPORT</h3></td></tr></table>";

            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            //html += "</div>";

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

            //<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>INSPECTION AND ACCEPTANCE REPORT<h3></td</tr>";
            // html += "<tr><td style='width:50%;text-align: left;font-size:13;'><br/><b>ASSET ISSUANCE REF. NO.:</b> <u>" + issuanceno + "</u> </br><b>ASSIGNEE NAME:</b> <u>" + employeename + "</u> </td><td style='text-align: left;font-size:13;'><br/><b>DATE:</b> <u>" + DateTime.Today.ToString("dd/MM/yyyy") + "</u> </br><b>DEPARTMENT: </b> <u>" + "" + "</u> </td></tr>";
            html += "<tr><td colspan='2' align='justify' style='font-size:12;'><br>Entity Name : " + CompanyName + "<br></td</tr></table>";

            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%';><center><i><b>INSPECTION</b></i></center></td><td width='50%';><center><i><b>ACCEPTANCE</b></i></center></td></tr>";
            html += "<tr><td width='50%';><b>Date Inspected : ________________________________ </b> <br/> <input type='checkbox'> Inspected, verified and found in order as to quantity and specifications </br> </br> </br> </br>  </br> <center>__________________________________<br/>Inspection Officer/Inspection Committee</center></td>"
                + "<td width='50%';><b>Date Received : ________________________________ </b> <br/> <input type='checkbox'> Complete </br> <input type='checkbox'> Partial (pls. specify quantity) </br> </br> </br>  </br> <center>__________________________________<br/>Supply and/or Property Custodian</center></td></tr>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>Supplier : : " + suppliername + "<br/>PO No./Date : " + PONumber + "<br/>Requisitioning Office/Dept. : " + "" + "<br/>Responsibility Center Code : " + "" + "<br/></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>IAR No. : " + goodsreceiveid + "<br/>Date : " + Convert.ToDateTime(createddate).ToString("MM/dd/yyyy HH:mm:ss") + "<br/>Invoice No. : " + invNo + "<br/>Date : " + Convert.ToDateTime(createddate).ToString("MM/dd/yyyy HH:mm:ss") + "<br/></td></tr>"
                      + "<tr>"
                      + "<td width='25%'; style='text-align: center;border-left:1px solid;'><b><i>Supply/<br/>Property No.</i></b></td>"
                      + "<td width='35%'; style='text-align: center;border-left:1px solid;'><b><i>Description</i></b></td>"
                      + "<td width='15%'; style='text-align: center;border-left:1px solid;'><b><i>Unit</i></b></td>"
                      + "<td width='25%'; style='text-align: center;border-left:1px solid;border-right:1px solid;'><b><i>Quantity</i></b></td>"

                      + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + " </td>"
                          + "<td  style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "</tr>";
                }
                html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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
    //public static string GenerateGoodsReceiveIAR(DataTable dt, int goodsreceiveid, string suppliername, string PONumber, string createddate)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INSPECTION AND ACCEPTANCE REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%';><center><i><b>INSPECTION</b></i></center></td><td width='50%';><center><i><b>ACCEPTANCE</b></i></center></td></tr>";
    //        html += "<tr><td width='50%';><b>Date Inspected : ________________________________ </b> <br/> <input type='checkbox'> Inspected, verified and found in order as to quantity and specifications </br> </br> </br> </br>  </br> <center>__________________________________<br/>Inspection Officer/Inspection Committee</center></td>"
    //            + "<td width='50%';><b>Date Received : ________________________________ </b> <br/> <input type='checkbox'> Complete </br> <input type='checkbox'> Partial (pls. specify quantity) </br> </br> </br>  </br> <center>__________________________________<br/>Supply and/or Property Custodian</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='2';>Supplier : : " + suppliername + "<br/>PO No./Date : " + PONumber + "<br/>Requisitioning Office/Dept. : " + "" + "<br/>Responsibility Center Code : " + "" + "<br/></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>IAR No. : " + goodsreceiveid + "<br/>Date : " + createddate + "<br/>Invoice No. : " + "" + "<br/>Date : " + "" + "<br/></td></tr>"
    //                  + "<tr>"
    //                  + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Stock/<br/>Property No.</i></b></td>"
    //                  + "<td width='35%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Description</i></b></td>"
    //                  + "<td width='15%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;'><b><i>Unit</i></b></td>"
    //                  + "<td width='25%'; style='text-align: center;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b><i>Unit</i></b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Quantity"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='4' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateIssuanceFormPARRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            //"<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT<h3></td</tr>";


            //   html += "<table style='width:100%;font-family: Arial;'>";
            html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
                + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>PAR Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
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
                          //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
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
            html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
                "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
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
    //public static string GenerateIssuanceFormPARRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 210px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        ////header div
    //        //html += "<div class='page-header'>";
    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"

    //      + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

    //        //"<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
    //        html += "<tr><td colspan='4' align='center' ><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT<h3></td</tr>";


    //        //   html += "<table style='width:100%;font-family: Arial;'>";
    //        html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>PAR Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
    //            "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                   + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
    //                      + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    ////public static string GenerateIssuanceFormPARRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 130px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY ACKNOWLEDGEMENT RECEIPT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td align='right' style='font-size:12;'>PAR No. : <u>" + issuanceno + "</u></td><td style='font-size:12;'><u>" + "" + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%'><br/><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u><br/>Signature over Printed Name of End User<br/><br/><br/><br/><u>" + employeeposition + "/" + employeedepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date<center><br/></td>" +
    //            "<td><br/>&emsp;<b>Issued by:</b><br/><br/><br/><center>&emsp;&emsp;&emsp;<u>" + transactorname + "</u><br/>Signature over Printed Name of Supply<br/> and/or Property Custodian<br/><br/><u>" + transactorposition + "|" + transactordepartment + "</u><br/>Position/Office<br/><br/><u>" + issueddate + "</u><br/>Date</center></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                   + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
    //                      + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
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
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            //"<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>INVENTORY CUSTODIAN SLIP<h3></td</tr>";


            //   html += "<table style='width:100%;font-family: Arial;'>";
            //html += "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
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
                      //+ "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
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
                          //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
                          //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
                          //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
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
            //html += "<hr style='border-top: 1px dashed'>";
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
    //public static string GenerateIssuanceFormICSRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 130px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY CUSTODIAN SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : &emsp;" + CompanyName + "</td></tr>"
    //            + "<tr><td style='font-size:12;'>Fund Cluster : </td><td style='font-size:12;'><u>" + "" + "</u></td><td style='font-size:12;'>ICS Mo. : </td><td style='font-size:12;'><u>" + issuanceno + "</u></td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td width='50%'><b>Received from:</b><br/><br/><br/><center><u>" + transactorname + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + transactorposition + "/" + transactordepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td>" +
    //            "<td width='50%'><b>Received by:</b><br/><br/><br/><center><u>" + employeename + "</u></center><center>Signature over Printed Name</center><br/><center><u>" + employeeposition + "|" + employeedepartment + "</u></center><center>Position/Office</center><br/><center><u>" + issueddate + "</u><center><center>Date</center><br/></td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td colspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td rowspan='2' style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Estimated Useful Life</b></td>"
    //                  + "</tr><tr><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td><td style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Total Cost</b></td></tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      //+ "<td style='width:10%;'>1 " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["ModelName"].ToString() + "</td>"
    //                      //+ "<td >" + dt.Rows[i]["Description"].ToString() + "</td>"
    //                      //+ "<td style='width:15%;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
    //                      //+ "<td >" + String.Format("{0:N2}", Convert.ToDecimal(dt.Rows[i]["SalePrice"])) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>1 " + "" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["UsefulLife"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='7' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateTransferFormITRIssuanceRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"

          + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"

            + "<tr><td align='center' style='font-size:15;'><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
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
            html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
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
    //public static string GenerateTransferFormITRIssuanceRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 110px; }"
    //        + ".page-footer, .page-footer-space {height: 220px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>INVENTORY TRANSFER REPORT</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : " + CompanyName + "</td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions //signatories
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td><b>Reason/s for Transfer:</b><br/><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><center>__________________________________________________________________________</center><br/></td></tr>";
    //        //html += "<tr><td></br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Released/Issued by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Received by:<br/>"
    //        //                  + "Signature: &emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Printed Name: &nbsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Designation: &emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/>"
    //        //                  + "Date &nbsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;______________________________&emsp;&emsp;&emsp;&emsp;_______________________&emsp;&emsp;___________________________<br/><br/>"
    //        //    + "</td></tr>";
    //        html += "<tr><td><table style='font-size:75%;';>";
    //        html += "<tr><td width='23%';></td><td width='30%';>Approved by</td><td width='30%';>Released/Issued by:</td><td width='30%';>Received by:</td></tr>";
    //        html += "<tr><td>Signature :</td><td>______________</td><td>______________</td><td>______________</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td>" + approvername + "</td><td>" + transactorname + "</td><td>" + employeename + "</td></tr>";
    //        html += "<tr><td>Designation :</td><td></td><td>" + transactorposition + "</td><td>" + employeeposition + "</td></tr>";
    //        html += "<tr><td>Date :</td><td>" + issueddate + "</td><td>" + issueddate + "</td><td>" + issueddate + "</td></tr>";
    //        html += "</table></table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion


    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='4';>From Accountable Officer/Agency/Fund Cluster : " + transactorname + "<br/>To Accountable Officer/Agency/Fund Cluster : " + employeename + "</td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='2'>ITR No : " + issuanceno + "<br/>Date : " + issueddate + "</td></tr>"
    //                 + "<tr><td style='text-align: left; border-left:1px solid; border-right:1px solid;' colspan='6';>Transfer Type: (check only one)<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Donation&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Relocate<br/>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type='checkbox'>Reassignment&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<input type='checkbox'>Others (Specify) ____________</td></tr>"
    //                  + "<tr>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Acquired</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
    //                  + "<td width='10%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No./Date</b></td>"
    //                  + "<td width='30%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
    //                  + "<td width='15%'; style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Condition of PPE</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "/" + Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd") + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='6' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
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
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";

            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                   + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            //<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

            //html += "<table style='width:100%;font-family: Arial;'>"
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
            //html += "<hr style = 'border-top: 1px dashed' > ";
            html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: " + employeename + "." + remarks + "<br/><br/><br/>";
            //html += "<hr style='border-top: 1px dashed'>";
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
    //public static string GenerateRISFormIssuance(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site, string approverpos, string issuedby, string issuedpos)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";

    //        #region styles

    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 200px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";

    //        #endregion styles

    //        #region div header and footer

    //        #region header

    //        //header div
    //        //html += "<div class='page-header'>";
    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";

    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //               + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

    //        //<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
    //        html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";

    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer

    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + issuedby + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td style='text-align: center;'>" + approverpos + "</td><td style='text-align: center;'>" + issuedpos + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";

    //        #endregion footer

    //        #endregion div header and footer

    //        #region header placeholder

    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";

    //        #endregion header placeholder

    //        #region body placeholder

    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusNo = "";
    //                string statusYes = "1";
    //                if (dt.Rows[i]["Status"].ToString() == "Issued")
    //                {
    //                    statusNo = "1";
    //                    statusYes = "";
    //                }
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";

    //        #endregion body placeholder

    //        #region footer placeholder

    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";

    //        #endregion footer placeholder

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //} //public static string GenerateRISFormIssuance(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername,string site)
    ////{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + custodian + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td></td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusNo = "";
    //                string statusYes = "1";
    //                if (dt.Rows[i]["Status"].ToString() == "Issued")
    //                {
    //                    statusNo = "1";
    //                    statusYes = "";
    //                }
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}
    public static string GenerateRISFormIssuanceRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site, string IssuedBy, string IssuedByPos, string approverpos)
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
            #endregion

            #region div header and footer
            #region header
            //header div
            //html += "<div class='page-header'>";
            //html += "<table style='width:100%;font-family: Arial;'>"
            //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                 + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "'; width='50%'; style='width: 150px; height: 150px' /></td></tr>";

            //<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
            html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";


            // html += "<table style='width:100%;font-family: Arial;'>"
            html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
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
                html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
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
            html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
            html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + transactorname + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
            html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td style='text-align: center;'>" + approverpos + "</td><td style='text-align: center;'>" + transactordepartment + "</td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
            html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
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

    //public static string GenerateRISFormIssuanceRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 200px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        //html += "<div class='page-header'>";
    //        //html += "<table style='width:100%;font-family: Arial;'>"
    //        //    + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //             + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "';  width='50%'; style='width: 150px; height: 150px' /></td</tr>";

    //        //<tr><td colspan='1' align='left'; '><img  src='../Pictures/DICT_LOGO.png' style='width: 200px; height :100px; '></td><td colspan='2' align='left'; font-size:13;><table><center><tr ><center>Department of Information and Communications Technology</center></tr><tr><center>ADDRESS</center></tr><tr><center>PHONE NUMBER</center></tr></table></td></tr>";
    //        html += "<tr><td colspan='4' align='center' ><br><h3>REQUISITION AND ISSUE SLIP<h3></td</tr>";


    //        // html += "<table style='width:100%;font-family: Arial;'>"
    //        html += "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + custodian + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td></td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusNo = "";
    //                string statusYes = "1";
    //                if (dt.Rows[i]["Status"].ToString() == "Issued")
    //                {
    //                    statusNo = "1";
    //                    statusYes = "";
    //                }
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}


    //public static string GenerateRISFormIssuanceRequest(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string site)
    //{
    //    try
    //    {
    //        MiscBll miscBll = new MiscBll();
    //        MiscEntity miscEntity = new MiscEntity();

    //        string x = miscBll.GetCompanyInformation(Common.ConnString);
    //        CompanyName = x.Split('|')[0];
    //        CompanyContactNumber = x.Split('|')[3];
    //        CompanyAddress = x.Split('|')[5];

    //        string html = "";
    //        #region styles
    //        //style
    //        html += "<style>"
    //        + ".page-header, .page-header-space {height: 113px; }"
    //        + ".page-footer, .page-footer-space {height: 250px;}"
    //        + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
    //        + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
    //        + ".page-body {page-break-after: auto;}"
    //        + "@page {size: A4; margin: 10mm;}"

    //        + "@media print {"
    //            + "table {padding: 0px 10px 0px 0px;}"
    //            + "thead {display: table-header-group; }"
    //            + "tfoot {display: table-footer-group;}"
    //            + "body { margin: 0;}"
    //        + "}"
    //        + "</style >";
    //        #endregion

    //        #region div header and footer
    //        #region header
    //        //header div
    //        html += "<div class='page-header'>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td align='center' style='font-size:15;'><br><h3>REQUISITION AND ISSUE SLIP</h3></br></td</tr></table>";
    //        html += "<table style='width:100%;font-family: Arial;'>"
    //            + "<tr><td style='font-size:12;'>Entity Name : <u>" + CompanyName + "</u></td><td style='text-align: right;font-size:12;'>Fund Cluster : _______________ </td></tr></table>";
    //        html += "</div>";

    //        #endregion header

    //        #region footer
    //        //footer div
    //        html += "<div class='page-footer'>";
    //        //terms and conditions
    //        //html += "<hr style = 'border-top: 1px dashed' > ";
    //        html += "<p style='font-size:9.5px; font-family: Arial;'>Purpose: <br/><br/><br/>";
    //        //html += "<hr style='border-top: 1px dashed'>";
    //        html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
    //        html += "<tr><td style='height:35px;'; valign='bottom'; align='left'; >Signature :</td><td valign='top'; align='left';>Requested by</td><td valign='top'; align='left'>Approved by</td><td valign='top'; align='left'>Issued by</td><td valign='top'; align='left'>Recieved by</td></tr>";
    //        html += "<tr><td>Printed Name :</td><td style='text-align: center;'>" + employeename + "</td><td style='text-align: center;'>" + approvername + "</td><td style='text-align: center;'>" + custodian + "</td><td style='text-align: center;'>" + employeename + "</td></tr>";
    //        html += "<tr><td >Designation :</td><td style='text-align: center;'>" + employeeposition + "</td><td></td><td></td><td style='text-align: center;'>" + employeeposition + "</td></tr>";
    //        html += "<tr><td >Date :</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td><td style='text-align: center;'>" + issueddate + "</td></tr>";
    //        html += "</table>";
    //        html += "</div>";
    //        #endregion
    //        #endregion

    //        #region header placeholder
    //        //table header
    //        html += "<table style='width:100%;' cellspacing='0'><thead><tr><td><div class='page-header-space'>";
    //        html += "</div></td></tr></thead>";
    //        #endregion

    //        #region body placeholder
    //        //table body
    //        html += "<tbody style='font-family: Arial; font-size:12;'><tr><td><div class='page-body' style='line-height: 1;'>";
    //        if (dt.Rows.Count > 0)
    //        {
    //            html += "<tr><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;' colspan='5';>Division: <u>" + site + "</u><br/>Office: <u>" + employeedepartment + "</u></td><td style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;' colspan='3'>Responsibility Center Code:<br/>RIS No: <u>" + issuanceno + "</u></td></tr>"
    //                 + "<tr><td style='text-align: center; border-left:1px solid;' colspan='4';><i><b>Requisition</b></i></td><td style='text-align: center; border-left:1px solid;'  colspan='2'><i><b>Stock Available?</b></i></td><td style='text-align: center; border-left:1px solid;border-right:1px solid;'  colspan='2'><i><b>Issue</b></i></td></tr>"
    //                  + "<tr>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Stock No.</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Description</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Yes</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>No</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity</b></td>"
    //                  + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"

    //                  + "</tr>";
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                string statusNo = "";
    //                string statusYes = "1";
    //                if (dt.Rows[i]["Status"].ToString() == "Issued")
    //                {
    //                    statusNo = "1";
    //                    statusYes = "";
    //                }
    //                html += "<tr>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ItemCode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["UOMcode"].ToString() + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["Description"].ToString() + " </td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusYes + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + statusNo + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
    //                      + "<td  style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "." + dt.Rows[i]["Remarks"].ToString() + "</td>"
    //                      + "</tr>";
    //            }
    //            html += "<tr><td colspan='8' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
    //        }
    //        else
    //        {
    //            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
    //        }
    //        html += "</div></td></tr></tbody>";
    //        #endregion


    //        #region footer placeholder
    //        //table footer
    //        html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
    //        html += "</div></td></tr></tfoot></table>";
    //        #endregion

    //        return html;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return "";
    //    }
    //}


    public static string GeneratePPELedgerCard(DataTable dt)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 88px; }"
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>PROPERTY, PLANT AND EQUIPMENT<b><br/><br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:13;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:13;'>Fund Cluster: ______________________</td></tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            ////html += "<tr><td style='width:60%'><br/><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</br></br></br></br>&emsp;&emsp;Requested by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:<br/><br/>&emsp;&emsp;&emsp;<u>" + transactorname + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of <br/>&emsp;&emsp;&emsp;&emsp;&emsp; Accountable Officer) &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Authorized Officer) <br/><br/>&emsp;&emsp;&emsp;<u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;___________________________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Accountable Official)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Authorized Official)<br/><br/></td>"
            ////    + "<td align='left';valign='top' style='width:40%'><br/> I CERTIFY that I have inspected each &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;I CERTIFY that I have <br/> and every article enumerated in this report,&emsp;&emsp;&emsp;&emsp;witnessed the disposition of the<br/>and that the disposition made therof was, &emsp;&emsp;&emsp;&emsp; articles enumerated on this <br/> in my judgement, the best for the public &emsp;&emsp;&emsp;&emsp;&emsp; report this __day of <br/>interest. &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_____________,______<br/><br/><br/>________________________________&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;________________________<br/>&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of<br>&emsp;&emsp;&emsp;&emsp;&emsp;Inspection Officer)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Witness)</td></tr></table>";
            //html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
            //    + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + "" + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + inspectorname + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + certifiername + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                    + "<td colspan='8';  style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'> Property, Plant and Equipment: Property Number</td> <td colspan=3; rowspan=2;  style='text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'> Object Account Code: ___________________________ <br/> Estimated Useful Life: ___________________________ <br/> Rate of Depreciation: ___________________________ </td> "
                    + "</tr><tr><td colspan='8';  style='text-align: left;border-bottom:1px solid;border-left:1px solid;'> Description: " + "" + "</td>"
                    + "</tr>"
                + "<tr>"
                 + "<td rowspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Date</b></td>"
                 + "<td rowspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Reference</b></td>"
                 + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Receipt</b></td>"
                 + "<td rowspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Depriciation</b></td>"
                 + "<td rowspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Accumulated Impairment Losses</b></td>"
                 + "<td rowspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Issues/Transfers/Adjustment/s </b></td>"
                 + "<td rowspan='2'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;'><b>Adjusted Cost </b></td>"
                 + "<td colspan='3'; style='text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Repair History</b></td>"
                 + "</tr>"
                 + "<tr><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Unit Cost</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Total Cost</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Nature of Repair</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;border-right:1px solid;' ;>Amount</td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int AD = Convert.ToInt32(dt.Rows[i]["AcquiredCost"]) - Convert.ToInt32(dt.Rows[i]["ValueDeducted"]);
                    html += "<tr>"
                          + "<td  style='width:9%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransactionNumber"].ToString() + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";


                    html += "<tr>"
                          + "<td  style='width:9%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransactionNumber"].ToString() + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TaskName"].ToString() + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";


                    html += "<tr>"
                          + "<td  style='width:9%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + AD.ToString() + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                          + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + "" + "</td>"
                          + "</tr>";
                }

                // html += "<tr><td colspan='11' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
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


    public static string GenerateRSPI(DataTable dt)
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
            + ".page-header, .page-header-space {height: 127px; }"
            + ".page-footer, .page-footer-space {height: 180px;}"
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
                + "<tr><td align='center' style='font-size:15;'><br><h3>REPORT OF SEMI-EXPENDABLE PROPERTY ISSUED</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='width:50%;font-size:12;'>Entity Name : <u>" + CompanyName + "</td><td style='font-size:12;'>Serial No. : <u>" + "" + "</td></tr>"
                + "<tr><td style='width:50%;font-size:12;'>Fund Cluster : <u>" + "" + "</td><td style='font-size:12;'>Date. : <u>" + "" + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'> <center>I hereby certify to the correctness of the above information.<br/>________________________________________<br/>Signature over Printed Name of Supply and/or<br/> Property Custodian</center></td>" +
                "<td width='50%'><center><table style='font-size:75%;'><tr><td width='65%'>Posted by:<br/><br/><center>______________________<br/>Signature over Printed Name of <br/>Designated Accounting</center></td><td width='35%'><br/><br/><br/><center>__________<br/></center></td></tr></table></center></td>";
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
                html += "<tr><td colspan='6'; style='text-align: center; border-top:1px solid;border-right:1px solid;border-left:1px solid;'><i>To be filled up by the Supply and/or Property Division/Unit</i></td><td colspan='6'; style='text-align: center; border-top:1px solid;border-right:1px solid;'><i>To be filled up by the Accounting Division/Unit</i></td></tr>";
                html += "<tr>"
                       + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>ICS No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Responsibility Center Code </b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Semi-expendable Property No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Quantity Issued</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Unit Cost</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Amount</b></td>"
                          + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double Amount = Convert.ToDouble(dt.Rows[i]["Amount"]) * Convert.ToDouble(dt.Rows[i]["Quantity"]);
                    html += "<tr>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["IssuanceNo"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["UOMcode"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Quantity"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Amount"].ToString() + "</td>"
                          + "<td  style='width:12%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + BasePage.ConvertToMoneyFormat(Amount.ToString()) + "</td>"
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

    //PTR

    public static string ExportToExcelPTR(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername, string fromloc, string toloc)
    {
        //Get company info
        MiscBll miscBll = new MiscBll();
        //string x = miscBll.GetCompanyInformation(Common.ConnString);
        //CompanyName = x.Split('|')[0];
        //CompanyContactNumber = x.Split('|')[3];
        //CompanyAddress = x.Split('|')[5];


        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("PTR");
            //TITLE START
            var IARText = worksheet.Range("A4:I4").Merge();
            worksheet.Cell(4, 1).Value = "PROPERTY TRANSFER REPORT";
            IARText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            IARText.Style.Font.Bold = true;
            IARText.Style.Font.FontSize = 16;
            //TITLE END --------------------------------------------------------------------------

            //HEADER START
            //var ClusText = worksheet.Cell("A6");
            var ClusText1 = worksheet.Cell("G6");

            var cell1 = worksheet.Cell(6, 1);
            cell1.RichText.AddText("Entity Name:  ");
            cell1.RichText.AddText(CompanyName).SetUnderline(XLFontUnderlineValues.Single);

            worksheet.Cell(6, 1).Value = "Entity Name: ";
            worksheet.Cell(6, 7).Value = "Fund Cluster: ";
            //ClusText.Style.Font.Bold = true;
            ClusText1.Style.Font.Bold = true;


            worksheet.Cell(8, 1).Value = "From Accountable Officer/Agency/Fund Cluster :  " + fromloc;
            worksheet.Cell(9, 1).Value = "To Accountable Officer/Agency/Fund Cluster :  " + toloc;


            worksheet.Cell(8, 8).Value = "PTR No. :  " + issuanceno;
            worksheet.Cell(9, 8).Value = "Date :  " + issueddate;

            worksheet.Cell(11, 1).Value = "Transfer Type: (check only one)";

            worksheet.Cell(13, 2).Value = "☐";
            worksheet.Cell(13, 3).Value = "Donation";

            worksheet.Cell(13, 5).Value = "☐";
            worksheet.Cell(13, 6).Value = "Relocate";

            worksheet.Cell(14, 2).Value = "☐";
            worksheet.Cell(14, 3).Value = "Reassignment ";

            worksheet.Cell(14, 5).Value = "☐";
            worksheet.Cell(14, 6).Value = "Others (Specify) _________________";




            //borderu
            var B1 = worksheet.Range("A8:I10");
            var B1A = B1.Style.Border;
            B1A.OutsideBorder = XLBorderStyleValues.Thin;


            var B2 = worksheet.Range("G8:G10");
            var B2A = B2.Style.Border;
            B2A.RightBorder = XLBorderStyleValues.Thin;

            var B3 = worksheet.Range("A11:I15");
            var B3A = B3.Style.Border;
            B3A.OutsideBorder = XLBorderStyleValues.Thin;


            var B4 = worksheet.Range("A16:I17");
            var B4A = B4.Style.Border;
            B4A.OutsideBorder = XLBorderStyleValues.Thin;

            var B5 = worksheet.Range("B16:C17");
            var B5A = B5.Style.Border;
            B5A.RightBorder = XLBorderStyleValues.Thin;
            B5A.LeftBorder = XLBorderStyleValues.Thin;

            var B6 = worksheet.Range("D16:G17");
            var B6A = B6.Style.Border;
            B6A.RightBorder = XLBorderStyleValues.Thin;

            var B7 = worksheet.Range("H16:H17");
            var B7A = B7.Style.Border;
            B7A.RightBorder = XLBorderStyleValues.Thin;




            var a8 = worksheet.Range("A16:A17").Merge();
            worksheet.Cell(16, 1).Value = "Date Acquired";
            a8.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a8.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a8.Style.Font.Bold = true;
            a8.Style.Alignment.WrapText = true;


            var a9 = worksheet.Range("B16:C17").Merge();
            worksheet.Cell(16, 2).Value = "Property No.";
            a9.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a9.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a9.Style.Font.Bold = true;
            a9.Style.Alignment.WrapText = true;

            var a10 = worksheet.Range("D16:G17").Merge();
            worksheet.Cell(16, 4).Value = "Description";
            a10.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a10.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a10.Style.Font.Bold = true;
            a10.Style.Alignment.WrapText = true;

            var a11 = worksheet.Range("H16:H17").Merge();
            worksheet.Cell(16, 8).Value = "Amount";
            a11.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a11.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a11.Style.Font.Bold = true;
            a11.Style.Alignment.WrapText = true;

            var a12 = worksheet.Range("I16:I17").Merge();
            worksheet.Cell(16, 9).Value = "Condition of \nPPE";
            a12.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a12.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a12.Style.Font.Bold = true;
            a12.Style.Alignment.WrapText = true;

            //HEADER END --------------------------------------------------------------------------


            //DETAILS START ------------------------------------------------------------------------


            int countMax = 42;

            int SBorder = 18;

            if (dt.Rows.Count > 23)
            {
                countMax = dt.Rows.Count + 18;
            }

            //line pababa
            var DS = worksheet.Range("A" + SBorder + ":I" + countMax + "");
            var DSA = DS.Style.Border;
            DSA.OutsideBorder = XLBorderStyleValues.Thin;

            var DS1 = worksheet.Range("A" + SBorder + ":A" + countMax + "");
            var DS1A = DS1.Style.Border;
            DS1A.RightBorder = XLBorderStyleValues.Thin;

            var DS2 = worksheet.Range("C" + SBorder + ":C" + countMax + "");
            var DS2A = DS2.Style.Border;
            DS2A.RightBorder = XLBorderStyleValues.Thin;

            //var DS3 = worksheet.Range("D" + SBorder + ":G" + countMax + "");
            //var DS3A = DS3.Style.Border;
            //DS3A.RightBorder = XLBorderStyleValues.Thin;

            //var DS4 = worksheet.Range("D" + SBorder + ":D" + countMax + "");
            //var DS4A = DS4.Style.Border;
            //DS4A.RightBorder = XLBorderStyleValues.Thin;

            //var DS5 = worksheet.Range("E" + SBorder + ":E" + countMax + "");
            //var DS5A = DS5.Style.Border;
            //DS5A.RightBorder = XLBorderStyleValues.Thin;

            //var DS7 = worksheet.Range("F" + SBorder + ":F" + countMax + "");
            //var DS7A = DS7.Style.Border;
            //DS7A.RightBorder = XLBorderStyleValues.Thin;

            var DS8 = worksheet.Range("G" + SBorder + ":G" + countMax + "");
            var DS8A = DS8.Style.Border;
            DS8A.RightBorder = XLBorderStyleValues.Thin;

            var DS9 = worksheet.Range("H" + SBorder + ":H" + countMax + "");
            var DS9A = DS9.Style.Border;
            DS9A.RightBorder = XLBorderStyleValues.Thin;

            //var DS10 = worksheet.Range("I" + SBorder + ":I" + countMax + "");
            //var DS10A = DS10.Style.Border;
            //DS10A.RightBorder = XLBorderStyleValues.Thin;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var d1 = worksheet.Cell(SBorder, 1);
                    worksheet.Cell(SBorder, 1).Value = Convert.ToDateTime(dt.Rows[i]["DateOfAcquisition"].ToString()).ToString("yyyy-MM-dd");
                    d1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    d1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    d1.Style.Alignment.WrapText = true;

                    var d2 = worksheet.Cell(SBorder, 2);
                    worksheet.Cell(SBorder, 2).Value = dt.Rows[i]["PropertyNumber"].ToString();
                    d2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    d2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    d2.Style.Alignment.WrapText = true;

                    worksheet.Range("B" + SBorder + ":C" + SBorder).Merge();

                    var d3 = worksheet.Cell(SBorder, 4);
                    worksheet.Cell(SBorder, 4).Value = dt.Rows[i]["Description"].ToString();
                    d3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    d3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    d3.Style.Alignment.WrapText = true;

                    worksheet.Range("D" + SBorder + ":G" + SBorder).Merge();


                    //var d4 = worksheet.Cell(SBorder, 1);
                    //worksheet.Cell(SBorder, 4).Value = dt.Rows[i]["Description"].ToString();
                    //d4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //d4.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //d4.Style.Alignment.WrapText = true;

                    //worksheet.Range("D" + SBorder + ":G" + SBorder).Merge();

                    var d5 = worksheet.Cell(SBorder, 8);
                    worksheet.Cell(SBorder, 8).Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    d5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    d5.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    d5.Style.Alignment.WrapText = true;

                    var d6 = worksheet.Cell(SBorder, 9);
                    worksheet.Cell(SBorder, 9).Value = "";
                    d6.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    d6.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    d6.Style.Alignment.WrapText = true;



                    worksheet.Range("A" + SBorder + ":I" + SBorder + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A" + SBorder + ":I" + SBorder + "").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;




                    SBorder += 1;
                }
            }
            //DETAILS END -------------------------------------------------------------------------------

            //FOOTER START


            var FooterLeftRange = worksheet.Range("A" + (countMax + 1) + ":I" + (countMax + 7) + "");
            var FooterLeftBorder = FooterLeftRange.Style.Border;
            FooterLeftBorder.OutsideBorder = XLBorderStyleValues.Thin;

            var FooterBottRange = worksheet.Range("A" + (countMax + 8) + ":I" + (countMax + 14) + "");
            var FooterBottBorder = FooterBottRange.Style.Border;
            FooterBottBorder.OutsideBorder = XLBorderStyleValues.Thin;

            //var f1 = worksheet.Range("A" + (countMax + 6) + ":I" + (countMax + 13) + "");
            //var f1a = f1.Style.Border;
            //f1a.OutsideBorder = XLBorderStyleValues.Thin;

            //var f2 = worksheet.Range("A" + (countMax + 14) + ":E" + (countMax + 16) + "");
            //var f2a = f2.Style.Border;
            //f2a.OutsideBorder = XLBorderStyleValues.Thin;

            //var f3 = worksheet.Range("F" + (countMax + 14) + ":I" + (countMax + 16) + "");
            //var f3a = f3.Style.Border;
            //f3a.OutsideBorder = XLBorderStyleValues.Thin;



            var b0 = worksheet.Cell(countMax + 1, 1);
            worksheet.Cell(countMax + 1, 1).Value = "Reason for Transfer:";
            //b0.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //b0.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            b0.Style.Font.Bold = true;



            var b1 = worksheet.Range("A" + (countMax + 2) + ":I" + (countMax + 2) + "").Merge();
            worksheet.Cell((countMax + 2), 1).Value = "___________________________________________________________________________";
            b1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b2 = worksheet.Range("A" + (countMax + 3) + ":I" + (countMax + 3) + "").Merge();
            worksheet.Cell((countMax + 3), 1).Value = "___________________________________________________________________________";
            b2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b3 = worksheet.Range("A" + (countMax + 4) + ":I" + (countMax + 4) + "").Merge();
            worksheet.Cell((countMax + 4), 1).Value = "___________________________________________________________________________";
            b3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b4 = worksheet.Range("A" + (countMax + 5) + ":I" + (countMax + 5) + "").Merge();
            worksheet.Cell((countMax + 5), 1).Value = "___________________________________________________________________________";
            b4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b5 = worksheet.Range("A" + (countMax + 6) + ":I" + (countMax + 6) + "").Merge();
            worksheet.Cell((countMax + 6), 1).Value = "___________________________________________________________________________";
            b5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //g1
            var g1 = worksheet.Cell(countMax + 9, 2);
            worksheet.Cell(countMax + 9, 2).Value = "Approved by:";
            g1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g1.Style.Font.Bold = true;

            var g2 = worksheet.Cell(countMax + 9, 6);
            worksheet.Cell(countMax + 9, 6).Value = "Released/Issued by:";
            g2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g2.Style.Font.Bold = true;

            var g3 = worksheet.Cell(countMax + 9, 8);
            worksheet.Cell(countMax + 9, 8).Value = "Received by:";
            g3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g3.Style.Font.Bold = true;

            //g2
            var g4 = worksheet.Cell(countMax + 10, 1);
            worksheet.Cell(countMax + 10, 1).Value = "Signature :";
            g4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g4.Style.Font.Bold = true;

            var g5 = worksheet.Cell(countMax + 11, 1);
            worksheet.Cell(countMax + 11, 1).Value = "Printed Name :";
            g5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g5.Style.Font.Bold = true;

            var g6 = worksheet.Cell(countMax + 12, 1);
            worksheet.Cell(countMax + 12, 1).Value = "Designation :";
            g6.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g6.Style.Font.Bold = true;

            var g7 = worksheet.Cell(countMax + 13, 1);
            worksheet.Cell(countMax + 13, 1).Value = "Date :";
            g7.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            g7.Style.Font.Bold = true;



            //g3
            var g8 = worksheet.Range("B" + (countMax + 10) + ":C" + (countMax + 10) + "").Merge();
            worksheet.Cell(countMax + 10, 2).Value = "________________________";
            //worksheet.Cell(countMax + 10, 2).RichText.AddText(approvername).SetUnderline(XLFontUnderlineValues.Single);
            g8.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            var g9 = worksheet.Range("B" + (countMax + 11) + ":C" + (countMax + 11) + "").Merge();
            worksheet.Cell(countMax + 11, 2).RichText.AddText(approvername).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 11, 2).Value = "________________________";
            g9.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g9.Style.Font.Bold = true;

            var g10 = worksheet.Range("B" + (countMax + 12) + ":C" + (countMax + 12) + "").Merge();
            worksheet.Cell(countMax + 12, 2).RichText.AddText(approvername).SetUnderline(XLFontUnderlineValues.Single);
            worksheet.Cell(countMax + 12, 2).Value = "";
            g10.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g10.Style.Font.Bold = true;

            var g11 = worksheet.Range("B" + (countMax + 13) + ":C" + (countMax + 13) + "").Merge();
            worksheet.Cell(countMax + 13, 2).RichText.AddText(issueddate).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 13, 2).Value = "________________________";
            g11.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g11.Style.Font.Bold = true;

            //g4


            var g12 = worksheet.Cell(countMax + 10, 6);
            //worksheet.Cell(countMax + 10, 2).RichText.AddText(approvername).SetUnderline(XLFontUnderlineValues.Single);
            worksheet.Cell(countMax + 10, 6).Value = "________________________";
            g12.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g12.Style.Font.Bold = true;

            var g13 = worksheet.Cell(countMax + 11, 6);
            worksheet.Cell(countMax + 11, 6).RichText.AddText(transactorname).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 11, 6).Value = "________________________";
            g13.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g13.Style.Font.Bold = true;

            var g14 = worksheet.Cell(countMax + 12, 6);
            worksheet.Cell(countMax + 12, 6).RichText.AddText(transactorposition).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 12, 6).Value = "________________________";
            g14.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g14.Style.Font.Bold = true;

            var g15 = worksheet.Cell(countMax + 13, 6);
            worksheet.Cell(countMax + 13, 6).RichText.AddText(issueddate).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 13, 6).Value = "________________________";
            g15.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g15.Style.Font.Bold = true;


            //g5

            var g16 = worksheet.Range("H" + (countMax + 10) + ":I" + (countMax + 10) + "").Merge();
            //worksheet.Cell(countMax + 10, 2).RichText.AddText(approvername).SetUnderline(XLFontUnderlineValues.Single);
            worksheet.Cell(countMax + 10, 8).Value = "________________________";
            g16.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var g17 = worksheet.Range("H" + (countMax + 11) + ":I" + (countMax + 11) + "").Merge();
            worksheet.Cell(countMax + 11, 8).RichText.AddText(employeename).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 11, 8).Value = "________________________";
            g17.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g3.Style.Font.Bold = true;

            var g18 = worksheet.Range("H" + (countMax + 12) + ":I" + (countMax + 12) + "").Merge();
            worksheet.Cell(countMax + 12, 8).RichText.AddText(employeeposition).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 12, 8).Value = "________________________";
            g18.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g3.Style.Font.Bold = true;

            var g19 = worksheet.Range("H" + (countMax + 13) + ":I" + (countMax + 13) + "").Merge();
            worksheet.Cell(countMax + 13, 8).RichText.AddText(issueddate).SetUnderline(XLFontUnderlineValues.Single);
            //worksheet.Cell(countMax + 13, 8).Value = "________________________";
            g19.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            g19.Style.Font.Bold = true;




            //FOOTER END --------------------------------------------------------------------------

            //ADJUST ALL CELLS ACCORDING TO TEXT INSIDE
            foreach (var column in worksheet.ColumnsUsed())
            {
                column.AdjustToContents();
            }


            worksheet.Column("A").Width = 14.14;
            worksheet.Column("B").Width = 3.57;
            worksheet.Column("C").Width = 13.14;
            worksheet.Column("D").Width = 4.14;
            worksheet.Column("E").Width = 2;
            worksheet.Column("F").Width = 26.43;
            worksheet.Column("G").Width = 1.57;
            worksheet.Column("H").Width = 11.71;
            worksheet.Column("I").Width = 13.86;

            worksheet.Row(7).Height = 6;
            worksheet.Row(8).Height = 20.25;
            worksheet.Row(10).Height = 8;
            worksheet.Row(12).Height = 4.5;
            worksheet.Row(15).Height = 4.5;
            worksheet.Row(56).Height = 8;

            //SAVE PATH
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.SaveAs(ms);

                byte[] output = ms.ToArray();
                return Convert.ToBase64String(output, 0, output.Length);
            }
        }
    }
    //RIS

    public static string GenerateregSEPI(DataTable dt)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            CompanyName = x.Split('|')[0];
            CompanyContactNumber = x.Split('|')[3];
            CompanyAddress = x.Split('|')[5];

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 110px; }"
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
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>REGISTRY OF SEMI-EXPENDABLE PROPERTY ISSUED<b><br/><br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:13;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:13;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td align='left' style='font-size:13;'>Semi-expendable Property: <u>" + "______________________" + "</u></td><td align='right' style='font-size:13;'>Sheet No.: ______________________</td></tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            ////html += "<tr><td style='width:60%'><br/><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</br></br></br></br>&emsp;&emsp;Requested by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:<br/><br/>&emsp;&emsp;&emsp;<u>" + transactorname + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of <br/>&emsp;&emsp;&emsp;&emsp;&emsp; Accountable Officer) &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Authorized Officer) <br/><br/>&emsp;&emsp;&emsp;<u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;___________________________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Accountable Official)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Authorized Official)<br/><br/></td>"
            ////    + "<td align='left';valign='top' style='width:40%'><br/> I CERTIFY that I have inspected each &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;I CERTIFY that I have <br/> and every article enumerated in this report,&emsp;&emsp;&emsp;&emsp;witnessed the disposition of the<br/>and that the disposition made therof was, &emsp;&emsp;&emsp;&emsp; articles enumerated on this <br/> in my judgement, the best for the public &emsp;&emsp;&emsp;&emsp;&emsp; report this __day of <br/>interest. &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_____________,______<br/><br/><br/>________________________________&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;________________________<br/>&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of<br>&emsp;&emsp;&emsp;&emsp;&emsp;Inspection Officer)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Witness)</td></tr></table>";
            //html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
            //    + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + "" + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + inspectorname + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + certifiername + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
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
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";

            html += "<tr>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Date</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Reference</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Estimated Useful Life</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Issued</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Returned</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Re-issued</b></td>"
             + "<td style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Disposed</b></td>"
             + "<td style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Balance</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"
             + "</tr>"
             + "<tr>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>ICS/RRSP No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Semi-expendable Property No.</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Group/Officer</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Group/Officer</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Group/Officer</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Qty.</td>"
             + "</tr>";

            if (dt.Rows.Count > 0)
            {
                bool reIssuance = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TransType"].ToString() == "Issuance" && reIssuance)
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                        reIssuance = false;
                    }
                    else if (dt.Rows[i]["TransType"].ToString() == "Issuance" && !reIssuance)
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                    }
                    else if (dt.Rows[i]["TransType"].ToString() == "Return")
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["Amount"].ToString()) + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                    }
                    else
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                    }

                }
                //  html += "<tr><td colspan='11' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                //  html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
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

    public static string ConvertImageToBase64()
    {
        string fileName = "Image\\" + Common.CompanyInfo + "\\FormLogo.png";
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        int newWidth = 90;
        int newHeight = 130;

        // Resize the image and convert it to base64
        string base64Image = ResizeAndConvertToBase64(filePath, newWidth, newHeight);



        //byte[] fileBytes = File.ReadAllBytes(filePath);
        //string base64ImageRepresentation = Convert.ToBase64String(fileBytes);

        return base64Image;
        //string base64ImageRepresentation = Convert.ToBase64String(fileBytes);
        //string bs64= ResizeBase64Image(base64ImageRepresentation,100,100);
        //return bs64;
    }


    public static string ResizeAndConvertToBase64(string imagePath, int newWidth, int newHeight)
    {
        // Load the original image
        using (Bitmap originalImage = new Bitmap(imagePath))
        {
            // Create a new bitmap with the specified width and height
            using (Bitmap resizedImage = new Bitmap(originalImage, newWidth, newHeight))
            {
                // Convert the resized image to base64
                string base64String = ConvertToBase64(resizedImage);

                return base64String;
            }
        }
    }

    private static string ConvertToBase64(Bitmap image)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Save the image to the memory stream in PNG format (you can change the format if needed)
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            // Convert the image bytes to base64 string
            byte[] imageBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
    }

}