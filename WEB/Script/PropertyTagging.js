$(document).ready(function () {
    $('.select2').select2();
    $("#tblAssets").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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

    $("#tblStock").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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
    $("#tblPrintJobs").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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
    $("#tblGR").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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
    //DeviceConnect();
    load_data_printjobs();
    ddlTagTypeChange();
    GetDeviceWriterStatus();
    checkRetaggingApproval();
    GetDeviceStatus();

    //GetPropertyPerGR();
    //load_data();
    //load_data_Stock();
    $("#tblAssets").DataTable().clear().draw();
    $("#tblStock").DataTable().clear().draw();
});

//Property Tagging
function ddlTagTypeChange() {
    var tag = $('#BodyHolder_ddlTagType').val();
    if (tag == '--- Select Tag ---') {
        tag = 0;
    }
    var tagtype = 0;

    //if ($('#BodyHolder_ddlTaggingOption').val() == 'Re-tagging') {
    //    $('#tab_GR').hide();
    //    $('#GRTab').hide();
    //}
    //else {
    //    $('#tab_GR').show();
    //    $('#GRTab').show();
    //}

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyTagging.aspx/GetTagByID",
        data: "{ tagid: '" + tag + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            tagtype = msg.d;
            //if (tagtype == 4) {
            //    document.getElementById("ReaderConnection").style.display = "block";
            //    document.getElementById("PrinterConnection").style.display = "none";
            //    document.getElementById("cartwheel").style.display = "none";
            //    load_data();
            //    load_data_Stock();
            //}
            //else {
            //    document.getElementById("ReaderConnection").style.display = "none";
            //    document.getElementById("PrinterConnection").style.display = "block";
            //    document.getElementById("cartwheel").style.display = "block";
            //    load_data();
            //    load_data_Stock();
            //}

            ///// New Update for RADMWare
            ///// Load Property and Stock
            load_data();
            load_data_Stock();
            GetPropertyPerGR();
        }
    });
}

function GetDeviceStatus() {
    var status = document.getElementById('lblDeviceStatus');
    var deviceid = $('#ddlPrinter').val();
    //alert(deviceid);

    document.getElementById('lblPrinterStatus').style.display = 'block';
    document.getElementById('lblDeviceStatus').style.display = 'block';
    if ($('#ddlPrinter').val() == "-1") {
        document.getElementById('lblDeviceStatus').innerHTML = "Select Device";
        status.classList.remove("text-green");
        status.classList.add("text-red");
    }
    else if ($('#ddlPrinter').val() == "0") {
        document.getElementById('lblPrinterStatus').style.display = 'none';
        document.getElementById('lblDeviceStatus').style.display = 'none';
    }
    else {
        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyTagging.aspx/GetDeviceStatus",
            data: "{ deviceid: '" + deviceid + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "OK") {
                    document.getElementById('lblDeviceStatus').innerHTML = "ONLINE";
                    status.classList.remove("text-red");
                    status.classList.add("text-green");
                    GetCount();
                }
                else {
                    document.getElementById('lblDeviceStatus').innerHTML = msg.d;
                    status.classList.remove("text-green");
                    status.classList.add("text-red");
                    GetCount();
                }
            }
        });
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
            url: "Operation_Module/PropertyTagging.aspx/GetDeviceWriterStatus",
            data: "{ deviceid: '" + deviceid + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "OK") {
                    document.getElementById('lblDeviceWriterStatus').innerHTML = "CONNECTION OK";
                    status.classList.remove("text-red");
                    status.classList.add("text-green");
                    GetCount();
                }
                else {
                    document.getElementById('lblDeviceWriterStatus').innerHTML = msg.d;
                    status.classList.remove("text-green");
                    status.classList.add("text-red");
                    GetCount();
                }
            }
        });
    }
}

