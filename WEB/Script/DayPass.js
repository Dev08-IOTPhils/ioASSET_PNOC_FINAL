
var idForUpdate = 0;
var requestcount = 0;
var isEdit = '';
var ImaheExt = '';
$(document).ready(function () {
    $('.select2').select2();
    GetApproversDP();
    IfGatePassNeedsApproval();

    $("#tblAssetList").DataTable({
        "scrollX": true
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request '
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request '
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request '
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request '
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request '
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request '
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
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


    //$("#tblAssetList").DataTable({
    //    "scrollX": true
    //    //, "searching": false
    //    , dom: 'Brftip',
    //    buttons: {
    //        buttons: [
    //                'copy',
    //                'excel',
    //                'csv',
    //                //'pdf',
    //                'print',
    //                'colvis'
    //        ]
    //    }, colReorder: true
    //        , stateSave: true
    //});
    //$("#tblPendingUpload").DataTable({
    //    "scrollX": true
    //    //, "searching": false
    //    , dom: 'Brftip',
    //    buttons: {
    //        buttons: [
    //                'copy',
    //                'excel',
    //                'csv',
    //                //'pdf',
    //                'print',
    //                'colvis'
    //        ]
    //    }, colReorder: true
    //});
    load_data();
    load_data_Pendingupload();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
});

function getImage(event) {
    console.log(event.target.files[0]);
    $('#BodyHolder_PropertyImage').html("");
    var image = URL.createObjectURL(event.target.files[0]);
    var imagediv = document.getElementById('BodyHolder_PropertyImage');
    var newimg = document.createElement('img');
    newimg.src = image;
    newimg.width = "300";
    imagediv.appendChild(newimg);
}

function OCSerial() {
    if (isEdit = 'True') {
        $('#BodyHolder_PropertyImage').html("");
    }
}
var needsapproval = "False";
function IfGatePassNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/DayPass.aspx/IfGatePassNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var x = document.getElementById("labelApprover1");
            var y = document.getElementById("labelApprover");
            if (msg.d == "True") {
                x.style.display = 'inline';
                y.style.display = 'inline';
            }
            else {
                x.style.display = 'none';
                y.style.display = 'none';
            }
        }
    });
}

