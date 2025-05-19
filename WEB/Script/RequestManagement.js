var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $("#tblGoodsReceive").DataTable({
        "scrollX": true, dom: 'Brftip',
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblPropertyIssuance").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Issuance'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblPropertyRequest").DataTable({
        "scrollX": true,
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Request'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblPropertyTransfer").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Transfer'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblPropertyDisposal").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Property Disposal'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblPropertyInspectionDisposal").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblPropertyCertificationDisposal").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    $("#tblPendingStockMerge").DataTable({
        "scrollX": true
       , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
    $("#tblDatabaseRestore").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });
    $("#tblDayPass").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    $('#tblPendingRequestExtension').DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    $("#tblStockIssuance").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Issuance'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    $("#tblStockRequest").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management - Supply Request'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    $("#tblStockDisposal").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
        , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    $("#tblStockAllocation").DataTable({
        "scrollX": true, dom: 'Brftip',
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
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Request Management'
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
    , 'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' }
    });

    AddandRemoveClass();
});

function AddandRemoveClass() {
    var approvaltype = window.location.href.split('=')[1];
    var grtab = document.getElementById("caTab");
    var grtab1 = document.getElementById("tab_1");

    //var satab = document.getElementById("saTab");
    //var satab1 = document.getElementById("tab_2");

    var airtab = document.getElementById("airTab");
    var airtab1 = document.getElementById("tab_3");

    var atrtab = document.getElementById("atrTab");
    var atrtab1 = document.getElementById("tab_4");

    var adrtab = document.getElementById("adrTab");
    var adrtab1 = document.getElementById("tab_6");

    var gprtab = document.getElementById("gprTab");
    var gprtab1 = document.getElementById("tab_7");

    var ufrtab = document.getElementById("ufrTab");
    var ufrtab1 = document.getElementById("tab_8");

    //var rdrtab = document.getElementById("rdrTab");
    //var rdrtab1 = document.getElementById("tab_9");

    var sirTab = document.getElementById("sirTab");
    var sirTab1 = document.getElementById("tab_10");

    var sdrTab = document.getElementById("sdrTab");
    var sdrTab1 = document.getElementById("tab_11");


    var sarTab = document.getElementById("sarTab");
    var sarTab1 = document.getElementById("tab_12");

    if (approvaltype == "1") { //goods receive
        grtab.classList.add("active");
        grtab1.classList.add("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "2") { //property issuance
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.add("active");
        airtab1.classList.add("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");
        
        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "3") { // property request
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.add("active");
        airtab1.classList.add("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "4") { // property transfer
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.add("active");
        atrtab1.classList.add("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "5") { //property dispsoal
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.add("active");
        adrtab1.classList.add("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "6") { //gate pass
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.add("active");
        gprtab1.classList.add("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "7") { //day pass
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.add("active");
        gprtab1.classList.add("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "8") { //uploaded file
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.add("active");
        ufrtab1.classList.add("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "9") { //restore database
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.add("active");
        //rdrtab1.classList.add("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "10") { //stock merge
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.add("active");
        //satab1.classList.add("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "11") { // return date extension
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.add("active");
        airtab1.classList.add("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "12") { // stock issuance
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.add("active");
        sirTab1.classList.add("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else if (approvaltype == "13") { // stock disposal
        grtab.classList.remove("active");
        grtab1.classList.remove("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.add("active");
        sdrTab1.classList.add("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
    else {
        grtab.classList.add("active");
        grtab1.classList.add("active");

        //satab.classList.remove("active");
        //satab1.classList.remove("active");

        airtab.classList.remove("active");
        airtab1.classList.remove("active");

        atrtab.classList.remove("active");
        atrtab1.classList.remove("active");

        adrtab.classList.remove("active");
        adrtab1.classList.remove("active");

        gprtab.classList.remove("active");
        gprtab1.classList.remove("active");

        ufrtab.classList.remove("active");
        ufrtab1.classList.remove("active");

        //rdrtab.classList.remove("active");
        //rdrtab1.classList.remove("active");

        sirTab.classList.remove("active");
        sirTab1.classList.remove("active");

        sdrTab.classList.remove("active");
        sdrTab1.classList.remove("active");

        sarTab.classList.remove("active");
        sarTab1.classList.remove("active");
    }
}