function load_data() {
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag ---')
        tagtype = 0;

    var taggingoption = $('#BodyHolder_ddlTaggingOption').val();

    if (taggingoption == "Tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/PropertyAvailableForTagging",
            data: "{ tagtype : '" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                0
                $('#assetlist').hide();
                $('#assetlist').innerHTML = "";
                $('#assetlist').fadeIn(200);

                myJsonData = data;
                populateDataTableProperty(myJsonData);

                console.log(data);
                console.log('Success');

                GetCount();
            },
            error: function (e) {
                //alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    else if (taggingoption == "Re-tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/PropertyAvailableForRetagging",
            data: "{ tagtype : '" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#assetlist').hide();
                $('#assetlist').innerHTML = "";
                $('#assetlist').fadeIn(200);

                myJsonData = data;
                populateDataTableProperty(myJsonData);

                console.log(data);
                console.log('Success');

                GetCount();
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
}

function populateDataTableProperty(data) {
    try {
        $('#tblAssets').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true, select: { style: 'multi' },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Action" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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
                         $('#tblAssets').DataTable().rows().select();
                     }
                 },
                {
                    text: 'Select none',
                    action: function () {
                        $('#tblAssets').DataTable().rows().deselect();
                    }
                },
                {
                    text: 'Add to Tag/Print List',
                    action: function () {
                        var deviceid = $('#ddlPrinter').val();
                        var tagtype = $('#BodyHolder_ddlTagType').val();
                        var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
                        if (TaggingType == 'Tagging') {
                            TaggingType = 1;
                        }
                        else if (TaggingType == 'Re-tagging') {
                            TaggingType = 2;
                        }
                        else {
                            TaggingType = 0;
                        }
                        if (deviceid == 'Paper Print') {
                            deviceid = 0;
                        }

                        var count = $('#tblAssets').DataTable().rows({ selected: true }).count();
                        if (count == 0) {
                            swal("Warning", "Please select an item to add", "warning");
                        }
                        else if ($('#ddlPrinter').val() == "-1") {
                            swal("Warning", "Select a DEVICE!", "warning");
                        }
                        else {
                            var ids = $.map($('#tblAssets').DataTable().rows('.selected').data(), function (item) {
                                return item.pmlid;
                            });
                            console.log(ids);

                            ids.forEach((pmlid) => {
                                $(".se-pre-con").fadeIn(100);
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/PropertyTagging.aspx/AddToPrint",
                                    data: "{tagtype : '" + tagtype + "',assetid: '" + pmlid + "', deviceid: '" + deviceid + "', assettype: 'p', quantity : '1', TaggingType: '" + TaggingType + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        ddlTagTypeChange();
                                        $(".se-pre-con").hide();
                                    }
                                });
                                GetCount();
                                $('#tblAssets').DataTable()
                                .rows('.selected')
                                .remove()
                                .draw(false);
                            });

                        }
                    }
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        $('#tblAssets').off('click');
        $('#tblAssets').on('click', 'button.btn_Add', function (e) {
            e.preventDefault();
            //var currentRow = $(this).closest("tr");
            //currentRow.remove();
        });
    } catch (e) {
        var table = $('#tblAssets').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_printjobs() {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyTagging.aspx/GetPrintJobs",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#printjobs').hide();
            $('#printjobs').innerHTML = "";
            $('#printjobs').fadeIn(200);

            myJsonData = data;
            populateDataTablePrintJobs(myJsonData);

            console.log(data);
            console.log('Success');

            GetCount();
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePrintJobs(data) {
    try {
        $('#tblPrintJobs').DataTable({
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
            "searching": false,
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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
            },
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "LabelToPrint" },
                { "data": "Status" }
            ]
        });

        $('#tblPrintJobs').off('click');
    } catch (e) {
        var table = $('#tblPrintJobs').DataTable();
        table
            .clear()
            .draw();
    }
}

var isRetaggingApproval = '';
function PrintList() {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyTagging.aspx/CheckRetaggingApproval",
        data: "{ }",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            isRetaggingApproval = data.d;
        }
    });
    if (isRetaggingApproval == 'True') {
        if ($('#BodyHolder_ddlTaggingOption').val() == 'Re-tagging') {
            GetApprover();
            $('#divForRetagging').show();
        }
        else {
            $('#divForRetagging').hide();
        }
    }

    if ($("#btnCart span").text() == " 0") {
        alert('No Assets for Printing')
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;
        pagePrint();
    }
}

function pagePrint() {
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (counT == '') {
        GetCount();
    }
    if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag ---' || $('#ddlPrinter').val() == '-1' || $('#BodyHolder_ddlTaggingOption').val() == '--- Select Tagging type ---') {
        //alert('Please select TAG TYPE and/or PRINTER');

        swal("Warning", "Please select TAG TYPE and/or Device", "warning");
    }
    else if (counT == '0' || counT == '') {
        swal("Warning", "No Property/Supply to Tag", "warning");
    }
    else {
        if (counT != '0') {
            load_data_printlist();
            $('#PrintList').modal('show');
        }
    }
}

