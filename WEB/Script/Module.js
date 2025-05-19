var idForUpdate = 0;
var currentPage = 1;


$(function () {
    LoadControls();
});

function LoadControls() {
    $(".btn").button();
    $('.select2').select2();
    //load_data();
}

$(document).ready(function () {
    $('.select2').select2();
    $("#tblModuleList").DataTable({
        "scrollX": true,
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
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
    load_data2();
});


function load_data() {
    var myJsonData = [];
    $.ajax({
        type: 'POST',
        url: "System/Module.aspx/GetDynamicLocation",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
           
            console.log(msg);
            console.log('Success');
            //myJsonData = JSON.parse(msg.d);
            //alert(myJsonData[0].Location);
            $("#mySelect").select2({
                placeholder: 'Select an option',
                width: "100%",
                data: JSON.parse(msg.d),
                formatSelection: function (item) {
                    return item.text
                },
                formatResult: function (item) {
                    return item.text
                },
                templateResult: formatResult,
            });
        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });

    function formatResult(node) {
        var $result = $('<span style="padding-left:' + (5 * node.level) + 'px;">' + node.text + '</span>');
        return $result;
    };

    

}

function addModuleDialogBox() {
    document.getElementById("chkActive").checked = true;
    document.getElementById("chkWebPage").checked = true;
    $("#addModuleDialogBox").modal("show");
}

function SaveModule() {

    var na = $('#txtName').val();
    var nam = na.replace("'", "`");
    var name = nam.replace("\\", "");
    var arr = $('#txtArrangement').val();
    var arra = arr.replace("'", "`");
    var arran = arra.replace("\\", "");
    var ur = $('#txtURL').val();
    var url = ur.replace("'", "`");
    var urla = url.replace("\\", "");
    var des = $('#txtDescription').val();
    var desc = des.replace("'", "`");
    var descr = desc.replace("\\", "");
    

    var alertMsg;
    var arrangement = $('#txtArrangement').val().trim();
    if ($('#txtName').val().trim() == '' || $('#txtArrangement').val().trim() == '' || $('#txtURL').val().trim() == '') {
        alertMsg = 'Fill-out all the required field(s): ';
        if ($('#txtName').val().trim() == '')
            alertMsg += '\nName ';
        if ($('#txtArrangement').val().trim() == '')
            alertMsg += '\nArrangement';
        if ($('#txtURL').val().trim() == '')
            alertMsg += '\nURL';
        alert(alertMsg);
    }
    else if (!arrangement.match(/^\d+/)) {
        alert("Enter only numbers in Arrangement field");
    }
    else {
        $.ajax({
            type: "POST",
            url: "System/Module.aspx/SaveModule",
            data: "{id: '" + idForUpdate + "', name: '" + name + "',description: '" + descr + "',arrangement: '" + arran + "',URL: '" + urla + "',parentModuleID: '" + $("[id*='BodyHolder_ddlParentModule'] :selected").val() + "', webpage: '" + document.getElementById("chkWebPage").checked + "', active: '" + document.getElementById("chkActive").checked + "',currentPage: '" + currentPage + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_ModuleList').hide();
                if (msg.d.split('|')[1] == '0') {
                    alert("Module Successfully Saved!");
                    $('#BodyHolder_ModuleList').html(msg.d.split('|')[0]);
                    //$('#BodyHolder_ModuleList').html(msg.d);
                    $("#addModuleDialogBox").modal("hide");
                    Clear();
                }
                else {
                    alert("Module Already Taken!");
                    //$('#BodyHolder_ModuleList').html(msg.d);
                    $('#BodyHolder_ModuleList').html(msg.d.split('|')[0]);
                }

                $('#BodyHolder_ModuleList').fadeIn(200);
            }
        });

    }
}

function Clear() {
    idForUpdate = 0;
    $('#txtName').val("");
    $('#txtName').prop("disabled", false);
    $('#txtDescription').val("");
    $('#txtArrangement').val("");
    $('#txtURL').val("");
    $('#BodyHolder_ddlParentModule').val("--None--");
}
function Pagination(pageNo) {

    currentPage = pageNo;

    $.ajax({
        type: "POST",
        url: "System/Module.aspx/Pagination",
        data: "{ pageNo: '" + currentPage + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ModuleList').hide();
            $('#BodyHolder_ModuleList').html(msg.d);
            $('#BodyHolder_ModuleList').fadeIn(200);
        }
    });
}


function load_data2() {

    $.ajax({
        type: 'POST',
        url: "System/Module.aspx/GetModule",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#ModuleList').hide();
            $('#ModuleList').innerHTML = "";
            $('#ModuleList').fadeIn(200);

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

function GetModuleId(moduleId) {
    idForUpdate = moduleId;
    $("#addModuleDialogBox").modal("show");
    $.ajax({
        type: "POST",
        url: "System/Module.aspx/ModulebyId",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.d.split('|')[5] == "0") {
                $('#BodyHolder_ddlParentModule').val('0');
            }
            else {
                ModuleName(msg.d.split('|')[5]);
            }

            $('#txtName').val(msg.d.split('|')[1]);
            $('#txtName').prop('disabled', true);
            $('#txtDescription').val(msg.d.split('|')[2]);
            $('#txtArrangement').val(msg.d.split('|')[3]);
            $('#txtURL').val(msg.d.split('|')[4]);

            if (msg.d.split('|')[6] == 'True') {
                document.getElementById("chkWebPage").checked = true;
            }
            else {
                document.getElementById("chkWebPage").checked = false;
            }
            if (msg.d.split('|')[7] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
            CountChar($('#txtDescription').val(), 100, 'cnt', 'txtDescription');

        }
    });
}

function ModuleName(parentModuleId) {
    $.ajax({
        type: "POST",
        url: "System/Module.aspx/ModulebyId",
        data: "{ id: '" + parentModuleId + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ddlParentModule').val(msg.d.split('|')[0]);
            $('#BodyHolder_ddlParentModule').text() = msg.d.split('|')[1];
        }
    });
}

function deleteModule(moduleid) {
    idForUpdate = moduleid;
    $("#deleteModule").modal("show");
}

function DeleteModule(moduleid) {
    $("#deleteModule").modal("hide");
    $.ajax({
        type: "POST",
        url: "System/Module.aspx/DeleteModule",
        data: "{ moduleid: '" + idForUpdate + "',pageNo: '" + currentPage + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ModuleList').hide();
            $('#BodyHolder_ModuleList').html(msg.d);
            alert("Module Successfully Deleted!");
            $('#BodyHolder_ModuleList').fadeIn(200);
            idForUpdate = 0;
        }
    });
}

function populateDataTable(data) {
    try {

        $('#tblModuleList').DataTable({
            "destroy": true,
            "processing": true,
            //"language": {
            //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '                
            //},
            'language': { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Name" },
                { "data": "ArrangemenT" },
                { "data": "URL" },
                { "data": "ParentModuleID" },
                { "data": "IsActive" },
                { "data": "WebPage" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Module List'
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

        $('#tblModuleList').off('click');

    } catch (e) {
        var table = $('#tblModuleList').DataTable();
        table
            .clear()
            .draw();
    }
}