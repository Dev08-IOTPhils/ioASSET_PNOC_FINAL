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

public partial class Views_PropertyIssuanceHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    //Pending Upload Issuance Transactions
    [WebMethod]
    public static string GetPendingUploadList(string search, string datefrom, string dateto, string AccessButton)
    {
        PropertyIssuanceHistoryBll y = new PropertyIssuanceHistoryBll();
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("PropertyIssuanceHistory", Common.ConnString, Common.UserName, "1");
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



            DataTable dt = y.IssuancePendingUpload(Common.ConnString, Convert.ToInt32(Common.SiteID), search, datefrom, dateto, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameIssuance = dt.Rows[i]["IssuanceNo"].ToString();
                    string[] filesIssuance = new string[0];
                    try
                    {
                        filesIssuance = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyIssuance/" + foldernameIssuance));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.RemarksTransfer = dt.Rows[i]["DeclineRemarks"].ToString();
                    foreach (string name in filesIssuance)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyIssuance/" + foldernameIssuance + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View ' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["IssuanceNo"] + "','" + dt.Rows[i]["EmployeeName"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["Counter"] + "','" + dt.Rows[i]["CounterChecker"] + "','" + dt.Rows[i]["IsApproved"] + "','" + dt.Rows[i]["Remarks"] + "')\"><span class='fa fa-eye'></span></a></center>";
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
    public static string GetPLPendingUploadList(string issuanceno)
    {
        PropertyIssuanceBll y = new PropertyIssuanceBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.IssuanceNumber = issuanceno;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PLPendingUpload(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();

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
            drpFilterBy.Items.Add("IssuanceNo");
            drpFilterBy.Items.Add("EmployeeName");
            drpFilterBy.Items.Add("Remarks");
            drpFilterBy.Items.Add("CreatedDate");
        }
        catch
        {
        }
    }
}