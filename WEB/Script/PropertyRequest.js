$(document).ready(function () {
    $('.select2').select2();
    IfIssuanceNeedsApproval();

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDOR').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        startDate: '+0d',
        autoclose: true
    });

    $('#txtDOR').datepicker('setDate', today);

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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
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
    $("#tblRequestList").DataTable({
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
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
    $("#tblPUL2").DataTable({
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
    });
    load_data();
    load_data_pendingupload();
    $("#tblAssetList").DataTable().clear().draw();
    $("#tblRequestList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
    $("#tblPUL2").DataTable().clear().draw();
    GetApproverByCustodian();
    //$('#risbtn').off('click').on('click', function () {
    //    $('#RISsigandCertModal').modal('show');
    //});

    $('#risxcl').off('click').on('click', function () {
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
            data: "{ key1: '" + 'RIS' + "', key2: '" + requestno + "', key3: '" + 'PR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RIS_" + formattedDateTime + ".xls");
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
            url: "Operation_Module/PropertyRequest.aspx/CreateRISRequest",
            data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "', approveddatestamp:'" + approveddatestamp.prop('checked') + "', issueddatestamp:'" + issueddatestamp.prop('checked') + "' }",
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
                        a.attr("download", "Property_Request.pdf");
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
    ClearSignature();
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


function GetApproverByCustodian() {
    custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
    //if ($('#BodyHolder_ddlFromSite').val() == 'Receiving Site')
    //    warehouse = 1;
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyRequest.aspx/GetApproverByCustodian",
        data: " { custodianid : '" + custodianid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ddlApprover = $("[id*=ddlApprover]");
            ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
            //ddlCustodian.append('<option value="0">--- Select Approver ---</option>');
            $.each(msg.d, function () {
                ddlApprover.append($("<option></option>").val(this['Text']).html(this['Value']));
            });
        }
    });
}

function ValidateDate(dateString) {
    //Get the date from the TextBox.
    var dtCurrent = new Date() + 1;
    var parts = dateString.split("/");
    var dt = new Date(parts[0] + "/" + parts[1] + "/" + parts[2]);
    if (dt < dtCurrent)
        return false;
    return true;
}

var needsapproval = "False";
function IfIssuanceNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyRequest.aspx/IfRequestNeedsApproval",
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
        load_data_pendingupload();
        return false;
    }
}

function load_data() {
    var custodian = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
    //alert(custodian);

    var search = "";
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = "0";
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
    //$(".se-pre-con").fadeIn();
    //alert("s");
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequest.aspx/GetList",
        data: "{custodianid: '" + custodian + "', search: '" + search + "', expenseType: '" + ExpenseType + "'}",
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
            //GetCount();
            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request.2..");
            console.log("error: " + JSON.stringify(e));
        }
    });

    GetCount(); //added by nero 022224
    //var custodianid = $('#BodyHolder_ddlCustodian').val();
    //var search = $('#txtSearch').val().trim();
    //$(".se-pre-con").fadeIn();
    //$.ajax({
    //    type: 'POST',
    //    url: "Operation_Module/PropertyRequest.aspx/GetList",
    //    data: "{ custodianid : '" + custodianid + "', search: '" + search + "' }, search: '" + "1" + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {
    //        $('#assetlist').hide();
    //        $('#assetlist').innerHTML = "";
    //        $('#assetlist').fadeIn(200);

    //        myJsonData = data;
    //        populateDataTable(myJsonData);

    //        console.log(data);
    //        console.log('Success');

    //        $(".se-pre-con").fadeOut("slow");
    //    },
    //    error: function (e) {
    //        alert(e);
    //        console.log("There was an error with your request...");
    //        console.log("error: " + JSON.stringify(e));
    //    }
    //});
}