function load_data_printlist() {
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var deviceid = $('#ddlPrinter').val();
    if (deviceid == '0') {
        deviceid = 0;
        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyTagging.aspx/PrintPaper",
            data: "{tagtype: '" + tagtype + "' ,deviceid: '" + deviceid + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divReportTagging').hide();
                $('#BodyHolder_divReportTagging').html(msg.d);
                $('#BodyHolder_divReportTagging').fadeIn(200);
            }
        });
    }
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyTagging.aspx/GetPrintList",
        data: "{ tagtype: '" + tagtype + "', deviceid : '" + deviceid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#printlist').hide();
            $('#printlist').innerHTML = "";
            $('#printlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePrintList(myJsonData);

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

function populateDataTablePrintList(data) {
    try {
        $('#tblPrintList').DataTable({
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
                { "data": "AssetType" },
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "PropertyNumber" },
                { "data": "SerialNo" },
                { "data": "Action" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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

        $('#tblPrintList').off('click');
        $('#tblPrintList').on('click', 'button.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            currentRow.remove();
        });
    } catch (e) {
        var table = $('#tblPrintList').DataTable();
        table
            .clear()
            .draw();
    }
}

function AddToPrint(assetid) {
    var deviceid = $('#ddlPrinter').val();
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }
    //if (deviceid == '1') {
    //    deviceid = 0;
    //}

    if ($('#ddlPrinter').val() == "-1") {
        swal("Warning", "Select a DEVICE!", "warning");
    }
    else {
        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyTagging.aspx/AddToPrint",
            data: "{tagtype : '" + tagtype + "',assetid: '" + assetid + "', deviceid: '" + deviceid + "', assettype: 'p', quantity : '1', TaggingType: '" + TaggingType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ddlTagTypeChange();
            }
        });
    }
}

function addtoPrintPerGR(GRNumber) {
    var deviceid = $('#ddlPrinter').val();
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }
    //if (deviceid == '1') {
    //    deviceid = 0;
    //}

    if ($('#ddlPrinter').val() == "-1") {
        swal("Warning", "Select a DEVICE!", "warning");
    }
    else {
        $.ajax({
            type: "POST",
            async: false,
            url: "Operation_Module/PropertyTagging.aspx/getRecIDtoAddPropertyToPrint",
            data: "{ GRNumber : '" + GRNumber + "', tagtype:'" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var data = msg.d;
                var cnt = (data.split('|').length - 2);

                for (var i = 0; i <= cnt; i++) {
                    //  alert(data.split('|')[i]);
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: "Operation_Module/PropertyTagging.aspx/AddToPrint",
                        data: "{tagtype : '" + tagtype + "',assetid: '" + data.split('|')[i] + "', deviceid: '" + deviceid + "', assettype: 'p', quantity : '1', TaggingType: '" + TaggingType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            GetCount();
                        }
                    });
                }

                ddlTagTypeChange();
            }
        });
    }
}

function RemoveToPrint(assetid, assettype) {
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyTagging.aspx/RemoveToPrint",
        data: "{assetid: '" + assetid + "',assettype: '" + assettype + "',TaggingType: '" + TaggingType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ddlTagTypeChange();
            load_data_printlist();
            GetCount();
        }
    });
}

