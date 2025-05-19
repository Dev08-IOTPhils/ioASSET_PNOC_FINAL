<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyGatePassRequest.aspx.cs" Inherits="Operation_Module_PropertyGatePassRequest" %>
<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyGatePassRequest.js"></script>
    <script type="text/javascript" lang="Javascript">

        function uploadSuccess() {
            alert('Transaction Completed with Supporting Document!');
            window.location.href = window.location.href;
        }

        function uploadSuccessW() {
            alert('Transaction Completed without Supporting Document!');
            window.location.href = window.location.href;
        }

        function filenotseen() {
            swal("Error", "Please select a file to upload", "error");
            preventDefault();
            window.location.href = window.location.href;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Gate Pass Request
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Property Gate Pass Request</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>You may request gate pass for properties assigned to you. Choose your property below that you want to request for a Gate Pass.</i></p></center>
            </div>
        </div>
        <br />
        <div class="row" id="cartwheel" style="display: block">
            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
            </div>

            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12" align="right">
                <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="GatePassList()">
                    <i class="glyphicon glyphicon-list"></i>
                    <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View Property Gate Pass Request List:&nbsp;0</span>
                </a>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Available for Gate Pass</h3></center>
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
                <center><h3 class="box-title">Property Gate Pass Request Pending Approval</h3></center>
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
                                <td style="width: 10%">GATE PASS #</td>
                                <td style="width: 30%">DATE</td>
                                <td style="width: 50%">REMARKS</td>
                                <td style="width: 30%">STATUS</td>
                                <td style="width: 10%">ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pendinguploadlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="row" id="printablepage" runat="server" hidden>
            <div class="row" id="divGatePassForm" runat="server">
            </div>
        </div>
        <div id="GatePassList" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: steelblue; text-align: center;">
                        <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">
                            <p style="color: white;"><span class="fa fa-edit"></span>List of Property for Gate Pass</p>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row" id="withorwithoutyou">
                            <div class="col-lg-12">
                                <center><p style="font-size:12px;"><i>
                                    By clicking the confirm button, the selected property/ies will be sent for approval to your selected approver.
                           The approver will be notified with your request. After the approver's reply you too will be receiving  a notification. Make sure that the list is correct.
                                </i></p></center>
                            </div>
                        </div>
                        <div class="row" id="withorwithoutyou1">
                            <div class="col-lg-12">
                                <center><p style="font-size:12px;"><i>
                                    By clicking the confirm button, the selected property/ies will be sent for Property Issuance for the selected Employee. Make sure that the list is correct.
                                </i></p></center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                                <label class="control-label">
                                    Remarks:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                                <textarea class="form-control" id="txtRemarks"></textarea>
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12" id="labelAppro2ver">
                                <label class="control-label">
                                    Requested By:
                                </label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="dropdownApp2rover">
                                <asp:DropDownList ID="ddlRequestedBy" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                            </div>
                        </div>

                        <br />
                           <div class="row">
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
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
                                <table id="tblGatePassList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                                    <thead style="font-weight: bold">
                                        <tr>
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
                                    <tbody id="gatepasslist">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmGatePass()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                        <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToGatePass()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div id="AssetList1" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>Gate Pass Request Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <input type="text" id="txtGatePassNumber" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Gate Pass No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblGatePassNumber" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPURL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
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
                                <tbody id="purl">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="btn-group pull-left">
                        <button type="button" class="btn btn-success pull-left">Print</button>
                        <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                            <span class="caret"></span>
                            <span class="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul class="dropdown-menu" role="menu" id="printGrid">
                            <li><a style="cursor: pointer" onclick="PrintGridData()">Gate Pass Form</a></li>
                            <li><a id="gatepassbtn" style="cursor: pointer">Gate Pass Form With Certificate</a></li>
                            <li><a id="gpxcl" style="cursor: pointer">Gate Pass Excel </a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- RIS Signature and Certificate -->

    <div id="GatesigandCertModal" class="modal fade" tabindex="-1" role="dialog">
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
                                    <td colspan="2">Requested By</td>
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
                                        <input type="checkbox" class="form-check-input" id="requesteddatestamp" /><label for="bydatestamp">&nbsp;Add Date Time Stamp</label>
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
                                        <input type="checkbox" class="form-check-input" id="approveddatestamp" /><label for="bydatestamp">&nbsp;Add Date Time Stamp</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row" style="margin: 5px;">
                        <div class="col-lg-6">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                                <tr>
                                    <td>Inspected by</td>
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
                                        <input type="checkbox" class="form-check-input" id="issueddatestamp" /><label for="bydatestamp">&nbsp;Add Date Time Stamp</label>
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
     <cert:cert id="cert" runat="server" />
</asp:Content>