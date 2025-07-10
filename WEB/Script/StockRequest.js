var url = "";

$(document).ready(function () {
    //alert('request');
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    IfIssuanceNeedsApproval();
    GetApprover();
    $("#divInsuficientStock").hide();
    $("#tblStockList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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

    load_data();
    load_data_Pendingupload();

    $("#tblStockList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();

    //$('#risbtn').off('click').on('click', function () {
    //    $('#RISsigandCertModal').modal('show');
    //});

    $('#risxclR').off('click').on('click', function () {
        const now = new Date();
        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');
        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;
        var context = window.location.pathname.substring(1);
        //alert(disposalnumber);
        $.ajax({
            async: false,
            type: 'POST',
            url: context + '/ReportPage',
            data: "{ key1: '" + 'RIS' + "', key2: '" + requestno + "', key3: '" + 'SR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert(msg.d);
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
                    a.attr("download", "WithdrawalSlip_" + formattedDateTime + ".xlsx");
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

    $('#approvedsignatureupload').off('change').on('change', function () {
        approvedreadURL(this);
    });

    $('#issuedsignatureupload').off('change').on('change', function () {
        issuedreadURL(this);
    });
    $('#requestedsignatureupload').off('change').on('change', function () {
        requestedreadURL(this);
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
            url: url + "/StockRequest.aspx/CreateRISRequest",
            data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "', approveddatestamp: '" + approveddatestamp.prop('checked') + "',issueddatestamp: '" + issueddatestamp.prop('checked') + "' }",
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
                        a.attr("download", "Stock_Request.pdf");
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
});

var SMLID = '';
var BalanceQuantity = 0;

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
function SweetAlertSuccess(e) { swal({ icon: "success", text: e }) } function SweetAlertWarning(e) { swal({ icon: "warning", text: e }) } function SweetAlertInfo(e) { swal({ icon: "info", text: e }) } function SweetAlertError(e) { swal({ icon: "error", text: e }) }
var issuedcertbase64 = "";
var approvedcertbase64 = "";
var requestedcertbase64 = "";
var issuedimagebase64 = "";
var approvedimagebase64 = "";
var requestedimagebase64 = "";
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
function IfIssuanceNeedsApproval() {
    $.ajax({
        type: "POST",
        url: url + "/StockRequest.aspx/IfIssuanceNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var y = document.getElementById("labelApprover");
            var z = document.getElementById("dropdownApprover");
            if (msg.d == "True") {
                y.style.display = 'inline';
                z.style.display = 'inline';
            }
            else {
                y.style.display = 'none';
                z.style.display = 'none';
            }
        }
    });
}

function GetApprover() {
    $.ajax({
        type: "POST",
        url: url + "/StockRequest.aspx/GetApprover",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlApprover = $("[id*=ddlApprover]");
            ddlApprover.empty().append("<option selected='selected' value='0'>--- Select Approver ---</option>");
            $.each(msg.d, function () {
                ddlApprover.append($("<option></option>").val(this['Value']).html(this['Text']));
                GetCount();
            });
        },
        error: function (msg1) {
            var ddlApprover = $("[id*=ddlApprover]");
            ddlApprover.empty().append("<option selected='selected' value='0'>--- Select Approver ---</option>");
            ddlApprover.append($("<option></option>").val(this['Value']).html(this['Text']));
        }
    });
}

var GetCounter = 0;
function GetCount() {
    var custodianid = $('#BodyHolder_ddlCustodian').val();

    $.ajax({
        type: "POST",
        url: "My_Request/StockRequest.aspx/StockRequestGetCount",
        data: "{ custodian: '" + custodianid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Supply List for Request: " + msg.d);
            GetCounter = msg.d;
            if (GetCounter == 0) {
                document.getElementById("btnAssetItemTransfer").disabled = true;
            } else {
                document.getElementById("btnAssetItemTransfer").disabled = false;
            }
        }
    });
}
var QuantityCol = false;
function checkQuantityColumn() {
    $.ajax({
        type: 'POST',
        url: url + "/StockRequest.aspx/CheckStockRemainingQuantity",
        data: "{ }",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'True') {
                QuantityCol = true;
            }
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function load_data() {
    checkQuantityColumn();
    var custodianid = $('#BodyHolder_ddlCustodian').val();
    if (custodianid == '--- Select Custodian Type ---') {
        custodianid = 0;
    }
    var search = "";
    $.ajax({
        type: 'POST',
        url: url + "/StockRequest.aspx/GetList",
        data: "{ search: '" + search + "', custodian: '" + custodianid + "'}",
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
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}
var qty = '';
function populateDataTable(data) {
    try {

        if (QuantityCol) {
            $('#tblStockList').DataTable({
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
                    { "data": "Location" },
                    { "data": "Action" }
                ],

                "rowCallback": function (row, data, index) {
                    BalanceQuantity = data.QuantityString;
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
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                            },
                             {
                                 extend: 'csv',
                                 text: 'CSV',
                                 className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                             },
                              {
                                  extend: 'print',
                                  text: 'PRINT',
                                  className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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

                colReorder: true
            , stateSave: true
            });

            $('#tblStockList').off('click');

            $('#tblStockList').on('click', 'a.btn_Add', function (e) {
                e.preventDefault();

                var currentRow = $(this).closest("tr");
                var itemcode = currentRow.find("td:eq(0)").text();
                code = itemcode;
                var smlid = $(this).attr('smlid');

                SMLID = smlid;
                $('#txtItemCode').val(itemcode);
                $('#addToListStock').modal('show');
            });
        }
        else {
            $('#tblStockList').DataTable({
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
                    { "data": "Location" },
                    { "data": "Action" }


                ],

                "rowCallback": function (row, data, index) {
                    BalanceQuantity = data.QuantityString;
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
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                            },
                            {
                                extend: 'excel',
                                text: 'EXCEL',
                                className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                            },
                             {
                                 extend: 'csv',
                                 text: 'CSV',
                                 className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                             },
                              {
                                  extend: 'print',
                                  text: 'PRINT',
                                  className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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

                colReorder: true
            , stateSave: true
            });

            $('#tblStockList').off('click');

            $('#tblStockList').on('click', 'a.btn_Add', function (e) {
                e.preventDefault();
                var currentRow = $(this).closest("tr");
                var itemcode = currentRow.find("td:eq(0)").text();
                code = itemcode;

                ItemCode = itemcode;

                var smlid = $(this).attr('smlid');
                SMLID = smlid;

                $('#txtItemCode').val(itemcode);
                $('#addToListStock').modal('show');
            });
        }
    } catch (e) {
        var table = $('#tblStockList').DataTable();
        table
            .clear()
            .draw();
    }
}
var isSufficient = true;
function RequestStockAsset() {
    var requestquantity = $('#txtRequestQuantity').val();
    var custodianid = $('#BodyHolder_ddlCustodian').val();

    if (isSufficient == false) {
        swal("Warning", "Insufficient Supply", "warning");
    }
    else {
        if (requestquantity == "") {
            swal("Warning", "Input Request Quantity", "warning");
        }
        else if (parseInt(requestquantity) == 0) {
            swal("Warning", "Input Request Quantity", "warning");
        }
        else {
            $.ajax({
                type: "POST",
                url: url + "/StockRequest.aspx/StockTransferToRequest",
                data: "{smlid: '" + SMLID + "', custodian: '" + custodianid + "', quantity: '" + requestquantity + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#addToListStock').modal('hide');
                    GetCount();
                    Clear();
                    BalanceQuantity = 0;
                }
            });
        }
    }
}

//function RemoveAllToRequest() {
//    var custodianid = $('#BodyHolder_ddlCustodian').val();
//    swal({
//        title: "Cancel Transaction?",
//        text: "Are you sure you want to cancel this transaction?",
//        icon: "warning",
//        buttons: [
//          'No',
//          'Yes'
//        ],
//        dangerMode: true,
//    }).then(function (isConfirm) {
//        if (isConfirm) {
//            $.ajax({
//                type: "POST",
//                url: url + "/StockRequest.aspx/StockRemoveAllToRequest",
//                data: "{custodian: '" + custodianid + "'}",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (msg) {
//                    load_data();
//                    load_data_requestlist();
//                    GetCount();
//                    swal("Successful", "Supply Request cancelled successfully.", "success");
//                    $('#RequestList').modal('hide');
//                }
//            });
//        } else {
//            swal("Cancelled", "Cancelled by user", "warning");
//        }
//    });
//}

function ConfirmStockRequest() {
    $('.btn').prop('disabled', true);

    var custodianid = $('#BodyHolder_ddlCustodian').val();
    var approver = $('#BodyHolder_ddlApprover').val();
    var alertMsg = "";
    if (needsapproval == "False") {
        if ($('#BodyHolder_txtRemarks').val().trim() == "") {
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else {
            swal({
                title: "Confirm Transaction?",
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
                        url: url + "/StockRequest.aspx/ConfirmStockRequest",
                        data: "{ custodian : '" + custodianid + "', approver: '0', remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Issued and Pending For Upload.\n\nMail not sent. Please check you mail settings at Miscellaneous.');
                                swal("Successful", "Successfully Requested and Pending For Upload.\n\nMail not sent.", "success");
                                load_data();
                                Cleart();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#RequestList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Issued and Pending For Upload');
                                swal("Successful", "Successfully Requested and Pending For Upload.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount();
                                $('#RequestList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                    $('.btn').prop('disabled', false);
                }
            });
        }
    }
    else {
        if ($('#BodyHolder_txtRemarks').val().trim() == "" || $('#BodyHolder_ddlApprover').val() == "0") {
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlApprover').val() == "0")
                alertMsg += "\nApprover ";
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else {
            swal({
                title: "Confirm Transaction?",
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
                        url: url + "/StockRequest.aspx/ConfirmStockRequest",
                        data: "{ custodian : '" + custodianid + "', approver: '" + approver + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Issued and Pending For Upload.\n\nMail not sent. Please check you mail settings at Miscellaneous.');
                                swal("Successful", "Successfully Requested and Pending For Approval/Upload.\n\nMail not sent.", "success");
                                load_data();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#RequestList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Issued and Pending For Upload');
                                swal("Successful", "Successfully Requested and Pending For Approval/Upload.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount();
                                $('#RequestList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                    $('.btn').prop('disabled', false);
                }
            });
        }
    }
}

function RequestList() {
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val(0).trigger('change');
    load_data();
    load_data_requestlist();
    $('#RequestList').modal('show');

    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}

function load_data_requestlist() {
    var custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();

    // var custodianid = $('#BodyHolder_ddlCustodian').val();

    $.ajax({
        type: 'POST',
        url: url + "/StockRequest.aspx/GetStockRequestList",
        data: "{ custodian: '" + custodianid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#requestlist').hide();
            $('#requestlist').innerHTML = "";
            $('#requestlist').fadeIn(200);

            myJsonData = data;
            populateDataTableForRequest(myJsonData);

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

function populateDataTableForRequest(data) {
    try {
        $('#tblRequestList').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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

        $('#tblRequestList').off('click');

        $('#tblRequestList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var itemcode = currentRow.find("td:eq(0)").text();
            code = itemcode;
            var custodianid = $('#BodyHolder_ddlCustodian').val();
            var smlid = $(this).attr('smlid');
            $.ajax({
                type: "POST",
                url: url + "/StockRequest.aspx/StockRemoveToRequest",
                data: "{smlid: '" + smlid + "', custodian: '" + custodianid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    currentRow.remove();
                    GetCount();
                }
            });
        });
    } catch (e) {
        var table = $('#tblRequestList').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_Pendingupload() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: url + "/StockRequest.aspx/StockRequestPendingUpload",
        data: "{search : '" + search + "' }",
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
                { "data": "RequestNo" },
                { "data": "IssuedTo" },
                { "data": "Remarks" },
                //{ "data": "NoOfItem" },
                { "data": "TransactionDate" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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

var requestno = '';
function ViewStocks(stockrequestno) {
    document.getElementById('lblStockRequestNo').innerHTML = stockrequestno;
    $('#BodyHolder_txtStockRequestNo').val(stockrequestno);
    requestno = stockrequestno;

    load_data_PUL1();
    $('#btndownloadrispdf').attr('transno', stockrequestno);
    $('#btndownloadcreatepdf').attr('transno', stockrequestno);

    $('#StockDetailsPendingUpload').modal('show');

    $.ajax({
        type: "POST",
        url: url + "/StockRequest.aspx/GenerateStockRequestReport",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockRequestReport').hide();
            $('#BodyHolder_divStockRequestReport').html(msg.d);
            $('#BodyHolder_divStockRequestReport').fadeIn(200);
        },
        error: function (xml) {
            //window.location.replace('../Login.aspx');
        }
    });
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/StockRequest.aspx/StockDetailsPendingUpload",
        data: "{requestno: '" + requestno + "'}",
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
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Request'
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

        $('#tblPUL1').off('click');
    } catch (e) {
        var table = $('#tblPUL1').DataTable();
        table
            .clear()
            .draw();
    }
}

function CancelStockRequest(stockrequestno) {
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
    textareatt.id = "cancelrequest";

    swal({
        title: "Cancel Transaction?",
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

            var textremarks = $('#cancelrequest').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: url + "/StockRequest.aspx/CancelStockRequest",
                data: "{requestno: '" + stockrequestno + "', remarks: '" + textremarks + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    swal("Successful", "Supply Request cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

var code = "";
function checkQuantity() {
    var qty = $('#txtRequestQuantity').val();

    $.ajax({
        type: "POST",
        url: url + "/StockRequest.aspx/checkQuantity",
        data: "{itemCode: '" + code + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // alert(msg.d);
            if (parseInt(msg.d) < parseInt(qty)) {
                $("#divInsuficientStock").show();
                isSufficient = false;
            }
            else {
                $("#divInsuficientStock").hide();
                isSufficient = true;
            }
        }
    });
}

function Clear() 
{
    $('#txtRequestQuantity').val('');
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val('--- Select Custodian Type ---').trigger('change');
}