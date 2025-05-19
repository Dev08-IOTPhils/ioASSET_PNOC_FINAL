
var url = '';

//////////////////---------------------------------------old
var hasDuplicate = 'false';
$(document).ready(function () {
    $('.select2').select2();
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtExpiry').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        startDate: '+0d',
        autoclose: true
    });
    $('#txtWarranty').daterangepicker({

        "minDate": "01/01/1970",
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
            'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')],
            'This Year': [moment().startOf('year'), moment().endOf('year')]
        },
        "alwaysShowCalendars": true,
        "opens": "center",
        drops: "up",
        "minYear": "1970",
        autoUpdateInput: true
    }).on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });;

    $('#txtInsurance').daterangepicker({

        "minDate": "01/01/1970",
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
            'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')],
            'This Year': [moment().startOf('year'), moment().endOf('year')]
        },
        "alwaysShowCalendars": true,
        "opens": "center",
        drops: "up",
        "minYear": "1970",
        autoUpdateInput: true
    }).on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });;

    $("#tblAssetList").DataTable({
        "scrollX": true
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
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

    load_data();
    GetUserCcustodian();

    $("#tblAssetList").DataTable().clear().draw();

    $("#txtSerialNo").change(function () {
        CheckDuplicateSerial(ReplaceStr1($('#txtSerialNo').val().trim()));
    })
});

function EnterKeySearch(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        load_data();
        return false;
    }
}

function CheckDuplicateSerial(id) {
    $.ajax({
        type: "POST",
        url: "Custodian_Operation_Module/PropertyInformation.aspx/CheckDuplicateSerial",
        data: " { serialno : '" + id + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "duplicate") {
                document.getElementById("txtSerialNo").style.borderColor = "#FF0000";
                hasDuplicate = 'true';
            } else {
                document.getElementById("txtSerialNo").style.borderColor = "";
                hasDuplicate = 'false';
            }
        }
    });
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

