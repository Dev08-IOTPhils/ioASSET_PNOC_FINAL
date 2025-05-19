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

public partial class Files_Data_TagType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string SaveTagType(int id, int locationid, string TagType, string description, bool active)
    {
        TagTypeEntity x = new TagTypeEntity();
        TagTypeBll y = new TagTypeBll();
        
        x.TagTypeID = id;
        x.TagType = TagType;
        x.Description = description;
        x.Active = active;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.SaveTagType(x).ToString();
    }

    [WebMethod]
    public static string TagTypeByID(int id)
    {
        TagTypeEntity x = new TagTypeEntity();
        TagTypeBll y = new TagTypeBll();

        x.TagTypeID = id;
        x.ConnectionString = Common.ConnString;

        return y.GetTagTypeByName(x);
    }


    [WebMethod]
    public static string getTagType()
    {
        TagTypeBll y = new TagTypeBll();
        string html = string.Empty;
        List<TagTypeEntity> JsonItemList = new List<TagTypeEntity>();
        try
        {
            DataTable dt = y.GetTagTypeList(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TagTypeEntity x = new TagTypeEntity();
                    x.TagType = dt.Rows[i]["TagType"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["IsActive"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit TagType Information' onclick=\"GetTagTypeID('" + dt.Rows[i]["TagTypeID"] + "')\"><span class='fa fa-edit'></span></button> "
                        + " <button type='button' class='btn btn-xs btn-danger btn_Delete' data-toggle='tooltip' title='Delete TagType' onclick=\"deleteTagType('" + dt.Rows[i]["TagTypeID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
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
    public static string DeleteTagType(int id)
    {

        TagTypeEntity x = new TagTypeEntity();
        TagTypeBll y = new TagTypeBll();

        x.TagTypeID = id;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.DeleteTagType(x);

        return "";
    }
}