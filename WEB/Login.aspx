<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" charset="utf-8" />
    <title>ioASSET | Log in</title>

    <link rel="shortcut icon" href="<%= Page.ResolveClientUrl("~/Image/IOT/favico.ico") %>" />

    <link href="StyleSheets/select2.min.css" rel="stylesheet" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="StyleSheets/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="StyleSheets/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="StyleSheets/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="StyleSheets/AdminLTE.min.css" />
    <!-- iCheck -->
    <%--<link rel="stylesheet" href="AdminLTE-2.4.7/plugins/iCheck/square/blue.css" />--%>
    <link href="StyleSheets/bs-stepper.min.css" rel="stylesheet" />
    <link href="StyleSheets/bs-stepper.css" rel="stylesheet" />
    <!-- jQuery 3 -->
    <script src="../Scripts/jquery-3.5.1.js"></script>
    <!-- jQuery UI 1.13.2 -->
    <script src="../Scripts/jquery-ui.min.js"></script>
   
    <!-- Bootstrap 3.3.7 -->
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Script/Login.js"></script>
    <link href="StyleSheets/sweetalert.min.css" rel="stylesheet" />
    <%--<link href="AdminLTE-2.4.7/plugins/sweet/bootstrap-4.min.css" rel="stylesheet" />--%>
    <script src="../Scripts/sweetalert.min.js"></script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
    <script>
        function togglePass() {
            var password = document.getElementById('txtPassword');
            var togglepass = document.getElementById('togglePassword');
            //const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
            //password.setAttribute('type', type);
            var type = password.getAttribute('type');
            if (type == 'password') {
                password.setAttribute('type', 'text')
                togglepass.classList.remove("fa-eye");
                togglepass.classList.add("fa-eye-slash");
            }
            else {
                password.setAttribute('type', 'password')
                togglepass.classList.remove("fa-eye-slash");
                togglepass.classList.add("fa-eye");
            }
        }

        function showSSAnotif(sval) {
            swal({
                title: "Software Support Agreement",
                text: sval,
                icon: "warning",
                buttons: {
                    cancel: {
                        text: "No",
                        value: null,
                        visible: false,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            });
        }

        function invalidlicensecall() {
            swal({
                title: "Your License Key is invalid.",
                text: "Please contact administrator or Click YES to activate",
                icon: "info",
                buttons: {
                    cancel: {
                        text: "No",
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Yes",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            }).then((willUpload) => {
                if (willUpload) {
                    $('#FileUploadActivation').trigger('click');
                    $("#FileUploadActivation").change(function () {
                        $('#Button1').trigger('click');
                    });
                }
            });
        }

        function script404() {
            swal({
                title: "Your trial period has expired.",
                text: "Please contact Internet of Things Philippines or Click YES to upload activation key",
                icon: "info",
                buttons: {
                    cancel: {
                        text: "No",
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Yes",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            }).then((willUpload) => {
                if (willUpload) {
                    $('#FileUploadActivation').trigger('click');
                    $("#FileUploadActivation").change(function () {
                        var fileUpload = $("#FileUploadActivation").get(0);
                        var files = fileUpload.files;
                        var test = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            test.append(files[i].name, files[i]);
                        }
                        $.ajax({
                            url: "LicenseUpload.ashx?",
                            type: "POST",
                            contentType: false,
                            processData: false,
                            data: test,
                            success: function (result) {
                                if (result.includes("File Uploaded Successfully", 0)) {
                                    $.ajax({
                                        async: false,
                                        type: 'POST',
                                        url: "Login.aspx/btnUploadActivation",
                                        data: "{ filename: '" + result.split(':')[1] + "' }",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (xml) {
                                            if (xml.d == "activated") {
                                                swal('Success', 'System activated. Thank you!', 'success');
                                            }
                                            else if (xml.d == "extended") {
                                                swal('Success', 'Temporary lincense extended. Thank you!', 'success');
                                            }
                                            else if (xml.d == "temp") {
                                                swal('Success', 'Temporary lincense activated. Thank you!', 'success');
                                            }
                                            else if (xml.d == "error") {
                                                swal('Error', 'Registry', 'error');
                                            }
                                            else {
                                                swal('Error', 'System cannot activate. Please contact solution provider', 'error');
                                            }
                                           
                                        },
                                        error: function (xhr, status, error) {
                                            alert(error);
                                            console.log(xhr);
                                        }
                                    });
                                }
                                else {
                                    swal({
                                        title: "Warning",
                                        text: result,
                                        icon: "warning"
                                    }).then(function () {
                                        $('#FileUploadActivation').trigger('click');
                                    });
                                }
                            }
                        });
                    });
                }
            });
        }

        function timebombactivated() {
            swal({
                title: "Software Support Agreement has lapsed.",
                text: "Consult your Software Vendor to get a new SSA-2 activation file. Click Yes to re activate",
                icon: "info",
                buttons: {
                    cancel: {
                        text: "No",
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Yes",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            }).then((willUpload) => {
                if (willUpload) {
                    $('#FileUploadActivation').trigger('click');
                    $("#FileUploadActivation").change(function () {
                        $('#Button1').trigger('click');
                    });
                }
            });
        }

        function licensedayCount(sval) {
            swal({
                title: "Extensed license",
                text: sval,
                icon: "info",
                buttons: {
                    cancel: {
                        text: "No",
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Yes",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            }).then((willUpload) => {
                if (willUpload) {
                    $('#FileUploadActivation').trigger('click');
                    $("#FileUploadActivation").change(function () {
                        var fileUpload = $("#FileUploadActivation").get(0);
                        var files = fileUpload.files;
                        var test = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            test.append(files[i].name, files[i]);
                        }
                        $.ajax({
                            url: "LicenseUpload.ashx?",
                            type: "POST",
                            contentType: false,
                            processData: false,
                            data: test,
                            success: function (result) {
                                if (result.includes("File Uploaded Successfully", 0)) {
                                    $.ajax({
                                        async: false,
                                        type: 'POST',
                                        url: "Login.aspx/btnUploadActivation",
                                        data: "{ filename: '" + result.split(':')[1] + "' }",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (xml) {
                                            if (xml.d == "activated") {
                                                swal('Success', 'System activated. Thank you!', 'success');
                                            }
                                            else if (xml.d == "extended") {
                                                swal('Success', 'Temporary lincense extended. Thank you!', 'success');
                                            }
                                            else if (xml.d == "temp") {
                                                swal('Success', 'Temporary lincense activated. Thank you!', 'success');
                                            }
                                            else {
                                                swal('Error', 'System cannot activate. Please contact solution provider', 'error');
                                            }
                                        },
                                        error: function (xhr, status, error) {
                                            alert(error);
                                            console.log(xhr);
                                        }
                                    });
                                }
                                else {
                                    swal({
                                        title: "Warning",
                                        text: result,
                                        icon: "warning"
                                    }).then(function () {
                                        $('#FileUploadActivation').trigger('click');
                                    });
                                }
                            }
                        });
                    });
                }
            });
        }

        function uploadLicense() {
            swal({
                title: "No License Found",
                text: "Do you want to activate now?",
                icon: "info",
                buttons: {
                    cancel: {
                        text: "No",
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Yes",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            }).then((willUpload) => {
                if (willUpload) {
                    $('#FileUploadActivation').trigger('click');
                    $("#FileUploadActivation").change(function () {
                        var fileUpload = $("#FileUploadActivation").get(0);
                        var files = fileUpload.files;
                        var test = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            test.append(files[i].name, files[i]);
                        }

                        $.ajax({
                            url: "LicenseUpload.ashx?",
                            type: "POST",
                            contentType: false,
                            processData: false,
                            data: test,
                            success: function (result) {
                                alert(result);
                                if (result.includes("File Uploaded Successfully", 0)) {
                                    $.ajax({
                                        async: false,
                                        type: 'POST',
                                        url: "Login.aspx/btnUploadActivation",
                                        data: "{ filename: '" + result.split(':')[1] + "' }",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (xml) {
                                            alert(xml.d);
                                            if (xml.d == "activated") {
                                                swal('Success', 'System activated. Thank you!', 'success');
                                            }
                                            else if (xml.d == "extended") {
                                                swal('Success', 'Temporary lincense extended. Thank you!', 'success');
                                            }
                                            else if (xml.d == "temp") {
                                                swal('Success', 'Temporary lincense activated. Thank you!', 'success');
                                            }
                                            else {
                                                swal('Error', 'System cannot activate. Please contact solution provider', 'error');
                                            }
                                        },
                                        error: function (xhr, status, error) {
                                            alert(error);
                                            console.log(xhr);
                                        }
                                    });
                                }
                                else {
                                    swal({
                                        title: "Warning",
                                        text: result,
                                        icon: "warning"
                                    }).then(function () {
                                        $('#FileUploadActivation').trigger('click');
                                    });
                                }
                            }
                        });
                    });
                }
            });
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
    <style>
        .geeks {
                width:100%;
                height:100%;
            }

         img {
                width:20%;
                height:auto;
            }
    </style>
</head>
<body class="hold-transition login-page">
    <br />
    <div class="row" id="divLicense" runat="server">
    </div>
        <div class="login-logo geeks" runat="server" id="LoginImage">
            <%--<a><img src="Image/LogoAndName.png" style="width:300px; height:130px;" /></a>--%>
        </div>
    <div class="login-box">
        <!-- /.login-logo -->
        <div class="login-box-body">
            <form id="form1" runat="server">
                <div>
                    <asp:Label ID="lbaelDB" runat="server" Text="Select Database" hidden/>
                    <div class="form-group" hidden>
                        <label class="sr-only">Database</label>
                        <asp:DropDownList ID="ddlDatabase" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                    </div>
                    <asp:Label ID="lblError" runat="server" Text="Enter Username & Password" />
                    <div class="form-group">
                        <label class="sr-only">Username</label>
                        <asp:TextBox ID="txtUserId" runat="server" required="true" placeholder="Username..." class="form-control"></asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label class="sr-only">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Password..." class="form-control" TextMode="Password" autocomplete="off"></asp:TextBox><span class="input-group-addon"><i id="togglePassword" class="fa fa-eye" onclick="togglePass()"></i></span>
                    </div>
                    <br />
                    <%-- <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-addon"><i class="fa fa-check"></i></span>
              </div>--%>
                    <div align="center" id="login">
                        <asp:Button ID="Button1" runat="server" type="submit" class="btn btn-med btn-primary" Text="Sign in" OnClick="btnLogin_Click" Style="width: 100%"></asp:Button>
                        
<%--                         <asp:Button ID="TestExcel" runat="server" type="submit" class="btn btn-med btn-primary" Text="Test Excel" OnClick="btnTest_Click" Style="width: 100%"></asp:Button>--%>
                       

                        <%--<div id="FPassword" onClick="ForgotPasswordDialogBox()"><label class="sr-only">Forgot Password</label></div>--%>
                        &nbsp
                        <div><a onclick="javascript:ForgotPasswordDialogBox()" style="cursor: pointer">Forgot Password</a></div>
                        <br />
                        <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="ForgotPasswordDialogBox()">Forgot Password</asp:LinkButton>--%>
                         <div><asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-small btn-social" Style="width:100%;text-align:left;" OnClick="Button3_Click"><i class="fa fa-google"></i>Sign in with Google</asp:LinkButton></div>
                        <%--<asp:Button ID="Button3" runat="server" type="submit" class="btn btn-med btn-primary" Text="Sign in with Google"  Style="width: 100%" OnClick="Button3_Click"></asp:Button>--%>
                    </div>
                    <%--<hr />
                    <p > Copyright © 2014-2018 Internet Of Things Phils. Inc. All rights reserved. <a href="http://www.iotphils.com">"Internet Of Things"</a>is a registered trademark of Internet Of Things Phils. Inc</p>--%>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <%-- <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" align="right"><img src="../Image/IOT/iot.png" width="100%"><h3 align="center">IOT PHILS</h3></div>--%>
                            <%--<%--<div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" align="left">--%><%--<h3 align="center">RFID - ioASSET</h3>--%>
                            <%--<div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" align="left"><h3 align="center">AMIS</h3>--%>

                            <%--</div>--%>
                        </div>
                        <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <center><h6>Powered by <a href="http://www.iotphils.com"><img src="Image/IOT/iot.png" width="8%"></a></h6></center>
                        </div>--%>
                    </div>
                </div>
                <div id="modalActivateDialog" class="modal fade" tabindex="-1">
                    <div class="modal-dialog modal-med">
                        <div class="modal-content">
                            <div class="modal-header">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearPass()">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">Upload Activation File</h4>
                            </div>
                            <div class="modal-body">
                                <asp:FileUpload ID="FileUploadActivation" class="form-control" runat="server" Style="width: 100%; height: 35px" />
                            </div>
                            <div class="modal-footer">
                                <%--<button type="button" class="btn btn-sm btn-success" id="btnSavePassword" onclick="ChangePassword()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>--%>
                                <%--<asp:Button ID="Button1" runat="server" type="submit" class="btn btn-med btn-success" Text="Upload" OnClick="btnUploadActivation" ></asp:Button>--%>
                                <%--<asp:Button ID="Button1" UseSubmitBehavior="false" runat="server" class="btn btn-sm btn-success" OnClick="btnUploadActivation" Text="Upload" />--%>
                                <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="ClearPass()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                            </div>
                        </div>
                    </div>
                </div>


                <div id="modalExtensionDialog" class="modal fade" tabindex="-1">
                    <div class="modal-dialog modal-med">
                        <div class="modal-content">
                            <div class="modal-header">

                                <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearPass()">
                                    <span aria-hidden="true">&times;</span></button>--%>

                                <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" onclick="ClearPass()" aria-hidden="true"></button>
                                <h4 class="modal-title">Upload Extension File</h4>
                            </div>
                            <div class="modal-body">
                                <asp:FileUpload ID="FileUploadExtension" class="form-control" runat="server" Style="width: 100%; height: 35px" />
                            </div>
                            <div class="modal-footer">
                                <%--<button type="button" class="btn btn-sm btn-success" id="btnSavePassword" onclick="ChangePassword()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>--%>
                                <%--<asp:Button ID="Button1" runat="server" type="submit" class="btn btn-med btn-success" Text="Upload" OnClick="btnUploadActivation" ></asp:Button>--%>
                                <%--<asp:Button ID="Button2" UseSubmitBehavior="false" runat="server" class="btn btn-sm btn-success" OnClick="btnUploadActivation" Text="Upload" />--%>
                                <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="ClearPass()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div id="ForgotPassword" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Forgot Password</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <!-- /.card-header -->
                    <!-- /.tab-pane -->
                    <div class="bs-stepper">
                        <div class="bs-stepper-header" role="tablist">
                            <!-- your steps here -->
                            <div class="step" data-target="#step1-part">
                                <button type="button" class="step-trigger" role="tab" aria-controls="step1-part" id="step1-part-trigger">
                                    <span class="bs-stepper-circle">1</span>
                                    <span class="bs-stepper-label">Enter Email Address</span>
                                </button>
                            </div>
                            <div class="line"></div>
                            <div class="step" data-target="#step2-part">
                                <button type="button" class="step-trigger" role="tab" aria-controls="step2-part" id="step2-part-trigger">
                                    <span class="bs-stepper-circle">2</span><br />
                                    <span class="bs-stepper-label">Change Password</span>
                                </button>
                            </div>
                        </div>
                        <div class="bs-stepper-content">
                            <!-- STEP 1 -->
                            <div id="step1-part" class="content" role="tabpanel" aria-labelledby="step1-part-trigger">
                                <div class="col-lg-12">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>Enter Email Address</label>
                                                <input type="email" class="form-control" id="InputEmail1" placeholder="Enter email">
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-primary float-right" type="button" id="btnNextStepper">Next</button>
                                </div>
                            </div>
                            <!-- STEP 2-->
                            <div id="step2-part" class="content" role="tabpanel" aria-labelledby="step2-part-trigger">
                                <div class="col-lg-12">
                                    <label>Questions:</label>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label id="lblQ1">Question  1:</label>
                                                <input type="email" class="form-control" id="txtAnswer1" placeholder="Question 1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label id="lblQ2">Question  2:</label>
                                                <input type="email" class="form-control" id="txtAnswer2" placeholder="Question 2" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label id="lblQ3">Question  3:</label>
                                                <input type="email" class="form-control" id="txtAnswer3" placeholder="Question 3" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="modal-footer">
                                    <div class="col-lg-1">
                                        <button class="btn btn-primary float-right" type="button" id="btnPrevStepper">Previous</button>
                                    </div>
                                    <div class="col-lg-11">
                                        <button class="btn btn-primary float-right" type="button" id="btnConfirmPurchaseOrder" onclick="CheckAnswers()">Confirm</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>

        <script>
            //wmr
            $('#itrbtnxcl').off('click').on('click', function () {
                alert('x');
                //Get GoodsRecieveID and put in key2

                var sessionData = { key1: 'RLSDDSP', key2: '', key3: '' };
                // Serialize sessionData to a query string
                var queryString = $.param(sessionData);

                // Construct the URL with the session data
                var url = 'ReportPage.aspx?' + queryString;


                // Open the new window with the URL
                var newWindow = window.open(url, '_blank');
                swal("File Downloaded", "", "success");

            });


            document.addEventListener('DOMContentLoaded', function () {
                window.stepper = new Stepper(document.querySelector('.bs-stepper'))
            })
            $.widget.bridge('uibutton', $.ui.button);

            swal({
                title: "",
                text: "",
                icon: "",
                timer: 1
            });
        </script>
    </div>

    <!-- Upload License Modal-->

    <%--    <div id="uploadLicenseModal" class="modal fade" tabindex="-1">
         <div class="swal-icon swal-icon--info"></div>
        <div class="swal-title" style="">No License Found.</div>
        <div class="swal-text" style="">Click OK to Activate</div>
        <div class="swal-footer">
            <div class="swal-button-container">

                <button class="swal-button swal-button--confirm">OK</button>

                <div class="swal-button__loader">
                    <div></div>
                    <div></div>
                    <div></div>
                </div>
            </div>
        </div>
         </div>--%>

    <div id="uploadLicenseModal" class="swal-modal" role="dialog" aria-modal="true">
        <div class="swal-icon swal-icon--info"></div>
        <div class="swal-title" style="">No License Found.</div>
        <div class="swal-text" style="">Click OK to Activate</div>
        <div class="swal-footer">
            <div class="swal-button-container">

                <button class="swal-button swal-button--confirm">OK</button>

                <div class="swal-button__loader">
                    <div></div>
                    <div></div>
                    <div></div>
                </div>
            </div>
        </div>
    </div>

    <%--    <div id="uploadLicenseModal" class="swal-modal" role="dialog" tabindex="-1" aria-hidden="true">
        <div class="swal-icon swal-icon--info"></div>
        <div class="swal-title" style="">No License Found.</div>
        <div class="swal-text" style="">Click OK to Activate</div>
        <div class="swal-footer">
            <div class="swal-button-container">

                <button class="swal-button swal-button--confirm">OK</button>

                <div class="swal-button__loader">
                    <div></div>
                    <div></div>
                    <div></div>
                </div>
            </div>
        </div>
    </div>--%>

    <!-- Upload Modal -->

    <script>
        //$('.select2').select2();
    </script>

    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->

     <%--Select2--%>
    <script src="../Scripts/select2.full.min.js"></script>
   
    <!-- Slimscroll -->
    <script src="../Scripts/jquery.slimscroll.min.js"></script>
    <script src="../Scripts/bs-stepper.min.js"></script>
    <script src="../Scripts/bs-stepper.js"></script>
    <!-- AdminLTE App -->
    <script src="../Scripts/adminlte.min.js"></script>
  
</body>
</html>
