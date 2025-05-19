$(document).ready(function () {
    // GetUserCcustodian();
    IfDisposalNeedsApproval();
    $("#tblAssetList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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
    });
    //R
    $('#wmrxclR').off('click').on('click', function () {

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert(disposalnumber);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'WMR' + "', key2: '" + disposalnumberrequest + "', key3: '" + 'PDisposalR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "WMR_" + formattedDateTime + ".xls");
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

    $('#rlsddpxclR').off('click').on('click', function () {

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert(disposalnumberrequest);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RLSDDP' + "', key2: '" + disposalnumberrequest + "', key3: '" + 'PDisposalR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RLSDDP_" + formattedDateTime + ".xls");
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
    $('#rlsddspxclR').off('click').on('click', function () {

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert(disposalnumberrequest);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RLSDDSP' + "', key2: '" + disposalnumberrequest + "', key3: '" + 'PDisposalR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RLSDDSP_" + formattedDateTime + ".xls");
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

    $('#BodyHolder_ddlCustodian').on('change', function () {
        load_dataPDisposalRequest();
    });
    load_data_Pendingupload();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
});

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function load_dataPDisposalRequest() {
    var expense = $('#BodyHolder_ddlItem').val();
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

    var search = "";
    var custodian = $('#BodyHolder_ddlCustodian').val();
    if (custodian == '--- Select Custodian Type ---') {
        custodian = 0;
    }
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyDisposalRequest.aspx/GetList",
        data: "{ search: '" + search + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData, ExpenseType);
            GetCount(ExpenseType);
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

function populateDataTable(data, ExpenseType) {
    try {
        $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true, select: { style: 'multi' },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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
                    text: 'Add to Disposal List',
                    action: function () {
                        var expense = $('#BodyHolder_ddlItem').val();
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
                        var custodian = $('#BodyHolder_ddlCustodian').val();
                        if (custodian == '--- Select Custodian Type ---') {
                            custodian = 0;
                        }
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (count == 0) {
                            swal("Warning", "Please select an item to add", "warning");
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
                                    url: "Operation_Module/PropertyDisposalRequest.aspx/PropertyTransferToDisposal",
                                    data: "{pmlid: '" + pmlid + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
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
                                        GetCount(ExpenseType);
                                        $(".se-pre-con").hide();
                                    }
                                });
                            });
                        }
                    }
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(1)").text();
            var custodian = $('#BodyHolder_ddlCustodian').val();
            if (custodian == '--- Select Custodian Type ---') {
                custodian = 0;
            }
            var pmlid = $(this).attr('pmlid');
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposalRequest.aspx/PropertyTransferToDisposal",
                data: "{pmlid: '" + pmlid + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#assetlist').hide();
                    $('#assetlist').innerHTML = "";
                    $('#assetlist').fadeIn(200);
                    GetCount(ExpenseType);
                    //load_data();
                    $('#tblAssetList').DataTable().rows(currentRow)
                    .remove()
                    .draw(false);
                    //currentRow.remove();
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


var disposalnumberrequest = '';
function ViewDisposalDetailsRequest(disposalno, transactorname, createddate, isapproved, counter, counterchecker, status, PARICS) {
    disposalnumberrequest = disposalno;
    document.getElementById('lblDisposalNoRequest').innerHTML = disposalno;
    document.getElementById('lblDisposedByRequest').innerHTML = transactorname
    document.getElementById('lblDisposedDateRequest').innerHTML = createddate;
    $('#BodyHolder_txtDisposalNumberRequest').val(disposalno);

    if (PARICS == "1") {
        document.getElementById("printGrid2").children[0].style.display = "none";
        document.getElementById("printGrid2").children[1].style.display = "inline";
        document.getElementById("printGrid2").children[2].style.display = "inline";

        document.getElementById("printGrid2").children[3].style.display = "inline";
        document.getElementById("printGrid2").children[4].style.display = "none";
        document.getElementById("printGrid2").children[5].style.display = "inline";
    }
    else if (PARICS == "2" || PARICS == "3") {
        document.getElementById("printGrid2").children[0].style.display = "inline";
        document.getElementById("printGrid2").children[1].style.display = "none";
        document.getElementById("printGrid2").children[2].style.display = "inline";

        document.getElementById("printGrid2").children[3].style.display = "nonep";
        document.getElementById("printGrid2").children[4].style.display = "inline";
        document.getElementById("printGrid2").children[5].style.display = "inline";
    }
    else {
        document.getElementById("printGrid2").children[0].style.display = "none";
        document.getElementById("printGrid2").children[1].style.display = "none";
        document.getElementById("printGrid2").children[2].style.display = "none";
        document.getElementById("printGrid2").children[3].style.display = "none";
        document.getElementById("printGrid2").children[4].style.display = "none";
        document.getElementById("printGrid2").children[5].style.display = "none";

    }

    var x = document.getElementById("showorhide1");
    x.style.display = "none";
    if ((isapproved == "True") && status == "Pending Upload") {
        if (counter == counterchecker) {
            load_data_PULRequest();
            $('#ALPendingUploadRequest').modal('show');
            x.style.display = "inline";
        }
        else {
            load_data_PULRequest();
            $('#ALPendingUploadRequest').modal('show');
            x.style.display = "none";
        }
    }
    else {
        //if (counter == counterchecker) {
        //    load_data_PUL1();
        //    $('#ALPendingUpload').modal('show');
        //    x.style.display = "inline";
        //}
        //else {
        //    load_data_PUL1();
        //    $('#ALPendingUpload').modal('show');
        //    x.style.display = "none";
        //}

        load_data_PULRequest();
        $('#ALPendingUploadRequest').modal('show');
        x.style.display = "none";
    }

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/GenerateDisposalReportROSEPRequest",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // console.log(msg.d);
            $('#BodyHolder_divTurnInROSEP1').hide();
            $('#BodyHolder_divTurnInROSEP1').html(msg.d);
            $('#BodyHolder_divTurnInROSEP1').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/GenerateDisposalReportROPRequest",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //console.log(msg.d);
            $('#BodyHolder_divTurnInROP1').hide();
            $('#BodyHolder_divTurnInROP1').html(msg.d);
            $('#BodyHolder_divTurnInROP1').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/GenerateDisposalReportWMRRequest",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //    console.log(msg.d);
            $('#BodyHolder_divTurnInWMR1').hide();
            $('#BodyHolder_divTurnInWMR1').html(msg.d);
            $('#BodyHolder_divTurnInWMR1').fadeIn(200);
        }
    });
}


