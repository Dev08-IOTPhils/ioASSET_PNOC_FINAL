var idForUpdate = 0;

$(document).ready(function () {
    load_data_PropertyRequest();
});

//Property Request
function load_data_PropertyRequest() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/PropertyRequestPendingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyrequestlist').hide();
            $('#propertyrequestlist').innerHTML = "";
            $('#propertyrequestlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadRequest(myJsonData);

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

function populateDataTablePendingUploadRequest(data) {
    try {
        $('#tblPropertyRequest').DataTable({
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
                { "data": "RequestNumber" },
                { "data": "RequestedBy" },
                //{ "data": "NoOfItemString" },
                { "data": "DateOfReturn" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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

        $('#tblPropertyRequest').off('click');
        $('#tblPropertyRequest').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyRequest').DataTable();
        table
            .clear()
            .draw();
    }
}

var requestno = '';
function ViewAssetsRequest(requestnumber, counter, counterchecker, isapproved) {
    requestno = requestnumber;
    document.getElementById('lblRequestNumber1').innerHTML = requestnumber;

    load_data_purl();
    $('#viewPropertyRequestDetails').modal('show');
    x.style.display = "none";
}

function load_data_purl() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPRequestPendingUploadList",
        data: "{requestnumber: '" + requestno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#requestlist').hide();
            $('#requestlist').innerHTML = "";
            $('#requestlist').fadeIn(200);

            myJsonData = data;

            populateDataTablePULrequest(myJsonData);

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

function populateDataTablePULrequest(data) {
    try {
        $('#tblRequestList').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
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

        $('#tblRequestList').off('click');
    } catch (e) {
        var table = $('#tblRequestList').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApprovePropertyRequest() {
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
                url: "Home/RequestManagement.aspx/ApprovePropertyRequest",
                data: "{requestno:'" + requestno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyRequest();
                        $('#viewPropertyRequestDetails').modal('hide');
                        //alert('Property Request successfully APPROVED.');
                        swal("Success", "Property Request successfully APPROVED.", "success");
                    }
                    else {
                        load_data_PropertyRequest();
                        $('#viewPropertyRequestDetails').modal('hide');
                        //alert('Property Request successfully APPROVED.\n\nPlease check Mail setting.');
                        swal("Success", "Property Request successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyRequest() {
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
            url: "Home/RequestManagement.aspx/DeclinePropertyRequest",
            data: "{requestno:'" + requestno + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyRequest();
                    $('#viewPropertyRequestDetails').modal('hide');
                    //alert('Property Request successfully DECLINED.');
                    swal("Success", "Property Request successfully DECLINED.", "success");
                }
                else {
                    load_data_PropertyRequest();
                    $('#viewPropertyRequestDetails').modal('hide');
                    //alert('Property Request successfully DECLINED.\n\nPlease check Mail setting.');
                    swal("Success", "Property Request successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}