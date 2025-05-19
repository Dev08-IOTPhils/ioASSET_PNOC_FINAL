var idForUpdate = 0;

$(document).ready(function () {
    load_data_StockRequest();
});

function load_data_StockRequest() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/StockRequestPendingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockrequestlist').hide();
            $('#stockrequestlist').innerHTML = "";
            $('#stockrequestlist').fadeIn(200);

            myJsonData = data;
            populateDataStockRequest(myJsonData);

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

function populateDataStockRequest(data) {
    try {
        $('#tblStockRequest').DataTable({
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
                { "data": "RequestNo" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
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

        $('#tblStockRequest').off('click');

        $('#tblStockRequest').on('click', 'button.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblStockRequest').on('click', 'button.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblStockRequest').DataTable();
        table
            .clear()
            .draw();
    }
}

var requestnumber = '';
var accountableEmail = '';
var accountableName = '';
function ViewStockRequestDetails(requestno, accountable, accountableemail, accountablename) {
    requestnumber = requestno;
    accountableEmail = accountableemail;
    accountableName = accountablename;
    document.getElementById('lblStockRequestNo').innerHTML = requestno;
    document.getElementById('lblStockRequestPerson').innerHTML = accountable;

    load_data_StockRequestDetails();
    $('#viewStockRequestDetails').modal('show');
}

function load_data_StockRequestDetails() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/StockRequestDetailsPendingApproval",
        data: "{stockrequestno: '" + requestnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockrequestdetails').hide();
            $('#stockrequestdetails').innerHTML = "";
            $('#stockrequestdetails').fadeIn(200);

            myJsonData = data;

            populateDataStockRequestDets(myJsonData);

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

function populateDataStockRequestDets(data) {
    try {
        $('#tblStockRequestDetails').DataTable({
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
                { "data": "QuantityString" },
                { "data": "QuantityStringIssue" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
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

        $('#tblStockRequestDetails').off('click');
    } catch (e) {
        var table = $('#tblStockRequestDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveStockRequest() {
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
                url: "Home/RequestManagement.aspx/ApproveStockRequest",
                data: "{requestno:'" + requestnumber + "',transactoremail:'" + accountableEmail + "',name:'" + accountableName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_StockRequest();
                        $('#viewStockRequestDetails').modal('hide');
                        swal("Success", "Supply Request successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_StockRequest();
                        $('#viewStockRequestDetails').modal('hide');
                        swal("Success", "Supply Request successfully APPROVED.\n\nMail not Sent.", "success");
                    }
                    else {
                        load_data_StockRequest();
                        $('#viewStockRequestDetails').modal('hide');
                        swal("Success", "Supply Request successfully APPROVED.", "success");
                    }
                    $(".se-pre-con").hide();



                    //if (msg.d == "1") {
                    //load_data_StockRequest();
                    //$('#viewStockRequestDetails').modal('hide');
                    //swal("Success", "Stock Request successfully APPROVED.", "success");

                    //$(".se-pre-con").hide();
                    //}
                    //else {
                    //    load_data_PropertyRequest();
                    //    $('#viewPropertyRequestDetails').modal('hide');
                    //    swal("Success", "Property Request successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    //}
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineStockRequest() {
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
    textareatt.id = "declinestockrequesttextall";

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

        var textremarks = $('#declinestockrequesttextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }
        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclineStockRequest",
            data: "{requestno:'" + requestnumber + "',remarkS:'" + textremarks + "',transactoremail:'" + accountableEmail + "',name:'" + accountableName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_StockRequest();
                    $('#viewStockRequestDetails').modal('hide');
                    swal("Success", "Supply Request successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_StockRequest();
                    $('#viewStockRequestDetails').modal('hide');
                    swal("Success", "Supply Request successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_StockRequest();
                    $('#viewStockRequestDetails').modal('hide');
                    swal("Success", "Supply Request successfully DECLINED.\n\nMail not Sent. Inspector has no email.", "success");
                }

                $(".se-pre-con").hide();
            }
        });
    });
}