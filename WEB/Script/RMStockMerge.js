$(document).ready(function () {
    $('.select2').select2();
   
    load_data_pendingStockMerge();
    $("#tblPendingStockMerge").DataTable().clear().draw();

});



function load_data_pendingStockMerge() {

    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPendingApprovalStockMerge",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#pendingstockmerge').hide();
            $('#pendingstockmerge').innerHTML = "";
            $('#pendingstockmerge').fadeIn(200);

            myJsonData = data;
            populateDataTablependingStockMerge(myJsonData);

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

function populateDataTablependingStockMerge(data) {
    try {

        $('#tblPendingStockMerge').DataTable({
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
                { "data": "Approver" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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

        $('#tblPendingStockMerge').off('click');
        $('#tblPendingStockMerge').on('click', 'a.btn_ApproveStockMerge', function (e) {
            e.preventDefault();
        });
        $('#tblPendingStockMerge').on('click', 'a.btn_DeclineStockMerge', function (e) {
            e.preventDefault();
        });

    } catch (e) {
        var table = $('#tblPendingStockMerge').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveStockMerge(stockmergeno) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this transaction?",
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
                url: "Home/RequestManagement.aspx/ApproveStockMerge",
                data: "{stockmergeno: '" + stockmergeno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_pendingStockMerge();
                    //alert("Stock Merge approved successfully.");
                    swal("Success", "Supply Merge successfully APPROVED.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });

}

function DeclineStockMerge(stockmergeno) {
    //if (confirm("Are you sure you want to Decline this stock merge?") == false)
    //    return false;
    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
                url: "Home/RequestManagement.aspx/DeclineStockMerge",
                data: "{stockmergeno: '" + stockmergeno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_pendingStockMerge();
                    //alert("Stock Merge declined successfully.");
                    swal("Success", "Supply Merge successfully DECLINED.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}