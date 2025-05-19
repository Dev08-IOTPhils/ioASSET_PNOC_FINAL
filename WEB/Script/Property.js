var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblProperty").DataTable({
        "scrollX": true, fixedHeader: {
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
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
    load_data();
    //$('#addPropertyDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function UploadFile(fileUpload) {
    if (fileUpload.value != '') {
        //document.getElementById('btnUpload').click();

        //preventDefault();
    }
}

function RetrieveThreshold() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Property.aspx/RetrieveThreshold",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtMaintenanceThreshold').val(msg.d.split('|')[3]);
            //alert(msg.d.split('|')[3]);
        }
    });
}

function RetrieveUsefulLife() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Property.aspx/RetrieveUsefulLife",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtUsefulLife').val(msg.d.split('|')[1]);
            //alert(msg.d.split('|')[3]);
        }
    });
}


function load_data() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Property.aspx/GetProperty",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#property').hide();
            $('#property').innerHTML = "";
            $('#property').fadeIn(200);
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
        $('#tblProperty').DataTable({
            "destroy": true,
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true, fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "data": JSON.parse(data.d),
            "columns": [
                //{ "data": "Picture" },
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                { "data": "TagType" },
                { "data": "IsActive" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property List'
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
        $('#tblProperty').off('click');
    } catch (e) {
        var table = $('#tblProperty').DataTable();
        table
            .clear()
            .draw();
    }
}

function addPropertyDialogBox() {
    Clear();

    document.getElementById("chkActive").checked = true;
    $("#addPropertyDialogBox").modal("show");



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
                let elementsAddBrand = document.getElementsByClassName("btn_EditProperty");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSetCompanyInformation',
                    class: 'btn btn-success btn_EditProperty',
                    onclick: 'SaveProperty(this)',
                    innerHTML: 'Save'
                });

                $('#SaveCancelProperty').prepend($formBrand);
            }
        }
    });
}


