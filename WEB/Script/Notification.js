$(document).ready(function () {
    

    $('[name=Notif]').click(function () {
        //alert('clicked div');
        //console.log('div click aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa');
        var valueis = $(this).attr('value');
        alert(valueis);
        //readNotification(valueis);
    });
    
    GetNotification();
    readAll();
    checkuserRole();
    UsrInfNotif()
})

function UsrInfNotif() {

    
    $.ajax({
        type: "POST",
        url:"Home/Notification.aspx/GetUserInfo",
        data: " { } ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            usrinfnotif = msg.d;
            var admin1 = usrinfnotif.split('|')[0];
            var aprvr1 = usrinfnotif.split('|')[1];
            var cstdn1 = usrinfnotif.split('|')[4];

            var Li1 = document.getElementById("Li1");
            var a1 = document.getElementById("a1");
            var a2 = document.getElementById("a2");
            var a3 = document.getElementById("a3");
            var a4 = document.getElementById("a4");
            var a5 = document.getElementById("a5");
            var a6 = document.getElementById("a6");
            var a7 = document.getElementById("a7");
            var a8 = document.getElementById("a8");
            var a9 = document.getElementById("a9");
            var a10 = document.getElementById("a10");
            var a11 = document.getElementById("a11");
            var a12 = document.getElementById("a12");
            var a13 = document.getElementById("a13");

            var Li2 = document.getElementById("Li2");
            var v1 = document.getElementById("v1");
            var v2 = document.getElementById("v2");
            var v3 = document.getElementById("v3");
            var v4 = document.getElementById("v4");
            var v5 = document.getElementById("v5");
            var v6 = document.getElementById("v6");


            Li1.style.display = 'none';
            a1.style.display = 'none';
            a2.style.display = 'none';
            a3.style.display = 'none';
            a4.style.display = 'none';
            a5.style.display = 'none';
            a6.style.display = 'none';
            a7.style.display = 'none';
            a8.style.display = 'none';
            a9.style.display = 'none';
            a10.style.display = 'none';
            a11.style.display = 'none';
            a12.style.display = 'none';
            a13.style.display = 'none';

            Li2.style.display = 'none';
            v1.style.display = 'none';
            v2.style.display = 'none';
            v3.style.display = 'none';
            v4.style.display = 'none';
            v5.style.display = 'none';
            v6.style.display = 'none';
            if (admin1 == "True") {
                Li1.style.display = 'block';
                a1.style.display = 'block';
                a2.style.display = 'block';
                //a3.style.display = 'block';
                a4.style.display = 'block';
                a5.style.display = 'block';
                //a6.style.display = 'block';
                //a7.style.display = 'block';
                //a8.style.display = 'block';
                a9.style.display = 'block';
                //a10.style.display = 'block';
                a11.style.display = 'block';
                a12.style.display = 'block';
                a13.style.display = 'block';

                Li2.style.display = 'block';
                v1.style.display = 'block';
                v2.style.display = 'block';
                //v3.style.display = 'block';
                v4.style.display = 'block';
                v5.style.display = 'block';
                //v6.style.display = 'block';
            }
            if (aprvr1 == "True") {
                Li1.style.display = 'block';
                a1.style.display = 'block';
                a2.style.display = 'block';
                // a3.style.display = 'block';
                a4.style.display = 'block';
                a5.style.display = 'block';
                //a6.style.display = 'block';
                //a7.style.display = 'block';
                //a8.style.display = 'block';
                a9.style.display = 'block';
                //a10.style.display = 'block';
                a11.style.display = 'block';
                a12.style.display = 'block';
                a13.style.display = 'block';
            }
            if (cstdn1 == "True") {
                Li2.style.display = 'block';
                v1.style.display = 'block';
                v2.style.display = 'block';
                //v3.style.display = 'block';
                v4.style.display = 'block';
                v5.style.display = 'block';
                //v6.style.display = 'block';
            }

        }
    });
}



