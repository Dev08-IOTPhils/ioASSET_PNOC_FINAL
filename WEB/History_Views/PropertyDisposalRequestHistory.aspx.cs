using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_PropertyDisposalRequestHistory : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    [WebMethod]
    public static string GetDisposalHistory(string search, string datefrom, string dateto, string AccessButton)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("PropertyDisposalRequestHistory", Common.ConnString, Common.UserName, "1");
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


            DataTable dt = y.GetDisposalRequestHistory(Common.ConnString, Convert.ToInt32(Common.SiteID), search, datefrom, dateto, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameDisposal = dt.Rows[i]["DisposalNo"].ToString();
                    string[] filesGR = new string[0];
                    try
                    {
                        filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyDisposalRequest/" + foldernameDisposal));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.DisposalNumber = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactorName = dt.Rows[i]["TransactorName"].ToString();
                    x.NoOfItemString = dt.Rows[i]["Quantity"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    x.Remarks = dt.Rows[i]["DeclineRemarks"].ToString();
                    foreach (string name in filesGR)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyDisposalRequest/" + foldernameDisposal + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewDisposalDetails('" + dt.Rows[i]["DisposalNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["IsCertified"] + "')\"><span class='fa fa-eye'></span></a></center>";
                    }
                    //" <a href='' class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' ><span class='fa fa-times-circle'></span></a></center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static string GetPLPendingUploadList(string disposalno)
    {
        PropertyDisposalBll y = new PropertyDisposalBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.DisposalNumber = disposalno;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PDRLPendingUpload(propertyEntity);
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
                    x.Condition = dt.Rows[i]["Condition"].ToString();

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
            drpFilterBy.Items.Add("DisposalNo");
            drpFilterBy.Items.Add("TransactorName");
            drpFilterBy.Items.Add("Quantity");
            drpFilterBy.Items.Add("CreatedDate");
            drpFilterBy.Items.Add("Status");

        }
        catch
        {
        }
    }
}