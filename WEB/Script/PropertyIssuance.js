var url = '';

$(document).ready(function () {
    $('.select2').select2();
    departmentPI();
    IfIssuanceNeedsApproval();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    //alert(needsapproval);
    $("#tblAssetList").DataTable({
        "scrollX": true
        //, "searching": false,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                      },
                       {
                           extend: 'colvis',
                           text: 'COLUMN VISIBILITY',
                           className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                       }
                ],
                dropup: false, //btn btn-sm btn-successViewAssets
            }
            ]
        }, colReorder: true
, stateSave: true
    });
    $("#tblAssignedPropertyList").DataTable({
        "scrollX": true
        //, "searching": false,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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

    $('#parIxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'PAR' + "', key2: '" + issuancenumber + "', key3: '" + 'PIssuance' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "PAR_" + formattedDateTime + ".xlsx");
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
    $('#parRxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'PAR' + "', key2: '" + requestno + "', key3: '" + 'PRequest' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "PAR_" + formattedDateTime + ".xlsx");
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
            data: "{ key1: '" + 'RIS' + "', key2: '" + issuancenumber + "', key3: '" + 'PI' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RIS_" + formattedDateTime + ".xlsx");
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
                    a.attr("download", "RIS_" + formattedDateTime + ".xlsx");
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
            data: "{ key1: '" + 'ICS' + "', key2: '" + issuancenumber + "', key3: '" + 'PIssuance' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "ICS_" + formattedDateTime + ".xlsx");
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
    $('#icsxclrequest').off('click').on('click', function () {
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
            data: "{ key1: '" + 'ICS' + "', key2: '" + requestno + "', key3: '" + 'PRequest' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "ICS_" + formattedDateTime + ".xlsx");
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

    $('#parword').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportPAR').html();
        Export2WordV2(htmlstring, 'Property Acknowledgement Receipt Form');
    });
    $('#icsword').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportICS').html();
        Export2WordV2(htmlstring, 'Inventory Custodian Slip Form');
    });
    $('#itrword').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportITR').html();
        Export2WordV2(htmlstring, 'Inventory Transfer Report Form');
    });
    $('#risword').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportRIS').html();
        Export2WordV2(htmlstring, 'Requisition and Issue Slip Form');
    });
    $('#parwordrequest').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportPARRequest').html();
        Export2WordV2(htmlstring, 'Property Acknowledgement Receipt Form');
    });
    $('#icswordrequest').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportICSRequest').html();
        Export2WordV2(htmlstring, 'Inventory Custodian Slip Form');
    });
    $('#itrwordrequest').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportITRRequest').html();
        Export2WordV2(htmlstring, 'Inventory Transfer Report Form');
    });
    $('#riswordrequest').off('click').on('click', function () {
        var htmlstring = $('#BodyHolder_divIssuanceReportRISRequest').html();
        Export2WordV2(htmlstring, 'Requisition and Issue Slip Form');
    });

    $("#tblPendingUploadRequest").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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

    GetEmployeePI();
    GetUserCcustodian();
    $('#ddlCustodian').on('change', function () {
        load_data();
        load_data2();
    });
    $('#BodyHolder_ddlItem').on('change', function () {
        load_data();
        load_data2();
    });
    load_data_Pendingupload();
    load_data_pendinguploadRequest();

    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
    $("#tblPendingUploadRequest").DataTable().clear().draw();

    $('#BodyHolder_btnComplete').fadeIn();

    $('#BodyHolder_btnCompleteRequest').fadeIn();

    $(".switch_1").change(function () {
        if (this.checked) {
            $('#BodyHolder_btnComplete').hide();
            $('#BodyHolder_AFupload').fadeIn();
            $('#BodyHolder_uploadAF').fadeIn();

            $('#BodyHolder_btnCompleteRequest').hide();
            $('#BodyHolder_btnUploadRequest').fadeIn();
            $('#BodyHolder_fuUploadRequest').fadeIn();
        }
        else {
            $('#BodyHolder_btnComplete').fadeIn();
            $('#BodyHolder_AFupload').hide();
            $('#BodyHolder_uploadAF').hide();

            $('#BodyHolder_btnCompleteRequest').fadeIn();
            $('#BodyHolder_btnUploadRequest').hide();
            $('#BodyHolder_fuUploadRequest').hide();
        }
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
            SweetAlertError(alertMsg);
            return;
        }

        var itrapproveddatestamp = $('#itrapproveddatestamp');
        var itrreleaseddatestamp = $('#itrreleaseddatestamp');
        var itrreceiveddatestamp = $('#itrreceiveddatestamp');

        if (transtype == 'Issuance') {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreateITR",
                data: "{ issuanceno: '" + transno + "',  itrapprovedpkipassword: '" + itrapprovedpkipassword.val() + "', itrreleasedpkipassword: '" + itrreleasedpkipassword.val() + "', itrreceivedpkipassword: '" + itrreceivedpkipassword.val() + "', itrapprovedimagebase64: '" + itrapprovedimagebase64 + "', itrreleaseimagebase64: '" + itrreleaseimagebase64 + "', itrreceivedimagebase64: '" + itrreceivedimagebase64 + "', itrapprovedcertbase64: '" + itrapprovedcertbase64 + "', itrreleasecertbase64: '" + itrreleasecertbase64 + "', itrreceivedcertbase64: '" + itrreceivedcertbase64 + "', itrapproveddatestamp: '" + itrapproveddatestamp.prop('checked') + "',itrreleaseddatestamp: '" + itrreleaseddatestamp.prop('checked') + "',itrreceiveddatestamp: '" + itrreceiveddatestamp.prop('checked') + "' }",
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
        } else {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreateITRRequest",
                //data: "{ issuanceno: '" + transno + "',  itrapprovedpkipassword: '" + itrapprovedpkipassword.val() + "', itrreleasedpkipassword: '" + itrreleasedpkipassword.val() + "', itrreceivedpkipassword: '" + itrreceivedpkipassword.val() + "', itrapprovedimagebase64: '" + itrapprovedimagebase64 + "', itrreleaseimagebase64: '" + itrreleaseimagebase64 + "', itrreceivedimagebase64: '" + itrreceivedimagebase64 + "', itrapprovedcertbase64: '" + itrapprovedcertbase64 + "', itrreleasecertbase64: '" + itrreleasecertbase64 + "', itrreceivedcertbase64: '" + itrreceivedcertbase64 + "' }",
                data: "{ issuanceno: '" + transno + "',  itrapprovedpkipassword: '" + itrapprovedpkipassword.val() + "', itrreleasedpkipassword: '" + itrreleasedpkipassword.val() + "', itrreceivedpkipassword: '" + itrreceivedpkipassword.val() + "', itrapprovedimagebase64: '" + itrapprovedimagebase64 + "', itrreleaseimagebase64: '" + itrreleaseimagebase64 + "', itrreceivedimagebase64: '" + itrreceivedimagebase64 + "', itrapprovedcertbase64: '" + itrapprovedcertbase64 + "', itrreleasecertbase64: '" + itrreleasecertbase64 + "', itrreceivedcertbase64: '" + itrreceivedcertbase64 + "', itrapproveddatestamp: '" + itrapproveddatestamp.prop('checked') + "',itrreleaseddatestamp: '" + itrreleaseddatestamp.prop('checked') + "',itrreceiveddatestamp: '" + itrreceiveddatestamp.prop('checked') + "' }",
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
                url: url + "/PropertyIssuance.aspx/CreateRIS",
                data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "',approveddatestamp: '" + approveddatestamp.prop('checked') + "',issueddatestamp: '" + issueddatestamp.prop('checked') + "' }",
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
        } else {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreateRISRequest",
                //data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "' }",
                data: "{ issuanceno: '" + transno + "',  requestedpkipassword: '" + requestedpkipassword.val() + "', approvedpkipassword: '" + approvedpkipassword.val() + "', issuedpkipassword: '" + issuedpkipassword.val() + "', requestedimagebase64: '" + requestedimagebase64 + "', approvedimagebase64: '" + approvedimagebase64 + "', issuedimagebase64: '" + issuedimagebase64 + "', requestedcertbase64: '" + requestedcertbase64 + "', approvedcertbase64: '" + approvedcertbase64 + "', issuedcertbase64: '" + issuedcertbase64 + "', requesteddatestamp: '" + requesteddatestamp.prop('checked') + "',approveddatestamp: '" + approveddatestamp.prop('checked') + "',issueddatestamp: '" + issueddatestamp.prop('checked') + "' }",
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

    $('#btndownloadpdf').off('click').on('click', function () {
        var transno = $(this).attr('transno');
        var transtype = $(this).attr('transtype');
        //transno

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

        if (transtype == "ICS") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreateICS",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
        else if (transtype == "PAR") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreatePAR",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
                    }
                    else {
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
        } else if (transtype == "PARRequest") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreatePARRequest",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
                    }
                    else {
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
        } else if (transtype == "ICSRequest") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CreateICSRequest",
                data: "{ issuanceno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "',bydatestamp : '" + bydatestamp.prop('checked') + "' }",
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
        } else if (transtype == "ITRRequest") {
        }
    });



    $('#parbtn').off('click').on('click', function () {
        $('#sigandCertModal').modal('show');
    });

    $('#requestedsignatureupload').off('change').on('change', function () {
        requestedreadURL(this);
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

    $('#itrbtn').off('click').on('click', function () {
        $('#ITRsigandCertModal').modal('show');
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

    $('#icsrequestbtn').off('click').on('click', function () {
        $('#sigandCertModal').modal('show');
    });

    $('#parrequestbtn').off('click').on('click', function () {
        $('#sigandCertModal').modal('show');
    });

    $('#parexcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'PAR' + "', key2: '" + issuancenumber + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "PAR_" + formattedDateTime + ".xlsx");
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
            data: "{ key1: '" + 'ICS' + "', key2: '" + issuancenumber + "', key3: '" + 'PIssuance' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "ICS_" + formattedDateTime + ".xlsx");
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
    $('#icsxclrequest').off('click').on('click', function () {
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
            data: "{ key1: '" + 'ICS' + "', key2: '" + requestno + "', key3: '" + 'PRequest' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "ICS_" + formattedDateTime + ".xlsx");
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

    //$('#itrequestbtn').off('click').on('click', function () {
    //    $('#ITRsigandCertModal').modal('show');
    //});

    //$('#risrequestbtn').off('click').on('click', function () {
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
    var s = window.atob(base64.replace(/-/g, "+").replace(/_/g, "/"));
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

// From Private Key to a PKCS#8
function privateKeyToPkcs8(privateKey) {
    var rsaPrivateKey = forge.pki.privateKeyToAsn1(privateKey);
    var privateKeyInfo = forge.pki.wrapRsaPrivateKey(rsaPrivateKey);
    var privateKeyInfoDer = forge.asn1.toDer(privateKeyInfo).getBytes();
    var privateKeyInfoDerBuff = stringToArrayBuffer(privateKeyInfoDer);
    return privateKeyInfoDerBuff;
}

// From Public Key to a PKCS#8
function publicKeyToPkcs8(pk) {
    var subjectPublicKeyInfo = forge.pki.publicKeyToAsn1(pk);
    var der = forge.asn1.toDer(subjectPublicKeyInfo).getBytes();
    return stringToArrayBuffer(der);
}

function CreateDownload(text) {
    var textFile = null;
    var data = new Blob([text], { type: 'application/x-x509-user-cert' });
    textFile = window.URL.createObjectURL(data);
    return textFile;
}

function CertInfo(cert) {
    // Convert to ASN
    var asn1Cert = forge.pki.certificateToAsn1(cert);

    // Convert to DER format
    var p12Der = forge.asn1.toDer(asn1Cert).getBytes();

    // Encode with Base64
    var p12b64 = forge.util.encode64(p12Der);

    var j = '<a href="data:application/x-x509-ca-cert;base64,' + p12b64 + '" download="cert.der">Certificate Information</a><br>';
    try { j += "NAME: " + cert.subject.getField('CN').value + "<br>" } catch (err) { };
    try { j += "MAIL: " + cert.subject.getField('E').value + "<br>" } catch (err) { };
    try { j += "ISSUER: " + cert.issuer.getField('CN').value + "<br>" } catch (err) { };
    return j;
}

function importCryptoKeyPkcs8(privateKey, extractable) {
    var privateKeyInfoDerBuff = privateKeyToPkcs8(privateKey);

    return crypto.subtle.importKey(
		'pkcs8',
		privateKeyInfoDerBuff,
		{ name: "RSASSA-PKCS1-v1_5", hash: { name: "SHA-256" } },
		extractable,
		["sign"]);
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
        url: url + "/PropertyIssuance.aspx/GetUserCustodian",
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
        }
    });
}

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        event.preventDefault();
    }
}

