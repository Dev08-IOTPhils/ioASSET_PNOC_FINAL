using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using OpenHtmlToPdf;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;




public partial class Views_EmployeeLedgerCard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Metadata

    static string header1 = "";
    static string header2 = "";
    static string headerWidth = "";
    static string headerHeight = "";
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

    [WebMethod]
    public static string GenerateReport(string UserID)
    {
        PropertyBll y = new PropertyBll();

        DataTable dt = y.GetITemList(Common.ConnString, UserID);

        DataTable dt1 = y.GetITemList(Common.ConnString, UserID);

        return GeneratePDF(dt, dt1);
    }

    public static string GeneratePDF(DataTable dt, DataTable dt1)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string y = miscBll.GetFormHeader(Common.ConnString);
            header1 = y.Split('|')[0];
            header2 = y.Split('|')[1];
            headerHeight = y.Split('|')[2];
            headerWidth = y.Split('|')[3];

            string html = "";

            #region styles

            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 100px; }"
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
            html += "<table style='width:100%;font-family: Arial;'><tr><td style='font-size:15;'>" + header1 + "</td></tr>"
            + "<tr><td style='font-size:15;'>EMPLOYEE LEDGER CARD</td></tr></table>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td style='width:50%;font-size:12;'>Date : <u>" + DateTime.Now.ToShortDateString() + "</td><td style='font-size:12;'></td></tr>"
                + "<tr><td style='width:50%;font-size:12;'>Employee Name : <u>" + dt.Rows[0]["EmployeeName"].ToString() + "</td><td style='font-size:12;'>Employee No. : <u>" + dt.Rows[0]["EmployeeNo"].ToString() + "</td></tr>"
                + "<tr><td style='width:50%;font-size:12;'>Department : <u>" + dt.Rows[0]["EmployeeDepartment"].ToString() + "</td><td style='font-size:12;'></td></tr></table>";
            html += "</div>";

            #endregion header

            #region footer

            //footer div
            html += "<div class='page-footer'>";
            //terms and conditions //signatories
            //html += "<hr style='border-top: 1px dashed'>";
            //html += "<table class='table table-bordered' style='border-collapse:collapse; font-size:75%; font-family:Arial;' border='1' width='100%'>";
            //html += "<tr><td width='50%'> <center>I hereby certify to the correctness of the above information.<br/>________________________________________<br/>Signature over Printed Name of Supply and/or<br/> Property Custodian</center></td>" +
            //    "<td width='50%'><center><table style='font-size:75%;'><tr><td width='65%'>Posted by:<br/><br/><center>______________________<br/>Signature over Printed Name of <br/>Designated Accounting Staff</center></td><td width='35%'><br/><br/><br/><center>__________<br/>Date</center></td></tr></table></center></td>";
            //html += "</table>";
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
            double totalAmount = 0;
            if (dt.Rows.Count > 0)
            {
                html += "<tr>"
                       + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item No.</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Property Number</b></td>"
                      //+ "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Custodian Type</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;'><b>Acquisition Cost</b></td>"
                      + "<td style='text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>PAR/ICS Number</b></td>"
                          + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + i+1 + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          //+ "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["Custodian"].ToString() + "</td>"
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + dt.Rows[i]["LongDescription"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString() + " </ td > "
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;'> " + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + " </ td > "
                          + "<td  style='width:11%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'> " + dt.Rows[i]["PARICSnumber"].ToString() + "</td>"
                          + "</tr>";

                    double acquiredCost = 0;
                    if (double.TryParse(dt.Rows[i]["AcquiredCost"].ToString(), out acquiredCost))
                    {
                        totalAmount += acquiredCost;
                    }
                }
                html += "<tr><td></td><td></td><td></td><td></td><td>Number of Asset:</td><td>" + dt.Rows.Count + "</td></tr>"
                    + "<tr><td></td><td></td><td></td><td></td><td>Total Amount:</td><td>" + BasePage.ConvertToMoneyFormat(totalAmount.ToString()) + "</td></tr>";

                html += "<tr><td colspan='6'>CONFORME:<br><br></td></tr>"
                    + "<tr><td colspan='6'></td></tr>"
                    + "<tr><td colspan='6'>" + dt.Rows[0]["EmployeeName"].ToString() + "</td></tr>";


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



    #endregion Metadata

    #region Datatable

    [WebMethod]
    public static string LoadData(string empID)
    {

        MiscBll miscBll = new MiscBll();
        PropertyBll y = new PropertyBll();
        // DataTable dt = y.GetITemList(Common.ConnString, Common.FullName);

        string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetITemList(Common.ConnString, empID);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.CustodianIDstr = dt.Rows[i]["Custodian"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.AcquiredCostString = dt.Rows[i]["AcquiredCost"].ToString();
                    x.AssetType = dt.Rows[i]["PARICSnumber"].ToString();

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
}