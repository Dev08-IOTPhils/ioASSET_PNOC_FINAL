using System;
using System.Collections.Generic;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;

public partial class Views_RFIDLogsView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string GetRFIDLOGS(string datefrom, string dateto)
    {
        RFIDLogsViewBLL y = new RFIDLogsViewBLL();
        string html = string.Empty;
        List<RFIDLogsViewEntity> JsonItemList = new List<RFIDLogsViewEntity>();
        try
        {
            DataTable dt = y.GetRFIDLOGS(Common.ConnString, datefrom, dateto);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RFIDLogsViewEntity x = new RFIDLogsViewEntity();
                    x.RFIDLogID = Int32.Parse(dt.Rows[i]["RFIDLogID"].ToString());
                    x.DateTime = dt.Rows[i]["ReadDateTime"].ToString();
                    x.DeviceName = dt.Rows[i]["DeviceName"].ToString();
                    x.IPAddress = dt.Rows[i]["IPAddress"].ToString();
                    x.COMPort = Int32.Parse(dt.Rows[i]["COMPort"].ToString());
                    x.EPC = dt.Rows[i]["RFID"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumner = dt.Rows[i]["PropertyNumber"].ToString();
                    x.SerialNumber = dt.Rows[i]["SerialNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Site = dt.Rows[i]["Site"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Area = dt.Rows[i]["Area"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();
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
}
