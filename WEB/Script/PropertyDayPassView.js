
var idForUpdate = 0;
var requestcount = 0;
$(document).ready(function () {
    $('.select2').select2();
    AccessButton();
    $("#tblPendingUpload").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
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
    load_data_Pendingupload();
    load_Filter();
    $("#tblPendingUpload").DataTable().clear().draw();
});
var btnAccess = [];
function AccessButton() {
    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            btnAccess = msg1.d.split('|');
            if (btnAccess[0] == "0") {
                $('#tblPendingUpload').DataTable().column(7).visible(false);
            }
        }
    });
}
function ReplaceStr(str) {
    var newStr = str.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
    return newStr;
}

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Pendingupload();
        return false;
    }
}


function load_data_Pendingupload() {
    var search = "";
    if (btnAccess[0] == "0") {
        $('#tblPendingUpload').DataTable().column(7).visible(false);
    }
    $.ajax({
        type: 'POST',
        url: "Views/DayPassView.aspx/GetPropertyDayPassPendingApproval",
        data: "{ search: '" + search + "',AccessButton: '" + btnAccess[0] + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#pendinguploadlist').hide();
            $('#pendinguploadlist').innerHTML = "";
            $('#pendinguploadlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUpload(myJsonData);

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

function populateDataTablePendingUpload(data) {
    try {

        $('#tblPendingUpload').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DayPassNumber" },
                { "data": "CreatedBy" },
                { "data": "DateOfTransaction" },
                { "data": "Purpose" },
                { "data": "Status" },
                { "data": "isIn" },
                { "data": "isOut" },
                { "data": "Action" }
            ]
      ,  "scrollX": true,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request View'
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

        $('#tblPendingUpload').off('click');

        $('#tblPendingUpload').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var data = $('#tblPendingUpload').DataTable().row(currentRow).data();
            if (data['isInString'] == 'False') {
                document.getElementById("btnCheckIn").disabled = false;
                document.getElementById("btnCheckOut").disabled = true;
            }
            else {
                document.getElementById("btnCheckIn").disabled = true;
                document.getElementById("btnCheckOut").disabled = false;
            }
        });

    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var daypassnumber = '';
var createdby = '';
var createddate = '';
function ViewList(daypassno, transactor, date) {
    daypassnumber = daypassno;
    createdby = transactor;
    createddate = date;
    load_pending_list();
    $('#modalPropertyList').modal('show');
}

function load_pending_list() {

    $.ajax({
        type: "POST",
        url: "Views/DayPassView.aspx/PropertyDayPassRequestList",
        data: "{ daypassno : '" + daypassnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pal').hide();
            $('#pal').innerHTML = "";
            $('#pal').fadeIn(200);

            myJsonData = data;
            populateALPending(myJsonData);

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

function populateALPending(data) {
    try {
        $('#tblPAL').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "SerialNumber" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Description" }
            ], buttons: {
                buttons: [
                   
                    {
                        extend: 'excelHtml5',
                        text: '<i class="fa fa-file-excel-o"></i><span style="font-family:Source Sans Pro,Helvetica Neue,Helvetica,Arial,sans-serif; size:14px;"> Excel</span>',
                        //title: 'Excel',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csvHtml5',
                        text: '<i class="fa fa-file-excel-o"></i><span> CSV</span>',
                        fieldSeparator: '|',
                        extension: '.csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                     {
                         extend: 'print',
                         messageTop: '<b>Day Pass #:</b> ' + daypassnumber + '<b>Created By:</b> ' + createdby + '<br><b>Date:</b> ' + createddate,
                         text: '<i class="fa fa-print"></i><span> Print</span>',
                         exportOptions: {
                             columns: ':visible'
                         }
                     }
                ]
            }
        });

        $('#tblPAL').off('click');

    } catch (e) {
        var table = $('#tblPAL').DataTable();
        table
            .clear()
            .draw();
    }
}

function CheckIn() {
    //if (confirm("Are you sure you want to check in this Day Pass?") == false)
    //return false;
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to check in this Day Pass?",
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
                url: "Views/DayPassView.aspx/CheckIn",
                data: "{ daypassno : '" + daypassnumber + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_Pendingupload();
                    swal("Success", "Day Pass successfully Checked IN.", "success");
                    $('#modalPropertyList').modal('hide');
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });

}

function CheckOut() {
    //if (confirm("Are you sure you want to check out this Day Pass?") == false)
    //    return false; 
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to check out this Day Pass?",
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
                url: "Views/DayPassView.aspx/CheckOut",
                data: "{ daypassno : '" + daypassnumber + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_Pendingupload();
                    //alert('Day Pass successfully Checked OUT.');
                    swal("Success", "Day Pass successfully Checked OUT.", "success");
                    $('#modalPropertyList').modal('hide');
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}


//filter
function load_Filter() {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    $.ajax({
        type: 'POST',
        url: "Views/" + loc + "/GetFilter",
        data: "{  ReportName: '" + "DayPassView" + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#FilterList').hide();
            $('#FilterList').innerHTML = "";
            $('#FilterList').fadeIn(200);

            myJsonData = data;
            populateDataTableFilter(myJsonData);

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

function clear_filter() {
    $('#txtValue').val('');
    $('#drpLogic').val("Select Logic").trigger('change');
    $('#BodyHolder_drpFilterBy').val(" Select Filter ").trigger('change');
    $('#drpOperator').val("Select Operator").trigger('change');
    $('#btnAddFilter').val('Add Filter');
}


function populateDataTableFilter(data) {
    try {
        $('#tblFilterDisplay').DataTable({
            "destroy": true,
            "processing": true,
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Logic" },
                { "data": "FilterBy" },
                { "data": "Operator" },
                { "data": "Values" },
                { "data": "IsActive" },
                { "data": "Action" }
            ],
            dom: 'Brftip',
            "columnDefs": [
                      {
                          "targets": [0, 1, 2, 3, 5],
                          "orderable": false
                      },
                      {
                          targets: 'active',
                          data: {
                              _: "active.display",
                              sort: "active.@data-order",
                              type: "active.@data-order"
                          }
                      }
            ],
            buttons: {
                buttons: [
                        'copy',
                        'excel',
                        'csv',
                        //'pdf',
                        'print',
                        'colvis'
                ]
            }, colReorder: true
            , stateSave: true
        });
        $('#tblFilterDisplay').off('click');

    } catch (e) {
        var table = $('#tblFilterDisplay').DataTable();
        table
            .clear()
            .draw();
    }
}

function GetFilterID(id, logic, filterby, operator, value) {
    LogicEdit = logic;
    FilterEdit = filterby;
    OperatorEdit = operator;
    ValueEdit = value;
    recIdEdit = id;

    $('#txtValue').val(value);
    $('#drpLogic').val(logic).trigger('change');
    $('#BodyHolder_drpFilterBy').val(filterby).trigger('change');
    $('#drpOperator').val(operator).trigger('change');
    $('#btnAddFilter').val('Save Changes');
}


function SaveFilter(ReportName) {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    var Logic = $('#drpLogic').val();
    var Filter = $('#BodyHolder_drpFilterBy').val();
    var Operator = $('#drpOperator').val();
    var Value = $('#txtValue').val();
    var alertMsg = "";
    if (Logic == 'Select Logic' || Filter == ' Select Filter ' || Operator == 'Select Operator') {
        if (Logic == 'Select Logic')
            alertMsg += '\nLogic';
        if (Filter == ' Select Filter ')
            alertMsg += '\nFilter';
        if (Operator == 'Select Operator')
            alertMsg += '\nOperator';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this filter?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                if ($('#btnAddFilter').val() == 'Add Filter') {
                    $.ajax({
                        type: "POST",
                        url: "Views/" + loc + "/SaveFilter",
                        data: " { ReportName : '" + ReportName + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_Filter();
                            clear_filter();
                            load_data_Pendingupload();
                        }
                    });
                }
                else {
                    if (Logic == LogicEdit && Filter == FilterEdit && Operator == OperatorEdit && Value == ValueEdit) {
                        clear_filter();
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "Views/" + loc + "/UpdateFilter",
                            data: " { RecId : '" + recIdEdit + "' ,ReportName : '" + ReportName + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                load_Filter();
                                clear_filter();
                                load_data_Pendingupload();
                            }
                        });
                    }
                }
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function GetActive(id) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to change the Active status?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            var currentLocation = window.location;
            var loc = currentLocation.pathname.split('/')[2];

            $.ajax({
                type: "POST",
                url: "Views/" + loc + "/GetActive",
                data: " { id : '" + id + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_Filter();
                    load_data_Pendingupload();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}



function DeleteFilter(id) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to delete this filter?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            var currentLocation = window.location;
            var loc = currentLocation.pathname.split('/')[2];

            $.ajax({
                type: "POST",
                url: "Views/" + loc + "/DeleteFilter",
                data: " { id : '" + id + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_Filter();
                    clear_filter();
                    load_data_Pendingupload();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}