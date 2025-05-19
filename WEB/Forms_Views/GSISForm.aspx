<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="GSISForm.aspx.cs" Inherits="Views_GSIS_Form" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/GSISForm.js"></script>
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
        <h1>GSIS Form
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Views</li>
            <li>GSIS Form</li>
        </ol>
    </section>

    <section class="content">
        <%--ADVANCE FILTER--%>
       <%-- <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Advance Filter</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
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
                                        <input type="button" class="btn btn-primary" style="width: 100%" id="btnAddFilter" value="Add Filter" onclick="SaveFilter('StockCard')" />
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
        </div>--%>
        <%--END OF ADVANCE FILTER--%>
         <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">Filter</h3></center>
            </div>
             <div class="box-body">
                 <br />
                 <div class="row" style="display: block">
                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                         <div class="col-lg-1 col-md-4 col-sm-12 col-xs-12">
                             <asp:Label ID="lbldisplaySite" runat="server" Text="Site"></asp:Label>
                         </div>
                         <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                                 <select id="ddlWarehouse" onchange="loaddata();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                         </div>
                         <div class="col-lg-1 col-md-4 col-sm-12 col-xs-12">
                             <label class="control-label" align="left">
                                 Property Type Cost:
                             </label>
                         </div>
                         <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                                <%--<select id="ddlPropertyCost" onchange="LoadDatatable();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>--%>
                                <asp:DropDownList ID="ddlItem"  onchange="loaddata();" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                         </div>


                         <div class="col-lg-1 col-md-4 col-sm-12 col-xs-12">
                             <input type="checkbox" id="chkPropertyfloater" onchange="AbleDisableFilter()" class="switch_1 pull-right">
                         </div>
                         <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                             <label class="control-label" align="left">
                                 Property Floater
                             </label>
                         </div>

                     </div>
                 </div>
             </div>
          
        </div>

        <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">List of Insurable Assets</h3></center>
            </div>
           <%-- <div class="box-body">
                <br/>
                <div class="row" style="display: block">
                    <div class="col-lg-1">
                        <label class="control-label" align="left">
                            &emsp;&nbsp;&nbsp;  User Department:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                        <div id="HTPIssuanceDept" class="input-group">
                            <select id="ddlDepartmentPI" onchange="GetEmployeePI();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                            <span id="HTPIssuanceDeptC" class="btn btn-success input-group-addon" onclick="addDepartmentDialogBox('PI')"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <label class="control-label" align="left">
                            Employee:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                        <div id="HTPIssuanceEmployee" class="input-group">
                            <select id="ddlEmployeePI" onchange="LoadDatatable();" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                            <span id="HTPIssuanceEmployeeC" class="btn btn-success input-group-addon" onclick="addUserDialogBox1('PropertyIssuance')"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="box-body">

                <div class="row" style="padding-left: 20px; padding-right: 20px;">
                    <div class="col-lg-12">
                        <table class="table table-responsive table-striped table-bordered" id="tblItemList" style="width: 100%;">
                            <thead class="info">
                                <tr style="background-color: #D9EDF7;">
                                    <th>ITEM CODE</th>
                                     <th>CLASSIFICATION</th>
                                    <th>ITEM DESCRIPTION</th>
                                    <th>SITE</th>
                                    <th>LOCATION</th>
                                    <th>AREA</th>
                                    <th>AQUIRED DATE</th>
                                    <th>PROPERTY NUMBER</th>
                                    <th>AQUISITION COST</th>
                                </tr>
                            </thead>
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
                            <ul class="dropdown-menu" role="menu"  max-height: 100px;">
                                <li><a id="PrintExcel" style="cursor: pointer">GSIS Form (Excel)</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </section>
    <!-- Print -->

    <div class="row" id="printablepage" runat="server" hidden>
        <div class="row" id="divStockCard" runat="server">
        </div>
    </div>
</asp:Content>

