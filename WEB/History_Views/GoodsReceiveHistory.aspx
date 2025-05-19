<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReceiveHistory.aspx.cs" Inherits="Views_GoodsReceiveHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/GoodsReceiveHistory.js"></script>
    <style>
        table.dataTable tbody td {
            word-break: break-word;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Goods Receive History
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-eye"></i>Views</li>
            <li>Property Goods Receive History</li>
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
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('GoodsReceiveHistory')" />
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
            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date From</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" onchange="load_data_GRH();"  />
                </div>
            </div>
            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" onchange="load_data_GRH();" />
                </div>
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Goods Received</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                            <%--<input type="text" class="form-control" id="txtSearch" placeholder="Enter Goods Receive detail" onkeypress="return EnterKeySearch(event)" />--%>
                            <%--<span class="input-group-btn">
                                <button type="button" class="btn btn-primary btn-flat" onclick="load_data()">Search</button>
                            </span>--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <table id="tblGoodsReceive" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>GOODS RECEIVE #</td>
                                <td>RECEIVED BY</td>
                                <td>NO. OF ITEMCODE</td>
                                <td>DATE</td>
                                <td>DECLINE/CANCEL REMARKS</td>
                                <td>DOCUMENT</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="goodsreceive">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>
    <div id="viewGRDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p><span class="fa fa-list"></span>Goods Receive Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblGRDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100% ;"><%-- --%>
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>PR NUMBER</td>
                                        <td>PO NUMBER</td>
                                        <td>INVOICE NUMBER</td>
                                        <td>DR NUMBER</td>
                                        <td>REMARKS</td>
                                        <td>QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="grdetails">
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
                <div class="modal-footer" id="completetransaction">
                    <%-- <asp:Button ID="btncomplete" type="button" class="btn btn-sm btn-primary pull-right" runat="server" Text="Complete Transaction" OnClick="btnCompleteGR"  OnClientClick="Close()"/>
                    <asp:Button ID="uploadAF" type="button" class="btn btn-sm btn-primary pull-right" runat="server" Text="Upload" OnClick="btnUploadGR" OnClientClick="Close()" />
                    <asp:FileUpload ID="GRupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 30%;" />
                    <label class="pull-right">&nbsp;With Supporting Document?</label><input type="checkbox" id="chkWithDoc" class="checkbox pull-right" />--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

