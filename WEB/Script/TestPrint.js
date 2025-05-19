$(document).ready(function()
{
    getData();
})

function TestPrint()
{
    window.location.replace("Operation_Module/TestPrint.aspx");
    //Response.Redirect(ResolveUrl("~/Operation_Module/TestPrint.aspx"));
}

function getData()
{
    $.ajax({
        type: "POST",
        url: "Operation_Module/TestPrint.aspx/GetApproverByCustodian",
        data: " { receivedby : '" + ddlReceivedBy + "'} ",
        //data : "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
            //ddlCustodian.append('<option value="0">--- Select Approver ---</option>');
            $.each(msg.d, function () {
                ddlApprover.append($("<option></option>").val(this['Text']).html(this['Value']));
            });
        },
        error: function (msg1) {
            ddlApprover.empty().append('<option selected="selected" value="0">--- Select Approver ---</option>');
        }
    });
}