$(document).ready(function () {
    $('.select2').select2();

    IfGatePassNeedsApproval();

    $("#tblAssetList").DataTable({
        "scrollX": true
            , dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                      },
                       {
                           extend: 'colvis',
                           text: 'COLUMN VISIBILITY',
                           className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                       }
                ],
                dropup: false, //btn btn-sm btn-success
            }
            ]
        }, colReorder: true
            , stateSave: true
    });
    $("#tblPendingUpload").DataTable({
        "scrollX": true
            , dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                      },
                       {
                           extend: 'colvis',
                           text: 'COLUMN VISIBILITY',
                           className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                       }
                ],
                dropup: false, //btn btn-sm btn-success
            }
            ]
        }, colReorder: true
    });

    $("#tblPendingRequestExtension").DataTable({
        "scrollX": true
            , dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                      },
                       {
                           extend: 'colvis',
                           text: 'COLUMN VISIBILITY',
                           className: 'btn btn-primary', exportOptions: { columns: ':visible' },
                           collectionLayout: 'two-column'
                       }
                ],
                dropup: false, //btn btn-sm btn-success
            }
            ]
        }, colReorder: true
            , stateSave: true
    });
    load_data();
    load_data_Pendingupload();
    load_data_Extension();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
    $("#tblPendingRequestExtension").DataTable().clear().draw();
    GetCount();
    GetApproverByCustodian();
});
var pmlidReturn;
var needsapproval = "False";
function IfGatePassNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyReturnRequest.aspx/IfGatePassNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var y = document.getElementById("labelApprover");
            var z = document.getElementById("dropdownApprover");
            if (msg.d == "True") {
                y.style.display = 'inline';
                z.style.display = 'inline';
            }
            else {
                y.style.display = 'none';
                z.style.display = 'none';
            }
        }
    });
}

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        return false;
    }
}

function EnterKey1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Pendingupload();
        return false;
    }
}

function EnterKey2(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Extension();
        return false;
    }
}

function GetApproverByCustodian() {
    custodianid = $('#BodyHolder_ddlCustodian').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyReturnRequest.aspx/GetApproverByCustodian",
        data: " { custodianid : '" + custodianid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlApprover = $("[id*=ddlApprover]");
            ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
            //ddlCustodian.append('<option value="0">--- Select Approver ---</option>');
            $.each(msg.d, function () {
                ddlApprover.append($("<option></option>").val(this['Text']).html(this['Value']));
            });
        }
    });
}

