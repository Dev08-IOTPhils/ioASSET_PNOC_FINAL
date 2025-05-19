$(document).ready(function () {
    $('.select2').select2();

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateTo').datepicker('setDate', today);


    $("#tblRFIDLogs").DataTable({

        "scrollX": true,
        dom: 'Brtip',
        buttons: {
            buttons: [
                    'copy',
                    'excel',
                    'csv',
                    'pdf',
                    'print',
                    'colvis'
            ]
        }

    });

    $("#tblRFIDLogs").DataTable().clear().draw();
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $($.fn.dataTable.tables(true)).DataTable()
           .columns.adjust();
    });


    LoadRFIDLogs();
})

function LoadRFIDLogs()
{
    var datefrom = $('#txtDateFrom').val();
    var dateto = $('#txtDateTo').val();


    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "RFIDLogsView.aspx/GetRFIDLOGS",
        data: "{datefrom : '" + datefrom + "', dateto: '" + dateto + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            myJsonData = data;
            Load_DataTable(myJsonData);

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

function Load_DataTable(data)
{

    try {

        $('#tblRFIDLogs').DataTable({
            "destroy": true,
            "processing": true,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "DateTime" },
                { "data": "DeviceName" },
                { "data": "IPAddress" },
                { "data": "COMPort" },
                { "data": "EPC" },
                { "data": "ItemCode" },
                { "data": "PropertyNumner" },
                { "data": "SerialNumber" },
                { "data": "Description" },
                { "data": "Site" },
                { "data": "Location" },
                { "data": "Area" },
                { "data": "IssuedTo" },
                { "data": "Status" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RFID Logs View'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RFID Logs View'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RFID Logs View'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'RFID Logs View'
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

        $('#tblRFIDLogs').off('click');
    } catch (e) {
        var table = $('#tblRFIDLogs').DataTable();
        table
            .clear()
            .draw();
    }


}