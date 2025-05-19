var idForUpdate = 0;
var currentPage = 1;

var rfid = '';
var barcode = '';
var propertynogeneration = '';
var rfidcount = 0;
var barcodecount = 0;
var t;
var GRID = 0;
var IARHeader = 0;

$(document).ready(function () {
    //GetAccess();
    GetRFIDandBarcodeGeneration();
    IfNeedsApproval();
    IfPropNumberManaualAuto();
    GetWarehouseByIDGR();

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

    $('#iarbtn').off('click').on('click', function () {
        $('#sigandCertModal').modal('show');
        ClearSignature();
    });


    $('#iarbtnxcl').off('click').on('click', function () {
        //Get GoodsRecieveID and put in key2
        //alert(GRID);
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
            data: "{ key1: '" + 'IAR' + "', key2: '" + GRID + "', key3: '" + IARHeader + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "IAR_" + formattedDateTime + ".xls");
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

        var alertMsg = "Fill-out all the required field(s):\n";
        var hasError = false;

        var frompkipassword = $('#frompkipassword');
        var bypkipassword = $('#bypkipassword');

        if (frompkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Inspection)";
        }

        if (bypkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Acceptance)";
        }

        if (fromimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Inspection)";
        }
        if (byimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Acceptance)";
        }

        if (fromcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Inspection)";
        }
        if (bycertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Acceptance)";
        }

        if (hasError) {
            SweetAlertWarning(alertMsg);
            return;
        }

        var fromdatestamp = $('#fromdatestamp');
        var bydatestamp = $('#bydatestamp');

        $.ajax({
            type: "POST",
            url: "Operation_Module/GoodsReceive.aspx/CreatePDFIAR",
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
                        a.attr("download", "GoodsReceive.pdf");
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

    $('.select2').select2();
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDOA').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    //$.extend($.daterangepicker, { _checkOffset: function (inst, offset, isFixed) { alert(offset); return offset } });
    $('#txtExpiryDate').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });

    $('#txtDOA').datepicker('setDate', today);

    $("#tblProperty").DataTable({
        "scrollX": true,
        "sScrollX": "100%",
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Brftip',
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
        "sorting": false,
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        "scrollY": true,
        "sScrollY": "100%",
        //fixedColumns: {
        //    leftColumns: 2
        //}
    });

    $("#tblPropertyAddedList").DataTable({
        "scrollX": true,
        "sScrollX": "100%",
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Brftip',
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
        "sorting": false,
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        "scrollY": true,
        "sScrollY": "100%",
        //fixedColumns: {
        //    leftColumns: 2
        //}
    });

    $("#tblGoodsReceive").DataTable({
        "scrollX": true,
        dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        'columnDefs': [{ 'targets': 0, 'orderable': false }],
        "sorting": false,
        buttons: {
            buttons: [
            {
                extend: 'collection',
                text: '...',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'COPY',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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

    $("#tblPropertyDetails").DataTable({
        "scrollX": true
    });

    load_data_propertyGR();
    load_data_GRH();

    // $('#BodyHolder_btncomplete').fadeIn();
    // $('#BodyHolder_GRupload').hide();
    // $('#BodyHolder_uploadAF').hide();

    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btncomplete').hide();
            $('#BodyHolder_GRupload').fadeIn();
            $('#BodyHolder_uploadAF').fadeIn();
        }
        else {
            $('#BodyHolder_btncomplete').fadeIn();
            $('#BodyHolder_GRupload').hide();
            $('#BodyHolder_uploadAF').hide();
        }
    });
    GetApproverByCustodian();
    GetUserByCustodian();
});

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function GetAccess() {
    var context = window.location.pathname.substring(1);
    $.ajax({
        type: "POST",
        url: context + "/CheckA",
        data: " { ModuleAccess : '" + "Supplier|Region|Branch" + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            if (msg1.d != "") {
                var ModAccess = [];
                ModAccess = msg1.d.split('|');
                if (ModAccess[0] == "0") {
                    document.getElementById("HideThisSupplier").style.display = "none";

                    var element = document.getElementById("hidesupplier");
                    element.classList.remove("input-group");
                }
                if (ModAccess[1] == "0") {
                    document.getElementById("HideThisRegion").style.display = "none";

                    var element = document.getElementById("hideregion");
                    element.classList.remove("input-group");
                }
                if (ModAccess[2] == "0") {
                    document.getElementById("HideThisBranch").style.display = "none";

                    var element = document.getElementById("hidebranch");
                    element.classList.remove("input-group");
                }
            } else {
                document.getElementById("HideThisSupplier").style.display = "none";
                var element = document.getElementById("hidesupplier");
                element.classList.remove("input-group");

                document.getElementById("HideThisRegion").style.display = "none";
                var element = document.getElementById("hideregion");
                element.classList.remove("input-group");

                document.getElementById("HideThisBranch").style.display = "none";
                var element = document.getElementById("hidebranch");
                element.classList.remove("input-group");
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
var needsapproval = "False";
function IfNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx//IfNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var x = document.getElementById("lblApprover");
            var y = document.getElementById("dropdownApprover");
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


var propnumbergenerate = '';
function IfPropNumberManaualAuto() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/IfPropNumberManualAuto",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //alert(msg.d);
            propnumbergenerate = msg.d;
            if (propnumbergenerate == 'Manual Input') {
                t = $("#tblPropertyAdded").DataTable({
                    "scrollX": true, "paging": false,
                    dom: 'Brftip',
                    fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    },
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": true,
                            "searchable": false
                        },
                        //{
                        //    "targets": [2],
                        //    "visible": false,
                        //    "searchable": false
                        //},
                         {
                             "targets": [6],
                             "visible": false,
                             "searchable": false
                         },
                    ],
                    "sorting": false,
                    buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
            }
            else {
                t = $("#tblPropertyAdded").DataTable({
                    "scrollX": true, "paging": false,
                    dom: 'Brftip',
                    fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    },
                    "columnDefs": [
                      {
                          "targets": [0],
                          "visible": false,
                          "searchable": false
                      },
                      //{
                      //    "targets": [2],
                      //    "visible": false,
                      //    "searchable": false
                      //},
                       {
                           "targets": [6],
                           "visible": false,
                           "searchable": false
                       },
                    ],
                    "sorting": false,
                    buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
            }
        }
    });
}

function GetApproverByCustodian() {
    custodianid = $('#BodyHolder_ddlCustodian').val();
    var ddlReceivedBy = $("#ddlReceivedBy").val();
    var ddlApprover = $("[id*=ddlApprover]");
    if ($('#BodyHolder_ddlCustodian').val() == "--- Select Custodian Type ---") {
        ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
    }
    else {
        $.ajax({
            type: "POST",
            url: "Operation_Module/GoodsReceive.aspx/GetApproverByCustodian",
            data: " { receivedby : '" + ddlReceivedBy + "'} ",
            //data : "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
                //ddlCustodian.append('<option value="0">--- Select Approver ---</option>');
                $.each(msg.d, function () {
                    ddlApprover.append($("<option></option>").val(this['Text']).html(this['Value']));
                });
            },
            error: function (msg1) {
                ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
            }
        });
    }
}

function GetUserByCustodian() {
    custodianid = $('#BodyHolder_ddlCustodian').val();
    var ddlReceivedBy = $("[id*=ddlReceivedBy]");
    if ($('#BodyHolder_ddlCustodian').val() == "--- Select Custodian Type ---") {
        ddlReceivedBy.empty().append('<option selected="selected" value="0">--- Select Receiver ---</option>');
    }
    else {
        $.ajax({
            type: "POST",
            url: "Operation_Module/GoodsReceive.aspx/GetUserByCustodian",
            data: " { custodianid : '" + custodianid + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ddlReceivedBy.empty().append('<option selected="selected" value="0">--- Select Receiver ---</option>');
                //ddlCustodian.append('<option value="0">--- Select Approver ---</option>');
                $.each(msg.d, function () {
                    ddlReceivedBy.append($("<option></option>").val(this['Text']).html(this['Value']));
                });
            },
            error: function (msg1) {
                ddlReceivedBy.empty().append('<option selected="selected" value="0">--- Select Receiver ---</option>');
            }
        });
    }
}

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

