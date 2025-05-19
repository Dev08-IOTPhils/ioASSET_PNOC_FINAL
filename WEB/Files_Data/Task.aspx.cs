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

public partial class Files_Data_Task : BasePage
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

        string AccessButon = rb.CheckR("Files_Data/Task.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    [WebMethod]
    public static string SaveTask(int id, string task, string description,int numberofsteps, string steps, bool active)
    {
        TaskEntity TaskEntity = new TaskEntity();
        TaskBll TaskBll = new TaskBll();

        TaskEntity.TaskID = id;
        TaskEntity.TaskName = task;
        TaskEntity.Description = description;
        TaskEntity.NumberOfSteps = numberofsteps;
        TaskEntity.Steps = steps;
        TaskEntity.Active = active;
        TaskEntity.CreatedBy = Common.UserName;
        TaskEntity.ConnectionString = Common.ConnString;

        return TaskBll.SaveTask(TaskEntity).ToString();

    }

    [WebMethod]
    public static string TaskByID(int id)
    {
        TaskEntity TaskEntity = new TaskEntity();
        TaskBll TaskBll = new TaskBll();

        TaskEntity.TaskID = id;
        TaskEntity.ConnectionString = Common.ConnString;

        return TaskBll.TaskbyID(TaskEntity);
    }


    [WebMethod]
    public static string GetTask()
    {
        TaskBll TaskBll = new TaskBll();
        string html = string.Empty;
        List<TaskEntity> JsonItemList = new List<TaskEntity>();
        try
        {
            DataTable dt = TaskBll.GetTask(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TaskEntity x = new TaskEntity();
                    x.TaskName = dt.Rows[i]["TaskName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.StringNumberOfSteps = dt.Rows[i]["NumberOfSteps"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Task Information' onclick=\"GetTaskID('" + dt.Rows[i]["TaskID"] + "')\"><span class='fa fa-edit'></span></button> </center>";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Task Information' onclick=\"GetTaskID('" + dt.Rows[i]["TaskID"] + "')\"><span class='fa fa-edit'></span></button> "
                           + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteTask' data-toggle='tooltip' title='Delete Task' onclick=\"deleteTask('" + dt.Rows[i]["TaskID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
                    }
                    

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
    public static string DeleteTask(int id)
    {
        TaskEntity TaskEntity = new TaskEntity();
        TaskBll TaskBll = new TaskBll();

        TaskEntity.TaskID = id;
        TaskEntity.CreatedBy = Common.UserName;
        TaskEntity.ConnectionString = Common.ConnString;

        return TaskBll.DeleteTask(TaskEntity).ToString();
    }
}