function readAll()
{
    try {
        $.ajax({
            type: "POST",
            //url: url,
            url: "Home/Notification.aspx/ReadAllNotification",
            data: "{ }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {

                //alert('allread');
                GetNotification();
            }
        });


    } catch (e) {

    }

}


var url = (window.location.href).split('/').pop() + '/GetNotification';

function GetNotification()
{
   // alert((window.location.href).split('/').pop());
    if ((window.location.href).split('/').pop() == "Notification.aspx") {
        try {
            $.ajax({
                type: "POST",
                //url: url,
                url: "Home/Notification.aspx/GetNotification",
                data: "{ }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg.d);
                    var dat = msg.d.split("\n");

                    var date = "";
                    var append = '';
                    $('#NotificationDIsplay').empty();
                    for (var i = 0; i < dat.length - 1; i++) {
                        //for (var i = 0; i < 2; i++) {
                        var data = dat[i].split('|')

                        //data[0] - notificationID
                        //data[1] - toUser
                        //data[2] - fullname
                        //data[3] - Notificatiopn dets
                        //data[4] - date
                        //data[5] - isdone
                        //data[6] - Image
                        //data[7] - trx flag
                        //data[8] - link
                        //data[9] - isread

                        // console.log(data[9]);
                        //alert((data[9] == "False") ? "Not read" : "read");
                        if (date != data[4]) {
                            date = data[4];

                            if (append != '') {
                                append += '</div></div> \n \n'

                                $('#NotificationDIsplay').append(append);

                                append = '';
                            }

                            append += '<div class="box box-default" > \n <div class="box-header with-border"> \n <h4>';
                            append += date + '</h4></div> \n <div class="box-body"> \n';

                            append += '<a style="color:#000" Onclick="readNotification(\'' + data[0] + '\')">  \n';
                            append += (data[9] == "False") ? '<div class="callout  callout-info" value="' + data[0] + '" name="Notif"> \n' : '<div class="callout " value="' + data[0] + '" name="Notif"> \n &nbsp &nbsp &nbsp';
                            append += '<img src="../pictures/employeeimages/user_profile.png" class="user-image img-circle" style="width:40px;height:40px;">';
                            append += '\n&nbsp &nbsp &nbsp ' + data[3] + ' </div> \n </a> ';

                        }
                        else {
                            append += '<a style="color:#000" Onclick="readNotification(\'' + data[0] + '\')"> \n ';
                            append += (data[9] == "False") ? '<div class="callout  callout-info" value="' + data[0] + '" name="Notif"> \n' : '<div class="callout " value="' + data[0] + '" name="Notif"> \n &nbsp &nbsp &nbsp';
                            append += '<img src="../pictures/employeeimages/user_profile.png" class="user-image img-circle" style="width:40px;height:40px;">';
                            append += '\n&nbsp &nbsp &nbsp ' + data[3] + ' </div> \n </a>';

                        }

                    }

                    append += '</div></div> \n \n'

                    $('#NotificationDIsplay').append(append);
                    console.log(append);


                }
            });


        } catch (e) {
            console.log(e.message);
        }
    }
    
}

function readNotification(NotifID)
{
    //alert(NotifID + " readNotification");
    //console.log(NotifID);
    $.ajax({
        type: "POST",
        //url: url,
        url: "Home/Notification.aspx/ReadNotification",
        data: " { NotifID : '" + NotifID + "' }",
        
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg);
            GetNotification();
        }
    });
   
}


var url1 = (window.location.href).split('/').pop() + '/getTOp5List';

function getTOp5List()
{

    try {
        $.ajax({
            type: "POST",
            url: url1,
            //url: "Notification.aspx/getTOp5List",
            data: "{ }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                console.log(msg);
                alert(msg)
                // GetNotification();
                NotifMenu.html(msg);
            }
        });


    } catch (e) {

    }

}