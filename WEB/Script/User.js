var idForUpdate = 0;
var idforUpdate1 = 0;
var currentPage = 1;

var chkPersonnelMemberExistingCount = 0;
var chkPersonnelMemberNotExistingId = 0;
var chkPersonnelMemberExistingId = 0;
var chkPersonnelMemberNotExistingCount = 0;

var custodianArray = new Array();
var custodianArrayElements = new Array();
var CustodianCheckBoxCtr = 0;
var IsEmailUsername = '';

$(document).ready(function () {
    isEmailUsername();
    $('.select2').select2();

    $('#BodyHolder_txtUsername').prop('disabled', true);

    //$("#tblBrandList").DataTable({
    //    "scrollX": true
    //});
    //GetDepartmentList();
    //GetPosition1();
    //if (document.getElementById("chkActive").checked == false) {
    //    document.getElementById('divCustodian').style.pointerEvents = 'none';
    //}
    ////added by nero
    //$('#chkCustodian').change(function () {
    //    if ($(this).is(":checked")) {
    //        document.getElementById('divCustodian').style.pointerEvents = 'auto';

    //    }
    //    else {
    //        document.getElementById('divCustodian').style.pointerEvents = 'none';
    //        $('#BodyHolder_multiSelect').val('').trigger('change');
    //    }

    //});
    load_dataUser();

    //$(document).on('change', '#ddlDepartment1', function () {
    //    GetPosition1();
    //});
});

function isEmailUsername() {
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/GetUserAcccountSettings",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            IsEmailUsername = msg.d.split('|')[0];
        }
    });
}

function makeElement(element, options) {
    var $formField = document.createElement(element);
    $.each(options, function (key, value) {
        if (key === 'innerHTML') {
            $formField.innerHTML = value;
        }
        else {
            $formField.setAttribute(key, value);
        }
    });
    return $formField;
}





function load_dataUser() {
    $.ajax({
        type: 'POST',
        url: "User_Management/User.aspx/GetUser",
        data: "{ }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#brandlist').hide();
            $('#brandlist').innerHTML = "";
            $('#brandlist').fadeIn(200);

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
        $('#tblBrandList').DataTable({
            "destroy": true,
            "processing": true,
            fixedHeader: {
                header: true,
                footer: true,
                headerOffset: 50
            },
            "sorting": false,
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "Name" },
                { "data": "EmployeeNo" },
                { "data": "EmailAddress" },
                { "data": "RoleName" },
                { "data": "Site" },
                { "data": "Custodians" },
                { "data": "Administrative" },
                { "data": "Approver" },
                //{ "data": "Inspector" },
                //{ "data": "Certifier" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User List'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User List'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User List'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'User List'
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

        $('#tblBrandList').off('click');
    } catch (e) {
        var table = $('#tblBrandList').DataTable();
        table
            .clear()
            .draw();
    }
}

$(function () {
    LoadControls();
});

function LoadControls() {
    $('.btn').button();
}

function addWarehouseDialogBox1(RecID) {
    idForUpdate = RecID;
    LoadExistingPersonnelMember(RecID);
    LoadNotExistingPersonnelMember(RecID);
    $("#addWarehouseDialogBox").modal("show");
}

function addUserDialogBox(firstname, lastname, cardno, depid, posid, email, username, administrative, active) {
    $('#txtFirstName').val(firstname);
    $('#txtLastName').val(lastname);
    $('#txtEmail').val(email);
    $('#txtUsername').val(username);

    $('#BodyHolder_txtUsername').prop('disabled', true);

    if (active == 'True') {
        document.getElementById("chkAdministrative").checked = true;
    } else {
        document.getElementById("chkAdministrative").checked = false;
    }

    if (active == 'True') {
        document.getElementById("chkActiveUser").checked = true;
    } else {
        document.getElementById("chkActiveUser").checked = false;
    }

    $("#addUserDialogBox").modal("show");
}


function Clear() {
    preventDefault();
    $('#ddlDepartment1').val(0).trigger('change');
    $('#ddlPosition').val(0).trigger('change');
    $('#ddlWarehouse').val(0).trigger('change');
    $('#txtFirstName').val("");
    $('#txtLastName').val("");
    $('#txtEmail').val("");
    $('#txtEmployeeNo').val("");
    $('#txtUsername').val("");
    document.getElementById("<%= txtUsername.ClientID %>").value = "";
    $('#BodyHolder_txtUsername').prop('disabled', false);
    document.getElementById('BodyHolder_txtCardNo').value = '';
    document.getElementById("chkApprover").checked = false;
    $('#BodyHolder_ddlIntegrator').val("--- Select Custodian Type ---");
    idForUpdate = 0;
}

function deleteUser(id) {
    idForUpdate = id;
    //$("#deleteUser").modal("show");
    swal({
        title: "Delete User",
        text: "Are you sure you want to delete this user?",
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
                url: "User_Management/User.aspx/DeleteUser",
                data: "{ id: '" + idForUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 0) {
                        //alert('System Developer User Cannot be DELETED!');
                        swal("Warning", "System Developer User Cannot be DELETED!", "warning");
                    }
                    else if (msg.d == 1) {
                        load_dataUser();
                        //alert('User Successfully Deleted!');
                        swal("Successful", "User Successfully Deleted!", "success");
                        idForUpdate = '';
                    }
                    else {
                        //alert('User Has an Existing Asset/s Issued. Cannot be Deleted!');
                        swal("Warning", "User Has an Existing Property/ies Issued. Cannot be Deleted!", "warning");
                        idForUpdate = '';
                    }
                    //$('#BodyHolder_UserList').html(msg.d);
                    //$("#addUserDialogBox").modal("hide");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}

function DeleteUser() {
    $("#deleteUser").modal("hide");
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/DeleteUser",
        data: "{ id: '" + idForUpdate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == 0) {
                //alert('System Developer User Cannot be DELETED!');
                swal("Warning", "System Developer User Cannot be DELETED!", "warning");
            }
            else if (msg.d == 1) {
                load_data();
                //alert('User Successfully Deleted!');
                swal("Successful", "User Successfully Deleted!", "success");
                idForUpdate = '';
            }
            else {
                //alert('User Has an Existing Asset/s Issued. Cannot be Deleted!');
                swal("Warning", "User Has an Existing Asset/s Issued. Cannot be Deleted!", "warning");
                idForUpdate = '';
            }
            //$('#BodyHolder_UserList').html(msg.d);
            //$("#addUserDialogBox").modal("hide");
        }
    });
}

