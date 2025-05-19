

var idForUpdate = 0;
var requestcount = 0;
$(document).ready(function () {

    load_data_DayPass();

});

function load_data_DayPass() {

    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyDayPassPendingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#daypass').hide();
            $('#daypass').innerHTML = "";
            $('#daypass').fadeIn(200);

            myJsonData = data;
            populateDataTableDayPass(myJsonData);

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

function populateDataTableDayPass(data) {
    try {
        $('#tblDayPass').DataTable({
            "destroy": true,
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DayPassNumber" },
                { "data": "RequestedBy" },
                { "data": "DateOfTransaction" },
                { "data": "Purpose" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Day Pass Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Day Pass Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Day Pass Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Day Pass Request'
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

        $('#tblDayPass').off('click');

        $('#tblDayPass').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });


    } catch (e) {
        var table = $('#tblDayPass').DataTable();
        table
            .clear()
            .draw();
    }
}

var daypassnumber = '';
function ViewListDayPass(daypassno) {
    daypassnumber = daypassno;
    load_pending_daypasslist();


    $('#dayPassPropertyList').modal('show');
}

function load_pending_daypasslist() {

    $.ajax({
        type: "POST",
        url: "Home/RequestManagement.aspx/PropertyDayPassRequestList",
        data: "{ daypassno : '" + daypassnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pal').hide();
            $('#pal').innerHTML = "";
            $('#pal').fadeIn(200);

            myJsonData = data;
            populateDayPassList(myJsonData);

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

function populateDayPassList(data) {
    try {
        $('#tblPAL').DataTable({
            "destroy": true,
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "SerialNumber" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Description" }
            ],
            dom: 'frtip'
        });

        $('#tblPAL').off('click');

    } catch (e) {
        var table = $('#tblPAL').DataTable();
        table
            .clear()
            .draw();
    }
}


function ApprovePropertyDayPass() {
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
                url: "Home/RequestManagement.aspx/ApprovePropertyDayPass",
                data: "{daypassno:'" + daypassnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_DayPass();
                        $('#dayPassPropertyList').modal('hide');
                        //alert('Property Day Pass successfully APPROVED.');
                        swal("Success", "Property Day Pass successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_DayPass();
                        $('#dayPassPropertyList').modal('hide');
                        //alert('Property Day Pass successfully APPROVED.\n\nMail not sent. Please check mail setting.');
                        swal("Success", "Property Day Pass successfully APPROVED.\n\nMail not sent. Please check Mail setting.", "success");
                    }
                    else {
                        load_data_DayPass();
                        $('#dayPassPropertyList').modal('hide');
                        //alert('Property Day Pass successfully APPROVED.\n\nMail not sent. Requestor has no email.');
                        swal("Success", "Property Day Pass successfully APPROVED.\n\nMail not sent. Requestor has no email.", "success");
                    }

                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyDayPass() {
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
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclinePropertyDayPass",
            data: "{daypassno:'" + daypassnumber + "',remarkS:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_DayPass();
                    $('#dayPassPropertyList').modal('hide');
                    //alert('Property Day Pass successfully DECLINED.');
                    swal("Success", "Property Day Pass successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_DayPass();
                    $('#dayPassPropertyList').modal('hide');
                    //alert('Property Day Pass successfully DECLINED.\n\nMail not sent. Please check mail setting.');
                    swal("Success", "Property Day Pass successfully DECLINED.\n\nMail not sent. Please check Mail setting.", "success");
                }
                else {
                    load_data_DayPass();
                    $('#dayPassPropertyList').modal('hide');
                    //alert('Property Day Pass successfully DECLINED.\n\nMail not sent. Requestor has no email.');
                    swal("Success", "Property Day Pass successfully DECLINED.\n\nMail not sent. Requestor has no email.", "success");
                }

                $(".se-pre-con").hide();
            }
        });
    });
}