<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="User_Management_Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/Role.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Role
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-user"></i> User Management</li>
            <li>Role</li>
        </ol>
    </section>
    <br />
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
            <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                <input id="btnAdd" class="btn btn-block btn-primary btn_AddRole" type="button" value="Add Role" onclick="addRoleDialogBox1()" />
            </div>
        </div>
    </div>

    <section class="content">
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Roles</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
           <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblBrandList" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ROLE</td>
                                <td>DESCRIPTION</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="brandlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="addRoleDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color:steelblue; text-align:center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit Role</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Role:</b></h6>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtRole" maxlength="50" type="text" class="form-control" style="width: 100%" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Description:</b></h6>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <br />
                            <textarea class="form-control" id="txtDescription" rows="5" cols="1" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,100,'cnt', this.id)"></textarea>
                            <h6>
                                <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkActive" class="switch_1" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="SaveCancelRole">
                    <button type="button" class="btn btn-success btn_EditRole" id="btnSaveRole" onclick="SaveRole()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%--member--%>
    <div class="modal fade" tabindex="-1" role="dialog" id="memberDialogBoxMsg">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">Add / Remove Role Members</h4>
                </div>
                <div class="modal-body">
                    
                    <h4><label>Role : </label><label id="lblrolename1"></label></h4>
                    <div class='table-responsive'>
                        <table width="100%">
                            <thead>
                            </thead>
                            <tbody class="table">
                                <tr>
                                    <td width="45%">
                                        <label class="control-label col-sm-1" align="left">
                                            <h5><b>List&nbsp;of&nbsp;Users</b></h5>
                                        </label>
                                        <div id="UserList" runat="server" style="margin-top: 15px; overflow-y: scroll; width: 250px; height: 450px"></div>
                                    </td>
                                    <td width="10%">
                                        <input id="btnAddUserRole" class="btn btn-primary" type="button" value=">>" onclick="SaveRoleMember('I')" />
                                        <br />
                                        <br />
                                        <input id="btnRemoveUserRole" class="btn btn-primary" type="button" value="<<" onclick="SaveRoleMember('R')" />
                                    </td>
                                    <td width="45%">
                                        <label class="control-label col-sm-1" align="left">
                                            <h5><b>List&nbsp;of&nbsp;Members</b></h5>
                                        </label>
                                        <div id="MemberList" runat="server" style="margin-top: 35px; overflow-y: scroll; width: 250px; height: 450px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success btn_EditRole" onclick="UserAdded()">&nbsp;&nbsp;&nbsp;Done&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <%-- button access--%>
    <div class="modal fade" tabindex="-1" role="dialog" id="ButtonaccessDialogBoxMsg">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Button Access Operator Group Modules</h4>
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="ButtonModuleAdded()"></button>
                </div>
                <div class="modal-body">
                    <h4>
                        <label>Operator Group : </label>
                        <label id="lblrolename2"></label>
                    </h4>
                    <div class='table-responsive'>
                        <table width="100%">
                            <thead>
                            </thead>
                            <tbody class="table">
                                <tr>
                                    <td>
                                        <label class="control-label col-sm-1" align="left">
                                            <h5><b>Modules</b></h5>
                                        </label>
                                        <div id="ModuleListButton" runat="server" style="margin-top: 35px; overflow-y: scroll; width: 100%; height: 450px"></div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" onclick="ButtonModuleAdded()">&nbsp;&nbsp;&nbsp;Done&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>
    <%--end--%>


    <%-- module--%>
    <div class="modal fade" tabindex="-1" role="dialog" id="accessDialogBoxMsg">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">Add / Remove Role Modules</h4>
                </div>
                <div class="modal-body">
                    <h4><label>Role : </label><label id="lblrolename"></label></h4>
                    <div class='table-responsive'>
                        <table width="100%">
                            <thead>
                            </thead>
                            <tbody class="table">
                                <tr>
                                    <td width="45%">
                                        <label class="control-label col-sm-1" align="left">
                                            <h5><b>List&nbsp;of&nbsp;Modules</b></h5>
                                        </label>
                                        <div id="ModuleListAll" runat="server" style="margin-top: 15px; overflow-y: scroll; width: 250px; height: 450px"></div>
                                    </td>
                                    <td width="10%">
                                        <input id="btnAddRoleModule" class="btn btn-primary" type="button" value=">>" onclick="SaveRoleModule('I')" />
                                        <br />
                                        <br />
                                        <input id="btnRemoveRoleModule" class="btn btn-primary" type="button" value="<<" onclick="SaveRoleModule('R')" />
                                    </td>
                                    <td width="45%">
                                        <label class="control-label col-sm-1" align="left">
                                            <h5><b>Modules</b></h5>
                                        </label>
                                        <div id="ModuleList" runat="server" style="margin-top: 35px; overflow-y: scroll; width: 250px; height: 450px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success btn_EditRole" onclick="ModuleAdded()">&nbsp;&nbsp;&nbsp;Done&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