var selectedPosition = '';

function GetModelID(id) {
    if (IsEmailUsername == 'Email') {
        $('#txtEmail').prop('disabled', true);
    } else {
        $('#txtEmail').prop('disabled', true);
        $('#txtUsername').prop('disabled', true);
    }
    checker = true;
    $('#ddlPosition').val("");
    selectedCustodian = [];
    $(".cls_Custodian").prop("checked", false);
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
                let elementsAddCat = document.getElementsByClassName("btn_EditUser");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/GetUserByID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#ddlDepartment1').val(msg.d.split('|')[4]).trigger('change');
            $('#ddlPosition').val(msg.d.split('|')[5]).trigger('change');
            $('#txtFirstName').val(msg.d.split('|')[1]);
            $('#txtLastName').val(msg.d.split('|')[2]);
            $('#txtEmail').val(msg.d.split('|')[6]);
            $('#txtUsername').val(msg.d.split('|')[7]);
            if (msg.d.split('|')[8] == 'True') {
                document.getElementById("chkActiveUser").checked = true;
            }
            else {
                document.getElementById("chkActiveUser").checked = false;
            }
            if (msg.d.split('|')[9] == 'True') {
                document.getElementById("chkApprover").checked = true;
            }
            else {
                document.getElementById("chkApprover").checked = false;
            }
            if (IsEmailUsername == 'Email') {
                document.getElementById('DivUsername').style.display = 'none';
            }

            var userimage = "";
            if (msg.d.split('|')[17] == "") {
                userimage = "<img src='../Pictures/noimage.png' style='max-width: 100%; height:auto;'>";
            }
            else {
                userimage = "<img src='../Pictures/EmployeeImages/" + msg.d.split('|')[7] + msg.d.split('|')[17] + "' style='max-width: 100%; height:auto;'>";
            }
            $('#userImage1').html(userimage);
            $('#txtEmployeeNo').val(msg.d.split('|')[13]);
            $('#ddlWarehouse1').val(msg.d.split('|')[14]).trigger('change');
            if (msg.d.split('|')[15] == 'True') {
                document.getElementById("chkAdministrative").checked = true;
            }
            else {
                document.getElementById("chkAdministrative").checked = false;
            }

            //added by nero
            if (msg.d.split('|')[16] == 'True') {
                document.getElementById("chkCustodian").checked = true;
                $(".cls_Custodian").removeAttr("disabled");
            }
            else {
                document.getElementById("chkCustodian").checked = false;
            }

            var custodians = [];
            custodians = msg.d.split('|')[12].split(';');
            chkCustodianChecked = custodians.length;
            for (i = 0; i < custodians.length; i++) {
                selectedCustodian.push(custodians[i]);
                document.getElementById(custodians[i]).checked = true;
            }
        }
    });
    $('#FileUploadUser').val("");
    $("#addUserDialog").modal("show");
    checker = false;
}


