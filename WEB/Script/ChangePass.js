var idForUpdate = 0;
var currentPage = 1;

$(function () {
    LoadControls();
    Mousetrap.bind("g h e g h e", function () {
        setTimeout(function () {
            $("#myModal1").modal("show");
        }, 0);
    })
    Mousetrap.bind("d e b e l e n", function () {
        setTimeout(function () {
            $("#myModal").modal("show");
        }, 0);
    })
});

function LoadControls() {
    $(".btn").button();

}

function changePassDialog() {

    //window.location.replace("http://localhost:57131/Dashboard/Dashboard.aspx");
    ClearPass();
    $("#changePassDialog").modal("show");
}


function ChangePassword() {
    var currentLocation = window.location;
    var loc = currentLocation.pathname.split('/')[2];

    //alert(currentLocation);
    var pa = $('#txtPasswordM').val();
    var pas = pa.replace("'", "`");
    var pass = pas.replace("\\", "");
    var ne = $('#txtNewPassword').val();
    var newp = ne.replace("'", "`");
    var newpa = newp.replace("\\", "");
    var co = $('#txtConfirmPassword').val();
    var con = co.replace("'", "`");
    var conf = con.replace("\\", "");

    var alertMsg;

    if ($('#txtPasswordM').val().trim() == '' || $('#txtNewPassword').val().trim() == '' || $('#txtConfirmPassword').val().trim() == '') {
        alertMsg = '';

        if ($('#txtPasswordM').val().trim() == '')
            alertMsg += '\nPassword';
        if ($('#txtNewPassword').val().trim() == '')
            alertMsg += '\nNew Password';
        if ($('#txtConfirmPassword').val().trim() == '')
            alertMsg += '\nConfirm Password';
        swal("Fill-out all the required field(s): ", alertMsg, "warning");
    }
    else if (ne != co) {
        //alert("Password do not match.");
        swal("Error", "Password do not match.", "error");
    }
    else {
        $.ajax({
            type: "POST",
            url: loc + "/ChangePassword",
            data: " { password : '" + pass + "' ,newpassword : '" + newpa + "', confirmpassword :'" + conf + "'} ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "success") {
                    $("#changePassDialog").modal("hide");
                    //alert('Password Successfully Changed!');
                    swal("Successful", "Password Successfully Changed!", "success");
                    ClearPass();
                }
                else if (msg.d == "Incorrect Password") {
                    //alert('Password is Incorrect!');
                    swal("Error", "Password is Incorrect!", "error");
                }
                else {
                    //alert("Password must contain:\n\n*8 Characters Minimum\n*Upper Case\n*Lower Case\n*Numeric characters\n*Non-alphanumeric character");
                    swal("Warning", "Password must contain:\n\n*8 Characters Minimum\n*Upper Case\n*Lower Case\n*Numeric characters\n*Non-alphanumeric character", "warning");
                }
            }
        });
    }
}

function ClearPass() {
    $('#txtPasswordM').val("");
    $('#txtNewPassword').val("");
    $('#txtConfirmPassword').val("");
}
