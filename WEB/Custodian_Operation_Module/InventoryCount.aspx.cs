using AdvanceBll;
using AdvanceEntity;
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

public partial class Operation_Module_InventoryCount : BasePage
{
    string retSite = "";
    string retLoc = "";
    string retArea = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        InventoryType();
        GetItem();

        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        retSite = shSystemConfigurationsSite.Value;
        lbldisplaySite.Text = retSite + ":";
        lblListSite.Text = retSite;


        SystemConfigurations shSystemConfigurationsLocation = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        retLoc = shSystemConfigurationsLocation.Value;
        lbldisplayLoc.Text = retLoc + ":";
        lblListLoc.Text = retLoc;

        SystemConfigurations shSystemConfigurationsArea = (SystemConfigurations)Session["shSystemConfigurationsArea"];
        retArea = shSystemConfigurationsArea.Value;
        lbldisplayArea.Text = retArea + ":";
        lblListArea.Text = retArea;


    }

    [WebMethod]
    public static string CreateExcelSemi(string inventorycode, string Custodian)
    {
        try
        {
            string html = "";

            PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
            DataTable dt = y.GetPropertyInventoryListPrint(Common.ConnString, inventorycode);
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            html += "<style>@page Section1 {size:595.45pt 841.7pt; margin:1.0in 1.25in 1.0in 1.25in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}"
        + "div.Section1 {page:Section1;}"
        + "@page Section2 {size:841.7pt 595.45pt;mso-page-orientation:landscape;margin:1.25in 1.0in 1.25in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}"
        + "div.Section2 {page:Section2;}</style>";
            html += "<div class='Section2'><table style='width:100%;font-family: Arial;'>"
                + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
                    + "<tr><td align='center' style='font-size:16;'><br><b>REPORT ON THE PHYSICAL COUNT OF SEMI-EXPENDABLE PROPERTY2<b></td></tr>"
                + "<tr><td align='center' style='font-size:14;'><u>" + Custodian + "</u></td></tr>"
                    + "<tr><td align='center' style='font-size:14;'>(Type of Semi-expendable, Property)</td></tr>"
                + "<tr><td align='center' style='font-size:14;'>As at <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                    + "<tr><td align='left' style='font-size:14;'><br/>Fund Cluster : ____________________________</td></tr>"
                     + "<tr><td align='left' style='font-size:14;'><br/>For which : <u>" + Custodian + "</u>, ____________, <u>" + CompanyName + "</u> is accountable, having assumed such accountability on <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
               + "</table>";

            if (dt.Rows.Count > 0)
            {
                html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                     + "<tr>"
                     + "<th rowspan='2'><center><b>ARTICLE</b><center></th>"
                     + "<th rowspan='2'><center><b>DESCRIPTION</b><center></th>"
                     + "<th rowspan='2'><center><b>PROPERTY NUMBER</b><center></th>"
                     + "<th rowspan='2'><center><b>UNIT OF MEASURE<b></center></th>"
                     + "<th rowspan='2'><center><b>UNIT VALUE</b></center></th>"
                     + "<th rowspan='2'><center><b>QUANTITY per PROPERTY CARD</b></center></th>"
                     + "<th rowspan='2'><center><b>QUANTITY per PHYSICAL COUNT</b></center></th>"
                     + "<th colspan='2'><center><b>SHORTAGE/OVERAGE</b></center></th>"
                     + "<th><center></center></th>"
                     + "</tr><tr><td><center>Quantity</center></td><td><center>Value</center></td><td><center>Remarks</center></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dothis
                    string Status = "";
                    string Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    string quantity = "1";
                    html += "<tr>"
                          + "<td>" + dt.Rows[i]["ClassificationName"].ToString() + "</td>"
                          + "<td>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                          + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>";
                    if (dt.Rows[i]["Status"].ToString() == "0")
                    {
                        Status = "NOT FOUND";
                        html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                            + "<td class='col-md-1 col-lg-1'><center>" + "0" + "</center></td>";
                    }
                    else if (dt.Rows[i]["Status"].ToString() == "1")
                    {
                        quantity = "";
                        Status = "FOUND";
                        Value = "";
                        html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                            + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                    }
                    else
                    {
                        Status = "MISPLACED - ";
                        if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                        {
                            Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                        }
                        else
                        {
                            Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                        }
                        html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                            + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                    }

                    html += "<td align='center'c>" + quantity + "</td>"
                          + "<td>" + Value + "</td>"
                          + "<td>" + Status + "</td>";
                    html += "</tr>";
                }

                html += "<tr><td colspan='10'><table style='width:100%;font-family: Arial;'>";
                html += "<tr>"
                    + "<td style='font-size:13;width=30%;'></br>Certified Correct by:</br></br><center>______________________________</br>Signature over Printed Name of</br>Inventory Committee Chair and</br>Members</center></td>"
                    + "<td style='font-size:13;width=40%;'>Approved Correct by:</br></br><center>______________________________</br>Signature over Printed Name of Head of</br>Agency/Entity or Authorized Representative</center></td>"
                    + "<td style='font-size:13;width=30%;'>&emsp;&emsp;Verified by:</br></br><center>______________________________</br>Signature over Printed Name of COA</br>Representative</center></td>";
                html += "</tr></table></td></tr></table></div></div>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center></div>";
            }
            return html;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string CreateExcelPPE(string inventorycode, string custodian)
    {
        try
        {
            PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
            DataTable dt = y.GetPropertyInventoryListPrint(Common.ConnString, inventorycode);
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];
            string html = "";
            html += "<style>@page Section1 {size:595.45pt 841.7pt; margin:1.0in 1.25in 1.0in 1.25in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}"
        + "div.Section1 {page:Section1;}"
        + "@page Section2 {size:841.7pt 595.45pt;mso-page-orientation:landscape;margin:1.25in 1.0in 1.25in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}"
        + "div.Section2 {page:Section2;}</style>";

            html += "<div class='Section2'><table style='width:100%;font-family: Arial;'>"
                       + "<tr><td align='center' style='font-size:15;'><img src='data:image/png;base64," + ConvertImageToBase64() + "' style='width: 150px; height: 150px' /></td></tr>"
                    + "<tr><td align='center' style='font-size:16;'><br><b>REPORT ON THE PHYSICAL COUNT OF PROPERTY, PLANT AND EQUIPMENT <b></td></tr>"
                    + "<tr><td align='center' style='font-size:14;'><u>" + custodian + "</u></td></tr>"
                    + "<tr><td align='center' style='font-size:14;'>(Type of Property, Plant and Equipment)</td></tr>"
                    + "<tr><td align='center' style='font-size:14;'>As at <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                    + "<tr><td align='left' style='font-size:14;'><br/>Fund Cluster : ____________________________</td></tr>"
                     + "<tr><td align='left' style='font-size:14;'><br/>For which : <u>" + custodian + "</u>, ____________, <u>" + CompanyName + "</u> is accountable, having assumed such accountability on <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                + "</table>";

            if (dt.Rows.Count > 0)
            {
                html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                     + "<tr>"
                     + "<th rowspan='2'><center><b>ARTICLE</b><center></th>"
                     + "<th rowspan='2'><center><b>DESCRIPTION</b><center></th>"
                     + "<th rowspan='2'><center><b>PROPERTY NUMBER</b><center></th>"
                     + "<th rowspan='2'><center><b>UNIT OF MEASURE<b></center></th>"
                     + "<th rowspan='2'><center><b>UNIT VALUE</b></center></th>"
                     + "<th rowspan='2'><center><b>QUANTITY per PROPERTY CARD</b></center></th>"
                     + "<th rowspan='2'><center><b>QUANTITY per PHYSICAL COUNT</b></center></th>"
                     + "<th colspan='2'><center><b>SHORTAGE/OVERAGE</b></center></th>"
                     + "<th><center></center></th>"
                     + "</tr><tr><td><center>Quantity</center></td><td><center>Value</center></td><td><center>Remarks</center></td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dothis
                    string Status = "";
                    string Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    string quantity = "1";
                    html += "<tr>"
                          + "<td>" + "" + "</td>"
                          + "<td>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                          + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                          + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                          + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>";
                    if (dt.Rows[i]["Status"].ToString() == "0")
                    {
                        Status = "NOT FOUND";
                        html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                            + "<td class='col-md-1 col-lg-1'><center>" + "0" + "</center></td>";
                    }
                    else if (dt.Rows[i]["Status"].ToString() == "1")
                    {
                        quantity = "";
                        Status = "FOUND";
                        Value = "";
                        html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                            + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                    }
                    else
                    {
                        Status = "MISPLACED - ";
                        if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                        {
                            Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                        }
                        else
                        {
                            Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                        }
                        Status = "";
                        html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                            + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                    }

                    html += "<td align='center'c>" + quantity + "</td>"
                          + "<td>" + Value + "</td>"
                          + "<td>" + Status + "</td>";
                    html += "</tr>";
                }
                html += "<tr><td colspan='10'><table style='width:100%;font-family: Arial;'>";
                html += "<tr>"
                    + "<td style='font-size:13;width=30%;'></br>Certified Correct by:</br></br><center>______________________________</br>Signature over Printed Name of</br>Inventory Committee Chair and</br>Members</center></td>"
                    + "<td style='font-size:13;width=40%;'>Approved Correct by:</br></br><center>______________________________</br>Signature over Printed Name of Head of</br>Agency/Entity or Authorized Representative</center></td>"
                    + "<td style='font-size:13;width=30%;'>&emsp;&emsp;Verified by:</br></br><center>______________________________</br>Signature over Printed Name of COA</br>Representative</center></td>";
                html += "</tr></table></td></tr></table></div></div>";
            }
            else
            {
                html = "<center><h5 style='color: red;'>No Records Found </h4> </center></div>";
            }
            return html;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    #region Metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/InventoryCount.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    public void GetItem()
    {
        ddlItem.Items.Clear();
        ddlItem.Items.Add("--- Select Property Type Cost ---");
        ddlItem.Items.Add("ICS LOW - " + " Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and below.");
        ddlItem.Items.Add("ICS HIGH - " + " Between above Php " + BasePage.ConvertToMoneyFormat(Common.ICSValue) + " and Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
        ddlItem.Items.Add("PAR - " + " Above Php " + BasePage.ConvertToMoneyFormat(Common.PARValue));
    }

    [WebMethod]
    public static string GetUserCustodian()
    {
        CustodianBll cb = new CustodianBll();
        DataTable dt = cb.GetUserCustodian(Common.CustodianID, Common.Administrative, Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    private void InventoryType()
    {
        ddlInventoryType.Items.Clear();
        ddlInventoryType.Items.Insert(0, "--- Select Inventory Type ---");
        ddlInventoryType.Items.Insert(1, "Property");
        ddlInventoryType.Items.Insert(2, "Supply");
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
    public static string GetFromWarehouse()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
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
    public static string GetLocationListIC(int warehouse)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.WarehouseID = warehouse;
        locationEntity.ConnectionString = Common.ConnString;

        DataTable dt = locationBll.GetLocationListBySiteIC(locationEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }


    [WebMethod]
    public static List<ListItem> GetOfficeList(int location)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.LocationID = location;
        locationEntity.ConnectionString = Common.ConnString;

        return locationBll.GetOfficeByLocation(locationEntity);
    }

    [WebMethod]
    public static string GetOfficeListAC(int location)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.LocationID = location;
        locationEntity.ConnectionString = Common.ConnString;

        DataTable dt = locationBll.GetOfficeByLocationAC(locationEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    #endregion Metadata

    #region Datatables

    [WebMethod]
    public static string GetInventoryList()
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();

        string html = string.Empty;
        List<InventoryEntity> JsonItemList = new List<InventoryEntity>();

        try
        {
            DataTable dt = y.GetInventoryList(Common.ConnString, Common.CustodianID, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    InventoryEntity x = new InventoryEntity();
                    x.InventoryName = dt.Rows[i]["InventoryName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.IsReturnable = dt.Rows[i]["Area"].ToString();
                    if (dt.Rows[i]["InventoryType"].ToString() == "1")
                    {
                        x.InventoryTypeString = "Property";
                    }
                    else if (dt.Rows[i]["InventoryType"].ToString() == "2")
                    {
                        x.InventoryTypeString = "Supply";
                    }
                    else
                    {
                        x.InventoryTypeString = "Property & Supply";
                    }
                    x.DateCreated = dt.Rows[i]["CreatedDate"].ToString();
                    if (ViewAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List' onclick=\"ViewInventoryDetails('" + dt.Rows[i]["InventoryCode"] + "','" + dt.Rows[i]["InventoryType"].ToString() + "','" + dt.Rows[i]["WarehouseName"].ToString() + "','" + dt.Rows[i]["Location"].ToString() + "','" + dt.Rows[i]["PARICS"].ToString() + "','" + dt.Rows[i]["Custodian"].ToString() + "','" + dt.Rows[i]["Area"].ToString() + "','" + dt.Rows[i]["CreatedDate"].ToString() + "')\"><span class='fa fa-eye'></span></a></center>";
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
    public static string GetPropertyInventoryList(string inventorycode, int pageNo)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryList(Common.ConnString, inventorycode, pageNo);
        int totalPage = 0;
        try
        {
            totalPage = Int32.Parse(dt.Rows[0]["totalPage"].ToString());
        }
        catch { totalPage = 0; }
        if (totalPage < pageNo)
        {
            pageNo = 1;
            dt = y.GetPropertyInventoryList(Common.ConnString, inventorycode, pageNo);
        }

        string html = "";

        html += "<div class='table-responsive'><table id='tbl_PropertyInventoryList' class='table table-bordered' width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>DESCRIPTION</b><center></th>"
                + "<th><center><b>ACQUIRED COST</b><center></th>"
                + "<th><center><b>ORIGIN LOCATION</b><center></th>"
                + "<th><center><b>FOUND<b></center></th>"
                + "<th><center><b>NOT FOUND</b></center></th>"
                + "<th><center><b>MISPLACED</b></center></th>"
                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
                      + "<td align='center'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>";

                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "</td>";
                }
                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadios" + i + "' onclick=\"UpdateFound('" + dt.Rows[i]["RecID"] + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadios" + i + "' checked onclick=\"UpdateNotFound('" + dt.Rows[i]["RecID"] + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "' onclick=\"UpdateMisplaced('" + dt.Rows[i]["RecID"] + "')\"></center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadios" + i + "' checked onclick=\"UpdateFound('" + dt.Rows[i]["RecID"] + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadios" + i + "'  onclick=\"UpdateNotFound('" + dt.Rows[i]["RecID"] + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "' onclick=\"UpdateMisplaced('" + dt.Rows[i]["RecID"] + "')\"></center></td>";
                }
                else
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "' onclick=\"UpdateFound('" + dt.Rows[i]["RecID"] + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "'  onclick=\"UpdateNotFound('" + dt.Rows[i]["RecID"] + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "' checked onclick=\"UpdateMisplaced('" + dt.Rows[i]["RecID"] + "')\"></center></td>";
                }
                html += "</tr>";
            }
            html += "<tr><td></td><td></td><td>TOTAL ACQUIRED COST</td><td align='right '>" + BasePage.ConvertToMoneyFormat(GetCountAcquiredCost(inventorycode)) + "</td><td align='right'>TOTAL INVENTORY COUNT</td><td align='center'>" + GetCountFound(inventorycode) + "</td><td align='center'>" + GetCountMissing(inventorycode) + "</td><td align='center'>" + GetCountMisplaced(inventorycode) + "</td></tr>";
            html += "</table></div>";
            html += "<div style=\"text-align:center;padding:3px;\">" + InventoryPropertyDetails(pageNo, totalPage) + "</div>";

        }
        else
        {
            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetStockInventoryList(string inventorycode, int pageNo)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetStockInventoryList(Common.ConnString, inventorycode, pageNo);
        int totalPage = 0;
        try
        {
            totalPage = Int32.Parse(dt.Rows[0]["totalPage"].ToString());
        }
        catch { totalPage = 0; }
        if (totalPage < pageNo)
        {
            pageNo = 1;
            dt = y.GetStockInventoryList(Common.ConnString, inventorycode, pageNo);
        }

        string html = "";

        html += "<div class='table-responsive'><table class='table table-bordered' width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>DESCRIPTION</b><center></th>"
                + "<th><center><b>QUANTITY<b></center></th>"
               
                + "<th><center><b>FINAL COUNT</b></center></th>"
                + "<th><center><b>VARIANCE</b></center></th>"
                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + " <td>" + dt.Rows[i]["Description"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["Quantity"].ToString() + "</td>";

                html += "<td><center><input type='number' min='0' class='form-control' id='thirdcount" + i + "' value='" + dt.Rows[i]["Count3"].ToString() + "' onclick=\"UpdateLabelVariance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["Quantity"] + "','" + i + "')\" onkeypress=\"return onlyNos(event,this);\" onkeyup=\"UpdateLabelVariance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["Quantity"] + "','" + i + "')\" style='width:100%;'></center></td>";
                
                html += "<td><label id='variance" + i + "'>" + dt.Rows[i]["Variance"].ToString() + "</label></td>";
                html += "</tr>";
            }
            html += "</table></div>";
            html += "<div style=\"text-align:center;padding:3px;\">" + InventoryStockDetails(pageNo, totalPage) + "</div>";
        }
        else
        {

            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrint(string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrint(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];
        string html = "";
        html += "<table style='width:100%;font-family: Arial;'>"
                  + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                  + "<tr><td align='center' style='font-size:25;'><br><b>INVENTORY PROPERTY LIST <b><br/><br/></td></tr>"
              + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>ORIGIN LOCATION</b><center></th>"
                + "<th><center><b>UNIT COST</b></center></th>"
                + "<th><center><b>FOUND<b></center></th>"
                + "<th><center><b>NOT FOUND</b></center></th>"
                + "<th><center><b>MISPLACED</b></center></th>"
                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>";


                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "</td>";
                }

                html += "<td><center>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</center></td>";
                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "' checked></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "'></center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "' checked></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "'></center></td>";
                }
                else
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "' checked></center></td>";
                }


                html += "</tr>";
            }

            html += "<tr><td colspan='3' align='right'><b>TOTAL INVENTORY COUNT</b></td><td></td><td align='center'>" + GetCountFound(inventorycode) + "</td><td align='center'>" + GetCountMissing(inventorycode) + "</td><td align='center'>" + GetCountMisplaced(inventorycode) + "</td></tr>";

            html += "<tr><td colspan='3' align='right'><b>TOTAL AMOUNT</b></td><td align='center'>" + BasePage.ConvertToMoneyFormat(GetCountAcquiredCost(inventorycode)) + "</td><td></td><td></td><td></td></tr>";
            html += "</table></div>";
        }
        else
        {

            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrintFound(string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrintFOUND(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];
        string html = "";
        html += "<table style='width:100%;font-family: Arial;'>"
                  + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                  + "<tr><td align='center' style='font-size:25;'><br><b>INVENTORY PROPERTY LIST <b><br/><br/></td></tr>"
              + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>ORIGIN LOCATION</b><center></th>"
                + "<th><center><b>UNIT COST</b></center></th>"
                + "<th><center><b>FOUND<b></center></th>"

                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>";


                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "</td>";
                }

                html += "<td><center>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</center></td>";
                html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "' checked></center></td>";


                html += "</tr>";
            }

            html += "<tr><td colspan='3' align='right'><b>TOTAL INVENTORY COUNT</b></td><td></td><td align='center'>" + GetCountFound(inventorycode) + "</td></tr>";
            html += "<tr><td colspan='3' align='right'><b>TOTAL AMOUNT</b></td><td align='center'>" + BasePage.ConvertToMoneyFormat(GetCountAcquiredCostFound(inventorycode)) + "</td><td></td></tr>";
            html += "</table></div>";
        }
        else
        {

            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrintNotFound(string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrintNOTFOUND(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];
        string html = "";
        html += "<table style='width:100%;font-family: Arial;'>"
                  + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                  + "<tr><td align='center' style='font-size:25;'><br><b>INVENTORY PROPERTY LIST <b><br/><br/></td></tr>"
              + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>ORIGIN LOCATION</b><center></th>"
                + "<th><center><b>UNIT COST</b></center></th>"
                + "<th><center><b>NOT FOUND</b></center></th>"
                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>";


                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "</td>";
                }

                html += "<td><center>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</center></td>";
                html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "' checked></center></td>";


                html += "</tr>";
            }

            html += "<tr><td colspan='3' align='right'><b>TOTAL INVENTORY COUNT</b></td><td></td><td align='center'>" + GetCountMissing(inventorycode) + "</td></tr>";
            html += "<tr><td colspan='3' align='right'><b>TOTAL AMOUNT</b></td><td align='center'>" + BasePage.ConvertToMoneyFormat(GetCountAcquiredCostNotFound(inventorycode)) + "</td><td></td></tr>";
            html += "</table></div>";
        }
        else
        {

            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrintMisplaced(string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrintMISPLACED(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];
        string html = "";
        html += "<table style='width:100%;font-family: Arial;'>"
                  + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                  + "<tr><td align='center' style='font-size:25;'><br><b>INVENTORY PROPERTY LIST <b><br/><br/></td></tr>"
              + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>ORIGIN LOCATION</b><center></th>"
                + "<th><center><b>UNIT COST</b></center></th>"
                + "<th><center><b>MISPLACED</b></center></th>"
                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>";


                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "</td>";
                }

                html += "<td><center>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</center></td>";
                html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadio" + i + "' checked></center></td>";



                html += "</tr>";
            }

            html += "<tr><td colspan='3' align='right'><b>TOTAL INVENTORY COUNT</b></td><td></td><td align='center'>" + GetCountMisplaced(inventorycode) + "</td></tr>";
            html += "<tr><td colspan='3' align='right'><b>TOTAL AMOUNT</b></td><td align='center'>" + BasePage.ConvertToMoneyFormat(GetCountAcquiredCostMisplaced(inventorycode)) + "</td><td></td></tr>";

            html += "</table></div>";
        }
        else
        {

            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrintRCPPE(string inventorycode, string Custodian)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrintPAR(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];
        string html = "";
        html += "<style>"
            + "@page {size: A4; size: landscape; margin: 10mm;}"
            + "</style >";
        html += "<table style='width:100%;font-family: Arial;'>"
                   + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td align='center' style='font-size:16;'><br><b>REPORT ON THE PHYSICAL COUNT OF PROPERTY, PLANT AND EQUIPMENT <b></td></tr>"
                + "<tr><td align='center' style='font-size:14;'><u>" + Custodian + "</u></td></tr>"
                + "<tr><td align='center' style='font-size:14;'>(Type of Property, Plant and Equipment)</td></tr>"
                + "<tr><td align='center' style='font-size:14;'>As at <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                + "<tr><td align='left' style='font-size:14;'><br/>Fund Cluster : ____________________________</td></tr>"
               + "<tr><td align='left' style='font-size:14;'><br/>For which : <u>" + Custodian + "</u>, ____________, <u>" + CompanyName + "</u> is accountable, having assumed such accountability on <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                 + "</table>";

        if (dt.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                 + "<tr>"
                 + "<th rowspan='2'><center><b>ARTICLE</b><center></th>"
                 + "<th rowspan='2'><center><b>DESCRIPTION</b><center></th>"
                 + "<th rowspan='2'><center><b>PROPERTY NUMBER</b><center></th>"
                 + "<th rowspan='2'><center><b>UNIT OF MEASURE<b></center></th>"
                 + "<th rowspan='2'><center><b>UNIT VALUE</b></center></th>"
                 + "<th rowspan='2'><center><b>QUANTITY per PROPERTY CARD</b></center></th>"
                 + "<th rowspan='2'><center><b>QUANTITY per PHYSICAL COUNT</b></center></th>"
                 + "<th colspan='2'><center><b>SHORTAGE/OVERAGE</b></center></th>"
                 + "<th><center></center></th>"
                 + "</tr><tr><td><center>Quantity</center></td><td><center>Value</center></td><td><center>Remarks</center></td></tr>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dothis
                string Status = "";
                string Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                string quantity = "1";
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ClassificationName"].ToString() + "</td>" + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                      + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>";
                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    Status = "NOT FOUND - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "0" + "</center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    quantity = "";
                    Status = "FOUND - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    Value = "";
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                }
                else
                {
                    Status = "MISPLACED - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    Status = "";
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                }

                html += "<td align='center'c>" + quantity + "</td>"
                      + "<td>" + Value + "</td>"
                      + "<td>" + Status + "</td>";
                html += "</tr>";
            }
            html += "<tr><td colspan='10'><table style='width:100%;font-family: Arial;'>";
            html += "<tr>"
                + "<td style='font-size:13;width=30%;'></br>Certified Correct by:</br></br><center>______________________________</br>Signature over Printed Name of</br>Inventory Committee Chair and</br>Members</center></td>"
                + "<td style='font-size:13;width=40%;'>Approved Correct by:</br></br><center>______________________________</br>Signature over Printed Name of Head of</br>Agency/Entity or Authorized Representative</center></td>"
                + "<td style='font-size:13;width=30%;'>&emsp;&emsp;Verified by:</br></br><center>______________________________</br>Signature over Printed Name of COA</br>Representative</center></td>";
            html += "</tr></table></td></tr></table></div>";
        }
        else
        {

            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrintRCPSEP(string inventorycode, string Custodian)
    {
        string html = "";

        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrintICS(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];

        html += "<style>"
            + "@page {size: A4; size: landscape; margin: 10mm;}"
            + "</style >";
        html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td align='center' style='font-size:16;'><br><b>REPORT ON THE PHYSICAL COUNT OF SEMI-EXPENDABLE PROPERTY<b></td></tr>"

                + "<tr><td align='center' style='font-size:14;'><u>" + Custodian + "</u></td></tr>"
                + "<tr><td align='center' style='font-size:14;'>(Type of Semi-expendable, Property)</td></tr>"
                + "<tr><td align='center' style='font-size:14;'>As at <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                + "<tr><td align='left' style='font-size:14;'><br/>Fund Cluster : ____________________________</td></tr>"
               + "<tr><td align='left' style='font-size:14;'><br/>For which : <u>" + Custodian + "</u>, ____________, <u>" + CompanyName + "</u> is accountable, having assumed such accountability on <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                 + "</table>";

        if (dt.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                 + "<tr>"
                 + "<th rowspan='2'><center><b>ARTICLE</b><center></th>"
                 + "<th rowspan='2'><center><b>DESCRIPTION</b><center></th>"
                 + "<th rowspan='2'><center><b>PROPERTY NUMBER</b><center></th>"
                 + "<th rowspan='2'><center><b>UNIT OF MEASURE<b></center></th>"
                 + "<th rowspan='2'><center><b>UNIT VALUE</b></center></th>"
                 + "<th rowspan='2'><center><b>QUANTITY per PROPERTY CARD</b></center></th>"
                 + "<th rowspan='2'><center><b>QUANTITY per PHYSICAL COUNT</b></center></th>"
                 + "<th colspan='2'><center><b>SHORTAGE/OVERAGE</b></center></th>"
                 + "<th><center></center></th>"
                 + "</tr><tr><td><center>Quantity</center></td><td><center>Value</center></td><td><center>Remarks</center></td></tr>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dothis
                string Status = "";
                string Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                string quantity = "1";
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ClassificationName"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                      + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>";
                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    Status = "NOT FOUND - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "0" + "</center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    quantity = "";
                    Status = "FOUND - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    Value = "";
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                }
                else
                {
                    Status = "MISPLACED - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                }

                html += "<td align='center'c>" + quantity + "</td>"
                      + "<td>" + Value + "</td>"
                      + "<td>" + Status + "</td>";
                html += "</tr>";
            }
            html += "<tr><td colspan='10'><table style='width:100%;font-family: Arial;'>";
            html += "<tr>"
                + "<td style='font-size:13;width=30%;'></br>Certified Correct by:</br></br><center>______________________________</br>Signature over Printed Name of</br>Inventory Committee Chair and</br>Members</center></td>"
                + "<td style='font-size:13;width=40%;'>Approved Correct by:</br></br><center>______________________________</br>Signature over Printed Name of Head of</br>Agency/Entity or Authorized Representative</center></td>"
                + "<td style='font-size:13;width=30%;'>&emsp;&emsp;Verified by:</br></br><center>______________________________</br>Signature over Printed Name of COA</br>Representative</center></td>";
            html += "</tr></table></td></tr></table></div>";
        }
        else
        {

            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetPropertyInventoryListPrintRCPSEPH(string inventorycode, string Custodian)
    {
        string html = "";

        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrintICSH(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];

        html += "<style>"
            + "@page {size: A4; size: landscape; margin: 10mm;}"
            + "</style >";
        html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td align='center' style='font-size:16;'><br><b>REPORT ON THE PHYSICAL COUNT OF SEMI-EXPENDABLE PROPERTY<b></td></tr>"

                + "<tr><td align='center' style='font-size:14;'><u>" + Custodian + "</u></td></tr>"
                + "<tr><td align='center' style='font-size:14;'>(Type of Semi-expendable, PropLongDescriptionerty)</td></tr>"
                + "<tr><td align='center' style='font-size:14;'>As at <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                + "<tr><td align='left' style='font-size:14;'><br/>Fund Cluster : ____________________________</td></tr>"
               + "<tr><td align='left' style='font-size:14;'><br/>For which : <u>" + Custodian + "</u>, ____________, <u>" + CompanyName + "</u> is accountable, having assumed such accountability on <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                 + "</table>";

        if (dt.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                 + "<tr>"
                 + "<th rowspan='2'><center><b>ARTICLE</b><center></th>"
                 + "<th rowspan='2'><center><b>DESCRIPTION</b><center></th>"
                 + "<th rowspan='2'><center><b>PROPERTY NUMBER</b><center></th>"
                 + "<th rowspan='2'><center><b>UNIT OF MEASURE<b></center></th>"
                 + "<th rowspan='2'><center><b>UNIT VALUE</b></center></th>"
                 + "<th rowspan='2'><center><b>QUANTITY per PROPERTY CARD</b></center></th>"
                 + "<th rowspan='2'><center><b>QUANTITY per PHYSICAL COUNT</b></center></th>"
                 + "<th colspan='2'><center><b>SHORTAGE/OVERAGE</b></center></th>"
                 + "<th><center></center></th>"
                 + "</tr><tr><td><center>Quantity</center></td><td><center>Value</center></td><td><center>Remarks</center></td></tr>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dothis
                string Status = "";
                string Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                string quantity = "1";
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ClassificationName"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                      + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>";
                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    Status = "NOT FOUND - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "0" + "</center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    quantity = "";
                    Status = "FOUND - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    Value = "";
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                }
                else
                {
                    Status = "MISPLACED - ";
                    if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString();
                    }
                    else
                    {
                        Status += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString();
                    }
                    html += "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>"
                        + "<td class='col-md-1 col-lg-1'><center>" + "1" + "</center></td>";
                }

                html += "<td align='center'c>" + quantity + "</td>"
                      + "<td>" + Value + "</td>"
                      + "<td>" + Status + "</td>";
                html += "</tr>";
            }
            html += "<tr><td colspan='10'><table style='width:100%;font-family: Arial;'>";
            html += "<tr>"
                + "<td style='font-size:13;width=30%;'></br>Certified Correct by:</br></br><center>______________________________</br>Signature over Printed Name of</br>Inventory Committee Chair and</br>Members</center></td>"
                + "<td style='font-size:13;width=40%;'>Approved Correct by:</br></br><center>______________________________</br>Signature over Printed Name of Head of</br>Agency/Entity or Authorized Representative</center></td>"
                + "<td style='font-size:13;width=30%;'>&emsp;&emsp;Verified by:</br></br><center>______________________________</br>Signature over Printed Name of COA</br>Representative</center></td>";
            html += "</tr></table></td></tr></table></div>";
        }
        else
        {

            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }
    [WebMethod]
    public static string GetPropertyInventoryListPrintICF(string inventorycode, string Custodian)
    {
        string html = "";

        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryListPrint(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];

        html += "<style>"
            + "@page {size: A4; size: landscape; margin: 10mm;}"
            + "</style >";
        html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td align='center' style='font-size:16;'><br><b>INVENTORY COUNT FORM<b></td></tr>"

                + "<tr><td align='center' style='font-size:14;'>PROPERTY, PLANT AND EQUIPMENT</td></tr>"
                + "<tr><td align='center' style='font-size:14;'><b>PHILIPPINE NATIONAL OIL COMPANY</b></td></tr>"
                + "<tr><td align='center' style='font-size:14;'><b>as of " + dt.Rows[0]["CreatedDate"].ToString() + "</b></td></tr>"
                + "<tr><td align='center' style='font-size:14;'><b>PRM-04F3</b></td></tr><br/>"
                 + "</table>";

        if (dt.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                 + "<tr>"
                 + "<th rowspan='2'><center><b>Article/Item</b><center></th>"
                 + "<th rowspan='2'><center><b>Description</b><center></th>"
                 + "<th rowspan='2'><center><b>ASSET TAG</b><center></th>"
                 + "<th rowspan='2'><center><b>Unit of Measure<b></center></th>"
                 + "<th rowspan='2'><center><b>Unit Value</b></center></th>"
                 + "<th rowspan='2'><center><b>Quantity per Property Card</b></center></th>"
                 + "<th rowspan='2'><center><b>Quantity per Physical Count</b></center></th>"
                 + "<th rowspan='2'><center><b>Location/Whereabouts</b></center></th>"
                 + "<th rowspan='2'><center><b>Condition</b></center></th>"
                 + "<th rowspan='2'><center><b>Remarks</b></center></th></tr><tbody>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dothis
                string Status = "";
                string Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                string quantity = "1";


                if (dt.Rows[i]["WarehouseName"].ToString() == "NULL" || dt.Rows[i]["WarehouseName"].ToString() == null || dt.Rows[i]["WarehouseName"].ToString() == "")
                {
                }
                else
                {
                    Status += dt.Rows[i]["WarehouseName"].ToString();
                }
                if (dt.Rows[i]["Location"].ToString() == "Location" || dt.Rows[i]["Location"].ToString() == null || dt.Rows[i]["Location"].ToString() == "")
                {
                }
                else
                {
                    Status += "-" + dt.Rows[i]["Location"].ToString();
                }
                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                }
                else
                {
                    Status += "-" + dt.Rows[i]["Area"].ToString();
                }

                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ExpenseType"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                      + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                      + "<td>" + "1" + "</td>"
                      + "<td>" + "1" + "</td>"
                      + "<td>" + Status + "</td>"
                      + "<td>" + "Good" + "</td>"
                      + "<td>" + "" + "</td>";


                html += "</tr>";
            }
            html += "<tr><td colspan='10'><center>Note: for PPE items without Property No., provide in the <b>Remarks</b> column other information such as Serial No./Model No./ brief description that can be useful during the reconsillation process</center></td></tr>";
            html += "</tbody></table></div><div><br/><br/><br/><b>PREPARED BY:</b>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<b>REVIEWED BY:</b><br/><br/><br/><br/><br/><br/><br/><b>PHYCISAL INVENTORY CONDUCTED BY:</b>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<b>WITNESSED BY:</b></div>";
        }
        else
        {

            html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }


    [WebMethod]
    public static string GetPropertyInventoryListPrintROCI(string inventorycode, string Custodian)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetStockInventoryListPrint(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];
        string html = "";
        html += "<style>"
            + "@page {size: A4; size: landscape; margin: 10mm;}"
            + "</style >";
        html += "<table style='width:100%;font-family: Arial;'>"
            + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"

                + "<tr><td align='center' style='font-size:16;'><br><b>REPORT ON THE PHYSICAL COUNT OF INVENTORIES <b></td></tr>"

                + "<tr><td align='center' style='font-size:14;'><u>" + Custodian + "</u></td></tr>"
                + "<tr><td align='center' style='font-size:14;'>(Type of Inventory Item)</td></tr>"
                + "<tr><td align='center' style='font-size:14;'>As at <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                + "<tr><td align='left' style='font-size:14;'><br/>Fund Cluster : ____________________________</td></tr>"
                + "<tr><td align='left' style='font-size:14;'><br/>For which : <u>" + Custodian + "</u>, ____________, <u>" + CompanyName + "</u> is accountable, having assumed such accountability on <u>" + DateTime.Now.ToString("MM/dd/yyyy") + "</u></td></tr>"
                + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
               + "<tr>"
               + "<th rowspan='2'><center><b>ARTICLE</b><center></th>"
               + "<th rowspan='2'><center><b>DESCRIPTION</b><center></th>"
               + "<th rowspan='2'><center><b>Item Code</b><center></th>"
               + "<th rowspan='2'><center><b>Unit of Measure<b></center></th>"
               + "<th rowspan='2'><center><b>Unit Value</b></center></th>"
               + "<th ><center><b>Balance Per Card</b></center></th>"
               + "<th ><center><b>On Hand Per Count</b></center></th>"
               + "<th colspan='2'><center><b>Shortage/Overage</b></center></th>"
               + "<th rowspan='2'><center>Remarks</center></th>"
               + "</tr><tr><td><center>(Quantity)</center></td><td><center>(Quantity)</center></td><td><center>Quantity</center></td><td><center>Value</center></td></tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dothis
                string Status = "";
                string UnitCost = "0";
                if (dt.Rows[i]["AcquiredCost"].ToString() != "")
                {
                    UnitCost = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                }
                double cost = Convert.ToDouble(UnitCost) * Convert.ToDouble(dt.Rows[i]["Quantity"].ToString());
                html += "<tr>"
                + "<td>" + dt.Rows[i]["ClassificationName"].ToString() + "</td>"
                + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
                + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                + "<td>" + dt.Rows[i]["UnitOfMeasurement"].ToString() + "</td>"
                + "<td>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString()) + "</td>"
                + "<td>" + dt.Rows[i]["Quantity"].ToString() + "</td>";


                    html += "<td>" + dt.Rows[i]["Count3"].ToString() + "</td>";

                html += "<td>" + dt.Rows[i]["Variance"].ToString() + "</td>";
                html += "<td>" + cost.ToString() + "</td>";
                html += "<td>" + "" + "</td>";
                html += "</tr>";
            }
            html += "<tr><td colspan='10'><table style='width:100%;font-family: Arial;'>";
            html += "<tr>"
                + "<td style='font-size:13;width=30%;'></br>Certified Correct by:</br></br><center>______________________________</br>Signature over Printed Name of</br>Inventory COA Rep and</br>Members</center></td>"
                + "<td style='font-size:13;width=40%;'>Approved Correct by:</br></br><center>______________________________</br>Signature over Printed Name of Head of</br>Agency/Entity or Authorized Representative</center></td>"
                + "<td style='font-size:13;width=30%;'>&emsp;&emsp;Verified by:</br></br><center>______________________________</br>Signature over Printed Name of COA</br>Representative</center></td>";
            html += "</tr></table></td></tr></table></div>";
        }
        else
        {

            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    [WebMethod]
    public static string GetStockInventoryListPrint(string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetStockInventoryListPrint(Common.ConnString, inventorycode);
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        string x = miscBll.GetCompanyInformation(Common.ConnString);
        string CompanyName = x.Split('|')[0];
        string CompanyContactNumber = x.Split('|')[3];
        string CompanyAddress = x.Split('|')[5];

        string html = "";
        html += "<table style='width:100%;font-family: Arial;'>"
                  + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse;  font-family:Arial;' border ='1' width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>QUANTITY<b></center></th>"
                + "<th><center><b>THIRD COUNT</b></center></th>"
                + "<th><center><b>VARIANCE</b></center></th>"
                + "</tr>";

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>"
                      + "<td>" + dt.Rows[i]["ItemCode"].ToString() + "</td>"
                      + "<td>" + dt.Rows[i]["Quantity"].ToString() + "</td>";
                html += "<td><center>" + dt.Rows[i]["Count3"].ToString() + "</center></td>";

                html += "<td>" + dt.Rows[i]["Variance"].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table></div>";
        }
        else
        {
            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    #endregion Datatables

    #region methods

    [WebMethod]
    public static string SaveInventory(int id, string inventoryname, string custodian, int site, int location, int inventorytype, string description, int ExpenseType, int Office)
    {
        InventoryEntity x = new InventoryEntity();
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();

        x.InventoryID = id;
        x.InventoryName = inventoryname;
        x.SiteID = site;
        x.LocationID = location;
        x.InventoryType = inventorytype;
        x.CustodianIDStr = custodian;
        x.Description = description;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.ExpenseType = ExpenseType;
        x.AreaID = Office;

        return y.SaveInventory(x).ToString();
    }

    [WebMethod]
    public static int UpdateFoundByBarcode(string barcode, string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        return y.UpdateFoundByBarcode(Common.ConnString, barcode, inventorycode);
    }

    [WebMethod]
    public static int UpdateFound(int id, string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        return y.UpdateFound(Common.ConnString, id, inventorycode);
    }

    [WebMethod]
    public static int UpdateNotFound(int id, string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        return y.UpdateNotFound(Common.ConnString, id, inventorycode);
    }

    [WebMethod]
    public static int UpdateMisplaced(int id, string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        return y.UpdateMisplaced(Common.ConnString, id, inventorycode);
    }

    [WebMethod]
    public static int UpdateVariance(int recid, int firstcount, int secondcount, int thirdcount, int variance)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        return y.UpdateVariance(Common.ConnString, recid, firstcount, secondcount, thirdcount, variance);
    }

    [WebMethod]
    public static void UploadInventoryCount(string inventorycode)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        InventoryEntity x = new InventoryEntity();

        x.InventoryCode = inventorycode;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.UploadInventoryCount(x);
    }

    [WebMethod]
    public static string addPropertyInventoryToList(string inventorycode, string propertyno, int expensetype)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();

        return y.addPropertyInventoryToList(Common.ConnString, inventorycode, propertyno, expensetype);
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        if (txtInventoryType.Text == "1")
        {
            string inventorycode = txtInventoryCode.Text;
            UploadInventoryCount(inventorycode);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccess();", true);
        }
        else
        {
            string inventorycode = txtInventoryCode1.Text;
            UploadInventoryCount(inventorycode);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccess();", true);
        }
    }

    public bool success = false;

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtInventoryType.Text == "1")
            {
                string inventorycode = txtInventoryCode.Text;
                if (AFupload.HasFiles)
                {
                    string rootPath = Server.MapPath("~/SupportingDocuments/InventoryCount/" + inventorycode + "/");
                    string pathFinal = rootPath + '/' + inventorycode + '/';
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
                        UploadInventoryCount(inventorycode);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccess();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:selectfile();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:selectfile();", true);
                }
            }
            else
            {
                string inventorycode = txtInventoryCode1.Text;
                if (AFupload.HasFiles)
                {
                    string rootPath = Server.MapPath("~/SupportingDocuments/InventoryCount/" + inventorycode + "/");
                    string pathFinal = rootPath + '/' + inventorycode + '/';
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
                        UploadInventoryCount(inventorycode);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccess();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:selectfile();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:selectfile();", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    [WebMethod]
    public static int GetCountFound(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountFound(inventorycode, Common.ConnString);
    }

    [WebMethod]
    public static int GetCountMissing(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountMissing(inventorycode, Common.ConnString);
    }

    [WebMethod]
    public static int GetCountMisplaced(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountMisplaced(inventorycode, Common.ConnString);
    }

    [WebMethod]
    public static string GetCountAcquiredCost(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountAcquiredCost(inventorycode, Common.ConnString);
    }
    public static string GetCountAcquiredCostFound(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountAcquiredCostFound(inventorycode, Common.ConnString);
    }
    public static string GetCountAcquiredCostNotFound(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountAcquiredCostNotFound(inventorycode, Common.ConnString);
    }

    public static string GetCountAcquiredCostMisplaced(string inventorycode)
    {
        PropertyBll y = new PropertyBll();
        return y.getCountAcquiredCostMisplaced(inventorycode, Common.ConnString);
    }
    #endregion methods

    #region Pagination

    public static string InventoryPropertyDetails(int pageNo, int totalPage)
    {
        string html = "";

        if (pageNo > 1)
        {
            html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageProperty(1);\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageProperty(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
        }

        html += "<span>"
            + "<select id=\"ddlPaging\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"PageProperty( $('#ddlPaging').val() )\">";

        for (int ctr = 1; ctr <= totalPage; ctr++)
        {
            html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
        }

        html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
            + "</span>&nbsp;";

        if (pageNo < totalPage)
        {
            html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageProperty(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageProperty(" + totalPage.ToString() + ");\" ></span>";
        }

        return html;
    }

    public static string InventoryStockDetails(int pageNo, int totalPage)
    {
        string html = "";

        if (pageNo > 1)
        {
            html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStock(1);\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStock(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
        }

        html += "<span>"
            + "<select id=\"ddlPaging1\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"PageStock( $('#ddlPaging1').val() )\">";

        for (int ctr = 1; ctr <= totalPage; ctr++)
        {
            html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
        }

        html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
            + "</span>&nbsp;";

        if (pageNo < totalPage)
        {
            html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStock(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStock(" + totalPage.ToString() + ");\" ></span>";
        }

        return html;
    }

    public static string InventoryPropertyDetailsPrint(int pageNo, int totalPage)
    {
        string html = "";

        if (pageNo > 1)
        {
            html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PagePropertyPrint(1);\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PagePropertyPrint(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
        }

        html += "<span>"
            + "<select id=\"ddlPagingPrint\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"PagePropertyPrint( $('#ddlPagingPrint').val() )\">";

        for (int ctr = 1; ctr <= totalPage; ctr++)
        {
            html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
        }

        html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
            + "</span>&nbsp;";

        if (pageNo < totalPage)
        {
            html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PagePropertyPrint(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PagePropertyPrint(" + totalPage.ToString() + ");\" ></span>";
        }

        return html;
    }

    public static string InventoryStockDetailsPrint(int pageNo, int totalPage)
    {
        string html = "";

        if (pageNo > 1)
        {
            html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStockPrint(1);\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStockPrint(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
        }

        html += "<span>"
            + "<select id=\"ddlPagingPrint1\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"PageStockPrint( $('#ddlPagingPrint1').val() )\">";

        for (int ctr = 1; ctr <= totalPage; ctr++)
        {
            html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
        }

        html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
            + "</span>&nbsp;";

        if (pageNo < totalPage)
        {
            html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStockPrint(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"PageStockPrint(" + totalPage.ToString() + ");\" ></span>";
        }

        return html;
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

    #endregion Pagination
}