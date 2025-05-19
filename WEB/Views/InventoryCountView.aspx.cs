using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class Views_InventoryCountView : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
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

    #region Datatables
    [WebMethod]
    public static string GetInventoryList(string search, string datefrom, string dateto, string AccessButton)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<InventoryEntity> JsonItemList = new List<InventoryEntity>();

        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("InventoryCountView", Common.ConnString, Common.UserName, "1");
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
                        if (Value.Contains(','))
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

            //string frmDate = DateTime.TryParse(datefrom, "dd/MM/yyyy").ToString();
            //string toDate = DateTime.TryParse(dateto, "dd/MM/yyyy").ToString();

            DataTable dt = y.GetInventoryListView(Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), search, datefrom, dateto, Filter, Common.Administrative);
            //DataTable dt = y.GetInventoryListView(Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), search, frmDate, toDate, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["InventoryCode"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/InventoryCount/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    InventoryEntity x = new InventoryEntity();
                    x.InventoryName = dt.Rows[i]["InventoryName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    if (dt.Rows[i]["InventoryType"].ToString() == "1")
                    {
                        x.InventoryTypeString = "Property";
                    }
                    else if (dt.Rows[i]["InventoryType"].ToString() == "2")
                    {
                        x.InventoryTypeString = "Stock";
                    }
                    else
                    {
                        x.InventoryTypeString = "Property & Stock";
                    }
                    x.DateCreated = dt.Rows[i]["CreatedDate"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/InventoryCount/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List' onclick=\"ViewInventoryDetails('" + dt.Rows[i]["InventoryName"] + "','" + dt.Rows[i]["InventoryCode"] + "','" + dt.Rows[i]["WarehouseName"].ToString() + " - " + dt.Rows[i]["Location"].ToString() + "' )\"><span class='fa fa-eye'></span></a></center>";
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

            string msg = ex.Message.ToString();
            return msg;
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
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>";
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
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio'  name='optionsRadios" + i + "' checked></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "'></center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadios" + i + "' checked )\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "'></center></td>";
                }
                else
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadios" + i + "' ></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio'  name='optionsRadios" + i + "' checked ></center></td>";
                }
                html += "</tr>";
            }
            html += "<tr><td></td><td></td><td align='right'>TOTAL</td><td align='center'>" + GetCountFound(inventorycode) + "</td><td align='center'>" + GetCountMissing(inventorycode) + "</td><td align='center'>" + GetCountMisplaced(inventorycode) + "</td></tr>";
            html += "</table></div>";
            html += "<div style=\"text-align:center;padding:3px;\">" + InventoryPropertyDetails(pageNo, totalPage) + "</div>";
        }
        else
        {
            html = "<div class='table-responsive'><table class='table table-striped table-bordered' width='100%'>"
                + "<tr class='info'>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>FOUND<b></center></th>"
                + "<th><center><b>NOT FOUND</b></center></th>"
                + "<th><center><b>MISPLACED</b></center></th>"
                + "</tr>";
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
                + "<th><center><b>QUANTITY<b></center></th>"
                + "<th><center><b>FIRST COUNT</b></center></th>"
                + "<th><center><b>SECOND COUNT</b></center></th>"
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
                html += "<td>" + dt.Rows[i]["Count1"].ToString() + "</td>"
                     + "<td>" + dt.Rows[i]["Count2"].ToString() + "</td>"
                     + "<td>" + dt.Rows[i]["Count3"].ToString() + "</td>";

                html += "<td><label id='variance" + i + "'>" + dt.Rows[i]["Variance"].ToString() + "</label></td>";
                html += "</tr>";
            }
            html += "</table></div>";
            html += "<div style=\"text-align:center;padding:3px;\">" + InventoryStockDetails(pageNo, totalPage) + "</div>";
        }
        else
        {
            html = "<div class='table-responsive'><table class='table table-bordered' width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>QUANTITY<b></center></th>"
                + "<th><center><b>FIRST COUNT</b></center></th>"
                + "<th><center><b>SECOND COUNT</b></center></th>"
                + "<th><center><b>THIRD COUNT</b></center></th>"
                + "<th><center><b>VARIANCE</b></center></th>"
                + "</tr>";
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
        + "<tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>Inventory Property List<b></td</tr>"
                + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse; font-family:Arial;' border ='1'  width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
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
                      + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>";

                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null)
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "</td>";
                }

                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadiosv" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadiosv" + i + "' checked></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadiosv" + i + "'></center></td>";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadiosv" + i + "' checked )\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadiosv" + i + "')\"></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadiosv" + i + "'></center></td>";
                }
                else
                {
                    html += "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadiosv" + i + "'></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' disabled name='optionsRadiosv" + i + "' ></center></td>"
                        + "<td class='col-md-1 col-lg-1'><center><input type='radio' name='optionsRadiosv" + i + "' checked ></center></td>";
                }

                html += "</tr>";
            }
            html += "<tr><td></td><td></td><td align='right'>TOTAL INVENTORY COUNT</td><td align='center'>" + GetCountFound(inventorycode) + "</td><td align='center'>" + GetCountMissing(inventorycode) + "</td><td align='center'>" + GetCountMisplaced(inventorycode) + "</td></tr>";
            html += "</table></div>";
        }
        else
        {
            html = "<div class='table-responsive'><table class='table table-striped table-bordered' width='100%'>"
                + "<tr class='info'>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>PROPERTY #</b><center></th>"
                + "<th><center><b>FOUND<b></center></th>"
                + "<th><center><b>NOT FOUND</b></center></th>"
                + "<th><center><b>MISPLACED</b></center></th>"
                + "</tr>";
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
        html += "<table style='width:100%;font-family: Arial;'><tr><td align='center' style='font-family: Arial; font-size:13;'>" + CompanyName + "</td></tr>" + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px'; /></td</tr>"
               //+ "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px'; /></td</tr>"
                + "<tr><td align='center' style='font-size:11;'>" + CompanyAddress + "</td></tr>"
                + "<tr><td align='center' style='font-size:11;'>Tel. No./ CP No.: " + CompanyContactNumber + "</td</tr>"
                + "<tr><td align='center' style='font-size:15;'><br><b>Inventory Supply List<b></td</tr>"
                + "</table>";
        html += "<div class='table-responsive'><table class='table table-bordered' style='border-collapse:collapse;  font-family:Arial;' border ='1' width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>QUANTITY<b></center></th>"
                + "<th><center><b>FIRST COUNT</b></center></th>"
                + "<th><center><b>SECOND COUNT</b></center></th>"
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
                html += "<td>" + dt.Rows[i]["Count1"].ToString() + "</td>"
                     + "<td>" + dt.Rows[i]["Count2"].ToString() + "</td>"
                     + "<td>" + dt.Rows[i]["Count3"].ToString() + "</td>";
                html += "<td>" + dt.Rows[i]["Variance"].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table></div>";
        }
        else
        {
            html = "<div class='table-responsive'><table class='table table-bordered' width='100%'>"
                + "<tr>"
                + "<th><center><b>ITEM CODE</b><center></th>"
                + "<th><center><b>QUANTITY<b></center></th>"
                + "<th><center><b>FIRST COUNT</b></center></th>"
                + "<th><center><b>SECOND COUNT</b></center></th>"
                + "<th><center><b>THIRD COUNT</b></center></th>"
                + "<th><center><b>VARIANCE</b></center></th>"
                + "</tr>";
            html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
        }
        return html;
    }

    #endregion

    #region Export Inventory


    public static string donwloadTextFileProperty(string inventorycode, string report)
    {
        string path = HttpContext.Current.Server.MapPath("~/SupportingDocuments/InventoryDumps/" + inventorycode + ".csv");
        //
        File.WriteAllText(path, report);

        //Read the File as Byte Array.
        byte[] bytes = File.ReadAllBytes(path);
        //Convert File to Base64 string and send to Client.
        return Convert.ToBase64String(bytes, 0, bytes.Length);


        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.AddHeader("Content-disposition", "attachment;filename=" + inventorycode + "_" + DateTime.Now.ToShortDateString() + ".csv");
        //HttpContext.Current.Response.Charset = "utf-8";
        //HttpContext.Current.Response.ContentType = "text/csv";
        //HttpContext.Current.Response.Output.Write(report);
        //HttpContext.Current.Response.Flush();
        //HttpContext.Current.Response.SuppressContent = true;
        //HttpContext.Current.ApplicationInstance.CompleteRequest();
        //HttpContext.Current.Response.End();
    }

    public void btnExportProperty(object sender, EventArgs e)
    {
        GetInventoryExportProperty(txtInventoryCode.Text, txtInventoryName.Text, txtInventoryLocation.Text);
    }

    [WebMethod]
    public static string GetInventoryExportProperty(string inventorycode, string inventoryname, string inventorylocation)
    {
        PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
        DataTable dt = y.GetPropertyInventoryList(Common.ConnString, inventorycode);

        string report = "";

        report += "Inventory Name : " + inventoryname + Environment.NewLine;
        report += "Inventory Location : " + inventorylocation + Environment.NewLine;
        report += "List of Properties" + Environment.NewLine;

        report += "Item Code|Property #|Origin Location|Found|Not Found|Misplaced" + Environment.NewLine;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                report += dt.Rows[i]["ItemCode"].ToString() + "|" + dt.Rows[i]["PropertyNumber"].ToString() + "|";
                if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
                {
                    report += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "|";
                }
                else
                {
                    report += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "|";
                }

                if (dt.Rows[i]["Status"].ToString() == "0")
                {
                    report += "0|1|0";
                }
                else if (dt.Rows[i]["Status"].ToString() == "1")
                {
                    report += "1|0|0";
                }
                else
                {
                    report += "0|0|1";
                }
                report += Environment.NewLine;
            }
            report += "||TOTAL|" + GetCountFound(inventorycode) + "|" + GetCountMissing(inventorycode) + "|" + GetCountMisplaced(inventorycode) + Environment.NewLine;


            return donwloadTextFileProperty(inventorycode, report);
            //return report;
        }

        else
            return donwloadTextFileProperty(inventorycode, report);

    }
    #endregion Export Inventory

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
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add("InventoryName");
            drpFilterBy.Items.Add("Description");
            drpFilterBy.Items.Add("Site");
            drpFilterBy.Items.Add("Location");
            drpFilterBy.Items.Add("InventoryType");
            drpFilterBy.Items.Add("CreatedDate");
        }
        catch
        {
        }
    }
    #endregion
}