$(document).ready(function () {

    $('.select2').select2();
    DisplayUserInfo();
    //$("#btnSave").click(function () {
    //    saveUpdateProfile();
    //});

    $("#btnReset").click(function () {
        ResetProfileFileds();
    });
    //loadQuestion();
    GetSecretQuestion1('');
    $("#ddlQ1").change(function () {
        getQuestion('ddlQ1');
    })
    $("#ddlQ2").change(function () {
        getQuestion('ddlQ2');
    })
    $("#ddlQ3").change(function () {
        getQuestion('ddlQ3');
    })
    $("#ddlQ4").change(function () {
        getQuestion('ddlQ4');
    })
    $("#ddlQ5").change(function () {
        getQuestion('ddlQ5');
    })
    $("#txtPassword").change(function () {
        Password();
    })
    $("#txtConfirmPassword").change(function () {
        ConfirmPassword();
    })

    Load_Questions();

    $('body').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            saveUpdateProfile();
        }
    });
});




function Password() {
    var Password = $('#txtPassword').val();
    var ConfirmPassword = $('#txtConfirmPassword').val();
    if (ConfirmPassword != '') {
        if (Password == ConfirmPassword) {
            document.getElementById("txtPassword").style.borderColor = "";
            document.getElementById("txtConfirmPassword").style.borderColor = "";
        }
        else {
            document.getElementById("txtPassword").style.borderColor = "#FF0000";
        }
    }
}

function ConfirmPassword() {
    var Password = $('#txtPassword').val();
    var ConfirmPassword = $('#txtConfirmPassword').val();
    if (Password != '') {
        if (Password == ConfirmPassword) {
            document.getElementById("txtPassword").style.borderColor = "";
            document.getElementById("txtConfirmPassword").style.borderColor = "";
        }
        else {
            document.getElementById("txtConfirmPassword").style.borderColor = "#FF0000";
        }
    }
}


var tempQ1ValueGlobal = '';
var Q1 = '';
var Q2 = '';
var Q3 = '';
var Q4 = '';
var Q5 = '';
function loadQuestion() {
    $.ajax({
        async: true,
        type: 'POST',
        url: "User_Management/UserProfile.aspx/GetQuestions1",
        data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert("ss");
        },
        error: function (e) {
            alert(e);
            console.log(e);
        }
    });

    alert("ss");
    $.ajax({
        async: true,
        type: 'POST',
        url: "User_Management/UserProfile.aspx/GetQuestions2",
        data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
        },
        error: function (e) {
            console.log(e);
        }
    });

}

function UploadFile(fileUpload) {
    if (fileUpload.value != '') {
    }
}

function getImage(event) {
    console.log(event.target.files[0]);
    $('#BodyHolder_PropertyImage').html("");
    var image = URL.createObjectURL(event.target.files[0]);
    var imagediv = document.getElementById('BodyHolder_PropertyImage');
    var newimg = document.createElement('img');
    newimg.src = image;
    newimg.width = "300";
    imagediv.appendChild(newimg);
}
 

var username = "";
function DisplayUserInfo() {
    $.ajax({
        type: 'POST',
        url: "User_Management/UserProfile.aspx/GetUserInfo",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // 0 EmailAddress
            // 1 UserName
            // 2 Password
            // 3 EmployeeNo
            // 4 Image
            // 5 DepartmentName
            // 6 PositionName
            username = data.d.split('|')[1];
            $('#EmployeeNumber').text(data.d.split('|')[3]);
            $('#BodyHolder_PositionDisplay').text(data.d.split('|')[6]);
            $('#txtEmail').val(data.d.split('|')[0]);
            //$('#txtsignature').val(data.d.split('|')[0]);
            //$('#txtPassword').val(data.d.split('|')[2]);
            //$('#txtConfirmPassword').val(data.d.split('|')[2]);

            $('#txtPasswordhidden').val(data.d.split('|')[2]); //for password confirmation
            $('#BodyHolder_txtsignature').val("");
            $('#BodyHolder_UploadprofilePic').val("");

        },
        error: function (e) {
            alert(e);
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    });
}


