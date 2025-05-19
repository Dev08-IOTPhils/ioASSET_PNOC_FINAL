using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

public partial class Files_Data_Tag : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
           //GetTagType();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Tag.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    //private void GetTagType()
    //{
    //    TagTypeBll tagTypeBll = new TagTypeBll();

    //    DataTable dt = tagTypeBll.GetTagType(Common.ConnString);

    //    ddlTagType.DataSource = dt;
    //    ddlTagType.DataValueField = "TagTypeID";
    //    ddlTagType.DataTextField = "TagType";
    //    ddlTagType.DataBind();
    //    ddlTagType.Items.Insert(0, "--- Select Tag Type ---");
    //}
    
    //[WebMethod]
    //public static string SaveTag(int tagid, string tag, int tagtypeid, string description, bool active)
    //{
    //    TagTypeEntity x = new TagTypeEntity();
    //    TagTypeBll y = new TagTypeBll();

    //    x.TagID = tagid;
    //    x.Tag = tag;
    //    x.TagTypeID = tagtypeid;
    //    x.Description = description;
    //    x.Active = active;
    //    x.CreatedBy = Common.UserName;
    //    x.ConnectionString = Common.ConnString;

    //    return y.SaveTag(x).ToString();

    //    //return GetLocation(pageNo);
    //}

    [WebMethod]
    public static string GetTag()
    {
        TagTypeBll y = new TagTypeBll();
        string html = string.Empty;
        List<TagTypeEntity> JsonItemList = new List<TagTypeEntity>();
        try
        {
            DataTable dt = y.GetTagList(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TagTypeEntity x = new TagTypeEntity();
                    x.Tag = dt.Rows[i]["TagName"].ToString();
                    x.TagType = dt.Rows[i]["TagType"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Tag Information' onclick=\"GetTagByID('" + dt.Rows[i]["TagID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Tag Information' onclick=\"GetTagByID('" + dt.Rows[i]["TagID"] + "')\"><span class='fa fa-edit'></span></button> "
                            + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteTag' data-toggle='tooltip' title='Delete Tag' onclick=\"deleteTag('" + dt.Rows[i]["TagID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
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
    public static string DeleteTag(int tagid)
    {
        TagTypeBll y = new TagTypeBll();
        TagTypeEntity x = new TagTypeEntity();

        x.TagID = tagid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.DeleteTag(x).ToString();

    }

    [WebMethod]
    public static string GetTagByID(int tagid)
    {
        TagTypeBll y = new TagTypeBll();
        TagTypeEntity x = new TagTypeEntity();

        x.TagID = tagid;
        x.ConnectionString = Common.ConnString;

        return y.GetTagByID(x);
    }
}