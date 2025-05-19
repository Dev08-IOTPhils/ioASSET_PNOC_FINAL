var idForUpdate = 0;
var PropertyPageNo = 1;
var StockPageNo = 1;
var inventorycode = '';
var expensetype = '';
var custodianN = '';
var url = '';
var createddate = '';
$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    GetsiteIC();
    //GetFirstLocation();
    //GetFirstOffice();

    $('#excelbtnsemi').off('click').on('click', function () {
        $.ajax({
            type: "POST",
            url: url + "/InventoryCount.aspx/CreateExcelSemi",
            data: " { inventorycode: '" + inventorycode + "', custodian: '" + custodianN + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divPropertyPrintListRCPPE').hide();
                $('#BodyHolder_divPropertyPrintListRCPPE').html(msg.d);
                $('#BodyHolder_divPropertyPrintListRCPPE').fadeIn(200);
                Export2Word('BodyHolder_divPropertyPrintListRCPPE', 'Report on the Physical Count of Semi-Expendable Property Form');
            }
        });
    });
    $('#iplxcl').off('click').on('click', function () {
        //alert("helow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'IPL' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "Inventory Property List_" + formattedDateTime + ".xlsx");
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
    $('#iplFxcl').off('click').on('click', function () {
        //alert("helow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'IPLF' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "Inventory Property List for Found Property_" + formattedDateTime + ".xlsx");
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
    $('#iplMxcl').off('click').on('click', function () {
        //alert("helow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'IPLM' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "Inventory Property List for Misplaced Property Form_" + formattedDateTime + ".xlsx");
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

    $('#icfxcl').off('click').on('click', function () {
        //alert("helow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
       // alert(inventorycode);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'ICF' + "', key2: '" + inventorycode + "', key3: '" + createddate + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "Inventory Count Form_" + formattedDateTime + ".xlsx");
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

    $('#iplNFxcl').off('click').on('click', function () {
        //alert("helow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'IPLNF' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "Inventory Property List for Not Found Property Form_" + formattedDateTime + ".xlsx");
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
    $('#rcppexcl').off('click').on('click', function () {
        //alert("helow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RCPPE' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "RCPPE_" + formattedDateTime + ".xlsx");
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

    $('#rpcspxcl').off('click').on('click', function () {
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RPCSP' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "RPCSP_" + formattedDateTime + ".xlsx");
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
    $('#rpcshpxcl').off('click').on('click', function () {
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RPCSPH' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "RPCSP_" + formattedDateTime + ".xlsx");
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


    $('#rpcixcl').off('click').on('click', function () {
        // alert("hselow");
        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("hellow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RPCI' + "', key2: '" + inventorycode + "', key3: '" + '' + "', key4: '" + custodianN + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                    a.attr("download", "RPCI_" + formattedDateTime + ".xlsx");
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

    $('#excelbtnppe').off('click').on('click', function () {
        $.ajax({
            type: "POST",
            url: url + "/InventoryCount.aspx/CreateExcelPPE",
            data: " { inventorycode: '" + inventorycode + "', custodian: '" + custodianN + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divPropertyPrintListRCPPE').hide();
                $('#BodyHolder_divPropertyPrintListRCPPE').html(msg.d);
                $('#BodyHolder_divPropertyPrintListRCPPE').fadeIn(200);
                Export2Word('BodyHolder_divPropertyPrintListRCPPE', 'Report on the Physical Count of Property, Plant and Equipment Form');
            }
        });
    });

    $("#tblInventoryList").DataTable({
        "scrollX": true
        //, "searching": false
        , dom: 'Brftip',
        "searching": false,
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
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
    load_data_inventorylist();
    $("#tblInventoryList").DataTable().clear().draw();

    $('#BodyHolder_btnComplete').fadeIn();
    //$('#BodyHolder_AFupload').hide();
    //$('#BodyHolder_uploadAF').hide();

    $('#BodyHolder_btnComplete1').fadeIn();
    //$('#BodyHolder_AFupload1').hide();
    //$('#BodyHolder_uploadAF1').hide();
    GetUserCcustodian();

    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_AFupload').fadeIn();
            $('#BodyHolder_uploadAF').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_AFupload').hide();
            $('#BodyHolder_uploadAF').hide();
        }
    });

    $(".switchko").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete1').hide();
            $('#BodyHolder_AFupload1').fadeIn();
            $('#BodyHolder_uploadAF1').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete1').fadeIn();
            $('#BodyHolder_AFupload1').hide();
            $('#BodyHolder_uploadAF1').hide();
        }
    });
});
function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

var custodianArray = new Array();
function GetUserCcustodian() {
    var ddlCustodian = $('#ddlCustodian');
    ddlCustodian.empty();
    ddlCustodian.append($('<option>', {
        value: '0',
        text: '--- Select Custodian Type ---',
        rowindex: -1
    }));

    $.ajax({
        async: false,
        type: 'POST',
        url: url + "/InventoryCount.aspx/GetUserCustodian",
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                custodianArray = JSON.parse(xml.d);
                custodianArray.forEach((custodian, index) => {
                    ddlCustodian.append($('<option>', {
                        value: custodian.CustodianID,
                        text: custodian.Custodian,
                        rowindex: index
                    }));
                });
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            alert(error);
            console.log(xhr);
        }
    });
}

var SiteArrayIT = new Array();
var warehouseid = 0;
var SiteName = "";
function GetsiteIC() {
    SiteName = document.getElementById("BodyHolder_lbldisplaySite").textContent.replace(":", "");

    idForUpdate = warehouseid;
    var context = window.location.pathname.substring(1);
    var ddls = $('#ddlWarehouseIC');
    ddls.empty();
    ddls.append($('<option>', {
        value: '0',
        text: '--- Select ' + SiteName + ' ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetFromWarehouse',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                SiteArray = JSON.parse(xml.d);
                SiteArray.forEach((site, index) => {
                    ddls.append($('<option>', {
                        value: site.WarehouseID,
                        text: site.WarehouseName,
                        rowindex: index
                    }));
                });
                GetLocationIC();
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            //alert(error);
            console.log(xhr);
        }
    });
}



var loclist = new Array();
var LocName = "";

function GetLocationIC() {
    LocName = document.getElementById("BodyHolder_lbldisplayLoc").textContent.replace(":", "");

    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlLocationIC');
    ddlW.empty();
    ddlW.append($('<option>', {
        value: '0',
        text: '--- Select ' + LocName + ' ---',
        rowindex: -1
    }));
    ddlW.append($('<option>', {
        value: '1',
        text: 'Receiving Location',
        rowindex: -1
    }));

    var wareH = $('#ddlWarehouseIC').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetLocationListIC',
        data: "{ warehouse : '" + wareH + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                loclist = JSON.parse(xml.d);
                loclist.forEach((position, index) => {
                    ddlW.append($('<option>', {
                        value: position.LocationID,
                        text: position.Location,
                        rowindex: index
                    }));
                });
                GetOfficeIC();
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            //alert(error);
            console.log(xhr);
        }
    });
}



var officelist = new Array();
var areaName = "";

function GetOfficeIC() {
    areaName = document.getElementById("BodyHolder_lbldisplayArea").textContent.replace(":", "");

    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlOfficeIC');
    ddlW.empty();
    ddlW.append($('<option>', {
        value: '0',
        text: '--- Select ' + areaName + ' ---',
        rowindex: -1
    }));

    var wareH = $('#ddlLocationIC').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetOfficeListAC',
        data: "{ location : '" + wareH + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                officelist = JSON.parse(xml.d);
                officelist.forEach((position, index) => {
                    ddlW.append($('<option>', {
                        value: position.AreaID,
                        text: position.Area,
                        rowindex: index
                    }));
                });
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            //alert(error);
            console.log(xhr);
        }
    });
}

//var warehouseid = 0;
//function GetWarehouseByID() {
//    idForUpdate = warehouseid;
//    $.ajax({
//        type: "POST",
//        url: url + "/InventoryCount.aspx/GetWarehouseByID",
//        data: "{ }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            //$('#txtWarehouseName').val(msg.d.split('|')[1]);
//            alert(msg.d);
//            btnAccess = msg.d.split('|');
//            alert(btnAccess[0]);
//            warehouseid = msg.d.split('|')[0];

//            var ddlwarehouse = $("[id*=ddlWarehouseIT]");
//            ddlwarehouse.empty().append('<option selected="selected" value="0">--- Select Region ---</option>');
//            ddlwarehouse.append('<option value="' + msg.d.split('|')[0] + '">' + msg.d.split('|')[1] + '</option>');

//            GetLocation();
//        }
//    });
//}
//function GetFirstLocation() {
//    var ddlLocation = $("[id*=ddlLocationIC]");
//    ddlLocation.empty();//.append('<option selected="selected" value="0">--- Select Branch ---</option>');
//    ddlLocation.append('<option value="0">--- Select Branch ---</option>');
//}
//function GetLocation() {
//    warehouseid = $('#ddlWarehouseIT').val();
//    if (warehouseid == '0') {
//        GetFirstLocation();
//    }
//    else {
//        $.ajax({
//            type: "POST",
//            url: url + "/InventoryCount.aspx/GetLocationListLT",
//            data: " { warehouse : '" + warehouseid + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                var ddlLocation = $("[id*=ddlLocationIC]");
//                ddlLocation.empty();
//                ddlLocation.append('<option selected="selected" value="0">--- Select Branch ---</option>');
//                ddlLocation.append('<option value="1">Receiving Location</option>');
//                SiteArray = JSON.parse(xml.d);
//                SiteArray.forEach((site, index) => {
//                    ddls.append($('<option>', {
//                        value: site.WarehouseID,
//                        text: site.WarehouseName,
//                        rowindex: index
//                    }));
//                });
//                $.each(msg.d, function () {
//                    ddlLocation.append($("<option></option>").val(this['Text']).html(this['Value']));
//                });

//                GetOffice()
//            }
//        });
//    }
//}

var locationid = 0;
function GetFirstOffice() {
    var ddlLocation = $("[id*=ddlOfficeIC]");
    ddlLocation.empty();//.append('<option selected="selected" value="0">--- Select Branch ---</option>');
    ddlLocation.append('<option value="0">--- Select ' + areaName + ' ---</option>');
}
function GetOffice() {
    locationid = $('#ddlLocationIC').val();
    if (locationid == '--- Select ' + LocName + ' ---') {
        GetFirstOffice();
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/InventoryCount.aspx/GetOfficeList",
            data: " { location : '" + locationid + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ddlLocation = $("[id*=ddlOfficeIC]");
                ddlLocation.empty();
                ddlLocation.append('<option selected="selected" value="0">--- Select ' + areaName + ' ---</option>');
                $.each(msg.d, function () {
                    ddlLocation.append($("<option></option>").val(this['Text']).html(this['Value']));
                });
            }
        });
    }
}

function SaveInventory() {
    var custodians = $('#ddlCustodian').val();
    var alertMsg;
    var format = /[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/;
    var expense = $('#BodyHolder_ddlItem').val();
    var empID = $('#BodyHolder_ddlPersonResponsible').val();
    var ExpenseType = '0';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else {
        ExpenseType = '0'
    }
    var custodian = $('#ddlCustodian').val();
    var description = ReplaceStr1($('#txtDescription').val().trim());
    var location = $('#ddlLocationIC').val();
    var Office = $('#ddlOfficeIC').val();
    var inventorytype = $('#BodyHolder_ddlInventoryType').val();
    if ($('#BodyHolder_ddlInventoryType').val() == 'Property')
        inventorytype = 1;
    if ($('#BodyHolder_ddlInventoryType').val() == 'Supply')
        inventorytype = 2;
    if ($('#BodyHolder_ddlInventoryType').val() == 'Property & Supply')
        inventorytype = 3;
    if ($('#ddlLocationIC').val() == '0')
        location = 0;

    if ($('#ddlWarehouseIC').val() == '0' || $('#txtInventoryName').val().trim() == '' || $('#txtDescription').val() == "" || $('#BodyHolder_ddlInventoryType').val() == '--- Select Inventory Type ---' || $('#ddlCustodian').val() == '0') {
        alertMsg = ' ';
        if ($('#txtInventoryName').val() == '')
            alertMsg += '\nInventory Name';
        if ($('#ddlCustodian').val() == '')
            alertMsg += '\nCustodian';
        if ($('#ddlWarehouseIC').val() == '0')
            alertMsg += '\nSite';
        if ($('#BodyHolder_ddlInventoryType').val() == '--- Select Inventory Type ---')
            alertMsg += '\nInventory Type';
        if ($('#txtDescription').val() == '')
            alertMsg += '\nDescription';
        //if (ExpenseType == '0' && $('#BodyHolder_ddlInventoryType').val() == 'Property')
        //    alertMsg += '\Expense Type';

        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to create this inventory?",
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
                    url: url + "/InventoryCount.aspx/SaveInventory",
                    data: " { id: '" + idForUpdate + "',inventoryname : '" + $('#txtInventoryName').val() + "', custodian: '" + custodian + "', site : '" + $('#ddlWarehouseIC').val() + "',location : '" + location + "',inventorytype : '" + inventorytype + "', description: '" + description + "', ExpenseType: '" + ExpenseType + "', Office: '" + Office + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "0") {
                            load_data_inventorylist();
                            //alert('Inventory Successfully Saved!');
                            swal("Successful", "Inventory Successfully Created!", "success");
                            $('#txtInventoryName').val("");
                            clearfields();
                        }
                        else {
                            load_data_inventorylist();
                            //alert('Inventory already exists!');
                            swal("Warning", "Inventory already exists!", "warning");
                            $('#txtInventoryName').val("");
                            clearfields();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function load_data_inventorylist() {
    $.ajax({
        type: 'POST',
        url: url + "/InventoryCount.aspx/GetInventoryList",
        data: "{ }",
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
            "searching": true,
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
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
                { "data": "IsReturnable" },
                { "data": "InventoryTypeString" },
                { "data": "DateCreated" },
                { "data": "Action" }
            ],
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Inventory Count'
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
        url: url + "/InventoryCount.aspx/GetCountFound",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msgfound) {
            document.getElementById('countfound').innerHTML = msgfound.d;
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetCountMissing",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msgmissing) {
            document.getElementById('countmissing').innerHTML = msgmissing.d;
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetCountMisplaced",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msgmisplaced) {
            document.getElementById('countmisplaced').innerHTML = msgmisplaced.d;
        }
    });
}
function inventory_type() {
    var ddl = document.getElementById("BodyHolder_ddlItem");
    if ($('#BodyHolder_ddlInventoryType').val() == 'Property') {
        ddl.disabled = false;
    }
    else if ($('#BodyHolder_ddlInventoryType').val() == 'Supply') {
        ddl.disabled = true;
    }
    else {
        ddl.disabled = false;
    }
}
function ViewInventoryDetails(inventoryCode, inventoryType, site, location, PARICS, Custodian, Area, CreatedDate) {
    $('#BodyHolder_AFupload').val('');
    inventorycode = inventoryCode;
    expensetype = PARICS;
    custodianN = Custodian;
    createddate = CreatedDate;

    var AreaS = '';
    var LocationS = '';
    if (location != "") {
        LocationS = '-' + location;
    }
    if (Area != "") {
        AreaS = '-' + Area;
    }

    //if (PARICS == "1") {
    //    document.getElementById("printGrid").children[0].style.display = "inline";
    //    document.getElementById("printGrid").children[1].style.display = "inline";
    //    document.getElementById("printGrid").children[2].style.display = "inline";
    //    document.getElementById("printGrid").children[3].style.display = "inline";
    //    document.getElementById("printGrid").children[4].style.display = "inline";
    //    document.getElementById("printGrid").children[5].style.display = "none";
    //    document.getElementById("printGrid").children[6].style.display = "none";
    //    document.getElementById("printGrid").children[7].style.display = "none";
    //    document.getElementById("printGrid").children[8].style.display = "inline";
    //    document.getElementById("printGrid").children[9].style.display = "inline";
    //    document.getElementById("printGrid").children[10].style.display = "none";
    //    document.getElementById("printGrid").children[11].style.display = "inline";
    //    document.getElementById("printGrid").children[12].style.display = "none";
    //}
    //else if (PARICS == "2" || PARICS == "3") {
    //    document.getElementById("printGrid").children[0].style.display = "inline";
    //    document.getElementById("printGrid").children[1].style.display = "inline";
    //    document.getElementById("printGrid").children[2].style.display = "inline";
    //    document.getElementById("printGrid").children[3].style.display = "inline";
    //    document.getElementById("printGrid").children[4].style.display = "none";
    //    document.getElementById("printGrid").children[5].style.display = "inline";
    //    document.getElementById("printGrid").children[6].style.display = "none";
    //    document.getElementById("printGrid").children[7].style.display = "none";
    //    document.getElementById("printGrid").children[8].style.display = "inline";
    //    document.getElementById("printGrid").children[9].style.display = "none";
    //    document.getElementById("printGrid").children[10].style.display = "inline";
    //    document.getElementById("printGrid").children[11].style.display = "none";
    //    document.getElementById("printGrid").children[12].style.display = "inline";
    //}
    //else {
    //    document.getElementById("printGrid").children[0].style.display = "none";
    //    document.getElementById("printGrid").children[1].style.display = "none";
    //    document.getElementById("printGrid").children[2].style.display = "none";
    //    document.getElementById("printGrid").children[3].style.display = "none";
    //    document.getElementById("printGrid").children[4].style.display = "none";
    //    document.getElementById("printGrid").children[5].style.display = "none";
    //    document.getElementById("printGrid").children[6].style.display = "none";
    //    document.getElementById("printGrid").children[7].style.display = "none";
    //    document.getElementById("printGrid").children[8].style.display = "none";
    //    document.getElementById("printGrid").children[9].style.display = "none";
    //    document.getElementById("printGrid").children[10].style.display = "none";
    //    document.getElementById("printGrid").children[11].style.display = "none";
    //    document.getElementById("printGrid").children[12].style.display = "none";
    //}

    document.getElementById("printGrid").children[0].style.display = "none";
    document.getElementById("printGrid").children[1].style.display = "inline";
    document.getElementById("printGrid").children[2].style.display = "inline";
    document.getElementById("printGrid").children[3].style.display = "none";
    document.getElementById("printGrid").children[4].style.display = "none";
    document.getElementById("printGrid").children[5].style.display = "inline";
    document.getElementById("printGrid").children[6].style.display = "none";
    document.getElementById("printGrid").children[7].style.display = "inline";
    document.getElementById("printGrid").children[8].style.display = "none";
    document.getElementById("printGrid").children[9].style.display = "inline";
    document.getElementById("printGrid").children[10].style.display = "none";
    document.getElementById("printGrid").children[11].style.display = "inline";
    document.getElementById("printGrid").children[12].style.display = "none";
    document.getElementById("printGrid").children[13].style.display = "inline";
    document.getElementById("printGrid").children[14].style.display = "none";
    document.getElementById("printGrid").children[15].style.display = "inline";

    document.getElementById("printGrid").children[16].style.display = "none";
    document.getElementById("printGrid").children[17].style.display = "none";
    document.getElementById("printGrid").children[18].style.display = "none";
    document.getElementById("printGrid").children[19].style.display = "none";
    document.getElementById("printGrid").children[20].style.display = "none";

    ////gheghe(Certificate/Word)
    //document.getElementById("printGrid").children[3].style.display = "none";
    //document.getElementById("printGrid").children[4].style.display = "none";

    //PageProperty(PropertyPageNo);
    //PageStock(StockPageNo);
    //$('#BodyHolder_txtInventoryCode').val(inventoryCode);
    ////getInventoryCount();
    //$.ajax({
    //    type: "POST",
    //    url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
    //    data: " { inventorycode: '" + inventorycode + "'} ",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        $('#BodyHolder_divPropertyPrintList').hide();
    //        $('#BodyHolder_divPropertyPrintList').html(msg.d);
    //        $('#BodyHolder_divPropertyPrintList').fadeIn(200);
    //    }
    //});

    //$.ajax({
    //    type: "POST",
    //    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //    data: " { inventorycode: '" + inventorycode + "' } ",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        $('#BodyHolder_divStockPrintList').hide();
    //        $('#BodyHolder_divStockPrintList').html(msg.d);
    //        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //    }
    //});

    //$('#inventorylistmodal').modal('show');

    $('#BodyHolder_txtInventoryType').val(inventoryType);
    //alert(inventorycode);
    if (inventoryType == "1") {
        document.getElementById('InventoryLocation').innerHTML = site + LocationS + AreaS
        PageProperty(PropertyPageNo);
        $('#BodyHolder_txtInventoryCode').val(inventoryCode);
        $.ajax({
            type: "POST",
            url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
            data: " { inventorycode: '" + inventorycode + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divPropertyPrintList').hide();
                $('#BodyHolder_divPropertyPrintList').html(msg.d);
                $('#BodyHolder_divPropertyPrintList').fadeIn(200);
            }
        });
        $('#inventorylistmodal').modal('show');
    }
    else {
        PageStock(StockPageNo);
        $('#BodyHolder_txtInventoryCode1').val(inventoryCode);
        $.ajax({
            type: "POST",
            url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
            data: " { inventorycode: '" + inventorycode + "' } ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divStockPrintList').hide();
                $('#BodyHolder_divStockPrintList').html(msg.d);
                $('#BodyHolder_divStockPrintList').fadeIn(200);
            }
        });
        $('#inventorylistmodal1').modal('show');
    }

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintFound",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListFound').hide();
            $('#BodyHolder_divPropertyPrintListFound').html(msg.d);
            $('#BodyHolder_divPropertyPrintListFound').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintNotFound",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListNotFound').hide();
            $('#BodyHolder_divPropertyPrintListNotFound').html(msg.d);
            $('#BodyHolder_divPropertyPrintListNotFound').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintMisplaced",
        data: " { inventorycode: '" + inventorycode + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListMisplaced').hide();
            $('#BodyHolder_divPropertyPrintListMisplaced').html(msg.d);
            $('#BodyHolder_divPropertyPrintListMisplaced').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintRCPSEP",
        data: " { inventorycode: '" + inventorycode + "',Custodian: '" + Custodian + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListRCPSEP').hide();
            $('#BodyHolder_divPropertyPrintListRCPSEP').html(msg.d);
            $('#BodyHolder_divPropertyPrintListRCPSEP').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintRCPSEPH",
        data: " { inventorycode: '" + inventorycode + "',Custodian: '" + Custodian + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListRCPSEPH').hide();
            $('#BodyHolder_divPropertyPrintListRCPSEPH').html(msg.d);
            $('#BodyHolder_divPropertyPrintListRCPSEPH').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintICF",
        data: " { inventorycode: '" + inventorycode + "',Custodian: '" + Custodian + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListICF').hide();
            $('#BodyHolder_divPropertyPrintListICF').html(msg.d);
            $('#BodyHolder_divPropertyPrintListICF').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintRCPPE",
        data: " { inventorycode: '" + inventorycode + "',Custodian: '" + Custodian + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divPropertyPrintListRCPPE').hide();
            $('#BodyHolder_divPropertyPrintListRCPPE').html(msg.d);
            $('#BodyHolder_divPropertyPrintListRCPPE').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrintROCI",
        data: " { inventorycode: '" + inventorycode + "',Custodian: '" + Custodian + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockPrintListROCI').hide();
            $('#BodyHolder_divStockPrintListROCI').html(msg.d);
            $('#BodyHolder_divStockPrintListROCI').fadeIn(200);
        }
    });
}

