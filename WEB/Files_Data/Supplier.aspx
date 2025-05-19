<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Supplier.aspx.cs" Inherits="Files_Data_Supplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Supplier.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Supplier
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Supplier</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-5" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddSupplier" type="button" value="Add Supplier" onclick="addSupplierDialogBox()" />
                </div>
            </div>
        </div>
        <br />
        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Supplier</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblSupplier" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%; table-layout: auto;">
                        <%----%>
                        <thead style="font-weight: bold">
                            <tr>
                                <td>SUPPLIER NAME</td>
                                <td>CONTACT #</td>
                                <td>EMAIL</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>

                            </tr>
                        </thead>
                        <tbody id="supplier">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteSupplier" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Supplier</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Supplier?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveDepartment()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteSupplier" onclick="DeleteSupplier(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

