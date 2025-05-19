<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Custodian.aspx.cs" Inherits="Files_Data_Custodian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Custodian.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Custodian Type
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Custodian Type
            </li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-5" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddCustodian" type="button" value="Add Custodian Type" onclick="addCustodianDialogBox()" />
                </div>
            </div>
        </div>
        <br />

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Custodian Type</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblCustodianList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>CUSTODIAN TYPE</td>
                                <td>DESCRIPTION</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="custodianlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteCustodian" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Custodian Type</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Custodian Type?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveDepartment()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteCustodian" onclick="DeleteCustodian(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="addCustodianDialogBox" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit Custodian Type</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                             <label class="control-label" align="left">
                                <span style="color: red">*</span> Custodian Type:
                            </label>
                        </div>
                        <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <input id="txtCustodian" maxlength="50" type="text" onkeyup="return ReplaceStr2(this);" <%--placeholder="Custodian"--%> style="width: 100%" class="form-control" />
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                            <label class="control-label" align="left">
                                <span style="color: red">*</span> Description:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <textarea class="form-control" id="txtDescription" <%-- placeholder="Description"--%> rows="5" cols="1" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,500,'cnt', this.id); return ReplaceStr2(this);"></textarea>
                            <h6>
                                <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                            <%--</div><div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        </div>

                      
                    </div>
                      <div class="row">
                            <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>--%>
                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                <%--<input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left"/><label class="pull-left">&nbsp;&nbsp; Active</label>--%>
                                <input type="checkbox" class="switch_1 pull-left" id="chkActive" />
                                <label class="pull-left">&nbsp;&nbsp; Active</label>
                            </div>
                            <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" id="btnSaveCustodian" onclick="SaveCustodian()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;Cancel&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