function PageProperty(pageNo) {
    PropertyPageNo = pageNo;

    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/GetPropertyInventoryList",
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
        url: url + "/InventoryCount.aspx/GetStockInventoryList",
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

function EnterBarcode(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        var barcode = $('#txtBarcode').val().trim();
        $.ajax({
            type: "POST",
            url: url + "/InventoryCount.aspx/UpdateFoundByBarcode",
            data: "{ barcode :'" + barcode + "', inventorycode: '" + inventorycode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                PageProperty(PropertyPageNo);

                $('#txtBarcode').val("");
                $.ajax({
                    type: "POST",
                    url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
                    data: " { inventorycode: '" + inventorycode + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#BodyHolder_divPropertyPrintList').hide();
                        $('#BodyHolder_divPropertyPrintList').html(msg.d);
                        $('#BodyHolder_divPropertyPrintList').fadeIn(200);
                    }
                });
            }
        });
        return false;
    }
}

function UpdateFoundByBarcode(barcode) {
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/UpdateFoundByBarcode",
        data: "{ barcode :'" + barcode + "', inventorycode: '" + inventorycode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            PageProperty(PropertyPageNo);

            ('#txtBarcode').val("");
            $.ajax({
                type: "POST",
                url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
                data: " { inventorycode: '" + inventorycode + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#BodyHolder_divPropertyPrintList').hide();
                    $('#BodyHolder_divPropertyPrintList').html(msg.d);
                    $('#BodyHolder_divPropertyPrintList').fadeIn(200);
                }
            });
        }
    });
}

