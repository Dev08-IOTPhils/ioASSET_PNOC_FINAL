$(document).ready(function () {
    console.log('RMPropertyReTagging');
    $("#tblPropertyRetagging").DataTable({
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
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
            , stateSave: true,
        'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
    });
    checkRetaggingApproval();
    Load_Data();
});

function Load_Data() {
    $.ajax({
        type: "POST",
        url: "Home/RequestManagement.aspx/GetPropertyRetaggingHeader",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#PropertyRetagging').hide();
            $('#PropertyRetagging').innerHTML = "";
            $('#PropertyRetagging').fadeIn(200);

            myJsonData = msg;
            populateDataTable(myJsonData);

            console.log(msg);
            console.log('Success ReTagging');
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
        $('#tblPropertyRetagging').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "pagingType": "full_numbers",
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TransactionNumber" },
                { "data": "CreatedBy" },
                { "data": "Remarks" },
                { "data": "Action" }
            ]
            , dom: 'Bfrtip',
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

        $('#tblPropertyRetagging').off('click');
    } catch (e) {
        var table = $('#tblPropertyRetagging').DataTable();
        table
            .clear()
            .draw();
    }
}

var trxNo = '';
var TransactorEmail = '';
var TransactorName = '';

function viewRetaggingDetails(trxNumber, TrxrEmail, TrxrName) {
    trxNo = trxNumber;
    TransactorEmail = TrxrEmail;
    TransactorName = TrxrName;

    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/PropertyRetaggingApprovalDetails",
        data: "{TransactionNumber: '" + trxNumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //$('#ViewPropertyRetaggingDetails').hide();
            //$('#ViewPropertyRetaggingDetails').innerHTML = "";
            //$('#ViewPropertyRetaggingDetails').fadeIn(200);

            myJsonData = data;

            populateDataPropertyRetaggingDetails(myJsonData);

            console.log(data);
            console.log('Success viewRetaggingDetails');

            $('#ViewPropertyRetaggingDetails').modal('show');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataPropertyRetaggingDetails(data) {
    try {
        $('#tblpropertyRetaggingDetails').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [

                { "data": "TransactionNumber" },
                { "data": "PropertyNumber" },
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

        $('#tblpropertyRetaggingDetails').off('click');
    } catch (e) {
        var table = $('#tblpropertyRetaggingDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApprovePropertyRetagging() {
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
                url: "Home/RequestManagement.aspx/ApprovePropertyRetagging",
                data: "{transactionNumber:'" + trxNo + "', transactorname: '" + TransactorName + "', transactoremail: '" + TransactorEmail + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        $("#ViewPropertyRetaggingDetails").modal("hide");
                        Load_Data();
                        swal("Success", "Property Retagging successfully APPROVED.", "success");
                    }
                    else {
                        $("#ViewPropertyRetaggingDetails").modal("hide");
                        Load_Data();
                        swal("Success", "Property Retagging successfully APPROVED. \n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyRetagging() {
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
    textareatt.id = "declinegoodsreceivetextall";

    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
    }).then((value) => {
        if (value === null) return false;

        var textremarks = $('#declinegoodsreceivetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }

        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclinePropertyRetagging",
            data: "{transactionNumber:'" + trxNo + "', transactorname: '" + TransactorName + "', transactoremail: '" + TransactorEmail + "', remarkS: '" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    $("#ViewPropertyRetaggingDetails").modal("hide");
                    Load_Data();
                    swal("Success", "Property Retagging successfully DECLINED.", "success");
                }
                else {
                    $("#ViewPropertyRetaggingDetails").modal("hide");
                    Load_Data();
                    swal("Success", "Property Retagging successfully DECLINED. \n\nMail not Sent. Please check Mail Setting.", "success");
                }
            }
        });
    });
}

function checkRetaggingApproval() {
    $.ajax({
        type: "POST",
        url: "Home/RequestManagement.aspx/checkRetaggingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "True") {
                $("#rtgTab").show();
            }
            else {
                $("#rtgTab").hide();
            }
        }
    });
}