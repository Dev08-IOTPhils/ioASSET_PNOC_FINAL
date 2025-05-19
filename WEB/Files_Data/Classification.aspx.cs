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

public partial class Files_Data_Classification : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ClassificationList.InnerHtml = GetClassification("",1);
        CheckAccess();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Classification.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string ClassificationByID(int id)
    {
        ClassificationEntity classificationEntity = new ClassificationEntity();
        ClassificationBll classificationBll = new ClassificationBll();

        classificationEntity.ClassificationID = id;
        classificationEntity.ConnectionString = Common.ConnString;

        return classificationBll.ClassificationbyID(classificationEntity);
    }

    [WebMethod]
    public static int CheckCC(int id, string cc)
    {
        ClassificationEntity classificationEntity = new ClassificationEntity();
        ClassificationBll classificationBll = new ClassificationBll();

        classificationEntity.ClassificationID = id;
        classificationEntity.ClassificationCode = cc;
        classificationEntity.ConnectionString = Common.ConnString;

        return classificationBll.checkCC(classificationEntity);
    }

    [WebMethod]
    public static string GetClassification()
    {
        ClassificationBll classificationBll = new ClassificationBll();
        string html = string.Empty;
        List<ClassificationEntity> JsonItemList = new List<ClassificationEntity>();
        try
        {
            DataTable dt = classificationBll.GetClassification(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClassificationEntity x = new ClassificationEntity();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.ClassificationCode = dt.Rows[i]["ClassificationCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.IsFloater = "<img " + ((bool.Parse(dt.Rows[i]["IsFloater"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";



                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Classification Information' onclick=\"GetClassificationID('" + dt.Rows[i]["ClassificationID"] + "')\"><span class='fa fa-edit'></span></button> </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Classification Information' onclick=\"GetClassificationID('" + dt.Rows[i]["ClassificationID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteClass' data-toggle='tooltip' title='Delete Classification' onclick=\"deleteClassification('" + dt.Rows[i]["ClassificationID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteClassification(int id)
    {
        ClassificationEntity classificationEntity = new ClassificationEntity();
        ClassificationBll classificationBll = new ClassificationBll();

        classificationEntity.ClassificationID = id;
        classificationEntity.CreatedBy = Common.UserName;
        classificationEntity.ConnectionString = Common.ConnString;

        return classificationBll.DeleteClassification(classificationEntity).ToString();

    }
}