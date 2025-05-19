var idForUpdate = 0;
var currentPage = 1;

var rfid = '';
var barcode = '';
var rfidcount = 0;
var barcodecount = 0;
$(document).ready(function () {
    $('.select2').select2();

    $("#tblStock").DataTable({
        "scrollX": true,
        dom: 'Brftip', fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
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
    //GetRFIDandBarcodeGeneration();
    load_data();

    //$('#addStockDialogBox').modal({ backdrop: 'static', keyboard: false })

});

function GetRFIDandBarcodeGeneration() {
    $.ajax({
        type: "POST",
        url: "Files_Data/Stock.aspx/GetRFIDandBarcodeGeneration",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            rfid = msg.d.split('|')[0];
            barcode = msg.d.split('|')[1];
            rfidcount = msg.d.split('|')[2];
            barcodecount = msg.d.split('|')[3];

            var date = new Date();
            var year = date.getFullYear();
            var pad = 'A' + year + '0000000';
            var barcodecnt = '' + (parseInt(barcodecount));
            var rfidcnt = '' + (parseInt(rfidcount));
            var rfidnumber = pad.substr(0, pad.length - rfidcnt.length) + rfidcnt;
            var barcodenumber = pad.substr(0, pad.length - barcodecnt.length) + barcodecnt;

            var tag = $('#ddlTag').val();
            //if ($('#ddlTag').val() == '--- Select Tag ---')
            //    tag = 0;
            var tagtype = 0;
            $.ajax({
                type: "POST",
                url: "Files_Data/Stock.aspx/GetTagByID",
                data: "{ tagid: '" + tag + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg1) {
                    tagtype = msg1.d.split('|')[1];
                    if (rfid == "Manual Input" && barcode == "Manual Input") {
                        if (tagtype == 1) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 4) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 2) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                    + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                    + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
                                    + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                        } else {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                    + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                    + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                    + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%'  disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                    } else if (rfid == "Manual Input" && barcode == "Auto generated") {
                        if (tagtype == 1) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled />"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 4) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 2) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                    + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                    + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
                                    + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        } else {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                    + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                    + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                    + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%'  disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                    } else if (rfid == "Auto generated" && barcode == "Manual Input") {
                        if (tagtype == 1) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 4) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled/>"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 2) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                    + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                    + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled />"
                                    + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        } else {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%'  disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                    } else {
                        if (tagtype == 1) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 4) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                     + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                     + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled/>"
                                     + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else if (tagtype == 2) {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                    + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                    + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled />"
                                    + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                        else {
                            var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                      + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                      + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
                                      + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
                                   + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
                                       + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%'  disabled/>"
                                   + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
                            $('#divrfid').html(html);
                            $('#divbarcode').html(html1);
                            $('#divrfid').show();
                            $('#divbarcode').show();
                        }
                    }



                    if (tagtype == 0) {
                        $('#divrfid').hide();
                        $('#divbarcode').hide();
                    }
                }
            });
            //var tagtype = $('#BodyHolder_ddlTagType').val();
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: "Files_Data/Stock.aspx/GetRFIDandBarcodeGeneration",
    //    data: " { } ",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        rfid = msg.d.split('|')[0];
    //        barcode = msg.d.split('|')[1];
    //        rfidcount = msg.d.split('|')[2];
    //        barcodecount = msg.d.split('|')[3];
    //        var date = new Date();
    //        var year = date.getFullYear();
    //        var pad = 'A' + year + '0000000';
    //        var barcodecnt = '' + (parseInt(barcodecount));
    //        var rfidcnt = '' + (parseInt(rfidcount));
    //        var rfidnumber = pad.substr(0, pad.length - rfidcnt.length) + rfidcnt;
    //        var barcodenumber = pad.substr(0, pad.length - barcodecnt.length) + barcodecnt;
    //        //var tagtype = $('#BodyHolder_ddlTagType').val();
    //        if (rfid == "Manual Input" && barcode == "Manual Input") {
    //            if (tagtype == 1) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else if (tagtype == 4) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                        + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                        + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
    //                        + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //        } else if (rfid == "Manual Input" && barcode == "Auto generated") {
    //            if (tagtype == 1) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled />"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else if (tagtype == 4) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                        + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                        + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' />"
    //                        + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //        } else if (rfid == "Auto generated" && barcode == "Manual Input") {
    //            if (tagtype == 1) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else if (tagtype == 4) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled/>"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                        + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                        + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled />"
    //                        + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' />"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //        } else {
    //            if (tagtype == 1) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' disabled />"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else if (tagtype == 4) {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                         + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                         + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled/>"
    //                         + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //            else {
    //                var html = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                        + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                        + "<input id='RFIDNumber' placeholder='RFID Number'  type='text' class='form-control' style='width: 100%' value='" + rfidnumber + "'  disabled />"
    //                        + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                var html1 = "<div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>"
    //                       + "<div class='col-lg-8 col-md-8 col-sm-10 col-xs-12'>"
    //                           + "<input id='Barcode' placeholder='Barcode' type='text' class='form-control' style='width: 100%' value='" + barcodenumber + "' disabled/>"
    //                       + "</div><div class='col-lg-2 col-md-2 col-sm-10 col-xs-12'></div>";
    //                $('#divrfid').html(html);
    //                $('#divbarcode').html(html1);
    //            }
    //        }
    //    }
    //});
}

