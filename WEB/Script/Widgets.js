$(document).ready(function () {
    $('.select2').select2();
    StocksUnderMinLevel();
    //PropertyImportantSched();
    load_Status_tbl();
    load_data_Stock1();
    load_data_Property1();
    //load_data_TMPStock();

    //ed
    $('#tblAssetListTop10Modal').DataTable({
        "responsive": true,
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Bftip',
        buttons: {
            buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
                        },
                        {
                            extend: 'csv',
                            text: 'CSV',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
                        },
                        {
                            extend: 'print',
                            text: 'PRINT',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
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
        }
        , colReorder: true
        , stateSave: true
    });
    $('#tblAssetListTop10').DataTable({
        "responsive": true,
        "scrollX": true,
        dom: 'Bftip',
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
                        },
                        {
                            extend: 'csv',
                            text: 'CSV',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
                        },
                        {
                            extend: 'print',
                            text: 'PRINT',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most Used Property'
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
        }
        , colReorder: true
        , stateSave: true
    });

    //ed
    $('#tblAssetListDonnut').DataTable({
        "destroy": true,
        "responsive": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        "scrollX": true,
        dom: 'Bftip',
        buttons: {
            buttons: [
                'copy',
                'excel',
                'csv',
                'pdf',
                'print',
                'colvis'
            ]
        }, colReorder: true
        , stateSave: true,
        "initComplete": function (settings, json) {
            $("#tblAssetListDonnut").wrap("<div style='overflow:auto; width:100%;position:relative;'></div>");
        }
    });

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        autoclose: true,
        onSelect: function (dateText) {
            display("Selected date: " + dateText + ", Current Selected Value= " + this.value);
            $(this).change();
        }
    }).on("change", function () {
        ChangeDate();
    });

    //ed
    //stockorprop = $('#ddlTop10stock').val();
    stockorprop = $('#ddlTop10').val();
    top10MostUsedProperty();

    $('#ddlTop10').on('change', function () {
        ChangeDate1();
    });

    stockorprop = $('#ddlTop10').val();
    top10MostUsedStock();
    $('#ddlTop10').on('change', function () {
        ChangeDate1();
    });

    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        autoclose: true,
        onSelect: function (dateText) {
            display("Selected date: " + dateText + ", Current Selected Value= " + this.value);
            $(this).change();
        }
    }).on("change", function () {
        ChangeDate();
    });
    $('#txtDateTo').datepicker('setDate', today);

    $("#tblPropertyStatus").DataTable({
        "scrollX": true
        //, "searching": false
        ,
        dom: 'Bftip',
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                        },
                        {
                            extend: 'csv',
                            text: 'CSV',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                        },
                        {
                            extend: 'print',
                            text: 'PRINT',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
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

    $("#tblStocksUnderMinLevel").DataTable({
        "scrollX": true
        //, "searching": false
        ,
        dom: 'Bftip',
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                        },
                        {
                            extend: 'csv',
                            text: 'CSV',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                        },
                        {
                            extend: 'print',
                            text: 'PRINT',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
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

    $("#tblPropertyImportantSched").DataTable({
        "scrollX": true
        //, "searching": false
        ,
        dom: 'Bftip',
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
                        },
                        {
                            extend: 'csv',
                            text: 'CSV',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
                        },
                        {
                            extend: 'print',
                            text: 'PRINT',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
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

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDateFromMUP').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        autoclose: true,
        onSelect: function (dateText) {
            display("Selected date: " + dateText + ", Current Selected Value= " + this.value);
            $(this).change();
        }
    }).on("change", function () {
        ChangeDate1();
    });
    $('#txtDateFromMUP').datepicker('setDate', today);

    $('#txtDateToMUP').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        autoclose: true,
        onSelect: function (dateText) {
            display("Selected date: " + dateText + ", Current Selected Value= " + this.value);
            $(this).change();
        }
    }).on("change", function () {
        ChangeDate1();
    });
    $('#txtDateToMUP').datepicker('setDate', today);
});

var stockorprop = '';

function ChangeDate() {
    PropertyImportantSched();
    PISLWidgetType();
    //PISLGraphBar();
    //PISLdonutchart();
}

function ChangeDate1() {
    stockorprop = $('#ddlTop10').val();
    if (stockorprop == "2") {
        top10MostUsedStock();
    }
    else {
        top10MostUsedProperty();
    }
}

