<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SecretQuestion.aspx.cs" Inherits="Files_Data_SecretQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/SecretQuestion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Security Question
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Security Question</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-9 col-md-9 col-sm-8 col-xs-2"></div>
                <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddSecurityQuestion" type="button" value="Add Security Question" onclick="addSecretQuestionDialogBox()" />
                </div>
            </div>
        </div>
        <br />

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Security Question/s</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblBrandList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>QUESTION</td>
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

    <div id="deleteBrand" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Security Question</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Security Question?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveDepartment()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteBrand" onclick="DeleteBrand(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="addSecretQuestionDialogBox" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit Security Question</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                            <label class="control-label" align="left">
                                Security Question:
                            </label>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <input id="txtQuestion" maxlength="50" type="text" <%--placeholder="Brand Name"--%> style="width: 100%" class="form-control" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkActive" class="checkbox" />
                        </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <%--<div class="check">
                                <input id="check" type="checkbox">
                                <label for="check"></label>
                            </div>--%>

                            <%--<input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left" />--%>
                            <input type="checkbox" class="switch_1 pull-left" id="chkActive" />
                            <label class="pull-left">&nbsp;&nbsp; Active</label>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="SaveCancelSecurityQuestion">
                    <button type="button" class="btn btn-success btn_EditSecurityQuestion" id="btnSaveSecurityQuestion" onclick="SaveSecurityQuestion()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;Cancel&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        

    </script>
</asp:Content>