function RemoveAllToPrint() {
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var deviceid = $('#ddlPrinter').val();
    //if (confirm("Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?") == false) {
    //    return false;
    //}
    //else {
    //if (deviceid == '1') {
    //    deviceid = 0;
    //}

    swal({
        title: "Are you sure?",
        text: "Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?",
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
                url: "Operation_Module/PropertyTagging.aspx/RemoveAllToPrint",
                data: "{tagtype: '" + tagtype + "', deviceid: '" + deviceid + "', TaggingType: '" + TaggingType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    ddlTagTypeChange();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    //}
}

function ConfirmPrint() {
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var deviceid = $('#ddlPrinter').val();
    var approverID = $('#BodyHolder_ddlApprover').val() == null ? 0 : $('#BodyHolder_ddlApprover').val();
    var Remarks = $('#BodyHolder_txtRemarks').val();
    var isRetag = $('#BodyHolder_ddlTaggingOption').val() == 'Re-tagging' ? 1 : 0;
    if (isRetaggingApproval == 'True' && isRetag == 1) {
        if (approverID == 0 || Remarks == '') {
            swal("Warning", "Remarks or Approver is missing", "warning");
            return false;
        }
        else {
            if (deviceid == '0') {
                swal({
                    title: "",
                    text: "Printing/Tagging this list will be sent to selected DEVICE. Make sure that the list is correct.",
                    icon: "info",
                    reverseButtons: false,
                    buttons: [
                      'No',
                      'Yes'
                    ],
                    dangerMode: true,
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        PrintGridData();
                        $.ajax({
                            type: "POST",
                            url: "Operation_Module/PropertyTagging.aspx/ConfirmPrint",
                            data: "{tagtype: '" + tagtype + "', deviceid: '" + deviceid + "', approverID: '" + approverID + "', Remarks: '" + Remarks + "', isRetag: '" + isRetag + "', TaggingType: '" + TaggingType + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                ddlTagTypeChange();
                                //alert('success');
                                swal("Success", "Successfully sent to Device", "success");
                                load_data_printjobs();
                            }
                        });
                    } else {
                        swal("Cancelled", "Cancelled by user", "warning");
                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "Operation_Module/PropertyTagging.aspx/GetDeviceStatus",
                    data: "{ deviceid: '" + deviceid + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "OK") {
                            //document.getElementById('lblDeviceStatus').innerHTML = "ONLINE";
                            //status.classList.remove("text-red");
                            //status.classList.add("text-green");

                            swal({
                                title: "",
                                text: "Printing/Tagging this list will be sent to selected DEVICE. Make sure that the list is correct.",
                                icon: "info",
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
                                        url: "Operation_Module/PropertyTagging.aspx/ConfirmPrint",
                                        data: "{tagtype: '" + tagtype + "', deviceid: '" + deviceid + "',  approverID: '" + approverID + "', Remarks: '" + Remarks + "', isRetag: '" + isRetag + "', TaggingType: '" + TaggingType + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (msg) {
                                            ddlTagTypeChange();
                                            //alert('success');
                                            swal("Success", "Successfully sent to Device", "success");
                                            load_data_printjobs();
                                        }
                                    });
                                } else {
                                    swal("Cancelled", "Cancelled by user", "warning");
                                }
                            });
                        }
                        else {
                            swal("Warning", "Please check Device!", "warning");
                            //document.getElementById('lblDeviceStatus').innerHTML = msg.d;
                            //status.classList.remove("text-green");
                            //status.classList.add("text-red");
                        }
                    }
                });
            }
        }
    }
    else {
        if (deviceid == '0') {
            swal({
                title: "",
                text: "Printing/Tagging this list will be sent to selected DEVICE. Make sure that the list is correct.",
                icon: "info",
                reverseButtons: false,
                buttons: [
                  'No',
                  'Yes'
                ],
                dangerMode: true,
            }).then(function (isConfirm) {
                if (isConfirm) {
                    PrintGridData();
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyTagging.aspx/ConfirmPrint",
                        data: "{tagtype: '" + tagtype + "', deviceid: '" + deviceid + "', approverID: '0', Remarks: '', isRetag: '" + isRetag + "', TaggingType: '" + TaggingType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            ddlTagTypeChange();
                            //alert('success');
                            swal("Success", "Successfully sent to Device", "success");
                            load_data_printjobs();
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyTagging.aspx/GetDeviceStatus",
                data: "{ deviceid: '" + deviceid + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "OK") {
                        //document.getElementById('lblDeviceStatus').innerHTML = "ONLINE";
                        //status.classList.remove("text-red");
                        //status.classList.add("text-green");

                        swal({
                            title: "",
                            text: "Printing/Tagging this list will be sent to selected DEVICE. Make sure that the list is correct.",
                            icon: "info",
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
                                    url: "Operation_Module/PropertyTagging.aspx/ConfirmPrint",
                                    data: "{tagtype: '" + tagtype + "', deviceid: '" + deviceid + "',  approverID: '0', Remarks: '', isRetag: '" + isRetag + "', TaggingType: '" + TaggingType + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        ddlTagTypeChange();
                                        //alert('success');
                                        swal("Success", "Successfully sent to Device", "success");
                                        load_data_printjobs();
                                    }
                                });
                            } else {
                                swal("Cancelled", "Cancelled by user", "warning");
                            }
                        });
                    }
                    else {
                        swal("Warning", "Please check Device!", "warning");
                        //document.getElementById('lblDeviceStatus').innerHTML = msg.d;
                        //status.classList.remove("text-green");
                        //status.classList.add("text-red");
                    }
                }
            });
        }
    }
}

function PrintGridData() {
    var prtGrid = document.getElementById('<%=printablepage.ClientID %>');
    var prtwin = window.open(' ', ' ', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.innerHTML);
    prtwin.document.close();

    prtwin.focus();
    prtwin.print();
    prtwin.close();
}

