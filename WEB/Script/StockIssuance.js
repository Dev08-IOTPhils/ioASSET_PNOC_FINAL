var tblSPU;
var url = '';

$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    departmentSI();
    IfIssuanceNeedsApproval();
    GetEmployeeSI();
    //$("#tblStockList").DataTable({
    //    "scrollX": true
    //    , "searching": false
    //});
    tblSPU = $("#tblPendingUpload").DataTable({
        "scrollX": true
        //, "searching": false
        , "lengthChange": false,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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

    $("#tblRequestPendingUpload").DataTable({
        "scrollX": true
        //, "searching": false
        , "lengthChange": false,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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
            data: "{ key1: '" + 'RIS' + "', key2: '" + stockissueno + "', key3: '" + 'SI' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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

    //$("#tblRequestList").DataTable({
    //    "scrollX": true
    //});

    $('#ddlCustodian').on('change', function () {
        load_data();
    });
    GetUserCcustodian();
    //load_data();
    load_data_Pendingupload();
    load_data_PendinguploadRequest();

    //$("#tblStockList").DataTable().clear().draw();
    //$("#tblPendingUpload").DataTable().clear().draw();
    //$("#tblRequestPendingUpload").DataTable().clear().draw();
    //$("#tblRequestList").DataTable().clear().draw();

    $('#BodyHolder_btnComplete').fadeIn();
    //$('#BodyHolder_btnUpload').hide();
    //$('#BodyHolder_fuDocuments').hide();

    $('#BodyHolder_btnCompleteRequest').fadeIn();
    //$('#BodyHolder_btnUploadRequest').hide();
    //$('#BodyHolder_fuDocumentsRequest').hide();

    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_btnUpload').fadeIn();
            $('#BodyHolder_fuDocuments').fadeIn();

            $('#BodyHolder_btnCompleteRequest').hide();
            $('#BodyHolder_btnUploadRequest').fadeIn();
            $('#BodyHolder_fuDocumentsRequest').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_btnUpload').hide();
            $('#BodyHolder_fuDocuments').hide();

            $('#BodyHolder_btnCompleteRequest').fadeIn();
            $('#BodyHolder_btnUploadRequest').hide();
            $('#BodyHolder_fuDocumentsRequest').hide();
        }
    });

    //$('#risbtn').off('click').on('click', function () {
    //    $('#RISsigandCertModal').modal('show');
    //});
    //$('#itrbtn').off('click').on('click', function () {
    //    $('#ITRsigandCertModal').modal('show');
    //});

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
    $('#risbtnword').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divStockIssuanceReportRIS').html();
        Export2WordV2(htmlstring, 'Requisition and Issue Slip Form');
    });
    $('#itrbtnword').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divStockIssuanceReportITR').html();
        Export2WordV2(htmlstring, 'Inventory Transfer Report Form');
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


        if (transtype == 'Issuance') {
            $.ajax({
                type: "POST",
                url: url + "/StockIssuance.aspx/CreateRIS",
                data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "', approveddatestamp: '" + approveddatestamp.prop('checked') + "', issueddatestamp: '" + issueddatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Stock_Issuance.pdf");
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
        } else {
            $.ajax({
                type: "POST",
                url: url + "/StockIssuance.aspx/CreateRISRequest",
                data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "', approveddatestamp: '" + approveddatestamp.prop('checked') + "', issueddatestamp: '" + issueddatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Issuance.pdf");
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

    //itr
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
            SweetAlertWarning(alertMsg);
            return;
        }

        var itrapproveddatestamp = $('#itrapproveddatestamp');
        var itrreleaseddatestamp = $('#itrreleaseddatestamp');
        var itrreceiveddatestamp = $('#itrreceiveddatestamp');

        $.ajax({
            type: "POST",
            url: url + "/StockIssuance.aspx/CreateITR",
            data: "{ issuanceno: '" + transno + "',  itrapprovedpkipassword: '" + itrapprovedpkipassword.val() + "', itrreleasedpkipassword: '" + itrreleasedpkipassword.val() + "', itrreceivedpkipassword: '" + itrreceivedpkipassword.val() + "', itrapprovedimagebase64: '" + itrapprovedimagebase64 + "', itrreleaseimagebase64: '" + itrreleaseimagebase64 + "', itrreceivedimagebase64: '" + itrreceivedimagebase64 + "', itrapprovedcertbase64: '" + itrapprovedcertbase64 + "', itrreleasecertbase64: '" + itrreleasecertbase64 + "', itrreceivedcertbase64: '" + itrreceivedcertbase64 + "', itrapproveddatestamp: '" + itrapproveddatestamp.prop('checked') + "',itrreleaseddatestamp: '" + itrreleaseddatestamp.prop('checked') + "', itrreceiveddatestamp: '" + itrreceiveddatestamp.prop('checked') + "' }",
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
                        a.attr("download", "Property_Issuance.pdf");
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

    //$('#btnDownload').off('click').on('click', function () {
    //    $('#RISsigandCertModal').modal('show');
    //});
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

var needsapproval = "False";
function IfIssuanceNeedsApproval() {
    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/IfIssuanceNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            //var x = document.getElementById("withorwithoutyou");
            //var a = document.getElementById("withorwithoutyou1");
            var y = document.getElementById("labelApprover");
            var z = document.getElementById("dropdownApprover");
            if (msg.d == "True") {
                //x.style.visibility = 'visible';
                //a.style.visibility = 'hidden';
                //y.style.visibility = 'visible';
                //z.style.visibility = 'visible';
                //x.style.display = 'inline';
                //a.style.display = 'none';
                y.style.display = 'inline';
                z.style.display = 'inline';
            }
            else {
                //x.style.display = 'none';
                //a.style.display = 'inline';
                y.style.display = 'none';
                z.style.display = 'none';
            }
        }
    });
}

function GetApprover() {
    var employee = $('#ddlEmployeeSI').val();
    //var custodian = $('#ddlCustodian').val();
    //alert(custodian);
    if ($('#ddlEmployeeSI').val() == "0")
        employee = 0;

    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/GetApprover",
        data: " { issuedto : '" + employee + "'} ",
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

var DepartmentArraySI = new Array();
function departmentSI() {
    var context = window.location.pathname.substring(1);
    var ddlDept = $('#ddlDepartmentSI');
    ddlDept.empty();
    ddlDept.append($('<option>', {
        value: '0',
        text: '--- Select User Department ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetDepartmentSI',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DepartmentArraySI = JSON.parse(xml.d);
                DepartmentArraySI.forEach((dep, index) => {
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


function GetEmployeeSI() {
    var department = $('#ddlDepartmentSI').val();
    if ($('#ddlDepartmentSI').val() == "0")
        department = 0;
    //alert(department);
    if (department == 0) {
        $.ajax({
            type: "POST",
            url: url + "/StockIssuance.aspx/GetUserNoDepartment",
            data: " { } ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ddlEmployee = $("[id*=ddlEmployeeSI]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                $.each(msg.d, function () {
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmployeeSI]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/StockIssuance.aspx/GetUser",
            data: " { department : '" + department + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (department == 0) {
                    var ddlEmployee = $("[id*=ddlEmployeeSI]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                }
                else {
                    var ddlEmployee = $("[id*=ddlEmployeeSI]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    $.each(msg.d, function () {
                        ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmployeeSI]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
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
        url: url + "/StockIssuance.aspx/GetUserCustodian",
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
        event.preventDefault();
        return false;
    }
}

function EnterKey1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        return false;
    }
}

function EnterKey2(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Pendingupload();
        return false;
    }
}

function EnterKey3(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_PendinguploadRequest();
        return false;
    }
}

var SMLID = '';
var BalanceQuantity = 0;

function load_data() {
    var search = "";
    var custodian = $('#ddlCustodian').val().trim();
    $.ajax({
        type: 'POST',
        url: url + "/StockIssuance.aspx/GetList",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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
            $('#txtIssueQuantity').val('');
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var itemcode = currentRow.find("td:eq(0)").text();
            var balancequantity = currentRow.find("td:eq(1)").text();
            var smlid = $(this).attr('smlid');

            var employee = $('#ddlEmployeeSI').val();
            if ($('#ddlEmployeeSI').val() == "0" || $('#ddlEmployeeSI').val() == null)
                employee = 0;

            if (employee == 0) {
                swal("Warning", "Select Employee", "warning");
            }
            else {
                SMLID = smlid;
                BalanceQuantity = balancequantity;
                $('#txtItemCode').val(itemcode);
                $('#addToListStock').modal('show');
            }
        });
    } catch (e) {
        var table = $('#tblStockList').DataTable();
        table
            .clear()
            .draw();
    }
}

//function GetEmployee() {
//    var department = $('#BodyHolder_ddlDepartment').val();
//    if ($('#BodyHolder_ddlDepartment').val() == "--- Select Department ---")
//        department = 0;
//    $.ajax({
//        type: "POST",
//        url: url + "/StockIssuance.aspx/GetUser",
//        data: " { department : '" + department + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg) {
//            var ddlEmployee = $("[id*=ddlEmployee]");
//            ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
//            $.each(msg.d, function () {
//                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
//            });
//        }
//    });
//}

GetCounter = 0;
function GetCount() {
    var employee = $('#ddlEmployeeSI').val();
    var custodian = $('#ddlCustodian').val();
    if ($('#ddlEmployeeSI').val() == "0" || $('#ddlEmployeeSI').val() == null)
        employee = 0;

    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/StockIssuanceGetCount",
        data: "{ employeeid: '" + employee + "', custodian : '" + custodian + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Stock List for Issuance: " + msg.d);
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

function IssueStockAsset() {
    var issuequantity = $('#txtIssueQuantity').val();
    var employee = $('#ddlEmployeeSI').val();
    var custodian = $('#ddlCustodian').val();
    if (parseInt(issuequantity) > parseInt(BalanceQuantity)) {
        swal("Warning", "Issue Quantity should not be greater than the balance quantity.", "error");
    }
    else if (parseInt(issuequantity) == 0 || issuequantity == "") {
        swal("Warning", "Input Issue Quantity.", "error");
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/StockIssuance.aspx/StockTransferToIssuance",
            data: "{smlid: '" + SMLID + "', employeeid: '" + employee + "', quantity: '" + issuequantity + "', custodian : '" + custodian + "' }",
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

function RemoveAllToIssuance() {
    var employee = $('#ddlEmployeeSI').val();
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
                url: url + "/StockIssuance.aspx/StockRemoveAllToIssuance",
                data: "{employeeid: '" + employee + "', custodian: '" + custodian + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_issuancelist();
                    $('#IssuanceList').modal('hide');
                }
            });
        } else {
            swal("Canceled", "Cancelled by user", "warning");
        }
    });
}

function IssuanceList() {
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val(0).trigger('change');
    var employee = $('#ddlEmployeeSI').val();
    if ($('#ddlEmployeeSI').val() == "0" || $('#ddlEmployeeSI').val() == null)
        employee = 0;
    if (employee == 0) {
        swal("Warning", "Select Employee", "warning");
    }
    else {
        load_data();
        load_data_issuancelist();
        $('#IssuanceList').modal('show');

        if (GetCounter == 0) {
            document.getElementById("btnAssetItemTransfer").disabled = true;
        }
        else {
            document.getElementById("btnAssetItemTransfer").disabled = false;
        }
    }
}

function ConfirmStockIssuance() {
    var employee = $('#ddlEmployeeSI').val();
    var custodian = $('#ddlCustodian').val();
    var alertMsg;
    if (needsapproval == "False") {
        if ($('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = '';
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
                        url: url + "/StockIssuance.aspx/ConfirmStockIssuance",
                        data: "{ employeeid : '" + employee + "',custodian: '" + custodian + "', approverid : '0',remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Issued and Pending For Upload.\n\nMail not sent. Please check you mail settings at Miscellaneous.');
                                swal("Successful", "Successfully added for Issuance and Pending For Approval/Upload.\n\nMail not sent. Check you mail settings at Miscellaneous.", "success");
                                load_data();
                                load_data_Pendingupload();
                                $("#btnCart span").text(" 0");
                                $('#IssuanceList').modal('hide');
                            }
                            else {
                                //alert('Successfully Issued and Pending For Upload');
                                swal("Successful", "Successfully added for Issuance and Pending For Approval/Upload.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount();
                                $('#IssuanceList').modal('hide');
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
        if ($('#BodyHolder_txtRemarks').val().trim() == "" || $('#BodyHolder_ddlApprover').val() == '0') {
            alertMsg = '';
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlApprover').val() == '0')
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
                        url: url + "/StockIssuance.aspx/ConfirmStockIssuance",
                        data: "{ employeeid : '" + employee + "',custodian: '" + custodian + "', approverid : '" + $('#BodyHolder_ddlApprover').val() + "' ,remarks : '" + $('#BodyHolder_txtRemarks').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Issued and Pending For Upload.\n\nMail not sent. Please check you mail settings at Miscellaneous.');
                                swal("Successful", "Successfully added for Issuance  and Pending For Approval/Upload.\n\nMail not sent. Check you mail settings at Miscellaneous.", "success");
                                load_data();
                                load_data_Pendingupload();
                                //$("#btnCart span").text(" 0");
                                GetCount();
                                $('#IssuanceList').modal('hide');
                            }
                            else {
                                //alert('Successfully Issued and Pending For Upload');
                                swal("Successful", "Successfully added for Issuance and Pending For Approval/Upload.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount();
                                $('#IssuanceList').modal('hide');
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

function load_data_issuancelist() {
    var employee = $('#ddlEmployeeSI').val();
    var custodian = $('#ddlCustodian').val();
    if ($('#ddlEmployeeSI').val() == "--- Select Employee ---" || $('#ddlEmployeeSI').val() == null)
        employee = 0;
    if (employee == 0) {
        swal("Warning", "Select Employee", "warning");
    }
    else {
        $.ajax({
            type: 'POST',
            url: url + "/StockIssuance.aspx/GetStockIssuanceList",
            data: "{ employeeid: '" + employee + "',custodian: '" + custodian + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#issuancelist').hide();
                $('#issuancelist').innerHTML = "";
                $('#issuancelist').fadeIn(200);

                myJsonData = data;
                populateDataTableForIssuance(myJsonData);

                console.log(data);
                console.log('Success');
                GetCount();
            },
            error: function (e) {
                alert(e);
                console.log("There was an error with your request...");
                console.log("error: " + JSON.stringify(e));
                window.location.replace('../Login.aspx');
            }
        });
    }
}

function populateDataTableForIssuance(data) {
    try {
        $('#tblIssuanceList').DataTable({
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
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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

        $('#tblIssuanceList').off('click');

        $('#tblIssuanceList').on('click', 'a.btn_Remove', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var itemcode = currentRow.find("td:eq(0)").text();
            var smlid = $(this).attr('smlid');
            var employee = $('#ddlEmployeeSI').val();
            $.ajax({
                type: "POST",
                url: url + "/StockIssuance.aspx/StockRemoveToIssuance",
                data: "{smlid: '" + smlid + "', employeeid: '" + employee + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    currentRow.remove();
                    GetCount();
                }
            });
        });
    } catch (e) {
        var table = $('#tblIssuanceList').DataTable();
        table
            .clear()
            .draw();
    }
}

function load_data_Pendingupload() {
    var search = "";
    $.ajax({
        type: 'POST',
        url: url + "/StockIssuance.aspx/StockIssuancePendingUpload",
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
                { "data": "IssuanceNo" },
                { "data": "IssuedTo" },
                { "data": "Location" },
                { "data": "Remarks" },
                //{ "data": "NoOfItem" },
                { "data": "TransactionDate" },
                { "data": "Approver" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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
var stockissueno = '';
function ViewStocks(stockissuanceno, isapproved) {

    //gheghe(Certificate/Word)
    document.getElementById("printGrid").children[0].style.display = "none";
    document.getElementById("printGrid").children[1].style.display = "none";
    //document.getElementById("printGrid").children[2].style.display = "none";
    //document.getElementById("printGrid").children[3].style.display = "none";
    //document.getElementById("printGrid").children[4].style.display = "none";
    //document.getElementById("printGrid").children[5].style.display = "none";

    $('#BodyHolder_fuDocuments').val('');
    document.getElementById('lblStockIssuanceNo').innerHTML = stockissuanceno;
    //document.getElementById('lblStockIssuedBy').innerHTML = transactorname;
    //document.getElementById('lblStockAccountablePerson').innerHTML = employeename;
    //document.getElementById('lblStockDateIssued').innerHTML = createddate;
    $('#BodyHolder_txtStockIssuanceNo').val(stockissuanceno);
    stockissueno = stockissuanceno;
    $('#btndownloadcreatepdf').attr('transno', stockissuanceno);

    $('#btndownloadrispdf').attr('transno', stockissuanceno);
    $('#btndownloadrispdf').attr('transtype', 'Issuance');
    $('#btndownloaditrpdf').attr('transno', stockissuanceno);
    var x = document.getElementById("showorhide");
    if (isapproved == "True") {
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
        url: url + "/StockIssuance.aspx/GenerateStockIssuanceReport",
        data: "{issuanceno: '" + stockissueno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockIssuanceReport').hide();
            $('#BodyHolder_divStockIssuanceReport').html(msg.d);
            $('#BodyHolder_divStockIssuanceReport').fadeIn(200);
        },
        error: function (xml) {
            window.location.replace('../Login.aspx');
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/GenerateStockIssuanceReportRIS",
        data: "{issuanceno: '" + stockissueno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockIssuanceReportRIS').hide();
            $('#BodyHolder_divStockIssuanceReportRIS').html(msg.d);
            $('#BodyHolder_divStockIssuanceReportRIS').fadeIn(200);
        },
        error: function (xml) {
            window.location.replace('../Login.aspx');
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/GenerateStockIssuanceReportITR",
        data: "{issuanceno: '" + stockissueno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divStockIssuanceReportITR').hide();
            $('#BodyHolder_divStockIssuanceReportITR').html(msg.d);
            $('#BodyHolder_divStockIssuanceReportITR').fadeIn(200);
        },
        error: function (xml) {
            window.location.replace('../Login.aspx');
        }
    });
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/StockIssuance.aspx/StockDetailsPendingUpload",
        data: "{stockissuanceno: '" + stockissueno + "'}",
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

    $('#itrapprovedsignatureupload').val('');
    $('#itrreleasedsignatureupload').val('');
    $('#itrreceivedsignatureupload').val('');

    $('#itrapprovedpkicertificateupload').val('');
    $('#itrreleasedpkicertificateupload').val('');
    $('#itrreceivedpkicertificateupload').val('');

    itrapprovedimagebase64 = "";
    itrreleaseimagebase64 = "";
    itrreceivedimagebase64 = "";

    itrapprovedcertbase64 = "";
    itrreleasecertbase64 = "";
    itrreceivedcertbase64 = "";
}

function SweetAlertSuccess(e) { swal({ icon: "success", text: e }) } function SweetAlertWarning(e) { swal({ icon: "warning", text: e }) } function SweetAlertInfo(e) { swal({ icon: "info", text: e }) } function SweetAlertError(e) { swal({ icon: "error", text: e }) }

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
            //"dom": 'Bfrtip',
            //buttons: [
            //'print'
            //],
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                { "data": "QuantityString" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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

function CancelStockIssuance(stockissuanceno) {
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
    textareatt.id = "cancelissuance";

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

            var textremarks = $('#cancelissuance').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: url + "/StockIssuance.aspx/CancelStockIssuance",
                data: "{issuanceno: '" + stockissuanceno + "', remarks: '" + textremarks + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    //alert("Stock Issuance cancelled successfully.");
                    swal("Successful", "Supply Issuance cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

//stock request

function load_data_PendinguploadRequest() {
    var search = "";

    $.ajax({
        type: 'POST',
        url: url + "/StockIssuance.aspx/StockRequestPendingUpload",
        data: "{ search : '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendinguploadrequestlist').hide();
            $('#pendinguploadrequestlist').innerHTML = "";
            $('#pendinguploadrequestlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadRequest(myJsonData);

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

function populateDataTablePendingUploadRequest(data) {
    try {
        $('#tblRequestPendingUpload').DataTable({
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
                { "data": "Location" },
                { "data": "Remarks" },
                //{ "data": "NoOfItem" },
                { "data": "TransactionDate" },
                { "data": "Approver" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Issuance'
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

        $('#tblRequestPendingUpload').off('click');

        $('#tblRequestPendingUpload').on('click', 'button.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblRequestPendingUpload').on('click', 'button.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblRequestPendingUpload').DataTable();
        table
            .clear()
            .draw();
    }
}
var approverEmail = '';
var approverName = '';
var requestno = '';
function ViewStocksRequest(stockrequestno, isapproved, status, apName, apEmail, qsiid) {
    approverName = apName;
    approverEmail = apEmail;
    $('#BodyHolder_fuDocumentsRequest').val('');
    var x = document.getElementById("showorhide2");
    if (status == "Pending Quantity Issuance") {
        x.style.display = "inline";
    }
    else {
        x.style.display = "none";
    }
    document.getElementById('lblStockRequestNo').innerHTML = stockrequestno;
    //document.getElementById('lblStockIssuedBy').innerHTML = transactorname;
    //document.getElementById('lblStockAccountablePerson').innerHTML = employeename;
    //document.getElementById('lblStockDateIssued').innerHTML = createddate;

    $('#btndownloadcreatepdf').attr('transno', stockrequestno);

    $('#BodyHolder_txtStockRequestNo').val(stockrequestno);
    requestno = stockrequestno;
    $('#btndownloadrispdf').attr('transno', stockrequestno);
    $('#btndownloadrispdf').attr('transtype', 'Request');

    var x = document.getElementById("showorhide1");
    if (isapproved == "True") {
        load_data_PURL(isapproved, status);
        $('#StockRequestDetailsPendingUpload').modal('show');
        x.style.display = "inline";
        
    }
    else {
        load_data_PURL(isapproved, status);
        $('#StockRequestDetailsPendingUpload').modal('show');
        x.style.display = "none";
        
    }

    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/GenerateStockRequestReport",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //  alert(msg.d);
            $('#BodyHolder_divStockRequestReport').hide();
            $('#BodyHolder_divStockRequestReport').html(msg.d);
            $('#BodyHolder_divStockRequestReport').fadeIn(200);
        },
        error: function (xml) {
            //window.location.replace('../Login.aspx');
        }
    });
    
    //document.getElementById('qsi0').disabled = true;

}

function CompleteRequestStock() {
    //var quantity = $("#qsi" + qsiid).val();
    
    //alert(quantity);
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
        $(".se-pre-con").fadeIn("slow");
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: url + "/StockIssuance.aspx/CompleteRequestStock",
                data: " { requestno : '" + requestno + "', ApproverName : '" + approverName + "', ApproverEmail : '" + approverEmail + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "Success") {
                        load_data_PendinguploadRequest();
                        $(".se-pre-con").fadeOut("slow");
                        $('#StockRequestDetailsPendingUpload').modal('hide');
                        swal("Successful", "Supply Request Transaction Successfull!", "success");
                    }
                    else if (msg.d == "sorry") {
                        $(".se-pre-con").fadeOut("slow");
                        swal("Successful", "Supply Request Transaction Successfull!\n\nMail not sent.", "success");
                        load_data_PendinguploadRequest();
                        $('#StockRequestDetailsPendingUpload').modal('hide');
                    }
                    else {
                        swal("Error", "Supply Request Transaction cannot be completed.", "error");
                    }
                }
            });
        } else {
            $(".se-pre-con").fadeOut("slow");
            swal("Cancelled", "Cancelled by user", "warning");
        }

    });
}

function load_data_PURL(isapproved, status) {
    $.ajax({
        type: 'POST',
        url: url + "/StockIssuance.aspx/StockRequestDetailsPendingUpload",
        data: "{requestno: '" + requestno + "', approved: '" + isapproved + "', Stat: '" + status + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#purl').hide();
            $('#purl').innerHTML = "";
            $('#purl').fadeIn(200);

            myJsonData = data;

            populateDataTablePURL(myJsonData);

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

function populateDataTablePURL(data) {
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                { "data": "QuantityString" },
                { "data": "QuantityStringIssue" }
            ]
            , dom: 'rtip'
        });

        $('#tblPURL').off('click');
    } catch (e) {
        var table = $('#tblPURL').DataTable();
        table
            .clear()
            .draw();
    }
}

function updateissuequantity(recid, qsiid) {
    var quantity = $("#qsi" + qsiid).val();
    if (quantity == 0) {
        quantity = 1;
        $("#qsi" + qsiid).val("1");
    }
    $.ajax({
        type: "POST",
        url: url + "/StockIssuance.aspx/UpdateQuantityIssue",
        data: "{recid : '" + recid + "',issuequantity: '" + quantity + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}

function UploadStockIssuance() {
    var alertMsg;
    var issuanceno = $('#BodyHolder_txtStockIssuanceNo').val();
    var filess = $('#BodyHolder_fuDocuments').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
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
                    var fileUpload = $("#BodyHolder_fuDocuments").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + issuanceno + "|StockIssuance",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/StockIssuance.aspx/UploadStockIssuance",
                                    data: " { issuanceno : '" + issuanceno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        //alert(msg.d);
                                        if (msg.d == "Success") {
                                            load_data_Pendingupload();
                                            $('#StockDetailsPendingUpload').modal('hide');
                                            swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                        }
                                        else {
                                            swal("Error", "You are requesting beyond the maximum quantity of item code", "error");
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
    }
}

function UploadIssuanceRequest() {
    var alertMsg;
    var requestno = $('#BodyHolder_txtStockRequestNo').val();
    var filess = $('#BodyHolder_fuDocumentsRequest').val();
    var filename = "";
    if (filess == "") {
        filename = "";
    }
    if (filess == "") {
        swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
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
                    var fileUpload = $("#BodyHolder_fuDocumentsRequest").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + requestno + "|StockRequest",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/StockIssuance.aspx/UploadStockRequest",
                                    data: " { requestno : '" + requestno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        if (msg.d == "Success") {
                                            load_data_PendinguploadRequest();
                                            $('#StockRequestDetailsPendingUpload').modal('hide');
                                            swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                        }
                                        else {
                                            swal("Error", "Supply Issuance Transaction cannot be completed.", "error");
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
                url: url + "/StockIssuance.aspx/CancelStockRequest",
                data: "{requestno: '" + stockrequestno + "', remarks: '" + textremarks + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data_Pendingupload();
                    load_data_PendinguploadRequest();
                    swal("Successful", "Supply Request cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}