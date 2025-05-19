$(document).ready(function () {
    $("#tblStock").DataTable({
        "scrollX": true
    });
    load_data_stock();

    $("#tblStock").DataTable().clear().draw();

});

function load_data_stock() {
    $.ajax({
        type: 'POST',
        url: "PropertyTagging.aspx/StockAvailableForTagging",
        data: "{}",
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

            GetCountStock();
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
        $('#tblStock').DataTable({
            "destroy": true,
            "processing": true,
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Tagging'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Tagging'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Tagging'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Supply Tagging'
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
        $('#tblStock').on('click', 'button.btn_Add', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            currentRow.remove();
        });
    } catch (e) {
        var table = $('#tblStock').DataTable();
        table
            .clear()
            .draw();
    }
}


function RemoveToPrintStock(assetid) {
    $.ajax({
        type: "POST",
        url: "PropertyTagging.aspx/RemoveToPrintStock",
        data: "{recid: '" + assetid + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            GetCount();
        }
    });
}

function RemoveAllToPrint() {
    if (confirm("Cancelling this transaction will remove all PROPERTIES in the List.\n\nAre you sure you want to cancel?") == false) {
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: "PropertyTagging.aspx/RemoveAllToPrintStock",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                load_data_stock();
            }
        });
    }
}

function GetCountStock() {
    var TaggingType = $('#BodyHolder_ddlTaggingOption').val();
    if (TaggingType == 'Tagging') {
        TaggingType = 1;
    }
    else if (TaggingType == 'Re-tagging') {
        TaggingType = 2;
    }
    else {
        TaggingType = 0;
    }

    $.ajax({
        type: "POST",
        url: "PropertyTagging.aspx/GetCountStock",
        data: "{ TaggingType : '" + TaggingType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#btnCart span").text(" View List for Tagging/Printing: " + msg.d);
            if (msg.d == 0) {
                $('#PrintList').modal('hide');
            }
            else { }
        }
    });
}