function load_data() {

    $.ajax({
        type: 'POST',
        url: "Operation_Module/DayPass.aspx/GetPropertyDayPass",
        data: "{ }",
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
            GetCount();

        },
        error: function (e) {
            //alert(e);
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
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "SerialNumber" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Description" },
                { "data": "Image" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
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
        $('#tblAssetList').on('click', 'a.btn_Edit', function (e) {
            isEdit = "True";
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var data = $('#tblAssetList').DataTable().row(currentRow).data();
            idForUpdate = data['RecIDstring'];

            var serialno = currentRow.find("td:eq(0)").text();
            var category = currentRow.find("td:eq(1)").text();
            var brand = currentRow.find("td:eq(2)").text();
            var description = currentRow.find("td:eq(3)").text();
            ImaheExt = currentRow.find("td:eq(4)").text().split(".")[1];
            $('#txtSerialNumber').val(serialno);
            $('#txtCategory').val(category);
            $('#txtBrand').val(brand);
            $('#BodyHolder_txtDescription').val(description);

            //$("#BodyHolder_daypassImage").val("~/SupportingDocuments/DayPass/"+ currentRow.find("td:eq(4)").text());
            var image = "<img src='../SupportingDocuments/DayPass/" + currentRow.find("td:eq(4)").text() + "' style='width:300px;'/>";
            $('#BodyHolder_PropertyImage').html(image);
        });

        $('#tblAssetList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var data = $('#tblAssetList').DataTable().row(currentRow).data();
            idForUpdate = data['RecIDstring'];
            var serialno = currentRow.find("td:eq(0)").text();
            swal({
                title: "Are you sure?",
                text: "Are you sure you want to remove this Property?",
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
                        url: "Operation_Module/DayPass.aspx/PropertyRemoveToDayPass",
                        data: "{  recid: '" + idForUpdate + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            currentRow.remove();
                            load_data();
                            GetCount();
                            //alert('Property successfully removed.');
                            swal("Success", "Property successfully removed.", "success");
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
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

var approversDP = new Array();
function GetApproversDP() {
    var context = window.location.pathname.substring(1);
    var ddlApp = $('#ddlApproverDP');
    ddlApp.empty();
    ddlApp.append($('<option>', {
        value: '0',
        text: '--- Select Approver ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetApproverDP',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                approversDP = JSON.parse(xml.d);
                approversDP.forEach((usr, index) => {
                    ddlApp.append($('<option>', {
                        value: usr.UserID,
                        text: usr.FullName,
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

function PropertyAddToDayPass() {
    var alertMsg;
    var Imahe = $('#BodyHolder_daypassImage').val();
    var serialno = $('#txtSerialNumber').val();
    var category = $('#txtCategory').val();
    var brand = $('#txtBrand').val();
    var description = $('#BodyHolder_txtDescription').val();
    if ($('#txtSerialNumber').val() == "" || $('#txtCategory').val() == "" || $('#txtBrand').val() == "" || $('#BodyHolder_txtDescription').val() == "" || ($('#BodyHolder_daypassImage').val() == '' && isEdit == 'False')) {
        alertMsg = '';

        if ($('#txtSerialNumber').val() == '')
            alertMsg += '\nSerial Number';
        if ($('#txtCategory').val().trim() == '')
            alertMsg += '\nCategory';
        if ($('#txtBrand').val() == '')
            alertMsg += '\nBrand';
        if ($('#BodyHolder_txtDescription').val() == '')
            alertMsg += '\nDescription';
        if ($('#BodyHolder_daypassImage').val() == '' && isEdit == 'False')
            alertMsg += '\nImage';
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to add this Property?",
            icon: "warning",
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                if ($('#BodyHolder_daypassImage').val() != "") {
                    Imahe = "." + $('#BodyHolder_daypassImage').val().split(".")[1];
                }
                var imagefilename = serialno + Imahe;
                if (isEdit == 'True' && $('#BodyHolder_daypassImage').val() == "") {
                    imagefilename = serialno + "." + ImaheExt;
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/DayPass.aspx/PropertyAddToDayPass",
                        data: "{  recid: '" + idForUpdate + "',serialno: '" + serialno + "', category:'" + category + "', brand:'" + brand + "', description :'" + description + "', image: '" + imagefilename + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "0") {
                                load_data();
                                Clear();
                                swal("Successful", "Property Added to List!", "success");
                            }
                            else if (msg.d == "1") {
                                load_data();
                                Clear();
                                swal("Successful", "Property Successfully Modified!", "success");
                            }
                            else {
                                load_data();
                                Clear();
                                swal("Error", "Property Already Exist!", "error");
                            }
                        }
                    });
                }
                else {
                    $(function () {
                        var fileUpload = $("#BodyHolder_daypassImage").get(0);
                        var files = fileUpload.files;
                        var test = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            test.append(files[i].name, files[i]);
                        }
                        $.ajax({
                            url: "Operation_Module/DayPass.ashx?serialno=" + serialno + Imahe,
                            type: "POST",
                            contentType: false,
                            processData: false,
                            data: test,
                            success: function (result) {
                                if (result.includes("File Uploaded Successfully", 0)) {
                                    $.ajax({
                                        type: "POST",
                                        url: "Operation_Module/DayPass.aspx/PropertyAddToDayPass",
                                        data: "{  recid: '" + idForUpdate + "',serialno: '" + serialno + "', category:'" + category + "', brand:'" + brand + "', description :'" + description + "', image: '" + imagefilename + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (msg) {
                                            if (msg.d == "0") {
                                                load_data();
                                                Clear();
                                                swal("Successful", "Property Added to List!", "success");
                                            }
                                            else if (msg.d == "1") {
                                                load_data();
                                                Clear();
                                                swal("Successful", "Property Successfully Modified!", "success");
                                            }
                                            else {
                                                load_data();
                                                Clear();
                                                swal("Error", "Property Already Exist!", "error");
                                            }
                                        }
                                    });
                                }
                                else {
                                    swal("Warning", result, "warning");
                                }
                            },
                            error: function (err) {
                                swal("Warning", err.statusText, "warning");
                            }
                        });
                    });
                }
                //$.ajax({
                //    type: "POST",
                //    url: "Operation_Module/DayPass.aspx/PropertyAddToDayPass",
                //    data: "{  recid: '" + idForUpdate + "',serialno: '" + serialno + "', category:'" + category + "', brand:'" + brand + "', description :'" + description + "'}",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (msg) {
                //        if (msg.d == "0") {
                //            load_data();
                //            Clear();
                //            //alert('Property Added to List');
                //            swal("Successful", "Property Added to List!", "success");
                //        }
                //        else if (msg.d == "1") {
                //            load_data();
                //            Clear();
                //            //alert('Property Successfully Modified!');
                //            swal("Successful", "Property Successfully Modified!", "success");
                //        }
                //        else {
                //            load_data();
                //            Clear();
                //            //alert('Property Already Exist!');
                //            swal("Error", "Property Already Exist!", "error");
                //        }
                //    }
                //});
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function Clear() {
    isEdit = 'False';
    $('#BodyHolder_PropertyImage').html("");
    $('#BodyHolder_daypassImage').val('');
    $('#txtSerialNumber').val("");
    $('#txtCategory').val("");
    $('#txtBrand').val("");
    $('#BodyHolder_txtDescription').val("");
    idForUpdate = 0;
}

function ConfirmDayPass() {
    var alertMsg;
    if (requestcount == 0) {
        //alert('You don\'t have any Property request on the List.');
        swal("Error", "You don\'t have any Property request on the List.", "error");
    }
    else {
        if (needsapproval == "True") {
            if ($('#ddlApproverDP').val() == 0 || $('#BodyHolder_txtPurpose').val().trim() == '') {
                alertMsg = ' ';
                if ($('#BodyHolder_txtPurpose').val() == "")
                    alertMsg += "\nPurpose ";
                if ($('#ddlApproverDP').val() == 0)
                    alertMsg += "\nApprover";


                //alert(alertMsg);
                swal("Fill-out all the required field(s): ", alertMsg, "warning");
            }
            else {
                swal({
                    title: "Are you sure?",
                    text: "Are you sure you want to CONFIRM this transaction?",
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
                            url: "Operation_Module/DayPass.aspx/ConfirmPropertyDayPassRequest",
                            data: "{ approverid : '" + $('#ddlApproverDP').val() + "', purpose : '" + $('#BodyHolder_txtPurpose').val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d == "okay") {
                                    load_data();
                                    load_data_Pendingupload();
                                    requestcount = 0;
                                    //alert("Property successfully Request for Day Pass");
                                    swal("Successful", "Property successfully Request for Day Pass.", "success");
                                }
                                else if (msg.d == "sorry") {
                                    load_data();
                                    load_data_Pendingupload();
                                    requestcount = 0;
                                    //alert("Property successfully Request for Day Pass. \n\nMail not sent. Check mail setting.");
                                    swal("Successful", "Property successfully Request for Day Pass. \n\nMail not sent. Check mail setting.", "success");
                                }
                                else {
                                    load_data();
                                    load_data_Pendingupload();
                                    requestcount = 0;
                                    //alert("Property successfully Request for Day Pass. \n\nMail not sent. Approver has no email.");
                                    swal("Successful", "Property successfully Request for Day Pass. \n\nMail not sent. Approver has no email.", "success");
                                }

                                $('#BodyHolder_txtPurpose').val("");
                                $('#ddlApproverDP').val("0").trigger('change');;
                            }
                        });
                    } else {
                        swal("Cancelled", "Cancelled by user", "warning");
                    }
                });
            }
        }
        else {
            if ($('#BodyHolder_txtPurpose').val().trim() == "") {
                alertMsg = ' ';
                if ($('#BodyHolder_txtPurpose').val() == "")
                    alertMsg += "\nPurpose ";

                //alert(alertMsg);
                swal("Fill-out all the required field(s): ", alertMsg, "warning");
            }
            else {
                swal({
                    title: "Are you sure?",
                    text: "Are you sure you want to CONFIRM this transaction?",
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
                            url: "Operation_Module/DayPass.aspx/ConfirmPropertyDayPassRequest",
                            data: "{ approverid : '0', purpose : '" + $('#BodyHolder_txtPurpose').val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d == "okay") {
                                    load_data();
                                    load_data_Pendingupload();
                                    requestcount = 0;
                                    //alert("Property successfully Request for Day Pass");
                                    swal("Successful", "Property successfully Request for Day Pass.", "success");
                                }
                                else if (msg.d == "sorry") {
                                    load_data();
                                    load_data_Pendingupload();
                                    requestcount = 0;
                                    //alert("Property successfully Request for Day Pass. \n\nMail not sent. Check mail setting.");
                                    swal("Successful", "Property successfully Request for Day Pass.", "success");
                                }
                                else {
                                    load_data();
                                    load_data_Pendingupload();
                                    requestcount = 0;
                                    //alert("Property successfully Request for Day Pass. \n\nMail not sent. Approver has no email.");
                                    swal("Successful", "Property successfully Request for Day Pass.", "success");
                                }
                            }
                        });
                    } else {
                        swal("Cancelled", "Cancelled by user", "warning");
                    }
                });
            }
        }
    }
}

function GetCount() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/DayPass.aspx/GetCountPropertyDayPassRequest",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            requestcount = msg.d;
        }
    });
}

