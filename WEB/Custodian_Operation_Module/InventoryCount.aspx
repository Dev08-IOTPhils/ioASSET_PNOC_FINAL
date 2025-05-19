<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryCount.aspx.cs" Inherits="Operation_Module_InventoryCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyInventoryProcess.js"></script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8);  //Backspace
        specialKeys.push(9);  //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right

        function IsAlphaNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }

        function uploadSuccess() {
            swal("Success", "Inventory Count Transaction Complete!", "success");
            preventDefault();
        }

        function selectfile() {
            swal("Error", "Please Select a file to upload", "error");
            preventDefault();
        }

        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                if (charCode == 13) {
                    return false;
                    preventDefault();
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function PrintGridDataProperty() {
            var prtGrid = document.getElementById('<%=printpageProperty.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        
        function PrintGridDataPropertyFound() {
            var prtGrid = document.getElementById('<%=printpagePropertyFound.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataPropertyNotFound() {
            var prtGrid = document.getElementById('<%=printpagePropertyNotFound.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataPropertyMisplaced() {
            var prtGrid = document.getElementById('<%=printpagePropertyMisplaced.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataPropertyRCPPE() {
            var prtGrid = document.getElementById('<%=printpagePropertyRCPPE.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        
        function PrintGridDataPropertyICF() {
            var prtGrid = document.getElementById('<%=printpagePropertyICF.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataPropertyRCPSEP() {
            var prtGrid = document.getElementById('<%=printpagePropertyRCPSEP.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }

        function PrintGridDataPropertyRCPSEPH() {
            var prtGrid = document.getElementById('<%=printpagePropertyRCPSEPH.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataStock() {
            var prtGrid = document.getElementById('<%=printpageStock.ClientID %>');
            var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.innerHTML);
            prtwin.document.close();

            setTimeout(function () {
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }, 350);
        }
        function PrintGridDataPropertyROCI() {
            var prtGrid = document.getElementById('<%=printpageStockROCI.ClientID %>');
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
        <h1>Inventory Count
           <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Custodian Operation Module</li>
            <li>Inventory Count</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-lg-12">
                <center><p style="font-size:12px;"><i>You can start an Inventory count process below. Make sure the location, department and inventory type are correct for the system to generate the proper inventory list to be accounted for.
                </i></p></center>
            </div>
        </div>
        <div id="GoodsReceive" class="box box-solid box-primary" style="width: 100%;">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Inventory Count Details</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b>Inventory Name:</b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <input id="txtInventoryName" class="form-control" <%--placeholder="Inventory Name"--%> onkeypress="return IsAlphaNumeric(event);" ondrop="return false;" onpaste="return false;"></input>
                        <span id="error" style="color: Red; display: none">* Special Characters not allowed.</span>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><span style="color: red">*</span><b>Custodian Type:</b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <%--<select id="ddlCustodian" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>--%>
                        <select multiple="multiple" id="ddlCustodian" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35%" data-placeholder="    --- Select Custodian Type ---"></select>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b><asp:Label ID="lbldisplaySite" runat="server" Text="Label"></asp:Label></b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">

                        <div id="HTITRegion" class="input-group">
                            <select id="ddlWarehouseIC" class="form-control select2" onchange="GetLocationIC();" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                            <span id="HTITRegionC" class="btn btn-success input-group-addon" onclick="addWarehouseDialogBox('InventoryCount')"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b><asp:Label ID="lbldisplayLoc" runat="server" Text="Label"></asp:Label></b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <div id="HTITBranch" class="input-group">
                            <select id="ddlLocationIC" onchange="GetOfficeIC()" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                            <span id="HTITBranchC" class="btn btn-success input-group-addon" onclick="addLocationDialogBox('InventoryCount')"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b><asp:Label ID="lbldisplayArea" runat="server" Text="Label"></asp:Label></b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">                        
                        <div id="HTITOffice" class="input-group">
                            <select id="ddlOfficeIC" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                            <span id="HTITOfficeC" class="btn btn-success input-group-addon" onclick="addAreaDialogBox('InventoryCount')"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b>Inventory Type:</b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <asp:DropDownList ID="ddlInventoryType" class="form-control select2" onchange="inventory_type();" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row" hidden>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b>Property Type Cost:</b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <asp:DropDownList ID="ddlItem" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            <h5><b>Description:</b></h5>
                        </label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <textarea class="form-control" id="txtDescription" rows="2" cols="1" style="width: 100%; height: 100px"></textarea>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                        <label class="control-label" align="left"></label>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <input id="btnSave" class="btn btn-sm btn-primary btn_AddInvCount" type="button" value="Create" onclick="SaveInventory()" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-danger" onclick="Clear();">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary" id="divPending">
            <%-- style="display: none"--%>
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Inventory List</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblInventoryList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>INVENTORY NAME</td>
                                <td>DESCRIPTION</td>
                                <td><asp:Label ID="lblListSite" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td><asp:Label ID="lblListLoc" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td><asp:Label ID="lblListArea" runat="server" Text="Label" style=" text-transform: uppercase;"></asp:Label></td>
                                <td>INVENTORY TYPE</td>
                                <td>DATE CREATED</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="inventorylist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="row" id="printpageProperty" runat="server" hidden>
            <div class="row" id="divPropertyPrintList" runat="server">
            </div>
        </div>
        
        <div class="row" id="printpagePropertyFound" runat="server" hidden>
            <div class="row" id="divPropertyPrintListFound" runat="server">
            </div>
        </div>
        <div class="row" id="printpagePropertyNotFound" runat="server" hidden>
            <div class="row" id="divPropertyPrintListNotFound" runat="server">
            </div>
        </div>
        <div class="row" id="printpagePropertyMisplaced" runat="server" hidden>
            <div class="row" id="divPropertyPrintListMisplaced" runat="server">
            </div>
        </div>
        <div class="row" id="printpagePropertyRCPPE" runat="server" hidden>
            <div class="row" id="divPropertyPrintListRCPPE" runat="server">
            </div>
        </div>
        
        <div class="row" id="printpagePropertyRCPSEP" runat="server" hidden>
            <div class="row" id="divPropertyPrintListRCPSEP" runat="server">
            </div>
        </div>
          <div class="row" id="printpagePropertyRCPSEPH" runat="server" hidden>
            <div class="row" id="divPropertyPrintListRCPSEPH" runat="server">
            </div>
        </div>
        <div class="row" id="printpagePropertyICF" runat="server" hidden>
            <div class="row" id="divPropertyPrintListICF" runat="server">
            </div>
        </div>
        <div class="row" id="printpageStock" runat="server" hidden>
            <div class="row" id="divStockPrintList" runat="server">
            </div>
        </div>
        <div class="row" id="printpageStockROCI" runat="server" hidden>
            <div class="row" id="divStockPrintListROCI" runat="server">
            </div>
        </div>
    </section>

    <div id="inventorylistmodal" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List for Inventory</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtInventoryCode" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                Location:
                            </label>
                            <label id="InventoryLocation" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" align="right">
                            <input type="text" placeholder="Enter Barcode Number" class="form-control" id="txtBarcode" onkeypress="return EnterBarcode(event)" />
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Add Property" onclick="addRow('tbl_PropertyInventoryList')" />
                        </div>
                    </div>
                    <br />
                    <div class="box box-solid box-primary">
                        <div class="box-header with-border">
                            <center><h3 class="box-title">List of Property/ies</h3></center>
                        </div>
                        <div class="box-body">
                            <div class="divContentBody" id="PropertyInventoryListDetails" runat="server"></div>
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
                            <ul class="dropdown-menu" role="menu" id="printGrid" style="overflow: scroll; max-height:100px;">
                                <li><a style="cursor: pointer" onclick="PrintGridDataProperty()">Inventory Property List Form</a></li>
                                <li><a id="iplxcl" style="cursor: pointer">Inventory Property List Form (Excel)</a></li>
                                <li><a id="icfxcl" style="cursor: pointer">Inventory Count Form (Excel)</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyICF()">Inventory Count Form</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyFound()">Inventory Property List for Found Property Form</a></li>
                                <li><a id="iplFxcl" style="cursor: pointer">Inventory Property List for Found Property Form (Excel)</a></li>

                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyNotFound()">Inventory Property List for Not Found Property Form</a></li>
                                <li><a id="iplNFxcl" style="cursor: pointer">Inventory Property List for Not Found Property Form (Excel)</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyMisplaced()">Inventory Property List for Unlocated/Missing Property Form</a></li>
                                <li><a id="iplMxcl" style="cursor: pointer">Inventory Property List for Unlocated/Missing Property Form (Excel)</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyRCPPE()">Report on the Physical Count of Property, Plant and Equipment Form</a></li> 

                                <li><a id="rcppexcl" style="cursor: pointer">Report on the Physical Count of Property, Plant and Equipment (Excel)</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyRCPSEP()">Report on the Physical Count of Semi-Expendable Property Form (ICS HIGH)</a></li>
                                <li><a id="rpcspxcl" style="cursor: pointer">Report on the Physical Count of Semi-Expendable Property (ICS HIGH) (Excel)</a></li>
                                <li><a style="cursor: pointer" onclick="PrintGridDataPropertyRCPSEPH()">Report on the Physical Count of Semi-Expendable Property Form (ICS HIGH & ICS LOW)</a></li>
                                <li><a id="rpcshpxcl" style="cursor: pointer">Report on the Physical Count of Semi-Expendable Property (ICS HIGH & ICS LOW) (Excel)</a></li>

                                <li><a style="cursor: pointer" id="excelbtnppe" class="hide">Report on the Physical Count of Property, Plant and Equipment Form (Word)</a></li>
                                <li><a style="cursor: pointer" id="excelbtnsemi" class="hide">Report on the Physical Count of Semi-Expendable Property Form (Word)</a></li>
                                <li><a  style="cursor: pointer" id="btnPrintGridDataProperty">Inventory Property List With Certificate</a></li>
                                <li><a  style="cursor: pointer" id="btnPrintGridDataPropertyRCPPE" class="hide">Report on the Physical Count of Property, Plant and Equipment With Certificate</a></li>
                                <li><a  style="cursor: pointer" id="btnPrintGridDataPropertyRCPSEP" class="hide">Report on the Physical Count of Semi-Expendable Property With Certificate</a></li>   
                                
                            </ul>
                        </div>
                    </div>

                                        <div class="col-lg-9" id="showorhide">
                        <div class="col-lg-9">
                            <asp:FileUpload ID="AFupload" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 50%;" />
                        </div>
                        <div class="col-lg-3">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete" onclick="UploadInventoryCount()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="inventorylistmodal1" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">List for Inventory</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtInventoryCode1" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="box box-solid box-primary">
                            <div class="box-header with-border">
                                <center><h3 class="box-title">List of Supply/Supplies</h3></center>
                            </div>
                            <div class="box-body">
                                <div class="divContentBody" id="StockInventoryListDetails" runat="server"></div>
                            </div>
                            <br />
                            <br />
                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-success pull-left">Print</button>
                                <button type="button" class="btn btn-success dropdown-toggle pull-left" data-toggle="dropdown">
                                    <span class="caret"></span>
                                    <span class="sr-only">Toggle Dropdown</span>
                                </button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a style="cursor: pointer" onclick="PrintGridDataStock()">Inventory Supply List Form</a></li>
                                    <li><a style="cursor: pointer" onclick="PrintGridDataPropertyROCI()">Report On the Physical Count of Inventories Form</a></li>
                                    <li><a id="rpcixcl" style="cursor: pointer">Report On The Physical Count of Inventories (Excel)</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-primary pull-right" id="btnComplete1" onclick="UploadInventoryCountStock()">&nbsp;&nbsp;Complete Transaction&nbsp;&nbsp;</button>
                    <asp:FileUpload ID="AFupload1" class="btn btn-sm btn-primary pull-right" AllowMultiple="true" runat="server" Style="width: 30%;" />
                </div>
            </div>
        </div>
    </div>
    <div class="row" hidden>
        <asp:TextBox ID="txtInventoryType" runat="server" type="text" class="form-control" Style="width: 100%" />
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
</asp:Content>
