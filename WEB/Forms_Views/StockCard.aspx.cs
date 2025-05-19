using AdvanceBll;
using AdvanceDal;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class Views_StockCard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFlterTable();
    }

    private static int ConvertStringToInt(string sVal)
    {
        int myout = 0;
        int.TryParse(sVal, out myout);
        return myout;
    }

    [WebMethod]
    public static string GetStockCardV2(int StockID)
    {
        try
        {
            StockCardBll z = new StockCardBll();

            DataTable dt = z.GetStockCardV2(Common.ConnString, StockID);
            
            return Control.MyConverter.ConvertDataTabletoJSON(dt);
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    [WebMethod]
    public static string GetCompanyInfo()
    {
        try
        {
            return Common.CompanyInfo;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    [WebMethod]
    public static string GetStockCard(int StockID,
        int SelectedMonth,
        int SelectedYear)
    {
        try
        {
            //int SiteID = ConvertStringToInt(Common.SiteID);
            //string[] CustodianID = Common.CustodianID.Split(';');

            string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();

            string qry = "SELECT * FROM(SELECT tapt.StockID,tapt.CreatedDate, (tapt.GoodsReceiveNumber)as TransNum, (tapt.Quantity)as ReceiptQty, (0)as IssueQty, ('')as IssueOffice, ('Add')as AddOrMinus, ('GR')as TransType FROM t_tbl_AddedPropertyToList tapt "
            + "INNER JOIN tbl_Stock ts ON tapt.StockID = ts.StockID "
            + "WHERE tapt.StockID IS NOT NULL AND tapt.IsUploaded = 1 "
            + "UNION ALL "
            + "SELECT tsid.StockID, tsid.CreatedDate, (tsid.IssuanceNo)as TransNum, (0) as ReceiptQty, (tsid.Quantity) as IssueQty, (SELECT WarehouseName FROM tbl_Warehouse WHERE WarehouseID = (SELECT SiteID FROM tbl_User WHERE UserID = tsid.EmployeeID))as IssueOffice, ('Minus') as AddOrMinus , ('Issuance') as TransType FROM tbl_StockIssuanceDetails tsid "
            + "INNER JOIN tbl_Stock ts ON tsid.StockID = ts.StockID WHERE tsid.Status = 'Issued' "
            + " UNION ALL "
            + "SELECT tsid.StockID, tsid.CreatedDate, (tsid.DisposalNo)as TransNum, (0) as ReceiptQty, (tsid.Quantity) as IssueQty, ('') as IssueOffice, ('Minus') as AddOrMinus,  ('Disposal') as TransType  FROM tbl_StockDisposalDetails tsid "
            + "INNER JOIN tbl_Stock ts ON tsid.StockID = ts.StockID WHERE tsid.Status = 'Disposed' "
            + "UNION ALL "
            + "SELECT tsid.StockID, tsid.CreatedDate, (tsid.RequestNo)as TransNum, (0) as ReceiptQty, (tsid.Quantity) as IssueQty, (SELECT WarehouseName FROM tbl_Warehouse WHERE WarehouseID = (SELECT SiteID FROM tbl_User WHERE Username = tsid.CreatedBy))as IssueOffice, ('Minus') as AddOrMinus, ('Request') as TransType  FROM tbl_StockRequestDetails tsid "
            + "INNER JOIN tbl_Stock ts ON tsid.StockID = ts.StockID  WHERE tsid.Status = 'Issued' "
            + "UNION ALL "
            + "SELECT (SELECT StockID FROM tbl_StocksIssued WHERE RecID = tsid.IssuedRecID), tsid.CreatedDate, (tsid.ReturnNo)as TransNum, (0) as ReceiptQty, (tsid.ReturnQuantity) as IssueQty, ('') as IssueOffice, ('Add') as AddOrMinus, ('Return') as TransType  FROM tbl_StockReturnDetails tsid "
            + "WHERE tsid.Status = 'Returned' "
            + ")ad "
            + "WHERE ad.StockID = '" + StockID + "' "
            //+ "AND MONTH(ad.CreatedDate) = '" + (SelectedMonth + 1) + "' "
            //+ "AND YEAR(ad.CreatedDate) = '" + SelectedYear + "' "
            + "ORDER BY ad.CreatedDate";

            DataTable dt = DataAccess.MicrosoftDB.GetDataTable(qry, CommandType.Text, conString);

            return Control.MyConverter.ConvertDataTabletoJSON(dt);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public static string GetStockList()
    {
        try
        {
            StockCardBll z = new StockCardBll();
            PropertyMasterlistBll x = new PropertyMasterlistBll();

            string Filter = "";
            DataTable dtFilter = x.GetActiveFilter("StockCard" , Common.ConnString, Common.UserName, "1");
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

            DataTable dt = z.GetStockList(Common.ConnString, Filter);

            return Control.MyConverter.ConvertDataTabletoJSON(dt);
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public void GetFlterTable()
    {
        try
        {
            drpFilterBy.Items.Clear();
            drpFilterBy.Items.Add(" Select Filter ");
            drpFilterBy.Items.Add("ItemCode");
            drpFilterBy.Items.Add("Description");
            drpFilterBy.Items.Add("ClassificationName");
            drpFilterBy.Items.Add("CategoryName");
            drpFilterBy.Items.Add("BrandName");
            drpFilterBy.Items.Add("ModelName");
        }
        catch
        {
        }
    }

    //filter
    [WebMethod]
    public static string GetFilter(string ReportName)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetFilter(ReportName, Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.Logic = dt.Rows[i]["Logic"].ToString();
                    x.FilterBy = dt.Rows[i]["FilterBy"].ToString();
                    x.Operator = dt.Rows[i]["Operator"].ToString();
                    x.Values = dt.Rows[i]["Values"].ToString();
                    x.IsActive = "<label hidden>" + dt.Rows[i]["isActive"].ToString() + "</label><img onclick=\"GetActive(" + dt.Rows[i]["RecID"] + ")\"" + ((bool.Parse(dt.Rows[i]["isActive"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Filter Information' onclick=\"GetFilterID('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["Logic"] + "','" + dt.Rows[i]["FilterBy"] + "','" + dt.Rows[i]["Operator"] + "','" + dt.Rows[i]["Values"] + "')\"><span class='fa fa-edit'></span></button> "
                        + " <button type='button' class='btn btn-xs btn-danger btn_Delete' data-toggle='tooltip' title='Delete Filter' recid='" + dt.Rows[i]["RecID"] + "' onclick=\"DeleteFilter('" + dt.Rows[i]["RecID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

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
    public static int GetActive(string id)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.GetActive(id, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [WebMethod]
    public static int DeleteFilter(string id)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.DeleteFilter(id, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [WebMethod]
    public static int SaveFilter(string ReportName, string Logic, string Filter, string Operator, string Value)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.SaveFilter(ReportName, Common.UserName, Logic, Filter, Operator, Value, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }


    [WebMethod]
    public static int UpdateFilter(string RecId, string ReportName, string Logic, string Filter, string Operator, string Value)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.UpdateFilter(RecId, ReportName, Common.UserName, Logic, Filter, Operator, Value, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }



    [WebMethod]
    public static string GetAdvancefilterperUser(string ReportID, string trx)
    {
        //Console.WriteLine("getADVFilter");

        AdvanceFilterBll x = new AdvanceFilterBll();
        List<AdvanceFilter> AF = new List<AdvanceFilter>();
        var RET = string.Empty;
        try
        {
            DataTable dt = x.GetAdvancefilterperUser(ReportID, trx, Common.ConnString, Common.UserID);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdvanceFilter y = new AdvanceFilter();
                    var btnclass = string.Empty;
                    y.Logic = dt.Rows[i]["Logic"].ToString();
                    y.FilterBy = dt.Rows[i]["FilterBy"].ToString();
                    y.Operator = dt.Rows[i]["operator"].ToString();
                    y.Values = dt.Rows[i]["Values"].ToString();
                    if (dt.Rows[i]["isUse"].ToString() == "True")
                    {
                        btnclass = "btn-success";
                    }
                    else
                    {
                        btnclass = "btn-secondary";
                    }
                    y.ReportID = "<center><button class='btn btn-xs " + btnclass + "' id='UsedFilter" + dt.Rows[i]["RecID"] + "' type='button' data-toggle='tooltip'  title='Use Filter' onclick=\"ModADVfilter('" + dt.Rows[i]["RecID"] + "','USE')\"><span class=\"	fa fa-check-circle\" color: White;\"></span></button> "
                        + "<button class='btn btn-xs btn-danger' type='button' data-toggle='tooltip'  title='Delete Filter' onclick=\"ModifyAdvFilter('" + dt.Rows[i]["RecID"] + "','DELETE')\"><span class=\"fa fa-trash\" color: White;\"></span></button> ";
                    AF.Add(y);
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = Int32.MaxValue;
                RET = jss.Serialize(AF);

                return RET;
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


}