function load_data_Pendingupload() {

    $.ajax({
        type: 'POST',
        url: "Operation_Module/DayPass.aspx/GetPropertyDayPassPendingApproval",
        data: "{ }",
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
            //alert(e);
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
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DayPassNumber" },
                //{ "data": "NoOfItemReturn" },
                { "data": "DateOfTransaction" },
                { "data": "Purpose" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Personal Property Gate Pass Request'
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
        });
    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var daypassnumber = '';
function ViewList(daypassno) {
    daypassnumber = daypassno;
    load_pending_list();

    $.ajax({
        type: "POST",
        url: "Operation_Module/DayPass.aspx/GenerateDayPassReport",
        data: "{daypassno: '" + daypassno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divDayPassForm').hide();
            $('#BodyHolder_divDayPassForm').html(msg.d);
            $('#BodyHolder_divDayPassForm').fadeIn(200);
        }
    });
    $('#modalPropertyList').modal('show');
}

function load_pending_list() {

    $.ajax({
        type: "POST",
        url: "Operation_Module/DayPass.aspx/PropertyDayPassRequestList",
        data: "{ daypassno : '" + daypassnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pal').hide();
            $('#pal').innerHTML = "";
            $('#pal').fadeIn(200);

            myJsonData = data;
            populateALPending(myJsonData);

            console.log(data);
            console.log('Success');

        },
        error: function (e) {
            //alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateALPending(data) {
    try {
        $('#tblPAL').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "SerialNumber" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Description" }
            ]
        });

        $('#tblPAL').off('click');

    } catch (e) {
        var table = $('#tblPAL').DataTable();
        table
            .clear()
            .draw();
    }
}

function Cancel(daypassno) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to cancel this transaction?",
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
                url: "Operation_Module/DayPass.aspx/CancelDayPassRequest",
                data: "{ daypassno : '" + daypassno + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_Pendingupload();
                    //alert('Day Pass Request successfully cancelled.');
                    swal("Successful", "Successfully cancelled Transaction!", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });

}

