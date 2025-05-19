using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using AdvanceBll;
using AdvanceEntity;

public partial class Files_Data_DynamicLocation : System.Web.UI.Page
{
    LocationBll locationbll = new LocationBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        ParentLocation();
        LocationList.InnerHtml = LoadLocation();
    }

    [WebMethod]
    public void ParentLocation()
    {
        LocationEntity x = new LocationEntity();
        x.ConnectionString = Common.ConnString;


        DataTable dtParentModule = locationbll.GetDynamicLocationList(x);

        ddlParentLocation.DataSource = dtParentModule;
        ddlParentLocation.DataValueField = "LocationID";
        ddlParentLocation.DataTextField = "LocationName";
        ddlParentLocation.DataBind();
        ddlParentLocation.Items.Insert(0, new ListItem("--None--", "0"));

    }


    [WebMethod]
    public static string SaveDynamicLocation(int locationid, string location, string description, int parentlocationid,  bool active, int value)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBLL = new LocationBll();

        locationEntity.LocationID = locationid;
        locationEntity.Location = location;
        locationEntity.ParentLocationID = parentlocationid;
        locationEntity.Description = description;
        locationEntity.Active = active;
        locationEntity.User = Common.UserName;
        locationEntity.ConnectionString = Common.ConnString;
        locationEntity.Value = value;

        return locationBLL.SaveDynamicLocation(locationEntity).ToString();

        //return GetLocation(pageNo);
    }

    protected string LoadLocation()
    {
        string content = "";
        string i = "";
        LocationEntity x = new LocationEntity();
        x.ConnectionString = Common.ConnString;

        DataTable dtLocation = locationbll.GetDynamicLocationTree(x);

        for (int ctr = 0; ctr < dtLocation.Rows.Count; ctr++)
        {

            content += "<li class='sectionko'><input type='checkbox' id='group"+ctr+ "'/><label for='group" + ctr + "' style='font-size:14px;'>&nbsp;" + dtLocation.Rows[ctr]["LocationName"].ToString() + "</label>&nbsp;<button class='btn btn-success btn-xs' title='Add Location' type='button' onclick='AddLocationByID(" + dtLocation.Rows[ctr]["LocationID"].ToString() + ")'><span class='fa fa-plus'></span></button>";
            content += "<ul>";
            if (locationbll.CountSubLocation(Common.ConnString, int.Parse(dtLocation.Rows[ctr]["LocationID"].ToString())) > 0)
            {
                content += GetSubLocation(Common.ConnString, int.Parse(dtLocation.Rows[ctr]["LocationID"].ToString()));
            }
            content += "</ul>";
            content += "</li>";

            i = "";
        }
        //content += "</ul>";
        //Common.ModuleData = content;
        return content;
    }

    protected string GetSubLocation(string ConnString, int parentLocationID)
    {
        string contentsub = "";
        string abspath = HttpContext.Current.Request.Url.AbsolutePath;

        DataTable dtSubMenu = locationbll.GetSubLocation(ConnString, parentLocationID);

        for (int ctr = 0; ctr < dtSubMenu.Rows.Count; ctr++)
        {

            contentsub += "<li class='sectionko'><input type='checkbox' id='sub" + dtSubMenu.Rows[ctr]["LocationID"].ToString() + "'/><label for='sub" + dtSubMenu.Rows[ctr]["LocationID"].ToString() + "'  style='font-size:14px;'>&nbsp;" + dtSubMenu.Rows[ctr]["LocationName"].ToString()+ "</label>&nbsp;<button class='btn btn-success btn-xs' title='Add Location' type='button' onclick='AddLocationByID(" + dtSubMenu.Rows[ctr]["LocationID"].ToString() + ")'><span class='fa fa-plus'></span></button>";
            contentsub += "<ul>";
            if (locationbll.CountSubLocation(ConnString, int.Parse(dtSubMenu.Rows[ctr]["LocationID"].ToString())) > 0)
            {
                contentsub += GetSubLocation(ConnString, int.Parse(dtSubMenu.Rows[ctr]["LocationID"].ToString()));
            }
            contentsub += "</ul>";
            contentsub += "</li>";
        }
        return contentsub;
    }
}