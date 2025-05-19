var url = '';

$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    SupplyAllocationApproval();
    GetsitePT();
    GetsiteToPT();
    GetEmployeePT();
    $('#BodyHolder_btnComplete').fadeIn();

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
    load_data_Pendingupload1();
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
        load_data_Pendingupload1();
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

var needsapproval = "False";
function SupplyAllocationApproval() {
    $.ajax({
        type: "POST",
        url: url + "/SupplyAllocation.aspx/SupplyAllocationApproval",
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

//Added by: Doddom
function load_data() {
    warehouseid = $('#ddlFromSitePT').val();
    var search = "";
    var custodian = $('#ddlCustodian').val();
    var tosite = $('#ddlToSitePT').val();

    $.ajax({
        type: 'POST',
        url: url + "/SupplyAllocation.aspx/GetList",
        data: "{ site: '" + warehouseid +  "' ,tosite : '" + tosite + "', search: '" + search + "', custodianid: '" + custodian + "'}", 
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

var BalanceQuantity = 0;
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" },
                { "data": "UnitOfMeasurement" },
                { "data": "Location" },
                { "data": "Classification" },
                { "data": "Custodian" },
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
                }
                ]
            }, colReorder: true
            , stateSave: true
        });

        //added by: domdom

        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            $('#txtIssueQuantity').val('');
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var itemcode = currentRow.find("td:eq(0)").text();
            var balancequantity = currentRow.find("td:eq(2)").text();
            var smlid = $(this).attr('smlid');

            var ToSite = $('#ddlToSitePT').val();
            var FromSite = $('#ddlFromSitePT').val();

            if (ToSite == FromSite) {
                swal("Warning", "Should not transfer to the same site", "warning");
            }
            else {
                if ($('#ddlToSitePT').val() == "0" || $('#ddlToSitePT').val() == null)
                    ToSite = 0;

                if (ToSite == 0) {
                    swal("Warning", "Select To Site", "warning");
                }
                else {
                    SMLID = smlid;
                    BalanceQuantity = balancequantity;
                    $('#txtItemCode').val(itemcode);
                    $('#addToListStock').modal('show');
                }
            }
        });


    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

//added by: Domdom
function IssueStockAsset() {
    var issuequantity = $('#txtIssueQuantity').val();
    var employee = $('#ddlEmployeeSI').val();
    var custodian = $('#ddlCustodian').val();
    var fromsite = $('#ddlFromSitePT').val();
    var tosite = $('#ddlToSitePT').val();

    //alert(issuequantity + " "+ BalanceQuantity);

    if (parseInt(issuequantity) > parseInt(BalanceQuantity)) {
        swal("Warning", "Issue Quantity should not be greater than the balance quantity.", "error");
    }
    else if (parseInt(issuequantity) == 0 || issuequantity == "") {
        swal("Warning", "Input Quantity.", "error");
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/SupplyAllocation.aspx/StockTransferToAllocation",
            data: "{smlid: '" + SMLID + "', fromsite: '" + fromsite + "', tosite: '" + tosite + "', quantity: '" + issuequantity + "', custodian : '" + custodian + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "1") {
                    $('#addToListStock').modal('hide');
                    GetCount();
                    BalanceQuantity = 0;
                }
                else {
                    $('#addToListStock').modal('hide');
                    GetCount();
                    BalanceQuantity = 0;
                    swal("Warning", "Issue Quantity should not be greater than the balance quantity.", "error");
                }
            }
        });
    }
}

