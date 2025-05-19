var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblBrandList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
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
    load_dataBrand();

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $('#addBrandDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function load_dataBrand() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Brand.aspx/GetBrand",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#brandlist').hide();
            $('#brandlist').innerHTML = "";
            $('#brandlist').fadeIn(200);

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
        $('#tblBrandList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            //},

            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "BrandName" },
                { "data": "Description" },
                { "data": "IsActive" },
                { "data": "Action" }
            ],
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Brand List'
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

        $('#tblBrandList').off('click');
    } catch (e) {
        var table = $('#tblBrandList').DataTable();
        table
            .clear()
            .draw();
    }
}


function GetBrandID(id) {
    idForUpdate = id;
    var successHeader = '<span class="fa fa-check-circle fa-2x" '
           + 'style="vertical-align:middle;color:#08e001;">'
           + '</span> Brand';
    $("#addBrandDialogBox").modal("show");

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
                let elementsAddCat = document.getElementsByClassName("btn_EditBrand");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/Brand.aspx/BrandbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtBrandName').val(msg.d.split('|')[1]);
            $('#txtDescriptionBrand').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActiveBrand").checked = true;
            }
            else {
                document.getElementById("chkActiveBrand").checked = false;
            }
        }
    });
}

function deleteBrand(id) {
    swal({
        title: "Delete Brand",
        text: "Are you sure you want to delete this brand?",
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
                url: "Files_Data/Brand.aspx/DeleteBrand",
                data: "{ id: '" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        //alert('Brand Successfully Deleted!');
                        swal("Successful", "Brand Successfully Deleted!", "success");
                        Clear();
                        load_dataBrand();
                    }
                    else {
                        //alert('Brand cannot be Deleted, it is being actively used!');
                        swal("Error", "Brand cannot be Deleted, it is being actively used!", "warning");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });

    //idForUpdate = id;
    //$("#deleteBrand").modal("show");

    //idForUpdate = id;
    //alertify.confirm('Are you sure you want to delete this Brand?',
    //       function (e) {
    //           if (e) {
    //               $.ajax({
    //                   type: "POST",
    //                   url: "Files_Data/Brand.aspx/DeleteBrand",
    //                   data: "{ id: '" + idForUpdate + "'}",
    //                   contentType: "application/json; charset=utf-8",
    //                   dataType: "json",
    //                   success: function (msg) {
    //                       //alert('Brand Successfully Deleted!');
    //                       alertify.alert('Brand successfully deleted!').setHeader(successHeader);
    //                       Clear();
    //                       load_dataBrand();
    //                   }
    //               });
    //           }
    //           else {
    //               alertify.alert('y');
    //           }
    //       }).set({ transitionOff: true });
}

function DeleteBrand() {
    $("#deleteBrand").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Brand.aspx/DeleteBrand",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Brand Successfully Deleted!');
                Clear();
                load_dataBrand();
            }
            else {
                alert('Brand cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtBrandName').val("");
    $('#txtDescriptionBrand').val("");
    document.getElementById("chkActiveBrand").checked = true;
    idForUpdate = 0;
    CountChar($('#txtDescriptionBrand').val(), 500, 'cntBrand', 'txtDescriptionBrand');
}