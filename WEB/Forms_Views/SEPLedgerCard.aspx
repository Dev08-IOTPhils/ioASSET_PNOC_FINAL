<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="SEPLedgerCard.aspx.cs" Inherits="Views_SEPLedgerCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/SEPLedgerCard.js"></script>
    <script>
        function PrintReport() {
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
        <h1>Semi-Expendable Property Ledger Card
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Views</li>
            <li>Semi-Expendable Property Ledger Card</li>
        </ol>
    </section>

    <section class="content">
        <%--ADVANCE FILTER--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Advance Filter</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>


            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        Logic:
                                <div class="select2-blue">
                                    <select class="select2" id="drpLogic" data-placeholder="Select a Logic" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        <option>Select Logic </option>
                                        <option>AND </option>
                                        <option>OR </option>
                                    </select>
                                </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        Filter By:
                                <div class="form-group">
                                    <asp:DropDownList ID="drpFilterBy" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>

                                </div>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        Operator:
                                <div class="select2-blue">
                                    <select class="select2" id="drpOperator" data-placeholder="Select an Operator" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        <option selected="selected">Select Operator</option>
                                        <option>= </option>
                                        <option>> </option>
                                        <option>>= </option>
                                        <option>< </option>
                                        <option><= </option>
                                        <option>Not = </option>
                                        <option>Like </option>
                                        <option>In </option>
                                        <option>Not In </option>
                                    </select>
                                </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        Value:
                                <div class="form-group">
                                    <input type="text" style="width: 100%" placeholder="Value" min="0" id="txtValue" name="txtValue" class="form-control" />
                                    <%--<span id="spandropdown">
                                        <select class="select2" id="drpValue" data-placeholder="Select an Operator" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        </select>
                                    </span>
                                    <span id="MultiSelect1">
                                        <select class="select2" multiple="multiple" id="multiSelect" data-placeholder="Enter Value" data-dropdown-css-class="select2-blue" style="width: 100%;">
                                        </select>
                                    </span>--%>
                                </div>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        &nbsp
                                    <div class="form-group">
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('SEP')" />
                                    </div>
                    </div>

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                        <table id="tblFilterDisplay" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                            <thead style="font-weight: bold">
                                <tr>
                                    <td class="dt-body-center">Logic</td>
                                    <td class="dt-body-center">Filter By</td>
                                    <td class="dt-body-center">Operation</td>
                                    <td class="dt-body-center">Value</td>
                                    <td class="dt-body-center">Active</td>
                                    <td class="dt-body-center">Action</td>
                                </tr>
                            </thead>
                            <tbody id="FilterList">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <%--END OF ADVANCE FILTER--%>
        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Ledger Card</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                            <%--<input type="text" class="form-control" id="txtSearch" placeholder="Enter Issuance detail" onkeypress="return EnterKeySearch(event)" />--%>
                            <%--<span class="input-group-btn">
                                <button type="button" class="btn btn-primary btn-flat" onclick="load_data()">Search</button>
                            </span>--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12  col-md-6 col-sm-12 col-xs-12">
                    <table id="tblLedgerCardList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>PROPERTY NUMBER</td>
                                <td>CLASSIFICATION</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>MODEL</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="ledgercardlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

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
                        <%--<div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label>Return Type:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlReturnType" class="form-control" onchange="UpdateReturnType()" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>--%>
                    </div>
                    <%--<div class="row">
                        <div class="col-lg-2 col-md-1 col-sm-3 col-xs-4">
                            <label class="control-label">
                                <h6><b>Remarks:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-9 col-xs-9">
                            <textarea class="form-control" id="txtRemarks" runat="server"></textarea>
                        </div>
                    </div>--%>
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
                        <%--<button type="button" class="btn btn-sm btn-success pull-left" id="btnPrint" onclick="PrintGridData()">&nbsp;&nbsp;Print&nbsp;&nbsp;</button>--%>
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
                                <li><a style="cursor: pointer" onclick="PrintGridDataRARSEP()">Receipt of Returned Semi-Expendable Property Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataReturnPPE()">Return Slip for Property, Plant and Equipment</a></li>
                                <li><a id="turninreturnbtn" style="cursor: pointer">Turn-In Slip With Certificate</a></li>
                                <li><a id="rrspinreturnbtn" style="cursor: pointer">Receipt of Returned Semi-Expendable Property Form With Certificate</a></li>
                                <li><a id="rrspxcl" style="cursor: pointer">Receipt of Returned Semi-Expendables Property Excel </a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9" id="showorhide">
                        <%--<asp:Button ID="btnComplete" type="button" class="btn btn-sm btn-primary pull-right" runat="server" Text="Complete Transaction" OnClick="btnUpload_Click" />--%>
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete" onclick="UploadPropertyReturn()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        <%--<asp:Button ID="btnUpload" type="button" class="btn btn-sm btn-primary pull-right" runat="server" Text="Upload" OnClientClick="Close()" OnClick="btnUpload_Click" />--%>
                        <asp:FileUpload ID="AFupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 30%;" />
                        <%--<label class="pull-right">&nbsp;With Supporting Document?</label><input type="checkbox" id="chkWithDoc" class="switch_1 pull-right" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Print -->
    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divReport" runat="server">
        </div>
    </div>
</asp:Content>
