var idForUpdate = 0;
var currentPage = 1;

var filter = '';
var chkMenuCount = 0;
var chkUserGroupMenuCount = 0;

var chkRoleMemberExistingCount = 0;
var chkRoleMemberNotExistingCount = 0;
var chkRoleModuleExistingCount = 0;
var chkRoleModuleNotExistingCount = 0;

$(document).ready(function () {
    $('.select2').select2();

    $("#tblBrandList").DataTable({
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
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
                    },
                    {
                        extend: 'excel',
                        text: 'EXCEL',
                        className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
                    },
                     {
                         extend: 'csv',
                         text: 'CSV',
                         className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
                     },
                      {
                          extend: 'print',
                          text: 'PRINT',
                          className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
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
        url: "User_Management/Role.aspx/GetRole",
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
            "language": {
                processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
            },
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns": [
                { "data": "RoleName" },
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
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
                        },
                        {
                            extend: 'excel',
                            text: 'EXCEL',
                            className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
                        },
                         {
                             extend: 'csv',
                             text: 'CSV',
                             className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
                         },
                          {
                              extend: 'print',
                              text: 'PRINT',
                              className: 'btn btn-primary', exportOptions: { columns: ':visible' }, title: 'Role'
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

        $('#tblBrandList').on('click', 'button.btn_Member', function (e) {
            e.preventDefault();
        });
        $('#tblBrandList').on('click', 'button.btn_Module', function (e) {
            e.preventDefault();
        });
        $('#tblBrandList').on('click', 'button.btn_Edit', function (e) {
            e.preventDefault();
        });
    } catch (e) {
        var table = $('#tblBrandList').DataTable();
        table
            .clear()
            .draw();
    }
}

function LoadControls() {
    $(".btn").button();
}

function RoleByID(roleid) {
    idForUpdate = roleid;

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
                let elementsAddCat = document.getElementsByClassName("btn_EditRole");
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].remove();
                }
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/RoleByID",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtRole').val(msg.d.split('|')[1]);
            $('#txtDescription').val(msg.d.split('|')[2]);
            if (msg.d.split('|')[3] == 'True') {
                document.getElementById("chkActive").checked = true;
            }
            else {
                document.getElementById("chkActive").checked = false;
            }
        }
    });
    $("#addRoleDialogBox").modal("show");
}

function addRoleDialogBox1() {
    clear();
    document.getElementById("chkActive").checked = true;
    $("#addRoleDialogBox").modal("show");

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
                let elementsAddBrand = document.getElementsByClassName("btn_EditRole");
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].remove();
                }
                var $formBrand = makeElement('button', {
                    type: 'button',
                    id: 'btnSaveRole',
                    class: 'btn btn-success btn_EditRole',
                    onclick: 'SaveRole(this)',
                    innerHTML: 'Save'
                });

                $('#SaveCancelRole').prepend($formBrand);
            }
        }
    });
}

function UserAdded() {
    $("#memberDialogBoxMsg").modal("hide");
    swal("Success", "User/s Successfully added to role", "success");
}

function ModuleAdded() {
    $("#accessDialogBoxMsg").modal("hide");
    swal("Success", "Module/s Successfully added to role", "success");
}

function clear() {
    idForUpdate = 0;
    $('#txtRole').val("");
    $('#txtRole').prop("disabled", false);
    $('#txtDescription').val("");
}

function OpenDialogRoleMember(RoleId, roleName) {
    idForUpdate = RoleId;
    LoadExistingRoleMember(RoleId);
    LoadNotExistingRoleMember(RoleId);
    document.getElementById('lblrolename1').innerHTML = roleName;
    $("#memberDialogBoxMsg").modal("show");
}

function OpenDialogRoleAccess(RoleID, roleName) {
    idForUpdate = RoleID;
    LoadExistingRoleModule(RoleID);
    LoadNotExistingRoleModule(RoleID);
    //document.getElementById('rolename').innerText = roleName
    document.getElementById('lblrolename').innerHTML = roleName;
    $("#accessDialogBoxMsg").modal("show");
}

