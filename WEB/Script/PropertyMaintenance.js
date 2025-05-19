$(document).ready(function () {
    $('.select2').select2();

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        //endDate: '+0d',
        autoclose: true,
        onSelect: function (dateText) {
            display("Selected date: " + dateText + ", Current Selected Value= " + this.value);
            $(this).change();
        }
    }).on("change", function () {
        ChangeDate();
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        //endDate: '+0d',
        autoclose: true,
        onSelect: function (dateText) {
            display("Selected date: " + dateText + ", Current Selected Value= " + this.value);
            $(this).change();
        }
    }).on("change", function () {
        ChangeDate();
    });
    $('#txtDateTo').datepicker('setDate', today);

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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
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
    $("#tblPropertyOnMaintenance").DataTable({
        "scrollX": true
        , "searching": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
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
    $("#tblSteps").DataTable({
        "scrollX": true
        , "searching": false
        , dom: 'rtip'
        , fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        }, buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
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
        }
    });
    load_data();
    load_data_onmaintenance();

    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPropertyOnMaintenance").DataTable().clear().draw();
    $("#tblSteps").DataTable().clear().draw();

    document.getElementById("chkWithDoc").checked = false;
    var x = document.getElementById("hideAttachment");
    var y = document.getElementById("toggleAttachment");

    $('#btnComplete').fadeIn();
    $('#BodyHolder_btnSave').hide();
    x.style.display = "none";
    y.style.display = "none";

    $("#chkWithDoc").change(function () {
        if (this.checked) {
            $('#btnComplete').hide();
            $('#BodyHolder_btnSave').fadeIn();
            x.style.display = "inline";
        }
        else {
            $('#btnComplete').fadeIn();
            $('#BodyHolder_btnSave').hide();
            x.style.display = "none";
        }
    });
});

var idForUpdate = 0;
var propertynumber = '';
var taskID = 0;
var Remarks = '';
var assignto = 0;
var terminatestatus = '';
var returnrequestnumber = '';
var pmlid = 0;

function GetTaskID() {
    var taskid = $('#BodyHolder_ddlTask').val();
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyMaintenance.aspx/TaskbyID",
        data: "{ id: '" + taskid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_txtStep').val(msg.d.split('|')[4].split('-')[0]);
            $('#BodyHolder_txtNextStep').val(msg.d.split('|')[4].split('-')[1]);
        }
    });
}

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

function ChangeDate() {
    load_data();
}

function load_data() {
    var search = "";
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val();
    var date = new Date(dateto);
    var newdate = new Date(date);

    //newdate.setDate(newdate.getDate() + 1);

    //var dd = newdate.getDate(); if (dd.length == 1) { dd = "0" + dd; }
    //var mm = newdate.getMonth() + 1; if (mm.length == 1) { mm = "0" + mm; }
    //var y = newdate.getFullYear();

    //var someFormattedDate = mm + '/' + dd + '/' + y;

    //dateto = someFormattedDate;

    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyMaintenance.aspx/GetList",
        data: "{ search: '" + search + "',FilterDateFrom: '" + datefrom + "',FilterDateTo: '" + dateto + "'}",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
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
            $('#BodyHolder_txtRemarks').val('');
            e.preventDefault();
            defaultModalMaintenance()

            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(1)").text();
            var description = currentRow.find("td:eq(2)").text();
            propertynumber = propertyno;//currentRow.find("td:eq(1)").text();
            pmlid = $(this).attr('pmlid');
            document.getElementById('lblPropertyDetails').innerHTML = propertyno + " - " + description;

            //added by nero
            $('#BodyHolder_txtRecordIDhidden').val(idForUpdate);
            $('#BodyHolder_txtPropertyNumberhidden').val(propertyno);

            $('#ModalToMaintenance').modal('show');
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}
function defaultModalMaintenance() {//BodyHolder_txtStep  BodyHolder_txtNextStep select2-BodyHolder_ddlTask-container
    //  alert('enteredddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd');

    $('#BodyHolder_ddlTask').val('--- Select Task ---');
    $('#BodyHolder_ddlAssignTo').val('--- Select Service Provider ---');

    $('#BodyHolder_txtStep').val('');
    $('#BodyHolder_txtNextStep').val('');

    $('#BodyHolder_ddlTask').prop('disabled', false);
    $('#BodyHolder_txtStep').prop('visible', false);
    $('#BodyHolder_txtNextStep').prop('disabled', true);

    $("#select2-BodyHolder_ddlTask-container").text('--- Select Task ---');
    $('#select2-BodyHolder_ddlAssignTo-container').text('--- Select Service Provider ---');

    var table = $('#tblSteps').DataTable();
    table
        .clear()
        .draw();

    document.getElementById("chkSendEmail").checked = false;
    document.getElementById("chkWithDoc").checked = false;
    var x = document.getElementById("hideAttachment");
    var y = document.getElementById("toggleAttachment");

    x.style.display = "none";
    y.style.display = "none";

    var z = document.getElementById("CurrentStep");
    var a = document.getElementById("nextStep");

    z.style.display = "none";
    a.style.display = "none";

    clear();
}

