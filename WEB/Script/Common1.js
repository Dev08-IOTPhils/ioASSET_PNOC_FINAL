var usrinfnotif = '';
var url = "";


function UsrInfNotif() {


    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    $.ajax({
        type: "POST",
        url: context + "/GetUserInfo",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            usrinfnotif = msg.d;
            var admin1 = usrinfnotif.split('|')[0];
            var aprvr1 = usrinfnotif.split('|')[1];
            var cstdn = usrinfnotif.split('|')[4];

            var Li1 = document.getElementById("Li1");
            var a1 = document.getElementById("a1");
            var a2 = document.getElementById("a2");
            var a3 = document.getElementById("a3");
            var a4 = document.getElementById("a4");
            var a5 = document.getElementById("a5");
            var a6 = document.getElementById("a6");
            var a7 = document.getElementById("a7");
            var a8 = document.getElementById("a8");
            var a9 = document.getElementById("a9");
            var a10 = document.getElementById("a10");
            var a11 = document.getElementById("a11");
            var a12 = document.getElementById("a12");
            var a13 = document.getElementById("a13");

            var Li2 = document.getElementById("Li2");
            var v1 = document.getElementById("v1");
            var v2 = document.getElementById("v2");
            var v3 = document.getElementById("v3");
            var v4 = document.getElementById("v4");
            var v5 = document.getElementById("v5");
            var v6 = document.getElementById("v6");


            Li1.style.display = 'none';
            a1.style.display = 'none';
            a2.style.display = 'none';
            a3.style.display = 'none';
            a4.style.display = 'none';
            a5.style.display = 'none';
            a6.style.display = 'none';
            a7.style.display = 'none';
            a8.style.display = 'none';
            a9.style.display = 'none';
            a10.style.display = 'none';
            a11.style.display = 'none';
            a12.style.display = 'none';
            a13.style.display = 'none';

            Li2.style.display = 'none';
            v1.style.display = 'none';
            v2.style.display = 'none';
            v3.style.display = 'none';
            v4.style.display = 'none';
            v5.style.display = 'none';
            v6.style.display = 'none';

            if (aprvr1 == "True") {
                Li1.style.display = 'block';
                a1.style.display = 'block';
                a2.style.display = 'block';
                a3.style.display = 'block';
                a4.style.display = 'block';
                a5.style.display = 'block';
                a6.style.display = 'block';
                a7.style.display = 'block';
                a8.style.display = 'block';
                a9.style.display = 'block';
                //a10.style.display = 'block';
                a11.style.display = 'block';
                a12.style.display = 'block';
                a13.style.display = 'block';
            }
            if (cstdn1 == "True") {
                Li2.style.display = 'block';
                v1.style.display = 'block';
                v2.style.display = 'block';
                v3.style.display = 'block';
                v4.style.display = 'block';
                v5.style.display = 'block';
                v6.style.display = 'block';
            }
            if (admin1 == "True") {
                Li1.style.display = 'block';
                a1.style.display = 'block';
                a2.style.display = 'block';
                a3.style.display = 'block';
                a4.style.display = 'block';
                a5.style.display = 'block';
                a6.style.display = 'block';
                a7.style.display = 'block';
                a8.style.display = 'block';
                a9.style.display = 'block';
                //a10.style.display = 'block';
                a11.style.display = 'block';
                a12.style.display = 'block';
                a13.style.display = 'block';

                Li2.style.display = 'block';
                v1.style.display = 'block';
                v2.style.display = 'block';
                v3.style.display = 'block';
                v4.style.display = 'block';
                v5.style.display = 'block';
                v6.style.display = 'block';
            }
        }
    });
}


function ExportToExcel(htmlstring) {

    //var wb = XLSX.utils.table_to_book(htmlstring, { sheet: "sheet1" });
    //return   XLSX.writeFile(wb, fn || ('MySheetName.' + (type || 'xlsx')));
}
function ExportToExcelV2(type, fn, dl) {
    var elt = document.getElementById('BodyHolder_divPropertyPrintListRCPPE');
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
    return dl ?
      XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
      XLSX.writeFile(wb, fn || ('MySheetName.' + (type || 'xlsx')));
}
function Export2WordV2(htmlstring, filename) {
    var preHtml = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta charset='utf-8'><title>Export HTML To Doc</title></head><body>";
    var postHtml = "</body></html>";
    var html = preHtml + htmlstring + postHtml;

    console.log(html);

    var blob = new Blob(['\ufeff', html], {
        type: 'application/msword'
    });

    // Specify link url
    var url = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(html);

    // Specify file name
    filename = filename ? filename + '.doc' : 'document.doc';

    // Create download link element
    var downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = url;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }

    document.body.removeChild(downloadLink);
}

function addAttachment(transactionnumber, transactiontype) {
    $('#fuAttachment').val('');
    document.getElementById('lblTransactionNumber').innerHTML = transactionnumber;
    $('#txtTransactionNumber').val(transactionnumber);
    $('#txtTransactionType').val(transactiontype);
    LoadAttchments(transactionnumber, transactiontype);
    $('#modalAddAttachment').modal('show');
}

function DeleteAttachment(filename) {
    var context = window.location.pathname.substring(1);
    alert($('#txtTransactionNumber').val() + "----" + $('#txtTransactionType').val())
    $.ajax({
        type: 'POST',
        url: context + "/DeleteAttachment",
        data: "{transactiontype: '" + $('#txtTransactionType').val() + "', transactionnumber: '" + $('#txtTransactionNumber').val() + "', filename: '" + filename + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "file deleted.") {
                LoadAttchments($('#txtTransactionNumber').val(), $('#txtTransactionType').val());
            }
            else {

                ("Error", "File not found.", "error");
            }
        }
    });
}

function LoadAttchments(transactionnumber, transactiontype) {
    var context = window.location.pathname.substring(1);
    $.ajax({
        type: "POST",
        url: context + "/GetAttachments",
        data: "{ transactionnumber: '" + transactionnumber + "', transactiontype: '" + transactiontype + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#divAttachements').hide();
            $('#divAttachements').html(msg.d);
            $('#divAttachements').fadeIn(200);
        }
    });
}

function AddAttachment() {
    var transactionnumber = $('#txtTransactionNumber').val();
    var transactiontype = $('#txtTransactionType').val();
    var filess = $('#fuAttachment').val();
    var filename = $("#fuAttachment").get(0);
    if (filess == "") {
        filename = "";
    }
    if (filess == "") {
        swal("", "Select (jpg, png , gif, jpeg, pdf, txt, docx, xlsx) file to upload.", "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are You sure you want attach this document/s?",
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
                    var fileUpload = $("#fuAttachment").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: url + "/OperationUploadHandler.ashx?transactionnumber=" + transactionnumber + "|" + transactiontype,
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                $('#fuAttachment').val('');
                                LoadAttchments(transactionnumber, transactiontype);
                                swal("Successful", "Attachment successfully uploaded!", "success");
                            } else {
                                swal("Warning", result, "warning");
                            }
                        },
                        error: function (err) {
                            if (err.statusText == "error") {
                                swal("Warning", "You don't have access to the path.", "warning");
                            }
                            else {
                                swal("Warning", err.statusText, "warning");
                            }
                        }
                    });
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}


function Export2Word(element, filename) {
    var preHtml = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta charset='utf-8'><title>Export HTML To Doc</title></head><body>";
    var postHtml = "</body></html>";
    var html = preHtml + document.getElementById(element).innerHTML + postHtml;

    var blob = new Blob(['\ufeff', html], {
        type: 'application/msword'
    });

    // Specify link url
    var url = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(html);

    // Specify file name
    filename = filename ? filename + '.doc' : 'document.doc';

    // Create download link element
    var downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = url;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }

    document.body.removeChild(downloadLink);
}

function CountChar(text, counter, id, txtArea) {
    var len = text.length;

    if (len > counter) {
        text = text.substring(0, counter);
        $('#' + txtArea).val(text);
    }
    else {
        $('#' + id).text((counter - len) + " remaining character(s)");
    }
}

