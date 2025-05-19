//$(document).ready(function () {


//});
$(function () {
    $('#ack1').hide();
    $('#ack2').hide();
    $('#ack3').hide();
    LoadControls();

});

//Added by: Domdom
$(document).on('change', '#chkIssuanceAcknowledgement', function () {
    if (this.checked) {
        $('#ack1').show();
        $('#ack2').show();
        $('#ack3').show();
    }
    else
    {
        //document.getElementById("WebAcknowledged").checked = true;
        $('#ack1').hide();
        $('#ack2').hide();
        $('#ack3').hide();
    }
    //document.getElementById("chkIssuanceAcknowledgement").checked = false;

});
function LoadControls() {
    $('.btn').button();
    //Flat red color scheme for iCheck


    document.getElementById('DPass').style.display = 'none';
    GetDepreciation();
    GetRFIDandBarcodeGeneration();
    GetSystemSetting();
    GetUserAcccountSettings();
    //GetAccountingSetting();
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    })
}

//function showPass() {
//    var x = document.getElementById("<%=txtDefaultPassword.ClientID%>");
//    if (x.type === "password") {
//        x.type = "text";
//    } else {
//        x.type = "password";
//    }
//}

/////////////////////////////////////Threshold Settings
function SetThreshold() {
    var alertMsg;
    //var agingthreshold = $('#BodyHolder_txtAgingThreshold').val();
    //var handheldthreshold = $('#BodyHolder_txtHandheldThreshold').val();
    var maintenancethreshold = $('#BodyHolder_txtMaintenanceThreshold').val();
    if ($('#BodyHolder_txtMaintenanceThreshold').val().trim() == '') {//$('#BodyHolder_txtAgingThreshold').val().trim() == '' || $('#BodyHolder_txtHandheldThreshold').val().trim() == '') {
        alertMsg = ' ';
        //if ($('#BodyHolder_txtAgingThreshold').val() == '')
        //    alertMsg += '\n Aging Threshold';
        //if ($('#BodyHolder_txtHandheldThreshold').val() == '')
        //    alertMsg += '\n Handheld Threshold';
        if ($('#BodyHolder_txtMaintenanceThreshold').val() == '')
            alertMsg += '\n Maintenance Threshold';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Threshold?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this Threshold?",
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
                    url: "System/Miscellaneous.aspx/SetThreshold",
                    //data: " { agingthreshold : '" + agingthreshold + "',handheldthreshold : '" + handheldthreshold + "',maintenancethreshold : '" + maintenancethreshold + "'} ",
                    data: " { maintenancethreshold : '" + maintenancethreshold + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Threshold Successfully Set!');
                        swal("Successful", "Threshold Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}


function CheckCompanyName() {
    $('#BodyHolder_CompanyLogo').html("");
    $('#BodyHolder_FormLogo').html("");
}

/////////////////////////////////////Home Page Image
function loadfile() {
    var fu = $('#BodyHolder_fuHomePage').val();
    var file = [];
    var imagestring = '<img src="' + file + '">';
    $('#BodyHolder_divImageHome').val(imagestring);
}


function SaveHomePageImage(event) {
    var fu = $('#BodyHolder_fuHomePage').val();
    var file = [];
    file = fu.split('\\');
    var last = file.pop();

    if ($('#BodyHolder_fuHomePage').val() == '') {
        //alert("Please select an image to upload.");
        swal("Error", "Please select an image to upload.", "error");
        event.preventDefault();
    }
    else {
        //alert("pasok");

        $.ajax({
            type: "POST",
            url: "System/Miscellaneous.aspx/SaveHomePageImage",
            data: " { homepageimage : '" + last + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert('Home Page Image uploaded successfully');
                swal("Successful", "Home Page Image uploaded successfully!", "success");
                event.preventDefault();
            }
        });
    }
    //}
}

/////////////////////////////////////Company Information Settings
function SetCompanyInformation() {
    var alertMsg;

    //if ($('#BodyHolder_CompanyLogo').html() == "")
    //{
    //    alert("BISAYA");
    //}

    var companyname = ReplaceStr1($('#BodyHolder_txtCompanyName').val());
    var tradingname = ReplaceStr1($('#BodyHolder_txtTradingName').val());
    var contactperson = ReplaceStr1($('#BodyHolder_txtContactPerson').val());
    var contactnumber = ReplaceStr1($('#BodyHolder_txtContactNumber').val());
    var emailaddress = $('#BodyHolder_txtEmailAddress').val();
    var address = ReplaceStr1($('#BodyHolder_txtAddress').val());
    var companylogo = $('#BodyHolder_FileUploadCompanyLogo').val();
    var formlogo = $('#BodyHolder_FileUploadFormLogo').val();
    var webicon = $('#BodyHolder_FileUploadWebIcon').val();
    var companylogocontainer = "";
    if (companylogo == "") {
        companylogocontainer = "";
    }
    else {
        companylogocontainer = 'CompanyLogo.' + companylogo.split('.')[1];
    }

    var formlogocontainer = "";
    if (formlogo == "") {
        formlogocontainer = "";
    }
    else {
        formlogocontainer = 'FormLogo.' + formlogo.split('.')[1];
    }

    var webiconcontainer = "";
    if (webicon == "") {
        webiconcontainer = "";
    }
    else {
        webiconcontainer = 'favico.' + webicon.split('.')[1];
    }
    if (ReplaceStr1($('#BodyHolder_txtCompanyName').val()) == '' || ReplaceStr1($('#BodyHolder_txtTradingName').val()) == '' || ReplaceStr1($('#BodyHolder_txtContactPerson').val()) == '' ||
        ReplaceStr1($('#BodyHolder_txtContactNumber').val()) == '' || $('#BodyHolder_txtEmailAddress').val().trim() == '' || ReplaceStr1($('#BodyHolder_txtAddress').val()) == '' || $('#BodyHolder_CompanyLogo').html() == "" || $('#BodyHolder_FormLogo').html() == "") {
        alertMsg = '';
        if (ReplaceStr1($('#BodyHolder_txtCompanyName').val()) == '')
            alertMsg += '\nCompany Name';
        if (ReplaceStr1($('#BodyHolder_txtTradingName').val()) == '')
            alertMsg += '\nTrading Name';
        if (ReplaceStr1($('#BodyHolder_txtContactPerson').val()) == '')
            alertMsg += '\nContact Person';
        if (ReplaceStr1($('#BodyHolder_txtContactNumber').val()) == '')
            alertMsg += '\nContact Number';
        if ($('#BodyHolder_txtEmailAddress').val() == '')
            alertMsg += '\nEmail Address';
        if (ReplaceStr1($('#BodyHolder_txtAddress').val()) == '')
            alertMsg += '\nAddress';
        if ($('#BodyHolder_CompanyLogo').html() == "")
        {
            alertMsg += '\nCompany Logo';
        }
        if ($('#BodyHolder_FormLogo').html() == "") {
            alertMsg += '\nForm Logo';
        }
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
        ex.preventDefault()
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this Company Information?",
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
                    var fileUpload = $("#BodyHolder_FileUploadCompanyLogo").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: "System/CompanyInformation.ashx?CompanyName=" + $('#BodyHolder_txtCompanyName').val() + "|" + companylogocontainer,
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                var fileUpload = $("#BodyHolder_FileUploadFormLogo").get(0);
                                var files = fileUpload.files;
                                var test = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    test.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    url: "System/FormLogoHandler.ashx?CompanyName=" + $('#BodyHolder_txtCompanyName').val() + "|" + formlogocontainer,
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: test,
                                    success: function (result) {
                                        if (result.includes("File Uploaded Successfully", 0)) {
                                            var fileUpload = $("#BodyHolder_FileUploadWebIcon").get(0);
                                            var files = fileUpload.files;
                                            var test = new FormData();
                                            for (var i = 0; i < files.length; i++) {
                                                test.append(files[i].name, files[i]);
                                            }
                                            $.ajax({
                                                url: "System/WebIconHandler.ashx?CompanyName=" + $('#BodyHolder_txtCompanyName').val() + "|" + webiconcontainer,
                                                type: "POST",
                                                contentType: false,
                                                processData: false,
                                                data: test,
                                                success: function (result) {
                                                    if (result.includes("File Uploaded Successfully", 0)) {
                                                        $.ajax({
                                                            type: "POST",
                                                            url: "System/Miscellaneous.aspx/SetCompanyInformation",
                                                            data: " { companyname : '" + companyname + "',tradingname : '" + tradingname + "',contactperson : '" + contactperson + "',contactnumber : '" + contactnumber + "',emailaddress : '" + emailaddress + "',address : '" + address + "', companylogo : '" + companylogocontainer + "', formlogo : '" + formlogocontainer + "', webicon : '" + webiconcontainer + "'} ",
                                                            contentType: "application/json; charset=utf-8",
                                                            dataType: "json",
                                                            success: function (msg) {
                                                                //swal("Successful", "Company Information Successfully Set!", "success");

                                                                swal("Successful", "Company Information Successfully Set!\n\nYou will be logout from the system.", "success").then(function () {
                                                                    LogOut();
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
                                        } else {
                                            swal("Warning", result, "warning");
                                        }

                                    },
                                    error: function (err) {
                                        swal("Warning", err.statusText, "warning");
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



/////////////////////////////////////Depreciation Settings
var depreciationformula = '';

function displayRadioValue() {
    var radios = document.getElementsByName("r3");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            depreciationformula = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}

function SetDepreciation(event) {
    displayRadioValue();
    var alertMsg;
    var usefullife = $('#txtUsefulLifeDepre').val();
    var daysperyear = $('#txtDaysPerYearDepre').val();
    var residualvalue = $('#txtDefaultResidualValue').val();
    var salvagevalue = $('#txtDefaultSalvageValue').val();
    var rateofdepreciation = $('#txtDefaultRateDepreciation').val();

    //alert(depreciationformula);
    if (depreciationformula == '' || $('#txtUsefulLifeDepre').val().trim() == '' || $('#txtDaysPerYearDepre').val().trim() == '' ||
        $('#txtDefaultResidualValue').val().trim() == '' || $('#txtDefaultSalvageValue').val().trim() == '' || $('#txtDefaultRateDepreciation').val().trim() == '') {
        alertMsg = '';
        if (depreciationformula == '')
            alertMsg += "\nDepreciation Formula";
        if ($('#txtUsefulLifeDepre').val() == '')
            alertMsg += '\nUseful Life';
        if ($('#txtDaysPerYearDepre').val() == '')
            alertMsg += '\nDays in a year';
        if ($('#txtDefaultResidualValue').val() == '')
            alertMsg += '\nDefault Residual Value';
        if ($('#txtDefaultSalvageValue').val() == '')
            alertMsg += '\nDefault Salvaging Value';
        if ($('#txtDefaultRateDepreciation').val() == '')
            alertMsg += '\nDefault Rate of Depreciation';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Depreciation?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this Depreciation?",
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
                    url: "System/Miscellaneous.aspx/SetDepreciation",
                    data: " { depreciationformula : '" + depreciationformula + "',usefullife : '" + usefullife + "',daysperyear : '" + daysperyear + "',residualvalue : '" + residualvalue + "',salvagevalue : '" + salvagevalue + "',rateofdepreciation : '" + rateofdepreciation + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Depreciation Successfully Set!');
                        swal("Successful", "Depreciation Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function GetDepreciation() {
    $.ajax({
        type: "POST",
        url: "System/Miscellaneous.aspx/GetDepreciation",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var radio1 = 'radio1';
            var radio2 = 'radio2';
            var radio3 = 'radio3';

            $('#txtUsefulLifeDepre').val(msg.d.split('|')[1]);
            $('#txtDaysPerYearDepre').val(msg.d.split('|')[2]);
            $('#txtDefaultResidualValue').val(msg.d.split('|')[3]);
            $('#txtDefaultSalvageValue').val(msg.d.split('|')[4]);
            $('#txtDefaultRateDepreciation').val(msg.d.split('|')[5]);

            if (msg.d.split('|')[0] == "Straight Line Depreciation Method") {
                document.getElementById("radio1").checked = true;
                //alert(document.getElementById("radio1").checked);
            }
            else if (msg.d.split('|')[0] == "Diminishing Balance Method") {
                document.getElementById("radio2").checked = true;
                //alert(document.getElementById("radio2").checked);
            } else {
                document.getElementById("radio3").checked = true;
                //alert(document.getElementById("radio3").checked);
            }

            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            })
        }
    });
}



////////////////////////////////////RFID and Barcode
var GenerateOrManualRFID = '';
var GenerateOrManualBarcode = '';

function displayRadioValueRFID() {
    var radios = document.getElementsByName("r4");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            GenerateOrManualRFID = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}

function displayRadioValueBarcode() {
    var radios = document.getElementsByName("r5");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            GenerateOrManualBarcode = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}

function SetRFIDandBarcodeGeneration() {
    displayRadioValueRFID();
    displayRadioValueBarcode();
    var alertMsg;

    if (GenerateOrManualRFID == '' || GenerateOrManualBarcode == '') {
        alertMsg = '';
        if (GenerateOrManualRFID == '')
            alertMsg += '\nRFID Number generation';
        if (GenerateOrManualBarcode == '')
            alertMsg += '\nBarcode Number generation';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this RFID and Barcode generation?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this RFID and Barcode generation?",
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
                    url: "System/Miscellaneous.aspx/SetRFIDandBarcodeGeneration",
                    data: " { generateormanualrfid : '" + GenerateOrManualRFID + "',generateormanualbarcode : '" + GenerateOrManualBarcode + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('RFID and Barcode generation Successfully Set!');
                        swal("Successful", "RFID and Barcode generation Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function GetRFIDandBarcodeGeneration() {
    $.ajax({
        type: "POST",
        url: "System/Miscellaneous.aspx/GetRFIDandBarcodeGeneration",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.d.split('|')[0] == "Auto generated") {
                document.getElementById("radioRfidAuto").checked = true;
            }
            else if (msg.d.split('|')[0] == "RADMware") {
                document.getElementById("radioRfidRad").checked = true;
            }
            else {
                document.getElementById("radioRfidManual").checked = true;
            }

            if (msg.d.split('|')[1] == "Auto generated") {
                document.getElementById("radioBarcodeAuto").checked = true;
            }
            else {
                document.getElementById("radioBarcodeManual").checked = true;
            }

            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            })
        }
    });
}

////////////////////////////////////Require System Setting

var VerificationMode = '';

function displayRadioValueVerifyMode() {
    var radios = document.getElementsByName("rvm");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            VerificationMode = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}


var ExpenseType = '';

function displayRadioValueVersionMode() {
    var radios = document.getElementsByName("rvm3");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            ExpenseType = radios[i].value;
            //alert(ExpenseType);
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}

function SetSystemSetting() {
    displayRadioValueVerifyMode();
    displayRadioValueVersionMode();
    ////REQUIRE APPROVAL
    var creationapproval = document.getElementById("chkCreationApproval").checked;
    var stockmergeapproval = document.getElementById("chkMergeApproval").checked;
    var propertyissuanceapproval = document.getElementById("chkPropertyIssuanceApproval").checked;
    var propertytransferapproval = document.getElementById("chkPropertyTransferApproval").checked;
    var turnoverstockapproval = document.getElementById("chkTurnOverApproval").checked;
    var propertydisposalapproval = document.getElementById("chkDisposalApproval").checked;
    var gatepassapproval = document.getElementById("chkGatePassApproval").checked;
    var csvuploadapproval = document.getElementById("chkUploadApproval").checked;
    var dbrestoreapproval = document.getElementById("chkRestoreApproval").checked;
    var propertyrequestapproval = document.getElementById("chkPropertyRequestApproval").checked;
    var stockdisposalapproval = document.getElementById("chkStockDisposalApproval").checked;
    var stockissuanceapproval = document.getElementById("chkStockIssuanceApproval").checked;
    var requiremaintenance = document.getElementById("chkRequireMaintenance").checked;
    var RetaggingApproval = document.getElementById("chkPropertyRetagginApproval").checked;
    var SupplyAllocation = document.getElementById("chkSupployAlloc").checked;


    //////REQUIRE RFID SCANNER
    var creationscanner = document.getElementById("chkCreationScanner").checked;
    var propertyissuancescanner = document.getElementById("chkPropertyIssuanceScanner").checked;
    var propertytransferscanner = document.getElementById("chkPropertyTransferScanner").checked;
    var stocktaking = document.getElementById("chkStockTaking").checked;
    var propertyrequestscanner = document.getElementById("chkPropertyRequestScanner").checked;
    var propertyreturnrequestscanner = document.getElementById("chkPropertyReturnRequestScanner").checked;
    var propertydisposalscanner = document.getElementById("chkPropertyDisposalScanner").checked;



    var ICS = '0';
    var PAR = '0';
    var OPEXCAPEX = '0';
    if (ExpenseType == 'PAR/ICS') {
        ICS = ($('#BodyHolder_txtLOW').val());
        PAR = ($('#BodyHolder_txtHIGH').val());
    }
    else if (ExpenseType == 'OPEX/CAPEX') {
        OPEXCAPEX = ($('#BodyHolder_txtOPEXCAPEX').val());
    }

    //////REQUIRE RFID PRINTING
    var rfidprinting = document.getElementById("chkRFIDprinting").checked;

    /////ISSUANCE ACKNOWLEDGEMENT
    var IssuanceAcknowledgement = document.getElementById("chkIssuanceAcknowledgement").checked;
    var AcknowledgementMode = (document.getElementById("WebAcknowledged").checked) ? 'Web Manual Acknowledgement' : 'Buddy Lite';

    ////Property Number Generation
    var PropGenMode = (document.getElementById("PropAutoGen").checked) ? 'Auto generated' : 'Manual Input';


    ////Stock Request Remaining Quantity
    var StockRequestRemainingQuantity = document.getElementById("chkStockRequestQuantity").checked;

    swal({
        title: "Are you sure?",
        text: "Are you sure you want to set this System Setting?",
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
                url: "System/Miscellaneous.aspx/SetSystemSetting",
                data: " {verificationmode: '" + VerificationMode + "', creationapproval : '" + creationapproval + "', stockmergeapproval : '" + stockmergeapproval + "',propertyissuanceapproval : '" + propertyissuanceapproval + "',propertytransferapproval : '" + propertytransferapproval + "',turnoverstockapproval : '" + turnoverstockapproval + "',propertydisposalapproval : '" + propertydisposalapproval + "',gatepassapproval : '" + gatepassapproval + "',csvuploadapproval : '" + csvuploadapproval + "',dbrestoreapproval : '" + dbrestoreapproval + "', supplyallocationapproval: '" + SupplyAllocation + "',creationscanner : '" + creationscanner + "',propertyissuancescanner : '" + propertyissuancescanner + "',propertytransferscanner : '" + propertytransferscanner + "',stocktaking : '" + stocktaking + "',rfidprinting : '" + rfidprinting + "', propertyrequestapproval: '" + propertyrequestapproval + "',stockissuanceapproval:'" + stockissuanceapproval + "', stockdisposalapproval: '" + stockdisposalapproval + "', propertyrequestscanner: '" + propertyrequestscanner + "', propertyreturnrequestscanner: '" + propertyreturnrequestscanner + "', propertydisposalscanner: '" + propertydisposalscanner + "',IssuanceAcknowledgement:'" + IssuanceAcknowledgement + "',AcknowledgementMode:'" + AcknowledgementMode + "', requiremaintenance: '" + requiremaintenance + "', propertynogeneration: '" + PropGenMode + "', ExpenseType: '" + ExpenseType + "', ICS: '" + ICS + "', PAR: '" + PAR + "', OPEXCAPEX: '" + OPEXCAPEX + "', RetaggingApproval: '" + RetaggingApproval + "', StockRequestRemainingQuantity: '" + StockRequestRemainingQuantity + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //alert('System Setting Successfully Set!');
                    swal("Successful", "System Setting Successfully Set!\n\nYou will be logout from the system.", "success").then(function () {
                        LogOut();
                    });
                    //swal("Successful", "System Setting Successfully Set!\n\nPlease Log-out and then Log-in again for system settings to take effect.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function GetSystemSetting() {
    $.ajax({
        type: "POST",
        url: "System/Miscellaneous.aspx/GetSystemSetting",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.d.split('|')[0] == 'True') {
                document.getElementById("chkCreationApproval").checked = true;
            }
            else {
                document.getElementById("chkCreationApproval").checked = false;
            }

            if (msg.d.split('|')[1] == 'True') {
                document.getElementById("chkMergeApproval").checked = true;
            }
            else {
                document.getElementById("chkMergeApproval").checked = false;
            }

            if (msg.d.split('|')[2] == 'True') {
                document.getElementById("chkPropertyIssuanceApproval").checked = true;
            }
            else {
                document.getElementById("chkPropertyIssuanceApproval").checked = false;
            }

            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkPropertyTransferApproval").checked = true;
            }
            else {
                document.getElementById("chkPropertyTransferApproval").checked = false;
            }

            if (msg.d.split('|')[4] == 'True') {
                document.getElementById("chkTurnOverApproval").checked = true;
            }
            else {
                document.getElementById("chkTurnOverApproval").checked = false;
            }

            if (msg.d.split('|')[5] == 'True') {
                document.getElementById("chkDisposalApproval").checked = true;
            }
            else {
                document.getElementById("chkDisposalApproval").checked = false;
            }

            if (msg.d.split('|')[6] == 'True') {
                document.getElementById("chkGatePassApproval").checked = true;
            }
            else {
                document.getElementById("chkGatePassApproval").checked = false;
            }

            if (msg.d.split('|')[7] == 'True') {
                document.getElementById("chkUploadApproval").checked = true;
            }
            else {
                document.getElementById("chkUploadApproval").checked = false;
            }

            if (msg.d.split('|')[8] == 'True') {
                document.getElementById("chkRestoreApproval").checked = true;
            }
            else {
                document.getElementById("chkRestoreApproval").checked = false;
            }

            if (msg.d.split('|')[9] == 'True') {
                document.getElementById("chkCreationScanner").checked = true;
            }
            else {
                document.getElementById("chkCreationScanner").checked = false;
            }

            if (msg.d.split('|')[10] == 'True') {
                document.getElementById("chkPropertyIssuanceScanner").checked = true;
            }
            else {
                document.getElementById("chkPropertyIssuanceScanner").checked = false;
            }

            if (msg.d.split('|')[11] == 'True') {
                document.getElementById("chkPropertyTransferScanner").checked = true;
            }
            else {
                document.getElementById("chkPropertyTransferScanner").checked = false;
            }

            if (msg.d.split('|')[12] == 'True') {
                document.getElementById("chkStockTaking").checked = true;
            }
            else {
                document.getElementById("chkStockTaking").checked = false;
            }

            if (msg.d.split('|')[13] == 'True') {
                document.getElementById("chkRFIDprinting").checked = true;
            }
            else {
                document.getElementById("chkRFIDprinting").checked = false;
            }

            if (msg.d.split('|')[14] == 'True') {
                document.getElementById("chkPropertyRequestApproval").checked = true;
            }
            else {
                document.getElementById("chkPropertyRequestApproval").checked = false;
            }

            if (msg.d.split('|')[15] == 'True') {
                document.getElementById("chkPropertyRequestScanner").checked = true;
            }
            else {
                document.getElementById("chkPropertyRequestScanner").checked = false;
            }

            if (msg.d.split('|')[16] == 'True') {
                document.getElementById("chkPropertyReturnRequestScanner").checked = true;
            }
            else {
                document.getElementById("chkPropertyReturnRequestScanner").checked = false;
            }

            if (msg.d.split('|')[17] == 'True') {
                document.getElementById("chkPropertyDisposalScanner").checked = true;
            }
            else {
                document.getElementById("chkPropertyDisposalScanner").checked = false;
            }

            if (msg.d.split('|')[18] == "Web Manual Verification") {
                document.getElementById("radioMode1").checked = true;
            }
            else if (msg.d.split('|')[18] == "Barcode Scanner") {
                document.getElementById("radioMode2").checked = true;
            }
            else if (msg.d.split('|')[18] == "Thing Magic") {
                document.getElementById("radioMode3").checked = true;
            }
            else {
                document.getElementById("radioMode4").checked = true;
            }

            if (msg.d.split('|')[19] == 'True') {
                document.getElementById("chkIssuanceAcknowledgement").checked = true;
            }
            else {
                document.getElementById("chkIssuanceAcknowledgement").checked = false;
            }

            if (msg.d.split('|')[20] == 'Web Manual Acknowledgement') {
                document.getElementById("WebAcknowledged").checked = true;
            }
            else {
                document.getElementById("BodyLITEMode").checked = true;
            }

            if (msg.d.split('|')[21] == 'True') {
                document.getElementById("chkStockDisposalApproval").checked = true;
            }
            else {
                document.getElementById("chkStockDisposalApproval").checked = false;
            }

            if (msg.d.split('|')[22] == 'True') {
                document.getElementById("chkStockIssuanceApproval").checked = true;
            }
            else {
                document.getElementById("chkStockIssuanceApproval").checked = false;
            }

            if (msg.d.split('|')[23] == 'True') {
                document.getElementById("chkRequireMaintenance").checked = true;
            }
            else {
                document.getElementById("chkRequireMaintenance").checked = false;
            }
            //property no generation
            if (msg.d.split('|')[24] == 'Auto generated') {
                document.getElementById("PropAutoGen").checked = true;
            }
            else {
                document.getElementById("PropManual").checked = true;
            }

            if (msg.d.split('|')[26] == "PAR/ICS") {
                document.getElementById("PI").checked = true;
                //alert(msg.d.split('|')[27]);
                $('#BodyHolder_txtLOW').val(msg.d.split('|')[27]);
                $('#BodyHolder_txtHIGH').val(msg.d.split('|')[28]);
            }
            else if (msg.d.split('|')[26] == "OPEX/CAPEX") {
                document.getElementById("OC").checked = true;
                $('#BodyHolder_txtOPEXCAPEX').val(msg.d.split('|')[29]);
            }
            else {
                document.getElementById("radioMode4").checked = true;
            }

            if (msg.d.split('|')[30] == 'True') {
                document.getElementById("chkPropertyRetagginApproval").checked = true;
            }
            else {
                document.getElementById("chkPropertyRetagginApproval").checked = false;
            }

            if (msg.d.split('|')[31] == 'True') {
                document.getElementById("chkStockRequestQuantity").checked = true;
            }
            else {
                document.getElementById("chkStockRequestQuantity").checked = false;
            }

            if (msg.d.split('|')[32] == 'True') {
                document.getElementById("chkSupployAlloc").checked = true;
            }
            else {
                document.getElementById("chkSupployAlloc").checked = false;
            }

            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            })
        }
    });
}



////////////////////////////////////Mail Settings
function SetMail() {
    var alertMsg;
    var smtphost = $('#BodyHolder_txtSMTPhost').val();
    var smtpport = $('#BodyHolder_txtSMTPport').val();
    var email = $('#BodyHolder_txtEmail').val();
    var password = $('#BodyHolder_txtPassword').val();
    var emailsubject = $('#BodyHolder_txtEmailSubject').val();
    if ($('#BodyHolder_txtSMTPhost').val().trim() == '' || $('#BodyHolder_txtSMTPport').val().trim() == '' || $('#BodyHolder_txtEmail').val().trim() == '' || $('#BodyHolder_txtPassword').val().trim() == '' || $('#BodyHolder_txtPassword').val().trim() == '') {
        alertMsg = ' ';
        if ($('#BodyHolder_txtSMTPhost').val() == '')
            alertMsg += '\nSMTP Host';
        if ($('#BodyHolder_txtSMTPport').val() == '')
            alertMsg += '\nSMTP Port';
        if ($('#BodyHolder_txtEmail').val() == '')
            alertMsg += '\nE-mail';
        if ($('#BodyHolder_txtPassword').val() == '')
            alertMsg += '\nPassword';
        if ($('#BodyHolder_txtEmailSubject').val() == '')
            alertMsg += '\nEmail Subject Header';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Mail Setting?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this Mail Setting?",
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
                    url: "System/Miscellaneous.aspx/SetMail",
                    data: " { host : '" + smtphost + "',port : '" + smtpport + "',email : '" + email + "',password : '" + password + "', emailsubject: '" + emailsubject + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Mail Successfully Set!');
                        swal("Successful", "Mail Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function TestMail() {
    var alertMsg;
    var email = $('#BodyHolder_txtTestEmail').val();
    if ($('#BodyHolder_txtTestEmail').val().trim() == '') {
        alertMsg = ' ';
        if ($('#BodyHolder_txtTestEmail').val() == '')
            alertMsg += '\nTest Email';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        $.ajax({
            type: "POST",
            url: "System/Miscellaneous.aspx/SendTestEmail",
            data: " { email : '" + email + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == 'okay') {
                    //alert('Test Mail Successfully Sent!');
                    swal("Successful", "Test Mail Successfully Sent!", "success");
                }
                else {
                    //alert('Test Mail Not Sent!');
                    swal("Error", "Test Mail Not Sent!", "error");
                }
            }
        });
    }
}



////////////////////////////////////Database Back up Directory
function SetDirectory() {
    var alertMsg;
    var directory = $('#BodyHolder_txtDBDirectory').val();
    var path = directory.replace(/\\/g, ',');
    //if (path.includes(":\\") == false || path.includes('\\') == false || path.includes(':') == false) {
    //    alert("Invalid path / directory!");
    //}
    if ($('#BodyHolder_txtDBDirectory').val().trim() == '') {
        alertMsg = '';
        if ($('#BodyHolder_txtDBDirectory').val() == '')
            alertMsg += '\nDirectory';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Database Back-up Directory Path?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this Database Back-up Directory Path?",
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
                    url: "System/Miscellaneous.aspx/SetDBDirectory",
                    data: " { directory : '" + path + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Database Back-up Directory Successfully Set!');
                        swal("Successful", "Database Back-up Directory Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}



////////////////////////////////////Uploaded Files Directory
function SetDirectoryPath() {
    var alertMsg;
    var directory = $('#BodyHolder_txtUploadFilesDirectoryPath').val();
    var path = directory.replace(/\\/g, ',');
    if ($('#BodyHolder_txtUploadFilesDirectoryPath').val().trim() == '') {
        alertMsg = ' ';
        if ($('#BodyHolder_txtUploadFilesDirectoryPath').val() == '')
            alertMsg += '\nDirectory';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Uploaded Files Directory Path?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this  Uploaded Files Directory Path?",
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
                    url: "System/Miscellaneous.aspx/SetUploadedFilesDirectoryPath",
                    data: " { directory : '" + path + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Uploaded Files Directory Successfully Set!');
                        swal("Successful", "Uploaded Files Directory Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}



////////////////////////////////////User Default Password
function SetDefaultPassword() {
    var alertMsg;
    if ($('#BodyHolder_txtDefaultPassword').val().trim() == '') {
        alertMsg = ' ';
        if ($('#BodyHolder_txtDefaultPassword').val() == '')
            alertMsg += '\nDefault Password';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else if ($('#BodyHolder_txtDefaultPassword').val().length <= 5) {
        //alert('Password must be minimum of six(6) characters');
        swal("Warning", "Password must be minimum of six(6) characters", "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Default Password?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this Default Password?",
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
                    url: "System/Miscellaneous.aspx/SetDefaultPassword",
                    data: " { defaultpassword : '" + $('#BodyHolder_txtDefaultPassword').val().trim() + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d = 1) {
                            //alert('Defult Password Successfully Set!');
                            swal("Successful", "Defult Password Successfully Set!", "success");
                        }
                        else {
                            //alert('Defult Password Failed!');
                            swal("Error", "Defult Password Failed!", "error");
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}


/////////////////////////////////////Threshold Settings
function SetAccountingSetting() {
    var conversionmonth = 0;
    switch ($('#BodyHolder_ddlConversionMonth').val()) {
        case "January":
            conversionmonth = 1;
            break;
        case "February":
            conversionmonth = 2;
            break;
        case "March":
            conversionmonth = 3;
            break;
        case "April":
            conversionmonth = 4;
            break;
        case "May":
            conversionmonth = 5;
            break;
        case "June":
            conversionmonth = 6;
            break;
        case "July":
            conversionmonth = 7;
            break;
        case "August":
            conversionmonth = 8;
            break;
        case "September":
            conversionmonth = 9;
            break;
        case "October":
            conversionmonth = 10;
            break;
        case "November":
            conversionmonth = 11;
            break;
        case "December":
            conversionmonth = 12;
            break;
    }

    var endmonth = 0;
    switch ($('#BodyHolder_ddlEndMonth').val()) {
        case "January":
            endmonth = 1;
            break;
        case "February":
            endmonth = 2;
            break;
        case "March":
            endmonth = 3;
            break;
        case "April":
            endmonth = 4;
            break;
        case "May":
            endmonth = 5;
            break;
        case "June":
            endmonth = 6;
            break;
        case "July":
            endmonth = 7;
            break;
        case "August":
            endmonth = 8;
            break;
        case "September":
            endmonth = 9;
            break;
        case "October":
            endmonth = 10;
            break;
        case "November":
            endmonth = 11;
            break;
        case "December":
            endmonth = 12;
            break;
    }

    var transactionmonth = 0;
    switch ($('#BodyHolder_ddlTransactionMonth').val()) {
        case "January":
            transactionmonth = 1;
            break;
        case "February":
            transactionmonth = 2;
            break;
        case "March":
            transactionmonth = 3;
            break;
        case "April":
            transactionmonth = 4;
            break;
        case "May":
            transactionmonth = 5;
            break;
        case "June":
            transactionmonth = 6;
            break;
        case "July":
            transactionmonth = 7;
            break;
        case "August":
            transactionmonth = 8;
            break;
        case "September":
            transactionmonth = 9;
            break;
        case "October":
            transactionmonth = 10;
            break;
        case "November":
            transactionmonth = 11;
            break;
        case "December":
            transactionmonth = 12;
            break;
    }
    var x = 13; var y = 14;
    var alertMsg;
    var accountingperiod = $('#txtAccountingPeriod').val();
    var transactionyear = $('#txtTransactionYear').val();
    if ($('#txtAccountingPeriod').val().trim() == '' || $('#BodyHolder_ddlConversionMonth').val() == '--- Select Conversion Month ---' || $('#BodyHolder_ddlEndMonth').val() == '--- Select End Month ---' || $('#txtTransactionYear').val().trim() == '' || $('#BodyHolder_ddlTransactionMonth').val() == '--- Select Transaction Month ---') {
        alertMsg = ' ';
        if ($('#txtAccountingPeriod').val() == '')
            alertMsg += '\nAccounting Period';
        if ($('#BodyHolder_ddlConversionMonth').val() == '--- Select Conversion Month ---')
            alertMsg += '\nConversion Month';
        if ($('#BodyHolder_ddlEndMonth').val() == '--- Select End Month ---')
            alertMsg += '\nEnd Month';
        if ($('#txtTransactionYear').val() == '')
            alertMsg += '\nTransaction Year';
        if ($('#BodyHolder_ddlTransactionMonth').val() == '--- Select Transaction Month ---')
            alertMsg += '\nTransaction Month';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else if (parseInt(accountingperiod) >= y) {
        //alert("Accounting Period should not be greater than 13");
        swal("Warning", "Accounting Period should not be greater than 13", "warning");
    }
    else if (parseInt(accountingperiod) == x && parseInt(conversionmonth) > x || parseInt(accountingperiod) == 12 && parseInt(conversionmonth) > parseInt(accountingperiod)) {
        //alert('Conversion Month should not be greater than Accounting Period');
        swal("Warning", "onversion Month should not be greater than Accounting Period", "warning");
    }
    else if (parseInt(accountingperiod) == x && parseInt(endmonth) > x || parseInt(accountingperiod) == 12 && parseInt(endmonth) > parseInt(accountingperiod)) {
        //alert('End Month should not be greater than Accounting Period');
        swal("Warning", "End Month should not be greater than Accounting Period", "warning");
    }
    else if (parseInt(accountingperiod) == x && parseInt(transactionmonth) > x || parseInt(accountingperiod) == 12 && parseInt(transactionmonth) > parseInt(accountingperiod)) {
        //alert('Transaction Month should not be greater than Accounting Period');
        swal("Warning", "Transaction Month should not be greater than Accounting Period", "warning");
    }
    else {
        //if (confirm('Are You sure you want to set this Accounting Setting?') == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this User Account Setting?",
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
                    url: "System/Miscellaneous.aspx/SetAccountingSetting",
                    data: " { accountingperiod : '" + accountingperiod + "',conversionmonth : '" + conversionmonth + "',endmonth : '" + endmonth + "',transactionyear : '" + transactionyear + "',transactionmonth : '" + transactionmonth + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Accounting Setting Successfully Set!');
                        swal("Successful", "Accounting Setting Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });

    }
}


function GetUserAcccountSettings() {
    $.ajax({
        type: "POST",
        url: "System/Miscellaneous.aspx/GetUserAcccountSettings",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d.split('|')[0] == 'Username') {
                document.getElementById('Username').checked = true;
            }
            else if (msg.d.split('|')[0] == 'Email') {
                document.getElementById('Email').checked = true;
            }


            if (msg.d.split('|')[1] == 'Default') {
                document.getElementById('DefaultPassword').checked = true;
            }
            else if (msg.d.split('|')[1] == 'Generated') {
                document.getElementById('RandomPassword').checked = true;
            }
            $('#BodyHolder_txtDefaultPassword').val(msg.d.split('|')[2]);
            $('#BodyHolder_txtPasswordSize').val(msg.d.split('|')[3]);
            $('#BodyHolder_txtUpperCase').val(msg.d.split('|')[5]);
            if (msg.d.split('|')[4] == "True") {
                document.getElementById('chkUpperCase').checked = true;
            } else {
                document.getElementById('chkUpperCase').checked = false;
                document.getElementById('BodyHolder_txtUpperCase').disabled = true;
            }

            $('#BodyHolder_txtLowerCase').val(msg.d.split('|')[7]);
            if (msg.d.split('|')[6] == "True") {
                document.getElementById('chkLowerCase').checked = true;
            } else {
                document.getElementById('chkLowerCase').checked = false;
                document.getElementById('BodyHolder_txtLowerCase').disabled = true;
            }

            $('#BodyHolder_txtNumber').val(msg.d.split('|')[9]);
            if (msg.d.split('|')[8] == "True") {
                document.getElementById('chkNumber').checked = true;
            } else {
                document.getElementById('chkNumber').checked = false;
                document.getElementById('BodyHolder_txtNumber').disabled = true;
            }


            $('#BodyHolder_txtSymbol').val(msg.d.split('|')[11]);
            if (msg.d.split('|')[10] == "True") {
                document.getElementById('chkSymbol').checked = true;
            } else {
                document.getElementById('chkSymbol').checked = false;
                document.getElementById('BodyHolder_txtSymbol').disabled = true;
            }

            $('#BodyHolder_txtAccountLock').val(msg.d.split('|')[13]);
            if (msg.d.split('|')[12] == "True") {
                document.getElementById("chkAccountLock").checked = true;
            } else {
                document.getElementById('chkAccountLock').checked = false;
                document.getElementById('#BodyHolder_txtAccountLock').disabled = true;
            }
        }
    });
}

function GetAccountingSetting() {
    $.ajax({
        type: "POST",
        url: "System/Miscellaneous.aspx/GetAccountingSetting",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#txtAccountingPeriod').val(msg.d.split('|')[0]);
            $('#txtTransactionYear').val(msg.d.split('|')[3]);
            //$('#txtTransactionMonth').val(msg.d.split('|')[4]);
            switch (msg.d.split('|')[1]) {
                case "1":
                    $('#BodyHolder_ddlConversionMonth').val('January');
                    break;
                case "2":
                    $('#BodyHolder_ddlConversionMonth').val('February');
                    break;
                case "3":
                    $('#BodyHolder_ddlConversionMonth').val('March');
                    break;
                case "4":
                    $('#BodyHolder_ddlConversionMonth').val('April');
                    break;
                case "5":
                    $('#BodyHolder_ddlConversionMonth').val('May');
                    break;
                case "6":
                    $('#BodyHolder_ddlConversionMonth').val('June');
                    break;
                case "7":
                    $('#BodyHolder_ddlConversionMonth').val('July');
                    break;
                case "8":
                    $('#BodyHolder_ddlConversionMonth').val('August');
                    break;
                case "9":
                    $('#BodyHolder_ddlConversionMonth').val('September');
                    break;
                case "10":
                    $('#BodyHolder_ddlConversionMonth').val('October');
                    break;
                case "11":
                    $('#BodyHolder_ddlConversionMonth').val('November');
                    break;
                case "12":
                    $('#BodyHolder_ddlConversionMonth').val('December');
                    break;
            }

            switch (msg.d.split('|')[2]) {
                case "1":
                    $('#BodyHolder_ddlEndMonth').val('January');
                    break;
                case "2":
                    $('#BodyHolder_ddlEndMonth').val('February');
                    break;
                case "3":
                    $('#BodyHolder_ddlEndMonth').val('March');
                    break;
                case "4":
                    $('#BodyHolder_ddlEndMonth').val('April');
                    break;
                case "5":
                    $('#BodyHolder_ddlEndMonth').val('May');
                    break;
                case "6":
                    $('#BodyHolder_ddlEndMonth').val('June');
                    break;
                case "7":
                    $('#BodyHolder_ddlEndMonth').val('July');
                    break;
                case "8":
                    $('#BodyHolder_ddlEndMonth').val('August');
                    break;
                case "9":
                    $('#BodyHolder_ddlEndMonth').val('September');
                    break;
                case "10":
                    $('#BodyHolder_ddlEndMonth').val('October');
                    break;
                case "11":
                    $('#BodyHolder_ddlEndMonth').val('November');
                    break;
                case "12":
                    $('#BodyHolder_ddlEndMonth').val('December');
                    break;
            }

            switch (msg.d.split('|')[4]) {
                case "1":
                    $('#BodyHolder_ddlTransactionMonth').val('January');
                    break;
                case "2":
                    $('#BodyHolder_ddlTransactionMonth').val('February');
                    break;
                case "3":
                    $('#BodyHolder_ddlTransactionMonth').val('March');
                    break;
                case "4":
                    $('#BodyHolder_ddlTransactionMonth').val('April');
                    break;
                case "5":
                    $('#BodyHolder_ddlTransactionMonth').val('May');
                    break;
                case "6":
                    $('#BodyHolder_ddlTransactionMonth').val('June');
                    break;
                case "7":
                    $('#BodyHolder_ddlTransactionMonth').val('July');
                    break;
                case "8":
                    $('#BodyHolder_ddlTransactionMonth').val('August');
                    break;
                case "9":
                    $('#BodyHolder_ddlTransactionMonth').val('September');
                    break;
                case "10":
                    $('#BodyHolder_ddlTransactionMonth').val('October');
                    break;
                case "11":
                    $('#BodyHolder_ddlTransactionMonth').val('November');
                    break;
                case "12":
                    $('#BodyHolder_ddlTransactionMonth').val('December');
                    break;
            }
        }
    });
}

function getImage(event) {
    console.log(event.target.files[0]);
    $('#BodyHolder_CompanyLogo').html("");
    var image = URL.createObjectURL(event.target.files[0]);
    var imagediv = document.getElementById('BodyHolder_CompanyLogo');
    var newimg = document.createElement('img');
    newimg.src = image;
    newimg.width = "300";
    imagediv.appendChild(newimg);
}

var _URL = window.URL || window.webkitURL;
function getImage1(event) {
    var fileName = document.querySelector('#BodyHolder_FileUploadFormLogo').value;
    var extension = fileName.substring(fileName.lastIndexOf('.') + 1);
    if (extension == 'png') {
        console.log(event.target.files[0]);
        var file, img;
        if ((file = event.target.files[0])) {
            //alert(file);
            img = new Image();
            var objectUrl = _URL.createObjectURL(file);
            img.onload = function () {
                var size = document.getElementById('BodyHolder_FileUploadFormLogo').files[0].size;
                console.log(size);

                if (this.width > 300 && this.height > 200) {
                    swal("Warning!", "File dimension should not be greater than 300x200.", "warning");
                    $('#BodyHolder_FileUploadFormLogo').val("");
                    $('#BodyHolder_FormLogo').html("");
                }
                else {
                    $('#BodyHolder_FormLogo').html("");
                    var image = URL.createObjectURL(event.target.files[0]);
                    var imagediv = document.getElementById('BodyHolder_FormLogo');
                    var newimg = document.createElement('img');
                    newimg.src = image;
                    newimg.width = "300";
                    imagediv.appendChild(newimg);
                }
                console.log(this.width + " " + this.height);
                _URL.revokeObjectURL(objectUrl);
            };
            img.src = objectUrl;
        }
    }
    else {
        swal("Warning!", "Only .png file extension is allowed!", "warning");
        $('#BodyHolder_FileUploadFormLogo').val("");
        $('#BodyHolder_FormLogo').html("");
    }
}

function getImage2(event) {
    console.log(event.target.files[0]);
    var fileName = document.querySelector('#BodyHolder_FileUploadWebIcon').value;
    var extension = fileName.substring(fileName.lastIndexOf('.') + 1);
    if (extension == 'ico') {
        $('#BodyHolder_WebIcon').html("");
        var image = URL.createObjectURL(event.target.files[0]);
        var imagediv = document.getElementById('BodyHolder_WebIcon');
        var newimg = document.createElement('img');
        newimg.src = image;
        newimg.width = "300";
        imagediv.appendChild(newimg);
    }
    else {
        swal("Warning!", "Only .ico file extension is allowed!", "warning");
        $('#BodyHolder_FileUploadWebIcon').val("");
        $('#BodyHolder_WebIcon').html("");
    }
}

function Disabledtxt(txtDisabled) {
    var creationapproval = document.getElementById(txtDisabled).checked;
    var disabled = "BodyHolder_" + txtDisabled.replace("chk", "txt");
    document.getElementById(disabled).disabled = !creationapproval;
}

var GeneratePassword = '';

function displayRadioValueGeneratePassword() {
    var radios = document.getElementsByName("rbPassword");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            GeneratePassword = radios[i].value;
            //GeneratePassword(ExpenseType);
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}

var GenerateUsernameOrEmail = '';

function displayRadioValueUsernameOrEmail() {
    var radios = document.getElementsByName("Userusername");
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            GenerateUsernameOrEmail = radios[i].value;
            //GeneratePassword(ExpenseType);
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
}

function SetUserAccountSettings() {
    var alertMsg = '';
    displayRadioValueGeneratePassword();
    displayRadioValueUsernameOrEmail();
    var DefaultOrGenerated = '';
    if (GeneratePassword == 'Random Password') {
        if ($('#BodyHolder_txtPasswordSize').val() != '') {
            var UpperCase = 0;
            if (document.getElementById('chkUpperCase').checked) {
                UpperCase = $('#BodyHolder_txtUpperCase').val()
                if (UpperCase == '') {
                    UpperCase = 0;
                }
            }
            var LowerCase = 0;
            if (document.getElementById('chkLowerCase').checked) {
                LowerCase = $('#BodyHolder_txtLowerCase').val()
                if (LowerCase == '') {
                    LowerCase = 0;
                }
            }
            var Number = 0;
            if (document.getElementById('chkNumber').checked) {
                Number = $('#BodyHolder_txtNumber').val()
                if (Number == '') {
                    Number = 0;
                }
            }
            var Symbols = 0;
            if (document.getElementById('chkSymbol').checked) {
                Symbols = $('#BodyHolder_txtSymbol').val()
                if (Symbols == '') {
                    Symbols = 0;
                }
            }

            var PassSize = $('#BodyHolder_txtPasswordSize').val()
            if (PassSize == '') {
                PassSize = 0;
            }
            var PassSizE = parseInt(PassSize, 10) - (parseInt(UpperCase, 10) + parseInt(LowerCase, 10) + parseInt(Number, 10) + parseInt(Symbols, 10))
            if (PassSizE != 0) {
                alertMsg += "Password size is " + PassSize + " need " + PassSizE + " more";
                //swal("Warning", "Password size is " + PassSize + " need more" + PassSizE, "warning");
            }
            else {
                DefaultOrGenerated = 'Generated';
                alerT = true;
            }
        }
        else {
            alertMsg += "Random Generated Password";
        }
    }
    else if (GeneratePassword == 'Default Password') {
        if ($('#BodyHolder_txtDefaultPassword').val() != '') {
            DefaultOrGenerated = 'Default';
        }
        else {
            alertMsg += "\n" + "Default Password";
            // swal("Fill-out all the required field(s): ", "Default Password", "warning");
        }
    }

    if (document.getElementById('chkAccountLock').checked == true) {
        if ($('#BodyHolder_txtAccountLock').val() != '') {
        }
        else {
            alertMsg += "\n" + "Number of tries before account lock";
            //swal("Fill-out all the required field(s): ", "Number of tries before account lock", "warning");
        }
    }

    if (document.getElementById('BodyHolder_txtPasswordSize').value.trim() <= 0) {
        alertMsg += "\n Password length should not be less than 0";
    }

    if (alertMsg == '') {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to set this  Accounting Setting?",
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
                    url: "System/Miscellaneous.aspx/SetUserAcccountSettings",
                    data: " { UsernameOrEmail : '" + GenerateUsernameOrEmail + "',DefaultOrGenerated : '" + DefaultOrGenerated + "',defaultpassword : '" + $('#BodyHolder_txtDefaultPassword').val().trim() + "',PasswordSize : '" + PassSize + "',UpperCaseSize : '" + UpperCase + "',LowerCaseSize : '" + LowerCase + "',NumberSize : '" + Number + "',SymbolSize : '" + Symbols + "',LoginAttempt : '" + $('#BodyHolder_txtAccountLock').val() + "',WithLoginAttempt : '" + document.getElementById('chkAccountLock').checked + "',WithUpperCase : '" + document.getElementById('chkUpperCase').checked + "',WithLowerCase : '" + document.getElementById('chkLowerCase').checked + "',WithNumber : '" + document.getElementById('chkNumber').checked + "',WithSymbol : '" + document.getElementById('chkSymbol').checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert('Accounting Setting Successfully Set!');
                        swal("Successful", "Accounting Setting Successfully Set!", "success");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
    else {
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
}

//function SetThreshold() {
//    var alertMsg;
//    if ($('#BodyHolder_txtThreshold').val().trim() == '' || $('#BodyHolder_txtThreshold1').val().trim() == '') {
//        alertMsg = 'Fill-out all the required field(s): ';
//        if ($('#BodyHolder_txtThreshold').val() == '')
//            alertMsg += '\nThreshold';
//        if ($('#BodyHolder_txtThreshold1').val() == '')
//            alertMsg += '\nHandheld Threshold';
//        alert(alertMsg);
//    }
//    //else if (checknegative($('#BodyHolder_txtThreshold').val())==false || checknegative($('#BodyHolder_txtThreshold1').val())==false) {
//    //    alert('Negative Values Not allowed');
//    //}
//    else if (parseInt($('#BodyHolder_txtThreshold1').val()) <= 0) {
//        alert('Handheld Threshold must be greater than 0.');
//        $('#BodyHolder_txtThreshold1').focus();
//    }
//    else if (parseInt($('#BodyHolder_txtThreshold').val()) < 0 || parseInt($('#BodyHolder_txtThreshold1').val()) < 0) {
//        alert('Negative Values Not allowed.');
//    }
//    else if (parseInt($('#BodyHolder_txtThreshold').val()) > 365 || parseInt($('#BodyHolder_txtThreshold1').val()) > 365) {
//        alert('Threshold must not be greater than 365.');
//    }
//    else {
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SetThreshold",
//            data: " { threshold : '" + $('#BodyHolder_txtThreshold').val() + "',  threshold1 : '" + $('#BodyHolder_txtThreshold1').val() + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Threshold Successfully Set!');
//                //$('#ddlOption').val("--- Please Select ---");
//            }
//        });
//    }
//}

//function checknegative(str) {
//    if (parseFloat(str) < 0) {
//        str = "";
//        //str.focus();
//        //alert('Negative Values Not allowed');
//        return false;
//    }
//}

//function SetStartCount() {
//    var alertMsg;
//    if ($('#BodyHolder_txtCount').val().trim() == '
//        alertMsg = 'Fill-out all the required field(s): ';
//        if ($('#BodyHolder_txtCount').val() == '')
//            alertMsg += '\nStart Count';
//        alert(alertMsg);
//    }
//    else if (parseInt($('#BodyHolder_txtCount').val()) <= 1000 ) {
//        alert('Start count must be greater than 1000.')
//    }
//    else {
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SetStartCount",
//            data: " { startcount : '" + $('#BodyHolder_txtCount').val() + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Start Count Successfully Set!');
//                //$('#ddlOption').val("--- Please Select ---");
//            }
//        });
//    }
//}

//function SetAssetSetting() {
//    var alertMsg;
//    if ($('#BodyHolder_txtStartingCountTaggable').val().trim() == '' || $('#BodyHolder_txtPrefixTaggable').val() == '' || $('#BodyHolder_txtStartingCountNonTaggable').val().trim() == '' || $('#BodyHolder_txtPrefixNonTaggable').val() == '') {
//        alertMsg = 'Fill-out all the required field(s): ';
//        if ($('#BodyHolder_txtStartingCountTaggable').val() == '')
//            alertMsg += '\nPAR Starting Count';
//        if ($('#BodyHolder_txtPrefixTaggable').val() == '')
//            alertMsg += '\nPAR Prefix';
//        if ($('#BodyHolder_txtStartingCountNonTaggable').val() == '')
//            alertMsg += '\nICS Starting Count';
//        if ($('#BodyHolder_txtPrefixNonTaggable').val() == '')
//            alertMsg += '\nICS Prefix';
//        alert(alertMsg);
//    }
//    else {
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SetAssetSettings",
//            data: " { startingcounttaggable : '" + $('#BodyHolder_txtStartingCountTaggable').val().trim() + "',prefixtaggable : '" + $('#BodyHolder_txtPrefixTaggable').val().trim() + "',prefixnontaggable : '" + $('#BodyHolder_txtPrefixNonTaggable').val().trim() + "',startingcountnontaggable : '" + $('#BodyHolder_txtStartingCountNonTaggable').val().trim() + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Assets Setting Succesfully Set!');
//            }
//        });
//    }
//}

//function SETPARICS() {
//    var alertMsg;
//    if ($('#BodyHolder_txtCost').val().trim() == '') {
//        alertMsg = 'Fill-out all the required field(s): ';
//        if ($('#BodyHolder_txtCost').val() == '')
//            alertMsg += '\nAmount';
//        alert(alertMsg);
//    }
//    else {
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SetPARICS",
//            data: " { amount : '" + $('#BodyHolder_txtCost').val().trim() + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Amount Setting Succesfully Set!');
//            }
//        });
//    }
//}

//function SaveAnnouncement() {
//    var fu = $('#BodyHolder_FileUpload2').val();
//    var file = [];
//    file = fu.split('\\');
//    var last = file.pop();
//    //alert(last);
//    //if (last.split('.')[1] != 'gif' || last.split('.')[1] != 'jpg' || last.split('.')[1] != 'jpeg' || last.split('.')[1] != 'png') {
//    //    alert("Please Select .PNG, .GIF, .JPEG or .JPG file to upload");
//    //}
//    //else {
//        if ($('#BodyHolder_FileUpload2').val() == '') {
//            alert("Please select an image to upload.");
//        }
//        else {
//            //alert("pasok");
//            $.ajax({
//                type: "POST",
//                url: "Miscellaneous.aspx/SaveAnnouncement",
//                data: " { announcement : '" + last + "'} ",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (msg) {
//                    alert('Bulletin uploaded successfully');
//                }
//            });
//        }
//    //}
//}

//function SetMail() {
//    var alertMsg;
//    var smtphost = $('#BodyHolder_txtSMTPhost').val();
//    var smtpport = $('#BodyHolder_txtSMTPport').val();
//    var email = $('#BodyHolder_txtEmail').val();
//    var password = $('#BodyHolder_txtPassword').val();
//    if ($('#BodyHolder_txtSMTPhost').val().trim() == '' || $('#BodyHolder_txtSMTPport').val().trim() == '' || $('#BodyHolder_txtEmail').val().trim() == '' || $('#BodyHolder_txtPassword').val().trim() == '') {
//        alertMsg = 'Fill-out all the required field(s): ';
//        if ($('#BodyHolder_txtSMTPhost').val() == '')
//            alertMsg += '\nSMTP Host';
//        if ($('#BodyHolder_txtSMTPport').val() == '')
//            alertMsg += '\nSMTP Port';
//        if ($('#BodyHolder_txtEmail').val() == '')
//            alertMsg += '\nE-mail';
//        if ($('#BodyHolder_txtPassword').val() == '')
//            alertMsg += '\nPassword';
//        alert(alertMsg);
//    }
//    else {
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SetMail",
//            data: " { host : '" + smtphost + "',port : '" + smtpport + "',email : '" + email + "',password : '" + password + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Mail Successfully Set!');
//            }
//        });
//    }
//}

//function SaveSmallLogo() {
//    var fu = $('#BodyHolder_FileUploadSmallLogo').val();
//    var file = [];
//    file = fu.split('\\');
//    var last = file.pop();
//    //alert(last);
//    //if (last.split('.')[1] != 'gif' || last.split('.')[1] != 'jpg' || last.split('.')[1] != 'jpeg' || last.split('.')[1] != 'png') {
//    //    alert("Please Select .PNG, .GIF, .JPEG or .JPG file to upload");
//    //}
//    //else {
//    if ($('#BodyHolder_FileUploadSmallLogo').val() == '') {
//        alert("Please select an image to upload.");
//    }
//    else {
//        //alert("pasok");
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SaveSmallLogo",
//            data: " { smalllogo : '" + last + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Small Logo uploaded successfully');
//            }
//        });
//    }
//    //}
//}

//function SaveBigLogo() {
//    var fu = $('#BodyHolder_FileUploadBigLogo').val();
//    var file = [];
//    file = fu.split('\\');
//    var last = file.pop();
//    //alert(last);
//    //if (last.split('.')[1] != 'gif' || last.split('.')[1] != 'jpg' || last.split('.')[1] != 'jpeg' || last.split('.')[1] != 'png') {
//    //    alert("Please Select .PNG, .GIF, .JPEG or .JPG file to upload");
//    //}
//    //else {
//    if ($('#BodyHolder_FileUploadBigLogo').val() == '') {
//        alert("Please select an image to upload.");
//    }
//    else {
//        //alert("pasok");
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SaveBigLogo",
//            data: " { biglogo : '" + last + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Big Logo uploaded successfully');
//            }
//        });
//    }
//    //}
//}

//function SaveLoginLogo() {
//    var fu = $('#BodyHolder_FileUploadLoginLogo').val();
//    var file = [];
//    file = fu.split('\\');
//    var last = file.pop();
//    //alert(last);
//    //if (last.split('.')[1] != 'gif' || last.split('.')[1] != 'jpg' || last.split('.')[1] != 'jpeg' || last.split('.')[1] != 'png') {
//    //    alert("Please Select .PNG, .GIF, .JPEG or .JPG file to upload");
//    //}
//    //else {
//    if ($('#BodyHolder_FileUploadLoginLogo').val() == '') {
//        alert("Please select an image to upload.");
//    }
//    else {
//        //alert("pasok");
//        $.ajax({
//            type: "POST",
//            url: "Miscellaneous.aspx/SaveLoginLogo",
//            data: " { loginlogo : '" + last + "'} ",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                alert('Login Logo uploaded successfully');
//            }
//        });
//    }
//    //}
//}