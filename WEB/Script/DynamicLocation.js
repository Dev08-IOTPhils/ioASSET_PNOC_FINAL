var idForUpdate = 0;
var currentPage = 1;

$(function () {
    LoadControls();
});

function LoadControls() {
    $(".btn").button();
    $('#valuehidden').hide();
    $(".checkboxkoto").change(function () {
        if (this.checked) {
            $('#valuehidden').fadeIn();
        }
        else {
            $('#valuehidden').hide();
        }
    });
}

function addLocationDialogBox() {
    document.getElementById("chkActive").checked = true;
    $("#addLocationDialogBox").modal("show");
}



function SaveLocation() {
    var location = ReplaceStr1($('#txtLocation').val().trim());
    var description = ReplaceStr1($('#txtDescription').val().trim());
    var value = $('#txtValue').val();

    if ($('#txtValue').val() == "")
        value = 0;


    var alertMsg;
    if ($('#txtLocation').val().trim() == '' || $('#txtDescription').val().trim() == '' ) {
        alertMsg = 'Fill-out all the required field(s): ';
        if ($('#txtLocation').val().trim() == '')
            alertMsg += '\nLocation ';
        if ($('#txtDescription').val().trim() == '')
            alertMsg += '\nDescription';
        alert(alertMsg);
    }
   
    else {
        $.ajax({
            type: "POST",
            url: "DynamicLocation.aspx/SaveDynamicLocation",
            data: "{locationid: '" + idForUpdate + "', location: '" + location + "',description: '" + description + "',parentlocationid: '" + $("[id*='BodyHolder_ddlParentLocation'] :selected").val() + "', active: '" + document.getElementById("chkActive").checked + "', value: '"+value+"' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#BodyHolder_LocationList').hide();
                if (msg.d == '0') {
                    alert("Location Successfully Saved!");
                    window.location.reload();
                    //$('#BodyHolder_LocationList').html(msg.d.split('|')[0]);
                    //$("#addModuleDialogBox").modal("hide");
                    //Clear();
                }
                else if (msg.d == '1') {
                    alert("Location Successfully Modified!");
                    window.location.reload();
                }
                else {

                    alert("Location Already Exists!");
                }

                $('#BodyHolder_LocationList').fadeIn(200);
            }
        });

    }
}

function AddLocationByID(locationid) {
    $('#BodyHolder_ddlParentLocation').val(locationid);
    document.getElementById("chkActive").checked = true;
    $("#addLocationDialogBox").modal("show");
}