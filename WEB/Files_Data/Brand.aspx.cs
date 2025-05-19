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

public partial class Files_Data_Brand : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //BrandList.InnerHtml = GetBrand("",1);
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Brand.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
   

    [WebMethod]
    public static string BrandByID(int id)
    {
        BrandEntity brandEntity = new BrandEntity();
        BrandBll brandBll = new BrandBll();

        brandEntity.BrandID = id;
        brandEntity.ConnectionString = Common.ConnString;

        return brandBll.BrandbyID(brandEntity);
    }


    [WebMethod]
    public static string GetBrand()
    {
        BrandBll brandBll = new BrandBll();
        string html = string.Empty;
        List<BrandEntity> JsonItemList = new List<BrandEntity>();
        try
        {
            DataTable dt = brandBll.GetBrand(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BrandEntity x = new BrandEntity();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                   
                    x.IsActive = "<img "+ ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) +"/>";
                      if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit ' type='button' data-toggle='tooltip'  title='Edit Brand Information' onclick=\"GetBrandID('" + dt.Rows[i]["BrandID"] + "')\"><span class='fa fa-edit'></span></button> </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit ' type='button' data-toggle='tooltip'  title='Edit Brand Information' onclick=\"GetBrandID('" + dt.Rows[i]["BrandID"] + "')\"><span class='fa fa-edit'></span></button> "
                           + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteBrand' data-toggle='tooltip' title='Delete Brand' onclick=\"deleteBrand('" + dt.Rows[i]["BrandID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteBrand(int id)
    {
        BrandEntity brandEntity = new BrandEntity();
        BrandBll brandBll = new BrandBll();

        brandEntity.BrandID = id;
        brandEntity.CreatedBy = Common.UserName;
        brandEntity.ConnectionString = Common.ConnString;

        return brandBll.DeleteBrand(brandEntity).ToString();

        
    }
}