function LoadExistingPersonnelMember(RecID) {
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/LoadExistingPersonnelMember",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_AllowedWarehouses1').hide();
            $('#BodyHolder_AllowedWarehouses1').html(msg.d);
            $('#BodyHolder_AllowedWarehouses1').fadeIn(200);
        }
    });
}

function LoadNotExistingPersonnelMember(RecId) {
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/LoadNotExistingPersonnelMember",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_WarehouseList1').hide();
            $('#BodyHolder_WarehouseList1').html(msg.d);
            $('#BodyHolder_WarehouseList1').fadeIn(200);
        }
    });
}

//Checkbox
function SelectAllPersonnelMemberExisting() {
    for (var i = 0; i < $('#ctrPersonnelMemberExisting').val() ; i++) {
        if (document.getElementById('chkPersonnelMemberExistingId').checked == false) {
            document.getElementById('chkPersonnelMemberExistingId' + i).checked = false;
            chkPersonnelMemberExistingCount = 0;
        }
        else {
            document.getElementById('chkPersonnelMemberExistingId' + i).checked = true;
            chkPersonnelMemberExistingCount = document.getElementById('ctrPersonnelMemberExisting').value;
        }
    }
}

function CheckPersonnelMemberExisting(id) {
    if (document.getElementById('chkPersonnelMemberExistingId' + id).checked != true) {
        chkPersonnelMemberExistingCount -= 1;
        if (chkPersonnelMemberExistingCount < $('#ctrPersonnelMemberExisting').val())
            document.getElementById('chkPersonnelMemberExistingId').checked = false;
    }
    else {
        chkPersonnelMemberExistingCount += 1;
        if (chkPersonnelMemberExistingCount == $('#ctrPersonnelMemberExisting').val())
            document.getElementById('chkPersonnelMemberExistingId').checked = true;
    }
}

function SelectAllPersonnelMemberNotExisting() {
    for (var i = 0; i < $('#ctrPersonnelMemberNotExisting').val() ; i++) {
        if (document.getElementById('chkPersonnelMemberNotExistingId').checked == false) {
            document.getElementById('chkPersonnelMemberNotExistingId' + i).checked = false;
            chkPersonnelMemberNotExistingCount = 0;
        }
        else {
            document.getElementById('chkPersonnelMemberNotExistingId' + i).checked = true;
            chkPersonnelMemberNotExistingCount = document.getElementById('ctrPersonnelMemberNotExisting').value;
        }
    }
}

function CheckPersonnelMemberNotExisting(id) {
    if (document.getElementById('chkPersonnelMemberNotExistingId' + id).checked != true) {
        chkPersonnelMemberNotExistingCount -= 1;
        if (chkPersonnelMemberNotExistingCount < $('#ctrPersonnelMemberNotExisting').val())
            document.getElementById('chkPersonnelMemberNotExistingId').checked = false;
    }
    else {
        chkPersonnelMemberNotExistingCount += 1;
        if (chkPersonnelMemberNotExistingCount == $('#ctrPersonnelMemberNotExisting').val())
            document.getElementById('chkPersonnelMemberNotExistingId').checked = true;
    }
}

