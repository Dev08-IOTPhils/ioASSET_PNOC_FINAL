$(document).ready(function () {
    $('.select2').select2();
    IfGatePassNeedsApproval();

    //$('#gatepassbtn').off('click').on('click', function () {
    //    $('#GatesigandCertModal').modal('show');
    //    ClearSignature();
    //});
    $('#gpxcl').off('click').on('click', function () {

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert("yow");
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'GP' + "', key2: '" + gatepassnox + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "GP_" + formattedDateTime + ".xls");
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

    $('#btndownloadrispdf').off('click').on('click', function () {
        var transno = $(this).attr('transno');
        var transtype = $(this).attr('transtype');

        var requestedpkipassword = $('#requestedpkipassword');
        var approvedpkipassword = $('#approvedpkipassword');
        var issuedpkipassword = $('#issuedpkipassword');

        var alertMsg = "Fill-out all the required field(s):\n";
        var hasError = false;

        if (requestedpkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Requested By/Recieved By)";
        }

        if (approvedpkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Approved By)";
        }

        if (issuedpkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Issued By)";
        }

        if (requestedimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Requested By/Recieved By)";
        }

        if (approvedimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Approved By)";
        }

        if (issuedimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Issued By)";
        }

        if (requestedcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Requested By/Recieved By)";
        }

        if (approvedcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Approved By)";
        }

        if (issuedcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Issued By)";
        }

        if (hasError) {
            SweetAlertWarning(alertMsg);
            return;
        }

        var requesteddatestamp = $('#requesteddatestamp');
        var approveddatestamp = $('#approveddatestamp');
        var issueddatestamp = $('#issueddatestamp');

        $.ajax({
            type: "POST",
            url: "Operation_Module/PropertyGatePassRequest.aspx/CreateGatePass",
            data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "', approveddatestamp: '" + approveddatestamp.prop('checked') + "',issueddatestamp:'" + issueddatestamp.prop('checked') + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                if (msg.d == 'Error') {
                    SweetAlertError("An error occured.");
                    ClearSignature();
                }
                else if (msg.d == 'Invalid Pass Phrase') {
                    SweetAlertError("Invalid Pass Phrase");
                    ClearSignature();
                } else {
                    var bytes = Base64ToBytes(msg.d);

                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                        ClearSignature();
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", "Property_GatePass.pdf");
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);

                        ClearSignature();
                    }
                }
            }, error: function (xhr, status, error) {
                SweetAlertError("An error occured.");
                ClearSignature();
            }
        });
    });

    $('#requestedsignatureupload').off('change').on('change', function () {
        requestedreadURL(this);
    });

    $('#approvedsignatureupload').off('change').on('change', function () {
        approvedreadURL(this);
    });

    $('#issuedsignatureupload').off('change').on('change', function () {
        issuedreadURL(this);
    });

    $('#requestedpkicertificateupload').off('change').on('change', function () {
        requestedcertreadURL(this);
    });

    $('#approvedpkicertificateupload').off('change').on('change', function () {
        approvedcertreadURL(this);
    });

    $('#issuedpkicertificateupload').off('change').on('change', function () {
        issuedcertreadURL(this);
    });

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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
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
    load_data();
    load_data_Pendingupload();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
    GetCount();
});

var issuedcertbase64 = "";
var approvedcertbase64 = "";
var requestedcertbase64 = "";
var issuedimagebase64 = "";
var approvedimagebase64 = "";
var requestedimagebase64 = "";

function issuedcertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#issuedpkicertificateupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                issuedcertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function approvedcertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#approvedpkicertificateupload').val('');
        } else {
            var reader = new FileReader();

            reader.onload = function (e) {
                approvedcertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function requestedcertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#requestedpkicertificateupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                requestedcertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function issuedreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#issuedsignatureupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                issuedimagebase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function approvedreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#approvedsignatureupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                approvedimagebase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function requestedreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#requestedsignatureupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                requestedimagebase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function ClearSignature() {
    $('#fromsignatureupload').val('');
    $('#frompkipassword').val('');
    $('#frompkicertificateupload').val('');

    $('#bysignatureupload').val('');
    $('#bypkipassword').val('');
    $('#bypkicertificateupload').val('');

    fromimagebase64 = "";
    byimagebase64 = "";
    fromcertbase64 = "";
    bycertbase64 = "";

    $('#requestedpkipassword').val('');
    $('#approvedpkipassword').val('');
    $('#issuedpkipassword').val('');

    $('#requestedpkicertificateupload').val('');
    $('#approvedpkicertificateupload').val('');
    $('#issuedpkicertificateupload').val('');

    $('#requestedsignatureupload').val('');
    $('#approvedsignatureupload').val('');
    $('#issuedsignatureupload').val('');

    requestedimagebase64 = "";
    approvedimagebase64 = "";
    issuedimagebase64 = "";
    requestedcertbase64 = "";
    approvedcertbase64 = "";
    issuedcertbase64 = "";

    $('#itrapprovedpkipassword').val('');
    $('#itrreleasedpkipassword').val('');
    $('#itrreceivedpkipassword').val('');

    itrapprovedimagebase64 = "";
    itrreleaseimagebase64 = "";
    itrreceivedimagebase64 = "";

    itrapprovedcertbase64 = "";
    itrreleasecertbase64 = "";
    itrreceivedcertbase64 = "";
}

function SweetAlertSuccess(e) { swal({ icon: "success", text: e }) } function SweetAlertWarning(e) { swal({ icon: "warning", text: e }) } function SweetAlertInfo(e) { swal({ icon: "info", text: e }) } function SweetAlertError(e) { swal({ icon: "error", text: e }) }

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};
function stringToArrayBuffer(data) {
    var arrBuff = new ArrayBuffer(data.length);
    var writer = new Uint8Array(arrBuff);
    for (var i = 0, len = data.length; i < len; i++) {
        writer[i] = data.charCodeAt(i);
    }
    return arrBuff;
}
function arrayBufferToString(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return binary;
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
        load_data_Pendingupload();
        return false;
    }
}