$(document).on('keyup', '.txtSerial', function () {
    var $Item = $(this);
    var rowindex = $(this).attr('row');
    var idForUpdate = $(this).attr('id');

    CheckDuplicateSerial(idForUpdate);
});

function CheckDuplicateSerial(id) {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/CheckDuplicateSerial",
        data: " { serialno : '" + $('#' + id).val() + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "duplicate") {
                var element = document.getElementById(id);
                element.classList.add("hasError");
                //alert('has duplicate');
            } else {
                //alert('all goods');
                var element = document.getElementById(id);
                element.classList.remove("hasError");
            }
        }
    });
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

var storedserial = [];
function inputsHaveDuplicateValues() {
    var inputserial = $('.txtSerial');
    $.each(inputserial, function (k, v) {
        var getVal = $(v).val();
        if (storedserial.indexOf(getVal) != -1) {
            //$(v).fadeOut();
            var element = document.getElementById(v.id);
            element.classList.add("hasError");
            console.log(k);
        }
        else {
            storedserial.push($(v).val());
            CheckDuplicateSerial(v.id);
        }
        //alert($(v).val());
    });
}

$(document).on('keyup', '.txtPropertyNumber', function () {
    var $Item = $(this);
    var rowindex = $(this).attr('row');
    var idForUpdate = $(this).attr('id');

    CheckDuplicatePropertyNo(idForUpdate);
});

function CheckDuplicatePropertyNo(id) {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/CheckDuplicatePropertyNo",
        data: " { propertyno : '" + $('#' + id).val() + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "duplicate") {
                var element = document.getElementById(id);
                element.classList.add("hasError");
                //alert('has duplicate');
            } else {
                //alert('all goods');
                var element = document.getElementById(id);
                element.classList.remove("hasError");
            }
        }
    });
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

var storedpropertyno = [];
function inputsHaveDuplicateValuesPropNo() {
    var inputpropertyno = $('.txtPropertyNumber');
    $.each(inputpropertyno, function (k, v) {
        var getVal = $(v).val();
        if (storedpropertyno.indexOf(getVal) != -1) {
            //$(v).fadeOut();
            var element = document.getElementById(v.id);
            element.classList.add("hasError");
            console.log(k);
        }
        else {
            storedpropertyno.push($(v).val());
            CheckDuplicatePropertyNo(v.id);
        }
        //alert($(v).val());
    });
}

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_propertyGR();
        return false;
    }
}

function EnterKeySearch2(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_GRH();
        return false;
    }
}

function GetRFIDandBarcodeGeneration() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/GetRFIDandBarcodeGeneration",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            rfid = msg.d.split('|')[0];
            barcode = msg.d.split('|')[1];
            rfidcount = msg.d.split('|')[2];
            barcodecount = msg.d.split('|')[3];
            //alert(rfid);
        }
    });
}

function ValidateDate(dateString) {
    //Get the date from the TextBox.
    var dtCurrent = new Date();
    var parts = dateString.split("/");
    var dt = new Date(parts[0] + "/" + parts[1] + "/" + parts[2]);
    if (dt > dtCurrent)
        return false;
    return true;
}

////////////////////////////////PROPERTY GOODS RECEIVE
//////////////////////////////////////////////////////
function load_data_propertyGR() {
    var search = "";
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Operation_Module/GoodsReceive.aspx/GetProperty",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#property').hide();
            $('#property').innerHTML = "";
            $('#property').fadeIn(200);
            myJsonData = data;
            populateDataTablePropertyGR(myJsonData);
            console.log(data);
            console.log('Success');
            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePropertyGR(data) {
    try {
        $('#tblProperty').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            'language': {
                //'searchPlaceholder': "Search",
                //"search": '<i class="fa fa-search"></i>',
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "scrollY": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                //{ "data": "IsActive" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
            //fixedColumns: {
            //    leftColumns: 1
            //},
            //fixedHeader: true
        });
        $('#tblProperty').off('click');
    } catch (e) {
        var table = $('#tblProperty').DataTable();
        table
            .clear()
            .draw();
    }
}

var ttid = 0;
function AddToGoodsReceiveProperty(id, itemcode, tagtypeid, Description) {
    ttid = tagtypeid;
    var alertMsg;
    document.getElementById('lblitemcode').innerHTML = itemcode + ' - ' + Description;
    $("#txtPropertyQuantity").val("");
    $("#txtAcquiredCost").val("");
    QuantityKeyUp();
    if (needsapproval == "True") {
        if ($('#ddlFromSite').val() == '0' || $('#ddlLocation1').val() == '0' || $('#ddlSupplier').val() == '0' || $('#txtDOA').val().trim() == '' || $('#txtPRNumber').val().trim() == '' ||
            $('#ddlReceivedBy').val() == '0' || $('#txtPONumber').val().trim() == '' || $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ||
            $('#ddlApprover').val() == '0' || $('#BodyHolder_txtRemarks').val().trim() == '' || $('#BodyHolder_ddlReceivedBy').val() == "0" || $('#ddlApprover').val() == "0") {
            alertMsg = '';

            if ($('#ddlFromSite').val() == '0')
                alertMsg += '\nSite';
            if ($('#ddlLocation1').val() == '0')
                alertMsg += '\nLocation';
            //if ($('#BodyHolder_ddlEmployee').val() == 'NA')
            //    alertMsg += '\nAssignedPerson';
            if ($('#ddlSupplier').val() == '0')
                alertMsg += '\nSupplier';
            if ($('#txtDOA').val().trim() == '')
                alertMsg += '\nDate of Acquisition';
            if ($('#txtPRNumber').val().trim() == '')
                alertMsg += '\nPR Number';
            if ($('#ddlReceivedBy').val() == "0")
                alertMsg += '\nReceived By';
            if ($('#txtPONumber').val().trim() == '')
                alertMsg += '\nPO Number';
            if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                alertMsg += '\nCustodian Type';
            if ($('#ddlApprover').val() == '0' || $('#ddlApprover').val() == "0")
                alertMsg += '\nApprover';
            if ($('#BodyHolder_txtRemarks').val().trim() == '')
                alertMsg += '\nRemarks';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        } else if (ValidateDate(document.getElementById('txtDOA').value) == false) {
            //alert('Given date should not be greater than today`s date.');
            swal("Invalid Date", "Given date should not be greater than today`s date.", "error");
        }
        else {
            idForUpdate = id;
            GetRFIDandBarcodeGeneration();
            $('#txtPropertyItemCode').val(itemcode);
            $("#addToPropertyGoodsReceive").modal("show");
        }
        //var t = $("#tblPropertyAdded").DataTable();

        //t.clear().draw(false);
    }
    else {
        if ($('#ddlFromSite').val() == '0' || $('#ddlLocation1').val() == '0' || $('#ddlSupplier').val() == '0' || $('#txtDOA').val().trim() == '' || $('#txtPRNumber').val().trim() == '' ||
           $('#ddlReceivedBy').val() == '0' || $('#txtPONumber').val().trim() == '' || $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ||
            $('#BodyHolder_txtRemarks').val().trim() == '' || $('#ddlReceivedBy').val() == "0") {
            alertMsg = '';
            // alert($('#ddlFromSite').val() + "|" +$('#ddlLocation1').val() + "|" +$('#ddlSupplier').val() + "|" +$('#txtDOA').val() + "|" +$('#txtPRNumber').val() + "|" +$('#ddlReceivedBy').val() + "|" +$('#txtPONumber').val() + "|" +$('#BodyHolder_ddlCustodian').val() + "|" +$('#ddlReceivedBy').val() + "|" +$('#ddlReceivedBy').val() + "|" + );
            if ($('#ddlFromSite').val() == '0')
                alertMsg += '\nSite';
            if ($('#ddlLocation1').val() == '0')
                alertMsg += '\nLocation';
            //if ($('#BodyHolder_ddlEmployee').val() == 'NA')
            //    alertMsg += '\nAssignedPerson';
            if ($('#ddlSupplier').val() == '0')
                alertMsg += '\nSupplier';
            if ($('#txtDOA').val().trim() == '')
                alertMsg += '\nDate of Acquisition';
            if ($('#txtPRNumber').val().trim() == '')
                alertMsg += '\nPR Number';
            if ($('#ddlReceivedBy').val() == '0')
                alertMsg += '\nReceived By';
            if ($('#txtPONumber').val().trim() == '')
                alertMsg += '\nPO Number';
            if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                alertMsg += '\nCustodian Type';
            if ($('#BodyHolder_txtRemarks').val().trim() == '')
                alertMsg += '\nRemarks';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
        } else if (ValidateDate(document.getElementById('txtDOA').value) == false) {
            //alert('Given date should not be greater than today`s date.');
            swal("Invalid Date", "Given date should not be greater than today`s date.", "error");
        }
        else {
            idForUpdate = id;
            GetRFIDandBarcodeGeneration();
            $('#txtPropertyItemCode').val(itemcode);
            $("#addToPropertyGoodsReceive").modal("show");
        }

        //t.clear().draw(false);
    }
    //$("#addToPropertyGoodsReceive").modal("show");
}

