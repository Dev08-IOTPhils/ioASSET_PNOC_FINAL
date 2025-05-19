var idForUpdate = 0;
var currentPage = 1;

$(document).ready(function () {
    $('.select2').select2();
});

function GetConnectionString() {
    var ddlDB = $('#BodyHolder_ddlDatabase').val();

    $('#txtConnection').val(ddlDB);
}