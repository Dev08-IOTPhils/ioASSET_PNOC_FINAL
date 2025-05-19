<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Component.aspx.cs" Inherits="Files_Data_Component" %>

<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Component.js"></script>

    <style>
        /*.dataTables_scrollHead {
            overflow: visible !important;
            width: 100%;
        }*/
    </style>
    <script type="text/javascript" lang="Javascript">

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
        <h1>Component
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Component</li>
        </ol>
    </section>
    <br />
    <section class="content">

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Components</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblCompEntry" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>TRANSACTION #</td>
                                <td>PARENT PROPERTY #</td>
                                <td>PROPERTY STATUS</td>
                                <td>CREATED BY</td>
                                <td>DATE</td>
                                <td>TRANSACTION STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="compEntry">
                        </tbody>
                    </table>
                    <%--                                      <asp:Button ID="TestExcel" runat="server" type="submit" class="btn btn-med btn-primary" Text="Test Excel" OnClick="btnTest_Click" Style="width: 100%"></asp:Button>--%>
                </div>
            </div>
        </div>
        <div id="GoodsReceive" class="box box-solid box-primary" style="width: 100%;">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Details</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-2 col-md-1 col-sm-10 col-xs-12">
                            <input id="btnNew" class="btn btn-block btn-primary" type="button" value="Create New Transaction" onclick="NewT()" />
                        </div>
                        <div class="col-lg-2 col-md-4 col-sm-10 col-xs-12">
                            <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Select Property" onclick="SelectProperty()" />
                        </div>

                        <div class="col-lg-1 col-md-4 col-sm-10 col-xs-12">
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                            <label class="control-label">
                                Transaction Number:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtTrahsNum" type="text" placeholder="Auto Generated..." class="form-control" onkeypress="return EnterKey(event)" disabled />
                        </div>
                    </div>
                </div>

                <br />
                <div id="DEdit">
                    <%--//1--%>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                                <label class="control-label">
                                    Property #:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <label class="control-label" id="lpropertyno">
                                </label>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                                <label class="control-label">
                                    Serial #:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <label class="control-label" id="lserialno">
                                </label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <%--//2--%>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                                <label class="control-label">
                                    Description:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <label class="control-label" id="ldesc">
                                </label>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                                <label class="control-label">
                                    Category:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <label class="control-label" id="lcategory">
                                </label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                                <label class="control-label">
                                    Classification:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <label class="control-label" id="lclassification">
                                </label>
                            </div>

                            <%--<div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                                <label class="control-label">
                                    Category:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <label class="control-label" id="lcategory">
                                </label>
                            </div>--%>
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-9 col-md-10 col-sm-8 col-xs-4"></div>
                        <div class="col-lg-2 col-md-2 col-sm-8 col-xs-5" align="right">
                            <input id="btnAddComp" class="btn btn-block btn-primary" type="button" value="Add Components" onclick="AddToComponentEntry()" />
                        </div>

                        <div class="col-lg-1 col-md-2 col-sm-8 col-xs-5" align="right">
                            <input id="btnAddxx" class="btn btn-block btn-success" type="button" value="Save" onclick="CreateComponents()" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>

        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Components List</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblCompAdded" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>PROPERTY #</td>
                                <%--<td>DESCRIPTION</td>--%>
                                <td>LONG DESCRIPTION</td>
                                <td>SERIAL #</td>
                             <%--   <td>CLASSIFICATION</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>TAG</td>--%>
                                <td>UNIT OF MEASUREMENT</td>
                                <td>ACQUIRED COST</td>
                                <td>ACQUIRED DATE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="compadded">
                        </tbody>
                    </table>
                    <%--                                      <asp:Button ID="TestExcel" runat="server" type="submit" class="btn btn-med btn-primary" Text="Test Excel" OnClick="btnTest_Click" Style="width: 100%"></asp:Button>--%>
                </div>
            </div>
        </div>
    </section>
    <%--PROPERTY--%>

    <div id="SelectProperty" class="modal fade" tabindex="-1" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="ClearE()"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-plus"></span>Property Masterlist</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12  table-responsive">
                            <table id="tblSelectProperty" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <th >PROPERTY #</th>
                                        <th >DESCRIPTION</th>
                                        <th >SERIAL #</th>
                                        <th >CLASSIFICATION</th>
                                        <th >CATEGORY</th>
                                    </tr>
                                </thead>
                                <tbody id="selectproperty">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnAddProperty" onclick="addSelectProperty()">&nbsp;Confirm&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="ClearE()">&nbsp;Cancel&nbsp;</button>
                </div>
            </div>
        </div>
    </div>


    <div id="AddComponentEntry" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-plus"></span>Accessories Entry</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <input id="btnNe1w" class="btn btn-block btn-primary" type="button" value="Add Rows" onclick="QuantityKeyUpX()" />
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12"></div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12"></div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12  table-responsive">
                            <table id="tblPropertyAdded" role="grid" class="table table-bordered table-hover table-condensed " >
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td style="width:120px;">PROPERTY #</td>
                                        <%--<td style="width:120px;">DESCRIPTION</td>--%>
                                        <td style="width:120px;">LONG DESCRIPTION</td>
                                        <td style="width:120px;">SERIAL #</td>
                                  <%--      <td style="width:120px;">CLASSIFICATION</td>
                                        <td style="width:120px;">BRAND</td>
                                        <td style="width:120px;">MODEL</td>
                                        <td style="width:120px;">TAG</td>--%>
                                        <td style="width:120px;">UNIT OF MEASUREMENT</td>
                                        <td style="width:120px;">ACQUIRED COST</td>
                                        <td style="width:120px;">ACQUIRED DATE</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="propertyadded">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnAddProperty1" onclick="AddComponent()">&nbsp;Confirm&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="RevertRFIDNumbers()">&nbsp;Cancel&nbsp;</button>
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
            //Initialize Select2 Elements
            //$('.select2').select2({
            //    theme: "default"
            //});

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

