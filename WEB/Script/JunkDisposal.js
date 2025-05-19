var idForUpdate = 0;
var currentPage = 1;
var JDid = 0;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblJunkDisposal").DataTable({
        "scrollX": true,
        dom: 'Brftip',
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
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
});



function load_data() {
    $.ajax({
        type: 'POST',
        url: "Custodian_Operation_Module/JunkDisposal.aspx/GetJD",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#junkdisposal').hide();
            $('#junkdisposal').innerHTML = "";
            $('#junkdisposal').fadeIn(200);

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
        $('#tblJunkDisposal').DataTable({
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
                { "data": "TransactionNo" },
                { "data": "TransactionBy" },
                { "data": "ApprovedBy" },
                { "data": "Remarks" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Junk Disposal'
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

        $('#tblJunkDisposal').off('click');

        $('#tblJunkDisposal').on('click', 'button.btn_Member', function (e) {
            e.preventDefault();
        });
        $('#tblJunkDisposal').on('click', 'button.btn_Module', function (e) {
            e.preventDefault();
        });
        $('#tblJunkDisposal').on('click', 'button.btn_Edit', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblJunkDisposal').DataTable();
        table
            .clear()
            .draw();
    }
}


function addTransaction() {
    JDid = 0;
    $('#BodyHolder_ddlModeOfDisposal').val("--- Select Mode of Disposal ---").trigger('change');
    $('#BodyHolder_ddlApprover').val("--- Select Approver ---").trigger('change');
    $('#BodyHolder_txtRemarks').val("");
    document.getElementById("BodyHolder_ddlModeOfDisposal").disabled = false;
    document.getElementById("BodyHolder_ddlApprover").disabled = false;

    $("#addTransDialogBox").modal("show");

}



function SaveJD() {
    var alertMsg = '';

    var Mod = $('#BodyHolder_ddlModeOfDisposal').val();
    var appr = $('#BodyHolder_ddlApprover').val();
    var remarks = $('#BodyHolder_txtRemarks').val();

    if (Mod == "--- Select Mode of Disposal ---" || appr == "--- Select Approver ---" || remarks == "") {
        alertMsg = " ";
        if (Mod == "--- Select Mode of Disposal ---")
            alertMsg += "\nMode Of Disposal";
        if (appr == "--- Select Approver ---")
            alertMsg += "\nApproval";
        if (remarks == "")
            alertMsg += "\nRemarks";

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
        //alert(alertMsg);
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this transaction?",
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
                    url: "Custodian_Operation_Module/JunkDisposal.aspx/SaveJD",
                    data: "{ JDid: '" + JDid + "',  Mod: '" + Mod + "', approver: '" + appr + "', remarks: '" + remarks + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "0") {
                            load_data();
                            $("#addTransDialogBox").modal("hide");
                            swal("Successful", "Transaction Successfully Saved!", "success");
                        }
                        else {
                            load_data();
                            $("#addTransDialogBox").modal("hide");
                            swal("Successful", "Transaction Successfully Edited!", "success");
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

function CompleteJD(TransactionNo) {
    swal({
        title: "Add Property?",
        text: "Are you sure you want to complete this transaction?",
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
                url: "Custodian_Operation_Module/JunkDisposal.aspx/CompleteJD",
                data: " { TransactionNo: '" + TransactionNo + "' } ",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    swal("Successful", "Transaction Successfully Completed!", "success");
                    load_data();
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}


function DownloadTemplateJD() {
    var fileName = 'Appendix-65-WMR-PNOC.xlsx';
    $.ajax({
        type: "POST",
        url: "Custodian_Operation_Module/JunkDisposal.aspx/DownloadTemplate",
        data: " {fileName : '" + fileName + "' } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //Convert Base64 string to Byte Array.
            var bytes = Base64ToBytes(msg.d);

            //Convert Byte Array to BLOB.
            var blob = new Blob([bytes], { type: "application/octetstream" });

            //Check the Browser type and download the File.
            var isIE = false || !!document.documentMode;
            if (isIE) {
                window.navigator.msSaveBlob(blob, fileName);
            } else {
                var url = window.URL || window.webkitURL;
                link = url.createObjectURL(blob);
                var a = $("<a />");
                a.attr("download", fileName);
                a.attr("href", link);
                $("body").append(a);
                a[0].click();
                $("body").remove(a);
            }
        }
    });
}


function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
}


function editTransaction(Jdid,mod, rmks, apr) {
    JDid = Jdid;
    $('#BodyHolder_ddlModeOfDisposal').val(mod).trigger('change');
    $('#BodyHolder_ddlApprover').val(apr).trigger('change');
    $('#BodyHolder_txtRemarks').val(rmks);

    document.getElementById("BodyHolder_ddlModeOfDisposal").disabled = true;
    document.getElementById("BodyHolder_ddlApprover").disabled = true;

    $("#addTransDialogBox").modal("show");

}
