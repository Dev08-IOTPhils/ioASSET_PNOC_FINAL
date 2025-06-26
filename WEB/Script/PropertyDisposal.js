var url = '';
var propdisidstr = "";
var excelcount = 1;

$(document).ready(function () {

    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateTo').datepicker('setDate', today);

    IfDisposalNeedsApproval();

    $('#btnPrintGridDataROSEP').off('click').on('click', function () {
        $('#btndownloadpdf').attr('transtype', 'PrintGridDataROSEP');
        $('#sigandCertModal').modal('show');
        ClearSignature();
        $('#lbl_from').html('Transacted by');
        $('#lbl_by').html('Approved by');
    });

    $('#btnPrintGridDataROP').off('click').on('click', function () {
        $('#btndownloadpdf').attr('transtype', 'PrintGridDataROP');
        $('#sigandCertModal').modal('show');
        ClearSignature();
        $('#lbl_from').html('Certified Correct');
        $('#lbl_by').html('Disposal Approved');
    });
    $('#btnPrintGridDataIIRUP').off('click').on('click', function () {
        $('#btndownloadpdf').attr('transtype', 'PrintGridDataIIRUP');
        $('#sigandCertModal').modal('show');
        ClearSignature();
        $('#lbl_from').html('Certified Correct');
        $('#lbl_by').html('Disposal Approved');
    });


    $('#ciirupxcl').off('click').on('click', function () {
        //alert(excelcount);


        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        if (excelcount == 1) {
            swal("Warning", "Please select list of IIRUP", "warning");
        }
        else {
            $.ajax({
                async: false,
                type: 'POST',
                url: context + '/ReportPage',
                data: "{ key1: '" + 'CIIRUP' + "', key2: '" + prodisidstr + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                        a.attr("download", "IIRUP_" + formattedDateTime + ".xls");
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
        }
    });

    $('#ciiruspxcl').off('click').on('click', function () {

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);

        if (excelcount == 1) {
            swal("Warning", "Please select list of IIRUSP", "warning");
        }

        else {
            //alert(disposalnumberrequest);
            $.ajax({
                async: false,
                type: 'POST',
                url: context + '/ReportPage',
                data: "{ key1: '" + 'CIIRUSP' + "', key2: '" + prodisidstr + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                        a.attr("download", "IIRUSP_" + formattedDateTime + ".xls");
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
        }
    });

    $('#cwmrxcl').off('click').on('click', function () {

        const now = new Date();

        const year = now.getFullYear();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');

        const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

        var context = window.location.pathname.substring(1);
        var datefrom = $('#txtDateFrom').val();
        var dateto = $('#txtDateTo').val();
        //alert(disposalnumber);

        if (excelcount == 1) {
            swal("Warning", "Please select list of WMR", "warning");
        }
        else {
            $.ajax({
                async: false,
                type: 'POST',
                url: context + '/ReportPage',
                data: "{ key1: '" + 'CWMR' + "', key2: '" + prodisidstr + "', key3: '" + 'PDisposal' + "', key4: '" + datefrom + "', key5: '" + dateto + "'}",
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
                        a.attr("download", "WMR_" + formattedDateTime + ".xls");
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
        }
    });


    $('#wmrxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'WMR' + "', key2: '" + disposalnumber + "', key3: '" + 'PDisposal' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "WMR_" + formattedDateTime + ".xls");
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

    $('#iirupxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'IIRUP' + "', key2: '" + disposalnumber + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "IIRUP_" + formattedDateTime + ".xls");
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

    $('#iiruspxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'IIRUSP' + "', key2: '" + disposalnumber + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "IIRUSP_" + formattedDateTime + ".xls");
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

    $('#rlsddpxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'RLSDDP' + "', key2: '" + disposalnumber + "', key3: '" + 'PDisposal' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RLSDDP_" + formattedDateTime + ".xls");
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
    $('#rlsddspxcl').off('click').on('click', function () {

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
            data: "{ key1: '" + 'RLSDDSP' + "', key2: '" + disposalnumber + "', key3: '" + 'PDisposal' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RLSDDSP_" + formattedDateTime + ".xls");
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
    //R
    $('#wmrxclR').off('click').on('click', function () {

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
            data: "{ key1: '" + 'WMR' + "', key2: '" + disposalnumberrequest + "', key3: '" + 'PDisposalR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "WMR_" + formattedDateTime + ".xls");
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

    $('#rlsddpxclR').off('click').on('click', function () {

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
            data: "{ key1: '" + 'RLSDDP' + "', key2: '" + disposalnumberrequest + "', key3: '" + 'PDisposalR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RLSDDP_" + formattedDateTime + ".xls");
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
    $('#rlsddspxclR').off('click').on('click', function () {

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
            data: "{ key1: '" + 'RLSDDSP' + "', key2: '" + disposalnumberrequest + "', key3: '" + 'PDisposalR' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                    a.attr("download", "RLSDDSP_" + formattedDateTime + ".xls");
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
    $('#btndownloadpdf').off('click').on('click', function () {
        var transno = $('#btndownloadpdf').attr('transno');
        var transtype = $('#btndownloadpdf').attr('transtype');
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

        if (transtype == "PrintGridDataROSEP") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/CreatePDFPrintGridDataROSEP",
                data: "{ disposalno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    console.log(msg.d);
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
                            a.attr("download", "Property_Disposal.pdf");
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
        else if (transtype == "PrintGridDataIIRUP") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/CreatePDFIIRUP",
                data: "{ disposalno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    console.log(msg.d);
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
                            a.attr("download", "Property_Disposal.pdf");
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
        else if (transtype == "PrintGridDataIIRSEP") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/CreatePDFIISEP",
                data: "{ disposalno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    console.log(msg.d);
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
                            a.attr("download", "Property_Disposal.pdf");
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
        else if (transtype == "PrintGridDataROP") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/CreatePDFPrintGridDataROP",
                data: "{ disposalno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    console.log(msg.d);
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
                            a.attr("download", "Property_Disposal.pdf");
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
        else if (transtype == "Disposal From") {
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/CreatePDFDisposal",
                data: "{ disposalno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Disposal.pdf");
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
                url: url + "/PropertyDisposal.aspx/CreatePDFWasteMaterial",
                data: "{ disposalno: '" + transno + "', fromimagebase64: '" + fromimagebase64 + "', byimagebase64: '" + byimagebase64 + "', frompassphrase: '" + frompkipassword.val() + "', bypassphrase: '" + bypkipassword.val() + "', fromcertbase64: '" + fromcertbase64 + "', bycertbase64: '" + bycertbase64 + "', fromdatestamp: '" + fromdatestamp.prop('checked') + "', bydatestamp: '" + bydatestamp.prop('checked') + "' }",
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
                            a.attr("download", "Property_Disposal.pdf");
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

    $("#tblAssetList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                      },
                       {
                           extend: 'colvis',
                           text: 'COLUMN VISIBILITY',
                           className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                       }
                ],
                dropup: false, //btn btn-sm btn-success
            },

            ]
        }, colReorder: true
    });
    $("#tblPendingUpload1").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
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
        //load_data_Pendingupload();
    });
    load_data_Pendingupload();
    load_data_PendinguploadRequest();

    $("#tblAssetList").DataTable().clear().draw();
    $("#tblPendingUpload").DataTable().clear().draw();
    $("#tblPendingUpload1").DataTable().clear().draw();


    $('#BodyHolder_btnComplete').fadeIn();
    //$('#BodyHolder_AFupload').hide();
    //$('#BodyHolder_uploadAF').hide();

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
        url: url + "/PropertyDisposal.aspx/GetUserCustodian",
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

var needsapproval = "False";
function IfDisposalNeedsApproval() {
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/IfDisposalNeedsApproval",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            needsapproval = msg.d;
            var x = document.getElementById("approvers");
            var y = document.getElementById("withorwithoutyou");
            var z = document.getElementById("withorwithoutyou1");
            if (msg.d == "True") {
                x.style.display = 'inline';
                y.style.display = 'inline';
                z.style.display = 'none';
            }
            else {
                x.style.display = 'none';
                y.style.display = 'none';
                z.style.display = 'inline';
            }
        }
    });
}

function GetInspectorAndCertifier() {
    var approverid = $('#BodyHolder_ddlApprover').val();
    if ($('#BodyHolder_ddlApprover').val() == "--- Select Approver ---")
        approverid = 0;
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GetInspector",
        data: " { userid : '" + approverid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (approverid == 0) {
                var ddlInspector = $("[id*=ddlInspector]");
                ddlInspector.empty().append("<option selected='selected' value='0'>--- Select Inspector ---</option>");
                ddlInspector.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
            else {
                var ddlInspector = $("[id*=ddlInspector]");
                ddlInspector.empty().append("<option selected='selected' value='0'>--- Select Inspector ---</option>");
                $.each(msg.d, function () {
                    ddlInspector.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            }
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GetCertifier",
        data: " { userid : '" + approverid + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (approverid == 0) {
                var ddlCertifier = $("[id*=ddlCertifier]");
                ddlCertifier.empty().append("<option selected='selected' value='0'>--- Select Certifier ---</option>");
                ddlCertifier.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
            else {
                var ddlCertifier = $("[id*=ddlCertifier]");
                ddlCertifier.empty().append("<option selected='selected' value='0'>--- Select Certifier ---</option>");
                $.each(msg.d, function () {
                    ddlCertifier.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            }
        }
    });
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

    var search = "";
    var custodian = $('#ddlCustodian').val();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyDisposal.aspx/GetList",
        data: "{ search: '" + search + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData, ExpenseType);
            GetCount(ExpenseType);
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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
                    text: 'Add to Disposal List',
                    action: function () {
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
                        var count = $('#tblAssetList').DataTable().rows({ selected: true }).count();
                        if (count == 0) {
                            swal("Warning", "Please select an item to add", "warning");
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
                                    url: url + "/PropertyDisposal.aspx/PropertyTransferToDisposal",
                                    data: "{pmlid: '" + pmlid + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $('#assetlist').hide();
                                        $('#assetlist').innerHTML = "";
                                        $('#assetlist').fadeIn(200);
                                        $('#tblAssetList').DataTable()
                                        .rows('.selected')
                                        .remove()
                                        .draw(false);
                                        GetCount(ExpenseType);
                                        $(".se-pre-con").hide();
                                    }
                                });
                            });
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
            var propertyno = currentRow.find("td:eq(1)").text();
            var custodian = $('#ddlCustodian').val();
            var pmlid = $(this).attr('pmlid');
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/PropertyTransferToDisposal",
                data: "{pmlid: '" + pmlid + "',custodian: '" + custodian + "',ExpenseType: '" + ExpenseType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#assetlist').hide();
                    $('#assetlist').innerHTML = "";
                    $('#assetlist').fadeIn(200);
                    GetCount(ExpenseType);
                    //load_data();
                    $('#tblAssetList').DataTable().rows(currentRow)
                    .remove()
                    .draw(false);
                    //currentRow.remove();
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

var GetCounter = 0;
function GetCount(ExpenseType) {
    var custodian = $('#ddlCustodian').val();
    //alert(custodian);
    //alert(ExpenseType);
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/PropertyGetCount",
        data: "{ custodian: '" + custodian + "' ,ExpenseType: '" + ExpenseType + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View Property List for Disposal: " + msg.d);
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

    $('#BodyHolder_txtRemarks').val('');
    $('#BodyHolder_ddlApprover').val('--- Select Approver ---').trigger('change');
    $('#BodyHolder_ddlModeOfDisposal').val('--- Select Mode of Disposal ---').trigger('change');


    load_data();
    load_data_disposallist();
    GetCount(ExpenseType);

    if (GetCounter == 0) {
        document.getElementById("btnAssetItemTransfer").disabled = true;
    }
    else {
        document.getElementById("btnAssetItemTransfer").disabled = false;
    }
}

function load_data_disposallist() {
    var custodian = $('#ddlCustodian').val();
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
    $.ajax({
        type: 'POST',
        url: url + "/PropertyDisposal.aspx/GetPropertyDisposalList",
        data: "{ custodian: '" + custodian + "' ,ExpenseType: '" + ExpenseType + "' }", //add ExpenseType by nero 022624
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#disposallist').hide();
            $('#disposallist').innerHTML = "";
            $('#disposallist').fadeIn(200);

            myJsonData = data;
            populateDataTableForDisposal(myJsonData, ExpenseType);

            console.log(data);
            console.log('Success');
            $('#disposalList').modal('show');
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTableForDisposal(data, ExpenseType) {
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
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Condition" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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
            var propertyno = currentRow.find("td:eq(0)").text();
            var pmlid = $(this).attr('pmlid');

            //if (confirm("Remove this Property?") == false) { return false; }
            $.ajax({
                type: "POST",
                url: url + "/PropertyDisposal.aspx/PropertyRemoveToDisposal",
                data: "{pmlid: '" + pmlid + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#disposallist').hide();
                    $('#disposallist').innerHTML = "";
                    $('#disposallist').fadeIn(200);
                    load_data();
                    GetCount(ExpenseType);
                    currentRow.remove();
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
    //if (confirm("Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?") == false) {
    //    return false;
    //}
    //else {
    var custodian = $('#ddlCustodian').val();
    swal({
        title: "Are you sure?",
        text: "Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?",
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
                url: url + "/PropertyDisposal.aspx/PropertyRemoveAllToDisposal",
                data: "{custodian : '" + custodian + "' , ExpenseType:'" + ExpenseType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#disposalList').modal('hide');
                    load_data();
                    GetCount(ExpenseType);
                    swal("Successful", "Successfully cancelled Transaction!", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
    //}
}
var ddlconditioncounter = 0;
function checkddlcondition() {
    $(".ddlcondition").each(function () {
        if (this.value == "Select Condition") { //obviously do all the checking
            //found a select matching the conditions
            //Do stuff!
            //return false; //this exits the each loop ^^
            ddlconditioncounter = ddlconditioncounter + 1;
        }
    });
}

function changeCondition(recid, ddlconditionid) {
    var condition = $("#ddlCondition" + ddlconditionid).val();
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/changeCondition",
        data: "{recid : '" + recid + "',condition: '" + condition + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        }
    });
}

function ConfirmAssetDisposal() {
    ddlconditioncounter = 0;
    checkddlcondition();
    $('.btn').prop('disabled', true);
    var approver = $('#BodyHolder_ddlApprover').val();
    var inspector = $('#BodyHolder_ddlInspector').val();
    var certifier = $('#BodyHolder_ddlCertifier').val();
    var custodian = $('#ddlCustodian').val();
    var alertMsg;
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

    if (needsapproval == "True") {
        if (ddlconditioncounter != 0 || $('#BodyHolder_ddlApprover').val() == '--- Select Approver ---' || $('#BodyHolder_ddlInspector').val() == '0' || $('#BodyHolder_ddlCertifier').val() == '0' || $('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = '';
            if ($('#BodyHolder_ddlApprover').val() == '--- Select Approver ---')
                alertMsg += "\nApprover";
            if ($('#BodyHolder_ddlInspector').val() == '0')
                alertMsg += "\nInspector";
            if ($('#BodyHolder_ddlCertifier').val() == '0')
                alertMsg += "\nCertifier";
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---')
                alertMsg += "\nMode of Disposal";
            if (ddlconditioncounter != 0)
                alertMsg += "\nCondition";
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
                        url: url + "/PropertyDisposal.aspx/ConfirmPropertyDisposal",
                        data: "{ approverid : '" + approver + "',inspectorid: '0', certifierid: '0',modeofdisposal: '" + $('#BodyHolder_ddlModeOfDisposal').val() + "', remarks : '" + $('#BodyHolder_txtRemarks').val() + "', custodianid : '" + custodian + "', ExpenseType : '" + ExpenseType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested For Disposal and Pending For Approval/Upload.\n\nMail not sent. Please check you mail settings.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Approval.\n\nMail not sent. Check you mail settings.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else if (msg.d == "okay") {
                                //alert('Successfully Requested For Disposal and Pending For Approval/Upload');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Approval.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested For Disposal and Pending For Approval/Upload.\n\nMail not sent. Approver has no email.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Approval.\n\nMail not sent. Approver has no email.", "success");
                                load_data();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
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
        if (ddlconditioncounter != 0 || $('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---' || $('#BodyHolder_txtRemarks').val().trim() == "") {
            alertMsg = ' ';
            if ($('#BodyHolder_txtRemarks').val() == "")
                alertMsg += "\nRemarks ";
            if ($('#BodyHolder_ddlModeOfDisposal').val() == '--- Select Mode of Disposal ---')
                alertMsg += "\nMode of Disposal";
            if (ddlconditioncounter != 0)
                alertMsg += "\nCondition";
            //alert(alertMsg);
            swal("Fill-out all the required field(s): ", alertMsg, "warning");
            $('.btn').prop('disabled', false);
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
                        url: url + "/PropertyDisposal.aspx/ConfirmPropertyDisposal",
                        data: "{approverid : '0',inspectorid: '0', certifierid: '0',modeofdisposal: '" + $('#BodyHolder_ddlModeOfDisposal').val() + "',  remarks : '" + $('#BodyHolder_txtRemarks').val() + "', custodianid : '" + custodian + "' , ExpenseType : '" + ExpenseType + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "sorry") {
                                //alert('Successfully Requested for Disposal.\n\nMail not sent. Please check you mail settings at SYSTEM > MISCELLANEOUS.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Verification.", "success");
                                load_data();
                                //load_data_disposallist();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
                                $('.btn').prop('disabled', false);
                            }
                            else {
                                //alert('Successfully Requested for Disposal.');
                                swal("Successful", "Successfully Requested For Disposal and Pending For Verification.", "success");
                                load_data();
                                //load_data_disposallist();
                                load_data_Pendingupload();
                                GetCount(ExpenseType);
                                $('#disposalList').modal('hide');
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

function load_data_Pendingupload() {
    //var custodian = $('#ddlCustodian').val();
    var search = "";
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val();
    $.ajax({
        type: 'POST',
        url: url + "/PropertyDisposal.aspx/GetPendingUploadList",
        data: "{ search : '" + search + "', datefrom : '" + datefrom + "', dateto: '" + dateto + "'}",
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


        var mytable = $('#tblPendingUpload').DataTable({
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
                { "data": "DisposalNumber" },
                { "data": "Paricsword" },
                { "data": "TransactorName" },
                { "data": "ModeOfDisposal" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                          },
                           {
                               extend: 'colvis',
                               text: 'COLUMN VISIBILITY',
                               className: 'btn btn-primary', exportOptions: { columns: ':visible' }
                           }
                    ],
                    dropup: false, //btn btn-sm btn-success
                },
                { //Deselect all current data and then selects all row with TypeCost PAR
                    text: 'Select PAR',
                    action: function () {
                        $('#tblPendingUpload').DataTable().rows().deselect();

                        $('#tblPendingUpload').DataTable().rows(':contains("PAR")').select()

                    }
                },//Deselect all current data and then selects all row with TypeCost ICS HIGH AND ICS LOW
                                {
                                    text: 'Select ICS',
                                    action: function () {
                                        $('#tblPendingUpload').DataTable().rows().deselect();

                                        $('#tblPendingUpload').DataTable().rows(':contains("ICS LOW"), :contains("ICS HIGH")').select()

                                    }
                                },
                {
                    text: 'Select none',
                    action: function () {
                        $('#tblPendingUpload').DataTable().rows().deselect();
                        document.getElementById("printGrid1").children[0].style.display = "block";
                        document.getElementById("printGrid1").children[1].style.display = "block";
                        document.getElementById("printGrid1").children[2].style.display = "block";
                    }
                },
                ]
            }, colReorder: true
             , stateSave: true
        });


        var icslow = false;
        var icshigh = false;
        var par = false;

        mytable.on('select', function (e, dt, type, indexes) {

            icslow = false;
            icshigh = false;
            par = false;

            var count = $('#tblPendingUpload').DataTable().rows({ selected: true }).count();

            var data = mytable.rows({ selected: true }).data().toArray();
            console.log(data);
            var list = "";
            var propdisid = [];
            data.forEach((timex, index) => {
                list += timex.Paricsword + ",";
                //propdisid += timex.PropertyDisposalID + ",";
                propdisid.push(timex.PropertyDisposalID);
                prodisidstr = propdisid.join(',');


                //alert(timex.PropertyDisposalID);

                if (timex.Paricsword == "ICS HIGH") {
                    icshigh = true;
                }
                else if (timex.Paricsword == "ICS LOW") {
                    icslow = true;
                }
                else if (timex.Paricsword == "PAR") {
                    par = true;
                }
            });

            //alert(list + ' || ICS High: ' + icshigh + ' || ICS Low: ' + icslow + ' || PAR: ' + par);

            if (par == true && icshigh == false && icslow == false) {
                document.getElementById("printGrid1").children[0].style.display = "inline";
                document.getElementById("printGrid1").children[1].style.display = "none";
                document.getElementById("printGrid1").children[2].style.display = "none";
                excelcount = 0;

            }
            else if (par == false && icslow == true && icshigh == false || par == false && icshigh == true && icslow == false || par == false && icshigh == true && icslow == true) {
                document.getElementById("printGrid1").children[0].style.display = "none";
                document.getElementById("printGrid1").children[1].style.display = "inline";
                document.getElementById("printGrid1").children[2].style.display = "none";
                excelcount = 0;

            }
            else if (par == true && icslow == true && icshigh == false || par == true && icshigh == true && icslow == false || par == true && icshigh == true && icslow == true) {
                document.getElementById("printGrid1").children[0].style.display = "none";
                document.getElementById("printGrid1").children[1].style.display = "none";
                document.getElementById("printGrid1").children[2].style.display = "inline";
                excelcount = 0;

            }
            else {
                excelcount = 1;
            }
        });




        mytable.on('deselect', function (e, dt, type, indexes) {

            icslow = false;
            icshigh = false;
            par = false;

            var data = mytable.rows({ selected: true }).data().toArray();
            console.log(data);
            var list = "";
            var disposalid = "";
            data.forEach((timex, index) => {
                list += timex.Paricsword + ",";


                if (timex.Paricsword == "ICS HIGH") {
                    icshigh = true;
                }
                else if (timex.Paricsword == "ICS LOW") {
                    icslow = true;
                }
                else if (timex.Paricsword == "PAR") {
                    par = true;
                }
            });

            //alert(list + ' || ICS High: ' + icshigh + ' || ICS Low: ' + icslow + ' || PAR: ' + par);

            if (par == true && icshigh == false && icslow == false) {
                document.getElementById("printGrid1").children[0].style.display = "inline";
                document.getElementById("printGrid1").children[1].style.display = "none";
                document.getElementById("printGrid1").children[2].style.display = "none";
                excelcount = 0;

            }
            else if (par == false && icslow == true && icshigh == false || par == false && icshigh == true && icslow == false || par == false && icshigh == true && icslow == true) {
                document.getElementById("printGrid1").children[0].style.display = "none";
                document.getElementById("printGrid1").children[1].style.display = "inline";
                document.getElementById("printGrid1").children[2].style.display = "none";
                excelcount = 0;

            }
            else if (par == true && icslow == true && icshigh == false || par == true && icshigh == true && icslow == false || par == true && icshigh == true && icslow == true) {
                document.getElementById("printGrid1").children[0].style.display = "none";
                document.getElementById("printGrid1").children[1].style.display = "none";
                document.getElementById("printGrid1").children[2].style.display = "inline";
                excelcount = 0;

            }
            else {
                excelcount = 1;
            }
        });

        var ptable = $('#tblPendingUpload').DataTable();
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
                        url: url + "/PropertyDisposal.aspx/CancelPropertyDisposal",
                        data: "{disposalno: '" + disposalnumber + "', remarks: '" + textremarks + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#pendinguploadlist').hide();
                            $('#pendinguploadlist').innerHTML = "";
                            $('#pendinguploadlist').fadeIn(200);
                            load_data();
                            currentRow.remove();
                            //alert('Property Disposal Transaction successfully cancelled.');
                            swal("Successful", "Property Disposal Transaction successfully cancelled.", "success");
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




    //$('#tblPendingUpload tbody').off('click').on('click', 'tr', function () {

    //    var ids = $.map($('#tblPendingUpload').DataTable().rows('.selected').data(), function (item) {
    //        return item.DisposalNumber;
    //    });
    //    console.log(ids);

    //    alert(ids);

    //    //var data = ptable.row(this).data();
    //    //var currentRow = $(this).closest("tr");
    //    //var currentcolumn = currentRow.find('td');
    //    //var currentValue = currentcolumn.find('p');


    //    //if (ptempWIPH == currentValue.attr('precid')) {
    //    //    precId = '0';
    //    //    ptempWIPH = "";

    //    //} else if (currentValue.attr('precid') == null) {
    //    //    precId = '0';
    //    //    ptempWIPH = "";
    //    //}
    //    //else {
    //    //    ptempWIPH = currentValue.attr('precid');
    //    //    precId = currentValue.attr('precid');
    //    //    ppnum = currentValue.attr('ppnum');
    //    //    pserial = currentValue.attr('pserial');
    //    //    pdesc = currentValue.attr('pdesc');
    //    //    pcat = currentValue.attr('pcat');
    //    //    pclassi = currentValue.attr('pclassi');

    //    //}
    //});
}

var disposalnumber = '';
function ViewDisposalDetails(disposalno, transactorname, createddate, isapproved, counter, counterchecker, status, PARICS) {
    disposalnumber = disposalno;
    document.getElementById('lblDisposalNo').innerHTML = disposalno;
    document.getElementById('lblDisposedBy').innerHTML = transactorname
    document.getElementById('lblDisposedDate').innerHTML = createddate;
    $('#BodyHolder_txtDisposalNumber').val(disposalno);


    $('#btndownloadcreatepdf').attr('transno', disposalno);
    $('#btndownloadcreatepdf').attr('transactorname', transactorname);
    $('#btndownloadcreatepdf').attr('createddate', createddate);
    $('#btndownloadcreatepdf').attr('status', status);

    $('#btndownloadpdf').attr('transno', disposalno);
    $('#btndownloadpdf').attr('transactorname', transactorname);
    $('#btndownloadpdf').attr('createddate', createddate);
    $('#btndownloadpdf').attr('status', status);

    var x = document.getElementById("showorhide");
    x.style.display = "none";
    if ((isapproved == "True") && status == "Pending Upload") {
        if (counter == counterchecker) {
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
        //if (counter == counterchecker) {
        //    load_data_PUL1();
        //    $('#ALPendingUpload').modal('show');
        //    x.style.display = "inline";
        //}
        //else {
        //    load_data_PUL1();
        //    $('#ALPendingUpload').modal('show');
        //    x.style.display = "none";
        //}

        load_data_PUL1();
        $('#ALPendingUpload').modal('show');
        x.style.display = "none";
    }

    if (PARICS == "1") {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "inline";

        document.getElementById("printGrid").children[6].style.display = "inline";
        document.getElementById("printGrid").children[7].style.display = "none";
        document.getElementById("printGrid").children[8].style.display = "none";
        document.getElementById("printGrid").children[9].style.display = "inline";
        document.getElementById("printGrid").children[10].style.display = "inline";
        //document.getElementById("printGrid").children[11].style.display = "inline";

        //document.getElementById("printGrid").children[12].style.display = "inline";
        //document.getElementById("printGrid").children[13].style.display = "none";
        //document.getElementById("printGrid").children[14].style.display = "none";
        //document.getElementById("printGrid").children[15].style.display = "inline";
        //document.getElementById("printGrid").children[16].style.display = "inline";
    }
    else if (PARICS == "2" || PARICS == "3") {
        document.getElementById("printGrid").children[0].style.display = "none";
        document.getElementById("printGrid").children[1].style.display = "none";
        document.getElementById("printGrid").children[2].style.display = "none";
        document.getElementById("printGrid").children[3].style.display = "none";
        document.getElementById("printGrid").children[4].style.display = "none";
        document.getElementById("printGrid").children[5].style.display = "inline";

        document.getElementById("printGrid").children[6].style.display = "none";
        document.getElementById("printGrid").children[7].style.display = "inline";
        document.getElementById("printGrid").children[8].style.display = "inline";
        document.getElementById("printGrid").children[9].style.display = "none";
        document.getElementById("printGrid").children[10].style.display = "inline";
        //document.getElementById("printGrid").children[11].style.display = "inline";

        //document.getElementById("printGrid").children[12].style.display = "none";
        //document.getElementById("printGrid").children[13].style.display = "inline";
        //document.getElementById("printGrid").children[14].style.display = "inline";
        //document.getElementById("printGrid").children[15].style.display = "none";
        //document.getElementById("printGrid").children[16].style.display = "inline";
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
        //document.getElementById("printGrid").children[11].style.display = "none";

        //document.getElementById("printGrid").children[12].style.display = "none";
        //document.getElementById("printGrid").children[13].style.display = "none";
        //document.getElementById("printGrid").children[14].style.display = "none";
        //document.getElementById("printGrid").children[15].style.display = "none";
        //document.getElementById("printGrid").children[16].style.display = "none";
    }

    //gheghe(Certificate/Word)dren[6].style.display = "none";
    //document.getElementById("printGrid").children[7].style.display = "none";
    //document.getElementById("printGrid").children[8].style.display = "none";
    //document.getElementById("printGrid").children[9].style.display = "none";
    //document.getElementById("printGrid").children[10].style.display = "none";
    //document.getElementById("printGrid").children[
    //document.getElementById("printGrid").chil11].style.display = "none";

    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReport",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_divDisposalReport').hide();
            $('#BodyHolder_divDisposalReport').html(msg.d);
            $('#BodyHolder_divDisposalReport').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportIIRUP",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //alert(1);
            //  console.log(msg.d);
            $('#BodyHolder_divDisposalReportIIRUP').hide();
            $('#BodyHolder_divDisposalReportIIRUP').html(msg.d);
            $('#BodyHolder_divDisposalReportIIRUP').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportIIRSEP",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //   console.log(msg.d);
            $('#BodyHolder_divDisposalReportIIRSEP').hide();
            $('#BodyHolder_divDisposalReportIIRSEP').html(msg.d);
            $('#BodyHolder_divDisposalReportIIRSEP').fadeIn(200);
        }
    });

    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportWMR",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //    console.log(msg.d);
            $('#BodyHolder_divTurnInWMR').hide();
            $('#BodyHolder_divTurnInWMR').html(msg.d);
            $('#BodyHolder_divTurnInWMR').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportROP",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //console.log(msg.d);
            $('#BodyHolder_divTurnInROP').hide();
            $('#BodyHolder_divTurnInROP').html(msg.d);
            $('#BodyHolder_divTurnInROP').fadeIn(200);
        }
    });
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportROSEP",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // console.log(msg.d);
            $('#BodyHolder_divTurnInROSEP').hide();
            $('#BodyHolder_divTurnInROSEP').html(msg.d);
            $('#BodyHolder_divTurnInROSEP').fadeIn(200);
        }
    });
}

function load_data_PUL1() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyDisposal.aspx/GetPLPendingUploadList",
        data: "{disposalno: '" + disposalnumber + "'}",
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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

var disposalnumberrequest = '';
function ViewDisposalDetailsRequest(disposalno, transactorname, createddate, isapproved, counter, counterchecker, status, PARICS) {
    disposalnumberrequest = disposalno;
    document.getElementById('lblDisposalNoRequest').innerHTML = disposalno;
    document.getElementById('lblDisposedByRequest').innerHTML = transactorname
    document.getElementById('lblDisposedDateRequest').innerHTML = createddate;
    $('#BodyHolder_txtDisposalNumberRequest').val(disposalno);
    $('#BodyHolder_FileUploadRequest').val('');

    if (PARICS == "1") {
        document.getElementById("printGrid2").children[0].style.display = "inline";
        document.getElementById("printGrid2").children[1].style.display = "none";
        document.getElementById("printGrid2").children[2].style.display = "inline";

        document.getElementById("printGrid2").children[3].style.display = "inline";
        document.getElementById("printGrid2").children[4].style.display = "none";
        document.getElementById("printGrid2").children[5].style.display = "inline";

    }
    else if (PARICS == "2" || PARICS == "3") {
        document.getElementById("printGrid2").children[0].style.display = "none";
        document.getElementById("printGrid2").children[1].style.display = "inline";
        document.getElementById("printGrid2").children[2].style.display = "inline";

        document.getElementById("printGrid2").children[3].style.display = "none";
        document.getElementById("printGrid2").children[4].style.display = "inline";
        document.getElementById("printGrid2").children[5].style.display = "inline";
    }
    else {
        document.getElementById("printGrid2").children[0].style.display = "none";
        document.getElementById("printGrid2").children[1].style.display = "none";
        document.getElementById("printGrid2").children[2].style.display = "none";

        document.getElementById("printGrid2").children[3].style.display = "none";
        document.getElementById("printGrid2").children[4].style.display = "none";
        document.getElementById("printGrid2").children[5].style.display = "none";


    }

    var x = document.getElementById("showorhide1");
    x.style.display = "none";
    if ((isapproved == "True") && status == "Pending Upload") {
        if (counter == counterchecker) {
            load_data_PULRequest();
            $('#ALPendingUploadRequest').modal('show');
            x.style.display = "inline";
        }
        else {
            load_data_PULRequest();
            $('#ALPendingUploadRequest').modal('show');
            x.style.display = "none";
        }
    }
    else {
        //if (counter == counterchecker) {
        //    load_data_PUL1();
        //    $('#ALPendingUpload').modal('show');
        //    x.style.display = "inline";
        //}
        //else {
        //    load_data_PUL1();
        //    $('#ALPendingUpload').modal('show');
        //    x.style.display = "none";
        //}

        load_data_PULRequest();
        $('#ALPendingUploadRequest').modal('show');
        x.style.display = "none";
    }

    //$.ajax({
    //    type: "POST",
    //    url: url + "/PropertyDisposal.aspx/GenerateDisposalReportIIRUPRequest",
    //    data: "{disposalno: '" + disposalno + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        //  console.log(msg.d);
    //        $('#BodyHolder_divDisposalReportIIRUP1').hide();
    //        $('#BodyHolder_divDisposalReportIIRUP1').html(msg.d);
    //        $('#BodyHolder_divDisposalReportIIRUP1').fadeIn(200);
    //    }
    //});
    //$.ajax({
    //    type: "POST",
    //    url: url + "/PropertyDisposal.aspx/GenerateDisposalReportIIRSEPRequest",
    //    data: "{disposalno: '" + disposalno + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {

    //        //   console.log(msg.d);
    //        $('#BodyHolder_divDisposalReportIIRSEP1').hide();
    //        $('#BodyHolder_divDisposalReportIIRSEP1').html(msg.d);
    //        $('#BodyHolder_divDisposalReportIIRSEP1').fadeIn(200);
    //    }
    //});
    //g
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportROSEPRequest",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // console.log(msg.d);
            $('#BodyHolder_divTurnInROSEP1').hide();
            $('#BodyHolder_divTurnInROSEP1').html(msg.d);
            $('#BodyHolder_divTurnInROSEP1').fadeIn(200);
        }
    });
    //g
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportROPRequest",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //console.log(msg.d);
            $('#BodyHolder_divTurnInROP1').hide();
            $('#BodyHolder_divTurnInROP1').html(msg.d);
            $('#BodyHolder_divTurnInROP1').fadeIn(200);
        }
    });
    //$.ajax({
    //    type: "POST",
    //    url: url + "/PropertyDisposal.aspx/GenerateDisposalReportRequest",
    //    data: "{disposalno: '" + disposalno + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        $('#BodyHolder_divDisposalReport1').hide();
    //        $('#BodyHolder_divDisposalReport1').html(msg.d);
    //        $('#BodyHolder_divDisposalReport1').fadeIn(200);
    //    }
    //});
    //g
    $.ajax({
        type: "POST",
        url: url + "/PropertyDisposal.aspx/GenerateDisposalReportWMRRequest",
        data: "{disposalno: '" + disposalno + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //    console.log(msg.d);
            $('#BodyHolder_divTurnInWMR1').hide();
            $('#BodyHolder_divTurnInWMR1').html(msg.d);
            $('#BodyHolder_divTurnInWMR1').fadeIn(200);
        }
    });
}


function load_data_PULRequest() {
    $.ajax({
        type: 'POST',
        url: url + "/PropertyDisposal.aspx/GetPLPendingUploadListRequest",
        data: "{disposalno: '" + disposalnumberrequest + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pul1Request').hide();
            $('#pul1Request').innerHTML = "";
            $('#pul1Request').fadeIn(200);

            myJsonData = data;

            populateDataTablePULRequest(myJsonData);

            console.log(data);
            console.log('Success');
        },
        error: function (e) {
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function populateDataTablePULRequest(data) {
    try {
        $('#tblPUL1Request').DataTable({
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal Request'
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

        $('#tblPUL1Request').off('click');
    } catch (e) {
        var table = $('#tblPUL1Request').DataTable();
        table
            .clear()
            .draw();
    }
}

function UploadPropertyDisposalRequest() {
    var alertMsg;
    var disposalno = $('#BodyHolder_txtDisposalNumberRequest').val();
    var filess = $('#BodyHolder_FileUploadRequest').val();
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
                    var fileUpload = $("#BodyHolder_FileUploadRequest").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + disposalno + "|PropertyDisposalRequest",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyDisposal.aspx/UploadDocumentsDisposalRequest",
                                    data: " { disposalno : '" + disposalno + "'} ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        load_data_PendinguploadRequest();
                                        $('#ALPendingUploadRequest').modal('hide');
                                        //swal("Successful", "Transaction Completed with Supporting Document!", "success");
                                        swal({
                                            title: "Transaction Completed with Supporting Document!",
                                            icon: "success",
                                        }).then((value) => {
                                            load_data_PendinguploadRequest();
                                        });
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



function UploadPropertyDisposal() {
    var alertMsg;
    var disposalno = $('#BodyHolder_txtDisposalNumber').val();
    var filess = $('#BodyHolder_AFupload').val();
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
                    var fileUpload = $("#BodyHolder_AFupload").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + disposalno + "|PropertyDisposal",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $.ajax({
                                    type: "POST",
                                    url: url + "/PropertyDisposal.aspx/UploadDocumentsDisposal",
                                    data: " { disposalno : '" + disposalno + "'} ",
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



function load_data_PendinguploadRequest() {
    //var custodian = $('#ddlCustodian').val();
    var search = "";
    $.ajax({
        type: 'POST',
        url: url + "/PropertyDisposal.aspx/GetPendingUploadList1",
        data: "{ search : '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#pendinguploadlist1').hide();
            $('#pendinguploadlist1').innerHTML = "";
            $('#pendinguploadlist1').fadeIn(200);

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
                { "data": "DisposalNumber" },
                { "data": "TransactorName" },
                { "data": "ModeOfDisposal" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Disposal'
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

        $('#tblPendingUpload1').on('click', 'a.btn_Cancel1', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var disposalnumber = currentRow.find("td:eq(0)").text();
            swal({
                title: "Are you sure?",
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
                        url: url + "/PropertyDisposal.aspx/CancelPropertyDisposal1",
                        data: "{disposalno: '" + disposalnumber + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#pendinguploadlist1').hide();
                            $('#pendinguploadlist1').innerHTML = "";
                            $('#pendinguploadlist1').fadeIn(200);
                            //load_dataPDisposalRequest();
                            currentRow.remove();
                            //alert('Property Disposal Transaction successfully cancelled.');
                            swal("Successful", "Property Disposal Transaction successfully cancelled.", "success");
                        }
                    });
                } else {
                    swal("Cancelled", "Cancelled by user", "warning");
                }
            });
        });
    } catch (e) {
        var table = $('#tblPendingUpload1').DataTable();
        table
            .clear()
            .draw();
    }
}
function Clear() { }
