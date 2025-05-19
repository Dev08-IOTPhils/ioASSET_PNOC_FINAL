<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockDisposalHistory.aspx.cs" Inherits="Views_StockDisposalHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/StockDisposalHistory.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Supply Disposal History
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Views</li>
            <li>Supply Disposal History</li>
        </ol>
    </section>

    <section class="content">
        
        <div class="row" style="display: block">
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date From</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" onchange="load_data_Pendingupload();"/>
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
        <div class="box box-solid box-primary" id="divPending">
            <div class="box-header with-border">
                <center><h3 class="box-title">Supply Disposal History</h3></center>
            </div>
            <div class="box-body">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" hidden>

                    <input type="text" class="form-control" id="txtSearch" placeholder="Enter Disposal detail" onkeypress="return EnterKeySearch(event)" />

                </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td >DISPOSAL #</td>
                                <td >DISPOSED BY</td>
                                <td >DATE</td>
                                <td >STATUS</td>
                                <td >DECLINE/CANCEL REMARKS</td>
                                <td >DOCUMENT</td>
                                <td >ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pendinguploadlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="row" id="printablepage" runat="server" hidden>
            <div class="row" id="divDisposalReport" runat="server">
            </div>
        </div>
    </section>

    <%-- Stock Disposal --%>
    <div id="ALPendingUpload" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color:white;">List of Supply</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposal No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDisposalNo" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposed By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDisposedBy" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposed Date:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDisposedDate" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPUL1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td >ITEM CODE</td>
                                        <td >DESCRIPTION</td>
                                        <td >CLASSIFICATION</td>
                                        <td >CATEGORY</td>
                                        <td >BRAND</td>
                                        <td >MODEL</td>
                                        <td >QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="pul1">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

