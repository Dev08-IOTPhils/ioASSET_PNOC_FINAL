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

public partial class Files_Data_Category : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //GetAssetInventoryAccount();
        //CategoryList.InnerHtml = GetCategory("",1);
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Category.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    

    [WebMethod]
    public static int CheckCC(int id, string cc)
    {
        CategoryEntity categoryEntity = new CategoryEntity();
        CategoryBll categoryBll = new CategoryBll();


        categoryEntity.CategoryID = id;
        categoryEntity.Code = cc;
        categoryEntity.ConnectionString = Common.ConnString;

        return categoryBll.checkCC(categoryEntity);
    }

   

    [WebMethod]
    public static string CategoryByID(int id)
    {
        CategoryBll categoryBll = new CategoryBll();

        CategoryEntity categoryEntity = new CategoryEntity();

        categoryEntity.CategoryID = id;
        categoryEntity.ConnectionString = Common.ConnString;

        return categoryBll.CategoryByID(categoryEntity);
    }


    [WebMethod]
    public static string GetCategory()
    {
        CategoryBll categoryBll = new CategoryBll();
        string html = string.Empty;
        List<CategoryEntity> JsonItemList = new List<CategoryEntity>();
        try
        {
            DataTable dt = categoryBll.GetCategoryV2(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CategoryEntity x = new CategoryEntity();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Category Information' onclick=\"GetCategoryID('" + dt.Rows[i]["CategoryID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Category Information' onclick=\"GetCategoryID('" + dt.Rows[i]["CategoryID"] + "')\"><span class='fa fa-edit'></span></button> "
                            + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteCat' data-toggle='tooltip' title='Delete Category' onclick=\"deleteCategory('" + dt.Rows[i]["CategoryID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
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
    public static string DeleteCategory(int id)
    {
        CategoryBll categoryBLL = new CategoryBll();
        CategoryEntity categoryEntity = new CategoryEntity();

        categoryEntity.CategoryID = id;
        categoryEntity.CreatedBy = Common.UserName;
        categoryEntity.ConnectionString = Common.ConnString;

        return categoryBLL.DeleteCategory(categoryEntity).ToString();

    }
}