using AdvanceBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_InventoryConsolidatedReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void donwloadTextFileProperty(string inventorycode, string report)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("Content-disposition", "attachment;filename=" + inventorycode + "_" + DateTime.Now.ToShortDateString() + ".csv");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.Output.Write(report);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
        HttpContext.Current.Response.End();
    }

    public void btnExportProperty(object sender, EventArgs e)
    {
        //GetInventoryConsolidated(1);
    }

    //[WebMethod]
    //public void GetInventoryConsolidated(int site)
    //{
    //    PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
    //    DataTable dt = y.GetInventoryConsolidated(Common.ConnString,site);

    //    string report = "";


    //    if (dt.Rows.Count > 0)
    //    {
    //        report += "List of Properties" + Environment.NewLine;
    //        report += "Item Code|Property #|Origin Location|Found|Not Found|Misplaced" + Environment.NewLine;

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            report += dt.Rows[i]["ItemCode"].ToString() + "|" + dt.Rows[i]["PropertyNumber"].ToString() + "|";
    //            if (dt.Rows[i]["Area"].ToString() == "NULL" || dt.Rows[i]["Area"].ToString() == null || dt.Rows[i]["Area"].ToString() == "")
    //            {
    //                report += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "|";
    //            }
    //            else
    //            {
    //                report += dt.Rows[i]["WarehouseName"].ToString() + "-" + dt.Rows[i]["Location"].ToString() + "-" + dt.Rows[i]["Area"].ToString() + "|";
    //            }

    //            if (dt.Rows[i]["Status"].ToString() == "0")
    //            {
    //                report += "0|1|0";
    //            }
    //            else if (dt.Rows[i]["Status"].ToString() == "1")
    //            {
    //                report += "1|0|0";
    //            }
    //            else
    //            {
    //                report += "0|0|1";
    //            }
    //            report += Environment.NewLine;
    //        }
    //        report += "||TOTAL|" + GetCountFound(inventorycode) + "|" + GetCountMissing(inventorycode) + "|" + GetCountMisplaced(inventorycode) + Environment.NewLine;
    //        donwloadTextFileProperty(inventorycode, report);
    //        //return report;
    //    }

    //    else
    //        donwloadTextFileProperty("ConsolidatedReport", report);

    //}
}