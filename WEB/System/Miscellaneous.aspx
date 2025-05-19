<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Miscellaneous.aspx.cs" Inherits="System_Miscellaneous" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../Script/Miscellaneous.js"></script>
    <script src="../Script/Carousel.js"></script>
    <script>
        var loadFile = function (event) {
            var reader = new FileReader();
            reader.onload = function () {
                var output = document.getElementById('output');
                output.src = reader.result;
            };
            reader.readAsDataURL(event.target.files[0]);
        };
    </script>
    <script language="Javascript">
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
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isNumberKey1(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#BodyHolder_txtDefaultResidualValue").val()
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

        function isNumberKey2(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#BodyHolder_txtDefaultSalvageValue").val()
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

        function isNumberKey3(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtDefaultRateDepreciation").val()
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
        function upload(count) {
            if (count != 0) {
                alert("Upload Successful!\n\n(" + count + ") Assets Uploaded...");
            }
            else {
                alert("Upload Successful!\n\nNo assets uploaded.");
            }
        }

        function upload1() {
            alert("Problem occur in uploading assets, please check your data in the file!\n\nNo assets uploaded.");
        }

        function uploadSelect() {
            swal("Warning", "Please select an image to upload.", "warning");
        }

        function selectfile() {
            swal("Warning", "Please select an image to upload.", "warning");
            preventDefault();
        }

        function doneupload() {
            preventDefault();
        }
    </script>
    <style>
        .zoom {
            transition: transform .2s;
        }

            .zoom:hover {
                -ms-transform: scale(2.5); /* IE 9 */
                -webkit-transform: scale(2.5); /* Safari 3-8 */
                transform: scale(2.5);
            }

        .fixed-button {
            position: fixed;
            top: 20px; /* Adjust as needed */
            left: 20px; /* Adjust as needed */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Miscellaneous Setting
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i>System</li>
            <li>Miscellaneous Setting</li>
        </ol>
    </section>
    <br />


    <section class="content">
        <%--Threshold--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Threshold Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <%--  <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Aging Threshold(days):
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtAgingThreshold" placeholder="Number of days" runat="server" type="text" onkeypress="return isNumber(event)" title="This value will define after how many days the system will send a reminder notification on the dashboard for incomplete transactions requiring approval and/or pending transactions." class="form-control" Style="width: 100%" ondrop="return false;" onpaste="return false;" />
                    </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Handheld Threshold(days):
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtHandheldThreshold" placeholder="Number of days" runat="server" type="text" onkeypress="return isNumber(event)" title="This value will dictate how often you must syncronize your Handheld device with system. If your handheld device has not been syncronized for x days(where x is the handheld threshold value) the handheld device will not allow any verification or inventory process operations unless the user manually synchronizes the device with the server."  class="form-control" Style="width: 100%" ondrop="return false;" onpaste="return false;" />
                    </div>
                </div>--%>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Maintenance Threshold(days):
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtMaintenanceThreshold" placeholder="Number of days" runat="server" type="text" onkeypress="return isNumber(event)" title="This value will define whether a scheduled Preventive Maintenance order will be logged as a Preventive Maintenance transaction. If the Scheduled Preventive Maintenance order will be done/served WITHIN x days before OR x days after the scheduled PM (where x is the Maintenance Threshold value), it will still be considered as a Preventive Maintenance transaction. Otherwise, it will be considered as a Corrective Maintenance transaction." class="form-control" Style="width: 100%" ondrop="return false;" onpaste="return false;" />
                    </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetThreshold" onclick="SetThreshold();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>

        <%--Home Page Image Setting--%>
        <%-- <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border">
                <h3 class="box-title"><span></span>Home Page Image Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" align="left">
                        <asp:FileUpload ID="fuHomePage" class="btn btn-sm btn-primary" type="file" accept="image/*" onchange="loadFile(event);" runat="server" Style="width: 80%;" />
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <asp:Button ID="Button4" UseSubmitBehavior="false" runat="server" class="btn btn-sm btn-success pull-right" OnClientClick="SaveHomePageImage(this);" Text="Upload" OnClick="Upload" />
                    </div>
                </div>
                <!-- /.row -->
                <img id="output" />
            </div>
        </div>--%>

        <%--CAROUSEL--%>
        <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Bulletin Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div id="LoginImages" runat="server">
                </div>
                <br />
                <div id="samplediv">
                    <div id="dZUpload" class="dropzone">
                        <div class="dz-default dz-message">
                            <h2>Drop Files or Click to Browse. </h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--Database Back Up Directory--%>
        <div class="box  box-solid box-primary  collapsed-box" hidden>
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Database Back-up Directory Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Directory:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <asp:TextBox ID="txtDBDirectory" placeholder="Ex. D:\Folder1\Folder2\" data-toggle="tooltip" title="Ex. D:\Folder1\Folder2\" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetDirectory" onclick="SetDirectory();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>


                <!-- /.row -->
            </div>
            <!-- /.box-body -->
            <%--<div class="box-footer">
                Visit <a href="https://select2.github.io/">Select2 documentation</a> for more examples and information about the plugin.
            </div>--%>
        </div>

        <%--Uploaded Files--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Uploaded Files Directory Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Directory:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <asp:TextBox ID="txtUploadFilesDirectoryPath" placeholder="Ex. D:\Folder1\Folder2\" data-toggle="tooltip" title="Ex. D:\Folder1\Folder2\" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetDirectoryPath" onclick="SetDirectoryPath();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>


                <!-- /.row -->
            </div>
        </div>

        <%--User Default Password--%>
        <%-- <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;User Default Password Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>--%>
        <!-- /.box-header -->
        <%-- <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Default Password:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <asp:TextBox ID="txtDefaultPassword" runat="server" type="password" class="form-control" Style="width: 100%" /><%--<span class="input-group-addon"><i class="fa fa-eye" onclick="showPass()"></i></span>--%>
        <%--  </div>
                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetDefaultPassword" onclick="SetDefaultPassword();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>

                <div class="row">
                </div>
            </div>
        </div>--%>
        <%--User Account Settings--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;User Account Settings</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">


                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <input type="radio" id="Username" name="Userusername" value="Username" class="flat-red" />
                                User to enter username
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <input type="radio" id="Email" name="Userusername" value="Email" class="flat-red" />
                                Username the same as email
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <input type="checkbox" onclick="Disabledtxt('chkAccountLock')" id="chkAccountLock" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Number of tries before account lock : </label>
                                <div class="col-lg-6 col-md-6 col-sm-10 col-xs-10">
                                    <asp:TextBox ID="txtAccountLock" runat="server" type="text" class="form-control" Style="width: 40%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row" id="DPass">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <table>
                                    <tr>
                                        <td style="width: 50%">
                                            <input type="radio" id="DefaultPassword" name="rbPassword" value="Default Password" class="flat-red" />
                                            Default Password :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDefaultPassword" runat="server" type="password" class="form-control" Style="width: 120%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" />
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <br />
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <input type="radio" id="RandomPassword" name="rbPassword" value="Random Password" class="flat-red" />
                                Email Random Generated Password 
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-10 col-md-6 col-sm-12 col-xs-12">
                                <table>
                                    <tr>
                                        <td style="width: 50%">Password Length:</td>
                                        <td>
                                            <asp:TextBox ID="txtPasswordSize" runat="server" type="text" class="form-control" Style="width: 120%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-10 col-md-6 col-sm-12 col-xs-12">
                                <table>
                                    <tr>
                                        <td style="width: 50%">
                                            <input type="checkbox" onclick="Disabledtxt('chkUpperCase')" id="chkUpperCase" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Upper Case : </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUpperCase" runat="server" type="text" class="form-control" Style="width: 120%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-10 col-md-6 col-sm-12 col-xs-12">
                                <table>
                                    <tr>
                                        <td style="width: 50%">
                                            <input type="checkbox" onclick="Disabledtxt('chkLowerCase')" id="chkLowerCase" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Lower Case : </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLowerCase" runat="server" type="text" class="form-control" Style="width: 120%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-10 col-md-6 col-sm-12 col-xs-12">
                                <table>
                                    <tr>
                                        <td style="width: 50%">
                                            <input type="checkbox" onclick="Disabledtxt('chkNumber')" id="chkNumber" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Number : </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumber" runat="server" type="text" class="form-control" Style="width: 120%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-10 col-md-6 col-sm-12 col-xs-12">
                                <table>
                                    <tr>
                                        <td style="width: 50%">
                                            <input type="checkbox" onclick="Disabledtxt('chkSymbol')" id="chkSymbol" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Symbols : </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSymbol" runat="server" type="text" class="form-control" Style="width: 120%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetUserAccountSettings" onclick="SetUserAccountSettings()">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>
        <%--HRMIS--%>
        <div class="box  box-solid box-primary  collapsed-box" hidden>
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;HRMIS Integration Settings</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            API URL Address:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="TextBox1" placeholder="" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Username:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="TextBox2" runat="server" type="text" onkeypress="return isNumberKey(event)" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Password:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="TextBox3" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />

                <div class="row">
                    <div style="float: left">

                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSet" onclick="">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                        <button type="button" class="btn btn-sm btn-success pull-right" id="btnTest" onclick="">&nbsp;&nbsp; Test &nbsp;&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>
        <%--Email Setting--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Mail Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            SMTP Host:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtSMTPhost" placeholder="Ex.: smtp.gmail.com" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            SMTP port:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtSMTPport" runat="server" type="text" onkeypress="return isNumberKey(event)" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Email:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtEmail" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Password:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control" Style="width: 100%" />
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Mail Subject Header:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtEmailSubject" placeholder="Ex.: ioASSET" runat="server" type="text" class="form-control" Style="width: 100%" />
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Test Email:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:TextBox ID="txtTestEmail" runat="server" type="text" title="Please put destination email address for test email here." class="form-control" Style="width: 100%" />
                    </div>
                </div>

                <br />

                <%--                <div class="row">--%>
<%--                <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12"></div>--%>
                <div style="float: right">
                    <%--                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">--%>
                    <%--                        <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>--%>

                    <button type="button" class="btn btn-sm btn-success " id="btnTest" onclick="TestMail();">&nbsp;&nbsp;Send Test Email&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-primary" id="btnSetMail" onclick="SetMail();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                </div>
                <%--                </div>--%>
            </div>
        </div>

        <%--Depreciation Settings--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Depreciation Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">

                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <input type="radio" id="radio1" name="r3" value="Straight Line Depreciation Method" class="flat-red" />
                        Straight Line Depreciation Method

                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <input type="radio" id="radio2" name="r3" value="Diminishing Balance Method" class="flat-red" />
                        Diminishing Balance Method
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" hidden>
                        <input type="radio" id="radio3" name="r3" value="Unit of Product Method" class="flat-red" />
                        Unit of Product Method
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Useful Life (Days):
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <%--<input id="txtUsefulLifeDepre" placeholder="Number of days" type="integer" onkeypress="return isNumberKey(event)" class="form-control" style="width: 100%" ondrop="return false;" onpaste="return false;" />--%>
                        <input id="txtUsefulLifeDepre" placeholder="Number of days" type="text" class="form-control" onkeypress="return onlyNos(event,this)" ondrop="return false;" onpaste="return false;" />
                    </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Days in a year:
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <input id="txtDaysPerYearDepre" placeholder="Number of days" type="integer" onkeypress="return isNumberKey(event)" class="form-control" style="width: 100%" />
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Default Residual Value: (PHP)
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <input id="txtDefaultResidualValue" type="text" placeholder="0.00" onkeypress="return isNumberKey1(event)" class="form-control" style="width: 100%" ondrop="return false;" onpaste="return false;" />
                    </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Default Salvage Value: (PHP)
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <input id="txtDefaultSalvageValue" type="text" placeholder="0.00" onkeypress="return isNumberKey2(event)" class="form-control" style="width: 100%" ondrop="return false;" onpaste="return false;" />
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            Default Rate of Depreciation: (%)
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <input id="txtDefaultRateDepreciation" type="text" placeholder="0.00" onkeypress="return isNumberKey3(event)" class="form-control" style="width: 100%" ondrop="return false;" onpaste="return false;" />
                    </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <label class="control-label" align="left">
                            <%--<h6><b>Defaul Salvage Value:</b></h6>--%>
                        </label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <%--<asp:TextBox ID="TextBox2" placeholder="Number of days" runat="server" type="text" onkeypress="return isNumberKey(event)" class="form-control" Style="width: 100%" />--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetDepreciation" onclick="SetDepreciation(this);">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>

        <%--RFID and Barcode Settings--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;RFID and Barcode Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        RFID Number Generation:
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <%--Barcode Number Generation:--%>
                    </div>
                </div>
                <div class="row">

                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <input type="radio" id="radioRfidAuto" name="r4" value="Auto generated" class="flat-red" />
                        Auto generated
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <input type="radio" id="radioRfidManual" name="r4" value="Manual Input" class="flat-red" />
                        Manual Input
                    </div>
                    <%--   <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <input type="radio" id="radioRfidRad" name="r4" value="RADMWare" class="flat-red" />
                            RADMware
                        </div>--%>
                </div>

                <br />
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        Barcode Number Generation:
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <input type="radio" id="radioBarcodeAuto" name="r5" value="Auto generated" class="flat-red" />
                        Auto generated
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <input type="radio" id="radioBarcodeManual" name="r5" value="Manual Input" class="flat-red" />
                        Manual Input
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetRFIDandBarcode" onclick="SetRFIDandBarcodeGeneration();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>

        <%--Company Information Settings--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;Company Information Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Company Name:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:TextBox ID="txtCompanyName" runat="server" type="text" onchange="CheckCompanyName();" class="form-control" Style="width: 100%" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Trading Name:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:TextBox ID="txtTradingName" runat="server" type="text" class="form-control" Style="width: 100%" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Contact Person:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:TextBox ID="txtContactPerson" runat="server" type="text" class="form-control" Style="width: 100%" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Contact Number:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:TextBox ID="txtContactNumber" runat="server" type="text" class="form-control" Style="width: 100%" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Email Address:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:TextBox ID="txtEmailAddress" runat="server" type="text" class="form-control" Style="width: 100%" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Address:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:TextBox TextMode="MultiLine" Wrap="true" runat="server" class="form-control" ID="txtAddress" Rows="3" cols="1" Style="width: 100%; height: 150px" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Company Logo:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:FileUpload ID="FileUploadCompanyLogo" onchange="getImage(event)" class="form-control" placeholder="Please Select .jpg, .jpeg, .png file." runat="server" Style="width: 100%; height: 35px" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="CompanyLogo" runat="server">
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Form Logo:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:FileUpload ID="FileUploadFormLogo" onchange="getImage1(event)" class="form-control" placeholder="Please Select .jpg, .jpeg, .png file." runat="server" Style="width: 100%; height: 35px" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="FormLogo" runat="server">
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <label class="control-label" align="left">
                                    Web Icon:
                                </label>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <asp:FileUpload ID="FileUploadWebIcon" onchange="getImage2(event)" class="form-control" placeholder="Please Select .ico file." runat="server" Style="width: 100%; height: 35px" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="WebIcon" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:Button ID="btnUpload" UseSubmitBehavior="true" runat="server" class="btn btn-success" OnClick="CompanyLogoUpload" Text="Save" Style="visibility: hidden" />
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetCompanyInformation" onclick="SetCompanyInformation()">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>

                        <%--<asp:Button ID="btnSetCompanyInformation" UseSubmitBehavior="false" runat="server" class="btn btn-sm btn-primary pull-right" OnClientClick="SetCompanyInformation(this);" Text="Set" />--%>
                        <%--<button type="button" class="btn btn-sm btn-primary" id="btnSetCompanyInformation" onclick="SetCompanyInformation();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>--%>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>

        <%--System Settings--%>
        <div class="box  box-solid box-primary  collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>&nbsp;System Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Property Number Generation:
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="PropAutoGen" name="propnogeneration" value="Augo Generated" class="flat-red" />
                        Auto generated
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="PropManual" name="propnogeneration" value="Manual Input" class="flat-red" />
                        Manual Input
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Require Approval:
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkCreationApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Creation of new property under Goods Receive Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkMergeApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Merging of Supply under Supplies Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyIssuanceApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Issuance of Property under Property Issuance Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyTransferApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Transferring of Property under Property Transfer Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkTurnOverApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Turning Over Supply under Supplies Turn Over Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkStockIssuanceApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Issuance of Supply under Supplies Issuance/Request Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkStockDisposalApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Disposing Supply under Supplies Disposal Module</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkDisposalApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Disposing Property under Property Disposal Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkGatePassApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Creation of Gatepass under Gate/Day Pass Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkUploadApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Uploading of csv file under Data Upload Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkSupployAlloc" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Allocation of Supply under Supply Allocation Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkRestoreApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Restoring Database under Database Management Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyRequestApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Property Request Approval under Property Request Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyRetagginApproval" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Retagging Approval under Tagging Module</label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                        Require Verification:
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkCreationScanner" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Creation of new property under Goods Receive Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyIssuanceScanner" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Issuance of Property under Property Issuance Module</label>
                            </div>
                        </div>
                        <div class="row" hidden>
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyRequestScanner" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Property Request under Property Request Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyDisposalScanner" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Property Disposal under Property Disposal Module</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyReturnRequestScanner" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Property Return under Property Return Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkPropertyTransferScanner" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Transferring of Property under Property Transfer Module</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkStockTaking" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Supply Taking under Inventory Count Module</label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Verification Mode:
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="radioMode1" name="rvm" value="Web Manual Verification" class="flat-red" />
                        Web Manual Verification 
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" hidden>
                        <input type="radio" id="radioMode2" name="rvm" value="Barcode Scanner" class="flat-red" />
                        Barcode Scanner
                    </div>
                    <%-- <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="radioMode3" name="rvm" value="Thing Magic" class="flat-red" />
                        RADMware
                    </div>--%>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="radioMode4" name="rvm" value="Buddy Pro" class="flat-red" />
                        Buddy Pro
                    </div>
                </div>
                <br />

                <%--added by nero 20220517--%>
                <div>
                    <div class="row" hidden>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                Issuance Acknowledgement:
                            </label>
                        </div>
                    </div>
                    <div class="row" hidden>
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                                </div>
                                <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                    <input type="checkbox" id="chkIssuanceAcknowledgement" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Issuance of Property under Property Issuance Module</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="ack1" class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <label class="control-label" align="left">
                                Acknowledgement Mode:
                            </label>
                        </div>
                    </div>
                    <div id="ack2" class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <input type="radio" id="WebAcknowledged" name="AckMode" value="Web Manual Verification" class="flat-red" />
                            Web Manual Acknowledgement
                        </div>
                        <div id="ack3" class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <input type="radio" id="BodyLITEMode" name="AckMode" value="Buddy Lite" class="flat-red" />
                            Buddy Lite
                        </div>
                    </div>
                    <br />
                </div>
                <%--ends here--%>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Require Printing:
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkRFIDprinting" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Creation of new property under Goods Receive Module</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Require Maintenance:
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkRequireMaintenance" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Require Maintenance during Property Return</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    </div>
                </div>
                <%-- <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            System Version
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="gv" name="rvm2" value="Government Version" class="flat-red" />
                        Government Version  
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="pv" name="rvm2" value="Private Version" class="flat-red" />
                        Private Version
                    </div>
                   <%-- <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="radioMode3" name="rvm" value="Thing Magic" class="flat-red" />
                        RADMware
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="fv" name="rvm2" value="Full Version" class="flat-red" />
                        Full Version
                    </div>
                </div>--%>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Expense Type
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-5 col-md-3 col-sm-12 col-xs-12">
                        <input type="radio" id="PI" name="rvm3" value="PAR/ICS" class="flat-red" />
                        PAR/ICS     
                        <table>
                            <tr>
                                <td>ICS&nbsp;LOW&nbsp;<=&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtLOW" runat="server" type="text" class="form-control" Style="width: 70%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                <td>>&nbsp;ICS&nbsp;HIGH&nbsp;<=&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtHIGH" runat="server" type="text" class="form-control" Style="width: 70%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                <td>>&nbsp;PAR</td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-1 col-md-3 col-sm-12 col-xs-12">
                    </div>
                    <div class="col-lg-4 col-md-3 col-sm-12 col-xs-12" hidden>
                        <input type="radio" id="OC" name="rvm3" value="OPEX/CAPEX" class="flat-red" />
                        OPEX/CAPEX   
                        <table>
                            <tr>
                                <td>OPEX&nbsp;<=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtOPEXCAPEX" runat="server" type="text" class="form-control" Style="width: 70%" onkeypress="return isNumberKey1(event)" ondrop="return false;" onpaste="return false;" /></td>
                                <td>>&nbsp;CAPEX&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label class="control-label" align="left">
                            Supply Request Quantity:
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="row">
                            <div class="col-lg-1 col-md-2 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-lg-11 col-md-10 col-sm-12 col-xs-12">
                                <input type="checkbox" id="chkStockRequestQuantity" class="switch_1 pull-left" /><label class="pull-left">&nbsp;Show Remaining Quantity on Supply Request</label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-sm btn-primary pull-right" id="btnSetSystemSetting" onclick="SetSystemSetting();">&nbsp;&nbsp;Set&nbsp;&nbsp;</button>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>
    </section>


</asp:Content>

