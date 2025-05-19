<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Task.aspx.cs" Inherits="Files_Data_Task" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Script/Task.js"></script>
      <script type="text/javascript" lang="Javascript">


          function isNumberKey7(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#txtNumberOfSteps").val()
                if (inputValue.indexOf('.') < 1) {
                    return false;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="content-header">
        <h1>Task
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-folder"></i>Files Data</li>
            <li>Task</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
                <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                    <input id="btnAdd" class="btn btn-block btn-primary btn_AddTask" type="button" value="Add Task" onclick="addTaskDialogBox()" />
                </div>
            </div>
        </div>
        <br />

        <div class="box box-solid box-primary">
            <div class="box-header with-border" data-widget="collapse">
                <center><h3 class="box-title">List of Task</h3></center>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="col-lg-12">
                    <table id="tblTaskList" role="grid" class="table table-bordered table-hover table-condensed " style="width: 100%;table-layout:auto;">
                        <thead style="font-weight: bold">
                            <tr>
                                <td>TASK NAME</td>
                                <td>DESCRIPTION</td>
                                <td>NUMBER OF STEPS</td>
                                <td>ACTIVE</td>
                                <td>ACTION</td>
                            </tr>
                        </thead>
                        <tbody id="tasklist">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>

    <div id="deleteTask" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: indianred; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="Clear()">&times;</button>
                    <h4>
                        <p><span class="fa fa-trash"></span>Delete Task</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this Task?</p>
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-sm btn-info" id="Button1" onclick="SaveDepartment()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-info" data-dismiss="modal">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                    --%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">&nbsp;&nbsp;&nbsp;No&nbsp;&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-success pull-left" id="btnDeleteTask" onclick="DeleteTask(this.id)">&nbsp;&nbsp;Yes&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div id="addTaskDialogBox" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true" onclick="Clear();"></button>
                    <h4 class="modal-title">
                        <p style="color: white;">Add/Edit Task</p>
                    </h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                             <label class="control-label" align="left">
                                Task Name:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-10">
                            <input id="txtTaskName" maxlength="50" type="text" onkeyup="return ReplaceStr2(this);" style="width: 100%" class="form-control" />
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                             <label class="control-label" align="left">
                                Description:
                            </label>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-10">
                            <textarea class="form-control" id="txtDescription" rows="5" cols="1" style="width: 100%; height: 150px" onkeyup="CountChar(this.value,500,'cnt', this.id); return ReplaceStr2(this);"></textarea>
                            <h6>
                                <label><i id="cnt"><b>500 remaining character(s)</b></i></label></h6>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                             <label class="control-label" align="left">
                                Number of Steps:
                            </label>
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-10">
                            <input id="txtNumberOfSteps"type="number" placeholder="(Max of 10 steps)" style="width: 100%" class="form-control" onkeypress="return isNumberKey7(event,this)" onkeyup="addFields()" />
                        </div>
                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">

                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-10" id="steplist">

                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>--%>
                    </div>

                    <div class="row">
                        <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <h6><b>Active:</b></h6>
                        </div>--%>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">

                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-10">
                           <%-- <input type="checkbox" id="chkActive" class="checkbox icheckbox_flat-red pull-left" /><label class="pull-left">&nbsp;&nbsp; Active</label>--%>
                            
                            <input type="checkbox" class="switch_1 pull-left" id="chkActive" />
                            <label class="pull-left">&nbsp;&nbsp; Active</label>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">

                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="SaveCancelTask">
                    <button type="button" class="btn btn-success btn_EditTask" id="btnSaveTask" onclick="SaveTask()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;Cancel&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        //iCheck for checkbox and radio inputs

    </script>
</asp:Content>