function load_data() {
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '0';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else {
        ExpenseType = '0'
    }
    var search = "";
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
        custodianid = 0;
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyReturnRequest.aspx/GetPropertyAvailableForReturn",
        data: "{custodianid: '" + custodianid + "', search : '" + search + "' , ExpenseType : '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData);

            console.log(data);
            console.log('Success');
            $(".se-pre-con").fadeOut("slow");
            GetCount();
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

var propertynoextend = '';

function populateDataTable(data) {
    try {
        $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true, select: { style: 'multi' },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "DateOfReturn" },
                { "data": "Action" }
            ]
            , dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                },
                {
                    text: 'Select all',
                    action: function () {
                        $('#tblAssetList').DataTable().rows().select();
                    }
                },
                {
                    text: 'Select none',
                    action: function () {
                        $('#tblAssetList').DataTable().rows().deselect();
                    }
                },
                {
                    text: 'Add to Return List',
                    action: function () {
                        var expense = $('#BodyHolder_ddlItem').val();
                        var ExpenseType = '0';
                        if (expense.includes('ICS LOW')) {
                            ExpenseType = '2'
                        }
                        else if (expense.includes('ICS HIGH')) {
                            ExpenseType = '3'
                        }
                        else if (expense.includes('PAR')) {
                            ExpenseType = '1'
                        }
                        else {
                            ExpenseType = '0'
                        }
                        var custodianid = $('#BodyHolder_ddlCustodian').val();
                        if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                            custodianid = 0;
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (count == 0) {
                            swal("Warning", "Please select an item to return", "warning");
                        }
                        else {
                            var ids = $.map($('#tblAssetList').DataTable().rows('.selected').data(), function (item) {
                                return item.pmlid;
                            });
                            console.log(ids);
                            ids.forEach((pmlid) => {
                                $(".se-pre-con").fadeIn(100);
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/PropertyReturnRequest.aspx/TransferToPropertyReturnRequest",
                                    data: "{ custodianid: '" + custodianid + "', ExpenseType: '" + ExpenseType + "', RecID: '" + pmlid + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $('#assetlist').hide();
                                        $('#assetlist').innerHTML = "";
                                        $('#assetlist').fadeIn(200);
                                        GetCount();
                                        $('#tblAssetList').DataTable()
                                        .rows('.selected')
                                        .remove()
                                        .draw(false);
                                        $(".se-pre-con").hide();
                                    }
                                });
                            });
                        }
                    }
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            var expense = $('#BodyHolder_ddlItem').val();
            var ExpenseType = '0';
            if (expense.includes('ICS LOW')) {
                ExpenseType = '2'
            }
            else if (expense.includes('ICS HIGH')) {
                ExpenseType = '3'
            }
            else if (expense.includes('PAR')) {
                ExpenseType = '1'
            }
            else {
                ExpenseType = '0'
            }
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(0)").text();

            var custodianid = $('#BodyHolder_ddlCustodian').val();
            if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                custodianid = 0;

            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturnRequest.aspx/TransferToPropertyReturnRequest",
                data: "{custodianid: '" + custodianid + "', ExpenseType: '" + ExpenseType + "', RecID: '" + pmlid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#tblAssetList').DataTable().rows(currentRow)
                       .remove()
                    //currentRow.remove();
                    .draw(false);
                    //currentRow.remove();
                    GetCount();
                }
            });
        });

        $('#tblAssetList').on('click', 'a.btn_Extend', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertynoextend = currentRow.find("td:eq(0)").text();
            var returndate = currentRow.find("td:eq(7)").text().split(' ')[0];

            pmlidReturn = $(this).attr('pmlid');

            var date = new Date(returndate);
            var dd = date.getDate(); if (dd.length == 1) { dd = "0" + dd; }
            var mm = date.getMonth() + 1; if (mm.length == 1) { mm = "0" + mm; }
            var y = date.getFullYear();
            var someFormattedDate = dd + '/' + mm + '/' + y;
            date = someFormattedDate;

            var today = new Date(someFormattedDate);
            $('#txtDOR').datepicker({
                todayHighlight: true,
                format: 'mm/dd/yyyy',
                startDate: date,
                autoclose: true
            });

            $('#txtDOR').datepicker('setDate', today);

            $('#txtPropertyNo').val(propertynoextend);
            $('#addToExtensionList').modal('show');
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function RequestExtensionReturnDate() {
    var dateextension = $('#txtDOR').val();
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    var approver = $('#BodyHolder_ddlApprover').val();
    var remarks = $('#txtRemarks').val().replace(/  +/g, ' ');
    var pmlid = pmlidReturn;

    var alertMsg;
    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
        custodianid = 0;
    //if (confirm("Are you sure you want to request for extension for date of return?") == false) return false;

    if (needsapproval == "True") {
        if ($('#txtDOR').val() == "" || $('#txtRemarks').val() == "" || $('#BodyHolder_ddlApprover').val() == '0') {
            alertMsg = ' ';
            if ($('#txtDOR').val() == "")
                alertMsg += '\nDate Of Return';
            if ($('#txtRemarks').val() == "")
                alertMsg += '\nRemarks';
            if ($('#BodyHolder_ddlApprover').val() == '0')
                alertMsg += '\nApprover';
            //alert("{ propertyno: '" + $('#txtPropertyNo').val() + "', custodianid: '" + custodianid + "', dateofreturn: '" + dateextension + "', remarks : '" + remarks + "', approverid : '" + approver + "'}");
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else {
            swal({
                title: "Are you sure?",
                text: "Are you sure you want to extend the date of return of this asset?",
                icon: "warning",
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyReturnRequest.aspx/AddToRequestForExtensionReturnDate",
                        data: "{ propertyno: '" + $('#txtPropertyNo').val() + "', custodianid: '" + custodianid + "', dateofreturn: '" + dateextension + "', remarks : '" + remarks + "', approverid : '" + approver + "', pmlid : '" + pmlid + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_data();
                            load_data_Extension();
                            //alert('Successfully Requested for Extension for date of return and pending approval.');
                            swal("Successful", "Successfully Requested for Extension for date of return and pending approval.", "success");
                            $('#addToExtensionList').modal('hide');
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
    else {
        if ($('#txtDOR').val() == "" || $('#txtRemarks').val() == "") {
            alertMsg = ' ';
            if ($('#txtDOR').val() == "")
                alertMsg += '\nDate Of Return';
            if ($('#txtRemarks').val() == "")
                alertMsg += '\nRemarks';
            //alert("{ propertyno: '" + $('#txtPropertyNo').val() + "', custodianid: '" + custodianid + "', dateofreturn: '" + dateextension + "', remarks : '" + remarks + "', approverid : '" + approver + "'}");
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else {
            swal({
                title: "Are you sure?",
                text: "Are you sure you want to extend the date of return of this asset?",
                icon: "warning",
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyReturnRequest.aspx/AddToRequestForExtensionReturnDate",
                        data: "{ propertyno: '" + $('#txtPropertyNo').val() + "', custodianid: '" + custodianid + "', dateofreturn: '" + dateextension + "', remarks : '" + remarks + "', approverid : '0'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_data();
                            load_data_Extension();
                            //alert('Successfully Requested for Extension for date of return and pending approval.');
                            swal("Successful", "Successfully Requested for Extension for date of return and pending approval.", "success");
                            $('#addToExtensionList').modal('hide');
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
}

function load_data_Extension() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyReturnRequest.aspx/PropertyExtensionDateRequestList",
        data: "{search : '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendingrequestextension').hide();
            $('#pendingrequestextension').innerHTML = "";
            $('#pendingrequestextension').fadeIn(200);

            myJsonData = data;
            populateDataTableExtension(myJsonData);

            console.log(data);
            console.log('Success');
            GetCount();
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

var propertynoextend = '';

function populateDataTableExtension(data) {
    try {
        $('#tblPendingRequestExtension').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "Remarks" },
                { "data": "DateOfReturn" },
                { "data": "DateOfExtension" },
                { "data": "Action" }
            ]
            , dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' },
                               collectionLayout: 'two-column'
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblPendingRequestExtension').off('click');

        $('#tblPendingRequestExtension').on('click', 'a.btn_CancelExtension', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPendingRequestExtension').DataTable();
        table
            .clear()
            .draw();
    }
}

function CancelExtension(recid) {
    swal({
        title: "Cancel Transaction",
        text: "Are you sure you want to cancel this transaction?",
        icon: "warning",
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturnRequest.aspx/CancelExtension",
                data: "{ recid: '" + recid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Extension();
                    //alert('Successfully Cancelled the transaction.');
                    swal("Successful", "Successfully Cancelled the transaction.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}
var GetCounter = 0;
function GetCount() {
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '0';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
        custodianid = 0;
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyReturnRequest.aspx/GetCountPropertyReturnRequest",
        data: "{custodianid: '" + custodianid + "',ExpenseType: '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" Property Return Request List: " + msg.d);
            GetCounter = msg.d;
            if (GetCounter == 0) {
                document.getElementById("btnAssetItemTransfer").disabled = true;
            }
            else {
                document.getElementById("btnAssetItemTransfer").disabled = false;
            }
        }
    });
}

function RemoveAllToTurnIn() {
    $('.btn').prop('disabled', true);
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---') {
        custodianid = 0;
    }

    ////added by nero 02272024
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '0';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    ////ends here

    swal({
        title: "Cancel Transaction",
        text: "Are you sure you want to cancel this transaction?",
        icon: "warning",
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturnRequest.aspx/RemoveAllToPropertyReturnRequest",
                data: "{ custodianid: '" + custodianid + "' ,ExpenseType:'" + ExpenseType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('.btn').prop('disabled', false);
                    load_data();
                    $('#ReturnList').modal('hide');
                    swal("Successful", "Property Return Request cancelled successfully.", "success");
                }
            });
        } else {
            $('.btn').prop('disabled', false);
            $('#btnAssetItemTransfer').prop('disabled', true);
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmTurnIn() {
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '0';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    $('.btn').prop('disabled', true);
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
        custodianid = 0;
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to confirm this transaction?",
        icon: "warning",
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $(".se-pre-con").fadeIn();
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturnRequest.aspx/ConfirmPropertyReturnRequest",
                data: "{ custodianid: '" + custodianid + "',ExpenseType: '" + ExpenseType + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('.btn').prop('disabled', false);
                    //alert("Property successfully Request for Return");
                    swal("Successful", "Property successfully Request for Return.", "success");
                    $('#ReturnList').modal('hide');
                    load_data();
                    load_data_Pendingupload();
                    $(".se-pre-con").hide();
                }
            });
        } else {
            $('.btn').prop('disabled', false);
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    ////}
}

function ReturnList() {
    load_data();
    load_data_issuancelist();

    $('#ReturnList').modal('show');
    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}

function load_data_issuancelist() {
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    //added by nero 022624
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '0';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    //ends here

    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
        custodianid = 0;
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyReturnRequest.aspx/GetReturnPropertyList",
        data: "{ custodianid : '" + custodianid + "' , ExpenseType:'" + ExpenseType + "'}", // add ExpenseType by nero 202624
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#returnlist').hide();
            $('#returnlist').innerHTML = "";
            $('#returnlist').fadeIn(200);

            myJsonData = data;
            populateDataTableForIssuance(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTableForIssuance(data) {
    try {
        $('#tblReturnList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Action" }
            ], dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblReturnList').off('click');

        $('#tblReturnList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');
            //alert(pmlid);
            var custodianid = $('#BodyHolder_ddlCustodian').val();
            if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                custodianid = 0;

            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturnRequest.aspx/RemoveToPropertyReturnRequest",
                data: "{pmlid: '" + pmlid + "', custodianid: '" + custodianid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#issuancelist').hide();
                    $('#issuancelist').innerHTML = "";
                    $('#issuancelist').fadeIn(200);
                    load_data();
                    load_data_issuancelist();
                    GetCount();
                    currentRow.remove();
                }
            });
        });
    } catch (e) {
        var table = $('#tblReturnList').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_Pendingupload() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyReturnRequest.aspx/GetPropertyReturnPendingUpload",
        data: "{ search : '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendinguploadlist').hide();
            $('#pendinguploadlist').innerHTML = "";
            $('#pendinguploadlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUpload(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePendingUpload(data) {
    try {
        $('#tblPendingUpload').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ReturnRequestNumber" },
                //{ "data": "NoOfItemReturn" },
                { "data": "DateOfTransaction" },
                { "data": "Action" }
            ]
            , dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return Request'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblPendingUpload').off('click');

        $('#tblPendingUpload').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblPendingUpload').on('click', 'a.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var emp = ''
var returnrequestnumber = ''
var cnt = ''
var ctrchkr = ''
function ViewList(returnrequestno) {
    returnrequestnumber = returnrequestno;
    load_pending_list();
    document.getElementById('lblReturnRequestNumber').innerHTML = returnrequestno;
    $('#BodyHolder_txtReturnRequestNumber').val(returnrequestno);

    $('#AssetList1').modal('show');
}

function load_pending_list() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyReturnRequest.aspx/PropertyReturnRequestList",
        data: "{ returnrequestno : '" + returnrequestnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#purl').hide();
            $('#purl').innerHTML = "";
            $('#purl').fadeIn(200);

            myJsonData = data;
            populateALPending(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateALPending(data) {
    try {
        $('#tblPURL').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Status" }
            ], dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return History'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return History'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return History'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return History'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblPURL').off('click');
    } catch (e) {
        var table = $('#tblPURL').DataTable();
        table
            .clear()
            .draw();
    }
}

function Cancel(transactionno) {
    var textareatt = document.createElement("textarea");
    textareatt.type = "text";
    textareatt.placeholder = "Remarks";
    textareatt.maxLength = 100;
    textareatt.style.width = "100%";
    textareatt.style.height = "100px";
    textareatt.style.maxHeight = "100px";
    textareatt.style.minHeight = "100px";
    textareatt.style.maxWidth = "100%";
    textareatt.style.minWidth = "100%";
    textareatt.id = "cancelreturnrequest";
    swal({
        title: "Cancel Transaction",
        text: "Are you sure you want to cancel this transaction?",
        icon: "warning",
        reverseButtons: false,
        content: textareatt,
        buttons: {
            cancel: true,
            confirm: true,
        },

        showCancelButton: true,
        closeOnConfirm: false,
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            if (isConfirm === null) return false;

            var textremarks = $('#cancelreturnrequest').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturnRequest.aspx/CancelReturnRequest",
                data: "{ returnrequestno : '" + transactionno + "', remarks: '" + textremarks + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    swal("Successful", "Property Transfer cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}