function UpdateFound(id) {
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/UpdateFound",
        data: "{ id :'" + id + "', inventorycode: '" + inventorycode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            PageProperty(PropertyPageNo);

            $.ajax({
                type: "POST",
                url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
                data: " { inventorycode: '" + inventorycode + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#BodyHolder_divPropertyPrintList').hide();
                    $('#BodyHolder_divPropertyPrintList').html(msg.d);
                    $('#BodyHolder_divPropertyPrintList').fadeIn(200);
                }
            });
        }
    });
}

function UpdateNotFound(id) {
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/UpdateNotFound",
        data: "{ id :'" + id + "', inventorycode: '" + inventorycode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            PageProperty(PropertyPageNo);

            $.ajax({
                type: "POST",
                url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
                data: " { inventorycode: '" + inventorycode + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#BodyHolder_divPropertyPrintList').hide();
                    $('#BodyHolder_divPropertyPrintList').html(msg.d);
                    $('#BodyHolder_divPropertyPrintList').fadeIn(200);
                }
            });
        }
    });
}

function UpdateMisplaced(id) {
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/UpdateMisplaced",
        data: "{ id :'" + id + "', inventorycode: '" + inventorycode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            PageProperty(PropertyPageNo);

            $.ajax({
                type: "POST",
                url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
                data: " { inventorycode: '" + inventorycode + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#BodyHolder_divPropertyPrintList').hide();
                    $('#BodyHolder_divPropertyPrintList').html(msg.d);
                    $('#BodyHolder_divPropertyPrintList').fadeIn(200);
                }
            });
        }
    });
}

