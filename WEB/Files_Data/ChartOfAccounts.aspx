<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChartOfAccounts.aspx.cs" Inherits="Files_Data_ChartOfAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/ChartOfAccounts.js"></script>
    <script type="text/javascript" lang="Javascript">


        function isNumberKey1(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtBalance").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function formatCurrencyInputCOA(input) {
            let value = input.value.replace(/[^a-zA-Z0-9-.,]/g, '');

            input.value = value;
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
     <section class="content-header">
        <h1>Chart Of Accounts
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Chart Of Accounts</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-9 col-md-9 col-sm-8 col-xs-2"></div>
                <div class="col-lg-3 col-md-3 col-sm-8 col-xs-8" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddChartOfAccounts" type="button" value="Add Chart Of Accounts" onclick="addChartOfAccountsDialogBox()" />
                </div>
            </div>
        </div>
        <br />

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Chart Of Accounts</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblChartOfAccounts" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ACCOUNT NUMBER</td>
                                <td>DESCRIPTION</td>
                                <td>ACCOUNT HEADER</td>
                                <td>BALANCE</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="chartofaccountslist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteChartOfAccounts" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Chart Of Accounts</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Chart Of Accounts?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveDepartment()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteBrand" onclick="DeleteChartOfAccounts(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="addChartOfAccountsDialogBox" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear()"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit Chart Of Accounts</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-9">
                             <label class="control-label" align="left">
                                Account Number:
                            </label>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                            <input id="txtAccountNumber" maxlength="50" type="text" onkeyup="return formatCurrencyInputCOA(this);return ReplaceStr2(this);" <%--placeholder="Account Number"--%> style="width: 100%" class="form-control" />
                        </div>
                      <%--  <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                                Description:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                            <textarea class="form-control" id="txtDescription" <%--placeholder="Description"--%> rows="5" cols="1" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,500,'cnt', this.id);return ReplaceStr2(this);"></textarea>
                            <h6>
                                <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                                Account Header:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                            <asp:DropDownList ID="ddlAccountHeader" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                     <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label" align="left">
                                Balance:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                            <input id="txtBalance" maxlength="50" type="text" <%--placeholder="Balance (Amount Format: 0.00)"--%> style="width: 100%" class="form-control" onkeypress="return isNumberKey1(event,this)" ondrop="return false;" onpaste="return false;" />
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>--%>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">

                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                           <%-- <input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left" /><label class="pull-left">&nbsp;&nbsp; Active</label>--%>
                            <input type="checkbox" class="switch_1 pull-left" id="chkActive" />
                            <label class="pull-left">&nbsp;&nbsp; Active</label>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="SaveCancelCOA">
                    <button type="button" class="btn btn-success btn_EditChartOfAccounts" id="btnSaveBrand" onclick="SaveChartOfAccounts()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;Cancel&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        //iCheck for checkbox and radio inputs

    </script>
</asp:Content>