function AddToPropertyGoodsReceive() {

    storedserial = [];
    //inputsHaveDuplicateValues();
    storedpropertyno = [];
    inputsHaveDuplicateValuesPropNo();
    var alertMsg;

    var txtPropertyNumber = document.getElementsByClassName('txtPropertyNumber');
    var txtSerial = document.getElementsByClassName('txtSerial');
    var txtrfid = document.getElementsByClassName('txtrfid');
    var txtbarcode = document.getElementsByClassName('txtbarcode');
    var propertynoctr = 0;
    var serialctr = 0;
    var rfidctr = 0;
    var barcodectr = 0;

    for (var i in txtPropertyNumber) {
        if (txtPropertyNumber[i].value == '')
            propertynoctr++;
    }

    for (var i in txtSerial) {
        if (txtSerial[i].value == '')
            serialctr++;
    }

    if (rfid == "Auto generated") {
    }
    else {
        for (var i in txtrfid) {
            if (txtrfid[i].value == '')
                rfidctr++;
        }
    }

    if (barcode == "Auto generated") {
    }
    else {
        for (var i in txtbarcode) {
            if (txtbarcode[i].value == '')
                barcodectr++;
        }
    }
    if (needsapproval == "True") {
        swal({
            title: "Add Property?",
            text: "Are you sure you want to add this property to list?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var supplier = $('#ddlSupplier').val();
                var dateofacquisition = $('#txtDOA').val();
                var prnumber = $('#txtPRNumber').val();
                var receivedby = $('#ddlReceivedBy').val();
                var ponumber = $('#txtPONumber').val();
                var custodian = $('#BodyHolder_ddlCustodian').val();
                var invoicenumber = $('#txtInvoiceNumber').val();
                var approver = $('#ddlApprover').val();
                var drnumber = $('#txtDRNumber').val();
                var remarks = $('#BodyHolder_txtRemarks').val();

                var site = $('#ddlFromSite').val();
                var assignedperson = $('#ddlEmployee').val();
                //if (assignedperson == 'NA') {
                //    assignedperson = 0;
                //}
                var location = $('#ddlLocation1').val();

                if ($('#txtDRNumber').val() == '')
                    drnumber = 'N/A';
                if ($('#txtInvoiceNumber').val() == '')
                    invoicenumber = 'N/A';

                var quantity = parseInt($("#txtPropertyQuantity").val());
                var acquiredcost = $("#txtAcquiredCost").val();
                acquiredcost = acquiredcost.replaceAll(",", "");
                var saleprice = $("#txtSalePrice").val();

                if ($('#txtPropertyQuantity').val().trim() == '' || serialctr != 0 || rfidctr != 0 || barcodectr != 0 || propertynoctr != 0 ||
                    $('#txtPropertyQuantity').val().trim() == '0' ||
                    $('#txtAcquiredCost').val().trim() == '' ||
                    //$('#txtSalePrice').val().trim() == '' ||
                    $(".txtSerial").hasClass("hasError") ||
                    $(".txtPropertyNumber").hasClass("hasError")) {
                    alertMsg = '';

                    if ($('#txtPropertyQuantity').val().trim() == '' || $('#txtPropertyQuantity').val().trim() == '0')
                        alertMsg += '\nQuantity';
                    if ($('#txtAcquiredCost').val().trim() == '')
                        alertMsg += '\nUnit Cost';
                    //if ($('#txtSalePrice').val().trim() == '')
                    //    alertMsg += '\nSale Price';
                    if (propertynoctr != 0)
                        alertMsg += '\nProperty #';
                    if (serialctr != 0)
                        alertMsg += '\nSerial #';
                    if (rfidctr != 0)
                        alertMsg += '\nRFID #';
                    if (barcodectr != 0)
                        alertMsg += '\nBarcode #';
                    if ($(".txtSerial").hasClass("hasError"))
                        alertMsg += '\nDuplicate Serial';
                    if ($(".txtPropertyNumber").hasClass("hasError"))
                        alertMsg += '\nDuplicate Property #';
                    swal("Fill-out all the required field(s): ", alertMsg, "warning");
                }

                else {
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/GoodsReceive.aspx/AddToGroupPropertyList",
                        data: " { propertyid: '" + idForUpdate + "',supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                            + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                            + remarks + "', quantity : '" + quantity + "',acquiredcost : '" + acquiredcost + "',saleprice : '" + saleprice + "',site : '" + site + "',assignedperson : '" + assignedperson + "',location : '" + location + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log("successfully added to list (" + msg.d + ")");
                            /////////initialise datatable
                            //var table = $('#tblPropertyAdded').DataTable();
                            //////// Create a hidden element
                            for (var i = 0; i < t.rows().data().length; i++) {
                                t.rows().data()[i].PropertyNumber = $("#txtPropertyNumber" + i).val();
                                t.rows().data()[i].Serial = $("#txtSerial" + i).val();
                                t.rows().data()[i].Description = $("#txtDescription" + i).val();
                                t.rows().data()[i].WarrantyDate = $("#txtWarrantyDate" + i).val();
                                t.rows().data()[i].InsuranceDate = $("#txtInsurance" + i).val();
                                t.rows().data()[i].ExpiryDate = $("#txtExpiryDate" + i).val();
                                t.rows().data()[i].LabelToPrint = $("#txtLabelToPrint" + i).val();
                                t.rows().data()[i].RFIDNumber = $("#txtrfid" + i).val();
                                t.rows().data()[i].Barcode = $("#txtbarcode" + i).val();
                                ////////////pass to code behind for saving process
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/GoodsReceive.aspx/AddToPropertyList",
                                    data: " { grpropertyid : '" + msg.d + "',propertyid: '" + idForUpdate + "', propertyno : '" + t.rows().data()[i].PropertyNumber + "', serialno: '" + t.rows().data()[i].Serial + "',longdescription: '" + t.rows().data()[i].Description + "',warrantydate: '" + t.rows().data()[i].WarrantyDate + "', insurancedate: '" + t.rows().data()[i].InsuranceDate + "', expirydate: '" + t.rows().data()[i].ExpiryDate + "', labeltoprint: '', rfidnumber: '" + t.rows().data()[i].RFIDNumber + "', barcode: '" + t.rows().data()[i].Barcode + "', acquiredcost: '" + acquiredcost + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        console.log("successfully added to list('" + i + "')");
                                        GetCount();
                                        $("#txtPropertyQuantity").val("");
                                        $("#txtAcquiredCost").val("");
                                    }
                                });
                            };
                            swal("Successful", "Property Added Successfully!", "success");
                            $("#addToPropertyGoodsReceive").modal("hide");
                        }
                    });
                    SetRFIDandBarcodeCount(rfidinc, barcodeinc, quantity);
                }
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
    else {
        swal({
            title: "Add Property?",
            text: "Are you sure you want to add this property to list?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                //if (confirm('Are You sure you want to add this property to list?') == false)
                //    return false;
                var supplier = $('#ddlSupplier').val();
                var dateofacquisition = $('#txtDOA').val();
                var prnumber = $('#txtPRNumber').val();
                var receivedby = $('#ddlReceivedBy').val();
                var ponumber = $('#txtPONumber').val();
                var custodian = $('#BodyHolder_ddlCustodian').val();
                var invoicenumber = $('#txtInvoiceNumber').val();
                var approver = 0;
                var drnumber = $('#txtDRNumber').val();
                var remarks = $('#BodyHolder_txtRemarks').val();

                var site = $('#ddlFromSite').val();
                var assignedperson = $('#ddlEmployee').val();
                //if (assignedperson == 'NA') {
                //    assignedperson = 0;
                //}
                var location = $('#ddlLocation1').val();

                if ($('#txtDRNumber').val() == '')
                    drnumber = 'N/A';
                if ($('#txtInvoiceNumber').val() == '')
                    invoicenumber = 'N/A';

                var saleprice = $("#txtSalePrice").val();
                var quantity = parseInt($("#txtPropertyQuantity").val());
                var acquiredcost = $("#txtAcquiredCost").val();
                acquiredcost = acquiredcost.replaceAll(",", "");
                //var saleprice = $("#txtSalePrice").val();

                if ($('#txtPropertyQuantity').val().trim() == '' || serialctr != 0 || rfidctr != 0 || barcodectr != 0 || propertynoctr != 0 ||
                    $('#txtPropertyQuantity').val().trim() == '0' ||
                    $('#txtAcquiredCost').val().trim() == '' ||
                    //$('#txtSalePrice').val().trim() == '' ||
                    $(".txtSerial").hasClass("hasError") || $(".txtPropertyNumber").hasClass("hasError")) {
                    alertMsg = '';

                    if ($('#txtPropertyQuantity').val().trim() == '' || $('#txtPropertyQuantity').val().trim() == '0')
                        alertMsg += '\nQuantity';
                    if ($('#txtAcquiredCost').val().trim() == '')
                        alertMsg += '\nAcquired Cost';
                    //if ($('#txtSalePrice').val().trim() == '')
                    //    alertMsg += '\nSale Price';
                    if (propertynoctr != 0)
                        alertMsg += '\nProperty #';
                    if (serialctr != 0)
                        alertMsg += '\nSerial #';
                    if (rfidctr != 0)
                        alertMsg += '\nRFID #';
                    if (barcodectr != 0)
                        alertMsg += '\nBarcode #';
                    if ($(".txtSerial").hasClass("hasError"))
                        alertMsg += '\nDuplicate Serial';
                    if ($(".txtPropertyNumber").hasClass("hasError"))
                        alertMsg += '\nDuplicate Property #';
                    //alert(alertMsg);
                    swal("Fill-out all the required field(s): ", alertMsg, "warning");
                }

                else {
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/GoodsReceive.aspx/AddToGroupPropertyList",
                        data: " { propertyid: '" + idForUpdate + "',supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                            + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                            + remarks + "', quantity : '" + quantity + "',acquiredcost : '" + acquiredcost + "',saleprice : '0', site : '" + site + "',assignedperson : '" + assignedperson + "',location : '" + location + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log("successfully added to list (" + msg.d + ")");
                            /////////initialise datatable
                            //var table = $('#tblPropertyAdded').DataTable();
                            //////// Create a hidden element
                            for (var i = 0; i < t.rows().data().length; i++) {
                                t.rows().data()[i].PropertyNumber = $("#txtPropertyNumber" + i).val();
                                t.rows().data()[i].Serial = $("#txtSerial" + i).val();
                                t.rows().data()[i].Description = $("#txtDescription" + i).val();
                                t.rows().data()[i].WarrantyDate = $("#txtWarrantyDate" + i).val();
                                t.rows().data()[i].InsuranceDate = $("#txtInsurance" + i).val();
                                t.rows().data()[i].ExpiryDate = $("#txtExpiryDate" + i).val();
                                t.rows().data()[i].LabelToPrint = $("#txtLabelToPrint" + i).val();
                                t.rows().data()[i].RFIDNumber = $("#txtrfid" + i).val();
                                t.rows().data()[i].Barcode = $("#txtbarcode" + i).val();
                                ////////////pass to code behind for saving process
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/GoodsReceive.aspx/AddToPropertyList",
                                    data: " { grpropertyid : '" + msg.d + "',propertyid: '" + idForUpdate + "', propertyno : '" + t.rows().data()[i].PropertyNumber + "',serialno: '" + t.rows().data()[i].Serial + "',longdescription: '',warrantydate: '" + t.rows().data()[i].WarrantyDate + "', insurancedate: '" + t.rows().data()[i].InsuranceDate + "', expirydate: '" + t.rows().data()[i].ExpiryDate + "', labeltoprint: '', rfidnumber: '" + t.rows().data()[i].RFIDNumber + "', barcode: '" + t.rows().data()[i].Barcode + "', acquiredcost: '" + acquiredcost + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        console.log("successfully added to list('" + i + "')");
                                        GetCount();
                                        $("#txtPropertyQuantity").val("");
                                        $("#txtAcquiredCost").val("");
                                    }
                                });
                            };
                            //alert('Property Added Successfully!');
                            swal("Successful", "Property Added Successfully!", "success");
                            $("#addToPropertyGoodsReceive").modal("hide");
                        }
                    });
                    SetRFIDandBarcodeCount(rfidinc, barcodeinc, quantity);
                }
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

