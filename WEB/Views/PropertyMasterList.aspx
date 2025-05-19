<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyMasterList.aspx.cs" Inherits="Views_PropertyMasterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyMasterlist.js"></script>
    <%--<script src="../Script/AdvanceFilter.js"></script>--%>
    <style>
        button.buttons-columnVisibility a {
            cursor: pointer;
        }

        button.buttons-columnVisibility.active a {
            background-color: transparent;
            color: inherit;
        }

            button.buttons-columnVisibility.active a:hover {
                background-color: #f5f5f5;
                color: inherit;
            }

        button.buttons-columnVisibility a:before {
            width: 25px;
            font-family: "Glyphicons Halflings";
            content: "\e013";
            color: transparent;
            padding-right: 10px;
        }

        button.buttons-columnVisibility.active a:before {
            color: inherit;
        }

        table.dataTable tbody td {
            word-break: break-word;
            vertical-align: top;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Property Masterlist
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-eye"></i>Views</li>
            <li>Property Masterlist</li>
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
                                </div>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                        &nbsp
                                    <div class="form-group">
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('PropertyMasterlist')" />
                                    </div>
                    </div>
                    
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                        <table id="tblFilterDisplay" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout:auto">
                            <thead style="font-weight: bold">
                                <tr>
                                    <td class="dt-body-center">Logic</td>
                                    <td class="dt-body-center">Filter By</td>
                                    <td class="dt-body-center">Operation</td>
                                    <td class="dt-body-center">Value</td>
                                    <td class="dt-body-center active">Active</td>
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



        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Masterlist</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12 col-md-6 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div>
                        </div>
                    </div>
                </div>
                <table id="tblAssetList" class="table table-bordered table-hover table-condensed display" style="table-layout: auto;">
                    <thead style="font-weight: bold">
                        <tr>
                                                        <td>PROPERTY #</td> 
                            <td>ITEM CODE</td>
                            <td>DESCRIPTION</td>
                                                        <td>LONG DESCRIPTION</td>
                                                        <td>UNIT</td>
                                                        <td>SERIAL #</td>
                                                        <td>ISSUED TO/ASSIGNED TO</td>
                                                        <td>STATUS</td>
                                                        <td><asp:Label ID="lblMasterListSite" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                                        <td><asp:Label ID="lblMasterListLoc" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                                        <td><asp:Label ID="lblMasterListArea" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                                        <td>CUSTODIAN TYPE</td>
                                                        <td>CATEGORY</td>
                                                        <td>CLASSIFICATION</td>
                                                        <td>BRAND</td>
                                                        <td>MODEL</td>
                            <td>USER DEPARTMENT</td>
                            <td>TYPE COST</td>
                            <td>PR #</td>
                            <td>PO #</td>
                            <td>INVOICE #</td>
                            <td>DR #</td>
                            <td>ISSUANCE #</td>
                            <td>ACQUIRED DATE</td>
                            <td>USEFUL LIFE</td>
                            <td>REMAINING USEFUL LIFE</td>
                            <td>NEXT MAINTENANCE DATE</td>
                            <td>WARRANTY DATE</td>
                            <td>INSURANCE DATE</td>
                            <td>EXPIRY DATE</td>
                            <td>ACQUIRED COST</td>
                            <td>DEPRECIATED COST</td>
                            <td>DISPOSED DATE</td>
                            <td>ISSUED DATE</td>
                            <td>LAST MAINTENANCE DATE</td>
                            <td>LAST INVENTORY NAME</td>
                            <td>LAST INVENTORY DATE</td>
                            <td>LAST INVENTORY STATUS</td>
                            <td>LAST INVENTORY SITE</td>
                            <td>LAST INVENTORY LOCATION</td>
                        </tr>
                    </thead>
                    <tbody id="assetlist">
                    </tbody>
                </table>
                <%--</div>--%>
            </div>
        </div>
    </section>
    
     <div id="editFilterBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4>
                        <p style="color: white;">Edit Filter</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                            <label class="control-label" align="left">
                                Logic:
                            </label>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:TextBox ID="drpLogic2" placeholder="Location" MaxLength="50" runat="server" type="text" Style="width: 100%" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                    <br />
                    
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                            <label class="control-label" align="left">
                                Filter By:
                            </label>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:TextBox ID="drpFilterBy2" placeholder="Location" MaxLength="50" runat="server" type="text" Style="width: 100%" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                    <br />
                    
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                            <label class="control-label" align="left">
                                Operator:
                            </label>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:TextBox ID="drpOperator2" placeholder="Location" MaxLength="50" runat="server" type="text" Style="width: 100%" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                    <br />
                    
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">
                             <label class="control-label" align="left">
                                Site:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:DropDownList ID="ddlWarehouse" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-4">

                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <%--<input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left" /><label class="pull-left">&nbsp;&nbsp; Active</label>--%>
                            <input type="checkbox" class="switch_1 pull-left" id="chkActive" />
                            <label class="pull-left">&nbsp;&nbsp; Active</label>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnSaveLocation" onclick="SaveLocation()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    <%--<asp:Button ID="btnsave" UseSubmitBehavior="false" runat="server" class="btn btn-success  pull-left" OnClick="Upload" OnClientClick="SaveLocation();" Text="Save" />--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

