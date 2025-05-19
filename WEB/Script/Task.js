var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblTaskList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
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
    load_data();

    //$('#addTaskDialogBox').modal({ backdrop: 'static', keyboard: false })
});

function addFields() {
    var number = document.getElementById("txtNumberOfSteps").value;
    if (parseInt(number) > 10) {
        //alert("Maximum of 10 Steps.");
        swal("Warning", "Maximum of 10 Steps.", "warning"); $('#txtNumberOfSteps').val('10');
        number = 10;
        var container = document.getElementById("steplist");
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
        for (i = 0; i < number; i++) {
            //container.appendChild(document.createTextNode("Step " + (i + 1) + ":"));
            var input = document.createElement("input");
            input.type = "text";
            input.id = "step" + (i + 1);
            input.className = "form-control";
            input.placeholder = "step" + (i + 1);
            container.appendChild(input);
            container.appendChild(document.createElement("br"));
        }
    }
    else {
        var container = document.getElementById("steplist");
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
        for (i = 0; i < number; i++) {
            //container.appendChild(document.createTextNode("Step " + (i + 1) + ":"));
            var input = document.createElement("input");
            input.type = "text";
            input.id = "step" + (i + 1);
            input.className = "form-control";
            input.placeholder = "step" + (i + 1);
            container.appendChild(input);
            container.appendChild(document.createElement("br"));
        }
    }
}

function load_data() {
    $.ajax({
        type: 'POST',
        url: "Files_Data/Task.aspx/GetTask",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tasklist').hide();
            $('#tasklist').innerHTML = "";
            $('#tasklist').fadeIn(200);

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
        $('#tblTaskList').DataTable({
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
                { "data": "TaskName" },
                { "data": "Description" },
                { "data": "StringNumberOfSteps" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Task List'
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

        $('#tblTaskList').off('click');
    } catch (e) {
        var table = $('#tblTaskList').DataTable();
        table
            .clear()
            .draw();
    }
}

function addTaskDialogBox() {
    $('#txtNumberOfSteps').val('0');
    addFields();
    $('#txtNumberOfSteps').val('');
    Clear();
    document.getElementById("chkActive").checked = true;
    $("#addTaskDialogBox").modal("show");

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
                let elementsAddBrand = document.getElementsByClassName("btn_EditTask");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveTask',
                    class: 'btn btn-success btn_EditTask',
                    onclick: 'SaveTask()',
                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
                });

                $('#SaveCancelTask').prepend($formBrand);
            }
        }
    });
}
//function addTaskDialogBox() {
//    Clear();
//    document.getElementById("chkActive").checked = true;
//    $("#addTaskDialogBox").modal("show");

//    var context = window.location.pathname.substring(1);
//    var url = context.split('/')[1];
//    $.ajax({
//        type: "POST",
//        url: context + "/CheckR",
//        data: " { context : '" + context + "'} ",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (msg1) {
//            var btnAccess = [];
//            btnAccess = msg1.d.split('|');
//            if (btnAccess[1] == "1" && btnAccess[2] == "0") {
//                let elementsAddBrand = document.getElementsByClassName("btn_EditTask");
//                for (let i = 0; i < elementsAddBrand.length; i++) {
//                    elementsAddBrand[i].remove();
//                }
//                var $formBrand = makeElement('button', {
//                    type: 'button',
//                    id: 'btnSaveTask',
//                    class: 'btn btn-success btn_EditTask',
//                    onclick: 'SaveTask()',
//                    innerHTML: '&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;'
//                });

//                $('#SaveCancelTask').prepend($formBrand);
//            }
//        }
//    });
//}

