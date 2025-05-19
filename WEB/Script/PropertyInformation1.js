$(function () {
    $('.select2').select2();

    $(document).ready(function () {
        GetPropertyList();
    });

    $(document).on('click', '.btnView', function () {
        var currentRow = $(this).closest("tr");

        $.ajax({
            type: "POST",
            url: "PropertyInformation1.aspx/GetPropertyInformation",
            data: "{propertynumber: '" + currentRow.find("td:eq(1)").text() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                propertynumber = msg.d.split('|')[0];
                $('#txtItemCode').val(msg.d.split('|')[1]);
                $('#txtPropertyNo').val(msg.d.split('|')[0]);
                $('#txtSerialNo').val(msg.d.split('|')[2]);
                $('#txtLabelToPrint').val(msg.d.split('|')[3]);
                $('#txtDescription').val(msg.d.split('|')[4]);
                $('#modalPropertyInfo').modal('show');
            }
        });
       
    });

    $(document).on('click', '.btnUpdate', function () {

        //$(".se-pre-con").fadeIn();
        var serialno = ReplaceStr1($('#txtSerialNo').val().trim());
        var labeltoprint = ReplaceStr1($('#txtLabelToPrint').val().trim());
        var description = ReplaceStr1($('#txtDescription').val().trim());

        var alertMsg;

        if ($('#txtSerialNo').val().trim() == '' || $('#txtLabelToPrint').val().trim() == '' || $('#txtDescription').val().trim() == '') {
            alertMsg = 'Fill-out all the required field(s): ';

            if ($('#txtSerialNo').val() == '')
                alertMsg += '\nSerial Number';
            if ($('#txtLabelToPrint').val() == '')
                alertMsg += '\nLabel To Print';
            if ($('#txtDescription').val() == '')
                alertMsg += '\nDescription';
            alert(alertMsg);
        }
        else {

            if (confirm("Are you sure you want to update this property?") == false)
                return false;
            $.ajax({
                type: "POST",
                url: "PropertyInformation1.aspx/UpdatePropertyInformation",
                data: " { propertynumber : '" + propertynumber + "' , serialnumber : '" + serialno + "', labeltoprint :'" + labeltoprint + "', longdescription:'" + description + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    $(".se-pre-con").fadeOut("slow");
                    GetPropertyList();
                    $("#modalPropertyInfo").modal("hide");
                    alert('Property Successfully Updated!');
                }
            });

        }

    });

    var PropertyListArray = new Array();


    function GetPropertyList() {
        var search = $('#txtSearch').val().trim();
        tblAssetList.clear();
        $.ajax({
            type: "POST",
            url: "PropertyInformation1.aspx/SearchAssets",
            async: false,
            data: "{ search: '" + search + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                PropertyListArray = JSON.parse(msg.d);
                PropertyListArray.forEach((myprop, index) => {
                    var btnAction = '<center><button rowindex="' + index + '" recid="' + myprop.RecID + '" class="btn btn-xs btn-warning btnView" type="button"><span class="fa fa-list"></span></button></center>';
                    tblAssetList.row.add([myprop.ItemCode, myprop.PropertyNumber, myprop.Description, myprop.SerialNumber, myprop.ClassificationName, myprop.CategoryName, myprop.BrandName, myprop.ModelName,
                        btnAction]);
                });
            },
            error: function (xhr, status, error) {
            }
        });
        tblAssetList.draw();
    }


    var tblAssetList = $('#tblAssetList').DataTable({
        "destroy": true,
        "processing": true,
        "language": {
            "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
        },
        "pagingType": "full_numbers",
        "scrollX": true,
        dom: 'lfrtip'
    });

    function UpdatePropertyInfo() {
        var serialno = ReplaceStr1($('#txtSerialNo').val().trim());
        var labeltoprint = ReplaceStr1($('#txtLabelToPrint').val().trim());
        var description = ReplaceStr1($('#txtDescription').val().trim());

        var alertMsg;

        if ($('#txtSerialNo').val().trim() == '' || $('#txtLabelToPrint').val().trim() == '' || $('#txtDescription').val().trim() == '') {
            alertMsg = 'Fill-out all the required field(s): ';

            if ($('#txtSerialNo').val() == '')
                alertMsg += '\nSerial Number';
            if ($('#txtLabelToPrint').val() == '')
                alertMsg += '\nLabel To Print';
            if ($('#txtDescription').val() == '')
                alertMsg += '\nDescription';
            alert(alertMsg);
        }
        else {

            if (confirm("Are you sure you want to update this property?") == false)
                return false;
            $.ajax({
                type: "POST",
                url: "PropertyInformation1.aspx/UpdatePropertyInformation",
                data: " { propertynumber : '" + propertynumber + "' , serialnumber : '" + serialno + "', labeltoprint :'" + labeltoprint + "', longdescription:'" + description + "'} ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#modalPropertyInfo").modal("hide");
                    alert('Property Successfully Updated!');
                    GetPropertyList();
                }
            });

        }
    }
});


