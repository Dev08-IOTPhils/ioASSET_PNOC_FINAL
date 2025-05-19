var idForUpdate = 0;

$(document).ready(function () {
    load_data_PropertyIssuance();
    //load_data_Extension();
});

function load_data_PropertyIssuance() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPendingApprovalPropertyIssuance",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyissuancelist').hide();
            $('#propertyissuancelist').innerHTML = "";
            $('#propertyissuancelist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadIssuance(myJsonData);

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

function populateDataTablePendingUploadIssuance(data) {
    try {
        $('#tblPropertyIssuance').DataTable({
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
                { "data": "IssuanceNumber" },
                { "data": "IssuedTo" },
                //{ "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
                { "data": "Approver" },
                { "data": "Remarks" },
                { "data": "Status" },
                { "data": "SupportingDocument" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
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

        $('#tblPropertyIssuance').off('click');

        $('#tblPropertyIssuance').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyIssuance').DataTable();
        table
            .clear()
            .draw();
    }
}

var issuancenumber = '';
function ViewAssetsIssuanceDetails(issuanceno, employeename, transactorname, createddate, counter, counterchecker, isapproved) {
    issuancenumber = issuanceno;
    document.getElementById('lblPARICS').innerHTML = issuanceno;
    document.getElementById('lblIssuedBy').innerHTML = transactorname
    document.getElementById('lblPerson').innerHTML = employeename;
    document.getElementById('lblDateIssued').innerHTML = createddate;

    load_data_PropertyIssuanceDetails();
    $('#viewPropertyIssuanceDetails').modal('show');
}

function load_data_PropertyIssuanceDetails() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyIssuanceList",
        data: "{issuanceno: '" + issuancenumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyissuancedetails').hide();
            $('#propertyissuancedetails').innerHTML = "";
            $('#propertyissuancedetails').fadeIn(200);

            myJsonData = data;

            populateDataTablePULIssuance(myJsonData);

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

function populateDataTablePULIssuance(data) {
    try {
        $('#tblPropertyIssuanceDetails').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
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

        $('#tblPropertyIssuanceDetails').off('click');
    } catch (e) {
        var table = $('#tblPropertyIssuanceDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApprovePropertyIssuance() {
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
                url: "Home/RequestManagement.aspx/ApprovePropertyIssuance",
                data: "{issuanceno:'" + issuancenumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyIssuance();
                        $('#viewPropertyIssuanceDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.');
                        swal("Success", "Property Issuance successfully APPROVED.", "success");
                    }
                    else {
                        load_data_PropertyIssuance();
                        $('#viewPropertyIssuanceDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyIssuance() {
    //if (confirm("Are you sure you want to DECLINE this Property Issuance?") == false) {
    //    return false;
    //}

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
            url: "Home/RequestManagement.aspx/DeclinePropertyIssuance",
            data: "{issuanceno:'" + issuancenumber + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyIssuance();
                    $('#viewPropertyIssuanceDetails').modal('hide');
                    //alert('Property Issuance successfully DECLINED.');
                    swal("Success", "Property Issuance successfully DECLINED.", "success");
                }
                else {
                    load_data_PropertyIssuance();
                    $('#viewPropertyIssuanceDetails').modal('hide');
                    //alert('Property Issuance successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.');
                    swal("Success", "Property Issuance successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}

function load_data_Extension() {
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/PropertyExtensionDateRequestList",
        data: "{}",
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
            $(".se-pre-con").fadeOut("slow");
            //GetCount();
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

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
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "CreatedBy" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "Remarks" },
                { "data": "Approver" },
                { "data": "DateOfReturn" },
                { "data": "DateOfExtension" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
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

        $('#tblPendingRequestExtension').off('click');

        $('#tblPendingRequestExtension').on('click', 'a.btn_ApproveExtension', function (e) {
            e.preventDefault();
        });
        $('#tblPendingRequestExtension').on('click', 'a.btn_DeclineExtension', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPendingRequestExtension').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveExtension(recid) {
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
                url: "Home/RequestManagement.aspx/ApproveExtension",
                data: "{ recid: '" + recid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //  load_data_Extension();
                    //alert('Successfully approved the transaction.');
                    swal("Success", "Property Return Date of Extension successfully APPROVED.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineExtension(recid) {
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
                url: "Home/RequestManagement.aspx/DeclineExtension",
                data: "{ recid: '" + recid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // load_data_Extension();
                    swal("Success", "Property Return Date of Extension successfully DECLINED.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}