//Added by: Domdom
var GetCounter = 0;
function GetCount() {
    //alert(GetCounter);
    warehouseid = $('#ddlFromSitePT').val();
    var towarehouse = $('#ddlToSitePT').val();
    var ToEmployeeID = $('#ddlTopersonPT').val();
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: "POST",
        url: url + "/SupplyAllocation.aspx/SupplyTransferGetCount",
        data: "{ fromsite: '" + warehouseid +  "', tosite: '" + towarehouse + "', custodian: '" + custodian + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text("View Suppy List for Allocation: " + msg.d);
            if (msg.d == 0) {
                //$('#transferItemList').modal('hide');
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

//Added by: Domdom
function transferItem() {
    GetCount();
    load_data();
    load_data_transferlist();
    if (GetCounter == 0) {
        //document.getElementById("btnAssetItemTransfer").disabled = true;
        //swal("Warning", "There is No Property for Transfer", "warning");

        document.getElementById("btnAssetItemTransfer").disabled = false;

        $('#transferItemList').modal('show');
    }
    else {
        //alert('bisaya2');

        document.getElementById("btnAssetItemTransfer").disabled = false;

        $('#transferItemList').modal('show');
    }
}

//added by: domdom
function load_data_transferlist() {
    warehouseid = $('#ddlFromSitePT').val();
    var towarehouse = $('#ddlToSitePT').val();
    var custodian = $('#ddlCustodian').val();


    $.ajax({
        type: 'POST',
        url: url + "/SupplyAllocation.aspx/GetToTransferAssetList",
        data: "{ fromsite: '" + warehouseid + "', tosite: '" + towarehouse + "', custodian: '" + custodian + "' }",
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "QuantityString" },
                { "data": "UnitOfMeasurement" },
                //{ "data": "Location" },
                { "data": "Classification" },
                { "data": "Custodian" },
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
            var smlid = $(this).attr('smlid');
            var tosite = $('#ddlToSitePT').val();
            var fromsite = $('#ddlFromSitePT').val();

            //alert(tosite + " " + fromsite);
            var propertyno = currentRow.find("td:eq(0)").text();
            tooffice = 0;
            //if (confirm("Remove this Property?") == false) { return false; }
            $.ajax({
                type: "POST",
                url: url + "/SupplyAllocation.aspx/RemoveToTransfer",
                data: "{smlid: '" + smlid + "'}",
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


//created by: Domdom
function RemoveAllToTransfer() {
    warehouseid = $('#ddlFromSitePT').val();
    var custodian = $('#ddlCustodian').val();
    var towarehouse = $('#ddlToSitePT').val();


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
                url: url + "/SupplyAllocation.aspx/RemoveAllToTransfer",
                data: "{ fromsite: '" + warehouseid + "', tosite: '" + towarehouse + "',custodian:'" + custodian + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //GetCounter == 0;
                    load_data();
                    GetCount();
                    $('#transferItemList').modal('hide');
                    //swal("Successful", "Property List cancelled successfully.", "success");
                    swal({
                        title: "Property List cancelled successfully.",
                        icon: "success",
                    }).then((value) => {
                        window.location.replace(url + "/SupplyAllocation.aspx");
                    });
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });

}

//Created by: domdom
function ConfirmPropertyTransfer1() {

    warehouseid = $('#ddlFromSitePT').val();
    $("#spin").fadeIn(100);
    $('.btn').prop('disabled', true);

    var custodian = $('#ddlCustodian').val();

    var towarehouse = $('#ddlToSitePT').val();

    var alertMsg = "";
    if (needsapproval == "True") {
        //alert('test1');
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
                    $(".se-pre-con").fadeIn();
                    $.ajax({
                        type: "POST",
                        url: url + "/SupplyAllocation.aspx/ConfirmPropertyTransfer",
                        data: "{ approverid : '" + $('#BodyHolder_ddlApprover').val() + "', purpose : '" + $('#BodyHolder_txtRemarks').val() + "', fromsite: '" + warehouseid + "',tosite: '" + towarehouse + "', custodianid : '" + custodian + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested for Transfer and Pending For Approval.\n\nMail not sent. Please check you mail settings.');
                                swal("Successful", "Successfully Requested for Transfer and Pending For Approval.\n\nMail not sent. Check you mail settings.", "success");
                                load_data();
                                load_data_Pendingupload1();
                                $("#btnCart span").text(" 0");
                                $('#transferItemList').modal('hide');
                                $("#spin").hide();
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested for Transfer and Pending For Approval');
                                swal("Successful", "Successfully Requested for Transfer and Pending For Approval.", "success");
                                load_data();
                                load_data_Pendingupload1();
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
        //alert('test2');
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
                        url: url + "/SupplyAllocation.aspx/ConfirmPropertyTransfer",
                        data: "{ approverid : '0', purpose : '" + $('#BodyHolder_txtRemarks').val() + "', fromsite: '" + warehouseid + "',tosite: '" + towarehouse + "', custodianid : '" + custodian + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                swal("Successful", "Successfully Requested for Transfer and Pending For Upload.\n\nMail not sent. Check you mail settings.", "success");
                                load_data();
                                load_data_Pendingupload1();
                                $("#btnCart span").text(" 0");
                                $('#transferItemList').modal('hide');
                                $("#spin").hide();
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                swal("Successful", "Successfully Requested for Transfer and Pending For Upload", "success");
                                load_data();
                                load_data_Pendingupload1();
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

function load_data_Pendingupload1() {
    var search = "";
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/SupplyAllocation.aspx/PropertyTransferPendingApproval",
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
                { "data": "StockAllocationNo" },
                { "data": "Description" },
                { "data": "FromSiteStr" },
                { "data": "ToSiteStr" },
                //{ "data": "StringNoOfItem" },
                { "data": "CreatedBy" },
                { "data": "TransactionDate" },
                { "data": "Status" },
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
function ViewAssets(transactionno, isapproved) {
    $('#BodyHolder_AFupload').val('');
    transsno = transactionno;
    $('#BodyHolder_txtSupplyAlloc').val(transactionno);

    //if (transactiontype == 2) {
    //    actionCol = false;
    //    document.getElementById("isTransfer").style.display = "none";
    //}
    //else {
    //    document.getElementById("isTransfer").style.display = "block";
    //    if (parics == '1') {
    //        document.getElementById("printGrid").children[0].style.display = "none";
    //        document.getElementById("printGrid").children[1].style.display = "inline";
    //        document.getElementById("printGrid").children[2].style.display = "inline";
    //        document.getElementById("printGrid").children[3].style.display = "none";
    //        document.getElementById("printGrid").children[4].style.display = "none";
    //        document.getElementById("printGrid").children[5].style.display = "none";
    //        document.getElementById("printGrid").children[6].style.display = "none";
    //        document.getElementById("printGrid").children[7].style.display = "inline";
    //        document.getElementById("printGrid").children[8].style.display = "inline";
    //    } else if (parics == '2' || parics == '3') {
    //        document.getElementById("printGrid").children[0].style.display = "inline";
    //        document.getElementById("printGrid").children[1].style.display = "none";
    //        document.getElementById("printGrid").children[2].style.display = "none";
    //        document.getElementById("printGrid").children[3].style.display = "inline";
    //        document.getElementById("printGrid").children[4].style.display = "inline";
    //        document.getElementById("printGrid").children[5].style.display = "inline";
    //        document.getElementById("printGrid").children[6].style.display = "inline";
    //        document.getElementById("printGrid").children[7].style.display = "none";
    //        document.getElementById("printGrid").children[8].style.display = "none";
    //    } else {
            document.getElementById("printGrid").children[0].style.display = "none";
            document.getElementById("printGrid").children[1].style.display = "none";
            document.getElementById("printGrid").children[2].style.display = "none";
            document.getElementById("printGrid").children[3].style.display = "none";
            document.getElementById("printGrid").children[4].style.display = "none";
            document.getElementById("printGrid").children[5].style.display = "none";
            document.getElementById("printGrid").children[6].style.display = "none";
            document.getElementById("printGrid").children[7].style.display = "none";
            document.getElementById("printGrid").children[8].style.display = "none";
        //}

        //gheghe(Certificate/Word)
        //document.getElementById("printGrid").children[3].style.display = "none";

    //    $.ajax({
    //        type: "POST",
    //        url: url + "/PropertyTransfer.aspx/GenerateTransferReportITR",
    //        data: "{transferno : '" + transactionno + "' }",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {
    //            $('#BodyHolder_divGatePass').hide();
    //            $('#BodyHolder_divGatePass').html(msg.d);
    //            $('#BodyHolder_divGatePass').fadeIn(200);
    //        }
    //    });

    //    $.ajax({
    //        type: "POST",
    //        url: url + "/PropertyTransfer.aspx/GenerateTransferReportPTR",
    //        data: "{transferno : '" + transactionno + "' }",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {
    //            $('#BodyHolder_divIssuanceReportPTR').hide();
    //            $('#BodyHolder_divIssuanceReportPTR').html(msg.d);
    //            $('#BodyHolder_divIssuanceReportPTR').fadeIn(200);
    //        }
    //    });
    //    $.ajax({
    //        type: "POST",
    //        async: false,
    //        url: url + "/PropertyTransfer.aspx/GenerateIssuanceReportPAR",
    //        data: "{transferno: '" + transactionno + "' }",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {
    //            console.log(msg.d);
    //            $('#BodyHolder_divIssuanceReportPAR').show();
    //            $('#BodyHolder_divIssuanceReportPAR').html(msg.d);
    //            $('#BodyHolder_divIssuanceReportPAR').fadeIn(200);
    //        }
    //    });
    //    $.ajax({
    //        type: "POST",
    //        async: false,
    //        url: url + "/PropertyTransfer.aspx/GenerateIssuanceReportICS",
    //        data: "{transferno: '" + transactionno + "' }",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {
    //            $('#BodyHolder_divIssuanceReportICS').show();
    //            $('#BodyHolder_divIssuanceReportICS').html(msg.d);
    //            $('#BodyHolder_divIssuanceReportICS').fadeIn(200);
    //        }
    //    });
    //}

    $('#btndownloaditrpdf').attr('transno', transactionno);

    $('#btndownloadcreatepdf').attr('transno', transactionno);
    var x = document.getElementById("showorhide");
    //if (counter == counterchecker) {
        if (isapproved == "True") {
            load_data_PUL1();
            $('#addToListStock').modal('show');
            x.style.display = "inline";
        }
        else {
            load_data_PUL1();
            $('#addToListStock').modal('show');
            x.style.display = "none";
        }
    //}
    //else {
        //load_data_PUL1();
        //$('#ALPendingApproval').modal('show');
        //x.style.display = "inline";
    //}

}

//Added by: domdom
function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/SupplyAllocation.aspx/PLPendingApproval",
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
                { "data": "StockAllocationNo" },
                { "data": "Description" },
                { "data": "ItemCode" },
                { "data": "FromSite" },
                { "data": "ToSite" },
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

//function CheckTransactionType(PMLID, PARICS) {
//    if (PARICS == 1) {

//        PrintGridDataPAR();
//    }
//    else // if (PARICS == 2)
//    {

//        PrintGridDataICS();
//    }
//}

//function PrintGridDataPAR1() {
//    var prtGrid = document.getElementById('<%=printablepagePAR.ClientID %>');
//    var prtwin = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=1,scrollbars=1,status=0,resizable=1');
//    prtwin.document.write(prtGrid.innerHTML);
//    prtwin.document.close();

//    prtwin.focus();
//    prtwin.print();
//    prtwin.close();
//}


//function load_data_PUL2() {
//    $.ajax({
//        type: 'POST',
//        url: url + "/SupplyAllocation.aspx/ALPendingApprovalV2",
//        data: "{transactionno: '" + transsno + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            $('#pul2').hide();
//            $('#pul2').innerHTML = "";
//            $('#pul2').fadeIn(200);

//            myJsonData = data;

//            populateDataTablePUL2(myJsonData);

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

//function populateDataTablePUL2(data) {
//    try {
//        $('#tblPUL2').DataTable({
//            "destroy": true,
//            fixedHeader: {
//                header: true,
//                footer: true,
//                headerOffset: 50
//            },
//            "processing": true,
//            "language": {
//                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
//            },
//            "scrollX": true,
//            "data": JSON.parse(data.d),
//            "columns": [
//                { "data": "AssetNumber" },
//                { "data": "Description" },
//                { "data": "Serial" },
//                { "data": "From" },
//                { "data": "To" },
//                { "data": "Status" }
//            ], dom: 'Brftip',
//            buttons: {
//                buttons: [
//                {
//                    extend: 'collection',
//                    text: '...',
//                    buttons: [
//                        {
//                            extend: 'copy',
//                            text: 'COPY',
//                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
//                        },
//                        {
//                            extend: 'excel',
//                            text: 'EXCEL',
//                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
//                        },
//                         {
//                             extend: 'csv',
//                             text: 'CSV',
//                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
//                         },
//                          {
//                              extend: 'print',
//                              text: 'PRINT',
//                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Transfer'
//                          },
//                           {
//                               extend: 'colvis',
//                               text: 'COLUMN VISIBILITY',
//                               className: 'btn btn-primary', exportOptions: { columns: ':visible' },
//                               collectionLayout: 'two-column'
//                           }
//                    ],
//                    dropup: false, //btn btn-sm btn-success
//                }
//                ]
//            }, colReorder: true
//            , stateSave: true
//        });

//        $('#tblPUL2').off('click');
//    } catch (e) {
//        var table = $('#tblPUL2').DataTable();
//        table
//            .clear()
//            .draw();
//    }
//}

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
                url: url + "/SupplyAllocation.aspx/CancelPropertyTransfer",
                data: "{transactionno: '" + transno + "', remarks: '" + textremarks + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload1();
                    //alert("Property Transfer cancelled successfully.");
                    swal("Successful", "Property Transfer cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

//function ShowTransfertype() {
//    var transfertype = $('#BodyHolder_ddlTrasferType').val();
//    if (transfertype == 1) {
//        $('#showorHide').show();
//        $('#SourceLocation').hide();
//        $('#sourcePerson').show();
//        $('#DestinationLocation').hide();
//        $('#DestinationPerson').show();
//        GetEmployeePT();
//    }
//    else if (transfertype == 0) {
//        $('#showorHide').hide();
//        $('#SourceLocation').hide();
//        $('#sourcePerson').hide();
//        $('#DestinationLocation').hide();
//        $('#DestinationPerson').hide();
//    }
//    else {
//        $('#showorHide').hide();
//        $('#SourceLocation').show();
//        $('#sourcePerson').hide();
//        $('#DestinationLocation').show();
//        $('#DestinationPerson').hide();
//    }
//}

function GetListPerPerson() {
    var userID = $('#ddlFromPersonPT').val();
    //alert(userID);
    load_data();
}

function UploadStockAllocation() {
    var alertMsg;
    var issuanceno = $('#BodyHolder_txtSupplyAlloc').val();
    var filess = $('#BodyHolder_FileUpload1').val();
    var filename = "";

    //alert(issuanceno);
    if (filess == "") {
        filename = "";
    }
    //if (filess == "") {
    //    swal("", "Select a file to upload.", "warning");
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
                    var fileUpload = $("#BodyHolder_AFupload").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + issuanceno + "|StockAllocation",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/SupplyAllocation.aspx/UploadSupplyAllocation",
                                    data: " { transactionno : '" + issuanceno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_Pendingupload1();
                                        $('#viewStockAllocationDetails').modal('hide');
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
   // }
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
        value: '1',
        text: 'Receiving Location',
        rowindex: -1
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



var loctolist = new Array();
function GetLocationToPT() {
    var context = window.location.pathname.substring(1);
    var ddlW = $('#ddlToLocationPT');
    ddlW.empty();
    //ddlW.append($('<option>', {
    //    value: '0',
    //    text: '--- Select Branch ---',
    //    rowindex: -1
    //}));
    ddlW.append($('<option>', {
        value: '1',
        text: 'Receiving Location',
        rowindex: -1
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


//new modal for pending upload ----------------------------------------------------------------------------------------------------------------------------------------------------------------

var stockallocationno = '';
var transactorName = '';
var transactorEmail = '';
function ViewStockAllocationDetails(stockallocationno, transactorname, createddate, email, status) {
    //alert('1');
    var x = document.getElementById("showorhide1");

    if (status == "Pending Upload") {
        x.style.display = "inline";
    }
    else {
        x.style.display = "none";
    }

    disposalnumber = stockallocationno;
    transactorName = transactorname;
    transactorEmail = email;
    document.getElementById('lblStockAllocationNo').innerHTML = stockallocationno;
    document.getElementById('lblStockAllocationBy').innerHTML = transactorname
    document.getElementById('lblStockDateAllocation').innerHTML = createddate;

    $('#BodyHolder_txtSupplyAlloc').val(stockallocationno);

    load_data_SALDetails();
    $('#viewStockAllocationDetails').modal('show');
}

function load_data_SALDetails() {
    //alert('2');
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetSALPendingUpload",
        data: "{disposalno: '" + disposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockallocationdetails').hide();
            $('#stockallocationdetails').innerHTML = "";
            $('#stockallocationdetails').fadeIn(200);

            myJsonData = data;

            populateDataSDLDetails(myJsonData);

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

function populateDataSDLDetails(data) {
    try {
        $('#tblStockAllocationDetails').DataTable({
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
                { "data": "StockAllocationNo" },
                { "data": "Remarks" },
                { "data": "FromSiteStr" },
                { "data": "ToSiteStr" },
                { "data": "Status" },
                { "data": "QuantityString" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
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

        $('#tblStockAllocationDetails').off('click');
    } catch (e) {
        var table = $('#tblStockAllocationDetails').DataTable();
        table
            .clear()
            .draw();
    }
}