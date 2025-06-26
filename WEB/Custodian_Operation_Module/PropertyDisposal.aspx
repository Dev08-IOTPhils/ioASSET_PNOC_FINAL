<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyDisposal.aspx.cs" Inherits="Operation_Module_PropertyDisposal" %>

<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyDisposal.js"></script>
    <script>
        function uploadSuccess() {
            swal("Successful", "Transaction Completed with Supporting Document!", "success");
            prevent.Default();
            window.location.href = window.location.href;
        }

        function uploadSuccessW() {
            swal("Successful", "Transaction Completed without Supporting Document!", "success");
            prevent.Default();
            window.location.href = window.location.href;
        }

        function filenotseen() {
            swal("Error", "Please select a file to upload", "error");
            preventDefault();
            window.location.href = window.location.href;
        }
        function PrintGridDataWMR() {
            var prtGrid = document.getElementById('<%=printablepageWMR.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataWMR1() {
            var prtGrid = document.getElementById('<%=printablepageWMR1.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
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
        function PrintGridData1() {
            var prtGrid = document.getElementById('<%=printablepage1.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataIIRUP() {
            var prtGrid = document.getElementById('<%=printablepageIIRUP.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataIIRUP1() {
            var prtGrid = document.getElementById('<%=printablepageIIRUP1.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataIIRSEP() {
            var prtGrid = document.getElementById('<%=printablepageIIRSEP.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();
            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataIIRSEP1() {
            var prtGrid = document.getElementById('<%=printablepageIIRSEP1.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();
            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataROP() {
            var prtGrid = document.getElementById('<%=printablepageROP.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();
            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataROP1() {
            var prtGrid = document.getElementById('<%=printablepageROP1.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();
            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataROSEP() {
            var prtGrid = document.getElementById('<%=printablepageROSEP.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataROSEP1() {
            var prtGrid = document.getElementById('<%=printablepageROSEP1.ClientID %>');
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Disposal
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Property Disposal</li>
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
                    <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                        <div class="input-group margin">
                            <label class="control-label" align="left">
                                Custodian Type:
                            </label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                            <select id="ddlCustodian" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                        </div>
                        <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                            <label class="select2-primary" align="left">
                                Property Type Cost:
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                            <asp:DropDownList ID="ddlItem" class="form-control select2" runat="server" onchange="load_data();" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="display: block">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

            </div>
            <div class="col-lg-3 col-md-1 col-sm-12 col-xs-12" align="right">
            </div>

            <div class="col-lg-3 col-md-5 col-sm-3 col-xs-12" align="right">
                <div class="input-group margin">
                    <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="DisposalList()">
                        <i class="glyphicon glyphicon-list"></i>
                        <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View Property List for Disposal:&nbsp;0</span>
                    </a>
                </div>
            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center>
                    <h3 class="box-title">Property Available for Disposal</h3>
                </center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                        <thead style="font-weight: bold">
                            <tr>
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
                        <tbody id="assetlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center>
                    <h3 class="box-title">Property Disposal Pending Approval/Upload</h3>
                </center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

        <div class="row" style="display: block">
            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date From</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly" onchange="load_data_Pendingupload();"  />
                </div>
            </div>
            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                <div class="input-prepend input-group margin">
                    <span class="add-on input-group-addon">&nbsp;Date To</span>
                    <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly" onchange="load_data_Pendingupload();" />
                </div>
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
            </div>
        </div>
            <div class="box-body">

                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>DISPOSAL #</td>
                                <td>TYPE COST</td>
                                <td>DISPOSED BY</td>
                                <td>MODE OF DISPOSAL</td>
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
               <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                Print
                        <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu"  id="printGrid1">
                                <li><a id="ciirupxcl" style="cursor: pointer">Consolidated Inventory Inspection Report of Unserviceable Property Form (Excel)</a></li>
                                <li><a id="ciiruspxcl" style="cursor: pointer">Consolidated Inventory Inspection Report of Unserviceable Semi Expendable Property Form (Excel)</a></li>
                                <li><a id="cwmrxcl" style="cursor: pointer">Consolidated Waste Material Report (Excel)</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            <br />
        </div>

        <br />

        <div class="row" id="printablepage" runat="server" hidden>
            <div class="row" id="divDisposalReport" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageIIRUP" runat="server" hidden>
            <div class="row" id="divDisposalReportIIRUP" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageIIRSEP" runat="server" hidden>
            <div class="row" id="divDisposalReportIIRSEP" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageWMR" runat="server" hidden>
            <div class="row" id="divTurnInWMR" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageWMR1" runat="server" hidden>
            <div class="row" id="divTurnInWMR1" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageROP" runat="server" hidden>
            <div class="row" id="divTurnInROP" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageROSEP" runat="server" hidden>
            <div class="row" id="divTurnInROSEP" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageIIRUP1" runat="server" hidden>
            <div class="row" id="divDisposalReportIIRUP1" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageIIRSEP1" runat="server" hidden>
            <div class="row" id="divDisposalReportIIRSEP1" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageROSEP1" runat="server" hidden>
            <div class="row" id="divTurnInROSEP1" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageROP1" runat="server" hidden>
            <div class="row" id="divTurnInROP1" runat="server">
            </div>
        </div>
        <div class="row" id="printablepage1" runat="server" hidden>
            <div class="row" id="divDisposalReport1" runat="server">
            </div>
        </div>



    </section>

    <%--Property List For Disposal--%>
    <div id="disposalList" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;"><span class="fa fa-edit"></span>List of Property for Disposal</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" id="withorwithoutyou">
                        <div class="col-lg-12">
                            <center>
                                <p style="font-size: 12px;">
                                    <i>By clicking the confirm button, the selected property/ies will be sent for approval to your selected approver, Inspector and Certifier. They will be notified with your request. Property will not be disposed if Approver, Inspector and Certifier will not approve. After all of them have approved, you too will be receiving a notification. Make sure that the list is correct.</i>
                                </p>
                            </center>
                        </div>
                    </div>
                    <div class="row" id="withorwithoutyou1">
                        <div class="col-lg-12">
                            <center>
                                <p style="font-size: 12px;">
                                    <i>By clicking the confirm button, the selected property/ies will be sent for Property Disposal. Make sure that the list is correct.
                                    </i>
                                </p>
                            </center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" id="labelmode">
                                        <label class="control-label">
                                            Mode of Disposal:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12" id="dropdownmode">
                                        <asp:DropDownList ID="ddlModeOfDisposal" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                            Remarks:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <textarea class="form-control" id="txtRemarks" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="approvers">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" id="labelApprover">
                                        <label class="control-label">
                                            Approver:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12" id="dropdownApprover">
                                        <asp:DropDownList ID="ddlApprover" class="form-control select2" onchange="GetInspectorAndCertifier();" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblDisposalList" class="table table-bordered table-hover table-condensed display" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>CONDITION</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="disposallist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmAssetDisposal()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToDisposal()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Property Disposal --%>
    <div id="ALPendingUpload" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtDisposalNumber" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
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
                            <%--Accountable Person:--%>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <%--<label id="lblPerson" type="text" />--%>
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
                            <table id="tblPUL1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>STATUS</td>
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
                                <li><a style="cursor: pointer" onclick="PrintGridDataIIRUP()">Inventory and Inspection Report of Unserviceable Property Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataIIRSEP()">Inventory and Inspection Report of Semi-Expendable Property Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataROSEP()">Report of Lost, Stolen, Damaged, or Destoyed Semi-Expendable Property Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridData()">Disposal Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataWMR()">Waste Material Report Form</a></li>


                                <li><a id="iirupxcl" style="cursor: pointer">Inventory and Inspection Report of Unserviceable Property (Excel)</a></li>
                                <li><a id="iiruspxcl" style="cursor: pointer">Inventory and Inspection Report of Semi-Expendable Property (Excel)</a></li>
                                <li><a id="rlsddspxcl" style="cursor: pointer">Report of Lost, Stolen, Damaged, or Destoyed Semi-Expendable Property (Excel)</a></li>
                                <li><a id="wmrxcl" style="cursor: pointer">Waste Material Report (Excel)</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9" id="showorhide">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="AFupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>
                        <div class="col-lg-3">
                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete" onclick="UploadPropertyDisposal()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div id="ALPendingUploadRequest" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtDisposalNumberRequest" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposal No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDisposalNoRequest" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposed By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDisposedByRequest" type="text" />
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
                            <label id="lblDisposedDateRequest" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPUL1Request" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>STATUS</td>
                                    </tr>
                                </thead>
                                <tbody id="pul1Request">
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
                            <ul class="dropdown-menu" role="menu" id="printGrid2">
                                <li><a style="cursor: pointer" onclick="PrintGridDataROSEP1()">Report of Lost, Stolen, Damaged, or Destoyed Semi-Expendable Property Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataWMR1()">Waste Material Report Form</a></li>
                                <li><a id="rlsddspxclR" style="cursor: pointer">Report of Lost, Stolen, Damaged, or Destroyed Semi-Expendable Property (Excel)</a></li>
                                <li><a id="wmrxclR" style="cursor: pointer">Waste Materials Report Excel</a></li>




                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9" id="showorhide1">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="FileUploadRequest" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>
                        <div class="col-lg-3">
                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnCompleteRequest" onclick="UploadPropertyDisposalRequest()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div id="ALPendingUpload2" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn-danger pull-right" data-dismiss="modal" aria-hidden="true" onclick="Clear()"><span class='fa fa-times-circle'></span></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Issuance No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblPARICS2" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Issued By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblIssuedBy2" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Accountable Person:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblPerson2" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Issued Date:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDateIssued2" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPUL2" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>STATUS</td>
                                    </tr>
                                </thead>
                                <tbody id="pul2">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success pull-left" id="btnPrint1" onclick="PrintGridData()">&nbsp;&nbsp;Print&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
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
                                        <label id="lbl_from">Transacted by</label></td>
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
                                        <label id="lbl_by">Approved By</label></td>
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
    <cert:cert id="cert" runat="server" />
</asp:Content>
