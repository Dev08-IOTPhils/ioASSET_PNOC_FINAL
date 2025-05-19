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

public partial class Views_PropertyMaintenanceHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    //Properties Available For Maintenance
    [WebMethod]
    public static string GetPropertyOnMaintenance(string search, string datefrom, string dateto,string AccessButton)
    {
        PropertyMaintenanceHistoryBll y = new PropertyMaintenanceHistoryBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("PropertyMaintenanceHistory", Common.ConnString, Common.UserName, "1");
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


            DataTable dt = y.GetAvailablePropertyOnMaintenance(Common.ConnString,search, Convert.ToInt32(Common.SiteID), datefrom, dateto, Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.DateOfCreation = dt.Rows[i]["CreatedDate"].ToString();
                    x.NoOfDaysFromCreation = dt.Rows[i]["NoOfDaysCreatedDate"].ToString();
                    x.NextStep = dt.Rows[i]["NextStep"].ToString();
                    x.PercentageOfCompletion = "<a href='#' class='percent_design' data-toggle='tooltip' title='" + dt.Rows[i]["PercentageOfCompletion"].ToString() + "% Complete'><h3><small class='pull-right'>" + dt.Rows[i]["PercentageOfCompletion"].ToString() + "%</small></h3>"
                      + "<div class='progress xs'>"
                      + "<div class='progress-bar progress-bar-red' style='width: " + dt.Rows[i]["PercentageOfCompletion"].ToString() + "%' role='progressbar' aria-valuenow='20' aria-valuemin='0' aria-valuemax='100'>"
                      //+ "<span class='sr-only'>" + dt.Rows[i]["PercentageOfCompletion"].ToString() + "% Complete</span>"
                      + "</div>"
                      + "</div>"
                       + "</a>";//dt.Rows[i]["PercentageOfCompletion"].ToString();
                    x.MaintenanceRemarks = dt.Rows[i]["MaintenanceRemarks"].ToString();
                    x.MaintenanceType = dt.Rows[i]["MaintenanceType"].ToString();
                    if (AccessButton == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-sm btn-primary btn_View' data-toggle='tooltip'  title='View Task List' onclick=\"ViewTaskList('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + dt.Rows[i]["Description"] + "','" + dt.Rows[i]["TaskID"] + "','" + dt.Rows[i]["Step"] + "','" + dt.Rows[i]["MaintenanceRemarks"] + "','" + dt.Rows[i]["TaskName"] + "','" + dt.Rows[i]["MaintenanceType"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["TransactorName"] + "')\"><span class='fa fa-eye'></span></a></center> ";
                    }
                    //   + "<a href = '' class='btn btn-sm btn-danger btn_Stop' data-toggle='tooltip'  title='Terminate Property Maintenance' onclick=\"TerminateMaintenance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + dt.Rows[i]["Description"] + "','" + dt.Rows[i]["TaskID"] + "','" + dt.Rows[i]["Step"] + "','" + dt.Rows[i]["NumberOfSteps"] + "','" + dt.Rows[i]["EmployeeID"] + "','" + dt.Rows[i]["ReturnRequestNo"] + "')\"><span class='fa fa-stop'></span></a> </center>";
                    //    " <a href='' class='btn btn-xs btn-info btn_attach'><span class='fa fa-upload'></span></a> " +
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
    public static string PropertyMaintenanceSteps(int recid, string propertyno)
    {
        PropertyMaintenanceBll y = new PropertyMaintenanceBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyMaintenanceSteps(Common.ConnString,recid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string foldernameMaintenance = recid.ToString() + "-" + propertyno;
                    string[] fileMaintenance = new string[0];
                    try
                    {
                        fileMaintenance = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/PropertyMaintenance/" + foldernameMaintenance + "/" + dt.Rows[i]["Step"].ToString()));
                    }
                    catch
                    {

                    }
                    PropertyEntity x = new PropertyEntity();
                    x.MaintenanceStep = dt.Rows[i]["NextStep"].ToString();
                    x.ServiceProvider = dt.Rows[i]["SupplierName"].ToString();
                    x.DateOfCreation = dt.Rows[i]["CreatedDate"].ToString();
                    x.MaintenanceRemarks = dt.Rows[i]["Remarks"].ToString();
                    x.MaintenanceStatus = dt.Rows[i]["Status"].ToString();
                    foreach (string name in fileMaintenance)
                    {
                        int j = name.Split('\\').Length;
                        string[] file = name.Split('\\');
                        x.SupportingDocument += "<a target='_blank' href='../SupportingDocuments/PropertyMaintenance/" + foldernameMaintenance + "/" + dt.Rows[i]["Step"].ToString() + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><br>";
                    }
                    //x.Action = " <center><a href='' class='btn btn-sm btn-warning btn_Edit' data-toggle='tooltip'  title='Update Property Maintenance' onclick=\"UpdateMaintenance('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["PropertyNumber"] + "','" + dt.Rows[i]["Description"] + "','" + dt.Rows[i]["TaskID"] + "','" + dt.Rows[i]["Step"] + "','" + dt.Rows[i]["MaintenanceRemarks"] + "')\"><span class='fa fa-edit'></span></a> </center>";
                    //    " <a href='' class='btn btn-xs btn-info btn_attach'><span class='fa fa-upload'></span></a> " +
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
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add("PropertyNumber");
            drpFilterBy.Items.Add("Description");
            drpFilterBy.Items.Add("SerialNumber");
            drpFilterBy.Items.Add("TransactorName");
            drpFilterBy.Items.Add("CreatedDate");
            drpFilterBy.Items.Add("NoOfDaysCreatedDate");
            drpFilterBy.Items.Add("NextStep");
            drpFilterBy.Items.Add("PercentageOfCompletion");
            drpFilterBy.Items.Add("MaintenanceRemarks");
            drpFilterBy.Items.Add("MaintenanceType");
        }
        catch
        {
        }
    }
}