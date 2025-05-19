var idForUpdate = 0;

$(document).ready(function () {
    load_data_PropertyTransfer();
});

function load_data_PropertyTransfer() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/PropertyTransferPendingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertytransfer').hide();
            $('#propertytransfer').innerHTML = "";
            $('#propertytransfer').fadeIn(200);

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
        $('#tblPropertyTransfer').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TransferNumber" },
                { "data": "RemarksTransfer" },
                //{ "data": "StringNoOfItem" },
                { "data": "Status" },
                { "data": "CreatedBy" },
                { "data": "DateOfTransfer" },
                { "data": "Approver" },
                { "data": "Remarks" },
                { "data": "SupportingDocument" },
                { "data": "Action" }
                //DateOfTransaction
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
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

        $('#tblPropertyTransfer').off('click');

        $('#tblPropertyTransfer').on('click', 'button.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyTransfer').DataTable();
        table
            .clear()
            .draw();
    }
}

var transsno = '';
function ViewAssets(transactionno, counter, counterchecker, isapproved) {
    transsno = transactionno;

    load_data_PUL1();
    $('#viewPropertyTransferDetails').modal('show');
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/PLPendingApproval",
        data: "{transactionno: '" + transsno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertytransferlist').hide();
            $('#propertytransferlist').innerHTML = "";
            $('#propertytransferlist').fadeIn(200);

            myJsonData = data;

            populateDataTablePUL1(myJsonData);

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

function populateDataTablePUL1(data) {
    try {
        $('#tblPropertyTransferList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "Origin" },
                { "data": "Destination" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
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

        $('#tblPropertyTransferList').off('click');
    } catch (e) {
        var table = $('#tblPropertyTransferList').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApprovePropertyTransfer() {
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
                url: "Home/RequestManagement.aspx/ApprovePropertyTransfer",
                data: "{transferno:'" + transsno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyTransfer();
                        $('#viewPropertyTransferDetails').modal('hide');
                        //alert('Property Transfer successfully APPROVED.');
                        swal("Success", "Property Transfer successfully APPROVED.", "success");
                    }
                    else {
                        load_data_PropertyTransfer();
                        $('#viewPropertyTransferDetails').modal('hide');
                        //alert('Property Transfer successfully APPROVED.\n\nMail not sent. Please check Mail Setting.');
                        swal("Success", "Property Transfer successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyTransfer() {
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
        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclinePropertyTransfer",
            data: "{transferno:'" + transsno + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyTransfer();
                    $('#viewPropertyTransferDetails').modal('hide');
                    swal("Success", "Property Transfer successfully DECLINED.", "success");
                }
                else {
                    load_data_PropertyTransfer();
                    $('#viewPropertyTransferDetails').modal('hide');
                    swal("Success", "Property Transfer successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}