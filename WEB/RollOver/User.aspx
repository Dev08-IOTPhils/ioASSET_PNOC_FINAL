<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="RollOver_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/User.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>User
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-user"></i>User Management</li>
            <li>User</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Add User" onclick="addUserDialogBox1('Users')" />
                </div>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">List of Users</h3></center>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblBrandList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>NAME</td>
                                <td>EMPLOYEE #</td>
                                <td>EMAIL ADDRESS</td>
                                <td>APPROVER</td>
                                <td>INSPECTOR</td>
                                <td>CERTIFIER</td>
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

    <div class="modal fade" id="addUserDialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit User</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                               First Name:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtFirstName" maxlength="50" type="text" class="form-control" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                               Last Name:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtLastName" maxlength="50" type="text" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                               Employee No.:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtEmployeeNo" maxlength="50" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" hidden>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Card No:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:TextBox ID="txtCardNo" runat="server" type="text" Style="width: 100%" ReadOnly="true" class="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           User Department:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlDepartment" class="form-control" onchange="GetPosition()" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Job Title:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlPosition" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Custodian:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlIntegrator" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Site:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlSite" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label">
                               E-mail Address:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtEmail" type="text" class="form-control">
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                               Username:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:TextBox ID="txtUsername" MaxLength="50" runat="server" type="text" Style="width: 100%" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Image:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:FileUpload ID="FileUpload1" class="form-control" placeholder="Please Select .jpg, .jpeg file." runat="server" Style="width: 100%; height: 35px" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Is Approver:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkApprover" class="switch_1" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Is Inspector:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkInspector" class="switch_1" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Is Certifier:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkCertifier" class="switch_1" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                           Active:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkActive" class="switch_1" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsave1" class="btn btn-danger" onclick="saveuser1();" style="visibility: hidden">Modify</button>
                    <asp:Button ID="btnsave" UseSubmitBehavior="false" runat="server" class="btn btn-success" OnClick="Upload" OnClientClick="SaveUser();" Text="Save" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="resetPassword" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-exchange"></span>Reset Password</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to reset the password for this user?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnResetPassword" onclick="ResetPassword(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="deleteUser" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete user</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this user?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteUser" onclick="DeleteUser(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="addWarehouseDialogBox1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="Clear()">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <p><span class="fa fa-edit"></span>Add/Remove Site</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table width="100%">
                            <tr>
                                <td>
                                    <table width="100%">
                                        <thead>
                                        </thead>
                                        <tbody class="table">
                                            <tr>
                                                <td width="45%">
                                                    <label class="control-label col-sm-1" align="left">
                                                        <h5><b>Site&nbsp;List</b></h5>
                                                    </label>
                                                    <div id="WarehouseList1" runat="server" style="margin-top: 15px; overflow-y: scroll; width: 250px; height: 250px"></div>
                                                </td>
                                                <td width="10%">
                                                    <input id="btnAddWarehouse1" class="btn btn-primary" type="button" value=">>" onclick="SavePersonnelMember('I')" />
                                                    <br />
                                                    <br />
                                                    <input id="btnRemoveWarehouse1" class="btn btn-primary" type="button" value="<<" onclick="SavePersonnelMember('R')" />
                                                </td>
                                                <td width="45%">
                                                    <label class="control-label col-sm-1" align="left">
                                                        <h5><b>Allowed&nbsp;Sites</b></h5>
                                                    </label>
                                                    <div id="AllowedWarehouses1" runat="server" style="margin-top: 35px; overflow-y: scroll; width: 250px; height: 250px"></div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">Close</button>
                    <button type="button" id="btnsave1" class="btn btn-danger" onclick="saveuser1();" style="visibility: hidden">Modify</button>
                    <asp:Button ID="Button1" UseSubmitBehavior="false" runat="server" class="btn btn-success" OnClick="Upload" OnClientClick="SaveUser();" Text="Save" />--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--QRCODER--%>
    <div class="modal fade" id="qrcoderdialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="Clear()">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <p><span></span>Print QR</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <div id="qrcode" runat="server">
                           <img id="imgbarcode" src="asp.js" alt="" width="100" height="100"/><label id="empno"></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">Close</button>
                    <button type="button" id="btnsave1" class="btn btn-danger" onclick="saveuser1();" style="visibility: hidden">Modify</button>
                    <asp:Button ID="Button1" UseSubmitBehavior="false" runat="server" class="btn btn-success" OnClick="Upload" OnClientClick="SaveUser();" Text="Save" />--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

</asp:Content>