function EnterKey1(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_Pendingupload();
        event.preventDefault();
    }
}

function EnterKey2(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data_pendinguploadRequest();
        event.preventDefault();
    }
}

var needsapproval = "False";
function IfIssuanceNeedsApproval() {
    $.ajax({
        type: "POST",
        url: "Custodian_Operation_Module/PropertyIssuance.aspx/IfIssuanceNeedsApproval",
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

//$(document).on('change', '#ddlCustodian', function () {
//    load_data();
//});

//$('#ddlCustodian').on('change', function () {
//    load_data();
//});

function load_data() {
    var custodian = $('#ddlCustodian').val();
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
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyIssuance.aspx/GetList",
        data: "{custodian: '" + custodian + "', search: '" + search + "', ExpenseType: '" + ExpenseType + "', EmployeeID: '" + $('#ddlEmployeePI').val() + "', notlike: 'not'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData);

            GetCount();
            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
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
            "pagingType": "full_numbers",
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
                    text: 'Add to Issuance List',
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
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        var employee = $('#ddlEmployeePI').val();
                        var custodian = $('#ddlCustodian').val();
                        if ($('#ddlEmployeePI').val() == "0" || $('#ddlEmployeePI').val() == null)
                            employee = 0;
                        if (employee == 0) {
                            swal("Warning", "Select Employee", "warning");
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
                                    url: url + "/PropertyIssuance.aspx/PropertyTransferToIssuance",
                                    data: "{pmlid: '" + pmlid + "', employeeid: '" + employee + "', custodian : '" + custodian + "', ExpenseType : '" + ExpenseType + "'}",
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

        $('#tblAssetList').off('click');

        $('#tblAssetList').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(1)").text();
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

            var employee = $('#ddlEmployeePI').val();
            var custodian = $('#ddlCustodian').val();
            if ($('#ddlEmployeePI').val() == "0" || $('#ddlEmployeePI').val() == null)
                employee = 0;

            if (employee == 0) {
                //alert("Please select an Employee.");
                swal("Warning", "Select Employee", "warning");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: url + "/PropertyIssuance.aspx/PropertyTransferToIssuance",
                    data: "{pmlid: '" + pmlid + "', employeeid: '" + employee + "', custodian : '" + custodian + "', ExpenseType : '" + ExpenseType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
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

function load_data2() {
    var custodian = $('#ddlCustodian').val();
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
    else if (expense.includes('ITR')) {
        ExpenseType = '4'
    }
    else {
        ExpenseType = '0'
    }
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyIssuance.aspx/GetList2",
        data: "{custodian: '" + custodian + "', search: '" + search + "', ExpenseType: '" + ExpenseType + "', EmployeeID: '" + $('#ddlEmployeePI').val() + "', notlike: 'like'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assignedproperty').hide();
            $('#assignedproperty').innerHTML = "";
            $('#assignedproperty').fadeIn(200);

            myJsonData = data;
            populateDataTable2(myJsonData);

            GetCount();
            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
        }
    });
}

function populateDataTable2(data) {
    try {
        $('#tblAssignedPropertyList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "pagingType": "full_numbers",
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "ItemCode" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
        $('#tblAssignedPropertyList').off('click');

        $('#tblAssignedPropertyList ').on('click', 'a.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(1)").text();
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

            var employee = $('#ddlEmployeePI').val();
            var custodian = $('#ddlCustodian').val();
            if ($('#ddlEmployeePI').val() == "0" || $('#ddlEmployeePI').val() == null)
                employee = 0;

            if (employee == 0) {
                //alert("Please select an Employee.");
                swal("Warning", "Select Employee", "warning");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: url + "/PropertyIssuance.aspx/PropertyTransferToIssuance",
                    data: "{pmlid: '" + pmlid + "', employeeid: '" + employee + "', custodian : '" + custodian + "', ExpenseType : '" + ExpenseType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#assetlist').hide();
                        $('#assetlist').innerHTML = "";
                        $('#assetlist').fadeIn(200);
                        GetCount();
                        //load_data();
                        currentRow.remove();
                    }
                });
            }
        });
    } catch (e) {
        var table = $('#tblAssignedPropertyList').DataTable();
        table.clear();
        table.draw();
        //.clear()
        //.draw();
    }
}