function SaveRole() {
    var alertMsg = '';

    var role1 = ReplaceStr1($('#txtRole').val().trim().replace(/  +/g, ' '));
    var desc = ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' '));

    if (ReplaceStr1($('#txtRole').val().trim().replace(/  +/g, ' ')) == "" || ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == "") {
        alertMsg = " ";
        if (ReplaceStr1($('#txtRole').val().trim().replace(/  +/g, ' ')) == "")
            alertMsg += "\nName";
        if (ReplaceStr1($('#txtDescription').val().trim().replace(/  +/g, ' ')) == "")
            alertMsg += "\nDescription";

        swal("Fill-out all the required field(s): ", alertMsg, "warning");
        //alert(alertMsg);
    }
    else {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to save this role?",
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
                    url: "User_Management/Role.aspx/SaveRole",
                    data: "{ roleid: '" + idForUpdate + "', roleName: '" + role1 + "', description: '" + desc + "', isActive: '" + document.getElementById("chkActive").checked + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == 0) {
                            load_data();
                            $("#addRoleDialogBox").modal("hide");
                            //alert('Role Successfully Saved!');
                            swal("Successful", "Role Successfully Saved!", "success");
                            CountChar($('#txtDescription').val(), 100, 'cnt', 'txtDescription');
                            Clear();
                        }
                        else if (msg.d == 1) {
                            load_data();
                            $("#addRoleDialogBox").modal("hide");
                            //alert('Role Successfully Modified!');
                            swal("Successful", "Role Successfully Modified!", "success");
                            CountChar($('#txtDescription').val(), 100, 'cnt', 'txtDescription');
                            Clear();
                        }
                        else if (msg.d == 3) {
                            load_data();
                            $("#addRoleDialogBox").modal("hide");
                            //alert('Role Successfully Modified!');
                            swal("Warning", "Role Cannot be set to inactive, please remove user/s in this role!", "warning");
                            CountChar($('#txtDescription').val(), 100, 'cnt', 'txtDescription');
                            Clear();
                        }
                        else {
                            load_data();
                            $("#addRoleDialogBox").modal("hide");
                            //alert('Role Already Exist!');
                            swal("Error", "Role Already Exist!", "error");
                            CountChar($('#txtDescription').val(), 100, 'cnt', 'txtDescription');
                            Clear();
                        }
                    }
                });
            } else {
                swal("Cancelled", "Cancelled by user", "warning");
            }
        });
    }
}

//MEMBER

function LoadExistingRoleMember(RoleId) {
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/LoadExistingRoleMember",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_MemberList').hide();
            $('#BodyHolder_MemberList').html(msg.d);
            $('#BodyHolder_MemberList').fadeIn(200);
        }
    });
}

function LoadNotExistingRoleMember(RoleId) {
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/LoadNotExistingRoleMember",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_UserList').hide();
            $('#BodyHolder_UserList').html(msg.d);
            $('#BodyHolder_UserList').fadeIn(200);
        }
    });
}



function SelectAllRoleMemberExisting() {
    for (var i = 0; i < $('#ctrRoleMemberExisting').val() ; i++) {
        if (document.getElementById('chkRoleMemberExistingId').checked == false) {
            document.getElementById('chkRoleMemberExistingId' + i).checked = false;
            chkRoleMemberExistingCount = 0;
        }
        else {
            document.getElementById('chkRoleMemberExistingId' + i).checked = true;
            chkRoleMemberExistingCount = document.getElementById('ctrRoleMemberExisting').value;
        }
    }
}

function CheckRoleMemberExisting(id) {
    if (document.getElementById('chkRoleMemberExistingId' + id).checked != true) {
        chkRoleMemberExistingCount -= 1;
        if (chkRoleMemberExistingCount < $('#ctrRoleMemberExisting').val())
            document.getElementById('chkRoleMemberExistingId').checked = false;
    }
    else {
        chkRoleMemberExistingCount += 1;
        if (chkRoleMemberExistingCount == $('#ctrRoleMemberExisting').val())
            document.getElementById('chkRoleMemberExistingId').checked = true;
    }
}

function SelectAllRoleMemberNotExisting() {
    for (var i = 0; i < $('#ctrRoleMemberNotExisting').val() ; i++) {
        if (document.getElementById('chkRoleMemberNotExistingId').checked == false) {
            document.getElementById('chkRoleMemberNotExistingId' + i).checked = false;
            chkRoleMemberNotExistingCount = 0;
        }
        else {
            document.getElementById('chkRoleMemberNotExistingId' + i).checked = true;
            chkRoleMemberNotExistingCount = document.getElementById('ctrRoleMemberNotExisting').value;
        }
    }
}

