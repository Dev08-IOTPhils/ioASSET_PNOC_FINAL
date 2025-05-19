var url = "";

$(document).ready(function () {
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];
    departmentPI();

    GetEmployeePI();


    //ddlEmp

    ////EXCEL FROM ////

    $('#PrintExcel').off('click').on('click', function () {
       // alert($('#ddlEmp').val());
        var emp = $('#ddlEmp').val();
        if (emp == '0') {
            swal("Warning", "Select Employee to proceed.", "warning");
        }
        else { 
            const now = new Date();

            const year = now.getFullYear();
            const month = (now.getMonth() + 1).toString().padStart(2, '0');
            const day = now.getDate().toString().padStart(2, '0');
            const hours = now.getHours().toString().padStart(2, '0');
            const minutes = now.getMinutes().toString().padStart(2, '0');
            const seconds = now.getSeconds().toString().padStart(2, '0');

            const formattedDateTime = `${year}${month}${day}${hours}${minutes}${seconds}`;

            var context = window.location.pathname.substring(1);
           // alert(context);
            $.ajax({
                async: false,
                type: 'POST',
                url: context + '/ReportPage',
                data: "{ key1: '" + 'ELCard' + "', key2: '" + emp + "', key3: '" + '' + "', key4: '" + '' + "', key5: '" + '' + "'}",
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
                        a.attr("download", "ELC_" + formattedDateTime + ".xlsx");
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
    
    /////


});

function LoadDatatable()
{
    // $('#ddlEmp').val();

    load_dataELC();
    ViewAssets($('#ddlEmp').val());
    
}

function load_dataELC()
{
  //  alert('load_data ' + $('#ddlEmp').val());

    $.ajax({
        type: "POST",
        async: false,
        url: url + "/EmployeeLedgerCard.aspx/LoadData",
        data: "{empID: '" + $('#ddlEmp').val() + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#itemlist').hide();
            $('#itemlist').innerHTML = "";
            $('#itemlist').fadeIn(200);

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
        $('#tblItemList').DataTable({
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
                { "data": "IssuedTo" },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "CustodianIDstr" },
                { "data": "Description" },
                { "data": "AcquiredCostString" },
                { "data": "AssetType" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'ELC'
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

        $('#tblItemList').off('click');

        $('#tblItemList').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblItemList').DataTable();
        table
            .clear()
            .draw();
    }
}

function ViewAssets(UserID) {
    

    $.ajax({
        type: "POST",
        async: false,
        url: url + "/EmployeeLedgerCard.aspx/GenerateReport",
        data: "{UserID: '" + UserID  + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var divPropertyCard = $('#BodyHolder_divStockCard');
            divPropertyCard.html(msg.d);
           
        }
    });

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



function GetEmployeePI() {
    var department = $('#ddlDepartmentPI').val();
    if ($('#ddlDepartmentPI').val() == "0")
        department = 0;
    if (department == 0) {
        $.ajax({
            type: "POST",
            url: url + "/EmployeeLedgerCard.aspx/GetUserNoDepartment",
            data: " { } ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ddlEmployee = $("[id*=ddlEmp]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                $.each(msg.d, function () {
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmp]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/EmployeeLedgerCard.aspx/GetUser",
            data: " { department : '" + department + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (department == 0) {
                    var ddlEmployee = $("[id*=ddlEmp]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                }
                else {
                    var ddlEmployee = $("[id*=ddlEmp]");
                    ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                    $.each(msg.d, function () {
                        ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            },
            error: function (msg1) {
                var ddlEmployee = $("[id*=ddlEmp]");
                ddlEmployee.empty().append("<option selected='selected' value='0'>--- Select Employee ---</option>");
                ddlEmployee.append($("<option></option>").val(this['Value']).html(this['Text']));
            }
        });
    }
}

var empID = '';
function PrintPDF()
{ 
    empID = $('#ddlEmp').val()
    if (empID == '0')
    {
        swal("Warning", "Select Employee to proceed.", "warning");
    }
    else {
        PrintReport();
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