function ReplaceStr1(str) {
    var newStr = str.replace(/[*|\=?;:'"<>()\{\}\[\]\\\/]/gi, '');
    return newStr;
}

function ReplaceStr2(input) {
    // Remove all characters except for .,-
    let value = input.value.replace(/[^a-zA-Z0-9-., ]/g, '');
    input.value = value;


    //let value = input.value.replace(/[*|\=?;:'"<>()\{\}\[\]\\\/]/gi, '');

    //// Format the value
    //input.value = value;
}

function DivScrollDown(id) {
    $('#' + id).scrollTop($('#' + id)[0].scrollHeight);
}


function format(input) {
    var num = input.value.replace(/\,/g, '');
    if (!isNaN(num)) {
        if (num.indexOf('.') > -1) {
            num = num.split('.');
            num[0] = num[0].toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1,').split('').reverse().join('').replace(/^[\,]/, '');
            if (num[1].length > 2) {
                //alert('You may only enter two decimals!');
                num[1] = num[1].substring(0, num[1].length - 1);
            } input.value = num[0] + '.' + num[1];
        } else { input.value = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1,').split('').reverse().join('').replace(/^[\,]/, '') };
    }
    else {
        //alert('You may enter only numbers in this field!');
        input.value = input.value.substring(0, input.value.length - 1);
    }
}

var IsEmailUsername = '';
$(document).ready(function () {
    Object.assign(DataTable.defaults, {
        "fnInitComplete": function (oSettings, json) {
            $("#" + oSettings.sTableId + "_filter input").val("").trigger('keyup');
        }
    });
    //$('body').bind('cut paste', function (e) {
    //    e.preventDefault();
    //});
    $('.modal').on("hidden.bs.modal", function (e) {
        if ($('.modal:visible').length) {
            $('.modal-backdrop').first().css('z-index', parseInt($('.modal:visible').last().css('z-index')) - 10);
            $('body').addClass('modal-open');
        }
    }).on("show.bs.modal", function (e) {
        if ($('.modal:visible').length) {
            $('.modal-backdrop.in').first().css('z-index', parseInt($('.modal:visible').last().css('z-index')) + 10);
            $(this).css('z-index', parseInt($('.modal-backdrop.in').first().css('z-index')) + 10);
        }
    });


    $(document).on('change', '#chkCustodian', function () {
        if (this.checked) {
            $(".cls_Custodian").removeAttr("disabled");
            selectedCustodian = [];
        }
        else {
            $(".cls_Custodian").attr("disabled", true);
            $(".cls_Custodian").prop("checked", false);
            selectedCustodian = [];
        }
    });
    // Animate loader off screen
    $(".se-pre-con").fadeOut("slow");
    UsrInfNotif();
    LoadDatas();
    //GetCustodianElements();
    //isEmailUsername();
    GetMAccess();
    //assetinventoryaccount();
    //category();
    //model();
    //classification();
    //brand();
    //uom();
    //tag();
    //tagtype();
    //site();
    //site1();
    //locatioN();
    //department();
    //GetPosition();
    GetSupplier();
    GetUsers();
    //DeviceMngts();
    DeviceConnect();
    $("#logoclick").click(function () {
        window.location.href = "../Home/Dashboard.aspx";
    });
    $("#logoclick1").click(function () {
        window.location.href = "../Home/Dashboard.aspx";
    });

});

function LoadDatas() {
    var context = window.location.pathname.substring(1);
    if (context == 'Files_Data/Category.aspx') {
        assetinventoryaccount();
    } else if (context == 'User_Management/User.aspx') {
        GetPosition1();
        department();
        department1();
        GetPosition();
        site1();
        GetCustodianElements();
        isEmailUsername();
    } else if (context == 'Files_Data/Location.aspx') {
        site();
    } else if (context == 'Files_Data/Area.aspx') {
        site();
        locatioN();
    } else if (context == 'Files_Data/JobTitle.aspx') {
        department();
    } else if (context == 'Files_Data/Property.aspx') {
        assetinventoryaccount();
        category();
        model();
        classification();
        brand();
        uom();
        tag();
        tagtype();
    } else if (context == 'Files_Data/Stock.aspx') {
        assetinventoryaccount();
        category();
        model();
        classification();
        brand();
        uom();
        tag();
        tagtype();
    } else if (context == 'Files_Data/Tag.aspx') {
        tagtype();
    } else if (context == 'Operation_Module/PropertyTagging.aspx' || context == 'System/DeviceManagement.aspx') {
        DeviceMngts();
    } else if (context == 'Operation_Module/GoodsReceive.aspx') {
        GetSupplier();
        GetUsers();
        site();

        department1();
        GetPosition();
        GetPosition1();
        site1();
        //GetCustodianElements();
        isEmailUsername();
    } else if (context == 'Operation_Module/PropertyIssuance.aspx') {
        site1();
        department1();

        GetPosition1();
        department();
        GetPosition();
        //GetCustodianElements();
        isEmailUsername();
    } else if (context == 'Operation_Module/StockIssuance.aspx') {
        site1();
        department1();

        GetPosition1();
        department();
        GetPosition();
        //GetCustodianElements();
        isEmailUsername();
    } else if (context == 'Operation_Module/PropertyTransfer.aspx') {
        site1();
        department1();

        GetPosition1();
        department();
        GetPosition();
        //GetCustodianElements();
        isEmailUsername();

        site();
        site();
        locatioN();
    } else if (context == 'Operation_Module/PropertyReturn.aspx') {
        site1();
        department1();

        GetPosition1();
        department();
        GetPosition();
        //GetCustodianElements();
        isEmailUsername();
    } else if (context == 'Operation_Module/StockReturn.aspx') {
        site1();
        department1();

        GetPosition1();
        department();
        GetPosition();
        //GetCustodianElements();
        isEmailUsername();
    } else if (context == 'Operation_Module/InventoryCount.aspx') {
        site();
        site();
        locatioN();
    } else if (context == 'Operation_Module/DayPass.aspx') {
        site1();
        department1();

        GetPosition1();
        department();
        GetPosition();
        //GetCustodianElements();
        isEmailUsername();
    }

}

function isEmailUsername() {
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/GetUserAcccountSettings",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            IsEmailUsername = msg.d.split('|')[0];
        }
    });
}

