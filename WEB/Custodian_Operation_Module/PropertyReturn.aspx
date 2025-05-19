<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyReturn.aspx.cs" Inherits="Operation_Module_PropertyReturn" %>

<%@ Register Src="~/Modals/GRIARCertificateModal.ascx" TagPrefix="cert" TagName="cert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyReturn.js"></script>
    <style>
        .red {
            background-color: red !important;
        }
    </style>
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
        function PrintGridDataCTR() {
            var prtGrid = document.getElementById('<%=printablepageCTR.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataRAR() {
            var prtGrid = document.getElementById('<%=printablepageRAR.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataRARSEP() {
            var prtGrid = document.getElementById('<%=printablepageRARSEP.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }


        function PrintGridDataReturnPPE() {
            var prtGrid = document.getElementById('<%=printablepageReturnPPE.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataReturnRequestPPE() {
            var prtGrid = document.getElementById('<%=printablepageReturnRequestPPE.ClientID %>');
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

        function inputremarks() {
            swal("Fill in Required Field:", "Remarks", "warning");
            preventDefault();
            window.location.href = window.location.href;
        }

        function filenotseen() {
            swal("Error", "Please select a file to upload", "error");
            preventDefault();
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Return
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Property Return</li>
        </ol>
    </section>
    <br />
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
        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>Select employee below to see which borrowed properties are available for return.</i>
                </p></center>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-1">
                <label class="control-label" align="left">
                    &emsp;&nbsp;&nbsp;  User Department:
                </label>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                <div id="HTPReturnDept" class="input-group">
                    <select id="ddlDepartmentPR" onchange="GetEmployeePR();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                    <span id="HTPReturnDeptC" class="btn btn-success input-group-addon" onclick="addDepartmentDialogBox('')"><i class="fa fa-plus"></i></span>
                </div>
            </div>

            <div class="col-lg-1">
                <label class="control-label" align="left">
                    Employee:
                </label>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                <div id="HTPReturnEmployee" class="input-group">
                    <select id="ddlEmployeePR" onchange="load_data();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                    <span id="HTPReturnEmployeeC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('PropertyReturn')"><i class="fa fa-plus"></i></span>
                </div>
            </div>
            <div class="col-lg-1"></div>
            <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12" align="right">
                <a id="btnCart" class="btn btn-block btn-dict-pref pull-left" onclick="ReturnList()">
                    <i class="glyphicon glyphicon-list"></i>
                    <span style="font-family: 'Source Sans Pro','Helvetica Neue','Helvetica','Arial','sans-serif'; color: black;">&nbsp;View Property List for Return:&nbsp;0</span>
                </a>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Property Available for Return</h3></center>
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
                                <td>ASSIGNED EMPLOYEE</td>
                                <td>DATE OF RETURN</td>
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
                <center><h3 class="box-title">Pending Property Return</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>List of all returned properties but currently under maintenance and/or for inspection is displayed here.</i>
                    </p></center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload1" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>RETURN #</td>
                                <td>EMPLOYEE</td>
                                <td>DATE</td>
                                <td>STATUS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="pendinguploadlist1">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="box box-solid box-primary" hidden>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Pending Property Return Request</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <center><p style="font-size:12px;"><i>List of all returned properties but currently under maintenance and/or for inspection is displayed here.</i>
                    </p></center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>RETURN #</td>
                                <td>EMPLOYEE</td>
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
        <div class="row" id="printablepageCTR" runat="server" hidden>
            <div class="row" id="divTurnInCTR" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageRAR" runat="server" hidden>
            <div class="row" id="divTurnInRAR" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageRARSEP" runat="server" hidden>
            <div class="row" id="divTurnInRARSEP" runat="server">
            </div>
        </div>

        <div class="row" id="printablepageReturnPPE" runat="server" hidden>
            <div class="row" id="divTurnInReturnPPE" runat="server">
            </div>
        </div>
        <div class="row" id="printablepageReturnRequestPPE" runat="server" hidden>
            <div class="row" id="divTurnInReturnRequestPPE" runat="server">
            </div>
        </div>

    </section>

    <div id="ConfirmationAdd" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-edit"></span>Return Property</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    <h6><b>Condition :</b></h6>
                                </label>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                <asp:DropDownList ID="ddlCondition" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnDeleteBrand" onclick="TransferToTurnIn()">&nbsp;&nbsp;Add To Return&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="AssetListModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>Property Return List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtReturnRequestNumber" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Return Request No:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblReturnRequestNumber" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Date Requested:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDateRequested" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Accountable Person:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblAccountablePerson" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-9">
                            <textarea class="form-control" id="txtRemarks" runat="server"></textarea>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPURL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>REASON</td>
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
                    <div class="col-lg-3">
                        <div class="btn-group pull-left">
                            <button type="button" class="btn btn-success pull-left">Print</button>
                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu" id="printGrid">
                                <li><a style="cursor: pointer" onclick="PrintGridData()">Turn-In Slip</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataCTR()">CTR Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataRAR()">Return and Receipt of the Property/Equipment Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataRARSEP()">Property Return Slip Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataReturnPPE()">Property Return Slip Form</a></li>
<%--                                <li ><a id="turninreturnbtn" style="cursor: pointer">Turn-In Slip With Certificate</a></li>
                                <li><a id="rrspinreturnbtn" style="cursor: pointer" hidden>Receipt of Returned Semi-Expendable Property Form With Certificate</a></li>--%>
                                <li><a id="rrspxcl" style="cursor: pointer" >Property Return Slip Excel </a></li>
                            </ul>
                        </div>
                    </div>

                    <div class="col-lg-9" id="showorhide">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="AFupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>
                        <div class="col-lg-3">
                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete" onclick="UploadPropertyReturn()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="AssetListRequest" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span></span>Property Return List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="TextBox1" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Return Request No:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblReturnRequestNumberRequest" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Date Requested:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDateRequestedRequest" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Accountable Person:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblAccountablePersonRequest" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label class="control-label">
                                Remarks:
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-9">
                            <textarea class="form-control" id="txtRemarksRequest" runat="server"></textarea>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPURLRequest" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>DESCRIPTION</td>
                                        <td>SERIAL #</td>
                                        <td>CLASSIFICATION</td>
                                        <td>CATEGORY</td>
                                        <td>BRAND</td>
                                        <td>MODEL</td>
                                        <td>REASON</td>
                                        <td>STATUS</td>
                                    </tr>
                                </thead>
                                <tbody id="purlRequest">
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
                            <ul class="dropdown-menu" role="menu" id="printGrid1">
                                <li><a style="cursor: pointer" onclick="PrintGridData()">Turn-In Slip</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataCTR()">CTR Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataRAR()">Return and Receipt of the Property/Equipment Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataRARSEP()">Property Return Slip Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataReturnRequestPPE()">Return Slip for Property, Plant and Equipment</a></li>
<%--                                <li hidden><a id="returnrequestturninbtn" style="cursor: pointer">Turn-In Slip With Certificate</a></li>
                                <li hidden><a id="returnrequestrrspinbtn" style="cursor: pointer">Receipt of Returned Semi-Expendable Property Form With Certificate</a></li>--%>
                                <li><a id="rrspxcl1" style="cursor: pointer">Property Return Slip Excel </a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9" id="showorhide1">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="FileUpload1" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>
                        <div class="col-lg-3">
                            <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete1" onclick="UploadPropertyReturn1()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div id="AssetList1" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn-danger pull-right" data-dismiss="modal" aria-hidden="true"><span class='fa fa-times-circle'></span></button>
                    <h4>
                        <p style="color: white;"><span></span>Return Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <input type="text" id="txtTIN1" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Transaction No:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblTransactionNo1" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Date Requested:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblDateRequested1" type="text" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            Accountable Person:
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <label id="lblPerson1" type="text" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3 col-xs-4">
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
                                        <td>CONDITION</td>
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

    <div id="spin" style="display: none">
        <center style="position: absolute; top: 50%; left: 50%; transform: translateX(-50%) translateY(-50%); background: #222; opacity: 0.7; z-index: 1000; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center;">
            <h1>
                <div id="please-wait">Please wait</div>
                <br />
                <span class="fa fa-gear fa-spin"></span>
            </h1>
        </center>
    </div>
    <div id="ReturnList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;"><span class="fa fa-edit"></span>List of Property for Return</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                    By clicking the confirm button, the selected property/iess will be sent for Property Return Request. Make sure that the list is correct.</i>
                                </p></center>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblReturnList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;">
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
                                <tbody id="returnlist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success" id="btnAssetItemTransfer" onclick="ConfirmTurnIn()">&nbsp;&nbsp;Confirm&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="RemoveAllToTurnIn()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <div id="UploadDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn-danger pull-right" data-dismiss="modal" aria-hidden="true"><span class='fa fa-times-circle'></span></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Upload Return Slip</p>
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
                                        <label id="lbl_from">Received From</label></td>
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
                                        <label id="lbl_by">Received By</label></td>
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
