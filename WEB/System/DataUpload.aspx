<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataUpload.aspx.cs" Inherits="System_DataUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var rfid = '';
        var barcode = '';
        var fileName = '';
        $(document).ready(function () {
            GetRFIDandBarcodeGeneration();
        });
        function GetRFIDandBarcodeGeneration() {
            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/GetRFIDandBarcodeGeneration",
                data: " { } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    rfid = msg.d.split('|')[0];
                    barcode = msg.d.split('|')[1];
                    //alert(rfid + "-" + barcode);
                }
            });
        }

        //////////Template download for property and stock
        function DownloadTemplateProperty() {
            if (rfid == "Manual Input" && barcode == "Manual Input") {
                fileName = 'Property_Upload_Template.csv';
            }
            else if (rfid == "Auto generated" && barcode == "Manual Input") {
                fileName = 'Property_Upload_Template_ManualBarcode.csv';
            }
            else if (rfid == "Manual Input" && barcode == "Auto generated") {
                fileName = 'Property_Upload_Template_ManualRFID.csv';
            }
            else {
                fileName = 'Property_Upload_Template_Generate.csv';
            }
            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/DownloadTemplate",
                data: " {fileName : '" + fileName + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(msg.d);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        function DownloadTemplateStock() {
            var fileName = 'Supply_Upload_Template.csv';
            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/DownloadTemplate",
                data: " {fileName : '" + fileName + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(msg.d);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        function DownloadTemplateUser() {
            var fileName = 'User_Upload_Template.csv';
            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/DownloadTemplate",
                data: " {fileName : '" + fileName + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(msg.d);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        function Base64ToBytes(base64) {
            var s = window.atob(base64);
            var bytes = new Uint8Array(s.length);
            for (var i = 0; i < s.length; i++) {
                bytes[i] = s.charCodeAt(i);
            }
            return bytes;
        }

        function DownloadTemplateFilesData() {
            var fileName;
            var FilesData = $('#ddlFilesData').val();
            if (FilesData == "Select")
                swal("Error", "Select Files Data to Download a template.", "error");
            if (FilesData == "Category")
                fileName = "Category_Template.csv";
            if (FilesData == "Brand")
                fileName = "Brand_Template.csv";
            if (FilesData == "Model")
                fileName = "Model_Template.csv";
            if (FilesData == "Classification")
                fileName = "Classification_Template.csv";
            if (FilesData == "Unit of Measurement")
                fileName = "UnitOfMeasurement_Template.csv";
            if (FilesData == "Supplier")
                fileName = "Supplier_Template.csv";
            if (FilesData == "Region")
                fileName = "Region_Template.csv";
            if (FilesData == "Branch")
                fileName = "Branch_Template.csv";
            if (FilesData == "Office")
                fileName = "Office_Template.csv";
            if (FilesData == "Department")
                fileName = "Department_Template.csv";
            if (FilesData == "Job Title")
                fileName = "JobTitle_Template.csv";
            if (FilesData == "Custodian")
                fileName = "Custodian_Template.csv";
            if (FilesData == "Chart of Accounts")
                fileName = "ChartofAccounts_Template.csv";
            if (FilesData == "Chart of Accounts")
                fileName = "ChartofAccounts_Template.csv";
            if (FilesData == "Task")
                fileName = "Task_Template.csv";
            if (FilesData == "Tags")
                fileName = "Tags_Template.csv";
            if (FilesData == "Property")
                fileName = "Property_Template.csv";
            if (FilesData == "Stock")
                fileName = "Stock_Template.csv";

            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/DownloadFormatProperty",
                data: " {fileName : '" + fileName + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(msg.d);
                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });
                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        /////////Upload for property
        function UploadProperty() {
            var directory = $("#BodyHolder_txtDirectoryPath").val().replace(/[\\]/gi, ',');
            var filess = $('#BodyHolder_File_UploadProperty').val();
            if (filess == "") {
                swal("", "Select .csv file to upload.", "warning");
            }
            else {
                swal({
                    title: "Attention",
                    text: "Uploading of property will be subject for Approval if System Setting is set to ON, otherwise this will proceed to database entry.",
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
                            var fileUpload = $("#BodyHolder_File_UploadProperty").get(0);
                            var files = fileUpload.files;
                            var test = new FormData();
                            for (var i = 0; i < files.length; i++) {
                                test.append(files[i].name, files[i]);
                            }
                            $.ajax({
                                url: "System/Propertyupload.ashx?",
                                type: "POST",
                                contentType: false,
                                processData: false,
                                data: test,
                                success: function (result) {
                                    if (result.includes("File Uploaded Successfully", 0)) {
                                        var filename = result.split(':')[1];
                                        $.ajax({
                                            type: "POST",
                                            url: "System/DataUpload.aspx/ProcessDataUpload",
                                            data: "{ directory : '" + directory + "', filename : '" + filename + "', type: 'Property' }",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (msg) {
                                                if (msg.d == "Success upload with approval") {
                                                    swal("Success", "Upload Successful and For Approval!", "success");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d == "Upload failed batch name already exists") {
                                                    swal("Error", "Upload failed!\nBatch name already exists!", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("Success upload without approval", 0)) {
                                                    swal("Success", "Successfully uploaded [" + msg.d.split(':')[1] + "] property/ies.", "success");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("Duplicate property", 0)) {
                                                    swal("Warning", "Duplicate Property Numbers in csv file : \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("Duplicate serial", 0)) {
                                                    swal("Warning", "Duplicate Serial Numbers in csv file : \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("Duplicate rfid", 0)) {
                                                    //removed from sp checking
                                                    swal("Warning", "Duplicate RFID Numbers in csv file : \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("AlreadyExists")) {
                                                    //swal("Warning", "Property Numbers Already Exists in the system :  \n " + msg.d.split(':')[1], "warning");
                                                    loadAlreadyExsists(msg);
                                                    $("#modalPropertyAlreadyExists").modal("show");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d == "Multiple Batchname detected") {
                                                    swal("Error", "Multiple batch name on csv file.", "error");
                                                }
                                                else if (msg.d.includes("AlreadySerial")) {
                                                    //swal("Warning", "Serial Numbers Already Exists in the system :  \n " + msg.d.split(':')[1], "warning");
                                                    loadAlreadyExsistsSerial(msg);
                                                    $("#modalSerialAlreadyExists").modal("show");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("InvalidData")) {
                                                    //swal("Warning", "Property that has Invalid Data in csv File :  \n " + msg.d.split(':')[1], "warning");
                                                    loadInvalidData(msg);
                                                    $("#modalPropertyUploadInvalid").modal("show");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d == "File Empty") {
                                                    swal("Error", "File is Empty, no data to process", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("Invalid Format for Acquired Cost")) {
                                                    swal("Warning", "Acquired Cost must be greater than or equal to zero (0).", "warning");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d.includes("Incorrect Date Format.")) {
                                                    swal("Warning", "Please check the format of all encoded dates (Acquisition, Warranty, Insurance and Expiry Date)", "warning");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d == "Empty Field") {
                                                    swal("Error", "Required Field:\nPropertyNumber\nItemCode\nSerialNumber\nSupplier\nAcquiredCost\nSite\nLocation\nArea\nBatchName", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }

                                                else if (msg.d == "Empty EN") {
                                                    swal("Error", "Required field if you input an IssuanceNo: EmployeeNo", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                
                                                else if (msg.d == "Empty SN") {
                                                    swal("Error", "Required field if you input an EmployeeNo: IssuanceNo", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }

                                                else {//if (msg.d == "Mismatch header") {
                                                    swal("Error", "Mismatch Header Columns!", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
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

        /////////Upload for stock
        function UploadStock() {
            var directory = $("#BodyHolder_txtDirectoryPath").val().replace(/[\\]/gi, ',');
            var filess = $('#BodyHolder_File_UploadStock').val();
            if (filess == "") {
                swal("", "Select .csv file to upload.", "warning");
            }
            else {
                swal({
                    title: "Attention",
                    text: "Uploading of property will be subject for Approval if System Setting is set to ON, otherwise this will proceed to database entry.",
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
                            var fileUpload = $("#BodyHolder_File_UploadStock").get(0);
                            var files = fileUpload.files;
                            var test = new FormData();
                            for (var i = 0; i < files.length; i++) {
                                test.append(files[i].name, files[i]);
                            }
                            $.ajax({
                                url: "System/Propertyupload.ashx?",
                                type: "POST",
                                contentType: false,
                                processData: false,
                                data: test,
                                success: function (result) {
                                    if (result.includes("File Uploaded Successfully", 0)) {
                                        var filename = result.split(':')[1];
                                        $.ajax({
                                            type: "POST",
                                            url: "System/DataUpload.aspx/ProcessDataUpload",
                                            data: " { directory : '" + directory + "', filename : '" + filename + "', type: 'Stock' } ",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (msg) {
                                                if (msg.d == "Success upload with approval") {
                                                    swal("Success", "Upload Successful and For Approval!", "success");
                                                }
                                                else if (msg.d == "Upload failed batch name already exists") {
                                                    swal("Error", "Upload failed!\nBatch name already exists!", "error");
                                                }
                                                else if (msg.d.includes("Success upload without approval", 0)) {
                                                    swal("Success", "Successfully uploaded [" + msg.d.split(':')[1] + "] supply/supplies.", "success");
                                                }
                                                else if (msg.d.includes("Duplicate stock", 0)) {
                                                    swal("Warning", "Duplicate Item Code in csv File : \n " + msg.d.split(':')[1], "warning");
                                                }
                                                else if (msg.d.includes("Already Exists", 0)) {
                                                    swal("Warning", "Supply Already Exists in the system :  \n " + msg.d.split(':')[1], "warning");
                                                }
                                                else if (msg.d.includes("InvalidData", 0)) {
                                                    loadInvalidDataStock(msg);
                                                    $("#modalStockUploadInvalid").modal("show");
                                                }
                                                else if (msg.d == "File Empty") {
                                                    swal("Error", "File is Empty, no data to process.", "error");
                                                }
                                                else if (msg.d == "Multiple Batchname detected") {
                                                    swal("Error", "Multiple batch name on csv file.", "error");
                                                }
                                                else if (msg.d == "Empty Field") {
                                                    swal("Error", "Required Field:\nItemCode\nQuantity\nCustodianType\nBatchName", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else if (msg.d == "Below zero") {
                                                    swal("Error", "Quantity should not be below zero (0) or negative", "error");
                                                    $('#BodyHolder_File_UploadProperty').val("");
                                                }
                                                else {
                                                    swal("Error", "Mismatch Header Columns! (SUPPLIES)", "error");
                                                }
                                                $('#BodyHolder_File_UploadStock').val("");
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

        /////////Property Invalid Data
        function loadInvalidData(data) {
            $('#propertyuploadinvalid').hide();
            $('#propertyuploadinvalid').innerHTML = "";
            $('#propertyuploadinvalid').fadeIn(200);
            myJsonData = data;
            populatePropertyInvalid(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populatePropertyInvalid(data) {
            try {
                var table = $('#tblPropertyUploadInvalid').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "PropertyNumber" },
                        { "data": "ItemCode" },
                        { "data": "PONumber" },
                        { "data": "SupplierName" },
                        { "data": "Site" },
                        { "data": "Location" },
                        { "data": "Area" },
                        { "data": "DRNumber" }
                    ]
                    , columnDefs: [{
                        targets: [1, 2, 3, 4, 5, 6, 7],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:black">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:red">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Upload Invalid Data'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Upload Invalid Data'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Upload Invalid Data'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Upload Invalid Data'
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
                    }
                });

            } catch (e) {
                var table = $('#tblPropertyUploadInvalid').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

        /////////Property Already Exists
        function loadAlreadyExsists(data) {
            $('#propertyalreadyexists').hide();
            $('#propertyalreadyexists').innerHTML = "";
            $('#propertyalreadyexists').fadeIn(200);
            myJsonData = data;
            populatePropertyAlreadyExists(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populatePropertyAlreadyExists(data) {
            try {
                var table = $('#tblPropertyAlreadyExists').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "PropertyNumber" },
                        { "data": "ItemCode" },
                        { "data": "PONumber" },
                        { "data": "SupplierName" },
                        { "data": "Site" },
                        { "data": "Location" },
                        { "data": "Area" }
                    ]
                    , columnDefs: [{
                        targets: [0],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:red">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:black">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Numbers Already Exists'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Numbers Already Exists'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Numbers Already Exists'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Numbers Already Exists'
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
                    }
                });

            } catch (e) {
                var table = $('#tblPropertyAlreadyExists').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

        /////////Serial Already Exists
        function loadAlreadyExsistsSerial(data) {
            $('#serialalreadyexists').hide();
            $('#serialalreadyexists').innerHTML = "";
            $('#serialalreadyexists').fadeIn(200);
            myJsonData = data;
            populateSerialAlreadyExists(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populateSerialAlreadyExists(data) {
            try {
                var table = $('#tblSerialAlreadyExists').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "PropertyNumber" },
                        { "data": "SerialNo" },
                        { "data": "ItemCode" },
                        { "data": "PONumber" },
                        { "data": "SupplierName" },
                        { "data": "Site" },
                        { "data": "Location" },
                        { "data": "Area" }
                    ]
                    , columnDefs: [{
                        targets: [1],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:red">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:black">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
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
                    }
                });

            } catch (e) {
                var table = $('#tblSerialAlreadyExists').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

        /////////Stock Invalid data
        function loadInvalidDataStock(data) {
            $('#stockuploadinvalid').hide();
            $('#stockuploadinvalid').innerHTML = "";
            $('#stockuploadinvalid').fadeIn(200);
            myJsonData = data;
            populateStockInvalid(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populateStockInvalid(data) {
            try {
                var table = $('#tblStockUploadInvalid').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "ItemCode" },
                        { "data": "Custodian" },
                        { "data": "Region" },
                        //{ "data": "Location" },
                        { "data": "QuantityString" }
                    ]
                    , columnDefs: [{
                        targets: [0, 1, 2],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:black">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:red">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Upload Invalid Data'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Upload Invalid Data'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Upload Invalid Data'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Upload Invalid Data'
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
                    }
                });

            } catch (e) {
                var table = $('#tblPropertyUploadInvalid').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

        ////////Goods Receive Upload
        function DownloadTemplateGRProperty() {
            fileName = 'GoodsReceivePropertyUpload.csv';
            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/DownloadTemplate",
                data: " {fileName : '" + fileName + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(msg.d);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        function DownloadTemplateGRStock() {
            var fileName = 'GoodsReceiveSupplyUpload.csv';
            $.ajax({
                type: "POST",
                url: "System/DataUpload.aspx/DownloadTemplate",
                data: " {fileName : '" + fileName + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(msg.d);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        function UploadGR() {
            var filess = $('#BodyHolder_File_UploadGR').val();
            if (filess == "") {
                swal("", "Select .csv file to upload.", "warning");
            }
            else {
                if ($('#ddlGRType').val() == "Property") {
                    swal({
                        title: "Attention",
                        text: "Uploading of goods receive will be subject for Approval if System Setting is set to ON, otherwise this will proceed to database entry.",
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
                                var fileUpload = $("#BodyHolder_File_UploadGR").get(0);
                                var files = fileUpload.files;
                                var test = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    test.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    url: "System/Propertyupload.ashx?",
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: test,
                                    success: function (result) {
                                        if (result.includes("File Uploaded Successfully", 0)) {
                                            var filename = result.split(':')[1];
                                            $.ajax({
                                                type: "POST",
                                                url: "System/DataUpload.aspx/ProcessDataUploadGR",
                                                data: "{filename : '" + filename + "', type: 'Property' }",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == "Success upload with approval") {
                                                        swal("Success", "Upload Successful and For Approval!", "success");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("Success upload without approval", 0)) {
                                                        swal("Success", "Successfully uploaded [" + msg.d.split(':')[1] + "] property/ies.", "success");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("Duplicate serial", 0)) {
                                                        swal("Warning", "Duplicate Serial Numbers in csv file : \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("AlreadySerial")) {
                                                        loadAlreadyExsistsSerialGR(msg);
                                                        $("#modalGRSerialAlreadyExists").modal("show");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("InvalidData")) {
                                                        loadInvalidDataGR(msg);
                                                        $("#modalGRPropertyUploadInvalid").modal("show");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "File Empty") {
                                                        swal("Error", "File is Empty, no data to process", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Object Reference") {
                                                        swal("Error", "Please select type of upload.", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Acquired Cost must be greater than or equal zero (0)") {
                                                        swal("Error", "Acquired Cost must be greater than or equal zero (0).", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Incorrect Date Format.") {
                                                        swal("Error", "Incorrect Date Format.", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Empty Field") {
                                                        swal("Error", "Required Field:\nPONumber\nPRNumber\nSupplierName\nPropertyItemCode\nSerialNumber\nAcquiredCost\nReceivedBy\nCustodianType\nSite\nLocation\nApprover\nRemarks", "error");
                                                        $('#BodyHolder_File_UploadProperty').val("");
                                                    }
                                                    else {
                                                        swal("Error", "Mismatch Header Columns!", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
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
                else {
                    swal({
                        title: "Attention",
                        text: "Uploading of goods receive will be subject for Approval if System Setting is set to ON, otherwise this will proceed to database entry.",
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
                                var fileUpload = $("#BodyHolder_File_UploadGR").get(0);
                                var files = fileUpload.files;
                                var test = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    test.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    url: "System/Propertyupload.ashx?",
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: test,
                                    success: function (result) {
                                        if (result.includes("File Uploaded Successfully", 0)) {
                                            var filename = result.split(':')[1];
                                            $.ajax({
                                                type: "POST",
                                                url: "System/DataUpload.aspx/ProcessDataUploadGR",
                                                data: "{filename : '" + filename + "', type: 'Stock' }",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == "Success upload with approval") {
                                                        swal("Success", "Upload success and for approval!", "success");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("Success upload without approval", 0)) {
                                                        swal("Success", "Successfully uploaded [" + msg.d.split(':')[1] + "] supply/supplies.", "success");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("Duplicate stock")) {
                                                        swal("Warning", "Duplicate Supply in csv file : \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("Zero Quantity")) {
                                                        swal("Warning", "Supply with Zero (0) quantity in csv file: \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d.includes("InvalidData")) {
                                                        loadInvalidDataGRStock(msg);
                                                        $("#modalGRStockUploadInvalid").modal("show");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "File Empty") {
                                                        swal("Error", "File is Empty, no data to process", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Object Reference") {
                                                        swal("Error", "Please select type of upload.", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Incorrect Date Format.") {
                                                        swal("Error", "Incorrect Date Format.", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
                                                    }
                                                    else if (msg.d == "Empty Field") {
                                                        swal("Error", "Required Field:\nPONumber\nPRNumber\nSupplierName\nStockItemCode\nSerialNumber\nQuantity\nAcquiredCost\nCustodianType\nSite\nApprover\nRemarks", "error");
                                                        $('#BodyHolder_File_UploadProperty').val("");
                                                    }
                                                    else {
                                                        swal("Error", "Mismatch Header Columns!", "error");
                                                        $('#BodyHolder_File_UploadGR').val("");
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
        }

        /////////GR Serial Already Exists
        function loadAlreadyExsistsSerialGR(data) {
            $('#grserialalreadyexists').hide();
            $('#grserialalreadyexists').innerHTML = "";
            $('#grserialalreadyexists').fadeIn(200);
            myJsonData = data;
            populateGRSerialAlreadyExists(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populateGRSerialAlreadyExists(data) {
            try {
                var table = $('#tblGRSerialAlreadyExists').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "SerialNo" },
                        { "data": "ItemCode" },
                        { "data": "PONumber" },
                        { "data": "SupplierName" },
                        { "data": "Site" },
                        { "data": "Location" }
                    ]
                    , columnDefs: [{
                        targets: [0],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:red">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:black">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Serial Numbers Already Exists'
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
                    }
                });

            } catch (e) {
                var table = $('#tblGRSerialAlreadyExists').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

        /////////GR Property Invalid Data
        function loadInvalidDataGR(data) {
            $('#grpropertyuploadinvalid').hide();
            $('#grpropertyuploadinvalid').innerHTML = "";
            $('#grpropertyuploadinvalid').fadeIn(200);
            myJsonData = data;
            populateGRPropertyInvalid(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populateGRPropertyInvalid(data) {
            try {
                var table = $('#tblGRPropertyUploadInvalid').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "SerialNo" },
                        { "data": "ItemCode" },
                        { "data": "PONumber" },
                        { "data": "SupplierName" },
                        { "data": "Site" },
                        { "data": "Location" },
                        { "data": "Approver" },
                        { "data": "AssignedPersonName" }
                    ]
                    , columnDefs: [{
                        targets: [1, 2, 3, 4, 5, 6, 7],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:red">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:black">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Property Upload Invalid Data'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Property Upload Invalid Data'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Property Upload Invalid Data'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Property Upload Invalid Data'
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
                    }
                });

            } catch (e) {
                var table = $('#tblGRPropertyUploadInvalid').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

        //////////GR Stock Invalid Data
        function loadInvalidDataGRStock(data) {
            $('#grstockuploadinvalid').hide();
            $('#grstockuploadinvalid').innerHTML = "";
            $('#grstockuploadinvalid').fadeIn(200);
            myJsonData = data;
            populateGRStockInvalid(myJsonData);
            console.log(data);
            console.log('Success');
        }

        function populateGRStockInvalid(data) {
            try {
                var table = $('#tblGRStockUploadInvalid').DataTable({
                    "destroy": true
                    , "processing": true
                    , fixedHeader: {
                        header: true,
                        footer: true,
                        headerOffset: 50
                    }
                    , "language": {
                        processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                    }
                    , "scrollX": true
                    , "data": JSON.parse(data.d)
                    , "columns": [
                        { "data": "ItemCode" },
                        { "data": "Custodian" },
                        { "data": "SupplierName" },
                        { "data": "Region" },
                        { "data": "Approver" }
                    ]
                    , columnDefs: [{
                        targets: [0, 1, 2, 3, 4],
                        render: function (data, type, row) {
                            if (data.includes("---")) {
                                return '<span style="color:black">' + data.replace('---', '') + '</span>';
                            }
                            else {
                                return '<span style="color:red">' + data + '</span>';
                            }
                        }
                    }]
                    , dom: 'Brftip'
                    , colReorder: true
                    , stateSave: true
                    , buttons: {
                        buttons: [
                        {
                            extend: 'collection',
                            text: '...',
                            buttons: [
                                {
                                    extend: 'copy',
                                    text: 'COPY',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Supply Upload Invalid Data'
                                },
                                {
                                    extend: 'excel',
                                    text: 'EXCEL',
                                    className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Supply Upload Invalid Data'
                                },
                                 {
                                     extend: 'csv',
                                     text: 'CSV',
                                     className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Supply Upload Invalid Data'
                                 },
                                  {
                                      extend: 'print',
                                      text: 'PRINT',
                                      className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive Supply Upload Invalid Data'
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
                    }
                });

            } catch (e) {
                var table = $('#tblGRStockUploadInvalid').DataTable();
                table
                    .clear()
                    .draw();
            }
        }

            //User Upload - Addded by: Domdom
            function UploadUser() {
                var directory = $("#BodyHolder_txtDirectoryPath").val().replace(/[\\]/gi, ',');
                var filess = $('#BodyHolder_File_UploadUser').val();
                if (filess == "") {
                    swal("", "Select .csv file to upload.", "warning");
                }
                else {
                    swal({
                        title: "Attention",
                        text: "Uploading of user/s will proceed to database entry.",
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
                                var fileUpload = $("#BodyHolder_File_UploadUser").get(0);
                                var files = fileUpload.files;
                                var test = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    test.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    url: "System/Propertyupload.ashx?",
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: test,
                                    success: function (result) {
                                        if (result.includes("File Uploaded Successfully", 0)) {
                                            var filename = result.split(':')[1];
                                            $.ajax({
                                                type: "POST",
                                                url: "System/DataUpload.aspx/ProcessDataUpload",
                                                data: "{ directory : '" + directory + "', filename : '" + filename + "', type: 'User' }",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == "Success upload with approval") {
                                                        swal("Success", "Upload Successful and For Approval!", "success");
                                                    }
                                                    else if (msg.d == "Upload failed batch name already exists") {
                                                        swal("Error", "Upload failed!\nBatch name already exists!", "error");
                                                    }
                                                    else if (msg.d.includes("Success upload without approval", 0)) {
                                                        swal("Success", "Successfully uploaded [" + msg.d.split(':')[1] + "] user/s.", "success");

                                                    }
                                                    else if (msg.d.includes("Approver license is already full")) {
                                                        swal("Error", "Approver license is already full", "error");
                                                    }
                                                    else if (msg.d.includes("Duplicate user", 0)) {
                                                        swal("Warning", "Duplicate Username in csv file : \n " + msg.d.split(':')[1].replaceAll(',', '\n'), "warning");
                                                    }
                                                    else if (msg.d.includes("AlreadyExists")) {
                                                        loadAlreadyExsistsUser(msg);
                                                        $("#modalUserAlreadyExists").modal("show");
                                                    }
                                                    else if (msg.d.includes("InvalidData")) {
                                                        loadInvalidDataUser(msg);
                                                        $("#modalUserInvalidData").modal("show");
                                                    }
                                                    else if (msg.d == "File Empty") {
                                                        swal("Error", "File is Empty, no data to process", "error");
                                                    }
                                                    else if (msg.d == "Mismatch header") {
                                                        swal("Error", "Mismatch Header Columns!", "error");
                                                    }
                                                    else if (msg.d == "Empty Field") {
                                                        swal("Error", "Required Field:\nFirstName\nLastName\nUserName\nEmployeeNo\nSite\nCustodianType\nUserDepartment\nPosition\nBatchName", "error");
                                                        $('#BodyHolder_File_UploadProperty').val("");
                                                    }
                                                    else if (msg.d == "Empty Field Custodian") {
                                                        swal("Error", "CustodianType column should not be empty if IsCustodian is 'YES'", "error");
                                                        $('#BodyHolder_File_UploadProperty').val("");
                                                    }
                                                    else if (msg.d == "Invalid Data Custodian") {
                                                        swal("Error", "IsCustodian should only be 'YES' or 'NO'", "error");
                                                        $('#BodyHolder_File_UploadProperty').val("");
                                                    }
                                                    else {
                                                        alert("ERROR");
                                                    }
                                                    $('#BodyHolder_File_UploadUser').val("");
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

            /////////User Already Exists
            function loadAlreadyExsistsUser(data) {
                $('#useralreadyexists').hide();
                $('#useralreadyexists').innerHTML = "";
                $('#useralreadyexists').fadeIn(200);
                myJsonData = data;
                populateUserAlreadyExists(myJsonData);
                console.log(data);
                console.log('Success');
            }

            function populateUserAlreadyExists(data) {
                try {
                    var table = $('#tblUserAlreadyExists').DataTable({
                        "destroy": true
                        , "processing": true
                        , fixedHeader: {
                            header: true,
                            footer: true,
                            headerOffset: 50
                        }
                        , "language": {
                            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                        }
                        , "scrollX": true
                        , "data": JSON.parse(data.d)
                        , "columns": [
                            { "data": "FirstName" },
                            { "data": "UserName" },
                            { "data": "EmailAddress" },
                            { "data": "Department" },
                            { "data": "Position" },
                            { "data": "Custodians" },
                            { "data": "Site" }
                        ]
                        , columnDefs: [{
                            targets: [1],
                            render: function (data, type, row) {
                                if (data.includes("---")) {
                                    return '<span style="color:red">' + data.replace('---', '') + '</span>';
                                }
                                else {
                                    return '<span style="color:black">' + data + '</span>';
                                }
                            }
                        }]
                        , dom: 'Brftip'
                        , colReorder: true
                        , stateSave: true
                        , buttons: {
                            buttons: [
                            {
                                extend: 'collection',
                                text: '...',
                                buttons: [
                                    {
                                        extend: 'copy',
                                        text: 'COPY',
                                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Username Already Exists'
                                    },
                                    {
                                        extend: 'excel',
                                        text: 'EXCEL',
                                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Username Already Exists'
                                    },
                                     {
                                         extend: 'csv',
                                         text: 'CSV',
                                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Username Already Exists'
                                     },
                                      {
                                          extend: 'print',
                                          text: 'PRINT',
                                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Username Already Exists'
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
                        }
                    });

                } catch (e) {
                    var table = $('#tblUserAlreadyExists').DataTable();
                    table
                        .clear()
                        .draw();
                }
            }

            /////////User Invalid Data
            function loadInvalidDataUser(data) {
                $('#userinvaliddata').hide();
                $('#userinvaliddata').innerHTML = "";
                $('#userinvaliddata').fadeIn(200);
                myJsonData = data;
                populateUserInvalidData(myJsonData);
                console.log(data);
                console.log('Success');
            }

            function populateUserInvalidData(data) {
                try {
                    var table = $('#tblUserInvalidData').DataTable({
                        "destroy": true
                        , "processing": true
                        , fixedHeader: {
                            header: true,
                            footer: true,
                            headerOffset: 50
                        }
                        , "language": {
                            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                        }
                        , "scrollX": true
                        , "data": JSON.parse(data.d)
                        , "columns": [
                            { "data": "FirstName" },
                            { "data": "UserName" },
                            { "data": "EmailAddress" },
                            { "data": "Department" },
                            { "data": "Position" },
                            { "data": "Custodians" },
                            { "data": "Site" }
                        ]
                        , columnDefs: [{
                            targets: [3, 4, 5, 6],
                            render: function (data, type, row) {
                                if (data.includes("---")) {
                                    return '<span style="color:black">' + data.replace('---', '') + '</span>';
                                }
                                else {
                                    return '<span style="color:red">' + data + '</span>';
                                }
                            }
                        }]
                        , dom: 'Brftip'
                        , colReorder: true
                        , stateSave: true
                        , buttons: {
                            buttons: [
                            {
                                extend: 'collection',
                                text: '...',
                                buttons: [
                                    {
                                        extend: 'copy',
                                        text: 'COPY',
                                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User Upload Invalid Data'
                                    },
                                    {
                                        extend: 'excel',
                                        text: 'EXCEL',
                                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User Upload Invalid Data'
                                    },
                                     {
                                         extend: 'csv',
                                         text: 'CSV',
                                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User Upload Invalid Data'
                                     },
                                      {
                                          extend: 'print',
                                          text: 'PRINT',
                                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User Upload Invalid Data'
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
                        }
                    });

                } catch (e) {
                    var table = $('#tblUserInvalidData').DataTable();
                    table
                        .clear()
                        .draw();
                }

            }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="content-header">
        <h1>Data Upload
        </h1>
        <ol class="breadcrumb" runat="server" id="navigation_bread">
            <li><i class="fa fa-lock"></i>System</li>
            <li>Data Upload</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div class="row" hidden>
            <asp:TextBox ID="txtDirectoryPath" runat="server" type="text" class="form-control" Style="width: 100%" />
        </div>
        <%--//User--%>
        <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>User Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                         <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnDownloadUserFormat" onclick="DownloadTemplateUser()">&nbsp;&nbsp;Download Template&nbsp;&nbsp;</button>
                            <%--<asp:Button ID="btnDowloadPropertyFormat" runat="server" class="btn btn-sm btn-primary" Text="Download Template" Style="width: 100%;" OnClick="DownloadFormatProperty" />--%>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" align="right">
                            <asp:FileUpload ID="File_UploadUser" class="btn btn-sm btn-primary" runat="server" Style="width: 100%;" title="Please select a .csv file to upload." />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnUploadUser" onclick="UploadUser()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>
                            <%--<asp:Button ID="btnUploadProperty" runat="server" class="btn btn-sm btn-success" Text="Upload" Style="width: 100%" OnClick="btnImport_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box  box-solid box-primary collapsed-box" hidden>
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Existing User Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" align="right">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Existing Property Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnDowloadPropertyFormat" onclick="DownloadTemplateProperty()">&nbsp;&nbsp;Download Template&nbsp;&nbsp;</button>
                            <%--<asp:Button ID="btnDowloadPropertyFormat" runat="server" class="btn btn-sm btn-primary" Text="Download Template" Style="width: 100%;" OnClick="DownloadFormatProperty" />--%>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" align="right">
                            <asp:FileUpload ID="File_UploadProperty" class="btn btn-sm btn-primary" runat="server" Style="width: 100%;" title="Please select a .csv file to upload." />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnUploadProperty" onclick="UploadProperty()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>
                            <%--<asp:Button ID="btnUploadProperty" runat="server" class="btn btn-sm btn-success" Text="Upload" Style="width: 100%" OnClick="btnImport_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Existing Supply Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnDowloadStockFormat" onclick="DownloadTemplateStock()">&nbsp;&nbsp;Download Template&nbsp;&nbsp;</button>
                            <%--<asp:Button ID="btnDowloadStockFormat" runat="server" class="btn btn-sm btn-primary" Text="Download Template" Style="width: 100%;" OnClick="DownloadFormatStock" />--%>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" align="right">
                            <asp:FileUpload ID="File_UploadStock" class="btn btn-sm btn-primary" runat="server" Style="width: 100%;" title="Please select a .csv file to upload." />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" align="right">
                            <button type="button" class="btn btn-sm btn-primary " id="btnUploadStock" onclick="UploadStock()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>
                            <%--<asp:Button ID="btnUploadStock" runat="server" class="btn btn-sm btn-success" Text="Upload" Style="width: 100%" OnClick="btnImportStock_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box  box-solid box-primary collapsed-box">
            <div class="box-header with-border" data-widget="collapse">
                <h3 class="box-title"><span></span>Goods Receive Upload</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="btn-group pull-left col-lg-2">
                    <button type="button" class="btn btn-primary pull-left">Template</button>
                    <button type="button" class="btn btn-primary dropdown-toggle pull-left" data-toggle="dropdown">
                        <span class="caret"></span>
                        <span class="sr-only">Download Template</span>
                    </button>
                    <ul class="dropdown-menu" role="menu">
                        <li><a style="cursor: pointer" onclick="DownloadTemplateGRProperty()">Goods Receive Property</a></li>
                        <li><a style="cursor: pointer" onclick="DownloadTemplateGRStock()">Goods Receive Supply</a></li>
                    </ul>
                </div>
                <div class="col-lg-2">
                    <select id="ddlGRType" class="form-control select2" data-dropdown-css-class="select2-primary" style="width: 100%; height: 35px">
                        <option value="Property">Property</option>
                        <option value="Stock">Supply</option>
                    </select>
                </div>
                <div class="col-lg-6" align="right">
                    <asp:FileUpload ID="File_UploadGR" class="btn btn-sm btn-primary" runat="server" Style="width: 100%;" title="Please select a .csv file to upload." />
                </div>
                <div class="col-lg-2" align="right">
                    <button type="button" class="btn btn-sm btn-primary " id="btnUploadGRProperty" onclick="UploadGR()">&nbsp;&nbsp;Upload&nbsp;&nbsp;</button>
                    <%--<asp:Button ID="btnUploadStock" runat="server" class="btn btn-sm btn-success" Text="Upload" Style="width: 100%" OnClick="btnImportStock_Click" />--%>
                </div>
            </div>
        </div>
    </section>
    <%--Invalid Data Property--%>
    <div id="modalPropertyUploadInvalid" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Property Upload Details with Invalid Data</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                   *Rows and columns with <span style="color:red">RED COLOR</span> has an invalid data.</i>
                                <i><span style="color:red">*Double-check your CSV file columns in case you have missing inputted information.</span></i>
                                </p></center>
                        </div>
                        <div class="col-lg-12">
                            <table id="tblPropertyUploadInvalid" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SUPPLIER</td>
                                        <td>SITE</td>
                                        <td>LOCATION</td>
                                        <td>AREA</td>
                                        <td>EMPLOYEE #</td>
                                    </tr>
                                </thead>
                                <tbody id="propertyuploadinvalid">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Already Exists Property--%>
    <div id="modalPropertyAlreadyExists" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>&nbsp;Property Upload Details which already Exists in the system</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblPropertyAlreadyExists" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SUPPLIER</td>
                                        <td>SITE</td>
                                        <td>LOCATION</td>
                                        <td>AREA</td>
                                    </tr>
                                </thead>
                                <tbody id="propertyalreadyexists">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Already Exists Serial--%>
    <div id="modalSerialAlreadyExists" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>&nbsp;Property Upload Details which Serial # already exists in the system</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblSerialAlreadyExists" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>PROPERTY #</td>
                                        <td>SERIAL #</td>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SUPPLIER</td>
                                        <td>SITE</td>
                                        <td>LOCATION</td>
                                        <td>AREA</td>
                                    </tr>
                                </thead>
                                <tbody id="serialalreadyexists">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Invalid Data Stock--%>
    <div id="modalStockUploadInvalid" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Supply Upload Details with Invalid Data</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                   *Rows and columns with <span style="color:red">RED COLOR</span> has an invalid data.</i>
                                                                <i><span style="color:red">*Double-check your CSV file columns in case you have missing inputted information.</span></i>

                                </p></center>
                        </div>
                        <div class="col-lg-12">
                            <table id="tblStockUploadInvalid" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SITE</td>
                                        <td>QUANTITY</td>
                                    </tr>
                                </thead>
                                <tbody id="stockuploadinvalid">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Invalid Data GR Property--%>
    <div id="modalGRPropertyUploadInvalid" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Goods Receive Property Upload Details with Invalid Data</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                   *Rows and columns with <span style="color:red">RED COLOR</span> has an invalid data.</i>
                                                                <i><span style="color:red">*Double-check your CSV file columns in case you have missing inputted information.</span></i>

                                </p></center>
                        </div>
                        <div class="col-lg-12">
                            <table id="tblGRPropertyUploadInvalid" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>SERIAL #</td>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SUPPLIER</td>
                                        <td>SITE</td>
                                        <td>LOCATION</td>
                                        <td>APPROVER</td>
                                        <td>ASSIGNED PERSON</td>
                                    </tr>
                                </thead>
                                <tbody id="grpropertyuploadinvalid">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Already Exists Serial--%>
    <div id="modalGRSerialAlreadyExists" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>&nbsp;Goods Receive Property Upload Details which Serial # already exists in the system</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblGRSerialAlreadyExists" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>SERIAL #</td>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SUPPLIER</td>
                                        <td>SITE</td>
                                        <td>LOCATION</td>
                                    </tr>
                                </thead>
                                <tbody id="grserialalreadyexists">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Invalid Data GR Stock--%>
    <div id="modalGRStockUploadInvalid" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>Goods Receive Supply Upload Details with Invalid Data</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <center><p style="font-size:12px;"><i>
                                   *Rows and columns with <span style="color:red">RED COLOR</span> has an invalid data.</i>
                                                                <br><i><span style="color:red">*Double-check your CSV file columns in case you have missing inputted information.</span></i>

                                </p></center>
                        </div>
                        <div class="col-lg-12">
                            <table id="tblGRStockUploadInvalid" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>ITEM CODE</td>
                                        <td>CUSTODIAN TYPE</td>
                                        <td>SUPPLIER</td>
                                        <td>SITE</td>
                                        <td>APPROVER</td>
                                    </tr>
                                </thead>
                                <tbody id="grstockuploadinvalid">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <%--Invalid Data User--%>
    <div id="modalUserInvalidData" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>&nbsp;User Upload Details which has an invalid data</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblUserInvalidData" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>NAME</td>
                                        <td>USERNAME</td>
                                        <td>EMAIL ADDRESS</td>
                                        <td>DEPARTMENT</td>
                                        <td>JOB TITLE</td>
                                        <td>CUSTODIAN</td>
                                        <td>SITE</td>
                                    </tr>
                                </thead>
                                <tbody id="userinvaliddata">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <%--Already Exists User--%>
    <div id="modalUserAlreadyExists" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: steelblue; text-align: center;">
                    <button type="button" class="btn btn-sm btn-danger pull-right fa fa-close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4>
                        <p style="color: white;"><span class="fa fa-list"></span>&nbsp;User Upload Details which username already exists in the system</p>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="tblUserAlreadyExists" role="grid" class="table table-bordered table-hover table-condensed" style="width: 100%; table-layout: fixed;">
                                <thead style="font-weight: bold">
                                    <tr>
                                        <td>NAME</td>
                                        <td>USERNAME</td>
                                        <td>EMAIL ADDRESS</td>
                                        <td>DEPARTMENT</td>
                                        <td>POSITION</td>
                                        <td>CUSTODIAN</td>
                                        <td>SITE</td>
                                    </tr>
                                </thead>
                                <tbody id="useralreadyexists">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>