function LogOut() {
    $.ajax({
        type: "POST",
        url: "../Login.aspx/LogOut",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            window.location.assign(msg.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "Login.aspx/LogOut2",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            window.location.assign(msg.d);
        }
    });
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////FILES DATA//////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var idForUpdate = 0;

function GetMAccess() {
    var context = window.location.pathname.substring(1);
    $.ajax({
        type: "POST",
        url: context + "/CheckA",
        data: " { ModuleAccess : '" + "Region|Branch|Department|Supplier|Job Title|Category|Model|Classification|Brand|Unit of Measurement|Tags|User|Office" + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            if (msg1.d != "") {
                var ModAccess = [];
                ModAccess = msg1.d.split('|');
                if (ModAccess[0] == "0") {
                    document.getElementById("HideThisRegion").style.display = "none";
                    var element = document.getElementById("hideregion");
                    element.classList.remove("input-group");

                    document.getElementById("HDSiteC").style.display = "none";
                    var element = document.getElementById("HDSite");
                    element.classList.remove("input-group");

                    document.getElementById("HTUserRegionC").style.display = "none";
                    var element = document.getElementById("HTUserRegion");
                    element.classList.remove("input-group");

                    document.getElementById("HTITRegionc").style.display = "none";
                    var element = document.getElementById("HTITRegion");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTRegionC").style.display = "none";
                    var element = document.getElementById("HTPTRegion");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTToRegionC").style.display = "none";
                    var element = document.getElementById("HTPTToRegion");
                    element.classList.remove("input-group");
                }
                if (ModAccess[1] == "0") {
                    document.getElementById("HDLocC").style.display = "none";
                    var element = document.getElementById("HDLoc");
                    element.classList.remove("input-group");

                    document.getElementById("HideThisBranch").style.display = "none";
                    var element = document.getElementById("hidebranch");
                    element.classList.remove("input-group");

                    document.getElementById("HTUserDeptC").style.display = "none";
                    var element = document.getElementById("HTUserDept");
                    element.classList.remove("input-group");

                    document.getElementById("HTITBranchC").style.display = "none";
                    var element = document.getElementById("HTITBranch");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTBranchC").style.display = "none";
                    var element = document.getElementById("HTPTBranch");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTToBranchC").style.display = "none";
                    var element = document.getElementById("HTPTToBranch");
                    element.classList.remove("input-group");
                }
                if (ModAccess[2] == "0") {
                    document.getElementById("HDDeptC").style.display = "none";
                    var element = document.getElementById("HDDept");
                    element.classList.remove("input-group");

                    document.getElementById("HTPIssuanceDeptC").style.display = "none";
                    var element = document.getElementById("HTPIssuanceDept");
                    element.classList.remove("input-group");

                    document.getElementById("HTSIssuanceDeptC").style.display = "none";
                    var element = document.getElementById("HTSIssuanceDept");
                    element.classList.remove("input-group");

                    document.getElementById("HTPReturnDeptC").style.display = "none";
                    var element = document.getElementById("HTPReturnDept");
                    element.classList.remove("input-group");
                }
                if (ModAccess[3] == "0") {
                    document.getElementById("HideThisSupplier").style.display = "none";
                    var element = document.getElementById("hidesupplier");
                    element.classList.remove("input-group");
                }
                if (ModAccess[4] == "0") {
                    document.getElementById("HTUserPositionC").style.display = "none";
                    var element = document.getElementById("HTUserPosition");
                    element.classList.remove("input-group");
                }
                if (ModAccess[5] == "0") {
                    document.getElementById("HTPropertyCatC").style.display = "none";
                    var element = document.getElementById("HTPropertyCat");
                    element.classList.remove("input-group");

                    document.getElementById("HTStockCatC").style.display = "none";
                    var element = document.getElementById("HTStockCat");
                    element.classList.remove("input-group");
                }
                if (ModAccess[6] == "0") {
                    document.getElementById("HTPropertyModelC").style.display = "none";
                    var element = document.getElementById("HTPropertyModel");
                    element.classList.remove("input-group");

                    document.getElementById("HTStockModelC").style.display = "none";
                    var element = document.getElementById("HTStockModel");
                    element.classList.remove("input-group");
                }
                if (ModAccess[7] == "0") {
                    document.getElementById("HTPropertyClassificationC").style.display = "none";
                    var element = document.getElementById("HTPropertyClassification");
                    element.classList.remove("input-group");

                    document.getElementById("HTStockClassificationC").style.display = "none";
                    var element = document.getElementById("HTStockClassification");
                    element.classList.remove("input-group");
                }
                if (ModAccess[8] == "0") {
                    document.getElementById("HTPropertyBrandC").style.display = "none";
                    var element = document.getElementById("HTPropertyBrand");
                    element.classList.remove("input-group");

                    document.getElementById("HTStockBrandC").style.display = "none";
                    var element = document.getElementById("HTStockBrand");
                    element.classList.remove("input-group");
                }
                if (ModAccess[9] == "0") {
                    document.getElementById("HTPropertyUoMC").style.display = "none";
                    var element = document.getElementById("HTPropertyUoM");
                    element.classList.remove("input-group");

                    document.getElementById("HTStockUoMC").style.display = "none";
                    var element = document.getElementById("HTStockUoM");
                    element.classList.remove("input-group");
                }
                if (ModAccess[10] == "0") {
                    document.getElementById("HTPropertyTagC").style.display = "none";
                    var element = document.getElementById("HTPropertyTag");
                    element.classList.remove("input-group");

                    document.getElementById("HTStockTagC").style.display = "none";
                    var element = document.getElementById("HTStockTag");
                    element.classList.remove("input-group");
                }
                if (ModAccess[11] == "0") {
                    document.getElementById("HideThisReceivedBy").style.display = "none";
                    var element = document.getElementById("hidereceivedby");
                    element.classList.remove("input-group");

                    document.getElementById("HideThisEmployee").style.display = "none";
                    var element = document.getElementById("hideemployee");
                    element.classList.remove("input-group");

                    document.getElementById("HideThisApprover").style.display = "none";
                    var element = document.getElementById("hideapprover");
                    element.classList.remove("input-group");

                    document.getElementById("HTPReturnEmployeeC").style.display = "none";
                    var element = document.getElementById("HTPReturnEmployee");
                    element.classList.remove("input-group");

                    document.getElementById("HTSReturnEmployeeC").style.display = "none";
                    var element = document.getElementById("HTSReturnEmployee");
                    element.classList.remove("input-group");

                    document.getElementById("HTSDayPassApproverC").style.display = "none";
                    var element = document.getElementById("HTSDayPassApprover");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTFromPersonC").style.display = "none";
                    var element = document.getElementById("HTPTFromPerson");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTToPersonC").style.display = "none";
                    var element = document.getElementById("HTPTToPerson");
                    element.classList.remove("input-group");

                    document.getElementById("HTSIssuanceEmpC").style.display = "none";
                    var element = document.getElementById("HTSIssuanceEmp");
                    element.classList.remove("input-group");

                    document.getElementById("HTPIssuanceEmployeeC").style.display = "none";
                    var element = document.getElementById("HTPIssuanceEmployee");
                    element.classList.remove("input-group");
                }
                if (ModAccess[12] == "0") {
                    document.getElementById("HTITOfficeC").style.display = "none";
                    var element = document.getElementById("HTITOffice");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTOfficeC").style.display = "none";
                    var element = document.getElementById("HTPTOffice");
                    element.classList.remove("input-group");

                    document.getElementById("HTPTToOfficeC").style.display = "none";
                    var element = document.getElementById("HTPTToOffice");
                    element.classList.remove("input-group");
                }
            } else {
                document.getElementById("HideThisRegion").style.display = "none";
                var element = document.getElementById("hideregion");
                element.classList.remove("input-group");

                document.getElementById("HDSiteC").style.display = "none";
                var element = document.getElementById("HDSite");
                element.classList.remove("input-group");

                document.getElementById("HTUserRegionC").style.display = "none";
                var element = document.getElementById("HTUserRegion");
                element.classList.remove("input-group");

                document.getElementById("HDLocC").style.display = "none";
                var element = document.getElementById("HDLoc");
                element.classList.remove("input-group");

                document.getElementById("HideThisBranch").style.display = "none";
                var element = document.getElementById("hidebranch");
                element.classList.remove("input-group");

                document.getElementById("HTUserDeptC").style.display = "none";
                var element = document.getElementById("HTUserDept");
                element.classList.remove("input-group");

                document.getElementById("HDDeptC").style.display = "none";
                var element = document.getElementById("HDDept");
                element.classList.remove("input-group");

                document.getElementById("HideThisSupplier").style.display = "none";
                var element = document.getElementById("hidesupplier");
                element.classList.remove("input-group");

                document.getElementById("HTUserPositionC").style.display = "none";
                var element = document.getElementById("HTUserPosition");
                element.classList.remove("input-group");

                document.getElementById("HTPropertyCatC").style.display = "none";
                var element = document.getElementById("HTPropertyCat");
                element.classList.remove("input-group");

                document.getElementById("HTStockCatC").style.display = "none";
                var element = document.getElementById("HTStockCat");
                element.classList.remove("input-group");

                document.getElementById("HTPropertyModelC").style.display = "none";
                var element = document.getElementById("HTPropertyModel");
                element.classList.remove("input-group");

                document.getElementById("HTStockModelC").style.display = "none";
                var element = document.getElementById("HTStockModel");
                element.classList.remove("input-group");

                document.getElementById("HTPropertyClassificationC").style.display = "none";
                var element = document.getElementById("HTPropertyClassification");
                element.classList.remove("input-group");

                document.getElementById("HTStockClassificationC").style.display = "none";
                var element = document.getElementById("HTStockClassification");
                element.classList.remove("input-group");

                document.getElementById("HTPropertyBrandC").style.display = "none";
                var element = document.getElementById("HTPropertyBrand");
                element.classList.remove("input-group");

                document.getElementById("HTStockBrandC").style.display = "none";
                var element = document.getElementById("HTStockBrand");
                element.classList.remove("input-group");

                document.getElementById("HTPropertyUoMC").style.display = "none";
                var element = document.getElementById("HTPropertyUoM");
                element.classList.remove("input-group");

                document.getElementById("HTStockUoMC").style.display = "none";
                var element = document.getElementById("HTStockUoM");
                element.classList.remove("input-group");

                document.getElementById("HTPropertyTagC").style.display = "none";
                var element = document.getElementById("HTPropertyTag");
                element.classList.remove("input-group");

                document.getElementById("HTStockTagC").style.display = "none";
                var element = document.getElementById("HTStockTag");
                element.classList.remove("input-group");

                document.getElementById("HTPIssuanceDeptC").style.display = "none";
                var element = document.getElementById("HTPIssuanceDept");
                element.classList.remove("input-group");

                document.getElementById("HTSIssuanceDeptC").style.display = "none";
                var element = document.getElementById("HTSIssuanceDept");
                element.classList.remove("input-group");

                document.getElementById("HTPReturnDeptC").style.display = "none";
                var element = document.getElementById("HTPReturnDept");
                element.classList.remove("input-group");

                document.getElementById("HTITRegionc").style.display = "none";
                var element = document.getElementById("HTITRegion");
                element.classList.remove("input-group");

                document.getElementById("HTPTRegionC").style.display = "none";
                var element = document.getElementById("HTPTRegion");
                element.classList.remove("input-group");

                document.getElementById("HTPTToRegionC").style.display = "none";
                var element = document.getElementById("HTPTToRegion");
                element.classList.remove("input-group");

                document.getElementById("HTITBranchC").style.display = "none";
                var element = document.getElementById("HTITBranch");
                element.classList.remove("input-group");

                document.getElementById("HTPTBranchC").style.display = "none";
                var element = document.getElementById("HTPTBranch");
                element.classList.remove("input-group");

                document.getElementById("HTPTToBranchC").style.display = "none";
                var element = document.getElementById("HTPTToBranch");
                element.classList.remove("input-group");

                document.getElementById("HTITOfficeC").style.display = "none";
                var element = document.getElementById("HTITOffice");
                element.classList.remove("input-group");

                document.getElementById("HTPTOfficeC").style.display = "none";
                var element = document.getElementById("HTPTOffice");
                element.classList.remove("input-group");

                document.getElementById("HTPTToOfficeC").style.display = "none";
                var element = document.getElementById("HTPTToOffice");
                element.classList.remove("input-group");

                document.getElementById("HTPReturnEmployeeC").style.display = "none";
                var element = document.getElementById("HTPReturnEmployee");
                element.classList.remove("input-group");

                document.getElementById("HTSReturnEmployeeC").style.display = "none";
                var element = document.getElementById("HTSReturnEmployee");
                element.classList.remove("input-group");

                document.getElementById("HTSDayPassApproverC").style.display = "none";
                var element = document.getElementById("HTSDayPassApprover");
                element.classList.remove("input-group");

                document.getElementById("HTPTFromPersonC").style.display = "none";
                var element = document.getElementById("HTPTFromPerson");
                element.classList.remove("input-group");

                document.getElementById("HTPTToPersonC").style.display = "none";
                var element = document.getElementById("HTPTToPerson");
                element.classList.remove("input-group");

                document.getElementById("HTSIssuanceEmpC").style.display = "none";
                var element = document.getElementById("HTSIssuanceEmp");
                element.classList.remove("input-group");

                document.getElementById("HTPIssuanceEmployeeC").style.display = "none";
                var element = document.getElementById("HTPIssuanceEmployee");
                element.classList.remove("input-group");
            }
        }
    });
}

