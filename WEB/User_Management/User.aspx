<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User_Management_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/User.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
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
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddUser" type="button" value="Add User" onclick="addUserDialogBox1('Users')" />
                </div>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Users</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblBrandList" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>NAME</td>
                                <td>EMPLOYEE #</td>
                                <td>EMAIL ADDRESS</td>
                                <td>ROLE</td>
                                <td>SITE</td>
                                <td>CUSTODIAN</td>
                                <td>ADMINISTRATIVE</td>
                                <td>APPROVER</td>
                              <%--  <td hidden>INSPECTOR</td>
                                <td hidden>CERTIFIER</td>--%>
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

    <div id="resetPassword" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="btn btn-sm btn-primary pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;">Reset Password</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to reset the password for this user?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnResetPassword" onclick="ResetPassword(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
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



</asp:Content>

