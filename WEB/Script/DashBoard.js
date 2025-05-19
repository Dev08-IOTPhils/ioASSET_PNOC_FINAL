$(document).ready(function () {
    $('.select2').select2();
    UsrInf();
    ApprovalTiles();
    $("#tblITEMList").DataTable({
        "scrollX": true,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                      },
                       {
                           extend: 'colvis',
                           text: 'COLUMN VISIBILITY',
                           className: 'btn btn-primary', exportOptions: { columns: ':visible' },
                           collectionLayout: 'four-column'
                       }
                ],
                dropup: false, //btn btn-sm btn-success
            }
            ]
        }, colReorder: true
            , stateSave: true
    });

    //$("#tblAssetList").DataTable({
    //    "responsive": true,
    //    fixedHeader: {
    //        header: true,
    //        footer: true,
    //        headerOffset: 50
    //    },
    //    scrollX: true,
    //    "language": {
    //        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
    //    },
    //    dom: 'Brftip',
    //    buttons: {
    //        buttons: [
    //        {
    //            extend: 'collection',
    //            text: '...',
    //            buttons: [
    //                {
    //                    extend: 'copy',
    //                    text: 'COPY',
    //                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                },
    //                {
    //                    extend: 'excel',
    //                    text: 'EXCEL',
    //                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                },
    //                 {
    //                     extend: 'csv',
    //                     text: 'CSV',
    //                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                 },
    //                  {
    //                      extend: 'print',
    //                      text: 'PRINT',
    //                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                  },
    //                   {
    //                       extend: 'colvis',
    //                       postfixButtons: ['colvisRestore'],
    //                       text: 'COLUMN VISIBILITY',
    //                       className: 'btn btn-primary', exportOptions: { columns: ':visible' },
    //                       collectionLayout: 'four-column'
    //                   }
    //            ],
    //            dropup: false, //btn btn-sm btn-success
    //        }
    //        ]
    //    }, colReorder: true
    //        , stateSave: true
    //});

    //$("#tblStockList").DataTable({
    //    "scrollX": true,
    //    fixedHeader: {
    //        header: true,
    //        footer: true,
    //        headerOffset: 50
    //    },
    //    dom: 'Brftip',
    //    buttons: {
    //        buttons: [
    //        {
    //            extend: 'collection',
    //            text: '...',
    //            buttons: [
    //                {
    //                    extend: 'copy',
    //                    text: 'COPY',
    //                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                },
    //                {
    //                    extend: 'excel',
    //                    text: 'EXCEL',
    //                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                },
    //                 {
    //                     extend: 'csv',
    //                     text: 'CSV',
    //                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                 },
    //                  {
    //                      extend: 'print',
    //                      text: 'PRINT',
    //                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Daskboard'
    //                  },
    //                   {
    //                       extend: 'colvis',
    //                       postfixButtons: ['colvisRestore'],
    //                       text: 'COLUMN VISIBILITY',
    //                       className: 'btn btn-primary', exportOptions: { columns: ':visible' },
    //                       collectionLayout: 'two-column'
    //                   }
    //            ],
    //            dropup: false, //btn btn-sm btn-success
    //        }
    //        ]
    //    }, colReorder: true
    //        , stateSave: true
    //});

    load_data();
    //load_data_Property();
    //load_data_Stock();

    $("#tblITEMList").DataTable().clear().draw();
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $($.fn.dataTable.tables(true)).DataTable()
           .columns.adjust();
    });

    DashboardHideShowPannel();
    //checkuserRole();
});

/////added by deme
//function checkuserRole() {

//    // var loc = currentLocation.pathname.split('/')[2];

//    try {
//        $.ajax({
//            type: "POST",
//            //url: url,
//            url: "Home/Dashboard.aspx/CheckUserIsAdCust",
//            data: "{ }",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {

//                let dataval = msg.d;
//                let values = dataval.split("|");
//                console.log(values);
//                alert(values[0]);
//                //values[0] = true - user is admin
//                //values[1] = true - user is Approver
//                //values[2] = true - user is Custodian
//                if (values[0] = 'False') {
//                    return;
//                }
//                else {

