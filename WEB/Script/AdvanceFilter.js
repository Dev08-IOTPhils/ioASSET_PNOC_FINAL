$(document).ready(function () {
    $('#drpFilterBy').change(function () {
        hideShow();

    });

    $('#drpOperator').change(function () {
        NumberText();
        hideShow();
    });

    hideShow();
    loadFilters();

   
    resetFilterfields()


    var currentDate = new Date();
    $(".form-control datetimepicker-input").datepicker({
        maxDate: currentDate
    });

    $('.select2').select2();

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $('#txtDateFrom').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateFrom').datepicker('setDate', today);

    $('#txtDateTo').datepicker({
        todayHighlight: true,
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        autoclose: true
    });
    $('#txtDateTo').datepicker('setDate', today);



})



//function getallForReports() {
//    //var ReportID = $('#txbReportID').val();
//    var DateFr = $('#txbReportID').val();
//    var DateTo = $('#txbReportID').val();
//    var ReportID = (window.location.href).split('/').pop();
//    var urll = (window.location.href).split('/').pop() + '/GetAdvancefilterperUser';
//    $.ajax({
//        type: "POST",
//        url: urll,
//        data: "{ReportID : '" + ReportID + "',DateFrom : '" + DateFr + "',DateTo : '" + DateTo + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            myJsondata = data;
//            populateTables(myJsondata);
//            console.log(data);
//            console.log('Success');
//        },
//        error: function (e) {
//            console.log("There was an error with your request...");
//            console.log("error: " + JSON.stringify(e));
//        }
//    })
//}


//var dt
//function populateTables(data) {
//    try {
//        dt = $('#tblDisplay').DataTable({
//            "destroy": true,
//            "processing": true,
//            "pagingType": "full_numbers",
//            "scrollX": true,
//            "data": JSON.parse(data.d),
//            "columns":
//                [
//                    { "data": "userID" },
//                    { "data": "username" },
//                    { "data": "FirstName" },
//                    { "data": "LastName" },
//                    { "data": "address" },
//                    { "data": "ContactNumer" },
//                    { "data": "email" },
//                    { "data": "Password" },
//                    { "data": "Department" },
//                    { "data": "Area" },
//                    { "data": "DateCreated" }
//                ],
//            "select": {
//                'style': 'multi'
//            },
//            dom: 'Bfrtip',
//            stateSave: true,
//            buttons: {
//                buttons: [
//                    'copy',
//                    'excel',
//                    'csv',
//                    // 'pdf',
//                    'print',
//                    'colvis'
//                ],
//                dom: {
//                    container: {
//                        className: "dt-buttons btn-group flex-wrap"
//                    },
//                    button: {
//                        className: "btn btn-secondary "
//                    }
//                }
//            }
//        });
//        $('#tblDisplay').off('click');


//        $('<div class="pull-right">' +
//       '<select class="form-control">' +
//       '<option >ACTION</option>' +
//       '<option value="volvo">Volvo</option>' +
//       '<option value="saab">Saab</option>' +
//       '<option value="opel">Opel</option>' +
//       '</select>' +
//       '</div>').appendTo("#tblDisplay_wrapper .dataTables_filter"); //example is our table id

//        $(".dataTables_filter label").addClass("pull-right");


//    } catch (e) {
//        var table = $('#tblDisplay').DataTable();
//        table
//            .clear()
//            .draw();
//    }
//    PopulatefilterBy();
//}

/////////////////////////////////////////////////////////////
/////                   FILTER SECTION                  /////
/////////////////////////////////////////////////////////////

function getfilesDataForFilter()
{
    var urll = (window.location.href).split('/').pop() + '/getfilesDataForFilter';

    $.ajax({
        type: "POST",
        // url: "Reports.aspx/GetUserByfilter",
        url:urll,
        data: "{ReportID : '" + ReportID + "',DateFrom : '" + DateFr + "',DateTo : '" + DateTo + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsondata = data;
            populateTables(myJsondata);

            console.log(data);
            console.log('Success');

        },
        error: function (e) {
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    })
}


var filesData = ['Department', 'Area'] // temporary list of filesdata
var cols = [];
var ValueFlag = '';

