var idForUpdate = 0;
var currentPage = 1;

var url = '';
$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    $("#tblDepartment").DataTable({
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        "sorting": false,
        dom: 'Brftip',
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
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
    load_dataDept();
    //$('#addDepartmentDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataDept() {
    $.ajax({
        type: 'POST',
        url: url + "/Department.aspx/GetDepartment",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#department').hide();
            $('#department').innerHTML = "";
            $('#department').fadeIn(200);

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
        $('#tblDepartment').DataTable({
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

            "sorting": false,
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "columns": [
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Department'
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

        $('#tblDepartment').off('click');
    } catch (e) {
        var table = $('#tblDepartment').DataTable();
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

function GetDepartmentID(id) {
    idForUpdate = id;
    $("#addDepartmentDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditDepartment");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "User_Management/Department.aspx/DepartmentbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtDepartmentName').val(msg.d.split('|')[1]);
            $('#txtDescriptionDept').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveDept").checked = true;
            }
            else {
                document.getElementById("chkActiveDept").checked = false;
            }
            //$('#ContentPlaceHolder1_ddlEmployee').val(msg.d.split('|')[4]);
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

function deleteDepartment(id) {
    idForUpdate = id;
    //$("#deleteDepartment").modal("show");
    swal({
        title: "Delete User Department",
        text: "Are you sure you want to delete this user department?",
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
                url: url + "/Department.aspx/DeleteDepartment",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "User Department Successfully Deleted!", "success");
                        Clear();
                        load_dataDept();
                    }
                    else {
                        swal("Error", "User Department cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteDepartment() {
    $("#deleteDepartment").modal("hide");
    $.ajax({
        type: "POST",
        url: url + "/Department.aspx/DeleteDepartment",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('User Department Successfully Deleted!');
                Clear();
                load_dataDept();
            }
            else {
                alert('User Department cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtDepartmentName').val("");
    $('#txtDescriptionDept').val("");
    $('#ContentPlaceHolder1_ddlEmployee').val("--- Select Custodian Type ---");
    //$('#txtLife').val("");
    document.getElementById("chkActiveDept").checked = true;
    //document.getElementById("chkTag").checked = false;
    idForUpdate = 0;
    CountChar($('#txtDescriptionDept').val(), 500, 'cntDept', 'txtDescriptionDept');
}