function saveUpdateProfile() {
    var EmailAdd = $('#txtEmail').val();
    var PWord = $('#txtPassword').val();
    var Q1 = document.getElementById('ddlQ1').value;
    var Q2 = document.getElementById('ddlQ2').value;
    var Q3 = document.getElementById('ddlQ3').value;
    var Q4 = document.getElementById('ddlQ4').value;
    var Q5 = document.getElementById('ddlQ5').value;

    var Q1A = $('#txtQ1').val();
    var Q2A = $('#txtQ2').val();
    var Q3A = $('#txtQ3').val();
    var Q4A = $('#txtQ4').val();
    var Q5A = $('#txtQ5').val();

    if (Q1 == '0' || Q2 == '0' || Q3 == '0' || Q4 == '0' || Q5 == '0' || Q1A == '' || Q2A == '' || Q3A == '' || Q4A == '' || Q5A == '') {
        var alertMsg = '';
        if (Q1 == '0')
            alertMsg += "\nQuestion 1";
        if (Q2 == '0')
            alertMsg += "\nQuestion 2";
        if (Q3 == '0')
            alertMsg += "\nQuestion 3";
        if (Q4 == '0')
            alertMsg += "\nQuestion 4";
        if (Q5 == '0')
            alertMsg += "\nQuestion 5";

        if (Q1A == "")
            alertMsg += "\nAnswer 1 ";
        if (Q2A == "")
            alertMsg += "\nAnswer 2 ";
        if (Q3A == "")
            alertMsg += "\nAnswer 3 ";
        if (Q4A == "")
            alertMsg += "\nAnswer 4 ";
        if (Q5A == "")
            alertMsg += "\nAnswer 5 ";

        swal("Fill-out all the required field(s): ", alertMsg, "warning")
    }
    else if ($('#txtPassword').val() != $('#txtConfirmPassword').val()) {
        swal("Password", "The Password confirmation does not match", "warning");
    }
    else {

        var sig = "";
        var Imahe = "";


        if ($('#BodyHolder_UploadprofilePic').val() != "") {
            Imahe = "." + $('#BodyHolder_UploadprofilePic').val().split(".")[1];
        }
        var SecurityQuestions = Q1 + ";" + Q2 + ";" + Q3 + ";" + Q4 + ";" + Q5;
        var SecurityAnswers = Q1A + ";" + Q2A + ";" + Q3A + ";" + Q4A + ";" + Q5A;
        $(function () {
            var fileUpload = $("#BodyHolder_UploadprofilePic").get(0);
            var files = fileUpload.files;
            var test = new FormData();
            for (var i = 0; i < files.length; i++) {
                test.append(files[i].name, files[i]);
            }
            $.ajax({
                url: "User_Management/UserUpload.ashx?username=" + username + Imahe,
                type: "POST",
                contentType: false,
                processData: false,
                data: test,
                success: function (result) {
                    if (result.includes("File Uploaded Successfully", 0)) {
                        $.ajax({
                            async: true,
                            type: 'POST',
                            url: "User_Management/UserProfile.aspx/saveProfile",
                            data: "{email:'" + EmailAdd + "', Profile:'" + Imahe + "', signature:'" + sig + "', password:'" + PWord + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == "succesful") {
                                    $.ajax({
                                        async: true,
                                        type: 'POST',
                                        url: "User_Management/UserProfile.aspx/saveSecurityQandA",
                                        data: "{securityquestions: '" + SecurityQuestions + "', securityanswers: '" + SecurityAnswers + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (data) {
                                            swal("Success", "Successfully updated profile! You will be logout from the system.", "success").then(function () {
                                                LogOut();
                                            });
                                            //swal("Success", "Successfully saved!", "success");
                                           // swal("Success", "User Profle Successfully Save! Please Log-out and then Log-in again for user profile to take effect.", "success");
                                        },
                                        error: function (e) {
                                            console.log("There was an error with your request...");
                                            console.log("error: " + JSON.stringify(e));
                                        }
                                    });

                                }
                                else {
                                    swal("Password Policy", data.d, "warning");
                                }
                            },
                            error: function (e) {
                                console.log("There was an error with your request...");
                                console.log("error: " + JSON.stringify(e));
                            }
                        });
                    }
                    else {
                        $('#BodyHolder_PropertyImage').html("");
                        swal("Warning", result, "warning");
                    }
                },
                error: function (err) {
                    swal("Warning", err.statusText, "warning");
                }
            });
        });


        DisplayUserInfo();
        //swal("Enter current Password to confirm:",
        //    {
        //        content: {
        //            element: "input",
        //            attributes: {
        //                placeholder: "Type your password",
        //                type: "password",
        //            },
        //        },
        //    })
        //.then((value) => {
        //    $.ajax({
        //        type: 'POST',
        //        url: "User_Management/UserProfile.aspx/passwordchecking",
        //        data: "{inputPass:'" + value + "', hiddenPass:'" + $('#txtPasswordhidden').val() + "' }",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            if (data.d == "") {
        //                swal('password missmatch');
        //            }
        //            else {
        //                //swal('password match');
        //                PWord = data.d;
        //                SaveInfo(EmailAdd, PWord, Imahe, sig);
        //            }
        //        },
        //        error: function (e) {
        //            alert(e);
        //            console.log("There was an error with your request...");
        //            console.log("error: " + JSON.stringify(e));
        //        }
        //    });
        //});s
    }
}

