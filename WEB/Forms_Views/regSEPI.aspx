<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="regSEPI.aspx.cs" Inherits="Views_regSEPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/regSEPI.js"></script>
    <script>
        function PrintReport() {
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
        <h1>Registry of Semi-Expendable Property Issued
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Views</li>
            <li>Registry of Semi-Expendable Property Issued</li>
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
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('regSEPI')" />
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
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" onchange="load_data_Pendingupload();" />
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" onchange="load_data_Pendingupload();" />
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <%-- <div class="input-group margin">
                    <input type="text" class="form-control" id="txtSearch"  placeholder="Enter Property Issuance detail" />
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-primary btn-flat" onclick="load_data_Pendingupload()">Search</button>
                    </span>
                </div>--%>
            </div>
        </div>

        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Ledger Card</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                            <%--<input type="text" class="form-control" id="txtSearch" placeholder="Enter Issuance detail" onkeypress="return EnterKeySearch(event)" />--%>
                            <%--<span class="input-group-btn">
                                <button type="button" class="btn btn-primary btn-flat" onclick="load_data()">Search</button>
                            </span>--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12  col-md-6 col-sm-12 col-xs-12">
                    <table id="tblLedgerCardList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>DATE</td>
                                <td>PROPERTY NUMBER</td>
                                <td>ITEM DESCRIPTION</td>
                                <td>ESTIMATED USEFUL LIFE</td>
                                <td>AMOUNT</td>
                                <td>REMARKS</td>
<%--                                <td>MODEL</td>--%>
<%--                                <td>ACTION</td>--%>
                            </tr>
                        </thead>
                        <tbody id="ledgercardlist">
                        </tbody>
                    </table>
                </div>
                <div class="btn-group pull-left " >
                    <%--<button type="button" class="btn btn-success pull-left">Print</button>--%>
                    <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                        Print
                        <span class="caret"></span>
                        <span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <ul class="dropdown-menu" role="menu" style="overflow: scroll; max-height: 100px;">
                        <li><a id="rspibtn" style="cursor: pointer">Registry of Semi-Expendable Property Issued Form</a></li>
                        <%--//gheghe(Certificate/Word)--%>
                        <li><a id="rspixcl" style="cursor: pointer">Registry of Semi-Expendable Property Issued Form (excel)</a></li>
                    </ul>
                </div>
            </div>

        </div>

    </section>
    <!-- Print -->

    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divReport" runat="server">
        </div>
    </div>
</asp:Content>
