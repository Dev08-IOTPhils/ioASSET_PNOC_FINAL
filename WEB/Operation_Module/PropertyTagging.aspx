<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyTagging.aspx.cs" Inherits="Operation_Module_PropertyTagging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyTagging.js"></script>
    <script type="text/javascript" lang="Javascript">
        function PrintGridData() {
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
    <style>
        table.dataTable tbody td {
            word-break: break-word;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Tagging/Re-tagging
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Tagging</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Filter</h3>
            </div>
            <div class="box-body">
                <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                    <div class="input-group margin">
                        <label class="control-label" align="left">
                            Tag:
                        </label>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-9">
                    <asp:DropDownList ID="ddlTagType" class="form-control select2" runat="server" onchange="ddlTagTypeChange();" Style="width: 100%; height: 35px"></asp:DropDownList>
                </div>

                <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                    <div class="input-group margin">
                        <label class="control-label" align="left">
                            Tag Type:
                        </label>
                    </div>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-9">
                    <asp:DropDownList ID="ddlTaggingOption" class="form-control select2" runat="server" onchange="ddlTagTypeChange();" Style="width: 100%; height: 35px"></asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="box box-primary" id="ReaderConnection" style="display: none">
            <div class="box-header with-border">
                <h3 class="box-title">Writer Connection</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <asp:DropDownList ID="ddlCom" class="form-control select2" runat="server" Style="width: 100%; height: 35px" onchange="GetDeviceWriterStatus()"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label id="lblWriterStatus" style="font-family: 'Arial'; font-weight: 400; font-size: 14px; margin-top: 6px;">STATUS:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <label id="lblDeviceWriterStatus" style="font-family: 'Arial'; font-weight: 400; font-size: 14px; margin-top: 6px;"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary" id="PrinterConnection" style="display: block">
            <div class="box-header with-border">
                <h3 class="box-title">Device Connection</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                            <div class="input-group margin">
                                <label class="control-label" align="left">
                                    Device:
                                </label>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <div id="HTPTagging" class="input-group">
                                <select id="ddlPrinter" class="form-control select2" onchange="GetDeviceStatus()" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTaggingC" class="btn btn-success input-group-addon" onclick="addDeviceDialogBox('PTagging')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label id="lblPrinterStatus" style="font-family: 'Arial'; font-weight: 400; font-size: 14px; margin-top: 6px;">STATUS:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <label id="lblDeviceStatus" style="font-family: 'Arial'; font-weight: 400; font-size: 14px; margin-top: 6px;"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="cartwheel" style="display: block">
            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12" align="right"></div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12" align="right">
                <br />
                <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="PrintList()">
                    <i class="glyphicon glyphicon-list"></i>
                    <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View List for Tagging/Re-tagging:&nbsp;0</span>
                </a>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <!-- Custom Tabs -->
                <div class="nav-tabs-custom box-primary">
                    <ul class="nav nav-tabs">
                        <li class="active" id="GRTab"><a href="#tab_GR" data-toggle="tab">Goods Receive</a></li>
                        <li id="propertyTab"><a href="#tab_1" data-toggle="tab">Property</a></li>
                        <li id="stockTab"><a href="#tab_2" data-toggle="tab">Supply</a></li>
                    </ul>
                    <div class="tab-content">
                        <!-- goods receive -->
                        <div class="tab-pane active" id="tab_GR">
                            <br />
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Goods Receive for Tagging</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblGR" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>GOODS RECEIVE #</td>
                                                    <td>PO NUMBER</td>
                                                    <td>PR NUMBER</td>
                                                    <td>INVOICE NUMBER</td>
                                                    <td>DR NUMBER</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="GR">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- goods receive property -->
                        <div class="tab-pane" id="tab_1">
                            <br />
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Property for Tagging</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblAssets" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed; text-overflow: unset; white-space: pre-line;">
                                            <thead style="font-weight: bold;">
                                                <tr>
                                                    <td>PROPERTY NO</td>
                                                    <td>ITEM CODE</td>
                                                    <td>DESCRIPTION</td>
                                                    <td>SERIAL #</td>
                                                    <td>CLASSIFICATION</td>
                                                    <td>CATEGORY</td>
                                                    <td>BRAND</td>
                                                    <td>MODEL</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="assetlist">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- goods receive stock -->
                        <div class="tab-pane" id="tab_2">
                            <br />
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply for Tagging</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblStock" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>ITEM CODE</td>
                                                    <td>DESCRIPTION</td>
                                                    <td>CLASSIFICATION</td>
                                                    <td>CATEGORY</td>
                                                    <td>BRAND</td>
                                                    <td>MODEL</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="stock">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.tab-pane -->
                    </div>
                    <!-- /.tab-content -->
                </div>
                <!-- nav-tabs-custom -->
            </div>
            <!-- /.col -->
        </div>
        <div class="row" id="RetaggingTable">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="box box-solid box-primary">
                    <div class="box-header with-border" data-widget="collapse">
                        <center><h3 class="box-title">List of Property for Printing/Writing</h3></center>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <center><p style="font-size:12px;"><i>By clicking the confirm button the selected properties will be sent automatically to the selected printer. Please make sure that the list is correct and the printer is on and connected.
                                    </i></p></center>
                    </div>
                    <div class="box-body">
                        <div class="col-lg-12">
                            <table id="tblListPropertyPrintingWriting" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>TYPE</td>
                                        <td>PROPERTY NO</td>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="printprintingwriting">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="box box-solid box-primary">
                    <div class="box-header with-border" data-widget="collapse">
                        <center><h3 class="box-title">Print/Write Jobs</h3></center>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-lg-12">
                            <table id="tblPrintJobs" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>DEVICE NAME</td>
                                        <td>STATUS</td>
                                    </tr>
                                </thead>
                                <tbody id="printjobs">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

    <div id="QuantityToPrint" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;"><span class="fa fa-edit"></span>Quantity to Print</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Item Code:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtItemcode" disabled="true" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Quantity:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtQuantity" type="number" min="0" style="width: 100%" class="form-control" />
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnMergeStock" onclick="addtoprintstock()">&nbsp;&nbsp;Add To Print List&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="EnrollAsset" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-edit"></span>Tag Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to tag this Property?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary pull-left" id="btnDeleteBrand" onclick="Enroll(this.id)">&nbsp;&nbsp;Write Tag&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="spin" style="display: none">
        <center style="position: absolute; top: 50%; left: 50%; transform: translateX(-50%) translateY(-50%); background: #222; opacity: 0.7; z-index: 1000; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center;">
            <h1>
                <div id="please-wait">Please wait</div>
                <br/>
                <span class="fa fa-gear fa-spin"></span>
            </h1>
        </center>
    </div>

    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divReportTagging" runat="server">
        </div>
    </div>

    <div id="PrintList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;"><span class="fa fa-edit"></span>List of Property for Printing/Writing</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the confirm button, the selected property/ies will be sent automatically to the selected printer. 
                                Please make sure that the list is correct and the printer is on and connected.
                            </i></p></center>
                        </div>

                        <div class="row" id="divForRetagging" style="display: none">
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                                <label class="control-label">
                                    Remarks:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <textarea class="form-control" id="txtRemarks" runat="server" title="Remarks"></textarea>
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12" id="labelApprover">
                                <label class="control-label">
                                    Approver:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="dropdownApprover">
                                <asp:DropDownList ID="ddlApprover" class="form-control select2" runat="server" Style="width: 100%; height: 35px" ToolTip="Approver"></asp:DropDownList>
                            </div>
                            <br />
                        </div>


                        <div class="col-lg-12">
                            <table id="tblPrintList" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>TYPE</td>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>PROPERTY NUMBER</td>
                                        <td>SERIAL NUMBER</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="printlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmPrint()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToPrint()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="modalPropertyInfo" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="ModalProp">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="col-lg-12">
                        <table id="tblGRProperty" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto">
                            <thead style="font-weight: bold">
                                <tr>
                                    <td>PROPERTY NUMBER</td>
                                    <td>ITEM CODE</td>
                                    <td>DESCRIPTION</td>
                                    <td>SERIAL NUMBER</td>
                                    <td>CLASSIFICATION</td>
                                    <td>CATEGORY</td>
                                    <td>BRAND</td>
                                    <td>MODEL</td>
                                </tr>
                            </thead>
                            <tbody id="GRProperty">
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