function load_data() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Stock.aspx/GetStock",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#stock').hide();
            $('#stock').innerHTML = "";
            $('#stock').fadeIn(200);
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

function getImage(event) {
    console.log(event.target.files[0]);
    //$(".divIDClass").hide();
    $('#BodyHolder_StockImage').html("");
    var image = URL.createObjectURL(event.target.files[0]);
    var imagediv = document.getElementById('BodyHolder_StockImage');
    var newimg = document.createElement('img');
    newimg.src = image;
    newimg.width = "300";
    imagediv.appendChild(newimg);
    //var filename = $('#BodyHolder_FileUploadPropertyImage').val();
    ////alert(filename);
    //propertyimage = "<img src='" + filename + "' style='max-width: 100%; height:auto;'>";
    ////propertyimage = "<img src='D:/sad/santa.png' style='max-width: 100%; height:auto;'>";
    ////alert(propertyimage);
    //$('#BodyHolder_PropertyImage').html(propertyimage);
}

function populateDataTable(data) {
    try {
        $('#tblStock').DataTable({
            "destroy": true,
            "processing": true,
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
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
                { "data": "ItemCode" },
                { "data": "Description" },
                { "data": "Classification" },
                { "data": "Category" },
                { "data": "Brand" },
                { "data": "Model" },
                { "data": "TagType" },
                { "data": "IsReturnable" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply List'
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
        $('#tblStock').off('click');
    } catch (e) {
        var table = $('#tblStock').DataTable();
        table
            .clear()
            .draw();
    }
}

function addStockDialogBox() {
    Clear();
    document.getElementById("chkActive").checked = true;
    document.getElementById("chkIsReturnable").checked = false;
    $("#addStockDialogBox").modal("show");

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
                let elementsAddBrand = document.getElementsByClassName("btn_EditStock");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSetCompanyInformation',
                    class: 'btn btn-success btn_EditStock',
                    onclick: 'SaveStock(this)',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelStock').prepend($formBrand);
            }
        }
    });
}

function TagTypeChange() {
}

