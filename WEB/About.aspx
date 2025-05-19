<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="Javascript">
        function RequestRegistry() {
            $("#RLDialogBox").modal("show");
        }
        function closeModal() {
            $("#RLDialogBox").modal("hide");
            swal("Success", "Request For Extension of Temporary License Generated successfully.", "success")
        }
        function ShowSSAUploadModal() {
            $('#SSAUpdateModal').modal("show");
        }
        function InvalidFile() {
            swal("Error", "Select a .dat file to upload!", "error");
        }
        function Activated() {
            swal("Success", "System Activated. Thank you!", "success");
        }

        function Extended() {
            swal("Success", "Temporary License Extended. Thank you!", "success");
        }

        function TempActivated() {
            swal("Success", "Temporary License Activated. Thank you!", "success");
        }

        function Failed() {
            swal("Error", "System Cannot activate. Please Contact Solution Provider", "error");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>About
        </h1>
    </section>
    <section class="content">
        <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">About</h3></center>
            </div>
            <div class="box-body">
                <center><%--<h3>ioASSET</h3>--%><img src="Image/ioASSET/ioASSET.png" width="30%" />
                    <h6>Powered by <a href="http://www.iotphils.com"><img src="Image/IOT/iot.png" style="width:3%" /></a></h6>
                </center>
<%--                <label>Version No. : 1.6.0 </label>--%>
                <label>Version No. : </label>
                <asp:Label ID="lblListIOVers" runat="server" Text="Label"></asp:Label>

                <br />
                <label>&copy; 2014-<%=DateTime.Now.Year %> Internet Of Things Phils. Inc. All rights reserved.</label><br />
                

<%--                Can be hidden--%>
                <label hidden>This product was build using:</label>
                <div class="row" hidden>
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-body">
                                <label>Microsoft ASP.NET C#</label><br />
                                <label>Microsoft Framework 4.5.2</label><br />
                                <label>Microsoft Visual Studio 2015 Professional</label><br />
                                <label>AdminLTE-2.4.7 Bootstrap</label><br />
                                <label>Microsoft SQL Server 2019 Express</label>
                            </div>
                        </div>
                    </div>
                </div>


<%--                Added by: Domdom pogiiiiiiiii--%>
                <br />
                <label>Contact us at</label>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-body">
<%--                                <label>Administrative Services Department</label>--%>
                                <asp:Label ID="lblContactName" runat="server" Text="Label"></asp:Label>
                                <br /><br />
                                <label>Email: </label>
                                <asp:Label ID="lblContactEmail" runat="server" Text="Label"></asp:Label>
                                <br />
                                <label>Tel: </label>
                                <asp:Label ID="lblContactNumber" runat="server" Text="Label"></asp:Label>
                                <br />
                            </div>
                            <div class="box-body" hidden>
                                <label>Technical Support</label><br />
                                <label>Email: </label>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <label>Warning:</label>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-body">
                                <p>This computer program is protected by copyright law and international treaties. Unauthorized reproduction or distribution of this program, or any portion of it, may result in severe civil and criminal penalties, and will be prosecuted to the maximum extent possible under law.</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                            <label>License Type: </label>&nbsp<label id="lblLicenseType" runat="server"></label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" align="right">
                            <input id="btnAdd" runat="server" class="btn btn-block btn-primary" type="button" value="Request For Permanent License" onclick="RequestRegistry()" />
                            <input id="btnUploadSSA" runat="server" class="btn btn-block btn-primary" type="button" value="Upload License" onclick="ShowSSAUploadModal()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="RLDialogBox" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-med">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: steelblue; text-align: center;">
                        <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                        <h4 class="modal-title">
                            <p style="color: white;">Request Permanent License</p>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtPO" placeholder="PO #" runat="server" type="text" class="form-control" Style="width: 100%" title="Purchase Order #" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtDR" placeholder="DR #" runat="server" type="text" class="form-control" Style="width: 100%" title="Delivery Receipt #" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtSI" placeholder="SI #" runat="server" type="text" class="form-control" Style="width: 100%" title="Sales Invoice #" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtCompanyName" placeholder="Company Name" runat="server" type="text" class="form-control" Style="width: 100%" title="Company Name" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtAddress" placeholder="Address" runat="server" type="text" class="form-control" Style="width: 100%" title="Address" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtContactName" placeholder="Contact Name" runat="server" type="text" class="form-control" Style="width: 100%" title="Contact Name" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtContactNo" placeholder="Contact #" runat="server" type="text" class="form-control" Style="width: 100%" title="Contact #" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                                <asp:TextBox ID="txtEmail" placeholder="Email Address" runat="server" type="text" class="form-control" Style="width: 100%" title="Email Address" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-10 col-xs-12"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSetCompanyInformation" UseSubmitBehavior="false" runat="server" class="btn btn-success" OnClick="btnDownload_Click" OnClientClick="closeModal()" Text="Generate Registry Key" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- SSA Upload Start -->

        <div id="SSAUpdateModal" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-med">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: steelblue; text-align: center;">
                        <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                        <h4 class="modal-title">
                            <p style="color: white;">Upload License</p>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <asp:FileUpload ID="fuSSAUpload" runat="server" class="form-control" Style="width: 100%; height: 35px" />
                        <br />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSSAUpload" UseSubmitBehavior="false" runat="server" class="btn btn-success" OnClick="btnSSAUpload_Click" Text="Upload License" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