var GetCounter = 0;
function GetCount(ExpenseType) {
    var custodian = $('#BodyHolder_ddlCustodian').val();
    if (custodian == '--- Select Custodian Type ---') {
        custodian = 0;
    }
    $.ajax({
        async: false,
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/PropertyGetCount",
        data: "{ custodian: '" + custodian + "' ,ExpenseType: '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Property List for Disposal Request: " + msg.d);
            GetCounter = msg.d;
        }
    });
}

function RemoveAllToDisposal() {
    var expense = $('#BodyHolder_ddlItem').val();
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

    var custodian = $('#BodyHolder_ddlCustodian').val();
    swal({
        title: "Are you sure?",
        text: "Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?",
        icon: "warning",
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposalRequest.aspx/PropertyRemoveAllToDisposal",
                data: "{custodian : '" + custodian + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#disposalList').modal('hide');
                    load_dataPDisposalRequest();
                    GetCount(ExpenseType);
                    swal("Successful", "Successfully cancelled Transaction!", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    //}
}


function DisposalList() {
    $('.btn').prop('disabled', false);
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val('--- Select Approver ---').trigger('change');
    $('#BodyHolder_ddlModeOfDisposal').val('--- Select Mode of Disposal ---').trigger('change');
    var expense = $('#BodyHolder_ddlItem').val();
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
    load_dataPDisposalRequest();
    GetCount(ExpenseType);
    load_data_disposallist(ExpenseType); //add ExpenseType by nero 022624
    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;
        $('#disposalList').modal('show');
    }
}


function load_data_disposallist(ExpenseType) {
    var custodian = $('#BodyHolder_ddlCustodian').val();
    if (custodian == '--- Select Custodian Type ---') {
        custodian = 0;
    }
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyDisposalRequest.aspx/GetPropertyDisposalList",
        data: "{ custodian: '" + custodian + "' ,ExpenseType:'" + ExpenseType + "' }", //add ExpenseType by nero 022624
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#disposallist').hide();
            $('#disposallist').innerHTML = "";
            $('#disposallist').fadeIn(200);

            myJsonData = data;
            populateDataTableForDisposal(myJsonData);

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

function populateDataTableForDisposal(data) {
    try {
        $('#tblDisposalList').DataTable({
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
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Condition" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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

        $('#tblDisposalList').off('click');

        $('#tblDisposalList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');
            //if (confirm("Remove this Property?") == false) { return false; }
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposalRequest.aspx/PropertyRemoveToDisposal",
                data: "{pmlid: '" + pmlid + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#disposallist').hide();
                    $('#disposallist').innerHTML = "";
                    $('#disposallist').fadeIn(200);
                    load_dataPDisposalRequest();
                    var expense = $('#BodyHolder_ddlItem').val();
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

                    GetCount(ExpenseType);
                    currentRow.remove();
                    if (GetCounter == 0) {
                        document.getElementById("btnAssetItemTransfer").disabled = true;
                    }
                    else {
                        document.getElementById("btnAssetItemTransfer").disabled = false;
                    }
                }
            });
        });
    } catch (e) {
        var table = $('#tblDisposalList').DataTable();
        table
            .clear()
            .draw();
    }
}