//                    //document.getElementsByName("pendingApprovalNotif").forEach(el => {
//                    //    el.style.display = "none";
//                    //});
//                    document.getElementsById("spanApproval").style.display = "block";
//                    $('#spanApproval').css('display', 'block');
//                    //$('#spanApproval').display = "none";

//                    //var link = document.getElementById("spanApproval");
//                    //link.style.display = "none"; //or
//                }


//            }
//        });

//    } catch (e) {

//    }

//}

var usrinf = '';
function UsrInf() {

    var divPropertyMasterList = document.getElementById("divPropertyMasterList");
    var divStockMasterList = document.getElementById("divStockMasterList");

    divPropertyMasterList.style.display = 'none';
    divStockMasterList.style.display = 'none';
    $.ajax({
        type: "POST",
        url: "Home/Dashboard.aspx/GetUserInfo",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            usrinf = msg.d;
            //alert(usrinf);
            var admin = usrinf.split('|')[0];
            var aprvr = usrinf.split('|')[1];
            var crtfr = usrinf.split('|')[2];
            var inspctr = usrinf.split('|')[3];
            var cstdn = usrinf.split('|')[4];

            var divWidgets = document.getElementById("divWidgets");
            //var divGraphical = document.getElementById("divGraphical");
            var divPendingApproval = document.getElementById("divPendingApproval");
            var divPendingVerification = document.getElementById("divPendingVerification");
            var divOpenInventoryCnt = document.getElementById("divOpenInventoryCnt");
            divWidgets.style.display = 'none';
            //divGraphical.style.display = 'none';
            divPendingApproval.style.display = 'none';
            divPendingVerification.style.display = 'none';
            divOpenInventoryCnt.style.display = 'none';

            if (aprvr == "True") {
                divWidgets.style.display = 'none';
                //divGraphical.style.display = 'none';
                divPendingApproval.style.display = 'block';
                divPendingVerification.style.display = 'none';
                divOpenInventoryCnt.style.display = 'none';
                //divPropertyMasterList.style.display = 'none';
                //divStockMasterList.style.display = 'none';
            }
            if (crtfr == "True") {
                divWidgets.style.display = 'none';
                //divGraphical.style.display = 'none';
                divPendingApproval.style.display = 'block';
                divPendingVerification.style.display = 'none';
                divOpenInventoryCnt.style.display = 'none';
                //divPropertyMasterList.style.display = 'none';
                //divStockMasterList.style.display = 'none';
            }
            if (inspctr == "True") {
                divWidgets.style.display = 'none';
                //divGraphical.style.display = 'none';
                divPendingApproval.style.display = 'block';
                divPendingVerification.style.display = 'none';
                divOpenInventoryCnt.style.display = 'none';
                //divPropertyMasterList.style.display = 'none';
                //divStockMasterList.style.display = 'none';
            }
            if (cstdn == "True") {
                divWidgets.style.display = 'block';
                //divGraphical.style.display = 'block';
                divPendingApproval.style.display = 'none';
                divPendingVerification.style.display = 'block';
                divOpenInventoryCnt.style.display = 'block';
                //divPropertyMasterList.style.display = 'block';
                //divStockMasterList.style.display = 'block';
            }
            if (admin == "True") {
                divWidgets.style.display = 'block';
                //divGraphical.style.display = 'block';
                divPendingApproval.style.display = 'block';
                divPendingVerification.style.display = 'block';
                divOpenInventoryCnt.style.display = 'block';
                //divPropertyMasterList.style.display = 'block';
                //divStockMasterList.style.display = 'block';
            }
        }
    });
}

