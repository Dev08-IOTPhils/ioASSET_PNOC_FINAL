var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblSupplier").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
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
    load_dataSupplier();

    //$('#addSupplierDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataSupplier() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Supplier.aspx/GetSupplier",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#supplier').hide();
            $('#supplier').innerHTML = "";
            $('#supplier').fadeIn(200);

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
        $('#tblSupplier').DataTable({
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
                { "data": "Supplier" },
                { "data": "ContactNumber" },
                { "data": "Email" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplier List'
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

        $('#tblSupplier').off('click');
    } catch (e) {
        var table = $('#tblSupplier').DataTable();
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

function GetSupplierID(id) {
    idForUpdate = id;
    $("#addSupplierDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditSupplier");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/Supplier.aspx/SupplierbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtSupplierName').val(msg.d.split('|')[1]);
            //$('#txtAddress').val(msg.d.split('|')[2]);
            //$('#txtContactPerson').val(msg.d.split('|')[3]);
            $('#txtContactNo').val(msg.d.split('|')[4]);
            //$('#txtDescription').val(msg.d.split('|')[5]);
            if (msg.d.split('|')[6] == 'True') {
                document.getElementById("chkActiveSupplier").checked = true;
            }
            else {
                document.getElementById("chkActiveSupplier").checked = false;
            }
            $('#txtEmailAddress').val(msg.d.split('|')[7]);
        }
    });
}

function deleteSupplier(id) {
    idForUpdate = id;
    //$("#deleteSupplier").modal("show");
    swal({
        title: "Delete Supplier",
        text: "Are you sure you want to delete this supplier?",
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
                url: "Files_Data/Supplier.aspx/DeleteSupplier",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Supplier Successfully Deleted!", "success");
                        Clear();
                        load_dataSupplier();
                    }
                    else {
                        swal("Error", "Supplier cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteSupplier() {
    $("#deleteSupplier").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Supplier.aspx/DeleteSupplier",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Supplier Successfully Deleted!');
                Clear();
                load_dataSupplier();
            }
            else {
                alert('Supplier cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtSupplierName').val("");
    //$('#txtDescription').val("");
    //$('#txtAddress').val("");
    //$('#txtContactPerson').val("");
    $('#txtContactNo').val("");
    //$('#txtEmailAddress').val("");
    document.getElementById("chkActiveSupplier").checked = true;
    idForUpdate = 0;
}