function CheckRoleMemberNotExisting(id) {
    if (document.getElementById('chkRoleMemberNotExistingId' + id).checked != true) {
        chkRoleMemberNotExistingCount -= 1;
        if (chkRoleMemberNotExistingCount < $('#ctrRoleMemberNotExisting').val())
            document.getElementById('chkRoleMemberNotExistingId').checked = false;
    }
    else {
        chkRoleMemberNotExistingCount += 1;
        if (chkRoleMemberNotExistingCount == $('#ctrRoleMemberNotExisting').val())
            document.getElementById('chkRoleMemberNotExistingId').checked = true;
    }
}

function SaveRoleMember(action) {
    var result;

    if (action == 'R')
        result = GetRoleMemberExistingValue();
    else
        result = GetRoleMemberNotExistingValue();

    if (result == 'N') {
        if (action == 'R')
            //alert('Please select Members(s) you want to remove!');
            swal("Warning", "Please select Members(s) you want to remove!", "warning");
        else
            //alert('Please select User(s) you want to add!');
            swal("Warning", "Please select User(s) you want to add!", "warning");

        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: "User_Management/Role.aspx/SaveRoleMember",
            data: "{ roleID: '" + idForUpdate + "', users: '" + result + "', action: '" + action + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                LoadExistingRoleMember(idForUpdate);
                LoadNotExistingRoleMember(idForUpdate);
                chkRoleMemberExistingCount = 0;
                chkRoleMemberNotExistingCount = 0;
                //alert('Role members successfully saved!');
            }
        });
    }
}

