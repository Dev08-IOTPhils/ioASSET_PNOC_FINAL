<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockMerge.aspx.cs" Inherits="Operation_Module_StockMerge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/StockMerge.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supply Merge
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Supply Merge</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>Merge existing Supplies into an existing Supply item code. This is useful when you have multiple vendors which supplies the exact same Supply, but have different item codes.</i>
                </p></center>
            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Supply</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
               <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">

                </div>
                <div class="col-lg-12">
                    <table id="tblStockList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>QUANTITY</td>
                                <td>UOM</td>
                                <td>DESCRIPTION</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="stocklist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Supply Merge Pending Approval</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                 <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>Supply inventory of involved supply item code above will not update/reflect accordingly until it is approved.</i>
                    </p></center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">

                </div>
                <div class="col-lg-12">
                    <table id="tblPending" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>SUPPLY MERGE NO</td>
                                <td>FROM ITEM CODE</td>
                                <td>TO ITEM CODE</td>
                                <td>QUANTITY</td>
                                <td>REMARKS</td>
                                <td>TRANSACTION DATE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pending">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="mergeStock" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color:white;"><span class="glyphicon glyphicon-transfer"></span>Merge Supply</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" id="withorwithoutyou">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the merge button, the transaction will be sent for approval to your selected approver. 
                           The approver will be notified with your request. After the approver's reply you too will be receiving  a notification. Make sure that the details are correct.</i>
                            </p></center>
                        </div>
                    </div>
                    <div class="row" id="withorwithoutyou1">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the merge button, the transaction will proceed to merge without approval. Make sure that the details are correct.
                            </i></p></center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Item Code:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtItemCode" disabled type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Supply Quantity:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtStockQuantity" type="text" style="width: 100%" class="form-control" disabled />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Merge To:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlMergeTo" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4" id="labelApprover">
                            <label class="control-label">
                                Approver:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8" id="dropdownApprover">
                            <asp:DropDownList ID="ddlApprover" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <textarea class="form-control" id="txtRemarks" placeholder="Remarks" runat="server"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnIssueStock" onclick="MergeStock(this.id)">&nbsp;&nbsp;Merge&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

