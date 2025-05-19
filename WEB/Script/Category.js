var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblCategoryList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
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
    load_dataCat();
     //CheckR();
   // $('#addCategoryDialogBox').modal({ backdrop: 'static', keyboard: false })
});



function load_dataCat() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Category.aspx/GetCategory",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#categorylist').hide();
            $('#categorylist').innerHTML = "";
            $('#categorylist').fadeIn(200);

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
        $('#tblCategoryList').DataTable({
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
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "columns": [
                { "data": "CategoryName" },
                { "data": "Description" },
                { "data": "IsActive" },
                { "data": "Action" }
            ],
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Category List'
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

        $('#tblCategoryList').off('click');
    } catch (e) {
        var table = $('#tblCategoryList').DataTable();
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


function GetCategoryID(id) {
    idForUpdate = id;
    $("#addCategoryDialogBox").modal("show");

    $.ajax({
        type: "POST",
        url: "Files_Data/Category.aspx/CategorybyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCategoryName').val(msg.d.split('|')[1]);
            $('#txtDescriptionCat').val(msg.d.split('|')[2]);
            $('#ddlAssetInventoryAccount').val(msg.d.split('|')[6]).trigger('change');

            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveCat").checked = true;
            }
            else {
                document.getElementById("chkActiveCat").checked = false;
            }
            //if (msg.d.split('|')[6] == "0") {
            //    $('#ddlAssetInventoryAccount').val('').trigger('change');
            //}
            //else {
            //    $('#ddlAssetInventoryAccount').val(msg.d.split('|')[6]).trigger('change');
            //}


        }
    });

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
                let elementsAddCat = document.getElementsByClassName("btn_EditCat");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });
}

function deleteCategory(id) {
    idForUpdate = id;
    //$("#deleteCategory").modal("show");
    swal({
        title: "Delete Category",
        text: "Are you sure you want to delete this category?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "Files_Data/Category.aspx/DeleteCategory",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        //alert('Category Successfully Deleted!');
                        swal("Successful", "Category Successfully Deleted!", "success");
                        Clear();
                        load_dataCat();
                    }
                    else {
                        //alert('Category cannot be Deleted, it is being actively used!');
                        swal("Error", "Category cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteCategory() {
    $("#deleteCategory").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Category.aspx/DeleteCategory",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Category Successfully Deleted!');
                Clear();
                load_dataCat();
            }
            else {
                alert('Category cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtCategoryName').val("");
    $('#ddlAssetInventoryAccount').val(0).trigger('change');;
    $('#txtCategoryCode').val("");
    $('#txtDescriptionCat').val("");
    document.getElementById("chkActiveCat").checked = true;
    $('#txtStartingCount').val("");
    $('#txtLife').val("");
    idForUpdate = 0;
    CountChar($('#txtDescriptionCat').val(), 500, 'cnt', 'txtDescriptionCat');
}