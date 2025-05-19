
var url = '';

$(document).ready(function () {
    $('.select2').select2();
    transtype();

    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    $("#tblAssetList").DataTable({
        "scrollX": true,
        //"searching": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
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
    load_data();
    $("#tblAssetList").DataTable().clear().draw();
    GetDeviceWriterStatus();
    fromDashboard();
});
var transactiontype = "";
var tableAsset = '';

function transtype() {
    if ($('#BodyHolder_ddlTransactionType').val() == "GOODS RECEIVE") {
        transactiontype = " - Goods Receive"
    } else if ($('#BodyHolder_ddlTransactionType').val() == "ISSUANCE") {
        transactiontype = " - Issuance"
    } else if ($('#BodyHolder_ddlTransactionType').val() == "TRANSFER") {
        transactiontype = " - Transfer"
    } else if ($('#BodyHolder_ddlTransactionType').val() == "RETURN") {
        transactiontype = " - Return";
    } else if ($('#BodyHolder_ddlTransactionType').val() == "DISPOSAL") {
        transactiontype = " - Disposal";
    } else {
        transactiontype = " - All"
    }
}

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        return false;
    }
}

function EnterBarcode(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        var barcode = $('#txtBarcode').val().trim();
        $.ajax({
            type: "POST",
            url: url + "/PropertyVerification.aspx/VerifyPropertyByBarcode",
            data: "{barcode: '" + barcode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "1") {
                    $('#txtBarcode').val("");
                    swal("Successful", "Property verified successfully", "success");
                    load_data();
                }
                else {
                    $('#txtBarcode').val("");
                    swal("Error", "Barcode Number does not exist!", "error");
                }
            }
        });
        return false;
    }
}

function GetDeviceWriterStatus() {
    var status = document.getElementById('lblDeviceWriterStatus');
    var deviceid = $('#BodyHolder_ddlCom').val();
    if ($('#BodyHolder_ddlCom').val() == "--- Select USB Writer ---") {
        document.getElementById('lblDeviceWriterStatus').innerHTML = "Select USB Writer";
        status.classList.remove("text-green");
        status.classList.add("text-red");
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/PropertyVerification.aspx/GetDeviceWriterStatus",
            data: "{ deviceid: '" + deviceid + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "OK") {
                    document.getElementById('lblDeviceWriterStatus').innerHTML = "CONNECTION OK";
                    status.classList.remove("text-red");
                    status.classList.add("text-green");
                }
                else {
                    document.getElementById('lblDeviceWriterStatus').innerHTML = msg.d;
                    status.classList.remove("text-green");
                    status.classList.add("text-red");
                }
            }
        });
    }
}

var SystemVerificationMode = '';