function hideShow() {

    if (sa.includes($('#drpOperator').val())) {
        ///Multiple Select textbox show
        document.getElementById('txtValue').value = null;
        document.getElementById('drpValue').innerHTML = "";
        document.getElementById('multiSelect').innerHTML = "";

        document.getElementById("txtValue").style.display = 'none';
        $('#spandropdown').hide();
        $('#MultiSelect1').show();
        multiselect();
        ValueFlag = 'multiple';
    }
    else {
        if (filesData.includes($('#drpFilterBy').val())) {
            ///dropdown input show
            document.getElementById('txtValue').value = null;
            document.getElementById('drpValue').innerHTML = "";
            document.getElementById('multiSelect').innerHTML = "";

            document.getElementById("txtValue").style.display = 'none';
            $('#spandropdown').show();
            $('#MultiSelect1').hide();

            ValueFlag = 'dropdown';
            fillValue();
        }
        else {
            ////TextBox input show
            document.getElementById('txtValue').value = null;
            document.getElementById('drpValue').innerHTML = "";
            document.getElementById('multiSelect').innerHTML = "";

            document.getElementById("txtValue").style.display = '';
            $('#spandropdown').hide();
            $('#MultiSelect1').hide();

            ValueFlag = 'textbox';
        }
    }
}

function loadFilters() {
    //getallForReports();
    load_data(); // property Masterlist
    getADVFilter();
    resetFilterfields();
    hideShow();

}

function NumberText() //// forcing textbox, only receive Number when operator has > or <
{
    var drpFilter = $('#drpOperator').val();
    if (drpFilter.includes('>') || drpFilter.includes('<')) {
        $("#txtValue").attr("type", "number");
    }
    else {
        $("#txtValue").attr("type", "text");
    }
}
var sa = ['Exists', 'In', 'Not Exists', 'Not In'];

function multiselect() {
    var val = $('#drpOperator').val()
    if (sa.includes(val)) {
        fillValueMultiSelect();
    }
}


//function PopulatefilterBy() {
//    document.getElementById('drpFilterBy').innerHTML = "";
//    var $dropdown = $("#drpFilterBy");
//    var columns = dt.settings().init().columns;

//    dt.columns().every(function (index) {
//        cols.push(columns[index].data)
//        $dropdown.append($("<option />").val(columns[index].data).text(columns[index].data)); //adding data on dropdown 
//    })
//}

//////////////  ADDING OF ADV. FILTER  //////////////
function AddFilter(trx) {


    var urll = (window.location.href).split('/').pop() + '/AddAdvanceFilter';
    //alert(urll);

    var Logic = $('#drpLogic').val();
    var operator = $('#drpOperator').val();
    var filterby = $('#drpFilterBy').val();
    //var ReportID = $('#txbReportID').val();
    var ReportID = (window.location.href).split('/').pop();
    var dropValue = $('#drpValue').val();
    var Value = '';//(isDropdown == '' ? $('#txtValue').val() : $('#drpValue').val())
    switch (ValueFlag) {
        case 'multiple':
            Value = $('#multiSelect').val();
            break;
        case 'dropdown':
            Value = $('#drpValue').val();
            break;
        case 'textbox':
            Value = $('#txtValue').val();
            break;
        default:
            Value = ''
            break;
    }

    if (Logic == "Select Logic" || operator == "Select Operator" || Value == '') {
        swal({
            title: "",
            text: "Kindly fill-up all fields",
            icon: "warning",
        });
    }
    else {
        $.ajax({
            type: "POST",
            url:urll,
            //url: "Reports.aspx/AddAdvanceFilter",
            data: "{ReportID:'" + ReportID + "', Logic:'" + Logic + "', FilterBY:'" + filterby + "', Operator:'" + operator + "', val:'" + Value + "', trx:'" + trx + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == 1) {
                    swal({
                        title: "Alright!",
                        text: "Filter Added",
                        icon: "success",
                    });
                    resetFilterfields()
                    loadFilters();
                }
            }
        })
    }
}

function resetFilterfields() {
    $('#drpOperator').val("Select Operator") //operator 
    $('#drpLogic').val("Select Logic")  //Logic

    document.getElementById('txtValue').value = null;  //value textbox
    document.getElementById('drpValue').innerHTML = ""; //value dropdown
    document.getElementById('multiSelect').innerHTML = ""; //value dropdown

}