function ResetProfileFileds() {
    DisplayUserInfo();
    $('#BodyHolder_PropertyImage').html("");
}

//function SaveInfo(email, pass, DP, signature)
//{//saveProfile
//    $.ajax({
//        type: 'POST',
//        url: "User_Management/UserProfile.aspx/saveProfile",
//        data: "{email:'" + email + "', Profile:'" + DP + "', signature:'" + signature + "', password:'" + pass + "' }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            if (data.d == "succesfull") {
//                swal("Success", "Successfully saved!", "success");
//                $('#BodyHolder_btnUpload').click();
//            }
//            //swal("Success", "Your profile was update successfully!", "success");           
//        },
//        error: function (e) {
//            alert(e);
//            console.log("There was an error with your request...");
//            console.log("error: " + JSON.stringify(e));
//        }
//    })

//}


var element1 = '';
var element1Value = '';
function getQuestion(ddlQ) {
    if (document.getElementById(ddlQ).value != '0') {
        GetSecretQuestion1(ddlQ);
    }
}




//var Q1Array = new Array();
//var Q2Array = new Array();
//function GetSecretQuestion1(ddlQ) {
//    var tempQ3Value = document.getElementById('ddlQ3').value;
//    if (ddlQ != 'ddlQ1') {
//        var tempQ1Value = document.getElementById('ddlQ1').value;
//        var ddlQ1 = $('#ddlQ1');
//        ddlQ1.empty();
//        ddlQ1.append($('<option>', {
//            value: '0',
//            text: '--- Select Question ---',
//            rowindex: -1
//        }));
//        $.ajax({
//            async: false,
//            type: 'POST',
//            url: 'User_Management/UserProfile.aspx/GetQuestions1',
//            data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (xml) {
//                try {
//                    Q1Array = JSON.parse(xml.d);
//                    Q1Array.forEach((Q1, index) => {
//                        ddlQ1.append($('<option>', {
//                            value: Q1.QuestionID,
//                            text: Q1.Question,
//                            rowindex: index
//                        }));
//                    });
//                } catch (err) {
//                    console.log(err.message);
//                }
//            },
//            error: function (xhr, status, error) {
//                alert(error);
//                console.log(xhr);
//            }
//        });
//        if (tempQ1Value != '0' && ddlQ != '') {
//            document.getElementById('ddlQ1').value = tempQ1Value;

//        }
//    }
//    if (ddlQ != 'ddlQ2') {
//        var ddlQ2 = $('#ddlQ2');
//        var tempQ2Value = $('#ddlQ2').val();
//        ddlQ2.empty();
//        ddlQ2.append($('<option>', {
//            value: '0',
//            text: '--- Select Question ---',
//            rowindex: -1
//        }));
//        $.ajax({
//            async: false,
//            type: 'POST',
//            url: 'User_Management/UserProfile.aspx/GetQuestions2',
//            data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (xml) {
//                try {
//                    Q2Array = JSON.parse(xml.d);
//                    Q2Array.forEach((Q1, index) => {
//                        ddlQ2.append($('<option>', {
//                            value: Q1.QuestionID,
//                            text: Q1.Question,
//                            rowindex: index
//                        }));

