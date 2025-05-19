<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Module.aspx.cs" Inherits="System_Module" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/Module.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Modules
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i> System</li>
            <li>Module</li>
        </ol>
    </section>
    <br />
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
            <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Add Module" onclick="addModuleDialogBox()" />
            </div>
        </div>
    </div>

    <section class="content">
        <div class="box box-solid box-primary">
            <%--<div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Module</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body" id="ModuleList2" runat="server">
            </div>--%>

            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Module</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblModuleList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>Name</td>
                                <td>Arrangement</td>
                                <td>URL</td>
                                <td>Parent Module</td>
                                <td>Active</td>
                                <td>Web Page</td>
                                <td>Action</td>
                            </tr>
                        </thead>
                        <tbody id="ModuleList">
                        </tbody>
                    </table>
                </div>
            </div>


        </div>
    </section>

    <div id="deleteModule" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:indianred; text-align:center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4 class="modal-title"> <p><span class="fa fa-trash"></span>Delete Module</p></h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Module?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteModule" onclick="DeleteModule(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%--add module--%>
    <div id="addModuleDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color:steelblue; text-align:center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="Clear()">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title"><p><span class="fa fa-edit"></span>Add/Edit Module</p></h4>
                </div>
                <div class="modal-body">
                     <%--<div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Parent Module:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <select id="mySelect"></select>
                        </div>
                    </div>--%>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Name:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtName" maxlength="50" type="text" class="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>URL:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtURL" type="text" maxlength="100" class="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Arrangement:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtArrangement" type="text" class="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Parent Module:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlParentModule" class="select2 form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Description:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <textarea class="form-control" id="txtDescription" rows="5" cols="2" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,100,'cnt', this.id)"></textarea>
                            <h6 align="left">
                                <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Web Page:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkWebPage" class="checkbox" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Active:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkActive" class="checkbox" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

