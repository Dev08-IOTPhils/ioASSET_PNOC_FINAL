<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropertyMaintenance.aspx.cs" Inherits="Operation_Module_PropertyMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/PropertyMaintenance.js"></script>
    <script type="text/javascript" lang="Javascript">
        function uploadSuccess() {
            swal('Succes', 'Uploading Successful!', 'success');
            preventDefault();
            window.location.href = window.location.href;
        }

        function uploadSuccess1(a) {
            alert(a);
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
        <h1>Property Maintenance
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-gear"></i>Operation Module</li>
            <li>Property Maintenance</li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">Property Available for Maintenance</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                     <center><p style="font-size:12px;"><i>Properties displayed below are available for Maintenance. Below items can be nearing scheduled Preventive Maintenance, or Corrective Maintenance requested by employees. You may select property below and assign it to a maintenance personnel or 3rd party maintenance provider.
                    </i></p></center>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div>
                        <div class="input-prepend input-group">
                            <span class="add-on input-group-addon">Date From</span>
                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateFrom" class="form-control" readonly="readonly"/>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div>
                        <div class="input-prepend input-group">
                            <span class="add-on input-group-addon">Date To</span>
                            <input type="text" style="width: 100%" placeholder="mm/dd/yyyy" id="txtDateTo" class="form-control" readonly="readonly"/>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12"></div>
                <br />
                <div class="col-lg-12">
                <br />
                <br />
                <br />
                    <table id="tblAssetList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>ITEM CODE</td>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>SERIAL #</td>
                                <td>DATE OF MAINTENANCE</td>
                                <td>MAINTENANCE LEAD TIME</td>
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
                <center><h3 class="box-title">Property for Maintenance</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                     <center><p style="font-size:12px;"><i>All properties that are currently on maintenance  will be displayed here. Current stage or maintenance tasks to be done can also be seen here.
                    </i></p></center>
                </div>
                <div class="col-lg-8 col-md-6 col-sm-12 col-xs-12"></div>
                <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
                </div>
                <div class="col-lg-12">
                    <table id="tblPropertyOnMaintenance" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>PROPERTY #</td>
                                <td>DESCRIPTION</td>
                                <td>SERIAL #</td>
                                <td>CREATED BY</td>
                                <td>CREATED DATE</td>
                                <td># OF DAYS</td>
                                <td>NEXT STEP</td>
                                <td>% OF COMPLETION</td>
                                <td>REMARKS</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="propertyonmaintenance">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>
    <%--Property Add To Maintenance--%>
    <div id="ModalToMaintenance" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color:white;"><span></span>Add To Maintenance List</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row" hidden>
                        <asp:TextBox ID="txtRecordIDhidden" type="text" class="form-control" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtPropertyNumberhidden" type="text" class="form-control" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtTaskIDhidden" type="text" class="form-control" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtStepCurrent" type="text" class="form-control" runat="server"></asp:TextBox>
                       
                    </div>
                    <div class="row" >
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                               Selected Property:
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                <label id="lblPropertyDetails" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="row" id="divtask" style="display:inline">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                           Task:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddlTask" class="form-control select2" runat="server" Style="width: 100%; height: 35px" onchange="GetTaskID();"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="row" id="CurrentStep">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                           Current Step:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txtStep" type="text" class="form-control" runat="server"  disabled></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="row" id="nextStep">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                           Next Step:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <asp:TextBox ID="txtNextStep" type="text" class="form-control" runat="server" onkeypress="return EnterKey(event)" disabled></asp:TextBox>
                                    </div>
                                </div>
                               <%-- <br />--%>
                                <div class="row" id="divassign" style="display:inline">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                           Assign To:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <asp:DropDownList ID="ddlAssignTo" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row" style="display:inline"> <%--for send email--%>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <input type="checkbox" id="chkSendEmail" class="switch_1 pull-left" />
                                        <label class="pull-left">&nbsp;Send Email</label>
                                    </div>
                                </div>
                               <%-- <br />--%>
                                <div class="row" id="toggleAttachment">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <input type="checkbox" id="chkWithDoc" class="switch_1 pull-left" />
                                        <label class="pull-left">&nbsp;With Supporting Document?</label>
                                    </div>
                                </div>
                                <div class="row" id="hideAttachment">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                           Attachment:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <asp:FileUpload ID="AttachmentUpload" class="btn btn-sm btn-primary" AllowMultiple="true" runat="server" Style="width: 100%;" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="row" style="display:inline">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label">
                                           Remarks:
                                        </label>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                        <asp:TextBox class="form-control" ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <table id="tblSteps" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>STEP</td>
                                        <td>ASSIGN TO</td>
                                        <td>CREATED DATE</td>
                                        <td>REMARKS</td>
                                        <td>STATUS</td>
                                    </tr>
                                </thead>
                                <tbody id="steplist">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="modal-footer">
                              <button type="button" class="btn btn-success" id="btnSave" onclick="UploadMaintenance()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal" >&nbsp;Cancel&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="terminateMaintenance" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color:white;"><span ></span>Terminate Property Maintenance</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <p>Confirming termination of this property maintenance will be subject for DISPOSAL.</p>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                <label class="control-label">
                                   Remarks:
                                </label>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                <textarea class="form-control" id="txtTerminationRemarks"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnDeleteBrand" onclick="TerminatePropertyMaintenance()">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