function TagProperty(recid) {
    var deviceid = $('#BodyHolder_ddlCom').val();
    if (deviceid == 'Paper Print') {
        deviceid = 0;
    }
    if ($('#BodyHolder_ddlCom').val() == "--- Select USB Writer ---") {
        swal("Error!", "Select USB Writer", "warning");
    }
    else {
        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyTagging.aspx/TagProperty",
            data: "{recid: '" + recid + "', deviceid: '" + deviceid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ddlTagTypeChange();
                //alert('success');
                swal("Success", "Successfully sent to writer", "success");
            }
        });
    }
    //swal({
    //    title: "",
    //    text: "Printing/Tagging this list will be sent to selected PRINTER. Please make sure that the list is correct.",
    //    icon: "info",
    //    reverseButtons: false,
    //    buttons: [
    //      'No',
    //      'Yes'
    //    ],
    //    dangerMode: true,
    //}).then(function (isConfirm) {
    //    if (isConfirm) {
    //        $.ajax({
    //            type: "POST",
    //            url: "Operation_Module/PropertyTagging.aspx/ConfirmPrint",
    //            data: "{tagtype: '" + tagtype + "', deviceid: '" + deviceid + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (msg) {
    //                ddlTagTypeChange();
    //                //alert('success');
    //                swal("Success", "Successfully sent to printer", "sccess");
    //            }
    //        });
    //    } else {
    //        swal("Cancelled", "Cancelled by user", "warning");
    //    }
    //});
}
var counT = '';
function GetCount() {
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var deviceid = $('#ddlPrinter').val();
    //if (deviceid == '1') {
    //    deviceid = 0;
    //}
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyTagging.aspx/GetCount",
        data: "{ tagtype: '" + tagtype + "', deviceid : '" + deviceid + "', TaggingType : '" + TaggingType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            counT = msg.d;
            $("#btnCart span").text(" View List for Tagging/Re-tagging: " + msg.d);
            if (msg.d == 0) {
                $('#PrintList').modal('hide');
            }
            else { }
        }
    });
}

//stocks
function load_data_Stock() {
    //var tagtype = $('#BodyHolder_ddlTagType').val();
    //if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag Type ---')
    //    tagtype = 0;
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag Type ---')
        tagtype = 0;

    var taggingoption = $('#BodyHolder_ddlTaggingOption').val();
    if (taggingoption == "Tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/StockAvailableForTagging",
            data: "{ tagtype : '" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#stock').hide();
                $('#stock').innerHTML = "";
                $('#stock').fadeIn(200);

                myJsonData = data;
                populateDataTableStock(myJsonData);

                console.log(data);
                console.log('Success');

                GetCount();
            },
            error: function (e) {
                //alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    else if (taggingoption == "Re-tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/StockAvailableForRetagging",
            data: "{ tagtype : '" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#stock').hide();
                $('#stock').innerHTML = "";
                $('#stock').fadeIn(200);

                myJsonData = data;
                populateDataTableStock(myJsonData);

                console.log(data);
                console.log('Success');

                GetCount();
            },
            error: function (e) {
                //alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
}

function populateDataTableStock(data) {
    try {
        $('#tblStock').DataTable({
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
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Action" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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

        $('#tblStock').off('click');
        $('#tblStock').on('click', 'button.btn_Add', function (e) {
            e.preventDefault();
            //var currentRow = $(this).closest("tr");
            //currentRow.remove();
        });
    } catch (e) {
        var table = $('#tblStock').DataTable();
        table
            .clear()
            .draw();
    }
}

var stockid = 0;
function AddToPrintStock(assetid, itemcode) {
    stockid = assetid;
    if ($('#ddlPrinter').val() == "-1") {
        swal("Warning", "Select a DEVICE!", "warning");
    }
    else {
        $('#txtItemcode').val(itemcode);
        $('#QuantityToPrint').modal('show');
    }
}

function addtoprintstock() {
    var quantity = $('#txtQuantity').val();
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if (tagtype == '--- Select Tag ---') {
        tagtype = 0;
    }
    var deviceid = $('#ddlPrinter').val();
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }
    //if (deviceid == '1') {
    //    deviceid = 0;
    //}

    //if (deviceid == 'Paper Print') {
    //    deviceid = 0;
    //}
    if ($('#txtQuantity').val() == "0" || $('#txtQuantity').val() == "") {
        swal("Warning", "Input quantity", "warning");
    }
    else {
        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyTagging.aspx/AddToPrint",
            data: "{tagtype : '" + tagtype + "', assetid: '" + stockid + "', deviceid: '" + deviceid + "', assettype: 's', quantity : '" + quantity + "', TaggingType : '" + TaggingType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ddlTagTypeChange();
                $('#QuantityToPrint').modal('hide');
            }
        });
    }
}

