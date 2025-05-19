<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Location.aspx.cs" Inherits="Files_Data_Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/Location.js"></script>
    <style>
        .swal-text {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
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
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddLocation" type="button" value="Add Department" runat="server" onclick="addLocationDialogBox('Location')" />
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
                    <table id="tblLocation" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;"> <%----%>
                        <thead style="font-weight: bold">
                            <tr>
                                <td><asp:Label ID="lblListLoc" runat="server" Text="Label" ></asp:Label></td>
<%--                                <td>DEPARTMENT</td>--%>
                                <td><asp:Label ID="lblListLoc1" runat="server" Text="Label" ></asp:Label></td>
<%--                                <td>DEPARTMENT CODE</td>--%>
                                <td><asp:Label ID="lblListArea" runat="server" Text="Label" ></asp:Label></td>
<%--                                <td>LOCATION</td>--%>
                                <td>DESCRIPTION</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="location">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    
</asp:Content>

