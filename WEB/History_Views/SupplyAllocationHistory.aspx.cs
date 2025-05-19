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

public partial class Views_StockIssuanceHistory : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        //ret = shSystemConfigurationsSite.Value;
        //lblMasterListSite.Text = ret.ToUpper();
        //GetFlterTable();
    }

    [WebMethod]
    public static string StockAllocationHistory(string search, string datefrom, string dateto, string AccessButton)
    {
        SupplyAllocationBll y = new SupplyAllocationBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            string Filter = "";
            //DataTable dtFilter = z.GetActiveFilter("SupplyAllocationHistory", Common.ConnString, Common.UserName, "1");
            //if (dtFilter.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtFilter.Rows.Count; i++)
            //    {
            //        Regex reg = new Regex("[*'\";]");
            //        string Value = reg.Replace(dtFilter.Rows[i]["Values"].ToString(), string.Empty);
            //        string OperatorValue = dtFilter.Rows[i]["Operator"].ToString();
            //        if (OperatorValue == "Not =")
            //        {
            //            OperatorValue = " !='" + Value + "' ";
            //        }
            //        else if (OperatorValue == "Like")
            //        {
            //            OperatorValue = " like '%" + Value + "%' ";
            //        }

            //        else if (OperatorValue == "In" || OperatorValue == "Not In")
            //        {
            //            if (Value.Contains(','))
            //            {
            //                string[] multipleValue = Value.Split(',');
            //                string itemValue = "";
            //                foreach (var item in multipleValue)
            //                {
            //                    itemValue += "'" + item + "',";
            //                }
            //                OperatorValue = " " + OperatorValue + " (" + itemValue.Remove(itemValue.Length - 1) + ") ";
            //            }
            //            else
            //            {
            //                OperatorValue = " " + OperatorValue + " ('" + Value + "')";
            //            }
            //        }
            //        else if (OperatorValue == ">" || OperatorValue == ">=" || OperatorValue == "<" || OperatorValue == "<=")
            //        {SupplyAllocationHistory
            //            OperatorValue = " " + OperatorValue + Value;
            //        }
            //        else
            //        {
            //            OperatorValue = OperatorValue + " '" + Value + "'";
            //        }
            //        //if (i == 0)
            //        //{
            //        //    Filter += " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
            //        //}
            //        //else
            //        //{
            //        //    Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
            //        //}

            //        Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
            //    }
            //}

            DataTable dt = y.StockAllocationHistory(Common.ConnString, Convert.ToInt32(Common.SiteID), Common.CustodianID, search, datefrom, dateto, Filter, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["StockAllocationNo"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/StockAllocation/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    StockEntity x = new StockEntity();
                    x.StockAllocationNo = dt.Rows[i]["StockAllocationNo"].ToString();
                    x.Description = dt.Rows[i]["Remarks"].ToString();
                    x.FromSiteStr = dt.Rows[i]["FromSiteName"].ToString();
                    x.ToSiteStr = dt.Rows[i]["ToSiteName"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.TransactionDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.CancelRemarks = dt.Rows[i]["DeclineRemarks"].ToString();

                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/StockAllocation/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-primary btn_View' data-toggle='tooltip' title='View Transaction Details' type='button' onclick=\"ViewStocks('" + dt.Rows[i]["StockAllocationNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "')\"><span class='fa fa-eye'></span></button> "
                     + "</center>";
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
    public static string GetSALPendingUpload(string stockissuanceno)
    {
        SupplyAllocationBll y = new SupplyAllocationBll();
        StockEntity se = new StockEntity();

        se.DisposalNumber = stockissuanceno;
        se.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.SALPendingUpload(se);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.StockAllocationNo = dt.Rows[i]["StockAllocationNo"].ToString();
                    x.Remarks = dt.Rows[i]["Description"].ToString();
                    x.FromSiteStr = dt.Rows[i]["FromSiteName"].ToString();
                    x.ToSiteStr = dt.Rows[i]["ToSiteName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
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

    [WebMethod]
    public static string StockDetailsPendingUpload(string stockissuanceno)
    {
        StockIssuanceBll y = new StockIssuanceBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, stockissuanceno);
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
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();

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
            //drpFilterBy.Items.Add("IssuanceNo");
            //drpFilterBy.Items.Add("EmployeeName");
            //drpFilterBy.Items.Add("Remarks");
            //drpFilterBy.Items.Add("NoOfItem");
            //drpFilterBy.Items.Add("CreatedDate");
            //drpFilterBy.Items.Add("Status");

        }
        catch
        {
        }
    }
}