function GetApprover() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyTagging.aspx/GetApprover",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlApprover = $("[id*=ddlApprover]");
            ddlApprover.empty().append("<option selected='selected' value='0'>--- Select Approver ---</option>");
            $.each(msg.d, function () {
                ddlApprover.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (msg1) {
            var ddlApprover = $("[id*=ddlApprover]");
            ddlApprover.empty().append("<option selected='selected' value='0'>--- Select Approver ---</option>");
            ddlApprover.append($("<option></option>").val(this['Value']).html(this['Text']));
        }
    });
}

function checkRetaggingApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyTagging.aspx/checkRetaggingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "True") {
                $("#RetaggingTable").show();
            }
            else {
                $("#RetaggingTable").hide();
            }
        }
    });
}

function GetPropertyPerGR() {
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag ---')
        tagtype = 0;
    var taggingoption = $('#BodyHolder_ddlTaggingOption').val();
    if (taggingoption == "Tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/AvailableForTaggingPerGR",
            data: "{ tagtype : '" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                0
                $('#assetlist').hide();
                $('#assetlist').innerHTML = "";
                $('#assetlist').fadeIn(200);

                myJsonData = data;
                populateDataTableGR(myJsonData);

                console.log(data);
                console.log('Success');

                GetCount();
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    else if (taggingoption == "Re-tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/AvailableForRetaggingPerGR",
            data: "{ tagtype : '" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#assetlist').hide();
                $('#assetlist').innerHTML = "";
                $('#assetlist').fadeIn(200);

                myJsonData = data;
                populateDataTableGR(myJsonData);

                console.log(data);
                console.log('Success');

                GetCount();
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
}

function populateDataTableGR(data) {
    try {
        $('#tblGR').DataTable({
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

                { "data": "GoodsReceiveNumber" },
                { "data": "PONumber" },
                { "data": "PRNumber" },
                { "data": "InvoiceNumber" },
                { "data": "DRNumber" },
                { "data": "Status" },
                { "data": "Action" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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

        $('#tblGR').off('click');
        $('#tblGR').on('click', 'button.btn_Add', function (e) {
            e.preventDefault();
            //var currentRow = $(this).closest("tr");
            //currentRow.remove();
        });
    } catch (e) {
        var table = $('#tblGR').DataTable();
        table
            .clear()
            .draw();
    }
}

function ViewPropertyPerGR(GRNumber) {
    var tagtype = $('#BodyHolder_ddlTagType').val();
    if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag ---')
        tagtype = 0;
    var taggingoption = $('#BodyHolder_ddlTaggingOption').val();
    if (taggingoption == "Tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/ViewPropertyTaggingPerGR",
            data: "{ GRNumber : '" + GRNumber + "', tagtype:'" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                0
                $('#assetlist').hide();
                $('#assetlist').innerHTML = "";
                $('#assetlist').fadeIn(200);

                myJsonData = data;
                populateDataTablePropertyGR(myJsonData);
                $('#modalPropertyInfo').modal('show');
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    else if (taggingoption == "Re-tagging") {
        $.ajax({
            type: 'POST',
            url: "Operation_Module/PropertyTagging.aspx/ViewPropertyForRetaggingPerGR",
            data: "{ GRNumber : '" + GRNumber + "', tagtype:'" + tagtype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#assetlist').hide();
                $('#assetlist').innerHTML = "";
                $('#assetlist').fadeIn(200);

                myJsonData = data;
                populateDataTablePropertyGR(myJsonData);
                $('#modalPropertyInfo').modal('show');
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
}

function populateDataTablePropertyGR(data) {
    try {
        $('#tblGRProperty').DataTable({
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Tagging'
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

        $('#tblGRProperty').off('click');
        $('#tblGRProperty').on('click', 'button.btn_Add', function (e) {
            e.preventDefault();
            //var currentRow = $(this).closest("tr");
            //currentRow.remove();
        });
    } catch (e) {
        var table = $('#tblGRProperty').DataTable();
        table
            .clear()
            .draw();
    }
}