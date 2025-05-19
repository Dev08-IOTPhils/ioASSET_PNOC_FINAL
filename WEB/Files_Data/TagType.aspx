<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TagType.aspx.cs" Inherits="Files_Data_TagType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/TagType.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Tag Type
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Tag Type</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Add Tag Type" onclick="addTagTypeDialogBox()" />
                </div>
            </div>
        </div>

        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">List of Tag Type</h3></center>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblTagType" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>TAG TYPE</td>
                                <td>DESCRIPTION</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="tagtype">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteTagType" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Tag Type</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Tag Type?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveTag Type()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnTagType" onclick="DeleteTagType(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="addTagTypeDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit Tag Type</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <%--<div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:DropDownList ID="ddlLocation" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                    </div>
                    <br />--%>
                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Tag Type:</b></h6>
                        </div>--%>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <input id="txtTagType" maxlength="50" type="text" placeholder="Tag Type" style="width: 100%" class="form-control" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Description:</b></h6>
                        </div>--%>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <textarea class="form-control" id="txtDescription" placeholder="Description" rows="5" cols="1" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,500,'cnt', this.id)"></textarea>
                            <h6>
                                <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                        </div><div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                    </div>

                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>--%>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left" /><label class="pull-left">&nbsp;&nbsp; Active</label>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnSaveTagType" onclick="SaveTagType()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>