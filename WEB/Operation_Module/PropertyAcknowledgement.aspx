<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyAcknowledgement.aspx.cs" Inherits="Operation_Module_PropertyAcknowledgement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyAcknowledge.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Acknowledgement
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Property Acknowledgement</li>
        </ol>
    </section>

    <section class="content">
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Available for Acknowledgement</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>SERIAL #</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td><asp:Label ID="lblMasterListSite" runat="server" Text="Label"></asp:Label></td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