function GetRoleMemberExistingValue() {
    var result = '';
    var ctrChk = '';
    if (chkRoleMemberExistingCount != 0) {
        for (var i = 0; i < $('#ctrRoleMemberExisting').val() ; i++) {
            if (document.getElementById('chkRoleMemberExistingId' + i).checked == true) {
                if (ctrChk == chkRoleMemberExistingCount - 1) {
                    result += document.getElementById('RoleMemberExistingId' + i).value;
                }
                else {
                    result += document.getElementById('RoleMemberExistingId' + i).value + ';';
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

function GetRoleMemberNotExistingValue() {
    var result = '';
    var ctrChk = '';
    if (chkRoleMemberNotExistingCount != 0) {
        for (var i = 0; i < $('#ctrRoleMemberNotExisting').val() ; i++) {
            if (document.getElementById('chkRoleMemberNotExistingId' + i).checked == true) {
                if (ctrChk == chkRoleMemberNotExistingCount - 1) {
                    result += document.getElementById('RoleMemberNotExistingId' + i).value;
                }
                else {
                    result += document.getElementById('RoleMemberNotExistingId' + i).value + ';';
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

//module

function LoadExistingRoleModule(RoleID) {
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/LoadExistingRoleModule",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ModuleList').hide();
            $('#BodyHolder_ModuleList').html(msg.d);
            $('#BodyHolder_ModuleList').fadeIn(200);
        }
    });
}

function LoadNotExistingRoleModule(RoleId) {
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/LoadNotExistingRoleModule",
        data: "{ id: '" + idForUpdate + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#BodyHolder_ModuleListAll').hide();
            $('#BodyHolder_ModuleListAll').html(msg.d);
            $('#BodyHolder_ModuleListAll').fadeIn(200);
        }
    });
}

function SelectAllRoleModuleExisting() {
    for (var i = 0; i < $('#ctrRoleModuleExisting').val() ; i++) {
        if (document.getElementById('chkRoleModuleExistingId').checked == false) {
            document.getElementById('chkRoleModuleExistingId' + i).checked = false;
            chkRoleModuleExistingCount = 0;
        }
        else {
            document.getElementById('chkRoleModuleExistingId' + i).checked = true;
            chkRoleModuleExistingCount = document.getElementById('ctrRoleModuleExisting').value;
        }
    }
}

function CheckRoleModuleExisting(id) {
    if (document.getElementById('chkRoleModuleExistingId' + id).checked != true) {
        chkRoleModuleExistingCount -= 1;
        if (chkRoleModuleExistingCount < $('#ctrRoleModuleExisting').val())
            document.getElementById('chkRoleModuleExistingId').checked = false;
    }
    else {
        chkRoleModuleExistingCount += 1;
        if (chkRoleModuleExistingCount == $('#ctrRoleModuleExisting').val())
            document.getElementById('chkRoleModuleExistingId').checked = true;
    }
}

function SelectAllRoleModuleNotExisting() {
    for (var i = 0; i < $('#ctrRoleModuleNotExisting').val() ; i++) {
        if (document.getElementById('chkRoleModuleNotExistingId').checked == false) {
            document.getElementById('chkRoleModuleNotExistingId' + i).checked = false;
            chkRoleModuleNotExistingCount = 0;
        }
        else {
            document.getElementById('chkRoleModuleNotExistingId' + i).checked = true;
            chkRoleModuleNotExistingCount = document.getElementById('ctrRoleModuleNotExisting').value;
        }
    }
}

function CheckRoleModuleNotExisting(id) {
    if (document.getElementById('chkRoleModuleNotExistingId' + id).checked != true) {
        chkRoleModuleNotExistingCount -= 1;
        if (chkRoleModuleNotExistingCount < $('#ctrRoleModuleNotExisting').val())
            document.getElementById('chkRoleModuleNotExistingId').checked = false;
    }
    else {
        chkRoleModuleNotExistingCount += 1;
        if (chkRoleMemberNotExistingCount == $('#ctrRoleModuleNotExisting').val())
            document.getElementById('chkRoleModuleNotExistingId').checked = true;
    }
}

function SaveRoleModule(action) {
    var result;

    if (action == 'R') {
        result = GetRoleModuleExistingValue();
    }
    else {
        result = GetRoleModuleNotExistingValue();
    }

    if (result == 'N') {
        if (action == 'R') {
            //alert('Please select Modules(s) you want to remove!');
            swal("Warning", "Please select Module(s) you want to remove!", "warning");
        }
        else
            //alert('Please select User(s) you want to add!');
            swal("Warning", "Please select Module(s) you want to add!", "warning");

        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: "User_Management/Role.aspx/SaveRoleModule",
            data: "{ roleID: '" + idForUpdate + "', modules: '" + result + "', action: '" + action + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                LoadExistingRoleModule(idForUpdate);
                LoadNotExistingRoleModule(idForUpdate);
                chkRoleModuleExistingCount = 0;
                chkRoleModuleNotExistingCount = 0;
                //alert('Role Modules successfully saved!');
            }
        });
    }
}

function GetRoleModuleExistingValue() {
    var result = '';
    var ctrChk = '';
    if (chkRoleModuleExistingCount != 0) {
        for (var i = 0; i < $('#ctrRoleModuleExisting').val() ; i++) {
            if (document.getElementById('chkRoleModuleExistingId' + i).checked == true) {
                if (ctrChk == chkRoleModuleExistingCount - 1) {
                    result += document.getElementById('RoleModuleExistingId' + i).value;
                }
                else {
                    result += document.getElementById('RoleModuleExistingId' + i).value + ';';
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

function GetRoleModuleNotExistingValue() {
    var result = '';
    var ctrChk = '';
    if (chkRoleModuleNotExistingCount != 0) {
        for (var i = 0; i < $('#ctrRoleModuleNotExisting').val() ; i++) {
            if (document.getElementById('chkRoleModuleNotExistingId' + i).checked == true) {
                if (ctrChk == chkRoleModuleNotExistingCount - 1) {
                    result += document.getElementById('RoleModuleNotExistingId' + i).value;
                }
                else {
                    result += document.getElementById('RoleModuleNotExistingId' + i).value + ';';
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

function OpenDialogButtonAccess(RoleID, roleName) {
    idForUpdate = RoleID;
    LoadExistingOperatorGroupModuleButton(RoleID);
    //document.getElementById('rolename').innerText = roleName
    document.getElementById('lblrolename2').innerHTML = roleName;
    $("#ButtonaccessDialogBoxMsg").modal("show");
}

function LoadExistingOperatorGroupModuleButton(RoleID) {
    ctrview = 0; ctradd = 0; ctredit = 0; ctrdelete = 0;
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/LoadExistingOperatorGroupModuleButton",
        data: "{ id: '" + RoleID + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //alert(msg.d);
            $('#BodyHolder_ModuleListButton').hide();
            $('#BodyHolder_ModuleListButton').html(msg.d);
            $('#BodyHolder_ModuleListButton').fadeIn(200);
            //////VIEW
            var options = document.getElementsByClassName("chkBtnView");
            for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
                if (options[i].checked)
                    ctrview++;
            }
            if (ctrview == $('#ctrOperatorGroupModuleExisting').val())
                document.getElementById('chkBtnView').checked = true;
            console.log(ctrview);
            ///////ADD
            var optionsadd = document.getElementsByClassName("chkBtnAdd");
            for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
                if (optionsadd[i].checked)
                    ctradd++;
            }
            if (ctradd == $('#ctrOperatorGroupModuleExisting').val())
                document.getElementById('chkBtnAdd').checked = true;
            console.log(ctradd);
            ///////EDIT
            var optionsedit = document.getElementsByClassName("chkBtnEdit");
            for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
                if (optionsedit[i].checked)
                    ctredit++;
            }
            if (ctredit == $('#ctrOperatorGroupModuleExisting').val())
                document.getElementById('chkBtnEdit').checked = true;
            console.log(ctredit);
            ///////DELETE
            var optionsdelete = document.getElementsByClassName("chkBtnDel");
            for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
                if (optionsdelete[i].checked)
                    ctrdelete++;
            }
            if (ctrdelete == $('#ctrOperatorGroupModuleExisting').val())
                document.getElementById('chkBtnDel').checked = true;
            console.log(ctrdelete);
        }
    });
}

//addeditdelete

var ctrview = 0; ////view button
function SelectAllViewBtn() {
    for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
        if (document.getElementById('chkBtnView').checked != true) {
            if (document.getElementById('chkBtnView' + i).checked == false) {

            }
            else {
                document.getElementById('chkBtnView' + i).checked = false;
                $("#chkBtnView" + i).trigger("change");
                ctrview = 0;
            }
        }
        else {
            if (document.getElementById('chkBtnView' + i).checked == false) {
                document.getElementById('chkBtnView' + i).checked = true;
                $("#chkBtnView" + i).trigger("change");
                ctrview = $('#ctrOperatorGroupModuleExisting').val();
            }
            else {
            }
        }
    }
}

function UpdateViewBtn(ctr, OperatorGroupId, moduleid, btns) {

    var viewbtn = btns.split('|')[0];
    var addbtn = btns.split('|')[1];
    var editbtn = btns.split('|')[2];
    var delbtn = btns.split('|')[3];
    if (viewbtn == "0")
        viewbtn = 1;
    else
        viewbtn = 0;
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/UpdateViewBtn",
        data: "{ OperatorGroupId: '" + OperatorGroupId + "', ModuleId: '" + moduleid + "', buttonaccess: '" + viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            LoadExistingOperatorGroupModuleButton(OperatorGroupId);
        }
    });
    if (document.getElementById('chkBtnView' + ctr).checked != true) {
        ctrview -= 1;
        if (ctrview < $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnView').checked = false;
    }
    else {
        ctrview += 1;
        if (ctrview == $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnView').checked = true;
    }
}

var ctradd = 0;/////////add button
function SelectAllAddBtn() {
    for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
        if (document.getElementById('chkBtnAdd').checked != true) {
            if (document.getElementById('chkBtnAdd' + i).checked == false) {

            }
            else {
                document.getElementById('chkBtnAdd' + i).checked = false;
                $("#chkBtnAdd" + i).trigger("change");
                ctradd = 0;
            }
        }
        else {
            if (document.getElementById('chkBtnAdd' + i).checked == false) {
                document.getElementById('chkBtnAdd' + i).checked = true;
                $("#chkBtnAdd" + i).trigger("change");
                ctradd = $('#ctrOperatorGroupModuleExisting').val();
            }
            else {
            }
        }
    }
}

function UpdateAddBtn(ctr, OperatorGroupId, moduleid, btns) {
    var viewbtn = btns.split('|')[0];
    var addbtn = btns.split('|')[1];
    var editbtn = btns.split('|')[2];
    var delbtn = btns.split('|')[3];

    if (addbtn == "0")
        addbtn = 1;
    else
        addbtn = 0;

    //alert(viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn);
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/UpdateAddBtn",
        data: "{ OperatorGroupId: '" + OperatorGroupId + "', ModuleId: '" + moduleid + "', buttonaccess: '" + viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            LoadExistingOperatorGroupModuleButton(OperatorGroupId);
        }
    });
    if (document.getElementById('chkBtnAdd' + ctr).checked != true) {
        ctradd -= 1;
        if (ctradd < $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnAdd').checked = false;
    }
    else {
        ctradd += 1;
        if (ctradd == $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnAdd').checked = true;
    }
}

var ctredit = 0;/////////edit button
function SelectAllEditBtn() {
    for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
        if (document.getElementById('chkBtnEdit').checked != true) {
            if (document.getElementById('chkBtnEdit' + i).checked == false) {

            }
            else {
                document.getElementById('chkBtnEdit' + i).checked = false;
                $("#chkBtnEdit" + i).trigger("change");
                ctredit = 0;
            }
        }
        else {
            if (document.getElementById('chkBtnEdit' + i).checked == false) {
                document.getElementById('chkBtnEdit' + i).checked = true;
                $("#chkBtnEdit" + i).trigger("change");
                ctredit = $('#ctrOperatorGroupModuleExisting').val();
            }
            else {
            }
        }
    }
}

