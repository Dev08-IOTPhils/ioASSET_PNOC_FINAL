<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReceive.aspx.cs" Inherits="Operation_Module_GoodsReceive" %>

<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/GoodsReceive.js"></script>
    <script src="../Script/GoodsReceiveStock.js"></script>
    <script src="../Script/TestPrint.js"></script>
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

        function uploadSuccess() {
            swal("Successful", "Transaction Completed with Supporting Document!", "success");
            preventDefault();
        }

        function uploadSuccessW() {
            swal("Successful", "Transaction Completed without Supporting Document!", "success");
            preventDefault();
        }
        function filenotseen() {
            swal("Warning", "Please select a file to upload.", "warning");
            preventDefault();
        }
        function formatCurrency(input) {
            // Remove all characters except for digits and decimals
            let value = input.value.replace(/[^\d\.]/g, '');

            // Ensure there is only one decimal point
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.substring(0, value.lastIndexOf('.'));
            }

            // Format the value with commas for thousands separators
            input.value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
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
                if (charCode == 13) {
                    return false;
                    preventDefault();
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            if (charCode == 13) {
                return false;
                charCode.preventDefault();
            }
            return true;
        }

        function isNumberKey1(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtAcquiredCost").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            if (charCode == 13) {
                return false;
                charCode.preventDefault();
            }
            return true;
        }

        function isNumberKey2(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtSalePrice").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            if (charCode == 13) {
                return false;
                charCode.preventDefault();
            }
            return true;
        }

        function isNumberKey3(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtStockAcquiredCost").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            if (charCode == 13) {
                return false;
                charCode.preventDefault();
            }
            return true;
        }
    </script>
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

        .hasError {
            border-color: red;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">

    <section class="content-header">
        <h1>Goods Receive
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Goods Receive</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div id="GoodsReceive" class="box box-solid box-primary" style="width: 100%;">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Goods Receive Details</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <label class="control-label" align="left">
                        &emsp;<i><span style="color: red">* Required fields.</span>  </i>
                    </label>
                </div>
                <%--//1--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                <span style="color: red">*</span>PR Number:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtPRNumber" type="text" <%-- placeholder="PR Number"--%> class="form-control" onkeypress="return EnterKey(event)" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                Date of Acquisition:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDOA" class="form-control" onkeypress="return EnterKey(event)" readonly="readonly" />
                        </div>
                    </div>
                </div>
                <br />
                <%--//2--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                <span style="color: red">*</span> PO Number:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtPONumber" type="text" <%-- placeholder="PO Number" --%>class="form-control" onkeypress="return EnterKey(event)" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                <span style="color: red">*</span> Custodian Type:
                            </label>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <asp:DropDownList ID="ddlCustodian" class="select2" onchange="GetUserByCustodian();" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <%--//3--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Invoice Number:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtInvoiceNumber" type="text" <%--placeholder="Invoice Number"--%> class="form-control" onkeypress="return EnterKey(event)" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                <span style="color: red">*</span> Received By:
                            </label>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">

                            <div id="hidereceivedby" class="input-group">
                                <select id="ddlReceivedBy" class="form-control select2" onchange="GetApproverByCustodian();" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HideThisReceivedBy" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('GRCustodian')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--//4--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                DR Number:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtDRNumber" <%--placeholder="DR Number"--%> type="text" class="form-control" onkeypress="return EnterKey(event)" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                Assigned Person:
                            </label>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">

                            <div id="hideemployee" class="input-group">
                                <select id="ddlEmployee" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HideThisEmployee" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('GR')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--//5--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                <span style="color: red">*</span> Supplier:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <div id="hidesupplier" class="input-group">
                                <select id="ddlSupplier" class="form-control select2" onchange="GetLocation();" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HideThisSupplier" class="btn btn-success input-group-addon" onclick="addSupplierDialogBox()"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                <span style="color: red">*</span><asp:Label ID="lbldisplaySite1" runat="server" Text="Label"></asp:Label>/Department:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <div id="hideregion" class="input-group">
                                <select id="ddlFromSite" class="form-control select2" onchange="GetLocation();" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HideThisRegion" class="btn btn-success input-group-addon" onclick="addWarehouseDialogBox('GR')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--//6--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                <span style="color: red">*</span> Remarks:
                            </label>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <textarea class="form-control" id="txtRemarks" runat="server"></textarea><%-- placeholder="Remarks"--%>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                <span style="color: red">*</span><asp:Label ID="lbldisplayLoc" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <div id="hidebranch" class="input-group">
                                <select id="ddlLocation1" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HideThisBranch" class="btn btn-success input-group-addon" onclick="addLocationDialogBox()"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--//7--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                            </label>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0" id="lblApprover">
                            <label class="control-label">
                                <span style="color: red">*</span> Approver:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="dropdownApprover">

                            <div id="hideapprover" class="input-group">
                                <select id="ddlApprover" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HideThisApprover" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('GRApprover')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-9 col-md-7 col-sm-6 col-xs-12" align="right"></div>
            <div class="col-lg-3 col-md-5 col-sm-6 col-xs-12" align="right">
                <br />
                <a id="btnPL" class="btn btn-block btn-dict-pref pull-left" onclick="PropertyAddedList();">
                    <i class="glyphicon glyphicon-list"></i>
                    <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View List for Goods Receive:&nbsp;0</span>
                </a>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <!-- Custom Tabs -->
                <div class="nav-tabs-custom box-primary">
                    <ul class="nav nav-tabs">
                        <li class="active" id="propertyTab"><a href="#tab_1" data-toggle="tab">Property</a></li>
                        <li id="stockTab"><a href="#tab_2" data-toggle="tab">Supply</a></li>
                    </ul>
                    <div class="tab-content">
                        <!-- goods receive property-->
                        <div class="tab-pane active" id="tab_1">
                            <div class="col-lg-12">
                                <center><p style="font-size:12px;"><i>
                                    Once you have above details filled up, please select or enter existing item code in Search Box Below or click the Plus sign (+) to select Item.</i>
                                </p></center>
                            </div>
                            <br />
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">Property Item Code List</h3></center>
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
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <table id="tblProperty" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
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
                                            <tbody id="property">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- goods receive stock -->
                        <div class="tab-pane" id="tab_2">
                            <div class="col-lg-12">
                                <center><p style="font-size:12px;"><i>
                                    Once you have above details filled up, please select or enter existing item code in Search Box Below or click the Plus sign (+) to select Item.
                                </i></p>
                                    </center>
                            </div>
                            <br />
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">Supply Item Code List</h3></center>
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
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <table id="tblStock" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>ITEM CODE</td>
                                                    <td>DESCRIPTION</td>
                                                    <td>CLASSIFICATION</td>
                                                    <td>CATEGORY</td>
                                                    <td>BRAND</td>
                                                    <td>MODEL</td>
                                                    <%--<td>ACTIVE</td>--%>
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

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Goods Received</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>
                                   Below table shows your pending List of Goods Received, subject to approval by selected personnel in Goods Receive Details above.
                        </i></p> </center>
                </div>
                <br />
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                    </div>
                </div>
                <div class="col-lg-12">
                    <table id="tblGoodsReceive" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>GOODS RECEIVE #</td>
                                <td>RECEIVED BY</td>
                                <td>CREATED BY</td>
                                <td>DATE</td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="goodsreceive">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <%--PROPERTY--%>

    <div id="addToPropertyGoodsReceive" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-plus"></span>Receive Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                   Please enter below details of property to be received as appropriate.
                                </i></p>
                             </center>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            Item Code:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label id="lblitemcode">Item Code</label>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12"></div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            Quantity:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <input id="txtPropertyQuantity" type='number' min='0' style="width: 100%" placeholder="Quantity" class="form-control" onkeypress="return onlyNos(event,this); " onkeyup="QuantityKeyUp();" onchange="QuantityKeyUp();" ondrop="return false;" onpaste="return false;" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            Acquired Cost:
                            <br />
                            (Net of Value)
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <input id="txtAcquiredCost" type="text" style="width: 100%" class="form-control" placeholder="Unit Cost" onkeyup="return formatCurrency(this);" ondrop="return false;" onpaste="return false;" />

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12"></div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <input id="txtSalePrice" type="hidden" style="width: 100%" class="form-control" placeholder="Sale Price" onkeypress="return isNumberKey2(event,this);" ondrop="return false;" onpaste="return false;" value="0" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12"></div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12  table-responsive">
                            <table id="tblPropertyAdded" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <th>PROPERTY #</th>
                                        <th>SERIAL #</th>
                                        <th>LONG DESCRIPTION</th>
                                        <th>WARRANTY DATE</th>
                                        <th>INSURANCE DATE</th>
                                        <th>EXPIRY DATE</th>
                                        <th title="You may enter custom description to be printed on label aside from property number. However, please make sure that it will fit the intended label for the chosen property.">LABEL TO PRINT</th>
                                        <th>RFID NUMBER</th>
                                        <th>BARCODE</th>
                                        <th>ACTION</th>
                                    </tr>
                                </thead>
                                <tbody id="propertyadded">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnAddProperty" onclick="AddToPropertyGoodsReceive()">&nbsp;&nbsp;Add&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="RevertRFIDNumbers()">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="viewAddedPropertyList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Goods Receive Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the confirm button, the selected property/ies will be sent as a goods receive for approval to your selected approver.
                           The approver will be notified with your request. After the approver's reply you too will be receiving  a notification. Make sure that the list is correct.
                            </i></p></center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblPropertyAddedList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>TYPE</td>
                                        <td>ITEM CODE</td>
                                        <td>PR NUMBER</td>
                                        <td>PO NUMBER</td>
                                        <td>INVOICE NUMBER</td>
                                        <td>DR NUMBER</td>
                                        <td>REMARKS</td>
                                        <td>QUANTITY</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="propertyaddedlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirm" onclick="ConfirmGoodsReceive()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" data-toggle='tooltip' title='Cancel this whole list and start over again.' onclick="RemoveAllToGoodsReceiveProperty()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="viewGRDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Goods Receive Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtGoodsReceiveID" runat="server" type="text" class="form-control" Style="width: 100%" />
                        <asp:TextBox ID="txtGoodsReceiveNumber" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblGRDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>TYPE</td>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL NUMBER</td>
                                        <td>PR NUMBER</td>
                                        <td>PO NUMBER</td>
                                        <td>INVOICE NUMBER</td>
                                        <td>DR NUMBER</td>
                                        <td>REMARKS</td>
                                        <td>QUANTITY</td>
                                        <td>UNIT COST</td>
                                        <td>TOTAL COST</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="grdetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="completetransaction">
                    <div class="btn-group pull-left" id="idPrint">
                        <button type="button" class="btn btn-success pull-left">Print</button>
                        <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                            <span class="caret"></span>
                            <span class="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul class="dropdown-menu" role="menu" id="printGrid">
<%--                            <li><a onclick="PrintGridData()" style="cursor: pointer">Inspection and Acceptance Form</a></li>--%>
                            <li><a id="iarbtn" style="cursor: pointer; display: none;">Inspection and Acceptance Form With Certificate</a></li>
                            <li><a id="iarbtnxcl" style="cursor: pointer;">Inspection and Acceptance Form (Excel)</a></li>
                            <li hidden><a id="iarbtnv2" style="cursor: pointer">Inspection and Acceptance Form With Certificate</a></li>
                        </ul>
                    </div>
                    <button type="button" class="btn btn-sm btn-primary pull-right" id="btncomplete" onclick="btnCompleteGR()" style="margin">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                    <asp:FileUpload ID="GRupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 30%" />
                </div>
            </div>
        </div>
    </div>

    <div id="viewPropertyDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Goods Receive Property Details</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblPropertyDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>SERIAL #</td>
                                        <td>DESCRIPTION</td>
                                        <td>EXPIRY DATE</td>
                                    </tr>
                                </thead>
                                <tbody id="propertydetails">
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

    <%--STOCK--%>

    <div id="addToStockGoodsReceive" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-plus"></span>Receive Stock</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                            Item Code:
                        </div>
                        <div class="col-lg-8 col-md-12 col-sm-12 col-xs-12">
                            <input id="txtStockItemCode" disabled="true" type="text" style="width: 100%" class="form-control" />
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                            Quantity:
                        </div>
                        <div class="col-lg-8 col-md-12 col-sm-12 col-xs-12">
                            <input id="txtStockQuantity" type="text" style="width: 100%" class="form-control" onkeypress="return onlyNos(event,this)" ondrop="return false;" onpaste="return false;" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                            Unit Cost:
                        </div>
                        <div class="col-lg-8 col-md-12 col-sm-12 col-xs-12">
                            <input id="txtStockAcquiredCost" type="text" style="width: 100%" class="form-control" onkeypress="return isNumberKey3(event,this)" ondrop="return false;" onpaste="return false;" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnAddStock" onclick="AddToStockGoodsReceive()">&nbsp;&nbsp;Add&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divGR" runat="server">
        </div>
    </div>
    <div id="viewAddedStockList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Goods Receive Supply List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the confirm button, the selected Supply/Supplies will be sent as a goods receive for approval to your selected approver.
                           The approver will be notified with your request. After the approver's reply you too will be receiving  a notification. Make sure that the list is correct.
                            </i></p></center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblStockAddedList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>PR NUMBER</td>
                                        <td>PO NUMBER</td>
                                        <td>INVOICE NUMBER</td>
                                        <td>DR NUMBER</td>
                                        <td>REMARKS</td>
                                        <td>QUANTITY</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="stockaddedlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="RemoveAllToGoodsReceiveStock()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnConfirmStock" onclick="ConfirmGoodsReceiveStock()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <cert:cert id="cert" runat="server" />
    <!-- Signature and Certificate -->

    <div id="sigandCertModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">
                        <p>
                            <label style="color: white;">Certificate and Signature</label>
                        </p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="margin: 5px;">
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td>
                                        <label id="lbl_from">Inspection</label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="fromsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="frompkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="frompkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="fromdatestamp" /><label for="fromdatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td>
                                        <label id="lbl_by">Acceptance</label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="bysignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="bypkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="bypkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="bydatestamp" /><label for="bydatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Download" id="btndownloadpdf" transno="" transtype="" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Signature and Certificate -->
    <script>
        $(document).ready(function () {
            $('.js-example-basic-single').select2();
        });

        $(function () {

            //Datemask dd/mm/yyyy
            $('#datemask').inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' })
            //Datemask2 mm/dd/yyyy
            $('#datemask2').inputmask('mm/dd/yyyy', { 'placeholder': 'mm/dd/yyyy' })
            //Money Euro
            $('[data-mask]').inputmask()

            //Date range picker
            $('#reservation').daterangepicker()
            //Date range picker with time picker
            $('#reservationtime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'MM/DD/YYYY h:mm A' })
            //Date range as a button
            $('#daterange-btn').daterangepicker(
              {
                  ranges: {
                      'Today': [moment(), moment()],
                      'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                      'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                      'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                      'This Month': [moment().startOf('month'), moment().endOf('month')],
                      'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                  },
                  startDate: moment().subtract(29, 'days'),
                  endDate: moment()
              },
              function (start, end) {
                  $('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
              }
            )
        })
    </script>
</asp:Content>
