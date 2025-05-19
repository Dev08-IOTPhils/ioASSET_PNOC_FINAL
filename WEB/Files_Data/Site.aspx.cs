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
using AdvanceDal;
using System.Web.Script.Serialization;

public partial class Files_Data_Site : BasePage
{
    string ret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        ret = shSystemConfigurationsSite.Value;
        lblHeader.Text = ret;
        lblbreadCrumb.Text = ret;
        lblListOf.Text = "List of " + ret;
        btnAdd.Value = "Add " + ret;
        lblMasterListSite.Text = ret.ToUpper();
        lblMasterListSite1.Text = ret.ToUpper() + " CODE";
         

        CheckAccess();
    }

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Site.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string SaveWarehouse(int warehouseid, string sitecode, string warehouse, bool active)
    {
        WarehouseEntity warehouseEntity = new WarehouseEntity();
        WarehouseBll warehouseBll = new WarehouseBll();
        WarehouseDal whdal = new WarehouseDal();

        warehouseEntity.WarehouseID = warehouseid;
        warehouseEntity.WarehouseCode = sitecode;
        warehouseEntity.WarehouseName = warehouse;
        warehouseEntity.Active = active;
        warehouseEntity.User = Common.UserName;
        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.SaveWarehouse(warehouseEntity).ToString();
    }

    [WebMethod]
    public static string GetWarehouse()
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        string html = string.Empty;
        List<WarehouseEntity> JsonItemList = new List<WarehouseEntity>();
        try
        {
            DataTable dt = warehouseBll.GetWarehouse(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WarehouseEntity x = new WarehouseEntity();
                    x.WarehouseName = dt.Rows[i]["WarehouseName"].ToString();
                    x.WarehouseCode = dt.Rows[i]["WarehouseCode"].ToString();


                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Site Information' onclick=\"GetWarehouseByID('" + dt.Rows[i]["WarehouseID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Site Information' onclick=\"GetWarehouseByID('" + dt.Rows[i]["WarehouseID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteSite' data-toggle='tooltip' title='Delete Site' onclick=\"deleteWarehouse('" + dt.Rows[i]["WarehouseID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteWarehouse(int warehouseid)
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        WarehouseEntity warehouseEntity = new WarehouseEntity();

        warehouseEntity.WarehouseID = warehouseid;
        warehouseEntity.User = Common.UserName;
        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.DeleteWarehouse(warehouseEntity).ToString();
    }

    [WebMethod]
    public static List<ListItem> GetWarehouseByID(int warehouseid)
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        WarehouseEntity warehouseEntity = new WarehouseEntity();

        warehouseEntity.WarehouseID = warehouseid;
        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.GetWarehouseByID(warehouseEntity);
    }

    [WebMethod]
    public static string WarehouseByID(int warehouseid)
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        WarehouseEntity warehouseEntity = new WarehouseEntity();

        warehouseEntity.WarehouseID = warehouseid;
        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.GetWarehouseByName(warehouseEntity);
    }
}