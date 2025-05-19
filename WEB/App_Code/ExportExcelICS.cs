using AdvanceBll;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExportExcelICS
/// </summary>
public class ExportExcelICS
{
    static string CompanyName = "";
    static string CompanyContactNumber = "";
    static string CompanyAddress = "";

    static string Supplier = "";

    public ExportExcelICS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string ExportToExcel(DataTable dt, string issuanceno, string employeename, string employeedepartment, string employeeposition, string transactorname, string transactordepartment, string transactorposition, string issueddate, string remarks, string custodian, string approvername)
    {
        //Get company info
        MiscBll miscBll = new MiscBll();
        string x = miscBll.GetCompanyInformation(Common.ConnString);
        CompanyName = x.Split('|')[0];
        CompanyContactNumber = x.Split('|')[3];
        CompanyAddress = x.Split('|')[5];


        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("RIS");
            //TITLE START
            var IARText = worksheet.Range("A4:H4").Merge();
            worksheet.Cell(4, 1).Value = "INVENTORY CUSTODIAN  SLIP";
            IARText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            IARText.Style.Font.Bold = true;
            IARText.Style.Font.FontSize = 16;
            //TITLE END --------------------------------------------------------------------------

            //HEADER START
            var EntiText = worksheet.Cell("A6");
            var ClusText = worksheet.Cell("B7");
            var ClusText1 = worksheet.Cell("G7");
            worksheet.Cell(6, 1).Value = "Entity Name: ";
            worksheet.Cell(7, 1).Value = "Fund Cluster: ";
            worksheet.Cell(7, 7).Value = "ICS No : ";
            EntiText.Style.Font.Bold = true;
            ClusText.Style.Font.Bold = true;
            ClusText1.Style.Font.Bold = true;


            var a1 = worksheet.Range("A9:A11").Merge();
            worksheet.Cell(9, 1).Value = "Quantity";
            a1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.CenterContinuous;
            a1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a1.Style.Font.Bold = true;
            a1.Style.Font.FontSize = 11;
            a1.Style.Alignment.WrapText = true;

            var a2 = worksheet.Range("B9:B11").Merge();
            worksheet.Cell(9, 2).Value = "Unit";
            a2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a2.Style.Font.Bold = true;
            a2.Style.Font.FontSize = 11;
            a2.Style.Alignment.WrapText = true;

            var a3 = worksheet.Range("C9:D9").Merge();
            worksheet.Cell(9, 3).Value = "Amount";
            a3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a3.Style.Font.Bold = true;
            a3.Style.Font.FontSize = 11;
            a3.Style.Alignment.WrapText = true;

            var a4 = worksheet.Range("C10:C11").Merge();
            worksheet.Cell(10, 3).Value = "Unit Cost";
            a4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a4.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a4.Style.Font.FontSize = 11;
            a4.Style.Alignment.WrapText = true;

            var a5 = worksheet.Range("D10:D11").Merge();
            worksheet.Cell(10, 4).Value = "Total Cost";
            a5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a5.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a5.Style.Font.FontSize = 11;
            a5.Style.Alignment.WrapText = true;

            var a6 = worksheet.Range("E9:F11").Merge();
            worksheet.Cell(9, 5).Value = "Description";
            a6.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a6.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a6.Style.Font.Bold = true;
            a6.Style.Font.FontSize = 11;
            a6.Style.Alignment.WrapText = true;


            var a7 = worksheet.Range("G9:G11").Merge();
            worksheet.Cell(9, 7).Value = "Inventory Item No.";
            a7.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a7.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a7.Style.Font.Bold = true;
            a7.Style.Font.FontSize = 11;
            a7.Style.Alignment.WrapText = true;

            var a8 = worksheet.Range("H9:H11").Merge();
            worksheet.Cell(9, 8).Value = "Estimated\nUseful Life";
            a8.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            a8.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            a8.Style.Font.Bold = true;
            a8.Style.Font.FontSize = 11;
            a8.Style.Alignment.WrapText = true;

            var AllFormRange = worksheet.Range("A9:H11");
            var AllFormtBorder = AllFormRange.Style.Border;
            AllFormtBorder.OutsideBorder = XLBorderStyleValues.Thin;

            var DetailsLeftRange = worksheet.Range("A9:A11");
            var DetailsLeftBorder = DetailsLeftRange.Style.Border;
            DetailsLeftBorder.RightBorder = XLBorderStyleValues.Thin;

            var DetailsRightRange = worksheet.Range("B9:B11");
            var DetailsRightBorder = DetailsRightRange.Style.Border;
            DetailsRightBorder.RightBorder = XLBorderStyleValues.Thin;

            var B1 = worksheet.Range("C9:D9");
            var B1A = B1.Style.Border;
            B1A.BottomBorder = XLBorderStyleValues.Thin;

            var B2 = worksheet.Range("C10:D11");
            var B2A = B2.Style.Border;
            B2A.RightBorder = XLBorderStyleValues.Thin;

            var B3 = worksheet.Range("E9:F11");
            var B3A = B3.Style.Border;
            B3A.RightBorder = XLBorderStyleValues.Thin;
            B3A.LeftBorder = XLBorderStyleValues.Thin;

            var B4 = worksheet.Range("G9:G11");
            var B4A = B4.Style.Border;
            B4A.RightBorder = XLBorderStyleValues.Thin;


            //HEADER END --------------------------------------------------------------------------


            //DETAILS START ------------------------------------------------------------------------


            int countMax = 43;

            if (dt.Rows.Count > 31)
            {
                countMax = dt.Rows.Count + 12;
            }

            //line pababa
            var DS = worksheet.Range("A12:H"+ countMax + "");
            var DSA = DS.Style.Border;
            DSA.OutsideBorder = XLBorderStyleValues.Thin;

            var DS1 = worksheet.Range("A12:A" + countMax + "");
            var DS1A = DS1.Style.Border;
            DS1A.RightBorder = XLBorderStyleValues.Thin;

            var DS2 = worksheet.Range("B12:B" + countMax + "");
            var DS2A = DS2.Style.Border;
            DS2A.RightBorder = XLBorderStyleValues.Thin;

            var DS3 = worksheet.Range("C12:C" + countMax + "");
            var DS3A = DS3.Style.Border;
            DS3A.RightBorder = XLBorderStyleValues.Thin;

            var DS4 = worksheet.Range("C12:C" + countMax + "");
            var DS4A = DS4.Style.Border;
            DS4A.RightBorder = XLBorderStyleValues.Thin;

            var DS5 = worksheet.Range("D12:D" + countMax + "");
            var DS5A = DS5.Style.Border;
            DS5A.RightBorder = XLBorderStyleValues.Thin;

            //var DS6 = worksheet.Range("E12:E43");
            //var DS6A = DS6.Style.Border;
            //DS6A.RightBorder = XLBorderStyleValues.Thin;

            var DS7 = worksheet.Range("F12:F" + countMax + "");
            var DS7A = DS7.Style.Border;
            DS7A.RightBorder = XLBorderStyleValues.Thin;

            var DS8 = worksheet.Range("G12:G" + countMax + "");
            var DS8A = DS8.Style.Border;
            DS8A.RightBorder = XLBorderStyleValues.Thin;

            int SBorder = 12;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    worksheet.Cell(SBorder, 1).Value = "1";
                    worksheet.Cell(SBorder, 2).Value = dt.Rows[i]["UOMcode"].ToString();
                    worksheet.Cell(SBorder, 3).Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    worksheet.Cell(SBorder, 4).Value = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    worksheet.Range("E"+SBorder+ ":F" + SBorder).Merge();
                    worksheet.Cell(SBorder, 5).Value = dt.Rows[i]["Description"].ToString();
                    worksheet.Cell(SBorder, 7).Value = dt.Rows[i]["ItemCode"].ToString();
                    worksheet.Cell(SBorder, 8).Value = dt.Rows[i]["UsefulLife"].ToString();

                    worksheet.Range("A" + SBorder + ":H" + SBorder + "").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A" + SBorder + ":H" + SBorder + "").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    



                    SBorder += 1;
                }
            }

            //DETAILS END -------------------------------------------------------------------------------

            //FOOTER START
          

            var FooterLeftRange = worksheet.Range("A"+ (countMax + 1) + ":H" + (countMax + 8) + "");
            var FooterLeftBorder = FooterLeftRange.Style.Border;
            FooterLeftBorder.OutsideBorder = XLBorderStyleValues.Thin;

            var FooterBottRange = worksheet.Range("E" + (countMax + 1) + ":E" + (countMax + 8) + "");
            var FooterBottBorder = FooterBottRange.Style.Border;
            FooterBottBorder.RightBorder = XLBorderStyleValues.Thin;


            worksheet.Cell(countMax + 1 , 1).Value = "Received from:";
            worksheet.Cell(countMax + 1, 6).Value = "Received by:";


            //var b1a = worksheet.Range("A46:E46").Merge();
            //worksheet.Cell(44, 1).Value = "Received from:";
            //b1a.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


            //var b1b = worksheet.Range("A46:E46").Merge();
            //worksheet.Cell(44, 6).Value = "Received by:";
            //b1b.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            var b1 = worksheet.Range("A" + (countMax + 3) + ":E" + (countMax + 3) + "").Merge();
            worksheet.Cell((countMax + 3), 1).Value = "__________________________________";
            b1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b2 = worksheet.Range("A" + (countMax + 4) + ":E" + (countMax + 4) + "").Merge();
            worksheet.Cell((countMax + 4), 1).Value = "Signature Over Printed Name";
            b2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b3 = worksheet.Range("A" + (countMax + 5) + ":E" + (countMax + 5) + "").Merge();
            worksheet.Cell((countMax + 5), 1).Value = "__________________________________";
            b3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b4 = worksheet.Range("A" + (countMax + 6) + ":E" + (countMax + 6) + "").Merge();
            worksheet.Cell((countMax + 6), 1).Value = "Position/Office";
            b4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b5 = worksheet.Range("A" + (countMax + 7) + ":E" + (countMax + 7) + "").Merge();
            worksheet.Cell((countMax + 7), 1).Value = "__________________________________";
            b5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var b6 = worksheet.Range("A" + (countMax + 8) + ":E" + (countMax + 8) + "").Merge();
            worksheet.Cell((countMax + 8), 1).Value = "Date";
            b6.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var c1 = worksheet.Range("F" + (countMax + 3) + ":H" + (countMax + 3) + "").Merge();
            worksheet.Cell((countMax + 3), 6).Value = "    ____________________________    ";
            c1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var c2 = worksheet.Range("F" + (countMax + 4) + ":H" + (countMax + 4) + "").Merge();
            worksheet.Cell((countMax + 4), 6).Value = "Signature Over Printed Name";
            c2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var c3 = worksheet.Range("F" + (countMax + 5) + ":H" + (countMax + 5) + "").Merge();
            worksheet.Cell((countMax + 5), 6).Value = "    ____________________________    ";
            c3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var c4 = worksheet.Range("F" + (countMax + 6) + ":H" + (countMax + 6) + "").Merge();
            worksheet.Cell((countMax + 6), 6).Value = "Position/Office";
            c4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var c5 = worksheet.Range("F" + (countMax + 7) + ":H" + (countMax + 7) + "").Merge();
            worksheet.Cell((countMax + 7), 6).Value = "    ____________________________    ";
            c5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var c6 = worksheet.Range("F" + (countMax + 8) + ":H" + (countMax + 8) + "").Merge();
            worksheet.Cell((countMax + 8), 6).Value = "Date";
            c6.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;




            //FOOTER END --------------------------------------------------------------------------

            //ADJUST ALL CELLS ACCORDING TO TEXT INSIDE
            foreach (var column in worksheet.ColumnsUsed())
            {
                column.AdjustToContents();
            }

            worksheet.Column("A").Width = 8;
            worksheet.Column("B").Width = 6;
            worksheet.Column("C").Width = 11;
            worksheet.Column("D").Width = 11;
            worksheet.Column("E").Width = 10;
            worksheet.Column("F").Width = 10;
            worksheet.Column("G").Width = 13;
            worksheet.Column("H").Width = 13;

            //SAVE PATH
            workbook.SaveAs("D:\\SampleIAR.xlsx");
        }

        return "";
    }
}