function UpdateLabelVariance(recid, quantity, txtid) {
    //var firstcount = $('#firstcount' + txtid).val();
    //var secondcount = $('#secondcount' + txtid).val();
    var thirdcount = $('#thirdcount' + txtid).val();

    //var txtFirst = $('#firstcount' + txtid);
    //var txtSecond = $('#secondcount' + txtid);
    //var txtThird = $('#thirdcount' + txtid);
    var variance = 0;

    //if ($("#firstcount" + txtid).val() != "" && $("#secondcount" + txtid).val() != "" && $("#thirdcount" + txtid).val() != "") {
    //    if (quantity > parseInt($("#thirdcount" + txtid).val())) {
    //        variance = quantity - parseInt($("#thirdcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = '-' + variance;
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'" + secondcount + "',thirdcount :'" + thirdcount + "',variance :'" + '-' + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //    else if (quantity < parseInt($("#thirdcount" + txtid).val())) {
    //        variance = quantity - parseInt($("#thirdcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = '+' + Math.abs(variance);
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'" + secondcount + "',thirdcount :'" + thirdcount + "',variance :'" + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //    else {
    //        variance = quantity - parseInt($("#thirdcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = variance;
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'" + secondcount + "',thirdcount :'" + thirdcount + "',variance :'" + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //}
    //else if ($("#firstcount" + txtid).val() != "" && $("#secondcount" + txtid).val() != "" && $("#thirdcount" + txtid).val() == "") {
    //    if (quantity > parseInt($("#secondcount" + txtid).val())) {
    //        variance = quantity - parseInt($("#secondcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = '-' + variance;
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'" + secondcount + "',thirdcount :'0',variance :'" + '-' + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //    else if (quantity < parseInt($("#secondcount" + txtid).val())) {
    //        variance = quantity - parseInt($("#secondcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = '+' + Math.abs(variance);
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'" + secondcount + "',thirdcount :'0',variance :'" + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //    else {
    //        variance = quantity - parseInt($("#secondcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = variance;
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'" + secondcount + "',thirdcount :'0',variance :'" + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //}
    //else if ($("#firstcount" + txtid).val() != "" && $("#secondcount" + txtid).val() == "" && $("#thirdcount" + txtid).val() == "") {
    //    if (quantity > parseInt($("#firstcount" + txtid).val())) {
    //        variance = quantity - parseInt($("#firstcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = '-' + variance;
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'0',thirdcount :'0',variance :'" + '-' + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //    else if (quantity < parseInt($("#firstcount" + txtid).val())) {
    //        variance = quantity - parseInt($("#firstcount" + txtid).val());
    //        document.getElementById('variance' + txtid).innerHTML = '+' + Math.abs(variance);
    //        $.ajax({
    //            type: "POST",
    //            url: url + "/InventoryCount.aspx/UpdateVariance",
    //            data: "{ recid :'" + recid + "',firstcount :'" + firstcount + "',secondcount :'0',thirdcount :'0',variance :'" + variance + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                $.ajax({
    //                    type: "POST",
    //                    url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
    //                    data: " { inventorycode: '" + inventorycode + "' } ",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        $('#BodyHolder_divStockPrintList').hide();
    //                        $('#BodyHolder_divStockPrintList').html(msg.d);
    //                        $('#BodyHolder_divStockPrintList').fadeIn(200);
    //                    }
    //                });
    //            }
    //        });
    //    }
    //    else {
            variance = quantity - parseInt($("#thirdcount" + txtid).val());
            document.getElementById('variance' + txtid).innerHTML = variance;
            $.ajax({
                type: "POST",
                url: url + "/InventoryCount.aspx/UpdateVariance",
                data: "{ recid :'" + recid + "',firstcount :'0',secondcount :'0',thirdcount :'"+thirdcount+"',variance :'" + variance + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $.ajax({
                        type: "POST",
                        url: url + "/InventoryCount.aspx/GetStockInventoryListPrint",
                        data: " { inventorycode: '" + inventorycode + "' } ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#BodyHolder_divStockPrintList').hide();
                            $('#BodyHolder_divStockPrintList').html(msg.d);
                            $('#BodyHolder_divStockPrintList').fadeIn(200);
                        }
                    });
                }
            });
    //    }
    //}
}

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

