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
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

public partial class Files_Data_Location : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsLoc = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        ret = shSystemConfigurationsLoc.Value;
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];


        lblHeader.Text = ret;
        lblbreadCrumb.Text = ret;
        lblListOf.Text = "List of " + ret;
        btnAdd.Value = "Add " + ret;
        lblListLoc.Text = ret.ToUpper();
        lblListLoc1.Text = ret.ToUpper() + " CODE";
        lblListArea.Text = shSystemConfigurationsSite.Value.ToUpper();


        CheckAccess();
      //  GetWarehouseList();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Location.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    //[WebMethod]
    //public void GetWarehouseList()
    //{
    //    WarehouseEntity warehouseEntity = new WarehouseEntity();
    //    WarehouseBll warehouseBll = new WarehouseBll();


    //    DataTable dtWarehouse = warehouseBll.GetWarehouseList(Common.ConnString);

    //    ddlWarehouse.DataSource = dtWarehouse;
    //    ddlWarehouse.DataValueField = "WarehouseID";
    //    ddlWarehouse.DataTextField = "WarehouseName";
    //    ddlWarehouse.DataBind();
    //    ddlWarehouse.Items.Insert(0, "--- Select Region ---");
    //}



    [WebMethod]
    public static string SaveLocation(int locationid, string location, string locationcode, int warehouseid, string description, bool active)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBLL = new LocationBll();

        locationEntity.LocationID = locationid;
        locationEntity.Location = location;
        locationEntity.LocationCode = locationcode;
        locationEntity.WarehouseID = warehouseid;
        locationEntity.Description = description;
        locationEntity.Active = active;
        locationEntity.User = Common.UserName;
        locationEntity.ConnectionString = Common.ConnString;

        return locationBLL.SaveLocation(locationEntity).ToString();

        //return GetLocation(pageNo);
    }

    [WebMethod]
    public static int CheckLC(int id, string lc)
    {
        LocationEntity x = new LocationEntity();
        LocationBll y = new LocationBll();

        x.LocationID = id;
        x.LocationCode = lc;
        x.ConnectionString = Common.ConnString;

        return y.CheckLC(x);
    }

    [WebMethod]
    public static string GetLocation()
    {
        LocationBll locationBLL = new LocationBll();
        string html = string.Empty;
        List<LocationEntity> JsonItemList = new List<LocationEntity>();
        try
        {
            DataTable dt = locationBLL.GetLocation(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LocationEntity x = new LocationEntity();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.LocationCode = dt.Rows[i]["LocationCode"].ToString();
                    x.Warehouse = dt.Rows[i]["WarehouseName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = "<center>";
                    if (EditAccess == "1")
                    {
                        x.Action += "<button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Location Information' onclick=\"GetLocationByName('" + dt.Rows[i]["LocationID"] + "')\"><span class='fa fa-edit'></span></button> ";
                    }
                    if (DeleteAccess == "1")
                    {
                        x.Action += " <button type='button' class='btn btn-xs btn-danger btn_Delete' data-toggle='tooltip' title='Delete Location' onclick=\"deleteLocation('" + dt.Rows[i]["LocationID"] + "')\"><span class='fa fa-trash'></span></button> ";
                        // x.Action += " <button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Location Information' onclick=\"GetLocationByName('" + dt.Rows[i]["LocationID"] + "')\"><span class='fa fa-edit'></span></button>";

                    }

                    //if (EditAccess == "0")
                    //{
                    //    x.Action += "<button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Location Information' onclick=\"GetLocationByName('" + dt.Rows[i]["LocationID"] + "')\"><span class='fa fa-edit'></span></button> ";

                    //}
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

                    x.Action += "</center>";
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
    public static string DeleteLocation(int locationid)
    {
        LocationBll locationBLL = new LocationBll();
        LocationEntity locationEntity = new LocationEntity();

        locationEntity.LocationID = locationid;
        locationEntity.User = Common.UserName;
        locationEntity.ConnectionString = Common.ConnString;

        return locationBLL.DeleteLocation(locationEntity).ToString();

    }

    [WebMethod]
    public static string GetLocationByName(int locationid)
    {
        LocationBll locationBLL = new LocationBll();
        LocationEntity locationEntity = new LocationEntity();

        locationEntity.LocationID = locationid;
        locationEntity.ConnectionString = Common.ConnString;

        return locationBLL.GetLocationByName(locationEntity);
    }
}