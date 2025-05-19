<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryConsolidatedReport.aspx.cs" Inherits="Views_InventoryConsolidatedReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/ConsolidatedReport.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Inventory Count View
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Views</li>
            <li>Inventory Count View</li>
        </ol>
    </section>
    <section class="content">
        <div class="row" hidden>
            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-success pull-left" OnClick="btnExportConsolidated" />
        </div>
        <div class="row">
            <button type="button" class="btn btn-primary" onclick="getConsolidatedReport()">Generate</button>
        </div>

    </section>

</asp:Content>