var rfidinc = false;
var barcodeinc = false;

function SetRFIDandBarcodeCount(rfidinc, barcodeinc, increment) {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/SetRFIDandBarcodeCount",
        data: " { rfidinc: '" + rfidinc + "', barcodeinc: '" + barcodeinc + "', increment : '" + increment + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}

function getRFIDfromFile(txtrfid) {
    $("#txtrfid" + txtrfid).val("");
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/getRFIDfromFile",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#txtrfid" + txtrfid).val(msg.d);
            //alert(msg.d);
        }
    });
}

function RevertRFIDNumbers() {
    var storedrfids = [];
    var inputRFIDnumbers = $('.txtrfid');
    $.each(inputRFIDnumbers, function (k, v) {
        var getVal = $(v).val();
        if (storedrfids.indexOf(getVal) != -1) {
            //$(v).fadeOut();
            var element = document.getElementById(v.id);
            //element.classList.add("hasError");
            console.log(k);
        }
        else {
            storedrfids.push($(v).val());
        }
    });
    //alert(storedrfids.join(','));
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/RevertRFIDNumbers",
        data: " { epc : '" + storedrfids.join(',') + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //$("#txtrfid" + txtrfid).val(msg.d);
        }
    });
}

function QuantityKeyUp() {
    stored = [];
    var n = parseInt($("#txtPropertyQuantity").val());
    var i = 0;
    //var t = $("#tblPropertyAdded").DataTable({
    //    "autoWidth": true
    //});
    if (n > 50) {
        swal("Warning", "The maximum number is 50.", "warning");
        $("#txtPropertyQuantity").val(50);
        n = 50;
        //return false;
    }

    var date = new Date();
    var year = date.getFullYear();

    var tagtype = ttid;
    //var tagtype = $('#BodyHolder_ddlTagType').val();
    //if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag Type ---') {
    //    //alert('Please select tag Type First');
    //    swal("Warning", "Select Tag Type First", "warning")
    //}
    //else {
    t.clear().draw(false);
    //
    for (var i; i < n; i++) {
        var pad = 'A' + year + '0000000';
        var barcodecnt = '' + (parseInt(barcodecount) + parseInt(i));
        var rfidcnt = '' + (parseInt(rfidcount) + parseInt(i));
        var rfidnumber = pad.substr(0, pad.length - rfidcnt.length) + rfidcnt;
        var barcodenumber = pad.substr(0, pad.length - barcodecnt.length) + barcodecnt;

        if (rfid == "Auto generated" && barcode == "Auto generated") {
            if (tagtype == 1) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" disabled ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '" value="' + barcodenumber + '" disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                barcodeinc = true;
            }
            else if (tagtype == 4) {
                t.row.add(
                    [
                        //'<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" value="' + rfidnumber + '" disabled ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '"  disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                rfidinc = true;
            }
            else {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" value="' + rfidnumber + '" disabled ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '" value="' + barcodenumber + '" disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                rfidinc = true;
                barcodeinc = true;
            }
        }
        else if (rfid == "Auto generated" && barcode == "Manual Input") {
            if (tagtype == 1) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" disabled ></input>',
                        '<input class="form-control txtbarcode" onkeypress="return EnterKey(event);" id="txtbarcode' + i + ' ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');" >Clear</button>'
                    ]
                ).draw(false);
            }
            else if (tagtype == 4) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" value="' + rfidnumber + '" disabled ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '" disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                rfidinc = true;
            }
            else {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" value="' + rfidnumber + '" disabled ></input>',
                        '<input class="form-control txtbarcode" onkeypress="return EnterKey(event);" id="txtbarcode' + i + '" ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                rfidinc = true;
            }
        }
        else if (rfid == "Manual Input" && barcode == "Auto generated") {
            if (tagtype == 1) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" id="txtrfid' + i + '" disabled ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '"  value="' + barcodenumber + '" disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                barcodeinc = true;
            }
            else if (tagtype == 4) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '"  disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                barcodeinc = true;
            }
            else {
                t.row.add(
                   [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                       '<input class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" ></input>',
                       '<input class="form-control txtbarcode" id="txtbarcode' + i + '" value="' + barcodenumber + '" disabled ></input>',
                       '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                   ]
               ).draw(false);
                barcodeinc = true;
            }
        }
        else if (rfid == "RADMWare" && barcode == "Auto generated") {
            if (tagtype == 1) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty" onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<div class="input-group margin"><input type="text" class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" disabled ><span class="input-group-btn"><button type="button" class="btn btn-flat" onclick="getRFIDfromFile(' + i + ')"><i class="fa fa-wifi"></i></button></span></div>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '"  value="' + barcodenumber + '" disabled ;"></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                barcodeinc = true;
            }
            else if (tagtype == 4) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        //'<input class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" ></input>',
                        '<div class="input-group margin"><input type="text" class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" disabled ><span class="input-group-btn"><button type="button" class="btn btn-flat" onclick="getRFIDfromFile(' + i + ')"><i class="fa fa-wifi"></i></button></span></div>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '"  disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
                barcodeinc = true;
            }
            else {
                t.row.add(
                   [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                       //'<input class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" ></input>',
                        '<div class="input-group margin"><input type="text" class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" disabled ><span class="input-group-btn"><button type="button" class="btn btn-flat" onclick="getRFIDfromFile(' + i + ')"><i class="fa fa-wifi"></i></button></span></div>',
                       '<input class="form-control txtbarcode" id="txtbarcode' + i + '" value="' + barcodenumber + '" disabled ></input>',
                       '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                   ]
               ).draw(false);
                barcodeinc = true;
            }
        }
        else {
            if (tagtype == 1) {
                t.row.add(
                 [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                     '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                     '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                     '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                     '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                     '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                     '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                     '<input class="form-control txtrfid" id="txtrfid' + i + '" disabled></input>',
                     '<input class="form-control txtbarcode" onkeypress="return EnterKey(event);" id="txtbarcode' + i + '" ></input>',
                     '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                 ]
             ).draw(false);
            }
            else if (tagtype == 4) {
                t.row.add(
                    [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                        '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                        '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                        '<input class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '" ></input>',
                        '<input class="form-control txtbarcode" id="txtbarcode' + i + '"  disabled ></input>',
                        '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                    ]
                ).draw(false);
            }
            else {
                t.row.add(
                   [
                        '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + i + '" ></input>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + i + '"  ></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" id="txtDescription' + i + '"></input>',
                        '<input class="form-control txtwarranty"  onkeypress="return EnterKey(event);"  type="text" id="txtWarrantyDate' + i + '"></input>',
                        '<input class="form-control txtinsurance" onkeypress="return EnterKey(event);"  type="text" id="txtInsurance' + i + '"></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtExpiryDate' + i + '"></input>',
                       '<input class="form-control" onkeypress="return EnterKey(event);" id="txtLabelToPrint' + i + '" ></input>',
                       '<input class="form-control txtrfid" onkeypress="return EnterKey(event);" id="txtrfid' + i + '"  ></input>',
                       '<input class="form-control txtbarcode" onkeypress="return EnterKey(event);" id="txtbarcode' + i + '"  ></input>',
                       '<button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + i + '" type="button" onclick="ClearRow(' + i + ');">Clear</button>'
                   ]
               ).draw(false);
            }
        }

        $('#tblPropertyAdded').off('click');
        var warrantydates = $('.txtwarranty');
        $.each(warrantydates, function (k, v) {
            //var element = document.getElementById(v.id);
            $('#' + v.id).daterangepicker({
                "showDropdowns": true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                    'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')],
                    'This Year': [moment().startOf('year'), moment().endOf('year')]
                },
                "alwaysShowCalendars": true,
                "startDate": "01/01/1900",
                "endDate": "01/01/1900",
                "minDate": "01/01/1900",
                "opens": "center",
                "drops": "up"
            }, function (start, end, label) {
                console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            }).on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
            });
        });

        var insurancedates = $('.txtinsurance');
        $.each(insurancedates, function (k, v) {
            //var element = document.getElementById(v.id);
            $('#' + v.id).daterangepicker({
                "showDropdowns": true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                    'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')],
                    'This Year': [moment().startOf('year'), moment().endOf('year')]
                },
                "alwaysShowCalendars": true,
                "startDate": "01/01/1900",
                "endDate": "01/01/1900",
                "minDate": "01/01/1900",
                "opens": "center",
                "drops": "up"
            }, function (start, end, label) {
                console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            }).on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
            });
        });
    }
    //t.draw().columnDefs({ width: 200, targets: [3, 4] });
    //}
}

