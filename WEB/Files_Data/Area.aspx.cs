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

public partial class Files_Data_Area : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        // GetLocation();
        SystemConfigurations shSystemConfigurationsArea = (SystemConfigurations)Session["shSystemConfigurationsArea"];
        ret = shSystemConfigurationsArea.Value;
        SystemConfigurations shSystemConfigurationsLoc = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        SystemConfigurations shSystemConfigurationSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];

        lblHeader.Text = ret;
        lblbreadCrumb.Text = ret;
        lblListOf.Text = "List of " + ret;
        btnAdd.Value = "Add " + ret;
        lblListArea.Text = ret.ToUpper();
        lblListLoc.Text = shSystemConfigurationsLoc.Value.ToUpper();
        lblListSite.Text = shSystemConfigurationSite.Value.ToUpper();

    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Area.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    //[WebMethod]
    //public void GetLocation()
    //{
    //    LocationEntity locationEntity = new LocationEntity();
    //    LocationBll locationBll = new LocationBll();

    //    locationEntity.ConnectionString = Common.ConnString;

    //    DataTable dt = locationBll.GetLocationList(locationEntity);

    //    ddlLocation.DataSource = dt;
    //    ddlLocation.DataValueField = "LocationID";
    //    ddlLocation.DataTextField = "Location";
    //    ddlLocation.DataBind();
    //    ddlLocation.Items.Insert(0, "--- Select Branch ---");
    //}


    [WebMethod]
    public static string SaveArea(int id, int locationid, string Area, string description, bool active)
    {
        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.LocationID = locationid;
        areaEntity.AreaID = id;
        areaEntity.Area = Area;
        areaEntity.Description = description;
        areaEntity.Active = active;
        areaEntity.User = Common.UserName;
        areaEntity.ConnectionString = Common.ConnString;

        return areaBll.SaveArea(areaEntity).ToString();
    }

    [WebMethod]
    public static string AreaByID(int id)
    {
        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.AreaID = id;
        areaEntity.ConnectionString = Common.ConnString;

        return areaBll.GetAreaByName(areaEntity);
    }


    [WebMethod]
    public static string GetArea()
    {
        AreaBll areaBll = new AreaBll();
        string html = string.Empty;
        List<AreaEntity> JsonItemList = new List<AreaEntity>();
        try
        {
            DataTable dt = areaBll.GetArea(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AreaEntity x = new AreaEntity();
                    x.Area = dt.Rows[i]["Area"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Area Information' onclick=\"GetAreaID('" + dt.Rows[i]["AreaID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Area Information' onclick=\"GetAreaID('" + dt.Rows[i]["AreaID"] + "')\"><span class='fa fa-edit'></span></button> "
                            + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteArea' data-toggle='tooltip' title='Delete Area' onclick=\"deleteArea('" + dt.Rows[i]["AreaID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
                    }
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
    public static string DeleteArea(int id)
    {

        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.AreaID = id;
        areaEntity.User = Common.UserName;
        areaEntity.ConnectionString = Common.ConnString;

        return areaBll.DeleteArea(areaEntity).ToString();

    }
}