$(document).ready(function () {
    $('.select2').select2();

    //added by: Domdom
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+1d',
        autoclose: true
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+1d',
        autoclose: true
    });
    $('#txtDateTo').datepicker('setDate', today);
    //----------------------------------------------------------------------

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

    //$('#rsmibtn').off('click').on('click', function () {
    //        $('#sigandCertModal').modal('show');
    //        $('#btndownloadpdf').attr('transno', propertyno);
    //        ClearSignature();
    //});



    //$('#rsmixcl').off('click').on('click', function (propertyno) {
    //    const now = new Date();

    //    const year = now.getFullYear();
    //    const month = (now.getMonth() + 1).toString().padStart(2, '0');
    //    const day = now.getDate().toString().padStart(2, '0');
    //    const hours = now.getHours().toString().padStart(2, '0');
    //    const minutes = now.getMinutes().toString().padStart(2, '0');
    //    const seconds = now.getSeconds().toString().padStart(2, '0');

    //    const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

    //    var context = window.location.pathname.substring(1);
    //    //alert(disposalnumberrequest);
    //    $.ajax({
    //        async: false,
    //        type: 'POST',
    //        url: context + '/ReportPage',
    //        data: "{ key1: '" + 'RSPI' + "', key2: '" + propertyno + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {

    //            var bytes = Base64ToBytes(msg.d);

    //            var blob = new Blob([bytes], { type: "application/octetstream" });

    //            //Check the Browser type and download the File.
    //            var isIE = false || !!document.documentMode;
    //            if (isIE) {
    //                window.navigator.msSaveBlob(blob, fileName);
    //            } else {
    //                var url = window.URL || window.webkitURL;
    //                link = url.createObjectURL(blob);
    //                var a = $("<a />");
    //                a.attr("download", "RSPI_" + formattedDateTime + ".xls");
    //                a.attr("href", link);
    //                $("body").append(a);
    //                a[0].click();
    //                $("body").remove(a);

    //            }
    //        },
    //        error: function (xhr, status, error) {
    //            console.log(xhr);
    //        }
    //    });
    //});

    $('#rsmibtn').off('click').on('click', function () {
        CreateCertificate();
    });

    $('#rsmixcl').off('click').on('click', function (propertyno) {
            
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
                data: "{ key1: '" + 'RSPI' + "', key2: '" + propertyno + "', key3: '" + '' + "', key4: '" + datefroM + "', key5: '" + datetO + "'}",
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
                        a.attr("download", "RSPI_" + formattedDateTime + ".xls");
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
        //transno

        var alertMsg = "Fill-out all the required field(s):\n";
        var hasError = false;

        var frompkipassword = $('#frompkipassword');
        var bypkipassword = $('#bypkipassword');

        if (frompkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Property Custodian)";
        }

        if (bypkipassword.val() == '') {
            hasError = true;
            alertMsg += "\nNo Pass Phrase (Posted by)";
        }

        if (fromimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Property Custodian)";
        }
        if (byimagebase64 == '') {
            hasError = true;
            alertMsg += "\nNo image selected. (Posted by)";
        }

        if (fromcertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Property Custodian)";
        }
        if (bycertbase64 == '') {
            hasError = true;
            alertMsg += "\nNo certificate selected. (Posted by)";
        }

        if (hasError) {
            SweetAlertWarning(alertMsg);
            return;
        }

        var fromdatestamp = $('#fromdatestamp');
        var bydatestamp = $('#bydatestamp');
        console.log("{ propertynumber: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',fromdatestamp :'" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }");
        $.ajax({
            type: "POST",
            url: "Views/RSPI.aspx/CreateRSPI",
            data: "{ propertynumber: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "',fromdatestamp :'" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
                        a.attr("download", "RSPI.pdf");
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

    $("#tblLedgerCardList").DataTable({
        "destroy": true,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
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

    AccessButton();
    load_data_Pendingupload();
    load_Filter();
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
var btnAccess = [];
function AccessButton() {
    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        async: false,
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            btnAccess = msg1.d.split('|');
        }
    });
}
function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Pendingupload();
        return false;
    }
}

