$(document).ready(function () {
    $('.select2').select2();
    $('#tblAssetList').DataTable({
        "destroy": true, "autoWidth": true,
        //"searching": false,
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        buttons: {
            buttons: [
                    'copy',
                    'excel',
                    'csv',
                    //'pdf',
                    'print',
                    'colvis',

                {
                    extend: 'colvisGroup',
                    text: 'Properties for Repair and Maintenance',
                    show: [0, 1, 2, 3, 4, 18, 19, 20, 21, 25, 30],
                    hide: [5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 22, 23, 24, 26, 27, 28, 29, 31, 32, 33, 34, 35]
                },
                {
                    extend: 'colvisGroup',
                    text: 'Properties with valid Service Warranty',
                    show: [0, 1, 2, 3, 4, 18, 20, 22],
                    hide: [5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19, 21, 23, 24, 26, 27, 28, 29, 31, 32, 33, 34, 35]
                }
            ]
        }, colReorder: true
            , stateSave: true
    });
    load_data();
    load_Filter();
    //PopulatefilterBy();

});

var recIdEdit = "";
var LogicEdit = "";
var FilterEdit = "";
var OperatorEdit = "";
var ValueEdit = "";

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        return false;
    }
}

//var x = new Array();
function load_data() {
    var search = "";

    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Views/PropertyMasterList.aspx/GetList",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            //x = JSON.parse(data);


            populateDataTable(myJsonData);

            console.log(data);
            console.log('Success');

            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}



var dttable;

function populateDataTable(data) {
    //alert(data.d);
    try {
        dttable =
        $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            }, "autoWidth": true,
            //"autoWidth": false,
            "scrollX": true,
            //"searching": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "data": JSON.parse(data.d),
            "columns": [
                                { "data": "PropertyNumber" },

                { "data": "ItemCode" },
                { "data": "Description" },

                                { "data": "LongDescription" },
                                                                { "data": "UnitOfMeasurement" },

                                
                                                { "data": "SerialNo" },
                { "data": "IssuedTo" },
                                { "data": "Status" },
                                                { "data": "Site" },
                { "data": "Location" },
                { "data": "Area" },
                                { "data": "CustodianIDstr" },

                                { "data": "CategoryName" },
                                                { "data": "ClassificationName" },
                { "data": "BrandName" },
                                { "data": "ModelName" },


                { "data": "UserDepartment" },
                { "data": "TypeCost" },
                { "data": "PRNumber" },
                { "data": "PONumber" },
                { "data": "InvoiceNumber" },
                { "data": "DRNumber" },
                { "data": "IssuanceNumber" },

                { "data": "DateOfAcquisition" },
                { "data": "UsefulLifeString" },
                { "data": "RemainingUsefulLifeString" },
                { "data": "DateOfMaintenance" },
                { "data": "WarrantyDate" },
                { "data": "InsuranceDate" },
                { "data": "ExpiryDate" },
                { "data": "AcquiredCostString" },
                { "data": "DepreciatedCostString" },
                { "data": "DisposedDate" },
                { "data": "IssuedDate" },
                { "data": "LastmaintenanceDate" },
                { "data": "InventoryName" },
                { "data": "InventoryDate" },
                { "data": "InventoryStatus" },
                { "data": "InventorySite" },
                { "data": "InventoryLocation" }

            ],
            "rowCallback": function (row, data, index) {
                //if (data.Status == 'Disposed') {
                //    $('td', row).css('color', 'Gray');
                //}
                //else if (data.Status != 'On Stock' || data.Status != 'Issued' || parseInt(data.RemainingUsefulLifeString) <= 0) {
                //    $('td', row).css('color', 'Red');
                //}
                //else {
                //    //$('td', row).css('color', 'Red');
                //}
                if (data.Status == 'Disposed') {
                    $('td', row).css('color', 'Gray');
                }
                else if (data.Status == 'Issued') {
                    $('td', row).css('color', 'Black');
                }
                else if (data.Status == 'On Stock') {
                    $('td', row).css('color', 'Green');
                }
                //else if (parseInt(data.RemainingUsefulLifeString) <= 0 || (data.Status != 'On Stock' && data.Status != 'Issued')) {//|| dateNow.toLocaleDateString("en-US") > data.ExpiryDate) {
                //    $('td', row).css('color', 'Red');
                //}
                else {
                    $('td', row).css('color', 'Red');
                }
            },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                          },
                           {
                               extend: 'colvis',
                               postfixButtons: ['colvisRestore'],
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' },
                               collectionLayout: 'four-column'
                           },
                             {
                                 extend: 'colvisGroup',
                                 text: 'Properties for Repair and Maintenance',
                                 show: [0, 1, 2, 3, 4, 18, 19, 20, 21, 25, 30],
                                 hide: [5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 22, 23, 24, 26, 27, 28, 29, 31, 32, 33, 34, 35]
                             },
                {
                    extend: 'colvisGroup',
                    text: 'Properties with valid Service Warranty',
                    show: [0, 1, 2, 3, 4, 18, 20, 22],
                    hide: [5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19, 21, 23, 24, 26, 27, 28, 29, 31, 32, 33, 34, 35]
                }

                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
            , stateSave: true
        });
        $('#tblAssetList').off('click');

        //document.getElementById('drpFilterBy').innerHTML = "";
        //var $dropdown = $("#drpFilterBy");
        //$dropdown.append($('<option>', {
        //    value: '0',
        //    text: ' Select Filter ',
        //    rowindex: -1
        //}));
        //var columns = dttable.settings().init().columns;
        //dttable.columns().every(function (index) {
        //    //cols.push(columns[index].data)
        //    $dropdown.append($("<option />").val(columns[index].data).text(columns[index].data)); //adding data on dropdown 
        //})

        //console.log(dttable);
        //console.log(dttable.settings());
        //console.log(dttable.settings().init().columns);
        //var ds2d = dttable.settings().init().columns;
        //console.log("ss");
        //console.log(ds2d[0].data);
        //var dsd = dttable.settings().init().columns().data();
        //console.log(dsd[1].data);

    } catch (e) {
        var table = $('#tblAssetList').DataTable();
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
        data: "{  ReportName: '" + "PropertyMasterList" + "'}",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Masterlist'
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
                            load_data();
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
                                load_data();
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
                    load_data();
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
                    load_data();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}