function AddToMaintenance() { //lblPropertyDetails
    idForUpdate = $('#BodyHolder_txtRecordIDhidden').val();
    propertynumber = $('#BodyHolder_txtPropertyNumberhidden').val();

    taskID = $('#BodyHolder_ddlTask').val();
    var step = ReplaceStr1($('#BodyHolder_txtStep').val().trim());
    assignto = $('#BodyHolder_ddlAssignTo').val();
    var attachment = $('#BodyHolder_AttachmentUpload').val();
    var sendEmail = document.getElementById("chkSendEmail").checked;
    var currStep = $('#BodyHolder_txtStep').val();

    if ($('#BodyHolder_ddlAssignTo').val() == '--- Select Service Provider ---')
        assignto = 0;
    Remarks = ReplaceStr1($('#BodyHolder_txtRemarks').val().trim());

    if ($('#BodyHolder_ddlTask').val() == '--- Select Task ---' || $('#BodyHolder_txtRemarks').val().trim() == '' || assignto == 0) {
        alertMsg = '';

        if ($('#BodyHolder_ddlTask').val() == '--- Select Task ---')
            alertMsg += '\nTask Name';
        if (assignto == 0)
            alertMsg += '\nService Provider';
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
                    url: "Operation_Module/PropertyMaintenance.aspx/AddToMaintenance",
                    data: " { id: '" + idForUpdate + "', propertyno : '" + propertynumber + "', taskid :'" + taskID + "', serviceproviderid: '" + assignto + "', remarks: '" + Remarks + "', sendEmail:'" + sendEmail + "', currentStep:'" + currStep + "',pmlid : '" + pmlid + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "0") {
                            $("#ModalToMaintenance").modal("hide");
                            //alert('Property Successfully Added for Maintenance!');
                            swal("Successful", "Property Successfully Added for Maintenance!", "success");
                            load_data();
                            load_data_onmaintenance();
                            clear();
                        }
                        else {
                            $("#ModalToMaintenance").modal("hide");
                            //alert('Property Successfully Updated for Maintenance!');
                            swal("Successful", "Property Successfully Updated for Maintenance!", "success");
                            load_data();
                            load_data_onmaintenance();
                            clear();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
    //}
}

function clear() {
    idForUpdate = 0;
    propertynumber = '';
    taskID = 0;
    Remarks = '';
    assignto = 0;
}

function clearUpdate()
{
    document.getElementById("chkSendEmail").checked = true;
    document.getElementById("chkWithDoc").checked = false;
    $('#BodyHolder_AttachmentUpload').val('');
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlAssignTo').val('--- Select Service Provider ---');
    $('#BodyHolder_ddlAssignTo').val('0').trigger('change');


}

function load_data_onmaintenance() {
    var search = ReplaceStr1("");

    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyMaintenance.aspx/GetPropertyOnMaintenance",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
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

function UpdateMaintenance(recid, propertyno, description, taskid, step, remarks, SendEmail, pmlid1) {
    clearUpdate();
    //alert(step);
    var z = document.getElementById("CurrentStep");
    var a = document.getElementById("nextStep");

    z.style.display = "inline";
    a.style.display = "inline";

    pmlid = pmlid1;
    document.getElementById('lblPropertyDetails').innerHTML = propertyno + " - " + description;

    $('#BodyHolder_txtRecordIDhidden').val(recid);
    $('#BodyHolder_txtPropertyNumberhidden').val(propertyno);
    $('#BodyHolder_txtTaskIDhidden').val(taskid);
    $('#BodyHolder_txtStepCurrent').val(step);

    idForUpdate = recid;
    propertynumber = propertyno;
    taskID = taskid;
    Remarks = remarks;

    var y = document.getElementById("toggleAttachment");
    y.style.display = "inline";

    if (taskid == 0) {
        defaultModalMaintenance(); // added by nero
        //$('#BodyHolder_ddlTask').prop('disabled', false); $('#BodyHolder_ddlTask').val('--- Select Task ---').trigger('change'); // commented by nero
    }
    else {
        $('#BodyHolder_ddlTask').prop('disabled', true); $('#BodyHolder_ddlTask').val(taskid).trigger('change');
    }

    if (SendEmail == 'True') {
        document.getElementById("chkSendEmail").checked = true;
    }
    else {
        document.getElementById("chkSendEmail").checked = false;
    }
    if (taskid != 0) {
        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyMaintenance.aspx/TaskbyID",
            data: "{ id: '" + taskid + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_txtStep').val(msg.d.split('|')[4].split('-')[step - 1]);
                $('#BodyHolder_txtNextStep').val(msg.d.split('|')[4].split('-')[step]);
            }
        });
    }

    // $('#BodyHolder_txtRemarks').val(remarks);
    load_data_onmaintenancestep(recid);
    $('#ModalToMaintenance').modal('show');

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[2] == "0") {
                let elementsAddCat = document.getElementsByClassName("btn_EdiPropertyMaintenance");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });
}

