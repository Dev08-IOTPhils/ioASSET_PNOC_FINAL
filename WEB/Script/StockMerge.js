$(document).ready(function () {
    $('.select2').select2();
    IfMergeNeedsApproval();
    $("#tblStockList").DataTable({
        "scrollX": true

        , dom: 'Brftip',
        buttons: {
            buttons: [
                    'copy',
                    'excel',
                    'csv',
                    //'pdf',
                    'print',
                    'colvis'
            ]
        }, colReorder: true
            , stateSave: true
    });
    $("#tblPending").DataTable({
        "scrollX": true
        , dom: 'Brftip',
        buttons: {
            buttons: [
                    'copy',
                    'excel',
                    'csv',
                    'print',
                    'colvis'
            ]
        }, colReorder: true
    });


    load_data();
    load_data_pending();

    $("#tblStockList").DataTable().clear().draw();
    $("#tblPending").DataTable().clear().draw();
});

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

var ItemCode = '';
var StockQuantity = 0;

function ReplaceStr(str) {
    var newStr = str.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
    return newStr;
}

var needsapproval = "False";
function IfMergeNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "StockMerge.aspx/IfMergeNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var x = document.getElementById("withorwithoutyou");
            var a = document.getElementById("withorwithoutyou1");
            var y = document.getElementById("labelApprover");
            var z = document.getElementById("dropdownApprover");
            if (msg.d == "True") {
                x.style.display = 'inline';
                a.style.display = 'none';
                y.style.display = 'inline';
                z.style.display = 'inline';
            }
            else {
                x.style.display = 'none';
                a.style.display = 'inline';
                y.style.display = 'none';
                z.style.display = 'none';
            }
        }
    });
}

