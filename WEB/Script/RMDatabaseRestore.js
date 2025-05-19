var idForUpdate = 0;

$(document).ready(function () {
    load_data_DR();
});

function load_data_DR() {
    $.ajax({
        type: 'POST',
        url: "Home/RequestManagement.aspx/GetDatabaseRestore",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#databaserestore').hide();
            $('#databaserestore').innerHTML = "";
            $('#databaserestore').fadeIn(200);
            myJsonData = data;
            populateDataTableDR(myJsonData);
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

function populateDataTableDR(data) {
    try {
        $('#tblDatabaseRestore').DataTable({
            "destroy": true,
            "processing": true,
            //"language": {
            //processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "RestoreName" },
                { "data": "RestoreRequestor" },
                { "data": "RequestedDate" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Database Restore'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Database Restore'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Database Restore'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Database Restore'
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
        $('#tblDatabaseRestore').off('click');
    } catch (e) {
        var table = $('#tblDatabaseRestore').DataTable();
        table
            .clear()
            .draw();
    }
}

function ApproveDatabaseRestore(bakfilename) {
    $('#BodyHolder_txtBakFileName').val(bakfilename);

    $('#RestoreDB').modal('show');
}

function DeclineDatabaseRestore(id) {
    //if (confirm("Are you sure you want to DECLINE this Database Restore?") == false) {
    //    return false;
    //}
    swal({
        title: "Are you sure you want to DECLINE this Database Restoration?",
        text: "Please add remarks on declining this transaction.",
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
                url: "Home/RequestManagement.aspx/DeclineDatabaseRestore",
                data: "{recordid:'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //alert('Database Restore successfully DECLINED.');
                    swal("Success", "Database Restore successfully DECLINED.", "success");
                    load_data_DR();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}