function load_data_onmaintenancestep(recid) {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyMaintenance.aspx/PropertyMaintenanceSteps",
        data: "{ recid: '" + recid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#steplist').hide();
            $('#steplist').innerHTML = "";
            $('#steplist').fadeIn(200);

            myJsonData = data;
            populateDataTableOnMaintenanceSteps(myJsonData);

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

function populateDataTableOnMaintenanceSteps(data) {
    try {
        $('#tblSteps').DataTable({
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
                { "data": "MaintenanceStep" },
                { "data": "ServiceProvider" },
                { "data": "DateOfCreation" },
                { "data": "MaintenanceRemarks" },
                { "data": "MaintenanceStatus" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Maintenance'
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

        $('#tblSteps').off('click');
    } catch (e) {
        var table = $('#tblSteps').DataTable();
        table
            .clear()
            .draw();
    }
}

function cancelMaintenance(recid, propertyno, pmlid) {
    //alert("RecID: "+ recid +" propertyNo: "+ propertyno);

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
    textareatt.id = "cancelmaintenance";

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

            var textremarks = $('#cancelmaintenance').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyMaintenance.aspx/CancelMaintenance",
                data: " { id: '" + recid + "',propertyno : '" + propertyno + "',pmlid : '" + pmlid + "', remarks: '" + textremarks + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    swal("Successful", "Transaction Closed!", "success");
                    load_data_onmaintenance();
                }
            });

            load_data();
            clear();
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function TerminateMaintenance(recid, propertyno, description, taskid, step, noofsteps, employeeid, returnrequestno, terminatestat, Pmlid) {
    pmlid = Pmlid;
    idForUpdate = recid;
    propertynumber = propertyno;
    returnrequestnumber = returnrequestno;
    //if (returnrequestno == "") {
    //    if (employeeid == 0) {
    //        terminatestatus = 'For Disposal';
    //    }
    //    else {
    //        terminatestatus = 'For Return Request - Disposal';
    //    }
    //}
    //else {
    //    terminatestatus = 'For Disposal';
    //}
    //terminatestatus = 'For Disposal'
    terminatestatus = terminatestat

    $('#terminateMaintenance').modal('show');
}

function TerminatePropertyMaintenance() {
    remarks = ReplaceStr1($('#txtTerminationRemarks').val().trim());
    if ($('#txtTerminationRemarks').val().trim() == "") {
        //alert("Please Fill-up Remarks");
        swal("Fill-out all the required field(s): ", "\nRemarks", "warning");
    }
    else {
        //if (confirm("Are you sure you want to terminate this property maintenance?") == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to terminate this property maintenance?",
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
                    url: "Operation_Module/PropertyMaintenance.aspx/TerminatePropertyMaintenance",
                    data: "{ id: '" + idForUpdate + "', propertyno: '" + propertynumber + "', remarks: '" + remarks + "',status: '" + terminatestatus + "',returnrequestno: '" + returnrequestnumber + "' ,pmlid: '" + pmlid + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        swal("Successful", "Property Maintenance Terminated Successfully.", "success");
                        //alert("Property Maintenance Terminated Successfully.")
                        load_data_onmaintenance();
                        $('#terminateMaintenance').modal('hide');
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function UploadMaintenance() {
    var alertMsg;
    var propertyno = $('#BodyHolder_txtPropertyNumberhidden').val();
    var txtstep = $('#BodyHolder_txtStepCurrent').val();
    var recId = $('#BodyHolder_txtRecordIDhidden').val();
    var taskId = $('#BodyHolder_txtTaskIDhidden').val();
    var Remarks = $('#BodyHolder_txtRemarks').val();
    var serviceproviderid = $('#BodyHolder_ddlAssignTo').val();
    var filess = $('#BodyHolder_AttachmentUpload').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    //if (filess == "") {
    //    swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
    //}
    idForUpdate = $('#BodyHolder_txtRecordIDhidden').val();
    propertynumber = $('#BodyHolder_txtPropertyNumberhidden').val();
    //document.getElementById("chkWithDoc").checked = false;
    taskID = $('#BodyHolder_ddlTask').val();
    var step = ReplaceStr1($('#BodyHolder_txtStep').val().trim());
    assignto = $('#BodyHolder_ddlAssignTo').val();
    var attachment = $('#BodyHolder_AttachmentUpload').val();
    var sendEmail = document.getElementById("chkSendEmail").checked;
    var currStep = $('#BodyHolder_txtStep').val();
    //alert(" { id: '" + idForUpdate + "', propertyno : '" + propertynumber + "', taskid :'" + taskID + "', serviceproviderid: '" + assignto + "', remarks: '" + Remarks + "', sendEmail:'" + sendEmail + "', currentStep:'" + currStep + "',pmlid : '" + pmlid + "'} ");

    if ($('#BodyHolder_ddlAssignTo').val() == '--- Select Service Provider ---')
        assignto = 0;
    Remarks = ReplaceStr1($('#BodyHolder_txtRemarks').val().trim());

    if ($('#BodyHolder_ddlTask').val() == '--- Select Task ---' || $('#BodyHolder_txtRemarks').val().trim() == '' || assignto == 0 || document.getElementById("chkWithDoc").checked == true && $('#BodyHolder_AttachmentUpload').val().trim() == '') {
        alertMsg = '';

        if (document.getElementById("chkWithDoc").checked == true && $('#BodyHolder_AttachmentUpload').val().trim() == '')
            alertMsg += '\nNo document attached';
        if ($('#BodyHolder_ddlTask').val() == '--- Select Task ---')
            alertMsg += '\nTask Name';
        if (assignto == 0)
            alertMsg += '\nService Provider';
        if ($('#BodyHolder_txtRemarks').val() == '')
            alertMsg += '\nRemarks';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
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
                $(".se-pre-con").fadeIn();
                $(function () {
                    var fileUpload = $("#BodyHolder_AttachmentUpload").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: "Operation_Module/OperationUploadHandler.ashx?transactionnumber=" + propertyno + "|PropertyMaintenance|" + recId + "|" + txtstep,
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/PropertyMaintenance.aspx/AddToMaintenance",
                                    // data: " { id : '" + recId + "',propertyno : '" + propertyno + "',taskid : '" + taskId + "',serviceproviderid : '" + serviceproviderid + "',remarks : '" + Remarks + "',sendEmail : '" + "" + "',currentStep : '" + "" + "',pmlid : '" + "1" + "'} ",
                                    data: " { id: '" + idForUpdate + "', propertyno : '" + propertynumber + "', taskid :'" + taskID + "', serviceproviderid: '" + assignto + "', remarks: '" + Remarks + "', sendEmail:'" + sendEmail + "', currentStep:'" + currStep + "',pmlid : '" + pmlid + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_onmaintenance();
                                        $('#ModalToMaintenance').modal('hide');
                                        swal("Successful", "Transaction Completed without Supporting Document!", "success");
                                    }
                                });
                            } else {
                                swal("Warning", result, "warning");
                            }
                            $(".se-pre-con").hide();
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
    }
}