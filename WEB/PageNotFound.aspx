<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PageNotFound.aspx.cs" Inherits="PageNotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Page Not Found
            <%--<small>Control panel</small>--%>
        </h1>

    </section>

    <section class="content">
        <section class="content">
            <div class="error-page">
                <h2 class="headline text-yellow">404</h2>

                <div class="error-content">
                    <h3><i class="fa fa-warning text-yellow"></i>Oops! Access Denied.</h3>
                    
                    <p>
                        You do not have access to the requested page. Please ask your system administrator to grant you access to this page.
            Meanwhile, you may <a href="../Home/Dashboard.aspx">return to dashboard</a>.
                    </p>

                    <!--<form class="search-form">
            <div class="input-group">
              <input type="text" name="search" class="form-control" placeholder="Search">

              <div class="input-group-btn">
                <button type="submit" name="submit" class="btn btn-warning btn-flat"><i class="fa fa-search"></i>
                </button>
              </div>
            </div>-->
                    <!-- /.input-group -->
                    <%--</form>--%>
                </div>
                <!-- /.error-content -->
            </div>
            <!-- /.error-page -->
        </section>
    </section>
</asp:Content>

