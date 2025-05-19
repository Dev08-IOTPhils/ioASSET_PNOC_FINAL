using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_regSEPI : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Add(" Select Filter ");
            drpFilterBy.Items.Add("ItemCode");
            drpFilterBy.Items.Add("PropertyNumber");
            drpFilterBy.Items.Add("ClassificationName");
            drpFilterBy.Items.Add("CategoryName");
            drpFilterBy.Items.Add("BrandName");
            drpFilterBy.Items.Add("ModelName");

        }
        catch
        {
        }
    }
    [WebMethod]
    public static string GetList(int type, string AccessButton, string datefrom, string dateto)
    {
        regSEPIBll z = new regSEPIBll();
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        List<LedgerCardEntity> JsonItemList = new List<LedgerCardEntity>();
        string html = string.Empty;
        try
        {

            string Filter = "";
            DataTable dtFilter = y.GetActiveFilter("regSEPI", Common.ConnString, Common.UserName, "1");
            if (dtFilter.Rows.Count > 0)
            {
                for (int i = 0; i < dtFilter.Rows.Count; i++)
                {
                    Regex reg = new Regex("[*'\";]");
                    string Value = reg.Replace(dtFilter.Rows[i]["Values"].ToString(), string.Empty);
                    string OperatorValue = dtFilter.Rows[i]["Operator"].ToString();
                    if (OperatorValue == "Not =")
                    {
                        OperatorValue = " !='" + Value + "' ";
                    }
                    else if (OperatorValue == "Like")
                    {
                        OperatorValue = " like '%" + Value + "%' ";
                    }

                    else if (OperatorValue == "In" || OperatorValue == "Not In")
                    {
                        if (Value.Contains(","))
                        {
                            string[] multipleValue = Value.Split(',');
                            string itemValue = "";
                            foreach (var item in multipleValue)
                            {
                                itemValue += "'" + item + "',";
                            }
                            OperatorValue = " " + OperatorValue + " (" + itemValue.Remove(itemValue.Length - 1) + ") ";
                        }
                        else
                        {
                            OperatorValue = " " + OperatorValue + " ('" + Value + "')";
                        }
                    }
                    else if (OperatorValue == ">" || OperatorValue == ">=" || OperatorValue == "<" || OperatorValue == "<=")
                    {
                        OperatorValue = " " + OperatorValue + Value;
                    }
                    else
                    {
                        OperatorValue = OperatorValue + " '" + Value + "'";
                    }
                    //if (i == 0)
                    //{
                    //    Filter += " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                    //}
                    //else
                    //{
                    //    Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                    //}

                    Filter += " " + dtFilter.Rows[i]["Logic"].ToString() + " [" + dtFilter.Rows[i]["FilterBy"].ToString() + "] " + OperatorValue;
                }
            }


            DataTable dt = z.GetList(Common.ConnString, type, Filter, datefrom, dateto);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LedgerCardEntity x = new LedgerCardEntity();
                    x.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.LongDescription = dt.Rows[i]["LongDescription"].ToString();
                    x.RemainingUsefulLife = dt.Rows[i]["RemainingUsefulLife"].ToString();
                    x.Amount = dt.Rows[i]["Amount"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    //if (AccessButton == "0")
                    //{
                    //    x.Action = "";
                    //}
                    //else
                    //{
                    //    x.Action = "<center><a href='' class='btn btn-xs btn-primary btn_View ' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewAssets('" + dt.Rows[i]["PropertyNumber"].ToString() + "')\"><span class='fa fa-eye'></span></a> <a href='' class='btn btn-xs btn-primary btn_View' id'rsepixcl' data-toggle='tooltip'  title='View List of Properties' onclick=\"GenRSEPIexcel('" + dt.Rows[i]["PropertyNumber"].ToString() + "')\"><span class='fa fa-download'></span></a></center>";
                    //}

                    JsonItemList.Add(x);

                }

                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList);


                return html;
            }

            else { return ""; }



        }
        catch (Exception ex)
        {
            return "";
        }
    }

    //[WebMethod]
    //public static string GetList(string datefrom, string dateto)
    //{
    //    LedgerCardEntity x = new LedgerCardEntity();
    //    regSEPIBll y = new regSEPIBll();

    //    //x.PropertyNo = propertyno;
    //    x.ConnectionString = Common.ConnString;

    //    DataTable dt = y.getlists(x, datefrom, dateto);

    //    return GenerateregSEPI(dt);
    //}



    [WebMethod]
    public static string GenerateReport(string datefrom, string dateto)
    {
        LedgerCardEntity x = new LedgerCardEntity();
        regSEPIBll y = new regSEPIBll();

        //x.PropertyNo = propertyno;
        x.ConnectionString = Common.ConnString;

        DataTable dt = y.getlists(x, datefrom, dateto);

        return GenerateregSEPI(dt);
    }

    public static string GenerateregSEPI(DataTable dt)
    {
        try
        {
            MiscBll miscBll = new MiscBll();
            MiscEntity miscEntity = new MiscEntity();

            string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

            string x = miscBll.GetCompanyInformation(Common.ConnString);
            string CompanyName = x.Split('|')[0];
            string CompanyContactNumber = x.Split('|')[3];
            string CompanyAddress = x.Split('|')[5];

            string html = "";
            #region styles
            //style
            html += "<style>"
            + ".page-header, .page-header-space {height: 265px; }"
            + ".page-footer, .page-footer-space {height: 200px;}"
            + ".page-footer {position: fixed;bottom: 0;width: 100%; }"
            + ".page-header {position: fixed;top: 0mm;width: 100%;  }"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; size: landscape; margin: 10mm;}"

            + "@media print {"
                + "table {padding: 0px 10px 0px 0px;}"
                + "thead {display: table-header-group; }"
                + "tfoot {display: table-footer-group;}"
                + "body { margin: 0;}"
            + "}"
            + "</style >";
            #endregion

            #region div header and footer
            #region header
            //header div
            html += "<div class='page-header'>";
            html += "<table style='width:100%;font-family: Arial;'>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px'; /></td</tr>"
                + "<tr><td colspan='2'; align='center' style='font-size:15;'><br><b>REGISTRY OF SEMI-EXPENDABLE PROPERTY ISSUED<b><br/><br/><br/></td</tr>"
                + "<tr><td align='left' style='font-size:13;'>Entity Name: <u>" + CompanyName + "</u></td><td align='right' style='font-size:13;'>Fund Cluster: ______________________</td></tr>"
                + "<tr><td align='left' style='font-size:13;'>Semi-expendable Property: <u>" + "______________________" + "</u></td><td align='right' style='font-size:13;'>Sheet No.: ______________________</td></tr>"
                + "</table>";
            html += "</div>";

            #endregion header

            #region footer
            //footer div
            html += "<div class='page-footer'>";
            //html += "<table cellspacing='0' class='table table-bordered' style='border-collapse:collapse; font-size:10; font-family:Arial;' border='0' width='100%'>";
            ////html += "<tr><td style='width:60%'><br/><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</br></br></br></br>&emsp;&emsp;Requested by:&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:<br/><br/>&emsp;&emsp;&emsp;<u>" + transactorname + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>" + approvername + "</u><br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of <br/>&emsp;&emsp;&emsp;&emsp;&emsp; Accountable Officer) &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Authorized Officer) <br/><br/>&emsp;&emsp;&emsp;<u>" + transactorposition + "</u>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;___________________________<br/>&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Accountable Official)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Designation of Authorized Official)<br/><br/></td>"
            ////    + "<td align='left';valign='top' style='width:40%'><br/> I CERTIFY that I have inspected each &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;I CERTIFY that I have <br/> and every article enumerated in this report,&emsp;&emsp;&emsp;&emsp;witnessed the disposition of the<br/>and that the disposition made therof was, &emsp;&emsp;&emsp;&emsp; articles enumerated on this <br/> in my judgement, the best for the public &emsp;&emsp;&emsp;&emsp;&emsp; report this __day of <br/>interest. &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;_____________,______<br/><br/><br/>________________________________&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;________________________<br/>&emsp;(Signature over Printed Name of &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;(Signature over Printed Name of<br>&emsp;&emsp;&emsp;&emsp;&emsp;Inspection Officer)&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Witness)</td></tr></table>";
            //html += "<tr><td style='border-top:1px solid;border-left:1px solid;width:60%;'  colspan='2'><br/>&emsp;I HEREBY request inspection and disposition, pursuant to Section 79 of PD 1445, of the property enumerated above.</td><td style='border-top:1px solid;border-left:1px solid;width:25%;padding-right:35px'></br>&emsp;I CERTIFY that I have inspected each and every article enumerated in this report, and that the disposition made thereof was, in my judgment, the best for the public interest.</td><td style='border-top:1px solid;border-right:1px solid;width:15%'></br>&emsp;I CERTIFY that I have witnessed the disposition of the articles enumerated on this report this ____day of _____________, _____.</td></tr>"
            //    + "<tr><td style='border-bottom:1px solid;border-left:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;Requested by:</br></br><center><u>" + transactorname + "</u></br>(Signature over Printed Name of </br> Accountable Officer))</br></br><u>" + transactorposition + "</u></br>(Designation of Accountable Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;width:30%'><br/><br/>&emsp;&emsp;&emsp;&emsp;&emsp;Approved by:</br></br><center><u>" + approvername + "</u></br>(Signature over Printed Name of </br> Authorized Official)</br></br><u>" + "" + "</u></br>(Designation of Authorized Official)</center></td><td style='border-bottom:1px solid;'><center><u>" + inspectorname + "</u></br>(Signature over Printed Name of </br> Inspection Officer)</center></td><td style='border-bottom:1px solid;border-right:1px solid;'><center><u>" + certifiername + "</u></br>(Signature over Printed Name of </br> Witness)</center></td></tr></table>";
            html += "</div>";
            #endregion
            #endregion

            #region header placeholder
            //table header
            html += "<table cellspacing='0' style='width:100%;'><thead><tr><td><div class='page-header-space'>";
            html += "</div></td></tr></thead>";
            #endregion

            #region body placeholder
            //table body
            html += "<tbody style='font-family: Arial; font-size:9;'><tr><td><div class='page-body' style='line-height: 1;'>";

            html += "<tr>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Date</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Reference</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Item Description</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Estimated Useful Life</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Issued</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Returned</b></td>"
             + "<td colspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Re-issued</b></td>"
             + "<td style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Disposed</b></td>"
             + "<td style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Balance</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;'><b>Amount</b></td>"
             + "<td rowspan='2'; style='text-align: center; border-top:1px solid; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'><b>Remarks</b></td>"
             + "</tr>"
             + "<tr>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>ICS/RRSP No.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Semi-expendable Property No.</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Group/Officer</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Group/Officer</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Group/Officer</td>"
             + "<td style='text-align: center; border-left:1px solid;border-bottom:1px solid;';>Qty.</td><td style='text-align: center; border-left:1px solid;border-bottom:1px solid;' ;>Qty.</td>"
             + "</tr>";

            if (dt.Rows.Count > 0)
            {
                bool reIssuance = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TransType"].ToString() == "Issuance" && reIssuance)
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                        reIssuance = false;
                    }
                    else if (dt.Rows[i]["TransType"].ToString() == "Issuance" && !reIssuance)
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                    }
                    else if (dt.Rows[i]["TransType"].ToString() == "Return")
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["EmployeeName"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + BasePage.ConvertToMoneyFormat(dt.Rows[i]["Amount"].ToString()) + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                    }
                    else
                    {
                        html += "<tr>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["CreatedDate"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["TransNum"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["LongDescription"].ToString() + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + dt.Rows[i]["RemainingUsefulLife"].ToString() + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:10%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "1" + "</td>"
                              + "<td  style='width:4%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:7%; text-align: center; border-bottom:1px solid;border-left:1px solid;'>" + "0" + "</td>"
                              + "<td  style='width:8%; text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;'>" + dt.Rows[i]["Remarks"].ToString() + "</td>"
                              + "</tr>";
                    }

                }
                //  html += "<tr><td colspan='11' align='center'><i><b>***Nothing Follows***</b></i></td></tr>";
            }
            else
            {
                //  html = "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            html += "</div></td></tr></tbody>";
            #endregion


            #region footer placeholder
            //table footer
            html += "<tfoot><tr><td><div class='page footer page-footer-space'>";
            html += "</div></td></tr></tfoot></table>";
            #endregion

            return html;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }
}