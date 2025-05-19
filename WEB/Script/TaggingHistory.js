var url = "";

$(document).ready(function () {
    $('.select2').select2();

    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+1d',
        autoclose: true
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+1d',
        autoclose: true
    });
    $('#txtDateTo').datepicker('setDate', today);

    table = $('#tblTaggingHistory').DataTable({
        "scrollX": true
        , "searching": false
        , dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tagging History'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tagging History'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tagging History'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tagging History'
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
    load_Filter();
    $('#txtDateFrom').off('change').on('change', function () {
        SearchTaggingHistory();
    });

    $('#txtDateTo').off('change').on('change', function () {
        SearchTaggingHistory();
    });
});

var table;

function parseJsonDate(jsonDateString) {
    return new Date(parseInt(jsonDateString.replace('/Date(', '')));
}

function ConvertStringToDateInputFormat(sVal) {
    var now = new Date(sVal);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var selecteddate = (month) + "/" + (day) + "/" + now.getFullYear();

    return (selecteddate);
}
function SearchTaggingHistory() {
    var txtDateFrom = $('#txtDateFrom');
    var txtDateTo = $('#txtDateTo');
    table.clear().draw();
    $.ajax({
        type: "POST",
        url: url + "/TaggingHistory.aspx/SearchTaggingHistory",
        data: " { datefrom: '" + txtDateFrom.val() + "', dateto: '" + txtDateTo.val() + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg.d);
            var array = JSON.parse(msg.d);
            array.forEach((arr, index) => {
                table.row.add([arr.PropertyNumber, arr.PrinterName, arr.IPAddress, ConvertStringToDateInputFormat(parseJsonDate(arr.DateCreated))]);
            });
            table.draw();
        }
    });
}



//filter
function load_Filter() {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    $.ajax({
        type: 'POST',
        url: url + "/" + loc + "/GetFilter",
        data: "{  ReportName: '" + "TaggingHistory" + "'}",
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
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
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
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'reg SEPI'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'reg SEPI'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'reg SEPI'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'reg SEPI'
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
                        url: url + "/" + loc + "/SaveFilter",
                        data: " { ReportName : '" + ReportName + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_Filter();
                            clear_filter();
                            SearchTaggingHistory();
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
                            url: url + "/" + loc + "/UpdateFilter",
                            data: " { RecId : '" + recIdEdit + "' ,ReportName : '" + ReportName + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                load_Filter();
                                clear_filter();
                                SearchTaggingHistory();
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
                url: url + "/" + loc + "/GetActive",
                data: " { id : '" + id + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_Filter();
                    SearchTaggingHistory();
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
                url: url + "/" + loc + "/DeleteFilter",
                data: " { id : '" + id + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_Filter();
                    clear_filter();
                    SearchTaggingHistory();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}