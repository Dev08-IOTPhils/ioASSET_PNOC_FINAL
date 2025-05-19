<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SupplyAllocation.aspx.cs" Inherits="Custodian_Operation_Module_SupplyAllocation" %>

<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/SupplyAllocation.js"></script>
    <script type="text/javascript">
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

        function PrintGridDataPAR() {
            var prtGrid = document.getElementById('<%=printablepagePAR.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataPTR() {
            var prtGrid = document.getElementById('<%=printablepagePTR.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataICS() {
            var prtGrid = document.getElementById('<%=printablepageICS.ClientID %>');
            console.log(prtGrid);
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
            window.location.href = window.location.href;
        }

        function uploadSuccessW() {
            swal("Successful", "Transaction Completed without Supporting Document!", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function filenotseen() {
            swal("Error", "Please select a file to upload", "error");
            preventDefault();
            window.location.href = window.location.href;
        }
    </script>
    <style>
        .divider {
            width: 5px;
            height: auto;
            display: inline-block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supply Allocation
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Supply Allocation</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-lg-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Filter</h3>
                    </div>
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
<%--                        <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                            <div class="input-group margin">
                                <label class="control-label" align="left">
                                    Type:
                                </label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                            <asp:DropDownList ID="ddlTrasferType" class="form-control select2" onchange="ShowTransfertype()" runat="server" Style="width: 100%; height: 35px">
                                <asp:ListItem Text=" --- Select type --- " Value="0" />
                                <asp:ListItem Text="Person" Value="1" />
                                <asp:ListItem Text="Location" Value="2" />
                            </asp:DropDownList>
                        </div>--%>
<%--                        <div id="showorHide" style="display: none">
                            <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12">
                                <label class="select2-primary" align="left">
                                    Property Type Cost:
                                </label>
                            </div>
                            <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                                <asp:DropDownList ID="ddlItem" class="form-control select2" runat="server" onchange="load_data();" Style="width: 100%; height: 35px"></asp:DropDownList>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
<%--        <br />
        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>
                    Select Source location and destination site first and then select available properties for transfer. Take note that Issued or Borrowed Properties can only be transferred location to location within the same site. If Property Transfer is between Site to Site, properties cannot be transferred . It should be returned first before you can do property transfer between site to site.</i>
                </p></center>
            </div>
        </div>--%>
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Source</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="SourceLocation" style="display: block">
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <asp:Label ID="lblDisplayFromSite" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTRegion" class="input-group">
                                <select id="ddlFromSitePT" class="form-control select2" onchange="GetLocationPT();" data-dropdown-css-class="select2-primary" style="width: 220px; height: 35px"></select>
                                <span id="HTPTRegionC" class="btn btn-success input-group-addon" onclick="addWarehouseDialogBox('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>

<%--                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <asp:Label ID="lblDisplayFromLoc" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTBranch" class="input-group">
                                <select id="ddlFromLocationPT" onchange="GetOfficePT();load_data();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTBranchC" class="btn btn-success input-group-addon" onclick="addLocationDialogBox('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>--%>

<%--                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <asp:Label ID="lblDisplayFromArea" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTOffice" class="input-group">
                                <select id="ddlFromOfficePT" onchange="load_data();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTOfficeC" class="btn btn-success input-group-addon" onclick="addAreaDialogBox('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>--%>
                    </div>
<%--                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="sourcePerson" style="display: block">
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label">
                                From Site/Department:
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTFromPerson" class="input-group">
                                <select id="ddlFromPersonPT" onchange="GetListPerPerson();removeSelectedPerson();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTFromPersonC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Destination</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="DestinationLocation" style="display: block">
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <asp:Label ID="lblDisplayToSite" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTToRegion" class="input-group">
                                <select id="ddlToSitePT" class="form-control select2" onchange="GetLocationToPT();" data-dropdown-css-class="select2-primary" style="width: 220px; height: 35px"></select>
                                <span id="HTPTToRegionC" class="btn btn-success input-group-addon" onclick="addWarehouseDialogBox('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>

<%--                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <asp:Label ID="lblDisplayToLoc" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTToBranch" class="input-group">
                                <select id="ddlToLocationPT" onchange="GetOfficeToPT()" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTToBranchC" class="btn btn-success input-group-addon" onclick="addLocationDialogBox('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                <asp:Label ID="lblDisplayToArea" runat="server" Text="Label"></asp:Label>
                            </label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTToOffice" class="input-group">
                                <select id="ddlToOfficePT" class="form-control select2" onchange="GetCount();" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTToOfficeC" class="btn btn-success input-group-addon" onclick="addAreaDialogBox('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>--%>
                    </div>
<%--                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="DestinationPerson" style="display: block">
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <label class="control-label">
                                To Site/Department:
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-2 col-sm-12 col-xs-12">
                            <div id="HTPTToPerson" class="input-group">
                                <select id="ddlTopersonPT" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <span id="HTPTToPersonC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('PTransfer')"><i class="fa fa-plus"></i></span>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            </div>
            <div class="col-lg-3 col-md-1 col-sm-12 col-xs-12" align="right"></div>
            <div class="col-lg-3 col-md-5 col-sm-12 col-xs-12" align="right">
                <div class="input-group margin">
                    <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="transferItem()">
                        <i class="glyphicon glyphicon-list"></i>
                        <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View Suppy List for Allocation:&nbsp;0</span>
                    </a>
                </div>
            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Supply/ies Available for Allocation</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12">
                        <center><p style="font-size:12px;"><i>Click the plus icon below to add property to your List of supplies for allocation.</i></p></center>
                    </div>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>DESCRIPTION</td>
                                <td>QUANTITY</td>
                                <td>UNIT</td>
                                <td>SITE/DEPARTMENT</td>
                                <td>CLASSIFICATION</td>
                                <td>CUSTODIAN TYPE</td>
                                <td>ACTION</td>
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
                <center><h3 class="box-title">Supply Allocation Pending Approval/Upload</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
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
                                <td>ALLOCATION #</td>
                                <td>REMARKS</td>
                                <td>ORIGIN</td>
                                <td>DESTINATION</td>
                                 <td>CREATED BY</td>
                                <td>TRANSACTION DATE</td>
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
    </section>

    <div id="transferItemList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;"><span class="glyphicon glyphicon-transfer"></span>Supply for allocation</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" id="withorwithoutyou">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the confirm button, the selected property/ies will be sent for approval to your selected approver.
                           The approver will be notified with your request. After the approver's reply you too will be receiving  a notification. Make sure that the list is correct.</i>
                            </p></center>
                        </div>
                    </div>
                    <div class="row" id="withorwithoutyou1">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                By clicking the confirm button, the selected property/ies will be sent for Property Transfer for the selected Destination. Make sure that the list is correct.</i>
                            </p></center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <textarea class="form-control" id="txtRemarks" runat="server"></textarea>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12" id="labelApprover">
                            <label class="control-label">
                                Approver:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="dropdownApprover">
                            <asp:DropDownList ID="ddlApprover" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblTransferList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>QUANTITY</td>
                                        <td>UNIT</td>
<%--                                        <td>SITE/DEPARTMENT</td>--%>
                                        <td>CLASSIFICATION</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>ACTION</td>
                                    </tr>
                                </thead>
                                <tbody id="transferlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmPropertyTransfer1()">&nbsp;&nbsp;Confirm Transfer&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToTransfer()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="ConfirmTransfer" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="divContentHeader modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <p style="color: white;">Confirm Transfer</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to transfer these property/ies?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-primary" id="btnDeleteBrand" onclick="AssetItemLocTransfer()">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <div id="spin" style="display: none">
        <center style="position: absolute; top: 50%; left: 50%; transform: translateX(-50%) translateY(-50%); background: #222; opacity: 0.7; z-index: 1000; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center;">
                <h1><div id="please-wait">Please wait</div> <br /><span class="fa fa-gear fa-spin"></span></h1>
            </center>
    </div>

    <div class="row" id="printablepagePAR" runat="server" hidden>
        <div class="row" id="divIssuanceReportPAR" runat="server">
        </div>
    </div>

    <div class="row" id="printablepagePTR" runat="server" hidden>
        <div class="row" id="divIssuanceReportPTR" runat="server">
        </div>
    </div>
    <div class="row" id="printablepageICS" runat="server" hidden>
        <div class="row" id="divIssuanceReportICS" runat="server">
        </div>
    </div>

    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divGatePass" runat="server">
        </div>
    </div>

    <div id="ALPendingApproval" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Supply/ies</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtTransactionNo" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPUL1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>FROM</td>
                                        <td>TO</td>
                                    </tr>
                                </thead>
                                <tbody id="pul1">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div id="isTransfer">
                        <div class="btn-group pull-left">
                            <button type="button" class="btn btn-success pull-left">Print</button>
                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu" id="printGrid">
                                <li><a style="cursor: pointer" onclick="PrintGridData()">Inventory Transfer Report</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPTR()">Property Transfer Report</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPAR()">Property Acknowledgement Receipt Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataICS()">Inventory Custodian Slip Form</a></li>
                                <li><a id="transferitrbtn" style="cursor: pointer">Inventory Transfer Report Form With Certificate</a></li>
                                <li><a id="itrxcl" style="cursor: pointer">Inventory Transfer Report Excel</a></li>
                                <li><a id="icsxcl" style="cursor: pointer">Inventory Custodian Slip Excel</a></li>
                                <li><a id="ptrxcl" style="cursor: pointer">Property Transfer Report Excel</a></li>
                                <li><a id="parxcl" style="cursor: pointer">Property Acknowledgement Receipt Excel</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="showorhide">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete" onclick="UploadStockIssuance()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>

                        <asp:FileUpload ID="AFupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 30%;" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ALPendingApproval1" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Assets</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPUL2" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>FROM</td>
                                        <td>TO</td>
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
                </div>
            </div>
        </div>
    </div>

<%--    added by: Domdom--%>
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
                            Quantity:
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

    <div id="viewStockAllocationDetails" class="modal fade" tabindex="-1" role="dialog">
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
                        <%--<input type="text" id="txtPARICS" runat="server"  />--%>
                        <asp:TextBox ID="txtSupplyAlloc" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Allocation No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockAllocationNo" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Created By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockAllocationBy" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Date:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockDateAllocation" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <%--Accountable Person:--%>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <%--<label id="lblStockAccountablePerson" type="text" />--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblStockAllocationDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ALLOCATION #</td>
                                        <td>REMARKS</td>
                                        <td>ORIGIN</td>
                                        <td>DESTINATION</td>
                                        <td>STATUS</td>
                                        <td>QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="stockallocationdetails">
                                </tbody>
                            </table>
                        </div>
                        &nbsp;
                        <div class="col-lg-12" id="showorhide1">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete1" onclick="UploadStockAllocation()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        <asp:FileUpload ID="FileUpload1" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 30%;" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!-- ITR Signature and Certificate -->
    <!-- ITR Signature and Certificate -->
    <cert:cert id="cert" runat="server" />




</asp:Content>