var getcount = 0;
function GetCount() {
    var supplier = $('#ddlSupplier').val();
    var dateofacquisition = $('#txtDOA').val();
    var prnumber = $('#txtPRNumber').val();
    var receivedby = $('#ddlReceivedBy').val();
    var ponumber = $('#txtPONumber').val();
    var custodian = $('#BodyHolder_ddlCustodian').val();
    var invoicenumber = $('#txtInvoiceNumber').val();
    var approver = $('#ddlApprover').val();
    var drnumber = $('#txtDRNumber').val();
    var remarks = $('#BodyHolder_txtRemarks').val();

    var site = $('#ddlFromSite').val();
    var assignedperson = $('#ddlEmployee').val();
    var location = $('#ddlLocation1').val();

    if ($('#txtDRNumber').val() == '')
        drnumber = 'N/A';
    if ($('#txtInvoiceNumber').val() == '')
        invoicenumber = 'N/A';
    if ($('#ddlSupplier').val() == '0')
        supplier = 0;
    if ($('#ddlReceivedBy').val() == '0')
        receivedby = 0;
    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
        custodian = 0;
    if ($('#ddlApprover').val() == '0')
        approver = 0;
    if ($('#ddlFromSite').val() == '--- Select ' + SiteName + '/Department ---')
        site = 0;
    if ($('#ddlLocation1').val() == '--- Select ' + LocName + ' ---')
        location = 0;
    if ($('#ddlEmployee').val() == '0')
        assignedperson = 0;
    //alert("{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
    //            + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
    //            + remarks + "',site: '" + site + "',location: '" + location + "',assignedperson: '" + assignedperson + "'}");
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/GetCountGoodsReceive",
        data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                + remarks + "',site: '" + site + "',location: '" + location + "',assignedperson: '" + assignedperson + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg.d);
            $("#btnPL span").text(" View List For Good Receive: " + msg.d);
            getcount = msg.d;
            if (msg.d == 0) {
                $('#viewAddedPropertyList').modal('hide');
            }
            else { }
        }
    });
}

