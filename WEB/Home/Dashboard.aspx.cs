using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;
using IOTPIEncrypto;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using ClosedXML.Excel;

public partial class Home_Dashboard : System.Web.UI.Page
{
 
    string retSite = "";
    string retLoc = "";
    string retArea = "";
    string retEmail = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        //retSite = shSystemConfigurationsSite.Value;
        lblMasterListSite.Text = retSite;
        lblStockListSite.Text = retSite;

        SystemConfigurations shSystemConfigurationsLocation = (SystemConfigurations)Session["shSystemConfigurationsLocation"];
        //retLoc = shSystemConfigurationsLocation.Value;
        lblMasterListLoc.Text = retLoc;

        SystemConfigurations shSystemConfigurationsArea = (SystemConfigurations)Session["shSystemConfigurationsArea"];
        //retArea = shSystemConfigurationsArea.Value;
        lblMasterListArea.Text = retArea;

        SystemConfigurations shSystemConfigurationsEmail = (SystemConfigurations)Session["shSystemConfigurationsEmail"];
        //retEmail = shSystemConfigurationsEmail.Value;
        //lblMasterListArea.Text = retArea;

        if (!Common.IsUserAuthenticated)
            Response.Redirect(@"..\Login.aspx");

        if (Common.FirstLogin == "True")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:FirstTimer();", true);
        }

        carouselExampleIndicators.InnerHtml = GetCarouselImages();
        //announcement.InnerHtml = GetHomePageImage();
        if (Common.IsCustodian == "True")
        {

            divgrdetails.InnerHtml = "<h3>" + getCountGR() + "</h3><p>Goods Receive</p>";
            divissuancedetails.InnerHtml = "<h3>" + getCountIssuance() + "</h3><p>Property Issuance</p>";
            divtransferdetails.InnerHtml = "<h3>" + getCountTransfer() + "</h3><p>Property Transfer</p>";
            divreturndetails.InnerHtml = "<h3>" + getCountReturn() + "</h3><p>Property Return</p>";
            divdisposedetails.InnerHtml = "<h3>" + getCountDisposal() + "</h3><p>Property Disposal</p>";

            //AddedControl by nero
            divOpenInventoryCount.InnerHtml = "<h3>" + getOpenInventoryCount() + "</h3><p>Open Inventory Count</p>";
        }
        if (Common.Administrative == "True")
        {

            divgrdetails.InnerHtml = "<h3>" + getCountGR() + "</h3><p>Goods Receive</p>";
            divissuancedetails.InnerHtml = "<h3>" + getCountIssuance() + "</h3><p>Property Issuance</p>";
            divtransferdetails.InnerHtml = "<h3>" + getCountTransfer() + "</h3><p>Property Transfer</p>";
            divreturndetails.InnerHtml = "<h3>" + getCountReturn() + "</h3><p>Property Return</p>";
            divdisposedetails.InnerHtml = "<h3>" + getCountDisposal() + "</h3><p>Property Disposal</p>";

            divGRforApproval.InnerHtml = "<h3>" + GetGRPendingApproval() + "</h3><p>Goods Receive</p>";
            divPropertyIssuanceforApproval.InnerHtml = "<h3>" + GetPropertyIssuancePendingApproval() + "</h3><p>Property Issuance</p>";
            divPropertyTransferforApproval.InnerHtml = "<h3>" + GetPropertyTransferPendingApproval() + "</h3><p>Property Transfer</p>";
            divPropertyDisposalforApproval.InnerHtml = "<h3>" + GetPropertyDisposalPendingApproval() + "</h3><p>Property Disposal</p>";
            divPropertyGatePassforApproval.InnerHtml = "<h3>" + GetPropertyGatePassPendingApproval() + "</h3><p>Property Gate Pass</p>";
            divPropertyDayPassforApproval.InnerHtml = "<h3>" + GetPropertyDayPassPendingApproval() + "</h3><p>Property Day Pass</p>";

            divUploadedFileforApproval.InnerHtml = "<h3>" + GetFileUploadPendingApproval() + "</h3><p>File Upload</p>";
            divDatabaseRestoreforApproval.InnerHtml = "<h3>" + GetDatebaseRestorePendingApproval() + "</h3><p>Database Restore</p>";
            divStockMergeforApproval.InnerHtml = "<h3>" + GetStockMergePendingApproval() + "</h3><p>Stock Merge</p>";
            //divReturnExtensionforApproval.InnerHtml = "<h3>" + GetPropertyReturnDateExtensionPendingApproval() + "</h3><p>Return Date Extension</p>";


            divStockIssuanceforApproval.InnerHtml = "<h3>" + GetStockIssuancePendingApproval() + "</h3><p>Supply Issuance</p>";
            divStockRequestforApproval.InnerHtml = "<h3>" + GetStockRequestPendingApproval() + "</h3><p>Supply Request</p>";
            divStockDisposalforApproval.InnerHtml = "<h3>" + GetStockDisposalPendingApproval() + "</h3><p>Supply Disposal</p>";
            divPropertyDisposalRequestforApproval.InnerHtml = "<h3>" + GetPropertyDisposalRequestPendingApproval() + "</h3><p>Property Disposal Request</p>";

            //AddedControl by nero
            divOpenInventoryCount.InnerHtml = "<h3>" + getOpenInventoryCount() + "</h3><p>Open Inventory Count</p>";
        }
        if (Common.IsApprover == "True" || Common.IsCertifier == "True" || Common.IsInspector == "True")
        {


            divGRforApproval.InnerHtml = "<h3>" + GetGRPendingApproval() + "</h3><p>Goods Receive</p>";
            divPropertyIssuanceforApproval.InnerHtml = "<h3>" + GetPropertyIssuancePendingApproval() + "</h3><p>Property Issuance</p>";
            divPropertyTransferforApproval.InnerHtml = "<h3>" + GetPropertyTransferPendingApproval() + "</h3><p>Property Transfer</p>";
            divPropertyDisposalforApproval.InnerHtml = "<h3>" + GetPropertyDisposalPendingApproval() + "</h3><p>Property Disposal</p>";
            divPropertyGatePassforApproval.InnerHtml = "<h3>" + GetPropertyGatePassPendingApproval() + "</h3><p>Property Gate Pass</p>";
            divPropertyDayPassforApproval.InnerHtml = "<h3>" + GetPropertyDayPassPendingApproval() + "</h3><p>Property Day Pass</p>";

            divUploadedFileforApproval.InnerHtml = "<h3>" + GetFileUploadPendingApproval() + "</h3><p>File Upload</p>";
            divDatabaseRestoreforApproval.InnerHtml = "<h3>" + GetDatebaseRestorePendingApproval() + "</h3><p>Database Restore</p>";
            divStockMergeforApproval.InnerHtml = "<h3>" + GetStockMergePendingApproval() + "</h3><p>Supply Merge</p>";



            divStockIssuanceforApproval.InnerHtml = "<h3>" + GetStockIssuancePendingApproval() + "</h3><p>Supply Issuance</p>";
            divStockRequestforApproval.InnerHtml = "<h3>" + GetStockRequestPendingApproval() + "</h3><p>Supply Request</p>";
            divStockDisposalforApproval.InnerHtml = "<h3>" + GetStockDisposalPendingApproval() + "</h3><p>Supply Disposal</p>";
            divPropertyDisposalRequestforApproval.InnerHtml = "<h3>" + GetPropertyDisposalRequestPendingApproval() + "</h3><p>Property Disposal Request</p>";

   
        }

        divAcknowledgement.InnerHtml = "<h3>" + GetPropertyAcknowledgement() + "</h3><p>Acknowledgement</p>";
    }

    #region Bulletin news


    public string GetCarouselImages()
    {
        string[] filesGR = new string[0];
        try
        {
            filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("/Image/ioAsset/Carousel"));
        }
        catch
        {

        }
        string html = "";

        //list
        html += "<ol class='carousel-indicators'>";
        for (int i = 0; i < filesGR.Length; i++)
        {
            html += "<li data-target='#BodyHolder_carouselExampleIndicators' data-slide-to='" + i + "' class='active'></li>";
        }
        html += "</ol>";

        //carousel div images
        html += "<div class='carousel-inner'>";

        int ctr = 0;
        foreach (string name in filesGR)
        {
            int j = name.Split('\\').Length;
            string[] file = name.Split('\\');
            if (ctr == 0)
            {
                html += "<div class='item active'><center><img  src='/Image/ioASSET/Carousel/" + file[j - 1] + "' height='470px'></center></div>";
                ctr += 1;
            }
            else
            {
                html += "<div class='item'><center><img src='/Image/ioASSET/Carousel/" + file[j - 1] + "' height='470px'></center></div>";
            }
        }
        html += "</div>";

        //button previous and next
        html += "<a class='left carousel-control' role='button' href='#BodyHolder_carouselExampleIndicators' data-slide='prev'>"
                   + "<span class='fa fa-angle-left' aria-hidden='true'>"
                   + "</span>"
               + "</a>"
               + "<a class='right carousel-control' role='button' href='#BodyHolder_carouselExampleIndicators' data-slide='next'>"
                   + "<span class='fa fa-angle-right' aria-hidden='true'>"
                   + "</span>"
               + "</a>";
        return html;
    }
    #endregion

    [WebMethod]
    public static string GetApprovalTiles()
    {
        string Approval =
        Common.CreationApproval + "|" +
        Common.MergeStockApproval + "|" +
        Common.IssuanceApproval + "|" +
        Common.TransferApproval + "|" +
        Common.StockIssuanceApproval + "|" +
        Common.StockDisposalApproval + "|" +
        Common.DisposalApproval + "|" +
        Common.GatePassApproval + "|" +
        Common.DataUploadApproval + "|" +
        Common.DatabaseRestoreApproval + "|" +
        Common.RequestApproval;
        return Approval;
    }

    #region Pending Verification
    public static string getCountGR()
    {
        PropertyBll x = new PropertyBll();
        return x.GetGRPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountIssuance()
    {
        PropertyBll x = new PropertyBll();
        return x.GetIssuancePropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountTransfer()
    {
        PropertyBll x = new PropertyBll();
        return x.GetTransferPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountRequest()
    {
        PropertyBll x = new PropertyBll();
        return x.GetRequestPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountReturn()
    {
        PropertyBll x = new PropertyBll();
        return x.GetReturnPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string getCountDisposal()
    {
        PropertyBll x = new PropertyBll();
        return x.GetDisposalPropertyVerification(Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }
    #endregion

    #region Pending Approval
    public static string GetGRPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetGRPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyIssuancePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyIssuancePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyRequestPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyRequestPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyTransferPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyTransferPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyDisposalPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyDisposalPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyGatePassPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyGatePassPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetPropertyDayPassPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyDayPassPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetFileUploadPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetFileUploadPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetDatebaseRestorePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetDatebaseRestorePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockMergePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockMergePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }
    public static string GetPropertyReturnDateExtensionPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyReturnDateExtensionPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockIssuancePendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockIssuancePendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockRequestPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockRequestPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }

    public static string GetStockDisposalPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetStockDisposalPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }
    public static string GetPropertyDisposalRequestPendingApproval()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyDisposalRequestPendingApproval(Common.Administrative, Common.UserID, Convert.ToInt32(Common.SiteID), Common.ConnString).ToString();
    }
    #endregion

    #region Pending Acknowledgement
    public static string GetPropertyAcknowledgement()
    {
        PropertyBll x = new PropertyBll();
        return x.GetPropertyAcknowledgementCount(Convert.ToInt32(Common.UserID), Common.ConnString).ToString();
    }

    public static string getOpenInventoryCount()
    {
        PropertyBll x = new PropertyBll();
        return x.getOpenInventoryCount(Common.CustodianID, Common.ConnString,Common.Administrative).ToString();
    }
    #endregion

    #region My Items
    [WebMethod]
    public static string GetMyItems()
    {

        PropertyBll y = new PropertyBll();
        //MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetITemList(Common.ConnString, Common.UserID);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.AssetType = dt.Rows[i]["EmployeeName"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    x.IssuanceNumber = dt.Rows[i]["PARICSnumber"].ToString();
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
    #endregion

    [WebMethod]
    public static string GetUserInfo()
    {
        string userinfo = Common.Administrative + "|" + Common.IsApprover + "|" + Common.IsCertifier + "|" + Common.IsInspector + "|" + Common.IsCustodian;
        return userinfo;
    }

    [WebMethod]
    public static string DashboardHideShowPannel()
    {
        var res = "";

        res = (Common.IsCustodian + "|");
        res += Common.VerificationMode + "|";
        res += (Common.IssuanceAcknowledgement + "|");
        res += Common.Administrative + "|";
        res += Common.IsApprover + "|";
        res += Common.IssuanceAcknowledgement;
        return res;
    }

    #region Property masterlist
    [WebMethod]
    public static string GetListProperty(string search)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyMasterlist(Common.ConnString, search, "");
            //html = BasePage.ConvertDataTabletoJSON(dt);
            //return html;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.LongDescription = dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();

                    x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();

                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Area = dt.Rows[i]["Area"].ToString();

                    x.DateOfAcquisition = dt.Rows[i]["DateOfAcquisition"].ToString().Split(' ')[0];
                    x.UsefulLifeString = dt.Rows[i]["UsefulLife"].ToString().Split(' ')[0];
                    x.RemainingUsefulLifeString = dt.Rows[i]["RemainingUsefulLife"].ToString().Split(' ')[0];
                    x.DateOfMaintenance = dt.Rows[i]["MaintenanceDate"].ToString().Split(' ')[0];
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());
                    if (dt.Rows[i]["DepreciationMethod"].ToString() == "NONE")
                    {
                        x.DepreciatedCostString = "";
                    }
                    else
                    {
                        //x.DepreciatedCostString = "";
                        if (depreciationSetting == "Straight Line Depreciation Method")
                        {
                            if (Converter.ConvertStringToDouble(dt.Rows[i]["StraightLineDepreciation"].ToString()) < 0 || dt.Rows[i]["StraightLineDepreciation"].ToString() == null)
                            {
                                x.DepreciatedCostString = "0.00";
                            }
                            else
                            {
                                x.DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["StraightLineDepreciation"].ToString());
                            }
                        }
                        else if (depreciationSetting == "Diminishing Balance Method")
                        {
                            if (Converter.ConvertStringToDouble(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString()) < 0 || dt.Rows[i]["DiminishingBalanceDepreciation"].ToString() == null)
                            {
                                x.DepreciatedCostString = "0.00";
                            }
                            else
                            {
                                x.DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["DiminishingBalanceDepreciation"].ToString());
                            }
                        }
                        else
                        {
                            if (Converter.ConvertStringToDouble(dt.Rows[i]["UnitOfProductDepreciation"].ToString()) < 0 || dt.Rows[i]["UnitOfProductDepreciation"].ToString() == null)
                            {
                                x.DepreciatedCostString = "0.00";
                            }
                            else
                            {
                                x.DepreciatedCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["UnitOfProductDepreciation"].ToString());
                            }
                        }
                    }

                    if (dt.Rows[i]["TransactionType"].ToString() != "")
                    {
                        x.Status = dt.Rows[i]["Status"].ToString() + " (" + dt.Rows[i]["TransactionType"].ToString() + ")";
                    }
                    else
                    {
                        x.Status = dt.Rows[i]["Status"].ToString();
                    }
                    x.DisposedDate = dt.Rows[i]["DisposedDate"].ToString();
                    x.IssuedDate = dt.Rows[i]["IssuedDate"].ToString();
                    x.LastmaintenanceDate = dt.Rows[i]["LastMaintenanceDate"].ToString();
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

    #endregion

    #region Stock Masterlist

    [WebMethod]
    public static string GetStockMasterList(string search)
    {
        StockIssuanceBll y = new StockIssuanceBll();


        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetStockMasterList(Common.ConnString, Convert.ToInt32(Common.SiteID), search, "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    x.CriticalLevelString = dt.Rows[i]["CriticalLevel"].ToString();

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
    #endregion

    #region widgets
    [WebMethod]
    public static string StocksUnderMinLevel()
    {
        WidgetsBll y = new WidgetsBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StocksUnderMinLevel(Common.ConnString,"table");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Quantity = Int32.Parse(dt.Rows[i]["Quantity"].ToString());
                    x.CriticalLevel = Int32.Parse(dt.Rows[i]["CriticalLevel"].ToString());
                    x.Description = dt.Rows[i]["Description"].ToString();

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


    [WebMethod]
    public static string StocksUnderMinLevelGraph(string stat)
    {
        WidgetsBll y = new WidgetsBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.StocksUnderMinLevelGraph(Common.ConnString, "table", stat);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Quantity = Int32.Parse(dt.Rows[i]["Quantity"].ToString());
                    x.CriticalLevel = Int32.Parse(dt.Rows[i]["CriticalLevel"].ToString());
                    x.Description = dt.Rows[i]["Description"].ToString();

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

    [WebMethod]
    public static string PropertyImportantSched(string dateStart, string dateEnd, string label,string isdet)
    {
        WidgetsBll y = new WidgetsBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.PropertyImportantSched(Common.ConnString, "table", dateStart, dateEnd, label, isdet);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.DateOfTransaction = dt.Rows[i]["Date"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.TransactionType = dt.Rows[i]["Activity"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

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




    [WebMethod]
    public static string LoadGraphDonnut()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };
        dt = y.PropertyStatusChartDonnut(Common.ConnString);
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }

    [WebMethod]
    public static string LoadGraphDonnutSUML()
    {

        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };
        dt = y.StocksUnderMinLevel(Common.ConnString,"pie");
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }

    [WebMethod]
    public static string LoadGraphDonnutPISL(string dateStart, string dateEnd, string label, string isdet)
    {

        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };
        dt = y.PropertyImportantSched(Common.ConnString, "pie", dateStart, dateEnd, label, isdet);
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }

    //[WebMethod]
    //public static string LoadGraphBarPISL()
    //{
    //    WidgetsBll y = new WidgetsBll();
    //    DataTable dt = new DataTable();

    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    Dictionary<string, object> row;

    //    //var DashboardList = new List<DashboardEntity>() { };

    //    dt = y.PropertyImportantSched(Common.ConnString);
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        row = new Dictionary<string, object>();
    //        foreach (DataColumn col in dt.Columns)
    //        {
    //            row.Add(col.ColumnName, dr[col]);
    //        }
    //        rows.Add(row);
    //    }

    //    return serializer.Serialize(rows);
    //}



    [WebMethod]
    public static string LoadGraphBarSUML()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };

        dt = y.StocksUnderMinLevel(Common.ConnString,"bar");
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }


    [WebMethod]
    public static string LoadGraphBarPISL(string dateStart, string dateEnd, string label, string isdet)
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };

        dt = y.PropertyImportantSched(Common.ConnString, "bar",dateStart, dateEnd, label, isdet);
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }



    //Status datatable on modal
    [WebMethod]
    public static string GetStatusList(string search)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyByStatus(Common.ConnString, search);
            //html = BasePage.ConvertDataTabletoJSON(dt);
            //return html;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.LongDescription = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();

                    x.IssuanceNumber = dt.Rows[i]["IssuanceNo"].ToString();
                    x.IssuedTo = dt.Rows[i]["EmployeeName"].ToString();

                    x.Site = dt.Rows[i]["WarehouseName"].ToString();
                    x.Location = dt.Rows[i]["Location"].ToString();
                    x.Area = dt.Rows[i]["Area"].ToString();

                    x.DateOfAcquisition = dt.Rows[i]["DateOfAcquisition"].ToString().Split(' ')[0];
                    x.UsefulLifeString = dt.Rows[i]["UsefulLife"].ToString().Split(' ')[0];
                    x.RemainingUsefulLifeString = dt.Rows[i]["RemainingUsefulLife"].ToString().Split(' ')[0];
                    x.DateOfMaintenance = dt.Rows[i]["MaintenanceDate"].ToString().Split(' ')[0];
                    x.WarrantyDate = dt.Rows[i]["WarrantyStartDate"].ToString().Split(' ')[0] + " - " + dt.Rows[i]["WarrantyEndDate"].ToString().Split(' ')[0];
                    x.InsuranceDate = dt.Rows[i]["InsuranceStartDate"].ToString().Split(' ')[0] + " - " + dt.Rows[i]["InsuranceEndDate"].ToString().Split(' ')[0];
                    x.ExpiryDate = dt.Rows[i]["ExpiryDate"].ToString().Split(' ')[0];
                    x.AcquiredCostString = BasePage.ConvertToMoneyFormat(dt.Rows[i]["AcquiredCost"].ToString());

                    if (dt.Rows[i]["TransactionType"].ToString() != "")
                    {
                        x.Status = dt.Rows[i]["Status"].ToString() + " (" + dt.Rows[i]["TransactionType"].ToString() + ")";
                    }
                    else
                    {
                        x.Status = dt.Rows[i]["Status"].ToString();
                    }
                    x.DisposedDate = dt.Rows[i]["DisposedDate"].ToString().Split(' ')[0];
                    x.IssuedDate = dt.Rows[i]["IssuedDate"].ToString().Split(' ')[0];
                    x.LastmaintenanceDate = dt.Rows[i]["LastMaintenanceDate"].ToString().Split(' ')[0];
                    x.InventoryName = dt.Rows[i]["InventoryName"].ToString();
                    x.InventoryDate = dt.Rows[i]["InventoryCreatedDate"].ToString().Split(' ')[0];
                    if (dt.Rows[i]["InventoryStatus"].ToString() == "0")
                    {
                        x.InventoryStatus = "Not Found";
                    }
                    else if (dt.Rows[i]["InventoryStatus"].ToString() == "1")
                    {
                        x.InventoryStatus = "Found";
                    }
                    else
                    {
                        x.InventoryStatus = "Misplaced";
                    }
                    x.InventorySite = dt.Rows[i]["InventorySite"].ToString();
                    x.InventoryLocation = dt.Rows[i]["InventoryLocation"].ToString();
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


    [WebMethod]
    public static string GetPISLList(string search)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetPropertyByStatus(Common.ConnString, search);
            //html = BasePage.ConvertDataTabletoJSON(dt);
            //return html;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    
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

    //STATUS BARGRAPH
    [WebMethod]
    public static string LoadGraphBarStatus()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };

        dt = y.PropertyStatusChartDonnut(Common.ConnString);
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }

    [WebMethod]
    public static string GetTop10Property(string itemcode)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetTop10Property(Common.ConnString, itemcode);
            //html = BasePage.ConvertDataTabletoJSON(dt);
            //return html;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.QuantityString = dt.Rows[i]["value"].ToString();

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


    [WebMethod]
    public static string LoadGraphBarProperty()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };

        dt = y.Top10MostUsedProperty(Common.ConnString);
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }


    [WebMethod]
    public static string LoadGraphBarPropertyTbl()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();



        try
        {
            dt = y.Top10MostUsedProperty(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["value"].ToString();
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


    [WebMethod]
    public static string LoadGraphBarPropertyTbl1(string itemcode)
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();


        try
        {
            dt = y.Top10MostUsedProperty1(Common.ConnString, itemcode);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();

                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["value"].ToString();
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
    //Stock bar graph
    [WebMethod]
    public static string LoadGraphBarStock()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        //var DashboardList = new List<DashboardEntity>() { };

        dt = y.Top10MostUsedStock(Common.ConnString);
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }

        return serializer.Serialize(rows);
    }
    [WebMethod]
    public static string LoadGraphBarStocktbl()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();
        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();



        try
        {
            dt = y.Top10MostUsedStock(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["value"].ToString();
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
    [WebMethod]
    public static string LoadGraphBarStocktbl1(string itemcode)
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();
        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();



        try
        {
            dt = y.Top10MostUsedStock1(Common.ConnString, itemcode);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["value"].ToString();
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


    [WebMethod]
    public static string GetTop10Stock(string itemcode)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = y.GetTop10Stock(Common.ConnString, itemcode);
            //html = BasePage.ConvertDataTabletoJSON(dt);
            //return html;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["value"].ToString();
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

    [WebMethod]
    public static string LoadStatustbl()
    {
        WidgetsBll y = new WidgetsBll();
        DataTable dt = new DataTable();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();



        try
        {
            dt = y.PropertyStatusChartDonnut(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.Status = dt.Rows[i]["Label"].ToString();
                    x.Values = dt.Rows[i]["Value"].ToString();
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
    [WebMethod]
    public static string btnTest_Click()
    {
        WidgetsBll y = new WidgetsBll();

        DataTable dt = y.StocksUnderMinLevelGraph(Common.ConnString, "table", "SCOTHTAPE-12301");

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
            worksheet.Cell(6, 4).Value = "Fund Cluster: ";
            EntiText.Style.Font.Bold = true;
            ClusText.Style.Font.Bold = true;

            worksheet.Cell(8, 1).Value = "Supplier: ";
            worksheet.Cell(8, 4).Value = "IAR No.: ";

            worksheet.Cell(9, 1).Value = "PO No./Date: ";
            worksheet.Cell(9, 4).Value = "Date: ";

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

            var DescText = worksheet.Range("B13");
            //DescText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            DescText.Style.Font.Bold = true;
            worksheet.Cell(13, 2).Value = "Description";

            var UnitText = worksheet.Cell("C13");
            //UnitText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            UnitText.Style.Font.Bold = true;
            worksheet.Cell(13, 3).Value = "Unit";

            var QuantText = worksheet.Range("D13");
            //QuantText.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            QuantText.Style.Font.Bold = true;
            worksheet.Cell(13, 4).Value = "Quantity";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    worksheet.Cell(12 + 2, j + 1).Value = dt.Rows[i][j];
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
            //workbook.SaveAs("C:\\SampleIAR.xlsx");
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.SaveAs(ms);

                byte[] output = ms.ToArray();
                return Convert.ToBase64String(output, 0, output.Length);
            }
        }
    }
}
#endregion widget


