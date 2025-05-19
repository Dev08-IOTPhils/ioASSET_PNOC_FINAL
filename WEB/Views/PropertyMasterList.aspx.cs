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

public partial class Views_PropertyMasterList : BasePage
{
    string retSite = "";
    string retLoc = "";
    string retArea = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        retSite = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = retSite;


        SystemConfigurations shSystemConfigurationsLocation = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        retLoc = shSystemConfigurationsLocation.Value;
        lblMasterListLoc.Text = retLoc;

        SystemConfigurations shSystemConfigurationsArea = (SystemConfigurations)Session["shSystemConfigurationsArea"];
        retArea = shSystemConfigurationsArea.Value;
        lblMasterListArea.Text = retArea;

        GetFlterTable();
    }

    #region Datatables
    [WebMethod]
    public static string GetList(string search)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = y.GetActiveFilter("PropertyMasterlist", Common.ConnString, Common.UserName,"1");
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

                    else if (OperatorValue == "In"|| OperatorValue == "Not In")
                    {
                        if (Value.Contains(','))
                        {
                            string[] multipleValue = Value.Split(',');
                            string itemValue = "";
                            foreach (var item in multipleValue)
                            {
                                itemValue += "'" + item + "',";
                            }
                            OperatorValue = " "+OperatorValue+" (" + itemValue.Remove(itemValue.Length - 1) + ") ";
                        }
                        else
                        {
                            OperatorValue = " "+OperatorValue+" ('" + Value + "')";
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
                    if (i==0)
                    {
                        Filter += " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                    }
                    else
                    {
                        Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                    }
                }
            }


            DataTable dt = y.GetPropertyMasterlist(Common.ConnString, search, Filter);
            //html = BasePage.ConvertDataTabletoJSON(dt);
            //return html;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();

                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.LongDescription = dt.Rows[i]["LongDescription"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();

                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    if (dt.Rows[i]["Status"].ToString() == "On Stock")
                    {
                        x.IssuanceNumber = "";
                    }
                    else
                    {
                        if (dt.Rows[i]["IssuanceNo"].ToString() == null || dt.Rows[i]["IssuanceNo"].ToString() == "")
                        {
                            x.IssuanceNumber = dt.Rows[i]["PARICSnumber"].ToString();
                        }
                        else
                        {
                            x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                        }
                    }
                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Area = dt.Rows[i]["Area"].ToString();
                    x.CustodianIDstr = dt.Rows[i]["Custodian"].ToString();


                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();


                    x.UserDepartment = dt.Rows[i]["DepartmentName"].ToString();
                    x.TypeCost = dt.Rows[i]["TypeCost"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.DateOfAcquisition = dt.Rows[i]["DateOfAcquisition"].ToString().Split(' ')[0];
                    x.UsefulLifeString = dt.Rows[i]["UsefulLife"].ToString().Split(' ')[0];
                    x.RemainingUsefulLifeString = dt.Rows[i]["RemainingUsefulLife"].ToString().Split(' ')[0];
                    x.DateOfMaintenance = dt.Rows[i]["MaintenanceDate"].ToString().Split(' ')[0];
                    x.WarrantyDate = dt.Rows[i]["WarrantyStartDate"].ToString().Split(' ')[0] + " - " + dt.Rows[i]["WarrantyEndDate"].ToString().Split(' ')[0]; 
                    x.InsuranceDate = dt.Rows[i]["InsuranceStartDate"].ToString().Split(' ')[0] + " - " + dt.Rows[i]["InsuranceEndDate"].ToString().Split(' ')[0];
                    x.ExpiryDate = dt.Rows[i]["ExpiryDate"].ToString().Split(' ')[0];
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    if (dt.Rows[i]["DepreciationMethod"].ToString() == "NONE")
                    {
                        x.DepreciatedCostString = "";
                    }
                    else
                    {
                        //x.DepreciatedCostString = "";
                        if (depreciationSetting == "Straight Line Depreciation Method")
                        {
                            if (Converter.ConvertStringToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                            {
                                x.DepreciatedCostString = "0.00";
                            }
                            else
                            {
                                x.DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                            }
                        }
                        else if (depreciationSetting == "Diminishing Balance Method")
                        {
                            if (Converter.ConvertStringToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 ||  dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()))
                            {
                                x.DepreciatedCostString = "0.00";
                            }
                            else
                            {
                                x.DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                            }
                        }
                        else
                        {
                            if (Converter.ConvertStringToDouble(dt.Rows[i]["UnitOfProductDepreciation"].ToString()) < 0 || dt.Rows[i]["UnitOfProductDepreciation"].ToString() == null)
                            {
                                x.DepreciatedCostString = "0.00";
                            }
                            else
                            {
                                x.DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["UnitOfProductDepreciation"].ToString());
                            }
                        }
                    }

                    if (dt.Rows[i]["TransactionType"].ToString() != "")
                    {
                        x.Status = dt.Rows[i]["Status"].ToString() + " (" + dt.Rows[i]["TransactionType"].ToString() + ")";
                    }
                    else
                    {
                        x.Status = dt.Rows[i]["Status"].ToString();
                    }
                    x.DisposedDate = dt.Rows[i]["DisposedDate"].ToString().Split(' ')[0];
                    x.IssuedDate = dt.Rows[i]["IssuedDate"].ToString().Split(' ')[0];
                    x.LastmaintenanceDate = dt.Rows[i]["LastMaintenanceDate"].ToString().Split(' ')[0];
                    x.InventoryName = dt.Rows[i]["InventoryName"].ToString();
                    x.InventoryDate = dt.Rows[i]["InventoryCreatedDate"].ToString().Split(' ')[0];
                    if (dt.Rows[i]["InventoryStatus"].ToString() == "0")
                    {
                        x.InventoryStatus = "Not Found";
                    }
                    else if (dt.Rows[i]["InventoryStatus"].ToString() == "1")
                    {
                        x.InventoryStatus = "Found";
                    }
                    else if(dt.Rows[i]["InventoryStatus"].ToString() == "NULL" || dt.Rows[i]["InventoryStatus"].ToString() == "")
                    {
                        x.InventoryStatus = "";
                    }
                    else 
                    {
                        x.InventoryStatus = "Misplaced";
                    }
                    x.InventorySite = dt.Rows[i]["InventorySite"].ToString();
                    x.InventoryLocation = dt.Rows[i]["InventoryLocation"].ToString();
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


    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add(" Select Filter ");
            drpFilterBy.Items.Add("ItemCode");
            drpFilterBy.Items.Add("Description");
            drpFilterBy.Items.Add("PropertyNumber");
            drpFilterBy.Items.Add("LongDescription");
            drpFilterBy.Items.Add("SerialNumber");
            drpFilterBy.Items.Add("ClassificationName");
            drpFilterBy.Items.Add("CategoryName");
            drpFilterBy.Items.Add("BrandName");
            drpFilterBy.Items.Add("ModelName");
            drpFilterBy.Items.Add("PRNumber");
            drpFilterBy.Items.Add("PONumber");
            drpFilterBy.Items.Add("InvoiceNumber");
            drpFilterBy.Items.Add("DRNumber");
            drpFilterBy.Items.Add("IssuanceNo");
            drpFilterBy.Items.Add("EmployeeName");
            drpFilterBy.Items.Add("Site");
            drpFilterBy.Items.Add("Location");
            drpFilterBy.Items.Add("Area");
            drpFilterBy.Items.Add("DateOfAcquisition");
            drpFilterBy.Items.Add("UsefulLife");
            drpFilterBy.Items.Add("RemainingUsefulLife");
            drpFilterBy.Items.Add("MaintenanceDate");
            drpFilterBy.Items.Add("WarrantyStartDate");
            drpFilterBy.Items.Add("WarrantyEndDate");
            drpFilterBy.Items.Add("InsuranceStartDate");
            drpFilterBy.Items.Add("InsuranceEndDate");
            drpFilterBy.Items.Add("ExpiryDate");
            drpFilterBy.Items.Add("AcquiredCost");
            drpFilterBy.Items.Add("StraightLineDepreciation");
            drpFilterBy.Items.Add("DiminishingBalanceDepreciation");
            drpFilterBy.Items.Add("DiminishingBalanceDepreciation");
            drpFilterBy.Items.Add("Status");
            drpFilterBy.Items.Add("DisposedDate");
            drpFilterBy.Items.Add("IssuedDate");
            drpFilterBy.Items.Add("LastMaintenanceDate");
            drpFilterBy.Items.Add("InventoryName");
            drpFilterBy.Items.Add("InventoryCreatedDate");
            drpFilterBy.Items.Add("InventoryStatus");
            drpFilterBy.Items.Add("InventorySite");
            drpFilterBy.Items.Add("InventoryLocation");
        }
        catch
        {
        }
    }
    #endregion
}