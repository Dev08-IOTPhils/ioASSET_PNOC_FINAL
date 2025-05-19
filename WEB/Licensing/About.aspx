<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Licensing_About" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">
      <script type="text/javascript" lang="Javascript">
          function RequestRegistry() {
              $("#RLDialogBox").modal("show");
          }
    </script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>About
            <%--<small>Control panel</small>--%>
        </h1>
    </section>
    <section class="content">
        <div class="box box-solid box-primary">
            <div class="box-header with-border">
                <center><h3 class="box-title">About</h3></center>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8"></div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" align="right">
                            <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Request For Extension" onclick="RequestRegistry()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:content>

