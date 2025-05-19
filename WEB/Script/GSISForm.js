
var url = "";

$(document).ready(function () {
    GetsiteIC();
    load_data();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    DLexcel();

    load_data();
});
var warehouseID = '0';
var expType = '0';
var floater = false;
function DLexcel()
{
    $('#PrintExcel').off('click').on('click', function () {
        if ((document.getElementById("chkPropertyfloater").checked)) {
            floater = true;
        }
        else {
            floater = false;
        }
        //alert(floater);
        if (warehouseID == '0' && expType == '0') {
            swal("Warning", "Select Site or Property Type Cost to Proceed.", "warning");
        }

        else {
            const now = new Date();

            const year = now.getFullYear();
            const month = (now.getMonth() + 1).toString().padStart(2, '0');
            const day = now.getDate().toString().padStart(2, '0');
            const hours = now.getHours().toString().padStart(2, '0');
            const minutes = now.getMinutes().toString().padStart(2, '0');
            const seconds = now.getSeconds().toString().padStart(2, '0');

            const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

            var context = window.location.pathname.substring(1);
            //alert(warehouseID + ' ' + expType + ' ' + floater)
            // alert(context);
            $.ajax({
                async: false,
                type: 'POST',
                url: context + '/ReportPage',
                data: "{ key1: '" + 'GSISForm' + "', key2: '" + warehouseID + "', key3: '" + expType + "', key4: '" + floater + '' + "', key5: '" + '' + "'}",
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
                        a.attr("download", "GSISForm_" + formattedDateTime + ".xls");
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
        }
    });

    /////
}


function GetsiteIC() {
    SiteName = document.getElementById("BodyHolder_lbldisplaySite").textContent.replace(":", "");

  //  idForUpdate = warehouseid;
    var context = window.location.pathname.substring(1);
    var ddls = $('#ddlWarehouse');
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
               // GetLocationIC();
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

function AbleDisableFilter()
{
    load_data();
}


function load_data() {

    var siteID = $('#ddlWarehouse').val();
    var expense = $('#BodyHolder_ddlItem').val();
    var floater = false;

    if ((document.getElementById("chkPropertyfloater").checked))
    {
        floater = true;
    }
    else
    {
        floater = false;
    }

    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }

    //generatePDF(siteID, ExpenseType);

    $.ajax({
        type: "POST",
        async: false,
        url: url + "/GSISForm.aspx/LoadData",
        data: "{SiteID: '" + siteID + "' , proprtyTypeCost: '" + ExpenseType + "' , floater: '" + floater + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            myJsonData = msg;
            populateDataTable(myJsonData);

        }
    });


}
function populateDataTable(data) {
    try {
        $('#tblItemList').DataTable({
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
                { "data": "ItemCode"},
                { "data": "ClassificationName"},
                { "data": "Description" },
                { "data": "Site" },
                { "data": "Location" },
                { "data": "Area" },
                { "data": "DateOfAcquisition"},
                { "data": "PropertyNumber"},
                { "data": "AcquiredCostString" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
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

        $('#tblItemList').off('click');

        $('#tblItemList').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        console.log('Catchasdasdasdasdasd');
        
        console.log(e.message);
        console.log(e);
        var table = $('#tblItemList').DataTable();
        table
            .clear()
            .draw();
    }
}


function loaddata()
{
    //alert($('#ddlWarehouse').val());
    warehouseID = $('#ddlWarehouse').val();

    var expense = $('#BodyHolder_ddlItem').val();
    if (expense.includes('ICS LOW')) {
        expType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        expType = '3'
    }
    else if (expense.includes('PAR')) {
        expType = '1'
    }
    else if (expense.includes('ITR')) {
        expType = '4'
    }
    else {
        expType = '0'
    }
    
    load_data();
    
    
}



function generatePDF(sytID, expType, floater) {
    $.ajax({
        type: "POST",
        async: false,
        url: url + "/GSISForm.aspx/GenerateReport",
        data: "{siteID: '" + sytID + "' , proprtyTypeCost: '" + expType + "' , floater: '" + floater + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var divPropertyCard = $('#BodyHolder_divStockCard');
            divPropertyCard.html(msg.d);
            //var divPropertyCard = $('#BodyHolder_divReport');
            //divPropertyCard.html(msg.d);
            // alert('asdasdasdasd');

        }
    });

    PrintReport();

}

function PrintPDF() {
    var siteID = $('#ddlWarehouse').val();
    var expense = $('#BodyHolder_ddlItem').val();

    var floater = false;

    if ((document.getElementById("chkPropertyfloater").checked)) {
        floater = true;
    }
    else {
        floater = false;
    }
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }

    if (ExpenseType == '0' && siteID == '0') {
        swal("Warning", "Select Site or Property Type Cost to Proceed.", "warning");
    }
    else {
        
        generatePDF(siteID, ExpenseType, floater)
    }

}

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};