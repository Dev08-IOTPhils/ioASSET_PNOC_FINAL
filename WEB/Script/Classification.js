var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblClassificationList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
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
    load_dataClassification();

    //$('#addClassificationDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataClassification() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Classification.aspx/GetClassification",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#classificationlist').hide();
            $('#classificationlist').innerHTML = "";
            $('#classificationlist').fadeIn(200);

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
        $('#tblClassificationList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ClassificationName" },
                { "data": "ClassificationCode" },
                { "data": "Description" },
                { "data": "IsFloater" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Classification List'
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

        $('#tblClassificationList').off('click');
    } catch (e) {
        var table = $('#tblClassificationList').DataTable();
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

//function addClassificationDialogBox() {
//    Clear();
//    document.getElementById("chkActive").checked = true;
//    $("#addClassificationDialogBox").modal("show");

//    var context = window.location.pathname.substring(1);
//    var url = context.split('/')[1];
//    $.ajax({
//        type: "POST",
//        url: context + "/CheckR",
//        data: " { context : '" + context + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg1) {
//            var btnAccess = [];
//            btnAccess = msg1.d.split('|');
//            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
//                let elementsAddBrand = document.getElementsByClassName("btn_EditClass");
//                for (let i = 0; i < elementsAddBrand.length; i++) {
//                    elementsAddBrand[i].remove();
//                }
//                var $formBrand = makeElement('button', {
//                    type: 'button',
//                    id: 'btnSaveClassification',
//                    class: 'btn btn-success btn_EditClass',
//                    onclick: 'SaveClassification()',
//                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
//                });

//                $('#SaveCancelClass').prepend($formBrand);
//            }
//        }
//    });
//}

function Filter() {
    var search = $('#txtSearch').val();
    $.ajax({
        type: "POST",
        url: "Files_Data/Classification.aspx/GetClassification",
        data: "{ search: '" + search + "', pageNo: '" + currentPage + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ClassificationList').html(msg.d);
        }
    });
}

function Pagination(pageNo) {
    currentPage = pageNo;

    $.ajax({
        type: "POST",
        url: "Files_Data/Classification.aspx/GetClassification",
        data: "{ search: '" + $('#txtSearch').val() + "',pageNo: '" + pageNo + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ClassificationList').html(msg.d);
        }
    });
}


function GetClassificationID(id) {
    idForUpdate = id;
    $("#addClassificationDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditClass");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/Classification.aspx/ClassificationbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtClassificationName').val(msg.d.split('|')[1]);
            $('#txtClassificationCode').val(msg.d.split('|')[4]);
            $('#txtDescriptionClassification').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveClassification").checked = true;
            }
            else {
                document.getElementById("chkActiveClassification").checked = false;
            }
            if (msg.d.split('|')[6] == 'True') {
                document.getElementById("chkActiveIsFloater").checked = true;
            }
            else {
                document.getElementById("chkActiveIsFloater").checked = false;
            }

            //$('#txtClassificationCode').val(msg.d.split('|')[4]);
            //$('#txtStartingCount').val(msg.d.split('|')[5]);
            //$('#txtStartingCount').prop('disabled', true);
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

function deleteClassification(id) {
    idForUpdate = id;
    //$("#deleteClassification").modal("show");
    swal({
        title: "Delete Classification",
        text: "Are you sure you want to delete this classification?",
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
                url: "Files_Data/Classification.aspx/DeleteClassification",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Classification Successfully Deleted!", "success");
                        Clear();
                        load_dataClassification();
                    }
                    else {
                        swal("Error", "Classification cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteClassification() {
    $("#deleteClassification").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Classification.aspx/DeleteClassification",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                load_dataClassification();
                alert('Classification Successfully Deleted!');
                Clear();
            } else {
                alert('Classification cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtClassificationName').val("");
    $('#txtClassificationCode').val("");
    $('#txtDescriptionClassification').val("");
    $('#txtStartingCount').val("");
    $('#txtStartingCount').prop('disabled', false);
    //$('#txtLife').val("");
    document.getElementById("chkActiveClassification").checked = true;
    //document.getElementById("chkTag").checked = false;
    idForUpdate = 0;
    CountChar($('#txtDescriptionClassification').val(), 500, 'cntClassification', 'txtDescriptionClassification');
}