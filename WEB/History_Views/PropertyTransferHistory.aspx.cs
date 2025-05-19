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

public partial class Views_PropertyTransferHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    [WebMethod]
    public static string PropertyTransferHistory(string search, string datefrom, string dateto, string AccessButton)
    {
        PropertyTransferHistoryBll y = new PropertyTransferHistoryBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("PropertyTransferHistory", Common.ConnString, Common.UserName, "1");
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

            DataTable dt = y.PropertyTransferHistory(Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), search, datefrom, dateto, Filter, Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameTransfer = dt.Rows[i]["TransferNumber"].ToString();
                    string[] filesTransfer = new string[0];
                    try
                    {
                        filesTransfer = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyTransfer/" + foldernameTransfer));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.TransferNumber = dt.Rows[i]["TransferNumber"].ToString();
                    x.RemarksTransfer = dt.Rows[i]["Remarks"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.DateOfTransfer = dt.Rows[i]["CreatedDate"].ToString();
                    x.Remarks = dt.Rows[i]["DeclineRemarks"].ToString();
                    foreach (string name in filesTransfer)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyTransfer/" + foldernameTransfer + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = "<center><button class='btn btn-xs btn-primary btn_View btn_ViewPTH' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["TransferNumber"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "')\"><span class='fa fa-eye'></span></button></center>";
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
    public static string PLPendingApproval(string transactionno)
    {
        PropertyTransferBll propertytransferBll = new PropertyTransferBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertytransferBll.PLPendingApproval(Common.ConnString, transactionno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    if (dt.Rows[i]["TransactionType"].ToString() == "1")
                    {
                        x.Origin = dt.Rows[i]["FromEmployeeName"].ToString();
                        x.Destination = dt.Rows[i]["ToEmployeeName"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[i]["FromAreaName"].ToString() != "")
                        {
                            x.Origin = dt.Rows[i]["FromSiteName"].ToString() + "-" + dt.Rows[i]["FromLocationName"].ToString() + "-" + dt.Rows[i]["FromAreaName"].ToString();
                        }
                        else
                        {
                            x.Origin = dt.Rows[i]["FromSiteName"].ToString() + "-" + dt.Rows[i]["FromLocationName"].ToString();
                        }

                        if (dt.Rows[i]["ToAreaName"].ToString() != "")
                        {
                            x.Destination = dt.Rows[i]["ToSiteName"].ToString() + "-" + dt.Rows[i]["ToLocationName"].ToString() + "-" + dt.Rows[i]["ToAreaName"].ToString();
                        }
                        else
                        {
                            x.Destination = dt.Rows[i]["ToSiteName"].ToString() + "-" + dt.Rows[i]["ToLocationName"].ToString();
                        }
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
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add("TransferNumber");
            drpFilterBy.Items.Add("Remarks");
            drpFilterBy.Items.Add("TransactorName");
            drpFilterBy.Items.Add("CreatedDate");
        }
        catch
        {
        }
    }
}