function ClearRow(i) {
    $("#txtSerial" + i).val("");
    $("#txtDescription" + i).val("");
    $("#txtExpiryDate" + i).val("");
    $("#txtLabelToPrint" + i).val("");
    //$("#txtrfid" + i).val("");
    //$("#txtbarcode" + i).val("");
}

function PropertyAddedList() {
    GetCount();
    if (getcount == 0) {
        swal("Warning", "Need to enter GR details.", "warning");
    }
    else {
        load_data_propertyaddedlist();
        $("#viewAddedPropertyList").modal("show");
    }

    //var approver = $('#ddlApprover').val();
    //alert(approver);
}

function load_data_propertyaddedlist() {
    if (needsapproval == "True") {
        var supplier = $('#ddlSupplier').val();
        var dateofacquisition = $('#txtDOA').val();
        var prnumber = $('#txtPRNumber').val();
        var receivedby = $('#ddlReceivedBy').val();
        var ponumber = $('#txtPONumber').val();
        var custodian = $('#BodyHolder_ddlCustodian').val();
        var invoicenumber = $('#txtInvoiceNumber').val();
        var approver = $('#ddlApprover').val();
        var drnumber = $('#txtDRNumber').val();
        var remarks = $('#BodyHolder_txtRemarks').val();

        var site = $('#ddlFromSite').val();
        var assignedperson = $('#ddlEmployee').val();
        var location = $('#ddlLocation1').val();

        if ($('#txtDRNumber').val() == '')
            drnumber = 'N/A';
        if ($('#txtInvoiceNumber').val() == '')
            invoicenumber = 'N/A';
        if ($('#ddlSupplier').val() == '0')
            supplier = 0;
        if ($('#ddlReceivedBy').val() == '0' || $('#ddlReceivedBy').val() == null)
            receivedby = 0;
        if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
            custodian = 0;
        if ($('#ddlApprover').val() == '--- Select Approver ---' || $('#ddlApprover').val() == null)
            approver = 0;
        if ($('#ddlFromSite').val() == '--- Select ' + SiteName + '/Department ---')
            site = 0;
        if ($('#ddlLocation1').val() == '--- Select ' + LocName + ' ---')
            location = 0;
        //if ($('#ddlEmployee').val() == 'NA')
        //    assignedperson = 0;

        $.ajax({
            type: 'POST',
            url: "Operation_Module/GoodsReceive.aspx/GetPropertyAddedList",
            data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                    + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                    + remarks + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#propertyaddedlist').hide();
                $('#propertyaddedlist').innerHTML = "";
                $('#propertyaddedlist').fadeIn(200);
                myJsonData = data;
                populateviewPropertyAddedList(myJsonData);
                //console.log(data);
                //console.log('Success');
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
            }
        });
    }
    else {
        var supplier = $('#ddlSupplier').val();
        var dateofacquisition = $('#txtDOA').val();
        var prnumber = $('#txtPRNumber').val();
        var receivedby = $('#ddlReceivedBy').val();
        var ponumber = $('#txtPONumber').val();
        var custodian = $('#BodyHolder_ddlCustodian').val();
        var invoicenumber = $('#txtInvoiceNumber').val();
        var drnumber = $('#txtDRNumber').val();
        var remarks = $('#BodyHolder_txtRemarks').val();

        var site = $('#ddlFromSite').val();
        var assignedperson = $('#ddlEmployee').val();
        var location = $('#ddlLocation1').val();

        if ($('#txtDRNumber').val() == '')
            drnumber = 'N/A';
        if ($('#txtInvoiceNumber').val() == '')
            invoicenumber = 'N/A';
        if ($('#ddlSupplier').val() == '0')
            supplier = 0;
        if ($('#ddlReceivedBy').val() == '0' || $('#ddlReceivedBy').val() == null)
            receivedby = 0;
        if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
            custodian = 0;
        if ($('#ddlFromSite').val() == '--- Select ' + SiteName + '/Department ---')
            site = 0;
        if ($('#ddlLocation1').val() == '--- Select ' + LocName + ' ---')
            location = 0;
        if ($('#ddlEmployee').val() == '0')
            assignedperson = 0;

        $.ajax({
            type: 'POST',
            url: "Operation_Module/GoodsReceive.aspx/GetPropertyAddedList",
            data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                    + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '0',drnumber: '" + drnumber + "',remarks: '"
                    + remarks + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#propertyaddedlist').hide();
                $('#propertyaddedlist').innerHTML = "";
                $('#propertyaddedlist').fadeIn(200);
                myJsonData = data;
                populateviewPropertyAddedList(myJsonData);
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
}

function populateviewPropertyAddedList(data) {
    try {
        var table = $('#tblPropertyAddedList').DataTable({
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
                //{
                //    "class": "details-control",
                //    "orderable": false,
                //    "data": null,
                //    "defaultContent": ""
                //},
                { "data": "AssetType" },
                { "data": "ItemCode" },

                { "data": "PRNumber" },
                { "data": "PONumber" },
                { "data": "InvoiceNumber" },
                { "data": "DRNumber" },
                { "data": "Remarks" },
                { "data": "QuantityString" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        $('#tblPropertyAddedList').off('click');

        $('#tblPropertyAddedList').on('click', 'button.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            currentRow.remove();
        });

        ////////////////row details//////////////
        //var detailRows = [];
        //$('#tblPropertyAddedList tbody').on('click', 'tr td.details-control', function () {
        //    var tr = $(this).closest('tr');
        //    var row = table.row(tr);
        //    var idx = $.inArray(tr.attr('id'), detailRows);

        //    if (row.child.isShown()) {
        //        tr.removeClass('details');
        //        //row.child.hide();

        //        // Remove from the 'open' array
        //        detailRows.splice(idx, 1);
        //    }
        //    else {
        //        tr.addClass('details');
        //        //row.child(rowdetails(row.data())).show();
        //        rowdetails(row.data());

        //        // Add to the 'open' array
        //        if (idx === -1) {
        //            detailRows.push(tr.attr('id'));
        //        }
        //    }
        //});

        //// On each draw, loop over the `detailRows` array and show any child rows
        //table.on('draw', function () {
        //    $.each(detailRows, function (i, id) {
        //        $('#' + id + ' td.details-control').trigger('click');
        //    });
        //});
    } catch (e) {
        var table = $('#tblPropertyAddedList').DataTable();
        table
            .clear()
            .draw();
    }
}

function rowdetailssample(d) {
    var html = ''
    $.ajax({
        type: 'POST',
        url: "Operation_Module/GoodsReceive.aspx/getPropertyDetails",
        data: "{ grpropertyid: '" + d.GRPropertyID + "', propertyid : '" + d.PropertyID + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            html = data.d;
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
    return html;
}

function rowdetails(d) {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/GoodsReceive.aspx/getPropertyDetails",
        data: "{ grpropertyid: '" + d.GRPropertyID + "', propertyid : '" + d.PropertyID + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#propertydetails').hide();
            $('#propertydetails').innerHTML = "";
            $('#propertydetails').fadeIn(200);
            myJsonData = data;
            populateRowDetails(myJsonData);
            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });

    $("#viewPropertyDetails").modal("show");
}

function populateRowDetails(data) {
    try {
        $('#tblPropertyDetails').DataTable({
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
                { "data": "SerialNo" },
                { "data": "LongDescription" },
                { "data": "ExpiryDate" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        $('#tblPropertyDetails').off('click');
    } catch (e) {
        var table = $('#tblPropertyDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function RemoveToGoodsReceiveProperty(grpropertyid) {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/RemoveToPropertyList",
        data: " { grpropertyid : '" + grpropertyid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log("successfully removed to list");
            GetCount();
        }
    });
}

function RemoveToGoodsReceiveList(detailsid, grpropertyid, goodsreceivenumber) {
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/RemoveToGoodsReceiveList",
        data: " { detailsid : '" + detailsid + "', grpropertyid : '" + grpropertyid + "', goodsreceivenumber : '" + goodsreceivenumber + "'  } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log("successfully removed from list");

            load_data_GRD(GRID);
            load_data_GRH();
        }
    });
}

function RemoveAllToGoodsReceiveProperty() {
    //if (confirm("Cancel this whole list and start over again.\n\nAre you sure you want to cancel this transaction?") == false)
    //    return false;
    swal({
        title: "Are you sure you want to cancel this transaction",
        text: "Cancel this whole list and start over again.",
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
                url: "Operation_Module/GoodsReceive.aspx/RemoveAllToPropertyList",
                data: " { } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#viewAddedPropertyList").modal("hide");
                    //alert("Transaction successfully cancelled.");
                    swal("Cancelled", "Transaction successfully cancelled.", "success");
                    GetCount();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmGoodsReceive() {
    if (needsapproval == "True") {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to confirm this transaction?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                //if (confirm("Are you sure, you want to Confirm this transaction?") == false)
                //    return false;
                $(".se-pre-con").fadeIn();
                if (getcount == "0") {
                } else {
                    var supplier = $('#ddlSupplier').val();
                    var dateofacquisition = $('#txtDOA').val();
                    var prnumber = $('#txtPRNumber').val();
                    var receivedby = $('#ddlReceivedBy').val();
                    var ponumber = $('#txtPONumber').val();
                    var custodian = $('#BodyHolder_ddlCustodian').val();
                    var invoicenumber = $('#txtInvoiceNumber').val();
                    var approver = $('#ddlApprover').val();
                    var drnumber = $('#txtDRNumber').val();
                    var remarks = $('#BodyHolder_txtRemarks').val();

                    var site = $('#ddlFromSite').val();
                    var assignedperson = $('#ddlEmployee').val();
                    if (assignedperson == '0') {
                        assignedperson = 0;
                    }
                    var location = $('#ddlLocation1').val();

                    if ($('#txtDRNumber').val() == '')
                        drnumber = 'N/A';
                    if ($('#txtInvoiceNumber').val() == '')
                        invoicenumber = 'N/A';
                    if ($('#ddlSupplier').val() == '0')
                        supplier = 0;
                    if ($('#ddlReceivedBy').val() == '0')
                        receivedby = 0;
                    if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                        custodian = 0;
                    if ($('#ddlApprover').val() == '0')
                        approver = 0;
                    if ($('#ddlFromSite').val() == '--- Select ' + SiteName + '/Department ---')
                        site = 0;
                    if ($('#ddlLocation1').val() == '--- Select ' + LocName + ' ---')
                        location = 0;
                    if ($('#ddlEmployee').val() == '0')
                        assignedperson = 0;

                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/GoodsReceive.aspx/ConfirmGoodsReceive",
                        data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                                       + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '" + approver + "',drnumber: '" + drnumber + "',remarks: '"
                                       + remarks + "' ,site: '" + site + "',location: '" + location + "',assignedperson: '" + assignedperson + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert("Transaction succesfully confirmed and Pending For Approval/Upload.\n\nMail not sent. Please check you mail settings.");
                                swal("Transaction Successful!", "Transaction succesfully confirmed and Pending For Approval/Upload.\nMail not sent. Check your mail settings.", "success");
                                load_data_GRH();
                                GetCount();
                                $("#viewAddedPropertyList").modal("hide");
                            }
                            else if (msg.d == "okay") {
                                //alert("Transaction succesfully confirmed. Notification Email sent to approver.");
                                swal("Transaction Successful!", "Transaction succesfully confirmed. Notification Email sent to approver.", "success");
                                load_data_GRH();
                                GetCount();
                                $("#viewAddedPropertyList").modal("hide");
                            }
                            else {
                                //alert('Transaction succesfully confirmed and Pending For Approval/Upload.\n\nMail not sent. Approver has no email.');
                                swal("Transaction Successful!", "Transaction succesfully confirmed and Pending For Approval/Upload.\nMail not sent. Approver has no email.", "success");
                                load_data_GRH();
                                GetCount();
                                $("#viewAddedPropertyList").modal("hide");
                            }
                            ClearGR();
                            $(".se-pre-con").hide();
                        }
                    });
                }
            }
            else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to confirm this transaction?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                //if (confirm("Are you sure, you want to Confirm this transaction?") == false)
                //    return false;
                $(".se-pre-con").fadeIn();
                var supplier = $('#ddlSupplier').val();
                var dateofacquisition = $('#txtDOA').val();
                var prnumber = $('#txtPRNumber').val();
                var receivedby = $('#ddlReceivedBy').val();
                var ponumber = $('#txtPONumber').val();
                var custodian = $('#BodyHolder_ddlCustodian').val();
                var invoicenumber = $('#txtInvoiceNumber').val();
                var drnumber = $('#txtDRNumber').val();
                var remarks = $('#BodyHolder_txtRemarks').val();

                var site = $('#ddlFromSite').val();
                var assignedperson = $('#ddlEmployee').val();
                var location = $('#ddlLocation1').val();

                if ($('#txtDRNumber').val() == '')
                    drnumber = 'N/A';
                if ($('#txtInvoiceNumber').val() == '')
                    invoicenumber = 'N/A';
                if ($('#ddlSupplier').val() == '0')
                    supplier = 0;
                if ($('#ddlReceivedBy').val() == '0')
                    receivedby = 0;
                if ($('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---')
                    custodian = 0;

                if ($('#ddlFromSite').val() == '--- Select ' + SiteName + '/Department ---')
                    site = 0;
                if ($('#ddlLocation1').val() == '--- Select ' + LocName + ' ---')
                    location = 0;
                if ($('#ddlEmployee').val() == '0')
                    assignedperson = 0;

                //alert("{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                //                   + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '0',drnumber: '" + drnumber + "',remarks: '"
                //                   + remarks + "',site: '" + site + "',location: '" + location + "',assignedperson: '" + assignedperson + "' }");
                $.ajax({
                    type: "POST",
                    url: "Operation_Module/GoodsReceive.aspx/ConfirmGoodsReceive",
                    data: "{ supplier: '" + supplier + "',dateofacquisition: '" + dateofacquisition + "', prnumber: '" + prnumber + "', receivedby: '" + receivedby + "' , ponumber: '"
                                   + ponumber + "',custodian: '" + custodian + "',invoicenumber: '" + invoicenumber + "',approver: '0',drnumber: '" + drnumber + "',remarks: '"
                                   + remarks + "',site: '" + site + "',location: '" + location + "',assignedperson: '" + assignedperson + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "sorry") {
                            //alert("Transaction succesfully confirmed and Pending For Approval/Upload.\n\nMail not sent. Please check you mail settings.");
                            swal("Transaction Successful!", "Transaction succesfully confirmed and Pending For Upload.", "success");
                            load_data_GRH();
                            GetCount();
                            $("#viewAddedPropertyList").modal("hide");
                        }
                        else if (msg.d == "okay") {
                            //alert("Transaction succesfully confirmed. Notification Email sent to approver.");
                            swal("Transaction Successful!", "Transaction succesfully confirmed.", "success");
                            load_data_GRH();
                            GetCount();
                            $("#viewAddedPropertyList").modal("hide");
                        }
                        else {
                            //alert('Transaction succesfully confirmed and Pending For Approval/Upload.\n\nMail not sent. Approver has no email.');
                            swal("Transaction Successful!", "Transaction succesfully confirmed and Pending For Upload.", "success");
                            load_data_GRH();
                            GetCount();
                            $("#viewAddedPropertyList").modal("hide");
                        }
                        $(".se-pre-con").hide();
                    }
                });
            }
            else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function load_data_GRH() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/GoodsReceive.aspx/GetGoodsReceiveHeader",
        data: "{search : '" + search + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#goodsreceive').hide();
            $('#goodsreceive').innerHTML = "";
            $('#goodsreceive').fadeIn(200);
            myJsonData = data;
            populateDataTableGRH(myJsonData);
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