function GetInspectorAndCertifier() {
    var approverid = $('#BodyHolder_ddlApprover').val();
    if ($('#BodyHolder_ddlApprover').val() == "--- Select Approver ---")
        approverid = 0;
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/GetInspector",
        data: " { userid : '" + approverid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (approverid == 0) {
                var ddlInspector = $("[id*=ddlInspector]");
                ddlInspector.empty().append("<option selected='selected' value='0'>--- Select Inspector ---</option>");
                ddlInspector.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
            else {
                var ddlInspector = $("[id*=ddlInspector]");
                ddlInspector.empty().append("<option selected='selected' value='0'>--- Select Inspector ---</option>");
                $.each(msg.d, function () {
                    ddlInspector.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/GetCertifier",
        data: " { userid : '" + approverid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (approverid == 0) {
                var ddlCertifier = $("[id*=ddlCertifier]");
                ddlCertifier.empty().append("<option selected='selected' value='0'>--- Select Certifier ---</option>");
                ddlCertifier.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
            else {
                var ddlCertifier = $("[id*=ddlCertifier]");
                ddlCertifier.empty().append("<option selected='selected' value='0'>--- Select Certifier ---</option>");
                $.each(msg.d, function () {
                    ddlCertifier.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            }
        }
    });
}



var ddlconditioncounter = 0;
function checkddlcondition() {
    $(".ddlcondition").each(function () {
        if (this.value == "Select Condition") { //obviously do all the checking
            //found a select matching the conditions
            //Do stuff!
            //return false; //this exits the each loop ^^
            ddlconditioncounter = ddlconditioncounter + 1;
        }
    });
}

function changeCondition(recid, ddlconditionid) {
    var condition = $("#ddlCondition" + ddlconditionid).val();
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/changeCondition",
        data: "{recid : '" + recid + "',condition: '" + condition + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}


function ConfirmAssetDisposal() {
    ddlconditioncounter = 0;
    checkddlcondition();
    $('.btn').prop('disabled', true);
    var approver = $('#BodyHolder_ddlApprover').val();
    var inspector = $('#BodyHolder_ddlInspector').val();
    var certifier = $('#BodyHolder_ddlCertifier').val();
    var custodian = $('#BodyHolder_ddlCustodian').val();
    if (custodian == '--- Select Custodian Type ---') {
        custodian = 0;
    }
    var alertMsg;
    var expense = $('#BodyHolder_ddlItem').val();
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

    if (needsapproval == "True") {
        if (ddlconditioncounter != 0 || $('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_ddlInspector').val() == '0' || $('#BodyHolder_ddlCertifier').val() == '0' || $('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = '';
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
                alertMsg += "\nApprover";
            if ($('#BodyHolder_ddlInspector').val() == '0')
                alertMsg += "\nInspector";
            if ($('#BodyHolder_ddlCertifier').val() == '0')
                alertMsg += "\nCertifier";
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---')
                alertMsg += "\nMode of Disposal";
            if (ddlconditioncounter != 0)
                alertMsg += "\nCondition";
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else {
            //if (confirm("Are you sure you want to CONFIRM this transaction?") == false) {
            //    $('.btn').prop('disabled', false);
            //    return false;
            //}
            swal({
                title: "Are you sure?",
                text: "Are you sure you want to confirm this transaction?",
                icon: "warning",
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $(".se-pre-con").fadeIn();
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyDisposalRequest.aspx/ConfirmPropertyDisposal",
                        data: "{ approverid : '" + approver + "',inspectorid: '0', certifierid: '0',modeofdisposal: '" + $('#BodyHolder_ddlModeOfDisposal').val() + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "', custodianid : '" + custodian + "', ExpenseType : '" + ExpenseType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested For Disposal and Pending For Approval/Upload.\n\nMail not sent. Please check you mail settings.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Approval.\n\nMail not sent. Check you mail settings.", "success");
                                load_dataPDisposalRequest();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else if (msg.d == "okay") {
                                //alert('Successfully Requested For Disposal and Pending For Approval/Upload');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Approval.", "success");
                                load_dataPDisposalRequest();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested For Disposal and Pending For Approval/Upload.\n\nMail not sent. Approver has no email.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Approval.\n\nMail not sent. Approver has no email.", "success");
                                load_dataPDisposalRequest();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                    $('.btn').prop('disabled', false);
                }
            });
        }
    }
    else {
        if (ddlconditioncounter != 0 || $('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = ' ';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---')
                alertMsg += "\nMode of Disposal";
            if (ddlconditioncounter != 0)
                alertMsg += "\nCondition";
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else {
            swal({
                title: "Are you sure?",
                text: "Are you sure you want to confirm this transaction?",
                icon: "warning",
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $(".se-pre-con").fadeIn();
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyDisposalRequest.aspx/ConfirmPropertyDisposal",
                        data: "{approverid : '0',inspectorid: '0', certifierid: '0',modeofdisposal: '" + $('#BodyHolder_ddlModeOfDisposal').val() + "',  remarks : '" + $('#BodyHolder_txtRemarks').val() + "', custodianid : '" + custodian + "' , ExpenseType : '" + ExpenseType + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested for Disposal.\n\nMail not sent. Please check you mail settings at SYSTEM > MISCELLANEOUS.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Verification.", "success");
                                load_dataPDisposalRequest();
                                //load_data_disposallist();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested for Disposal.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Verification.", "success");
                                load_dataPDisposalRequest();
                                //load_data_disposallist();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
}



function load_data_Pendingupload() {
    //var custodian = $('#ddlCustodian').val();
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyDisposalRequest.aspx/GetPendingUploadList",
        data: "{ search : '" + search + "'}",
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
                { "data": "ModeOfDisposal" },
                { "data": "DateOfTransaction" },
                { "data": "Status" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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

        $('#tblPendingUpload').on('click', 'a.btn_Cancel', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var disposalnumber = currentRow.find("td:eq(0)").text();


            var textareatt = document.createElement("textarea");
            textareatt.type = "text";
            textareatt.placeholder = "Remarks";
            textareatt.maxLength = 100;
            textareatt.style.width = "100%";
            textareatt.style.height = "100px";
            textareatt.style.maxHeight = "100px";
            textareatt.style.minHeight = "100px";
            textareatt.style.maxWidth = "100%";
            textareatt.style.minWidth = "100%";
            textareatt.id = "canceldisposal";

            swal({
                title: "Are you sure?",
                text: "Are you sure you want to cancel this transaction?",
                icon: "warning",
                reverseButtons: false,
                content: textareatt,
                buttons: {
                    cancel: true,
                    confirm: true,
                },

                showCancelButton: true,
                closeOnConfirm: false,
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    if (isConfirm === null) return false;

                    var textremarks = $('#canceldisposal').val();

                    if (textremarks === "") {
                        swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                        return false
                    }
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyDisposalRequest.aspx/CancelPropertyDisposal",
                        data: "{disposalno: '" + disposalnumber + "', remarks: '" + textremarks + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#pendinguploadlist').hide();
                            $('#pendinguploadlist').innerHTML = "";
                            $('#pendinguploadlist').fadeIn(200);
                            load_dataPDisposalRequest();
                            currentRow.remove();
                            //alert('Property Disposal Transaction successfully cancelled.');
                            swal("Successful", "Property Disposal Transaction successfully cancelled.", "success");
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        });
    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}


var needsapproval = "False";
function IfDisposalNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyDisposalRequest.aspx/IfDisposalNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var x = document.getElementById("approvers");
            var y = document.getElementById("withorwithoutyou");
            var z = document.getElementById("withorwithoutyou1");
            if (msg.d == "True") {
                x.style.display = 'inline';
                y.style.display = 'inline';
                z.style.display = 'none';
            }
            else {
                x.style.display = 'none';
                y.style.display = 'none';
                z.style.display = 'inline';
            }
        }
    });
}


function load_data_PULRequest() {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyDisposalRequest.aspx/GetPLPendingUploadListRequest",
        data: "{disposalno: '" + disposalnumberrequest + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pul1Request').hide();
            $('#pul1Request').innerHTML = "";
            $('#pul1Request').fadeIn(200);

            myJsonData = data;

            populateDataTablePULRequest(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePULRequest(data) {
    try {
        $('#tblPUL1Request').DataTable({
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
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Status" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
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

        $('#tblPUL1Request').off('click');
    } catch (e) {
        var table = $('#tblPUL1Request').DataTable();
        table
            .clear()
            .draw();
    }
}