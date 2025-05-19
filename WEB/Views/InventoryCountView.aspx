<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryCountView.aspx.cs" Inherits="Views_InventoryCountView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyInventoryProcessView.js"></script>
    <script type="text/javascript">
        function PrintGridDataProperty() {
            var prtGrid = document.getElementById('<%=printpageProperty.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataStock() {
            var prtGrid = document.getElementById('<%=printpageStock.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
    </script>
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
        <%--ADVANCE FILTER--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Advance Filter</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>


            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        Logic:
                                <div class="select2-blue">
                                    <select class="select2" id="drpLogic" data-placeholder="Select a Logic" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        <option>Select Logic </option>
                                        <option>AND </option>
                                        <option>OR </option>
                                    </select>
                                </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        Filter By:
                                <div class="form-group">
                                    <asp:DropDownList ID="drpFilterBy" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>

                                </div>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        Operator:
                                <div class="select2-blue">
                                    <select class="select2" id="drpOperator" data-placeholder="Select an Operator" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        <option selected="selected">Select Operator</option>
                                        <option>= </option>
                                        <option>> </option>
                                        <option>>= </option>
                                        <option>< </option>
                                        <option><= </option>
                                        <option>Not = </option>
                                        <option>Like </option>
                                        <option>In </option>
                                        <option>Not In </option>
                                    </select>
                                </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        Value:
                                <div class="form-group">
                                    <input type="text" style="width: 100%" placeholder="Value" min="0" id="txtValue" name="txtValue" class="form-control" />
                                    <%--<span id="spandropdown">
                                        <select class="select2" id="drpValue" data-placeholder="Select an Operator" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        </select>
                                    </span>
                                    <span id="MultiSelect1">
                                        <select class="select2" multiple="multiple" id="multiSelect" data-placeholder="Enter Value" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        </select>
                                    </span>--%>
                                </div>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        &nbsp
                                    <div class="form-group">
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('PropertyMasterlist')" />
                                    </div>
                    </div>

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                        <table id="tblFilterDisplay" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                            <thead style="font-weight: bold">
                                <tr>
                                    <td class="dt-body-center">Logic</td>
                                    <td class="dt-body-center">Filter By</td>
                                    <td class="dt-body-center">Operation</td>
                                    <td class="dt-body-center">Value</td>
                                    <td class="dt-body-center">Active</td>
                                    <td class="dt-body-center">Action</td>
                                </tr>
                            </thead>
                            <tbody id="FilterList">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <%--END OF ADVANCE FILTER--%>
        <div class="row" style="display: block">
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date From</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" onchange="load_data_inventorylist();" />
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" onchange="load_data_inventorylist();" />
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <%-- <div class="input-group margin">
                    <input type="text" class="form-control" id="txtSearch" placeholder="Enter Inventory detail"/>
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-primary btn-flat" onclick="load_data_inventorylist()">Search</button>
                    </span>
                </div>--%>
            </div>
        </div>
        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Inventory List</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                    <%--<input type="text" class="form-control" id="txtSearch" placeholder="Enter Inventory detail" onkeypress="return EnterKeySearch(event)" />--%>
                </div>
                <div class="col-lg-12">
                    <table id="tblInventoryList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>INVENTORY NAME</td>
                                <td>DESCRIPTION</td>
                                <td>SITE</td>
                                <td>LOCATION</td>
                                <%--<td><asp:Label ID="lblMasterListSite" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td><asp:Label ID="lblMasterListLoc" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>--%>
                                <td>INVENTORY TYPE</td>
                                <td>DATE CREATED</td>
                                <td>DOCUMENT</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="inventorylist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="row" id="printpageProperty" runat="server" hidden>
            <div class="row" id="divPropertyPrintList" runat="server">
            </div>
        </div>

        <div class="row" id="printpageStock" runat="server" hidden>
            <div class="row" id="divStockPrintList" runat="server">
            </div>
        </div>
    </section>

    <div id="inventorylistmodal" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List for Inventory</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:Button ID="btnExport" runat="server" Text="Export" type="button" class="btn btn-success pull-left" OnClick="btnExportProperty" />
                        <asp:TextBox ID="txtInventoryCode" runat="server" type="text" class="form-control" Style="width: 100%" />
                        <asp:TextBox ID="txtInventoryName" runat="server" type="text" class="form-control" Style="width: 100%" />
                        <asp:TextBox ID="txtInventoryLocation" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <!-- Custom Tabs -->
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active" id="propertyTab"><a href="#tab_1" data-toggle="tab">Property</a></li>
                                <li id="stockTab"><a href="#tab_2" data-toggle="tab">Stock</a></li>
                            </ul>
                            <div class="tab-content">
                                <!-- Property-->
                                <div class="tab-pane active" id="tab_1">
                                    <div class="box box-solid box-primary">
                                        <div class="box-header with-border">
                                            <center><h3 class="box-title">List of Property/ies</h3></center>
                                        </div>
                                        <div class="box-body">
                                            <div class="divContentBody" id="PropertyInventoryListDetails" runat="server"></div>
                                        </div>
                                        <br />
                                        <br />
                                        <div hidden>
                                            <button type="button" class="btn btn-success pull-left" id="btnExportProperty" onclick="ExportProperty()">&nbsp;&nbsp;Export&nbsp;&nbsp;</button>
                                        </div>
                                        <div class="btn-group pull-left">
                                            <button type="button" class="btn btn-success pull-left">Print</button>
                                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                                <span class="caret"></span>
                                                <span class="sr-only">Toggle Dropdown</span>
                                            </button>
                                            <ul class="dropdown-menu" role="menu" id="printGrid">
                                                <li><a style="cursor: pointer" onclick="PrintGridDataProperty()">Inventory Property List</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- stock -->
                                <div class="tab-pane" id="tab_2">
                                    <br />
                                    <div class="box box-solid box-primary">
                                        <div class="box-header with-border">
                                            <center><h3 class="box-title">List of Supply/Supplies</h3></center>
                                        </div>
                                        <div class="box-body">
                                            <div class="divContentBody" id="StockInventoryListDetails" runat="server"></div>

                                        </div>
                                        <br />
                                        <br />
                                        <button type="button" class="btn btn-sm btn-success pull-left" id="btnPrintStock" onclick="PrintGridDataStock()">&nbsp;&nbsp;Print&nbsp;&nbsp;</button>
                                    </div>

                                </div>
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- nav-tabs-custom -->
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

