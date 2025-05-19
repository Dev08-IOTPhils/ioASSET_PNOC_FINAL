$(document).ready(function () {
    $('.select2').select2();

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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
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
    $('#tblPropertyOnMaintenance').DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
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

    load_data();
    load_data_onmaintenance();

    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPropertyOnMaintenance").DataTable().clear().draw();
});

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        return false;
    }
}

function EnterKey1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_onmaintenance();
        return false;
    }
}

var idForUpdate = 0;
var propertynumber = '';
var Remarks = '';
var pmlid;

function load_data() {
    var search = "";

    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequestForMaintenance.aspx/GetList",
        data: "{ search: '" + search + "'}",
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
        $('#tblAssetList').DataTable({
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
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "DateOfMaintenance" },
                { "data": "MaintenanceLeadTime" },
                { "data": "Action" }
            ],
            "rowCallback": function (row, data, index) {
                if (parseInt(data.MaintenanceLeadTime) == 0) {
                    $('td', row).css('color', 'Green');
                }
                if (parseInt(data.MaintenanceLeadTime) < 0) {
                    $('td', row).css('color', 'Red');
                }
            }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
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

        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(1)").text();
            pmlid = $(this).attr('pmlid');
            var description = currentRow.find("td:eq(2)").text();
            propertynumber = currentRow.find("td:eq(1)").text();
            document.getElementById('lblPropertyDetails').innerHTML = propertyno + " - " + description;

            $('#ModalToMaintenance').modal('show');
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function AddToMaintenance() {
    Remarks = $('#BodyHolder_txtRemarks').val();

    if ($('#BodyHolder_txtRemarks').val().trim() == '') {
        alertMsg = ' ';
        if ($('#BodyHolder_txtRemarks').val() == '')
            alertMsg += '\nRemarks';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
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
                $.ajax({
                    type: "POST",
                    url: "Operation_Module/PropertyRequestForMaintenance.aspx/AddToMaintenanceRequest",
                    data: " { id: '" + idForUpdate + "',pmlid : '" + pmlid + "', remarks: '" + Remarks + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#ModalToMaintenance").modal("hide");
                        swal("Successful", "Property Successfully Added for Maintenance!", "success");
                        //alert('Property Successfully Added for Maintenance!');
                        load_data();
                        load_data_onmaintenance();
                        clear();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
    //}
}

function load_data_onmaintenance() {
    var search = "";

    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequestForMaintenance.aspx/GetPropertyOnMaintenance",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertyonmaintenance').hide();
            $('#propertyonmaintenance').innerHTML = "";
            $('#propertyonmaintenance').fadeIn(200);

            myJsonData = data;
            populateDataTableOnMaintenance(myJsonData);

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

function populateDataTableOnMaintenance(data) {
    try {
        $('#tblPropertyOnMaintenance').DataTable({
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
                { "data": "CreatedBy" },
                { "data": "DateOfCreation" },
                { "data": "NoOfDaysFromCreation" },
                { "data": "NextStep" },
                { "data": "PercentageOfCompletion" },
                { "data": "MaintenanceRemarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request for Maintenance'
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

        $('#tblPropertyOnMaintenance').off('click');

        $('#tblPropertyOnMaintenance').on('click', 'a.btn_Edit', function (e) {
            e.preventDefault();
        });

        $('#tblPropertyOnMaintenance').on('click', 'a.percent_design', function (e) {
            e.preventDefault();
        });

        $('#tblPropertyOnMaintenance').on('click', 'a.btn_Stop', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPropertyOnMaintenance').DataTable();
        table
            .clear()
            .draw();
    }
}

function CancelMaintenanceRequest(recid) {
    //if (confirm("Are you sure you want to Cancel this Property on Maintenance Request?") == false)
    //    return false;
    swal({
        title: "Cancel Transaction",
        text: "Are you sure you want to Cancel this Property on Maintenance Request?",
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
                url: "Operation_Module/PropertyRequestForMaintenance.aspx/CancelMaintenanceRequest",
                data: " { recid: '" + recid + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_onmaintenance();
                    $("#ModalToMaintenance").modal("hide");
                    swal("Successful", "Property Successfully Cancelled the Maintenance Request.", "success");
                    //alert('Property Successfully Cancelled the Maintenance Request.');
                    clear();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}