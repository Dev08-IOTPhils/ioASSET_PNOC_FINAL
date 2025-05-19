<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DayPassView.aspx.cs" Inherits="Views_DayPassView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/PropertyDayPassView.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Personal Property Gate Pass Request View
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Personal Property Gate Pass Request View</li>
        </ol>
    </section>
    <br />
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
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('DayPassView')" />
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
         <%--<div class="row" style="display: block">
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon"><i class="fa fa-calendar"></i>&nbsp;Date From</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" />
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon"><i class="fa fa-calendar"></i>&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" />
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 pull-right">
                <div class="input-group margin">
                    <input type="text" class="form-control" id="txtSearch" placeholder="search for... Requestor or Day Pass Number" />
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-primary btn-flat"  onclick="load_data_Pendingupload()">Search</button>
                    </span>
                </div>
            </div>
        </div>--%>
        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Personal Property Gate Pass Request List</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body"><div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <%--<div class="input-group margin">--%>
                    <%--<input type="text" class="form-control" id="txtSearch" placeholder="Enter Requestor or Day Pass Number"  onkeypress="return EnterKeySearch(event)" />--%>
                    <%--<span class="input-group-btn">
                        <button type="button" class="btn btn-primary btn-flat"  onclick="load_data_Pendingupload()">Search</button>
                    </span>--%>
                <%--</div>--%>
            </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td style="width: 10%">PERSONAL PASS #</td>
                                <td style="width: 20%">REQUESTED BY</td>
                                <td style="width: 20%">DATE</td>
                                <td style="width: 35%">PURPOSE</td>
                                <td style="width: 15%">STATUS</td>
                                <td style="width: 5%">IN</td>
                                <td style="width: 5%">OUT</td>
                                <td style="width: 10%">ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pendinguploadlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div id="modalPropertyList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>Day Pass Request Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPAL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>SERIAL #</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>DESCRIPTION</td>
                                    </tr>
                                </thead>
                                <tbody id="pal">
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success pull-left" id="btnCheckIn" onclick="CheckIn()">&nbsp;&nbsp;Check In&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-success pull-left" id="btnCheckOut" onclick="CheckOut()">&nbsp;&nbsp;Check Out&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    </section>
</asp:Content>

