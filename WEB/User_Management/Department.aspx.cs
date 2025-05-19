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

public partial class Files_Data_Department : BasePage
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

        string AccessButon = rb.CheckR("User_Management/Department.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    [WebMethod]
    public static string GetDepartment()
    {
        DepartmentBll departmentBll = new DepartmentBll();
        string html = string.Empty;
        List<DepartmentEntity> JsonItemList = new List<DepartmentEntity>();
        try
        {
            DataTable dt = departmentBll.GetDepartment(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DepartmentEntity x = new DepartmentEntity();
                    x.Department = dt.Rows[i]["DepartmentName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Department Information' onclick=\"GetDepartmentID('" + dt.Rows[i]["DepartmentID"] + "')\"><span class='fa fa-edit'></span></button> </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit User Department Information' onclick=\"GetDepartmentID('" + dt.Rows[i]["DepartmentID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteDepartment' data-toggle='tooltip' title='Delete User Department' onclick=\"deleteDepartment('" + dt.Rows[i]["DepartmentID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DepartmentByID(int id)
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.DepartmentID = id;
        departmentEntity.ConnectionString = Common.ConnString;

        return departmentBll.GetDepartmentByName(departmentEntity);
    }
    
    [WebMethod]
    public static string DeleteDepartment(int id)
    {

        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.DepartmentID = id;
        departmentEntity.User = Common.UserName;
        departmentEntity.ConnectionString = Common.ConnString;

        return departmentBll.DeleteDepartment(departmentEntity).ToString();

        
    }

}