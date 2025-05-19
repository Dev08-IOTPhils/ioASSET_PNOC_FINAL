var url = '';

$(document).ready(function () {
    $('.select2').select2();
    $('#BodyHolder_btnComplete').fadeIn();
    departmentPR();

    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    $('#rrspxcl').off('click').on('click', function () {
        //alert(msg.d);
        const now = new Date();



        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        //alert(transsno);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RRSP' + "', key2: '" + returnnumberX + "', key3: '" + '' + "', key4: '" + remarksX + "', key5: '" + employeenameX + '|' + daterequestedX + '|' + custodianNameX + '|' + positionx + '|' + employeedepX + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var bytes = Base64ToBytes(msg.d);
                //alert(msg.d);
                var blob = new Blob([bytes], { type: "application/octetstream" });

                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, fileName);
                } else {
                    var url = window.URL || window.webkitURL;
                    link = url.createObjectURL(blob);
                    var a = $("<a />");
                    a.attr("download", "PRS_" + formattedDateTime + ".xls");
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

    $('#rrspxcl1').off('click').on('click', function () {
        //alert(msg.d);
        const now = new Date();
        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');
        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;
        var context = window.location.pathname.substring(1);
        //alert(transsno);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RRSP' + "', key2: '" + returnnumberX + "', key3: '" + '' + "', key4: '" + remarksX + "', key5: '" + employeenameX + '|' + daterequestedX + '|' + custodianNameX + '|' + positionx +"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var bytes = Base64ToBytes(msg.d);
                //alert(msg.d);
                var blob = new Blob([bytes], { type: "application/octetstream" });
                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, fileName);
                } else {
                    var url = window.URL || window.webkitURL;
                    link = url.createObjectURL(blob);
                    var a = $("<a />");
                    a.attr("download", "PRS_" + formattedDateTime + ".xls");
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

    $('#btndownloadpdf').off('click').on('click', function () {
        var transno = $('#btndownloadpdf').attr('transno');
        var transtype = $('#btndownloadpdf').attr('transtype');
        //transno

        var employeename = $('#btndownloadpdf').attr('employeename');
        var daterequested = $('#btndownloadpdf').attr('daterequested');
        var ctr = $('#btndownloadpdf').attr('ctr');
        var ctrchkr = $('#btndownloadpdf').attr('ctrchkr');
        var Remarks = $('#btndownloadpdf').attr('Remarks');
        var EmployeePosition = $('#btndownloadpdf').attr('EmployeePosition');
        var EmployeeDepartment = $('#btndownloadpdf').attr('EmployeeDepartment');
        var CustodianPosition = $('#btndownloadpdf').attr('CustodianPosition');
        var CustodianDepartment = $('#btndownloadpdf').attr('CustodianDepartment');
        var CustodianName = $('#btndownloadpdf').attr('CustodianName');
        var PARICS = $('#btndownloadpdf').attr('PARICS');

        var alertMsg = "Fill-out all the required field(s):\n";
        var hasError = false;

        var frompkipassword = $('#frompkipassword');
        var bypkipassword = $('#bypkipassword');

        if (frompkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Received from)";
        }

        if (bypkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Received by)";
        }

        if (fromimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Received from)";
        }
        if (byimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Received by)";
        }

        if (fromcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Received from)";
        }
        if (bycertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Received by)";
        }

        if (hasError) {
            SweetAlertWarning(alertMsg);
            return;
        }

        var fromdatestamp = $('#fromdatestamp');
        var bydatestamp = $('#bydatestamp');

        if (transtype == "Turn In") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyReturn.aspx/CreatePDFTurnIn",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "',EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "',CustodianDepartment: '" + CustodianDepartment + "',CustodianName: '" + CustodianName + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Return.pdf");
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
        }
        else if (transtype == "Returned") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyReturn.aspx/CreatePDFReturned",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "',EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "',CustodianDepartment: '" + CustodianDepartment + "',CustodianName: '" + CustodianName + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Return.pdf");
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
        } else if (transtype == "Turn In Return") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyReturn.aspx/CreatePDFTurnInReturn",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "',EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "',CustodianDepartment: '" + CustodianDepartment + "',CustodianName: '" + CustodianName + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Return.pdf");
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
        } else if (transtype == "Returned Return") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyReturn.aspx/CreatePDFReturnedReturn",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "',EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "',CustodianDepartment: '" + CustodianDepartment + "',CustodianName: '" + CustodianName + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Return.pdf");
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
        }
    });

    $('#fromsignatureupload').off('change').on("change", function () {
        fromreadURL(this);
    });

    $('#bysignatureupload').off('change').on("change", function () {
        byreadURL(this);
    });

    $('#frompkicertificateupload').off('change').on("change", function () {
        fromcertreadURL(this);
    });

    $('#bypkicertificateupload').off('change').on("change", function () {
        bycertreadURL(this);
    });

    $("#tblAssetList").DataTable({
        "scrollX": true
        //, "searching": false
        , "lengthChange": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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
        , "lengthChange": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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
    $("#tblPendingUpload1").DataTable({
        "scrollX": true
        //, "searching": false
       , "lengthChange": false
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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
    GetUserCcustodian();
    $('#ddlCustodian').on('change', function () {
        load_data();
    });
    load_data_Pendingupload();
    load_data_returnPendingupload();
    GetEmployeePR();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
    $("#tblPendingUpload1").DataTable().clear().draw();

    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_AFupload').fadeIn();
            $('#BodyHolder_btnUpload').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_AFupload').hide();
            $('#BodyHolder_btnUpload').hide();
        }
    });
});

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

function byreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#bysignatureupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                byimagebase64 = e.target.result;
                $('#imgicsby').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}
var fromimagebase64 = "";
var byimagebase64 = "";
var fromcertbase64 = "";
var bycertbase64 = "";

function bycertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#bypkicertificateupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                bycertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function fromcertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#frompkicertificateupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                fromcertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function fromreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            $('#fromsignatureupload').val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                fromimagebase64 = e.target.result;
                $('#imgicsfrom').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
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
var custodianArray = new Array();
function GetUserCcustodian() {
    var ddlCustodian = $('#ddlCustodian');
    ddlCustodian.empty();
    ddlCustodian.append($('<option>', {
        value: '0',
        text: '--- Select Custodian Type ---',
        rowindex: -1
    }));

    $.ajax({
        async: false,
        type: 'POST',
        url: "Custodian_Operation_Module/PropertyReturn.aspx/GetUserCustodian",
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                custodianArray = JSON.parse(xml.d);
                custodianArray.forEach((custodian, index) => {
                    ddlCustodian.append($('<option>', {
                        value: custodian.CustodianID,
                        text: custodian.Custodian,
                        rowindex: index
                    }));
                });
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            alert(error);
            console.log(xhr);
        }
    });
}

function GetEmployeePR() {
    var department = $('#ddlDepartmentPR').val();
    if ($('#ddlDepartmentPR').val() == "0")
        department = 0;
    if (department == 0) {
        $.ajax({
            type: "POST",
            url: url + "/PropertyReturn.aspx/GetUserNoDepartment",
            data: " { } ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ddlEmployee = $("[id*=ddlEmployeePR]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                $.each(msg.d, function () {
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmployeePR]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/PropertyReturn.aspx/GetUser",
            data: " { department : '" + department + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (department == 0) {
                    var ddlEmployee = $("[id*=ddlEmployeePR]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                }
                else {
                    var ddlEmployee = $("[id*=ddlEmployeePR]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    $.each(msg.d, function () {
                        ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmployeePR]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }

    load_data();
}


var DepartmentArrayPR = new Array();
function departmentPR() {
    var context = window.location.pathname.substring(1);
    var ddlDept = $('#ddlDepartmentPR');
    ddlDept.empty();
    ddlDept.append($('<option>', {
        value: '0',
        text: '--- Select User Department ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetDepartmentPR',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DepartmentArrayPR = JSON.parse(xml.d);
                DepartmentArrayPR.forEach((dep, index) => {
                    ddlDept.append($('<option>', {
                        value: dep.DepartmentID,
                        text: dep.DepartmentName,
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

function EnterKey2(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_returnPendingupload();
        return false;
    }
}

function ValidateDate(dateString) {
    //Get the date from the TextBox.
    var dtCurrent = new Date();
    var parts = dateString.split("/");
    var dt = new Date(parts[0] + "/" + parts[1] + "/" + parts[2]);
    if (dt <= dtCurrent)
        return true;
    return false;
}

function load_data() {
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

    var employeeid = $('#ddlEmployeePR').val();
    if ($('#ddlEmployeePR').val() == '0' || $('#ddlEmployeePR').val() == null)
        employeeid = 0;

    var custodian = $('#ddlCustodian').val();
    //alert("{  search : '" + search + "', employeeid : '" + employeeid + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}");
    var search = "";
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyReturn.aspx/GetPropertyAvailableForReturn",
        data: "{  search : '" + search + "', employeeid : '" + employeeid + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData, ExpenseType);

            console.log(data);
            console.log('Success');

            GetCount(ExpenseType);
            $(".se-pre-con").fadeOut("slow");
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
            "pagingType": "full_numbers",
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
                { "data": "TransactorName" },
                { "data": "DateOfReturn" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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
                    text: 'Add to Return List',
                    action: function () {
                        var employeeid = $('#ddlEmployeePR').val();
                        var custodian = $('#ddlCustodian').val();
                        if ($('#ddlEmployeePR').val() == "0" || $('#ddlEmployeePR').val() == null)
                            employeeid = 0;
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (employeeid == 0) {
                            swal("Warning", "Please select an Employee", "warning");
                        }
                        else if (count == 0) {
                            swal("Warning", "Please select an item to return", "warning");
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
                                    url: url + "/PropertyReturn.aspx/TransferToPropertyReturn",
                                    data: "{  pmlid: '" + pmlid + "', employeeid: '" + employeeid + "', custodian: '" + custodian + "', ExpenseType: '" + ExpenseType + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $('#assetlist').hide();
                                        $('#assetlist').innerHTML = "";
                                        $('#assetlist').fadeIn(200);
                                        GetCount(ExpenseType);
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
            , stateSave: true,
            "rowCallback": function (row, data, index) {
                if (ValidateDate(data.DateOfReturn) == true) {
                    $('td', row).css('color', 'Red');
                }
            }
        });

        $('#tblAssetList').off('click');
        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');

            var employeeid = $('#ddlEmployeePR').val();
            var custodian = $('#ddlCustodian').val();
            //alert(pmlid);
            if ($('#ddlEmployeePR').val() == '0' || $('#ddlEmployeePR').val() == '0') {
                swal("Warning", "Please select Employee", "warning");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: url + "/PropertyReturn.aspx/TransferToPropertyReturn",
                    data: "{  pmlid: '" + pmlid + "', employeeid: '" + employeeid + "', custodian: '" + custodian + "', ExpenseType: '" + ExpenseType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#tblAssetList').DataTable().rows(currentRow)
                    .remove()
                        .draw(false);
                        GetCount(ExpenseType);
                    }
                });
            }
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_Pendingupload() {
    var search = "";
    //var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyReturn.aspx/GetPropertyReturnPendingUpload",
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
                { "data": "ReturnRequestNumber" },
                { "data": "IssuedTo" },
                //{ "data": "NoOfItemReturn" },
                { "data": "DateOfTransaction" },
                { "data": "Status" },
                { "data": "Action" }
            ],
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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

//function load_data_Pendingupload() {
//    var search = $('#txtSearch1').val().trim();
//    $.ajax({
//        type: 'POST',
//        url: url + "/PropertyReturn.aspx/GetPropertyReturnPendingUpload",
//        data: "{ search : '"+search+"'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            $('#pendinguploadlist').hide();
//            $('#pendinguploadlist').innerHTML = "";
//            $('#pendinguploadlist').fadeIn(200);
//            myJsonData = data;
//            populateDataTablePendingUpload(myJsonData);
//            console.log(data);
//            console.log('Success');
//        },
//        error: function (e) {
//            alert(e);
//            console.log("There was an error with your request...");
//            console.log("error: " + JSON.stringify(e));
//        }
//    });
//}

//function populateDataTablePendingUpload(data) {
//    try {
//        $('#tblPendingUpload').DataTable({
//            "destroy": true,
//            "processing": true,
//            "language": {
//                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
//            },
//            "scrollX": true,
//            "data": JSON.parse(data.d),
//            "columns": [
//                { "data": "ReturnRequestNumber" },
//                { "data": "IssuedTo" },
//                //{ "data": "NoOfItemReturn" },
//                { "data": "DateOfTransaction" },
//                { "data": "Status" },
//                { "data": "Action" }
//            ],
//            dom: 'rtip'
//        });
//        $('#tblPendingUpload').off('click');
//        $('#tblPendingUpload').on('click', 'a.btn_View', function (e) {
//            e.preventDefault();
//        });
//        $('#tblPendingUpload').on('click', 'a.btn_Cancel', function (e) {
//            e.preventDefault();
//        });
//    } catch (e) {
//        var table = $('#tblPendingUpload').DataTable();
//        table
//            .clear()
//            .draw();
//    }
//}
var returnnumberX = ''
var emp = ''
var returnrequestnumber = ''
var cnt = ''
var ctrchkr = ''
function ViewList(returnrequestno, employeename, daterequested, status, ctr, ctrchkr, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName, PARICS) {
    //alert("TEST");
    $('#BodyHolder_FileUpload1').val('');
    $('#BodyHolder_TextBox1').val(returnrequestno);
    $('#BodyHolder_txtReturnRequestNumberRequest').val(returnrequestno);
    $('#BodyHolder_txtReturnRequestNumber').val(returnrequestno);
    //document.getElementById("BodyHolder_TextBox1") = returnrequestno;
    var x = document.getElementById("showorhide1");

    $('#btndownloadcreatepdf').attr('transno', returnrequestno);
    $('#btndownloadcreatepdf').attr('employeename', employeename);
    $('#btndownloadcreatepdf').attr('daterequested', daterequested);
    $('#btndownloadcreatepdf').attr('Remarks', Remarks);
    $('#btndownloadcreatepdf').attr('EmployeePosition', EmployeePosition);
    $('#btndownloadcreatepdf').attr('EmployeeDepartment', EmployeeDepartment);
    $('#btndownloadcreatepdf').attr('CustodianPosition', CustodianPosition);
    $('#btndownloadcreatepdf').attr('CustodianDepartment', CustodianDepartment);
    $('#btndownloadcreatepdf').attr('CustodianName', CustodianName);


    $('#btndownloadpdf').attr('transno', returnrequestno);
    $('#btndownloadpdf').attr('employeename', employeename);
    $('#btndownloadpdf').attr('daterequested', daterequested);
    $('#btndownloadpdf').attr('ctr', ctr);
    $('#btndownloadpdf').attr('ctrchkr', ctrchkr);
    $('#btndownloadpdf').attr('Remarks', Remarks);
    $('#btndownloadpdf').attr('EmployeePosition', EmployeePosition);
    $('#btndownloadpdf').attr('EmployeeDepartment', EmployeeDepartment);
    $('#btndownloadpdf').attr('CustodianPosition', CustodianPosition);
    $('#btndownloadpdf').attr('CustodianDepartment', CustodianDepartment);
    $('#btndownloadpdf').attr('CustodianName', CustodianName);
    $('#btndownloadpdf').attr('PARICS', PARICS);

    if (ctr == ctrchkr) {
        if (status == 'On Maintenance') {
            swal('Warning', 'There are Properties in this Transaction that is still ON MAINTENANCE.', 'warning');
            x.style.display = "none";
            returnrequestnumber = returnrequestno;
            load_pending_list();
            document.getElementById('lblReturnRequestNumberRequest').innerHTML = returnrequestno;
            document.getElementById('lblAccountablePersonRequest').innerHTML = employeename;
            document.getElementById('lblDateRequestedRequest').innerHTML = daterequested;
            $('#BodyHolder_txtReturnRequestNumberRequest').val(returnrequestno);

            $('#AssetListRequest').modal('show');
        }
        else {
            x.style.display = "inline";
            returnrequestnumber = returnrequestno;
            load_pending_list();
            document.getElementById('lblReturnRequestNumberRequest').innerHTML = returnrequestno;
            document.getElementById('lblAccountablePersonRequest').innerHTML = employeename;
            document.getElementById('lblDateRequestedRequest').innerHTML = daterequested;
            $('#BodyHolder_txtReturnRequestNumberRequest').val(returnrequestno);

            $('#AssetListRequest').modal('show');
        }
    }
    else {
        x.style.display = "none";
        returnrequestnumber = returnrequestno;
        load_pending_list();
        document.getElementById('lblReturnRequestNumberRequest').innerHTML = returnrequestno;
        document.getElementById('lblAccountablePersonRequest').innerHTML = employeename;
        document.getElementById('lblDateRequestedRequest').innerHTML = daterequested;
        $('#BodyHolder_txtReturnRequestNumber').val(returnrequestno);

        $('#AssetListRequest').modal('show');
    }

    if (PARICS == "1") {
        document.getElementById("printGrid1").children[0].style.display = "none";
        document.getElementById("printGrid1").children[1].style.display = "none";
        document.getElementById("printGrid1").children[2].style.display = "inline";
        document.getElementById("printGrid1").children[3].style.display = "none";
    }
    else if (PARICS == "2" || PARICS == "3") {
        document.getElementById("printGrid1").children[0].style.display = "none";
        document.getElementById("printGrid1").children[1].style.display = "none";
        document.getElementById("printGrid1").children[2].style.display = "none";
        document.getElementById("printGrid1").children[3].style.display = "inline";
    }
    else {
        document.getElementById("printGrid1").children[1].style.display = "none";
        document.getElementById("printGrid1").children[0].style.display = "none";
        document.getElementById("printGrid1").children[2].style.display = "none";
        document.getElementById("printGrid1").children[3].style.display = "none";
    }

    //gheghe(Certificate/Word)
    //document.getElementById("printGrid1").children[5].style.display = "none";
    //document.getElementById("printGrid1").children[6].style.display = "none";


    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnRequest",
        data: "{returnno: '" + returnrequestno + "', Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "',EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "',CustodianDepartment: '" + CustodianDepartment + "',CustodianName: '" + CustodianName + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnIn').hide();
            $('#BodyHolder_divTurnIn').html(msg.d);
            $('#BodyHolder_divTurnIn').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnRARRequest",
        data: "{returnno: '" + returnrequestno + "', Remarks: '" + Remarks + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "',CustodianName: '" + CustodianName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInRAR').hide();
            $('#BodyHolder_divTurnInRAR').html(msg.d);
            $('#BodyHolder_divTurnInRAR').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnRARSEPRequest",
        data: "{returnno: '" + returnrequestno + "', Remarks: '" + Remarks + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "',CustodianName: '" + CustodianName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInRARSEP').hide();
            $('#BodyHolder_divTurnInRARSEP').html(msg.d);
            $('#BodyHolder_divTurnInRARSEP').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnRequestPPE",
        data: "{returnno: '" + returnrequestno + "', Remarks: '" + Remarks + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "',CustodianName: '" + CustodianName + "',EmployeePosition: '" + EmployeePosition + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInReturnRequestPPE').hide();
            $('#BodyHolder_divTurnInReturnRequestPPE').html(msg.d);
            $('#BodyHolder_divTurnInReturnRequestPPE').fadeIn(200);
        }
    });
}

function load_pending_list() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/PropertyReturnRequestList",
        data: "{ returnrequestno : '" + returnrequestnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#purlRequest').hide();
            $('#purlRequest').innerHTML = "";
            $('#purlRequest').fadeIn(200);

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
        $('#tblPURLRequest').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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

        $('#tblPURLRequest').off('click');
    } catch (e) {
        var table = $('#tblPURLRequest').DataTable();
        table
            .clear()
            .draw();
    }
}

////return

function ReturnList() {
    load_data();
    load_data_returnlist();
    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        $('#ReturnList').modal('show');
        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}
function load_data_returnlist() {
    var custodian = $('#ddlCustodian').val();
    var employeeid = $('#ddlEmployeePR').val();

    //added by nero 022624
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
    //ends here


    if ($('#ddlEmployeePR').val() == '0' || $('#ddlEmployeePR').val() == null)
        employeeid = 0;

    if (employeeid == 0) {
        swal("Warning", "Select Employee", "warning");
    }
    else {

        $.ajax({
            type: 'POST',
            url: url + "/PropertyReturn.aspx/GetReturnPropertyList",
            data: "{ employeeid : '" + employeeid + "',custodian: '" + custodian + "',ExpenseType:'" + ExpenseType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#returnlist').hide();
                $('#returnlist').innerHTML = "";
                $('#returnlist').fadeIn(200);

                myJsonData = data;
                populateDataTableForIssuance(myJsonData);

                console.log(data);
                console.log('Success');
                $('#ReturnList').modal('show');
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    //$.ajax({
    //    type: 'POST',
    //    url: url + "/PropertyReturn.aspx/GetReturnPropertyList",
    //    data: "{ employeeid : '" + employeeid + "',custodian: '" + custodian + "',ExpenseType:'" + ExpenseType + "'}",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {
    //        $('#returnlist').hide();
    //        $('#returnlist').innerHTML = "";
    //        $('#returnlist').fadeIn(200);

    //        myJsonData = data;
    //        populateDataTableForIssuance(myJsonData);

    //        console.log(data);
    //        console.log('Success');
    //    },
    //    error: function (e) {
    //        alert(e);
    //        console.log("There was an error with your request...");
    //        console.log("error: " + JSON.stringify(e));
    //    }
    //});
}
function populateDataTableForIssuance(data) {
    try {
        $('#tblReturnList').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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

        $('#tblReturnList').off('click');

        $('#tblReturnList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');

            var employeeid = $('#ddlEmployeePR').val();
            if ($('#ddlEmployeePR').val() == '0')
                employeeid = 0;

            $.ajax({
                type: "POST",
                url: url + "/PropertyReturn.aspx/RemoveToPropertyReturn",
                data: "{pmlid: '" + pmlid + "', employeeid: '" + employeeid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#issuancelist').hide();
                    $('#issuancelist').innerHTML = "";
                    $('#issuancelist').fadeIn(200);
                    load_data();
                    load_data_returnlist();
                    currentRow.remove();
                }
            });
        });
    } catch (e) {
        var table = $('#tblReturnList').DataTable();
        table
            .clear()
            .draw();
    }
}

function RemoveAllToTurnIn() {
    $('.btn').prop('disabled', true);
    var custodian = $('#ddlCustodian').val();
    var employeeid = $('#ddlEmployeePR').val();
    if ($('#ddlEmployeePR').val() == '--- Select Employee ---')
        employeeid = 0;
    swal({
        title: "Cancel Transaction",
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
                url: url + "/PropertyReturn.aspx/RemoveAllToPropertyReturn",
                data: "{ employeeid: '" + employeeid + "',custodian:'" + custodian + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();

                    $('#ReturnList').modal('hide');
                    swal("Successful", "Property Return Request cancelled successfully.", "success");
                    $('.btn').prop('disabled', false);
                }
            });
        } else {
            $('.btn').prop('disabled', false);
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmTurnIn() {
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

    var custodian = $('#ddlCustodian').val();
    $('.btn').prop('disabled', true);
    var employeeid = $('#ddlEmployeePR').val();
    if ($('#ddlEmployeePR').val() == '--- Select Employee ---')
        employeeid = 0;
    if (custodian == 0) {
        swal("Warning", "Select a custodian.", "warning");
    }
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
                url: url + "/PropertyReturn.aspx/ConfirmPropertyReturn",
                data: "{ employeeid: '" + employeeid + "', custodian: '" + custodian + "', ExpenseType: '" + ExpenseType + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('.btn').prop('disabled', false);
                    //alert("Property successfully Request for Return");
                    swal("Successful", "Property successfully Request for Return.", "success");
                    $('#ReturnList').modal('hide');
                    load_data();

                    load_data_returnPendingupload();
                    $(".se-pre-con").hide();
                }
            });
        } else {
            $('.btn').prop('disabled', false);
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    ////}
}

var GetCounter = 0;
function GetCount(ExpenseType) {
    var employeeid = $('#ddlEmployeePR').val();
    var custodian = $('#ddlCustodian').val();
    if ($('#ddlEmployeePR').val() == '--- Select Employee ---' || $('#ddlEmployeePR').val() == null)
        employeeid = 0;
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GetCountPropertyReturn",
        data: "{employeeid: '" + employeeid + "', custodian: '" + custodian + "' , ExpenseType: '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Property List for Return: " + msg.d);
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

function load_data_returnPendingupload() {
    //var custodian = $('#ddlCustodian').val();
    var search = "";
    $.ajax({
        type: 'POST',
        url: url + "/PropertyReturn.aspx/GetPropertyReturnPending",
        data: "{ search : '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendinguploadlist1').hide();
            $('#pendinguploadlist1').innerHTML = "";
            $('#pendinguploadlist1').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUpload1(myJsonData);

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

function populateDataTablePendingUpload1(data) {
    try {
        $('#tblPendingUpload1').DataTable({
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
                { "data": "ReturnNumber" },
                { "data": "IssuedTo" },
                //{ "data": "NoOfItemReturn" },
                { "data": "DateOfTransaction" },
                { "data": "Status" },
                { "data": "Action" }
            ],
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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

        $('#tblPendingUpload1').off('click');

        $('#tblPendingUpload1').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblPendingUpload1').on('click', 'a.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblPendingUpload1').DataTable();
        table
            .clear()
            .draw();
    }
}

var emp = ''
var returnnumber = ''
var cnt = ''
var ctrchkr = ''
var returnnumberX = ''
var remarksX = ''
var employeenameX = ''
var daterequestedX = ''
var custodianNameX = ''
var positionx = ''
var employeedepX = ''
function ViewListReturn(returnno, employeename, daterequested, status, ctr, ctrchkr, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName, PARICS) {
    returnnumberX = returnno;
    remarksX = Remarks;
    employeenameX = employeename;
    daterequestedX = daterequested;
    custodianNameX = CustodianName;
    positionx = CustodianPosition;
    employeedepX = CustodianDepartment;
    $('#BodyHolder_AFupload').val('');
    var x = document.getElementById("showorhide");
    $('#BodyHolder_txtReturnRequestNumber').val(returnno);

    $('#btndownloadcreatepdf').attr('transno', returnno);
    $('#btndownloadcreatepdf').attr('employeename', employeename);
    $('#btndownloadcreatepdf').attr('daterequested', daterequested);
    $('#btndownloadcreatepdf').attr('Remarks', Remarks);
    $('#btndownloadcreatepdf').attr('EmployeePosition', EmployeePosition);
    $('#btndownloadcreatepdf').attr('EmployeeDepartment', EmployeeDepartment);
    $('#btndownloadcreatepdf').attr('CustodianPosition', CustodianPosition);
    $('#btndownloadcreatepdf').attr('CustodianDepartment', CustodianDepartment);
    $('#btndownloadcreatepdf').attr('CustodianName', CustodianName);


    $('#btndownloadpdf').attr('transno', returnno);
    $('#btndownloadpdf').attr('employeename', employeename);
    $('#btndownloadpdf').attr('daterequested', daterequested);
    $('#btndownloadpdf').attr('ctr', ctr);
    $('#btndownloadpdf').attr('ctrchkr', ctrchkr);
    $('#btndownloadpdf').attr('Remarks', Remarks);
    $('#btndownloadpdf').attr('EmployeePosition', EmployeePosition);
    $('#btndownloadpdf').attr('EmployeeDepartment', EmployeeDepartment);
    $('#btndownloadpdf').attr('CustodianPosition', CustodianPosition);
    $('#btndownloadpdf').attr('CustodianDepartment', CustodianDepartment);
    $('#btndownloadpdf').attr('CustodianName', CustodianName);
    $('#btndownloadpdf').attr('PARICS', PARICS);

    if (ctr == ctrchkr) {
        if (status == 'On Maintenance') {
            swal('Warning', 'There are Properties in this Transaction that is still ON MAINTENANCE.', 'warning');
            x.style.display = "none";
            returnnumber = returnno;
            loadreturn_pending_list();
            document.getElementById('lblReturnRequestNumber').innerHTML = returnno;
            document.getElementById('lblAccountablePerson').innerHTML = employeename;
            document.getElementById('lblDateRequested').innerHTML = daterequested;
            $('#BodyHolder_txtReturnRequestNumber').val(returnno);

            $('#AssetListModal').modal('show');
        }
        else {
            x.style.display = "inline";
            returnnumber = returnno;
            loadreturn_pending_list();
            document.getElementById('lblReturnRequestNumber').innerHTML = returnno;
            document.getElementById('lblAccountablePerson').innerHTML = employeename;
            document.getElementById('lblDateRequested').innerHTML = daterequested;
            $('#BodyHolder_txtReturnRequestNumber').val(returnno);

            $('#AssetListModal').modal('show');
        }
    }
    else {
        x.style.display = "none";
        returnnumber = returnno;
        loadreturn_pending_list();
        document.getElementById('lblReturnRequestNumber').innerHTML = returnno;
        document.getElementById('lblAccountablePerson').innerHTML = employeename;
        document.getElementById('lblDateRequested').innerHTML = daterequested;
        $('#BodyHolder_txtReturnRequestNumber').val(returnno);

        $('#AssetListModal').modal('show');
    }

    if (PARICS == "1") {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "inline";
        //document.getElementById("printGrid").children[6].style.display = "none";
        //document.getElementById("printGrid").children[7].style.display = "none";
    }
    else if (PARICS == "2" || PARICS == "3") {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "inline";
        //document.getElementById("printGrid").children[6].style.display = "none";
        //document.getElementById("printGrid").children[7].style.display = "inline";
    }
    else {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "none";
        //document.getElementById("printGrid").children[6].style.display = "none";
        //document.getElementById("printGrid").children[7].style.display = "none";
    }

    //gheghe(Certificate/Word)
    //document.getElementById("printGrid").children[5].style.display = "none";
    //document.getElementById("printGrid").children[6].style.display = "none";

    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturn",
        data: "{returnno: '" + returnno + "', Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "',EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "',CustodianDepartment: '" + CustodianDepartment + "',CustodianName: '" + CustodianName + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnIn').hide();
            $('#BodyHolder_divTurnIn').html(msg.d);
            $('#BodyHolder_divTurnIn').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnCTR",
        data: "{returnno: '" + returnno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInCTR').hide();
            $('#BodyHolder_divTurnInCTR').html(msg.d);
            $('#BodyHolder_divTurnInCTR').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnRAR",
        data: "{returnno: '" + returnno + "', Remarks: '" + Remarks + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "',CustodianName: '" + CustodianName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInRAR').hide();
            $('#BodyHolder_divTurnInRAR').html(msg.d);
            $('#BodyHolder_divTurnInRAR').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnRARSEP",
        data: "{returnno: '" + returnno + "', Remarks: '" + Remarks + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "',CustodianName: '" + CustodianName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInRARSEP').hide();
            $('#BodyHolder_divTurnInRARSEP').html(msg.d);
            $('#BodyHolder_divTurnInRARSEP').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/GenerateViewListReturnPPE",
        data: "{returnno: '" + returnno + "', Remarks: '" + Remarks + "',employeename: '" + employeename + "',daterequested: '" + daterequested + "',CustodianName: '" + CustodianName + "',EmployeePosition: '" + EmployeePosition + "',CustodianPosition: '" + CustodianPosition + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divTurnInReturnPPE').hide();
            $('#BodyHolder_divTurnInReturnPPE').html(msg.d);
            $('#BodyHolder_divTurnInReturnPPE').fadeIn(200);
        }
    });
}

function loadreturn_pending_list() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/PropertyReturnList",
        data: "{ returnno : '" + returnnumber + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#purl').hide();
            $('#purl').innerHTML = "";
            $('#purl').fadeIn(200);

            myJsonData = data;
            populateALPendingReturn(myJsonData);

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

function populateALPendingReturn(data) {
    try {
        $('#tblPURL').DataTable({
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
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Condition" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Return'
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

function CancelReturn(returnno) {
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
    textareatt.id = "cancelreturn";

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

            var textremarks = $('#cancelreturn').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }

            $.ajax({
                type: "POST",
                url: url + "/PropertyReturn.aspx/CancelReturn",
                data: "{ returnno: '" + returnno + "', remarks: '" + textremarks + "'  }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('.btn').prop('disabled', false);
                    //alert("Property successfully Request for Return");
                    swal("Successful", "Return Cancelled Successfully.", "success");
                    $('#ReturnList').modal('hide');
                    load_data();

                    load_data_returnPendingupload();
                }
            });
        } else {
            $('.btn').prop('disabled', false);
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    ////}
}

function UploadPropertyReturn() {
    var alertMsg;
    var requestno = $('#BodyHolder_txtReturnRequestNumber').val();
    var remarks = $('#BodyHolder_txtRemarks').val();
    var filess = $('#BodyHolder_AFupload').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    if (filess == "" || remarks == "") {
        alertMsg = "";
        if (ReplaceStr1(remarks.trim().replace(/  +/g, ' ')) == '')
            alertMsg += 'Remarks';
        if (filess == '')
            alertMsg += '\nSelect (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.';
        swal("Warning", alertMsg, "warning");
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
                $(function () {
                    var fileUpload = $("#BodyHolder_AFupload").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + requestno + "|PropertyReturn",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyReturn.aspx/UploadReturnRequest",
                                    data: " { returnrequestno : '" + requestno + "',remarks : '" + remarks + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_returnPendingupload();
                                        load_data_Pendingupload();
                                        $('#AssetListModal').modal('hide');
                                        swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                    }
                                });
                            } else {
                                swal("Warning", result, "warning");
                            }
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



function UploadPropertyReturn1() {
    var alertMsg;
    var requestno = $('#BodyHolder_TextBox1').val();
    var remarks = $('#BodyHolder_txtRemarksRequest').val();
    var filess = $('#BodyHolder_FileUpload1').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    if (filess == "" || remarks == "") {
        alertMsg = "";
        if (ReplaceStr1(remarks.trim().replace(/  +/g, ' ')) == '')
            alertMsg += 'Remarks';
        if (filess == '')
            alertMsg += '\nSelect (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.';
        swal("Warning", alertMsg, "warning");
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
                $(function () {
                    var fileUpload = $("#BodyHolder_FileUpload1").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + requestno + "|PropertyReturn",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyReturn.aspx/UploadReturnRequest",
                                    data: " { returnrequestno : '" + requestno + "',remarks : '" + remarks + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_returnPendingupload();
                                        load_data_Pendingupload();
                                        $('#AssetListRequest').modal('hide');
                                        swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                    }
                                });
                            } else {
                                swal("Warning", result, "warning");
                            }
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

function changeCondition(recid, ddlconditionid) {
    var condition = $("#ddlCondition" + ddlconditionid).val();
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/changeCondition",
        data: "{recid : '" + recid + "',condition: '" + condition + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}

function changeCondition1(recid, ddlconditionid) {
    var condition = $("#ddlConditionr" + ddlconditionid).val();
    $.ajax({
        type: "POST",
        url: url + "/PropertyReturn.aspx/changeConditionRequest",
        data: "{recid : '" + recid + "',condition: '" + condition + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}