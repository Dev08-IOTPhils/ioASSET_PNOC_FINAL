<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockReturn.aspx.cs" Inherits="Operation_Module_StockReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/StockReturn.js"></script>
    <script>
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function uploadsuccess() {
            swal("Success", "Supply Return Transaction Completed!", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadfailed(itemcode) {
            swal("Warning", "Supply Return Transaction cannot be completed. \n\nReturn Quantity of Itemcode ('" + itemcode + "') is greater than the Balance Quantity.", "warning");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadSelect() {
            swal("Warning", "Please select a file to upload.", "warning");
            preventDefault();
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supply Return
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Supply Return</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>Select employee below to see which issued supplies are available for return.</i>
                </p></center>
            </div>
        </div>

        <div class="row" id="cartwheel" style="display: block">
            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                <label class="control-label">
                    Assigned Employee:
                </label>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <div id="HTSReturnEmployee" class="input-group">
                    <select id="ddlEmployeeSR"  onchange="load_data();"  class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                    <span id="HTSReturnEmployeeC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('StockReturn')"><i class="fa fa-plus"></i></span>
                </div>
            </div>
            <div class="col-lg-4 col-md-1 col-sm-12 col-xs-12" align="right">

            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12" align="right">
                <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="ReturnList()">
                    <i class="glyphicon glyphicon-list"></i>
                    <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View Supply List for Return:&nbsp;0</span>
                </a>
            </div>
        </div>

        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Supplies Available for Return</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-responsive table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td >ITEM CODE</td>
                                <td >QUANTITY</td>
                                <td >DESCRIPTION</td>
                                <td><asp:Label ID="lblMasterListSite" runat="server" Text="Label"></asp:Label></td>
                                <td >ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="assetlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Supply Return Pending Upload</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>All pending returned supplies for receiving/uploading of documents can be seen here. Press the view icon below to see details.</i>
                    </p></center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>RETURN #</td>
                                <td>EMPLOYEE NAME</td>
                                <td>REMARKS</td>
                                <td>DATE</td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pendinguploadlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="row" id="printablepage" runat="server" hidden>
            <div class="row" id="divTurnIn" runat="server">
            </div>
        </div>
    </section>

    <div id="ReturnStockList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4 class="modal-title">
                        <p  style="color:white;"><span class="fa fa-edit"></span>List of Supplies for Return</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                          <center><p style="font-size:12px;"><i>
                                By clicking the confirm button, the selected supply/supplies will be sent for Supply Return. Make sure that the list is correct.</i>
                            </p></center>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <textarea class="form-control" id="txtRemarks" runat="server"></textarea>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblReturnList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td >ITEM CODE</td>
                                        <td >DESCRIPTION</td>
                                        <td >QUANTITY</td>
                                        <td >ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="returnlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmStockReturn()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToStockReturn()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="UploadDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4 class="modal-title">
                        <p  style="color:white;">Upload Return Slip</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="deleteBrand" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Cancel Supply Return</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to cancel this transaction?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnCanceltrans" onclick="DeleteBrand(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="AddToReturnModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p  style="color:white;"><span class="fa fa-edit"></span>Add To Supply Return</p>
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
                            <input id="txtQuantity" disabled="true" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            Return Quantity:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtReturnQuantity" type="text" style="width: 100%" class="form-control" onkeypress="return onlyNos(event,this)" />
                        </div>
                    </div>



                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnMergeStock" onclick="TransferToReturn()">&nbsp;&nbsp;Add To Return&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>


    <%-- Stock Details --%>
    <div id="StockReturnDetailsPendingUpload" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p  style="color:white;">List of Supplies</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtStockReturnNo" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>Supplies will not be reflected in inventory until supporting document is uploaded or you may opt to press complete transaction without uploading supporting document/s.</i>
                            </p></center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Return No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockReturnNo" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPURL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="purl">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete" onclick="StockReturnComplete()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        <asp:FileUpload ID="fuDocuments" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width:30%;" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>

