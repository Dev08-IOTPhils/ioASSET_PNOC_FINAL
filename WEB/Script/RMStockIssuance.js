var idForUpdate = 0;

$(document).ready(function () {
    load_data_StockIssuance();
});

function load_data_StockIssuance() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/StockIssuancePendingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockissuancelist').hide();
            $('#stockissuancelist').innerHTML = "";
            $('#stockissuancelist').fadeIn(200);

            myJsonData = data;
            populateDataStockIssuance(myJsonData);

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

function populateDataStockIssuance(data) {
    try {
        $('#tblStockIssuance').DataTable({
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
                { "data": "IssuanceNo" },
                { "data": "IssuedTo" },
                //{ "data": "ToSiteStr" },
                { "data": "Remarks" },
                { "data": "TransactionDate" },
                { "data": "Approver" },
                { "data": "Status" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
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

        $('#tblStockIssuance').off('click');

        $('#tblStockIssuance').on('click', 'button.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblStockIssuance').on('click', 'button.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblStockIssuance').DataTable();
        table
            .clear()
            .draw();
    }
}

var issuancenumber = '';
var accountableName = '';
var transactionEmail = '';
function ViewStockIssuanceDetails(issuanceno, accountable, transactionemail) {
    issuancenumber = issuanceno;
    transactionEmail = transactionemail;
    accountableName = accountable;
    document.getElementById('lblStockIssuanceNo').innerHTML = issuanceno;
    document.getElementById('lblStockAccountablePerson').innerHTML = accountable;

    load_data_StockIssuanceDetails();
    $('#viewStockIssuanceDetails').modal('show');
}

function load_data_StockIssuanceDetails() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/StockDetailsPendingApproval",
        data: "{stockissuanceno: '" + issuancenumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockissuancedetails').hide();
            $('#stockissuancedetails').innerHTML = "";
            $('#stockissuancedetails').fadeIn(200);

            myJsonData = data;

            populateDataStockIssuanceDets(myJsonData);

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

function populateDataStockIssuanceDets(data) {
    try {
        $('#tblStockIssuanceDetails').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
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

        $('#tblStockIssuanceDetails').off('click');
    } catch (e) {
        var table = $('#tblStockIssuanceDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveStockIssuance() {
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
            $(".se-pre-con").fadeIn();
            $.ajax({
                type: "POST",
                url: "Home/RequestManagement.aspx/ApproveStockIssuance",
                data: "{issuanceno:'" + issuancenumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //if (msg.d == "1") {
                    load_data_StockIssuance();
                    $('#viewStockIssuanceDetails').modal('hide');
                    swal("Success", "Supply Issuance successfully APPROVED.", "success");
                    $(".se-pre-con").hide();
                    //}
                    //else {
                    //    load_data_PropertyIssuance();
                    //    $('#viewPropertyIssuanceDetails').modal('hide');
                    //    swal("Success", "Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    //}
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineStockIssuance() {
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
    textareatt.id = "declinestockissuancetextall";

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

        var textremarks = $('#declinestockissuancetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }
        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclineStockIssuance",
            data: "{issuanceno:'" + issuancenumber + "',remarkS:'" + textremarks + "',transactoremail:'" + transactionEmail + "',name:'" + accountableName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_StockIssuance();
                    $('#viewStockIssuanceDetails').modal('hide');
                    swal("Success", "Supply Issuance successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_StockIssuance();
                    $('#viewStockIssuanceDetails').modal('hide');
                    swal("Success", "Supply Issuance successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_StockIssuance();
                    $('#viewStockIssuanceDetails').modal('hide');
                    swal("Success", "Supply Issuance successfully DECLINED.\n\nMail not Sent. Inspector has no email.", "success");
                }

                $(".se-pre-con").hide();
            }
        });
    });
}