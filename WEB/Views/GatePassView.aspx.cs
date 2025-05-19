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
using System.Text.RegularExpressions;

public partial class Views_GatePassView : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    [WebMethod]
    public static string GetPropertyGatePassPendingApproval(string search, string AccessButton)
    {
        PropertyGatePassBll y = new PropertyGatePassBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("GatePassView", Common.ConnString, Common.UserName, "1");
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


            DataTable dt = y.GetPropertyGatePassApproved(Common.ConnString,Convert.ToInt32(Common.SiteID), search, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.GatePassNumber = dt.Rows[i]["GatePassNo"].ToString();
                    x.CreatedBy = dt.Rows[i]["AccountableName"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Purpose = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.isOut = "<center><img " + ((bool.Parse(dt.Rows[i]["IsOut"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/></center>";
                    x.isIn = "<center><img " + ((bool.Parse(dt.Rows[i]["IsIn"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/></center>";
                    x.isOutString = dt.Rows[i]["IsOut"].ToString();
                    x.isInString = dt.Rows[i]["IsIn"].ToString();
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewList('" + dt.Rows[i]["GatePassNo"] + "','" + dt.Rows[i]["AccountableName"] + "','" + dt.Rows[i]["CreatedDate"] + "')\"><span class='fa fa-eye'></span></a></center";
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
    public static string PropertyGatePassRequestList(string gatepassno)
    {
        PropertyGatePassBll y = new PropertyGatePassBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyGatePassRequestList(Common.ConnString,gatepassno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();
                    x.Brand = dt.Rows[i]["BrandName"].ToString();
                    x.Model = dt.Rows[i]["ModelName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

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
    public static string CheckIn(string gatepassno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.GatePassNumber = gatepassno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.CheckIn(x);

        return "";
    }

    [WebMethod]
    public static string CheckOut(string gatepassno)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyGatePassBll y = new PropertyGatePassBll();

        x.GatePassNumber = gatepassno;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        y.CheckOut(x);

        return "";
    }
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add("GatePassNo");
            drpFilterBy.Items.Add("AccountableName");
            drpFilterBy.Items.Add("CreatedDate");
            drpFilterBy.Items.Add("Remarks");
            drpFilterBy.Items.Add("Status");
            drpFilterBy.Items.Add("IsOut");
            drpFilterBy.Items.Add("IsIn");
        }
        catch
        {
        }
    }
}