var needsapproval = "False";
function IfGatePassNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyGatePassRequest.aspx/IfGatePassNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var x = document.getElementById("withorwithoutyou");
            var a = document.getElementById("withorwithoutyou1");
            var y = document.getElementById("labelApprover");
            var z = document.getElementById("dropdownApprover");
            if (msg.d == "True") {
                //x.style.visibility = 'visible';
                //a.style.visibility = 'hidden';
                //y.style.visibility = 'visible';
                //z.style.visibility = 'visible';
                x.style.display = 'inline';
                a.style.display = 'none';
                y.style.display = 'inline';
                z.style.display = 'inline';
            }
            else {
                x.style.display = 'none';
                a.style.display = 'inline';
                y.style.display = 'none';
                z.style.display = 'none';
            }
        }
    });
}

function load_data() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyGatePassRequest.aspx/GetPropertyAvailableForGatePass",
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
            GetCount();
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
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
                    text: 'Add to List',
                    action: function () {
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (count == 0) {
                            swal("Warning", "Please select an item to request", "warning");
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
                                    url: "Operation_Module/PropertyGatePassRequest.aspx/TransferToPropertyGatePassRequest",
                                    data: "{  pmlid: '" + pmlid + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $('#assetlist').hide();
                                        $('#assetlist').innerHTML = "";
                                        $('#assetlist').fadeIn(200);
                                        GetCount();
                                        $('#tblAssetList').DataTable()
                                        .rows('.selected')
                                        .remove()
                                        .draw(false);
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
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');

            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyGatePassRequest.aspx/TransferToPropertyGatePassRequest",
                data: "{  pmlid: '" + pmlid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#tblAssetList').DataTable().rows(currentRow)
                     .remove()
                    .draw(false);
                    GetCount();
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

var GetCounter = 0;
function GetCount() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyGatePassRequest.aspx/GetCountPropertyGatePassRequest",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" Property Gate Pass Request List: " + msg.d);
            GetCounter = msg.d;
            if (GetCounter == 0) {
                document.getElementById("btnAssetItemTransfer").disabled = true;
            }
            else {
                document.getElementById("btnAssetItemTransfer").disabled = false;
            }
        }
    });
}

