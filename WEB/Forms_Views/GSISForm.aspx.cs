using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using OpenHtmlToPdf;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

public partial class Views_GSIS_Form : BasePage
{
    string retSite = "";

    //VW_GSIS_Forms
    //SP_SearchPropertyGSISForms
    protected void Page_Load(object sender, EventArgs e)
    {
        //SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        //retSite = shSystemConfigurationsSite.Value;
        //lbldisplaySite.Text = retSite + ":";


        GetItem();
    }
    #region metadata

    [WebMethod]
    public static string GetFromWarehouse()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

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


    #endregion metadata

    #region Datatable

    [WebMethod]
    public static string LoadData(string SiteID, string proprtyTypeCost, bool floater)
    {

        MiscBll miscBll = new MiscBll();
        PropertyBll y = new PropertyBll();
        // DataTable dt = y.GetITemList(Common.ConnString, Common.FullName);

        string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            // DataTable dt = y.GetITemList(Common.ConnString, SiteID);
            DataTable dt = y.GetITemListGSISForm(Common.ConnString, SiteID, proprtyTypeCost, floater);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    //x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ", " + dt.Rows[i]["LongDescription"].ToString();
                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Area = dt.Rows[i]["Area"].ToString();
                    x.DateOfAcquisition = dt.Rows[i]["DateOfAcquisition"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.AcquiredCostString = dt.Rows[i]["AcquiredCost"].ToString();


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

    #endregion Datatable

    #region metadata

    [WebMethod]
    public static string GenerateReport(string siteID, string proprtyTypeCost, bool floater)
    {
        PropertyBll y = new PropertyBll();

        DataTable dt = y.GetITemListGSISForm(Common.ConnString, siteID, proprtyTypeCost, floater);

        //DataTable dt1 = y.GetITemList(Common.ConnString, UserID);

        return GeneratePDF(dt);
    }

    public static string GeneratePDF(DataTable dt)
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
            html += "<table style='width:100%;font-family: Arial;'>" + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
            //html += "<table style='width:100%;font-family: Arial;'>" + "<tr><td colspan='4'; align='center' style='font-size:15;'><img src='data:image/png;base64," + "" + "' style='width: 150px; height: 150px' /></td></tr>"
            + "<tr><td align='center' style='font-size:15;'><br><h3>PROPERTY INVENTORY FORM</h3></br></td</tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='width:50%;font-size:12;'>Name of Office: <u>" + CompanyName + "</td><td style='font-size:12;'>Serial No. : <u>" + "" + "</td></tr>"
                + "<tr><td style='width:50%;font-size:12;'>Address : <u>" + "" + "</td><td style='font-size:12;'>Date. : <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            html += "<tr><td width='50%'> <center>I hereby certify to the correctness of the above information.<br/>________________________________________<br/>Signature over Printed Name of Supply and/or<br/> Property Custodian</center></td>" +
                "<td width='50%'><center><table style='font-size:75%;'><tr><td width='65%'>Posted by:<br/><br/><center>______________________<br/>Signature over Printed Name of <br/>Designated Accounting Staff</center></td><td width='35%'><br/><br/><br/><center>__________<br/>Date</center></td></tr></table></center></td>";
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
                       + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Classification/Type</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Nature of Occupancy</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Site</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Location</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Area</b></td>"


                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date Constructed/ Acquired/ Manufactured</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property No./ Other Reference</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Acquisition Cost/ Insurable Interest</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Market/ Appraisal/ Insurable Interest</b></td>"

                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Date of Appraisal</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right: 1px solid;'><b>Remarks</b></td>"
                          + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["ClassificationName"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Description"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "OFFICE" + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["WarehouseName"].ToString() + " </ td > "
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Location"].ToString() + " </ td > "
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Area"].ToString() + " </ td > "



                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["DateOfAcquisition"].ToString() + " </ td > "
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["AcquiredCost"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"

                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + "" + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right: 1px solid;'> " + "" + "</td>"
                          + "</tr>";
                }


                
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

    #endregion metadata

}