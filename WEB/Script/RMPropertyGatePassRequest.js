var idForUpdate = 0;

$(document).ready(function () {
    $("#tblGatePass").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
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
       , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    load_data_GatePass();
});

function load_data_GatePass() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyGatePassPendingUpload",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#gatepass').hide();
            $('#gatepass').innerHTML = "";
            $('#gatepass').fadeIn(200);

            myJsonData = data;
            populatePendingApprovalGatePass(myJsonData);

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

function populatePendingApprovalGatePass(data) {
    try {
        $('#tblGatePass').DataTable({
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
                { "data": "GatePassNumber" },
                { "data": "RequestedBy" },
                { "data": "NoOfItemReturn" },
                { "data": "DateOfTransaction" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
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

        $('#tblGatePass').off('click');

        $('#tblGatePass').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblGatePass').DataTable();
        table
            .clear()
            .draw();
    }
}

var gatepassnumber = ''
function ViewList(gatepassno) {
    gatepassnumber = gatepassno;
    load_pending_list();
    document.getElementById('lblGatePassNumber').innerHTML = gatepassno;

    $('#viewGatePassRequestDetails').modal('show');
}

function load_pending_list() {
    $.ajax({
        type: "POST",
        url: "Home/RequestManagement.aspx/PropertyGatePassRequestList",
        data: "{ gatepassno : '" + gatepassnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#gatepasslist').hide();
            $('#gatepasslist').innerHTML = "";
            $('#gatepasslist').fadeIn(200);

            myJsonData = data;
            populateALPending(myJsonData);

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

function populateALPending(data) {
    try {
        $('#tblGatePassList').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Gate Pass'
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

        $('#tblGatePassList').off('click');
    } catch (e) {
        var table = $('#tblGatePassList').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApprovePropertyGatePass() {
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
                url: "Home/RequestManagement.aspx/ApprovePropertyGatePass",
                data: "{gatepassno:'" + gatepassnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        //alert('Property Gate Pass successfully APPROVED.');
                        swal("Success", "Property Gate Pass successfully APPROVED.", "success");
                        $('#viewGatePassRequestDetails').modal('hide');
                        load_data_GatePass();
                    }
                    else if (msg.d == "sorry") {
                        //alert('Property Gate Pass successfully APPROVED.\n\nMail not sent. Please check Mail setting.');
                        swal("Success", "Property Gate Pass successfully APPROVED.\n\nMail not sent. Please check Mail setting.", "success");
                        $('#viewGatePassRequestDetails').modal('hide');
                        load_data_GatePass();
                    }
                    else {
                        //alert('Property Gate Pass successfully APPROVED.\n\nMail not sent. Requestor has no email.');
                        swal("Success", "Property Gate Pass successfully APPROVED.\n\nMail not sent. Requestor has no email.", "success");
                        $('#viewGatePassRequestDetails').modal('hide');
                        load_data_GatePass();
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyGatePass() {
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
        $(".se-pre-con").hide();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclinePropertyGatePass",
            data: "{gatepassno:'" + gatepassnumber + "',remarkS:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $(".se-pre-con").hide();
                if (msg.d == "okay") {
                    load_data_GatePass();
                    $('#viewGatePassRequestDetails').modal('hide');
                    swal("Success", "Property Gate Pass successfully DECLINED.", "success");
                }

                else if (msg.d == "sorry") {
                    load_data_GatePass();
                    $('#viewGatePassRequestDetails').modal('hide');
                    swal("Success", "Property Gate Pass successfully DECLINED.\n\nMail not sent. Please check Mail setting.", "success");
                }
                else {
                    load_data_GatePass();
                    $('#viewGatePassRequestDetails').modal('hide');
                    swal("Success", "Property Gate Pass successfully DECLINED.\n\nMail not sent. Requestor has no email.", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}