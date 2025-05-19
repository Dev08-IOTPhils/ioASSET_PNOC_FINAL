using AdvanceBll;
using AdvanceEntity;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class Views_RSPI : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add(" Select Filter ");
            drpFilterBy.Items.Add("ItemCode");
            drpFilterBy.Items.Add("PropertyNumber");
            drpFilterBy.Items.Add("Quantity");
            drpFilterBy.Items.Add("Description");
        }
        catch
        {
        }
    }

    [WebMethod]
    public static string CreateRSPI(string propertynumber,
     string fromimagebase64,
     string byimagebase64,
     string frompassphrase,
     string bypassphrase,
     string fromcertbase64,
     string bycertbase64,
     bool fromdatestamp,
     bool bydatestamp, 
     string datefrom, 
     string dateto)
    {
        try
        {
            RSPIEntity x = new RSPIEntity();
            RSPIBll y = new RSPIBll();

            x.PropertyNo = propertynumber;
            x.ConnectionString = Common.ConnString;

            DataTable dt = y.getlists(x, datefrom, dateto);

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
            + ".page-header, .page-header-space {height: 285px; }"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                       + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
                + "<tr><td align='center' style='font-size:15;'><br><h3>REPORT OF SEMI-EXPENDABLE PROPERTY ISSUED</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='width:50%;font-size:12;'>Entity Name : <u>" + CompanyName + "</td><td style='font-size:12;'>Serial No. : <u>" + "" + "</td></tr>"
                + "<tr><td style='width:50%;font-size:12;'>Fund Cluster : <u>" + "" + "</td><td style='font-size:12;'>Date. : <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer' style='margin-left: -8px;margin-bottom: 40px;'>";
            //terms and conditions //signatories
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td style='font-size:11.5px;'; width='50%'; style='font-size:11.5px; font-family: Arial;'> <center>I hereby certify to the correctness of the above information.<br/><img src='" + fromimagebase64 + "' width='50%' height='75' /><br />" + (fromdatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<br />________________________________________<br/>Signature over Printed Name of Supply and/or<br/> Property Custodian</center></td>" +
            "<td width='50%'><center><table style='font-size:11.5px;font-size:75%;'><tr><td style='font-size:11.5px;'; width='65%'; style='font-size:11.5px; font-family: Arial;'>Posted by:<br/><br/><center><img src='" + byimagebase64 + "' width='100%' height='75' /><br />" + (bydatestamp == true ? ("Date : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K") + "<br />") : "<br />") + "<br />______________________<br/>Signature over Printed Name of <br/>Designated Accounting</center></td></tr></table></center></td>";
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
    public static string GetList(int type, string AccessButton, string datefrom, string dateto)
    {
        RSPIBll z = new RSPIBll();
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        List<RSPIEntity> JsonItemList = new List<RSPIEntity>();
        string html = string.Empty;
        try
        {
            string Filter = "";
            DataTable dtFilter = y.GetActiveFilter("RSPI", Common.ConnString, Common.UserName, "1");
            if (dtFilter.Rows.Count > 0)
            {
                for (int i = 0; i < dtFilter.Rows.Count; i++)
                {
                    Regex reg = new Regex("[*'\";]");
                    string Value = reg.Replace(dtFilter.Rows[i]["Values"].ToString(), string.Empty);
                    string OperatorValue = dtFilter.Rows[i]["Operator"].ToString();
                    if (OperatorValue == "Not =")
                    {
                        OperatorValue = " !='" + Value + "' ";
                    }
                    else if (OperatorValue == "Like")
                    {
                        OperatorValue = " like '%" + Value + "%' ";
                    }
                    else if (OperatorValue == "In" || OperatorValue == "Not In")
                    {
                        if (Value.Contains(","))
                        {
                            string[] multipleValue = Value.Split(',');
                            string itemValue = "";
                            foreach (var item in multipleValue)
                            {
                                itemValue += "'" + item + "',";
                            }
                            OperatorValue = " " + OperatorValue + " (" + itemValue.Remove(itemValue.Length - 1) + ") ";
                        }
                        else
                        {
                            OperatorValue = " " + OperatorValue + " ('" + Value + "')";
                        }
                    }
                    else if (OperatorValue == ">" || OperatorValue == ">=" || OperatorValue == "<" || OperatorValue == "<=")
                    {
                        OperatorValue = " " + OperatorValue + Value;
                    }
                    else
                    {
                        OperatorValue = OperatorValue + " '" + Value + "'";
                    }
                    //if (i == 0)
                    //{
                    //    Filter += " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                    //}
                    //else
                    //{
                    //    Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                    //}

                    Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                }
            }

            DataTable dt = z.GetList(Common.ConnString, type, Filter, datefrom, dateto);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RSPIEntity x = new RSPIEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNo = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Quantity = dt.Rows[i]["Quantity"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    //if (AccessButton == "0")
                    //{
                    //    x.Action = "";
                    //}
                    //else
                    //{
                    //    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View ' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["PropertyNumber"].ToString() + "')\"><span class='fa fa-eye'></span></a>";
                    //    x.Action += "&nbsp;<a href='' class='btn btn-xs btn-primary btn_View ' data-toggle='tooltip'  title='Create Excel' onclick=\"ViewExcel('" + dt.Rows[i]["PropertyNumber"].ToString() + "')\"><span class='fa fa-download'></span></a>";
                    //    x.Action += "&nbsp;<a href='' class='btn btn-xs btn-primary btn_View ' data-toggle='tooltip'  title='Create PDF' onclick=\"CreateCertificate('" + dt.Rows[i]["PropertyNumber"].ToString() + "')\"><span class='fa fa-file-pdf-o'></span></a></center>";
                    //}
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
    public static string GenerateReport(string datefrom, string dateto)
    {
        RSPIEntity x = new RSPIEntity();
        RSPIBll y = new RSPIBll();

        //x.PropertyNo = propertyno;
        x.ConnectionString = Common.ConnString;

        DataTable dt = y.getlists(x, datefrom, dateto);

        return GenerateRSPI(dt);
    }

    public static string GenerateRSPI(DataTable dt)
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
            + ".page-header, .page-header-space {height: 300px; }"
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

            #endregion styles

            #region div header and footer

            #region header

            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                       + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px'; /></td</tr>"
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
}