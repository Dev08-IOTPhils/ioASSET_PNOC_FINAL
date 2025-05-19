var url = '';

$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    IfNeedsApproval();
    //$('#disposalbtn').off('click').on('click', function () {
    //    $('#btndownloadpdf').attr('transtype', 'Disposal From');
    //    $('#sigandCertModal').modal('show');
    //    ClearSignature();
    //    $('#lbl_from').html('Transacted by');
    //    $('#lbl_by').html('Approved by');
    //});
    //$('#wastematbtn').off('click').on('click', function () {
    //    $('#btndownloadpdf').attr('transtype', 'Waste Material');
    //    $('#sigandCertModal').modal('show');
    //    ClearSignature();
    //    $('#lbl_from').html('Certified Correct');
    //    $('#lbl_by').html('Disposal Approved');
    //});

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


        if (transtype == "Disposal From") {
            $.ajax({
                type: "POST",
                url: url + "/StockDisposal.aspx/CreatePDFDisposal",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',fromdatestamp :'" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Stock_Disposal.pdf");
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
        else if (transtype == "Waste Material") {
            $.ajax({
                type: "POST",
                url: url + "/StockDisposal.aspx/CreatePDFWasteMaterial",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',fromdatestamp :'" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Stock_Disposal.pdf");
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

    $("#tblStockList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
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

    GetUserCcustodian();
    $('#ddlCustodian').on('change', function () {
        load_data();
    });
    load_data_Pendingupload();
    $('#BodyHolder_btnComplete').fadeIn();
    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_btnUpload').fadeIn();
            $('#BodyHolder_fuDocuments').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_btnUpload').hide();
            $('#BodyHolder_fuDocuments').hide();
        }
    });
});

var needsapproval = "False";
function IfNeedsApproval() {
    $.ajax({
        type: "POST",
        url: url + "/StockDisposal.aspx/IfNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var y = document.getElementById("labelApprover");
            var z = document.getElementById("dropdownApprover");

            var a = document.getElementById("withorwithoutyou");
            var b = document.getElementById("withorwithoutyou1");
            if (msg.d == "True") {
                y.style.display = 'inline';
                z.style.display = 'inline';
                a.style.display = 'inline';
                b.style.display = 'none';
            }
            else {
                y.style.display = 'none';
                z.style.display = 'none';
                a.style.display = 'none';
                b.style.display = 'inline';
            }
        }
    });
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
        url: url + "/StockDisposal.aspx/GetUserCustodian",
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
        return false;
    }
}

function EnterKey1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}
var SMLID = '';
function load_data() {
    var search = "";
    var custodian = $('#ddlCustodian').val().trim();
    $.ajax({
        type: 'POST',
        url: url + "/StockDisposal.aspx/GetStockList",
        data: "{ search: '" + search + "', custodian : '" + custodian + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stocklist').hide();
            $('#stocklist').innerHTML = "";
            $('#stocklist').fadeIn(200);
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
        $('#tblStockList').DataTable({
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
                { "data": "ItemCode" },
                { "data": "QuantityString" },
                { "data": "Description" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Location" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
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

        $('#tblStockList').off('click');

        $('#tblStockList').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var itemcode = currentRow.find("td:eq(0)").text();
            var balancequantity = currentRow.find("td:eq(1)").text();

            var smlid = $(this).attr('smlid');
            SMLID = smlid;
            BalanceQuantity = balancequantity;
            $('#txtItemCode').val(itemcode);
            $('#addToDisposal').modal('show');
        });
    } catch (e) {
        var table = $('#tblStockList').DataTable();
        table
            .clear()
            .draw();
    }
}

function DisposeStockAsset() {
    var disposequantity = $('#txtDisposeQuantity').val();
    var custodian = $('#ddlCustodian').val();
    if (parseInt(disposequantity) > parseInt(BalanceQuantity)) {
        swal("Warning", "Dispose Quantity should not be greater than the balance quantity.", "error");
    }
    else if (parseInt(disposequantity) == 0 || disposequantity == "") {
        swal("Warning", "Input Dispose Quantity.", "error");
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/StockDisposal.aspx/StockTransferToDisposal",
            data: "{smlid: '" + SMLID + "', quantity: '" + disposequantity + "', custodian : '" + custodian + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "1") {
                    $('#addToDisposal').modal('hide');
                    GetCount();
                    BalanceQuantity = 0;
                    swal("Success", "Supply Added to disposal list", "success");
                }
                else {
                    $('#addToDisposal').modal('hide');
                    GetCount();
                    BalanceQuantity = 0;
                    swal("Warning", "Dispose Quantity should not be greater than the balance quantity.", "error");
                }
            }
        });
    }
}