function UpdateEditBtn(ctr, OperatorGroupId, moduleid, btns) {
    var viewbtn = btns.split('|')[0];
    var addbtn = btns.split('|')[1];
    var editbtn = btns.split('|')[2];
    var delbtn = btns.split('|')[3];

    if (editbtn == "0")
        editbtn = 1;
    else
        editbtn = 0;

    //alert(viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn);
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/UpdateEditBtn",
        data: "{ OperatorGroupId: '" + OperatorGroupId + "', ModuleId: '" + moduleid + "', buttonaccess: '" + viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            LoadExistingOperatorGroupModuleButton(OperatorGroupId);
        }
    });
    if (document.getElementById('chkBtnEdit' + ctr).checked != true) {
        ctredit -= 1;
        if (ctredit < $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnEdit').checked = false;
    }
    else {
        ctredit += 1;
        if (ctredit == $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnEdit').checked = true;
    }
}

var ctrdelete = 0;/////////delete button
function SelectAllDeleteBtn() {
    for (var i = 0; i < $('#ctrOperatorGroupModuleExisting').val() ; i++) {
        if (document.getElementById('chkBtnDel').checked != true) {
            if (document.getElementById('chkBtnDel' + i).checked == false) {

            }
            else {
                document.getElementById('chkBtnDel' + i).checked = false;
                $("#chkBtnDel" + i).trigger("change");
                ctrdelete = 0;
            }
        }
        else {
            if (document.getElementById('chkBtnDel' + i).checked == false) {
                document.getElementById('chkBtnDel' + i).checked = true;
                $("#chkBtnDel" + i).trigger("change");
                ctrdelete = $('#ctrOperatorGroupModuleExisting').val();
            }
            else {
            }
        }
    }
}

function UpdateDelBtn(ctr, OperatorGroupId, moduleid, btns) {
    var viewbtn = btns.split('|')[0];
    var addbtn = btns.split('|')[1];
    var editbtn = btns.split('|')[2];
    var delbtn = btns.split('|')[3];

    if (delbtn == "0")
        delbtn = 1;
    else
        delbtn = 0;

    //alert(viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn);
    $.ajax({
        type: "POST",
        url: "User_Management/Role.aspx/UpdateDelBtn",
        data: "{ OperatorGroupId: '" + OperatorGroupId + "', ModuleId: '" + moduleid + "', buttonaccess: '" + viewbtn + "|" + addbtn + "|" + editbtn + "|" + delbtn + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            LoadExistingOperatorGroupModuleButton(OperatorGroupId);
            uleButton(OperatorGroupId);
        }
    });
    if (document.getElementById('chkBtnDel' + ctr).checked != true) {
        ctrdelete -= 1;
        if (ctrdelete < $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnDel').checked = false;
    }
    else {
        ctrdelete += 1;
        if (ctrdelete == $('#ctrOperatorGroupModuleExisting').val())
            document.getElementById('chkBtnDel').checked = true;
    }
}