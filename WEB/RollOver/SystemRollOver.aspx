<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SystemRollOver.aspx.cs" Inherits="RollOver_SystemRollOver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="SystemRollOver.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>System Roll Over
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i>Roll Over</li>
            <li>System Roll Over</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div class="row" hidden>
            <asp:TextBox ID="txtDirectoryPath" runat="server" type="text" class="form-control" Style="width: 100%" />
        </div>

        <div class="box  box-solid box-primary">
            <div class="box-header with-border">
                <h3 class="box-title"><span class="fa fa-warning"></span>&nbsp;Attention!</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-lg-12">
                    <p align="justify">
                        Please be reminded that roll over function will enable the system to do the following:
                    </p>
                    1. System will create a new database instance with the new current year<br />
                    2. The current database will now be a previous database instance of the database<br />
                    3. System logs will be removed in the system<br />
                    4. Asset balances will be carried over to the new database<br />
                    5. System users can still be able to access the previous year database using the select database function in the Login Page<br /><br />
                     <p align="justify">
                        Please make sure that the following are met before goind forward:
                    </p>
                    1. All system users are logged out<br />
                    2. Monthly reconcilation report are finished<br />
                    3. No pending or open transactions<br />
                    4. All data are reconcilled<br /><br />
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnSetSystemRollOver" UseSubmitBehavior="false" runat="server" class="btn btn-sm btn-primary " OnClick="AddUpdateConnectionString" Text="Proceed to Roll-over" />
                </div>
            </div>
    </section>
</asp:Content>