function addRow(tableID) {
    var table = document.getElementById(tableID);

    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    var cell1 = row.insertCell(0);
    var element1 = document.createElement("input");
    element1.type = "text";
    element1.id = "itemcode" + (rowCount - 1);
    element1.className = "form-control";
    element1.style = "width: 100%";
    element1.disabled = true;
    cell1.appendChild(element1);

    var cell2 = row.insertCell(1);
    var element2 = document.createElement("input");
    element2.type = "text";
    element2.id = "propertyno" + (rowCount - 1);
    element2.className = "form-control";
    element2.style = "width: 100%";
    element2.addEventListener("change", function () {
        addToList(inventorycode, (rowCount - 1), expensetype);
    })
    element2.addEventListener("keypress", function (event) {
        var x = event.keyCode;
        if (x == 13) {  // 27 is the ESC key
            event.preventDefault();
            return false;
        }
    })
    cell2.appendChild(element2);

    var cell3 = row.insertCell(2);
    var element3 = document.createElement("input");
    element3.type = "text";
    element3.id = "originlocation" + (rowCount - 1);
    element3.className = "form-control";
    element3.style = "width: 100%";
    element3.disabled = true;
    cell3.appendChild(element3);

    var cell4 = row.insertCell(3);
    var element4 = document.createElement("input");
    element4.type = "radio";
    element4.disabled = true;
    element4.name = "optionRadios" + (rowCount - 1);
    element4.style = "left :50%";
    cell4.appendChild(element4);

    var cell5 = row.insertCell(4);
    var element5 = document.createElement("input");
    element5.type = "radio";
    element5.disabled = true;
    element5.name = "optionRadios" + (rowCount - 1);
    cell5.appendChild(element5);

    var cell6 = row.insertCell(5);
    var element6 = document.createElement("input");
    element6.type = "radio";
    element6.checked = true;
    element6.disabled = true;
    element6.name = "optionRadios" + (rowCount - 1);
    cell6.appendChild(element6);
}