function load_data2() {
    var custodian = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
    var search = "";
    var expense = $('#BodyHolder_ddlItem').val();
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
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequest.aspx/GetList",
        data: "{custodian: '" + custodian + "', search: '" + search + "', ExpenseType: '" + ExpenseType + "'}",
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
            //GetCount();
            $(".se-pre-con").fadeOut("slow");
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
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
                    text: 'Add to Request List',
                    action: function () {
                        var ExpenseType = '0';
                        var expense = $('#BodyHolder_ddlItem').val();
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
                        var custodianid = $('#BodyHolder_ddlCustodian').val();
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (ExpenseType == 0 || custodianid == 0) {
                            swal("Warning", "Please select Custodian Type and Property Type cost", "warning");
                        }
                        else if (count == 0) {
                            swal("Warning", "Please select an item to add", "warning");
                        }
                        else {
                            $(".se-pre-con").fadeIn(100);
                            var ids = $.map($('#tblAssetList').DataTable().rows('.selected').data(), function (item) {
                                return item.pmlid;
                            });
                            console.log(ids);
                            ids.forEach((pmlid) => {
                                $.ajax({
                                    type: "POST",
                                    url: "Operation_Module/PropertyRequest.aspx/PropertyTransferToRequest",
                                    data: "{pmlid: '" + pmlid + "', custodianid : '" + custodianid + "', ExpenseType : '" + ExpenseType + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $('#assetlist').hide();
                                        $('#assetlist').innerHTML = "";
                                        $('#assetlist').fadeIn(200);
                                        GetCount();
                                    }
                                });
                            });
                            $('#tblAssetList').DataTable()
                            .rows('.selected')
                            .remove()
                            .draw(false);
                            $(".se-pre-con").hide();
                        }
                    }
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        //GetCount();
        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            var expense = $('#BodyHolder_ddlItem').val();
            var ExpenseType = "0";
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
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(0)").text();

            var custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyRequest.aspx/PropertyTransferToRequest",
                data: "{pmlid: '" + pmlid + "', custodianid : '" + custodianid + "', ExpenseType : '" + ExpenseType + "'}",
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
        });
    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function RequestList() {
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val(0).trigger('change');
    if (GetCounter == 0) {
        swal("Warning", "Please add property first.", "warning");
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        load_data();
        load_data_requestlist();

        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}

function load_data_requestlist() {
    var custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
    //added by nero 022724
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = "0";
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
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequest.aspx/GetPropertyRequestList",
        data: "{ custodianid: '" + custodianid + "', ExpenseType:'" + ExpenseType + "' }",
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
            $('#RequestList').modal('show');
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
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
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');

            var custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyRequest.aspx/PropertyRemoveToRequest",
                data: "{pmlid: '" + pmlid + "', custodianid: '" + custodianid + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#requestlist').hide();
                    $('#requestlist').innerHTML = "";
                    $('#requestlist').fadeIn(200);
                    load_data();
                    GetCount();
                    currentRow.remove();
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

function RemoveAllToRequest() {
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
            var expense = $('#BodyHolder_ddlItem').val();
            var ExpenseType = "0";
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
            var custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyRequest.aspx/PropertyRemoveAllToRequest",
                data: "{custodianid : '" + custodianid + "', ExpenseType : '" + ExpenseType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    //load_data_requestlist();
                    GetCount();
                    swal("Successful", "Property Request cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

var GetCounter = 0;
function GetCount() {
    var custodian = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = "0";
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
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyRequest.aspx/RequestGetCount",
        data: "{custodianid : '" + custodian + "',ExpenseType : '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Property Request List: " + msg.d);
            if (msg.d == 0) {
                $('#RequestList').modal('hide');
            }
            else {
            }
            GetCounter = msg.d;
        }
    });
}