var DepartmentArrayPI = new Array();
function departmentPI() {
    var context = window.location.pathname.substring(1);
    var ddlDept = $('#ddlDepartmentPI');
    ddlDept.empty();
    ddlDept.append($('<option>', {
        value: '0',
        text: '--- Select User Department ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetDepartmentPI',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DepartmentArrayPI = JSON.parse(xml.d);
                DepartmentArrayPI.forEach((dep, index) => {
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

function GetApprover() {
    var employee = $('#ddlEmployeePI').val();
    //var custodian = $('#ddlCustodian').val();
    //alert(custodian);
    if ($('#ddlEmployeePI').val() == "0")
        employee = 0;

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GetApprover",
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
    load_data2();
    load_data();
}

function GetEmployeePI() {
    var department = $('#ddlDepartmentPI').val();
    if ($('#ddlDepartmentPI').val() == "0")
        department = 0;
    if (department == 0) {
        $.ajax({
            type: "POST",
            url: url + "/PropertyIssuance.aspx/GetUserNoDepartment",
            data: " { } ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ddlEmployee = $("[id*=ddlEmployeePI]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                $.each(msg.d, function () {
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmployeePI]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/PropertyIssuance.aspx/GetUser",
            data: " { department : '" + department + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (department == 0) {
                    var ddlEmployee = $("[id*=ddlEmployeePI]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                }
                else {
                    var ddlEmployee = $("[id*=ddlEmployeePI]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    $.each(msg.d, function () {
                        ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmployeePI]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
}

var GetCounter = 0;
function GetCount() {
    var employee = $('#ddlEmployeePI').val();
    var custodian = $('#ddlCustodian').val();
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
    //alert("custodian : '" + custodian + "', employeeid: '" + employee + "', ExpenseType: '" + ExpenseType );
    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/PropertyGetCount",
        data: "{ custodian : '" + custodian + "', employeeid: '" + employee + "', ExpenseType: '" + ExpenseType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Property List for Issuance: " + msg.d);
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

function IssuanceList() {
    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val(0).trigger('change');
    //load_data();
    load_data_issuancelist();

    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}

function load_data_issuancelist() {
    var employee = $('#ddlEmployeePI').val();

    var custodian = $('#ddlCustodian').val();
    var PARICS = '0'//added by Nero 022224    
    var expense = $('#BodyHolder_ddlItem').val();
    if (expense.includes('ICS LOW')) {
        PARICS = '2'
    }
    else if (expense.includes('ICS HIGH')) {
        PARICS = '3'
    }
    else if (expense.includes('PAR')) {
        PARICS = '1'
    }
    else if (expense.includes('ITR')) {
        PARICS = '4'
    }
    else {
        PARICS = '0'
    }




    if ($('#ddlEmployeePI').val() == "0" || $('#ddlEmployeePI').val() == null)
        employee = 0;
    if (employee == 0) {
        swal("Warning", "Select Employee", "warning");
    }
    else {
        $.ajax({
            type: 'POST',
            url: url + "/PropertyIssuance.aspx/GetPropertyIssuanceList",
            data: "{custodian : '" + custodian + "', employeeid: '" + employee + "' , typeCost:'" + PARICS + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#issuancelist').hide();
                $('#issuancelist').innerHTML = "";
                $('#issuancelist').fadeIn(200);

                myJsonData = data;
                populateDataTableForIssuance(myJsonData);

                $('#IssuanceList').modal('show');
            },
            error: function (e) {
                alert(e);
            }
        });
    }
}

function populateDataTableForIssuance(data) {
    try {
        $('#tblIssuanceList').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
            var pmlid = $(this).attr('pmlid');
            var propertyno = currentRow.find("td:eq(0)").text();

            var employee = $('#ddlEmployeePI').val();
            //if (confirm("Remove this Property?") == false) { return false; }
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/PropertyRemoveToIssuance",
                data: "{pmlid: '" + pmlid + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#issuancelist').hide();
                    $('#issuancelist').innerHTML = "";
                    $('#issuancelist').fadeIn(200);
                    load_data();
                    load_data2();
                    GetCount();
                    currentRow.remove();
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

function RemoveAllToIssuance() {
    var employee = $('#ddlEmployeePI').val();
    var custodian = $('#ddlCustodian').val();

    //if (confirm("Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?") == false) {
    //    return false;
    //}
    //else {
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
                url: url + "/PropertyIssuance.aspx/PropertyRemoveAllToIssuance",
                data: "{employeeid: '" + employee + "',custodian: '" + custodian + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#IssuanceList').modal('hide');
                    load_data();
                    load_data2();
                    //GetCount();

                    swal("Successful", "List successfully cancelled", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function ConfirmAssetIssuance() {
    //IfIssuanceNeedsApproval();
    $('.btn').prop('disabled', true);

    var employee = $('#ddlEmployeePI').val();
    var custodian = $('#ddlCustodian').val();
    var alertMsg;
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

    if (needsapproval == "True") {
        if ($('#BodyHolder_ddlApprover').val() == '0' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = ' ';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlApprover').val() == '0')
                alertMsg += "\nApprover";

            //alert(alertMsg);

            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else {
            //if (confirm("Are you sure you want to CONFIRM this transaction?") == false) {
            //    $('.btn').prop('disabled', false);
            //    return false;
            //}
            swal({
                title: "Are you sure?",
                text: "Are you sure, you want to Confirm this transaction?",
                icon: "warning",
                reverseButtons: false,
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
                        url: url + "/PropertyIssuance.aspx/ConfirmPropertyIssuance",
                        data: "{ employeeid : '" + employee + "',custodians: '" + custodian + "', approverid : '" + $('#BodyHolder_ddlApprover').val() + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "',ExpenseType: '" + ExpenseType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested For Issuance and Pending For Approval/Upload.\n\nMail not sent. Please check you mail settings at SYSTEM > MISCELLANEOUS.');

                                swal("Successful", "Successfully Requested For Issuance and Pending For Approval/Upload.\n\nMail not sent. Check you mail settings at SYSTEM > MISCELLANEOUS.", "success");
                                load_data();
                                load_data2();
                                //load_data_issuancelist();
                                load_data_Pendingupload();
                                //GetCount();
                                $('#IssuanceList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested For Issuance and Pending For Approval/Upload');
                                swal("Successful", "Successfully Requested For Issuance and Pending For Approval/Upload.", "success");
                                load_data();
                                load_data2();
                                //load_data_issuancelist();
                                load_data_Pendingupload();
                                //GetCount();
                                $('#IssuanceList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                }
                else {
                    swal("Cancelled", "Cancelled by user", "warning");
                    $('.btn').prop('disabled', false);
                }
            });
        }
    }
    else {
        if ($('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = ' ';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";

            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
        }
        else {
            //if (confirm("Are you sure you want to CONFIRM this transaction?") == false) {
            //    $('.btn').prop('disabled', false);
            //    return false;
            //}
            swal({
                title: "Are you sure?",
                text: "Are you sure, you want to Confirm this transaction?",
                icon: "warning",
                reverseButtons: false,
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
                        url: url + "/PropertyIssuance.aspx/ConfirmPropertyIssuance",
                        data: "{ employeeid : '" + employee + "',custodians: '" + custodian + "', approverid : '0', remarks : '" + $('#BodyHolder_txtRemarks').val() + "', ExpenseType: '" + ExpenseType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Issued and Pending For Upload.\n\nMail not sent. Please check you mail settings at SYSTEM > MISCELLANEOUS.');
                                swal("Successful", "Successfully Issued and Pending For Verification.", "success");

                                load_data();
                                load_data2();
                                //load_data_issuancelist();
                                load_data_Pendingupload();
                                GetCount();
                                $('#IssuanceList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Issued and Pending For Upload');
                                swal("Successful", "Successfully Requested For Issuance and Pending For Verification.", "success");
                                load_data();
                                load_data2();
                                //load_data_issuancelist();
                                load_data_Pendingupload();
                                GetCount();
                                $('#IssuanceList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            $(".se-pre-con").hide();
                        }
                    });
                }
                else {
                    swal("Cancelled", "Cancelled by user", "warning");
                    $('.btn').prop('disabled', false);
                }
            });
        }
    }
}

function CancelTransaction(issuanceno) {

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
    textareatt.id = "cancelissue";
    swal({
        title: "Are you sure?",
        text: "Are you sure, you want to Cancel this transaction?",
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

            var textremarks = $('#cancelissue').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }

            textremarks = ReplaceStr1(textremarks);
            $.ajax({
                type: "POST",
                url: url + "/PropertyIssuance.aspx/CancelPropertyIssuance",
                data: "{issuanceno : '" + issuanceno + "', remarks: '" + textremarks + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data();
                    load_data2();
                    load_data_Pendingupload();
                    //alert("Property Issuance cancelled successfully.");
                    swal("Successful", "Property Issuance cancelled successfully.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function load_data_Pendingupload() {
    var search = "";
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyIssuance.aspx/GetPendingUploadList",
        data: "{ search : '" + search + "', custodian : '" + custodian + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendinguploadlist').hide();
            $('#pendinguploadlist').innerHTML = "";
            $('#pendinguploadlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUpload(myJsonData);
        },
        error: function (e) {
            alert(e);
        }
    });
}

function populateDataTablePendingUpload(data) {
    try {
        $('#tblPendingUpload').DataTable({
            "destroy": true,
            "order": [],
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
                { "data": "IssuanceNumber" },
                { "data": "IssuedTo" },
                //{ "data": "NoOfItemString" },
                { "data": "DateOfTransaction" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
        table.clear().draw();
    }
}

var issuancenumber = '';
function ViewAssets(issuanceno, employeename, transactorname, createddate, counter, counterchecker, isapproved, staus, PARICS) {
    issuancenumber = issuanceno;
    $('#BodyHolder_AFupload').val('');
    $('#btndownloadcreatepdf').attr('transno', issuancenumber);

    $('#btndownloadpdf').attr('transno', issuancenumber);
    $('#btndownloadrispdf').attr('transno', issuancenumber);
    $('#btndownloaditrpdf').attr('transno', issuancenumber);
    $('#btndownloadrispdf').attr('transtype', 'Issuance');
    document.getElementById('lblPARICS').innerHTML = issuanceno;
    document.getElementById('lblIssuedBy').innerHTML = transactorname
    document.getElementById('lblPerson').innerHTML = employeename;
    document.getElementById('lblDateIssued').innerHTML = createddate;

    document.getElementById('lblPARICS2').innerHTML = issuanceno;
    document.getElementById('lblIssuedBy2').innerHTML = transactorname;
    document.getElementById('lblPerson2').innerHTML = employeename;
    document.getElementById('lblDateIssued2').innerHTML = createddate;
    $('#BodyHolder_txtIssuanceNo').val(issuanceno);

    var x = document.getElementById("showorhide");
    if (counter == counterchecker) {
        if ((isapproved == "True") && (staus != "For Acknowledgement")) {
            load_data_PUL1();
            $('#ALPendingUpload').modal('show');
            x.style.display = "inline";
        }
        else {
            load_data_PUL1();
            $('#ALPendingUpload').modal('show');
            x.style.display = "none";
        }
    }
    else {
        load_data_PUL1();
        $('#ALPendingUpload').modal('show');
        x.style.display = "none";
    }

    if (PARICS == "1") {
        $('#lbl_from').html('Received By');
        $('#lbl_by').html('Issued By');
        $('#btndownloadpdf').attr('transtype', "PAR");
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "inline";
        document.getElementById("printGrid").children[5].style.display = "none";
        document.getElementById("printGrid").children[6].style.display = "inline";
        document.getElementById("printGrid").children[7].style.display = "inline";
        document.getElementById("printGrid").children[8].style.display = "none";
        document.getElementById("printGrid").children[9].style.display = "none";
        document.getElementById("printGrid").children[10].style.display = "none";
        document.getElementById("printGrid").children[11].style.display = "none";
        document.getElementById("printGrid").children[12].style.display = "none";
        document.getElementById("printGrid").children[13].style.display = "none";
        document.getElementById("printGrid").children[14].style.display = "inline";
        document.getElementById("printGrid").children[15].style.display = "inline";
    }
    else if (PARICS == "2" || PARICS == "3") {
        $('#lbl_from').html('Received From');
        $('#lbl_by').html('Received By');
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "inline";
        document.getElementById("printGrid").children[5].style.display = "inline";
        document.getElementById("printGrid").children[6].style.display = "none";
        document.getElementById("printGrid").children[7].style.display = "inline";
        document.getElementById("printGrid").children[8].style.display = "none";
        document.getElementById("printGrid").children[9].style.display = "none";
        document.getElementById("printGrid").children[10].style.display = "none";
        document.getElementById("printGrid").children[11].style.display = "none";
        document.getElementById("printGrid").children[12].style.display = "none";
        document.getElementById("printGrid").children[13].style.display = "inline";
        document.getElementById("printGrid").children[14].style.display = "none";
        document.getElementById("printGrid").children[15].style.display = "inline";
        $('#btndownloadpdf').attr('transtype', "ICS");
    }
    else if (PARICS == "4") {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "none";
        document.getElementById("printGrid").children[6].style.display = "none";
        document.getElementById("printGrid").children[7].style.display = "none";
        document.getElementById("printGrid").children[8].style.display = "none";
        document.getElementById("printGrid").children[9].style.display = "none";
        document.getElementById("printGrid").children[10].style.display = "none";
        document.getElementById("printGrid").children[11].style.display = "none";
        document.getElementById("printGrid").children[12].style.display = "none";
        document.getElementById("printGrid").children[13].style.display = "none";
        document.getElementById("printGrid").children[14].style.display = "none";
        document.getElementById("printGrid").children[15].style.display = "none";
        $('#btndownloadpdf').attr('transtype', "");
        $('#btndownloaditrpdf').attr('transtype', "Issuance");
    }
    else {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "none";
        document.getElementById("printGrid").children[6].style.display = "none";
        document.getElementById("printGrid").children[7].style.display = "none";
        document.getElementById("printGrid").children[8].style.display = "none";
        document.getElementById("printGrid").children[9].style.display = "none";
        document.getElementById("printGrid").children[10].style.display = "none";
        document.getElementById("printGrid").children[11].style.display = "none";
        document.getElementById("printGrid").children[12].style.display = "none";
        document.getElementById("printGrid").children[13].style.display = "none";
        document.getElementById("printGrid").children[14].style.display = "none";
        document.getElementById("printGrid").children[15].style.display = "none";
        $('#btndownloadpdf').attr('transtype', "");
        $('#btndownloaditrpdf').attr('transtype', "Issuance");
    }

    //gheghe(Certificate/Word)
    //document.getElementById("printGrid").children[5].style.display = "none";
    //document.getElementById("printGrid").children[6].style.display = "none";
    //document.getElementById("printGrid").children[7].style.display = "none";
    //document.getElementById("printGrid").children[8].style.display = "none";
    //document.getElementById("printGrid").children[9].style.display = "none";
    //document.getElementById("printGrid").children[10].style.display = "none";
    //document.getElementById("printGrid").children[11].style.display = "none";

    $.ajax({
        type: "POST",
        url: "Custodian_Operation_Module/PropertyIssuance.aspx/GenerateIssuanceReportPAR",
        data: "{issuanceno: '" + issuanceno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportPAR').hide();
            $('#BodyHolder_divIssuanceReportPAR').html(msg.d);
            $('#BodyHolder_divIssuanceReportPAR').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportICS",
        data: "{issuanceno: '" + issuanceno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportICS').hide();
            $('#BodyHolder_divIssuanceReportICS').html(msg.d);
            $('#BodyHolder_divIssuanceReportICS').fadeIn(200);

            $('#BodyHolder_divIssuanceReportICS2').hide();
            $('#BodyHolder_divIssuanceReportICS2').html(msg.d);
            $('#BodyHolder_divIssuanceReportICS2').fadeIn(200);
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportICS",
    //    data: "{issuanceno: '" + issuanceno + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        $('#BodyHolder_divIssuanceReportICS2').hide();
    //        $('#BodyHolder_divIssuanceReportICS2').html(msg.d);
    //        $('#BodyHolder_divIssuanceReportICS2').fadeIn(200);
    //    }
    //});
    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportITR",
        data: "{issuanceno: '" + issuanceno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportITR').hide();
            $('#BodyHolder_divIssuanceReportITR').html(msg.d);
            $('#BodyHolder_divIssuanceReportITR').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportRIS",
        data: "{issuanceno: '" + issuanceno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg.d);
            $('#BodyHolder_divIssuanceReportRIS').hide();
            $('#BodyHolder_divIssuanceReportRIS').html(msg.d);
            $('#BodyHolder_divIssuanceReportRIS').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReport",
        data: "{issuanceno: '" + issuanceno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReport').hide();
            $('#BodyHolder_divIssuanceReport').html(msg.d);
            $('#BodyHolder_divIssuanceReport').fadeIn(200);
        }
    });
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyIssuance.aspx/GetPLPendingUploadList",
        data: "{issuanceno: '" + issuancenumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pul1').hide();
            $('#pul1').innerHTML = "";
            $('#pul1').fadeIn(200);
            myJsonData = data;
            populateDataTablePUL1(myJsonData);
        },
        error: function (e) {
            alert(e);
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
        table.clear().draw();
    }
}

//Property Request
function load_data_pendinguploadRequest() {
    var search = "";
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyIssuance.aspx/PropertyRequestPendingUpload",
        data: "{search: '" + search + "' , custodian : '" + custodian + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendinguploadrequestlist').hide();
            $('#pendinguploadrequestlist').innerHTML = "";
            $('#pendinguploadrequestlist').fadeIn(200);

            myJsonData = data;
            populateDataTablePendingUploadRequest(myJsonData);
            GetCount();
        },
        error: function (e) {
            alert(e);
        }
    });
}

function populateDataTablePendingUploadRequest(data) {
    try {
        $('#tblPendingUploadRequest').DataTable({
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
                { "data": "RequestedBy" },
                { "data": "DateOfReturn" },
                { "data": "DateOfTransaction" },
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

        $('#tblPendingUploadRequest').off('click');
        $('#tblPendingUploadRequest').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
        $('#tblPendingUploadRequest').on('click', 'a.btn_Cancel', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var requestnumber = currentRow.find("td:eq(0)").text();
            if (confirm("Are you sure you want to CANCEL this transaction?") == false) {
                return false;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: url + "/PropertyIssuance.aspx/CancelPropertyRequest",
                    data: "{requestno: '" + requestnumber + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#pendinguploadrequestlist').hide();
                        $('#pendinguploadrequestlist').innerHTML = "";
                        $('#pendinguploadrequestlist').fadeIn(200);
                        load_data();
                        load_data2();
                        load_data_pendingupload();
                        currentRow.remove();
                    }
                });
            }
        });
    } catch (e) {
        var table = $('#tblPendingUploadRequest').DataTable();
        table.clear().draw();
    }
}

var requestno = '';
function ViewAssetsRequest(requestnumber, counter, counterchecker, isapproved, PARICS) {
    requestno = requestnumber;
    $('#BodyHolder_fuUploadRequest').val('');
    document.getElementById('lblRequestNumber1').innerHTML = requestnumber;

    $('#btndownloadcreatepdf').attr('transno', requestnumber);

    $('#BodyHolder_txtRequestNo').val(requestnumber);

    $('#btndownloadpdf').attr('transno', requestnumber);
    $('#btndownloaditrpdf').attr('transno', requestnumber);
    $('#btndownloaditrpdf').attr('transtype', 'Request');
    $('#btndownloadrispdf').attr('transno', requestnumber);
    $('#btndownloadrispdf').attr('transtype', 'Request');
    var x = document.getElementById("showorhiderequest");
    if (counter == counterchecker) {
        if (isapproved == "True") {
            load_data_purl();
            $('#RLPendingUpload').modal('show');
            x.style.display = "inline";
        }
        else {
            load_data_purl();
            $('#RLPendingUpload').modal('show');
            x.style.display = "none";
        }
    }
    else {
        load_data_purl();
        $('#RLPendingUpload').modal('show');
        x.style.display = "none";
    }
    if (PARICS == "1") {
        $('#lbl_from').html('Received By');
        $('#lbl_by').html('Issued By');
        $('#btndownloadpdf').attr('transtype', 'PARRequest');
        document.getElementById("printGridRequest").children[0].style.display = "inline";
        document.getElementById("printGridRequest").children[1].style.display = "none";
        document.getElementById("printGridRequest").children[2].style.display = "none";
        document.getElementById("printGridRequest").children[3].style.display = "none";
        document.getElementById("printGridRequest").children[4].style.display = "inline";
        document.getElementById("printGridRequest").children[5].style.display = "none";
        document.getElementById("printGridRequest").children[6].style.display = "inline";
        document.getElementById("printGridRequest").children[7].style.display = "inline";
        document.getElementById("printGridRequest").children[8].style.display = "none";
        document.getElementById("printGridRequest").children[9].style.display = "none";
        document.getElementById("printGridRequest").children[10].style.display = "none";
        document.getElementById("printGridRequest").children[11].style.display = "none";
        document.getElementById("printGridRequest").children[12].style.display = "none";
        document.getElementById("printGridRequest").children[13].style.display = "none";
        document.getElementById("printGridRequest").children[14].style.display = "inline";
        document.getElementById("printGridRequest").children[15].style.display = "inline";
    }
    else if (PARICS == "2" || PARICS == "3") {
        $('#lbl_from').html('Received From');
        $('#lbl_by').html('Received By');
        $('#btndownloadpdf').attr('transtype', 'ICSRequest');
        document.getElementById("printGridRequest").children[0].style.display = "none";
        document.getElementById("printGridRequest").children[1].style.display = "inline";
        document.getElementById("printGridRequest").children[2].style.display = "none";
        document.getElementById("printGridRequest").children[3].style.display = "none";
        document.getElementById("printGridRequest").children[4].style.display = "inline";
        document.getElementById("printGridRequest").children[5].style.display = "inline";
        document.getElementById("printGridRequest").children[6].style.display = "none";
        document.getElementById("printGridRequest").children[7].style.display = "inline";
        document.getElementById("printGridRequest").children[8].style.display = "none";
        document.getElementById("printGridRequest").children[9].style.display = "none";
        document.getElementById("printGridRequest").children[10].style.display = "none";
        document.getElementById("printGridRequest").children[11].style.display = "none";
        document.getElementById("printGridRequest").children[12].style.display = "none";
        document.getElementById("printGridRequest").children[13].style.display = "inline";
        document.getElementById("printGridRequest").children[14].style.display = "none";
        document.getElementById("printGridRequest").children[15].style.display = "inline";
    }
    else {
        $('#btndownloadpdf').attr('transtype', 'ITRRequest');

        document.getElementById("printGridRequest").children[1].style.display = "none";
        document.getElementById("printGridRequest").children[0].style.display = "none";
        document.getElementById("printGridRequest").children[2].style.display = "none";
        document.getElementById("printGridRequest").children[3].style.display = "none";
        document.getElementById("printGridRequest").children[4].style.display = "none";
        document.getElementById("printGridRequest").children[5].style.display = "none";
        document.getElementById("printGridRequest").children[6].style.display = "none";
        document.getElementById("printGridRequest").children[7].style.display = "none";
        document.getElementById("printGridRequest").children[8].style.display = "none";
        document.getElementById("printGridRequest").children[9].style.display = "none";
        document.getElementById("printGridRequest").children[10].style.display = "none";
        document.getElementById("printGridRequest").children[11].style.display = "none";
        document.getElementById("printGridRequest").children[12].style.display = "none";
        document.getElementById("printGridRequest").children[13].style.display = "none";
        document.getElementById("printGridRequest").children[14].style.display = "none";
        document.getElementById("printGridRequest").children[15].style.display = "none";
    }

    //gheghe(Certificate/Word)
    //document.getElementById("printGridRequest").children[5].style.display = "none";
    //document.getElementById("printGridRequest").children[6].style.display = "none";
    //document.getElementById("printGridRequest").children[7].style.display = "none";
    //document.getElementById("printGridRequest").children[8].style.display = "none";
    //document.getElementById("printGridRequest").children[9].style.display = "none";
    //document.getElementById("printGridRequest").children[10].style.display = "none";
    //document.getElementById("printGridRequest").children[11].style.display = "none";
    //document.getElementById("printGridRequest").children[12].style.display = "none";

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportPARRequest",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportPARRequest').hide();
            $('#BodyHolder_divIssuanceReportPARRequest').html(msg.d);
            $('#BodyHolder_divIssuanceReportPARRequest').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportICSRequest",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportICSRequest').hide();
            $('#BodyHolder_divIssuanceReportICSRequest').html(msg.d);
            $('#BodyHolder_divIssuanceReportICSRequest').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportITRRequest",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportITRRequest').hide();
            $('#BodyHolder_divIssuanceReportITRRequest').html(msg.d);
            $('#BodyHolder_divIssuanceReportITRRequest').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateIssuanceReportRISRequest",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divIssuanceReportRISRequest').hide();
            $('#BodyHolder_divIssuanceReportRISRequest').html(msg.d);
            $('#BodyHolder_divIssuanceReportRISRequest').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyIssuance.aspx/GenerateRequestReport",
        data: "{requestno: '" + requestno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divRequestReport').hide();
            $('#BodyHolder_divRequestReport').html(msg.d);
            $('#BodyHolder_divRequestReport').fadeIn(200);
        }
    });
}

function load_data_purl() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyIssuance.aspx/GetPRequestPendingUploadList",
        data: "{requestnumber: '" + requestno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#requestlist').hide();
            $('#requestlist').innerHTML = "";
            $('#requestlist').fadeIn(200);
            myJsonData = data;
            populateDataTablePULrequest(myJsonData);
        },
        error: function (e) {
            alert(e);
        }
    });
}

function populateDataTablePULrequest(data) {
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Issuance'
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
    } catch (e) {
        var table = $('#tblRequestList').DataTable();
        table.clear().draw();
    }
}

function UploadPropertyIssuance() {
    var alertMsg;
    var issuanceno = $('#BodyHolder_txtIssuanceNo').val();
    var filess = $('#BodyHolder_AFupload').val();
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
                    var fileUpload = $("#BodyHolder_AFupload").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + issuanceno + "|PropertyIssuance",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyIssuance.aspx/UploadPropertyIssuance",
                                    data: " { issuanceno : '" + issuanceno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_Pendingupload();
                                        $('#ALPendingUpload').modal('hide');
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

function UploadPropertyRequest() {
    var alertMsg;
    var requestno = $('#BodyHolder_txtRequestNo').val();
    var filess = $('#BodyHolder_fuUploadRequest').val();
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
                    var fileUpload = $("#BodyHolder_fuUploadRequest").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + requestno + "|PropertyRequest",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyIssuance.aspx/UploadPropertyRequest",
                                    data: " { requestno : '" + requestno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_pendinguploadRequest();
                                        $('#RLPendingUpload').modal('hide');
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