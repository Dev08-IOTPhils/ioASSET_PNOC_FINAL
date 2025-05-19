<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SampleDataUpload.aspx.cs" Inherits="System_SampleDataUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Data Upload
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i>System</li>
            <li>Data Upload</li>
        </ol>
    </section>
    <br />
    <section class="content">
       <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Existing Property Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnDowloadPropertyFormat" onclick="DownloadTemplateProperty()">&nbsp;&nbsp;Download Template&nbsp;&nbsp;</button>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" align="right">
                            <asp:FileUpload ID="SampleFile" class="btn btn-sm btn-primary" runat="server" Style="width: 100%;" title="Please select a .csv file to upload." />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnUploadProperty" onclick="UploadProperty()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

