using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using AdvanceBll;
using System.Data;
//using System.Convert;

/// <summary>
/// Summary description for ExportExcelIAR
/// </summary>
public class ExportExcelIAR
{
    static string CompanyName = "";
    static string CompanyContactNumber = "";
    static string CompanyAddress = "";

    static string Supplier = "";

    public ExportExcelIAR()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string ExportToExcel(DataTable dt, DataTable dt1, String ReportTitle)
    {
        //Get company info
        MiscBll miscBll = new MiscBll();
        string x = miscBll.GetCompanyInformation(Common.ConnString);
        CompanyName = x.Split('|')[0];
        CompanyContactNumber = x.Split('|')[3];
        CompanyAddress = x.Split('|')[5];


        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("IAR");

            //TITLE START
            var IARText = worksheet.Range("A3:F3").Merge();
            worksheet.Cell(3, 1).Value = "INSPECTION AND ACCEPTANCE REPORT";
            IARText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            IARText.Style.Font.Bold = true;
            IARText.Style.Font.FontSize = 16;
            //TITLE END --------------------------------------------------------------------------

            //HEADER START
            var EntiText = worksheet.Cell("A6");
            var ClusText = worksheet.Cell("D6");
            worksheet.Cell(6, 1).Value = "Entity Name: ";
            worksheet.Cell(6, 2).Value = CompanyName;
            worksheet.Cell(6, 4).Value = "Fund Cluster: ";
            EntiText.Style.Font.Bold = true;
            ClusText.Style.Font.Bold = true;

            worksheet.Cell(8, 1).Value = "Supplier  : ";
            worksheet.Cell(8, 2).Value = dt1.Rows[0][0];
            worksheet.Cell(8, 4).Value = "IAR No.: ";
            worksheet.Cell(8, 5).Value = dt1.Rows[0][2];

            worksheet.Cell(9, 1).Value = "PO No./Date: ";
            worksheet.Cell(9, 2).Value = dt1.Rows[0][1];
            worksheet.Cell(9, 4).Value = "Date: ";
            worksheet.Cell(9, 5).Value = dt1.Rows[0][3];

            worksheet.Cell(10, 1).Value = "Requisitioning Group/User Dept.: ";
            worksheet.Cell(10, 4).Value = "Invoice No.: ";

            worksheet.Cell(11, 1).Value = "Responsibility Group/User Dept.: ";
            worksheet.Cell(11, 4).Value = "Date:";

            var AllFormRange = worksheet.Range("A8:F37");
            var AllFormtBorder = AllFormRange.Style.Border;
            AllFormtBorder.OutsideBorder = XLBorderStyleValues.Thin;

            var DetailsLeftRange = worksheet.Range("A8:C11");
            var DetailsLeftBorder = DetailsLeftRange.Style.Border;
            DetailsLeftBorder.OutsideBorder = XLBorderStyleValues.Thin;

            var DetailsRightRange = worksheet.Range("D8:F11");
            var DetailsRightBorder = DetailsRightRange.Style.Border;
            DetailsRightBorder.OutsideBorder = XLBorderStyleValues.Thin;
            //HEADER END --------------------------------------------------------------------------


            //DETAILS START ------------------------------------------------------------------------
            var StockPropText = worksheet.Cell("A13");
            StockPropText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            StockPropText.Style.Font.Bold = true;
            worksheet.Cell(13, 1).Value = "Supply/Property No.";

            var DescText = worksheet.Range("B13:C13").Merge();
            DescText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            DescText.Style.Font.Bold = true;
            worksheet.Cell(13, 2).Value = "Description";

            var UnitText = worksheet.Cell("D13");
            UnitText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            UnitText.Style.Font.Bold = true;
            worksheet.Cell(13, 4).Value = "Unit";

            var QuantText = worksheet.Range("E13:F13").Merge();
            QuantText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            QuantText.Style.Font.Bold = true;
            worksheet.Cell(13, 5).Value = "Quantity";

            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    worksheet.Cell(((14)+i), j + 1).Value = dt.Rows[i][j];
                    var rangemerge = worksheet.Range(14+i, 2, 14 + i, 3);
                    rangemerge.Merge();
                    var rangemerge2 = worksheet.Range(14 + i, 5, 14 + i, 6);
                    rangemerge2.Merge();
                }
            }

            var DetailsBodyStockRange = worksheet.Range("A14:A26");
            var DetailsBodyStockBorder = DetailsBodyStockRange.Style.Border;
            DetailsBodyStockBorder.OutsideBorder = XLBorderStyleValues.Thin;
            DetailsBodyStockBorder.InsideBorder = XLBorderStyleValues.Thin;

            var DetailsBodyDescRange = worksheet.Range("B14:C26");
            var DetailsBodyDescBorder = DetailsBodyDescRange.Style.Border;
            DetailsBodyDescBorder.TopBorder = XLBorderStyleValues.Thin;
            DetailsBodyDescBorder.BottomBorder = XLBorderStyleValues.Thin;

            var DetailsBodUnitRange = worksheet.Range("D14:D26");
            var DetailsBodyUnitBorder = DetailsBodUnitRange.Style.Border;
            DetailsBodyUnitBorder.OutsideBorder = XLBorderStyleValues.Thin;
            DetailsBodyUnitBorder.InsideBorder = XLBorderStyleValues.Thin;

            var DetailsBodyQuanRange = worksheet.Range("E14:F26");
            var DetailsBodyQuanBorder = DetailsBodyQuanRange.Style.Border;
            DetailsBodyQuanBorder.OutsideBorder = XLBorderStyleValues.Thin;
            DetailsBodyQuanBorder.BottomBorder = XLBorderStyleValues.Thin;

            //DETAILS END -------------------------------------------------------------------------------

            //FOOTER START

            var FooterLeftRange = worksheet.Range("A27:C37");
            var FooterLeftBorder = FooterLeftRange.Style.Border;
            FooterLeftBorder.OutsideBorder = XLBorderStyleValues.Thin;

            var FooterBottRange = worksheet.Range("A27:F27");
            var FooterBottBorder = FooterBottRange.Style.Border;
            FooterBottBorder.OutsideBorder = XLBorderStyleValues.Thin;


            var InspecText = worksheet.Range("A27:C27").Merge();
            worksheet.Cell(27, 1).Value = "INSPECTION";
            InspecText.Style.Font.Bold = true;
            InspecText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            InspecText.Style.Font.FontSize = 14;

            var AccepText = worksheet.Range("D27:F27").Merge();
            worksheet.Cell(27, 4).Value = "ACCEPTANCE";
            AccepText.Style.Font.Bold = true;
            AccepText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            AccepText.Style.Font.FontSize = 14;

            var DateInspecText = worksheet.Range("A28:C29").Merge();
            worksheet.Cell(28, 1).Value = "Date Inspected: ";
            DateInspecText.Style.Font.Bold = true;


            var DateAcceptText = worksheet.Range("D28:F29").Merge();
            worksheet.Cell(28, 4).Value = "Date Received: ";
            DateAcceptText.Style.Font.Bold = true;

            var InspectCheckText = worksheet.Range("A30:C33").Merge();
            worksheet.Cell(30, 1).Value = "▢ Inspected, verified and found in order as to quantity and specifications";
            InspectCheckText.Style.Alignment.WrapText = true;

            var CompCheckText = worksheet.Range("D30:F31").Merge();
            var PartCheckText = worksheet.Range("D32:F33").Merge();

            worksheet.Cell(30, 4).Value = "▢ Complete";
            worksheet.Cell(32, 4).Value = "▢ Partial (pls. specify quantity)";

            var InspectOscarText = worksheet.Range("A36:C36").Merge();
            worksheet.Cell(36, 1).Value = "Inspection Officer/Inspection Committee";
            InspectOscarText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            var SupCustText = worksheet.Range("D36:F36").Merge();
            worksheet.Cell(36, 4).Value = "Supply and/or Property Custodian";
            SupCustText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //FOOTER END --------------------------------------------------------------------------

            //ADJUST ALL CELLS ACCORDING TO TEXT INSIDE
            foreach (var column in worksheet.ColumnsUsed())
            {
                column.AdjustToContents();
            }

            //SAVE PATH
            workbook.SaveAs("C:\\SampleIAR.xlsx");

        }

        return "";
    }
}