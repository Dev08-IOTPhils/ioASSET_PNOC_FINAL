var idForUpdate = 0;

$(document).ready(function () {
    load_data_Comp();
});

function load_data_Comp() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetComponentHeader",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#goodsreceive').hide();
            $('#goodsreceive').innerHTML = "";
            $('#goodsreceive').fadeIn(200);
            myJsonData = data;
            populateDataTableSA(myJsonData);
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

function populateDataTableSA(data) {
    try {
        $('#tblStockAllocation').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "StockAllocationNo" },
                { "data": "TransactorName" },
                { "data": "FromSiteStr" },
                //{ "data": "NoOfItem" },
                { "data": "ToSiteStr" },
                { "data": "Status" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Goods Receive'
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
        $('#tblStockAllocation').off('click');
    } catch (e) {
        var table = $('#tblStockAllocation').DataTable();
        table
            .clear()
            .draw();
    }
}

var stockallocationno = '';
var transactorName = '';
var transactorEmail = '';
function ViewStockAllocationDetails(stockallocationno, transactorname, createddate, email) {
    //alert('1');
    disposalnumber = stockallocationno;
    transactorName = transactorname;
    transactorEmail = email;
    document.getElementById('lblStockAllocationNo').innerHTML = stockallocationno;
    document.getElementById('lblStockAllocationBy').innerHTML = transactorname
    document.getElementById('lblStockDateAllocation').innerHTML = createddate;

    $('#BodyHolder_txtStockDisposalNo').val(stockallocationno);

    load_data_SALDetails();
    $('#viewStockAllocationDetails').modal('show');
}

function load_data_SALDetails() {
    //alert('2');
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetSALPendingUpload",
        data: "{disposalno: '" + disposalnumber + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stockallocationdetails').hide();
            $('#stockallocationdetails').innerHTML = "";
            $('#stockallocationdetails').fadeIn(200);

            myJsonData = data;

            populateDataSDLDetails(myJsonData);

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

function populateDataSDLDetails(data) {
    try {
        $('#tblStockAllocationDetails').DataTable({
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
                { "data": "StockAllocationNo" },
                { "data": "Remarks" },
                { "data": "FromSiteStr" },
                { "data": "ToSiteStr" },
                { "data": "Status" },
                { "data": "QuantityString" },
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

        $('#tblStockAllocationDetails').off('click');
    } catch (e) {
        var table = $('#tblStockAllocationDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveStockDisposal2() {
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
                url: "Home/RequestManagement.aspx/ApproveStockAllocation2",
                data: "{disposalno:'" + disposalnumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_SA();
                    $('#viewStockAllocationDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully APPROVED.", "success");
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineStockAllocationSup() {

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
            url: "Home/RequestManagement.aspx/DeclineStockAllocation",
            data: "{disposalno:'" + disposalnumber + "',remarkS:'" + textremarks + "',transactoremail:'" + transactorEmail + "',name:'" + transactorName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    load_data_SA();
                    $('#viewStockAllocationDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully DECLINED.", "success");
                }
                else if (msg.d == "sorry") {
                    load_data_SA();
                    $('#viewStockAllocationDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully DECLINED.\n\nMail not Sent. Please check Mail Setting.", "success");
                }
                else {
                    load_data_SA();
                    $('#viewStockAllocationDetails').modal('hide');
                    swal("Success", "Supply Disposal successfully DECLINED.\n\nMail not Sent. Inspector has no email.", "success");
                }

                $(".se-pre-con").hide();
            }
        });
    });
}