function GetPersonnelMemberExistingValue() {
    var result = '';
    var ctrChk = '';
    if (chkPersonnelMemberExistingCount != 0) {
        for (var i = 0; i < $('#ctrPersonnelMemberExisting').val() ; i++) {
            if (document.getElementById('chkPersonnelMemberExistingId' + i).checked == true) {
                if (ctrChk == chkPersonnelMemberExistingCount - 1) {
                    result += document.getElementById('PersonnelMemberExistingId' + i).value;
                }
                else {
                    result += document.getElementById('PersonnelMemberExistingId' + i).value + ';';
                }
                ctrChk++;
            }
        }
    }
    else {
        result = "N";
    }
    return result;
}

function GetPersonnelMemberNotExistingValue() {
    var result = '';
    var ctrChk = '';
    if (chkPersonnelMemberNotExistingCount != 0) {
        for (var i = 0; i < $('#ctrPersonnelMemberNotExisting').val() ; i++) {
            if (document.getElementById('chkPersonnelMemberNotExistingId' + i).checked == true) {
                if (ctrChk == chkPersonnelMemberNotExistingCount - 1) {
                    result += document.getElementById('PersonnelMemberNotExistingId' + i).value;
                }
                else {
                    result += document.getElementById('PersonnelMemberNotExistingId' + i).value + ';';
                }
                ctrChk++;
            }
        }
    }
    else {
        result = "N";
    }
    return result;
}

function SavePersonnelMember(action) {
    var result

    if (action == 'R')
        result = GetPersonnelMemberExistingValue();
    else
        result = GetPersonnelMemberNotExistingValue();

    if (result == 'N') {
        if (action == 'R')
            alert('Please select Site(s) you want to remove!');
        else
            alert('Please select Site(s) you want to add!');

        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: "User_Management/User.aspx/AddWarehouse",
            data: "{ recid: '" + idForUpdate + "', warehouseid: '" + result + "', action: '" + action + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                LoadExistingPersonnelMember(idForUpdate);
                LoadNotExistingPersonnelMember(idForUpdate);
                chkPersonnelMemberExistingCount = 0;
                chkPersonnelMemberNotExistingCount = 0;
            }
        });
    }
}

var Lname = '';
var Fname = '';
var Email = '';
var UserName = '';
function resetPassword(id, lname, fname, email, uname) {
    idForUpdate = id;
    Lname = lname;
    Fname = fname;
    Email = email;
    UserName = uname;
    //alert(id +" - " + lname + " - " + fname + " - " + email);
    $("#resetPassword").modal("show");
}

function ResetPassword() {
    $("#resetPassword").modal("hide");
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/ResetPassword",
        data: "{ id: '" + idForUpdate + "',LastName: '" + Lname + "',FirstName: '" + Fname + "',Email: '" + Email + "',UserName: '" + UserName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                load_dataUser();
                //alert('User Password Successfully Reset!');
                swal("Successful!", "User Password Successfully Reset! Kindly check your Email", "success");
                idForUpdate = '';
            }
            else {
                load_dataUser();
                idForUpdate = '';
                swal("Error", "User Password cannot be Reset!", "error");
            }
        }
    });
}

function printqr(employeeno) {
    $.ajax({
        type: "POST",
        url: "User_Management/User.aspx/PrintQR",
        data: "{ employeeno: '" + employeeno + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            document.getElementById("imgbarcode").src = msg.d;
            document.getElementById("empno").innerHTML = employeeno;

            $("#qrcoderdialog").modal("show");
        }
    });
}

function ForceLogOut(UserId) {
    swal({
        title: "LogOut User",
        text: "Are you sure you want to LogOut this user?",
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
                url: "User_Management/User.aspx/ForceLogOut",
                data: "{ Userid: '" + UserId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 1) {
                        swal("Successful", "User Forced Log Out!", "success");
                    }
                    else {
                        //alert('User Has an Existing Asset/s Issued. Cannot be Deleted!');
                        swal("Warning", "User Has an Existing Property/ies Issued. Cannot be Log Out!", "warning");
                        idForUpdate = '';
                    }
                    //$('#BodyHolder_UserList').html(msg.d);
                    //$("#addUserDialogBox").modal("hide");
                }
            });
        } else {
            swal("Cancelled", "Cancelled by user", "warning");
        }
    });
}