var AssetInventoryAccountArray = new Array();
function assetinventoryaccount() {
    var ddlAssetInventoryAccount = $('#ddlAssetInventoryAccount');
    ddlAssetInventoryAccount.empty();
    ddlAssetInventoryAccount.append($('<option>', {
        value: '0',
        text: '--- Select Inventory Account ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: 'Files_Data/Category.aspx/GetAssetInventoryAccount',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                AssetInventoryAccountArray = JSON.parse(xml.d);
                AssetInventoryAccountArray.forEach((inventoryaccount, index) => {
                    ddlAssetInventoryAccount.append($('<option>', {
                        value: inventoryaccount.ChartOfAccountsID,
                        text: inventoryaccount.AccountNumber,
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

var CategoryArray = new Array();
function category() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlCategory');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select Category ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetCetagoryList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                CategoryArray = JSON.parse(xml.d);
                CategoryArray.forEach((category, index) => {
                    ddlCategory.append($('<option>', {
                        value: category.CategoryID,
                        text: category.CategoryName,
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


var ModelArray = new Array();
function model() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlModel');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select Model ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetModelList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                ModelArray = JSON.parse(xml.d);
                ModelArray.forEach((model, index) => {
                    ddlCategory.append($('<option>', {
                        value: model.ModelID,
                        text: model.ModelName,
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


var ClassificationArray = new Array();
function classification() {
    var context = window.location.pathname.substring(1);
    var ddlClassification = $('#ddlClassification');
    ddlClassification.empty();
    ddlClassification.append($('<option>', {
        value: '0',
        text: '--- Select Classification ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetClassificationList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                ClassificationArray = JSON.parse(xml.d);
                ClassificationArray.forEach((classification, index) => {
                    ddlClassification.append($('<option>', {
                        value: classification.CLassificationID,
                        text: classification.ClassificationName,
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


var BrandArray = new Array();
function brand() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlBrand');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select Brand ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetBrandList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                BrandArray = JSON.parse(xml.d);
                BrandArray.forEach((brand, index) => {
                    ddlCategory.append($('<option>', {
                        value: brand.BrandID,
                        text: brand.BrandName,
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


var UoMArray = new Array();
function uom() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlUnitOfMeasurement');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select Unit Of Measurement ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetUnitOfMeasurementList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                UoMArray = JSON.parse(xml.d);
                UoMArray.forEach((uom, index) => {
                    ddlCategory.append($('<option>', {
                        value: uom.UnitOfMeasurementID,
                        text: uom.UnitOfMeasurement,
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


var TagArray = new Array();
function tag() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlTag');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select Tag ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetTag',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                TagArray = JSON.parse(xml.d);
                TagArray.forEach((tag, index) => {
                    ddlCategory.append($('<option>', {
                        value: tag.TagID,
                        text: tag.TagName,
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

var TagTypeArray = new Array();
function tagtype() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlTagType');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select Tag Type ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetTagType',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                TagTypeArray = JSON.parse(xml.d);
                TagTypeArray.forEach((tag, index) => {
                    ddlCategory.append($('<option>', {
                        value: tag.TagTypeID,
                        text: tag.TagType,
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


var SiteArray = new Array();
function site() {
    var sitename = document.getElementById("lblSiteAdduser").textContent;

    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlWarehouse');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select ' + sitename + ' ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetSite',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                SiteArray = JSON.parse(xml.d);
                SiteArray.forEach((site, index) => {
                    ddlCategory.append($('<option>', {
                        value: site.WarehouseID,
                        text: site.WarehouseName,
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

var SiteArray1 = new Array();
function site1() {
    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlWarehouse1');
    var sitename = document.getElementById("lblSiteAdduser").textContent;

    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select ' + sitename + ' ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetSite',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                SiteArray1 = JSON.parse(xml.d);
                SiteArray1.forEach((site, index) => {
                    ddlCategory.append($('<option>', {
                        value: site.WarehouseID,
                        text: site.WarehouseName,
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


var LocationArray = new Array();
function locatioN() {
    var locName = document.getElementById("lblLocAreaDialog").textContent.replace(":", "");

    var context = window.location.pathname.substring(1);
    var ddlCategory = $('#ddlLocation');
    ddlCategory.empty();
    ddlCategory.append($('<option>', {
        value: '0',
        text: '--- Select ' + locName + ' ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetLocation',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                LocationArray = JSON.parse(xml.d);
                LocationArray.forEach((loc, index) => {
                    ddlCategory.append($('<option>', {
                        value: loc.LocationID,
                        text: loc.Location,
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


var DepartmentArray = new Array();
function department() {
    var context = window.location.pathname.substring(1);
    var ddlDept = $('#ddlDepartment');
    ddlDept.empty();
    ddlDept.append($('<option>', {
        value: '0',
        text: '--- Select User Department ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetDepartmentList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DepartmentArray = JSON.parse(xml.d);
                DepartmentArray.forEach((dep, index) => {
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


var DepartmentArray1 = new Array();
function department1() {
    var context = window.location.pathname.substring(1);
    var ddlDept = $('#ddlDepartment1');
    ddlDept.empty();
    ddlDept.append($('<option>', {
        value: '0',
        text: '--- Select User Department ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetDepartmentList1',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DepartmentArray1 = JSON.parse(xml.d);
                DepartmentArray1.forEach((dep, index) => {
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


var positionlist = new Array();
function GetPosition() {
    var context = window.location.pathname.substring(1);
    var ddlPosition = $('#ddlPosition');
    ddlPosition.empty();
    ddlPosition.append($('<option>', {
        value: '0',
        text: '--- Select Job Position ---',
        rowindex: -1
    }));
    var departmentid = $('#ddlDepartment').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetPosition',
        data: "{ department : '" + departmentid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                positionlist = JSON.parse(xml.d);
                positionlist.forEach((position, index) => {
                    ddlPosition.append($('<option>', {
                        value: position.PositionID,
                        text: position.PositionName,
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

var positionlist1 = new Array();
function GetPosition1() {
    var context = window.location.pathname.substring(1);
    var ddlPosition = $('#ddlPosition');
    ddlPosition.empty();
    ddlPosition.append($('<option>', {
        value: '0',
        text: '--- Select Job Position ---',
        rowindex: -1
    }));
    var departmentid = $('#ddlDepartment1').val();
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetPosition',
        data: "{ department : '" + departmentid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                positionlist1 = JSON.parse(xml.d);
                positionlist1.forEach((position, index) => {
                    ddlPosition.append($('<option>', {
                        value: position.PositionID,
                        text: position.PositionName,
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


var supplier = new Array();
function GetSupplier() {
    var context = window.location.pathname.substring(1);
    var ddlSupplier = $('#ddlSupplier');
    ddlSupplier.empty();
    ddlSupplier.append($('<option>', {
        value: '0',
        text: '--- Select Supplier ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetSupplier',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                supplier = JSON.parse(xml.d);
                supplier.forEach((sup, index) => {
                    ddlSupplier.append($('<option>', {
                        value: sup.SupplierID,
                        text: sup.SupplierName,
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

var users = new Array();
function GetUsers() {
    var context = window.location.pathname.substring(1);
    var ddlSupplier = $('#ddlEmployee');
    ddlSupplier.empty();
    ddlSupplier.append($('<option>', {
        value: '0',
        text: '--- Select Assigned Person ---',
        rowindex: -1
    }));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetEmployee',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                users = JSON.parse(xml.d);
                users.forEach((usr, index) => {
                    ddlSupplier.append($('<option>', {
                        value: usr.UserID,
                        text: usr.FullName,
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

var DeviceMngt = new Array();
function DeviceMngts() {
    var context = window.location.pathname.substring(1);
    var ddlDM = $('#ddlDeviceUsageType');
    ddlDM.empty();
    //ddlDM.append($('<option>', {
    //    value: '0',
    //    text: 'NA',
    //    rowindex: -1
    //}));
    $.ajax({
        //async: false,
        type: 'POST',
        url: context + '/GetDeviceUsageType',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DeviceMngt = JSON.parse(xml.d);
                DeviceMngt.forEach((usr, index) => {
                    ddlDM.append($('<option>', {
                        value: usr.DeviceTypeID,
                        text: usr.DeviceType,
                        rowindex: index
                    }));
                });
            } catch (err) {
                console.log(err.message);
            }

        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}

var DeviceCon = new Array();
function DeviceConnect() {
    var context = window.location.pathname.substring(1);
    var ddlDM = $('#ddlPrinter');
    ddlDM.empty();
    ddlDM.append($('<option>', {
        value: '-1',
        text: '--- Select Device ---',
        rowindex: -1
    }));
    ddlDM.append($('<option>', {
        value: '0',
        text: 'Paper Print',
        rowindex: -1
    }));
    $.ajax({
        //async: false,
        type: 'POST',
        url: context + '/GetDeviceList',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                DeviceCon = JSON.parse(xml.d);
                DeviceCon.forEach((usr, index) => {
                    ddlDM.append($('<option>', {
                        value: usr.DeviceID,
                        text: usr.DeviceName,
                        rowindex: index
                    }));
                });
            } catch (err) {
                console.log(err.message);
            }

        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}



//SavingAdding
function SaveCategory() {
    var cate = ReplaceStr1($('#txtCategoryName').val().trim().replace(/  +/g, ''));
    var desc = ReplaceStr1($('#txtDescriptionCat').val().replace(/  +/g, ''));
    var alertMsg;

    if (ReplaceStr1($('#txtCategoryName').val().trim().replace(/  +/g, '')) == '' || ReplaceStr1($('#txtDescriptionCat').val().trim().replace(/  +/g, '')) == '' ||
    $('#ddlAssetInventoryAccount').val() == 0) {
        alertMsg = '';
        if ($('#ddlAssetInventoryAccount').val() == 0)
            alertMsg += '\nInventory Account';
        if (ReplaceStr1($('#txtCategoryName').val().trim().replace(/  +/g, '')) == '')
            alertMsg += '\nCategory Name';
        if (ReplaceStr1($('#txtDescriptionCat').val().trim().replace(/  +/g, '')) == '')
            alertMsg += '\nDescription';
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this category?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/SaveCategory",
                    data: " { id : '" + idForUpdate + "' , category : '" + cate + "', description :'" + desc + "',inventoryaccount: '" + $('#ddlAssetInventoryAccount').val() + "', active:'" + document.getElementById("chkActiveCat").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        category();
                        if (msg.d == 0) {
                            $("#addCategoryDialogBox").modal("hide");
                            swal("Successful", "Category successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addCategoryDialogBox").modal("hide");
                            swal("Successful", "Category successfully modified!", "success");
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Category already exist!", "error");
                        }
                        else {
                            swal("Error", "Category cannot be set to inactive. This is being actively used in property and/or supply!", "error");
                            $("#addCategoryDialogBox").modal("hide");
                        }
                        load_dataCat();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

function addCategoryDialogBox() {
    idForUpdate = 0;
    document.getElementById("chkActiveCat").checked = true;
    $('#ddlAssetInventoryAccount').val(0).trigger('change');
    $('#txtCategoryName').val("");
    $('#txtDescriptionCat').val("");
    $("#addCategoryDialogBox").modal("show");
    $('#cntCat').text("500 remaining character(s)");
    var context = window.location.pathname.substring(1);
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditCat");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveCategory',
                    class: 'btn btn-success btn_EditCat',
                    onclick: 'SaveCategory()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });
                $('#SaveCancelCategory').prepend($formBrand);
            }
        }
    });
}


function addModelDialogBox() {
    idForUpdate = 0;
    document.getElementById("chkActiveModel").checked = true;
    $('#txtDescriptionModel').val("");
    $('#txtModel').val("");
    $('#cntModel').text("500 remaining character(s)");
    $("#addModelDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditModel");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveModel',
                    class: 'btn btn-success btn_EditModel',
                    onclick: 'SaveModel()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelModel').prepend($formBrand);
            }
        }
    });
}


function SaveModel() {
    var mode = ReplaceStr1($('#txtModel').val().trim().replace(/  +/g, ''));

    var desc = ReplaceStr1($('#txtDescriptionModel').val().trim().replace(/  +/g, ' '));

    //var mo = $('#BodyHolder_txtModel').val().trim();
    //var mod = mo.replace("'", "`");
    //var mode = mod.replace("\\", "");
    //var de = $('#txtDescription').val();
    //var des = de.replace("'", "`");
    //var desc = des.replace("\\", "");

    var alertMsg;
    if (ReplaceStr1($('#txtModel').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionModel').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = '';
        if (ReplaceStr1($('#txtModel').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nModel ';
        if (ReplaceStr1($('#txtDescriptionModel').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        //alert(alertMsg);

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this model?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/SaveModel",
                    data: " { modelid : " + idForUpdate + ",modelname : '" + mode + "', description:'" + desc + "',  active:'" + document.getElementById("chkActiveModel").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addModelDialogBox").modal("hide");
                            swal("Successful", "Model successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addModelDialogBox").modal("hide");
                            swal("Successful", "Model successfully modified!", "success");
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Model already exist!", "error");
                        }
                        else {
                            swal("Error", "Model cannot be set to inactive. This is being actively used in property and/or supply!", "error");
                            $("#addModelDialogBox").modal("hide");
                        }

                        model();
                        load_dataModel("");
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function addClassificationDialogBox() {
    idForUpdate = 0;
    document.getElementById("chkActiveClassification").checked = true;
    $('#txtDescriptionClassification').val("");
    $('#txtClassificationName').val("");
    $('#cntClassification').text("500 remaining character(s)")
    $("#addClassificationDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditClass");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveClassification',
                    class: 'btn btn-success btn_EditClass',
                    onclick: 'SaveClassification()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelClass').prepend($formBrand);
            }
        }
    });
}


function SaveClassification() {
    var cate = ReplaceStr1($('#txtClassificationName').val().trim().replace(/  +/g, ' '));
    var catc = ReplaceStr1($('#txtClassificationCode').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionClassification').val().trim().replace(/  +/g, ' '));

    var alertMsg;

    if (ReplaceStr1($('#txtClassificationName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionClassification').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionClassification').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = ' ';

        if (ReplaceStr1($('#txtClassificationName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nClassification Name';
        if (ReplaceStr1($('#txtClassificationCode').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nClassification Code';
        if (ReplaceStr1($('#txtDescriptionClassification').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this classification?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/SaveClassification",
                    data: " { id : '" + idForUpdate + "' , classification : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActiveClassification").checked + "', classificationcode:'" + catc + "', floater:'" + document.getElementById("chkActiveIsFloater").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg1) {
                        if (msg1.d == 0) {
                            $("#addClassificationDialogBox").modal("hide");
                            swal("Successful", "Classification successfully saved!", "success");
                        }
                        else if (msg1.d == 1) {
                            $("#addClassificationDialogBox").modal("hide");
                            swal("Successful", "Classification successfully modified!", "success");
                        }
                        else if (msg1.d == 2) {
                            swal("Error", "Classification already exist!", "error");
                        }
                        else if (msg1.d == 5) {
                            swal("Error", "Fill-out all the required field:\nClassification Code", "error");
                        }
                        else {
                            swal("Error", "Classification cannot be set to inactive. This is being actively used in property and/or supply!", "error");
                            $("#addClassificationDialogBox").modal("hide");
                        }
                        classification();
                        load_dataClassification();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}



function addBrandDialogBox() {
    idForUpdate = 0;
    document.getElementById("chkActiveBrand").checked = true;
    $('#txtBrandName').val("");
    $('#txtDescriptionBrand').val("");
    $('#cntBrand').text("500 remaining character(s)")
    $("#addBrandDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditBrand");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveBrand',
                    class: 'btn btn-success btn_EditBrand',
                    onclick: 'SaveBrand()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });
                $('#SaveCancelBrand').prepend($formBrand);
            }
        }
    });
}

function SaveBrand() {
    var cate = ReplaceStr1($('#txtBrandName').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionBrand').val().trim().replace(/  +/g, ' '));
    var alertMsg;
    if (ReplaceStr1($('#txtBrandName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionBrand').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = '';

        if (ReplaceStr1($('#txtBrandName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nBrand Name';
        if (ReplaceStr1($('#txtDescriptionBrand').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        //if (confirm("Are you sure you want to save this Brand?") == false)
        //    return false;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this brand?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/SaveBrand",
                    data: " { id : '" + idForUpdate + "' , brand : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActiveBrand").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addBrandDialogBox").modal("hide");
                            swal("Successful", "Brand successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addBrandDialogBox").modal("hide");
                            swal("Successful", "Brand successfully modified!", "success");
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Brand already exist!", "error");
                        }
                        else {
                            swal("Error", "Brand cannot be set to inactive. This is being actively used in property and/or supply!", "error");
                            $("#addBrandDialogBox").modal("hide");
                        }
                        brand();
                        load_dataBrand();
                    }
                })
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}



function addUnitOfMeasurementDialogBox() {
    idForUpdate = 0;
    document.getElementById("chkActiveUOM").checked = true;
    $('#txtUnitOfMeasurement').val("");
    $('#txtUnitOfMeasurementCode').val("");
    $('#txtDescriptionUOM').val("");
    $('#cntUOM').text("500 remaining character(s)")
    $("#addUnitOfMeasurementDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditUOM");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveUnitOfMeasurement',
                    class: 'btn btn-success btn_EditUOM',
                    onclick: 'SaveUnitOfMeasurement()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelUOM').prepend($formBrand);
            }
        }
    });
}

function SaveUnitOfMeasurement() {
    var cate = ReplaceStr1($('#txtUnitOfMeasurement').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionUOM').val().trim().replace(/  +/g, ' '));
    var uomcode = ReplaceStr1($('#txtUnitOfMeasurementCode').val().trim().replace(/  +/g, ' '));
    //alert(idForUpdate);
    var alertMsg;
    //alert( "id : '" + idForUpdate + "' , unitofmeasurement : '" + cate + "', code: '" + $('#txtUnitOfMeasurementCode').val() + "', description :'" + desc + "', active:'" + document.getElementById("chkActive").checked + "', pageNo : '" + currentPage + "'");
    if (ReplaceStr1($('#txtUnitOfMeasurement').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtUnitOfMeasurementCode').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionUOM').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = '';

        if (ReplaceStr1($('#txtUnitOfMeasurement').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nUnit Of Measurement';
        if (ReplaceStr1($('#txtUnitOfMeasurementCode').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nUnit Of Measurement Code';
        if (ReplaceStr1($('#txtDescriptionUOM').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        //if ($('#txtLife').val() == '')
        //    alertMsg += '\nUseful Life';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this unit of measurement?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/CheckUOMCode",
                    data: " { id : '" + idForUpdate + "',cc: '" + $('#txtUnitOfMeasurementCode').val() + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 2) {
                            $.ajax({
                                type: "POST",
                                url: "Files_Data/UnitOfMeasurement.aspx/SaveUnitOfMeasurement",
                                data: " { id : '" + idForUpdate + "' , unitofmeasurement : '" + cate + "', code: '" + uomcode + "', description :'" + desc + "', active:'" + document.getElementById("chkActiveUOM").checked + "'} ",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.d == 0) {
                                        $("#addUnitOfMeasurementDialogBox").modal("hide");
                                        swal("Successful", "Unit of measurement successfully saved!", "success");
                                        //Clear();
                                    }
                                    else if (msg.d == 1) {
                                        $("#addUnitOfMeasurementDialogBox").modal("hide");
                                        swal("Successful", "Unit of measurement successfully modified!", "success");
                                        //Clear();
                                    }
                                    else if (msg.d == 2) {
                                        swal("Error", "Unit of measurement already exist!", "error");
                                    }
                                    else {
                                        swal("Error", "Unit of measurement cannot be set to inactive. This is being actively used in property and/or supply!", "error");
                                        $("#addUnitOfMeasurementDialogBox").modal("hide");
                                        // Clear();
                                    }
                                    uom();
                                    load_dataUOM();
                                }
                            });
                        }
                        else {
                            //alert('Unit Of Measurement Code Already Exist!');
                            swal("Error", "Unit Of Measurement Code Already Exist!", "error");
                        }
                    },
                    error: function (msg1) {
                        alert(msg1.d);
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}


function addTagDialogBox() {

    $('#ddlTagType').val("0").trigger('change');
    $('#txtTag').val("");
    $('#txtTag').prop('disabled', false);
    $('#txtDescriptionTag').val("");
    $('#ddlTagType').val("0");
    CountChar($('#txtDescriptionTag').val(), 500, 'cntTag', 'txtDescriptionTag');
    idForUpdate = 0;
    document.getElementById("chkActiveTag").checked = true;
    $("#addTagDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditTag");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveTag',
                    class: 'btn btn-success btn_EditTag',
                    onclick: 'SaveTag()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelTag').prepend($formBrand);
            }
        }
    });

    //Clear();
}

function SaveTag() {
    var alertMsg;

    var loca = ReplaceStr1($('#txtTag').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionTag').val().trim().replace(/  +/g, ' '));

    if (ReplaceStr1($('#txtTag').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionTag').val().trim().replace(/  +/g, ' ')) == '' || $('#ddlTagType').val() == "--- Select Tag Type ---") {
        alertMsg = ' ';
        if ($('#ddlTagType').val() == 0)
            alertMsg += '\nTag Type';
        if (ReplaceStr1($('#txtTag').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nTag ';
        if (ReplaceStr1($('#txtDescriptionTag').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this tag?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/SaveTag",
                    data: " { tagid : '" + idForUpdate + "', tag : '" + loca + "', tagtypeid : '" + $('#ddlTagType').val() + "', description:'" + desc + "', active:'" + document.getElementById("chkActiveTag").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addTagDialogBox").modal("hide");
                            swal("Successful", "Tag Successfully Saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addTagDialogBox").modal("hide");
                            swal("Successful", "Tag Successfully Modified!", "success");
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Tag Already Exist!", "error");
                        }
                        else {
                            swal("Error", "Tag cannot be set to inactive. This is being actively used in property/supply!", "error");
                            $("#addTagDialogBox").modal("hide");
                            //Clear();
                        }
                        tag();
                        load_dataTag();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function addTagTypeDialogBox() {
    idForUpdate = 0;
    document.getElementById("chkActiveTagType").checked = true;
    $("#addTagTypeDialogBox").modal("show");
}


function SaveTagType() {
    var cate = ReplaceStr1($('#txtTagType').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionTagType').val().replace(/  +/g, ' '));
    var alertMsg;
    if ($('#txtTagType').val().trim() == '' || $('#txtDescriptionTagType').val().trim() == '') {
        alertMsg = 'Fill-out all the required field(s): ';

        if ($('#txtTagType').val() == '')
            alertMsg += '\nTagType';
        if ($('#txtDescriptionTagType').val() == '')
            alertMsg += '\nDescription';
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this tag?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var context = window.location.pathname.substring(1);
                $.ajax({
                    type: "POST",
                    url: context + "/SaveTagType",
                    data: " { id : '" + idForUpdate + "' , TagType : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActiveTagType").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addTagTypeDialogBox").modal("hide");
                            swal("Successful", "Tag type successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addTagTypeDialogBox").modal("hide");
                            swal("Successful", "Tag type successfully modified!", "success");
                        }
                        else {
                            $("#addTagTypeDialogBox").modal("hide");
                            swal("Error", "Tag type already exist!", "error");
                        }
                        tagtype();
                    }

                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

var WMLocation = '';
function addLocationDialogBox(WModule) {
    idForUpdate = 0;
    WMLocation = WModule;
    $('#txtLocation').val("");
    $('#txtLocationCode').val("");
    $('#txtLocation').prop('disabled', false);
    $('#txtDescriptionLocation').val("");
    $('#ddlWarehouse').val("0").trigger('change');
    CountChar($('#txtDescriptionLocation').val(), 500, 'cntLocation', 'txtDescriptionLocation')
    document.getElementById("chkActiveLocation").checked = true;
    $("#addLocationDialogBox").modal("show");
}

function SaveLocation() {
    var alertMsg;

    var loca = ReplaceStr1($('#txtLocation').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionLocation').val().trim().replace(/  +/g, ' '));

    var locacode = ReplaceStr1($('#txtLocationCode').val().trim().replace(/  +/g, ' '));

    if (ReplaceStr1($('#txtLocation').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtLocationCode').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionLocation').val().trim().replace(/  +/g, ' ')) == '' || $('#ddlWarehouse').val() == "0") {
        alertMsg = ' ';
        if ($('#ddlWarehouse').val() == '0')
            alertMsg += '\nSite';
        if (ReplaceStr1($('#txtLocation').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nLocation ';
        if (ReplaceStr1($('#txtLocationCode').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nLocation Code';
        if (ReplaceStr1($('#txtDescriptionLocation').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        //alert(alertMsg);

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
        //alert(" { location : '" + $('#txtLocation').val() + "', warehouseid : '" + $('#BodyHolder_ddlWarehouse').val() + "', description:'" + $('#txtDescription').val() + "', active:'" + document.getElementById("chkActive").checked + "', pageNo : '" + currentPage + "'} ");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this location?",
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
                    url: "Files_Data/Location.aspx/CheckLC",
                    data: " { id : '" + idForUpdate + "',lc: '" + locacode + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert(msg.d);
                        if (msg.d == 2) {
                            $.ajax({
                                type: "POST",
                                url: "Files_Data/Location.aspx/SaveLocation",
                                data: " { locationid : '" + idForUpdate + "', location : '" + loca + "', locationcode : '" + locacode + "', warehouseid : '" + $('#ddlWarehouse').val() + "', description:'" + desc + "', active:'" + document.getElementById("chkActiveLocation").checked + "'} ",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.d == 0) {
                                        $("#addLocationDialogBox").modal("hide");
                                        swal("Successful", "Location successfully saved!", "success");
                                    }
                                    else if (msg.d == 1) {
                                        load_dataLocation();
                                        $("#addLocationDialogBox").modal("hide");
                                        swal("Successful", "Location successfully modified!", "success");
                                    }
                                    else if (msg.d == 2) {
                                        swal("Error", "Location already exist!", "error");
                                    }
                                    else {
                                        swal("Error", "Location cannot be set to inactive. This is being actively used in property!", "error");
                                        $("#addLocationDialogBox").modal("hide");
                                    }
                                    locatioN();
                                    if (WMLocation == 'Location') {
                                        load_dataLocation();
                                    } else if (WMLocation == 'InventoryCount') {
                                        GetLocationIC();
                                    } else if (WMLocation == 'PTransfer') {
                                        GetLocationPT();
                                        GetLocationToPT();
                                    }
                                }
                            });
                        }
                        else {
                            swal("Error", "Location code already exist!", "error");
                        }
                    },
                    error: function (msg) {
                        alert(msg.d);
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

var WMSite = '';
var idForUpdateSite = '';
function addWarehouseDialogBox(WModule) {
    WMSite = WModule;
    idForUpdateSite = 0;
    $('#txtWarehouseName').val("");
    $('#txtCode').val("");
    document.getElementById("chkActiveSite").checked = true;
    $("#addWarehouseDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditSite");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveWarehouse',
                    class: 'btn btn-success btn_EditSite',
                    onclick: 'SaveWarehouse()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelSite').prepend($formBrand);
            }
        }
    });
}

function SaveWarehouse() {
    var alertMsg;

    var ware = ReplaceStr1($('#txtWarehouseName').val().trim().replace(/  +/g, ' '));
    var sitecode = ReplaceStr1($('#txtSiteCode').val().trim().replace(/  +/g, ' '));

    //alert(sitecode);
    if (ReplaceStr1($('#txtWarehouseName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtSiteCode').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = '';
        if (ReplaceStr1($('#txtWarehouseName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nSite ';
        if (ReplaceStr1($('#txtSiteCode').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nSite Code ';


        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this location?",
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
                    url: "Files_Data/Site.aspx/SaveWarehouse",
                    data: " {warehouseid : '" + idForUpdateSite + "', sitecode : '" + sitecode + "', warehouse : '" + ware + "', active:'" + document.getElementById("chkActiveSite").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addWarehouseDialogBox").modal("hide");
                            swal("Successful", "Site successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addWarehouseDialogBox").modal("hide");
                            swal("Successful", "Site successfully modified!", "success");
                            load_dataSite();
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Site already exist!", "error");
                        }
                        else if (msg.d == 3) {
                            swal("Error", "Duplicate Site Code!", "error");
                        }
                        else {
                            swal("Error", "Location cannot be set to inactive. This is being actively used!", "error");
                        }
                        if (WMSite == 'Site') {
                            load_dataSite();
                        } else if (WMSite == 'InventoryCount') {
                            GetsiteIC();
                        } else if (WMSite == 'GR') {
                            GetWarehouseByIDGR();
                        } else if (WMSite == 'PTransfer') {
                            GetsitePT();
                            GetsiteToPT();
                        }
                        site();
                        site1();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

var WMArea = '';
function addAreaDialogBox(wmodule) {
    idForUpdate = 0;
    WMArea = wmodule;
    $('#txtArea').val("");
    $('#txtDescriptionArea').val("");
    $('#ddlLocation').val("0").trigger('change');
    document.getElementById("chkActiveArea").checked = true;
    $("#addAreaDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditArea");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveArea',
                    class: 'btn btn-success btn_EditArea',
                    onclick: 'SaveArea()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelArea').prepend($formBrand);
            }
        }
    });
}

function SaveArea() {
    var cate = ReplaceStr1($('#txtArea').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionArea').val().trim().replace(/  +/g, ' '));
    var location = $('#ddlLocation').val();
    var alertMsg;
    if (ReplaceStr1($('#txtArea').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionArea').val().trim().replace(/  +/g, ' ')) == '' || $('#ddlLocation').val() == '0') {
        alertMsg = '';

        if ($('#ddlLocation').val() == '0')
            alertMsg += '\nLocation';
        if (ReplaceStr1($('#txtArea').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nArea';
        if (ReplaceStr1($('#txtDescriptionArea').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this area?",
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
                    url: "Files_Data/Area.aspx/SaveArea",
                    data: " { id : '" + idForUpdate + "' ,locationid: '" + $('#ddlLocation').val() + "', Area : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActiveArea").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addAreaDialogBox").modal("hide");
                            swal("Successful", "Area successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addAreaDialogBox").modal("hide");
                            load_dataArea();
                            swal("Successful", "Area successfully modified!", "success");
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Area already exist!", "error");
                        }
                        else {
                            swal("Error", "Area cannot be set to inactive. This is being actively used in property!", "error");
                            $("#addAreaDialogBox").modal("hide");
                        }
                        if (WMArea == 'Area') {
                            load_dataArea();
                        } else if (WMArea == 'InventoryCount') {
                            GetOfficeIC();
                        } else if (WMArea == 'PTransfer') {
                            GetOfficePT();
                            GetOfficeToPT();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

var mwMDept = "";
function addDepartmentDialogBox(mwDept) {
    idForUpdate = 0;
    mwMDept = mwDept;
    department();
    document.getElementById("chkActiveDept").checked = true;
    $('#txtDepartmentName').val("");
    $('#txtDescriptionDept').val("");
    $("#addDepartmentDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditDepartment");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveDepartment',
                    class: 'btn btn-success btn_EditDepartment',
                    onclick: 'SaveDepartment()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelDepartment').prepend($formBrand);
            }
        }
    });
}

function SaveDepartment() {
    var cate = ReplaceStr1($('#txtDepartmentName').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionDept').val().trim().replace(/  +/g, ' '));
    //alert(idForUpdate);
    var alertMsg;

    if (ReplaceStr1($('#txtDepartmentName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionDept').val().trim().replace(/  +/g, ' ')) == '') {
        alertMsg = '';

        if (ReplaceStr1($('#txtDepartmentName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDepartment';
        if (ReplaceStr1($('#txtDescriptionDept').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        //if ($('#txtLife').val() == '')
        //    alertMsg += '\nUseful Life';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this user department?",
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
                    url: "Files_Data/Department.aspx/SaveDepartment",
                    data: " { id : '" + idForUpdate + "' , department : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActiveDept").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addDepartmentDialogBox").modal("hide");
                            swal("Successful", "User Department successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addDepartmentDialogBox").modal("hide");
                            load_dataDept();
                            swal("Successful", "User Department successfully modified!", "success");
                        }
                        else if (msg.d == 4) {
                            $("#addDepartmentDialogBox").modal("hide");
                            swal("Error", "User Department cannot be set to inactive because it is actively used.", "error");
                        }
                        else if (msg.d == 5) {
                            $("#addDepartmentDialogBox").modal("hide");
                            swal("Error", "User Department cannot be set to inactive because it is actively used.", "error");
                        }
                        else {
                            swal("Error", "User Department already exist!", "error");
                        }
                        department();
                        department1();
                        if (mwMDept == 'PI') {
                            departmentPI();
                        }
                        if (mwMDept == 'Department') {
                            load_dataDept();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

var wmPos = '';
function addPositionDialogBox(mwPos) {
    idForUpdate = 0;
    wmPos = mwPos;
    department();
    document.getElementById("chkActiveJobTitle").checked = true;
    $('#ddlDepartment').val(0).trigger('change');
    $('#txtPositionName').val("");
    $('#txtDescriptionJobTitle').val("");
    $("#addPositionDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditPosition");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveBrand',
                    class: 'btn btn-success btn_EditPosition',
                    onclick: 'SavePosition()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelPosition').prepend($formBrand);
            }
        }
    });
}

function SavePosition() {
    var subc = ReplaceStr1($('#txtPositionName').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescriptionJobTitle').val().trim().replace(/  +/g, ' '));
    //alert(" { Positionid : " + idForUpdate + ",Position : '" + subc + "', catedgoryid:'" + $('#BodyHolder_ddlCategory').val() + "', description:'" + desc + "', active:'" + document.getElementById("chkActive").checked + "', image: '" + fu1 + "', pageNo : '" + currentPage + "'} ");

    var alertMsg;
    if (ReplaceStr1($('#txtPositionName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescriptionJobTitle').val().trim().replace(/  +/g, ' ')) == '' || $('#ddlDepartment').val() == '0') {// || $('#txtLife').val().trim() == '') {
        alertMsg = ' ';
        if (ReplaceStr1($('#txtPositionName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nJob Title ';
        if (ReplaceStr1($('#txtDescriptionJobTitle').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        if ($('#ddlDepartment').val().trim() == '0')
            alertMsg += '\nUser Department';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this job title?",
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
                    url: "Files_Data/JobTitle.aspx/SavePosition",
                    data: " { positionid : " + idForUpdate + ",position : '" + subc + "', departmentid:'" + $('#ddlDepartment').val() + "', description:'" + desc + "', active:'" + document.getElementById("chkActiveJobTitle").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addPositionDialogBox").modal("hide");
                            swal("Successful", "Job title successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addPositionDialogBox").modal("hide");
                            swal("Successful", "Job title successfully modified!", "success");
                            load_dataJobTitle();
                        }
                        else if (msg.d == 2) {
                            swal("Error", "Job title already exist!", "error");
                        }
                        else {
                            swal("Error", "Job title cannot be set to inactive. This is being actively used!", "error");
                            $("#addPositionDialogBox").modal("hide");
                        }
                        if (wmPos == 'Pos') {
                            load_dataJobTitle();
                        }
                        GetPosition();

                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}



function addSupplierDialogBox() {
    idForUpdate = 0;
    $('#txtSupplierName').val("");
    $('#txtContactNo').val("");
    $('#txtEmailAddress').val("");
    document.getElementById("chkActiveSupplier").checked = true;
    $("#addSupplierDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditSupplier");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveSupplier',
                    class: 'btn btn-success btn_EditSupplier',
                    onclick: 'SaveSupplier()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelSupplier').prepend($formBrand);
            }
        }
    });
}

function SaveSupplier() {
    var cate = ReplaceStr1($('#txtSupplierName').val().trim().replace(/  +/g, ' '));
    var contactno = ReplaceStr1($('#txtContactNo').val().trim().replace(/  +/g, ' '));
    var email = $('#txtEmailAddress').val().trim();
    var alertMsg;

    if (ReplaceStr1($('#txtSupplierName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtContactNo').val().trim().replace(/  +/g, ' ')) == '' || email.includes('@') == false || $('#txtEmailAddress').val().trim() == '') {
        alertMsg = '';

        if (ReplaceStr1($('#txtSupplierName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nSupplier Name';
        if (ReplaceStr1($('#txtContactNo').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nContact Number';
        if (($('#txtEmailAddress').val().trim()) == '')
            alertMsg += '\nEmail Address';
        if (email.includes('@') == false) {
            alertMsg += '\nEnter Valid Email Address';
        }
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this supplier?",
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
                    url: "Files_Data/Supplier.aspx/SaveSupplier",
                    data: " { id : '" + idForUpdate + "' , supplier : '" + cate + "', contactnumber: '" + contactno + "', active:'" + document.getElementById("chkActiveSupplier").checked + "', email:'" + email + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addSupplierDialogBox").modal("hide");
                            swal("Successful", "Supplier successfully saved!", "success");
                        }
                        else if (msg.d == 1) {
                            $("#addSupplierDialogBox").modal("hide");
                            swal("Successful", "Supplier successfully modified!", "success");
                        }
                        else if (msg.d == 3) {
                            $("#addSupplierDialogBox").modal("hide");
                            swal("Error", "Supplier cannot be set to inactive. This is being actively used", "error");
                        }
                        else {
                            $("#addSupplierDialogBox").modal("hide");
                            swal("Error", "Supplier already exist!", "error");
                        }
                        GetSupplier();
                        load_dataSupplier();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
}

var wModuleUser = '';
function addUserDialogBox1(WModule) {
    idForUpdate = 0;
    $('#txtEmail').prop('disabled', false);
    GetCustodianElements();
    wModuleUser = WModule;
    $('#ddlDepartment1').val(0).trigger('change');
    $('#ddlPosition').val(0).trigger('change');
    $('#ddlWarehouse1').val(0).trigger('change');
    document.getElementById("chkCustodian").checked = false;
    document.getElementById("chkAdministrative").checked = false;
    document.getElementById("chkApprover").checked = false;
    document.getElementById("chkInspector").checked = false;
    document.getElementById("chkCertifier").checked = false;

    $('#FileUploadUser').val("");

    $('#userImage1').html("");

    $('#txtFirstName').val("");
    $('#txtLastName').val("");
    $('#txtEmail').val("");
    $('#txtEmployeeNo').val("");
    $('#txtUsername').val("");
    if (IsEmailUsername == 'Email') {
        document.getElementById('DivUsername').style.display = 'none';
    }
    document.getElementById("chkActiveUser").checked = true;

    $('#txtUsername').prop('disabled', false);
    idForUpdate = 0;

    $("#addUserDialog").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditUser");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnsave',
                    class: 'btn btn-success btn_EditUser',
                    onclick: 'SaveUser(this)',
                    innerHTML: 'Save'
                });

                $('#SaveCancelUser').prepend($formBrand);
            }
        }
    });
}



function SelectItem(id) {
    if (document.getElementById(id).checked != true) {
        chkCustodianChecked -= 1;
        if (chkCustodianChecked < $('#indexCtr').val())
            document.getElementById(id).checked = false;
        var index = selectedCustodian.indexOf(id);
        selectedCustodian.splice(index, 1);
    }
    else {
        chkCustodianChecked += 1;
        if (chkCustodianChecked == $('#indexCtr').val())
            document.getElementById(id).checked = true;
        selectedCustodian.push(id);
    }
    //alert(selectedCustodian.join(';'));
}

$(document).on('change', 'input.cls_Custodian', function () {
    var $Item = $(this);
    var idForUpdate = $(this).attr('id');
    SelectItem(idForUpdate);
});

var selectedCustodian = [];
var chkCustodianChecked = 0;
var usernameedit = '';
function SaveUser() {
    $('#txtUsername').prop('disabled', false);

    var fu = $('#FileUploadUser').val();
    var fu1 = "";

    if (fu == "") {
        fu1 = "";
    }
    else {
        fu1 = fu.split('.')[1];
    }
    var site = $('#ddlWarehouse1').val();
    var firs = ReplaceStr1($('#txtFirstName').val().trim());
    var last = ReplaceStr1($('#txtLastName').val().trim());
    var emai = $('#txtEmail').val().trim();
    var user = ReplaceStr1($('#txtUsername').val().trim());
    if (IsEmailUsername == 'Email') {
        user = emai;
    }
    var alertMsg = "";
    alertMsg = CheckInputsUser();
    if (alertMsg != "") {
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this user?",
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
                    url: "User_Management/User.aspx/CheckEmpNo",
                    data: " { userid : '" + idForUpdate + "',empno: '" + $('#txtEmployeeNo').val() + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert(msg.d);
                        if (msg.d == 2) {
                            $(function () {
                                var fileUpload = $("#FileUploadUser").get(0);
                                var files = fileUpload.files;
                                var test = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    test.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    url: "User_Management/UserUpload.ashx?username=" + user + "." + fu.split('.')[1],
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: test,
                                    success: function (result) {
                                        //alert(result);

                                        if (result.includes("File Uploaded Successfully", 0)) {
                                            //alert(" { userid: '" + idForUpdate + "',firstname:'" + firs + "', lastname:'" + last + "', employeeno: '" + $('#txtEmployeeNo').val() + "', emailaddress:'" + emai + "', username:'" + user + "', active:'" + document.getElementById("chkActiveUser").checked + "', custodian:'" + document.getElementById("chkCustodian").checked + "', administrative:'" + document.getElementById("chkAdministrative").checked + "',approver:'" + document.getElementById("chkApprover").checked + "', inspector:'" + document.getElementById("chkInspector").checked + "',  certifier:'" + document.getElementById("chkCertifier").checked + "',extension : '" + fu1 + "', department : '" + $('#ddlDepartment1').val() + "',position : '" + $('#ddlPosition').val() + "',integrator : '" + selectedCustodian.join(';') + "', siteid: '" + site + "'} ");
                                            $.ajax({
                                                type: "POST",
                                                async: false,
                                                url: "User_Management/User.aspx/SaveUser",
                                                data: " { userid: '" + idForUpdate + "',firstname:'" + firs + "', lastname:'" + last + "', employeeno: '" + $('#txtEmployeeNo').val() + "', emailaddress:'" + emai + "', username:'" + user + "', active:'" + document.getElementById("chkActiveUser").checked + "', custodian:'" + document.getElementById("chkCustodian").checked + "', administrative:'" + document.getElementById("chkAdministrative").checked + "',approver:'" + document.getElementById("chkApprover").checked + "', inspector:'" + document.getElementById("chkInspector").checked + "',  certifier:'" + document.getElementById("chkCertifier").checked + "',extension : '" + fu1 + "', department : '" + $('#ddlDepartment1').val() + "',position : '" + $('#ddlPosition').val() + "',integrator : '" + selectedCustodian.join(';') + "', siteid: '" + site + "'} ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == "0") {
                                                        swal("Success", "User successfully saved!", "success");
                                                        $("#addUserDialog").modal("hide");
                                                        load_dataUser();

                                                    }
                                                    else if (msg.d == "1") {
                                                        swal("Success", "User successfully modified!", "success");
                                                        $("#addUserDialog").modal("hide");
                                                        load_dataUser();

                                                    }
                                                    else if (msg.d == "2") {
                                                        swal("Error", "User already exists!", "error");
                                                        $("#addUserDialog").modal("hide");
                                                        load_dataUser();

                                                    }
                                                    else if (msg.d == "5") {
                                                        swal("Error", "You already consume the maximum number of Custodian Type User for your License.", "error");
                                                        load_dataUser();

                                                        //$("#addUserDialog").modal("hide");
                                                    }
                                                    else if (msg.d == "6") {
                                                        swal("Error", "You already consume the maximum number of Administrator User for your License.", "error");
                                                        load_dataUser();

                                                        //$("#addUserDialog").modal("hide");
                                                    }
                                                    else {
                                                        swal("Warning", "User cannot be modified!", "warning");
                                                        $("#addUserDialog").modal("hide");
                                                        load_dataUser();

                                                    }
                                                    if (wModuleUser == 'GR') {
                                                        GetUsers();
                                                        GetUserByCustodian();
                                                        GetApproverByCustodian();
                                                    }
                                                    else if (wModuleUser == 'GRCustodian') {
                                                        GetUsers();
                                                        GetUserByCustodian();
                                                        GetApproverByCustodian();
                                                    }
                                                    else if (wModuleUser == 'GRApprover') {
                                                        GetUsers();
                                                        GetApproverByCustodian();
                                                        GetUserByCustodian();
                                                    }
                                                    else if (wModuleUser == 'Users') {
                                                        load_dataUser();
                                                    }
                                                    else if (wModuleUser == 'PropertyIssuance') {
                                                        GetEmployeePI();
                                                    }
                                                    else if (wModuleUser == 'StockIssuance') {
                                                        GetEmployeeSI();
                                                    }
                                                    else if (wModuleUser == 'PropertyReturn') {
                                                        GetEmployeePR();
                                                    }
                                                    else if (wModuleUser == 'StockReturn') {
                                                        GetEmployeeSR();
                                                    }
                                                    else if (wModuleUser == 'DayPass') {
                                                        GetApproversDP();
                                                    }
                                                    else if (wModuleUser == 'PTransfer') {
                                                        GetEmployeePT();
                                                    }
                                                    else {
                                                        GetUsers();
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

                        }
                        else {
                            swal("Error", "Employee number already exist!", "error");
                        }
                    },
                    error: function (msg) {
                        alert(msg.d);
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }
    load_dataUser();
}

function getImageUser(event) {
    console.log(event.target.files[0]);
    //$(".divIDClass").hide();
    $('#userImage1').html("");
    var image = URL.createObjectURL(event.target.files[0]);
    var imagediv = document.getElementById('userImage1');
    var newimg = document.createElement('img');
    newimg.src = image;
    newimg.width = "300";
    imagediv.appendChild(newimg);
}


function CheckInputsUser() {
    var site = $('#ddlWarehouse').val();
    var firs = ReplaceStr1($('#txtFirstName').val().trim());
    var last = ReplaceStr1($('#txtLastName').val().trim());
    if (IsEmailUsername != 'Email') {
        var emai = $('#txtEmail').val().trim();
    }
    var user = ReplaceStr1($('#txtUsername').val().trim());

    var alertMsg = "";
    if ($('#txtEmployeeNo').val().trim() == "" || firs.trim() == "" || last.trim() == "" || $('#ddlDepartment1').val() == 0 || $('#ddlPosition').val() == 0 || $('#ddlWarehouse1').val() == "0" || $('#txtEmail').val() == "" || !$('#txtEmail').val().includes('@')) {// || $('#ddlDepartment1').val() == "--- Select Department ---" || $('#ddlPosition').val() == "") {
        //alertMsg = 'Fill-out all the required field(s): ';
        if (firs == "")
            alertMsg += '\nFirst Name';
        if (last == "")
            alertMsg += '\nLast Name';
        if ($('#txtEmployeeNo').val() == "")
            alertMsg += '\nEmployeeNo';
        if (IsEmailUsername != 'Email') {
            if ($('#txtUsername').val() == '')
                alertMsg += '\nUsername';
        }
        if ($('#ddlDepartment1').val() == 0)
            alertMsg += '\nUser Department';
        if ($('#ddlPosition').val() == 0)
            alertMsg += '\nJob Title / Position';
        if ($('#ddlWarehouse1').val() == "0")
            alertMsg += '\nSite';
        if ($('#txtEmail').val() == "")
            alertMsg += '\nEmail';
        if (!$('#txtEmail').val().includes('@'))
            alertMsg += '\nInvalid Email';
        //alert(alertMsg);
        //swal("Fill-out all the required field(s): ", alertMsg, "warning");
        //ex.preventDefault();
    }
    if (document.getElementById('chkCustodian').checked == true) {
        //var selectedCustodian = [];
        //$.each($(".cls_Custodian:checked"), function () {
        //    selectedCustodian.push($(this).val());
        //});
        //if (selectedCustodian.length == 0) {
        //    alertMsg += '\nCustodian';
        //}
        var count = 0;
        var checkboxes = document.getElementsByClassName('cls_Custodian');
        for (var i in checkboxes) {
            if (checkboxes[i].checked)
                count++;
        }
        if (count == 0)
            alertMsg += '\nCustodian Type';
    }
    return alertMsg;
}

var CustodianCheckBoxCtr = 0;
function GetCustodianElements() {
    //document.getElementById('Custodians').innerHTML = "";
    var div = document.getElementById('Custodians');
    while (div.firstChild) {
        div.removeChild(div.firstChild);
    }
    $.ajax({
        type: 'POST',
        url: 'User_Management/User.aspx/GetIntegratorList',
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                custodianArrayElements = [];
                custodianArrayElements = JSON.parse(xml.d);
                custodianArrayElements.forEach((custodian, index) => {
                    //alert(custodian.CustodianID);
                    var $formInputGroup = makeElement('div', {
                        class: 'input-group',
                        id: 'ig' + custodian.CustodianID
                    });
                    $('#Custodians').prepend($formInputGroup);

                    var $formCustodiansLabel = makeElement('label', {
                        class: 'pull-left',
                        innerHTML: '&nbsp;&nbsp;' + custodian.Custodian
                    });
                    $('#ig' + custodian.CustodianID).prepend($formCustodiansLabel);

                    var $formCustodiansCheckBox = makeElement('input', {
                        type: 'checkbox',
                        id: custodian.CustodianID,
                        class: 'switch_1 pull-left cls_Custodian',
                        disabled: 'true'
                    });
                    $('#ig' + custodian.CustodianID).prepend($formCustodiansCheckBox);
                    CustodianCheckBoxCtr += 1;
                });

                document.getElementById('indexCtr').value = CustodianCheckBoxCtr;
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
//function GetCustodianElements() {
//    $.ajax({
//        type: 'POST',
//        url: 'User_Management/User.aspx/GetIntegratorList',
//        data: "{  }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (xml) {
//            try {
//                custodianArrayElements = [];
//                custodianArrayElements = JSON.parse(xml.d);
//                custodianArrayElements.forEach((custodian, index) => {
//                    //alert(custodian.CustodianID);
//                    var $formInputGroup = makeElement('div', {
//                        class: 'input-group',
//                        id: 'ig' + custodian.CustodianID
//                    });
//                    $('#Custodians').prepend($formInputGroup);
//                    var $formCustodiansLabel = makeElement('label', {
//                        class: 'pull-left',
//                        innerHTML: '&nbsp;&nbsp;' + custodian.Custodian
//                    });
//                    $('#ig' + custodian.CustodianID).prepend($formCustodiansLabel);
//                    var $formCustodiansCheckBox = makeElement('input', {
//                        type: 'checkbox',
//                        id: custodian.CustodianID,
//                        class: 'switch_1 pull-left cls_Custodian',
//                        disabled: 'true'
//                    });
//                    $('#ig' + custodian.CustodianID).prepend($formCustodiansCheckBox);
//                    CustodianCheckBoxCtr += 1;
//                });
//                document.getElementById('indexCtr').value = CustodianCheckBoxCtr;
//            } catch (err) {
//                console.log(err.message);
//            }
//        },
//        error: function (xhr, status, error) {
//            alert(error);
//            console.log(xhr);
//        }
//    });
//}

var wModelDevice = '';
function addDeviceDialogBox(WModelD) {
    idForUpdate = 0;
    wModelDevice = WModelD;
    $('#txtDeviceName').val("");
    $('#txtDescriptionDeviceMngt').val("");
    $('#txtIPaddress').val("");
    $('#txtPort').val("");
    document.getElementById("chkActiveDeviceMngt").checked = true;
    $("#addDeviceDialogBox").modal("show");

    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg1) {
            var btnAccess = [];
            btnAccess = msg1.d.split('|');
            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
                let elementsAddBrand = document.getElementsByClassName("btn_EditDeviceMngt");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveDevice',
                    class: 'btn btn-success btn_EditDeviceMngt',
                    onclick: 'SaveDevice()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelDeviceMngt').prepend($formBrand);
            }
        }
    });
}

function SaveDevice() {
    var cate = ReplaceStr1($('#txtDeviceName').val().trim());
    var desc = ReplaceStr1($('#txtDescriptionDeviceMngt').val());
    var ipaddress = $('#txtIPaddress').val().replace(/[`~!@#$%^&*()_|+\-=?;:'",<>\{\}\[\]\\\/]/gi, '');
    var port = $('#txtPort').val();
    var deviceusagetype = $('#ddlDeviceUsageType').val();
    var alertMsg;

    if ($('#txtDeviceName').val().trim() == '' || $('#txtDescriptionDeviceMngt').val().trim() == '' || $('#txtIPaddress').val().trim() == '' || $('#txtPort').val().trim() == '') {
        alertMsg = '';

        if ($('#txtDeviceName').val() == '')
            alertMsg += '\nDevice Name';
        if ($('#txtIPaddress').val() == '')
            alertMsg += '\nIP Address';
        if ($('#txtPort').val() == '')
            alertMsg += '\nPort';
        if ($('#txtDescriptionDeviceMngt').val() == '')
            alertMsg += '\nDescription';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this device?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                //   alert(" { id : '" + idForUpdate + "' , device : '" + cate + "', ipaddress: '" + ipaddress + "', port: '" + port + "',description :'" + desc + "', devicetypeid: '" + deviceusagetype + "', active:'" + document.getElementById("chkActiveDeviceMngt").checked + "'} ");
                if (ValidateIPaddress(ipaddress) == true)
                    $.ajax({
                        type: "POST",
                        url: "System/DeviceManagement.aspx/SaveDevice",
                        data: " { id : '" + idForUpdate + "' , device : '" + cate + "', ipaddress: '" + ipaddress + "', port: '" + port + "',description :'" + desc + "', devicetypeid: '" + deviceusagetype + "', active:'" + document.getElementById("chkActiveDeviceMngt").checked + "'} ",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 0) {
                                $("#addDeviceDialogBox").modal("hide");
                                //alert('Device Successfully Saved!');
                                swal("Success", "Device Successfully Saved!", "success");
                                //ClearDeviceMngt();
                            }
                            else if (msg.d == 1) {
                                $("#addDeviceDialogBox").modal("hide");
                                //alert('Device Successfully Modified!');
                                swal("Success", "Device Successfully Modified!", "success");
                                // ClearDeviceMngt();
                            }
                            else {
                                $("#addDeviceDialogBox").modal("hide");
                                //alert('Device Already Exist!');
                                swal("Error", "Device Already Exist!", "error");
                                // ClearDeviceMngt();
                            }
                            if (wModelDevice == 'DMngt') {
                                load_dataDeviceMngt();
                                ClearDeviceMngt();
                            } else if (wModelDevice == 'PTagging') {
                                DeviceConnect();
                            }
                        }
                    });
                else {
                    swal("Warning", "You have entered an invalid IP Address", "warning");
                }
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function ValidateIPaddress(ipadd) {
    var ipformat = /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
    if (ipadd.match(ipformat)) {
        return true;
    }
    else {
        return false;
    }
}


