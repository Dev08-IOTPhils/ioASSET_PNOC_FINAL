var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblCustodianList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
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
    //$('#addCustodianDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_data() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Custodian.aspx/GetCustodian",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#custodianlist').hide();
            $('#custodianlist').innerHTML = "";
            $('#custodianlist').fadeIn(200);

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
        $('#tblCustodianList').DataTable({
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
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Custodian" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Custodian List'
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

        $('#tblCustodianList').off('click');
    } catch (e) {
        var table = $('#tblCustodianList').DataTable();
        table
            .clear()
            .draw();
    }
}

function addCustodianDialogBox() {
    Clear();
    document.getElementById("chkActive").checked = true;
    $("#addCustodianDialogBox").modal("show");
}

function SaveCustodian() {
    var cate = ReplaceStr1($('#txtCustodian').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' '));
    var alertMsg;

    if (ReplaceStr1($('#txtCustodian').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = '';


        if (ReplaceStr1($('#txtCustodian').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nCustodian Type';
        if (ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this custodian?",
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
                    url: "Files_Data/Custodian.aspx/SaveCustodian",
                    data: " { id : '" + idForUpdate + "' , custodian : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActive").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addCustodianDialogBox").modal("hide");
                            swal("Successful", "Custodian successfully saved!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 1) {
                            $("#addCustodianDialogBox").modal("hide");
                            swal("Successful", "Custodian successfully modified!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Custodian already exist!", "error");
                        }
                        else if (msg.d == 3) {
                            $("#addCustodianDialogBox").modal("hide");
                            swal("Error", "Custodian license is full!", "error");
                            Clear();
                            load_data();
                        }
                        else {
                            $("#addCustodianDialogBox").modal("hide");
                            swal("Error", "Custodian cannot be set to inactive. This is being actively used!", "error");
                            Clear();
                            load_data();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

function GetCustodianID(id) {
    idForUpdate = id;
    $("#addCustodianDialogBox").modal("show");
    $.ajax({
        type: "POST",
        url: "Files_Data/Custodian.aspx/CustodianbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCustodian').val(msg.d.split('|')[1]);
            $('#txtDescription').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
        }
    });
}

function deleteCustodian(id) {
    idForUpdate = id;
    //$("#deleteCustodian").modal("show");
    swal({
        title: "Delete Custodian Type",
        text: "Are you sure you want to delete this custodian type?",
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
                url: "Files_Data/Custodian.aspx/DeleteCustodian",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Custodian Type Successfully Deleted!", "success");
                        Clear();
                        load_data();
                    }
                    else {
                        swal("Error", "Custodian cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteCustodian() {
    $("#deleteCustodian").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Custodian.aspx/DeleteCustodian",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Custodian Successfully Deleted!');
                Clear();
                load_data();
            }
            else {
                alert('Custodian cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtCustodian').val("");
    $('#txtDescription').val("");
    document.getElementById("chkActive").checked = true;
    idForUpdate = 0;
}