function SaveStock() {
    var itemcode = ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' '));
    var description = ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' '));
    var criticallevel = $('#txtCriticalLevel').val();
    if ($('#txtCriticalLevel').val() == "")
        criticallevel = 0;

    //var assetinventoryaccount = $('#BodyHolder_ddlAssetInventoryAccount').val();
    //if ($('#BodyHolder_ddlAssetInventoryAccount').val() == '--- Select Inventory Account ---' || $('#BodyHolder_ddlAssetInventoryAccount').val() == null)
    //    assetinventoryaccount = 0;
    var incomeaccount = $('#BodyHolder_ddlIncomeAccount').val();
    if ($('#BodyHolder_ddlIncomeAccount').val() == '--- Select Income Account ---' || $('#BodyHolder_ddlIncomeAccount').val() == null)
        incomeaccount = 0;
    var expenseaccount = $('#BodyHolder_ddlExpenseAccount').val();
    if ($('#BodyHolder_ddlExpenseAccount').val() == '--- Select Expense Account ---' || $('#BodyHolder_ddlExpenseAccount').val() == null)
        expenseaccount = 0;

    var stocklogo = $('#BodyHolder_FileUploadStockImage').val();
    var stocklogocontainer = "";

    if (stocklogo == "") {
        stocklogocontainer = "";
    }
    else {
        stocklogocontainer = itemcode + '.' + stocklogo.split('.')[1];
    }
    var alertMsg = "";

    var tag = $('#ddlTag').val();

    //if ($('#BodyHolder_ddlTagType').val() == '')
    //    tag = 0;
    var tagtype = 0;
    //if ($('#BodyHolder_ddlTagType').val() == '--- Select Tag ---') {
    //    swal("Warning ", "Select a Tag", "warning");
    //    ex.preventDefault();
    //}
    //else {
    if (ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' ')) == '' || $('#ddlClassification').val() == '0' || $('#ddlCategory').val() == '0' || $('#ddlBrand').val() == '0' || $('#ddlModel').val() == '0' || $('#ddlUnitOfMeasurement').val() == '0' || $('#ddlTag').val() == '0' || ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '' || criticallevel == '') {
        //alertMsg = 'Fill-out all the required field(s):';


        if (ReplaceStr1($('#BodyHolder_txtItemCode').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nItem Code';
        if (criticallevel == '')
            alertMsg += '\nMin supply Level';
        if ($('#ddlClassification').val() == '0')
            alertMsg += '\nClassification';

        if ($('#ddlCategory').val() == '0')
            alertMsg += '\nCategory';
        if ($('#ddlBrand').val() == '0')
            alertMsg += '\nBrand';
        if ($('#ddlModel').val() == '0')
            alertMsg += '\nModel';
        if ($('#ddlUnitOfMeasurement').val() == '0')
            alertMsg += '\nUnit Of Measurement';
        if (ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        if ($('#ddlTag').val() == '0')
            alertMsg += '\nTag';
        //alert(alertMsg);
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this supply?",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                var fileUpload = $("#BodyHolder_FileUploadStockImage").get(0);
                var files = fileUpload.files;
                var test = new FormData();
                for (var i = 0; i < files.length; i++) {
                    test.append(files[i].name, files[i]);
                }
                $.ajax({
                    url: "Files_Data/FilesData.ashx?transactionnumber=" + itemcode + "|Stock|" + stocklogocontainer,
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: test,
                    success: function (result) {
                        if (result.includes("File Uploaded Successfully", 0)) {
                            if (tag != 0) {
                                var rfidvalue = ReplaceStr1($('#RFIDNumber').val().trim().replace(/  +/g, ''));
                                var barcodevalue = ReplaceStr1($('#Barcode').val().trim().replace(/  +/g, ''));
                            }

                            $.ajax({
                                type: "POST",
                                url: "Files_Data/Stock.aspx/GetTagByID",
                                data: "{ tagid: '" + tag + "' }",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg1) {
                                    tagtype = msg1.d.split('|')[1];

                                    if (rfid == "Manual Input" && barcode == "Manual Input") {
                                        if (tagtype == 1) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        //alert("Stock Successfully Saved!");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        //alert("Stock Successfully Modified!");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        //alert("Stock Already Exist!");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else if (tagtype == 4) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else if (tagtype == 2) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '', barcodenumber: ''  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '', barcodenumber: ''  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                    } else if (rfid == "Manual Input" && barcode == "Auto generated") {
                                        if (tagtype == 1) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        //alert("Stock Successfully Saved!");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        //alert("Stock Successfully Modified!");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        //alert("Stock Already Exist!");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else if (tagtype == 4) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else if (tagtype == 2) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '', barcodenumber: ''  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '', barcodenumber: ''  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        //$.ajax({
                                        //    type: "POST",
                                        //    url: "Files_Data/Stock.aspx/SaveStock",
                                        //    data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag+ "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                        //    contentType: "application/json; charset=utf-8",
                                        //    dataType: "json",
                                        //    success: function (msg) {
                                        //        if (msg.d == 0) {
                                        //            load_data();
                                        //            $("#addStockDialogBox").modal("hide");
                                        //            swal("Successful", "Stock Successfully Saved!", "success");
                                        //            //alert("Stock Successfully Saved!");
                                        //            Clear();
                                        //        }
                                        //        else if (msg.d == 1) {
                                        //            load_data();
                                        //            $("#addStockDialogBox").modal("hide");
                                        //            swal("Successful", "Stock Successfully Modified!", "success");
                                        //            //alert("Stock Successfully Modified!");
                                        //            Clear();
                                        //        }
                                        //        else {
                                        //            load_data();
                                        //            $("#addStockDialogBox").modal("hide");
                                        //            swal("Error", "Stock Already Exist!", "error");
                                        //            //alert("Stock Already Exist!");
                                        //            Clear();
                                        //        }
                                        //    }
                                        //});
                                    } else if (rfid == "Auto generated" && barcode == "Manual Input") {
                                        $.ajax({
                                            type: "POST",
                                            url: "Files_Data/Stock.aspx/SaveStock",
                                            //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                            data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (msg) {
                                                if (msg.d == 0) {
                                                    load_data();
                                                    $("#addStockDialogBox").modal("hide");
                                                    swal("Successful", "Supply successfully saved!", "success");
                                                    Clear();
                                                }
                                                else if (msg.d == 1) {
                                                    load_data();
                                                    $("#addStockDialogBox").modal("hide");
                                                    swal("Successful", "Supply successfully modified!", "success");
                                                    Clear();
                                                }
                                                else {
                                                    load_data();
                                                    $("#addStockDialogBox").modal("hide");
                                                    swal("Error", "Supply already exist!", "error");
                                                    Clear();
                                                }
                                            }
                                        });
                                    }
                                    else {
                                        if (tagtype == 1) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else if (tagtype == 4) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else if (tagtype == 2) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "'  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        else {
                                            $.ajax({
                                                type: "POST",
                                                url: "Files_Data/Stock.aspx/SaveStock",
                                                //data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '', barcodenumber: ''  } ",
                                                data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag + "', rfidnumber: '', barcodenumber: ''  } ",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (msg) {
                                                    if (msg.d == 0) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully saved!", "success");
                                                        Clear();
                                                    }
                                                    else if (msg.d == 1) {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Successful", "Supply successfully modified!", "success");
                                                        Clear();
                                                    }
                                                    else {
                                                        load_data();
                                                        $("#addStockDialogBox").modal("hide");
                                                        swal("Error", "Supply already exist!", "error");
                                                        Clear();
                                                    }
                                                }
                                            });
                                        }
                                        //$.ajax({
                                        //    type: "POST",
                                        //    url: "Files_Data/Stock.aspx/SaveStock",
                                        //    data: " { id : '" + idForUpdate + "' ,itemcode : '" + itemcode + "', classificationid :'" + $('#ddlClassification').val() + "', categoryid: '" + $('#ddlCategory').val() + "', brandid: '" + $('#ddlBrand').val() + "', modelid :'" + $('#ddlModel').val() + "', uomid: '" + $('#ddlUnitOfMeasurement').val() + "', description: '" + description + "', active:'" + document.getElementById("chkActive").checked + "',isreturnable:'" + document.getElementById("chkIsReturnable").checked + "',assetinventoryaccount : '" + assetinventoryaccount + "', incomeaccount: '" + incomeaccount + "', expenseaccount : '" + expenseaccount + "', stockimage: '" + stocklogocontainer + "', criticallevel: '" + criticallevel + "', tagtype: '" + tag+ "', rfidnumber: '" + rfidvalue + "', barcodenumber: '" + barcodevalue + "' } ",
                                        //    contentType: "application/json; charset=utf-8",
                                        //    dataType: "json",
                                        //    success: function (msg) {
                                        //        if (msg.d == 0) {
                                        //            load_data();
                                        //            $("#addStockDialogBox").modal("hide");
                                        //            swal("Successful", "Stock Successfully Saved!", "success");
                                        //            Clear();
                                        //        }
                                        //        else if (msg.d == 1) {
                                        //            load_data();
                                        //            $("#addStockDialogBox").modal("hide");
                                        //            swal("Successful", "Stock Successfully Modified!", "success");
                                        //            Clear();
                                        //        }
                                        //        else {
                                        //            load_data();
                                        //            $("#addStockDialogBox").modal("hide");
                                        //            swal("Error", "Stock Already Exist!", "error");
                                        //            Clear();
                                        //        }
                                        //    }
                                        //});
                                    }
                                }
                            });
                        } else {
                            swal("Warning", result, "warning");
                        }
                    }
                });
            }
        });
    }
}

