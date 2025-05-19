$(document).ready(function () {
    $('.select2').select2();

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateTo').datepicker('setDate', today);

    $("#tblPendingUpload").DataTable({
        "scrollX": true
        //, "searching": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
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

    AccessButton();
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
                $('#tblPendingUpload').DataTable().column(5).visible(false);
            }
        }
    });
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
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val();
    if (btnAccess[0] == "0") {
        $('#tblPendingUpload').DataTable().column(5).visible(false);
    }
    $.ajax({
        type: 'POST',
        url: "Views/PropertyDisposalRequestHistory.aspx/GetDisposalHistory",
        data: "{ search: '" + search + "', datefrom : '" + datefrom + "', dateto: '" + dateto + "',AccessButton: '" + btnAccess[0] + "' }",
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
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DisposalNumber" },
                { "data": "TransactorName" },
                //{ "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
                { "data": "Status" },
                { "data": "Remarks" },
                { "data": "SupportingDocument" },
                { "data": "Action" }
            ]
        , dom: 'Brftip',
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
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
        });
    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var disposalnumber = '';
function ViewDisposalDetails(disposalno, transactorname, createddate, isapproved) {
    disposalnumber = disposalno;
    document.getElementById('lblDisposalNo').innerHTML = disposalno;
    document.getElementById('lblDisposedBy').innerHTML = transactorname
    document.getElementById('lblDisposedDate').innerHTML = createddate;

    $('#BodyHolder_txtDisposalNumber').val(disposalno);

    load_data_PUL1();
    $('#ALPendingUpload').modal('show');
    x.style.display = "none";
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: "Views/PropertyDisposalRequestHistory.aspx/GetPLPendingUploadList",
        data: "{disposalno: '" + disposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pul1').hide();
            $('#pul1').innerHTML = "";
            $('#pul1').fadeIn(200);

            myJsonData = data;

            populateDataTablePUL1(myJsonData);

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

function populateDataTablePUL1(data) {
    try {
        $('#tblPUL1').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Status" },
                { "data": "Condition" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request Disposal'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' },
                               collectionLayout: 'two-column'
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
, stateSave: true
        });

        $('#tblPUL1').off('click');
    } catch (e) {
        var table = $('#tblPUL1').DataTable();
        table
            .clear()
            .draw();
    }
}

//filter
function load_Filter() {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    $.ajax({
        type: 'POST',
        url: "Views/" + loc + "/GetFilter",
        data: "{  ReportName: '" + "PropertyDisposalRequestHistory" + "'}",
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
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request History'
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