var idForUpdate = 0;
var recid = 0;
var url = '';
$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    GetUsersSR();
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
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
    GetCount();
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
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
    load_data();
    load_data_Pendingupload();

    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();

    $('#BodyHolder_btnComplete').fadeIn();
    //$('#BodyHolder_fuDocuments').hide();
    //$('#BodyHolder_btnUpload').hide();

    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_fuDocuments').fadeIn();
            $('#BodyHolder_btnUpload').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_fuDocuments').hide();
            $('#BodyHolder_btnUpload').hide();
        }
    });
});



var usersSR = new Array();
function GetUsersSR() {
    var context = window.location.pathname.substring(1);
    var ddlSupplier = $('#ddlEmployeeSR');
    ddlSupplier.empty();
    ddlSupplier.append($('<option>', {
        value: '0',
        text: '--- Select Employee ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetEmployeeSR',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                usersSR = JSON.parse(xml.d);
                usersSR.forEach((usr, index) => {
                    ddlSupplier.append($('<option>', {
                        value: usr.UserID,
                        text: usr.FullName,
                        rowindex: index
                    }));
                });
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            //alert(error);
            console.log(xhr);
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
function load_data() {
    var search = "";
    var accountable = $('#ddlEmployeeSR').val();
    if ($('#ddlEmployeeSR').val() == '0') {
        accountable = 0;
    }
    $.ajax({
        type: 'POST',
        url: url + "/StockReturn.aspx/GetStockAvailableForReturn",
        data: "{ accountable: '" + accountable + "', search: '" + search + "'}",
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
            GetCount();
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your return...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTable(data) {
    try {
        $('#tblAssetList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "QuantityString" },
                { "data": "Description" },
                { "data": "Location" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
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

        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function addToReturnList(Recid, itemcode, quantity) {
    recid = Recid;
    $('#txtItemcode').val(itemcode);
    $('#txtQuantity').val(quantity);
    $('#AddToReturnModal').modal('show');
}

function TransferToReturn() {
    var accountable = $('#ddlEmployeeSR').val();
    if ($('#ddlEmployeeSR').val() == '0') {
        accountable = 0;
    }
    var returnquantity = $('#txtReturnQuantity').val();
    if (parseInt($('#txtReturnQuantity').val()) <= 0 || $('#txtReturnQuantity').val() == "") {
        //alert("Return Quantity should not be BLANK or LESS THAN OR EQUAL TO ZERO.");
        swal("Warning! ", "Return Quantity should not be BLANK or LESS THAN OR EQUAL TO ZERO.", "warning");
    }
    else if (parseInt($('#txtReturnQuantity').val()) > parseInt($('#txtQuantity').val())) {
        //alert("Return Quantity should not be Greater than the Quantity.");
        swal("Warning! ", "Return Quantity should not be Greater than the Quantity.", "warning");
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/StockReturn.aspx/TransferToReturn",
            data: "{ accountable : '" + accountable + "', recid: '" + recid + "', returnquantity: '" + returnquantity + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                recid = 0;
                load_data();
                $('#AddToReturnModal').modal('hide');
            }
        });
    }
}

function RemoveAllToStockReturn() {
    //if (confirm("Are you sure you want to cancel this transaction?") == false) return false;
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to cancel this transaction?",
        icon: "warning",
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            var accountable = $('#ddlEmployeeSR').val();
            if ($('#ddlEmployeeSR').val() == '0') {
                accountable = 0;
            }
            $.ajax({
                type: "POST",
                url: url + "/StockReturn.aspx/RemoveAllToStockReturn",
                data: "{ accountable : '" + accountable + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    $('#ReturnStockList').modal('hide');
                    swal("Success", "Cancelled by user", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmStockReturn() {
    var accountable = $('#ddlEmployeeSR').val();
    //if ($('#ddlEmployee').val() == '--- Select Employee ---') {
    //    accountable = 0;
    //}
    if ($('#BodyHolder_txtRemarks').val().trim() == "") {
        alertMsg = ' ';
        if ($('#BodyHolder_txtRemarks').val() == "")
            alertMsg += "\nRemarks ";
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
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
                    url: url + "/StockReturn.aspx/ConfirmStockReturn",
                    data: "{ accountable : '" + accountable + "',  remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert("Stocks successfully returned and waiting for\n uploading of Documents.");
                        swal("Successful", "Supplies successfully requested for return and waiting for\n uploading of Documents.", "success");
                        $('#ReturnStockList').modal('hide');
                        load_data();
                        load_data_Pendingupload();
                        $(".se-pre-con").hide();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

var GetCounter = 0;
function GetCount() {
    var employeeid = $('#ddlEmployeeSR').val();
    if ($('#ddlEmployeeSR').val() == '0') {
        employeeid = 0;
    }
    $.ajax({
        type: "POST",
        url: url + "/StockReturn.aspx/StockReturnGetCount",
        data: "{ employeeid: '" + employeeid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Supply List for Return: " + msg.d);
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

function ReturnList() {
    $('#BodyHolder_txtRemarks').val('');
    //$('#ReturnStockList').modal('show');


    if ($('#ddlEmployeeSR').val() == '0') {
        swal("Warning", "Select Employee", "warning");
    }
    else if (GetCounter == 0) {
        swal("Warning", "Please select an item to add", "warning");
    } else {
        load_data();
        load_data_returnlist();
        $('#ReturnStockList').modal('show');
    }


    //if (GetCounter == 0) {
    //    document.getElementById("btnAssetItemTransfer").disabled = true;
    //}
    //else {
    //    document.getElementById("btnAssetItemTransfer").disabled = false;
    //}
}

function load_data_returnlist() {
    var employeeid = $('#ddlEmployeeSR').val();
    if ($('#ddlEmployeeSR').val() == '0') {
        employeeid = 0;
    }
    $.ajax({
        type: 'POST',
        url: url + "/StockReturn.aspx/GetStockReturnList",
        data: "{ accountable: '" + employeeid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#returnlist').hide();
            $('#returnlist').innerHTML = "";
            $('#returnlist').fadeIn(200);

            myJsonData = data;
            populateDataTableForReturn(myJsonData);

            console.log(data);
            console.log('Success');
            //GetCount();
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your return...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTableForReturn(data) {
    try {
        $('#tblReturnList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" },
                { "data": "Action" }
            ],
            "rowCallback": function (row, data, index) {
                recid = data.RecIDstring;
            }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
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
            $.ajax({
                type: "POST",
                url: url + "/StockReturn.aspx/RemoveToStockReturn",
                data: "{recid: '" + recid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    currentRow.remove();
                    GetCount();
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
        url: url + "/StockReturn.aspx/StockReturnPendingUpload",
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
            console.log("There was an error with your return...");
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
                { "data": "ReturnNo" },
                { "data": "IssuedTo" },
                { "data": "Remarks" },
                //{ "data": "NoOfItem" },
                { "data": "TransactionDate" },
                { "data": "Status" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
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

        $('#tblPendingUpload').on('click', 'button.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblPendingUpload').on('click', 'button.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

function CancelStockReturn(returnno) {
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
                url: url + "/StockReturn.aspx/CancelStockReturn",
                data: "{returnno: '" + returnno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    //alert("Stock Return cancelled successfully.");
                    swal("Successful", "Supply Return cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

var returnno = '';
function ViewStocks(stockreturnno) {
    document.getElementById('lblStockReturnNo').innerHTML = stockreturnno;
    $('#BodyHolder_txtStockReturnNo').val(stockreturnno);
    returnno = stockreturnno;

    load_data_PUL1();

    $('#StockReturnDetailsPendingUpload').modal('show');
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/StockReturn.aspx/StockDetailsPendingUpload",
        data: "{returnno: '" + returnno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#purl').hide();
            $('#purl').innerHTML = "";
            $('#purl').fadeIn(200);

            myJsonData = data;

            populateDataTablePUL1(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your return...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePUL1(data) {
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                { "data": "QuantityString" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Return'
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

function StockReturnComplete() {
    var stockreturnId = $('#BodyHolder_txtStockReturnNo').val();
    var filess = $('#BodyHolder_fuDocuments').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    //if (filess == "") {
    //    swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
    //}
    //else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to complete this transaction?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                $(function () {
                    var fileUpload = $("#BodyHolder_fuDocuments").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + stockreturnId + "|StockReturn",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/StockReturn.aspx/UploadStockReturn",
                                    data: " { returnno : '" + stockreturnId + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_Pendingupload();
                                        $('#StockReturnDetailsPendingUpload').modal('hide');
                                        swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                    }
                                });
                            } else {
                                swal("Warning", result, "warning");
                            }
                        },
                        error: function (err) {
                            swal("Warning", err.statusText, "warning");
                        }
                    });
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    //}
}