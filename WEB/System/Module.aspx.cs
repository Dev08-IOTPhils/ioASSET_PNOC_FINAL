using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Script.Serialization;

public partial class System_Module : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Common.IsUserAuthenticated)
            Response.Redirect(Page.ResolveClientUrl("~/Login.aspx"));
        //ModuleList.InnerHtml = GetModule(1);
        ParentModule();
    }

    [WebMethod]
    public static string GetDynamicLocation()
    {
        LocationBll areaBll = new LocationBll();
        LocationEntity xy = new LocationEntity();
        xy.ConnectionString = Common.ConnString;
        string html = string.Empty;
        List<LocationEntity> JsonItemList = new List<LocationEntity>();
        List<ddl> jsonlist = new List<ddl>();
        try
        {
            DataTable dt = areaBll.GetDynamicLocationList(xy);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl x = new ddl();
                    x.id = dt.Rows[i]["LocationID"].ToString();
                    x.text = dt.Rows[i]["LocationName"].ToString();
                    x.level = dt.Rows[i]["ParentID"].ToString();

                    jsonlist.Add(x);

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
    public void ParentModule()
    {
        ModuleBll moduleBLL = new ModuleBll();

        DataTable dtParentModule = moduleBLL.GetModule(Common.ConnString);

        ddlParentModule.DataSource = dtParentModule;
        ddlParentModule.DataValueField = "ModuleID";
        ddlParentModule.DataTextField = "Name";
        ddlParentModule.DataBind();
        ddlParentModule.Items.Insert(0, new ListItem("--None--", "0"));

    }

    [WebMethod]
    public static string SaveModule(int id, string name, string description, int arrangement, string URL, int parentModuleID, bool webpage, bool active, int currentPage)
    {
        ModuleBll moduleBLL = new ModuleBll();
        ModuleEntity module = new ModuleEntity();

        module.ModuleID = id;
        module.Name = name;
        module.Description = description;
        module.Arrangement = arrangement;
        module.URL = URL;
        module.ParentModuleID = parentModuleID;
        module.IsWebPage = webpage;
        module.Active = active;
        module.User = Common.UserID;
        module.ConnectionString = Common.ConnString;

        ModuleEntity moduleCheck = new ModuleEntity();

        moduleCheck.ModuleID = id;
        moduleCheck.Arrangement = arrangement;
        moduleCheck.ParentModuleID = parentModuleID;
        moduleCheck.ConnectionString = Common.ConnString;

        if (moduleBLL.CheckArrangement(moduleCheck) == "update1")
        {
            moduleBLL.SaveModule(module);
            return GetModule() + "|" + "0";
        }
        else if (moduleBLL.CheckArrangement(moduleCheck) != "0")
        {
            if (id != 0)
            {
                moduleCheck.ModuleID = 0;
                if (moduleBLL.CheckArrangement(moduleCheck) != "0")
                {
                     return GetModule() + "|" + "1";
                }
                else
                {
                    moduleBLL.SaveModule(module);
                    return GetModule() + "|" + "0";
                }
            }
            else
            {
                  return GetModule() + "|" + "1";
            }
        }
        else
        {
            moduleBLL.SaveModule(module);
             return GetModule() + "|" + "0";
        }
    }

    //[WebMethod]
    //public static string Pagination(int pageNo)
    //{
    //    return GetModule(pageNo);
    //}

    //[WebMethod]
    //private static string GetModule(int pageNo)
    //{
    //    ModuleBll moduleBLL = new ModuleBll();
    //    DataTable dt = moduleBLL.GetModuleWithPaging(Common.ConnString,pageNo);

    //    string html = "";

    //    html = "<div class='table-responsive'><table class='table table-bordered table-hover dataTable' role='grid'>";
    //    html += "<tr class='info'>"
    //            + "<th><center><b>Name</b></center></th>"
    //            + "<th><center><b>Arrangement</b></center></th>"
    //            + "<th><center><b>URL</b></center></th>"
    //            + "<th><center><b>Parent Module</b></center></th>"
    //            + "<th><center><b>Active</b></center></th>"
    //            + "<th><center><b>Web Page</b></center></th>"
    //            + "<th colspan='2'><center><b>Action</b></th>"
    //            + "</tr>";

    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            html += "<tr>"
    //                 + "<td class='col-md-3 text-danger' style='font-weight: bold;'>" + dt.Rows[i]["Name"].ToString() + "</td>"
    //                 + "<td align='center' class='col-md-1'>" + dt.Rows[i]["Arrangement"].ToString() + "</td>"
    //                 + "<td class='col-md-4'>" + dt.Rows[i]["URL"].ToString() + "</div></td>"
    //                 + "<td>" + dt.Rows[i]["Parent"].ToString() + "</td>"
    //                 + "<td align='center'><img " + (bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src='../image/icon/yes.png'" : "src='../image/icon/no.png'") + "/></td>"
    //                 + "<td align='center'><img " + (bool.Parse(dt.Rows[i]["IsWebPage"].ToString()) == true ? "src='../image/icon/yes.png'" : "src='../image/icon/no.png'") + "/></td>"
    //                 //+ "<td align='center'><button class='btn btn-xs btn-warning' type='button' onclick=\"addModuleDialogBox('" + dt.Rows[i]["Name"] + "','" + dt.Rows[i]["Description"] + "','" + dt.Rows[i]["Arrangement"] + "','" + dt.Rows[i]["URL"] + "','" + dt.Rows[i]["ParentModuleID"] + "', '" + (bool.Parse(dt.Rows[i]["Active"].ToString()))+ "')\"><span class='fa fa-edit'></span> Edit</button></td>"
    //                 + "<td align='center'><button class='btn btn-xs btn-warning' type='button' onclick=\"GetModuleId('" + dt.Rows[i]["ModuleID"] + "')\"><span class='fa fa-edit'></span> Edit</button></td>"
    //                 + "<td align='center'class='col-md-1'><button class='btn btn-xs btn-danger' type='button' onclick=\"deleteModule('" + dt.Rows[i]["ModuleID"] + "')\"><span class='fa fa-trash'></span> Delete</button></td>"

    //                 + "</tr>";


    //        }
    //        html += "</table></div>";
    //        int totalPage = int.Parse(dt.Rows[0]["totalPage"].ToString());
    //        html += "<div style=\"text-align:center;padding:3px;\">" + Control.Paging.Style0(pageNo, totalPage) + "</div>";
    //    }
    //    else
    //    {
    //        html = "<center><h5 style='color: red;'>No Records Found </h5></center>";
    //    }
    //    return html;
    //}

    [WebMethod]
    public static string GetModule()
    {

        ModuleBll moduleBLL = new ModuleBll();
        string html = string.Empty;
        List<ModuleEntity> JsonItemList = new List<ModuleEntity>();
        try
        {
            DataTable dt = moduleBLL.GetModuleWithPaging(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ModuleEntity y = new ModuleEntity();
                    y.Name = dt.Rows[i]["Name"].ToString();
                    y.ArrangemenT = dt.Rows[i]["Arrangement"].ToString();
                    y.URL = dt.Rows[i]["URL"].ToString();
                    y.ParentModule = dt.Rows[i]["Parent"].ToString();
                    y.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    y.WebPage = "<img " + ((bool.Parse(dt.Rows[i]["IsWebPage"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";

                    y.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Category Information' onclick=\"GetModuleId('" + dt.Rows[i]["ModuleID"] + "')\"><span class='fa fa-edit'></span></button> "
                        + " <button type='button' class='btn btn-xs btn-danger btn_Delete' data-toggle='tooltip' title='Delete Category' onclick=\"deleteModule('" + dt.Rows[i]["ModuleID"] + "')\"><span class='fa fa-trash'></span></button> </center>";


                    JsonItemList.Add(y);

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
    public static string ModulebyId(int id)
    {
        ModuleBll moduleBLL = new ModuleBll();
        ModuleEntity module = new ModuleEntity();

        module.ModuleID = id;
        module.ConnectionString = Common.ConnString;

        return moduleBLL.GetModuleById(module);
    }

    [WebMethod]
    public static string DeleteModule(int moduleid, int pageNo)
    {
        ModuleBll moduleBll = new ModuleBll();
        ModuleEntity moduleEntity = new ModuleEntity();

        moduleEntity.ModuleID = moduleid;
        moduleEntity.User = Common.UserID;
        moduleEntity.ConnectionString = Common.ConnString;

        moduleBll.DeleteModule(moduleEntity);

        return GetModule();
    }
}