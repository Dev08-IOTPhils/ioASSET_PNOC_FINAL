var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblTagType").DataTable({
        "scrollX": true
    });
    load_data();
});

function load_data() {
    $.ajax({
        type: 'POST',
        url: "TagType.aspx/getTagType",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tagtype').hide();
            $('#tagtype').innerHTML = "";
            $('#tagtype').fadeIn(200);

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
        $('#tblTagType').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "TagType" },
                { "data": "Description" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tag Type'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tag Type'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tag Type'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Tag Type'
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

        $('#tblTagType').off('click');
    } catch (e) {
        var table = $('#tblTagType').DataTable();
        table
            .clear()
            .draw();
    }
}

function LoadControls() {
    $(".btn").button();

    $('input[type="checkbox"].minimal').iCheck({
        checkboxClass: 'icheckbox_flat-red',
        checked: true
    })
}

function addTagTypeDialogBox() {
    Clear();
    document.getElementById("chkActive").checked = true;
    $("#addTagTypeDialogBox").modal("show");
}

function SaveTagType() {
    var cate = ReplaceStr1($('#txtTagType').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescription').val().replace(/  +/g, ' '));
    var alertMsg;
    if ($('#txtTagType').val().trim() == '' || $('#txtDescription').val().trim() == '') {
        alertMsg = 'Fill-out all the required field(s): ';

        if ($('#txtTagType').val() == '')
            alertMsg += '\nTagType';
        if ($('#txtDescription').val() == '')
            alertMsg += '\nDescription';
        alert(alertMsg);
    }
    else {
        if (confirm("Are you sure you want to save this TagType?") == false)
            return false;
        $.ajax({
            type: "POST",
            url: "TagType.aspx/SaveTagType",
            data: " { id : '" + idForUpdate + "' , TagType : '" + cate + "', description :'" + desc + "', active:'" + document.getElementById("chkActive").checked + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == 0) {
                    load_data();
                    $("#addTagTypeDialogBox").modal("hide");
                    alert('Tag Type Successfully Saved!');
                    Clear();
                }
                else if (msg.d == 1) {
                    load_data();
                    $("#addTagTypeDialogBox").modal("hide");
                    alert('Tag Type Successfully Modified!');
                    Clear();
                }
                else {
                    load_data();
                    $("#addTagTypeDialogBox").modal("hide");
                    alert('Tag Type Already Exist!');
                    Clear();
                }
            }
        });
    }
}

function GetTagTypeID(id) {
    idForUpdate = id;
    $("#addTagTypeDialogBox").modal("show");
    $.ajax({
        type: "POST",
        url: "TagType.aspx/TagTypebyID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtTagType').val(msg.d.split('|')[1]);
            $('#txtDescription').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
        }
    });
}

function deleteTagType(id) {
    idForUpdate = id;
    $("#deleteTagType").modal("show");
}

function DeleteTagType() {
    $("#deleteTagType").modal("hide");
    $.ajax({
        type: "POST",
        url: "TagType.aspx/DeleteTagType",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            load_data();
            alert('Tag Type Successfully Deleted!');
            Clear();
        }
    });
}

function Clear() {
    $('#txtTagType').val("");
    $('#txtDescription').val("");
    document.getElementById("chkActive").checked = true;
    idForUpdate = 0;
}