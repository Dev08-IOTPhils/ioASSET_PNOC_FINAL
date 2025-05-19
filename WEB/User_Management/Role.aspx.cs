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

public partial class User_Management_Role : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string SaveRole(int roleid, string roleName, string description, bool isActive)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity roleEntity = new RoleEntity();

        roleEntity.RoleID = roleid;
        roleEntity.RoleName = roleName;
        roleEntity.Description = description;
        roleEntity.Active = isActive;
        roleEntity.User = Common.UserID;
        roleEntity.ConnectionString = Common.ConnString;
        return roleBLL.SaveRole(roleEntity).ToString();

        //return GetRole(pageNo);
    }


    [WebMethod]
    public static string RoleByID(int id)
    {
        RoleEntity roleEntity = new RoleEntity();
        RoleBll roleBll = new RoleBll();

        roleEntity.RoleID = id;
        roleEntity.ConnectionString = Common.ConnString;

        return roleBll.RoleByID(roleEntity);
    }

    [WebMethod]
    public static string GetRole()
    {
        RoleBll roleBLL = new RoleBll();
        string html = string.Empty;
        List<RoleEntity> JsonItemList = new List<RoleEntity>();
        try
        {
            DataTable dt = roleBLL.GetRole(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RoleEntity x = new RoleEntity();
                    if (Convert.ToInt32(Common.Role) == 1 || Convert.ToInt32(Common.Role) == 2)
                    {

                        x.RoleName = dt.Rows[i]["Name"].ToString();
                        x.Description = dt.Rows[i]["Description"].ToString();

                        x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                        if (bool.Parse(dt.Rows[i]["Active"].ToString()) == true)
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success btn_Member' data-toggle='tooltip'   title='Add / Remove Role Members' onclick=\"OpenDialogRoleMember(" + dt.Rows[i]["RoleID"] + ",'" + dt.Rows[i]["Name"] + "')\"><span class='fa fa-user-plus'></span></button> "
                                + " <button class='btn btn-xs btn-primary btn_Module'  data-toggle='tooltip'  title='Add / Remove Role Module' onclick=\"OpenDialogRoleAccess(" + dt.Rows[i]["RoleID"] + ",'" + dt.Rows[i]["Name"] + "')\"><span class='fa fa-arrow-circle-o-right'></span></button> "
                                + " <button class='btn btn-xs btn-primary btn_ButtonAccess'  data-toggle='tooltip'  title='Add / Remove Button Access' type='button' onclick=\"OpenDialogButtonAccess(" + dt.Rows[i]["RoleID"] + ",'" + dt.Rows[i]["Name"] + "')\"><span class='fa fa-arrow-circle-o-right'></span></button> "
                                + "<button class='btn btn-xs btn-warning btn_Edit' type='button' title='Edit' onclick=\"RoleByID('" + dt.Rows[i]["RoleID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                        }
                        else
                        {
                            x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' title='Edit' onclick=\"RoleByID('" + dt.Rows[i]["RoleID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                        }
                        //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                        //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";
                    }
                    else
                    {
                        if (dt.Rows[i]["Name"].ToString() == "Web Administrator")
                        {

                        }
                        else
                        {
                            x.RoleName = dt.Rows[i]["Name"].ToString();
                            x.Description = dt.Rows[i]["Description"].ToString();

                            x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                            if (bool.Parse(dt.Rows[i]["Active"].ToString()) == true)
                            {
                                x.Action = " <center><button class='btn btn-xs btn-success btn_Member' type='button' data-toggle='tooltip'   title='Add / Remove Role Members' onclick=\"OpenDialogRoleMember(" + dt.Rows[i]["RoleID"] + ",'" + dt.Rows[i]["Name"] + "')\"><span class='fa fa-user-plus'></span></button> "
                               + " <button class='btn btn-xs btn-primary btn_Module' type='button' data-toggle='tooltip'  title='Add / Remove Role Module' onclick=\"OpenDialogRoleAccess(" + dt.Rows[i]["RoleID"] + ",'" + dt.Rows[i]["Name"] + "')\"><span class='fa fa-arrow-circle-o-right'></span></button> "
                               + "<button class='btn btn-xs btn-warning btn_Edit' type='button' title='Edit' onclick=\"RoleByID('" + dt.Rows[i]["RoleID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                            }
                            else
                            {
                                x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' title='Edit' onclick=\"RoleByID('" + dt.Rows[i]["RoleID"] + "')\"><span class='fa fa-edit'></span></button></center>";
                            }


                        }
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
    public static string LoadExistingRoleMember(int id)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity role = new RoleEntity();

        role.RoleID = id;
        role.ConnectionString = Common.ConnString;
        DataTable dtRoleMemberExisting = roleBLL.RetrieveExistingRoleMember(role);

        string html = "";

        if (dtRoleMemberExisting.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' width=\"100%\" style=\"margin:auto;\">"
               + "<tr>"
               + "<th width=\"30px\" align=\"center\"><input name=\"chkRoleMemberExistingId\" id=\"chkRoleMemberExistingId\" type=\"checkbox\" onClick=\"SelectAllRoleMemberExisting();\"/></th>"
               + "<th width=\"50px\" align=\"left\">User</th>"
               + "</tr>";

            for (int ctr = 0; ctr < dtRoleMemberExisting.Rows.Count; ctr++)
            {

                html += "<tr>"
                    + "<input type=\"hidden\" id=\"RoleMemberExistingId" + ctr + "\" value=\"" + dtRoleMemberExisting.Rows[ctr]["UserID"].ToString() + "\">"
                    + "<td width=\"30px\" align=\"center\"><input name=\"chkRoleMemberExistingId" + ctr + "\" id=\"chkRoleMemberExistingId" + ctr + "\" type=\"checkbox\" onClick=\"CheckRoleMemberExisting(" + ctr + ")\"/></td>"
                    + "<td width=\"50px\" align=\"left\">" + dtRoleMemberExisting.Rows[ctr]["UserName"].ToString() + "</td>"
                    + "</tr>";
            }

            html += "</table></div>"
                + "<input type=\"hidden\" name=\"ctrRoleMemberExisting\" id=\"ctrRoleMemberExisting\" value=\"" + dtRoleMemberExisting.Rows.Count + "\">";
        }
        else
        {
            html = "<div><label class=\"required\">No members found!</label></div>";
        }

        return html;
    }

    [WebMethod]
    public static string LoadNotExistingRoleMember(int id)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity role = new RoleEntity();

        role.RoleID = id;
        role.ConnectionString = Common.ConnString;
        DataTable dtRoleMemberNotExisting = roleBLL.RetrieveNotExistingRoleMember(role);

        string html = "";

        if (dtRoleMemberNotExisting.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class=' table table-bordered' width=\"100%\" style=\"margin:auto;\">"
               + "<tr>"
               + "<th width=\"30px\" align=\"center\"><input name=\"chkRoleMemberNotExistingId\" id=\"chkRoleMemberNotExistingId\" type=\"checkbox\" onClick=\"SelectAllRoleMemberNotExisting();\"/></th>"
               + "<th width=\"50px\" align=\"left\">User</th>"
               + "</tr>";

            for (int ctr = 0; ctr < dtRoleMemberNotExisting.Rows.Count; ctr++)
            {

                html += "<tr>"
                    + "<input type=\"hidden\" id=\"RoleMemberNotExistingId" + ctr + "\" value=\"" + dtRoleMemberNotExisting.Rows[ctr]["UserID"].ToString() + "\">"
                    + "<td width=\"30px\"  align=\"center\"><input name=\"chkRoleMemberNotExistingId" + ctr + "\" id=\"chkRoleMemberNotExistingId" + ctr + "\" type=\"checkbox\" onClick=\"CheckRoleMemberNotExisting(" + ctr + ")\"/></td>"
                    + "<td width=\"50px\"  align=\"left\">" + dtRoleMemberNotExisting.Rows[ctr]["UserName"].ToString() + "</td>"
                    + "</tr>";
            }

            html += "</table></div>"
                + "<input type=\"hidden\" name=\"ctrRoleMemberNotExisting\" id=\"ctrRoleMemberNotExisting\" value=\"" + dtRoleMemberNotExisting.Rows.Count + "\">";
        }
        else
        {
            html = "<div><label class=\"required\">No users found!</label></div>";
        }

        return html;
    }

    [WebMethod]
    public static string SaveRoleMember(int roleID, string users, string action)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity role = new RoleEntity();

        string[] userID = users.Split(';');

        List<RoleMember> ListRoleMember = new List<RoleMember>();
        RoleMember roleMember = new RoleMember();

        for (int ctr = 0; ctr < userID.Length; ctr++)
        {
            roleMember = new RoleMember();
            roleMember.UserID = userID[ctr];
            ListRoleMember.Add(roleMember);
        }
        roleMember.getSetUserID = ListRoleMember;

        role.RoleID = roleID;
        role.ConnectionString = Common.ConnString;
        role.GetSetRoleMember = roleMember.getSetUserID;

        if (action == "I")
            roleBLL.AddRoleMember(role);
        else
            roleBLL.RemoveRoleMember(role);

        return "";
    }

    //access

    [WebMethod]
    public static string LoadExistingRoleModule(int id)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity role = new RoleEntity();

        role.RoleID = id;
        role.ConnectionString = Common.ConnString;
        DataTable dtRoleModuleExisting = roleBLL.RetrieveExistingRoleModule(role);

        string html = "";

        if (dtRoleModuleExisting.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' width=\"100%\" style=\"margin:auto;\">"
               + "<tr>"
               + "<th width=\"30px\" align=\"center\"><input name=\"chkRoleModuleExistingId\" id=\"chkRoleModuleExistingId\" type=\"checkbox\" onClick=\"SelectAllRoleModuleExisting();\"/></th>"
               + "<th width=\"50px\" align=\"left\">Module</th>"
               + "</tr>";

            for (int ctr = 0; ctr < dtRoleModuleExisting.Rows.Count; ctr++)
            {

                html += "<tr>"
                    + "<input type=\"hidden\" id=\"RoleModuleExistingId" + ctr + "\" value=\"" + dtRoleModuleExisting.Rows[ctr]["ModuleID"].ToString() + "\">"
                    + "<td width=\"30px\" align=\"center\"><input name=\"chkRoleModuleExistingId" + ctr + "\" id=\"chkRoleModuleExistingId" + ctr + "\" type=\"checkbox\" onClick=\"CheckRoleModuleExisting(" + ctr + ")\"/></td>"
                    + "<td width=\"50px\" align=\"left\"><label data-toggle='tooltip'  title='" + dtRoleModuleExisting.Rows[ctr]["Description"].ToString() + "'>" + dtRoleModuleExisting.Rows[ctr]["Name"].ToString() + "</label></td>"
                    + "</tr>";
            }

            html += "</table></div>"
                + "<input type=\"hidden\" name=\"ctrRoleModuleExisting\" id=\"ctrRoleModuleExisting\" value=\"" + dtRoleModuleExisting.Rows.Count + "\">";
        }
        else
        {
            html = "<div><label class=\"required\">No Modules found!</label></div>";
        }

        return html;
    }


    [WebMethod]
    public static string LoadNotExistingRoleModule(int id)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity role = new RoleEntity();

        role.RoleID = id;
        role.ConnectionString = Common.ConnString;
        DataTable dtRoleModuleNotExisting = roleBLL.RetrieveNotExistingRoleModule(role);

        string html = "";

        if (dtRoleModuleNotExisting.Rows.Count > 0)
        {
            html += "<table class=' table table-striped table-bordered' width=\"100%\" style=\"margin:auto;\">"
               + "<tr>"
               + "<th width=\"30px\" align=\"center\"><input name=\"chkRoleModuleNotExistingId\" id=\"chkRoleModuleNotExistingId\" type=\"checkbox\" onClick=\"SelectAllRoleModuleNotExisting();\"/></th>"
               + "<th width=\"50px\" align=\"left\">Module</th>"
               + "</tr>";

            for (int ctr = 0; ctr < dtRoleModuleNotExisting.Rows.Count; ctr++)
            {

                html += "<tr>"
                    + "<input type=\"hidden\" id=\"RoleModuleNotExistingId" + ctr + "\" value=\"" + dtRoleModuleNotExisting.Rows[ctr]["ModuleID"].ToString() + "\">"
                    + "<td width=\"30px\" align=\"center\"><input name=\"chkRoleModuleNotExistingId" + ctr + "\" id=\"chkRoleModuleNotExistingId" + ctr + "\" type=\"checkbox\" onClick=\"CheckRoleModuleNotExisting(" + ctr + ")\"/></td>"
                    + "<td width=\"50px\" align=\"left\"><label data-toggle='tooltip'  title='" + dtRoleModuleNotExisting.Rows[ctr]["Description"].ToString() + "'>" + dtRoleModuleNotExisting.Rows[ctr]["Name"].ToString() + "</label></td>"
                    + "</tr>";
            }

            html += "</table>"
                + "<input type=\"hidden\" name=\"ctrRoleModuleNotExisting\" id=\"ctrRoleModuleNotExisting\" value=\"" + dtRoleModuleNotExisting.Rows.Count + "\">";
        }
        else
        {
            html = "<div><label class=\"required\">No Modules found!</label></div>";
        }

        return html;
    }

    [WebMethod]
    public static string SaveRoleModule(int roleID, string modules, string action)
    {
        RoleBll roleBLL = new RoleBll();
        RoleEntity role = new RoleEntity();

        string[] moduleID = modules.Split(';');

        List<RoleAccess> ListRoleAccess = new List<RoleAccess>();
        RoleAccess roleAccess = new RoleAccess();

        for (int ctr = 0; ctr < moduleID.Length; ctr++)
        {
            roleAccess = new RoleAccess();
            roleAccess.ModuleID = int.Parse(moduleID[ctr]);
            ListRoleAccess.Add(roleAccess);
        }
        roleAccess.getSetModuleID = ListRoleAccess;

        role.RoleID = roleID;
        role.ConnectionString = Common.ConnString;
        role.GetSetRoleAccess = roleAccess.getSetModuleID;

        if (action == "I")
            roleBLL.AddRoleModule(role);
        else
            roleBLL.RemoveRoleModule(role);

        return "";
    }
    //Access Button
    [WebMethod]
    public static string LoadExistingOperatorGroupModuleButton(int id)
    {
        RoleBll ogb = new RoleBll();
        RoleEntity oge = new RoleEntity();

        oge.RoleID = id;
        oge.ConnectionString = Common.ConnString;
        DataTable dtOperatorGroupModuleExisting = ogb.RetrieveExistingOperatorGroupModuleButton(oge);

        string html = "";

        if (dtOperatorGroupModuleExisting.Rows.Count > 0)
        {
            html += "<div class='table-responsive'><table class='table table-bordered' width=\"100%\" style=\"margin:auto;\">"
               + "<tr>"
               + "<th align=\"left\">Module</th>"
               + "<th align=\"left\">View<input class='switch_1' type=\"checkbox\" id=\"chkBtnView\" name=\"chkBtnView\"  onChange=\"SelectAllViewBtn();\"/></th>"
               + "<th align=\"left\">Add <input class='switch_1' type=\"checkbox\" id=\"chkBtnAdd\" name=\"chkBtnAdd\"  onChange=\"SelectAllAddBtn();\"/></th>"
               + "<th align=\"left\">Edit <input class='switch_1' type=\"checkbox\" id=\"chkBtnEdit\" name=\"chkBtnEdit\"  onChange=\"SelectAllEditBtn();\"/></th>"
               + "<th align=\"left\">Delete <input class='switch_1' type=\"checkbox\" id=\"chkBtnDel\" name=\"chkBtnDel\"  onChange=\"SelectAllDeleteBtn();\"/></th>"
               + "</tr>";

            for (int ctr = 0; ctr < dtOperatorGroupModuleExisting.Rows.Count; ctr++)
            {
                html += "<tr>"
                    + "<input type=\"hidden\" id=\"OperatorGroupModuleExistingId" + ctr + "\" value=\"" + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + "\">"
                    + "<td align=\"left\"><label data-toggle='tooltip'  title='" + dtOperatorGroupModuleExisting.Rows[ctr]["Description"].ToString() + "'>" + dtOperatorGroupModuleExisting.Rows[ctr]["Name"].ToString() + "</label></td>";
                //btn view
                if (dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString().Split('|')[0] == "0")
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnView' name=\"chkBtnView" + ctr + "\" id=\"chkBtnView" + ctr + "\" type=\"checkbox\" onChange=\"UpdateViewBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\" /></td>";
                }
                else
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnView' checked name=\"chkBtnView" + ctr + "\" id=\"chkBtnView" + ctr + "\" type=\"checkbox\" onChange=\"UpdateViewBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\" /></td>";
                }

                //btn add
                if (dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString().Split('|')[1] == "0")
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnAdd' name=\"chkBtnAdd" + ctr + "\" id=\"chkBtnAdd" + ctr + "\" type=\"checkbox\" onChange=\"UpdateAddBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\"/></td>";
                }
                else
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnAdd' checked name=\"chkBtnAdd" + ctr + "\" id=\"chkBtnAdd" + ctr + "\" type=\"checkbox\" onChange=\"UpdateAddBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\"/></td>";
                }
                //btn edit
                if (dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString().Split('|')[2] == "0")
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnEdit' name=\"chkBtnEdit" + ctr + "\" id=\"chkBtnEdit" + ctr + "\" type=\"checkbox\" onChange=\"UpdateEditBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\"/></td>";
                }
                else
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnEdit' checked name=\"chkBtnEdit" + ctr + "\" id=\"chkBtnEdit" + ctr + "\" type=\"checkbox\" onChange=\"UpdateEditBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\"/></td>";
                }
                //btn del
                if (dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString().Split('|')[3] == "0")
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnDel' name=\"chkBtnDel" + ctr + "\" id=\"chkBtnDel" + ctr + "\" type=\"checkbox\" onChange=\"UpdateDelBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\"/></td>";
                }
                else
                {
                    html += "<td align=\"center\"><input class='switch_1 chkBtnDel' checked name=\"chkBtnDel" + ctr + "\" id=\"chkBtnDel" + ctr + "\" type=\"checkbox\" onChange=\"UpdateDelBtn(" + ctr + "," + id + "," + dtOperatorGroupModuleExisting.Rows[ctr]["ModuleId"].ToString() + ",'" + dtOperatorGroupModuleExisting.Rows[ctr]["ButtonAccess"].ToString() + "')\"/></td>";
                }
                html += "</tr>";
            }

            html += "</table></div>"
                + "<input type=\"hidden\"  name=\"ctrOperatorGroupModuleExisting\" id=\"ctrOperatorGroupModuleExisting\" value=\"" + dtOperatorGroupModuleExisting.Rows.Count + "\">";
        }
        else
        {
            html = "<div><label class=\"required\">No Modules found!</label></div>";
        }

        return html;
    }


    [WebMethod]
    public static string UpdateViewBtn(string OperatorGroupId, string ModuleId, string buttonaccess)
    {
        RoleBll ogb = new RoleBll();

        return ogb.UpdateViewBtn(Common.ConnString, OperatorGroupId, ModuleId, buttonaccess).ToString();

        //return GetOperatorGroup(pageNo);
    }

    [WebMethod]
    public static string UpdateAddBtn(string OperatorGroupId, string ModuleId, string buttonaccess)
    {
        RoleBll ogb = new RoleBll();

        return ogb.UpdateAddBtn(Common.ConnString, OperatorGroupId, ModuleId, buttonaccess).ToString();

        //return GetOperatorGroup(pageNo);
    }

    [WebMethod]
    public static string UpdateEditBtn(string OperatorGroupId, string ModuleId, string buttonaccess)
    {
        RoleBll ogb = new RoleBll();

        return ogb.UpdateEditBtn(Common.ConnString, OperatorGroupId, ModuleId, buttonaccess).ToString();

        //return GetOperatorGroup(pageNo);
    }

    [WebMethod]
    public static string UpdateDelBtn(string OperatorGroupId, string ModuleId, string buttonaccess)
    {
        RoleBll ogb = new RoleBll();

        return ogb.UpdateDelBtn(Common.ConnString, OperatorGroupId, ModuleId, buttonaccess).ToString();

        //return GetOperatorGroup(pageNo);
    }

}