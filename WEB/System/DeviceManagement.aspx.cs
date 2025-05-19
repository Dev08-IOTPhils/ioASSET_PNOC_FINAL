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

public partial class System_DeviceManagement : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }



    [WebMethod]
    public static string DeviceByID(int id)
    {
        DeviceEntity deviceEntity = new DeviceEntity();
        DeviceBll deviceBll = new DeviceBll();

        deviceEntity.DeviceID = id;
        deviceEntity.ConnectionString = Common.ConnString;

        return deviceBll.DevicebyID(deviceEntity);
    }


    [WebMethod]
    public static string GetDevice()
    {
        DeviceBll deviceBll = new DeviceBll();
        string html = string.Empty;
        List<DeviceEntity> JsonItemList = new List<DeviceEntity>();
        try
        {
            DataTable dt = deviceBll.GetDevice(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DeviceEntity x = new DeviceEntity();
                    x.DeviceName = dt.Rows[i]["DeviceName"].ToString();
                    x.IPAddress = dt.Rows[i]["IPaddress"].ToString();
                    x.PortString = dt.Rows[i]["Port"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.DeviceUsageType = dt.Rows[i]["DeviceType"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Device Information' onclick=\"GetDeviceID('" + dt.Rows[i]["DeviceID"] + "')\"><span class='fa fa-edit'></span></button> "
                        + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteDeviceMngt' data-toggle='tooltip' title='Delete Device' onclick=\"deleteDevice('" + dt.Rows[i]["DeviceID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteDevice(int id)
    {
        DeviceEntity deviceEntity = new DeviceEntity();
        DeviceBll deviceBll = new DeviceBll();

        deviceEntity.DeviceID = id;
        deviceEntity.CreatedBy = Common.UserID;
        deviceEntity.ConnectionString = Common.ConnString;

        return deviceBll.DeleteDevice(deviceEntity).ToString();
        
    }
}