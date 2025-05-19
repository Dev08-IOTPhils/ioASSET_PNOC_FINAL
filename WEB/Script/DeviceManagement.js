var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblDevice").DataTable({
        "scrollX": true,
        "sorting": false,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
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
    load_dataDeviceMngt();
});

function load_dataDeviceMngt() {
    $.ajax({
        type: 'POST',
        url: "System/DeviceManagement.aspx/GetDevice",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#device').hide();
            $('#device').innerHTML = "";
            $('#device').fadeIn(200);

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
        $('#tblDevice').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "sorting": false,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DeviceName" },
                { "data": "IPAddress" },
                { "data": "PortString" },
                { "data": "Description" },
                { "data": "DeviceUsageType" },
                { "data": "IsActive" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Device Management'
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

        $('#tblDevice').off('click');
    } catch (e) {
        var table = $('#tblDevice').DataTable();
        table
            .clear()
            .draw();
    }
}



function GetDeviceID(id) {
    idForUpdate = id;
    $("#addDeviceDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[2] == "0") {
                let elementsAddCat = document.getElementsByClassName("btn_EditDeviceMngt");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "System/DeviceManagement.aspx/DevicebyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtDeviceName').val(msg.d.split('|')[1]);
            $('#txtIPaddress').val(msg.d.split('|')[2]);
            $('#txtPort').val(msg.d.split('|')[3]);
            $('#txtDescriptionDeviceMngt').val(msg.d.split('|')[4]);
            $('#ddlDeviceUsageType').val(msg.d.split('|')[5]);
            if (msg.d.split('|')[6] == 'True') {
                document.getElementById("chkActiveDeviceMngt").checked = true;
            }
            else {
                document.getElementById("chkActiveDeviceMngt").checked = false;
            }
        }
    });
}

function deleteDevice(id) {
    idForUpdate = id;
    //$("#deleteDevice").modal("show");
    swal({
        title: "Delete Device",
        text: "Are you sure you want to delete this Device?",
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
                url: "System/DeviceManagement.aspx/DeleteDevice",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //alert('Device Successfully Deleted!');
                    //Clear();
                    //load_dataDeviceMngt();
                    if (msg.d == "1") {
                        swal("Successful", "Device Successfully Deleted!", "success");
                        ClearDeviceMngt();
                        load_dataDeviceMngt();
                    }
                    else {
                        swal("Error", "Device cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteDevice() {
    $("#deleteDevice").modal("hide");
    $.ajax({
        type: "POST",
        url: "System/DeviceManagement.aspx/DeleteDevice",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            alert('Device Successfully Deleted!');
            ClearDeviceMngt();
            load_dataDeviceMngt();
        }
    });
}

function ClearDeviceMngt() {
    $('#txtDeviceName').val("");
    $('#txtDescriptionDeviceMngt').val("");
    $('#txtIPaddress').val("");
    $('#txtPort').val("");
    document.getElementById("chkActiveDeviceMngt").checked = true;
    idForUpdate = 0;
}