var datefroM = "";
var datetO = "";

function load_data_Pendingupload() {
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val();
    $.ajax({
        type: 'POST',
        async: false,
        url: "Views/RSPI.aspx/GetList",
        data: "{ type: '2', AccessButton:'" + btnAccess[0] + "', datefrom:'" + datefrom + "', dateto:'" + dateto + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#ledgercardlist').hide();
            $('#ledgercardlist').innerHTML = "";
            $('#ledgercardlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUpload(myJsonData);
            datefroM = datefrom;
            datetO = dateto;

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            alert(e);
            console(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePendingUpload(data) {
    try {
        $('#tblLedgerCardList').DataTable({
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
                { "data": "PropertyNo" },
                { "data": "Quantity" },
                { "data": "Description" },
                //{ "data": "Action" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
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

        $('#tblLedgerCardList').off('click');

        $('#tblLedgerCardList').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblLedgerCardList').DataTable();
        table
            .clear()
            .draw();
    }
}

function CreateCertificate(propertyno) {
    $('#sigandCertModal').modal('show');
    $('#btndownloadpdf').attr('transno', propertyno);
    ClearSignature();
}

function ViewAssets(datefrom, dateto) {
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val(); $.ajax({
        type: "POST",
        async: false,
        url: "Views/RSPI.aspx/GenerateReport",
        //data: "{propertyno: '" + propertyno + "' }",
        data: "{datefrom:'" + datefrom + "', dateto:'" + dateto + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var divPropertyCard = $('#BodyHolder_divReport');
            divPropertyCard.html(msg.d);
            //$('#BodyHolder_divReport').hide();
            //$('#BodyHolder_divReport').html(msg.d);
            //$('#BodyHolder_divReport').fadeIn(200);
        }
    });
    PrintReport();
}

//function ViewExcel(propertyno)
//{
//    const now = new Date();

//    const year = now.getFullYear();
//    const month = (now.getMonth() + 1).toString().padStart(2, '0');
//    const day = now.getDate().toString().padStart(2, '0');
//    const hours = now.getHours().toString().padStart(2, '0');
//    const minutes = now.getMinutes().toString().padStart(2, '0');
//    const seconds = now.getSeconds().toString().padStart(2, '0');

//    const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

//    var context = window.location.pathname.substring(1);
//    //alert(disposalnumberrequest);
//    $.ajax({
//        async: false,
//        type: 'POST',
//        url: context + '/ReportPage',
//        data: "{ key1: '" + 'RSPI' + "', key2: '" + propertyno + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {

//            var bytes = Base64ToBytes(msg.d);

//            var blob = new Blob([bytes], { type: "application/octetstream" });

//            //Check the Browser type and download the File.
//            var isIE = false || !!document.documentMode;
//            if (isIE) {
//                window.navigator.msSaveBlob(blob, fileName);
//            } else {
//                var url = window.URL || window.webkitURL;
//                link = url.createObjectURL(blob);
//                var a = $("<a />");
//                a.attr("download", "RSPI_" + formattedDateTime + ".xls");
//                a.attr("href", link);
//                $("body").append(a);
//                a[0].click();
//                $("body").remove(a);

//            }
//        },
//        error: function (xhr, status, error) {
//            console.log(xhr);
//        }
//    });
//}

//filter
function load_Filter() {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    $.ajax({
        type: 'POST',
        url: "Views/" + loc + "/GetFilter",
        data: "{  ReportName: '" + "RSPI" + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#FilterList').hide();
            $('#FilterList').innerHTML = "";
            $('#FilterList').fadeIn(200);

            myJsonData = data;
            populateDataTableFilter(myJsonData);

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

function clear_filter() {
    $('#txtValue').val('');
    $('#drpLogic').val("Select Logic").trigger('change');
    $('#BodyHolder_drpFilterBy').val(" Select Filter ").trigger('change');
    $('#drpOperator').val("Select Operator").trigger('change');
    $('#btnAddFilter').val('Add Filter');
}

function populateDataTableFilter(data) {
    try {
        $('#tblFilterDisplay').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Logic" },
                { "data": "FilterBy" },
                { "data": "Operator" },
                { "data": "Values" },
                { "data": "IsActive" },
                { "data": "Action" }
            ],
            dom: 'Brftip',
            "columnDefs": [
                      {
                          "targets": [0, 1, 2, 3, 5],
                          "orderable": false
                      },
                      {
                          targets: 'active',
                          data: {
                              _: "active.display",
                              sort: "active.@data-order",
                              type: "active.@data-order"
                          }
                      }
            ],
            buttons: {
                buttons: [
                {
                    extend: 'collection',
                    text: '...',
                    buttons: [
                        {
                            extend: 'copy',
                            text: 'COPY',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RSPI'
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
        $('#tblFilterDisplay').off('click');
    } catch (e) {
        var table = $('#tblFilterDisplay').DataTable();
        table
            .clear()
            .draw();
    }
}

function GetFilterID(id, logic, filterby, operator, value) {
    LogicEdit = logic;
    FilterEdit = filterby;
    OperatorEdit = operator;
    ValueEdit = value;
    recIdEdit = id;

    $('#txtValue').val(value);
    $('#drpLogic').val(logic).trigger('change');
    $('#BodyHolder_drpFilterBy').val(filterby).trigger('change');
    $('#drpOperator').val(operator).trigger('change');
    $('#btnAddFilter').val('Save Changes');
}

function SaveFilter(ReportName) {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    var Logic = $('#drpLogic').val();
    var Filter = $('#BodyHolder_drpFilterBy').val();
    var Operator = $('#drpOperator').val();
    var Value = $('#txtValue').val();
    var alertMsg = "";
    if (Logic == 'Select Logic' || Filter == ' Select Filter ' || Operator == 'Select Operator') {
        if (Logic == 'Select Logic')
            alertMsg += '\nLogic';
        if (Filter == ' Select Filter ')
            alertMsg += '\nFilter';
        if (Operator == 'Select Operator')
            alertMsg += '\nOperator';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this filter?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                if ($('#btnAddFilter').val() == 'Add Filter') {
                    $.ajax({
                        type: "POST",
                        url: "Views/" + loc + "/SaveFilter",
                        data: " { ReportName : '" + ReportName + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            load_Filter();
                            clear_filter();
                            load_data_Pendingupload();
                        }
                    });
                }
                else {
                    if (Logic == LogicEdit && Filter == FilterEdit && Operator == OperatorEdit && Value == ValueEdit) {
                        clear_filter();
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "Views/" + loc + "/UpdateFilter",
                            data: " { RecId : '" + recIdEdit + "' ,ReportName : '" + ReportName + "' , Logic : '" + Logic + "', Filter :'" + Filter + "', Operator:'" + Operator + "', Value:'" + Value + "'} ",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                load_Filter();
                                clear_filter();
                                load_data_Pendingupload();
                            }
                        });
                    }
                }
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function GetActive(id) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to change the Active status?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            var currentLocation = window.location;
            var loc = currentLocation.pathname.split('/')[2];

            $.ajax({
                type: "POST",
                url: "Views/" + loc + "/GetActive",
                data: " { id : '" + id + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_Filter();
                    load_data_Pendingupload();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteFilter(id) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to delete this filter?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            var currentLocation = window.location;
            var loc = currentLocation.pathname.split('/')[2];

            $.ajax({
                type: "POST",
                url: "Views/" + loc + "/DeleteFilter",
                data: " { id : '" + id + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_Filter();
                    clear_filter();
                    load_data_Pendingupload();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}