<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DatabaseBackUpAndRestore.aspx.cs" Inherits="System_DatabaseBackUpAndRestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/Database.js"></script>
    <script>
        function nofile(ex) {
            swal('Warning', 'Please Select .bak file to restore.', 'warning');
            preventDefault();
            //ex.preventDefault();
        }

        function Success() {
            swal('Success', 'Successfully Requested the Database Restoration.', 'success');
            preventDefault();
        }

        function successRetore() {
            swal('Success', 'Database Restoration Successful.', 'success');
            preventDefault();
        }

        function errorRetore() {
            swal('Error', 'Error during database restoration!', 'error');
            preventDefault();
        }

        function SuccessBackup() {
            swal('Success', 'Database Back-Up Successful.', 'success');
            preventDefault();
        }
        function ErrorBackup() {
            swal('Error', 'Error occured during database back-up', 'error');
            preventDefault();
        }
        function invalidPath() {
            swal('Error', 'Invalid Path/Directory.', 'error');
            preventDefault();
        }

        function invalidFile() {
            swal('Error', 'Please Select a .bak file to restore.', 'error');
            preventDefault();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Database Back-up and Restore
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i> System</li>
            <li>Database Back-up and Restore</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div class="box  box-solid box-primary">
            <div class="box-header with-border"  data-widget="collapse">
                <h3 class="box-title"> Back-Up Database</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                        <label class="control-label" align="left">
                            Directory:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-8 col-xs-4">
                        <asp:TextBox ID="txtDirectory" runat="server" type="text" disabled="true" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="col-lg-7 col-md-7 col-sm-4 col-xs-4" align="right">
                        <asp:Button ID="Button2" runat="server" class="btn btn-sm btn-primary" Text="Backup Database" OnClick="BackUp" />
                    </div>
                </div>
                <%--<br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="left">
                        <asp:Button ID="Button1" runat="server" class="btn btn-sm btn-primary" Text="Backup Database" OnClick="BackUp" />
                    </div>
                </div>--%>
            </div>
        </div>

         <div class="box  box-solid box-primary" id="restoredb">
            <div class="box-header with-border"  data-widget="collapse">
                <h3 class="box-title">Restore Database</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                 <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                        Database Name:
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                        <asp:FileUpload ID="FileUpload1" accept=".bak"  class="form-control" data-toggle="tooltip" title="Please select .bak file to restore." placeholder="Please Select .bak file." runat="server" Style="width: 100%; height: 35px" />
                    </div>
                    <div class="col-lg-7 col-md-7 col-sm-4 col-xs-4" align="right">
                        <button type="button" class="btn btn-sm btn-primary " id="btnRestore" onclick="openConfirmationDialog()">&nbsp;&nbsp;Restore Database&nbsp;&nbsp;</button>
                    </div>
                </div>
                <%--<br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <button type="button" class="btn btn-primary pull-left" id="btnRestore" onclick="openConfirmationDialog()">&nbsp;&nbsp;Restore Database&nbsp;&nbsp;</button>
                    </div>
                </div>--%>
            </div>
        </div>

        <div id="restoreDB" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4>
                        <p style="color:white;"><span></span>Restore Database</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Attention: The database file being restored will be for verification and approval. By restoring this database file it replace the current database. An external audit record will be created once approve.</p>
                    <br />
                    <p>Are you sure you want to continue this action?</p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="upload" runat="server" class="btn btn-success" OnClick="Upload"  Text="Yes" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    </section>
</asp:Content>

