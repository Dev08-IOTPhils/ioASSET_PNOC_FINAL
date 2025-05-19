var idForUpdate = 0;

$(document).ready(function () {
    load_data_PropertyDisposal();
    load_data_PropertyDisposalRequest();
    //load_data_PropertyInspectionDisposal();
    //load_data_PropertyCertificationDisposal();
});

function load_data_PropertyDisposal() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPendingApprovalPropertyDisposal",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertydisposal').hide();
            $('#propertydisposal').innerHTML = "";
            $('#propertydisposal').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadDisposal(myJsonData);

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

function populateDataTablePendingUploadDisposal(data) {
    try {
        $('#tblPropertyDisposal').DataTable({
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
                { "data": "TransactionType" },
                { "data": "DisposalNumber" },
                { "data": "TransactorName" },
                { "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
                { "data": "Approver" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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

        $('#tblPropertyDisposal').off('click');

        $('#tblPropertyDisposal').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyDisposal').DataTable();
        table
            .clear()
            .draw();
    }
}


function load_data_PropertyDisposalRequest() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPendingApprovalPropertyDisposalRequest",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertydisposalRequest').hide();
            $('#propertydisposalRequest').innerHTML = "";
            $('#propertydisposalRequest').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadDisposalRequest(myJsonData);

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

function populateDataTablePendingUploadDisposalRequest(data) {
    try {
        $('#tblPropertyDisposalRequest').DataTable({
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
                { "data": "DisposalNumber" },
                { "data": "TransactorName" },
                { "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
                { "data": "Approver" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal Request'
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

        $('#tblPropertyDisposalRequest').off('click');

        $('#tblPropertyDisposalRequest').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyDisposalRequest').DataTable();
        table
            .clear()
            .draw();
    }
}


function load_data_PropertyInspectionDisposal() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/DisposalPendingUploadViewInspection",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyinspectiondisposal').hide();
            $('#propertyinspectiondisposal').innerHTML = "";
            $('#propertyinspectiondisposal').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadDisposalInspection(myJsonData);

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

function populateDataTablePendingUploadDisposalInspection(data) {
    try {
        $('#tblPropertyInspectionDisposal').DataTable({
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
                { "data": "DisposalNumber" },
                { "data": "TransactorName" },
                { "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
                { "data": "Approver" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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

        $('#tblPropertyInspectionDisposal').off('click');

        $('#tblPropertyInspectionDisposal').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyInspectionDisposal').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_PropertyCertificationDisposal() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/DisposalPendingUploadViewCertify",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertycertificationdisposal').hide();
            $('#propertycertificationdisposal').innerHTML = "";
            $('#propertycertificationdisposal').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadDisposalCertify(myJsonData);

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

function populateDataTablePendingUploadDisposalCertify(data) {
    try {
        $('#tblPropertyCertificationDisposal').DataTable({
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
                { "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
                { "data": "Approver" },
                { "data": "Status" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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

        $('#tblPropertyCertificationDisposal').off('click');

        $('#tblPropertyCertificationDisposal').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyCertificationDisposal').DataTable();
        table
            .clear()
            .draw();
    }
}

var disposalrequestnumber = '';
function ViewDisposalDetailsRequest(disposalno, transactorname, createddate, isapproved) {
    disposalrequestnumber = disposalno;
    document.getElementById('lblDisposalNoRequest').innerHTML = disposalno;
    document.getElementById('lblDisposedByRequest').innerHTML = transactorname
    document.getElementById('lblDisposedDateRequest').innerHTML = createddate;

    load_data_PropertyDisposalDetailsRequest();
    $('#viewDisposalRequestDetailsRequest').modal('show');
}

var disposalnumber = '';
var remarkS = '';
function ViewDisposalDetails(disposalno, transactorname, createddate, isapproved, Type, remarks) {
    disposalnumber = disposalno;
    remarkS = remarks;
    if (Type == 'Junk Disposal') {
        load_data_PropertyJDisposalDetails();
        $('#viewJDisposalRequestDetails').modal('show');
    } else {
        document.getElementById('lblDisposalNo').innerHTML = disposalno;
        document.getElementById('lblDisposedBy').innerHTML = transactorname
        document.getElementById('lblDisposedDate').innerHTML = createddate;

        load_data_PropertyDisposalDetails();
        $('#viewDisposalRequestDetails').modal('show');
    }
}

var inspectiondisposalnumber = '';
function ViewDisposalDetailsInspection(disposalno, transactorname, createddate, isapproved) {
    inspectiondisposalnumber = disposalno;
    document.getElementById('lblInspectionDisposalNo').innerHTML = disposalno;
    document.getElementById('lblInspectionDisposedBy').innerHTML = transactorname
    document.getElementById('lblInspectionDisposedDate').innerHTML = createddate;

    load_data_PropertyDisposalDetailsInspection();
    $('#viewDisposalRequestDetailsInspection').modal('show');
}

var certificationdisposalnumber = '';
function ViewDisposalDetailsCertification(disposalno, transactorname, createddate, isapproved) {
    certificationdisposalnumber = disposalno;
    document.getElementById('lblCertificationDisposalNo').innerHTML = disposalno;
    document.getElementById('lblCertificationDisposedBy').innerHTML = transactorname
    document.getElementById('lblCertificationDisposedDate').innerHTML = createddate;

    load_data_PropertyDisposalDetailsCertification();
    $('#viewDisposalRequestDetailsCertification').modal('show');
}

function load_data_PropertyDisposalDetailsRequest() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyDisposalListRequest",
        data: "{disposalno: '" + disposalrequestnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#disposaldetailsRequest').hide();
            $('#disposaldetailsRequest').innerHTML = "";
            $('#disposaldetailsRequest').fadeIn(200);
            myJsonData = data;

            populateDataTableDisposalDetailsRequest(myJsonData);

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

function populateDataTableDisposalDetailsRequest(data) {
    try {
        $('#tblDisposalDetailsRequest').DataTable({
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
                { "data": "Status" },
                { "data": "Condition" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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

        $('#tblDisposalDetailsRequest').off('click');
    } catch (e) {
        var table = $('#tblDisposalDetailsRequest').DataTable();
        table
            .clear()
            .draw();
    }
}
function load_data_PropertyDisposalDetails() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyDisposalList",
        data: "{disposalno: '" + disposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#disposaldetails').hide();
            $('#disposaldetails').innerHTML = "";
            $('#disposaldetails').fadeIn(200);
            myJsonData = data;

            populateDataTableDisposalDetails(myJsonData);

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

function populateDataTableDisposalDetails(data) {
    try {
        $('#tblDisposalDetails').DataTable({
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
                { "data": "Status" },
                { "data": "Condition" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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

        $('#tblDisposalDetails').off('click');
    } catch (e) {
        var table = $('#tblDisposalDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_PropertyJDisposalDetails() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyJDisposalList",
        data: "{disposalno: '" + disposalnumber + "',remarks: '" + remarkS + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#jdisposaldetails').hide();
            $('#jdisposaldetails').innerHTML = "";
            $('#jdisposaldetails').fadeIn(200);
            myJsonData = data;

            populateDataTableJDisposalDetails(myJsonData);

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

function populateDataTableJDisposalDetails(data) {
    try {
        $('#tblJDisposalDetails').DataTable({
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
                { "data": "TransactionNo" },
                { "data": "Remarks" },
                { "data": "SupportingDocument" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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

        $('#tblJDisposalDetails').off('click');
    } catch (e) {
        var table = $('#tblJDisposalDetails').DataTable();
        table
            .clear()
            .draw();
    }
}
////////inspection
function load_data_PropertyDisposalDetailsInspection() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyDisposalList",
        data: "{disposalno: '" + inspectiondisposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#inspectiondisposaldetails').hide();
            $('#inspectiondisposaldetails').innerHTML = "";
            $('#inspectiondisposaldetails').fadeIn(200);

            myJsonData = data;

            populateDataTableDisposalDetailsInspection(myJsonData);

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

function populateDataTableDisposalDetailsInspection(data) {
    try {
        $('#tblInspectionDisposalDetails').DataTable({
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
                { "data": "Status" },
                { "data": "Condition" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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
        $('#tblInspectionDisposalDetails').off('click');
    } catch (e) {
        var table = $('#tblInspectionDisposalDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

////////certification
function load_data_PropertyDisposalDetailsCertification() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetPropertyDisposalList",
        data: "{disposalno: '" + certificationdisposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#certificationdisposaldetails').hide();
            $('#certificationdisposaldetails').innerHTML = "";
            $('#certificationdisposaldetails').fadeIn(200);
            myJsonData = data;

            populateDataTableDisposalDetailsCertification(myJsonData);

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

function populateDataTableDisposalDetailsCertification(data) {
    try {
        $('#tblCertificationDisposalDetails').DataTable({
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
                { "data": "Status" },
                { "data": "Condition" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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
        $('#tblCertificationDisposalDetails').off('click');
    } catch (e) {
        var table = $('#tblCertificationDisposalDetails').DataTable();
        table
            .clear()
            .draw();
    }
}
//Disposal Request

function ApprovePropertyDisposalRequest() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this Property Disposal Request?",
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
                url: "Home/RequestManagement.aspx/ApprovePropertyDisposalRequest",
                data: "{disposalno:'" + disposalrequestnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyDisposalRequest();
                        $('#viewDisposalRequestDetailsRequest').modal('hide');
                        swal("Success", "Property Disposal Request successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_PropertyDisposalRequest();
                        $('#viewDisposalRequestDetailsRequest').modal('hide');
                        swal("Success", "Property Disposal Request successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    else {
                        load_data_PropertyDisposalRequest();
                        $('#viewDisposalRequestDetailsRequest').modal('hide');
                        swal("Success", "Property Disposal Request successfully APPROVED.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyDisposalRequest() {
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
    textareatt.id = "declinegoodsreceivetextall";

    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
    }).then((value) => {
        if (value === null) return false;

        var textremarks = $('#declinegoodsreceivetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }

        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclinePropertyDisposalRequest",
            data: "{disposalno:'" + disposalrequestnumber + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyDisposalRequest();
                    $('#viewDisposalRequestDetailsRequest').modal('hide');
                    swal("Success", "Property Disposal successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_PropertyDisposalRequest();
                    $('#viewDisposalRequestDetailsRequest').modal('hide');
                    swal("Success", "Property Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_PropertyDisposalRequest();
                    $('#viewDisposalRequestDetailsRequest').modal('hide');
                    swal("Success", "Property Disposal successfully DECLINED.\n\nMail not Sent. ", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}

///////////////////////////////////////////////approval and decline
function ApprovePropertyDisposal() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this Property Disposal?",
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
                url: "Home/RequestManagement.aspx/ApprovePropertyDisposal",
                data: "{disposalno:'" + disposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyDisposal();
                        // load_data_PropertyInspectionDisposal();
                        //    load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetails').modal('hide');
                        //alert('Property Disposal successfully APPROVED.');
                        swal("Success", "Property Disposal successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //  load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetails').modal('hide');
                        //alert('Property Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    else {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //  load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetails').modal('hide');
                        //alert('Property Disposal successfully APPROVED.\n\nMail not Sent. Inspector has no email.');
                        swal("Success", "Property Disposal successfully APPROVED. ", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyDisposal() {
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
    textareatt.id = "declinegoodsreceivetextall";

    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
    }).then((value) => {
        if (value === null) return false;

        var textremarks = $('#declinegoodsreceivetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }

        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclinePropertyDisposal",
            data: "{disposalno:'" + disposalnumber + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyDisposal();
                    //   load_data_PropertyInspectionDisposal();
                    //   load_data_PropertyCertificationDisposal();
                    $('#viewDisposalRequestDetails').modal('hide');
                    swal("Success", "Property Disposal successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_PropertyDisposal();
                    //  load_data_PropertyInspectionDisposal();
                    //   load_data_PropertyCertificationDisposal();
                    $('#viewDisposalRequestDetails').modal('hide');
                    //alert('Property Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.');
                    swal("Success", "Property Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_PropertyDisposal();
                    //  load_data_PropertyInspectionDisposal();
                    //  load_data_PropertyCertificationDisposal();
                    $('#viewDisposalRequestDetails').modal('hide');
                    //alert('Property Disposal successfully DECLINED.\n\nMail not Sent. Transactor has no email.');
                    swal("Success", "Property Disposal successfully DECLINED.\n\nMail not Sent. ", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}



function ApproveJDisposal() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this Junk Disposal?",
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
                url: "Home/RequestManagement.aspx/ApproveJDisposal",
                data: "{disposalno:'" + disposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyDisposal();
                        // load_data_PropertyInspectionDisposal();
                        //    load_data_PropertyCertificationDisposal();
                        $('#viewJDisposalRequestDetails').modal('hide');
                        //alert('Property Disposal successfully APPROVED.');
                        swal("Success", "Junk Disposal successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //  load_data_PropertyCertificationDisposal();
                        $('#viewJDisposalRequestDetails').modal('hide');
                        //alert('Property Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Junk Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    else {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //  load_data_PropertyCertificationDisposal();
                        $('#viewJDisposalRequestDetails').modal('hide');
                        //alert('Property Disposal successfully APPROVED.\n\nMail not Sent. Inspector has no email.');
                        swal("Success", "Junk Disposal successfully APPROVED. ", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineJDisposal() {
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
    textareatt.id = "declinegoodsreceivetextall";

    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
    }).then((value) => {
        if (value === null) return false;

        var textremarks = $('#declinegoodsreceivetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }

        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclineJDisposal",
            data: "{disposalno:'" + disposalnumber + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyDisposal();
                    //   load_data_PropertyInspectionDisposal();
                    //   load_data_PropertyCertificationDisposal();
                    $('#viewJDisposalRequestDetails').modal('hide');
                    swal("Success", "Junk Disposal successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_PropertyDisposal();
                    //  load_data_PropertyInspectionDisposal();
                    //   load_data_PropertyCertificationDisposal();
                    $('#viewJDisposalRequestDetails').modal('hide');
                    //alert('Property Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.');
                    swal("Success", "Junk Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_PropertyDisposal();
                    //  load_data_PropertyInspectionDisposal();
                    //  load_data_PropertyCertificationDisposal();
                    $('#viewJDisposalRequestDetails').modal('hide');
                    //alert('Property Disposal successfully DECLINED.\n\nMail not Sent. Transactor has no email.');
                    swal("Success", "Junk Disposal successfully DECLINED.\n\nMail not Sent. ", "success");
                }
                $(".se-pre-con").hide();
            }
        });
    });
}

function ApprovePropertyInspectionDisposal() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this Property Inspection Disposal?",
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
                url: "Home/RequestManagement.aspx/ApproveInspectPropertyDisposal",
                data: "{disposalno:'" + inspectiondisposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyDisposal();
                        // load_data_PropertyInspectionDisposal();
                        //    load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsInspection').modal('hide');
                        //alert('Property Inspection Disposal successfully APPROVED.');
                        swal("Success", "Property Inspection Disposal successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //     load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsInspection').modal('hide');
                        //alert('Property Inspection Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Inspection Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    else {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //   load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsInspection').modal('hide');
                        //alert('Property Inspection Disposal successfully APPROVED.\n\nMail not Sent. Certifier has no email.');
                        swal("Success", "Property Inspection Disposal successfully APPROVED.\n\nMail not Sent. Certifier has no email.", "success");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyInspectionDisposal() {
    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
                url: "Home/RequestManagement.aspx/DeclineInspectPropertyDisposal",
                data: "{disposalno:'" + inspectiondisposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyDisposal();
                        //   load_data_PropertyInspectionDisposal();
                        //    load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsInspection').modal('hide');
                        //alert('Property Inspection Disposal successfully DECLINED.');
                        swal("Success", "Property Inspection Disposal successfully DECLINED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_PropertyDisposal();
                        // load_data_PropertyInspectionDisposal();
                        //  load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsInspection').modal('hide');
                        //alert('Property Inspection Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Inspection Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    else {
                        load_data_PropertyDisposal();
                        // load_data_PropertyInspectionDisposal();
                        //   load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsInspection').modal('hide');
                        //alert('Property Inspection Disposal successfully DECLINED.\n\nMail not Sent. Transactor has no email.');
                        swal("Success", "Property Inspection Disposal successfully DECLINED.\n\nMail not Sent. Transactor has no email.", "success");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ApprovePropertyCertificationDisposal() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this Property Certification Disposal?",
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
                url: "Home/RequestManagement.aspx/ApproveCertifyPropertyDisposal",
                data: "{disposalno:'" + certificationdisposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        load_data_PropertyDisposal();
                        // load_data_PropertyInspectionDisposal();
                        //       load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsCertification').modal('hide');
                        //alert('Property Certification Disposal successfully APPROVED.');
                        swal("Success", "Property Certification Disposal successfully APPROVED.", "success");
                    }
                    else if (msg.d == "sorry") {
                        load_data_PropertyDisposal();
                        //  load_data_PropertyInspectionDisposal();
                        //     load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsCertification').modal('hide');
                        //alert('Property Certification Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Property Certification Disposal successfully APPROVED.\n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    else {
                        load_data_PropertyDisposal();
                        //    load_data_PropertyInspectionDisposal();
                        //     load_data_PropertyCertificationDisposal();
                        $('#viewDisposalRequestDetailsCertification').modal('hide');
                        //alert('Property Certification Disposal successfully APPROVED.\n\nMail not Sent. Transactor has no email.');
                        swal("Success", "Property Certification Disposal successfully APPROVED.\n\nMail not Sent. Transactor has no email.", "success");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclinePropertyCertificationDisposal() {
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
    textareatt.id = "declinegoodsreceivetextall";

    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
    }).then((value) => {
        if (value === null) return false;

        var textremarks = $('#declinegoodsreceivetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclineCertifyPropertyDisposal",
            data: "{disposalno:'" + certificationdisposalnumber + "', remarkS:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_PropertyDisposal();
                    //   load_data_PropertyInspectionDisposal();
                    //     load_data_PropertyCertificationDisposal();
                    $('#viewDisposalRequestDetailsCertification').modal('hide');
                    swal("Success", "Property Certification Disposal successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_PropertyDisposal();
                    //     load_data_PropertyInspectionDisposal();
                    //     load_data_PropertyCertificationDisposal();
                    $('#viewDisposalRequestDetails').modal('hide');
                    swal("Success", "Property Certification Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_PropertyDisposal();
                    //    load_data_PropertyInspectionDisposal();
                    //   load_data_PropertyCertificationDisposal();
                    $('#viewDisposalRequestDetails').modal('hide');
                    swal("Success", "Property Certification Disposal successfully DECLINED.\n\nMail not Sent. Transactor has no email.", "success");
                }
            }
        });
    });
}