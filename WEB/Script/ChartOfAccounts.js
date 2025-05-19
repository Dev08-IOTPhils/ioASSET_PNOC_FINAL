var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblChartOfAccounts").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
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

    //$('#addChartOfAccountsDialogBox').modal({ backdrop: 'static', keyboard: false })

});

function load_data() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/ChartOfAccounts.aspx/GetChartOfAccounts",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#chartofaccountslist').hide();
            $('#chartofaccountslist').innerHTML = "";
            $('#chartofaccountslist').fadeIn(200);

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
        $('#tblChartOfAccounts').DataTable({
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
                { "data": "AccountNumber" },
                { "data": "Description" },
                { "data": "AccountHeader" },
                { "data": "StringBalance" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Chart of Account List'
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

        $('#tblChartOfAccounts').off('click');
    } catch (e) {
        var table = $('#tblChartOfAccounts').DataTable();
        table
            .clear()
            .draw();
    }
}

function addChartOfAccountsDialogBox() {
    Clear();

    $('#txtBalance').prop('disabled', false);
    document.getElementById("chkActive").checked = true;
    $("#addChartOfAccountsDialogBox").modal("show");

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
                let elementsAddBrand = document.getElementsByClassName("btn_EditChartOfAccounts");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveBrand',
                    class: 'btn btn-success btn_EditChartOfAccounts',
                    onclick: 'SaveChartOfAccounts()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelCOA').prepend($formBrand);
            }
        }
    });
}

function SaveChartOfAccounts() {
    var accountnumber = ReplaceStr1($('#txtAccountNumber').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' '));
    var balance = $('#txtBalance').val().trim();
    var accountheader = $('#BodyHolder_ddlAccountHeader').val();
    var alertMsg;
    if (ReplaceStr1($('#txtAccountNumber').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '' || $('#txtBalance').val().trim() == '' || $('#BodyHolder_ddlAccountHeader').val() == '--- Select Account Header ---') {
        alertMsg = ' ';
        if (ReplaceStr1($('#txtAccountNumber').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nAccount Number';
        if (ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        if ($('#txtBalance').val() == '')
            alertMsg += '\nBalance';
        if ($('#BodyHolder_ddlAccountHeader').val() == '--- Select Account Header ---')
            alertMsg += '\nAccount Header';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this chart of account?",
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
                    url: "Files_Data/ChartOfAccounts.aspx/SaveChartOfAccounts",
                    data: " { id : '" + idForUpdate + "' , accountnumber : '" + accountnumber + "', description :'" + desc + "', accountheader : '" + $('#BodyHolder_ddlAccountHeader').val() + "', balance : '" + balance + "',active:'" + document.getElementById("chkActive").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addChartOfAccountsDialogBox").modal("hide");
                            swal("Successful", "Chart of account successfully saved!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 1) {
                            $("#addChartOfAccountsDialogBox").modal("hide");
                            swal("Successful", "Chart of account successfully modified!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Chart of account already exist!", "error");
                        }
                        else {
                            swal("Error", "Chart of account cannot be set to inactive. This is being actively used in property and/or supply!", "error");
                            $("#addChartOfAccountsDialogBox").modal("hide");
                            Clear();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

function GetChartOfAccountsID(id) {
    idForUpdate = id;
    $("#addChartOfAccountsDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditChartOfAccounts");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/ChartOfAccounts.aspx/ChartOfAccountsbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtAccountNumber').val(msg.d.split('|')[1]);
            $('#txtDescription').val(msg.d.split('|')[2]);
            $('#BodyHolder_ddlAccountHeader').val(msg.d.split('|')[3]).trigger('change');
            $('#txtBalance').val(msg.d.split('|')[4]);
            $('#txtBalance').prop('disabled', true);
            if (msg.d.split('|')[5] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
        }
    });
}

function deleteChartOfAccounts(id) {
    idForUpdate = id;
    //$("#deleteChartOfAccounts").modal("show");
    swal({
        title: "Delete Chart of Account",
        text: "Are you sure you want to delete this chart of account?",
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
                url: "Files_Data/ChartOfAccounts.aspx/DeleteChartOfAccounts",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Chart Of Account Successfully Deleted!", "success");
                        Clear();
                        load_data();
                    }
                    else {
                        swal("Error", "Chart Of Account cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteChartOfAccounts() {
    $("#deleteChartOfAccounts").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/ChartOfAccounts.aspx/DeleteChartOfAccounts",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Chart Of Accounts Successfully Deleted!');
                Clear();
                load_data();
            }
            else {
                alert('Chart Of Accounts cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtAccountNumber').val("");
    $('#txtDescription').val("");
    $('#BodyHolder_ddlAccountHeader').val("--- Select Account Header ---").trigger('change');
    $('#txtBalance').val("");
    document.getElementById("chkActive").checked = true;
    idForUpdate = 0;
    CountChar($('#txtDescription').val(), 500, 'cnt', 'txtDescription');
}