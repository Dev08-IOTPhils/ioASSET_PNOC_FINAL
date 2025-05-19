$(document).ready(function () {
    $("#tblStock").DataTable({
        "scrollX": true,
        dom: 'Brftip',
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
        "sorting": false,
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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

    load_data_stockGR();
    IfNeedsApproval1();
});

var needsapproval1 = "False";
function IfNeedsApproval1() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/IfNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval1 = msg.d;
        }
    });
}


function EnterKeySearch1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_stockGR();
        return false;
    }
}

function ValidateDate(dateString) {
    //Get the date from the TextBox.
    var dtCurrent = new Date();
    var parts = dateString.split("/");
    var dt = new Date(parts[0] + "/" + parts[1] + "/" + parts[2]);
    if (dt > dtCurrent)
        return false;
    return true;
}

function load_data_stockGR() {
    var search = "";
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Operation_Module/GoodsReceive.aspx/GetStock",
        data: "{search: '" + search + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stock').hide();
            $('#stock').innerHTML = "";
            $('#stock').fadeIn(200);
            myJsonData = data;
            populateDataTableStockGR(myJsonData);
            console.log(data);
            console.log('Success');
            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTableStockGR(data) {
    try {
        $('#tblStock').DataTable({
            "destroy": true,
            "processing": true,
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
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
                { "data": "Action" }
            ],
            dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        $('#tblStock').off('click');

    } catch (e) {
        var table = $('#tblStock').DataTable();
        table
            .clear()
            .draw();
    }
}

function AddToGoodsReceiveStock(id, itemcode) {
    var alertMsg;

    if (needsapproval1 == "True") {
        if ($('#ddlFromSite').val() == '0' || $('#ddlLocation1').val() == '0' || $('#ddlSupplier').val() == '0' || $('#txtDOA').val().trim() == '' || $('#txtPRNumber').val().trim() == '' ||
            $('#ddlReceivedBy').val() == '0' || $('#txtPONumber').val().trim() == '' || $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ||
             $('#ddlApprover').val() == '0' || $('#BodyHolder_txtRemarks').val().trim() == '' || $('#ddlReceivedBy').val() == "0" || $('#ddlApprover').val() == "0") {
            alertMsg = ' ';

            if ($('#ddlSupplier').val() == '0')
                alertMsg += '\nSupplier';
            if ($('#txtDOA').val().trim() == '')
                alertMsg += '\nDate of Acquisition';
            if ($('#txtPRNumber').val().trim() == '')
                alertMsg += '\nPR Number';
            if ($('#ddlReceivedBy').val() == '0' || $('#ddlReceivedBy').val() == "0")
                alertMsg += '\nReceived By';
            if ($('#txtPONumber').val().trim() == '')
                alertMsg += '\nPO Number';
            if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                alertMsg += '\nCustodian Type';
            //if ($('#txtInvoiceNumber').val().trim() == '')
            //    alertMsg += '\nInvoice Number';
            if ($('#ddlApprover').val() == '0' || $('#ddlApprover').val() == "0")
                alertMsg += '\nApprover';
            //if ($('#txtDRNumber').val().trim() == '')
            //    alertMsg += '\nDR Number';
            if ($('#BodyHolder_txtRemarks').val().trim() == '')
                alertMsg += '\nRemarks';

            if ($('#ddlFromSite').val() == '0')
                alertMsg += '\nLocation';
            if ($('#ddlLocation1').val() == '0')
                alertMsg += '\nDepartment';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else if (ValidateDate(document.getElementById('txtDOA').value) == false) {
            alert('Given date should not be greater than today`s date.');
        }
        else {
            idForUpdate = id;
            $('#txtStockItemCode').val(itemcode);
            $("#addToStockGoodsReceive").modal("show");
        }
    }
    else {
        if ($('#ddlFromSite').val() == '0' || $('#ddlLocation1').val() == '0' || $('#ddlSupplier').val() == '0' || $('#txtDOA').val().trim() == '' || $('#txtPRNumber').val().trim() == '' ||
            $('#ddlReceivedBy').val() == '--- Select User ---' || $('#txtPONumber').val().trim() == '' || $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ||
           $('#BodyHolder_txtRemarks').val().trim() == '' || $('#ddlReceivedBy').val() == "0") {
            alertMsg = ' ';

            if ($('#ddlSupplier').val() == '0')
                alertMsg += '\nSupplier';
            if ($('#txtDOA').val().trim() == '')
                alertMsg += '\nDate of Acquisition';
            if ($('#txtPRNumber').val().trim() == '')
                alertMsg += '\nPR Number';
            if ($('#ddlReceivedBy').val() == '0' || $('#ddlReceivedBy').val() == "0")
                alertMsg += '\nReceived By';
            if ($('#txtPONumber').val().trim() == '')
                alertMsg += '\nPO Number';
            if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                alertMsg += '\nCustodian';
            //if ($('#txtInvoiceNumber').val().trim() == '')
            //    alertMsg += '\nInvoice Number';
            //if ($('#txtDRNumber').val().trim() == '')
            //    alertMsg += '\nDR Number';
            if ($('#BodyHolder_txtRemarks').val().trim() == '')
                alertMsg += '\nRemarks';
            if ($('#ddlFromSite').val() == '0')
                alertMsg += '\nSite';
            if ($('#ddlLocation1').val() == '0')
                alertMsg += '\nLocation';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else if (ValidateDate(document.getElementById('txtDOA').value) == false) {
            alert('Given date should not be greater than today`s date.');
        }
        else {
            idForUpdate = id;
            $('#txtStockItemCode').val(itemcode);
            $("#addToStockGoodsReceive").modal("show");
        }
    }
}


function AddToStockGoodsReceive() {

    if (needsapproval1 == "True") {
        var supplier = $('#ddlSupplier').val();
        var dateofacquisition = $('#txtDOA').val();
        var prnumber = $('#txtPRNumber').val();
        var receivedby = $('#ddlReceivedBy').val();
        var ponumber = $('#txtPONumber').val();
        var custodian = $('#BodyHolder_ddlCustodian').val();
        var invoicenumber = $('#txtInvoiceNumber').val();
        var approver = $('#ddlApprover').val();
        var drnumber = $('#txtDRNumber').val();
        var remarks = $('#BodyHolder_txtRemarks').val();
        var quantity = parseInt($("#txtStockQuantity").val());
        var acquiredcost = $("#txtStockAcquiredCost").val();
        var site = $('#ddlFromSite').val();
        var assignedperson = $('#ddlEmployee').val();
        if (assignedperson == '0') {
            assignedperson = 0;
        }
        var location = $('#ddlLocation1').val();
        if ($('#txtDRNumber').val() == '')
            drnumber = 'N/A';
        if ($('#txtInvoiceNumber').val() == '')
            invoicenumber = 'N/A';
        var alertMsg;
        if ($("#txtStockQuantity").val() == '' || $("#txtStockAcquiredCost").val() == '' || $('#txtStockQuantity').val() == '0') {
            alertMsg = ' ';
            if ($('#txtStockQuantity').val() == '' || $('#txtStockQuantity').val() == '0')
                alertMsg += '\nQuantity';
            if ($('#txtStockAcquiredCost').val() == '')
                alertMsg += '\nAcquired Cost';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else {
            //if (confirm('Are You sure you want to add this stock to list?') == false)
            //    return false;

            swal({
                title: "Add Supply?",
                text: "Are You sure you want to add this supply to list?",
                icon: "warning",
                reverseButtons: false,
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/GoodsReceive.aspx/AddToStockList",
                        data: " { stockid: '" + idForUpdate + "',supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                            + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                            + remarks + "', quantity : '" + quantity + "',acquiredcost : '" + acquiredcost + "',site : '" + site + "',assignedperson : '" + assignedperson + "',location : '" + location + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            GetCount();
                            console.log("successfully added to list (" + msg.d + ")");
                            //alert('Stock Added Successfully!');
                            swal("Success", "Supply Added Successfully!", "success");
                            $("#addToStockGoodsReceive").modal("hide");
                            ClearStockGR();
                            //ClearGR();
                        }


                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
    else {
        var supplier = $('#ddlSupplier').val();
        var dateofacquisition = $('#txtDOA').val();
        var prnumber = $('#txtPRNumber').val();
        var receivedby = $('#ddlReceivedBy').val();
        var ponumber = $('#txtPONumber').val();
        var custodian = $('#BodyHolder_ddlCustodian').val();
        var invoicenumber = $('#txtInvoiceNumber').val();
        var approver = 0
        var drnumber = $('#txtDRNumber').val();
        var remarks = $('#BodyHolder_txtRemarks').val();
        var quantity = parseInt($("#txtStockQuantity").val());
        var acquiredcost = $("#txtStockAcquiredCost").val();
        var site = $('#ddlFromSite').val();
        var assignedperson = $('#ddlEmployee').val();
        if (assignedperson == '0') {
            assignedperson = 0;
        }
        var location = $('#ddlLocation1').val();
        if ($('#txtDRNumber').val() == '')
            drnumber = 'N/A';
        if ($('#txtInvoiceNumber').val() == '')
            invoicenumber = 'N/A';
        var alertMsg;
        if ($("#txtStockQuantity").val() == '' || $("#txtStockAcquiredCost").val() == '' || $('#txtStockQuantity').val() == '0') {
            alertMsg = ' ';
            if ($('#txtStockQuantity').val() == '' || $('#txtStockQuantity').val() == '0')
                alertMsg += '\nQuantity';
            if ($('#txtStockAcquiredCost').val() == '')
                alertMsg += '\nAcquired Cost';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else {
            //if (confirm('Are You sure you want to add this stock to list?') == false)
            //    return false;

            swal({
                title: "Add Supply?",
                text: "Are You sure you want to add this supply to list?",
                icon: "warning",
                reverseButtons: false,
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/GoodsReceive.aspx/AddToStockList",
                        data: " { stockid: '" + idForUpdate + "',supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                            + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                            + remarks + "', quantity : '" + quantity + "',acquiredcost : '" + acquiredcost + "',site : '" + site + "',assignedperson : '" + assignedperson + "',location : '" + location + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            GetCount();
                            console.log("successfully added to list (" + msg.d + ")");
                            //alert('Stock Added Successfully!');
                            swal("Success", "Supply Added Successfully!", "success");
                            $("#addToStockGoodsReceive").modal("hide");
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
}

function ClearStockGR() {
    $('#txtStockQuantity').val('');
    $('#txtStockAcquiredCost').val('');

}

//function ClearGR() {
//    //dothis
//    $('#ddlSupplier').val('0').trigger('change');
//    $('#txtPRNumber').val('');
//    $('#txtPONumber').val('');
//    $('#ddlFromSite').val(0).trigger('change');
//    $('#txtInvoiceNumber').val('');
//    $('#txtDRNumber').val('');
//    $('#BodyHolder_ddlCustodian').val('--- Select Custodian ---').trigger('change');
//    $('#ddlReceivedBy').val(0).trigger('change');
//    $('#ddlLocation1').val(0).trigger('change');
//    $('#ddlEmployee').val('0').trigger('change');
//    $('#BodyHolder_txtRemarks').val('');
//    $('#ddlApprover').val('0');
//}


//function StockAddedList() {

//    load_data_stockaddedlist();
//    $("#viewAddedStockList").modal("show");
//}

//function load_data_stockaddedlist() {
//    var supplier = $('#ddlSupplier').val();
//    var dateofacquisition = $('#txtDOA').val();
//    var prnumber = $('#txtPRNumber').val();
//    var receivedby = $('#BodyHolder_ddlReceivedBy').val();
//    var ponumber = $('#txtPONumber').val();
//    var custodian = $('#BodyHolder_ddlCustodian').val();
//    var invoicenumber = $('#txtInvoiceNumber').val();
//    var approver = $('#BodyHolder_ddlApprover').val();
//    var drnumber = $('#txtDRNumber').val();
//    var remarks = $('#BodyHolder_txtRemarks').val();
//    if ($('#txtDRNumber').val() == '')
//        drnumber = 'N/A';
//    if ($('#txtInvoiceNumber').val() == '')
//        invoicenumber = 'N/A';
//    if ($('#ddlSupplier').val() == '--- Select Supplier ---')
//        supplier = 0;
//    if ($('#BodyHolder_ddlReceivedBy').val() == '--- Select User ---')
//        receivedby = 0;
//    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian ---')
//        custodian = 0;
//    if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
//        approver = 0;

//    $.ajax({
//        type: 'POST',
//        url: "Operation_Module/GoodsReceive.aspx/GetStockAddedList",
//        data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
//                + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
//                + remarks + "' }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            $('#stockaddedlist').hide();
//            $('#stockaddedlist').innerHTML = "";
//            $('#stockaddedlist').fadeIn(200);
//            myJsonData = data;
//            populateviewStockAddedList(myJsonData);
//            console.log(data);
//            console.log('Success');
//        },
//        error: function (e) {
//            alert(e);
//            console.log("There was an error with your request...");
//            console.log("error: " + JSON.stringify(e));
//        }
//    });
//}

//function populateviewStockAddedList(data) {
//    try {
//        var table = $('#tblStockAddedList').DataTable({
//            "destroy": true,
//            "processing": true,
//            "language": {
//                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
//            },
//            "scrollX": true,
//            "data": JSON.parse(data.d),
//            "columns": [

//                { "data": "ItemCode" },
//                { "data": "PRNumber" },
//                { "data": "PONumber" },
//                { "data": "InvoiceNumber" },
//                { "data": "DRNumber" },
//                { "data": "Remarks" },
//                { "data": "QuantityString" },
//                { "data": "Action" }
//            ]
//        });
//        $('#tblStockAddedList').off('click');

//        $('#tblStockAddedList').on('click', 'button.btn_Remove', function (e) {
//            e.preventDefault();
//            var currentRow = $(this).closest("tr");
//            currentRow.remove();
//        });


//    } catch (e) {
//        var table = $('#tblStockAddedList').DataTable();
//        table
//            .clear()
//            .draw();
//    }
//}

//function RemoveToGoodsReceiveStock(grstockid) {
//    $.ajax({
//        type: "POST",
//        url: "Operation_Module/GoodsReceive.aspx/RemoveToStockList",
//        data: " { grstockid : '" + grstockid + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            console.log("successfully removed to list");
//        }
//    });
//}

//function RemoveAllToGoodsReceiveStock() {
//    if (confirm("Are you sure you want to cancel this transaction?") == false)
//        return false;
//    $.ajax({
//        type: "POST",
//        url: "Operation_Module/GoodsReceive.aspx/RemoveAllToStockList",
//        data: " { } ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            $("#viewAddedStockList").modal("hide");
//            alert("Transaction successfully cancelled.")
//        }
//    });
//}

//function ConfirmGoodsReceiveStock() {
//    if (confirm("Are you sure you want to Confirm this transaction?") == false)
//        return false;

//    var supplier = $('#ddlSupplier').val();
//    var dateofacquisition = $('#txtDOA').val();
//    var prnumber = $('#txtPRNumber').val();
//    var receivedby = $('#BodyHolder_ddlReceivedBy').val();
//    var ponumber = $('#txtPONumber').val();
//    var custodian = $('#BodyHolder_ddlCustodian').val();
//    var invoicenumber = $('#txtInvoiceNumber').val();
//    var approver = $('#BodyHolder_ddlApprover').val();
//    var drnumber = $('#txtDRNumber').val();
//    var remarks = $('#BodyHolder_txtRemarks').val();
//    if ($('#txtDRNumber').val() == '')
//        drnumber = 'N/A';
//    if ($('#txtInvoiceNumber').val() == '')
//        invoicenumber = 'N/A';
//    if ($('#ddlSupplier').val() == '--- Select Supplier ---')
//        supplier = 0;
//    if ($('#BodyHolder_ddlReceivedBy').val() == '--- Select User ---')
//        receivedby = 0;
//    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian ---')
//        custodian = 0;
//    if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
//        approver = 0;

//    $.ajax({
//        type: "POST",
//        url: "Operation_Module/GoodsReceive.aspx/ConfirmGoodsReceiveStock",
//        data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
//                       + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
//                       + remarks + "' }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            //load_data_GRH();
//            GetCount();
//            $("#viewAddedStockList").modal("hide");
//            alert("Transaction successfully confirmed.")
//        }
//    });
//}

//function GetCount() {
//    var supplier = $('#ddlSupplier').val();
//    var dateofacquisition = $('#txtDOA').val();
//    var prnumber = $('#txtPRNumber').val();
//    var receivedby = $('#BodyHolder_ddlReceivedBy').val();
//    var ponumber = $('#txtPONumber').val();
//    var custodian = $('#BodyHolder_ddlCustodian').val();
//    var invoicenumber = $('#txtInvoiceNumber').val();
//    var approver = $('#BodyHolder_ddlApprover').val();
//    var drnumber = $('#txtDRNumber').val();
//    var remarks = $('#BodyHolder_txtRemarks').val();
//    if ($('#txtDRNumber').val() == '')
//        drnumber = 'N/A';
//    if ($('#txtInvoiceNumber').val() == '')
//        invoicenumber = 'N/A';
//    if ($('#ddlSupplier').val() == '--- Select Supplier ---')
//        supplier = 0;
//    if ($('#BodyHolder_ddlReceivedBy').val() == '--- Select Receiver ---')
//        receivedby = 0;
//    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian ---')
//        custodian = 0;
//    if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
//        approver = 0;
//    $.ajax({
//        type: "POST",
//        url: "Operation_Module/GoodsReceive.aspx/GetCountGoodsReceive",
//        data: "{ supplier: '" + supplier + "',dateofacquisition: 'voicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
//                + remarks + "'}"," + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
//                + ponumber + "',custodian: '" + custodian + "',in
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            $("#btnPL span").text(" View List For Good Receive: " + msg.d);
//            if (msg.d == 0) {
//                $('#viewAddedPropertyList').modal('hide');
//            }
//            else { }
//        }
//    });
//}