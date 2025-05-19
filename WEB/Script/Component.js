var idForUpdate = 0;
var PropertyId = 0;
var TransID = 0;
var currentPage = 1;
var url = '';
var precId = "0";
var ppnum = "";
var pserial = "";
var pdesc = "";
var pcat = "";
var pclassi = "";
var tb;
var ca;

$(document).ready(function () {
    var context = window.location.pathname.substring(1);
    url = context.split('/')[0];

    document.getElementById("btnAddComp").style.display = "none";

    GetTag();
    
    tb = $("#tblPropertyAdded").DataTable({
        "scrollX": true, "paging": false,
        dom: 'Brftip',
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
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
    });
    ca = $("#tblCompAdded").DataTable({
        "scrollX": true, select: true,
        "sScrollX": "100%",
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
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
        },
        "scrollY": true,
        "sScrollY": "100%",
        //fixedColumns: {
        //    leftColumns: 2
        //}
    });

    $("#tblSelectProperty").DataTable({
        "scrollX": true, select: true,
        "sScrollX": "100%",
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
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
            , stateSave: true,
        "scrollY": true,
        "sScrollY": "100%",
        //fixedColumns: {
        //    leftColumns: 2
        //}
    });

    $("#tblCompEntry").DataTable({
        "scrollX": true, select: true,
        "sScrollX": "100%",
        fixedHeader: {
            header: true,
            footer: true,
            headerOffset: 50
        },
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
           , stateSave: true,
        "scrollY": true,
        "sScrollY": "100%",
        //fixedColumns: {
        //    leftColumns: 2
        //}
    });


    var ptempWIPH = "";
    var ptable = $('#tblSelectProperty').DataTable();
    $('#tblSelectProperty tbody').on('click', 'tr', function () {
        var data = ptable.row(this).data();
        var currentRow = $(this).closest("tr");
        var currentcolumn = currentRow.find('td');
        var currentValue = currentcolumn.find('p');


        if (ptempWIPH == currentValue.attr('precid')) {
            precId = '0';
            ptempWIPH = "";

        } else if (currentValue.attr('precid') == null) {
            precId = '0';
            ptempWIPH = "";
        }
        else {
            ptempWIPH = currentValue.attr('precid');
            precId = currentValue.attr('precid');
            ppnum = currentValue.attr('ppnum');
            pserial = currentValue.attr('pserial');
            pdesc = currentValue.attr('pdesc');
            pcat = currentValue.attr('pcat');
            pclassi = currentValue.attr('pclassi');

        }
    });

    var tempWIPH = "";
    var transno = "";
    var table = $('#tblCompEntry').DataTable();
    $('#tblCompEntry tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        var currentRow = $(this).closest("tr");
        var currentcolumn = currentRow.find('td');
        var currentValue = currentcolumn.find('p');


        if (tempWIPH == currentValue.attr('grid')) {
            ClearComp();
            tempWIPH = "";
        } else if (currentValue.attr('grid') == null) {
            ClearComp();
            tempWIPH = "";
        }
        else {
            tempWIPH = currentValue.attr('grid');
            transno = currentValue.attr('grno');
            if (currentValue.attr('status') == 'In Progress') {
                document.getElementById("btnAdd").value = "Edit Property";
                document.getElementById("btnAddComp").style.display = "block";
                document.getElementById("btnAddxx").style.display = "none";
                EditComp(currentValue.attr('grid'), currentValue.attr('grno'), currentValue.attr('pmlid'), currentValue.attr('pnum'), currentValue.attr('descr')
                    , currentValue.attr('status'), currentValue.attr('serno'), currentValue.attr('categ'), currentValue.attr('classi'));
              //  GetCount();
               // load_data(currentValue.attr('custodianid'));
            } else {
                document.getElementById("btnAdd").value = "Select Property";
                document.getElementById("btnAddComp").style.display = "none";
                document.getElementById("btnAddxx").style.display = "none";
                idForUpdate = 0;
                TransID = transno;

                load_data_compAddedLocked();
                //TransID = 0;
                //ClearCompLocked();
                EditCompLocked(currentValue.attr('grid'), currentValue.attr('grno'), currentValue.attr('pmlid'), currentValue.attr('pnum'), currentValue.attr('descr')
                    , currentValue.attr('status'), currentValue.attr('serno'), currentValue.attr('categ'), currentValue.attr('classi'));

            }

        }
    });

    load_data_compEntry();
});



