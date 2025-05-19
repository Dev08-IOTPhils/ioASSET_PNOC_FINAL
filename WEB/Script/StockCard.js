$(function () {
    $('.select2').select2();
    var StockListArray = new Array();
    var StockCardArray = new Array();
    $(document).ready(function () {
        GetCompanyInfo();
        GetStockList();
        load_Filter();
    });
    function GetStockList() {
        tblStockList.clear().draw();
        $.ajax({
            type: 'POST',
            url: "Views/StockCard.aspx/GetStockList",
            data: "{ }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                StockListArray = JSON.parse(data.d);
                StockListArray.forEach((mystock, index) => {
                    tblStockList.row.add([mystock.ItemCode, mystock.Description, mystock.ClassificationName, mystock.CategoryName, mystock.BrandName, mystock.ModelName, '<center><a rowindex="' + index + '" stockid="' + mystock.StockID + '" class="btn btn-xs btn-primary btn_View" data-toggle="tooltip" title="View Details"><span class="fa fa-eye"></span> </a>&nbsp;&nbsp;<a rowindex="' + index + '" stockid="' + mystock.StockID + '" class="btn btn-xs btn-primary btn_Viewxcl" data-toggle="tooltip" title="Download Excel"><span class="fa fa-download"></span></a></center>']);
                });
                tblStockList.draw();
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    $(document).on('click', '.btn_View', function () {
        var Item = $(this);
        var stockid = $(this).attr('stockid');
        var rowindex = $(this).attr('rowindex');

        $.ajax({
            type: 'POST',
            url: "Views/StockCard.aspx/GetStockCardV2",
            data: "{  StockID: '" + stockid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                StockCardArray = JSON.parse(data.d);

                GenerateSTOCKCARD(stockid, rowindex);
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    });

    $(document).on('click', '.btn_Viewxcl', function () {
        var Item = $(this);
        var stockid = $(this).attr('stockid');
        var rowindex = $(this).attr('rowindex');

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("{ key1: '" + 'SC' + "', key2: '" + stockid + "', key3: '" + '' + "', key4: '" + StockListArray[rowindex].Description + '|' + StockListArray[rowindex].ItemCode + '|' + StockListArray[rowindex].CriticalLevel + '|' + StockListArray[rowindex].UnitOfMeasurement + "', key5: '" + '' + "'}");

        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'SC' + "', key2: '" + stockid + "', key3: '" + '' + "', key4: '" + StockListArray[rowindex].Description + '|' + StockListArray[rowindex].ItemCode + '|' + StockListArray[rowindex].CriticalLevel + '|' + StockListArray[rowindex].UnitOfMeasurement + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert(msg.d);
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
                    a.attr("download", "SC_" + formattedDateTime + ".xls");
                    a.attr("href", link);
                    $("body").append(a);
                    a[0].click();
                    $("body").remove(a);

                }
            },
            error: function (xhr, status, error) {
                console.log(xhr);
            }
        });


        //$.ajax({
        //    type: 'POST',
        //    url: "Views/StockCard.aspx/GetStockCardV2",
        //    data: "{  StockID: '" + stockid + "'}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    success: function (data) {
        //        StockCardArray = JSON.parse(data.d);

        //        GenerateSTOCKCARD(stockid, rowindex);
        //    },
        //    error: function (e) {
        //        alert(e);
        //        console.log("There was an error with your request...");
        //        console.log("error: " + JSON.stringify(e));
        //    }
        //});
    });
    var tblStockList = $('#tblStockList').DataTable({
        'oLanguage': {
            'sSearch': ''
        }, dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        scrollX: true,
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
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
            , stateSave: true,
        'responsive': true,
    });

    var tblLOAList = $('#tblLOAList').DataTable({
        'oLanguage': {
            'sSearch': ''
        },
        'columnDefs': [
            { 'width': 65, 'targets': 6 }
        ],
        'responsive': true,
        'scrollCollapse': true,
        'paging': false,
        'bInfo': false,
        'bPaginate': false,
        'dom': 'lrtip',// remove search bar
        'language': { 'searchPlaceholder': "Type here", "search": '<i class="fa fa-search"></i>' },
        'bFilter': true,
        'autoWidth': false,
        'responsive': true,
        'bLengthChange': false, //remove show entries
        'pageLength': 10, //show # of rows
        'columnDefs': [{
            'orderable': false,
            'targets': '_all'
        }]
    });

    function RecreateTable() {
        tblLOAList = $('#tblLOAList').DataTable({
            'oLanguage': {
                'sSearch': ''
            },
            'columnDefs': [
                { 'width': 65, 'targets': 6 }
            ],
            'ordering': false,
            'responsive': true,
            'scrollCollapse': true,
            'paging': false,
            'bInfo': false,
            'bPaginate': false,
            'dom': 'lrtip',// remove search bar
            'language': { 'searchPlaceholder': "Type here", "search": '<i class="fa fa-search"></i>' },
            'bFilter': true,
            'autoWidth': false,
            'responsive': true,
            'bLengthChange': false, //remove show entries
            'pageLength': 10, //show # of rows
            'columnDefs': [{
                'orderable': false,
                'targets': '_all',
                'className': 'dt-center'
            }]
        });
    }

    function parseJsonDate(jsonDateString) {
        return new Date(parseInt(jsonDateString.replace('/Date(', '')));
    }

    function ConvertStringToDateInputFormatMMMddyyyy(sVal) {
        if (sVal == null || sVal == '') {
            return "";
        }
        var now = new Date(parseJsonDate(sVal));
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var xxxxx = (day) + "-" + getMonthName(month) + "-" + now.getFullYear();
        return (xxxxx);
    }

    function getMonthName(currentmonth) {
        const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        return months[(currentmonth - 1)];
    }

    function convertYear(sval) {
        if (sval == null || sval == '') {
            return "";
        } else {
            var now = new Date(parseJsonDate(sval));
            return now.getFullYear();
        }
    }

    function convertMonth(sval) {
        if (sval == null || sval == '') {
            return "";
        } else {
            var now = new Date(parseJsonDate(sval));
            return now.getMonth();
        }
    }

    function checkIfNull(sVal) {
        if (sVal == null) {
            return "";
        }
        else {
            return sVal;
        }
    }

    function getWholeMonthName(currentmonth) {
        const months = ['JANUARY', 'FEBRUARY', 'MARCH', 'APRIL', 'MAY', 'JUNE', 'JULY', 'AUGUST', 'SEPTEMBER', 'OCTOBER', 'NOVEMBER', 'DECEMBER'];
        return months[(currentmonth - 1)];
    }

    function convertStringToInt(n) {
        return parseInt(n);
    }

    function numberWithCommas(n) {
        if (n == null) {
            return "0.00";
        }
        else if (isNaN(n)) {
            return "0.00";
        }
        return n.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
    }

    function ParseMeInt(sVal) {
        try {
            if (sVal == null) {
                return 0;
            }
            else return parseInt(sVal);
        } catch (err) {
            return 0;
        }
    }

    function GenerateSTOCKCARD(stockid, rowindex) {
        var html =
            '<p style="font-style:italic;float:right;margin-top:-10px;">Appendix 58</p><br />'
       + "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr></table>"
           + '<center><p style="font-size: 30px;">SUPPLY CARD<br />';
        html += '<table style="width:100%;"><tr><td><p style="font-size: 10px;">Entity Name: <u>' + CompanyInfo + '</u></p></td>';
        html += '<td style="text-align:right;"><p>Fund Cluster: <u>______________</u></p></td></tr></table>';
        html += '<table style="width: 100%;border-collapse: collapse;">';
        html += '<thead class="info">';
        html += '<tr style="background-color: #D9EDF7;">';
        html += '<th style="border: 1px solid black;text-align:left;">Item:</th>';
        html += '<th colspan="5" style="border: 1px solid black;text-align:left;">' + StockListArray[rowindex].Description + '</th>';
        html += '<th style="border: 1px solid black;text-align:left;">Supply No.: ' + StockListArray[rowindex].ItemCode + '</th>';
        html += '</tr>';
        html += '<tr style="background-color: #D9EDF7;">';
        html += '<th style="border: 1px solid black;text-align:left;">Description:</th>';
        html += '<th colspan="5" style="border: 1px solid black;text-align:left;">' + StockListArray[rowindex].Description + '</th>';
        html += '<th colspan="1" style="border: 1px solid black;text-align:left;">Re-order Point: ' + StockListArray[rowindex].CriticalLevel + '</th>';
        html += '</tr>';
        html += '<tr style="background-color: #D9EDF7;">';
        html += '<th colspan="6" style="border: 1px solid black;text-align:left;">Unit of Measurement: ' + StockListArray[rowindex].UnitOfMeasurement + '</th>';

        html += '<th style="border: 1px solid black;"></th>';
        html += '</tr>';
        html += '<tr style="background-color: #D9EDF7;">';
        html += '<th rowspan="2" style="text-align:center;vertical-align:middle;border: 1px solid black;">Date</th>';
        html += '<th rowspan="2" style="text-align:center;vertical-align:middle;border: 1px solid black;">Reference</th>';
        html += '<th colspan="1" style="border: 1px solid black;"><center>Receipt</center></th>';
        html += '<th colspan="2" style="border: 1px solid black;"><center>Issue</center></th>';
        html += '<th style="text-align:center;border: 1px solid black;">Balance</th>';
        html += '<th rowspan="2" style="text-align:center;vertical-align:middle;border: 1px solid black;">No. of Days to Consume</th>';
        html += '</tr>';
        html += '<tr style="background-color: #D9EDF7;">';
        html += '<th style="text-align:center;border: 1px solid black;">Qty</th>';
        html += '<th style="text-align:center;border: 1px solid black;">Qty</th>';
        html += '<th style="text-align:center;border: 1px solid black;">Group</th>';
        html += '<th style="text-align:center;border: 1px solid black;">Qty</th>';
        html += '<th></th>';
        html += '</tr>';

        html += '</thead>';

        var balanceqty = 0;
        StockCardArray.forEach((mystock, index) => {
            if (mystock.AddOrMinus == 'Add') {
                balanceqty += (mystock.ReceiptQty + mystock.IssueQty);
            } else {
                balanceqty -= (mystock.ReceiptQty + mystock.IssueQty);
            }

            html += "<tr>";
            html += "<td style='text-align:center;border:1px solid black;'>" + ConvertStringToDateInputFormatMMMddyyyy(mystock.CreatedDate) + "</td>";
            html += "<td style='text-align:center;border:1px solid black;'>" + mystock.TransNum + "</td>";
            html += "<td style='text-align:center;border:1px solid black;'>" + checkIfNull(mystock.ReceiptQty) + "</td>";
            html += "<td style='text-align:center;border:1px solid black;'>" + checkIfNull(mystock.IssueQty) + "</td>";
            html += "<td style='text-align:center;border:1px solid black;'>" + checkIfNull(mystock.IssueOffice) + "</td>";
            html += "<td style='text-align:center;border:1px solid black;'>" + checkIfNull(balanceqty) + "</td>";
            html += "<td style='text-align:center;border:1px solid black;'></td>";
            html += "</tr>";
        });

        html += '<tr><td colspan="7" style="text-align:center;border:1px solid black;font-weight: bold;font-style: italic;">***Nothing Follows***</td></tr>';
        html += '</table>';

        var divStockCard = $('#BodyHolder_divStockCard');
        divStockCard.html(html);
        PrintStockCard();
    }
});
function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};
var CompanyInfo = '';

function GetCompanyInfo() {
    $.ajax({
        type: 'POST',
        url: "Views/StockCard.aspx/GetCompanyInfo",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            CompanyInfo = msg.d;
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
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
        data: "{  ReportName: '" + "StockCard" + "'}",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Card'
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
                            // load_data_inventorylist();
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
                                // load_data_inventorylist();
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
                    //load_data_inventorylist();
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
                    //  load_data_inventorylist();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}