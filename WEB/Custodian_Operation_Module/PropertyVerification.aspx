<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyVerification.aspx.cs" Inherits="Operation_Module_PropertyVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyVerification.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Verification
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Property Verification</li>
        </ol>
    </section>

    <section class="content">
        <div class="row" style="display: block">
            <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                <asp:DropDownList ID="ddlTransactionType" class="form-control select2" onchange="load_data();" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="ddlView" style="display: none">
                <asp:DropDownList ID="ddlCom" class="form-control select2" runat="server" Style="width: 100%; height: 35px" onchange="GetDeviceWriterStatus()"></asp:DropDownList>
            </div>
            <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12" id="divStat" style="display: none">
                <label id="lblWriterStatus" style="font-family: 'Arial'; font-weight: 400; font-size: 14px; margin-top: 6px;">STATUS:</label>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" id="divStatus" style="display: none">
                <label id="lblDeviceWriterStatus" style="font-family: 'Arial'; font-weight: 400; font-size: 14px; margin-top: 6px;"></label>
            </div>
        </div>
      
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Available for Verification</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12">
                        <input type="text" placeholder="Enter Barcode Number" class="form-control" id="txtBarcode" onkeypress="return EnterBarcode(event)" style="width: 50%;" />
                    </div>
                    <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                    </div>
                </div>
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>TRANSACTION TYPE</td>
                                <td>ITEM CODE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>SERIAL #</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