function SaveProperty() {
    var depreciationmethod = $('#BodyHolder_ddlDepreciationMethod').val();
    //alert(depreciationmethod);
    var itemcode = ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' '));
    var description = ReplaceStr1($('#txtPropertyDescription').val().trim().replace(/  +/g, ' '));
    //var assetinventoryaccount = $('#ddlAssetInventoryAccount').val();
    //if ($('#ddlAssetInventoryAccount').val() == '--- Select Inventory Account ---')
    //    assetinventoryaccount = 0;
    var incomeaccount = $('#BodyHolder_ddlIncomeAccount').val();
    if ($('#BodyHolder_ddlIncomeAccount').val() == '--- Select Income Account ---')
        incomeaccount = 0;
    var expenseaccount = $('#BodyHolder_ddlExpenseAccount').val();
    if ($('#BodyHolder_ddlExpenseAccount').val() == '--- Select Expense Account ---')
        expenseaccount = 0;

    var tag = $('#ddlTag').val();

    var propertylogo = $('#BodyHolder_FileUploadPropertyImage').val();
    var propertylogocontainer = "";

    if (propertylogo == "") {
        propertylogocontainer = "";
    }
    else {
        propertylogocontainer = itemcode + '.' + propertylogo.split('.')[1];
    }
    var alertMsg;
    if (ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' ')) == '' || $('#ddlClassification').val() == '0' || $('#ddlCategory').val() == '0' ||
        $('#ddlBrand').val() == '0' || $('#ddlModel').val() == '0' || $('#ddlTag').val() == '0' || $('#txtUsefulLife').val().trim() == '' || $('#txtUsefulLife').val().trim() == '0' ||
        $('#ddlUnitOfMeasurement').val() == '0' ||
        //$('#ddlAssetInventoryAccount').val() == '--- Select Inventory Account ---' ||
        ReplaceStr1($('#txtPropertyDescription').val().trim().replace(/  +/g, ' ')) == '' || $('#BodyHolder_ddlDepreciationMethod').val() == '0' || $('#txtMaintenanceThreshold').val().trim() == '' || $('#txtMaintenanceThreshold').val().trim() == '0') {
        alertMsg = '';

        if (ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nItem Code';
        if ($('#ddlClassification').val() == '0')
            alertMsg += '\nClassification';
        if ($('#ddlCategory').val() == '0')
            alertMsg += '\nCategory';
        if ($('#ddlBrand').val() == '0')
            alertMsg += '\nBrand';
        if ($('#ddlModel').val() == '0')
            alertMsg += '\nModel';
        if ($('#BodyHolder_ddlDepreciationMethod').val() == '--- Select Depreciation Method ---')
            alertMsg += '\nDepreciation method';
        if ($('#ddlUnitOfMeasurement').val() == '0')
            alertMsg += '\nUnit of measurement';
        if ($('#ddlTag').val() == '0')
            alertMsg += '\nTag';
        if (ReplaceStr1($('#txtPropertyDescription').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        if ($('#txtMaintenanceThreshold').val() == '' || $('#txtMaintenanceThreshold').val() == '0')
            alertMsg += '\nMaintenance threshold';
        if ($('#txtUsefulLife').val().trim() == '' || $('#txtUsefulLife').val().trim() == '0')
            alertMsg += '\nUseful life';
        //if ($('#ddlAssetInventoryAccount').val() == '--- Select Inventory Account ---')
        //    alertMsg += '\nAsset Inventory Account';
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
        ex.preventDefault();
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this property?",
            icon: "warning",
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            //alert(" { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', incomeaccount: '" + incomeaccount + "', expenseaccount: '" + expenseaccount + "', propertyimage: '" + propertylogocontainer + "', maintenancethreshold: '" + $('#txtMaintenanceThreshold').val() + "', active:'" + document.getElementById("chkActive").checked + "', tagid: '" + tag + "', usefullife: '" + $('#txtUsefulLife').val() + "', depreciationmethod : '" + $('#BodyHolder_ddlDepreciationMethod').val() + "'} ");
            if (isConfirm) {
                var fileUpload = $("#BodyHolder_FileUploadPropertyImage").get(0);
                var files = fileUpload.files;
                var test = new FormData();
                for (var i = 0; i < files.length; i++) {
                    test.append(files[i].name, files[i]);
                }
                $.ajax({
                    url: "Files_Data/FilesData.ashx?transactionnumber=" + itemcode + "|Property|" + propertylogocontainer,
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: test,
                    success: function (result) {
                        if (result.includes("File Uploaded Successfully", 0)) {
                            $.ajax({
                                type: "POST",
                                url: "Files_Data/Property.aspx/SaveProperty",
                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', assetinventoryaccount: '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount: '" + expenseaccount + "', propertyimage: '" + propertylogocontainer + "', maintenancethreshold: '" + $('#txtMaintenanceThreshold').val() + "', active:'" + document.getElementById("chkActive").checked + "', tagid: '" + tag + "', usefullife: '" + $('#txtUsefulLife').val() + "', depreciationmethod : '" + depreciationmethod + "'} ",
                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', incomeaccount: '" + incomeaccount + "', expenseaccount: '" + expenseaccount + "', propertyimage: '" + propertylogocontainer + "', maintenancethreshold: '" + $('#txtMaintenanceThreshold').val() + "', active:'" + document.getElementById("chkActive").checked + "', tagid: '" + tag + "', usefullife: '" + $('#txtUsefulLife').val() + "', depreciationmethod : '" + $('#BodyHolder_ddlDepreciationMethod').val() + "'} ",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.d == 0) {
                                        load_data();
                                        $("#addPropertyDialogBox").modal("hide");
                                        swal("Successful", "Property successfully saved!", "success");
                                        Clear();
                                    }
                                    else if (msg.d == 1) {
                                        load_data();
                                        $("#addPropertyDialogBox").modal("hide");
                                        swal("Successful", "Property successfully modified!", "success");
                                        Clear();
                                    }
                                    else {
                                        load_data();
                                        $("#addPropertyDialogBox").modal("hide");
                                        swal("Error", "Property already exist!", "error");
                                        Clear();
                                    }
                                }
                            });
                            $('#BodyHolder_btnUpload').click();
                        } else {
                            swal("Warning", result, "warning");
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user.", "warning");
            }
        });
    }

}

//function SaveProperty(ex) {
//    var depreciationmethod = $('#BodyHolder_ddlDepreciationMethod').val();
//    var itemcode = ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' '));
//    var description = ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' '));
//    var assetinventoryaccount = $('#BodyHolder_ddlAssetInventoryAccount').val();
//    if ($('#BodyHolder_ddlAssetInventoryAccount').val() == '--- Select Inventory Account ---')
//        assetinventoryaccount = 0;
//    var incomeaccount = $('#BodyHolder_ddlIncomeAccount').val();
//    if ($('#BodyHolder_ddlIncomeAccount').val() == '--- Select Income Account ---')
//        incomeaccount = 0;
//    var expenseaccount = $('#BodyHolder_ddlExpenseAccount').val();
//    if ($('#BodyHolder_ddlExpenseAccount').val() == '--- Select Expense Account ---')
//        expenseaccount = 0;
//    var tag = $('#BodyHolder_ddlTag').val();
//    var propertylogo = $('#BodyHolder_FileUploadPropertyImage').val();
//    var propertylogocontainer = "";
//    if (propertylogo == "") {
//        propertylogocontainer = "";
//    }
//    else {
//        propertylogocontainer = itemcode + '.' + propertylogo.split('.')[1];
//    }
//    var alertMsg;
//    if (ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' ')) == '' || $('#BodyHolder_ddlClassification').val() == '--- Select Classification ---' || $('#ddlCategory').val() == '--- Select Category ---' ||
//        $('#BodyHolder_ddlBrand').val() == '--- Select Brand ---' || $('#BodyHolder_ddlModel').val() == '--- Select Model ---' || $('#BodyHolder_ddlTag').val() == '--- Select Tag ---' || $('#txtUsefulLife').val().trim() == '' || $('#txtUsefulLife').val().trim() == '0' ||
//        $('#BodyHolder_ddlUnitOfMeasurement').val() == '--- Select Unit Of Measurement ---' || ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '' || $('#BodyHolder_ddlDepreciationMethod').val() == '--- Select Depreciation Method ---' || $('#txtMaintenanceThreshold').val().trim() == '' || $('#txtMaintenanceThreshold').val().trim() == '0') {
//        alertMsg = '';
//        if (ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' ')) == '')
//            alertMsg += '\nItem Code';
//        if ($('#BodyHolder_ddlClassification').val() == '--- Select Classification ---')
//            alertMsg += '\nClassification';
//        if ($('#ddlCategory').val() == '--- Select Category ---')
//            alertMsg += '\nCategory';
//        if ($('#BodyHolder_ddlBrand').val() == '--- Select Brand ---')
//            alertMsg += '\nBrand';
//        if ($('#BodyHolder_ddlModel').val() == '--- Select Model ---')
//            alertMsg += '\nModel';
//        if ($('#BodyHolder_ddlDepreciationMethod').val() == '--- Select Depreciation Method ---')
//            alertMsg += '\nDepreciation Method';
//        if ($('#BodyHolder_ddlUnitOfMeasurement').val() == '--- Select Unit Of Measurement ---')
//            alertMsg += '\nUnit Of Measurement';
//        if ($('#BodyHolder_ddlTag').val() == '--- Select Tag ---')
//            alertMsg += '\nTag';
//        if (ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '')
//            alertMsg += '\nDescription';
//        if ($('#txtMaintenanceThreshold').val() == '' || $('#txtMaintenanceThreshold').val() == '0')
//            alertMsg += '\nMaintenance Threshold';
//        if ($('#txtUsefulLife').val().trim() == '' || $('#txtUsefulLife').val().trim() == '0')
//            alertMsg += '\nUseful Life';
//        swal("Fill-out all the required field(s): ", alertMsg, "warning");
//        ex.preventDefault();
//    }
//    else {
//        swal({
//            title: "Are you sure?",
//            text: "Are you sure you want to save this property?",
//            icon: "warning",
//            buttons: [
//              'No',
//              'Yes'
//            ],
//            dangerMode: true,
//        }).then(function (isConfirm) {
//            if (isConfirm) {
//                $.ajax({
//                    type: "POST",
//                    url: "Files_Data/Property.aspx/SaveProperty",
//                    data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#BodyHolder_ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#BodyHolder_ddlBrand').val() + "', modelid :'" + $('#BodyHolder_ddlModel').val() + "', uomid: '" + $('#BodyHolder_ddlUnitOfMeasurement').val() + "', description: '" + description + "', assetinventoryaccount: '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount: '" + expenseaccount + "', propertyimage: '" + propertylogocontainer + "', maintenancethreshold: '" + $('#txtMaintenanceThreshold').val() + "', active:'" + document.getElementById("chkActive").checked + "', tagid: '" + tag + "', usefullife: '" + $('#txtUsefulLife').val() + "', depreciationmethod : '" + depreciationmethod + "'} ",
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (msg) {
//                        if (msg.d == 0) {
//                            load_data();
//                            $("#addPropertyDialogBox").modal("hide");
//                            swal("Successful", "Property Successfully Saved!", "success");
//                            Clear();
//                        }
//                        else if (msg.d == 1) {
//                            load_data();
//                            $("#addPropertyDialogBox").modal("hide");
//                            swal("Successful", "Property Successfully Modified!", "success");
//                            ex.preventDefault();
//                            Clear();
//                        }
//                        else {
//                            load_data();
//                            $("#addPropertyDialogBox").modal("hide");
//                            swal("Error", "Property Already Exist!", "error");
//                            ex.preventDefault();
//                            Clear();
//                        }
//                    }
//                });
//                $('#BodyHolder_btnUpload').click();
//            } else {
//                swal("Cancelled", "Cancelled by user", "warning");
//            }
//        });
//        ex.preventDefault();
//    }
//}

function Clear() {
    $('#BodyHolder_FileUploadPropertyImage').val("");
    $('#BodyHolder_ddlDepreciationMethod').val("--- Select Depreciation Method ---").trigger('change');

    $('#ddlTag').val("0").trigger('change');
    $('#ddlClassification').val("0").trigger('change');
    $('#ddlCategory').val("0").trigger('change');
    $('#ddlBrand').val("0").trigger('change');
    $('#ddlModel').val("0").trigger('change');
    $('#ddlUnitOfMeasurement').val("0").trigger('change');
    $('#txtPropertyDescription').val(""); $('#BodyHolder_txtItemCode').val("");
    RetrieveUsefulLife();
    RetrieveThreshold();
    $('#BodyHolder_PropertyImage').html("");
    $('#BodyHolder_ddlAssetInventoryAccount').val("--- Select Inventory Account ---").trigger('change');
    $('#BodyHolder_ddlIncomeAccount').val("--- Select Income Account ---").trigger('change');
    $('#BodyHolder_ddlExpenseAccount').val("--- Select Expense Account ---").trigger('change');
    idForUpdate = 0;
    CountChar($('#txtPropertyDescription').val(), 500, 'cnt', 'txtPropertyDescription');
}

function GetPropertyID(propertyid) {
    idForUpdate = propertyid;


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
            if (btnAccess[2] == "0") {
                let elementsAddCat = document.getElementsByClassName("btn_EditProperty");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });





    $.ajax({
        type: "POST",
        url: "Files_Data/Property.aspx/PropertyByID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_txtItemCode').val(msg.d.split('|')[1]);
            $('#txtPropertyDescription').val(msg.d.split('|')[2]);
            $('#ddlClassification').val(msg.d.split('|')[3]).trigger('change');
            $('#ddlCategory').val(msg.d.split('|')[4]).trigger('change');
            $('#ddlBrand').val(msg.d.split('|')[5]).trigger('change');
            $('#ddlModel').val(msg.d.split('|')[6]).trigger('change');
            $('#ddlUnitOfMeasurement').val(msg.d.split('|')[7]).trigger('change');


            //if (msg.d.split('|')[8] == "0" || msg.d.split('|')[8] == "") {
            //    $('#BodyHolder_ddlAssetInventoryAccount').val("--- Select Inventory Account ---").trigger('change');
            //}
            //else {
            //    $('#BodyHolder_ddlAssetInventoryAccount').val(msg.d.split('|')[8]).trigger('change');
            //}

            if (msg.d.split('|')[9] == "0" || msg.d.split('|')[9] == "") {
                $('#BodyHolder_ddlIncomeAccount').val("--- Select Income Account ---").trigger('change');
            }
            else {
                $('#BodyHolder_ddlIncomeAccount').val(msg.d.split('|')[9]).trigger('change');
            }
            if (msg.d.split('|')[10] == "0" || msg.d.split('|')[10] == "") {
                $('#BodyHolder_ddlExpenseAccount').val("--- Select Expense Account ---").trigger('change');
            }
            else {
                $('#BodyHolder_ddlExpenseAccount').val(msg.d.split('|')[10]).trigger('change');
            }

            var propertyimage = "";
            if (msg.d.split('|')[11] == "") {
                propertyimage = "<img src='../Pictures/noimage.png' style='max-width: 100%; height:auto;'>";
            }
            else {
                propertyimage = "<img src='../Image/PropertyImage/" + msg.d.split('|')[1] + "/" + msg.d.split('|')[11] + "' style='max-width: 100%; height:auto;'>";
            }
            $('#BodyHolder_PropertyImage').html(propertyimage);

            if (msg.d.split('|')[12] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
            $('#txtMaintenanceThreshold').val(msg.d.split('|')[13]);

            $('#ddlTag').val(msg.d.split('|')[14]).trigger('change');
            $('#txtUsefulLife').val(msg.d.split('|')[15]);
            $('#BodyHolder_ddlDepreciationMethod').val(msg.d.split('|')[16]).trigger('change');
        }
    });
    $("#addPropertyDialogBox").modal("show");
}

function getImage(event) {
    console.log(event.target.files[0]);
    $('#BodyHolder_PropertyImage').html("");
    var image = URL.createObjectURL(event.target.files[0]);
    var imagediv = document.getElementById('BodyHolder_PropertyImage');
    var newimg = document.createElement('img');
    newimg.src = image;
    newimg.width = "300";
    imagediv.appendChild(newimg);
}


function deleteProperty(id) {
    idForUpdate = id;
    //$("#deleteProperty").modal("show");
    swal({
        title: "Delete Property",
        text: "Are you sure you want to delete this property?",
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
                url: "Files_Data/Property.aspx/DeleteProperty",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Property Successfully Deleted!", "success");
                        Clear();
                        load_data();
                    }
                    else {
                        swal("Error", "Property cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteProperty() {
    $("#deleteProperty").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Property.aspx/DeleteProperty",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Property Successfully Deleted!');
                Clear();
                load_data();
            }
            else {
                alert('Property cannot be Deleted, it is being actively used!');
            }
        }
    });
}