///////// USED ADV FILTER ////////// not done yet
function UsedADVFilter() {

    if ($('#UsedFilter').hasClass("btn btn-xs btn-secondary")) {
        $('#UsedFilter').removeClass("btn btn-xs btn-secondary");
        $('#UsedFilter').attr("class", "btn btn-xs btn-primary");
        //alert("used");
    }
    else {
        $('#UsedFilter').removeClass("btn btn-xs btn-primary");
        $('#UsedFilter').attr("class", "btn btn-xs btn-secondary");
        //alert("Not Used");
    }
}


///////// DELETE FILTER //////////
function ModADVfilter(RecID, trx) {
    if (trx == "DELETE") {
        swal({
            title: "Delete this filter??",
            text: "Once deleted, you will not be able to recover this Filter!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    ModifyAdvFilter(RecID, trx);
                } else {
                    swal("Ok noted!");
                }
            });
    }
    else {
        ModifyAdvFilter(RecID, trx);
        //UsedADVFilter();
    }
}


function ModifyAdvFilter(RecID, trx) {

    var urll = (window.location.href).split('/').pop() + '/ADVfilterModification';

    $.ajax({
        type: "POST",
        url: urll,
        data: "{RecID:'" + RecID + "', trx:'" + trx + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 0) {
                swal("Poof! Filter has been deleted!", {
                    icon: "success",
                });
            }
            //if (trx == "DELETE") {
            //    loadFilters();
            //}
            //else {
            //    getallForReports();

            //}
            loadFilters();
        }
    })
}

/////////////////// Populate DataTable for filter ///////////////////

function getADVFilter() {
   // var ReportID = $('#txbReportID').val();
    var ReportID = (window.location.href).split('/').pop();

    var urll = (window.location.href).split('/').pop() + '/GetAdvancefilterperUser';

    $.ajax({
        type: "POST",
        url: urll,
        data: "{ReportID:'" + ReportID + "',trx :'Select'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            myJsondata = data;

            populateTables1(myJsondata);

            console.log(data);
            console.log('Success');

        },
        error: function (e) {
            console.log("There was an error with your request...");
            console.log("error: " + JSON.stringify(e));
        }
    })
}
var dtFilter;

function populateTables1(data) {
    try {
        dtFilter = $('#tblFilterDisplay').DataTable({
            "destroy": true,
            "processing": true,
            "pagingType": "full_numbers",
            "scrollX": true,
            "data": JSON.parse(data.d),
            "columns":
                [
                    { "data": "Logic" },
                    { "data": "FilterBy" },
                    { "data": "Operator" },
                    { "data": "Values" },
                    { "data": "ReportID" }
                ],
            dom: 't', //tip
        });
        $('#tblFilterDisplay').off('click');
    } catch (e) {
        var table = $('#tblFilterDisplay').DataTable();
        table
            .clear()
            .draw();
    }
}

//////// populate dropdown Value ///////
function fillValue() {
    //document.getElementById('drpValue').innerHTML = "";
    var x = document.getElementById("drpFilterBy").selectedIndex;
    var y = document.getElementById("drpFilterBy").options;

    var data1 = dt
        .column(y[x].index)
        .data();

    for (var i = 0; i < data1.length; i++) {
        $("#drpValue").append($("<option />").val(data1[i]).text(data1[i]));
    }
    removedup("drpValue");
}

function removedup(elem) {
    var optionValues = [];
    if (elem == "drpValue") {
        $('#drpValue option').each(function () {
            if ($.inArray(this.value, optionValues) > -1) {
                $(this).remove()
            } else {
                optionValues.push(this.value);
            }
        });
    }
    else {
        $('#drpFilterBy option').each(function () {
            if ($.inArray(this.value, optionValues) > -1) {
                $(this).remove()
            } else {
                optionValues.push(this.value);
            }
        });
    }
}

//////// populate dropdown MultiSelectValue ///////
function fillValueMultiSelect() {
    //document.getElementById('drpValue')drpFilterBy.innerHTML = "";
    var x = document.getElementById("drpFilterBy").selectedIndex;
    var y = document.getElementById("drpFilterBy").options;

    var data1 = dt
        .column(y[x].index)
        .data();

    for (var i = 0; i < data1.length; i++) {
        $("#multiSelect").append($("<option />").val(data1[i]).text(data1[i]));
    }
    removedup("");
}


