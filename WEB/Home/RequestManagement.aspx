<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequestManagement.aspx.cs" Inherits="Home_RequestManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/RequestManagement.js"></script>
    <script src="../Script/RMGoodsReceive.js"></script>
    <script src="../Script/RMPropertyIssuance.js"></script>
    <script src="../Script/RMPropertyRequest.js"></script>
    <script src="../Script/RMPropertyTransfer.js"></script>
    <script src="../Script/RMPropertyDisposal.js"></script>
    <script src="../Script/RMPropertyGatePassRequest.js"></script>
    <script src="../Script/RMPropertyDayPassRequest.js"></script>
    <script src="../Script/RMPropertyReTagging.js"></script>
    <script src="../Script/RMDataUpload.js"></script>
    <script src="../Script/RMStockIssuance.js"></script>
    <script src="../Script/RMStockRequest.js"></script>
    <script src="../Script/RMStockDisposal.js"></script>
    <script src="../Script/RMSupplyAllocation.js"></script>
<%--    <script src="../Script/RMComponent.js"></script>--%>

    

    <script>
 
        function ApproveUserUpload() {
            swal("Successful", "User Upload successfully APPROVED", "success");
            preventDefault();
            window.location.href = window.location.href;
        }

        function DeclineUserUpload() {
            swal("Successful", "User Upload successfully DECLINED", "success");
            preventDefault();
            window.location.href = window.location.href;
        }


        function ErrorDatabaseRestore() {
            swal("Warning", "Error occurred during database restoration", "error");
            preventDefault();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Approvals
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-home"></i>Home</li>
            <li>Approvals</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <!-- Custom Tabs -->
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active" id="caTab"><a href="#tab_1" data-toggle="tab">Goods Receive Approvals</a></li>
                        <li id="airTab"><a href="#tab_3" data-toggle="tab">Property Issuance Approvals</a></li>
                        <li id="atrTab"><a href="#tab_4" data-toggle="tab">Property Transfer Approvals</a></li>
                        <li id="adrTab"><a href="#tab_6" data-toggle="tab">Property Disposal Approvals</a></li>
                        <li id="ufrTab"><a href="#tab_8" data-toggle="tab">Upload File Approvals</a></li>
                        <li id="sirTab"><a href="#tab_10" data-toggle="tab">Supply Issuance/Request Approvals</a></li>
                        <li id="sdrTab"><a href="#tab_11" data-toggle="tab">Supply Disposal Approvals</a></li>
                        <li id="rtgTab"><a href="#tab_12" data-toggle="tab">Retagging</a></li>
                        <li id="sarTab"><a href="#tab_13" data-toggle="tab">Supply Allocation Approval</a></li>
<%--                        <li id="comTab"><a href="#tab_14" data-toggle="tab">Component Approval</a></li>--%>
                    </ul>
                    <div class="tab-content">
                        <!-- create property /stock-->
                        <div class="tab-pane active" id="tab_1">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All Property or Supplies that have been received pending approval will be shown below.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="property">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests for Goods Receive</h3></center>
                                    <div class="box-tools pull-right">
                                        <bu.on type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></bu.on>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblGoodsReceive" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>GOODS RECEIVE #</td>
                                                    <td>RECEIVED BY</td>
                                                    <td>CREATED BY</td>
                                                    <%--<td>NO. OF ITEM</td>--%>
                                                    <td>DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ATTACHMENT/S</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="goodsreceive">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- merge stock -->
                        <div class="tab-pane" id="tab_2">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending requests to Merge existing supplies unto an existing supply item code will be shown below. This is useful when you have multiple vendors which supplies the exact same stock, but have different item codes.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply Merge Pending Approval/Upload</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPendingStockMerge" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>SUPPLY MERGE NO</td>
                                                    <td>FROM ITEM CODE</td>
                                                    <td>TO ITEM CODE</td>
                                                    <td>QUANTITY</td>
                                                    <td>REMARKS</td>
                                                    <td>TRANSACTION DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="pendingstockmerge">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- asset issuance -->
                        <div class="tab-pane" id="tab_3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Property Issuance Request and Property Request will be shown below. Property Issuance Requests are requested by custodians to issue properties for employees whereas Property Requests are requested by the employees themselves.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPending">
                                <%-- style="display: none"--%>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Property Issuance Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyIssuance" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>ISSUANCE #</td>
                                                    <td>ISSUED TO</td>
                                                    <td>DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ATTACHMENT/S</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertyissuancelist">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="box box-solid box-primary" id="divPendingRequest" hidden>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Property Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyRequest" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>REQUEST #</td>
                                                    <td>REQUESTED BY</td>
                                                    <%--<td># OF ITEM</td>--%>
                                                    <td>RETURN DATE</td>
                                                    <td>TRANSACTION DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ATTACHMENT/S</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertyrequestlist">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPending" hidden>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">Property Return Date of Extension Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPendingRequestExtension" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>REQUESTOR</td>
                                                    <td>PROPERTY #</td>
                                                    <td>DESCRIPTION</td>
                                                    <td>SERIAL #</td>
                                                    <td>CLASSIFICATION</td>
                                                    <td>REMARKS</td>
                                                    <td>APPROVER</td>
                                                    <td>CURRENT RETURN DATE</td>
                                                    <td>EXTENSION RETURN DATE</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="pendingrequestextension">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- asset transfer -->
                        <div class="tab-pane" id="tab_4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Properties for Transfer will be shown below.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">Property Transfer Pending Approval/Upload</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyTransfer" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>TRANSACTION #</td>
                                                    <td>PURPOSE</td>
                                                    <%--<td># OF ITEM</td>--%>
                                                    <td>STATUS</td>
                                                    <td>CREATED BY</td>
                                                    <td>TRANSACTION DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>REMARKS</td>
                                                    <td>ATTACHMENT/S</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertytransfer">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- turn over -->

                        <!-- asset disposal -->
                        <div class="tab-pane" id="tab_6">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Properties for Disposal will be shown below.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests For Property Disposal</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyDisposal" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>TRANSACTION TYPE</td>
                                                    <td>DISPOSAL #</td>
                                                    <td>DISPOSED BY</td>
                                                    <td># OF ITEM </td>
                                                    <td>DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertydisposal">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                      <%--      <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Properties for Disposal Request will be shown below.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests For Property Disposal Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyDisposalRequest" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>DISPOSAL #</td>
                                                    <td>DISPOSED BY</td>
                                                    <td># OF ITEM </td>
                                                    <td>DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertydisposalRequest">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />--%>
                            <div class="row" hidden>
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All approved Properties for Disposal that will be subject for inspection will be shown below.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" hidden>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests For Property Inspection Disposal</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyInspectionDisposal" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>DISPOSAL #</td>
                                                    <td>DISPOSED BY</td>
                                                    <td># OF ITEM </td>
                                                    <td>DATE</td>
                                                    <td>INSPECTOR</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertyinspectiondisposal">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" hidden>
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All inspected Properties for Disposal that will be subject for certification will be shown below. 
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" hidden>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests For Property Certification Disposal</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyCertificationDisposal" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>DISPOSAL #</td>
                                                    <td>DISPOSED BY</td>
                                                    <td># OF ITEM </td>
                                                    <td>DATE</td>
                                                    <td>CERTIFIER</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertycertificationdisposal">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- upload file  -->
                        <div class="tab-pane" id="tab_8">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending excel file uploads of your existing properties, supplies and users/employees will be shown below. Uploaded excel files of property, supplies and user information will not be reflected unto the system untill you approve below requests.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="propertyuploadbox">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests for Property Upload</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>BATCH NAME</td>
                                                    <td>CREATED BY</td>
                                                    <td>DATE</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="propertyupload">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="box box-solid box-primary" id="stockuploadbox">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests for Supply Upload</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblStockUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>BATCH NAME</td>
                                                    <td>CREATED BY</td>
                                                    <td>DATE</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="stockupload">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="box box-solid box-primary" id="useruploadbox">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests for User Upload</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblUserUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>BATCH NAME</td>
                                                    <td>CREATED BY</td>
                                                    <td>DATE</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="userupload">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- restore database  -->
                        <div class="tab-pane" id="tab_9">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>Pending database restoration requests will be shown below. Please make sure and advise your organization to stop all transactions prior to approving below database restoration request. All transactions made after the creation date of the backup Database file to be restored will be lost.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests for Restore Database</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblDatabaseRestore" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>RESTORE NAME</td>
                                                    <td>REQUESTOR</td>
                                                    <td>DATE</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="databaserestore">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- stock issuance -->
                        <div class="tab-pane" id="tab_10">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Supply Issuance  will be shown below. Supply Issuance Requests are requested by custodians to issue supplies for employees.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPendingA">
                                <%-- style="display: none"--%>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply Issuance Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblStockIssuance" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>ISSUANCE #</td>
                                                    <td>ISSUED TO</td>
                                                    <td>REMARKS</td>
                                                    <td>DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="stockissuancelist">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="box box-solid box-primary" id="divPendingaa">
                                <%-- style="display: none"--%>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblStockRequest" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>REQUEST #</td>
                                                    <td>REQUEST BY</td>
                                                    <td>REMARKS</td>
                                                    <td>DATE</td>
                                                    <td>APPROVER</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="stockrequestlist">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- stock disposal -->
                        <div class="tab-pane" id="tab_11">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Supply Disposal  will be shown below. Supply Disposal Requests are requested by custodians.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPendingStockDisposal">
                                <%-- style="display: none"--%>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply Disposal Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblStockDisposal" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>DISPOSAL #</td>
                                                    <td>TRANSACTOR</td>
                                                    <td>APPROVER</td>
                                                    <td>MODE OF DISPOSAL</td>
                                                    <td>REMARKS</td>
                                                    <td>CREATED DATE</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="stockdisposal">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Component -->
<%--                        <div class="tab-pane" id="tab_14" hidden>
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Supply Allocation will be shown below. Supply Allocation Requests are requested by custodians.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPendingComponent">
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply Allocation Request</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblComponent" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>COMPONENT #</td>
                                                    <td>CREATED BY</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="component">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                      <!-- stock allocation -->
                        <div class="tab-pane" id="tab_13">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All pending Supply Allocations will be shown below. Supply Allocation Requests are requested by custodians.

                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPendingStockAllocation">
                                <%-- style="display: none"--%>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Supply Allocation Approvals</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblStockAllocation" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>ALLOCATION #</td>
                                                    <td>CREATED BY</td>
                                                    <td>SOURCE</td>
                                                    <td>DESTINATION</td>
                                                    <td>REMARKS</td>
                                                    <td>STATUS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="stockallocation">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- retagging -->
                        <div class="tab-pane" id="tab_12">
                            <div class="row">
                                <div class="col-lg-12">
                                    <center><p style="font-size:12px;"><i>All Property or Supplies that have been retagged pending approval will be shown below.
                                    </i></p></center>
                                </div>
                            </div>
                            <div class="box box-solid box-primary" id="divPropertyRetaggingApproval">
                                <%-- style="display: none"--%>
                                <div class="box-header with-border" data-widget="collapse">
                                    <center><h3 class="box-title">List of Requests for ReTagging</h3></center>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-lg-12">
                                        <table id="tblPropertyRetagging" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                            <thead style="font-weight: bold">
                                                <tr>
                                                    <td>TRANSACTION #</td>
                                                    <td>CREATED BY</td>
                                                    <td>REMARKS</td>
                                                    <td>ACTION</td>
                                                </tr>
                                            </thead>
                                            <tbody id="PropertyRetagging">
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
    </section>

    <%--Goods Receive Details--%>
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
                    <%--<div class="row">
                        <div class="col-lg-12">
                            <p>
                                By clicking the confirm button, the selected property/ies will be sent as a goods receive for approval to your selected approver.
                           The approver will be notified with your request. After the approver's reply you too will be receiving  a notification. Make sure that the list is correct.
                            </p>
                        </div>
                    </div>--%>

                    <div class="row">
                        <div class="col-lg-12" style="overflow: auto">
                            <table id="tblGRDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>TYPE</td>
                                        <td>ITEM CODE</td>
                                        <td>SERIAL NUMBER</td>
                                        <td>DESCRIPTION</td>
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
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirm" onclick="ApproveGoodsReceive()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger"  data-dismiss="modal" onclick="DeclineGoodsReceive()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Property issuance details --%>
    <div id="viewPropertyIssuanceDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
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
                            <label id="lblPARICS" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Issued By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblIssuedBy" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Accountable Person:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblPerson" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Issued Date:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDateIssued" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPropertyIssuanceDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                <tbody id="propertyissuancedetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyIssuance" onclick="ApprovePropertyIssuance()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger"  data-dismiss="modal"  onclick="DeclinePropertyIssuance()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Property Request --%>
    <div id="viewPropertyRequestDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Request No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblRequestNumber1" type="text" />
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
                            <table id="tblRequestList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                <tbody id="requestlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyRequest" onclick="ApprovePropertyRequest()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger"  data-dismiss="modal"  onclick="DeclinePropertyRequest()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Property Transfer --%>
    <div id="viewPropertyTransferDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPropertyTransferList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>FROM</td>
                                        <td>TO</td>
                                    </tr>
                                </thead>
                                <tbody id="propertytransferlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyTransfer" onclick="ApprovePropertyTransfer()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger"  data-dismiss="modal" onclick="DeclinePropertyTransfer()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%--Day Pass--%>
    <div id="dayPassPropertyList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>Personal Property Gate Pass Request List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPAL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>SERIAL #</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>DESCRIPTION</td>
                                    </tr>
                                </thead>
                                <tbody id="pal">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyDayPass" onclick="ApprovePropertyDayPass()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclinePropertyDayPass()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Property Disposal --%>
    <div id="viewDisposalRequestDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
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
                            <table id="tblDisposalDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                        <td>CONDITION</td>
                                    </tr>
                                </thead>
                                <tbody id="disposaldetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyDisposal" onclick="ApprovePropertyDisposal()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclinePropertyDisposal()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    

     <div id="viewJDisposalRequestDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblJDisposalDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>DISPOSAL #</td>
                                        <td>REMARKS</td>
                                        <td>ATTACHMENTS</td>
                                    </tr>
                                </thead>
                                <tbody id="jdisposaldetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyJDisposal" onclick="ApproveJDisposal()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclineJDisposal()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%--////TO BE DELETED --%>
    <%-- Property Disposal Request --%>
<%--    <div id="viewDisposalRequestDetailsRequest" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
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
                            <table id="tblDisposalDetailsRequest" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                        <td>CONDITION</td>
                                    </tr>
                                </thead>
                                <tbody id="disposaldetailsRequest">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyDisposalRequest" onclick="ApprovePropertyDisposalRequest()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclinePropertyDisposalRequest()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>--%>
     <%-- Property Retagging --%>
    <div id="ViewPropertyRetaggingDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblpropertyRetaggingDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>TRANSACTION #</td>
                                        <td>PROPERTY #</td>
                                        <td>STATUS</td>
                                        
                                    </tr>
                                </thead>
                                <tbody id="propertyRetaggingDetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyRetagging" onclick="ApprovePropertyRetagging()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclinePropertyRetagging()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="viewDisposalRequestDetailsInspection" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposal No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblInspectionDisposalNo" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposed By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblInspectionDisposedBy" type="text" />
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
                            <label id="lblInspectionDisposedDate" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblInspectionDisposalDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                        <td>CONDITION</td>
                                    </tr>
                                </thead>
                                <tbody id="inspectiondisposaldetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyInspectionDisposal" onclick="ApprovePropertyInspectionDisposal()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclinePropertyInspectionDisposal()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    
    <div id="viewDisposalRequestDetailsCertification" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposal No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblCertificationDisposalNo" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposed By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblCertificationDisposedBy" type="text" />
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
                            <label id="lblCertificationDisposedDate" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblCertificationDisposalDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                        <td>CONDITION</td>
                                    </tr>
                                </thead>
                                <tbody id="certificationdisposaldetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnConfirmPropertyCertificationDisposal" onclick="ApprovePropertyCertificationDisposal()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  data-dismiss="modal"  onclick="DeclinePropertyCertificationDisposal()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%--Goods Property Upload Details--%>
    <div id="viewPropertyUploadDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Upload Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                         <input id="txtBatchName" runat="server" type="text" class="form-control" style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblPropertyUploadDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <%--<td>STOCK NUMBER</td>--%>
                                        <td>PROPERTY #</td>
                                        <td>SERIAL #</td>
                                        <td>DESCRIPTION</td>
                                    </tr>
                                </thead>
                                <tbody id="propertyuploaddetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<asp:Button class="btn btn-success" UseSubmitBehavior="false" runat="server" OnClick="ApprovePU_Click" Text="Approve" />
                    <asp:Button class="btn btn-danger" UseSubmitBehavior="false" runat="server" OnClick="DeclinePU_Click" Text="Decline" />--%>
                    
                    <button type="button" class="btn btn-success" onclick="ApprovePropertyUpload()">&nbsp;&nbsp;&nbsp;Approve&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" onclick="DeclinePropertyUpload()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="viewStockUploadDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Upload Supply List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <input id="txtStockBatchName" type="text" class="form-control" style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblStockUploadDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="stockuploaddetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<asp:Button class="btn btn-success" UseSubmitBehavior="false" runat="server" OnClick="ApproveSU_Click" Text="Approve" />
                    <asp:Button class="btn btn-danger" UseSubmitBehavior="false" runat="server" OnClick="DeclineSU_Click" Text="Decline" />--%>
                    <button type="button" class="btn btn-success" onclick="ApproveStockUpload()">&nbsp;&nbsp;&nbsp;Approve&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" onclick="DeclineStockUpload()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="viewUserUploadDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Upload User List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <input id="txtUserBatchName" runat="server" type="text" class="form-control" style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblUserUploadDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>NAME</td>
                                        <td>EMAIL ADDRESS</td>
                                        <td>USERNAME</td>
                                        <td>EMPLOYEE #</td>
                                    </tr>
                                </thead>
                                <tbody id="useruploaddetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" onclick="ApproveUserUpload1()">&nbsp;&nbsp;&nbsp;Approve&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" onclick="DeclineUserUpload1()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                   <%-- <asp:Button class="btn btn-success" UseSubmitBehavior="false" runat="server" OnClick="ApproveUU_Click" Text="Approve" />
                    <asp:Button class="btn btn-danger" UseSubmitBehavior="false" runat="server" OnClick="DeclineUU_Click" Text="Decline" />--%>
                    <%--<button type="button" class="btn btn-danger" onclick="DeclineUserUpload()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>--%>
                </div>
            </div>
        </div>
    </div>

    <%--Database Restore--%>
    <div id="RestoreDB" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: white; text-align: center;">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>--%>
                    <h4>
                        <p style="color: black;"><span></span>Restore Database</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <center><p style="font-size:12px;">Are you sure you want to continue this action? By, clicking yes, Database will be restored. Please Make sure no one is accessing the System.</p></center>
                    </div>
                    <div class="row" hidden>
                        <asp:TextBox ID="txtDirectory" runat="server" type="text" disabled="true" class="form-control" Style="width: 100%; table-layout: fixed;" />
                        <input id="txtBakFileName" runat="server" type="text" class="form-control" style="width: 100%" />
                    </div>
                </div>
                <div class="modal-footer">
                    <div>
                        <div class="col-lg-6">
                            <asp:Button ID="upload" runat="server" class="btn btn-success pull-right" OnClick="Restore" Text="Yes" />
                        </div>
                        <div class="col-lg-6">
                            <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Stock Issuance --%>
    <div id="viewStockIssuanceDetails" class="modal fade" tabindex="-1" role="dialog">
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
                            <%--Issued By:--%>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <%--<label id="lblStockIssuedBy" type="text" />--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Accountable Person:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockAccountablePerson" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <%--Issued Date:--%>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <%--<label id="lblStockDateIssued" type="text" />--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblStockIssuanceDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                <tbody id="stockissuancedetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-success" id="btnConfirmStockIssuance" onclick="ApproveStockIssuance()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclineStockIssuance()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Stock Request --%>
    <div id="viewStockRequestDetails" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List of Suoplies</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <%--<input type="text" id="txtPARICS" runat="server"  />--%>
                        <asp:TextBox ID="txtStockRequestNo" runat="server" type="text" class="form-control" Style="width: 100%; table-layout: fixed;" />
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
                            <%--Issued By:--%>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <%--<label id="lblStockIssuedBy" type="text" />--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Accountable Person:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockRequestPerson" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <%--Issued Date:--%>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <%--<label id="lblStockDateIssued" type="text" />--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblStockRequestDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>QUANTITY</td>
                                        <td>ISSUE QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="stockrequestdetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-success" id="btnConfirmStockRequest" onclick="ApproveStockRequest()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"  onclick="DeclineStockRequest()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Stock Disposal --%>
    <div id="viewStockDisposalDetails" class="modal fade" tabindex="-1" role="dialog">
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
                        <asp:TextBox ID="txtStockDisposalNo" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposal No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockDisposalNo" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Disposed By:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockDisposedBy" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Date:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblStockDateDisposed" type="text" />
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
                            <table id="tblStockDisposalDetails" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                <tbody id="stockdisposaldetails">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-success" id="btnConfirmStockDisposal" onclick="ApproveStockDisposal1()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="DeclineStockDisposal()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

        <%-- Stock Allocation --%>
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
                        <asp:TextBox ID="TextBox1" runat="server" type="text" class="form-control" Style="width: 100%" />
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
                                        <td>ITEM CODE</td>
                                        <td>DESCRIPTION</td>
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
                    </div>
                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-success" id="btnConfirmStockAllocation" onclick="ApproveStockDisposal2()">&nbsp;&nbsp;Approve&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="DeclineStockAllocation()">&nbsp;&nbsp;&nbsp;Decline&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
