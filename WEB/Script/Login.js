var idForUpdate = 0;
var currentPage = 1;
var Q1ID = '';
var Q2ID = '';
var Q3ID = '';

var Q1A = '';
var Q2A = '';
var Q3A = '';

$(document).ready(function () {

    var txtuser = document.getElementById('txtUserId');
    txtuser.focus();
    $('.select2').select2();
    document.getElementById("btnNextStepper").onclick = function () {
        CheckEmail();
        ValidateEmail();
    };
    document.getElementById("btnPrevStepper").onclick = function () {
        CleaR();
    }

    document.getElementById("InputEmail1").onchange = function () {
        CheckEmail();
    }
    CheckLicense();
    //ResetTheTimer();
    //$('body').mousemove(function () { // or other events
    //    ResetTheTimer();
    //});
    //$('body').keydown(function () { // or other events
    //    ResetTheTimer();
    //});
});

var intervalset;
var MilliSecondsTimeOut = 1 * 1000 * 60;
function ResetTheTimer() {
    clearInterval(intervalset);
    intervalset = window.setInterval(Redirect, MilliSecondsTimeOut);
    console.log('reset');
}
function Redirect() {
    window.location.reload();
}


function CheckLicense() {
    $.ajax({
        async: false,
        type: 'POST',
        url: "Login.aspx/CheckLicense",
        data: "{  }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            if (xml.d == "No License Found.") {
                uploadLicense();
            }
            else if (xml.d == "404") {
                script404();
            }
            else if (xml.d == "alright") {
            } else {
                licensedayCount(xml.d);
            }
        },
        error: function (xhr, status, error) {
            alert(error);
            console.log(xhr);
        }
    });
}

function btnUploadActivation() {
    $(function () {
        var fileUpload = $("#FileUploadActivation").get(0);
        var files = fileUpload.files;
        var test = new FormData();
        for (var i = 0; i < files.length; i++) {
            test.append(files[i].name, files[i]);
        }
        alert(files);
        $.ajax({
            url: "LicenseUpload.ashx?",
            type: "POST",
            contentType: false,
            processData: false,
            data: test,
            success: function (result) {
                alert(result);
                if (result.includes("File Uploaded Successfully", 0)) {

                }
            }
        });

    });
}

function ActivateDialog() {

    $("#modalActivateDialog").modal("show");
}

function ForgotPasswordDialogBox() {
    $("#ForgotPassword").modal("show");
    CleaR();
}
var QArray = new Array();
var isValidEmail = 'False';
function CheckEmail() {
    $.ajax({
        async: false,
        type: 'POST',
        url: "Login.aspx/CheckEmail",
        data: "{ Email :'" + document.getElementById('InputEmail1').value + "',ConnString : '" + document.getElementById('ddlDatabase').value + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (xml) {
            try {
                var i = 1;
                QArray = JSON.parse(xml.d);
                QArray.forEach((QA, index) => {
                    if (QA.Question == 'Invalid Email') {
                        document.getElementById("InputEmail1").style.borderColor = "#FF0000";
                        isValidEmail = 'False';
                    }
                    else {
                        document.getElementById("InputEmail1").style.borderColor = "#00FF00";
                        isValidEmail = 'True';
                        if (i == 1) {
                            Q1A = i + ". " + QA.Question;
                            Q1ID = QA.QuestionID;
                        } else if (i == 2) {
                            Q2A = i + ". " + QA.Question;
                            Q2ID = QA.QuestionID;
                        }
                        else {
                            Q3A = i + ". " + QA.Question;
                            Q3ID = QA.QuestionID;
                        }
                        i += 1;
                    }

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
}


var QArray = new Array();
function ValidateEmail() {
    if (isValidEmail != 'True') {
        //alert('Invalid Email');
        swal({
            title: "Warning",
            text: "Invalid E-mail Address",
            icon: "warning"
        });
    }
    else {
        stepper.next();
        document.getElementById('lblQ1').innerHTML = Q1A;
        document.getElementById('lblQ2').innerHTML = Q2A;
        document.getElementById('lblQ3').innerHTML = Q3A;
    }
    //$.ajax({
    //    async: false,
    //    type: 'POST',
    //    url: "Login.aspx/CheckEmail",
    //    data: "{ Email :'" + document.getElementById('InputEmail1').value + "',ConnString : '" + document.getElementById('ddlDatabase').value + "' }",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (xml) {
    //        try {
    //            var i = 1;
    //            QArray = JSON.parse(xml.d);
    //            QArray.forEach((QA, index) => {
    //                if (QA.Question == 'Invalid Email') {
    //                    alert('Invalid Email');
    //                }
    //                else {
    //                    stepper.next();
    //                    document.getElementById('lblQ' + i).innerHTML = i + ". " + QA.Question;
    //                    if (i == 1) {
    //                        Q1ID = QA.QuestionID;
    //                    } else if (i == 2) {
    //                        Q2ID = QA.QuestionID;
    //                    }
    //                    else {
    //                        Q3ID = QA.QuestionID;
    //                    }
    //                    i += 1;
    //                }

    //            });
    //        } catch (err) {
    //            console.log(err.message);
    //        }
    //    },
    //    error: function (xhr, status, error) {
    //        alert(error);
    //        console.log(xhr);
    //    }
    //});
}


function CheckAnswers() {
    var SecurityQuestions = Q1ID + "|" + Q2ID + "|" + Q3ID;
    var SecurityAnswers = document.getElementById('txtAnswer1').value + "|" + document.getElementById('txtAnswer2').value + "|" + document.getElementById('txtAnswer3').value;
    swal({
        title: "Password reset.",
        text: "Are you sure you want to reset your password?",
        icon: "warning",
        buttons: [
          'No',
          'Yes'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            $(".se-pre-con").fadeIn();
            $.ajax({
                async: false,
                type: 'POST',
                url: "Login.aspx/CheckAnswer",
                data: "{Email:'" + document.getElementById('InputEmail1').value + "',ConnString : '" + document.getElementById('ddlDatabase').value + "',Questions:'" + SecurityQuestions + "',Answers:'" + SecurityAnswers + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    try {
                        if (msg.d == '1|1|1') {
                            swal({
                                title: "Password reset successul!",
                                text: "You've successfully reset your password, Kindly check E-mail.",
                                icon: "success"
                            })
                            //alert("You've successfully reset your password, Kindly check your E-mail."); 
                            $("#ForgotPassword").modal("hide");
                        }
                        else {
                            var alertMsgAns = '';
                            if (msg.d.split('|')[0] == '0') {
                                alertMsgAns += 'Question 1';
                            }
                            if (msg.d.split('|')[1] == '0') {
                                alertMsgAns += '\nQuestion 2';
                            }
                            if (msg.d.split('|')[2] == '0') {
                                alertMsgAns += '\nQuestion 3';
                            }
                            //alert("Answer(s) are incorrect: " + alertMsgAns);
                            swal("Answer(s) are incorrect: ", alertMsgAns, "warning");
                        }

                    } catch (err) {
                        console.log(err.message);
                    }

                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    //alert(error);
                    $(".se-pre-con").hide();
                    console.log(xhr);
                }
            });
        }
    });
}

function CleaR() {
    stepper.previous();
    document.getElementById('InputEmail1').value = '';
    document.getElementById("InputEmail1").style.borderColor = "";
}