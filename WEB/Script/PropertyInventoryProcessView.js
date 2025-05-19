var idForUpdate = 0;
var PropertyPageNo = 1;
var StockPageNo = 1;
var inventorycode = '';

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

    $("#tblInventoryList").DataTable({
        "scrollX": true
        //"searching": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
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
    load_data_inventorylist();
    load_Filter();
    $("#tblInventoryList").DataTable().clear().draw();
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
                $('#tblInventoryList').DataTable().column(7).visible(false);
            }
        }
    });
}

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_inventorylist();
        return false;
    }
}

function load_data_inventorylist() {
    var search = "";
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val();
    if (btnAccess[0] == "0") {
        $('#tblInventoryList').DataTable().column(7).visible(false);
    }
    $.ajax({
        type: 'POST',
        url: "Views/InventoryCountView.aspx/GetInventoryList",
        data: "{ search: '" + search + "', datefrom : '" + datefrom + "', dateto: '" + dateto + "' ,AccessButton: '" + btnAccess[0] + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#inventorylist').hide();
            $('#inventorylist').innerHTML = "";
            $('#inventorylist').fadeIn(200);

            myJsonData = data;
            populateDataTableInventoryList(myJsonData);

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

function populateDataTableInventoryList(data) {
    try {
        $('#tblInventoryList').DataTable({
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
                { "data": "InventoryName" },
                { "data": "Description" },
                { "data": "Site" },
                { "data": "Location" },
                { "data": "InventoryTypeString" },
                { "data": "DateCreated" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
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

        $('#tblInventoryList').off('click');

        $('#tblInventoryList').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblInventoryList').DataTable();
        table
            .clear()
            .draw();
    }
}

function getInventoryCount() {
    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetCountFound",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msgfound) {
            document.getElementById('countfound').innerHTML = msgfound.d;
        }
    });

    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetCountMissing",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msgmissing) {
            document.getElementById('countmissing').innerHTML = msgmissing.d;
        }
    });

    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetCountMisplaced",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msgmisplaced) {
            document.getElementById('countmisplaced').innerHTML = msgmisplaced.d;
        }
    });
}

function ViewInventoryDetails(inventoryname, inventoryCode, location) {
    inventorycode = inventoryCode;
    PageProperty(PropertyPageNo);
    PageStock(StockPageNo);
    $('#BodyHolder_txtInventoryCode').val(inventorycode);
    $('#BodyHolder_txtInventoryName').val(inventoryname);
    $('#BodyHolder_txtInventoryLocation').val(location);
    //getInventoryCount();
    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetPropertyInventoryListPrint",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintList').hide();
            $('#BodyHolder_divPropertyPrintList').html(msg.d);
            $('#BodyHolder_divPropertyPrintList').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetStockInventoryListPrint",
        data: " { inventorycode: '" + inventorycode + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockPrintList').hide();
            $('#BodyHolder_divStockPrintList').html(msg.d);
            $('#BodyHolder_divStockPrintList').fadeIn(200);
        }
    });

    $('#inventorylistmodal').modal('show');
}

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
}

function ExportProperty() {
    //$('#BodyHolder_btnExport').click();  
    var inventorycode = $('#BodyHolder_txtInventoryCode').val();
    var inventoryname = $('#BodyHolder_txtInventoryName').val();
    var inventorylocation = $('#BodyHolder_txtInventoryLocation').val();
    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetInventoryExportProperty",
        data: " { inventorycode: '" + inventorycode + "',inventoryname: '" + inventoryname + "',inventorylocation: '" + inventorylocation + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //Convert Base64 string to Byte Array.
            var bytes = Base64ToBytes(msg.d);

            //Convert Byte Array to BLOB.
            var blob = new Blob([bytes], { type: "application/octetstream" });

            //Check the Browser type and download the File.
            var isIE = false || !!document.documentMode;
            if (isIE) {
                window.navigator.msSaveBlob(blob, inventorycode);
            } else {
                var url = window.URL || window.webkitURL;
                link = url.createObjectURL(blob);
                var a = $("<a />");
                a.attr("download", inventorycode + ".csv");
                a.attr("href", link);
                $("body").append(a);
                a[0].click();
                $("body").remove(a);
            }
        }
    });
}

function PageProperty(pageNo) {
    PropertyPageNo = pageNo;

    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetPropertyInventoryList",
        data: " { inventorycode: '" + inventorycode + "', pageNo: '" + PropertyPageNo + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_PropertyInventoryListDetails').hide();
            $('#BodyHolder_PropertyInventoryListDetails').html(msg.d);
            $('#BodyHolder_PropertyInventoryListDetails').fadeIn(200);
        }
    });
}

function PageStock(pageNo) {
    StockPageNo = pageNo;

    $.ajax({
        type: "POST",
        url: "Views/InventoryCountView.aspx/GetStockInventoryList",
        data: " { inventorycode: '" + inventorycode + "', pageNo: '" + StockPageNo + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_StockInventoryListDetails').hide();
            $('#BodyHolder_StockInventoryListDetails').html(msg.d);
            $('#BodyHolder_StockInventoryListDetails').fadeIn(200);
        }
    });
}

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

//filter
function load_Filter() {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    $.ajax({
        type: 'POST',
        url: "Views/" + loc + "/GetFilter",
        data: "{  ReportName: '" + "InventoryCountView" + "'}",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count View'
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
                            load_data_inventorylist();
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
                                load_data_inventorylist();
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
                    load_data_inventorylist();
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
                    load_data_inventorylist();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}