function RemoveAllToGatePass() {
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
            $(".se-pre-con").fadeIn();
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyGatePassRequest.aspx/RemoveAllToPropertyGatePassRequest",
                data: "{ }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#GatePassList').modal('hide');
                    load_data();
                    swal("Successful", "Successfully cancelled Transaction!", "success");
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmGatePass() {
    $('.btn').prop('disabled', true);
    var alertMsg;

    if (needsapproval == "True") {
        if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_ddlRequestedBy').val() == '--- Select Requested By ---' || $('#txtRemarks').val().trim() == "") {
            alertMsg = '';
            if ($('#txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
                alertMsg += "\nApprover";
            if ($('#BodyHolder_ddlRequestedBy').val() == '--- Select Requested By ---')
                alertMsg += "\nRequestedBy";

            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            //alert(alertMsg);
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
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyGatePassRequest.aspx/ConfirmPropertyGatePassRequest",
                        data: "{ approverid : '" + $('#BodyHolder_ddlApprover').val() + "', remarks : '" + $('#txtRemarks').val() + "', requestedBy : '" + $('#BodyHolder_ddlRequestedBy').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "okay") {
                                $('.btn').prop('disabled', false);
                                $('#GatePassList').modal('hide');
                                load_data();
                                load_data_Pendingupload();
                                swal("Successful", "Property successfully Request for Gate Pass!", "success");
                                //alert("Property successfully Request for Gate Pass");
                            }
                            else if (msg.d == 'sorry') {
                                $('.btn').prop('disabled', false);
                                $('#GatePassList').modal('hide');
                                load_data();
                                load_data_Pendingupload();
                                swal("Successful", "Property successfully Request for Gate Pass.\n\nMail not sent. Check Mail Setting.", "success");
                                //alert("Property successfully Request for Gate Pass.\n\nMail not sent. Please check Mail Setting.");
                            }
                            else {
                                $('.btn').prop('disabled', false);
                                $('#GatePassList').modal('hide');
                                load_data();
                                load_data_Pendingupload();
                                //alert("Property successfully Request for Gate Pass.\n\nMail not sent. Approver has no email.");
                                swal("Successful", "Property successfully Request for Gate Pass.\n\nMail not sent. Approver has no email.", "success");
                            }
                        }
                    });
                } else {
                    $('.btn').prop('disabled', false);
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
    else {
        if ($('#txtRemarks').val().trim() == "") {
            alertMsg = 'Fill-out all the required field(s): ';
            if ($('#txtRemarks').val() == "" || $('#BodyHolder_ddlRequestedBy').val() == '--- Select Requested By ---')
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlRequestedBy').val() == '--- Select Requested By ---')
                alertMsg += "\nRequestedBy";


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
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyGatePassRequest.aspx/ConfirmPropertyGatePassRequest",
                        data: "{ approverid : '0', remarks : '" + $('#txtRemarks').val() + "', requestedBy : '" + $('#BodyHolder_ddlRequestedBy').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('.btn').prop('disabled', false);
                            $('#GatePassList').modal('hide');
                            load_data();
                            load_data_Pendingupload();
                            //alert("Property successfully Request for Gate Pass");
                            swal("Successful", "Property successfully Request for Gate Pass!", "success");
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
}

function GatePassList() {
    $('#txtRemarks').val('');
    $('#BodyHolder_ddlRequestedBy').val('--- Select Requested By ---').trigger('change');
    $('#BodyHolder_ddlApprover').val('--- Select Approver ---').trigger('change');

    
    if (GetCounter == 0) {
        swal("Warning", "Please select an item to add", "warning");
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        load_data();
        load_data_issuancelist();

        $('#GatePassList').modal('show');
        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}

function load_data_issuancelist() {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyGatePassRequest.aspx/GetGatePassPropertyList",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#gatepasslist').hide();
            $('#gatepasslist').innerHTML = "";
            $('#gatepasslist').fadeIn(200);

            myJsonData = data;
            populateDataTableForIssuance(myJsonData);

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

function populateDataTableForIssuance(data) {
    try {
        $('#tblGatePassList').DataTable({
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

        $('#tblGatePassList').off('click');

        $('#tblGatePassList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');

            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyGatePassRequest.aspx/RemoveToPropertyGatePassRequest",
                data: "{pmlid: '" + pmlid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#gatepasslist').hide();
                    $('#gatepasslist').innerHTML = "";
                    $('#gatepasslist').fadeIn(200);
                    load_data();
                    load_data_issuancelist();
                    GetCount();
                    currentRow.remove();
                }
            });
        });
    } catch (e) {
        var table = $('#tblGatePassList').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_Pendingupload() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyGatePassRequest.aspx/GetPropertyGatePassPendingUpload",
        data: "{ search: '" + search + "'}",
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
            "searching": false,
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "GatePassNumber" },
                //{ "data": "NoOfItemReturn" },
                { "data": "DateOfTransaction" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass Request'
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

        $('#tblPendingUpload').on('click', 'button.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblPendingUpload').on('click', 'button.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}

var emp = ''
var gatepassnumber = ''

var cnt = ''
var ctrchkr = ''
var gatepassnox = ''
function ViewList(gatepassno) {
    gatepassnumber = gatepassno;
    gatepassnox = gatepassno;
    load_pending_list();
    document.getElementById('lblGatePassNumber').innerHTML = gatepassno;
    $('#btndownloadcreatepdf').attr('transno', gatepassno);
    $('#btndownloadrispdf').attr('transno', gatepassno);

    $('#BodyHolder_txtGatePassNumber').val(gatepassno);
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyGatePassRequest.aspx/GenerateGatePassReport",
        data: "{gatepassno: '" + gatepassno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divGatePassForm').hide();
            $('#BodyHolder_divGatePassForm').html(msg.d);
            $('#BodyHolder_divGatePassForm').fadeIn(200);
        }
    });
    $('#AssetList1').modal('show');
}

function load_pending_list() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyGatePassRequest.aspx/PropertyGatePassRequestList",
        data: "{ gatepassno : '" + gatepassnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#purl').hide();
            $('#purl').innerHTML = "";
            $('#purl').fadeIn(200);

            myJsonData = data;
            populateALPending(myJsonData);

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

function populateALPending(data) {
    try {
        $('#tblPURL').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Gate Pass'
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

        $('#tblPURL').off('click');
    } catch (e) {
        var table = $('#tblPURL').DataTable();
        table
            .clear()
            .draw();
    }
}

function Cancel(gatepassno) {

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
    textareatt.id = "cancelgatepassrequest";

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

            var textremarks = $('#cancelgatepassrequest').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyGatePassRequest.aspx/CancelGatePassRequest",
                data: "{ gatepassno : '" + gatepassno + "', remarks: '" + textremarks + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    swal("Successful", "Successfully cancelled Transaction!", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}