var idForUpdate = 0;
var currentPage = 1;
$(document).ready(function () {
    $('.select2').select2();

    $("#tblUOM").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
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
    load_dataUOM();
    //$('#addUnitOfMeasurementDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataUOM() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/UnitOfMeasurement.aspx/GetUOM",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#uom').hide();
            $('#uom').innerHTML = "";
            $('#uom').fadeIn(200);

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
        $('#tblUOM').DataTable({
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
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "UnitOfMeasurement" },
                { "data": "UOMCode" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Unit of Measurement List'
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

        $('#tblUOM').off('click');
    } catch (e) {
        var table = $('#tblUOM').DataTable();
        table
            .clear()
            .draw();
    }
}

function LoadControls() {
    $(".btn").button();

    $('input[type="checkbox"].minimal').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        checked: true
    })
}


function GetUnitOfMeasurementID(id) {
    idForUpdate = id;

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
                let elementsAddCat = document.getElementsByClassName("btn_EditUOM");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/UnitOfMeasurement.aspx/UnitOfMeasurementbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtUnitOfMeasurement').val(msg.d.split('|')[1]);
            $('#txtDescriptionUOM').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveUOM").checked = true;
            }
            else {
                document.getElementById("chkActiveUOM").checked = false;
            }
            $('#txtUnitOfMeasurementCode').val(msg.d.split('|')[4]);
            //$('#txtLife').val(msg.d.split('|')[4]);
            //if (msg.d.split('|')[5] == 'True') {
            //    document.getElementById("chkTag").checked = true;
            //}
            //else {
            //    document.getElementById("chkTag").checked = false;
            //}
        }
    });

    $("#addUnitOfMeasurementDialogBox").modal("show");

}

function deleteUnitOfMeasurement(id) {
    idForUpdate = id;
    //$("#deleteUnitOfMeasurement").modal("show");
    swal({
        title: "Delete Unit Of Measurement",
        text: "Are you sure you want to delete this Unit Of Measurement?",
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
                url: "Files_Data/UnitOfMeasurement.aspx/DeleteUnitOfMeasurement",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Unit Of Measurement Successfully Deleted!", "success");
                        Clear();
                        load_dataUOM();
                    }
                    else {
                        swal("Error", "Unit Of Measurement cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteUnitOfMeasurement() {
    $("#deleteUnitOfMeasurement").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/UnitOfMeasurement.aspx/DeleteUnitOfMeasurement",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Unit Of Measurement Successfully Deleted!');
                Clear();
                load_dataUOM();
            }
            else {
                alert('Unit Of Measurement cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtUnitOfMeasurement').val("");
    $('#txtUnitOfMeasurementCode').val("");
    $('#txtDescriptionUOM').val("");

    $('#txtLife').val("");
    document.getElementById("chkActiveUOM").checked = true;
    //document.getElementById("chkTag").checked = false;
    idForUpdate = 0;
    CountChar($('#txtDescriptionUOM').val(), 500, 'cntUOM', 'txtDescription');
}