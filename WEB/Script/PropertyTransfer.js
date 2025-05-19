var url = '';

$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    IfTransferNeedsApproval();
    //GetWarehouseByID();
    GetsitePT();
    GetsiteToPT();
    GetEmployeePT();
    $('#BodyHolder_btnComplete').fadeIn();
    //$('#BodyHolder_AFupload').hide();
    //$('#BodyHolder_uploadAF').hide();

    //GetLocation();
    //GetToLocation();
    $('#itrxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'ITR' + "', key2: '" + transsno + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "ITR_" + formattedDateTime + ".xls");
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
    $('#parxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'PAR' + "', key2: '" + transsno + "', key3: '" + 'PTransfer' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "PAR_" + formattedDateTime + ".xls");
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
    $('#ptrxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'PTR' + "', key2: '" + transsno + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "PTR_" + formattedDateTime + ".xls");
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
    $('#icsxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'ICS' + "', key2: '" + transsno + "', key3: '" + 'PTransfer' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "ICS_" + formattedDateTime + ".xls");
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
    $('#itrapprovedsignatureupload').off('change').on('change', function () {
        itrapprovedimagereadURL(this);
    });

    $('#itrreleasedsignatureupload').off('change').on('change', function () {
        itrreleaseimagereadURL(this);
    });

    $('#itrreceivedsignatureupload').off('change').on('change', function () {
        itrreceivedimagereadURL(this);
    });

    $('#itrapprovedpkicertificateupload').off('change').on('change', function () {
        itrapprovedcertreadURL(this);
    });

    $('#itrreleasedpkicertificateupload').off('change').on('change', function () {
        itrreleasecertreadURL(this);
    });

    $('#itrreceivedpkicertificateupload').off('change').on('change', function () {
        itrreceivedcertreadURL(this);
    });
    //$('#transferitrbtn').off('click').on('click', function () {
    //    $('#ITRsigandCertModal').modal('show');
    //});

    $('#btndownloaditrpdf').off('click').on('click', function () {
        var transno = $(this).attr('transno');
        var transtype = $(this).attr('transtype');

        var itrapprovedpkipassword = $('#itrapprovedpkipassword');
        var itrreleasedpkipassword = $('#itrreleasedpkipassword');
        var itrreceivedpkipassword = $('#itrreceivedpkipassword');

        var alertMsg = "Fill-out all the required field(s):\n";
        var hasError = false;

        if (itrapprovedpkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Approved By)";
        }
        if (itrreleasedpkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Released/Issued by)";
        }
        if (itrreceivedpkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Received By)";
        }
        if (itrapprovedimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Approved By)";
        }
        if (itrreleaseimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Released/Issued by)";
        }
        if (itrreceivedimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Received By)";
        }
        if (itrapprovedcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Approved By)";
        }
        if (itrreleasecertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Released/Issued by)";
        }
        if (itrreceivedcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Received By)";
        }
        if (hasError) {
            SweetAlertError(alertMsg);
            return;
        }

        var itrapproveddatestamp = $('#itrapproveddatestamp');
        var itrreleaseddatestamp = $('#itrreleaseddatestamp');
        var itrreceiveddatestamp = $('#itrreceiveddatestamp');

        $.ajax({
            type: "POST",
            url: url + "/PropertyTransfer.aspx/CreateITR",
            data: "{ issuanceno: '" + transno + "',  itrapprovedpkipassword: '" + itrapprovedpkipassword.val() + "', itrreleasedpkipassword: '" + itrreleasedpkipassword.val() + "', itrreceivedpkipassword: '" + itrreceivedpkipassword.val() + "', itrapprovedimagebase64: '" + itrapprovedimagebase64 + "', itrreleaseimagebase64: '" + itrreleaseimagebase64 + "', itrreceivedimagebase64: '" + itrreceivedimagebase64 + "', itrapprovedcertbase64: '" + itrapprovedcertbase64 + "', itrreleasecertbase64: '" + itrreleasecertbase64 + "', itrreceivedcertbase64: '" + itrreceivedcertbase64 + "', itrapproveddatestamp: '" + itrapproveddatestamp.prop('checked') + "', itrreleaseddatestamp: '" + itrreleaseddatestamp.prop('checked') + "', itrreceiveddatestamp: '" + itrreceiveddatestamp.prop('checked') + "' }",
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
                        a.attr("download", "Property_Transfer.pdf");
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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
    $('#tblPUL1').DataTable({
        "scrollX": false,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        "columnDefs": [
                { "width": "100px", "targets": 0 },
                { "width": "200px", "targets": 1 },
                { "width": "100px", "targets": 2 },
                { "width": "200px", "targets": 3 },
                { "width": "200px", "targets": 4 }
        ],
    });
    //$("#tblTransferList").DataTable({
    //    "scrollX": true
    //});
    $("#tblPendingUpload").DataTable({
        "scrollX": true
        , "order": []
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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
    GetUserCcustodian();
    $('#ddlCustodian').on('change', function () {
        load_data();
        //load_data_Pendingupload();
        //load_data_pendinguploadRequest();
    });
    //load_data();
    load_data_Pendingupload();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblTransferList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();

    //document.getElementById('chkWithDoc').checked == false;
    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_AFupload').fadeIn();
            $('#BodyHolder_uploadAF').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_AFupload').hide();
            $('#BodyHolder_uploadAF').hide();
        }
    });
});

var itrapprovedimagebase64 = "";
var itrreleaseimagebase64 = "";
var itrreceivedimagebase64 = "";

