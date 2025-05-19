<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
            <h1>Home
            <%--<small>Control panel</small>--%>
            </h1>
            <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-home"></i>Home</li>
            <li>Home Page</li>
        </ol>
        </section>
        <section class="content">
            <div class="container" id="announcement" runat="server"></div>
        </section>
</asp:Content>

