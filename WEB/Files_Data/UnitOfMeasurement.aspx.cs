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

public partial class Files_Data_UnitOfMeasurement : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UnitOfMeasurementList.InnerHtml = GetUnitOfMeasurement("", 1);
        CheckAccess();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/UnitOfMeasurement.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    [WebMethod]
    public static int CheckUOMCode(int id, string cc)
    {
        UOMEntity uomEntity = new UOMEntity();
        UOMBll uomBll = new UOMBll();

        uomEntity.UnitOfMeasurementID = id;
        uomEntity.UOMCode = cc;
        uomEntity.ConnectionString = Common.ConnString;

        return uomBll.checkUOMCode(uomEntity);
    }

    [WebMethod]
    public static string SaveUnitOfMeasurement(int id, string unitofmeasurement, string code, string description, bool active)
    {
        UOMEntity uomEntity = new UOMEntity();
        UOMBll UOMBll = new UOMBll();

        uomEntity.UnitOfMeasurementID = id;
        uomEntity.UnitOfMeasurement= unitofmeasurement;
        uomEntity.UOMCode = code;
        uomEntity.Description = description;
        uomEntity.Active = active;
        uomEntity.CreatedBy = Common.UserName;
        uomEntity.ConnectionString = Common.ConnString;

        return UOMBll.SaveUOM(uomEntity).ToString();

        //return GetUnitOfMeasurement(UOMEntity,pageNo);
    }

    [WebMethod]
    public static string UnitOfMeasurementbyID(int id)
    {
        UOMBll UOMBll = new UOMBll();

        UOMEntity uomEntity = new UOMEntity();

        uomEntity.UnitOfMeasurementID = id;
        uomEntity.ConnectionString = Common.ConnString;

        return UOMBll.UnitOfMeasurementByID(uomEntity);
    }

    [WebMethod]
    public static string GetUOM()
    {
        UOMBll UOMBll = new UOMBll();
        string html = string.Empty;
        List<UOMEntity> JsonItemList = new List<UOMEntity>();
        try
        {
            DataTable dt = UOMBll.GetUOM(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UOMEntity x = new UOMEntity();
                    x.UnitOfMeasurement = dt.Rows[i]["UnitOfMeasurement"].ToString();
                    x.UOMCode = dt.Rows[i]["UOMCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Unit of Measurement Information' onclick=\"GetUnitOfMeasurementID('" + dt.Rows[i]["UnitOfMeasurementID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Unit of Measurement Information' onclick=\"GetUnitOfMeasurementID('" + dt.Rows[i]["UnitOfMeasurementID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteUOM' data-toggle='tooltip' title='Delete Unit of Measurement' onclick=\"deleteUnitOfMeasurement('" + dt.Rows[i]["UnitOfMeasurementID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteUnitOfMeasurement(int id)
    {
        UOMBll UOMBll = new UOMBll();
        UOMEntity uomEntity = new UOMEntity();

        uomEntity.UnitOfMeasurementID = id;
        uomEntity.CreatedBy = Common.UserName;
        uomEntity.ConnectionString = Common.ConnString;

        return UOMBll.DeleteUOM(uomEntity).ToString();

        
    }
}