function load_data() {
    var search = "";

    $.ajax({
        type: 'POST',
        url: "Custodian_Operation_Module/PropertyInformation.aspx/GetList",
        data: "{ search: '" + search + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            populateDataTable(myJsonData);

            console.log(data);
            console.log('Success');

            $(".se-pre-con").fadeOut("slow");
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

var propertynumber = '';
function populateDataTable(data) {
    try {
        var table = $('#tblAssetList').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Custodian" },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "DateOfCreation" },
                { "data": "AcquiredCostString" },
                { "data": "Status" },
                { "data": "Action" },
            ],
            "order": [[8, 'desc']],
            "columnDefs": [{ 'targets': 8, 'type': 'date-ph', 'orderable': false }],

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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Information'
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
            // , stateSave: true
            //, columnDefs: [{ type: 'date', 'targets': [4] }]
        });

        $('#tblAssetList').on('click', 'button.btn_show', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");

            var context = window.location.pathname.substring(1);
            var url = context.split('/')[1];

            $('#txtNewPropertyNo').val("");

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
                        let elementsAddCat = document.getElementsByClassName("btn_EditPropertyInformation");
                        for (let i = 0; i < elementsAddCat.length; i++) {
                            elementsAddCat[i].remove();
                        }
                    }
                }
            });
            $.ajax({
                type: "POST",
                url: "Custodian_Operation_Module/PropertyInformation.aspx/GetPropertyInformation",
                data: "{propertynumber: '" + currentRow.find("td:eq(2)").text() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    propertynumber = msg.d.split('|')[0];

                    $('#txtItemCode').val(msg.d.split('|')[1]);
                    $('#txtPropertyNo').val(msg.d.split('|')[0]);
                    $('#txtSerialNo').val(msg.d.split('|')[2]);
                    $('#txtDescription').val(msg.d.split('|')[4]);
                    $('#txtExpiry').val(msg.d.split('|')[5].split(' ')[0]);
                    $('#txtWarranty').val(msg.d.split('|')[6].split(' ')[0] + ' - ' + msg.d.split('|')[7].split(' ')[0]);
                    $('#txtInsurance').val(msg.d.split('|')[8].split(' ')[0] + ' - ' + msg.d.split('|')[9].split(' ')[0]);
                    document.getElementById("txtSerialNo").style.borderColor = "";
                    $('#txtacquiredcost').val(msg.d.split('|')[10]);
                    $('#ddlCustodian').val(msg.d.split('|')[11]).trigger('change');

                    if (msg.d.split('|')[12] == "On Stock") {
                        document.getElementById("txtacquiredcost").disabled = false;
                        document.getElementById("ddlCustodian").disabled = false;
                    } else {
                        document.getElementById("txtacquiredcost").disabled = true;
                        document.getElementById("ddlCustodian").disabled = true;
                    }
                    $('#modalPropertyInfo').modal('show');
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

function format(d) {

    return '<table>' +
        '<tr><td>SUPPLIER:</td><td>' + d.SupplierName + '</td><td>&emsp;</td><td>SITE :</td><td>' + d.Site + '</td><td>&emsp;</td><td>DATE OF ACQUISITION :</td><td>' + d.DateOfAcquisition + '</td></tr>' +
        '<tr><td>PR #:</td><td>' + d.PRNumber + '</td><td>&emsp;</td><td>LOCAION :</td><td>' + d.Location + '</td><td>&emsp;</td><td>DATE OF MAINTENANCE :</td><td>' + d.DateOfMaintenance + '</td></tr>' +
        '<tr><td>PO #:</td><td>' + d.PONumber + '</td><td>&emsp;</td><td>AREA :</td><td>' + d.Area + '</td><td>&emsp;</td><td>STATUS :</td><td>' + d.Status + '</td></tr>' +
        '<tr><td>DR #:</td><td>' + d.DRNumber + '</td><td>&emsp;</td><td>ISSUED TO :</td><td>' + d.IssuedTo + '</td></tr>' +
        '<tr><td>INVOICE #:</td><td>' + d.InvoiceNumber + '</td><td>&emsp;</td><td>ISSUANCE # :</td><td>' + d.IssuanceNumber + '</td></tr>' +
        '<table>';
}

function UpdatePropertyInfo() {
    var serialno = ReplaceStr1($('#txtSerialNo').val().trim());
    var labeltoprint = "";
    var description = ReplaceStr1($('#txtDescription').val().trim());
    var warranty = $('#txtWarranty').val();
    var insurance = $('#txtInsurance').val();
    var expiry = $('#txtExpiry').val();
    var NewPrNr = $('#txtNewPropertyNo').val();
    var acquiredcost = $('#txtacquiredcost').val();
    var custodian = $('#ddlCustodian').val();


    var alertMsg;

    if ($('#txtSerialNo').val().trim() == '' || $('#txtDescription').val().trim() == '' || hasDuplicate == "true" || $('#ddlCustodian').val() == "0") {
        alertMsg = '';

        if ($('#txtSerialNo').val() == '')
            alertMsg += '\nSerial Number';
        if ($('#txtDescription').val() == '')
            alertMsg += '\nDescription';
        if (hasDuplicate == "true")
            alertMsg += '\nDuplicated Serial';
        if ($('#ddlCustodian').val() == '0')
            alertMsg += '\nCustodian';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Update Property?",
            text: "Are you sure, you want to update this property?",
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
                    url: "Custodian_Operation_Module/PropertyInformation.aspx/UpdatePropertyInformation",
                    data: " { propertynumber : '" + propertynumber + "' , serialnumber : '" + serialno + "', labeltoprint :'" + labeltoprint + "', longdescription:'" + description + "', warranty: '" + warranty + "', insurance: '" + insurance + "', expiry: '" + expiry + "', newprnr: '" + NewPrNr + "', acquiredcost: '" + acquiredcost + "', custodian: '" + custodian + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 1) {
                            swal("Successful", "Property Successfully Updated!", "success");
                            $("#modalPropertyInfo").modal("hide");
                            load_data();
                        }
                        else {
                            swal("WARNING", "Duplicate Property Number!", "warning");

                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function UpdateNewPropNum() {
    swal({
        title: "Update Current Property Number?",
        text: "Are you sure, you want to update the property number?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            swal("Success", "Property Number Successfully Updated!", "success");

            $("#modalEditPropertyNum").modal("hide");
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function EditPropertyNum(recID) {
    alert(recID);
    //alert('PROPERTY INFO');
    $("#modalEditPropertyNum").modal("show");

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
        url: "Custodian_Operation_Module/PropertyInformation.aspx/GetUserCustodian",
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