//                    });
//                    alert(tempQ2Value);
//                    $('#ddlQ2').val(tempQ2Value).trigger("change");
//                } catch (err) {
//                    console.log(err.message);
//                }
//            },
//            error: function (xhr, status, error) {
//                //alert(error);
//                console.log(xhr);
//            }
//        });
//        if (tempQ2Value != '0' && ddlQ != '') {
//            //var numbb = Number(tempQ2Value);
//            //alert(numbb);
//            //document.getElementById('ddlQ2').value() = tempQ2Value;
//            alert(tempQ2Value);

//        }

//    }

//}


var Q1Array = new Array();
var Q2Array = new Array();
var Q3Array = new Array();
var Q4Array = new Array();
var Q5Array = new Array();
function GetSecretQuestion1(ddlQ) {
    var tempQ1Value = document.getElementById('ddlQ1').value;
    var tempQ2Value = document.getElementById('ddlQ2').value;
    var tempQ3Value = document.getElementById('ddlQ3').value;
    var tempQ4Value = document.getElementById('ddlQ4').value;
    var tempQ5Value = document.getElementById('ddlQ5').value;

    if (ddlQ != 'ddlQ1') {
        var ddlQ1 = $('#ddlQ1');
        ddlQ1.empty();
        ddlQ1.append($('<option>', {
            value: '0',
            text: '--- Select Question ---',
            rowindex: -1
        }));
        $.ajax({
            async: false,
            type: 'POST',
            url: 'User_Management/UserProfile.aspx/GetQuestions1',
            data: "{Q1:'" + document.getElementById('ddlQ1').value + "',Q2:'" + document.getElementById('ddlQ2').value + "',Q3:'" + document.getElementById('ddlQ3').value + "',Q4:'" + document.getElementById('ddlQ4').value + "',Q5:'" + document.getElementById('ddlQ5').value + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (xml) {
                try {
                    Q1Array = JSON.parse(xml.d);
                    Q1Array.forEach((Q1A, index) => {
                        ddlQ1.append($('<option>', {
                            value: Q1A.QuestionID,
                            text: Q1A.Question,
                            rowindex: index
                        }));
                    });
                } catch (err) {
                    console.log(err.message);
                }
            },
            error: function (xhr, status, error) {
                alert(error);
                console.log(xhr);
            }
        });
        //alert(tempQ1Value);
        if (tempQ1Value != '0' && ddlQ != '') {

            document.getElementById('ddlQ1').value = tempQ1Value;
        }
    }
    if (ddlQ != 'ddlQ2') {
        console.log(tempQ2Value);
        var ddlQ1 = $('#ddlQ2');
        ddlQ1.empty();
        ddlQ1.append($('<option>', {
            value: '0',
            text: '--- Select Question ---',
            rowindex: -1
        }));
        $.ajax({
            async: false,
            type: 'POST',
            url: 'User_Management/UserProfile.aspx/GetQuestions2',
            data: "{Q1:'" + document.getElementById('ddlQ1').value + "',Q2:'" + document.getElementById('ddlQ2').value + "',Q3:'" + document.getElementById('ddlQ3').value + "',Q4:'" + document.getElementById('ddlQ4').value + "',Q5:'" + document.getElementById('ddlQ5').value + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (xml) {
                try {
                    Q2Array = JSON.parse(xml.d);
                    Q2Array.forEach((Q2A, index) => {
                        ddlQ1.append($('<option>', {
                            value: Q2A.QuestionID,
                            text: Q2A.Question,
                            rowindex: index
                        }));
                    });
                } catch (err) {
                    console.log(err.message);
                }
            },
            error: function (xhr, status, error) {
                alert(error);
                console.log(xhr);
            }
        });
        if (tempQ2Value != '0' && ddlQ != '') {
            document.getElementById('ddlQ2').value = tempQ2Value;
        }
    }

    if (ddlQ != 'ddlQ3') {
        var ddlQ1 = $('#ddlQ3');
        ddlQ1.empty();
        ddlQ1.append($('<option>', {
            value: '0',
            text: '--- Select Question ---',
            rowindex: -1
        }));
        $.ajax({
            async: false,
            type: 'POST',
            url: 'User_Management/UserProfile.aspx/GetQuestions3',
            data: "{Q1:'" + document.getElementById('ddlQ1').value + "',Q2:'" + document.getElementById('ddlQ2').value + "',Q3:'" + document.getElementById('ddlQ3').value + "',Q4:'" + document.getElementById('ddlQ4').value + "',Q5:'" + document.getElementById('ddlQ5').value + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (xml) {
                try {
                    Q3Array = JSON.parse(xml.d);
                    Q3Array.forEach((Q3A, index) => {
                        ddlQ1.append($('<option>', {
                            value: Q3A.QuestionID,
                            text: Q3A.Question,
                            rowindex: index
                        }));
                    });
                } catch (err) {
                    console.log(err.message);
                }
            },
            error: function (xhr, status, error) {
                alert(error);
                console.log(xhr);
            }
        });
        if (tempQ3Value != '0' && ddlQ != '') {
            document.getElementById('ddlQ3').value = tempQ3Value;
        }
    }

    if (ddlQ != 'ddlQ4') {
        var ddlQ1 = $('#ddlQ4');
        ddlQ1.empty();
        ddlQ1.append($('<option>', {
            value: '0',
            text: '--- Select Question ---',
            rowindex: -1
        }));
        $.ajax({
            async: false,
            type: 'POST',
            url: 'User_Management/UserProfile.aspx/GetQuestions4',
            data: "{Q1:'" + document.getElementById('ddlQ1').value + "',Q2:'" + document.getElementById('ddlQ2').value + "',Q3:'" + document.getElementById('ddlQ3').value + "',Q4:'" + document.getElementById('ddlQ4').value + "',Q5:'" + document.getElementById('ddlQ5').value + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (xml) {
                try {
                    Q4Array = JSON.parse(xml.d);
                    Q4Array.forEach((Q2A, index) => {
                        ddlQ1.append($('<option>', {
                            value: Q2A.QuestionID,
                            text: Q2A.Question,
                            rowindex: index
                        }));
                    });
                } catch (err) {
                    alert('ssa');
                    console.log(err.message);
                }
            },
            error: function (xhr, status, error) {
                alert(error);
                console.log(xhr);
            }
        });
        if (tempQ4Value != '0' && ddlQ != '') {
            document.getElementById('ddlQ4').value = tempQ4Value;
        }
    }

    if (ddlQ != 'ddlQ5') {
        var ddlQ1 = $('#ddlQ5');
        ddlQ1.empty();
        ddlQ1.append($('<option>', {
            value: '0',
            text: '--- Select Question ---',
            rowindex: -1
        }));
        $.ajax({
            async: false,
            type: 'POST',
            url: 'User_Management/UserProfile.aspx/GetQuestions5',
            data: "{Q1:'" + document.getElementById('ddlQ1').value + "',Q2:'" + document.getElementById('ddlQ2').value + "',Q3:'" + document.getElementById('ddlQ3').value + "',Q4:'" + document.getElementById('ddlQ4').value + "',Q5:'" + document.getElementById('ddlQ5').value + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (xml) {
                try {
                    Q5Array = JSON.parse(xml.d);
                    Q5Array.forEach((Q2A, index) => {
                        ddlQ1.append($('<option>', {
                            value: Q2A.QuestionID,
                            text: Q2A.Question,
                            rowindex: index
                        }));
                    });
                } catch (err) {
                    alert('ssa');
                    console.log(err.message);
                }
            },
            error: function (xhr, status, error) {
                alert(error);
                console.log(xhr);
            }
        });
        if (tempQ5Value != '0' && ddlQ != '') {
            document.getElementById('ddlQ5').value = tempQ5Value;
        }
    }

}