var approvalsettings = '';
function ApprovalTiles() {
    $.ajax({
        type: "POST",
        url: "Home/Dashboard.aspx/GetApprovalTiles",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            approvalsettings = msg.d;
            var gr = approvalsettings.split('|')[0];
            var stockmerge = approvalsettings.split('|')[1];
            var propissuance = approvalsettings.split('|')[2];
            var proptransfer = approvalsettings.split('|')[3];
            var stockissuereq = approvalsettings.split('|')[4];
            var stockdisposal = approvalsettings.split('|')[5];
            var propdisposal = approvalsettings.split('|')[6];
            var gpdp = approvalsettings.split('|')[7];
            var csvupload = approvalsettings.split('|')[8];
            var dbrestore = approvalsettings.split('|')[9];
            var proprequest = approvalsettings.split('|')[10];

            var divPendingApproval = document.getElementById("divPendingApproval");

            var divGR = document.getElementById("divGR");
            var divPropIssuance = document.getElementById("divPropIssuance");
            var divPropTransfer = document.getElementById("divPropTransfer");
            var divPropDisposal = document.getElementById("divPropDisposal");
            var divGatePass = document.getElementById("divGatePass");
            var divDayPass = document.getElementById("divDayPass");
            var divCsvUpload = document.getElementById("divCsvUpload");
            var divDB = document.getElementById("divDB");
            var divStockMerge = document.getElementById("divStockMerge");
            var divExtensionDate = document.getElementById("divExtensionDate");
            var divStockIssuance = document.getElementById("divStockIssuance");
            var divStockRequest = document.getElementById("divStockRequest");
            var divStockDisposal = document.getElementById("divStockDisposal");
            divGR.style.display = 'none';
            divPropIssuance.style.display = 'none';
            divPropRequest.style.display = 'none';
            divPropTransfer.style.display = 'none';
            divPropDisposal.style.display = 'none';
            divGatePass.style.display = 'none';
            divDayPass.style.display = 'none';
            divCsvUpload.style.display = 'none';
            divDB.style.display = 'none';
            divStockMerge.style.display = 'none';
            divExtensionDate.style.display = 'none';
            divStockIssuance.style.display = 'none';
            divStockRequest.style.display = 'none';
            divStockDisposal.style.display = 'none';
            if (approvalsettings == "False|False|False|False|False|False|False|False|False|False|False") {
                divPendingApproval.style.display = 'none';
            }
            if (gr == "True") {
                divGR.style.display = 'block';
            }
            if (propissuance == "True") {
                divPropIssuance.style.display = 'block';
            }

            if (proptransfer == "True") {
                divPropTransfer.style.display = 'block';
            }
            if (propdisposal == "True") {
                divPropDisposal.style.display = 'block';
            }
            if (gpdp == "True") {
                divGatePass.style.display = 'block';
                divDayPass.style.display = 'block';
            }
            if (csvupload == "True") {
                divCsvUpload.style.display = 'block';
            }
            if (dbrestore == "True") {
                divDB.style.display = 'block';
            }
            if (stockissuereq == "True") {
                divStockIssuance.style.display = 'block';
                divStockRequest.style.display = 'block';
            }
            if (stockmerge == "True") {
                divStockMerge.style.display = 'block';
            }
            if (stockdisposal == "True") {
                divStockDisposal.style.display = 'block';
            }
        }
    });
}

/////////My List
function load_data() {
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetMyItems",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;

            populateDataTable(myJsonData);
            console.log('GetMyItems');
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

function populateDataTable(data) {
    try {
        $('#tblITEMList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "AssetType" },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "Custodian" },
                { "data": "Description" },
                { "data": "AcquiredCostString" },
                { "data": "IssuanceNumber" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Employee Ledger Card'
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

        $('#tblITEMList').off('click');
    } catch (e) {
        var table = $('#tblITEMList').DataTable();
        table
            .clear()
            .draw();
    }
}

function DashboardHideShowPannel() {
    //alert("OH TEN");

    document.getElementById("divPendingAcknowledgement").style.display = "none";
    document.getElementById("divPendingApproval").style.display = "none";
    document.getElementById("divPendingVerification").style.display = "none";
    document.getElementById("divOpenInventoryCnt").style.display = "none";

    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/DashboardHideShowPannel",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            //alert(data.d.split("|")[5]);
            if (data.d.split("|")[5] == "True") {
                document.getElementById("divPendingAcknowledgement").style.display = "block";
            }
            if (data.d.split("|")[3] == "True") {
                if (data.d.split("|")[2] == "True") {
                    //document.getElementById("divPendingAcknowledgement").style.display = "block";
                    document.getElementById("divPendingApproval").style.display = "block";
                    document.getElementById("divPendingVerification").style.display = "block";
                    document.getElementById("divOpenInventoryCnt").style.display = "block";
                }
                else {
                    //document.getElementById("divPendingAcknowledgement").style.display = "block";
                    document.getElementById("divPendingApproval").style.display = "block";
                    document.getElementById("divPendingVerification").style.display = "block";
                    document.getElementById("divOpenInventoryCnt").style.display = "block";

                }
            }
            else {
                if (data.d.split("|")[0] == "0") {
                    //document.getElementById("divPendingAcknowledgement").style.display = "block";
                    document.getElementById("divPendingApproval").style.display = "block";
                    document.getElementById("divOpenInventoryCnt").style.display = "block";

                    if (data.d.split("|")[1] == "Web Manual Verification") {
                        document.getElementById("divPendingVerification").style.display = "block";
                    }
                }
                if (data.d.split("|")[2] == "True") {
                    document.getElementById("divPendingAcknowledgement").style.display = "block";
                }
                if (data.d.split("|")[4] == "True") {
                    document.getElementById("divPendingApproval").style.display = "block";
                }
            }
        }
    });
}

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Property();
        return false;
    }
}