function ConfirmPropertyRequest() {
    $('.btn').prop('disabled', true);
    var custodianid = $('#BodyHolder_ddlCustodian').val() == '--- Select Custodian Type ---' ? 0 : $('#BodyHolder_ddlCustodian').val();
    var expense = $('#BodyHolder_ddlItem').val();
    var ExpenseType = "0";
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
    var alertMsg;

    //alert($('#BodyHolder_ddlApprover').val());
    if (needsapproval == "True") {
        if ($('#BodyHolder_ddlApprover').val() == '0' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = '';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlApprover').val() == '0')
                alertMsg += "\nApprover";
            //if ($('#txtDOR').val().trim() == '')
            //    alertMsg += '\nDate of Return';
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");

            $('.btn').prop('disabled', false);
        }
        else if (ValidateDate(document.getElementById('txtDOR').value) == false) {
            $('.btn').prop('disabled', false);
            swal("Warning ", "Date of Return should not be less than today`s date", "warning");
            //alert('Date of Return should not be less than today`s date');
        }
        else {
            //if (confirm("Are you sure you want to CONFIRM this transaction?") == false) {
            //    $('.btn').prop('disabled', false);
            //    return false;
            //}

            swal({
                title: "Confirm Transaction",
                text: "Are you sure you want to CONFIRM this transaction?",
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
                        url: "Operation_Module/PropertyRequest.aspx/ConfirmPropertyRequest",
                        data: "{ approverid : '" + $('#BodyHolder_ddlApprover').val() + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "', dor: '" + document.getElementById('txtDOR').value + "', custodianid : '" + custodianid + "', ExpenseType : '" + ExpenseType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (needsapproval == "True") {
                                if (msg.d == "sorry") {
                                    //alert('Successfully Requested and Pending For Approval.\n\nMail not sent. Please check you mail settings.');
                                    swal("Successful", "Successfully Requested and Pending For Approval.\n\nMail not sent. Check you mail settings.", "success");
                                    load_data();
                                    load_data_pendingupload();
                                    $("#btnCart span").text(" 0");
                                    $('#RequestList').modal('hide');
                                    $('.btn').prop('disabled', false);
                                }
                                else {
                                    //alert('Successfully Requested and Pending For Approval.');
                                    swal("Successful", "Successfully Requested and Pending For Approval.", "success");
                                    load_data();
                                    load_data_pendingupload();
                                    $("#btnCart span").text(" 0");
                                    $('#RequestList').modal('hide');
                                    $('.btn').prop('disabled', false);
                                }
                            }
                            else {
                                swal("Successful", "Successfully Requested and Pending For Verification.", "success");
                                load_data();
                                load_data_pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#RequestList').modal('hide');
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
            alertMsg = '';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else if (ValidateDate(document.getElementById('txtDOR').value) == false) {
            $('.btn').prop('disabled', false);
            swal("Warning ", "Date of Return should not be less than today`s date", "warning");
        }
        else {
            swal({
                title: "Confirm Transaction",
                text: "Are you sure you want to CONFIRM this transaction?",
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
                        url: "Operation_Module/PropertyRequest.aspx/ConfirmPropertyRequest",
                        data: "{ approverid : '0', remarks : '" + $('#BodyHolder_txtRemarks').val() + "', dor: '" + document.getElementById('txtDOR').value + "', custodianid : '" + custodianid + "', ExpenseType : '" + ExpenseType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                swal("Successful", "Successfully Requested and Pending For Verification.\n\nMail not sent. Check you mail settings.", "success");
                                load_data();
                                load_data_pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#RequestList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                swal("Successful", "Successfully Requested  and Pending For Verification.", "success");
                                load_data();
                                load_data_pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#RequestList').modal('hide');
                                $('.btn').prop('disabled', false);
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
}

function load_data_pendingupload() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequest.aspx/PropertyRequestPendingUpload",
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
            GetCount();
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
                { "data": "RequestNumber" },
                //{ "data": "NoOfItemString" },
                { "data": "DateOfReturn" },
                { "data": "DateOfTransaction" },
                { "data": "Approver" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
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
            var requestnumber = currentRow.find("td:eq(0)").text();

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

                    var textremarks = $('#cancelrequest').val();

                    if (textremarks === "") {
                        swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                        return false
                    }
                    $.ajax({
                        type: "POST",
                        url: "Operation_Module/PropertyRequest.aspx/CancelPropertyRequest",
                        data: "{requestno: '" + requestnumber + "', remarks: '" + textremarks + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#pendinguploadlist').hide();
                            $('#pendinguploadlist').innerHTML = "";
                            $('#pendinguploadlist').fadeIn(200);
                            load_data();
                            load_data_pendingupload();
                            currentRow.remove();
                            swal("Cancelled", "Property Request Cancelled successfully", "success");
                        }
                    });
                } else {
                    //swal("Cancelled", "Property Request Cancelled successfully", "warning");
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

var requestno = '';
var isapproveD;
var ctr = 0;
var ctrckr = 0;

function ViewAssets(requestnumber) {

    //gheghe(Certificate/Word)
    //document.getElementById("printGridRequest").children[1].style.display = "none";

    requestno = requestnumber;
    document.getElementById('lblRequestNumber1').innerHTML = requestnumber;
    $('#btndownloadcreatepdf').attr('transno', requestnumber);
    $('#btndownloadrispdf').attr('transno', requestnumber);

    load_data_PUL2();
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyRequest.aspx/GenerateRequestReport",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divRequestReport').hide();
            $('#BodyHolder_divRequestReport').html(msg.d);
            $('#BodyHolder_divRequestReport').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyRequest.aspx/GenerateRequestReportRIS",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divRequestReportRIS').hide();
            $('#BodyHolder_divRequestReportRIS').html(msg.d);
            $('#BodyHolder_divRequestReportRIS').fadeIn(200);
        }
    });
    $('#RLPendingUpload').modal('show');

    //$.ajax({
    //    type: "POST",
    //    url: "Operation_Module/PropertyRequest.aspx/GenerateICS",
    //    data: "{requestnumber: '" + requestnumber + "',  requesteddate: '" + requesteddate + "',remarks: '" + remarks + "',requestorname: '" + requestorname + "', requestordepartment: '" + requestordepartment + "', requestorposition: '" + requestorposition + "', approvername : '" + approvername + "', custodian : '" + custodian + "', assignedcustodian: '" + assignedcustodian + "', dateofreturn: '" + dateofreturn + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        $('#BodyHolder_divICS').hide();
    //        $('#BodyHolder_divICS').html(msg.d);
    //        $('#BodyHolder_divICS').fadeIn(200);
    //    }
    //});
}

function load_data_PUL2() {
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyRequest.aspx/GetPLPendingUploadList",
        data: "{requestnumber: '" + requestno + "'}",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Request'
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

        $('#tblPUL2').off('click');
    } catch (e) {
        var table = $('#tblPUL2').DataTable();
        table
            .clear()
            .draw();
    }

}

