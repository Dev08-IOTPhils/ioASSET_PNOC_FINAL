using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;
using System.Data;
using AdvanceEntity;
using AdvanceBll;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

public partial class Views_PropertyMovement : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    [WebMethod]
    public static string GetPropertyMovement(string search, string datefrom, string dateto)
    {
        PropertyMovementBll y = new PropertyMovementBll();
        PropertyMasterlistBll z = new PropertyMasterlistBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            string Filter = "";
            DataTable dtFilter = z.GetActiveFilter("PropertyMovement", Common.ConnString, Common.UserName, "1");
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


            DataTable dt = y.GetPropertyMovement(Common.ConnString, search, datefrom, dateto, Convert.ToInt32(Common.SiteID), Filter);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.PropertyLog = dt.Rows[i]["PropertyLog"].ToString();
                    x.PropertyLogDate = dt.Rows[i]["LogDate"].ToString();

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
            drpFilterBy.Items.Add("PropertyLog");
            drpFilterBy.Items.Add("LogDate");
        }
        catch
        {
        }
    }
}