function populateDataTableGRH(data) {
    try {
        $('#tblGoodsReceive').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "GoodsReceiveNumber" },
                { "data": "Receiver" },
                                { "data": "CreatedBy" },
                //{ "data": "NoOfItem" },
                { "data": "CreateDate" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        $('#tblGoodsReceive').off('click');
    } catch (e) {
        var table = $('#tblGoodsReceive').DataTable();
        table
            .clear()
            .draw();
    }
}

function viewGRDetails(id, goodsreceivenumber, status) {

    GRID = id;
    $('#BodyHolder_GRupload').val('');
    $('#btndownloadpdf').attr('transno', id);
    $('#btndownloadcreatepdf').attr('transno', id);
    $('#btndownloadcreatepdf').attr('transtype', 'IAR');
    $('#btndownloadcreatepdf').attr('operationtype', 'GoodsReceive');
    load_data_GRD(id);
    $('#BodyHolder_txtGoodsReceiveID').val(id);
    $('#BodyHolder_txtGoodsReceiveNumber').val(goodsreceivenumber);
    var x = document.getElementById("completetransaction");
    var y = document.getElementById("idPrint");
    if (status == "Pending Upload") {
        x.style.visibility = 'visible';
        y.style.visibility = 'visible';
    }
    else {
        x.style.visibility = 'hidden';
        y.style.visibility = 'visible';
    }
    $("#viewGRDetails").modal("show");
}

