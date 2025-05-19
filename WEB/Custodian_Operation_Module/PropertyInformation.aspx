<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyInformation.aspx.cs" Inherits="Operation_Module_PropertyInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyInformation.js"></script>
    <style>
        td.details-control {
            background: url('../Image/Icon/plus_PNG47.png') no-repeat center center;
            cursor: pointer;
            background-size: 25px 25px;
        }

        tr.details td.details-control {
            background: url('../Image/Icon/minus_PNG47.png') no-repeat center center;
            background-size: 25px 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Information
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Property Information</li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Information</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                    </div>
                </div>
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>CUSTODIAN</td>
                                <td>ITEM CODE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>SERIAL #</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>DATE CREATED</td>
                                <td>ACQUIRED COST</td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </section>
    <div id="modalPropertyInfo" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-med">
            <div class="modal-content" id="ModalProp">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color:white;"><span></span>Property Information</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Item Code:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtItemCode" disabled="true" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Property #:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtPropertyNo" disabled="true" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            New Property #:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtNewPropertyNo" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Custodian Type:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <select id="ddlCustodian" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Acquired Cost:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtacquiredcost" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Serial #:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtSerialNo" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    
                    <br />
                    <div class="row" hidden>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Label To Print:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtLabelToPrint" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br / hidden>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Long Description:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <textarea id="txtDescription" type="text" style="width: 100%" class="form-control"></textarea>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Warranty Date:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtWarranty" class="form-control" onkeypress="return EnterKey(event)" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Insurance Date:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtInsurance" class="form-control" onkeypress="return EnterKey(event)" />
                        </div>
                    </div>
                     <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Expiry Date:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtExpiry" class="form-control" onkeypress="return EnterKey(event)" readonly="readonly" disabled />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success btn_EditPropertyInformation" onclick="UpdatePropertyInfo()">&nbsp;&nbsp;Update Information&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="modalEditPropertyNum" class="modal fade" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content" id="ModalProp">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color:white;"><span></span>Property Information</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Current Property #:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtCurrPN" disabled="true" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            New Property #:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtnewPN" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success btn_EditPropertyInformation" onclick="UpdateNewPropNum()">&nbsp;&nbsp;Update&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