function PropertyImportantSched() {
    var dateStart = $('#txtDateFrom').val();
    var dateEnd = $('#txtDateTo').val();

    //var dateStart = '10/04/2022';
    //var dateEnd = '04/28/2023';

    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/PropertyImportantSched",
        data: "{ dateStart:'" + dateStart + "', dateEnd:'" + dateEnd + "', label:'" + label + "' , isdet: 'display'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;
            populateDataTablePIS(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            //alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePIS(data) {
    try {
        $('#tblPropertyImportantSched').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [

                { "data": "DateOfTransaction" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "TransactionType" }

            ],
            dom: 'Bftip',
            buttons: {
                buttons: [
                    {
                        extend: 'collection',
                        text: '...',
                        buttons: [
                            {
                                extend: 'copy',
                                text: 'COPY',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule List'
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

        $('#tblPropertyImportantSched').off('click');
    } catch (e) {
        var table = $('#tblPropertyImportantSched').DataTable();
        table
            .clear()
            .draw();
    }
}

/////////////////////////////////////////////
////////    PROPERTY STATUS       ///////////
/////////////////////////////////////////////

var stat = '';
function modalPropertyStatusChartDonnut() {
    $('#legend').empty();
    var randomcolors = [];
    for (var i = 0; i <= 20; i++) {
        randomcolors.push('#' + Math.floor(Math.random() * 16777215).toString(16));
    }
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphDonnut",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var donut =
                Morris.Donut({
                    element: 'Status_Count',
                    resize: true,
                    colors: randomcolors,//["#3c8dbc", "#f56954", "#00a65a", "#00aa00", "#ffaa00"],
                    data: JSON.parse(result.d),
                    hideHover: 'auto',
                }).on('click', function (i, row) {
                    stat = row.label;
                    //alert(stat);
                    load_data_PS();
                    $("#DonnutModal").modal("show");
                });
            donut.options.data.forEach(function (label, i) {
                //$('#legend').empty();
                var legendItem = $('<a><span onclick="clicklegend(' + label['label'] + ')"></span></a>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                legendItem.find('span')
                    .css('backgroundColor', donut.options.colors[i])
                    .css('width', '20px')
                    .css('display', 'inline-block')
                    .css('margin', '5px');
                $('#legend').append(legendItem)
            });
        },
        error: function (e) {
            alert(e);

            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function clicklegend(status) {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetStatusList",
        data: "{search: '" + status + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlistdonnut').hide();
            $('#assetlistdonnut').innerHTML = "";
            $('#assetlistdonnut').fadeIn(200);

            myJsonData = data;

            populateDataTablePS(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });

    $("#DonnutModal").modal("show");
}

function clicklegend(SUML) {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetStatusList",
        data: "{search: '" + status + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlistdonnut').hide();
            $('#assetlistdonnut').innerHTML = "";
            $('#assetlistdonnut').fadeIn(200);

            myJsonData = data;

            populateDataTablePS(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });

    $("#DonnutModal").modal("show");
}

function load_data_PS() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetStatusList",
        data: "{search: '" + stat + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlistdonnut').hide();
            $('#assetlistdonnut').innerHTML = "";
            $('#assetlistdonnut').fadeIn(200);

            myJsonData = data;

            populateDataTablePS(myJsonData);

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

function load_data_PISL() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetStatusList",
        data: "{search: '" + stat + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlistdonnut').hide();
            $('#assetlistdonnut').innerHTML = "";
            $('#assetlistdonnut').fadeIn(200);

            myJsonData = data;

            populateDataTablePS(myJsonData);

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

function load_data_Property1() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetTop10Property",
        data: "{search: '" + stat + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlistdonnut').hide();
            $('#assetlistdonnut').innerHTML = "";
            $('#assetlistdonnut').fadeIn(200);

            myJsonData = data;

            populateDataTableProperty1(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            //

            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function load_Status_tbl() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadStatustbl",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;

            PopulateStatusTbl(myJsonData);

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

function PopulateStatusTbl(data) {
    try {
        $('#tblPropertyStatus').DataTable({
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
                { "data": "Status" },
                { "data": "Values" }

            ],
            "initComplete": function (settings, json) {
                $("#tblPropertyStatus").wrap("<div style='overflow:auto; width:100%;position:relative;'></div>");
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
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
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
    } catch (e) {
        var table = $('#tblPropertyStatus').DataTable();
        table
            .clear()
            .draw();
    }
}

function populateDataTablePS(data) {
    try {
        $('#tblAssetListDonnut').DataTable({
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
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "LongDescription" },
                { "data": "Status" }
            ],
            "initComplete": function (settings, json) {
                $("#tblAssetListDonnut").wrap("<div style='overflow:auto; width:100%;position:relative;'></div>");
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
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Status'
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
        }).search('').draw();

        $('#tblAssetListDonnut').off('click');
    } catch (e) {
        var table = $('#tblAssetListDonnut').DataTable();
        table
            .clear()
            .draw();
    }
}

//////////////////////////////////////////////////////////////

var itemsPackChart;
//function packResult(startdate, enddate, result) {
function packResult(result) {
    result = JSON.parse(result);
    console.log(result.length);
    var data = [];
    var value = 0;
    var itemcode = '';
    for (var i = 0; i < result.length; i++) {
        itemcode = result[i].ItemCode;
        value = result[i].value;
        break;
    }

    data.push({ ItemCode: itemcode, value: value });
    if (itemsPackChart != null) {
        itemsPackChart.setData(data);
    } else {
        $("#bar-chart-Stock").empty().off('click');
        itemsPackChart =
            Morris.Bar({
                element: 'bar-chart-Stock',
                data: data,
                xkey: 'ItemCode',
                ykeys: ['value'],
                labels: ['Count'],
                hideHover: true,
                resize: true,
                barColors: ['#00a65a'],
                //barColors: ['#ed5565'],
            }).on('click', function (j, row) {
                //alert('hello');

                if (stockorprop == "2") {
                    itemcode1 = row.ItemCode;
                    ;
                    load_data_TMPStock();
                    //alert(itemcode1);
                    $("#top10ModalStock").modal("show");
                }
                else {
                    itemcodestock = row.ItemCode;
                    //alert(itemcodestock);
                    load_data_TMPStock();
                    $("#top10Modalstock").modal("show");
                }
            });
    }
}

/////////////////////////////////////////////////
////////  STOCK UNDER MINIMUM LEVEL    /////////
////////////////////////////////////////////////

function SUMLWidgetType() {
    var type = $('#ddlSUMLType').val();
    if (type == '1') {
        $('#SUMLTable').show();
        $('#SUMLBarChart').hide();
        $('#DIVDonutChart').hide();
        StocksUnderMinLevel();
    }
    else if (type == '2') {
        $('#SUMLTable').hide();
        $('#SUMLBarChart').show();
        $('#DIVDonutChart').hide();
        SUMLGraphBar();
    }
    else if (type == '3') {
        $('#SUMLTable').hide();
        $('#SUMLBarChart').hide();
        $('#DIVDonutChart').show();
        SUMLLoadDonnutGraph();
    }
    else {
        $('#DIVDonutChart').hide();
        $('#SUMLTable').hide();
        $('#SUMLBarChart').hide();
    }
}

function StocksUnderMinLevel() {
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/StocksUnderMinLevel",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;
            populateDataTableSUNML(myJsonData);

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

function populateDataTableSUNML(data) {
    try {
        $('#tblStocksUnderMinLevel').DataTable({
            "destroy": true,
            "processing": true,
            "scrollX": false,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "CriticalLevel" },
                { "data": "Quantity" }
            ],
            "rowCallback": function (row, data, index) {
                //if (parseInt(data.QuantityString) <= parseInt(data.CriticalLevelString)) {
                    $('td', row).css('color', 'Red');
                //}
                //else {
                //    $('td', row).css('color', 'Green');
                //}
                //if (parseInt(data.MaintenanceLeadTime) < 0) {
                //    $('td', row).css('color', 'Red');
                //}
            }, dom: 'Brftip',
            buttons: {
                buttons: [
                    {
                        extend: 'collection',
                        text: '...',
                        buttons: [
                            {
                                extend: 'copy',
                                text: 'COPY',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
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

        $('#tblStocksUnderMinLevel').off('click');
    }
    catch (e) {
        var table = $('#tblStocksUnderMinLevel').DataTable();
        table
            .clear()
            .draw();
    }
}

var stat = '';
function SUMLLoadDonnutGraph() {
    $('#SUMLlegend').empty();
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphDonnutSUML",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var donut =
                Morris.Donut({
                    element: 'DonutChart',
                    resize: true,
                    colors: ["#3c8dbc", "#f56954", "#00a65a", "#00aa00", "#ffaa00"],
                    data: JSON.parse(result.d),
                    hideHover: 'auto',
                }).on('click', function (i, row) {
                    stat = row.label;
                    //alert(stat);
                    StocksUnderMinLevelModal(stat);
                    $("#SUMLModal").modal("show");
                });
            donut.options.data.forEach(function (label, i) {
                //$('#legend').empty();
                var legendItem = $('<a><span onclick="clicklegend(' + label['label'] + ')"></span></a>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                legendItem.find('span')
                    .css('backgroundColor', donut.options.colors[i])
                    .css('width', '20px')
                    .css('display', 'inline-block')
                    .css('margin', '5px');
                $('#SUMLlegend').append(legendItem)
            });
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function StocksUnderMinLevelModal(stat) {
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/StocksUnderMinLevelGraph",
        data: "{stat: '" + stat + "' }",    
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;
            populateDataTableSUNMLModal(myJsonData);

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

function populateDataTableSUNMLModal(data) {
    try {
        $('#SUMLtblModal').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "scrollX": false,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "CriticalLevel" },
                { "data": "Quantity" }
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
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supplies Under Reorder Point'
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
    } catch (e) {
        var table = $('#tblStocksUnderMinLevel').DataTable();
        table
            .clear()
            .draw();
    }
}

//////// BAR GRAPH

var itemcode1 = '';
var stat1 = '';
function SUMLGraphBar() {
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphBarSUML",
        //url: "DashBoard.aspx/LoadGraphBarSUML",
        //data: "{ startdate: '" + $('#txtDateFromMUP').val() + "', enddate: '" + $('#txtDateToMUP').val() + "' }",
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //packResult($('#txtDateFromMUP').val(), $('#txtDateToMUP').val(), result.d);
            //packResult(result.d);
            //var data = JSON.parse(result.d);
            //console.log(data);
            var barColorsArray = ['#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00'];
            //var barColorsArray = ['#00a65a', '#014421', '#008631', '#00ab41', '#00c04b', '#1fd655', '#39e75f', '#5ced73', '#83f28f', '#abf7b1', '#cefad0'];
            var colorIndex = 0;
            $("#bar-chart1").empty().off('click');
            new Morris.Bar({
                element: 'bar-chart1',
                resize: true,
                data: JSON.parse(result.d),
                //barColors: ['#00a65a'],
                hoverCallback: function (index, options, content, row) {
                    return (content);
                },
                xkey: 'ItemCode',
                ykeys: ['Quantity', 'CriticalLevel'],
                labels: ['Quantity', 'CriticalLevel'],
                xLabelAngle: 60,
                hideHover: 'auto',
                xLabelMargin: 10, padding: 60,
                barColors: function () {
                    if (colorIndex < 10)
                        return barColorsArray[++colorIndex];
                    else {
                        colorIndex = 0;
                        return barColorsArray[++colorIndex];
                    }
                },
            })
                .on('click', function (i, row) {
                    //load_data_Property();
                    stat1 = row.ItemCode;
                    StocksUnderMinLevelModal(stat1);
                    $("#SUMLModal").modal("show");
                });
                
        },
        error: function (e) {
        }
    });
}

var itemcode1 = '';
var label = '';
function PISLGraphBar() {
    var dateStart = $('#txtDateFrom').val();
    var dateEnd = $('#txtDateTo').val();

    //alert(dateStart);
    //alert(dateEnd)

    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphBarPISL",
        data: "{ dateStart:'" + dateStart + "', dateEnd:'" + dateEnd + "', label:'" + label + "' , isdet:'display'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //packResult($('#txtDateFromMUP').val(), $('#txtDateToMUP').val(), result.d);
            //packResult(result.d);
            var data = JSON.parse(result.d);
            //console.log(data);
            //alert(result.d);
            //var barColorsArray = ['#00a65a', '#014421', '#008631', '#00ab41', '#00c04b', '#1fd655', '#39e75f', '#5ced73', '#83f28f', '#abf7b1', '#cefad0'];
            var barColorsArray = ['#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00'];
            var colorIndex = 0;
            $("#PISLbarchart").empty();
            new Morris.Bar({
                element: 'PISLbarchart',
                resize: true,
                data: JSON.parse(result.d),
                //barColors: ['#00a65a'],
                hoverCallback: function (index, options, content, row) {
                    return (content);
                },
                xkey: 'label',
                ykeys: ['value'],
                labels: ['Count'],
                xLabelAngle: 60,
                hideHover: 'auto',
                xLabelMargin: 10, padding: 60,
                barColors: function () {
                    if (colorIndex < 10)
                        return barColorsArray[++colorIndex];
                    else {
                        colorIndex = 0;
                        return barColorsArray[++colorIndex];
                    }
                },
            })
                .on('click', function (i, row) {
                    //stock = row.ItemCode;
                    //PropertyImportantSchedbarModal(row.label);
                    //$("#PISLDonnutModal").modal("show");

                    label = row.label;
                    PropertyImportantSchedpieModal(label);
                    $("#PISLDonnutModal").modal("show");
                })
                .off('click');
        },
        error: function (e) {
            //alert(e);
            //console.log(e);
        }//
    });
}

var stat = '';
var label = '';
function PISLdonutchart() {
    var dateStart = $('#txtDateFrom').val();
    var dateEnd = $('#txtDateTo').val();

    $('#PISLlegend').empty();
    //alert('1');
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphDonnutPISL",
        //url: "DashBoard.aspx/PropertyImportantSched",
        data: "{ dateStart:'" + dateStart + "', dateEnd:'" + dateEnd + "', label:'" + label + "' , isdet:'display'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //alert(result.d);
            var donut =
                Morris.Donut({
                    element: 'DonutPropertyImportantSched',
                    resize: true,
                    colors: ["#3c8dbc", "#f56954", "#00a65a", "#00aa00", "#ffaa00"],
                    data: JSON.parse(result.d),

                    hideHover: 'auto',
                }).on('click', function (i, row) {
                    label = row.label;
                    PropertyImportantSchedpieModal(label);
                    $("#PISLDonnutModal").modal("show");
                });
            donut.options.data.forEach(function (label, i) {
                //$('#legend').empty();
                var legendItem = $('<a><span onclick="clicklegend(' + label['label'] + ')"></span></a>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                legendItem.find('span')
                    .css('backgroundColor', donut.options.colors[i])
                    .css('width', '20px')
                    .css('display', 'inline-block')
                    .css('margin', '5px');
                $('#PISLlegend').append(legendItem);
            });
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function clicklegend(PISL) {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarPISL",
        data: "{search: '" + PISL + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#PISLLlistdonnut').hide();
            $('#PISLLlistdonnut').innerHTML = "";
            $('#PISLLlistdonnut').fadeIn(200);

            myJsonData = data;

            populateDataTablePS(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });

    $("#PILSModal").modal("show");
}

//table
function PropertyImportantSchedModal(label) {
    var dateStart = $('#txtDateFrom').val();
    var dateEnd = $('#txtDateTo').val();
    var type = $('#pislwidgettype').val();
    //var dateStart = '10/04/2022';
    //var dateEnd = '04/28/2023';

    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/PropertyImportantSched",//LoadGraphDonnutPISL
        data: "{dateStart:'" + dateStart + "', dateEnd:'" + dateEnd + "', label:'" + label + "' , isdet: 'details'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;
            populateDataTablePISModal(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            //alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

//////////////////bar/////////////////
function PropertyImportantSchedbarModal(label) {
    var dateStart = $('#txtDateFrom').val();
    var dateEnd = $('#txtDateTo').val();
    var type = $('#pislwidgettype').val();
    //var dateStart = '10/04/2022';
    //var dateEnd = '04/28/2023';

    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphBarPISL",//LoadGraphDonnutPISL
        data: "{dateStart:'" + dateStart + "', dateEnd:'" + dateEnd + "', label:'" + label + "' , isdet:'details'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;
            populateDataTablePISModal(myJsonData);

            //console.log(data);
            // console.log('Success');
        },
        error: function (e) {
            //alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}
///////////////////////////////pie//////////////////////
function PropertyImportantSchedpieModal(label) {
    var dateStart = $('#txtDateFrom').val();
    var dateEnd = $('#txtDateTo').val();
    var type = $('#pislwidgettype').val();
    //var dateStart = '10/04/2022';
    //var dateEnd = '04/28/2023';

    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphDonnutPISL",
        data: "{dateStart:'" + dateStart + "', dateEnd:'" + dateEnd + "', label:'" + label + "' , isdet:'details'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;
            populateDataTablePISModal(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            //alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePISModal(data) {
    try {
        $('#PISLDonnutModaltbl').DataTable({
            "destroy": true,
            "processing": true,
            "scrollX": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "data": JSON.parse(data.d),
            "columns": [

                { "data": "DATE" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "ACTIVITY" }

            ],
            dom: 'Bftip',
            buttons: {
                buttons: [
                    {
                        extend: 'collection',
                        text: '...',
                        buttons: [
                            {
                                extend: 'copy',
                                text: 'COPY',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Important Schedule'
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

        $('#PISLtblModal').off('click');
    } catch (e) {
        var table = $('#PISLtblModal').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_TMP() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarPropertyTbl1",
        data: "{itemcode: '" + itemcodePtop10 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlisttop10modal').hide();
            $('#assetlisttop10modal').innerHTML = "";
            $('#assetlisttop10modal').fadeIn(200);

            myJsonData = data;
            populateDataTableTMP(myJsonData);
            console.log(data);
            console.log('Success');
        },
        error: function (e) {
        }
    });
}

function populateDataTableTMP(data) {
    try {
        $('#tblAssetListTop10Modal').DataTable({
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
                { "data": "ItemCode" },
                //{ "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "QuantityString" }
            ],
            dom: 'Brftip', colReorder: true
            , stateSave: true,

            buttons: {
                buttons: [
                    {
                        extend: 'collection',
                        text: '...',
                        buttons: [
                            {
                                extend: 'copy',
                                text: 'COPY',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
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
            }
            , colReorder: true
            , stateSave: true
        });

        $('#tblAssetListTop10Modal').off('click');
    } catch (e) {
        var table = $('#tblAssetListTop10Modal').DataTable();
        table
            .clear()
            .draw();
    }
}

/////////////////////////////////////////////////////
//////////////////  STATUS  /////////////////////////
/////////////////////////////////////////////////////
var stat = '';
var itemcode1 = '';
function StatusGraphBar() {
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphDonnut",
        //url: "DashBoard.aspx/LoadGraphBarSUML",
        //data: "{ startdate: '" + $('#txtDateFromMUP').val() + "', enddate: '" + $('#txtDateToMUP').val() + "' }",
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //packResult($('#txtDateFromMUP').val(), $('#txtDateToMUP').val(), result.d);
            //packResult(result.d);
            //var data = JSON.parse(result.d);
            //console.log(data);

            var barColorsArray = ['#000000', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00', '#000000', '#3c8dbc', '#f56954', '#00a65a', '#00aa00', '#ffaa00'];
            //var barColorsArray = ['#00a65a', '#014421', '#008631', '#00ab41', '#00c04b', '#1fd655', '#39e75f', '#5ced73', '#83f28f', '#abf7b1', '#cefad0'];
            var colorIndex = 0;
            $("#StatusBarChart1").empty();
            new Morris.Bar({
                element: 'StatusBarChart1',
                resize: true,
                data: JSON.parse(result.d),
                //barColors: ['#00a65a'],
                hoverCallback: function (index, options, content, row) {
                    return (content);
                },
                xkey: 'label',
                ykeys: ['value'],
                labels: ['Count'],
                xLabelAngle: 60,
                hideHover: 'auto',
                xLabelMargin: 10, padding: 60,
                barColors:
                    function () {
                        if (colorIndex < 10)
                            return barColorsArray[++colorIndex];
                        else {
                            colorIndex = 0;
                            return barColorsArray[++colorIndex];
                        }
                    },
            })
                .on('click', function (i, row) {
                    stat = row.label;
                    //alert(stat);
                    load_data_PS();
                    $("#DonnutModal").modal("show");
                })
                .off('click');
        },
        error: function (e) {
        }
    });
}

function StatusWidgetType() {
    var type = $('#ddlStatusType').val();
    //alert(type);
    if (type == '1') {
        $('#StatusBody').show();
        $('#StatusBarChart').hide();
        $('#StatusDonut').hide();
        load_Status_tbl();
    }
    else if (type == '2') {
        $('#StatusBody').hide();
        $('#StatusBarChart').show();
        $('#StatusDonut').hide();
        StatusGraphBar();
    }
    else if (type == '3') {
        $('#StatusBody').hide();
        $('#StatusBarChart').hide();
        $('#StatusDonut').show();
        modalPropertyStatusChartDonnut();
    }
    else {
        $('#StatusBody').show();
        $('#StatusBarChart').hide();
        $('#StatusDonut').hide();
        load_Status_tbl();
    }
}

//////////////////////////////////////////////////////
///////////    TOP 10 MOST USED ITEM      ///////////
/////////////////////////////////////////////////////

//// PROPERTY
var itemcode1 = '';
var itemcodePtop10 = '';
function top10MostUsedProperty() {
    //$('#txtDateFromMUP').val()
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphBarProperty",
        data: "{}",
        //data: "{ startdate: '" + $('#txtDateFromMUP').val() + "', enddate: '" + $('#txtDateToMUP').val() + "' }",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //packResult($('#txtDateFromMUP').val(), $('#txtDateToMUP').val(), result.d);
            //packResult(result.d);
            //console.log(result.d);
            var barColorsArray = ['#00a65a', '#014421', '#008631', '#00ab41', '#00c04b', '#1fd655', '#39e75f', '#5ced73', '#83f28f', '#abf7b1', '#cefad0'];
            var colorIndex = 0;
            $("#bar-chart-Property").empty().off('click');
            new Morris.Bar({
                element: 'bar-chart-Property',
                resize: true,
                data: JSON.parse(result.d),
                //barColors: ['#00a65a'],
                xkey: 'ItemCode',
                ykeys: ['value'],
                labels: ['Count'],
                hideHover: 'auto',
                xLabelAngle: 60,
                xLabelMargin: 10, padding: 60,
                barColors: function () {
                    if (colorIndex < 10)
                        return barColorsArray[++colorIndex];
                    else {
                        colorIndex = 0;
                        return barColorsArray[++colorIndex];
                    }
                },
            })
                 .on('click', function (i, row) {
                     if (stockorprop == "1") {
                         itemcode1 = row.ItemCode;
                         itemcodePtop10 = row.ItemCode;
                         //alert('property');
                         load_data_TMP();

                         $("#top10Modal").modal("show");
                     }
                 });
        },
        error: function (e) {
        }
    });
}

//STOCK

var itemcodestock = '';
var itemcodestockT10 = '';
function top10MostUsedStock() {
    $.ajax({
        type: 'POST',
        url: "Home/DashBoard.aspx/LoadGraphBarStock",
        //url: "DashBoard.aspx/LoadGraphBarSUML",
        //data: "{ startdate: '" + $('#txtDateFromMUP').val() + "', enddate: '" + $('#txtDateToMUP').val() + "' }",
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //packResult($('#txtDateFromMUP').val(), $('#txtDateToMUP').val(), result.d);
            //packResult(result.d);
            //var data = JSON.parse(result.d);
            //console.log(data);

            var barColorsArray = ['#00a65a', '#014421', '#008631', '#00ab41', '#00c04b', '#1fd655', '#39e75f', '#5ced73', '#83f28f', '#abf7b1', '#cefad0'];
            var colorIndex = 0;
            $("#bar-chart-stock").empty().off('click');;
            new Morris.Bar({
                element: 'bar-chart-stock',
                resize: true,
                data: JSON.parse(result.d),
                //barColors: ['#00a65a'],
                hoverCallback: function (index, options, content, row) {
                    return (content);
                },
                xkey: 'ItemCode',
                ykeys: ['value'],
                labels: ['Count'],
                xLabelAngle: 60,
                hideHover: 'auto',
                xLabelMargin: 10, padding: 60,
                barColors: function () {
                    if (colorIndex < 10)
                        return barColorsArray[++colorIndex];
                    else {
                        colorIndex = 0;
                        return barColorsArray[++colorIndex];
                    }
                },
            })
               .on('click', function (i, row) {
                    if (stockorprop == "2") {
                        //itemcodestock = row.ItemCode;
                        itemcodestockT10 = row.ItemCode;
                       // alert(itemcodestockT10);
                        load_data_TMS();
                        $("#top10Modalstock").modal("show");
                    }
                });
               
        },
        error: function (e) {
        }
    });
}

function load_data_TMS() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarStocktbl1",
        data: "{itemcode: '" + itemcodestockT10 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlisttop10modalstock').hide();
            $('#assetlisttop10modalstock').innerHTML = "";
            $('#assetlisttop10modalstock').fadeIn(200);

            myJsonData = data;
            populateDataTableTMS(myJsonData);
            console.log(data);
            console.log('Success');
        },
        error: function (e) {
        }
    });
}

function populateDataTableTMS(data) {
    try {
        $('#tblAssetListTop10Modalstock').DataTable({
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
                { "data": "ItemCode" },
                //{ "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "QuantityString" }
            ],
            dom: 'Brftip', colReorder: true
            , stateSave: true,

            buttons: {
                buttons: [
                    {
                        extend: 'collection',
                        text: '...',
                        buttons: [
                            {
                                extend: 'copy',
                                text: 'COPY',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Supply'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Supply'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Supply'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Supply'
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
            }
        });

        $('#tblAssetListTop10Modalstock').off('click');
    } catch (e) {
        var table = $('#tblAssetListTop10Modalstock').DataTable();
        table
            .clear()
            .draw();
    }
}

/////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////
function load_data_Status() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarStocktbl",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;

            PopulateDataTableStock1(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
        }
    });
}

function load_data_Property1() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarPropertyTbl",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;

            PopulateDataTableProperty1(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
        }
    });
}

////////top10property table TEST/////////////
function PopulateDataTableProperty1(data) {
    try {
        $('#tblAssetListTop10').DataTable({
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" }
            ],
            dom: 'Bftip',
            stateSave: true,

            buttons: {
                buttons: [
                    {
                        extend: 'collection',
                        text: '...',
                        buttons: [
                            {
                                extend: 'copy',
                                text: 'COPY',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
                            },
                            {
                                extend: 'csv',
                                text: 'CSV',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
                            },
                            {
                                extend: 'print',
                                text: 'PRINT',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Most Used Property'
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
            }
        });

        $('#tblAssetListTop10').off('click');
    }
    catch (e) {
        var table = $('#tblAssetListTop10').DataTable();
        table
            .clear()
            .draw();
    }
}
//////////////////////////////////////////////////////////////////

//////////////top10stocktable//////////////

function load_data_Property1() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarPropertyTbl",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;

            PopulateDataTableStock1(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
        }
    });
}

function PopulateDataTableStock1(data) {
    try {
        $('#tblAssetListTop10').DataTable({
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
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

        $('#tblAssetListTop10').off('click');
    }
    catch (e) {
        var table = $('#tblAssetListTop10').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_Stock1() {
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/LoadGraphBarStocktbl",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsonData = data;

            PopulateDataTableProperty1(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
        }
    });
}

function PopulateDataTableProperty1(data) {
    try {
        $('#tblStockListTop10').DataTable({
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Top 10 Most used Property'
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

        $('#tblStockListTop10').off('click');
    }
    catch (e) {
        var table = $('#tblStockListTop10').DataTable();
        table
            .clear()
            .draw();
    }
}

function StockOrProperty() {
    var type = $('#ddlTop10').val();
    if (type == '1') {
        $('#Property_body').show();
        $('#Stock_body').hide();
        load_data_Property1();
    }
    else if (type == '2') {
        $('#Property_body').hide();
        $('#Stock_body').show();
        load_data_Stock1();
    }
    else {
        $('#Property_body').hide();
        $('#Stock_body').hide();
        load_data_Property1();
    }
}

function PropertyWidgetType() {
    var type = $('#ddlTop10WidgetTypeProperty').val()
    if (type == '1') {
        $('#tblAssetListBodytop10').show();
        $('#barAssetListBodytop10').hide();
        top10MostUsedProperty();
    }
    else if (type == '2') {
        $('#tblAssetListBodytop10').hide();
        $('#barAssetListBodytop10').show();
        top10MostUsedProperty();
    }
    else {
    }
}

function StockWidgetType() {
    var type = $('#ddlTop10WidgetTypeStock').val()
    if (type == '1') {
        $('#tblStockListBodytop10').show();
        $('#barStockListBodytop10').hide();
        top10MostUsedStock();
    }
    else if (type == '2') {
        $('#tblStockListBodytop10').hide();
        $('#barStockListBodytop10').show();
        top10MostUsedStock();
    }
    else {
    }
}

function PISLWidgetType() {
    var type = $('#pislwidgettype').val();
    if (type == '1') {
        $('#PISLtablebody').show();
        $('#PISLbarchart').hide();
        $('#PropImpSchedBody').hide();
        PropertyImportantSched();
    }
    else if (type == '2') {
        $('#PISLtablebody').hide();
        $('#PISLbarchart').show();
        $('#PropImpSchedBody').hide();
        PISLGraphBar();
    }
    else if (type == '3') {
        $('#PISLtablebody').hide();
        $('#PISLbarchart').hide();
        $('#PropImpSchedBody').show();
        PISLdonutchart();
    }
    else {
    }
}

function TestExcel()
{
    $.ajax({
        type: "POST",
        url: "Home/Dashboard.aspx/btnTest_Click",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {

            var bytes = Base64ToBytes(msg.d);

            var blob = new Blob([bytes], { type: "application/octetstream" });

            //Check the Browser type and download the File.
            var isIE = false || !!document.documentMode;
            if (isIE) {
                window.navigator.msSaveBlob(blob, fileName);
            } else {
                var url = window.URL || window.webkitURL;
                link = url.createObjectURL(blob);
                var a = $("<a />");
                a.attr("download", "sample.xls");
                a.attr("href", link);
                $("body").append(a);
                a[0].click();
                $("body").remove(a);

            }

        }, error: function (xhr, status, error) {
            //SweetAlertError("An error occured.");
            //ClearSignature();
        }
    });
}
function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};
