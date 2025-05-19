<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="StockCard.aspx.cs" Inherits="Views_StockCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/StockCard.js"></script>
    <script>
        function PrintStockCard() {
            var prtGrid = document.getElementById('<%=printablepage.ClientID %>');
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
        <h1>Stock Card
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Views</li>
            <li>Stock Card</li>
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
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('StockCard')" />
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
        <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">Stock Card</h3></center>
            </div>
            <div class="box-body">

                <div class="row" style="padding-left: 20px; padding-right: 20px;">
                    <div class="col-lg-12">
                        <table class="table table-responsive table-striped table-bordered" id="tblStockList" style="width: 100%;">
                            <thead class="info">
                                <tr style="background-color: #D9EDF7;">
                                    <th>ITEM CODE</th>
                                    <th>DESCRIPTION</th>
                                    <th>CLASSIFICATION</th>
                                    <th>CATEGORY</th>
                                    <th>BRAND</th>
                                    <th>MODEL</th>
                                    <th>ACTION</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>

                <div class="row" hidden>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <h6>Item Code:</h6>
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <select id="cbStock" class="form-control select2" style="width: 100%;"></select>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <h6>Month:</h6>
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <select id="cbMonth" class="form-control select2" style="width: 100%;"></select>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <h6>Year:</h6>
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <select id="cbYear" class="form-control select2" style="width: 100%;"></select>
                        </div>
                    </div>
                </div>
                <div class="row" hidden>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <button type="button" id="btnView" class="btn btn-sm btn-primary">View</button>
                            <button type="button" id="btnPrint" class="btn btn-sm btn-success">Print</button>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" style="padding-left: 20px; padding-right: 20px;" hidden>
                    <div class="table-responsive" id="tblHtml">
                        <table class="table table-striped table-bordered" id="tblLOAList">
                            <thead class="info">
                                <tr style="background-color: #D9EDF7;">
                                    <th>Item:</th>
                                    <th colspan="5"></th>
                                    <th>Stock No.:</th>
                                </tr>
                                <tr style="background-color: #D9EDF7;">
                                    <th>Description:</th>
                                    <th colspan="5"></th>
                                    <th colspan="2">Re-order Point:</th>
                                </tr>
                                <tr style="background-color: #D9EDF7;">
                                    <th colspan="2">Unit of Measurement:</th>
                                    <th colspan="4"></th>
                                    <th></th>
                                </tr>
                                <tr style="background-color: #D9EDF7;">
                                    <th rowspan="2" style="text-align: center; vertical-align: middle;">Date</th>
                                    <th rowspan="2" style="text-align: center; vertical-align: middle;">Reference</th>
                                    <th colspan="1" style="text-align: center; vertical-align: middle;">Receipt</th>
                                    <th colspan="2" style="text-align: center; vertical-align: middle;">Issue</th>
                                    <th style="text-align: center; vertical-align: middle;">Balance</th>
                                    <th rowspan="2" style="text-align: center; vertical-align: middle;">No. of Days to Consume</th>
                                </tr>
                                <tr style="background-color: #D9EDF7;">
                                    <th style="text-align: center; vertical-align: middle;">Qty</th>
                                    <th style="text-align: center; vertical-align: middle;">Qty</th>
                                    <th style="text-align: center; vertical-align: middle;">Group</th>
                                    <th>Qty</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Print -->

    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divStockCard" runat="server">
        </div>
    </div>
</asp:Content>
