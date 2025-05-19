<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SampleDropzone.aspx.cs" Inherits="Files_Data_SampleDropzone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../AdminLTE-2.4.7/plugins/dropzone/min/dropzone.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content">
            <div class="col">
                <div class="fallback">
                    <input name="file" type="file" multiple />
                </div>
            </div>
    </section>
    <script src="../AdminLTE-2.4.7/plugins/dropzone/min/dropzone.min.js"></script>
</asp:Content>

