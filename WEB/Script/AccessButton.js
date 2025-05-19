var RB = '';
$(document).ready(function () {
    CheckR();
});

function getURL() {
    var context = window.location.pathname.substring(1);
}

function CheckA(ModuleAccess) {
    $.ajax({
        type: "POST",
        url: context + "/CheckA",
        data: " { ModuleAccess : '" + ModuleAccess + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

        }
    });
}

function CheckR() {
    var context = window.location.pathname.substring(1);
    var url = context.split('/')[1];
    $.ajax({
        type: "POST",
        url: context + "/CheckR",
        data: " { context : '" + context + "'} ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //FilesData
            //Category
            let elementsAddCat = document.getElementsByClassName("btn_AddCat");
            let elementsEditCat = document.getElementsByClassName("btn_EditCat");
            let elementsDelCat = document.getElementsByClassName("btn_DeleteCat");
            ////Brand
            let elementsAddBrand = document.getElementsByClassName("btn_AddBrand");
            let elementsEditBrand = document.getElementsByClassName("btn_EditBrand");
            let elementsDelBrand = document.getElementsByClassName("btn_DeleteBrand");
            ////Model
            let elementsAddModel = document.getElementsByClassName("btn_AddModel");
            let elementsEditModel = document.getElementsByClassName("btn_EditModel");
            let elementsDelModel = document.getElementsByClassName("btn_DeleteModel");
            ////Classification
            let elementsAddClass = document.getElementsByClassName("btn_AddClass");
            let elementsEditClass = document.getElementsByClassName("btn_EditClass");
            let elementsDelClass = document.getElementsByClassName("btn_DeleteClass");
            //Unit of Measurement
            let elementsAddUOM = document.getElementsByClassName("btn_AddUOM");
            let elementsEditUOM = document.getElementsByClassName("btn_EditUOM");
            let elementsDelUOM = document.getElementsByClassName("btn_DeleteUOM");
            //Supplier
            let elementsAddSupplier = document.getElementsByClassName("btn_AddSupplier");
            let elementsEditSupplier = document.getElementsByClassName("btn_EditSupplier");
            let elementsDelSupplier = document.getElementsByClassName("btn_DeleteSupplier");
            //Site
            let elementsAddSite = document.getElementsByClassName("btn_AddSite");
            let elementsEditSite = document.getElementsByClassName("btn_EditSite");
            let elementsDelSite = document.getElementsByClassName("btn_DeleteSite");
            //Location
            let elementsAddLocation = document.getElementsByClassName("btn_AddLocation");
            let elementsEditLocation = document.getElementsByClassName("btn_EditLocation");
            let elementsDelLocation = document.getElementsByClassName("btn_DeleteLocation");
            //Area
            let elementsAddArea = document.getElementsByClassName("btn_AddArea");
            let elementsEditArea = document.getElementsByClassName("btn_EditArea");
            let elementsDelArea = document.getElementsByClassName("btn_DeleteArea");
            ////Department
            let elementsAddDepartment = document.getElementsByClassName("btn_AddDepartment");
            let elementsEditDepartment = document.getElementsByClassName("btn_EditDepartment");
            let elementsDelDepartment = document.getElementsByClassName("btn_DeleteDepartment");
            ////Job Title
            let elementsAddPosition = document.getElementsByClassName("btn_AddPosition");
            let elementsEditPosition = document.getElementsByClassName("btn_EditPosition");
            let elementsDelPosition = document.getElementsByClassName("btn_DeletePosition");
            //Custodian
            let elementsAddCustodian = document.getElementsByClassName("btn_AddCustodian");
            let elementsEditCustodian = document.getElementsByClassName("btn_EditCustodian");
            let elementsDelCustodian = document.getElementsByClassName("btn_DeleteCustodian");
            //Property
            let elementsAddProperty = document.getElementsByClassName("btn_AddProperty");
            let elementsEditProperty = document.getElementsByClassName("btn_EditProperty");
            let elementsDelProperty = document.getElementsByClassName("btn_DeleteProperty");
            //Stock
            let elementsAddStock = document.getElementsByClassName("btn_AddStock");
            let elementsEditStock = document.getElementsByClassName("btn_EditStock");
            let elementsDelStock = document.getElementsByClassName("btn_DeleteStock"); 
            //Chart of Accounts
            let elementsAddCOA = document.getElementsByClassName("btn_AddChartOfAccounts");
            let elementsEditCOA = document.getElementsByClassName("btn_EditChartOfAccounts");
            let elementsDelCOA = document.getElementsByClassName("btn_DeleteChartOfAccounts");
            //Task
            let elementsAddTask = document.getElementsByClassName("btn_AddTask");
            let elementsEditTask = document.getElementsByClassName("btn_EditTask");
            let elementsDelTask = document.getElementsByClassName("btn_DeleteTask");
            //Tag
            let elementsAddTag = document.getElementsByClassName("btn_AddTag");
            let elementsEditTag = document.getElementsByClassName("btn_EditTag");
            let elementsDelTag = document.getElementsByClassName("btn_DeleteTag");
            //SecurityQuestion
            let elementsAddSecurityQuestion = document.getElementsByClassName("btn_AddSecurityQuestion");
            let elementsEditSecurityQuestion = document.getElementsByClassName("btn_EditSecurityQuestion");
            let elementsDelSecurityQuestion = document.getElementsByClassName("btn_DeleteSecurityQuestion");


            //SystemSettings            
            //DeviceMngt
            let elementsAddDeviceMngt = document.getElementsByClassName("btn_AddDeviceMngt");
            let elementsEditDeviceMngt = document.getElementsByClassName("btn_EditDeviceMngt");
            let elementsDelDeviceMngt = document.getElementsByClassName("btn_DeleteDeviceMngt");

            //UserMngt            
            //User
            let elementsAddUser = document.getElementsByClassName("btn_AddUser");
            let elementsEditUser = document.getElementsByClassName("btn_EditUser");
            let elementsDelUser = document.getElementsByClassName("btn_DeleteUsers");
            //Role
            let elementsAddRole = document.getElementsByClassName("btn_AddRole");
            let elementsEditRole = document.getElementsByClassName("btn_EditRole");
            let elementsDelRole = document.getElementsByClassName("btn_DeleteRole");

            //Views
            //GRH
            let elementsViewGRH = document.getElementsByClassName("btn_ViewGRH");
            //PIH
            let elementsViewPIH = document.getElementsByClassName("btn_ViewPIH");
            //PRH
            let elementsViewPRH = document.getElementsByClassName("btn_ViewPRH");
            //PTH
            let elementsViewPTH = document.getElementsByClassName("btn_ViewPTH");

            //OperationModule
            //IC
            let elementsAddInvCount = document.getElementsByClassName("btn_AddInvCount");
            //DayPass
            let elementsAddDayPass = document.getElementsByClassName("btn_AddDayPass");


            var btnAccess = [];
            btnAccess = msg.d.split('|');
        //    alert(msg.d);
            if (btnAccess[0] == "0") {
                //GRH
                for (let i = 0; i < elementsViewGRH.length; i++) {
                    elementsViewGRH[i].style.visibility = "hidden";
                }
                //PIH
                for (let i = 0; i < elementsViewPIH.length; i++) {
                    elementsViewPIH[i].style.visibility = "hidden";
                }
                //PRH
                for (let i = 0; i < elementsViewPRH.length; i++) {
                    elementsViewPRH[i].style.visibility = "hidden";
                }
                //PTH
                for (let i = 0; i < elementsViewPTH.length; i++) {
                    elementsViewPTH[i].style.visibility = "hidden";
                }
            }
            if (btnAccess[1] == "0") {
                //FilesData
                //Category
                for (let i = 0; i < elementsAddCat.length; i++) {
                    elementsAddCat[i].style.visibility = "hidden";
                }
                ////Brand
                for (let i = 0; i < elementsAddBrand.length; i++) {
                    elementsAddBrand[i].style.visibility = "hidden";
                }
                ////Model
                for (let i = 0; i < elementsAddModel.length; i++) {
                    elementsAddModel[i].style.visibility = "hidden";
                }
                //Classification
                for (let i = 0; i < elementsAddClass.length; i++) {
                    elementsAddClass[i].style.visibility = "hidden";
                }
                ////Unit of Measurement
                for (let i = 0; i < elementsAddUOM.length; i++) {
                    elementsAddUOM[i].style.visibility = "hidden";
                }
                //Supplier
                for (let i = 0; i < elementsAddSupplier.length; i++) {
                    elementsAddSupplier[i].style.visibility = "hidden";
                }
                //Site
                for (let i = 0; i < elementsAddSite.length; i++) {
                    elementsAddSite[i].style.visibility = "hidden";
                }
                //Location
                for (let i = 0; i < elementsAddLocation.length; i++) {
                    elementsAddLocation[i].style.visibility = "hidden";
                }
                //Area
                for (let i = 0; i < elementsAddArea.length; i++) {
                    elementsAddArea[i].style.visibility = "hidden";
                }
                //Department
                for (let i = 0; i < elementsAddDepartment.length; i++) {
                    elementsAddDepartment[i].style.visibility = "hidden";
                }
                //Job Title
                for (let i = 0; i < elementsAddPosition.length; i++) {
                    elementsAddPosition[i].style.visibility = "hidden";
                }
                //Custodian
                for (let i = 0; i < elementsAddCustodian.length; i++) {
                    elementsAddCustodian[i].style.visibility = "hidden";
                }
                //Property
                for (let i = 0; i < elementsAddProperty.length; i++) {
                    elementsAddProperty[i].style.visibility = "hidden";
                }
                //Stock
                for (let i = 0; i < elementsAddStock.length; i++) {
                    elementsAddStock[i].style.visibility = "hidden";
                }
                //Chart of Accounts
                for (let i = 0; i < elementsAddCOA.length; i++) {
                    elementsAddCOA[i].style.visibility = "hidden";
                }
                //Task
                for (let i = 0; i < elementsAddTask.length; i++) {
                    elementsAddTask[i].style.visibility = "hidden";
                }
                //Tag
                for (let i = 0; i < elementsAddTag.length; i++) {
                    elementsAddTag[i].style.visibility = "hidden";
                }
                //SecurityQuestion
                for (let i = 0; i < elementsAddSecurityQuestion.length; i++) {
                    elementsAddSecurityQuestion[i].style.visibility = "hidden";
                }

                //SystemSettings
                //DeviceMngt
                for (let i = 0; i < elementsAddDeviceMngt.length; i++) {
                    elementsAddDeviceMngt[i].style.visibility = "hidden";
                }

                //UserMngt
                //User
                for (let i = 0; i < elementsAddUser.length; i++) {
                    elementsAddUser[i].style.visibility = "hidden";
                }
                //Role
                for (let i = 0; i < elementsAddRole.length; i++) {
                    elementsAddRole[i].style.visibility = "hidden";
                }

                //InvCount
                for (let i = 0; i < elementsAddInvCount.length; i++) {
                    elementsAddInvCount[i].style.visibility = "hidden";
                }
                //Daypass
                for (let i = 0; i < elementsAddDayPass.length; i++) {
                    elementsAddDayPass[i].style.visibility = "hidden";
                }

            }
            if (btnAccess[2] == "0") {
                //FilesData
                //Category
                for (let i = 0; i < elementsEditCat.length; i++) {
                    elementsEditCat[i].style.visibility = "hidden";
                }
                //Brand
                for (let i = 0; i < elementsEditBrand.length; i++) {
                    elementsEditBrand[i].style.visibility = "hidden";
                }
                //Model
                for (let i = 0; i < elementsEditModel.length; i++) {
                    elementsEditModel[i].style.visibility = "hidden";
                }
                //Classification
                for (let i = 0; i < elementsEditClass.length; i++) {
                    elementsEditClass[i].style.visibility = "hidden";
                }
                //UOM
                for (let i = 0; i < elementsEditUOM.length; i++) {
                    elementsEditUOM[i].style.visibility = "hidden";
                }
                //Supplier 
                for (let i = 0; i < elementsEditSupplier.length; i++) {
                    elementsEditSupplier[i].style.visibility = "hidden";
                }
                //Site 
                for (let i = 0; i < elementsEditSite.length; i++) {
                    elementsEditSite[i].style.visibility = "hidden";
                }
                //Location 
                for (let i = 0; i < elementsEditLocation.length; i++) {
                    elementsEditLocation[i].style.visibility = "hidden";
                }
                //Department
                for (let i = 0; i < elementsEditArea.length; i++) {
                    elementsEditArea[i].style.visibility = "hidden";
                }
                //Office 
                for (let i = 0; i < elementsEditDepartment.length; i++) {
                    elementsEditDepartment[i].style.visibility = "hidden";
                }
                //Position 
                for (let i = 0; i < elementsEditPosition.length; i++) {
                    elementsEditPosition[i].style.visibility = "hidden";
                }
                //Custodian 
                for (let i = 0; i < elementsEditCustodian.length; i++) {
                    elementsEditCustodian[i].style.visibility = "hidden";
                }
                //Property 
                for (let i = 0; i < elementsEditProperty.length; i++) {
                    elementsEditProperty[i].style.visibility = "hidden";
                }
                //Stock 
                for (let i = 0; i < elementsEditStock.length; i++) {
                    elementsEditStock[i].style.visibility = "hidden";
                }
                //COA 
                for (let i = 0; i < elementsEditCOA.length; i++) {
                    elementsEditCOA[i].style.visibility = "hidden";
                }
                //Task 
                for (let i = 0; i < elementsEditTask.length; i++) {
                    elementsEditTask[i].style.visibility = "hidden";
                }
                //Tag
                for (let i = 0; i < elementsEditTag.length; i++) {
                    elementsEditTag[i].style.visibility = "hidden";
                }
                //SecurityQuestion
                for (let i = 0; i < elementsEditSecurityQuestion.length; i++) {
                    elementsEditSecurityQuestion[i].style.visibility = "hidden";
                }

                //SystemSettings
                //DeviceMngt
                for (let i = 0; i < elementsEditDeviceMngt.length; i++) {
                    elementsEditDeviceMngt[i].style.visibility = "hidden";
                }

                //UserMngt
                //User
                for (let i = 0; i < elementsEditUser.length; i++) {
                    elementsEditUser[i].style.visibility = "hidden";
                }
                //Role
                for (let i = 0; i < elementsEditRole.length; i++) {
                    elementsEditRole[i].style.visibility = "hidden";
                }

               

                
            }
            if (btnAccess[3] == "0") {
                //FilesData
                //Category
                for (let i = 0; i < elementsDelCat.length; i++) {
                    elementsDelCat[i].style.visibility = "hidden";
                }
                //Brand
                for (let i = 0; i < elementsDelBrand.length; i++) {
                    elementsDelBrand[i].style.visibility = "hidden";
                }
                //Model
                for (let i = 0; i < elementsDelModel.length; i++) {
                    elementsDelModel[i].style.visibility = "hidden";
                }
                //Classification
                for (let i = 0; i < elementsDelClass.length; i++) {
                    elementsDelClass[i].style.visibility = "hidden";
                }
                //UOM
                for (let i = 0; i < elementsDelUOM.length; i++) {
                    elementsDelUOM[i].style.visibility = "hidden";
                }
                //Supplier
                for (let i = 0; i < elementsDelSupplier.length; i++) {
                    elementsDelSupplier[i].style.visibility = "hidden";
                }
                //Site
                for (let i = 0; i < elementsDelSite.length; i++) {
                    elementsDelSite[i].style.visibility = "hidden";
                }
                //Location 
                for (let i = 0; i < elementsDelLocation.length; i++) {
                    elementsDelLocation[i].style.visibility = "hidden";
                }
                //Department 
                for (let i = 0; i < elementsDelArea.length; i++) {
                    elementsDelArea[i].style.visibility = "hidden";
                }
                //Office 
                for (let i = 0; i < elementsDelDepartment.length; i++) {
                    elementsDelDepartment[i].style.visibility = "hidden";
                }
                //Position 
                for (let i = 0; i < elementsDelPosition.length; i++) {
                    elementsDelPosition[i].style.visibility = "hidden";
                }
                //Custodian 
                for (let i = 0; i < elementsDelCustodian.length; i++) {
                    elementsDelCustodian[i].style.visibility = "hidden";
                }
                //Property 
                for (let i = 0; i < elementsDelProperty.length; i++) {
                    elementsDelProperty[i].style.visibility = "hidden";
                }
                //Stock 
                for (let i = 0; i < elementsDelStock.length; i++) {
                    elementsDelStock[i].style.visibility = "hidden";
                }
                //COA 
                for (let i = 0; i < elementsDelCOA.length; i++) {
                    elementsDelCOA[i].style.visibility = "hidden";
                }
                //Task 
                for (let i = 0; i < elementsDelTask.length; i++) {
                    elementsDelTask[i].style.visibility = "hidden";
                }
                //Tag
                for (let i = 0; i < elementsDelTag.length; i++) {
                    elementsDelTag[i].style.visibility = "hidden";
                }
                //SecurityQuestion
                for (let i = 0; i < elementsDelSecurityQuestion.length; i++) {
                    elementsDelSecurityQuestion[i].style.visibility = "hidden";
                }

                //SystemSettings
                //DeviceMngt
                for (let i = 0; i < elementsDelDeviceMngt.length; i++) {
                    elementsDelDeviceMngt[i].style.visibility = "hidden";
                }

                //UserMngt
                //User
                for (let i = 0; i < elementsDelUser.length; i++) {
                    elementsDelUser[i].style.visibility = "hidden";
                }
                //Role
                for (let i = 0; i < elementsDelRole.length; i++) {
                    elementsDelRole[i].style.visibility = "hidden";
                }
            }

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

function ReplaceStr(str) {
    var newStr = str.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
    return newStr;
}

function EnterKey(event) {
    var x = event.keyCode;
    if (x == 13) {  // 27 is the ESC key
        return false;
    }
}

function onlyNos(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        if (charCode == 13) {
            return false;
            preventDefault();
        }
        return true;
    }
    catch (err) {
        alert(err.Description);
    }
}