<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Area.aspx.cs" Inherits="Files_Data_Area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Area.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1><asp:Label ID="lblHeader" runat="server" Text="Label"></asp:Label>

            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li><asp:Label ID="lblbreadCrumb" runat="server" Text="Label"></asp:Label></li>
        </ol>
    </section>

    <section class="content">


        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-5" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddArea" type="button" value="Add Office" runat="server"  onclick="addAreaDialogBox('Area')" />
                </div>
            </div>
        </div>

        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title"><asp:Label ID="lblListOf" runat="server" Text="-"></asp:Label></h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblArea" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; "> <%--table-layout: fixed;--%>
                        <thead style="font-weight: bold">
                            <tr>
                            <td><asp:Label ID="lblListArea" runat="server" Text="Label" ></asp:Label></td>
                            <td><asp:Label ID="lblListLoc" runat="server" Text="Label" ></asp:Label></td>
                            <td><asp:Label ID="lblListSite" runat="server" Text="Label" ></asp:Label></td>
<%--                                <td>GROUP</td>
                                <td>DEPARTMENT</td>--%>
                                <td>DESCRIPTION</td>
                                <td>ACTIVEssssssssssssssssssssss</td>
                                <td>ACTIONSSSSSSSSSSSSSSSSSSSSSS</td>
                            </tr>
                        </thead>
                        <tbody id="area">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteArea" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Office</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Office?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveArea()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnAreaArea" onclick="DeleteArea(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>   
</asp:Content>

