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

public partial class Views_StockMasterList : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        ret = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = ret.ToUpper();
        GetFlterTable();
    }

    [WebMethod]
    public static string GetStockMasterList(string search)
    {
        StockIssuanceBll y = new StockIssuanceBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();


        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("StockMasterlist", Common.ConnString, Common.UserName, "1");
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

                    Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                }
            }


            DataTable dt = y.GetStockMasterList(Common.ConnString, Convert.ToInt32(Common.SiteID), search, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    //x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    x.CriticalLevelString = dt.Rows[i]["CriticalLevel"].ToString();
                    x.PRNumber = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    x.PONumber = BasePage.ConvertToMoneyFormat(dt.Rows[i]["TotalCost"].ToString());

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
    public static string GetStockMasterListSMPI()
    {
        StockIssuanceBll y = new StockIssuanceBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();


        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetStockMasterListSMPI(Common.ConnString, Convert.ToInt32(Common.SiteID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["TotalQuantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.CriticalLevelString = dt.Rows[i]["CriticalLevel"].ToString();

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
    public static string GetStockMinimumLevel(string search)
    {
        StockIssuanceBll y = new StockIssuanceBll();


        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetStockMinimumLevel(Common.ConnString, Convert.ToInt32(Common.SiteID), search);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    x.CriticalLevelString = dt.Rows[i]["CriticalLevel"].ToString();

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
            drpFilterBy.Items.Add("Quantity");
            drpFilterBy.Items.Add("UOMcode");
            drpFilterBy.Items.Add("Custodian");
            drpFilterBy.Items.Add("Site");
            drpFilterBy.Items.Add("CriticalLevel");
        }
        catch
        {
        }
    }

}