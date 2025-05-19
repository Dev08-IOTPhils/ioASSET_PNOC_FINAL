<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockIssuance.aspx.cs" Inherits="Operation_Module_StockIssuance" %>

<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/StockIssuance.js"></script>
    <script>
        function uploadsuccess() {
            swal("Successful", "Transaction Completed with Supporting Document!", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadsuccessW() {
            swal("Successful", "Transaction Completed without Supporting Document!", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadfailed(itemcode) {
            swal("Error", "Supply Issuance Transaction cannot be completed. \nIssued Quantity of Itemcode ('" + itemcode + "') is greater than the Balance Quantity.", "error");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadsuccessRequest() {
            swal("Successful", "Transaction Completed with Supporting Document!", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadsuccessRequestW() {
            swal("Successful", "Transaction Completed without Supporting Document!", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadfailedRequest(itemcode) {
            swal("Error", "Supply Request Transaction cannot be completed. \nRequest Quantity of Itemcode ('" + itemcode + "') is greater than the Balance Quantity.", "error");
            preventDefault();
            window.location.href = window.location.href;
        }

        function filenotseen() {
            swal("Error", "Please select a file to upload", "error");
            preventDefault();
            window.location.href = window.location.href;
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
                if (charCode > 31 && (charCode < 49 || charCode > 57)) {
                    Math.min(this.value, 1)
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=printablepage.ClientID %>');
            var prtwin = window.open('Stock Issuance Form', '', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataRIS() {
            var prtGrid = document.getElementById('<%=printablepageRIS.ClientID %>');
            var prtwin = window.open('Stock Issuance Form', '', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataITR() {
            var prtGrid = document.getElementById('<%=printablepageITR.ClientID %>');
            var prtwin = window.open('Stock Issuance Form', '', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();
            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridData1() {
            var prtGrid = document.getElementById('<%=printablepage1.ClientID %>');
            var prtwin = window.open('Stock Request Form', '', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supply Issuance
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Supply Issuance</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-lg-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Filter</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                            <div class="input-group margin">
                                <label class="control-label" align="left">
                                    Custodian Type:
                                </label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                            <select id="ddlCustodian" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                        </div>
                    </div>
                    <!-- /.box-body -->

                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>
                    Select Employee below and then on the "Supply Available for Issuance table" below, click the plus sign(+) beside the supply you want to issue. You may check current list of Supply Issuance requests by clicking the View Supply List for Issuance.</i></p>
                </center>
            </div>
        </div>
        <br />
        <div class="row" style="display: block">
            <div class="col-lg-1">
                <label class="control-label" align="left">
                    User Department:
                </label>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <div id="HTSIssuanceDept" class="input-group ">
                    <select id="ddlDepartmentSI" onchange="GetEmployeeSI();" class="form-control select2" data-dropdown-css-class="select2-primary"></select>
                    <span id="HTSIssuanceDeptC" class="btn btn-success input-group-addon" onclick="addDepartmentDialogBox('')"><i class="fa fa-plus"></i></span>
                </div>
            </div>
            <div class="col-lg-1">
                <label class="control-label" align="left">
                    Employee:
                </label>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <div id="HTSIssuanceEmp" class="input-group">
                    <select id="ddlEmployeeSI" onchange="GetApprover();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                    <span id="HTSIssuanceEmpC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('StockIssuance')"><i class="fa fa-plus"></i></span>
                </div>
            </div>
            <div class="col-lg-3"></div>
            <div class="col-lg-3 col-md-5 col-sm-12 col-xs-12" align="right">
                <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="IssuanceList()">
                    <i class="glyphicon glyphicon-list"></i>
                    <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View Supply List for Issuance:&nbsp;0</span>
                </a>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary" hidden>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Assigned Supplies from Goods Receive</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body" hidden>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblAssignedStockList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>QUANTITY</td>
                                <td>DESCRIPTION</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="assignedstock">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Supply Available for Issuance</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblStockList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>QUANTITY</td>
                                <td>DESCRIPTION</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td><asp:Label ID="lblMasterListSite" runat="server" Text="Label"></asp:Label></td>
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
        <div class="box box-solid box-primary" id="divPending">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Supply Issuance Pending Transaction</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>
                    Pending Supply Issuance Request(to issue) for employees can be seen here. </i></p>
                </center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ISSUANCE #</td>
                                <td>ISSUED TO</td>
                                <td>SITE/DEPARTMENT</td>
                                <td>REMARKS</td>
                                <td>DATE</td>
                                <td>APPROVER</td>
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
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Supply Request Pending Transaction</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>
                    Pending Supply Request(to borrow) by employees can be seen here. </i></p>
                </center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblRequestPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>REQUEST #</td>
                                <td>REQUESTED BY</td>
                                <td>SITE/DEPARTMENT</td>
                                <td>REMARKS</td>
                                <td>DATE</td>
                                <td>APPROVER</td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pendinguploadrequestlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="row" id="printablepage" runat="server" hidden>
            <div class="row" id="divStockIssuanceReport" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageRIS" runat="server" hidden>
            <div class="row" id="divStockIssuanceReportRIS" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageITR" runat="server" hidden>
            <div class="row" id="divStockIssuanceReportITR" runat="server">
            </div>
        </div>

        <div class="row" id="printablepage1" runat="server" hidden>
            <div class="row" id="divStockRequestReport" runat="server">
            </div>
        </div>
    </section>

    <div id="addToListStock" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>Issue Supply Asset</p>
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
                            Issue Quantity:
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtIssueQuantity" type="text" style="width: 100%" class="form-control" onkeypress="return onlyNos(event,this)" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnIssueStock" onclick="IssueStockAsset(this.id)">&nbsp;&nbsp;Add to Issuance List&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="IssuanceList" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;"><span class="fa fa-edit"></span>List of Supplies for Issuance</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <p style="font-size: 12px;">
                                <i>By clicking the confirm button, the selected Supply/Supplies will be sent for Supply Issuance for the selected Employee. Make sure that the list is correct.</i>
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <textarea class="form-control" id="txtRemarks" runat="server" title="Remarks"></textarea>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12">
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12" id="labelApprover">
                            <label class="control-label">
                                Approver:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="dropdownApprover">
                            <asp:DropDownList ID="ddlApprover" class="form-control select2" runat="server" Style="width: 100%; height: 35px" ToolTip="Approver"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblIssuanceList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>QUANTITY</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="issuancelist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmStockIssuance()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToIssuance()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Stock Details --%>
    <div id="StockDetailsPendingUpload" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Supplies</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtStockIssuanceNo" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Issuance No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockIssuanceNo" type="text" />
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
                            <table id="tblPUL1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                <tbody id="pul1">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-3">
                        <div class="btn-group pull-left">
                            <button type="button" class="btn btn-success pull-left">Print</button>
                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu" id="printGrid">
                                <li><a style="cursor: pointer" onclick="PrintGridDataRIS()">Withdrawal Slip for Common-Used Office Supplies and Forms</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataITR()" class="hide">Inventory Transfer Report Form</a></li>
                                <li><a id="stockrisbtn" style="cursor: pointer" class="hide">Requisition and Issue Slip Form With Certificate</a></li>
                                <li><a id="stockitrbtn" style="cursor: pointer" class="hide">Inventory Transfer Report Form With Certificate</a></li>
                                <li><a id="risbtnword" style="cursor: pointer" class="hide">Requisition and Issue Slip Form (Word)</a></li>
                                <li><a id="itrbtnword" style="cursor: pointer" class="hide">Inventory Transfer Report Form (Word)</a></li>
                                <li><a id="risxcl" style="cursor: pointer">Withdrawal Slip for Common-Used Office Supplies and Forms (Excel)</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9" id="showorhide">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="fuDocuments" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>
                        <div class="col-lg-3">
                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnPrint2" onclick="UploadStockIssuance()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Stock Details --%>
    <div id="StockRequestDetailsPendingUpload" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Supplies</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtStockRequestNo" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Request No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockRequestNo" type="text" />
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
                            <table id="tblPURL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>REQUESTED QUANTITY</td>
                                        <td>ISSUE QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="purl">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <div class="col-lg-3">
                        <div class="btn-group pull-left">
                            <button type="button" class="btn btn-success pull-left">Print</button>
                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <li><a style="cursor: pointer" onclick="PrintGridData1()">Withdrawal Slip for Common-Used Office Supplies and Forms</a></li>
                                <li><a id="stockrequestrisbtn" style="cursor: pointer" class="hide">Requisition and Issue Slip With Certificate</a></li>
                                <li><a id="risxclR" style="cursor: pointer">Withdrawal Slip for Common-Used Office Supplies and Forms (Excel)</a></li>
                                <li><a style="cursor: pointer" id="risbtnstock" class="hide">Requisition and Issue Slip Form (Word)</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="showorhide2">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnCompleteRequest2" onclick="CompleteRequestStock()">&nbsp;&nbsp;Confirm Issued Quantity&nbsp;&nbsp;</button>
                    </div>

                    <div class="col-lg-9" id="showorhide1">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="fuDocumentsRequest" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>   

                        <div class="col-lg-3">
                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnCompleteRequest" onclick="UploadIssuanceRequest()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
    <!-- RIS Signature and Certificate -->

    <div id="RISsigandCertModal" class="modal fade" tabindex="-1" role="dialog">
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
                                    <td colspan="2">Requested By/Recieved By</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="requestedsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="requestedpkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="requestedpkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="requesteddatestamp" /><label for="requesteddatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td>Approved By</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="approvedsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="approvedpkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="approvedpkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="approveddatestamp" /><label for="approveddatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row" style="margin: 5px;">
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td>Issued By</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="issuedsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="issuedpkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="issuedpkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="issueddatestamp" /><label for="issueddatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Download" id="btndownloadrispdf" transno="" transtype="" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- RIS Signature and Certificate -->
    <!-- ITR Signature and Certificate -->
    <div id="ITRsigandCertModal" class="modal fade" tabindex="-1" role="dialog">
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
                                    <td colspan="2">Approved By</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="itrapprovedsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="itrapprovedpkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="itrapprovedpkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="itrapproveddatestamp" /><label for="itrapproveddatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td colspan="2">Released/Issued by</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="itrreleasedsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="itrreleasedpkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="itrreleasedpkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="itrreleaseddatestamp" /><label for="itrreleaseddatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row" style="margin: 5px;">
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td>Received By</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Signature<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="itrreceivedsignatureupload" class="form-control" accept=".png, .png|image/*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Pass Phrase</td>
                                    <td style="width: 70%;">
                                        <input type="password" id="itrreceivedpkipassword" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">Certificate<br />
                                        (Max: 2mb)</td>
                                    <td style="width: 70%;">
                                        <input type="file" id="itrreceivedpkicertificateupload" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: center;">&nbsp;</td>
                                    <td style="width: 70%;">
                                        <input type="checkbox" class="form-check-input" id="itrreceiveddatestamp" /><label for="itrreceiveddatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Download" id="btndownloaditrpdf" transno="" transtype="" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- ITR Signature and Certificate -->
    <cert:cert id="cert" runat="server" />
    </label>
    </label>
</asp:Content>
