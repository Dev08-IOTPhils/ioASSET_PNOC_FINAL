<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignalR.aspx.cs" Inherits="Home_SignalR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.1.2.min.js"></script>
    <script type="text/javascript">

        $(function () {

            // Declare a proxy to reference the hub.          

            var notifications = $.connection.notificationHub;

            // Create a function that the hub can call to broadcast messages.

            notifications.client.recieveNotification = function (totalNewMessages, totalNewCircles, totalNewJobs, totalNewNotifications) {

                // Add the message to the page.                    

                $('#spanNewMessages').text(totalNewMessages);

                $('#spanNewCircles').text(totalNewCircles);

                $('#spanNewJobNotifications').text(totalNewJobs);

                $('#spanNewNotifications').text(totalNewNotifications);

            };

            // Start the connection.

            $.connection.hub.start().done(function () {

                notifications.server.sendNotifications();

            }).fail(function (e) {

                alert(e);

            });

            $.connection.hub.start();

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Messages
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-home"></i>Home</li>
            <li>Messages</li>
        </ol>
    </section>
    <section class="content">
        <div>

            <b>You have <span id="spanNewMessages">0</span> New Message Messages.</b><br />

            <b>You have <span id="spanNewCircles">0</span> New Circles Messages.</b><br />

            <b>You have <span id="spanNewJobNotifications">0</span> New Job Messages.</b><br />

            <b>You have <span id="spanNewNotifications">0</span> New Messages.</b>

        </div>
    </section>
</asp:Content>