function deleteRow(tableID) {
    try {
        var table = document.getElementById(tableID);
        var rowCount = table.rows.length;

        for (var i = 0; i < rowCount; i++) {
            var row = table.rows[i];
            var chkbox = row.cells[0].childNodes[0];
            if (null != chkbox && true == chkbox.checked) {
                table.deleteRow(i);
                rowCount--;
                i--;
            }
        }
    } catch (e) {
        alert(e);
    }
}

function addToList(invcode, id, expensetype) {
    var propertyno = $("#propertyno" + (id)).val();
    $.ajax({
        type: "POST",
        url: url + "/InventoryCount.aspx/addPropertyInventoryToList",
        data: "{inventorycode: '" + invcode + "', propertyno :'" + propertyno + "', expensetype :'" + expensetype + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "already exist") {
                //alert('Property # already exists!');
                swal("Error", "Property # already exists!", "error");
            }
            else if (msg.d == "does not exist") {
                //alert('Property # does not exists!');
                swal("Error", "Property # does not exists!", "error");
            }
            else {
                $("#itemcode" + id).val(msg.d.split('|')[0]);
                $("#originlocation" + id).val(msg.d.split('|')[1]);
                //alert('Property Added Successfully');
                swal("Success", "Property Added Successfully!", "success");
                getInventoryCount();

                $.ajax({
                    type: "POST",
                    url: url + "/InventoryCount.aspx/GetPropertyInventoryListPrint",
                    data: " { inventorycode: '" + inventorycode + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#BodyHolder_divPropertyPrintList').hide();
                        $('#BodyHolder_divPropertyPrintList').html(msg.d);
                        $('#BodyHolder_divPropertyPrintList').fadeIn(200);
                    }
                });
            }
        }
    });
}

