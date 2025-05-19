var idForUpdate = 0;
var currentPage = 1;

$(function () {
    LoadControls();
});

function LoadControls() {
    $(".btn").button();
    GetModule();
}

function GetModule() {
    $.ajax({
        type: "POST",
        url: "System/DatabaseBackUpAndRestore.aspx/GetModule",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "wag itago") {
                document.getElementById('restoredb').style.visibility = 'visible';
            }
            else {
                document.getElementById('restoredb').style.visibility = 'hidden';
            }
        }
    });
}
function openConfirmationDialog() {
    $("#restoreDB").modal("show");
}


function backup() {
    var directory = $("#BodyHolder_txtDirectory").val().replace(/[\\]/gi, '.');
    $.ajax({
        type: "POST",
        url: "System/DatabaseBackUpAndRestore.aspx/BackUp",
        data: " { directory : '" + directory + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "success") {
                swal('Success', 'Database Back-Up Successful.', 'success');
            }
            else {
                swal('Error', 'Error occured during back-up', 'error');
            }
        }
    });
}

function restore() {
    var directory = $("#BodyHolder_txtDirectory").val().replace(/[\\]/gi, '.');
    var filess = $('#BodyHolder_FileUpload1').val();
    if (filess == "") {
        swal("", "Select .bak file to upload.", "warning");
    }
    else {
        swal({
            title: "Attention",
            text: "The database file being restored will be for verification and approval. By restoring this database file it replace the current database. An external audit record will be created once approve.",
            icon: "warning",
            reverseButtons: false,
            buttons: [
              'No',
              'Yes'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                $(function () {
                    var fileUpload = $("#BodyHolder_FileUpload1").get(0);
                    var files = fileUpload.files;
                    var test = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        test.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        url: "System/DatabaseRestore.ashx?",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: test,
                        success: function (result) {
                            if (result.includes("File Uploaded Successfully", 0)) {
                                var filename = result.split(':')[1];
                                $.ajax({
                                    type: "POST",
                                    url: "System/DatabaseBackUpAndRestore.aspx/Upload",
                                    data: " { directory : '" + directory + "', filename : '" + filename + "' } ",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        if (msg.d == "success") {
                                            swal("Successful", "Database Restoration successful!", "success");
                                        }
                                        else if (msg.d == "success with approval") {
                                            swal("Successful", "Database Restoration is waiting for approval!", "success");
                                        }
                                        else {
                                            swal('Warning', 'Error during database restoration!', 'warning');
                                        }
                                    }
                                });
                            } else {
                                swal("Warning", result, "warning");
                            }
                        },
                        error: function (err) {
                            swal("Warning", err.statusText, "warning");
                        }
                    });
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}
