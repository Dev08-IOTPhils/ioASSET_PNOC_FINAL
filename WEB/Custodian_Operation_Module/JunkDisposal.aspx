<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JunkDisposal.aspx.cs" Inherits="Operation_Module_JunkDisposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/JunkDisposal.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Junk Disposal
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-user"></i>Custodian Operation Module</li>
            <li>Junk Disposal</li>
        </ol>
    </section>
    <br />
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
            <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                <input id="btnAdd" class="btn btn-block btn-primary btn_AddR2ole" type="button" value="Add Transaction" onclick="addTransaction()" />
            </div>
        </div>
    </div>

    <section class="content">
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Junk Disposal Pending Approval/Upload</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblJunkDisposal" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>TRANSACTION #</td>
                                <td>TRANSACTED BY</td>
                                <td>APPROVED BY</td>
                                <td>REMARKS</td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="junkdisposal">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="addTransDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add Transaction</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" id="labelmode">
                            <label class="control-label">
                                Mode of Disposal:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12" id="dropdownmode">
                            <asp:DropDownList ID="ddlModeOfDisposal" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" id="approver">
                            <label class="control-label">
                                Approver:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12" id="approvermode">
                            <asp:DropDownList ID="ddlApprover" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                            <textarea class="form-control" id="txtRemarks" runat="server"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="SaveCancelRole">
                    <button type="button" class="btn btn-success btn_EditRol1e" id="btnSaveRole1" onclick="SaveJD()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
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

                    <h4>
                        <label>Role : </label>
                        <label id="lblrolename1"></label>
                    </h4>
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
                    <h4>
                        <label>Role : </label>
                        <label id="lblrolename"></label>
                    </h4>
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

