<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="UserProfile.aspx.cs" Inherits="User_Management_User_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/UserProfile.js"></script>
    <script>
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <style>
    </style>


    <section class="content-header">
        <h1>User Profile</h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-user"></i>User Management</li>
            <li>User Profile</li>
        </ol>
    </section>
    <section class="content" runat="server">
        <div class="row">
            <div class="col-md-4">
                <div class="box box-primary" style="height: auto">
                    <br />
                    <div class="box-body container" runat="server" id="DisplayPhoto" style="width: inherit">
                    </div>

                    <h3 class="profile-username text-center" runat="server" id="fullName">Nina Mcintire</h3>
                    <h4 class="text-muted text-center" runat="server" id="PositionDisplay">Software Engineer</h4>
                    <h4 class="text-muted text-center" id="EmployeeNumber">123</h4>

                    <br />
                </div>
            </div>

            <div class="col-md-8">
                <div class="box box-primary">
                    <div class="box-body">
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    E-mail Address:
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtEmail" type="text" class="form-control" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    New Password:
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtPassword" type="password" class="form-control someTextBox" />

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Confirm Password:
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtConfirmPassword" type="password" class="form-control someTextBox" />

                            </div>
                        </div>
                        <%--<br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label">
                                Signature:
                            </label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <asp:FileUpload ID="txtsignature" class="form-control" placeholder="Please Select .jpg, .jpeg file." runat="server" Style="width: 100%; height: 35px" />
                        </div>
                    </div>--%>

                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Display Photo:<br /><h6><b>(Maximum File Size: 2MB)</b></h6>
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <%--<input id="UploadprofilePic" runat="server" type="file" class="form-control" />--%>
                                <%--<asp:FileUpload ID="UploadprofilePic" class="form-control" placeholder="Please Select .jpg, .jpeg file." runat="server" Style="width: 100%; height: 35px" />--%>
                                <asp:FileUpload ID="UploadprofilePic" onchange="getImage(event)" class="form-control" placeholder="Please Select .jpg, .jpeg file." runat="server" Style="width: 100%; height: 35px" />

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4"></div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" id="PropertyImage" runat="server"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Security Question 1 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <select id="ddlQ1" class="form-control select2 ddlQ" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <%--<asp:DropDownList ID="ddlQ1" class="form-control select2" runat="server" onchange="getQuestion('ddlQ1');" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Secret Answer 1 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtQ1" type="text" class="form-control txtA someTextBox"  />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Security Question 2 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <select id="ddlQ2" class="form-control select2 ddlQ" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>
                                <%--<asp:DropDownList ID="ddlQ2" class="form-control select2" runat="server" onchange="getQuestion();" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Secret Answer 2 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtQ2" type="text" class="form-control txtA someTextBox"  />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Security Question 3 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <%--<asp:DropDownList ID="ddlQ3" class="form-control select2" runat="server" onchange="getQuestion();" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                <select id="ddlQ3" class="form-control select2 ddlQ" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Secret Answer 3 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtQ3" type="text" class="form-control txtA someTextBox"  />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Security Question 4 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <%--<asp:DropDownList ID="ddlQ4" class="form-control select2" runat="server" onchange="getQuestion();" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                <select id="ddlQ4" class="form-control select2 ddlQ" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Secret Answer 4 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtQ4" type="text" class="form-control txtA someTextBox"/>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Security Question 5 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <%--<asp:DropDownList ID="ddlQ5" class="form-control select2" runat="server" onchange="getQuestion();" Style="width: 100%; height: 35px"></asp:DropDownList>--%>
                                <select id="ddlQ5" class="form-control select2 ddlQ" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px"></select>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                                <label class="control-label">
                                    Secret Answer 5 :
                                </label>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <input id="txtQ5" type="text" class="form-control txtA someTextBox" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-9 col-md-6 col-sm-8 col-xs-4" align="right">
                                    <button type="button" class="btn btn-sm btn-success" id="btnCompleteRequest" onclick="saveUpdateProfile();">Save</button>
                                    <%--<asp:Button ID="Button1" UseSubmitBehavior="true" runat="server" class="btn btn-success" OnClick="Upload" Text="Save" Style="visibility: hidden" />--%>
                                    <%--<asp:Button ID="Button2" UseSubmitBehavior="false" runat="server" class="btn btn-success" OnClientClick="saveUpdateProfile(this);" Text="Save" />--%>
                                </div>
                                <%--<div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                                    <input id="btnReset" class="btn btn-block btn-warning" type="button" value="Reset" style="width: 150px" />
                                </div>--%>
                            </div>
                        </div>

                        <input id="txtPasswordhidden" class="btn btn-block btn-primary" type="button" value="Save" style="display: none" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>