var itrapprovedcertbase64 = "";
var itrreleasecertbase64 = "";
var itrreceivedcertbase64 = "";

function itrreceivedcertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            input.val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                itrreceivedcertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function itrreleasecertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            input.val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                itrreleasecertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function itrapprovedcertreadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            input.val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                itrapprovedcertbase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function itrreceivedimagereadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            input.val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                itrreceivedimagebase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function itrreleaseimagereadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            input.val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                itrreleaseimagebase64 = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function itrapprovedimagereadURL(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size > 2097152) {
            SweetAlertError("File is too big.");
            input.val('');
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                itrapprovedimagebase64 = e.target.result;
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
        url: url + "/PropertyTransfer.aspx/GetUserCustodian",
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

function GetSite() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyTransfer.aspx/GetSite",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}

var warehouseid = 0;
//function GetWarehouseByID() {
//    $.ajax({
//        type: "POST",
//        url: url + "/PropertyTransfer.aspx/GetWarehouseByID",
//        data: "{ }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            //$('#txtWarehouseName').val(msg.d.split('|')[1]);
//            warehouseid = msg.d.split('|')[0];

//            var ddlFromSite = $("[id*=ddlFromSite]");
//            ddlFromSite.empty();//.append('<option selected="selected" value="0">--- Select Location ---</option>');
//            ddlFromSite.append('<option value="' + msg.d.split('|')[0] + '">' + msg.d.split('|')[1] + '</option>');

//            GetLocation();
//        }
//    });
//}

var needsapproval = "False";
function IfTransferNeedsApproval() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyTransfer.aspx/IfTransferNeedsApproval",
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
function GetEmployeePT() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyTransfer.aspx/GetUserNoDepartment",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlEmployee = $("[id*=ddlFromPersonPT]");
            ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
            $.each(msg.d, function () {
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
            var ddlToEmployee = $("[id*=ddlTopersonPT]");
            ddlToEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
            $.each(msg.d, function () {
                ddlToEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (msg1) {
            var ddlEmployee = $("[id*=ddlFromPersonPT]");
            ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
            ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            var ddlToEmployee = $("[id*=ddlTopersonPT]");
            ddlToEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
            ddlToEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
        }
    });
}

function GetEmployeeToperson() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyTransfer.aspx/GetUserNoDepartment",
        data: " { } ",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ddlToEmployee = $("[id*=ddlTopersonPT]");
            ddlToEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
            $.each(msg.d, function () {
                ddlToEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (msg1) {

            var ddlToEmployee = $("[id*=ddlTopersonPT]");
            ddlToEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
            ddlToEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
        }
    });
}

//function GetLocation() {
//    warehouseid = $('#BodyHolder_ddlFromSite').val();
//    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
//    //    warehouse = 1;
//    $.ajax({
//        type: "POST",
//        url: url + "/PropertyTransfer.aspx/GetLocationListLT",
//        data: " { warehouse : '" + warehouseid + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            var ddlLocation = $("[id*=ddlFromLocation]");
//            ddlLocation.empty();//.append('<option selected="selected" value="0">--- Select Location ---</option>');
//            ddlLocation.append('<option value="1">Receiving Location</option>');
//            $.each(msg.d, function () {
//                ddlLocation.append($("<option></option>").val(this['Text']).html(this['Value']));
//            });
//            GetOfficeLists();
//        }
//    });
//}

//function GetToLocation() {
//    var warehouse = $('#BodyHolder_ddlToSite').val();
//    if ($('#BodyHolder_ddlToSite').val() == 'Receiving Site')
//        warehouse = 1;

//    $.ajax({
//        type: "POST",
//        url: url + "/PropertyTransfer.aspx/GetToLocationList",
//        data: " { warehouse : '" + warehouse + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            var ddlLocation = $("[id*=ddlToLocation]");
//            ddlLocation.empty();//.append('<option selected="selected" value="0">--- Select Location ---</option>');
//            ddlLocation.append('<option value="1">Receiving Location</option>');
//            $.each(msg.d, function () {
//                ddlLocation.append($("<option></option>").val(this['Text']).html(this['Value']));
//            });
//            GetToOfficeLists();
//        }
//    });
//}

//function GetOfficeLists() {
//    var location = $('#BodyHolder_ddlFromLocation').val();
//    $.ajax({
//        type: "POST",
//        url: url + "/PropertyTransfer.aspx/GetOfficeLists",
//        data: " { location : '" + location + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            var ddlOffice = $("[id*=ddlFromOffice]");
//            if (location == 0) {
//                ddlOffice.empty().append('<option selected="selected" value="0">--- Select Area ---</option>');
//            }
//            else {
//                ddlOffice.empty().append('<option selected="selected" value="0">--- Select Area ---</option>');
//                $.each(msg.d, function () {
//                    ddlOffice.append($("<option></option>").val(this['Text']).html(this['Value']));
//                });
//            }
//            load_data();
//        }
//    });
//}

//function GetToOfficeLists() {
//    var location = $('#BodyHolder_ddlToLocation').val();
//    $.ajax({
//        type: "POST",
//        url: url + "/PropertyTransfer.aspx/GetToOfficeLists",
//        data: " { location : '" + location + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            var ddlOffice = $("[id*=ddlToOffice]");
//            if (location == 0) {
//                ddlOffice.empty().append('<option selected="selected" value="0">--- Select Area ---</option>');
//            } else {
//                ddlOffice.empty().append('<option selected="selected" value="0">--- Select Area ---</option>');
//                $.each(msg.d, function () {
//                    ddlOffice.append($("<option></option>").val(this['Text']).html(this['Value']));
//                });
//            }
//            GetCount();
//            load_data();
//        }
//    });
//}

function load_data() {
    warehouseid = $('#ddlFromSitePT').val();
    var search = "";
    var custodian = $('#ddlCustodian').val();
    //var warehouse = $('#BodyHolder_ddlFromSite').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }

    var tosite = $('#ddlToSitePT').val();

    var location = $('#ddlFromLocationPT').val();
    if ($('#ddlFromLocationPT').val() == '1')
        location = 1;

    var office = $('#ddlFromOfficePT').val();
    if ($('#ddlFromOfficePT').val() == '0' || $('#ddlFromOfficePT').val() == null)
        office = 0;

    var TrasferType = $('#BodyHolder_ddlTrasferType').val();
    var Employee = $('#ddlFromPersonPT').val();

    $.ajax({
        type: 'POST',
        url: url + "/PropertyTransfer.aspx/GetList",
        data: "{ site: '" + warehouseid + "', location: '" + location + "', area: '" + office + "' ,tosite : '" + tosite + "', search: '" + search + "', custodianid: '" + custodian + "', employeeID: '" + Employee + "', TrasferType: '" + TrasferType + "', ExpenseType: '" + ExpenseType + "'}", //
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData);
            GetCount();
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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
                    text: 'Add to Transfer List',
                    action: function () {
                        var location = $('#ddlFromLocationPT').val();
                        //if ($('#ddlFromLocationPT').val() == '--- Select Location ---')
                        //    location = 0;
                        //if ($('#ddlFromLocationPT').val() == 'Receiving Location')
                        //    location = 1;
                        var office = $('#ddlFromOfficePT').val();
                        if ($('#ddlFromLocationPT').val() == '0')
                            office = 0;
                        var towarehouse = $('#ddlToSitePT').val();
                        //if ($('#BodyHolder_ddlToSite').val() == 'Receiving Site')
                        //    towarehouse = 1;
                        //if ($('#BodyHolder_ddlToSite').val() == '--- Select Site ---')
                        //    towarehouse = 0;
                        var tolocation = $('#ddlToLocationPT').val();
                        //if ($('#BodyHolder_ddlToLocation').val() == '--- Select Location ---')
                        //    tolocation = 0;
                        var tooffice = $('#ddlToOfficePT').val();
                        //if ($('#BodyHolder_ddlToOffice').val() == '--- Select Area ---')
                        //    tooffice = 0;
                        var trasferType = $('#BodyHolder_ddlTrasferType').val();
                        var employeeIDFrom = $('#ddlFromPersonPT').val();
                        var employeeIDTo = $('#ddlTopersonPT').val();
                        var custodian = $('#ddlCustodian').val();
                        var proceed = 'False';
                        if (trasferType == 2) {
                            if (towarehouse == 0 || tolocation == 0 || tooffice == 0) {
                                //alert('Required Field(s):\n-To Site\n-To Location\n-To Area');
                                swal("Required Field(s):", "\n-To Location\n-To Department\n-To Group", "warning")
                            }
                            else {
                                proceed = 'True';
                            }
                        }
                        else {
                            if (employeeIDTo == 0) {
                                swal("Required Field(s):", "\n-To Person", "warning")
                            }
                            else {
                                proceed = 'True';
                            }
                        }
                        var expense = $('#BodyHolder_ddlItem').val();
                        var ExpenseType = '';
                        if (expense.includes('ICS LOW')) {
                            ExpenseType = '2'
                        }
                        else if (expense.includes('ICS HIGH')) {
                            ExpenseType = '3'
                        }
                        else if (expense.includes('PAR')) {
                            ExpenseType = '1'
                        }
                        else if (expense.includes('ITR')) {
                            ExpenseType = '4'
                        }
                        else {
                            ExpenseType = '0'
                        }
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (proceed == 'True' && count != 0) {
                            var ids = $.map($('#tblAssetList').DataTable().rows('.selected').data(), function (item) {
                                return item.pmlid;
                            });
                            console.log(ids);
                            ids.forEach((pmlid) => {
                                $(".se-pre-con").fadeIn(100);
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyTransfer.aspx/TransferToTempTable",
                                    data: "{ pmlid :  '" + pmlid + "', fromsite: '" + warehouseid + "', fromlocation: '" + location + "', fromarea : '" + office + "',tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea : '" + tooffice + "',custodian:'" + custodian + "',transferType:'" + trasferType + "',employeeIDFrom:'" + employeeIDFrom + "',employeeIDTo:'" + employeeIDTo + "',ExpenseType:'" + ExpenseType + "'} ",
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
                        else {
                            swal("Warning", "Please select an item to transfer", "warning");
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
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(0)").text();

            //var warehouse = $('#BodyHolder_ddlFromSite').val();
            //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
            //    warehouse = 1;

            var expense = $('#BodyHolder_ddlItem').val();
            var ExpenseType = '';
            if (expense.includes('ICS LOW')) {
                ExpenseType = '2'
            }
            else if (expense.includes('ICS HIGH')) {
                ExpenseType = '3'
            }
            else if (expense.includes('PAR')) {
                ExpenseType = '1'
            }
            else if (expense.includes('ITR')) {
                ExpenseType = '4'
            }
            else {
                ExpenseType = '0'
            }
            var location = $('#ddlFromLocationPT').val();
            if ($('#ddlFromLocationPT').val() == '0')
                location = 0;
            if ($('#ddlFromLocationPT').val() == '1')
                location = 1;
            var office = $('#ddlFromOfficePT').val();
            if ($('#ddlFromOfficePT').val() == '0')
                office = 0;

            var towarehouse = $('#ddlToSitePT').val();
            var tolocation = $('#ddlToLocationPT').val();
            var tooffice = $('#ddlToOfficePT').val();
            if ($('#ddlToOfficePT').val() == '0')
                tooffice = 0;

            var trasferType = $('#BodyHolder_ddlTrasferType').val();
            var employeeIDFrom = $('#ddlFromPersonPT').val();
            var employeeIDTo = $('#ddlTopersonPT').val();

            var custodian = $('#ddlCustodian').val();
            var proceed = 'False';
            if (trasferType == 2) {
                if (towarehouse == 0 || tolocation == 0 || tooffice == 0) {
                    //alert('Required Field(s):\n-To Site\n-To Location\n-To Area');
                    swal("Required Field(s):", "\n-To Location\n-To Department\n-To Group", "warning")
                }
                else {
                    proceed = 'True';
                }
            }
            else {
                if (employeeIDTo == 0) {
                    swal("Required Field(s):", "\n-To Person", "warning")
                }
                else {
                    proceed = 'True';
                }
            }

            if (proceed == 'True') {
                $.ajax({
                    type: "POST",
                    url: url + "/PropertyTransfer.aspx/TransferToTempTable",
                    data: "{ pmlid :  '" + pmlid + "', fromsite: '" + warehouseid + "', fromlocation: '" + location + "', fromarea : '" + office + "',tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea : '" + tooffice + "',custodian:'" + custodian + "',transferType:'" + trasferType + "',employeeIDFrom:'" + employeeIDFrom + "',employeeIDTo:'" + employeeIDTo + "',ExpenseType:'" + ExpenseType + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#assetlist').hide();
                        $('#assetlist').innerHTML = "";
                        $('#assetlist').fadeIn(200);
                        GetCount();
                        $('#tblAssetList').DataTable().rows(currentRow)
                      .remove()
                        .draw(false);
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

var GetCounter = 0;
function GetCount() {
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }

    warehouseid = $('#ddlFromSitePT').val();
    //var warehouse = $('#BodyHolder_ddlFromSite').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    var location = $('#ddlFromLocationPT').val();
    if ($('#ddlFromLocationPT').val() == '0')
        location = 0;
    if ($('#ddlFromLocationPT').val() == '0')
        location = 1;
    var office = $('#ddlFromOfficePT').val();
    if ($('#ddlFromOfficePT').val() == '0')
        office = 0;

    var towarehouse = $('#ddlToSitePT').val();
    var tolocation = $('#ddlToLocationPT').val();
    var tooffice = $('#ddlToOfficePT').val();
    if ($('#ddlToOfficePT').val() == '0')
        tooffice = 0;

    var TransferType = $('#BodyHolder_ddlTrasferType').val();

    var FromEmployeeID = $('#ddlFromPersonPT').val();
    var ToEmployeeID = $('#ddlTopersonPT').val();

    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: "POST",
        url: url + "/PropertyTransfer.aspx/PropertyTransferGetCount",
        data: "{ fromsite: '" + warehouseid + "', fromlocation: '" + location + "', fromarea: '" + office + "', tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea: '" + tooffice + "', custodian: '" + custodian + "', FromEmployeeID: '" + FromEmployeeID + "', ToEmployeeID: '" + ToEmployeeID + "', TransferType: '" + TransferType + "',ExpenseType: '" + ExpenseType + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Property List for Transfer: " + msg.d);
            if (msg.d == 0) {
                $('#transferItemList').modal('hide');
            }
            else {
            }
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

function transferItem() {
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val('--- Select Approver ---').trigger('change');
    load_data();
    load_data_transferlist();
    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
        //alert("There is No Property for Transfer");
        swal("Warning", "There is No Property for Transfer", "warning");
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;

        $('#transferItemList').modal('show');
    }
}

function load_data_transferlist() {
    warehouseid = $('#ddlFromSitePT').val();
    //var warehouse = $('#BodyHolder_ddlFromSite').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }
    var custodian = $('#ddlCustodian').val();
    var location = $('#ddlFromLocationPT').val();
    if ($('#ddlFromLocationPT').val() == '0')
        location = 0;
    if ($('#ddlFromLocationPT').val() == '1')
        location = 1;
    var office = $('#ddlFromOfficePT').val();
    if ($('#ddlFromOfficePT').val() == '0')
        office = 0;

    var towarehouse = $('#ddlToSitePT').val();
    var tolocation = $('#ddlToLocationPT').val();
    var tooffice = $('#ddlToOfficePT').val();
    if ($('#ddlToOfficePT').val() == '0')
        tooffice = 0;

    var TransferType = $('#BodyHolder_ddlTrasferType').val();

    var FromEmployeeID = $('#ddlFromPersonPT').val();
    var ToEmployeeID = $('#ddlTopersonPT').val();

    $.ajax({
        type: 'POST',
        url: url + "/PropertyTransfer.aspx/GetToTransferAssetList",
        data: "{ fromsite: '" + warehouseid + "', fromlocation: '" + location + "',  fromarea : '" + office + "', tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea: '" + tooffice + "', custodian: '" + custodian + "', FromEmployeeID: '" + FromEmployeeID + "', ToEmployeeID: '" + ToEmployeeID + "', TransferType: '" + TransferType + "', ExpenseType : '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#transferlist').hide();
            $('#transferlist').innerHTML = "";
            $('#transferlist').fadeIn(200);

            myJsonData = data;
            populateDataTableForIssuance(myJsonData);

            console.log(data);
            console.log('Success');
            //$('#transferItemList').modal('show');
            GetCount();
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
        $('#tblTransferList').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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

        $('#tblTransferList').off('click');

        $('#tblTransferList').on('click', 'button.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(0)").text();
            var trasferType = $('#BodyHolder_ddlTrasferType').val();
            tooffice = 0;
            //if (confirm("Remove this Property?") == false) { return false; }
            $.ajax({
                type: "POST",
                url: url + "/PropertyTransfer.aspx/RemoveToTransfer",
                data: "{pmlid: '" + pmlid + "', trasferType:'" + trasferType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#transferlist').hide();
                    $('#transferlist').innerHTML = "";
                    $('#transferlist').fadeIn(200);
                    load_data();
                    GetCount();
                    currentRow.remove();
                }
            });
        });
    } catch (e) {
        var table = $('#tblTransferList').DataTable();
        table
            .clear()
            .draw();
    }
}

function RemoveAllToTransfer() {
    warehouseid = $('#ddlFromSitePT').val();
    //var warehouse = $('#BodyHolder_ddlFromSite').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }
    var custodian = $('#ddlCustodian').val();
    var location = $('#ddlFromLocationPT').val();
    if ($('#ddlFromLocationPT').val() == '1')
        location = 1;
    var office = $('#ddlFromOfficePT').val();
    if ($('#ddlFromOfficePT').val() == '0')
        office = 0;

    var towarehouse = $('#ddlToSitePT').val();
    var tolocation = $('#ddlToLocationPT').val();
    var tooffice = $('#ddlToOfficePT').val();
    if ($('#ddlToOfficePT').val() == '0')
        tooffice = 0;
    var trasferType = $('#BodyHolder_ddlTrasferType').val();
    var employeeIDFrom = $('#ddlFromPersonPT').val();
    var employeeIDTo = $('#ddlTopersonPT').val();

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
                url: url + "/PropertyTransfer.aspx/RemoveAllToTransfer",
                data: "{ fromsite: '" + warehouseid + "', fromlocation: '" + location + "',  fromarea : '" + office + "', tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea: '" + tooffice + "',custodian:'" + custodian + "',trasferType:'" + trasferType + "',employeeIDFrom:'" + employeeIDFrom + "',employeeIDTo:'" + employeeIDTo + "', Expensetype: '" + ExpenseType + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_transferlist();
                    GetCount();
                    $('#transferItemList').modal('hide');
                    swal("Successful", "Property List cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmPropertyTransfer() {
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = '';
    if (expense.includes('ICS LOW')) {
        ExpenseType = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        ExpenseType = '3'
    }
    else if (expense.includes('PAR')) {
        ExpenseType = '1'
    }
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }

    warehouseid = $('#ddlFromSitePT').val();
    $("#spin").fadeIn(100);
    $('.btn').prop('disabled', true);
    //alert($('#BodyHolder_txtRemarks').val());
    //var warehouse = $('#BodyHolder_ddlFromSite').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    var custodian = $('#ddlCustodian').val();
    var location = $('#ddlFromLocationPT').val();
    if ($('#ddlFromLocationPT').val() == '0')
        location = 0;
    if ($('#ddlFromLocationPT').val() == '1')
        location = 1;
    var office = $('#ddlFromOfficePT').val();
    if ($('#ddlFromOfficePT').val() == '0')
        office = 0;

    var towarehouse = $('#ddlToSitePT').val();
    var tolocation = $('#ddlToLocationPT').val();
    var tooffice = $('#ddlToOfficePT').val();
    if ($('#ddlToOfficePT').val() == '0')
        tooffice = 0;

    var TransferType = $('#BodyHolder_ddlTrasferType').val();
    var FromEmployeeID = $('#ddlFromPersonPT').val();
    var ToEmployeeID = $('#ddlTopersonPT').val();

    var alertMsg = "";
    if (needsapproval == "True") {
        if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            //alertMsg = 'Fill-out all the required field(s): ';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nPurpose ";
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
                alertMsg += "\nApprover";

            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
            $("#spin").hide();
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
                    //   alert("{ approverid : '" + $('#BodyHolder_ddlApprover').val() + "', purpose : '" + $('#BodyHolder_txtRemarks').val() + "', fromsite: '" + warehouseid + "', fromlocation: '" + location + "',fromarea: '" + office + "',tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea: '" + tooffice + "', custodianid : '" + custodian + "', FromEmployeeID : '" + FromEmployeeID + "', ToEmployeeID : '" + ToEmployeeID + "', TransferType : '" + TransferType + "', ExpenseType : '" + ExpenseType + "'} ");

                    $(".se-pre-con").fadeIn();
                    $.ajax({
                        type: "POST",
                        url: url + "/PropertyTransfer.aspx/ConfirmPropertyTransfer",
                        data: "{ approverid : '" + $('#BodyHolder_ddlApprover').val() + "', purpose : '" + $('#BodyHolder_txtRemarks').val() + "', fromsite: '" + warehouseid + "', fromlocation: '" + location + "',fromarea: '" + office + "',tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea: '" + tooffice + "', custodianid : '" + custodian + "', FromEmployeeID : '" + FromEmployeeID + "', ToEmployeeID : '" + ToEmployeeID + "', TransferType : '" + TransferType + "', ExpenseType : '" + ExpenseType + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested for Transfer and Pending For Approval.\n\nMail not sent. Please check you mail settings.');
                                swal("Successful", "Successfully Requested for Transfer and Pending For Approval.\n\nMail not sent. Check you mail settings.", "success"); load_data();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#transferItemList').modal('hide');
                                $("#spin").hide();
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested for Transfer and Pending For Approval');
                                swal("Successful", "Successfully Requested for Transfer and Pending For Approval.", "success");
                                load_data();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#transferItemList').modal('hide');
                                $("#spin").hide();

                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
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
        if ($('#BodyHolder_txtRemarks').val().trim() == "") {
            //alertMsg = 'Fill-out all the required field(s): ';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nPurpose ";

            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
            $("#spin").hide();
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
                        url: url + "/PropertyTransfer.aspx/ConfirmPropertyTransfer",
                        data: "{ approverid : '0', purpose : '" + $('#BodyHolder_txtRemarks').val() + "', fromsite: '" + warehouseid + "', fromlocation: '" + location + "',fromarea: '" + office + "',tosite: '" + towarehouse + "', tolocation: '" + tolocation + "', toarea: '" + tooffice + "', custodianid : '" + custodian + "', FromEmployeeID : '" + FromEmployeeID + "', ToEmployeeID : '" + ToEmployeeID + "', TransferType : '" + TransferType + "', ExpenseType : '" + ExpenseType + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                swal("Successful", "Successfully Requested for Transfer and Pending For Upload.\n\nMail not sent. Check you mail settings.", "success");
                                load_data();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#transferItemList').modal('hide');
                                $("#spin").hide();
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                swal("Successful", "Successfully Requested for Transfer and Pending For Upload", "success");
                                load_data();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#transferItemList').modal('hide');
                                $("#spin").hide();

                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                } else {
                    $('.btn').prop('disabled', false);
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        }
    }
}

function load_data_Pendingupload() {
    var search = "";
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyTransfer.aspx/PropertyTransferPendingApproval",
        data: "{ search : '" + search + "',custodian: '" + custodian + "' }",
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
                { "data": "TransferNumber" },
                { "data": "RemarksTransfer" },
                //{ "data": "StringNoOfItem" },
                { "data": "Status" },
                { "data": "CreatedBy" },
                { "data": "DateOfTransfer" },
                { "data": "Action" }
                //DateOfTransaction
            ]

            , "order": []
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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
var actionCol = true;
var transsno = '';
var ttype = '';
function ViewAssets(transactionno, counter, counterchecker, isapproved, transactiontype, parics) {
    $('#BodyHolder_AFupload').val('');
    transsno = transactionno;
    ttype = transactiontype;
    $('#BodyHolder_txtTransactionNo').val(transactionno);

    if (transactiontype == 2) {
        actionCol = false;
        document.getElementById("isTransfer").style.display = "none";
    }
    else {
        document.getElementById("isTransfer").style.display = "block";
        if (parics == '1') {
            document.getElementById("printGrid").children[0].style.display = "none";
            document.getElementById("printGrid").children[1].style.display = "none";
            document.getElementById("printGrid").children[2].style.display = "none";
            document.getElementById("printGrid").children[3].style.display = "none";
            document.getElementById("printGrid").children[4].style.display = "none";
            document.getElementById("printGrid").children[5].style.display = "none";
            document.getElementById("printGrid").children[6].style.display = "none";
            document.getElementById("printGrid").children[7].style.display = "inline";
            document.getElementById("printGrid").children[8].style.display = "inline";
        } else if (parics == '2' || parics == '3') {
            document.getElementById("printGrid").children[0].style.display = "none";
            document.getElementById("printGrid").children[1].style.display = "none";
            document.getElementById("printGrid").children[2].style.display = "none";
            document.getElementById("printGrid").children[3].style.display = "none";
            document.getElementById("printGrid").children[4].style.display = "inline";
            document.getElementById("printGrid").children[5].style.display = "inline";
            document.getElementById("printGrid").children[6].style.display = "inline";
            document.getElementById("printGrid").children[7].style.display = "none";
            document.getElementById("printGrid").children[8].style.display = "none";
        } else {
            document.getElementById("printGrid").children[0].style.display = "none";
            document.getElementById("printGrid").children[1].style.display = "none";
            document.getElementById("printGrid").children[2].style.display = "none";
            document.getElementById("printGrid").children[3].style.display = "none";
            document.getElementById("printGrid").children[4].style.display = "none";
            document.getElementById("printGrid").children[5].style.display = "none";
            document.getElementById("printGrid").children[6].style.display = "none";
            document.getElementById("printGrid").children[7].style.display = "none";
            document.getElementById("printGrid").children[8].style.display = "none";
        }

        //gheghe(Certificate/Word)
        //document.getElementById("printGrid").children[3].style.display = "none";

        $.ajax({
            type: "POST",
            url: url + "/PropertyTransfer.aspx/GenerateTransferReportITR",
            data: "{transferno : '" + transactionno + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divGatePass').hide();
                $('#BodyHolder_divGatePass').html(msg.d);
                $('#BodyHolder_divGatePass').fadeIn(200);
            }
        });

        $.ajax({
            type: "POST",
            url: url + "/PropertyTransfer.aspx/GenerateTransferReportPTR",
            data: "{transferno : '" + transactionno + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divIssuanceReportPTR').hide();
                $('#BodyHolder_divIssuanceReportPTR').html(msg.d);
                $('#BodyHolder_divIssuanceReportPTR').fadeIn(200);
            }
        });
        $.ajax({
            type: "POST",
            async: false,
            url: url + "/PropertyTransfer.aspx/GenerateIssuanceReportPAR",
            data: "{transferno: '" + transactionno + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                console.log(msg.d);
                $('#BodyHolder_divIssuanceReportPAR').show();
                $('#BodyHolder_divIssuanceReportPAR').html(msg.d);
                $('#BodyHolder_divIssuanceReportPAR').fadeIn(200);
            }
        });
        $.ajax({
            type: "POST",
            async: false,
            url: url + "/PropertyTransfer.aspx/GenerateIssuanceReportICS",
            data: "{transferno: '" + transactionno + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_divIssuanceReportICS').show();
                $('#BodyHolder_divIssuanceReportICS').html(msg.d);
                $('#BodyHolder_divIssuanceReportICS').fadeIn(200);
            }
        });
    }

    $('#btndownloaditrpdf').attr('transno', transactionno);

    $('#btndownloadcreatepdf').attr('transno', transactionno);
    var x = document.getElementById("showorhide");
    if (counter == counterchecker) {
        if (isapproved == "True") {
            load_data_PUL1();
            $('#ALPendingApproval').modal('show');
            x.style.display = "inline";
        }
        else {
            load_data_PUL1();
            $('#ALPendingApproval').modal('show');
            x.style.display = "none";
        }
    }
    else {
        load_data_PUL1();
        $('#ALPendingApproval').modal('show');
        x.style.display = "none";
    }

}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyTransfer.aspx/PLPendingApproval",
        data: "{transactionno: '" + transsno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pul1').hide();
            $('#pul1').innerHTML = "";
            $('#pul1').fadeIn(200);

            myJsonData = data;

            populateDataTablePUL1(myJsonData);

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

function populateDataTablePUL1(data) {
    try {
        $('#tblPUL1').DataTable({
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
            "autoWidth": true,
            "scrollX": false,
            //"dom": 'Bfrtip',
            //buttons: [
            //'print'
            //],
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "Origin" },
                { "data": "Destination" },
                //{ "data": "Action" }
            ],
            //"rowCallback": function (nRow, aData, iDisplayIndex) {
            //    if (aData != null && aData != "") {
            //        $('td', nRow).eq(0).css({ "background-color": "#ffffff" });
            //        $('td', nRow).eq(0).css({ "overflow-y": "hidden" });
            //        $('td', nRow).eq(0).css({ "overflow-x": "hidden" });
            //    }
            //},
            "columnDefs": [
                { "width": "100px", "targets": 0 },
                { "width": "200px", "targets": 1 },
                { "width": "100px", "targets": 2 },
                { "width": "200px", "targets": 3 },
                { "width": "200px", "targets": 4 },
                //{ "visible": "false", "targets": 5 }

                //{ 'visible': actionCol, 'targets': 5 }

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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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

        $('#tblPUL1').off('click');
    } catch (e) {
        var table = $('#tblPUL1').DataTable();
        table
            .clear()
            .draw();
    }
}

function CheckTransactionType(PMLID, PARICS) {
    if (PARICS == 1) {

        PrintGridDataPAR();
    }
    else // if (PARICS == 2)
    {

        PrintGridDataICS();
    }
}

//function PrintGridDataPAR1() {
//    var prtGrid = document.getElementById('<%=printablepagePAR.ClientID %>');
//    var prtwin = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
//    prtwin.document.write(prtGrid.innerHTML);
//    prtwin.document.close();

//    prtwin.focus();
//    prtwin.print();
//    prtwin.close();
//}

function load_data_PUL2() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyTransfer.aspx/ALPendingApprovalV2",
        data: "{transactionno: '" + transsno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pul2').hide();
            $('#pul2').innerHTML = "";
            $('#pul2').fadeIn(200);

            myJsonData = data;

            populateDataTablePUL2(myJsonData);

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

function populateDataTablePUL2(data) {
    try {
        $('#tblPUL2').DataTable({
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
                { "data": "AssetNumber" },
                { "data": "Description" },
                { "data": "Serial" },
                { "data": "From" },
                { "data": "To" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
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

        $('#tblPUL2').off('click');
    } catch (e) {
        var table = $('#tblPUL2').DataTable();
        table
            .clear()
            .draw();
    }
}

function CancelTransaction(transno) {
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
    textareatt.id = "canceltransfer";


    swal({
        title: "Cancel Transaction",
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

            var textremarks = $('#canceltransfer').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: url + "/PropertyTransfer.aspx/CancelPropertyTransfer",
                data: "{transactionno: '" + transno + "', remarks: '" + textremarks + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    //alert("Property Transfer cancelled successfully.");
                    swal("Successful", "Property Transfer cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ShowTransfertype() {
    var transfertype = $('#BodyHolder_ddlTrasferType').val();

    //alert(transfertype);

    if (transfertype == 1) {
        $('#showorHide').show();
        $('#SourceLocation').hide();
        $('#sourcePerson').show();
        $('#DestinationLocation').hide();
        $('#DestinationPerson').show();
        GetEmployeePT();
    }
    else if (transfertype == 0) {
        $('#showorHide').hide();
        $('#SourceLocation').hide();
        $('#sourcePerson').hide();
        $('#DestinationLocation').hide();
        $('#DestinationPerson').hide();
    }
    else {
        $('#showorHide').hide();
        $('#SourceLocation').show();
        $('#sourcePerson').hide();
        $('#DestinationLocation').show();
        $('#DestinationPerson').hide();
    }

    //load_data();
}

function GetListPerPerson() {
    var userID = $('#ddlFromPersonPT').val();
    //alert(userID);
    load_data();
}

function UploadStockIssuance() {
    var alertMsg;
    var issuanceno = $('#BodyHolder_txtTransactionNo').val();
    var filess = $('#BodyHolder_AFupload').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    if (ttype == '1') {
        if (filess == "") {
            swal("", "Select a file to upload.", "warning");
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
                            url: url + "/OperationUploadHandler.ashx?transactionnumber=" + issuanceno + "|PropertyTransfer",
                            type: "POST",
                            contentType: false,
                            processData: false,
                            data: test,
                            success: function (result) {
                                if (result.includes("File Uploaded Successfully", 0)) {
                                    $.ajax({
                                        type: "POST",
                                        url: url + "/PropertyTransfer.aspx/UploadPropertyTransfer",
                                        data: " { transactionno : '" + issuanceno + "'} ",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (msg) {
                                            load_data_Pendingupload();
                                            $('#ALPendingApproval').modal('hide');
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
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + issuanceno + "|PropertyTransfer",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyTransfer.aspx/UploadPropertyTransfer",
                                    data: " { transactionno : '" + issuanceno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_Pendingupload();
                                        $('#ALPendingApproval').modal('hide');
                                        swal("Successful", "Transaction Completed!", "success");
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



var SiteArrayPT = new Array();
function GetsitePT() {
    var context = window.location.pathname.substring(1);
    var ddls = $('#ddlFromSitePT');
    ddls.empty();

    ddls.append($('<option>', {
        value: '0',
        text: '--- Select Site/Department ---',
        rowindex: -1
    }));


    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetWarehousePT',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                SiteArrayPT = JSON.parse(xml.d);
                SiteArrayPT.forEach((site, index) => {
                    ddls.append($('<option>', {
                        value: site.WarehouseID,
                        text: site.WarehouseName,
                        rowindex: index
                    }));
                });
                GetLocationPT();
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


var SiteArrayToPT = new Array();
function GetsiteToPT() {
    var context = window.location.pathname.substring(1);
    var ddls = $('#ddlToSitePT');
    ddls.empty();
    ddls.append($('<option>', {
        value: '0',
        text: '--- Select Site/Department ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetWarehouseToPT',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                SiteArrayToPT = JSON.parse(xml.d);
                SiteArrayToPT.forEach((site, index) => {
                    ddls.append($('<option>', {
                        value: site.WarehouseID,
                        text: site.WarehouseName,
                        rowindex: index
                    }));
                });
                GetLocationToPT();
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



var loclist = new Array();
function GetLocationPT() {
    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlFromLocationPT');
    ddlW.empty();
    //ddlW.append($('<option>', {
    //    value: '0',
    //    text: '--- Select Branch ---',
    //    rowindex: -1
    //}));
    ddlW.append($('<option>', {
        value: '0',
        text: '--- Select Location ---',
        rowindex: -1
    }));
    ddlW.append($('<option>', {
        value: '1',
        text: 'Receiving Location',
        rowindex: 0
    }));

    var wareH = $('#ddlFromSitePT').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetLocationListPT',
        data: "{ warehouse : '" + wareH + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                loclist = JSON.parse(xml.d);
                loclist.forEach((position, index) => {
                    ddlW.append($('<option>', {
                        value: position.LocationID,
                        text: position.Location,
                        rowindex: index
                    }));
                });
                GetOfficePT();
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



var officelist = new Array();
function GetOfficePT() {
    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlFromOfficePT');
    ddlW.empty();
    ddlW.append($('<option>', {
        value: '0',
        text: '--- Select Group ---',
        rowindex: -1
    }));

    var wareH = $('#ddlFromLocationPT').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetOfficeListsPT',
        data: "{ location : '" + wareH + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                officelist = JSON.parse(xml.d);
                officelist.forEach((position, index) => {
                    ddlW.append($('<option>', {
                        value: position.AreaID,
                        text: position.Area,
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

    load_data();
}







var loctolist = new Array();
function GetLocationToPT() {
    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlToLocationPT');
    ddlW.empty();
    ddlW.append($('<option>', {
        value: '0',
        text: '--- Select Location ---',
        rowindex: -1
    }));
    ddlW.append($('<option>', {
        value: '1',
        text: 'Receiving Location',
        rowindex: 0
    }));

    var wareH = $('#ddlToSitePT').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetLocationListToPT',
        data: "{ warehouse : '" + wareH + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                loctolist = JSON.parse(xml.d);
                loctolist.forEach((position, index) => {
                    ddlW.append($('<option>', {
                        value: position.LocationID,
                        text: position.Location,
                        rowindex: index
                    }));
                });
                GetOfficeToPT();
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



var officetolist = new Array();
function GetOfficeToPT() {
    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlToOfficePT');
    ddlW.empty();
    ddlW.append($('<option>', {
        value: '0',
        text: '--- Select Group ---',
        rowindex: -1
    }));

    var wareH = $('#ddlToLocationPT').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetOfficeListsToPT',
        data: "{ location : '" + wareH + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                officetolist = JSON.parse(xml.d);
                officetolist.forEach((position, index) => {
                    ddlW.append($('<option>', {
                        value: position.AreaID,
                        text: position.Area,
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


function removeSelectedPerson() {
    //var TrasferType = $('#BodyHolder_ddlTrasferType').val();
    //alert(TrasferType);
    GetEmployeeToperson();
    GetCount();
    var value = $('#ddlFromPersonPT').val();

    if (value != "0") {
        $("#ddlTopersonPT option[value='" + value + "']").each(function () {
            $(this).remove();
        });
    }


}
