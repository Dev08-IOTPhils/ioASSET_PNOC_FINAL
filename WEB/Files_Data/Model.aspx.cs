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
using System.Configuration;
using System.IO;
using System.Web.Script.Serialization;

public partial class Files_Data_Model :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //ModelList.InnerHtml = GetModel("",1);
        //TagType();
        //GetCategoryList();
    }

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Model.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

  

    //[WebMethod]
    //public static string getmodelname(string modelname, string extension)
    //{
    //    string html = "";
    //    if (extension == "")
    //    {
    //        html += "<img src = '../Pictures/AssetImages/No_Image_Available.jpg' onmouseover='bigImg(this)' onmouseout='normalImg(this)'Style='width: 150px; height: 150px'/>";
    //    }
    //    else
    //    {
    //        html += "<img src = '../Pictures/AssetImages/" + modelname + extension + "' onmouseover='bigImg(this)' onmouseout='normalImg(this)'Style='width: 150px; height: 150px'/>";
    //    }
    //    return html;
    //}

    [WebMethod]
    public static string GetModel(string orderby)
    {
        ModelBll modelBll = new ModelBll();
        string html = string.Empty;
        List<ModelEntity> JsonItemList = new List<ModelEntity>();
        try
        {
            DataTable dt = modelBll.GetModel(Common.ConnString, orderby);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ModelEntity x = new ModelEntity();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Model Information' onclick=\"GetModelID('" + dt.Rows[i]["ModelID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Model Information' onclick=\"GetModelID('" + dt.Rows[i]["ModelID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteModel' data-toggle='tooltip' title='Delete Model' onclick=\"deleteModel('" + dt.Rows[i]["ModelID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteModel(int modelid)
    {
        ModelBll modelBll = new ModelBll();
        ModelEntity modelEntity = new ModelEntity();

        modelEntity.ModelID = modelid;
        modelEntity.CreatedBy = Common.UserName;
        modelEntity.ConnectionString = Common.ConnString;

        return modelBll.DeleteModel(modelEntity).ToString();
        
    }


    [WebMethod]
    public static string GetModelByID(int id)
    {
        ModelBll modelBll = new ModelBll();
        ModelEntity modelEntity = new ModelEntity();

        modelEntity.ModelID = id;
        modelEntity.ConnectionString = Common.ConnString;

        return modelBll.GetModelByID(modelEntity);
    }
    

    //protected void Upload(object sender, EventArgs e)
    //{
    //    Boolean fileOK = false;
    //    String path = Server.MapPath("~/Pictures/AssetImages/");
    //    if (ModelUpload.HasFile)
    //    {
    //        String fileExtension = System.IO.Path.GetExtension(ModelUpload.FileName).ToLower();
    //        String[] allowedExtensions =
    //            {".gif", ".png", ".jpeg", ".jpg"};
    //        for (int i = 0; i < allowedExtensions.Length; i++)
    //        {
    //            if (fileExtension == allowedExtensions[i])
    //            {
    //                fileOK = true;
    //            }
    //        }
    //    }

    //    if (fileOK)
    //    {
    //        try
    //        {
    //            ModelUpload.SaveAs(path + txtModel.Text + System.IO.Path.GetExtension(ModelUpload.FileName).ToLower());
    //        }
    //        catch (Exception ex)
    //        {
    //            //Label1.Text = "File could not be uploaded.";
    //        }
    //    }
    //    else
    //    {
    //        //Response.Write("<script>alert('Please Select .PNG, .JPEG or .JPG to upload');</script>");
    //    }
    //}

}