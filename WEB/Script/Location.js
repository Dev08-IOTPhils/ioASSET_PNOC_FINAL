var idForUpdate = 0;
var currentPage = 1;

var url = "";

$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    $("#tblLocation").DataTable({
        "scrollX": true,
        dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
        "sorting": false,
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
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
    load_dataLocation();

    //$('#addLocationDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataLocation() {
    $.ajax({
        type: 'POST',
        url: url + "/Location.aspx/GetLocation",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#location').hide();
            $('#location').innerHTML = "";
            $('#location').fadeIn(200);
            myJsonData = data;
            populateDataTable(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            //  alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTable(data) {
    try {
        $('#tblLocation').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { 'searchPlaceholder': "", "search": '', processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Location" },
                { "data": "LocationCode" },
                { "data": "Warehouse" },
                { "data": "Description" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
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

        $('#tblLocation').off('click');
    } catch (e) {
        // alert(e);
        var table = $('#tblLocation').DataTable();
        table
            .clear()
            .draw();
    }
}
function LoadControls() {
    $('.btn').button();
}

function GetLocationByName(location) {
    idForUpdate = location;
    $("#addLocationDialogBox").modal("show");
    $.ajax({
        type: "POST",
        url: url + "/Location.aspx/GetLocationByName",
        data: "{ locationid: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtLocation').val(msg.d.split('|')[1]);
            $('#txtLocationCode').val(msg.d.split('|')[5]);
            $('#ddlWarehouse').val(msg.d.split('|')[2]).trigger('change');
            $('#txtDescriptionLocation').val(msg.d.split('|')[3]);
            if (msg.d.split('|')[4] == 'True') {
                document.getElementById("chkActiveLocation").checked = true;
            }
            else {
                document.getElementById("chkActiveLocation").checked = false;
            }
            CountChar($('#txtDescriptionLocation').val(), 500, 'cntLocation', 'txtDescriptionLocation');
        }
    });
}

function deleteLocation(locationid) {
    idForUpdate = locationid;
    //$("#deleteLocation").modal("show");
    swal({
        title: "Delete Location",
        text: "Are you sure you want to delete this location?",
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
                url: url + "/Location.aspx/DeleteLocation",
                data: "{ locationid: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#addLocationDialogBox").modal("hide");
                    if (msg.d == "1") {
                        swal("Successful", "Location Successfully Deleted!", "success");
                        Clear();
                        load_dataLocation();
                    }
                    else {
                        swal("Error", "Location cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteLocation() {
    $("#deleteLocation").modal("hide");
    $.ajax({
        type: "POST",
        url: url + "/Location.aspx/DeleteLocation",
        data: "{ locationid: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#addLocationDialogBox").modal("hide");
            if (msg.d == "1") {
                alert('Location Successfully Deleted!');
                Clear();
                load_dataLocation();
            }
            else {
                alert('Location cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtLocation').val("");
    $('#txtLocationCode').val("");
    $('#txtLocation').prop('disabled', false);
    $('#txtDescriptionLocation').val("");
    $('#ddlWarehouse').val("0").trigger('change');
    CountChar($('#txtDescriptionLocation').val(), 500, 'cntLocation', 'txtDescriptionLocation');
    idForUpdate = 0;
}