<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RFIDLogsView.aspx.cs" Inherits="Views_RFIDLogsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/RFIDLogsView.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Logs History
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Property Logs History</li>
        </ol>
    </section>
    <br />
    <section class="content">

        <div class="row" style="display: block">
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date From</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" onchange="LoadRFIDLogs();" />
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" onchange="LoadRFIDLogs();" />
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            </div>
        </div>


        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border">
                <center><h3 class="box-title">RFID Logs</h3></center>
            </div>
            <div class="box-body">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
               <%-- <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                    <input type="text" class="form-control" id="txtSearch" placeholder="search" onkeypress="return EnterKeySearch(event)" />

                </div>--%>
            <div class="col-lg-12">
                <table id="tblRFIDLogs" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                    <thead style="font-weight: bold">
                        <tr>

                            <td>DATE</td>
                            <td>DEVICE NAME</td>
                            <td>IP ADDRESS</td>
                            <td>COM. PORT</td>
                            <td>RFID</td>
                            <td>ITEM CODE</td>
                            <td>PROPERRTY NUMBER</td>
                            <td>SERIAL NUMBER</td>
                            <td>DESCRIPTION</td>
                            <td>SITE</td>
                            <td>LOCATION</td>
                            <td>AREA</td>
                            <td>ISSUED TO</td>
                            <td>STATUS</td>

                        </tr>
                    </thead>
                    <tbody id="tblRFIDLogsDets">
                    </tbody>
                </table>
            </div>
        </div>
        </div>
    </section>
</asp:Content>