GetCounter = 0;
function GetCount() {
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: "POST",
        url: url + "/StockDisposal.aspx/StockDisposalGetCount",
        data: "{  custodian : '" + custodian + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Supply List for Disposal: " + msg.d);
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

function DisposalList() {
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val('--- Select Approver ---').trigger('change');
    $('#BodyHolder_ddlModeOfDisposal').val('--- Select Mode of Disposal ---').trigger('change');
    load_data_disposallist();
    $('#DisposalList').modal('show');
}

function load_data_disposallist() {
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/StockDisposal.aspx/GetStockDisposalList",
        data: "{custodian: '" + custodian + "' }",
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
            GetCount();
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "QuantityString" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
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
            var itemcode = currentRow.find("td:eq(0)").text();
            var custodian = $('#ddlCustodian').val();
            var smlid = $(this).attr('smlid');
            $.ajax({
                type: "POST",
                url: url + "/StockDisposal.aspx/StockRemoveToDisposal",
                data: "{smlid: '" + smlid + "', custodian: '" + custodian + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    currentRow.remove();
                    GetCount();
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

function RemoveAllToDisposal() {
    var custodian = $('#ddlCustodian').val();
    swal({
        title: "Are you sure",
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
                url: url + "/StockDisposal.aspx/StockRemoveAllToDisposal",
                data: "{ custodian: '" + custodian + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_disposallist();
                    $('#DisposalList').modal('hide');
                }
            });
        } else {
            swal("Canceled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmStockDisposal1() {
    var custodian = $('#ddlCustodian').val();
    var alertMsg;
    if (needsapproval == "True") {
        if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---' || $('#BodyHolder_txtRemarks').val() == "") {
            alertMsg = '';
            if ($('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---')
                alertMsg += "\nMode of Disposal";
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
                alertMsg += "\nApprover";
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
                    $(".se-pre-con").fadeIn();
                    $.ajax({
                        type: "POST",
                        url: url + "/StockDisposal.aspx/ConfirmStockDisposal1",
                        data: "{ custodian : '" + custodian + "',approver: '" + $('#BodyHolder_ddlApprover').val() + "',modeofdisposal: '" + $('#BodyHolder_ddlModeOfDisposal').val() + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                swal("Successful", "Successfully Confirmed for Disposal and Pending For Upload.\n\nMail not sent.", "success");
                                load_data();
                                load_data_Pendingupload();
                                load_data_PUL1
                                GetCount();
                                $('#DisposalList').modal('hide');
                            }
                            else {
                                swal("Successful", "Successfully Confirmed for Disposal and Pending For Approval.", "success");
                                load_data();
                                load_data_Pendingupload();
                                load_data_PUL1
                                GetCount();
                                $('#DisposalList').modal('hide');
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
    else {
        if ($('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---' || $('#BodyHolder_txtRemarks').val() == "") {
            alertMsg = '';
            if ($('#BodyHolder_ddlModeOfDisposal').val() == "--- Select Mode of Disposal ---")
                alertMsg += "\nMode of Dispoasl ";
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
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
                    $(".se-pre-con").fadeIn();
                    $.ajax({
                        type: "POST",
                        url: url + "/StockDisposal.aspx/ConfirmStockDisposal1",
                        data: "{ custodian : '" + custodian + "',approver: '0', modeofdisposal: '" + $('#BodyHolder_ddlModeOfDisposal').val() + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            //if (msg.d == "sorry") {
                            //    swal("Successful", "Successfully Confirmed for Disposal and Pending For Upload. Check you mail settings at Miscellaneous.", "success");
                            //    load_data();
                            //    //load_data_Pendingupload();
                            //    $("#btnCart span").text(" 0");
                            //    $('#DisposalList').modal('hide');
                            //}
                            //else {
                            swal("Successful", "Successfully Confirmed for Disposal and Pending For Upload.", "success");
                            load_data();
                            //load_data_Pendingupload();
                            GetCount();
                            $('#DisposalList').modal('hide');
                            //}
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
    var search = "";
    $.ajax({
        type: 'POST',
        url: url + "/StockDisposal.aspx/GetPendingUploadList",
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
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
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
                        url: url + "/StockDisposal.aspx/CancelStockDisposal",
                        data: "{disposalno: '" + disposalnumber + "', remarks: '" + textremarks + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#pendinguploadlist').hide();
                            $('#pendinguploadlist').innerHTML = "";
                            $('#pendinguploadlist').fadeIn(200);
                            load_data();
                            currentRow.remove();
                            //alert('Stock Disposal Transaction successfully cancelled.');
                            swal("Successful", "Supply Disposal Transaction successfully cancelled.", "success");
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

var disposalnumber = '';
function ViewDisposalDetails(disposalno, transactorname, createddate, status) {

    //gheghe(Certificate/Word)
    //document.getElementById("printGrid").children[2].style.display = "none";
    //document.getElementById("printGrid").children[3].style.display = "none";

    disposalnumber = disposalno;
    document.getElementById('lblDisposalNo').innerHTML = disposalno;
    document.getElementById('lblDisposedBy').innerHTML = transactorname
    document.getElementById('lblDisposedDate').innerHTML = createddate;

    $('#btndownloadcreatepdf').attr('transno', disposalno);
    $('#btndownloadcreatepdf').attr('transactorname', transactorname);
    $('#btndownloadcreatepdf').attr('createddate', createddate);
    $('#btndownloadcreatepdf').attr('status', status);

    $('#btndownloadpdf').attr('transno', disposalno);
    $('#btndownloadpdf').attr('transactorname', transactorname);
    $('#btndownloadpdf').attr('createddate', createddate);
    $('#btndownloadpdf').attr('status', status);

    $('#BodyHolder_txtDisposalNumber').val(disposalno);

    var x = document.getElementById("showorhide");
    x.style.display = "none";
    if (status == "Pending Upload") {
        load_data_PUL1();
        $('#StockDetailsPendingUpload').modal('show');
        x.style.display = "inline";
    }
    else {
        load_data_PUL1();
        $('#StockDetailsPendingUpload').modal('show');
        x.style.display = "none";
    }
    $.ajax({
        type: "POST",
        url: url + "/StockDisposal.aspx/GenerateStockDisposalReport",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockDisposalReport').hide();
            $('#BodyHolder_divStockDisposalReport').html(msg.d);
            $('#BodyHolder_divStockDisposalReport').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/StockDisposal.aspx/GenerateStockDisposalReportWMR",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockDisposalWMR').hide();
            $('#BodyHolder_divStockDisposalWMR').html(msg.d);
            $('#BodyHolder_divStockDisposalWMR').fadeIn(200);
        }
    });
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/StockDisposal.aspx/GetSDLPendingUpload",
        data: "{disposalno: '" + disposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#sddetails').hide();
            $('#sddetails').innerHTML = "";
            $('#sddetails').fadeIn(200);

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
        $('#tblSDDetails').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Disposal'
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

        $('#tblSDDetails').off('click');
    } catch (e) {
        var table = $('#tblSDDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function UploadStockDisposal() {
    var alertMsg;
    var disposalno = $('#BodyHolder_txtDisposalNumber').val();
    var filess = $('#BodyHolder_fuDocuments').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    //if (filess == "") {
    //    swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
    //}
    //else {
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
                    var fileUpload = $("#BodyHolder_fuDocuments").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + disposalno + "|StockDisposal",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/StockDisposal.aspx/UploadStockDisposal",
                                    data: " { disposalno : '" + disposalno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        if (msg.d == "Success") {
                                            load_data_Pendingupload();
                                            $('#StockDetailsPendingUpload').modal('hide');
                                            swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                        }
                                        else {
                                            swal("Error", "Stock Disposal Transaction cannot be completed.", "error");
                                        }
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
  //  }
}