function SelectProperty() {
    load_data_propertyselect();
    
    document.getElementById("btnAddxx").style.display = "block";
    $("#SelectProperty").modal("show");

}

function load_data_propertyselect() {
    var search = "";
    //$(".se-pre-con").fadeIn();
    $.ajax({
        type: 'POST',
        url: url + "/Component.aspx/SelectProperty",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#selectproperty').hide();
            $('#selectproperty').innerHTML = "";
            $('#selectproperty').fadeIn(200);
            myJsonData = data;
            populateDataTableSelectProperty(myJsonData);
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

function populateDataTableSelectProperty(data) {
    try {
        $('#tblSelectProperty').DataTable({
            "destroy": true,
            "processing": true, select: true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            'language': {
                //'searchPlaceholder': "Search",
                //"search": '<i class="fa fa-search"></i>',
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "scrollY": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "PropertyNumber" },
                { "data": "Description" },
                { "data": "SerialNo" },
                { "data": "ClassificationName" },
                { "data": "Category" }
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Goods Receive'
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
            //fixedColumns: {
            //    leftColumns: 1
            //},
            //fixedHeader: true
        });
        $('#tblSelectProperty').off('click');
    } catch (e) {
        var table = $('#tblSelectProperty').DataTable();
        table
            .clear()
            .draw();
    }
}



function load_data_compEntry() {
    $.ajax({
        type: 'POST',
        url: url + "/Component.aspx/GetCompEntry",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#compEntry').hide();
            $('#compEntry').innerHTML = "";
            $('#compEntry').fadeIn(200);
            myJsonData = data;
            populateDataTableCompEntry(myJsonData);
            console.log(data);
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}
function populateDataTableCompEntry(data) {
    try {
        $('#tblCompEntry').DataTable({
            "destroy": true,
            "processing": true, select: true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            'columnDefs': [{ 'targets': 0, 'orderable': false }],
            "sorting": false,
            'language': {
                //'searchPlaceholder': "Search",
                //"search": '<i class="fa fa-search"></i>',
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "scrollY": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TransactionNumber" },
                { "data": "PropertyNumber" },
                { "data": "MaintenanceRemarks" },
                { "data": "CreatedBy" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Accessories'
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
            //fixedColumns: {
            //    leftColumns: 1
            //},
            //fixedHeader: true
        });
        $('#tblCompEntry').off('click');
    } catch (e) {
        var table = $('#tblCompEntry').DataTable();
        table
            .clear()
            .draw();
    }
}






function addSelectProperty() {
    var alertMsg = '';
    if (precId == '0') {
        if (precId == '0')
            alertMsg += '\nProperty';
        swal("Please select a property ", alertMsg, "warning");
    }
    else {
        document.getElementById("lpropertyno").innerHTML = ppnum;
        document.getElementById("lserialno").innerHTML = pserial;
        document.getElementById("ldesc").innerHTML = pdesc;
        document.getElementById("lcategory").innerHTML = pcat;
        document.getElementById("lclassification").innerHTML = pclassi;
        $('#SelectProperty').modal('hide');
        document.getElementById("btnAddComp").style.display = "none";
        //ca.clear().draw(false);

    }
}



var alertMsg = "";
function CreateComponents() {
    if (precId == '0') {
        if (precId == '0')
            alertMsg = '\nProperty';
        swal("Please select a property ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Confirm Transaction?",
            text: "Are you sure you want to confirm this transaction?",
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
                    url: url + "/Component.aspx/CreateComponents",
                    data: "{ TransId : '" + idForUpdate + "', pmlid : '" + precId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                       if (msg.d == "1") {
                           swal("Successful", "Transaction has been successfully saved.", "success");
                           load_data_compEntry();
                           ClearComp();
                           document.getElementById("btnAddxx").style.display = "none";
                        }
                        else if (msg.d == "0") {
                            swal("Successful", "Transaction has been successfully updated.", "success");
                            load_data_compEntry();
                            document.getElementById("btnAddxx").style.display = "none";
                          //  load_data_Pendingupload();
                          //  load_data($('#ddlCustodian').val());
                        }
                       ClearComp();
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}


function EditComp(grid, grno, pmlid, pnum, desc, status, serial, cat, classi) {
    idForUpdate = grid;
    TransID = grno;
    $('#txtTrahsNum').val(grno);

    document.getElementById("lpropertyno").innerHTML = pnum;
    document.getElementById("lserialno").innerHTML = serial;
    document.getElementById("ldesc").innerHTML = desc;
    document.getElementById("lcategory").innerHTML = cat;
    document.getElementById("lclassification").innerHTML = classi;

    load_data_compAdded();
}

function EditCompLocked(grid, grno, pmlid, pnum, desc, status, serial, cat, classi) {
    
    $('#txtTrahsNum').val(grno);

    document.getElementById("lpropertyno").innerHTML = pnum;
    document.getElementById("lserialno").innerHTML = serial;
    document.getElementById("ldesc").innerHTML = desc;
    document.getElementById("lcategory").innerHTML = cat;
    document.getElementById("lclassification").innerHTML = classi;

}

function ClearComp() {
    idForUpdate = 0;
    TransID = 0;
    document.getElementById("btnAdd").value = "Select Property";
    document.getElementById("btnAddComp").style.display = "none";
    $('#txtTrahsNum').val('');

    document.getElementById("lpropertyno").innerHTML = '';
    document.getElementById("lserialno").innerHTML = '';
    document.getElementById("ldesc").innerHTML = '';
    document.getElementById("lcategory").innerHTML = '';
    document.getElementById("lclassification").innerHTML = '';

    load_data_compAdded();
}

function ClearCompLocked() {
    idForUpdate = 0;
    TransID = 0;
    document.getElementById("btnAdd").value = "Select Property";
    document.getElementById("btnAddComp").style.display = "none";
    $('#txtTrahsNum').val('');

    document.getElementById("lpropertyno").innerHTML = '';
    document.getElementById("lserialno").innerHTML = '';
    document.getElementById("ldesc").innerHTML = '';
    document.getElementById("lcategory").innerHTML = '';
    document.getElementById("lclassification").innerHTML = '';

}

function NewT() {
    swal({
        title: "Are you sure you want to create a new transaction?",
        text: "Note: The current property details fields will be cleared.",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            swal("Successful", "Property fields have been cleared.", "success");

            document.getElementById("btnAddxx").style.display = "inline";
            ClearComp();
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });


}

var IntI = 0;
var TempIntI = 0;
function AddToComponentEntry() {
    //QuantityKeyUp();
    TempIntI = 0;
    IntI = 0;
    listL.length = 0;
    tb.clear().draw(false);
    $("#AddComponentEntry").modal("show");
}


//function QuantityKeyUpX() {
//    tb.row.add([
//                   '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + IntI + '" value="' + '' + '" disabled style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtDesc' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtLongDesc' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtClassification' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtBrand' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtModel' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtTag' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<select class="form-control txtSerial" id="txtTag' + IntI + '" style="width:120px;">' +
                   
//                    taglist.forEach((position, index) => {
                        
//                        '<option value="'+position.CustodianID+'">'+position.Custodian+'</option>' +
//                    })
//                })
//                   '<option value="1">METAL</option>'+
//                   '<option value="2">NON METAL</option>'+
//                   '</select>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtUOM' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtAcquiredcost' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
//                   '<center><button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + IntI + '" type="button" onclick="RemoveGRDetails(' + '' + ',' + '' + ');"><b>X</b></button></center>'
//    ]
//           ).draw(false);
//    IntI += 1;
//}


function QuantityKeyUpX() {
    GetTaglist();

    tb.row.add([
                       '<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="txtPropertyNumber' + IntI + '" value="' + '' + '" style="width:120px;"></input>',
                       ' <textarea class="form-control" id="txtLongDesc' + IntI + '" style="width:120px;"  style="width:120px;" runat="server"></textarea>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtSerial' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="txtUOM' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',
                       '<input class="form-control txtSerial" onkeyup="return formatCurrency(this);" id="txtAcquiredcost' + IntI + '" value="' + '' + '" style="width:120px;" style="width:120px;"></input>',

                       '<input class="form-control" onkeypress="return EnterKey(event);" type="date" id="txtacquireddate' + IntI + '"></input>',
                       '<center><button class="btn btn-danger btn-med btn_Clear" id="btnClearRow' + IntI + '" type="button" onclick="RemoveComponentDetails(this);"><b>X</b></button></center>'
        ]
               ).draw(false);
        IntI += 1;
        for (var i = 0; i < TempIntI; i++) {
            RemoveComponentDetails1(rComponent[i]);
        }

}

let listL = [];


var rComponent = [];
function RemoveComponentDetails1(button) {
    var currentRow = $(button).closest("tr");

    currentRow.remove();
}



var rComponent = [];
function RemoveComponentDetails(button) {
    rComponent[TempIntI] = button;
    TempIntI += 1;
    var currentRow = $(button).closest("tr");
    listL.push(button.id.substring(11, 12));
    currentRow.remove().draw(false);


    //// Initialize table
    //var table = $('#tb').DataTable();

    //// Get the closest row to the button clicked
    //var row = $(button).closest('tr');

    //// Check if row is found and remove it
    //if (row && row.length > 0) {
    //    row.remove();
    //    table.row(row).remove().draw(true); // Use 'true' to fully redraw and remove row data
    //    console.log("Row removed completely.");
    //} else {
    //    console.error("Unable to locate row for complete removal.");
    //}
}

var tagl = '';
function GetTaglist() {
    tagl = '';
    taglist.forEach((position, index) => {
        tagl += '<option value="' + position.TagID + '">' + position.TagName + '</option>'
    })
}

function ComponentEntry() {    
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to save this property/ies?",
        icon: "warning",
        reverseButtons: false,
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            var isSuccess = "";
            for (var i = 0; i < IntI; i++) {
               // alert($("#txtExpiryDate" + i).val());
                $.ajax({
                    type: "POST",
                    async:false,
                    url: url + "/GoodsReceive.aspx/UpdateGoodsReceivePropertyDetails",
                    data: " { detailsid: '" + myArrayX[i] + "', propertyno: '" + $("#txtPropertyNumber" + i).val() + "',serialno: '" + $("#txtSerial" + i).val() + "',longdesc: '" + $("#txtDescription" + i).val() + "',warranty: '" + $("#txtWarrantyDate" + i).val() + "',insurance: '" + $("#txtInsurance" + i).val() + "',expirydate: '" + $("#txtExpiryDate" + i).val() + "',label: '" + $("#txtLabelToPrint" + i).val() + "',rfid: '" + $("#txtrfid" + i).val() + "',barcode: '" + $("#txtbarcode" + i).val() + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "1") {
                            isSuccess = "True";
                        }
                        else if (msg.d == "0") {
                            isSuccess = "False"
                        }
                    }
                });
            }
            if (isSuccess == "True") {
                swal("Successful", "property/ies successfully modified.", "success");
                $("#addToPropertyGoodsReceive1").modal("hide");
            }
            else {
                swal("Warning", "Cannot edit the property/ies", "warning");
                $("#addToPropertyGoodsReceive1").modal("hide");
            }

        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}



var taglist = new Array();
function GetTag() {
    var context = window.location.pathname.substring(1);
    //var ddlPosition = $('#txtTag' + IntI);
    //ddlPosition.empty();
    //ddlPosition.append($('<option>', {
    //    value: '0',
    //    text: '--- Select Tag Type ---',
    //    rowindex: -1
    //}));
    $.ajax({
        async: false,
        type: 'POST',
        url: context + '/GetTagList',
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                taglist = JSON.parse(xml.d);
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            //alert(error);
            console.log(xhr);
        }
    });
}

function AddComponent() {
    var alertMsg = '';
    var isDesc = '';
    var isLDesc = '';
    var isClass = '';
    var isUOM = '';
    var isACost = '';
    for (var i = 0; i < IntI; i++) {
        //  if (!listL.includes(i.toString())) {
        if ($('#txtPropertyNumber' + i).val() == '' || $('#txtLongDesc' + i).val() == '' || $('#txtSerial' + i).val() == '' || $('#txtUOM' + i).val() == '' || $('#txtAcquiredcost' + i).val() == '' || $('#txtacquireddate' + i).val() == '') {

            if ($('#txtPropertyNumber' + i).val() == '') {
                alertMsg += '\nProperty Number';
                    isDesc = 'x';
                }
                if ($('#txtLongDesc' + i).val() == '') {
                    alertMsg += '\nLong Description';
                    isLDesc = 'x';
                }
                if ($('#txtClassification' + i).val() == '') {
                    alertMsg += '\nClassification';
                    isClass = 'x';
                }
                if ($('#txtUOM' + i).val() == '') {
                    alertMsg += '\nUnit Of Measurement';
                    isUOM = 'x';
                }
                if ($('#txtAcquiredcost' + i).val() == '') {
                    alertMsg += '\nAcquired Cost';
                    isACost = 'x';
                }
                if ($('#txtSerial' + i).val() == '') {
                    alertMsg += '\nSerial No';
                    isACost = 'x';
                }
                if ($('#txtacquireddate' + i).val() == '') {
                    alertMsg += '\nAcquired Date';
                    isACost = 'x';
                }
            }
        //}
    }

    if (alertMsg != '') {
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }else {
        var isSuccess = "";
        for (var i = 0; i < IntI; i++) {
            if (!listL.includes(i.toString())) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: url + "/Component.aspx/AddComponents",
                    data: " { idForUpdate: '" + TransID + "', propertyno: '" + $("#txtPropertyNumber" + i).val()
                        + "', acquireddate: '" + $("#txtacquireddate" + i).val()
                        + "', longdesc: '" + $("#txtLongDesc" + i).val()
                        + "',serial: '" + $("#txtSerial" + i).val() + "',uom: '" + $("#txtUOM" + i).val() + "',acquiredcost: '"
                        + $("#txtAcquiredcost" + i).val().replaceAll(",", "") + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "1") {
                        }
                        else if (msg.d == "0") {
                            isSuccess = "False"
                        }
                    }
                });
            }
        }
        if (isSuccess != "False") {
            load_data_compAdded();
            $("#AddComponentEntry").modal("hide");
        }
        else {
            swal("Warning", "Cannot add components", "warning");
            $("#AddComponentEntry").modal("hide");
        }
    }
}


var intIi = 0;
function load_data_compAdded() {
    $.ajax({
        type: 'POST',
        url: url + "/Component.aspx/GetCompAdded",
        data: "{ idForUpdate: '" + TransID + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            ca.clear().draw(false);

           var compAddedList = JSON.parse(data.d);
           console.log(compAddedList);
           intIi = 0;
           compAddedList.forEach((myprop, index) => {
               QuantityKeyUp(intIi, myprop.ComponentNumber, myprop.Description, myprop.SerialNo, myprop.UnitOfMeasurement, myprop.AcquiredCost, myprop.AcquiredDate, myprop.ComponentEntryDetailsID);
                intIi = intIi + 1;
            });

        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}

function load_data_compAddedLocked() {
    $.ajax({
        type: 'POST',
        url: url + "/Component.aspx/GetCompAdded",
        data: "{ idForUpdate: '" + TransID + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            ca.clear().draw(false);

            var compAddedList = JSON.parse(data.d);
            console.log(compAddedList);
            intIi = 0;
            compAddedList.forEach((myprop, index) => {
                QuantityKeyUpLocked(intIi, myprop.ComponentNumber, myprop.Description, myprop.SerialNo, myprop.UnitOfMeasurement, myprop.AcquiredCost, myprop.AcquiredDate, myprop.ComponentEntryDetailsID);
                intIi = intIi + 1;
            });

        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}


function QuantityKeyUp(i, propertyno, desc, serial, uom, ac, ad, compEntryDetails) {
  //  GetTaglist();
    ca.row.add([
                       //'<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="AtxtPropertyNumber' + i + '" value="' + propertyno + '" disabled style="width:140px;"></input>',
                     //  '<input class="form-control txtDesc" onkeypress="return EnterKey(event);" id="AtxtDesc' + i + '" value="' + desc + '" disabled style="width:120px;height:120px;"></input>',
                       ' <textarea class="form-control" id="AtxtPropertyNumber' + i + '"  disabled style="width:140px;" runat="server">' + propertyno + '</textarea>',
                       //' <textarea class="form-control" id="AtxtDesc' + i + '" style="width:120px;"  disabled style="width:120px;" runat="server">' + desc + '</textarea>',
                       ' <textarea class="form-control" id="AtxtLongDesc' + i + '" style="width:120px;"  disabled style="width:120px;" runat="server">' + desc + '</textarea>',
                      // '<input class="form-control txtLongDesc" onkeypress="return EnterKey(event);" id="AtxtLongDesc' + i + '" value="' + longdesc + '" disabled style="width:120px;"></input>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="AtxtSerial' + i + '" value="' + serial + '" disabled style="width:120px;"></input>',
                     //  '<input class="form-control txtClassification" onkeypress="return EnterKey(event);" id="AtxtClassification' + i + '" value="' + classification + '" disabled  style="width:120px;"></input>',
                     //  '<input class="form-control txtBrand" onkeypress="return EnterKey(event);" id="AtxtBrand' + i + '" value="' + brand + '" disabled style="width:120px;"></input>',
                     //  '<input class="form-control txtModel" onkeypress="return EnterKey(event);" id="AtxtModel' + i + '" value="' + model + '"  disabled style="width:120px;"></input>',
                     //  '<select class="form-control txtTag" id="AtxtTag' + i + '" disabled style="width:120px;">' +
                     //tagl +
                     //  '</select>',
                       '<input class="form-control txtUOM" onkeypress="return EnterKey(event);" id="AtxtUOM' + i + '" value="' + uom + '" disabled style="width:120px;"></input>',
                       '<input class="form-control txtAcquiredcost" onkeyup="return formatCurrency(this);" id="AtxtAcquiredcost' + i + '" value="' + convertToMoneyFormat(ac) + '" disabled style="width:120px;"></input>',

                       '<input class="form-control txtacquireddate" onkeypress="return EnterKey(event);" type="date" id="Atxtacquireddate' + i + '"></input>',
                       '<center><button class="btn btn-warning btn-med btn_Edit " id="AbtnEditRow|' + i + "|" + compEntryDetails + '" type="button" onclick="EditComponentDetails(this);"><span class="fa fa-edit"></span></button> ' +
                               '<button class="btn btn-danger btn-med btn_Clear" id="AbtnClearRow' + i + '" type="button" onclick="DeleteComponent(' + compEntryDetails + ');"><b>  X  </b></button> </center>'
    ]
               ).draw(false);


    let date = new Date(parseInt(ad.match(/\d+/)[0]));
    let formattedDate = date.toISOString().split('T')[0];
    document.getElementById('Atxtacquireddate' + i).value = formattedDate;
    document.getElementById("Atxtacquireddate" + i).disabled = true;
    //setTimeout(function () {
    //    $('#AtxtTag' + i).val(tag).trigger('change');
    //}, 1000)
}

function QuantityKeyUpLocked(i, propertyno, desc, serial, uom, ac, ad, compEntryDetails) {
    //  GetTaglist();
    ca.row.add([
                       //'<input class="form-control txtPropertyNumber" onkeypress="return EnterKey(event);" id="AtxtPropertyNumber' + i + '" value="' + propertyno + '" disabled style="width:140px;"></input>',
                     //  '<input class="form-control txtDesc" onkeypress="return EnterKey(event);" id="AtxtDesc' + i + '" value="' + desc + '" disabled style="width:120px;height:120px;"></input>',
                       ' <textarea class="form-control" id="AtxtPropertyNumber' + i + '"  disabled style="width:140px;" runat="server">' + propertyno + '</textarea>',
                       //' <textarea class="form-control" id="AtxtDesc' + i + '" style="width:120px;"  disabled style="width:120px;" runat="server">' + desc + '</textarea>',
                       ' <textarea class="form-control" id="AtxtLongDesc' + i + '" style="width:120px;"  disabled style="width:120px;" runat="server">' + desc + '</textarea>',
                      // '<input class="form-control txtLongDesc" onkeypress="return EnterKey(event);" id="AtxtLongDesc' + i + '" value="' + longdesc + '" disabled style="width:120px;"></input>',
                       '<input class="form-control txtSerial" onkeypress="return EnterKey(event);" id="AtxtSerial' + i + '" value="' + serial + '" disabled style="width:120px;"></input>',
                     //  '<input class="form-control txtClassification" onkeypress="return EnterKey(event);" id="AtxtClassification' + i + '" value="' + classification + '" disabled  style="width:120px;"></input>',
                     //  '<input class="form-control txtBrand" onkeypress="return EnterKey(event);" id="AtxtBrand' + i + '" value="' + brand + '" disabled style="width:120px;"></input>',
                     //  '<input class="form-control txtModel" onkeypress="return EnterKey(event);" id="AtxtModel' + i + '" value="' + model + '"  disabled style="width:120px;"></input>',
                     //  '<select class="form-control txtTag" id="AtxtTag' + i + '" disabled style="width:120px;">' +
                     //tagl +
                     //  '</select>',
                       '<input class="form-control txtUOM" onkeypress="return EnterKey(event);" id="AtxtUOM' + i + '" value="' + uom + '" disabled style="width:120px;"></input>',
                       '<input class="form-control txtAcquiredcost" onkeyup="return formatCurrency(this);" id="AtxtAcquiredcost' + i + '" value="' + convertToMoneyFormat(ac) + '" disabled style="width:120px;"></input>',

                       '<input class="form-control txtacquireddate" onkeypress="return EnterKey(event);" type="date" id="Atxtacquireddate' + i + '"></input>',
                       ''
    ]
               ).draw(false);


    let date = new Date(parseInt(ad.match(/\d+/)[0]));
    let formattedDate = date.toISOString().split('T')[0];
    document.getElementById('Atxtacquireddate' + i).value = formattedDate;
    document.getElementById("Atxtacquireddate" + i).disabled = true;
    //setTimeout(function () {
    //    $('#AtxtTag' + i).val(tag).trigger('change');
    //}, 1000)
}

function convertToMoneyFormat(data) {
    try {
        let resultValue = "";

        const parsedValue = parseFloat(data);

        if (!isNaN(parsedValue)) {
            resultValue = new Intl.NumberFormat('en-US', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            }).format(parsedValue);
        }

        return resultValue;
    } catch (error) {
        return "";
    }
}

 
function EditComponentDetails(button) {
    var idString = button.id.split('|')[1];
    var recString = button.id.split('|')[2];
    if (button.className.includes("primary")) {
        document.getElementById("AtxtPropertyNumber" + idString).disabled = true;
        document.getElementById("AtxtLongDesc" + idString).disabled = true;
        document.getElementById("AtxtSerial" + idString).disabled = true;
        //document.getElementById("AtxtClassification" + idString).disabled = true;
        //document.getElementById("AtxtBrand" + idString).disabled = true;
        //document.getElementById("AtxtModel" + idString).disabled = true;
        //document.getElementById("AtxtTag" + idString).disabled = true;
        document.getElementById("AtxtUOM" + idString).disabled = true;
        document.getElementById("AtxtAcquiredcost" + idString).disabled = true;
        document.getElementById("Atxtacquireddate" + idString).disabled = true;

        document.getElementById("AbtnEditRow" + button.id.substring(11)).classList.remove("btn-primary");
        document.getElementById("AbtnEditRow" + button.id.substring(11)).classList.add("btn-warning");

        document.getElementById("AbtnEditRow" + button.id.substring(11)).querySelector("span").classList.remove("fa-save");
        document.getElementById("AbtnEditRow" + button.id.substring(11)).querySelector("span").classList.add("fa-edit");
        EditComponent(recString, $("#AtxtPropertyNumber" + idString).val(), $("#AtxtLongDesc" + idString).val(), $("#AtxtSerial" + idString).val()
            , $("#Atxtacquireddate" + idString).val(), $("#AtxtUOM" + idString).val()
            , $("#AtxtAcquiredcost" + idString).val());

    } else {
        //document.getElementById("AtxtDesc" + idString).disabled = false;
        document.getElementById("AtxtPropertyNumber" + idString).disabled = false;
        document.getElementById("AtxtLongDesc" + idString).disabled = false;
        document.getElementById("AtxtSerial" + idString).disabled = false;
        //document.getElementById("AtxtClassification" + idString).disabled = false;
        //document.getElementById("AtxtBrand" + idString).disabled = false;
        //document.getElementById("AtxtModel" + idString).disabled = false;
        //document.getElementById("AtxtTag" + idString).disabled = false;
        document.getElementById("AtxtUOM" + idString).disabled = false;
        document.getElementById("AtxtAcquiredcost" + idString).disabled = false;
        document.getElementById("Atxtacquireddate" + idString).disabled = false;

        document.getElementById("AbtnEditRow" + button.id.substring(11)).classList.remove("btn-warning");
        document.getElementById("AbtnEditRow" + button.id.substring(11)).classList.add("btn-primary");

        document.getElementById("AbtnEditRow" + button.id.substring(11)).querySelector("span").classList.remove("fa-edit");
        document.getElementById("AbtnEditRow" + button.id.substring(11)).querySelector("span").classList.add("fa-save");
    }
}

function EditComponent(id,propertyno,longdesc,serial,acquireddate,uom,acost) {
    $.ajax({
        type: "POST",
        async: false,
        url: url + "/Component.aspx/UpdateComponents",
        data: " { id: '" + id + "',idForUpdate: '" + TransID + "', propertyno: '" + propertyno + "',longdesc: '" + longdesc
            + "',serial: '" + serial + "',acquireddate: '" + acquireddate + "',uom: '" + uom + "',acquiredcost: '"
            + acost.replaceAll(",", "") + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                load_data_compAdded();
            }
            else if (msg.d == "0") {
                swal("Warning", "Cannot add components", "warning");
            }
        }
    });
}

function DeleteComponent(id) {
    $.ajax({
        type: "POST",
        async: false,
        url: url + "/Component.aspx/RemoveComponents",
        data: " { idForUpdate: '" + id + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                load_data_compAdded();
            }
            else if (msg.d == "0") {
                swal("Warning", "Cannot remove components", "warning");
            }
        }
    });
}

