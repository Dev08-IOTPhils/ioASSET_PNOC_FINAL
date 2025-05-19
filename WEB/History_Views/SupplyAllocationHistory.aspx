<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplyAllocationHistory.aspx.cs" Inherits="Views_StockIssuanceHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/SupplyAllocationHistory.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supply Allocation History
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-eye"></i>Views</li>
            <li>Supply Allocation History</li>
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
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('StockIssuanceHistory')" />
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
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly"  onchange="load_data_Pendingupload();"/>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" onchange="load_data_Pendingupload();" />
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Stock Allocation History</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                 <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">


                </div>
                <div class="col-lg-12">
                    <table id="tblStockIssuance" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ALLOCATION #</td>
                                <td>REMARKS</td>
                                <td>ORIGIN</td>
                                <td>DESTINATION</td>
                                 <td>CREATED BY</td>
                                <td>TRANSACTION DATE</td>
                                <td>STATUS</td>
                                <td>DECLINE/CANCEL REMARKS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="stockissuance">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <%-- Stock Details --%>
        <div id="StockDetailsPendingUpload" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: steelblue; text-align: center;">
                        <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">
                            <p>List of Supplies</p>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                                Issuance No:
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                                <label id="lblStockIssuanceNo" type="text" />
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                            </div>
                            <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                                <%--Issued By:--%>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                                <%--<label id="lblStockIssuedBy" type="text" />--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                                <%--Accountable Person:--%>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                                <%--<label id="lblStockAccountablePerson" type="text" />--%>
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                            </div>
                            <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                                <%--Issued Date:--%>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                                <%--<label id="lblStockDateIssued" type="text" />--%>
                            </div>
                        </div>
                        <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPUL1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ALLOCATION #</td>
                                        <td>REMARKS</td>
                                        <td>ORIGIN</td>
                                        <td>DESTINATION</td>
                                        <td>STATUS</td>
                                        <td>QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="stockallocationdetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
<%--                        <div class="row">
                            <div class="col-lg-12 table-responsive">
                                <table id="tblPUL1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                                    <thead style="font-weight: bold">
                                        <tr>
                                            <td>ITEM CODE</td>
                                            <td>DESCRIPTION</td>
                                            <td>CLASSIFICATION</td>
                                            <td>CATEGORY</td>
                                            <td>BRAND</td>
                                            <td>MODEL</td>
                                            <td>QUANTITY</td>
                                            <td><asp:Label ID="lblMasterListSite" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </thead>
                                    <tbody id="pul1">
                                    </tbody>
                                </table>
                            </div>
                        </div>--%>
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

