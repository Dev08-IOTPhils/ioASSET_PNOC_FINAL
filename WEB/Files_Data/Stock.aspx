<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Files_Data_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Stock.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supply Item Code
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Supply Item Code</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddStock" type="button" value="Add Supply Item Code" onclick="addStockDialogBox()" />
                </div>
            </div>
        </div>
        <br />

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Supply Item Code</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblStock" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>DESCRIPTION</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>TAG TYPE</td>
                                <td>RETURNABLE</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="property">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteStock" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Supply</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Supply?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteStock" onclick="DeleteStock(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="addStockDialogBox" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4 class="modal-title">
                        <p style="color: white;" style="color: white;">Add/Edit Supply Item Code</p>
                    </h4>
                </div>
                <div class="modal-body">
                      <div class="row">
                         <label class="control-label" align="left">
                                    <i>   <span style="color: red">* Required fields.</span>  </i>
                                    </label>
                    </div>
                        <br/>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                           
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                     <label class="control-label" align="left">
                               <span style="color: red">*</span> Item Code:
                            </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <%--<input id="txtItemCode" maxlength="50" type="text" placeholder="Max Length is 50" style="width: 100%" class="form-control" />--%>
                                    <asp:TextBox ID="txtItemCode" runat="server" type="text" onkeyup="return ReplaceStr2(this);" class="form-control" Style="width: 100%" /> <%-- placeholder="Item Code"--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                       <span style="color: red">*</span> Category:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>

                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                      <div id="HTStockCat" class="input-group">
                                        <%--<asp:DropDownList ID="ddlCategory" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                        <select id="ddlCategory" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                        <span id="HTStockCatC" class="btn btn-success input-group-addon" onclick="addCategoryDialogBox()"><i class="fa fa-plus"></i></span>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlCategsory" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                       <span style="color: red">*</span> Model:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                     <div id="HTStockModel" class="input-group">
                                       <select id="ddlModel" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                     <span id="HTStockModelC" class="btn btn-success input-group-addon" onclick="addModelDialogBox()"><i class="fa fa-plus"></i></span>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlModel" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row" hidden>
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        Asset Inventory Account:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <asp:DropDownList ID="ddlAssetInventoryAccount" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br / hidden>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        Income Account:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <asp:DropDownList ID="ddlIncomeAccount" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        Expense Account:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <asp:DropDownList ID="ddlExpenseAccount" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        <span style="color: red">*</span>Description:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <textarea class="form-control" id="txtDescription"<%-- placeholder="Description"--%> rows="5" cols="1" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,500,'cnt', this.id); return ReplaceStr2(this);"></textarea>
                                     <h6>
                                    <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <%--<div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        Active:
                                    </label>
                                </div>--%>
                               <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12"></div>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <%--<input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left" /><label class="pull-left">&nbsp;&nbsp; Active</label>--%>

                                    <input type="checkbox" class="switch_1 pull-left" id="chkActive" />
                                    <label class="pull-left">&nbsp;&nbsp; Active</label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        is Returnable:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <input type="checkbox" id="chkIsReturnable" class="switch_1 pull-left" /><label class="pull-left">&nbsp;&nbsp; Is Returnable</label>
                                    <%--<input type="checkbox" id="chkIsReturnable" class="checkbox" />--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <span style="color: red">*</span>Re order point:
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <%--<input id="txtItemCode" maxlength="50" type="text" placeholder="Max Length is 50" style="width: 100%" class="form-control" />--%>
                                    <input id="txtCriticalLevel"<%-- placeholder="Minimum stocking level"--%> type='number' min='0' class="form-control" onkeypress="return onlyNos(event,this)" ondrop="return false;" onpaste="return false;" style="width: 100%" />
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                 <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                       <span style="color: red">*</span> Classification:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <div id="HTStockClassification" class="input-group">
                                       <select id="ddlClassification" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                     <span id="HTStockClassificationC" class="btn btn-success input-group-addon" onclick="addClassificationDialogBox()"><i class="fa fa-plus"></i></span>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlClassification" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        <span style="color: red">*</span>Brand:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                     <div id="HTStockBrand" class="input-group">
                                       <select id="ddlBrand" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                     <span id="HTStockBrandC" class="btn btn-success input-group-addon" onclick="addBrandDialogBox()"><i class="fa fa-plus"></i></span>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlBrand" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        <span style="color: red">*</span>Unit of Measurement:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                     <div id="HTStockUoM" class="input-group">
                                       <select id="ddlUnitOfMeasurement" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                     <span id="HTStockUoMC" class="btn btn-success input-group-addon" onclick="addUnitOfMeasurementDialogBox()"><i class="fa fa-plus"></i></span>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlUnitOfMeasurement" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>

                            <br />
                             <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        Tag:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <div id="HTStockTag" class="input-group">
                                       <select id="ddlTag" class="form-control select2" onchange="GetRFIDandBarcodeGeneration();" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                     <span id="HTStockTagC" class="btn btn-success input-group-addon" onclick="addTagDialogBox()"><i class="fa fa-plus"></i></span>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlTagType" class="form-control select2" runat="server" Style="width: 100%; height: 35px" onchange="GetRFIDandBarcodeGeneration();"></asp:DropDownList>--%>
                                </div>
                            </div>
                            <br />
                            <div class="row" id="divrfid">
                               <%-- <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <asp:TextBox ID="RFIDNumber" placeholder="RFID Number" runat="server" type="text" class="form-control" Style="width: 100%" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row" id="divbarcode">
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <asp:TextBox ID="BarcodeNumber" placeholder="Barcode" runat="server" type="text" class="form-control" Style="width: 100%" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                    <label class="control-label">
                                        Image:
                                    </label>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                    <asp:FileUpload ID="FileUploadStockImage" onchange="getImage(event)" class="form-control" placeholder="Please Select .jpg, .jpeg, .png file." runat="server" Style="width: 100%; height: 35px" />
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <%--<div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                </div>--%>
                                <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" id="StockImage" runat="server">
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="SaveCancelStock">
                   <button type="button" class="btn btn-success" id="btnCompleteRequest" onclick="SaveStock()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                 <%--   <asp:Button ID="btnUpload" UseSubmitBehavior="true" runat="server" class="btn btn-success" OnClick="StockImageUpload" Text="Save" style="visibility: hidden"/>
                    <asp:Button ID="btnSetCompanyInformation" UseSubmitBehavior="false" runat="server" class="btn btn-success btn_EditStock" OnClientClick="SaveStock(this);"  Text="Save" />--%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;Cancel&nbsp;</button>
                    <%--<button type="button" class="btn btn-success pull-left" id="btnSaveStock" onclick="SaveStock()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

