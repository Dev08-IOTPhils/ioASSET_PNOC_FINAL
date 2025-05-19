var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblArea").DataTable({
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
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
    load_dataArea();
    //$('#addAreaDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataArea() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Area.aspx/getArea",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#area').hide();
            $('#area').innerHTML = "";
            $('#area').fadeIn(200);

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
        $('#tblArea').DataTable({
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
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            "columns": [
                { "data": "Area" },
                { "data": "Location" },
                { "data": "Site" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Area List'
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

        $('#tblArea').off('click');
    } catch (e) {
        var table = $('#tblArea').DataTable();
        table
            .clear()
            .draw();
    }
}

function LoadControls() {
    $(".btn").button();

    $('input[type="checkbox"].minimal').iCheck({
        checkboxClass: 'icheckbox_flat-red',
        checked: true
    })
}


function GetAreaID(id) {
    idForUpdate = id;
    $("#addAreaDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditArea");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/Area.aspx/AreabyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtArea').val(msg.d.split('|')[1]);
            $('#txtDescriptionArea').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveArea").checked = true;
            }
            else {
                document.getElementById("chkActiveArea").checked = false;
            }
            $('#ddlLocation').val(msg.d.split('|')[4]).trigger('change');
            //$('#txtLife').val(msg.d.split('|')[4]);
            //if (msg.d.split('|')[5] == 'True') {
            //    document.getElementById("chkTag").checked = true;
            //}
            //else {
            //    document.getElementById("chkTag").checked = false;
            //}
        }
    });
}

function deleteArea(id) {
    idForUpdate = id;
    //$("#deleteArea").modal("show");
    swal({
        title: "Delete Area?",
        text: "Are you sure you want to delete this area?",
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
                url: "Files_Data/Area.aspx/DeleteArea",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Area Successfully Deleted!", "success");
                        Clear();
                        load_dataArea();
                    }
                    else {
                        swal("Error", "Area cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteArea() {
    $("#deleteArea").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Area.aspx/DeleteArea",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Area Successfully Deleted!');
                Clear();
                load_dataArea();
            }
            else {
                alert('Area cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#ddlLocation').val("0").trigger('change');
    $('#txtArea').val("");
    $('#txtDescriptionArea').val("");
    document.getElementById("chkActiveArea").checked = true;
    idForUpdate = 0;
    CountChar($('#txtDescriptionArea').val(), 500, 'cntArea', 'txtDescriptionArea');
}