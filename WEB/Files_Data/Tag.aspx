<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tag.aspx.cs" Inherits="Files_Data_Tag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Tag.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Tag
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Tag</li>
        </ol>
    </section>


    <section class="content">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddTag" type="button" value="Add Tag" onclick="addTagDialogBox()" />
                </div>
            </div>
        </div>

        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Tag</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblTag" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%;table-layout:auto;"> <%----%>
                        <thead style="font-weight: bold">
                            <tr>
                                <td>TAG</td>
                                <td>TAG TYPE</td>
                                <td>DESCRIPTION</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="Tag">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

