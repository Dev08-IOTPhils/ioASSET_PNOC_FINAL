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

public partial class Files_Data_JobTitle : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("User_Management/JobTitle.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string GetPosition()
    {
        PositionBll positionBll = new PositionBll();
        string html = string.Empty;
        List<PositionEntity> JsonItemList = new List<PositionEntity>();
        try
        {
            DataTable dt = positionBll.GetPosition(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PositionEntity x = new PositionEntity();
                    x.Position = dt.Rows[i]["PositionName"].ToString();
                    x.Department = dt.Rows[i]["DepartmentName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Job Title Information' onclick=\"GetPositionID('" + dt.Rows[i]["PositionID"] + "')\"><span class='fa fa-edit'></span></button> </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Job Title Information' onclick=\"GetPositionID('" + dt.Rows[i]["PositionID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeletePosition' data-toggle='tooltip' title='Delete job Title' onclick=\"deletePosition('" + dt.Rows[i]["PositionID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

                    }
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

                    JsonItemList.Add(x);

                }

                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList); ;


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
    public static string GetPositionByID(int id)
    {
        PositionEntity positionEntity = new PositionEntity();
        PositionBll positionBll = new PositionBll();

        positionEntity.PositionID = id;
        positionEntity.ConnectionString = Common.ConnString;

        return positionBll.GetPositionByID(positionEntity);
    }

    [WebMethod]
    public static string DeletePosition(int positionid)
    {
        PositionEntity positionEntity = new PositionEntity();
        PositionBll positionBll = new PositionBll();

        positionEntity.PositionID = positionid;
        positionEntity.CreatedBy = Common.UserName;
        positionEntity.ConnectionString = Common.ConnString;

        return positionBll.DeletePosition(positionEntity).ToString();
        
    }
}