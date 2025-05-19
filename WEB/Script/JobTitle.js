var idForUpdate = 0;
var currentPage = 1;
var url = '';

$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    $("#tblJobTitle").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
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
    load_dataJobTitle();
    //$('#addPositionDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataJobTitle() {
    $.ajax({
        type: 'POST',
        url: url + "/JobTitle.aspx/GetPosition",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#jobtitle').hide();
            $('#jobtitle').innerHTML = "";
            $('#jobtitle').fadeIn(200);

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
        $('#tblJobTitle').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Position" },
                { "data": "Department" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Job Title List'
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

        $('#tblJobTitle').off('click');
    } catch (e) {
        var table = $('#tblJobTitle').DataTable();
        table
            .clear()
            .draw();
    }
}

function LoadControls() {
    $('.btn').button();
    $('.select2').select2({
        theme: "default"
    });
}


function deletePosition(Positionid) {
    idForUpdate = Positionid;
    //$("#deletePosition").modal("show");
    swal({
        title: "Delete Job Title",
        text: "Are you sure you want to delete this job title?",
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
                url: url + "/JobTitle.aspx/DeletePosition",
                data: "{ positionid: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#deletePosition").modal("hide");
                    if (msg.d == "1") {
                        swal("Successful", "Job Title Successfully Deleted!", "success");
                        Clear();
                        load_dataJobTitle();
                    }
                    else {
                        swal("Error", "Job Title cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeletePosition() {
    $.ajax({
        type: "POST",
        url: url + "/JobTitle.aspx/DeletePosition",
        data: "{ positionid: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#deletePosition").modal("hide");
            if (msg.d == "1") {
                alert('Position Successfully Deleted!');
                Clear();
                load_dataJobTitle();
            }
            else {
                alert('Position cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function GetPositionID(id) {
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
                let elementsAddCat = document.getElementsByClassName("btn_EditPosition");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "User_Management/JobTitle.aspx/GetPositionByID",
        data: "{ id: '" + id + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtPositionName').val(msg.d.split('|')[1]);
            $('#ddlDepartment').val(msg.d.split('|')[2]).trigger('change');
            $('#txtDescriptionJobTitle').val(msg.d.split('|')[3]);
            if (msg.d.split('|')[4] == 'True') {
                document.getElementById("chkActiveJobTitle").checked = true;
            }
            else {
                document.getElementById("chkActiveJobTitle").checked = false;
            }
        }
    });
    $("#addPositionDialogBox").modal("show");
}

function Clear() {
    $('#txtPositionName').val("");
    $('#ddlDepartment').val("0");
    $('#txtDescriptionJobTitle').val("");
    document.getElementById("chkActiveJobTitle").checked = false;
    CountChar($('#txtDescriptionJobTitle').val(), 500, 'cntJobTitle', 'txtDescriptionJobTitle');
    idForUpdate = 0;
}