function clearfields() {
    $('#txtInventoryName').val('');
    $('#txtDescription').val('');
    GetUserCcustodian();
    //GetFirstLocation();
    //GetFirstOffice();

    $('#ddlWarehouseIC').val('0').trigger('change');
    $('#BodyHolder_ddlItem').val('--- Select Property Type Cost ---').trigger('change');
    $('#BodyHolder_ddlInventoryType').val('--- Select Inventory Type ---').trigger('change');
}

function UploadInventoryCount() {
    var alertMsg;
    var inventoryno = $('#BodyHolder_txtInventoryCode').val();
    var filess = $('#BodyHolder_AFupload').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    //if (filess == "") {
    //    swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
    //}
    //else {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to complete this transaction?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $(function () {
                var fileUpload = $("#BodyHolder_AFupload").get(0);
                var files = fileUpload.files;
                var test = new FormData();
                for (var i = 0; i < files.length; i++) {
                    test.append(files[i].name, files[i]);
                }
                $.ajax({
                    url: url + "/OperationUploadHandler.ashx?transactionnumber=" + inventoryno + "|InventoryCount",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: test,
                    success: function (result) {
                        if (result.includes("File Uploaded Successfully", 0)) {
                            $.ajax({
                                type: "POST",
                                url: url + "/InventoryCount.aspx/UploadInventoryCount",
                                data: " { inventorycode : '" + inventoryno + "'} ",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    load_data_inventorylist();
                                    $('#inventorylistmodal').modal('hide');
                                    swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                }
                            });
                        } else {
                            swal("Warning", result, "warning");
                        }
                    },
                    error: function (err) {
                        swal("Warning", err.statusText, "warning");
                    }
                });
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    //}
}

function UploadInventoryCountStock() {
    var alertMsg;
    var inventoryno = $('#BodyHolder_txtInventoryCode1').val();
    var filess = $('#BodyHolder_AFupload1').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to complete this transaction?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $(function () {
                if (filess == "") {
                    $.ajax({
                        type: "POST",
                        url: url + "/InventoryCount.aspx/UploadInventoryCount",
                        data: " { inventorycode : '" + inventoryno + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_data_inventorylist();
                            $('#inventorylistmodal1').modal('hide');
                            swal("Successful", "Transaction Completed without Supporting Document!", "success");
                        }
                    });
                }
                else {
                    var fileUpload = $("#BodyHolder_AFupload1").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + inventoryno + "|InventoryCount",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/InventoryCount.aspx/UploadInventoryCount",
                                    data: " { inventorycode : '" + inventoryno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_inventorylist();
                                        $('#inventorylistmodal1').modal('hide');
                                        swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                    }
                                });
                            } else {
                                swal("Warning", result, "warning");
                            }
                        },
                        error: function (err) {
                            swal("Warning", err.statusText, "warning");
                        }
                    });
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}