<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Home_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PassFilter(filter) {
            var alreadyFilter = '../Custodian_Operation_Module/PropertyVerification.aspx'.indexOf("?") // there is already filter
            if (filter != -1)
                document.location.href = '../Custodian_Operation_Module/PropertyVerification.aspx&' + filter
            else
                document.location.href = '../Custodian_Operation_Module/PropertyVerification.aspx?' + filter
        }

        function FirstTimer() {
            swal({
                title: "Password!",
                text: "Kindly update your password!",
                icon: "warning",
                reverseButtons: false,
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    window.location.href = "../User_Management/UserProfile.aspx";
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    </script>
    <script src="../Script/DashBoard.js"></script>
    <script src="../Script/Widgets.js"></script>

    <style>
        svg {
            overflow: visible !important;
        }
    </style>
    <%--added by nero--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Dashboard
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-home"></i>Home</li>
            <li>Dashboard</li>
        </ol>
    </section>
    <section class="content">
        <%--Carousel Bulletin--%>
        <div class="box  box-solid box-primary" id="divCarousel">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Bulletin</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-lg12 col-md-12">
                    <div class="row">
                        <div id="carouselExampleIndicators" runat="server" class="carousel slide" data-ride="carousel">
                        </div>
                    </div>
                    <div class="row">
                        <div id="announcement" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>

        <%-- ---- ---- widgets ---- ---- --%> <%--Nero--%>
        <div id="divWidgets">
            <div class="row">
                <section class="col-lg-6">
                    <div class="box  box-solid box-primary ">
                        <div class="box-header with-border" data-widget="collapse">
                            <h3 class="box-title"><span></span>Supply Under Reorder Point</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <%-- %><div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                            <div class="input-group margin">
                                <select id="ddlSUMLType" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px" onchange="SUMLWidgetType()">
                                    <option value="Table">Table</option>
                                    <option value="Bar Graph">Bar Graph</option>
                                    <option value="Pie Chart">Pie Graph</option>
                                </select>
                            </div>
                        </div>--%>
                        <div class="box-body row">
<%--                        <button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="TestExcel()">TEST EXCEL</button>--%>
                            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                <select id="ddlSUMLType" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px" onchange="SUMLWidgetType()">
                                    <option value="1">Table</option>
                                    <option value="2">Bar Graph</option>
                                    <option value="3">Pie Graph</option>
                                </select>
                            </div>
                        </div>
                        <div class="box-body">
                            <div id="SUMLTable" class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row">
                                <table id="tblStocksUnderMinLevel" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                                    <thead style="font-weight: bold">
                                        <tr>
                                            <td>ITEM CODE</td>
                                            <td>DESCRIPTION</td>
                                            <td>REORDER POINT</td>
                                            <td>QUANTITY</td>
                                        </tr>
                                    </thead>
                                    <tbody id="StocksUnderMinLevel">
                                    </tbody>
                                </table>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row" id="SUMLBarChart" style="display: none">
                                <%--<div class="chart" id="SUMLBarchartDisplay" style="height: auto"></div>--%>
                                <div class="chart" id="bar-chart1" style="height: auto"></div>
                                <div id="stocks-bar-chartLegend"></div>
                            </div>
                            <div class="box-body">
                                <div class="row" id="DIVDonutChart" style="display: none">
                                    <div class="row">
                                        <div class="chart" id="DonutChart" style="height: 300px; position: relative;"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div id="SUMLlegend" class="donut-legend"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <section class="col-lg-6">
                    <div class="box  box-solid box-primary">
                        <div class="box-header with-border" data-widget="collapse">
                            <h3 class="box-title"><span></span>Property Important Schedule</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                    <%--<div class="input-group margin">--%>
                                    <select id="pislwidgettype" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px; margin-top: 6px; margin-bottom: 6px" onchange="PISLWidgetType()">
                                        <option value="1">Table</option>
                                        <option value="2">Bar Graph</option>
                                        <option value="3">Pie Graph</option>
                                    </select>
                                    <%--</div>--%>
                                </div>
                            </div>

                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <div>
                                        <div class="input-prepend input-group">
                                            <span class="add-on input-group-addon">Date From</span>
                                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <div>
                                        <div class="input-prepend input-group">
                                            <span class="add-on input-group-addon">Date To</span>
                                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row mb-5">

                            </div>--%>
                        </div>
                        <div class="box-body">
                            <div class="row mb-5" id="PISLtablebody">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <table id="tblPropertyImportantSched" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                        <thead style="font-weight: bold">
                                            <tr>
                                                <td>DATE</td>
                                                <td>PROPERTY NO.</td>
                                                <td>DESCRIPTION</td>
                                                <td>ACTIVITY</td>

                                            </tr>
                                        </thead>
                                        <tbody id="PropertyImportantSchedList">
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="row" id="PISLbarbody" <%--style="display: none"--%>>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row" id="PISLBarChartBody">
                                    <%--<div class="chart" id="SUMLBarchartDisplay" style="height: auto"></div>--%>
                                    <div class="chart" id="PISLbarchart" style="height: auto"></div>
                                    <div id="PISL-bar-chartLegend"></div>
                                </div>

                            </div>
                            <div class="box-body">
                                <div class="row" id="PISLdonutbody" <%--style="display:none"--%>>
                                    <div id="PropImpSchedBody">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row">
                                            <div class="chart" id="DonutPropertyImportantSched" style="height: 300px; position: relative;"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div id="PISLlegend" class="donut-legend"></div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>



                        </div>
                    </div>
                </section>
            </div>
            <div class="row">
                <section class="col-lg-6 ">
                    <div class="box  box-solid box-primary " data-widget="collapse">
                        <div class="box-header with-border" data-widget="collapse">
                            <h3 class="box-title"><span></span>Property Status</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12 ">
                                    <%--<div class="input-group margin">--%>
                                    <select id="ddlStatusType" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px" onchange="StatusWidgetType()">
                                        <option value="1">Table</option>
                                        <option value="2">Bar Graph</option>
                                        <option value="3">Pie Graph</option>
                                    </select>
                                    <%--</div>--%>
                                </div>
                            </div>

                        </div>
                        <div class="box-body" id="StatusPie">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row" id="StatusBody">
                                <table id="tblPropertyStatus" class="table table-bordered table-hover table-condensed display" style="width: 100%; margin-top: 6px">
                                    <thead style="font-weight: bold">
                                        <tr>
                                            <td>STATUS</td>
                                            <td>COUNT</td>
                                        </tr>
                                    </thead>
                                    <tbody id="PropertyStatus">
                                    </tbody>
                                </table>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row" id="StatusBarChart" style="display: none">
                                <div class="chart" id="StatusBarChart1" style="height: auto"></div>

                            </div>

                            <div id="StatusDonut" style="display: none">
                                <div class="row">
                                    <div class="chart" id="Status_Count" style="height: 300px; position: relative;"></div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div id="legend" class="donut-legend"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </section>
                <section class="col-lg-6">
                    <div class="box  box-solid box-primary">
                        <div class="box-header with-border" data-widget="collapse">
                            <h3 class="box-title"><span></span>Top 10 Most Used Item</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>


                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                    <%--<div class="input-group margin">--%>
                                    <select id="ddlTop10" class="form-control select2 row" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px" onchange="StockOrProperty()">
                                        <option value="1">Property</option>
                                        <option value="2">Supplies</option>
                                    </select>
                                    <%--</div>--%>
                                </div>
                            </div>
                             
                            <div>
                                <div class="mt-2 row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 5px;">

                                        <div id="Property_body">
                                            <%--<div class="input-group margin">--%>
                                            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12 row">
                                                <select id="ddlTop10WidgetTypeProperty" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px" onchange="PropertyWidgetType()">
                                                    <option value="1">Table</option>
                                                    <option value="2">Bar Graph</option>
                                                </select>
                                                
                                            </div>
                                            <br/><br/>
                                            <%--</div>--%>
                                            <div >
                                                <div id="tblAssetListBodytop10">
                                                   
                                                    <table id="tblAssetListTop10" role="grid" class="table table-bordered table-hover table-condensed display" style="width: 100%; table-layout: fixed;">
                                                        <thead style="font-weight: bold">
                                                            <tr>
                                                                <td>ITEM CODE</td>
                                                                <td>DESCRIPTION</td>
                                                                <td>VALUE</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="assetlisttop10">
                                                        </tbody>
                                                    </table>
                                                </div>

                                                <div id="barAssetListBodytop10" style="display: none">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row">
                                                        <div class="chart" id="bar-chart-Property" style="height: auto"></div>
                                                        <div id="bar-chartLegend-prop"></div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class=" mt-2 row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div id="Stock_body" style="display: none">
                                            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12 row">
                                                <select id="ddlTop10WidgetTypeStock" class="form-control select2 row" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px" onchange="StockWidgetType()">
                                                    <option value="1">Table</option>
                                                    <option value="2">Bar Graph</option>
                                                </select>
                                            </div>
                                            <br/>
                                            <div id="tblStockListBodytop10">
                                                <table id="tblStockListTop10" role="grid" class="table table-bordered table-hover table-condensed display" style="width: 100%; table-layout: fixed;">
                                                    <thead>
                                                        <tr>
                                                            <td>ITEM CODE</td>
                                                            <td>DESCRIPTION</td>
                                                            <td>VALUE</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="stocklisttop10">
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div id="barStockListBodytop10" style="display: none">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row">
                                                    <div class="chart" id="bar-chart-stock" style="height: auto"></div>
                                                    <div id="bar-chartLegend-stock"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
        <%-- ---- ---- ends widgets here ---- ---- --%>

        <%--Pending Acknowledgement--%>
        <div class="box  box-solid box-primary" id="divPendingAcknowledgement">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Acknowledgement</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Operation_Module/PropertyAcknowledgement.aspx">
                                    <div style="color: white;" id="divAcknowledgement" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--<div class="inner" id="divAcknowledgement" runat="server">
                            </div>--%>
                            <a href="../Operation_Module/PropertyAcknowledgement.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--Pending Approval--%>
        <div class="box  box-solid box-primary" id="divPendingApproval">
            <div class="box-header with-border" data-widget="collapse">

                <h3 class="box-title"><span></span>Pending Approval</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-3 col-xs-6" id="divGR">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=1">
                                    <div style="color: white;" id="divGRforApproval" runat="server">
                                        <%-- <h3>150</h3>

                                <p>New Orders</p>--%>
                                    </div>
                                </a>

                            </div>
                            <%--<div class="icon">
                                <i class="fa fa-shopping-cart"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=1" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                                <%--<a href="../Home/RequestManagement.aspx?tt=2" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>--%>
                            </a>
                            <%-- <asp:LinkButton ID="lnkFilter1" runat="server" OnClientClick="return PassFilter('tt=1')" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:LinkButton>--%>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divPropIssuance">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=3">
                                    <div style="color: white;" id="divPropertyIssuanceforApproval" runat="server">
                                        <%--<h3>44</h3>

                                <p>User Registrations</p>--%>
                                    </div>
                                </a>
                            </div>
                            <%-- <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=3" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>

                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divPropTransfer">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=4">
                                    <div style="color: white;" id="divPropertyTransferforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--  <div class="icon">
                                <i class="ion ion-pie-graph"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=4" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-3 col-xs-6" id="divPropDisposal">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=5">
                                    <div style="color: white;" id="divPropertyDisposalforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--<div class="icon">
                                <i class="fa fa-shopping-cart"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=5" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divGatePass" hidden>
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=6">
                                    <div style="color: white;" id="divPropertyGatePassforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%-- <div class="inner" id="divPropertyGatePassforApproval" runat="server">
                                <%--<h3>53<sup style="font-size: 20px">%</sup></h3>

                                <p>Bounce Rate</p>--%>
                            <%-- <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=6" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divStockRequest">
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=12">
                                    <div style="color: white;" id="divStockRequestforApproval" runat="server">
                                    </div>
                                </a>

                            </div>

                            <%--<div class="icon">
                                <i class="fa fa-shopping-cart"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=12" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>

                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divCsvUpload">
                        <!-- small box -->
                        <div class="small-box bg-red">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=8">
                                    <div style="color: white;" id="divUploadedFileforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--  <div class="icon">
                                <i class="ion ion-pie-graph"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=8" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divDB">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=9">
                                    <div style="color: white;" id="divDatabaseRestoreforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--<div class="icon">
                                <i class="fa fa-shopping-cart"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=9" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6" id="divStockDisposal">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=13">
                                    <div style="color: white;" id="divStockDisposalforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--<div class="icon">
                                <i class="fa fa-shopping-cart"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=13" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-xs-6" id="divStockIssuance">
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=12">
                                    <div style="color: white;" id="divStockIssuanceforApproval" runat="server">
                                    </div>
                                </a>

                            </div>

                            <%--  <div class="icon">
                                <i class="ion ion-pie-graph"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=12" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                      <div class="col-lg-3 col-xs-6" id="divPropDisposalRequest" hidden>
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <a href="../Home/RequestManagement.aspx?tt=5">
                                    <div style="color: white;" id="divPropertyDisposalRequestforApproval" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--<div class="icon">
                                <i class="fa fa-shopping-cart"></i>
                            </div>--%>
                            <a href="../Home/RequestManagement.aspx?tt=5" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <div hidden>
                        <!-- ./col -->
                        <div class="col-lg-3 col-xs-6" id="divExtensionDate">
                            <!-- small box -->
                            <div class="small-box bg-yellow">
                                <div class="inner" id="divReturnExtensionforApproval" runat="server">
                                    <%--<h3>44</h3>

                                <p>User Registrations</p>--%>
                                </div>
                                <%-- <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>--%>
                                <a href="../Home/RequestManagement.aspx?tt=11" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                        <!-- ./col -->

                        <div class="col-lg-3 col-xs-6" id="divDayPass">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner" id="divPropertyDayPassforApproval" runat="server">
                                    <%--<h3>44</h3>

                                <p>User Registrations</p>--%>
                                </div>
                                <%-- <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>--%>
                                <a href="../Home/RequestManagement.aspx?tt=7" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-3 col-xs-6" id="divStockMerge">
                            <!-- small box -->
                            <div class="small-box bg-green">
                                <div class="inner" id="divStockMergeforApproval" runat="server">
                                    <%--<h3>53<sup style="font-size: 20px">%</sup></h3>

                                <p>Bounce Rate</p>--%>
                                </div>
                                <%-- <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>--%>
                                <a href="../Home/RequestManagement.aspx?tt=10" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
            </div>
        </div>

        <%--Pending Verification--%>
        <div class="box  box-solid box-primary" id="divPendingVerification">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Pending Verification</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=1">
                                    <div style="color: white;" id="divgrdetails" runat="server">
                                    </div>
                                </a>

                            </div>
                            <%--<div class="icon">
                            <i class="fa fa-shopping-cart"></i>
                        </div>--%>
                            <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=1" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                                <%--<a href="../Home/RequestManagement.aspx?tt=2" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>--%>
                            </a>
                            <%-- <asp:LinkButton ID="lnkFilter1" runat="server" OnClientClick="return PassFilter('tt=1')" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                        </asp:LinkButton>--%>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=2">
                                    <div style="color: white;" id="divissuancedetails" runat="server">
                                    </div>
                                </a>
                            </div>
                            <%-- <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>--%>
                            <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=2" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=3">
                                    <div style="color: white;" id="divtransferdetails" runat="server">
                                    </div>
                                </a>
                            </div>

                            <%-- <div class="icon">
                            <i class="ion ion-person-add"></i>
                        </div>--%>
                            <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=3" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                                        <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-red">
                            <div class="inner">
                                <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=5">
                                    <div style="color: white;" id="divreturndetails" runat="server">
                                    </div>
                                </a>
                            </div>

                            <%--<div class="icon">
                            <i class="fa fa-shopping-cart"></i>
                        </div>--%>
                            <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=5" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>

                    <!-- ./col -->
                </div>
                <div class="row">

                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=6">
                                    <div style="color: white;" id="divdisposedetails" runat="server">
                                    </div>
                                </a>
                            </div>

                            <%-- <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>--%>
                            <a href="../Custodian_Operation_Module/PropertyVerification.aspx?tt=6" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                            </a>
                        </div>
                    </div>
                    <!-- ./col -->

                </div>
            </div>
        </div>

        <%--Open Inventory Count--%>
        <div class="box  box-solid box-primary" id="divOpenInventoryCnt">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Open Inventory Count</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <a href="../Custodian_Operation_Module/InventoryCount.aspx">
                                    <div style="color: white;" id="divOpenInventoryCount" runat="server">
                                    </div>
                                </a>

                            </div>
                            <a href="../Custodian_Operation_Module/InventoryCount.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
                                <%--<a href="../Home/RequestManagement.aspx?tt=2" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>--%>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--Employee Ledger Card--%>
        <div class="box  box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Employee Ledger Card</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                            <%--<input type="text" class="form-control" id="txtSearch" placeholder="Enter Property detail" onkeypress="return EnterKeySearch(event)" />--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <table id="tblITEMList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>EMPLOYEE</td>
                                <td>ITEM CODE</td>
                                <td>PROPERTY #</td>
                                <td>CUSTODIAN TYPE</td>
                                <td>ITEM DESCRIPTION</td>
                                <td>ACQUISITION COST</td>
                                <td>PAR/ICS NUMBER</td>
                                <%--<td>PR #</td>
                            <td>PO #</td>
                            <td>INVOICE #</td>
                            <td>DR #</td>
                            <td>ISSUANCE #</td>
                            <td>ISSUED TO</td>
                            <td>SITE</td>
                            <td>LOCATION</td>
                            <td>AREA</td>
                            <td>ACQUIRED DATE</td>
                            <td>USEFUL LIFE</td>
                            <td>REMAINING USEFUL LIFE</td>
                            <td>NEXT MAINTENANCE DATE</td>
                            <td>ACQUIRED COST</td>
                            <td>DEPRECIATED COST</td>--%>
                            </tr>
                        </thead>
                        <tbody id="ItemList">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <%--Property Masterlist--%>
        <div class="box  box-solid box-primary" id="divPropertyMasterList">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Property Masterlist</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                            <%--<input type="text" class="form-control" id="txtSearchProperty" placeholder="Enter Property detail" onkeypress="return EnterKeySearch(event)" />--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <table id="tblAssetList" class="table table-bordered table-hover table-condensed compact" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>DESCRIPTION</td>
                                <td>PROPERTY #</td>
                                <td>LONG DESCRIPTION</td>
                                <td>SERIAL #</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>PR #</td>
                                <td>PO #</td>
                                <td>INVOICE #</td>
                                <td>DR #</td>
                                <td>ISSUANCE #</td>
                                <td>ISSUED TO</td>
                                <td><asp:Label ID="lblMasterListSite" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td><asp:Label ID="lblMasterListLoc" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td><asp:Label ID="lblMasterListArea" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td>ACQUIRED DATE</td>
                                <td>USEFUL LIFE</td>
                                <td>REMAINING USEFUL LIFE</td>
                                <td>NEXT MAINTENANCE DATE</td>
                                <td>ACQUIRED COST</td>
                                <td>DEPRECIATED COST</td>
                                <td>STATUS</td>
                                <td>DISPOSED DATE</td>
                                <td>ISSUED DATE</td>
                                <td>LAST MAINTENANCE DATE</td>
                            </tr>
                        </thead>
                        <tbody id="assetlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <%--Stock Masterlist--%>
        <div class="box  box-solid box-primary" id="divStockMasterList">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Supply Masterlist</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                            <%--<input type="text" class="form-control" id="txtSearchStock" placeholder="Enter Stock detail" onkeypress="return EnterKeySearch1(event)" />--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <table id="tblStockList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>DESCRIPTION</td>
                                <td>QUANTITY</td>
                                <td>UNIT</td>
                                <td>CUSTODIAN TYPE</td>
                                <td><asp:Label ID="lblStockListSite" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td>MIN. SUPPLY LEVEL</td>
                            </tr>
                        </thead>
                        <tbody id="stocklist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="DonnutModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Property Status</p>
                    </h4>
                </div>
                <div class="modal-body">


                    <table id="tblAssetListDonnut" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>STATUS</td>
                            </tr>
                        </thead>
                        <tbody id="assetlistdonnut">
                        </tbody>
                    </table>

                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="PISLModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Property Important Schedule List</p>
                    </h4>
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

                    <table id="PISLtblModal" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>DATE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>ACTIVITY</td>
                            </tr>
                        </thead>
                        <tbody id="PISLassetlistdonnut">
                        </tbody>
                    </table>

                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="SUMLModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Supplies Under Min Level List</p>
                    </h4>
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

                    <table id="SUMLtblModal" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>DESCRIPTION</td>
                                <td>REORDER POINT</td>
                                <td>QUANTITY</td>
                            </tr>
                        </thead>
                        <tbody id="SUMLLassetlistdonnut">
                        </tbody>
                    </table>

                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="StatBarModalBody" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Supplies Under Min Level List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <table id="StatBarModal" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>PROPERTY#</td>
                                <td>DESCRIPTION</td>
                                <td>VALUE</td>
                            </tr>
                        </thead>
                        <tbody id="StatModalBardonnut">
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>



    <div id="PISLDonnutModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Property Important Schedule List</p>
                    </h4>
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

                    <table id="PISLDonnutModaltbl" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>DATE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTIVE</td>
                                <td>ACTIVITY</td>
                            </tr>
                        </thead>
                        <tbody id="PISLassetlistdonnutModal">
                        </tbody>
                    </table>

                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>


    <div id="PILSModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Supplies Under Min Level List</p>
                    </h4>
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

                    <table id="PILStblModal" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>DATE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTIVE</td>
                                <td>ACTIVITY</td>
                            </tr>
                        </thead>
                        <tbody id="PISLLlistdonnut">
                        </tbody>
                    </table>

                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>


    <div id="top10Modal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Top 10 Most used Property</p>
                    </h4>

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

                    <table id="tblAssetListTop10Modal" class="table table-bordered table-hover table-condensed display" style="width: 100%; table-layout: fixed;">
                        <thead>
                            <tr>
                                <td>ITEM CODE</td>
                                <%--<td>PROPERTY #</td>--%>
                                <td>DESCRIPTION</td>
                                <td>VALUE</td>
                            </tr>
                        </thead>
                        <tbody id="assetlisttop10modal">
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="top10Modalsstock" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Top 10 Most used Supply</p>
                    </h4>
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
                    <table id="tblAssetListTop10Modalstock" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>DESCRIPTION</td>
                                <td>VALUE</td>
                            </tr>
                        </thead>
                        <tbody id="assetlisttop10modalstock">
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-sm btn-success" id="btnSaveModule" onclick="SaveModule()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

