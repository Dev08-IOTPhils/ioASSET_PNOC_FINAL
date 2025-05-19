<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DynamicLocation.aspx.cs" Inherits="Files_Data_DynamicLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Script/DynamicLocation.js"></script>
    <link href="../Script/tree.css" rel="stylesheet" />
    <script>
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                if (charCode == 13) {
                    return false;
                    preventDefault();
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
    <%--<style>
        ul, #LocationList {
            list-style-type: none;
        }

        #LocationList {
            margin: 0;
            padding: 0;
        }

        .caret {
            cursor: pointer;
            -webkit-user-select: none; /* Safari 3.1+ */
            -moz-user-select: none; /* Firefox 2+ */
            -ms-user-select: none; /* IE 10+ */
            user-select: none;
        }

            .caret::before {
                content: "\25B6";
                color: black;
                display: inline-block;
                margin-right: 6px;
            }

        .caret-down::before {
            -ms-transform: rotate(90deg); /* IE 9 */
            -webkit-transform: rotate(90deg); /* Safari */
            transform: rotate(90deg);
        }

        .nested {
            display: none;
        }

        .active {
            display: block;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Locations
            <%--<small>Control panel</small>--%>
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i>Files Data</li>
            <li>Location</li>
        </ol>
    </section>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-lg-10 col-md-10 col-sm-8 col-xs-4"></div>
            <div class="col-lg-2 col-md-2 col-sm-8 col-xs-4" align="right">
                <input id="btnAdd" class="btn btn-block btn-primary" type="button" value="Add Location" onclick="addLocationDialogBox()" />
            </div>
        </div>
    </div>

    <section class="content">
        <div class="box box-solid box-primary" style=" height:800px;">
            <div class="box-header with-border">
                <center><h3 class="box-title">List of Location</h3></center>
            </div>
            <div class="box-body">
                <ul class="treeko" runat="server" id="LocationList">
                </ul>
            </div>
        </div>
    </section>

    <div id="addLocationDialogBox" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-med">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="Clear()">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <p><span class="fa fa-edit"></span>Add/Edit Location</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Location:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtLocation" maxlength="50" type="text" class="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Parent Location:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <asp:DropDownList ID="ddlParentLocation" class="form-control" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Description:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <textarea class="form-control" id="txtDescription" rows="5" cols="2" style="width: 100%; height: 150px"></textarea>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Has Multiple Value:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkHasMultiple" class="checkbox checkboxkoto" />
                        </div>
                    </div>

                    <div class="row" id="valuehidden">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Value:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input id="txtValue" maxlength="50" type="text" onkeypress="return onlyNos(event,this); "  ondrop="return false;" onpaste="return false;" class="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-4">
                            <label class="control-label col-sm-3" align="left">
                                <h6><b>Active:</b></h6>
                            </label>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-8">
                            <input type="checkbox" id="chkActive" class="checkbox" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-success pull-left" id="btnSaveModule" onclick="SaveLocation()">&nbsp;&nbsp;Save&nbsp;&nbsp;</button>
                    <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal" onclick="Clear()">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        var toggler = document.getElementsByClassName("caret");
        var i;

        for (i = 0; i < toggler.length; i++) {
            toggler[i].addEventListener("click", function () {
                this.parentElement.querySelector(".nested").classList.toggle("active");
                this.classList.toggle("caret-down");
            });
        }
    </script>
</asp:Content>

