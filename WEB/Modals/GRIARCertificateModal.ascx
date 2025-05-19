<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GRIARCertificateModal.ascx.cs" Inherits="Modals_GRIARCertificateModal" %>

<script type="text/javascript">

    var imagebase64 = "";
    var certbase64 = "";

    function togglePass() {
        var password = document.getElementById('pkipassword');
        var togglepass = document.getElementById('togglePassword');
        var type = password.getAttribute('type');
        if (type == 'password') {
            password.setAttribute('type', 'text')
            togglepass.classList.remove("fa-eye");
            togglepass.classList.add("fa-eye-slash");
        }
        else {
            password.setAttribute('type', 'password')
            togglepass.classList.remove("fa-eye-slash");
            togglepass.classList.add("fa-eye");
        }
    }

    $(document).ready(function () {

        //Goods Receive
        $('#iarbtnv2').off('click').on("click", function () {
            openCertModal();
        });

        //Property Issuance - ICS
        $('#icsbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'ICS');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyIssuance');
            openCertModal();
        });

        //Property Issuance - RIS
        $('#risbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RIS');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyIssuance');
            openCertModal();
        });

        //Property Issuance (Request) - ICS
        $('#icsrequestbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'ICS');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyRequest');
            openCertModal();
        });

        //Property Issuance (Request) - ITR
        $('#itrequestbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'ITR');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyRequest');
            openCertModal();
        });
        //Property Issuance (Request) - RIS
        $('#risrequestbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RIS');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyRequest');
            openCertModal();
        });

        //Stock Issuance - RIS
        $('#stockrisbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RIS');
            $('#btndownloadcreatepdf').attr('operationtype', 'StockIssuance');
            openCertModal();
        });

        //Stock Issuance - ITR
        $('#stockitrbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'ITR');
            $('#btndownloadcreatepdf').attr('operationtype', 'StockIssuance');
            openCertModal();
        });

        //Stock Issuance (Request) - RIS
        $('#stockrequestrisbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RIS');
            $('#btndownloadcreatepdf').attr('operationtype', 'StockRequest');
            openCertModal();
        });

        //Property Request - RIS
        $('#propertyrequestrisbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RIS');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyRequest');
            openCertModal();
        });

        //Property Return - TIS
        $('#turninreturnbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'TIS');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyReturn');
            openCertModal();
        });

        //Property Return - RSEP
        $('#rrspinreturnbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RSEP');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyReturn');
            openCertModal();
        });

        //Property Return - Turn In
        $('#returnrequestturninbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'Turn In');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyReturnRequest');
            openCertModal();
        });
        //Property Return - Returned
        $('#returnrequestrrspinbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'Returned');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyReturnRequest');
            openCertModal();
        });

        //Gate Pass Request
        $('#gatepassbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'GatePass');
            $('#btndownloadcreatepdf').attr('operationtype', 'GatePassRequest');
            openCertModal();
        });

        //Property Disposal - Waste Material
        $('#btnPrintGridDataWMR').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'WasteMaterial');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyDisposal');
            openCertModal();
        });
        //Property Disposal - Disposal
        $('#btnbtnPrintGridData').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'DisposalForm');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyDisposal');
            openCertModal();
        });
        //Property Disposal - Report of Lost
        $('#btnPrintGridDataROSEP').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'ROSEP');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyDisposal');
            openCertModal();
        });
        //Property Disposal - RSEP
        $('#btnPrintGridDataIIRSEP').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'RSEP');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyDisposal');
            openCertModal();
        });

        //Stock Disposal - Disposal Form
        $('#disposalbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'Disposal Form');
            $('#btndownloadcreatepdf').attr('operationtype', 'StockDisposal');
            openCertModal();
        });
        //Stock Disposal - WasteMaterial
        $('#wastematbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'WasteMaterial');
            $('#btndownloadcreatepdf').attr('operationtype', 'StockDisposal');
            openCertModal();
        });


        //Property Transfer - ITR
        $('#transferitrbtn').off('click').on('click', function () {
            $('#btndownloadcreatepdf').attr('transtype', 'ITR');
            $('#btndownloadcreatepdf').attr('operationtype', 'PropertyTransfer');
            openCertModal();
        });

        //RSMI - RSMI
        //$('#rsmibtn').off('click').on('click', function () {
        //    $('#btndownloadcreatepdf').attr('transtype', 'RSMI');
        //    $('#btndownloadcreatepdf').attr('operationtype', 'RSMI');
        //    openCertModal();
        //});

        function openCertModal() {
            clearModal();
            $('#sigandCertModalGRAIR').modal('show');
        }

        function clearModal() {
            imagebase64 = "";
            certbase64 = "";
            $('#pkipassword').val('');
            $('#signatureupload').val('');
            $('#pkicertificateupload').val('');
            $('#datestamp').prop('checked', false);
            var password = document.getElementById('pkipassword');
            var togglepass = document.getElementById('togglePassword');
            var type = password.getAttribute('type');
            password.setAttribute('type', 'password')
            togglepass.classList.remove("fa-eye-slash");
            togglepass.classList.add("fa-eye");
        }

        $('#signatureupload').off('change').on("change", function () {
            signaturereadURL(this);
        });

        $('#pkicertificateupload').off('change').on("change", function () {
            certificatereadURL(this);
        });

        $('#btndownloadcreatepdf').off('click').on('click', function () {
            try {
                var transnum = $(this).attr('transno');
                var transtype = $(this).attr('transtype');
                var operationtype = $(this).attr('operationtype');
                var datestamp = $('#datestamp');
                var alertMsg = "Fill-out all the required field(s):\n";
                var hasError = false;
                var pkipassword = $('#pkipassword');
                if (pkipassword.val() == '') {
                    hasError = true;
                    alertMsg += "\nNo Pass Phrase";
                }
                if (imagebase64 == '') {
                    hasError = true;
                    alertMsg += "\nNo image selected.";
                }
                if (certbase64 == '') {
                    hasError = true;
                    alertMsg += "\nNo certificate selected.";
                }
                if (hasError) {
                    SweetAlertWarning(alertMsg);
                    return;
                }
                if (operationtype == 'GoodsReceive' && transtype == 'IAR') {
                    createGRIAR(transnum, transtype, operationtype, datestamp);
                } else if (operationtype == 'PropertyIssuance' && transtype == 'ICS') {
                    createICSPI(transnum, transtype, operationtype, datestamp);
                } else if (operationtype == 'PropertyIssuance' && transtype == 'RIS') {
                    createRISPI(transnum, transtype, operationtype, datestamp);
                } else if (operationtype == 'PropertyRequest' && transtype == 'ICS') {
                    createICSPR(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyRequest' && transtype == 'ITR') {
                    createITRPR(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyRequest' && transtype == 'RIS') {
                    createRISPR(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'StockIssuance' && transtype == 'RIS') {
                    createRISSI(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'StockIssuance' && transtype == 'ITR') {
                    createITRSI(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'StockRequest' && transtype == 'RIS') {
                    createRISSIR(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyReturn' && transtype == 'TIS') {

                    var employeename = $(this).attr('employeename');
                    var daterequested = $(this).attr('daterequested');

                    var Remarks = $(this).attr('Remarks');
                    var EmployeePosition = $(this).attr('EmployeePosition');

                    var EmployeeDepartment = $(this).attr('EmployeeDepartment');
                    var CustodianPosition = $(this).attr('CustodianPosition');

                    var CustodianDepartment = $(this).attr('CustodianDepartment');
                    var CustodianName = $(this).attr('CustodianName');

                    createPRTIS(transnum, transtype, operationtype, datestamp, employeename, daterequested, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName);
                }
                else if (operationtype == 'PropertyReturn' && transtype == 'RSEP') {

                    var employeename = $(this).attr('employeename');
                    var daterequested = $(this).attr('daterequested');

                    var Remarks = $(this).attr('Remarks');
                    var EmployeePosition = $(this).attr('EmployeePosition');

                    var EmployeeDepartment = $(this).attr('EmployeeDepartment');
                    var CustodianPosition = $(this).attr('CustodianPosition');

                    var CustodianDepartment = $(this).attr('CustodianDepartment');
                    var CustodianName = $(this).attr('CustodianName');

                    createPRRSEP(transnum, transtype, operationtype, datestamp, employeename, daterequested, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName);
                }
                else if (operationtype == 'PropertyReturnRequest' && transtype == 'Turn In') {

                    var employeename = $(this).attr('employeename');
                    var daterequested = $(this).attr('daterequested');

                    var Remarks = $(this).attr('Remarks');
                    var EmployeePosition = $(this).attr('EmployeePosition');

                    var EmployeeDepartment = $(this).attr('EmployeeDepartment');
                    var CustodianPosition = $(this).attr('CustodianPosition');

                    var CustodianDepartment = $(this).attr('CustodianDepartment');
                    var CustodianName = $(this).attr('CustodianName');

                    createPRTurnIn(transnum, transtype, operationtype, datestamp, employeename, daterequested, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName);
                }
                else if (operationtype == 'PropertyReturnRequest' && transtype == 'Returned') {

                    var employeename = $(this).attr('employeename');
                    var daterequested = $(this).attr('daterequested');

                    var Remarks = $(this).attr('Remarks');
                    var EmployeePosition = $(this).attr('EmployeePosition');

                    var EmployeeDepartment = $(this).attr('EmployeeDepartment');
                    var CustodianPosition = $(this).attr('CustodianPosition');

                    var CustodianDepartment = $(this).attr('CustodianDepartment');
                    var CustodianName = $(this).attr('CustodianName');

                    createPRReturned(transnum, transtype, operationtype, datestamp, employeename, daterequested, Remarks, EmployeePosition, EmployeeDepartment, CustodianPosition, CustodianDepartment, CustodianName);
                }
                else if (operationtype == 'GatePassRequest' && transtype == 'GatePass') {
                    createGatePass(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyDisposal' && transtype == 'WasteMaterial') {
                    createPDWasteMaterial(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyDisposal' && transtype == 'DisposalForm') {
                    createPDDisposalForm(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyDisposal' && transtype == 'ROSEP') {
                    createPDROSEP(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyDisposal' && transtype == 'RSEP') {
                    createPDRSEP(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'StockDisposal' && transtype == 'Disposal Form') {
                    createSDDisposalForm(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'StockDisposal' && transtype == 'WasteMaterial') {
                    createSDWasteMaterial(transnum, transtype, operationtype, datestamp);
                }
                else if (operationtype == 'PropertyTransfer' && transtype == 'ITR') {
                    createPRITR(transnum, transtype, operationtype, datestamp);
                }
                //else if (operationtype == 'RSMI' && transtype == 'RSMI') {
                //    var departmentid = deptiD;
                //    var datefrom = $('#txtDateFrom').val();
                //    var dateto = $('#txtDateTo').val();
                //    createRSMIRSMI(transnum, transtype, operationtype, datestamp, departmentid, datefrom, dateto);
                //}
            } catch (e) {
                console.log(e);
            }
        });

        function createPRITR(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyTransfer.aspx/CreateITRHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createRSMIRSMI(transnum, transtype, operationtype, datestamp, departmentid, datefrom, dateto) {
            $.ajax({
                type: "POST",
                url: "Views/RSMI.aspx/CreatePDFRSMI",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "',departmentid: '" + departmentid + "', datefrom: '" + datefrom + "', dateto: '" + dateto + "'  }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createSDWasteMaterial(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/StockDisposal.aspx/CreateSDWasteMaterial",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createSDDisposalForm(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/StockDisposal.aspx/CreateSDDisposalForm",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPDRSEP(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposal.aspx/CreatePDRSEPHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPDROSEP(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposal.aspx/CreatePDROSEPHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPDDisposalForm(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposal.aspx/CreatePDDisposalFormHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPDWasteMaterial(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyDisposal.aspx/CreatePDWasteMaterialHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createGatePass(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyGatePassRequest.aspx/CreateGatePassHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPRReturned(transnum, transtype, operationtype, datestamp,
          employeename,
          daterequested,
          Remarks,
          EmployeePosition,
          EmployeeDepartment,
          CustodianPosition,
          CustodianDepartment,
          CustodianName) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturn.aspx/CreateReturnedHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "',employeename: '" +
                    employeename + "', daterequested: '" + daterequested + "', Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "', EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "', CustodianDepartment: '" + CustodianDepartment + "', CustodianName: '" + CustodianName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPRTurnIn(transnum, transtype, operationtype, datestamp,
          employeename,
          daterequested,
          Remarks,
          EmployeePosition,
          EmployeeDepartment,
          CustodianPosition,
          CustodianDepartment,
          CustodianName) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturn.aspx/CreatePDFTurnInHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "',employeename: '" +
                    employeename + "', daterequested: '" + daterequested + "', Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "', EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "', CustodianDepartment: '" + CustodianDepartment + "', CustodianName: '" + CustodianName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPRRSEP(transnum, transtype, operationtype, datestamp,
           employeename,
           daterequested,
           Remarks,
           EmployeePosition,
           EmployeeDepartment,
           CustodianPosition,
           CustodianDepartment,
           CustodianName) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturn.aspx/CreateRSEPHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "',employeename: '" +
                    employeename + "', daterequested: '" + daterequested + "', Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "', EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "', CustodianDepartment: '" + CustodianDepartment + "', CustodianName: '" + CustodianName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createPRTIS(transnum, transtype, operationtype, datestamp,
            employeename,
            daterequested,
            Remarks,
            EmployeePosition,
            EmployeeDepartment,
            CustodianPosition,
            CustodianDepartment,
            CustodianName) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyReturn.aspx/CreatePDFTurnInReturnHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "',employeename: '" +
                    employeename + "', daterequested: '" + daterequested + "', Remarks: '" + Remarks + "',EmployeePosition: '" + EmployeePosition + "', EmployeeDepartment: '" + EmployeeDepartment + "',CustodianPosition: '" + CustodianPosition + "', CustodianDepartment: '" + CustodianDepartment + "', CustodianName: '" + CustodianName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }
        function createRISSIR(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/StockIssuance.aspx/CreateRequestRISHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createITRSI(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/StockIssuance.aspx/CreateITRHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createRISSI(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/StockIssuance.aspx/CreateRISHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createRISPR(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyIssuance.aspx/CreateRISRequestHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createITRPR(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyIssuance.aspx/CreateITRRequestHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }
        function createICSPR(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyIssuance.aspx/CreateICSRequestHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createRISPI(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyIssuance.aspx/CreateRISHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createICSPI(transnum, transtype, operationtype, datestamp) {
            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyIssuance.aspx/CreateICSHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function createGRIAR(transnum, transtype, operationtype, datestamp) {

            $.ajax({
                type: "POST",
                url: "Operation_Module/GoodsReceive.aspx/CreateIARHTML",
                data: "{ issuanceno: '" + transnum + "', imagebase64: '" + imagebase64 + "', passphrase: '" + $('#pkipassword').val() + "', certbase64: '" + certbase64 + "', datestamp: '" + datestamp.prop('checked') + "', transtype: '" + transtype + "', operationtype: '" + operationtype + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d == 'Error') {
                        SweetAlertError("An error occured.");
                        clearModal();
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
                            a.attr("download", operationtype + ".pdf");
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                            ClearSignature();
                        }
                    }
                }, error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr);
                    console.log(status);
                    SweetAlertError("An error occured.");
                    clearModal();
                }
            });
        }

        function signaturereadURL(input) {
            if (input.files && input.files[0]) {
                if (input.files[0].size > 2097152) {
                    SweetAlertError("File is too big.");
                    $('#signatureupload').val('');
                } else {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        imagebase64 = e.target.result;
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

        function certificatereadURL(input) {
            if (input.files && input.files[0]) {
                if (input.files[0].size > 2097152) {
                    SweetAlertError("File is too big.");
                    $('#pkicertificateupload').val('');
                } else {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        certbase64 = e.target.result;
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

    });
</script>

<div id="sigandCertModalGRAIR" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color: steelblue; text-align: center;">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">
                    <p>
                        <label style="color: white;">Certificate and Signature</label>
                    </p>
                </h4>
            </div>
            <div class="modal-body">
                <div class="row" style="margin: 5px;">
                    <%--  <div class="col-lg-6">--%>
                    <table style="width: 100%; border-collapse: separate; border-spacing: 0 1em;">
                        <tr>
                            <td style="width: 30%; text-align: center;">Signature<br />
                                (Max: 2mb)</td>
                            <td style="width: 70%;">
                                <input type="file" id="signatureupload" class="form-control" accept=".png, .png|image/*" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: center;">Pass Phrase</td>
                            <td style="width: 70%;">
                                <%--<input type="password" id="pkipassword" class="form-control" />--%>

                                <div class="input-group">
                                    <label class="sr-only">Password</label>
                                    <input name="txtPassword" type="password" id="pkipassword" placeholder="Password..." class="form-control" autocomplete="off"><span class="input-group-addon"><i id="togglePassword" class="fa fa-eye" onclick="togglePass()"></i></span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: center;">Certificate<br />
                                (Max: 2mb)</td>
                            <td style="width: 70%;">
                                <input type="file" id="pkicertificateupload" class="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: center;">&nbsp;</td>
                            <td style="width: 70%;">
                                <input type="checkbox" class="form-check-input" id="datestamp" /><label for="fromdatestamp">&nbsp;Add Date Time Stamp</label>
                            </td>
                        </tr>
                    </table>
                    <%-- </div>--%>
                </div>
            </div>
            <div class="modal-footer">
                <input type="button" value="Download" id="btndownloadcreatepdf" transno="" transtype="" class="btn btn-success" />
            </div>
        </div>
    </div>
</div>