function cancelComponent() {
    
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
    textareatt.id = "canceldisposal";

    swal({
        title: "Are you sure?",
        text: "Are you sure you want to cancel this transaction?",
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
    }).then(function (isConfirm) {
        if (isConfirm) {
            if (isConfirm === null) return false;

            var textremarks = $('#canceldisposal').val();

            if (textremarks === "") {
                swal("Warning", "Add remarks to CANCEL this transaction.", "warning");
                return false
            }
            $.ajax({
                type: "POST",
                url: url + "/Component.aspx/CancelComponent",
                data: "{transno: '" + TransID + "', remarks: '" + textremarks + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    load_data_compEntry();
                    ClearComp();

                    swal("Successful", "Component Transaction successfully cancelled.", "success");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function LockTransaction(id) {
    swal({
        title: "Lock Transaction?",
        text: "Are you sure you want to lock this transaction?",
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
                url: url + "/Component.aspx/LockTransaction",
                data: "{ TransId : '" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Transaction has been successfully locked.", "success");
                        load_data_compEntry();
                    }
                    ClearComp();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}


function UnlockTransaction(id) {
    swal({
        title: "Lock Transaction?",
        text: "Are you sure you want to unlock this transaction?",
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
                url: url + "/Component.aspx/UnlockTransaction",
                data: "{ TransId : '" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Transaction has been successfully unlocked.", "success");
                        load_data_compEntry();
                    }
                    ClearComp();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}


function ClearE() {
    document.getElementById("btnAddxx").style.display = "none";
    $('#selectproperty').hide();
}