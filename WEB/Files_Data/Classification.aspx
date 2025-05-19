<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Classification.aspx.cs" Inherits="Files_Data_Classification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/Classification.js"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtCost").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Classification
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Classification</li>
        </ol>
    </section>


    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-6" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddClass" type="button" value="Add Classification" onclick="addClassificationDialogBox()" />
                </div>
            </div>
        </div>  
        <br />

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Classification</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblClassificationList" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%;table-layout:auto;"> <%----%>
                        <thead style="font-weight: bold">
                            <tr>
                                <td>CLASSIFICATION NAME</td>
                                <td>CLASSIFICATION CODE</td>
                                <td>DESCRIPTION</td>
                                <td>FLOATER</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="classificationlist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteClassification" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Classification</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Classification?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveDepartment()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteClassification" onclick="DeleteClassification(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        //iCheck for checkbox and radio inputs

    </script>
</asp:Content>