function load_data() {

    var search = ReplaceStr("");
    $.ajax({
        type: 'POST',
        url: "StockMerge.aspx/GetList",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#stocklist').hide();
            $('#stocklist').innerHTML = "";
            $('#stocklist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData);

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

function populateDataTable(data) {
    try {

        $('#tblStockList').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "QuantityString" },
                { "data": "UnitOfMeasurement" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                { "data": "Action" }
            ]
            , dom: 'Brftip',
            buttons: {
                buttons: [
                        'copy',
                        'excel',
                        'csv',
                        //'pdf',
                        'print',
                        'colvis'
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblStockList').off('click');

        $('#tblStockList').on('click', 'a.btn_Merge', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var itemcode = currentRow.find("td:eq(0)").text();
            var stockquantity = currentRow.find("td:eq(1)").text();

            ItemCode = itemcode;
            StockQuantity = stockquantity;
            $('#txtItemCode').val(itemcode);
            $('#txtStockQuantity').val(stockquantity);
            GetStockItemCode();
            $('#mergeStock').modal('show');

        });
    } catch (e) {
        var table = $('#tblStockList').DataTable();
        table
            .clear()
            .draw();
    }
}

function GetStockItemCode() {
    $.ajax({
        type: "POST",
        url: "StockMerge.aspx/GetStockItemCode",
        data: " { itemcode : '" + ItemCode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlMergeTo = $("[id*=ddlMergeTo]");
            ddlMergeTo.empty().append('<option selected="selected" value="0">--- Select Item Code ---</option>');
            //ddlMergeTo.append('<option value="1">--- Select Item Code ---</option>');
            $.each(msg.d, function () {
                ddlMergeTo.append($("<option></option>").val(this['Text']).html(this['Value']));
            });
        }
    });
}

function MergeStock() {
    var mergefrom = ItemCode;
    var mergeto = $('#BodyHolder_ddlMergeTo').val();
    var ApproverID = $('#BodyHolder_ddlApprover').val();
    if (needsapproval == "True") {
        if ($('#BodyHolder_ddlMergeTo').val() == 0 || $('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = '';

            if ($('#BodyHolder_ddlMergeTo').val() == 0)
                alertMsg += '\nMerge To Item Code';
            if ($('#BodyHolder_txtRemarks').val() == '')
                alertMsg += '\nRemarks';
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
                alertMsg += '\nApprover';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else {
            swal({
                title: "Are you sure?",
                text: "Are you sure, you want to merge this supply?",
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
                        url: "StockMerge.aspx/Merge",
                        data: "{ mergefrom: '" + mergefrom + "', mergeto : '" + mergeto + "', approverid: '" + ApproverID + "', remarks: '" + $('#BodyHolder_txtRemarks').val() + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 'for approval') {
                                swal("Successful", "Successfully Requested for supply merge and waiting for approval.", "success");
                                load_data_pending();
                                load_data();
                                $("#mergeStock").modal("hide");

                            }
                            else if (msg.d == 'okay') {
                                swal("Successful", "Successfully Requested for supply merge and waiting for approval.", "success");
      
                                load_data();
                                load_data_pending();
                                $("#mergeStock").modal("hide");
                            }
                            else {
                       
                                swal("Warning", "Supplies Cannot be Merge because of Unit of Measurement Conflict.", "error");
                                load_data();
                                load_data_pending();
                                $("#mergeStock").modal("hide");
                            }

                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
    else {
        if ($('#BodyHolder_ddlMergeTo').val() == 0 || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = '';

            if ($('#BodyHolder_ddlMergeTo').val() == 0)
                alertMsg += '\nMerge To Item Code';
            if ($('#BodyHolder_txtRemarks').val() == '')
                alertMsg += '\nRemarks';
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        }
        else {
            swal({
                title: "Are you sure?",
                text: "Are you sure, you want to merge this supply??",
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
                        url: "StockMerge.aspx/Merge",
                        data: "{ mergefrom: '" + mergefrom + "', mergeto : '" + mergeto + "', approverid: '0', remarks: '" + $('#BodyHolder_txtRemarks').val() + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 'for approval') {
                                swal("Successful", "Successfully Requested for supply merge and waiting for approval.", "success");
                                load_data_pending();
                                load_data();
                                $("#mergeStock").modal("hide");

                            }
                            else if (msg.d == 'okay') {
                                swal("Successful", "Successfully Requested for supply merge and waiting for approval.", "success");
                                load_data();
                                load_data_pending();
                                $("#mergeStock").modal("hide");
                            }
                            else {
                                swal("Warning", "Supplies Cannot be Merge because of Unit of Measurement Conflict.", "error");
                                load_data();
                                load_data_pending();
                                $("#mergeStock").modal("hide");
                            }

                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
}

function load_data_pending() {

    var search = ReplaceStr("");
    $.ajax({
        type: 'POST',
        url: "StockMerge.aspx/GetPendingApprovalStockMerge",
        data: "{search: '" + search + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#pending').hide();
            $('#pending').innerHTML = "";
            $('#pending').fadeIn(200);

            myJsonData = data;
            populateDataTablepending(myJsonData);

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

function populateDataTablepending(data) {
    try {

        $('#tblPending').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "StockMergeNo" },
                { "data": "FromStockIC" },
                { "data": "ToStockIC" },
                { "data": "QuantityString" },
                { "data": "Remarks" },
                { "data": "TransactionDate" },
                { "data": "Action" }
            ]
        , dom: 'Brftip',
            buttons: {
                buttons: [
                        'copy',
                        'excel',
                        'csv',
                        //'pdf',
                        'print',
                        'colvis'
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblPending').off('click');
        $('#tblPending').on('click', 'a.btn_Cancel', function (e) {
            e.preventDefault();
        });


    } catch (e) {
        var table = $('#tblPending').DataTable();
        table
            .clear()
            .draw();
    }
}

function CancelStockMerge(stockmergeno) {
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
                url: "StockMerge.aspx/CancelStockmerge",
                data: "{stockmergeno: '" + stockmergeno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_pending();
                    //alert("Stock Merge cancelled successfully.");
                    swal("Successful", "Supply Merge cancelled successfully.", "success");
                }
            });

        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}