function load_data_GRD(id) {

    //gheghe(Certificate/Word)
    //document.getElementById("printGrid").children[1].style.display = "none";
    //document.getElementById("printGrid").children[2].style.display = "none";
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/GetGoodsReceiveDetailsForm",
        data: "{goodsreceiveid : '" + id + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg.d);
            $('#BodyHolder_divGR').hide();
            $('#BodyHolder_divGR').html(msg.d);
            $('#BodyHolder_divGR').fadeIn(200);
        }
    });



    $.ajax({
        type: 'POST',
        url: "Operation_Module/GoodsReceive.aspx/GetGoodsReceiveDetails",
        data: "{goodsreceiveid: '" + id + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#grdetails').hide();
            $('#grdetails').innerHTML = "";
            $('#grdetails').fadeIn(200);
            myJsonData = data;
            populateDataTableGRD(myJsonData);
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

function populateDataTableGRD(data) {
    try {
        $('#tblGRDetails').DataTable({
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
                { "data": "AssetType" },
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "PRNumber" },
                { "data": "PONumber" },
                { "data": "InvoiceNumber" },
                { "data": "DRNumber" },
                { "data": "Remarks" },
                { "data": "QuantityString" },
                { "data": "UnitCostString" },
                { "data": "AcquiredCostString" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
        $('#tblGRDetails').off('click');
    } catch (e) {
        var table = $('#tblGRDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function cancelGoodsReceive(id) {
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
    textareatt.id = "cancelgr";

    swal({
        title: "Are you sure?",
        text: "Are you sure you want to cancel this Goods Receive?",
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

            var textremarks = $('#cancelgr').val();

            if (textremarks === "") {
                swal("Warning", "Add reason to CANCEL this transaction.", "warning");
                return false
            }

            textremarks = ReplaceStr1(textremarks);
            $.ajax({
                type: "POST",
                url: "Operation_Module/GoodsReceive.aspx/CancelGoodsReceive",
                data: " { goodsreceiveid : '" + id + "', remarks: '" + textremarks + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log("Goods Receive Cancelled Successfully!");
                    swal("Success", "Goods Receive Cancelled Successfully!", "success");
                    load_data_GRH();
                }
            });
        }
        else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}
var warehouseid = 0;
var SiteName = "";
function GetWarehouseByIDGR() {

    SiteName = document.getElementById("BodyHolder_lbldisplaySite1").textContent.replace(":", "");
    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/GetWarehouseByID",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //$('#txtWarehouseName').val(msg.d.split('|')[1]);
            // warehouseid = msg.d.split('|')[0];

            var ddlFromSite = $("[id*=ddlFromSite]");
            ddlFromSite.empty();//.append('<option selected="selected" value="0">--- Select Location ---</option>');
            ddlFromSite.append('<option value="0">--- Select ' + SiteName + '/Department ---</option>');
            // ddlFromSite.append('<option value="' + msg.d.split('|')[0] + '">' + msg.d.split('|')[1] + '</option>');
            console.log(msg.d);
            $.each(msg.d, function () {
                ddlFromSite.append($("<option></option>").val(this['Text']).html(this['Value']));
            });

            GetLocation();
        }
    });
}

var LocName = "";
function GetLocation() {
    warehouseid = $('#ddlFromSite').val();
    //if ($('#ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    LocName = document.getElementById("BodyHolder_lbldisplayLoc").textContent.replace(":", "");

    $.ajax({
        type: "POST",
        url: "Operation_Module/GoodsReceive.aspx/GetLocationListLT",
        data: " { warehouse : '" + warehouseid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlLocation = $("[id*=ddlLocation]");
            ddlLocation.empty();//.append('<option selected="selected" value="0">--- Select Location ---</option>');
            ddlLocation.append('<option value="0">--- Select ' + LocName + ' ---</option>');
            $.each(msg.d, function () {
                ddlLocation.append($("<option></option>").val(this['Text']).html(this['Value']));
            });
        }
    });
}

function ClearGR() {
    //dothis
    $('#ddlSupplier').val('0').trigger('change');
    $('#txtPRNumber').val('');
    $('#txtPONumber').val('');
    $('#ddlFromSite').val(0).trigger('change');
    $('#txtInvoiceNumber').val('');
    $('#txtDRNumber').val('');
    $('#BodyHolder_ddlCustodian').val('--- Select Custodian Type ---').trigger('change');
    $('#ddlReceivedBy').val(0).trigger('change');
    $('#ddlLocation1').val(0).trigger('change');
    $('#ddlEmployee').val('0').trigger('change');
    $('#BodyHolder_txtRemarks').val('');
    $('#ddlApprover').val('0');
}

function btnCompleteGR() {
    var goodsreceiveid = $('#BodyHolder_txtGoodsReceiveID').val();
    var goodsreceivenumber = $('#BodyHolder_txtGoodsReceiveNumber').val();
    var filess = $('#BodyHolder_GRupload').val();
    var filename = $("#BodyHolder_GRupload").get(0);
    if (filess == "") {
        filename = "";
    }
    if (filess == "") {
        swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want Complete this transaction?",
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
                    var fileUpload = $("#BodyHolder_GRupload").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: "Operation_Module/OperationUploadHandler.ashx?transactionnumber=" + goodsreceivenumber + "|GoodsReceive",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/GoodsReceive.aspx/UploadDocumentsGR",
                                    data: " { goodsreceiveid : '" + goodsreceiveid + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_GRH();
                                        $('#viewGRDetails').modal('hide');
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