//var Q1Array = new Array();
//function GetSecretQuestion1(ddlQ) {
//    if (ddlQ != 'ddlQ1') {
//        var ddlQ1 = $('#ddlQ1');
//        var tempQ1Value = $('#ddlQ1').val();
//        ddlQ1.empty();
//        ddlQ1.append($('<option>', {
//            value: '0',
//            text: '--- Select Question ---',
//            rowindex: -1
//        }));
//        $.ajax({
//            async: false,
//            type: 'POST',
//            url: 'User_Management/UserProfile.aspx/GetQuestions1',
//            data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (xml) {
//                try {
//                    Q1Array = JSON.parse(xml.d);
//                    Q1Array.forEach((Q1, index) => {
//                        ddlQ1.append($('<option>', {
//                            value: Q1.QuestionID,
//                            text: Q1.Question,
//                            rowindex: index
//                        }));
//                    });
//                } catch (err) {
//                    console.log(err.message);
//                }
//            },
//            error: function (xhr, status, error) {
//                alert(error);
//                console.log(xhr);
//            }
//        });
//        if (tempQ1Value != '0' && ddlQ != '') {
//            $('#ddlQ1').val(tempQ1Value).trigger('change'); //= 5;
//        }
//    }
//    if (ddlQ != 'ddlQ2') {
//            var ddlQ1 = $('#ddlQ2');
//            var tempQ1Value = document.getElementById('ddlQ2').value;
//            ddlQ1.empty();
//            ddlQ1.append($('<option>', {
//                value: '0',
//                text: '--- Select Question ---',
//                rowindex: -1
//            }));
//            $.ajax({
//                async: false,
//                type: 'POST',
//                url: 'User_Management/UserProfile.aspx/GetQuestions2',
//                data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (xml) {
//                    try {
//                        Q1Array = JSON.parse(xml.d);
//                        Q1Array.forEach((Q1, index) => {
//                            ddlQ1.append($('<option>', {
//                                value: Q1.QuestionID,
//                                text: Q1.Question,
//                                rowindex: index
//                            }));
//                        });
//                    } catch (err) {
//                        console.log(err.message);
//                    }
//                },
//                error: function (xhr, status, error) {
//                    alert(error);
//                    console.log(xhr);
//                }
//            });
//            if (tempQ1Value != '0' && ddlQ != '') {
//                document.getElementById('ddlQ2').value = tempQ1Value;
//            }
//    }
//    //if (ddlQ != 'ddlQ3') {
//    //    var ddlQ1 = $('#ddlQ3');
//    //    var tempQ1Value = document.getElementById('ddlQ3').value;
//    //    ddlQ1.empty();
//    //    ddlQ1.append($('<option>', {
//    //        value: '0',
//    //        text: '--- Select Question ---',
//    //        rowindex: -1
//    //    }));
//    //    $.ajax({
//    //        async: false,
//    //        type: 'POST',
//    //        url: 'User_Management/UserProfile.aspx/GetQuestions3',
//    //        data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//    //        contentType: "application/json; charset=utf-8",
//    //        dataType: "json",
//    //        success: function (xml) {
//    //            try {
//    //                Q1Array = JSON.parse(xml.d);
//    //                Q1Array.forEach((Q1, index) => {
//    //                    ddlQ1.append($('<option>', {
//    //                        value: Q1.QuestionID,
//    //                        text: Q1.Question,
//    //                        rowindex: index
//    //                    }));
//    //                });
//    //            } catch (err) {
//    //                console.log(err.message);
//    //            }
//    //        },
//    //        error: function (xhr, status, error) {
//    //            alert(error);
//    //            console.log(xhr);
//    //        }
//    //    });
//    //    if (tempQ1Value != '0' && ddlQ != '') {
//    //        document.getElementById('ddlQ3').value = tempQ1Value;
//    //    }
//    //}
//    //if (ddlQ != 'ddlQ4') {
//    //    var ddlQ1 = $('#ddlQ4');
//    //    var tempQ1Value = document.getElementById('ddlQ4').value;
//    //    ddlQ1.empty();
//    //    ddlQ1.append($('<option>', {
//    //        value: '0',
//    //        text: '--- Select Question ---',
//    //        rowindex: -1
//    //    }));
//    //    $.ajax({
//    //        async: false,
//    //        type: 'POST',
//    //        url: 'User_Management/UserProfile.aspx/GetQuestions4',
//    //        data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//    //        contentType: "application/json; charset=utf-8",
//    //        dataType: "json",
//    //        success: function (xml) {
//    //            try {
//    //                Q1Array = JSON.parse(xml.d);
//    //                Q1Array.forEach((Q1, index) => {
//    //                    ddlQ1.append($('<option>', {
//    //                        value: Q1.QuestionID,
//    //                        text: Q1.Question,
//    //                        rowindex: index
//    //                    }));
//    //                });
//    //            } catch (err) {
//    //                console.log(err.message);
//    //            }
//    //        },
//    //        error: function (xhr, status, error) {
//    //            alert(error);
//    //            console.log(xhr);
//    //        }
//    //    });
//    //    if (tempQ1Value != '0' && ddlQ != '') {
//    //        document.getElementById('ddlQ4').value = tempQ1Value;
//    //    }
//    //}
//    //if (ddlQ != 'ddlQ5') {
//    //    var ddlQ1 = $('#ddlQ5');
//    //    var tempQ1Value = document.getElementById('ddlQ5').value;
//    //    ddlQ1.empty();
//    //    ddlQ1.append($('<option>', {
//    //        value: '0',
//    //        text: '--- Select Question ---',
//    //        rowindex: -1
//    //    }));
//    //    $.ajax({
//    //        async: false,
//    //        type: 'POST',
//    //        url: 'User_Management/UserProfile.aspx/GetQuestions1',
//    //        data: "{Q1:'" + Q1 + "',Q2:'" + Q2 + "',Q3:'" + Q3 + "',Q4:'" + Q4 + "',Q5:'" + Q5 + "' }",
//    //        contentType: "application/json; charset=utf-8",
//    //        dataType: "json",
//    //        success: function (xml) {
//    //            try {
//    //                Q1Array = JSON.parse(xml.d);
//    //                Q1Array.forEach((Q1, index) => {
//    //                    ddlQ1.append($('<option>', {
//    //                        value: Q1.QuestionID,
//    //                        text: Q1.Question,
//    //                        rowindex: index
//    //                    }));
//    //                });
//    //            } catch (err) {
//    //                console.log(err.message);
//    //            }
//    //        },
//    //        error: function (xhr, status, error) {
//    //            alert(error);
//    //            console.log(xhr);
//    //        }
//    //    });
//    //    if (tempQ1Value != '0' && ddlQ != '') {
//    //        document.getElementById('ddlQ5').value = tempQ1Value;
//    //    }
//    //}
//}

