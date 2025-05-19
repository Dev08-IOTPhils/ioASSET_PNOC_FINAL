using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceEntity;
using AdvanceBll;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Web.Services;
using System.Text.RegularExpressions;

public partial class Views_GoodsReceiveHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    [WebMethod]
    public static string GetGoodsReceiveHeader(string search, string datefrom, string dateto, string AccessButton)
    {
        GoodsReceiveHistoryBll y = new GoodsReceiveHistoryBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("GoodsReceiveHistory", Common.ConnString, Common.UserName, "1");
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



            DataTable dt = y.GetGoodsReceiveHeader(Common.ConnString, Convert.ToInt32(Common.SiteID), search, datefrom, dateto, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameGR = dt.Rows[i]["GoodsReceiveNumber"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/GoodsReceive/" + foldernameGR));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.GoodsReceiveNumber = dt.Rows[i]["GoodsReceiveNumber"].ToString();
                    x.Receiver = dt.Rows[i]["Receiver"].ToString();
                    x.NoOfItem = dt.Rows[i]["NoOfItem"].ToString();
                    x.CreateDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.MaintenanceRemarks = dt.Rows[i]["DeclineRemarks"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/GoodsReceive/" + foldernameGR + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-med  btn-primary fa fa-eye btn_View btn_ViewGRH' type='button' data-toggle='tooltip'  title='View Details' onclick=\"viewGRDetails('" + dt.Rows[i]["GoodsReceiveID"] + "','" + dt.Rows[i]["GoodsReceiveNumber"] + "','" + dt.Rows[i]["Receiver"] + "','" + dt.Rows[i]["CreatedDate"] + "')\"></button></center> ";
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
    public static string GetGoodsReceiveDetails(int goodsreceiveid)
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, goodsreceiveid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
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

    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add(" Select Filter ");
            drpFilterBy.Items.Add("GoodsReceiveNumber");
            drpFilterBy.Items.Add("Receiver");
            drpFilterBy.Items.Add("NoOfItem");
            drpFilterBy.Items.Add("CreatedDate");
        }
        catch
        {
        }
    }
}