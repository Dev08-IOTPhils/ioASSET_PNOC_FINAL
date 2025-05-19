$(document).ready(function () {
    $("#tblAssetList").DataTable({
        "language": {
            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
    , searchPlaceholder: "Search\t\t\t     🔍︎", search: ""
        },
        "scrollX": true,
        dom: 'Brftip',
        fixedHeader: {
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Acknowledgement'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Acknowledgement'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Acknowledgement'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Property Acknowledgement'
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

    $("#tblAssetList").DataTable().clear().draw();

    //alert("PropertyAcknowledge"); GetList
    Load_data();
});



function Load_data() {
    getsystemSettings();
    getAcknowledgementList();
}

var AckownledgementMode = '';

function getsystemSettings() {
    $.ajax({
        type: "POST",
        url: "Operation_Module/PropertyAcknowledgement.aspx/GetSystemSetting",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            AckownledgementMode = msg.d.split('|')[20];
        }
    })
}


function getAcknowledgementList() {
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: "Operation_Module/PropertyAcknowledgement.aspx/GetList",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#assetlist').hide();
            $('#assetlist').innerHTML = "";
            $('#assetlist').fadeIn(200);

            myJsonData = data;
            if (AckownledgementMode == 'Web Manual Acknowledgement') {
                populateDataTable(myJsonData, true);
            }
            else {
                populateDataTable(myJsonData, false);
            }



            //console.log('GetMyItems');
            //console.log(data);
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


function populateDataTable(data, vis) {
    try {
        $('#tblAssetList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
                , searchPlaceholder: "Search\t\t\t     🔍︎", search: ""
            },
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                //{ "data": null, defaultContent: '' },
                { "data": "ItemCode" },
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                //{ "data": "Custodian" },
                { "data": "ClassificationName" },
                { "data": "CategoryName" },
                { "data": "BrandName" },
                { "data": "ModelName" },
                { "data": "Site" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Location List'
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

        $('#tblAssetList').on('click', 'button.btn_Acknowledge', function (e) {
            e.preventDefault();
            var currentRow = $(this).closest("tr");
            var pmlid = $(this).attr('pmlid');
            currentRow.remove();

            $.ajax({
                type: "POST",
                url: "Operation_Module/PropertyAcknowledgement.aspx/AcknowledgeProperty",
                data: "{propertynumber: '" + currentRow.find("td:eq(2)").text() + "',pmlid: '" + pmlid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    swal("Successful", "Property acknowledged successfully", "success");
                    //load_data();
                }
            });

            console.log(currentRow.find("td:eq(2)").text());

        });

    } catch (e) {
        var table = $('#tblAssetList').DataTable();
        table
            .clear()
            .draw();
    }
}

function btnClick(recid) {
    alert(recid);
    console.log(recid);
}