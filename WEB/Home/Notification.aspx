<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Notification.aspx.cs" Inherits="Home_Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <script src="../Script/Notification.js"></script>

    <section class="content-header">
        <h1>Messages
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-home"></i>Home</li>
            <li>Messages</li>
        </ol>
    </section>
    <section class="content" runat="server">
        <div class="row" id="NotificationDIsplay">
            <%--<div class="box box-solid box-primary">
                    <div class="box-header with-border">
                        <center> 
                            <h3 class="box-title">List of Messages</h3> 
                        </center>
                    </div>
                    <div class="box-body">
                        <div class="col-lg-12">
                            <div class="row">
                                <h4> Monday, December 13 2021 </h4>
                               
                                <div>
                                    &nbsp; &nbsp; &nbsp;<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp; &nbsp; &nbsp;Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                                </div>
                                <br>
                                <div>
                                    &nbsp; &nbsp; &nbsp;<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp; &nbsp; &nbsp;<a href="../Home/RequestManagement.aspx" style="background-color: #b0c4de;">Approver's Your Goods Receive Transaction was Approved [2021-12-0008]</a>
                                </div>
                            </div>
                            <div class="row">
                                <h4> Wednesday, January 12 2022 </h4>
                                <div>
                                    &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp &nbsp &nbsp  Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
        </div>


        <%--            <div class="box box-default">
                <div class="box-header with-border">
                    <h4> Wednesday, January 12 2022 </h4>
                </div>
                <div class="box-body">
                    <a href="#" style="color:#000"> 
                        <div class="callout callout-info"style="background-color: #d8e1ee;" name="Notif" id ="1" value="1">
                            &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                            &nbsp &nbsp &nbsp Approver's Your Goods Receive Transaction was Approved [2021-12-0008] 
                        </div>
                    </a>
                    <a href="../Home/Dashboard.aspx" style="color:#000"> 
                        <div class="callout "style="background-color: #d8e1ee;" name="Notif" value="2">
                            &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                            &nbsp &nbsp &nbsp  Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                        </div>
                    </a>
                </div>
            </div>--%>

        <%--<div class="box box-default">
                <div class="box-header with-border">
                    <h4> Monday, December 13 2021 </h4>
                </div>
                <div class="box-body">
                    <div class="callout" style="background-color: #d8e1ee;" name="Notif" value="3"> 
                        &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp &nbsp &nbsp  Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                    </div>
                    <div class="callout " name="Notif" value="4">
                        &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp &nbsp &nbsp  Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                    </div>
                    <div class="callout "style="background-color: #d8e1ee;" name="Notif" value="5">
                        &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp &nbsp &nbsp  Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                    </div>
                    <div class="callout "style="background-color: #d8e1ee;" name="Notif" value="6">
                        &nbsp &nbsp &nbsp<img src="../pictures/employeeimages/user_profile.png" alt="..." class="user-image img-circle" style="width:40px;height:40px;">
                                   &nbsp &nbsp &nbsp  Approver's Your Goods Receive Transaction was Approved [2021-12-0008]
                    </div>
                </div>
            </div>--%>
    </section>
</asp:Content>

