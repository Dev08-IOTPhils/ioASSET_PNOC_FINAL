var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    //$('#addSecretQuestionDialogBox').modal({ backdrop: 'static', keyboard: false })

    $('.select2').select2();

    $("#tblBrandList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
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
    load_data();

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


});

function load_data() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/SecretQuestion.aspx/GetSecretQuestion",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#brandlist').hide();
            $('#brandlist').innerHTML = "";
            $('#brandlist').fadeIn(200);

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
        $('#tblBrandList').DataTable({
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
                { "data": "Question" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Security Question List'
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
        $('#tblBrandList').off('click');
    } catch (e) {
        var table = $('#tblBrandList').DataTable();
        table
            .clear()
            .draw();
    }
}

function addSecretQuestionDialogBox() {
    Clear();
    document.getElementById("chkActive").checked = true;
    $("#addSecretQuestionDialogBox").modal("show");

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
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditSecurityQuestion");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveSecurityQuestion',
                    class: 'btn btn-success btn_EditSecurityQuestion',
                    onclick: 'SaveSecurityQuestion()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelSecurityQuestion').prepend($formBrand);
            }
        }
    });
}

function SaveSecurityQuestion() {
    var cate = $('#txtQuestion').val().replace(/'/g, '');;
    var alertMsg;

    if ($('#txtQuestion').val() == '') {
        alertMsg = '';

        if ($('#txtQuestion').val() == '')
            alertMsg += '\Security Question';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm("Are you sure you want to save this Brand?") == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this security question?",
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
                    url: "Files_Data/SecretQuestion.aspx/SaveSecretQuestion",
                    data: " { id : '" + idForUpdate + "' , question : '" + cate + "',  active:'" + document.getElementById("chkActive").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addSecretQuestionDialogBox").modal("hide");
                            swal("Successful", "Security question successfully saved!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 1) {
                            $("#addSecretQuestionDialogBox").modal("hide");
                            swal("Successful", "Security question successfully modified!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Security question already exist!", "error");
                        }
                        else {
                            swal("Error", "Security question cannot be set to inactive. This is being actively used!", "error");
                            $("#addTagDialogBox").modal("hide");
                            Clear();
                        }
                    }
                })
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

function GetSecreQuestionID(id) {
    idForUpdate = id;
    var successHeader = '<span class="fa fa-check-circle fa-2x" '
           + 'style="vertical-align:middle;color:#08e001;">'
           + '</span> Security Question';
    $("#addSecretQuestionDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditSecurityQuestion");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/SecretQuestion.aspx/SecretQuestionByID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtQuestion').val(msg.d.split('|')[1]);
            if (msg.d.split('|')[2] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
        }
    });
}

function DeleteSecretQuestion(id) {
    swal({
        title: "Delete Security Question",
        text: "Are you sure you want to delete this Security Question?",
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
                url: "Files_Data/SecretQuestion.aspx/DeleteSecretQuestion",
                data: "{ id: '" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        //alert('Brand Successfully Deleted!');
                        swal("Successful", "Security Question Successfully Deleted!", "success");
                        Clear();
                        load_data();
                    }
                    else {
                        //alert('Brand cannot be Deleted, it is being actively used!');
                        swal("Error", "Security Question cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function Clear() {
    $('#txtQuestion').val("");
    $('#txtDescription').val("");
    document.getElementById("chkActive").checked = true;
    idForUpdate = 0;
}