////////////Property Masterlist
function load_data_Property() {
    var search = "";

    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetListProperty",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            //x = JSON.parse(data);
            populateDataTableProperty(myJsonData);

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

function populateDataTableProperty(data) {
    try {
        $('#tblAssetList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "PropertyNumber" },
                { "data": "LongDescription" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "PRNumber" },
                { "data": "PONumber" },
                { "data": "InvoiceNumber" },
                { "data": "DRNumber" },
                { "data": "IssuanceNumber" },
                { "data": "IssuedTo" },
                { "data": "Site" },
                { "data": "Location" },
                { "data": "Area" },
                { "data": "DateOfAcquisition" },
                { "data": "UsefulLifeString" },
                { "data": "RemainingUsefulLifeString" },
                { "data": "DateOfMaintenance" },
                { "data": "AcquiredCostString" },
                { "data": "DepreciatedCostString" },
                { "data": "Status" },
                { "data": "DisposedDate" },
                { "data": "IssuedDate" },
                { "data": "LastmaintenanceDate" }
            ],
            "rowCallback": function (row, data, index) {
                if (data.Status != 'On Stock' || data.Status != 'Issued' || parseInt(data.RemainingUsefulLifeString) <= 0) {
                    $('td', row).css('color', 'Red');
                }
                else if (data.Status == 'Disposed') {
                    $('td', row).css('color', 'Grey');
                }
                else {
                    //$('td', row).css('color', 'Green');
                }
            },
            dom: 'Brftip',
            lengthMenu: [
            [5, 10, 25, 50, -1],
            [5, 10, 25, 50, 'All'],
            ], buttons: {
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
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                }
                ]
            }, colReorder: true
            , stateSave: true
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function EnterKeySearch1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Stock();
        return false;
    }
}
////////////Stock Masterlist
function load_data_Stock() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Home/Dashboard.aspx/GetStockMasterList",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stocklist').hide();
            $('#stocklist').innerHTML = "";
            $('#stocklist').fadeIn(200);

            myJsonData = data;
            populateDataTableStock(myJsonData);

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

function populateDataTableStock(data) {
    try {
        $('#tblStockList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" },
                { "data": "UnitOfMeasurement" },
                { "data": "Custodian" },
                { "data": "Location" },
                { "data": "CriticalLevelString" }
            ],
            "rowCallback": function (row, data, index) {
                if (parseInt(data.QuantityString) <= parseInt(data.CriticalLevelString)) {
                    $('td', row).css('color', 'Red');
                }
                else {
                    $('td', row).css('color', 'Green');
                }
            }
            , dom: 'Brftip'
            , buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Stock Masterlist'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Stock Masterlist'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Stock Masterlist'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Stock Masterlist'
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

        $('#tblStockList').off('click');
    } catch (e) {
        var table = $('#tblStockList').DataTable();
        table
            .clear()
            .draw();
    }
}