var QArray = new Array();
function Load_Questions() {
    $.ajax({
        async: false,
        type: 'POST',
        url: 'User_Management/UserProfile.aspx/GetSecurityQuestion',
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                var i = 1;
                QArray = JSON.parse(xml.d);
                QArray.forEach((Q1A, index) => {
                    console.log(Q1A.Answer);
                    console.log(Q1A.QuestionID);
                    $('#txtQ' + i).val(Q1A.Answer);
                    $('#ddlQ' + i).val(Q1A.QuestionID);

                    tempQ1ValueGlobal = '';

                    i += 1;
                    //var i = Q1A.length;
                    //var txtAnswers = document.getElementsByClassName('txtA');
                    //var txtQuestions = document.getElementsByClassName('ddlQ');

                    //for (i in txtQuestions) {
                    //    $('#ddlQ' + i).val(Q1A.QuestionID).trigger('change');
                    //}

                    //for (i in txtAnswers) {
                    //    $('#txtQ' + i).val(Q1A.Answer).trigger('change');
                    //}
                    //i -= 1;
                });
            } catch (err) {
                console.log(err.message);
            }
        },
        error: function (xhr, status, error) {
            alert(error);
            console.log(xhr);
        }
    });
    //alert(tempQ1Value);
    //if (tempQ1Value != '0' && ddlQ != '') {
    //    document.getElementById('ddlQ1').value = temp

    //    Q1Value;
    //}
}