function Clear() {
    $('#ddlTag').val("0").trigger('change');
    $('#txtCriticalLevel').val("");
    $('#BodyHolder_FileUploadStockImage').val("");
    $('#ddlClassification').val("0").trigger('change');
    $('#ddlCategory').val("0").trigger('change');
    $('#ddlBrand').val("0").trigger('change');
    $('#ddlModel').val("0").trigger('change');
    $('#ddlUnitOfMeasurement').val("0").trigger('change');
    $('#txtDescription').val(""); $('#BodyHolder_txtItemCode').val("");

    $('#BodyHolder_StockImage').html("");
    $('#BodyHolder_ddlAssetInventoryAccount').val("--- Select Inventory Account ---").trigger('change');
    $('#BodyHolder_ddlIncomeAccount').val("--- Select Income Account ---").trigger('change');
    $('#BodyHolder_ddlExpenseAccount').val("--- Select Expense Account ---").trigger('change');
    idForUpdate = 0;
    CountChar($('#txtDescription').val(), 500, 'cnt', 'txtDescription');
}

function GetStockID(stockid) {
    idForUpdate = stockid;

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
                let elementsAddCat = document.getElementsByClassName("btn_EditStock");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/Stock.aspx/StockByID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_txtItemCode').val(msg.d.split('|')[1]);
            $('#txtDescription').val(msg.d.split('|')[2]);
            $('#ddlClassification').val(msg.d.split('|')[3]).trigger('change');
            $('#ddlCategory').val(msg.d.split('|')[4]).trigger('change');
            $('#ddlBrand').val(msg.d.split('|')[5]).trigger('change');
            $('#ddlModel').val(msg.d.split('|')[6]).trigger('change');
            $('#ddlUnitOfMeasurement').val(msg.d.split('|')[7]).trigger('change');
            if (msg.d.split('|')[8] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
            if (msg.d.split('|')[9] == 'True') {
                document.getElementById("chkIsReturnable").checked = true;
            }
            else {
                document.getElementById("chkIsReturnable").checked = false;
            }

            var stockimage = "";
            if (msg.d.split('|')[13] == "") {
                stockimage = "<img src='../Pictures/noimage.png' style='max-width: 100%; height:auto;'>";
            }
            else {
                stockimage = "<img src='../Image/StockImage/" + msg.d.split('|')[1] + "/" + msg.d.split('|')[13] + "' style='max-width: 100%; height:auto;'>";
            }
            $('#BodyHolder_StockImage').html(stockimage);

            $('#BodyHolder_PropertyImage').html("");
            if (msg.d.split('|')[10] == "0" || msg.d.split('|')[10] == "") {
                $('#BodyHolder_ddlAssetInventoryAccount').val("--- Select Inventory Account ---").trigger('change');
            }
            else {
                $('#BodyHolder_ddlAssetInventoryAccount').val(msg.d.split('|')[10]).trigger('change');
            }

            if (msg.d.split('|')[11] == "0" || msg.d.split('|')[11] == "") {
                $('#BodyHolder_ddlIncomeAccount').val("--- Select Income Account ---").trigger('change');
            }
            else {
                $('#BodyHolder_ddlIncomeAccount').val(msg.d.split('|')[11]).trigger('change');
            }
            if (msg.d.split('|')[12] == "0" || msg.d.split('|')[12] == "") {
                $('#BodyHolder_ddlExpenseAccount').val("--- Select Expense Account ---").trigger('change');
            }
            else {
                $('#BodyHolder_ddlExpenseAccount').val(msg.d.split('|')[12]).trigger('change');
            }

            $('#txtCriticalLevel').val(msg.d.split('|')[14]);
            if (msg.d.split('|')[15] == "0") {
                $('#ddlTag').val("0").trigger('change');
            }
            else {
                $('#ddlTag').val(msg.d.split('|')[15]).trigger('change');
            }
            $('#RFIDNumber').val(msg.d.split('|')[16]);
            $('#Barcode').val(msg.d.split('|')[17]);
            $("#addStockDialogBox").modal("show");
        }
    });
}

function deleteStock(id) {
    idForUpdate = id;
    //$("#deleteStock").modal("show");
    swal({
        title: "Delete Supply",
        text: "Are you sure you want to delete this supply?",
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
                url: "Files_Data/Stock.aspx/DeleteStock",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Supply Successfully Deleted!", "success");
                        Clear();
                        load_data();
                    }
                    else {
                        swal("Error", "Supply cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteStock() {
    $("#deleteStock").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Stock.aspx/DeleteStock",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Supply Successfully Deleted!');
                Clear();
                load_data();
            }
            else {
                alert('Supply cannot be Deleted, it is being actively used!');
            }
        }
    });
}