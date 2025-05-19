var idForUpdate = 0;
var currentPage = 1;
var checker = false;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblModelList").DataTable({
        "scrollX": true,
        dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
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
    });
    load_dataModel("");

    //$('#addModelDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataModel(orderby) {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Model.aspx/GetModel",
        data: "{orderby : '" + orderby + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#modellist').hide();
            $('#modellist').innerHTML = "";
            $('#modellist').fadeIn(200);

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
        $('#tblModelList').DataTable({
            "destroy": true,
            "processing": true,
            "sorting": false,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ModelName" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Model List'
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

        $('#tblModelList').off('click');
    } catch (e) {
        var table = $('#tblModelList').DataTable();
        table
            .clear()
            .draw();
    }
}
function LoadControls() {
    $('.btn').button();
}

function myFunction(modelname) {
    //var popup = document.getElementById("myPopup");
    //popup.classList.toggle("show");
    //alert(modelname);
}



function deleteModel(modelid) {
    //idForUpdate = modelid;
    //$("#deleteModel").modal("show");
    swal({
        title: "Delete Model",
        text: "Are you sure you want to delete this model?",
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
                url: "Files_Data/Model.aspx/DeleteModel",
                data: "{ modelid: '" + modelid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Model Successfully Deleted!", "success");
                        Clear();
                        load_dataModel("");
                    }
                    else {
                        swal("Error", "Model cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteModel() {
    $("#deleteModel").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Model.aspx/DeleteModel",
        data: "{ modelid: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Model Successfully Deleted!');
                Clear();
                load_dataModel("");
            }
            else {
                alert('Model cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function GetModelID(id) {
    checker = true;
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
                let elementsAddCat = document.getElementsByClassName("btn_EditModel");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });
    //alert(checker);

    $.ajax({
        type: "POST",
        url: "Files_Data/Model.aspx/GetModelByID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //$.ajax({
            //    type: "POST",
            //    url: "Files_Data/Model.aspx/GetCategoryID",
            //    data: "{ id: '" + msg.d.split('|')[2] + "' }",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (msg1) {
            //        $('#ddlCategory').val(msg1.d.split('|')[0]);
            //      //  GetSubCategory();
            //        $.ajax({
            //            type: "POST",
            //            url: "Files_Data/Model.aspx/GetModelByID",
            //            data: "{ id: '" + idForUpdate + "' }",
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //            success: function (msg) {
            //                $('#BodyHolder_ddlSubCategory').val(msg.d.split('|')[2]);
            //            }
            //        });
            //    }
            //});
            //$('#BodyHolder_ddlSubCategory').val(msg.d.split('|')[2]);
            $('#txtModel').val(msg.d.split('|')[1]);
            $('#txtDescriptionModel').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveModel").checked = true;
            }
            else {
                document.getElementById("chkActiveModel").checked = false;
            }
            $('#txtLife').val(msg.d.split('|')[5]);
            //if (msg.d.split('|')[6] == '1') {
            //    $('#BodyHolder_ddlTagType').val("Barcode");
            //}
            //else if (msg.d.split('|')[6] == '2') {
            //    $('#BodyHolder_ddlTagType').val("RFID");
            //}
            //else {
            //    $('#BodyHolder_ddlTagType').val("Barcode & RFID");
            //}

            //if (msg.d.split('|')[7] == 'True') {
            //    document.getElementById("chkIsComponent").checked = true;
            //}
            //else {
            //    document.getElementById("chkIsComponent").checked = false;
            //}
            $("#addModelDialogBox").modal("show");
        }
    });
    checker = false;
}

function Clear() {
    $('#txtModel').val("");
    //$('#BodyHolder_ddlSubCategory').val("--- Select Sub-Category ---");
    //$('#BodyHolder_ddlCategory').val("--- Select Category ---");
    $('#txtDescriptionModel').val("");

    //$('#txtLife').val("");
    document.getElementById("chkActiveModel").checked = false;

    //document.getElementById("chkIsComponent").checked = false;
    //document.getElementById("chkTag").checked = false;
    //CountChar($('#txtDescription').val(), 100, 'cnt', 'txtDescription');
    idForUpdate = 0;
    CountChar($('#txtDescriptionModel').val(), 500, 'cntModel', 'txtDescriptionModel');
}

function GetSubCategory() {
    var category = $('#BodyHolder_ddlCategory').val();
    if ($('#BodyHolder_ddlCategory').val() == "--- Select Category ---")
        category = 0;
    if (checker == true) {
    }
    else if (checker == false) {
        $.ajax({
            type: "POST",
            url: "Files_Data/Model.aspx/GetSubCategory",
            data: " { category : '" + category + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ddlSubCategory = $("[id*=ddlSubCategory]");
                ddlSubCategory.empty().append("<option selected='selected' value='0'>--- Select Sub-Category ---</option>");
                $.each(msg.d, function () {
                    ddlSubCategory.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            }
        });
    }
}