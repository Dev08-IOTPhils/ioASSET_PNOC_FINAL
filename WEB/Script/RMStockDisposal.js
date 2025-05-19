var idForUpdate = 0;

$(document).ready(function () {
    load_data_StockDisposal();
});

function load_data_StockDisposal() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetSDPendingApproval",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockdisposal').hide();
            $('#stockdisposal').innerHTML = "";
            $('#stockdisposal').fadeIn(200);

            myJsonData = data;
            populateDataStockDisposal(myJsonData);

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

function populateDataStockDisposal(data) {
    try {
        $('#tblStockDisposal').DataTable({
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
                { "data": "Approver" },
                { "data": "ModeOfDisposal" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
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

        $('#tblStockDisposal').off('click');

        $('#tblStockDisposal').on('click', 'a.btn_View', function (e) {
            e.preventDefault();
        });

        $('#tblStockDisposal').on('click', 'a.btn_Cancel', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblStockDisposal').DataTable();
        table
            .clear()
            .draw();
    }
}

var disposalnumber = '';
var transactorName = '';
var transactorEmail = '';
function ViewStockDisposalDetails(disposalno, transactorname, createddate, email) {
    disposalnumber = disposalno;
    transactorName = transactorname;
    transactorEmail = email;
    document.getElementById('lblStockDisposalNo').innerHTML = disposalno;
    document.getElementById('lblStockDisposedBy').innerHTML = transactorname
    document.getElementById('lblStockDateDisposed').innerHTML = createddate;

    $('#BodyHolder_txtStockDisposalNo').val(disposalno);

    load_data_SDLDetails();
    $('#viewStockDisposalDetails').modal('show');
}

function load_data_SDLDetails() {
    //alert(1);
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetSDLPendingUpload",
        data: "{disposalno: '" + disposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockdisposaldetails').hide();
            $('#stockdisposaldetails').innerHTML = "";
            $('#stockdisposaldetails').fadeIn(200);
           
            myJsonData = data;

            populateDataSDLDetailstoo(data);

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

function populateDataSDLDetailstoo(data) {
    try {
        //alert('populateDataSDLDetails');

        $('#tblStockDisposalDetails').DataTable({
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "QuantityString" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Disposal'
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

        $('#tblStockDisposalDetails').off('click');
    } catch (e) {
        var table = $('#tblStockDisposalDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveStockDisposal1() {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to APPROVE this transaction?",
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
                url: "Home/RequestManagement.aspx/ApproveStockDisposal1",
                data: "{disposalno:'" + disposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_StockDisposal();
                    load_data_SDLDetails();
                    $('#viewStockDisposalDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully APPROVED.", "success");
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineStockDisposal() {
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
    textareatt.id = "declinestockdisposaltextall";

    swal({
        title: "Are you sure you want to DECLINE?",
        text: "Please add remarks on declining this transaction.",
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
    }).then((value) => {
        if (value === null) return false;

        var textremarks = $('#declinestockdisposaltextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }
        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclineStockDisposal",
            data: "{disposalno:'" + disposalnumber + "',remarkS:'" + textremarks + "',transactoremail:'" + transactorEmail + "',name:'" + transactorName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_StockDisposal();
                    $('#viewStockDisposalDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_StockDisposal();
                    $('#viewStockDisposalDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_StockDisposal();
                    $('#viewStockDisposalDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully DECLINED.\n\nMail not Sent. Inspector has no email.", "success");
                }

                $(".se-pre-con").hide();
            }
        });
    });
}