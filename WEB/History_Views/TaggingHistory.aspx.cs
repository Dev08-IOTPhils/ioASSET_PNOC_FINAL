using AdvanceBll;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Services;

public partial class Views_TaggingHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add(" Select Filter ");
            drpFilterBy.Items.Add("PropertyNumber");
            drpFilterBy.Items.Add("PrinterName");
            drpFilterBy.Items.Add("IPAddress");
            drpFilterBy.Items.Add("DateCreated");
        }
        catch
        {
        }
    }
    [WebMethod]
    public static string SearchTaggingHistory(string datefrom, string dateto)
    {
        try
        {
            TaggingHistoryBll taggingHistoryBll = new TaggingHistoryBll();

            string Filter = "";
            DataTable dtFilter = taggingHistoryBll.GetActiveFilter("TaggingHistory", Common.ConnString, Common.UserName, "1");
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
                        if (Value.Contains(","))
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




            DataTable dt = taggingHistoryBll.GetTaggingHistory(datefrom, dateto, Common.ConnString, Filter);

            return Converter.ConvertDataTabletoJSON(dt);
        }
        catch (Exception ex)
        {
            string xx = ex.Message;
            return "";
        }
    }
}