function SaveTask() {
    var number = document.getElementById("txtNumberOfSteps").value;
    var cate = ReplaceStr1($('#txtTaskName').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' '));
    var steps = [];
    var stringsteps;
    var alertMsg = '';

    for (i = 0; i < number; i++) {
        
        if ($('#step' + (i + 1)).val()) {
            steps.push(ReplaceStr1($('#step' + (i + 1)).val()));
        }
    }
    stringsteps = steps.join().replace(/,/g, '-');

    //alert(stringsteps);
    if (ReplaceStr1($('#txtTaskName').val().trim().replace(/  +/g, ' ')) == '' || ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '' || $('#txtNumberOfSteps').val().trim() == '' || $('#txtNumberOfSteps').val() == '0' || stringsteps == '') {
       // alert(step);
        if (ReplaceStr1($('#txtTaskName').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nTask Name';
        if (ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == '')
            alertMsg += '\nDescription';
        if ($('#txtNumberOfSteps').val() == '' || $('#txtNumberOfSteps').val() == '0')
            alertMsg += '\nNumber of Steps';
        if (stringsteps == '')
            alertMsg += '\nSteps';

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this task?",
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
                    url: "Files_Data/Task.aspx/SaveTask",
                    data: " { id : '" + idForUpdate + "' , task : '" + cate + "', description :'" + desc + "', numberofsteps: '" + steps.length + "', steps: '" + stringsteps + "',active:'" + document.getElementById("chkActive").checked + "'} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            $("#addTaskDialogBox").modal("hide");
                            swal("Successful", "Task Successfully Saved!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 1) {
                            $("#addTaskDialogBox").modal("hide");
                            swal("Successful", "Task Successfully Modified!", "success");
                            Clear();
                            load_data();
                        }
                        else if (msg.d == 3) {
                            $("#addTaskDialogBox").modal("hide");
                            swal("Error", "Task cannot be modified at the moment. There is an ongoing procedure for this task!", "error");
                            Clear();
                            load_data();
                        }
                        else {
                            $("#addTaskDialogBox").modal("hide");
                            swal("Error", "Task Already Exist!", "error");
                            Clear();
                            load_data();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function GetTaskID(id) {
    idForUpdate = id;

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
                let elementsAddCat = document.getElementsByClassName("btn_EditTask");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "Files_Data/Task.aspx/TaskbyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtTaskName').val(msg.d.split('|')[1]);
            $('#txtDescription').val(msg.d.split('|')[2]);
            $('#txtNumberOfSteps').val(msg.d.split('|')[3]);
            if (msg.d.split('|')[5] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
            var number = msg.d.split('|')[3];
            var container = document.getElementById("steplist");
            while (container.hasChildNodes()) {
                container.removeChild(container.lastChild);
            }
            for (i = 0; i < number; i++) {
                container.appendChild(document.createTextNode("Step " + (i + 1) + ":"));
                var input = document.createElement("input");
                input.type = "text";
                input.id = "step" + (i + 1);
                input.placeholder = "step" + (i + 1);
                input.className = "form-control";
                input.value = msg.d.split('|')[4].split('-')[i];
                container.appendChild(input);
                container.appendChild(document.createElement("br"));
            }
        }
    });

    $("#addTaskDialogBox").modal("show");
}

function deleteTask(id) {
    idForUpdate = id;
    //$("#deleteTask").modal("show");
    swal({
        title: "Delete Task",
        text: "Are you sure you want to delete this task?",
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
                url: "Files_Data/Task.aspx/DeleteTask",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "1") {
                        swal("Successful", "Task Successfully Deleted!", "success");
                        Clear();
                        load_data();
                    }
                    else {
                        swal("Error", "Task cannot be Deleted, it is being actively used!", "error");
                    }
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteTask() {
    $("#deleteTask").modal("hide");
    $.ajax({
        type: "POST",
        url: "Files_Data/Task.aspx/DeleteTask",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert('Task Successfully Deleted!');
                Clear();
                load_data();
            }
            else {
                alert('Task cannot be Deleted, it is being actively used!');
            }
        }
    });
}

function Clear() {
    $('#txtTaskName').val("");
    $('#txtDescription').val("");
    document.getElementById("chkActive").checked = true;
    $('#txtNumberOfSteps').val("");
    idForUpdate = 0;
    CountChar($('#txtDescription').val(), 500, 'cnt', 'txtDescription');
}