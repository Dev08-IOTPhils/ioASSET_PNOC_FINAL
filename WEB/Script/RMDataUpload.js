var idForUpdate = 0;

$(document).ready(function () {
    load_data_propertyuploadapproval();
    load_data_stockuploadapproval();
    load_data_useruploadapproval();
    $("#tblPropertyUploadDetails").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
    });

    $('#tblPropertyUpload').DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
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
        'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
    });

    $('#tblStockUpload').DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
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
        'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
    });

    $('#tblUserUpload').DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
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
        'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
    });
});

function load_data_propertyuploadapproval() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyUploadApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyupload').hide();
            $('#propertyupload').innerHTML = "";
            $('#propertyupload').fadeIn(200);
            myJsonData = data;
            populateDataTableUploadProperty(myJsonData);
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

function populateDataTableUploadProperty(data) {
    try {
        $('#tblPropertyUpload').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DataUploadFileNameProperty" },
                { "data": "UploadBy" },
                { "data": "UploadDate" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Upload'
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
        $('#tblPropertyUpload').off('click');
    } catch (e) {
        var table = $('#tblPropertyUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var BatchName = '';
function viewPropertyUploadDetails(batchname) {
    $('#BodyHolder_txtBatchName').val(batchname);
    BatchName = batchname;

    load_data_propertyuploaddetails(batchname);

    $('#viewPropertyUploadDetails').modal('show');
}

function load_data_propertyuploaddetails(batchname) {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyUploadApprovalDetails",
        data: "{batchname: '" + batchname + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyuploaddetails').hide();
            $('#propertyuploaddetails').innerHTML = "";
            $('#propertyuploaddetails').fadeIn(200);
            myJsonData = data;
            populateDataTablePUD(myJsonData);
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

function populateDataTablePUD(data) {
    try {
        $('#tblPropertyUploadDetails').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "lengthMenu": [20, 40, 60, 80, 1000],
            "pageLength": 20,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                //{ "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "SerialNo" },
                { "data": "Description" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        $('#tblPropertyUploadDetails').off('click');
    } catch (e) {
        var table = $('#tblPropertyUploadDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApprovePropertyUpload() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this transaction?",
        icon: "warning",
        reverseButtons: false,
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
                url: "Home/RequestManagement.aspx/ApprovePropertyUpload",
                data: "{batchname:'" + BatchName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_propertyuploadapproval();
                        $('#viewPropertyUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.');
                        swal("Success", "Property Upload successfully APPROVED.", "success");
                    }
                    else {
                        load_data_propertyuploadapproval();
                        $('#viewPropertyUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Upload successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyUpload() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this transaction?",
        icon: "warning",
        reverseButtons: false,
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
                url: "Home/RequestManagement.aspx/DeclinePropertyUpload",
                data: "{batchname:'" + BatchName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_propertyuploadapproval();
                        $('#viewPropertyUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.');
                        swal("Success", "Property Upload successfully Declined.", "success");
                    }
                    else {
                        load_data_propertyuploadapproval();
                        $('#viewPropertyUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Upload successfully Declined.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}
var BatchName1 = '';
function ApproveStockUpload() {
    //alert(BatchName1);
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this transaction?",
        icon: "warning",
        reverseButtons: false,
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
                url: "Home/RequestManagement.aspx/ApproveStockUpload",
                data: "{batchname:'" + BatchName1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_stockuploadapproval();
                        $('#viewStockUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.');
                        swal("Success", "Supply upload successfully approved.", "success");
                    }
                    else {
                        load_data_stockuploadapproval();
                        $('#viewStockUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Supply upload successfully approved.\n\nMail not Sent. Please check mail setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}
function DeclineStockUpload() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to decline this transaction?",
        icon: "warning",
        reverseButtons: false,
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
                url: "Home/RequestManagement.aspx/DeclineStockUpload",
                data: "{batchname:'" + BatchName1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_stockuploadapproval();
                        $('#viewStockUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.');
                        swal("Success", "Supply upload successfully declined.", "success");
                    }
                    else {
                        load_data_stockuploadapproval();
                        $('#viewStockUploadDetails').modal('hide');
                        //alert('Property Issuance successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Supply upload successfully declined.\n\nMail not sent. Please check mail setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}
////////////////////////////////////////////////////////////////
///////////////////////////////STOCK////////////////////////////
////////////////////////////////////////////////////////////////

function load_data_stockuploadapproval() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetStockUploadApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockupload').hide();
            $('#stockupload').innerHTML = "";
            $('#stockupload').fadeIn(200);
            myJsonData = data;
            populateDataTableUploadStock(myJsonData);
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

function populateDataTableUploadStock(data) {
    try {
        $('#tblStockUpload').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DataUploadFileNameStock" },
                { "data": "UploadBy" },
                { "data": "UploadDate" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Upload'
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
        $('#tblStockUpload').off('click');
    } catch (e) {
        var table = $('#tblStockUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

function viewStockUploadDetails(batchname) {
    BatchName1 = batchname;
    $('#BodyHolder_txtStockBatchName').val(batchname);

    load_data_stockuploaddetails(batchname);

    $('#viewStockUploadDetails').modal('show');
}

function load_data_stockuploaddetails(batchname) {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetStockUploadApprovalDetails",
        data: "{batchname: '" + batchname + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockuploaddetails').hide();
            $('#stockuploaddetails').innerHTML = "";
            $('#stockuploaddetails').fadeIn(200);
            myJsonData = data;
            populateDataTableSUD(myJsonData);
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

function populateDataTableSUD(data) {
    try {
        $('#tblStockUploadDetails').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        $('#tblStockUploadDetails').off('click');
    } catch (e) {
        var table = $('#tblStockUploadDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

////////////////////////////////////////////////////////////////
///////////////////////////////USER/////////////////////////////
////////////////////////////////////////////////////////////////

function ApproveUserUpload1() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this transaction?",
        icon: "warning",
        reverseButtons: false,
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
                url: "Home/RequestManagement.aspx/ApproveUserUpload",
                data: "{batchname:'" + BatchNameUser + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "0") {
                        load_data_useruploadapproval();
                        $('#viewUserUploadDetails').modal('hide');
                        swal("Warning", "You already consume the maximum number of Custodian Type User for your License.", "warning");
                    }
                    else if (msg.d == "2") {
                        load_data_useruploadapproval();
                        $('#viewUserUploadDetails').modal('hide');
                        swal("Warning", "You already consume the maximum number of Administrator User for your License.", "warning");
                    }
                    else {
                        load_data_useruploadapproval();
                        $('#viewUserUploadDetails').modal('hide');
                        swal("Success", "Supply upload successfully approved", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineUserUpload1() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to DECLINE this transaction?",
        icon: "warning",
        reverseButtons: false,
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
                url: "Home/RequestManagement.aspx/DeclineUserUpload",
                data: "{batchname:'" + BatchNameUser + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_useruploadapproval();
                        $('#viewUserUploadDetails').modal('hide');
                        swal("Success", "User Upload successfully DECLINED.", "success");
                    }
                    else {
                        load_data_useruploadapproval();
                        $('#viewUserUploadDetails').modal('hide');
                        swal("Success", "User Upload successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function load_data_useruploadapproval() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetUserUploadApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#userupload').hide();
            $('#userupload').innerHTML = "";
            $('#userupload').fadeIn(200);
            myJsonData = data;
            populateDataTableUploadUser(myJsonData);
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

function populateDataTableUploadUser(data) {
    try {
        $('#tblUserUpload').DataTable({
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
                { "data": "DataUploadFileNameUser" },
                { "data": "UploadBy" },
                { "data": "UploadDate" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - User Upload'
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
        $('#tblUserUpload').off('click');
    } catch (e) {
        var table = $('#tblUserUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var BatchNameUser = '';
function viewUserUploadDetails(batchname) {
    $('#BodyHolder_txtUserBatchName').val(batchname);
    BatchNameUser = batchname;

    load_data_useruploaddetails(batchname);

    $('#viewUserUploadDetails').modal('show');
}

function load_data_useruploaddetails(batchname) {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetUserUploadApprovalDetails",
        data: "{batchname: '" + batchname + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#useruploaddetails').hide();
            $('#useruploaddetails').innerHTML = "";
            $('#useruploaddetails').fadeIn(200);
            myJsonData = data;
            populateDataTableUUD(myJsonData);
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

function populateDataTableUUD(data) {
    try {
        $('#tblUserUploadDetails').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,


            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "FirstName" },
                { "data": "EmailAddress" },
                { "data": "UserName" },
                { "data": "EmployeeNo" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        $('#tblUserUploadDetails').off('click');
    } catch (e) {
        var table = $('#tblUserUploadDetails').DataTable();
        table
            .clear()
            .draw();
    }
}