function load_data() {
var transactiontype = $('#BodyHolder_ddlTransactionType').val();
            if ($('#BodyHolder_ddlTransactionType').val() == "--- SELECT TRANSACTION ---") {
                transactiontype = 0;
            } else if ($('#BodyHolder_ddlTransactionType').val() == "GOODS RECEIVE") {
                transactiontype = 1;
            } else if ($('#BodyHolder_ddlTransactionType').val() == "ISSUANCE") {
                transactiontype = 2;
            } else if ($('#BodyHolder_ddlTransactionType').val() == "TRANSFER") {
                transactiontype = 3;
            } else if ($('#BodyHolder_ddlTransactionType').val() == "RETURN") {
                transactiontype = 5;
            } else {
                transactiontype = 6;
            }

    $.ajax({
        type: "POST",
        url: url + "/PropertyVerification.aspx/GetSystemSetting",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            SystemVerificationMode = msg.d.split('|')[18];
            //alert(SystemVerificationMode);
            var search = "";
            
            transtype();
            //$(".se-pre-con").fadeIn();

            var x = document.getElementById("txtBarcode");

            if (SystemVerificationMode == "Web Manual Verification") {
                document.getElementById("ddlView").style.display = "none";
                document.getElementById("divStat").style.display = "none";
                document.getElementById("divStatus").style.display = "none";

                if (window.location.href.split('=')[1] == null) {
                }
                else {
                    transactiontype = window.location.href.split('=')[1];
                }
                x.style.display = 'none';
                $.ajax({
                    type: 'POST',
                    url: url + "/PropertyVerification.aspx/GetList",
                    data: "{ transactiontype: '" + transactiontype + "',search: '" + search + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#assetlist').hide();
                        $('#assetlist').innerHTML = "";
                        $('#assetlist').fadeIn(200);

                        myJsonData = data;
                        populateDataTable(myJsonData);

                        console.log(data);
                        console.log('Success');

                        var uri = window.location.href.toString();
                        if (uri.indexOf("?") > 0) {
                            var clean_uri = uri.substring(0, uri.indexOf("?"));
                            window.history.replaceState({}, document.title, clean_uri);
                        }

                        $(".se-pre-con").fadeOut("slow");
                    },
                    error: function (e) {
                        alert(e);
                        console.log("There was an error with your request...");
                        console.log("error: " + JSON.stringify(e));
                    }
                });
            }
            else if (SystemVerificationMode == "Thing Magic") {
                //document.getElementById("ddlView").style.display = "block";
                //document.getElementById("divStat").style.display = "block";
                //document.getElementById("divStatus").style.display = "block";
                if (window.location.href.split('=')[1] == null) {
                }
                else {
                    transactiontype = window.location.href.split('=')[1];
                }
                x.style.display = 'none';
                $.ajax({
                    type: 'POST',
                    url: url + "/PropertyVerification.aspx/GetListThingMagicMode",
                    data: "{ transactiontype: '" + transactiontype + "',search: '" + search + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#assetlist').hide();
                        $('#assetlist').innerHTML = "";
                        $('#assetlist').fadeIn(200);

                        myJsonData = data;
                        populateDataTableTM(myJsonData);

                        console.log(data);
                        console.log('Success');
                        var uri = window.location.href.toString();
                        if (uri.indexOf("?") > 0) {
                            var clean_uri = uri.substring(0, uri.indexOf("?"));
                            window.history.replaceState({}, document.title, clean_uri);
                        }
                        $(".se-pre-con").fadeOut("slow");
                    },
                    error: function (e) {
                        alert(e);
                        console.log("There was an error with your request...");
                        console.log("error: " + JSON.stringify(e));
                    }
                });
            }
            else if (SystemVerificationMode == "Barcode Scanner") {
                document.getElementById("ddlView").style.display = "none";
                document.getElementById("divStat").style.display = "none";
                document.getElementById("divStatus").style.display = "none";
                if (window.location.href.split('=')[1] == null) {
                }
                else {
                    transactiontype = window.location.href.split('=')[1];
                }
                x.style.display = 'inline';
                $.ajax({
                    type: 'POST',
                    url: url + "/PropertyVerification.aspx/GetListBarcodeBuddyProMode",
                    data: "{ transactiontype: '" + transactiontype + "',search: '" + search + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#assetlist').hide();
                        $('#assetlist').innerHTML = "";
                        $('#assetlist').fadeIn(200);

                        myJsonData = data;
                        populateDataTableBarcodeBuddyPro(myJsonData);

                        console.log(data);
                        console.log('Success');
                        var uri = window.location.href.toString();
                        if (uri.indexOf("?") > 0) {
                            var clean_uri = uri.substring(0, uri.indexOf("?"));
                            window.history.replaceState({}, document.title, clean_uri);
                        }
                        $(".se-pre-con").fadeOut("slow");
                    },
                    error: function (e) {
                        alert(e);
                        console.log("There was an error with your request...");
                        console.log("error: " + JSON.stringify(e));
                    }
                });
            }
            else { // body pro mode
                document.getElementById("ddlView").style.display = "none";
                document.getElementById("divStat").style.display = "none";
                document.getElementById("divStatus").style.display = "none";
                if (window.location.href.split('=')[1] == null) {
                }
                else {
                    transactiontype = window.location.href.split('=')[1];
                }
                x.style.display = 'none';
                $.ajax({
                    type: 'POST',
                    url: url + "/PropertyVerification.aspx/GetListBarcodeBuddyProMode",
                    data: "{ transactiontype: '" + transactiontype + "',search: '" + search + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#assetlist').hide();
                        $('#assetlist').innerHTML = "";
                        $('#assetlist').fadeIn(200);

                        myJsonData = data;
                        populateDataTableBarcodeBuddyPro(myJsonData);

                        console.log(data);
                        console.log('Success');
                        var uri = window.location.href.toString();
                        if (uri.indexOf("?") > 0) {
                            var clean_uri = uri.substring(0, uri.indexOf("?"));
                            window.history.replaceState({}, document.title, clean_uri);
                        }
                        $(".se-pre-con").fadeOut("slow");
                    },
                    error: function (e) {
                        alert(e);
                        console.log("There was an error with your request...");
                        console.log("error: " + JSON.stringify(e));
                    }
                });
            }
        }
    });
}

