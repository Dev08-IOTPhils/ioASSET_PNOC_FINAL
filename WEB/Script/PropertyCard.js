var url = "";

$(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    var PropertyListArray = new Array();

    var PropertyCardArray = new Array();

    var tblItemCodeList = $('#tblItemCodeList').DataTable({
        'oLanguage': {
            'Search': ''
        },
        'responsive': true,
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
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
    var tblProperty = $('#tblProperty').DataTable({
        'oLanguage': {
            'sSearch': ''
        },
        'responsive': true,
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
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

    var tblPropertyList = $('#tblPropertyList').DataTable({
        'oLanguage': {
            'sSearch': ''
        },
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
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
            'targets': '_all',
            'className': 'dt-center'
        }]
    });

    $(document).ready(function () {
        GetCompanyInfo();
        GetPropertyList();
        load_Filter();
    });

    $('#cbType').on('change', function () {
        GetPropertyList();
        load_Filter();
    });

    function GetPropertyList() {
        var cbProperty = $('#cbProperty');
        cbProperty.empty();

        var typeid = $('#cbType').val();

        if (typeid == 0) {
            $('#drpFilterBy').empty();
            $("#drpFilterBy").append("<option value=' Select Filter '> Select Filter </option>");
            $("#drpFilterBy").append("<option value='ItemCode'>ItemCode</option>");
            $("#drpFilterBy").append("<option value='Description'>Description</option>");
            $("#drpFilterBy").append("<option value='ClassificationName'>ClassificationName</option>");
            $("#drpFilterBy").append("<option value='CategoryName'>CategoryName</option>");
            $("#drpFilterBy").append("<option value='BrandName'>BrandName</option>");
            $("#drpFilterBy").append("<option value='ModelName'>ModelName</option>");
        }
        else if (typeid == 1) {
            $('#drpFilterBy').empty();
            $("#drpFilterBy").append("<option value=' Select Filter '> Select Filter </option>");
            $("#drpFilterBy").append("<option value='ItemCode'>ItemCode</option>");
            $("#drpFilterBy").append("<option value='PropertyNumber'>PropertyNumber</option>");
            $("#drpFilterBy").append("<option value='LongDescription'>LongDescription</option>");
            $("#drpFilterBy").append("<option value='ClassificationName'>ClassificationName</option>");
            $("#drpFilterBy").append("<option value='CategoryName'>CategoryName</option>");
            $("#drpFilterBy").append("<option value='BrandName'>BrandName</option>");
            $("#drpFilterBy").append("<option value='ModelName'>ModelName</option>");
        }
        else {
            $('#drpFilterBy').empty();
            $("#drpFilterBy").append("<option value=' Select Filter '> Select Filter </option>");
            $("#drpFilterBy").append("<option value='ItemCode'>ItemCode</option>");
            $("#drpFilterBy").append("<option value='PropertyNumber'>PropertyNumber</option>");
            $("#drpFilterBy").append("<option value='LongDescription'>LongDescription</option>");
            $("#drpFilterBy").append("<option value='ClassificationName'>ClassificationName</option>");
            $("#drpFilterBy").append("<option value='CategoryName'>CategoryName</option>");
            $("#drpFilterBy").append("<option value='BrandName'>BrandName</option>");
            $("#drpFilterBy").append("<option value='ModelName'>ModelName</option>");
        }

        if (typeid == 0) {
            $('#tbllist1').css("display", "block");
            $('#tbllist2').css("display", "none");
        } else {
            $('#tbllist1').css("display", "none");
            $('#tbllist2').css("display", "block");
        }

        tblItemCodeList.clear().draw();
        tblProperty.clear().draw();

        $.ajax({
            type: 'POST',
            url: url + "/PropertyCard.aspx/GetPropertyList",
            data: "{ type: '" + typeid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                PropertyListArray = JSON.parse(data.d);
                console.log(PropertyListArray);
                PropertyListArray.forEach((myprop, index) => {
                    if (typeid == 0) {
                        tblItemCodeList.row.add([myprop.ItemCode, myprop.Description, myprop.ClassificationName, myprop.CategoryName, myprop.BrandName, myprop.ModelName, '<a rowindex="' + index + '" propertyid="' + myprop.PropertyID + '" class="btn btn-xs btn-primary btn_View_ItemCode_PAR" data-toggle="tooltip" title="View Details PAR"><span class="fa fa-eye"></span></a>&nbsp;&nbsp;<a rowindex="' + index + '" propertyid="' + myprop.PropertyID + '" class="btn btn-xs btn-primary btn_View_ItemCode_ICS" data-toggle="tooltip" title="View Details ICS"><span class="fa fa-eye"></span></a> &nbsp;&nbsp;<a rowindex="' + index + '" propertyid="' + myprop.PropertyID + '" class="btn btn-xs btn-primary btn_View_ItemCode_PARxcl" data-toggle="tooltip" title="Property Card Excel"><span class="fa fa-download"></span></a>&nbsp;&nbsp;<a rowindex="' + index + '" propertyid="' + myprop.PropertyID + '" class="btn btn-xs btn-primary btn_View_ItemCode_ICSxcl" data-toggle="tooltip" title="Semi-Expendable Property Card Excel"><span class="fa fa-download"></span></a>']);
                    } else if (typeid == 1) {
                        tblProperty.row.add([myprop.ItemCode, myprop.PropertyNumber, myprop.ClassificationName, myprop.CategoryName, myprop.BrandName, myprop.ModelName, '&nbsp;&nbsp;<a rowindex="' + index + '" recid="' + myprop.RecID + '"class="btn btn-xs btn-primary btn_View_ItemCode_PCxcl" data-toggle="tooltip" title="Property Card Excel"><span class="fa fa-download"></span></a>']);
                    } else if (typeid == 2) {
                        tblProperty.row.add([myprop.ItemCode, myprop.PropertyNumber, myprop.ClassificationName, myprop.CategoryName, myprop.BrandName, myprop.ModelName, '&nbsp;&nbsp;<a rowindex="' + index + '" recid="' + myprop.RecID + '"class="btn btn-xs btn-primary btn_View_ItemCode_ICS_xcl" data-toggle="tooltip" title="Semi-Expendable Property Card Excel"><span class="fa fa-download"></span></a>']);
                    } else {
                        tblProperty.row.add([myprop.ItemCode, myprop.PropertyNumber, myprop.ClassificationName, myprop.CategoryName, myprop.BrandName, myprop.ModelName, '']);
                    }
                });
                tblItemCodeList.draw();
                tblProperty.draw();
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }

    $(document).on('click', '.btn_View_ItemCode_ICSxcl', function () {
        var Item = $(this);
        var propertyid = $(this).attr('propertyid');
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
            data: "{ key1: '" + 'SPC' + "', key2: '" + propertyid + "', key3: '" + 'ICS' + "', key4: '" + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '|' + PropertyListArray[rowindex].ItemCode + '|' + PropertyListArray[rowindex].Description + "', key5: '" + 'ICS' + "'}",
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
                    a.attr("download", "SPC_" + formattedDateTime + ".xls");
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
    });

    $(document).on('click', '.btn_View_ItemCode_PARxcl', function () {
        var Item = $(this);
        var propertyid = $(this).attr('propertyid');
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
            data: "{ key1: '" + 'PC' + "', key2: '" + propertyid + "', key3: '" + 'PAR' + "', key4: '" + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '|' + PropertyListArray[rowindex].ItemCode + '|' + PropertyListArray[rowindex].LongDescription + "', key5: '" + 'PAR' + "'}",
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
                    a.attr("download", "PC_" + formattedDateTime + ".xls");
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
        //    url: url + "/PropertyCard.aspx/GetPropertyCardByItemCode",
        //    data: "{ PropertyID: '" + propertyid + "', TransType: 'PAR' }",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (data) {
        //        PropertyCardArray = JSON.parse(data.d);
        //        parloaddetailsbyitemcode(rowindex);
        //    },
        //    error: function (e) {
        //        alert(e);
        //        console.log("There was an error with your request...");
        //        console.log("error: " + JSON.stringify(e));
        //    }
        //});
    });

    $(document).on('click', '.btn_View_ItemCode_ICS_xcl', function () {
        //alert('hello');
        var Item = $(this);
        var recid = $(this).attr('recid');
        var propertyid = $(this).attr('propertyid');
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
        //alert("{ key1: '" + 'PC' + "', key2: '" + PropertyListArray[rowindex].PropertyNumber + "', key3: '" + 'PC' + "', key4: '" + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '|' + PropertyListArray[rowindex].ItemCode + '|' + PropertyListArray[rowindex].Description + "', key5: '" + 'PAR' + "'}");

        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'SPC' + "', key2: '" + PropertyListArray[rowindex].PropertyNumber + "', key3: '" + 'SPC' + "', key4: '" + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '|' + PropertyListArray[rowindex].ItemCode + '|' + PropertyListArray[rowindex].Description + "', key5: '" + 'ICS' + "'}",
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
                    a.attr("download", "SPC_" + formattedDateTime + ".xls");
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
    });

    $(document).on('click', '.btn_View_ItemCode_PCxcl', function () {
        var Item = $(this);
        var recid = $(this).attr('recid');
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
        //alert("{ key1: '" + 'PC' + "', key2: '" + PropertyListArray[rowindex].PropertyNumber + "', key3: '" + 'PC' + "', key4: '" + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '|' + PropertyListArray[rowindex].ItemCode + '|' + PropertyListArray[rowindex].Description + "', key5: '" + 'PAR' + "'}");

        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'PC' + "', key2: '" + PropertyListArray[rowindex].PropertyNumber + "', key3: '" + 'PC' + "', key4: '" + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '|' + PropertyListArray[rowindex].ItemCode + '|' + PropertyListArray[rowindex].LongDescription + '|' + PropertyListArray[rowindex].PropertyNumber + "', key5: '" + 'PAR' + "'}",
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
                    a.attr("download", "PC_" + formattedDateTime + ".xls");
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
        //    url: url + "/PropertyCard.aspx/GetPropertyCardByItemCode",
        //    data: "{ PropertyID: '" + propertyid + "', TransType: 'PAR' }",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (data) {
        //        PropertyCardArray = JSON.parse(data.d);
        //        parloaddetailsbyitemcode(rowindex);
        //    },
        //    error: function (e) {
        //        alert(e);
        //        console.log("There was an error with your request...");
        //        console.log("error: " + JSON.stringify(e));
        //    }
        //});
    });

    $(document).on('click', '.btn_View_ItemCode_PAR', function () {
        var Item = $(this);
        var propertyid = $(this).attr('propertyid');
        var rowindex = $(this).attr('rowindex');

        $.ajax({
            type: 'POST',
            url: url + "/PropertyCard.aspx/GetPropertyCardByItemCode",
            data: "{ PropertyID: '" + propertyid + "', TransType: 'PAR' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                PropertyCardArray = JSON.parse(data.d);
                parloaddetailsbyitemcode(rowindex);
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    });

    $(document).on('click', '.btn_View_ItemCode_ICS', function () {
        var Item = $(this);
        var propertyid = $(this).attr('propertyid');
        var rowindex = $(this).attr('rowindex');
        $.ajax({
            type: 'POST',
            url: url + "/PropertyCard.aspx/GetPropertyCardByItemCode",
            data: "{ PropertyID: '" + propertyid + "', TransType: 'ICS' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                PropertyCardArray = JSON.parse(data.d);
                icsloaddetailsbyitemcode(rowindex);
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    });

    $(document).on('click', '.btn_View_Property', function () {
        var Item = $(this);
        var recid = $(this).attr('recid');
        var rowindex = $(this).attr('rowindex');

        $.ajax({
            type: 'POST',
            url: url + "/PropertyCard.aspx/GetPropertyCard",
            data: "{ PropertyNumber: '" + PropertyListArray[rowindex].PropertyNumber + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                PropertyCardArray = JSON.parse(data.d);
                if ($('#cbType').val() == 1) {
                    parloaddetails(rowindex);
                }
                else if ($('#cbType').val() == 2) {
                    icsloaddetails(rowindex);
                }
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    });

    function numberWithCommas(n) {
        if (n == null) {
            return "0.00";
        }
        else if (isNaN(n)) {
            return "0.00";
        }
        return n.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
    }

    function parseJsonDate(jsonDateString) {
        return new Date(parseInt(jsonDateString.replace('/Date(', '')));
    }

    function getMonthName(currentmonth) {
        const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        return months[(currentmonth - 1)];
    }

    function getWholeMonthName(currentmonth) {
        const months = ['JANUARY', 'FEBRUARY', 'MARCH', 'APRIL', 'MAY', 'JUNE', 'JULY', 'AUGUST', 'SEPTEMBER', 'OCTOBER', 'NOVEMBER', 'DECEMBER'];
        return months[(currentmonth - 1)];
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

    $('#btnPrint').off('click').on('click', function () {
        PrintPropertyCard();
    });

    function icsloaddetails(rowindex) {
        var html = '';
        html += '<p style="font-style:italic;float:right;margin-top: -10px;">Annex A.1</p><br />';
        html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr></table>";
        html += '<center><p style="font-size: 25px;">SEMI-EXPENDABLE PROPERTY CARD</p></center>';
        html += '<table style="width: 100%;"><tr><td style="width: 50%;">Entity Name: <p style="font-size: 10px;"><u>' + CompanyInfo + '</u></p></td><td style="width: 50%;text-align:right;">Fund Cluster: <u>____________</u></td></tr></table>';
        html += '<table class="table table-striped table-bordered" style="width:100%;border-collapse: collapse;">';
        html += '<thead class="info">';
        html += '<tr">';
        html += '<th colspan="9" style="border: 1px solid black;text-align:left;">Semi-expendable Property :' + PropertyListArray[rowindex].ClassificationName + '/' + PropertyListArray[rowindex].CategoryName + '/' + PropertyListArray[rowindex].BrandName + '</th>';
        html += '<th colspan="2" rowspan="2" style="vertical-align: middle;border: 1px solid black;text-align:left;">Semi-expendable Property Number : ' + PropertyListArray[rowindex].PropertyNumber + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th colspan="9" style="border: 1px solid black;text-align:left;">Description : ' + PropertyListArray[rowindex].Description + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Date</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Reference</th>';

        html += '<th colspan="3" style="vertical-align: middle; text-align: center;border: 1px solid black;">Receipt</th>';
        html += '<th colspan="3" style="vertical-align: middle; text-align: center;border: 1px solid black;">Issue/Transfer/Disposal</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Balance</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Amount</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Remarks</th>';
        html += '</tr>';
        html += '<tr >';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;"></th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;"></th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Item No.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Group/Officer</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';

        var lblPropDescription = $('#lblPropDescription');
        var lblPropNum = $('#lblPropNum');
        var lblPropDetailedSpec = $('#lblPropDetailedSpec');

        lblPropDescription.html(PropertyListArray[rowindex].ItemCode);
        lblPropNum.html(PropertyListArray[rowindex].PropertyNumber);
        lblPropDetailedSpec.html(PropertyListArray[rowindex].LongDescription);

        var balanceqty = 0;

        var lastissuance = "";
        var haspar = false;
        PropertyCardArray.forEach((myprop, index) => {
            if (myprop.AddOrMinus == 'Add') {
                if (myprop.TransType == 'Update Property Number') {
                    balanceqty += 0;
                }
                else {
                    balanceqty += 1;
                }
            } else {
                if (myprop.TransType != 'Transfer') {
                    balanceqty -= myprop.IssueQty;
                }
            }

            if (myprop.TransType == 'Issuance') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Transfer') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Update Property Number') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Return') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            else if (!haspar) {
                lastissuance = myprop.TransNum;
            }

            html += '<tr>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + ConvertStringToDateInputFormatMMMddyyyy(myprop.CreatedDate) + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + lastissuance + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.ReceiptQty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black; width: 20px;"></td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black; width: 20px;"></td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + PropertyListArray[rowindex].ItemCode + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.IssueQty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.Custodian + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + balanceqty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + numberWithCommas(myprop.Amount) + '</td>';

            if (myprop.OfficeOfficer != '') {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.TransNum + '<br/>' + myprop.OfficeOfficer + '<br/>' + myprop.Remarks + '</td>';
            } else {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.TransNum + '<br/>' + myprop.Remarks + '</td>';
            }

            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        var divPropertyCard = $('#BodyHolder_divPropertyCard');
        divPropertyCard.html(html);
        PrintPropertyCard();
    }

    function icsloaddetailsbyitemcode(rowindex) {
        var html = '';
        html += '<p style="font-style:italic;float:right;margin-top: -10px;">Annex A.1</p><br />';

        html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr></table>";
        html += '<center><p style="font-size: 25px;">SEMI-EXPENDABLE PROPERTY CARD</p></center>';
        html += '<table style="width: 100%;"><tr><td style="width: 50%;">Entity Name: <p style="font-size: 10px;"><u>' + CompanyInfo + '</u></p></td><td style="width: 50%;text-align:right;">Fund Cluster: <u>____________</u></td></tr></table>';
        html += '<table class="table table-striped table-bordered" style="width:100%;border-collapse: collapse;">';
        html += '<thead class="info">';
        html += '<tr">';
        html += '<th colspan="9" style="border: 1px solid black;text-align:left;">Semi-expendable Property :' + PropertyListArray[rowindex].ClassificationName + '/' + PropertyListArray[rowindex].CategoryName + '/' + PropertyListArray[rowindex].BrandName + '</th>';
        html += '<th colspan="2" rowspan="2" style="vertical-align: middle;border: 1px solid black;text-align:left;">Semi-expendable Property Number : ' + PropertyListArray[rowindex].ItemCode + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th colspan="9" style="border: 1px solid black;text-align:left;">Description : ' + PropertyListArray[rowindex].Description + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Date</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Reference</th>';

        html += '<th colspan="3" style="vertical-align: middle; text-align: center;border: 1px solid black;">Receipt</th>';
        html += '<th colspan="3" style="vertical-align: middle; text-align: center;border: 1px solid black;">Issue/Transfer/Disposal</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Balance</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Amount</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Remarks</th>';
        html += '</tr>';
        html += '<tr >';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;"></th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;"></th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Item No.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Group/Officer</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';

        var lblPropDescription = $('#lblPropDescription');
        var lblPropNum = $('#lblPropNum');
        var lblPropDetailedSpec = $('#lblPropDetailedSpec');

        lblPropDescription.html(PropertyListArray[rowindex].ItemCode);
        lblPropNum.html(PropertyListArray[rowindex].PropertyNumber);
        lblPropDetailedSpec.html(PropertyListArray[rowindex].LongDescription);

        var balanceqty = 0;

        var lastissuance = "";
        var haspar = false;
        PropertyCardArray.forEach((myprop, index) => {
            if (myprop.AddOrMinus == 'Add') {
                if (myprop.TransType == 'Update Property Number') {
                    balanceqty += 0;
                }
                else {
                    balanceqty += 1;
                }
            } else {
                if (myprop.TransType != 'Transfer') {
                    balanceqty -= myprop.IssueQty;
                }
            }
            if (myprop.TransType == 'Issuance') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Transfer') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Update Property Number') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Return') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            else if (!haspar) {
                lastissuance = myprop.TransNum;
            }

            html += '<tr>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + ConvertStringToDateInputFormatMMMddyyyy(myprop.CreatedDate) + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + lastissuance + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.ReceiptQty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black; width: 20px;"></td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black; width: 20px;"></td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + PropertyListArray[rowindex].ItemCode + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.IssueQty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.Custodian + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + balanceqty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + numberWithCommas(myprop.Amount) + '</td>';

            if (myprop.TransType == 'Goods Receive') {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.Remarks + '</td>';
            } else {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.TransNum + '<br/>' + myprop.PropertyNumber + '<br/>' + myprop.OfficeOfficer + '<br/>' + myprop.Remarks + '</td>';
            }

            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        var divPropertyCard = $('#BodyHolder_divPropertyCard');
        divPropertyCard.html(html);
        PrintPropertyCard();
    }

    function parloaddetailsbyitemcode(rowindex) {
        var html = '';
        html += '<p style="font-style:italic;float:right;margin-top: -10px;">Appendix 69</p><br />';
        html += "<table style='width:100%;font-family: Arial;'>"
             + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr></table>";
        html += '<center><p style="font-size: 25px;">PROPERTY CARD</p></center>';
        html += '<table style="width: 100%;"><tr><td style="width: 50%;">Entity Name: <p style="font-size: 10px;"><u>' + CompanyInfo + '</u></p></td><td style="width: 50%;text-align:right;">Fund Cluster: <u>____________</u></td></tr></table>';
        html += '<table class="table table-striped table-bordered" style="width:100%;border-collapse: collapse;">';
        html += '<thead class="info">';
        html += '<tr">';
        html += '<th colspan="6" style="border: 1px solid black;text-align:left;">Property, Plant and Equipment :' + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '</th>';
        html += '<th colspan="2" rowspan="2" style="vertical-align: middle;border: 1px solid black;text-align:left;">Property Number : ' + PropertyListArray[rowindex].ItemCode + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th colspan="6" style="border: 1px solid black;text-align:left;">Description : ' + PropertyListArray[rowindex].Description + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Date</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Reference/PAR No.</th>';

        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Receipt</th>';
        html += '<th colspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Issue/Transfer/Disposal</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Balance</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Amount</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Remarks</th>';
        html += '</tr>';
        html += '<tr >';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Group/Officer</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';

        var lblPropDescription = $('#lblPropDescription');
        var lblPropNum = $('#lblPropNum');
        var lblPropDetailedSpec = $('#lblPropDetailedSpec');

        lblPropDescription.html(PropertyListArray[rowindex].ItemCode);
        lblPropNum.html(PropertyListArray[rowindex].PropertyNumber);
        lblPropDetailedSpec.html(PropertyListArray[rowindex].LongDescription);

        var balanceqty = 0;

        var lastissuance = "";
        var haspar = false;
        PropertyCardArray.forEach((myprop, index) => {
            if (myprop.AddOrMinus == 'Add') {
                balanceqty += myprop.ReceiptQty + myprop.IssueQty;
            } else if (myprop.AddOrMinus == 'Minus') {
                balanceqty -= myprop.IssueQty;
            }
            if (myprop.TransType == 'Goods Receive') {
                lastissuance = myprop.TransNum;
            }
            else if (myprop.TransType == 'Issuance') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            else if (!haspar) {
                lastissuance = myprop.TransNum;
            }

            html += '<tr>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + ConvertStringToDateInputFormatMMMddyyyy(myprop.CreatedDate) + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + lastissuance + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.ReceiptQty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.IssueQty + '</td>';


            if (myprop.TransType == 'Goods Receive') {

                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + "" + '</td>';
            }
            else {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.OfficeOfficer + '</td>';

            }

            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + balanceqty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + numberWithCommas(myprop.Amount) + '</td>';

            if (myprop.TransType == 'Goods Receive') {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.Remarks + '</td>';
            } else {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.TransNum + '<br/>' + myprop.PropertyNumber + '<br/>' + myprop.OfficeOfficer + '<br/>' + myprop.Remarks + '</td>';
            }

            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        var divPropertyCard = $('#BodyHolder_divPropertyCard');
        divPropertyCard.html(html);
        PrintPropertyCard();
    }

    function parloaddetails(rowindex) {
        var html = '';
        //html += '<p style="font-style:italic;float:right;margin-top: -10px;">Appendix 69</p><br />';
        html += "<table style='width:100%;font-family: Arial;'>"
             + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr></table>";

        html += '<center><p style="font-size: 25px;">PROPERTY CARD</p></center>';
        //html += '<table style="width: 100%;"><tr><td style="width: 50%;">Entity Name: <p style="font-size: 10px;"><u>' + CompanyInfo + '</u></p></td><td style="width: 50%;text-align:right;">Fund Cluster: <u>____________</u></td></tr></table>';
        html += '<table class="table table-striped table-bordered" style="width:100%;border-collapse: collapse;">';
        html += '<thead class="info">';
        html += '<tr">';
        html += '<th colspan="6" style="border: 1px solid black;text-align:left;">Property, Plant and Equipment :' + PropertyListArray[rowindex].ClassificationName + ' / ' + PropertyListArray[rowindex].CategoryName + ' / ' + PropertyListArray[rowindex].BrandName + '</th>';
        html += '<th colspan="2" rowspan="2" style="vertical-align: middle;border: 1px solid black;text-align:left;">Property Number : ' + PropertyListArray[rowindex].PropertyNumber + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th colspan="6" style="border: 1px solid black;text-align:left;">Description : ' + PropertyListArray[rowindex].LongDescription + '</th>';
        html += '</tr>';
        html += '<tr>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Date</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Reference/PAR No.</th>';

        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Receipt</th>';
        html += '<th colspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Issue/Transfer/Disposal (merge e,f,g)</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Balance</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Amount</th>';
        html += '<th rowspan="2" style="vertical-align: middle; text-align: center;border: 1px solid black;">Remarks</th>';
        html += '</tr>';
        html += '<tr >';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Office/Officer</th>';
        html += '<th style="vertical-align: middle; text-align: center;border: 1px solid black;">Qty.</th>';
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';

        var lblPropDescription = $('#lblPropDescription');
        var lblPropNum = $('#lblPropNum');
        var lblPropDetailedSpec = $('#lblPropDetailedSpec');

        lblPropDescription.html(PropertyListArray[rowindex].ItemCode);
        lblPropNum.html(PropertyListArray[rowindex].PropertyNumber);
        lblPropDetailedSpec.html(PropertyListArray[rowindex].LongDescription);

        var balanceqty = 0;

        var lastissuance = "";
        var haspar = false;
        PropertyCardArray.forEach((myprop, index) => {
            if (myprop.AddOrMinus == 'Add') {
                if (myprop.TransType == 'Update Property Number') {
                    balanceqty += 0;
                }
                else {
                    balanceqty += 1;
                }
            }

            else {
                if (myprop.TransType != 'Transfer') {
                    balanceqty -= myprop.IssueQty;
                }
            }

            if (myprop.TransType == 'Issuance') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Transfer') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Update Property Number') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Return') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            if (myprop.TransType == 'Disposal') {
                lastissuance = myprop.TransNum;
                haspar = true;
            }
            else if (!haspar) {
                lastissuance = myprop.TransNum;
            }

            html += '<tr>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + ConvertStringToDateInputFormatMMMddyyyy(myprop.CreatedDate) + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + lastissuance + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.ReceiptQty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.IssueQty + '</td>';

            if (myprop.TransType == 'Goods Receive') {

                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + "" + '</td>';
            }
            else {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.OfficeOfficer + '</td>';

            }

            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + balanceqty + '</td>';
            html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + numberWithCommas(myprop.Amount) + '</td>';

            if (myprop.OfficeOfficer != '') {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.TransNum + '<br/>' + myprop.OfficeOfficer + '<br/>' + myprop.Remarks + '</td>';
            } else {
                html += '<td style="vertical-align: middle; text-align: center;border: 1px solid black;">' + myprop.TransNum + '<br/>' + myprop.Remarks + '</td>';
            }

            html += '</tr>';
        });
        html += '</tbody>';
        html += '</table>';
        var divPropertyCard = $('#BodyHolder_divPropertyCard');
        divPropertyCard.html(html);
        PrintPropertyCard();
    }

    $('#btnView').off('click').on('click', function () {
        tblPropertyList.clear().draw();
        var propertynumber = $("#cbProperty").find(':selected').text();
    });
});

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
}
var CompanyInfo = '';

function GetCompanyInfo() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyCard.aspx/GetCompanyInfo",
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
    var typeid = $('#cbType').val()

    $.ajax({
        type: 'POST',
        url: url + "/" + loc + "/GetFilter",
        data: "{  ReportName: '" + "PropertyCard|" + typeid + "'}",
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
    $('#drpFilterBy').val(" Select Filter ").trigger('change');
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Card'
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
    $('#drpFilterBy').val(filterby).trigger('change');
    $('#drpOperator').val(operator).trigger('change');
    $('#btnAddFilter').val('Save Changes');
}

function SaveFilter(ReportName) {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];
    var typeid = $('#cbType').val()
    var NameReport = ReportName + "|" + typeid
    var Logic = $('#drpLogic').val();
    var Filter = $('#drpFilterBy').val();
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
                        data: " { ReportName : '" + NameReport + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_Filter();
                            clear_filter();
                            GetPropertyList();
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
                            data: " { RecId : '" + recIdEdit + "' ,ReportName : '" + NameReport + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                load_Filter();
                                clear_filter();
                                GetPropertyList();
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
                    GetPropertyList();
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
                    GetPropertyList();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}