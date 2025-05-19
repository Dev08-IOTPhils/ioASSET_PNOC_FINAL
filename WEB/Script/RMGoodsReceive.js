var idForUpdate = 0;

$(document).ready(function () {
    load_data_GRH();

});

function load_data_GRH() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetGoodsReceiveHeader",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#goodsreceive').hide();
            $('#goodsreceive').innerHTML = "";
            $('#goodsreceive').fadeIn(200);
            myJsonData = data;
            populateDataTableGRH(myJsonData);
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

function populateDataTableGRH(data) {
    try {
        $('#tblGoodsReceive').DataTable({
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
                { "data": "GoodsReceiveNumber" },
                { "data": "Receiver" },
                { "data": "CreatedBy" },
                //{ "data": "NoOfItem" },
                { "data": "CreateDate" },
                { "data": "Approver" },
                { "data": "Remarks" },
                { "data": "Status" },
                { "data": "SupportingDocument" },
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
        $('#tblGoodsReceive').off('click');
    } catch (e) {
        var table = $('#tblGoodsReceive').DataTable();
        table
            .clear()
            .draw();
    }
}

function viewGRDetails(id) {
    idForUpdate = id;
    load_data_GRD(id);
    $("#viewGRDetails").modal("show");
}

function load_data_GRD(id) {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetGoodsReceiveDetails",
        data: "{goodsreceiveid: '" + id + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#grdetails').hide();
            $('#grdetails').innerHTML = "";
            $('#grdetails').fadeIn(200);
            myJsonData = data;
            populateDataTableGRD(myJsonData);
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

function populateDataTableGRD(data) {
    try {
        console.log(data.d);
        $('#tblGRDetails').DataTable({
            "destroy": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "AssetType" },
                { "data": "ItemCode" },
                { "data": "SerialNo" },
                { "data": "Description" },
                { "data": "PRNumber" },
                { "data": "PONumber" },
                { "data": "InvoiceNumber" },
                { "data": "DRNumber" },
                { "data": "Remarks" },
                { "data": "QuantityString" },
                { "data": "UnitCostString" },
                { "data": "AcquiredCostString" },
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
        $('#tblGRDetails').off('click');
    } catch (e) {
        var table = $('#tblGRDetails').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveGoodsReceive() {
    //if (confirm("Are you sure you want to CONFIRM this Goods Receive?") == false) {
    //    return false;
    //}
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
                url: "Home/RequestManagement.aspx/ApproveGoodsReceive",
                data: "{goodsreceiveid:'" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "okay") {
                        $("#viewGRDetails").modal("hide");
                        load_data_GRH();
                        //alert('Goods Received successfully APPROVED.');
                        swal("Success", "Goods Received successfully APPROVED.", "success");
                    }
                    else {
                        $("#viewGRDetails").modal("hide");
                        load_data_GRH();
                        //alert(' Goods Received successfully APPROVED. \n\nMail not Sent. Please check Mail Setting.');
                        swal("Success", "Goods Received successfully APPROVED. \n\nMail not Sent. Please check Mail Setting.", "success");
                    }
                    $(".se-pre-con").hide();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeclineGoodsReceive() {
    //if (confirm("Are you sure you want to DECLINE this Goods Receive?") == false) {
    //    return false;
    //}
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
    textareatt.id = "declinegoodsreceivetextall";

    swal({
        title: "Are you sure you want to DECLINE? ",
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

        var textremarks = $('#declinegoodsreceivetextall').val();

        if (textremarks === "") {
            swal("Warning", "Add cancel reason to DECLINE this transaction.", "warning");
            return false
        }

        textremarks = ReplaceStr1(textremarks);
        $(".se-pre-con").fadeIn();
        $.ajax({
            type: "POST",
            url: "Home/RequestManagement.aspx/DeclineGoodsReceive",
            data: "{goodsreceiveid:'" + idForUpdate + "',remarks:'" + textremarks + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "okay") {
                    $("#viewGRDetails").modal("hide");
                    load_data_GRH();
                    //alert('Goods Received successfully DECLINED.');
                    swal("Success", "Goods Received successfully DECLINED.", "success");
                }
                else {
                    $("#viewGRDetails").modal("hide");
                    load_data_GRH();
                    //alert(' Goods Received successfully DECLINED. \n\nMail not Sent. Please check Mail Setting.');
                    swal("Success", "Goods Received successfully DECLINED. \n\nMail not Sent. Please check Mail Setting.", "success");
                }
                $(".se-pre-con").hide();
                $("#viewGRDetails").modal("hide");
            }
        });
    });
}

function RemoveRMGoodsReceive(detailsid, grpropertyid, goodsreceivenumber) {
    $.ajax({
        type: "POST",
        url: "Home/RequestManagement.aspx/RemoveRMGoodsReceive",
        data: " { detailsid : '" + detailsid + "', grpropertyid : '" + grpropertyid + "', goodsreceivenumber : '" + goodsreceivenumber + "'  } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log("successfully removed from list");
            if (msg.d == 0) {
                load_data_GRD(idForUpdate);
                load_data_GRH();
            }
            else {
                load_data_GRD(idForUpdate);
                load_data_GRH();

            }

        }
    });
}