<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DayPass.aspx.cs" Inherits="Operation_Module_DayPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/DayPass.js"></script>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Personal Property Gate Pass Request
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Personal Property Gate Pass Request</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div class="row">
        </div>
        <div id="GoodsReceive" class="box box-solid box-primary" style="width: 100%;">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Personal Property Gate Pass Request Details</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Serial Number:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtSerialNumber" onchange="OCSerial();" type="text" <%-- placeholder="Serial Number" --%>class="form-control" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                            <label class="control-label">
                                Category:
                            </label>
                        </div>


                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtCategory" type="text" <%--placeholder="Category"--%> class="form-control" />
                        </div>

                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Brand:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <input id="txtBrand" type="text" <%--placeholder="Brand"--%> class="form-control" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                            <label class="control-label">
                                Description:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <textarea class="form-control" id="txtDescription" runat="server" style="max-width: 100%"></textarea>
                            <%--placeholder="Description"--%>
                        </div>

                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                            <label class="control-label">
                                Image:
                            </label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <asp:FileUpload ID="daypassImage" onchange="getImage(event)" class="form-control" placeholder="Please Select .jpg, .jpeg file." runat="server" Style="width: 100%; height: 35px" />
                        </div>

                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                           
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                           
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                            
                        </div>
                               
                        <div class="col-lg-1 col-md-1 col-sm-0 col-xs-0">
                           
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="PropertyImage" runat="server">
                            
                        </div>
                        
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                            <label class="control-label" align="left"></label>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <input id="btnSave" class="btn btn-sm btn-success btn_AddDayPass" type="button" value="Save" onclick="PropertyAddToDayPass()" />
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <button type="button" class="btn btn-sm btn-danger" onclick="Clear();">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property List</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-2 col-sm-10 col-xs-12">
                            <label class="control-label">
                                Purpose:
                            </label>
                        </div>
                         <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12">
                            <textarea class="form-control" id="txtPurpose" runat="server"></textarea> <%-- placeholder="Purpose"--%>
                        </div>
                        
                            <div class="col-lg-1 col-md-1 col-sm-10 col-xs-12">
                           
                        </div>
                        <div class="col-lg-1 col-md-2 col-sm-0 col-xs-0" id="labelApprover1">
                             <label class="control-label">
                                Approver:
                            </label>
                        </div>

                    
                        <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12" id="labelApprover">
                            
                            <div id="HTSDayPassApprover" class="input-group">
                                    <select id="ddlApproverDP" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                    <span id="HTSDayPassApproverC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('DayPass')"><i class="fa fa-plus"></i></span>
                             </div>
                        </div>
                       
                    </div>
                </div>
                <br />
                <div class="col-lg-12">
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>SERIAL #</td>
                                <td>CATEGORY</td>
                                <td>BRAND</td>
                                <td>DESCRIPTION</td>
                                <td>IMAGE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="assetlist">
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="box-footer">
                <input id="btnConfirm" class="btn btn-sm btn-success center-block btn_AddDayPass" type="button" value="Confirm Day Pass Request" onclick="ConfirmDayPass()" />
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Personal Property Gate Pass Request Pending Approval</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblPendingUpload" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
                        <thead style="font-weight: bold">
                            <tr>
                                <td style="width: 10%">PERSONAL PASS #</td>
                                <td style="width: 20%">DATE</td>
                                <td style="width: 45%">PURPOSE</td>
                                <td style="width: 15%">STATUS</td>
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
            <div class="row" id="divDayPassForm" runat="server">
            </div>
        </div>
        <div id="modalPropertyList" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p><span></span>Day Pass Request Property List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 table-responsive">
                            <table id="tblPAL" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%">
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
                    <div class="btn-group pull-left">
                            <button type="button" class="btn btn-success pull-left">Print</button>
                            <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu" id="printGrid">
                                <li><a onclick="PrintGridData()">Personal Property Gate Pass Form</a></li>
                            </ul>
                        </div>
                </div>
            </div>
        </div>
    </div>
    </section>
</asp:Content>