function populateDataTable(data) {
    try {
        console.log('populateDataTable');
        console.log(data.d);
        var table = $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true, select: { style: 'multi' },
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TransactionType" },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification' + transactiontype
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                },
                {
                    text: 'Select all',
                    action: function () {
                        $('#tblAssetList').DataTable().rows().select();
                    }
                },
                {
                    text: 'Select none',
                    action: function () {
                        $('#tblAssetList').DataTable().rows().deselect();
                    }
                },
                {
                    text: 'Verify Selected Property',
                    action: function () {
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (count == 0) {
                            swal("Warning", "Please select an item to verify", "warning");
                        }
                        else {
                            var ids = $.map($('#tblAssetList').DataTable().rows('.selected').data(), function (item) {
                                return item.pmlid;
                            });
                            console.log(ids);
                            ids.forEach((pmlid) => {
                                $(".se-pre-con").fadeIn(100);
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyVerification.aspx/VerifyProperty",
                                    data: "{pmlid: '" + pmlid + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $('#assetlist').hide();
                                        $('#assetlist').innerHTML = "";
                                        $('#assetlist').fadeIn(200);
                                        $('#tblAssetList').DataTable()
                                        .rows('.selected')
                                        .remove()
                                        .draw(false);
                                        $(".se-pre-con").hide();
                                    }
                                });
                                swal("Successful", "Property verified successfully", "success");
                            });
                        }
                    }
                }
                ]
            }, colReorder: true
            , stateSave: true
        });
        table.column(9).visible(true);
        $('#tblAssetList').on('click', 'button.btn_Verify', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            //currentRow.remove();
            //var propertynumber = currentRow.find("td:eq(2)").text();
            //alert(propertynumber);
            $.ajax({
                type: "POST",
                url: url + "/PropertyVerification.aspx/VerifyProperty",
                data: "{pmlid: '" + pmlid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    swal("Successful", "Property verified successfully", "success");
                    $('#tblAssetList').DataTable().rows(currentRow)
                     .remove()
                     .draw(false);
                    //load_data();
                }
            });
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function populateDataTableTM(data) {
    try {
        var table = $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TransactionType" },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
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
             , "columnDefs": [{
                 "targets": [9],
                 "visible": false
             }]
        });

        //$('#tblAssetList').on('click', 'button.btn_VerifyByTM', function (e) {
        //    e.preventDefault();
        //    var currentRow = $(this).closest("tr");
        //    var propertynumber = currentRow.find("td:eq(2)").text();
        //    //alert(propertynumber);
        //    var deviceid = $('#BodyHolder_ddlCom').val();
        //    if ($('#BodyHolder_ddlCom').val() == "--- Select USB Writer ---") {
        //        swal("Error!", "Select USB Writer", "warning");
        //    }
        //    else {
        //        $.ajax({
        //            type: "POST",
        //            url: url + "/PropertyVerification.aspx/VerifyPropertyByTM",
        //            data: "{propertynumber: '" + currentRow.find("td:eq(1)").text() + "', deviceid: '"+deviceid+"'}",
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (msg) {
        //                swal("Successful", "Property verified successfully", "success");
        //                load_data();
        //            }
        //        });
        //    }
        //});
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function populateDataTableBarcodeBuddyPro(data) {
    try {
        var table = $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TransactionType" },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" }
                , { "data": "Action" }
            ],
            // "columnDefs": [
            //    { 'visible': false, 'targets': 9 }
            //],
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Verification'
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
        table.column(9).visible(false);
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function fromDashboard() {
    var approvaltype = window.location.href.split('=')[1];
    if (approvaltype == '1') {
        $('#BodyHolder_ddlTransactionType').val("GOODS RECEIVE").trigger('change');
    } else if (approvaltype == '2') {
        $('#BodyHolder_ddlTransactionType').val("ISSUANCE").trigger('change');
    } else if (approvaltype == '3') {
        $('#BodyHolder_ddlTransactionType').val("TRANSFER").trigger('change');
    } else if (approvaltype == '4') {
        $('#BodyHolder_ddlTransactionType').val("REQUEST").trigger('change');
    } else if (approvaltype == '5') {
        $('#BodyHolder_ddlTransactionType').val("RETURN").trigger('change');
    } else if (approvaltype == '6') {
        $('#BodyHolder_ddlTransactionType').val("DISPOSAL").trigger('change');
    } else {
        $('#BodyHolder_ddlTransactionType').val("--- SELECT TRANSACTION ---").trigger('change');
    }
}