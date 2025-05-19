using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML;
using AdvanceBll;
using System.Data;

public partial class ReportPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key1 = Request.QueryString["Key1"]; //ReportType
        string key2 = Request.QueryString["Key2"]; //GoodsRecieveID
        string key3 = Request.QueryString["Key3"]; //HeaderInfo

        if (key1 == "IAR")
        {

            //get datable of IAR
            PropertyBll propertyBll = new PropertyBll();
            GoodsReceiveHistoryBll GoodsReceiveHistoryBll = new GoodsReceiveHistoryBll();

            DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, Convert.ToInt32(key2));
            DataTable dt1 = GoodsReceiveHistoryBll.GetGRHeader(Common.ConnString, Convert.ToInt32(key2));
            String ReportTitle = "IAR";

            //DataTable dt = new DataTable();
            DataTable dtFiltered = new DataTable();
            dtFiltered.Columns.Add("PRNumber", typeof(string));
            dtFiltered.Columns.Add("Description", typeof(string));
            dtFiltered.Columns.Add("Sample1", typeof(string));
            dtFiltered.Columns.Add("UnitOfMeasurement", typeof(string));
            dtFiltered.Columns.Add("Quantity", typeof(string));
            dtFiltered.Columns.Add("Sample12", typeof(string));
            DataRow dtFilteredRow = dtFiltered.NewRow();

            DataTable dtGRHeader = new DataTable();
            dtGRHeader.Columns.Add("SupplierName", typeof(string));
            dtGRHeader.Columns.Add("PONumber", typeof(string));
            dtGRHeader.Columns.Add("GoodsReceiveNumber", typeof(string));
            dtGRHeader.Columns.Add("CreatedDate", typeof(string));
            DataRow dtGRHeaderRow = dtGRHeader.NewRow();


            foreach (DataRow suRow in dt.Rows)
            {

                dtFilteredRow = dtFiltered.NewRow();
                dtFilteredRow["PRNumber"] = suRow[4];
                dtFilteredRow["Description"] = suRow[14];
                dtFilteredRow["UnitOfMeasurement"] = suRow[15];
                dtFilteredRow["Quantity"] = suRow[3];

                dtFiltered.Rows.Add(dtFilteredRow);

                //add to FIlter
            }

            foreach (DataRow suRow1 in dt1.Rows)
            {

                dtGRHeaderRow = dtGRHeader.NewRow();
                dtGRHeaderRow["SupplierName"] = suRow1[20];
                dtGRHeaderRow["PONumber"] = suRow1[19];
                dtGRHeaderRow["GoodsReceiveNumber"] = suRow1[1];
                dtGRHeaderRow["CreatedDate"] = suRow1[8];

                dtGRHeader.Rows.Add(dtGRHeaderRow);

                //add to FIlter
            }

            //call ExportToExcelIAR
            ExportExcelIAR ExportToExcelIAR = new ExportExcelIAR();
            ExportToExcelIAR.ExportToExcel(dtFiltered, dtGRHeader, ReportTitle);

        }

    }
}